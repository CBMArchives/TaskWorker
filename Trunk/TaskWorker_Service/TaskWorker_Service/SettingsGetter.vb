Public Class SettingsGetter

    Shared Function GetApplicationConfiguration(ByVal connStr As String, ByVal ApplicationCode As String, ByVal Version As String, ByVal AppSection As String) As Xml.Linq.XElement
        Dim dbConn As New SqlClient.SqlConnection(connStr)
        Dim dbComm As New SqlClient.SqlCommand("cbm_GetApplicationConfiguration", dbConn)
        dbComm.Connection.Open()
        dbComm.CommandType = CommandType.StoredProcedure
        dbComm.Parameters.Add(New SqlClient.SqlParameter("@AppCode", ApplicationCode))
        dbComm.Parameters.Add(New SqlClient.SqlParameter("@AppVersion", Version))
        dbComm.Parameters.Add(New SqlClient.SqlParameter("@AppSection", AppSection))

        Dim c As Xml.Linq.XElement = XElement.Parse(dbComm.ExecuteScalar)

        dbComm.Dispose()
        dbConn.Dispose()
        Return c
    End Function




End Class
