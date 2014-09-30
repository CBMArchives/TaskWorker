Public Class TypeInfo
    Public Property TypeName As String
    Public Property AssemblyPath As String
    Public Property ClassName As String
    Public Property ID As Integer
    Public Property IsActive As Boolean

    Public Shared Function GetTypeInfo(ByVal appConnectionStr As String, ByVal TypeName As String) As TypeInfo
        'RaiseEvent LogItem("Getting assembly path from database: " & procRunName & " for typename: " & TypeName, twk_LogLevels.Info_Detail, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
        Dim result As New TypeInfo

        Dim dbConn As New SqlClient.SqlConnection(appConnectionStr)
        Dim dbComm As New SqlClient.SqlCommand("taskworker_GetTypeInfo", dbConn)
        dbComm.CommandType = CommandType.StoredProcedure
        dbComm.Parameters.Add(New SqlClient.SqlParameter("@typename", TypeName))


        Dim da As New SqlClient.SqlDataAdapter(dbComm)
        dbConn.Open()

        Dim dt As New DataTable
        da.Fill(dt)
        For Each dr As DataRow In dt.Rows
            result.AssemblyPath = dr("AssemblyPath")
            result.ClassName = dr("ClassName")
            result.ID = dr("AssemblyID")
            result.IsActive = dr("IsActive")
            result.TypeName = TypeName
        Next
        dbConn.Close()
        dbConn.Dispose()
        dbComm.Dispose()


        Return result
    End Function
End Class
