using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RunProcedureManager.Models
{
    public class Data
    {
        public string
            procedureId,
            name,
            source,
            priority,
            currentStatus,
            procsXml,
            maxRetries,
            isComplete,
            startedOn,
            completedOn,
            errorCount,
            processingBy,
            isProcessing,
            hostName,
            ipAddress,
            comments,
            addedBy,
            addedOn,
            trackingNumber,
            trackingNumberType;

        private Data() { }
        public static List<Data> get(Func<Data, bool> filter = null)
        {
            filter = filter ?? (data => true);
            using (var sqlConnection = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            using (var sqlCommand = new System.Data.SqlClient.SqlCommand("select * from cbm_RunProcedures", sqlConnection))
            {
                sqlConnection.Open();
                var sqlReader = sqlCommand.ExecuteReader();
                Func<string, System.Type, string> getVal = (name, type) =>
                    {
                        var val = sqlReader[name];
                        return val is System.DBNull ? null : System.Convert.ChangeType(val, type).ToString();
                    };
                var list = new List<Data>();
                while (sqlReader.Read())
                {
                    var data = new Data {
                        procedureId = getVal("procedureID", typeof(int)),
                        name = getVal("name", typeof(string)),
                        source = getVal("source", typeof(string)),
                        priority = getVal("priority", typeof(int)),
                        currentStatus = getVal("currentstatus", typeof(string)),
                        procsXml = getVal("procsXML", typeof(string)),
                        maxRetries = getVal("maxretrys", typeof(int)),
                        isComplete = getVal("IsComplete", typeof(bool)),
                        startedOn = getVal("StartedOn", typeof(DateTime)),
                        completedOn = getVal("CompletedOn", typeof(DateTime)),
                        errorCount = getVal("ErrorCount", typeof(int)),
                        processingBy = getVal("ProcessingBy", typeof(string)),
                        isProcessing = getVal("IsProcessing", typeof(bool)),
                        hostName = getVal("HostName", typeof(string)),
                        ipAddress = getVal("IPAddress", typeof(string)),
                        comments = getVal("Comments", typeof(string)),
                        addedBy = getVal("AddedBy", typeof(string)),
                        addedOn = getVal("AddedOn", typeof(string)),
                        trackingNumber = getVal("TrackingNumber", typeof(string)),
                        trackingNumberType = getVal("TrackingNumberType", typeof(int))
                    };

                    if (filter(data))
                    {
                        list.Add(data);
                    }
                }
                return list;
            }
        }
        public static void put(params Data[] list)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            using (var command = new System.Data.SqlClient.SqlCommand(
                @"update cbm_RunProcedures
                set
                    name = @name,
                    source = @source,
                    priority = @priority,
                    currentstatus = @currentStatus,
                    procsXML = @procsXml,
                    maxretrys = @maxRetries,
                    IsComplete = @isComplete,
                    StartedOn = @startedOn,
                    CompletedOn = @completedOn,
                    ErrorCount = @errorCount,
                    ProcessingBy = @processingBy,
                    IsProcessing = @isProcessing,
                    HostName = @hostName,
                    IPAddress = @ipAddress,
                    Comments = @comments,
                    AddedBy = @addedBy,
                    AddedOn = @addedOn,
                    TrackingNumber = @trackingNumber,
                    TrackingNumberType = @trackingNumberType
                where procedureID = @procedureId", connection))
            {
                command.Parameters.Add("name", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("source", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("priority", System.Data.SqlDbType.Int);
                command.Parameters.Add("currentStatus", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("procsXml", System.Data.SqlDbType.Xml);
                command.Parameters.Add("maxRetries", System.Data.SqlDbType.Int);
                command.Parameters.Add("isComplete", System.Data.SqlDbType.Bit);
                command.Parameters.Add("startedOn", System.Data.SqlDbType.DateTime);
                command.Parameters.Add("completedOn", System.Data.SqlDbType.DateTime);
                command.Parameters.Add("errorCount", System.Data.SqlDbType.Int);
                command.Parameters.Add("processingBy", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("isProcessing", System.Data.SqlDbType.Bit);
                command.Parameters.Add("hostName", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("ipAddress", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("comments", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("addedBy", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("addedOn", System.Data.SqlDbType.DateTime);
                command.Parameters.Add("trackingNumber", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("trackingNumberType", System.Data.SqlDbType.Int);
                command.Parameters.Add("procedureId", System.Data.SqlDbType.Int);

                connection.Open();

                foreach (var data in list)
                {
                    command.Parameters["name"].Value = data.name ?? (dynamic)System.DBNull.Value;
                    command.Parameters["source"].Value = data.source ?? (dynamic)System.DBNull.Value;
                    command.Parameters["priority"].Value = data.priority == null ? (dynamic)System.DBNull.Value : System.Convert.ToInt64(data.priority);
                    command.Parameters["currentStatus"].Value = data.currentStatus ?? (dynamic)System.DBNull.Value;
                    command.Parameters["procsXml"].Value = data.procsXml ?? (dynamic)System.DBNull.Value;
                    command.Parameters["maxRetries"].Value = data.maxRetries == null ? (dynamic)System.DBNull.Value : System.Convert.ToInt64(data.maxRetries);
                    command.Parameters["isComplete"].Value = data.isComplete == null ? (dynamic)System.DBNull.Value : System.Convert.ToBoolean(data.isComplete);
                    command.Parameters["startedOn"].Value = data.startedOn == null ? (dynamic)System.DBNull.Value : System.DateTime.Parse(data.startedOn);
                    command.Parameters["completedOn"].Value = data.completedOn == null ? (dynamic)System.DBNull.Value : System.DateTime.Parse(data.completedOn);
                    command.Parameters["errorCount"].Value = data.errorCount == null ? (dynamic)System.DBNull.Value : System.Convert.ToInt64(data.errorCount);
                    command.Parameters["processingBy"].Value = data.processingBy ?? (dynamic)System.DBNull.Value;
                    command.Parameters["isProcessing"].Value = data.isProcessing == null ? (dynamic)System.DBNull.Value : System.Convert.ToBoolean(data.isProcessing);
                    command.Parameters["hostName"].Value = data.hostName ?? (dynamic)System.DBNull.Value;
                    command.Parameters["ipAddress"].Value = data.ipAddress ?? (dynamic)System.DBNull.Value;
                    command.Parameters["comments"].Value = data.comments ?? (dynamic)System.DBNull.Value;
                    command.Parameters["addedBy"].Value = data.addedBy ?? (dynamic)System.DBNull.Value;
                    command.Parameters["addedOn"].Value = data.addedOn == null ? (dynamic)System.DBNull.Value : System.DateTime.Parse(data.addedOn);
                    command.Parameters["trackingNumber"].Value = data.trackingNumber ?? (dynamic)System.DBNull.Value;
                    command.Parameters["trackingNumberType"].Value = data.trackingNumber == null ? (dynamic)System.DBNull.Value : System.Convert.ToInt64(data.trackingNumberType);
                    command.Parameters["procedureId"].Value = System.Convert.ToInt64(data.procedureId);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}