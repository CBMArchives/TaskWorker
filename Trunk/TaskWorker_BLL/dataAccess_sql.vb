Public Class dataAccess_sql
    Public Event SQLError(ByVal Msg As String)

    Public Function GetDataReader_NoParameters(ByVal dbConnStr As String, ByVal spName As String) As SqlClient.SqlDataReader
        Dim result As SqlClient.SqlDataReader = Nothing

        Dim dbConn As New SqlClient.SqlConnection(dbConnStr)
        Try
            dbConn.Open()
        Catch ex As Exception
            dbConn.Dispose()
            Return Nothing
        End Try

        Dim dbComm As New SqlClient.SqlCommand(spName, dbConn)
        dbComm.CommandType = CommandType.StoredProcedure

        Try
            result = dbComm.ExecuteReader()
        Catch ex As Exception
            dbConn.Dispose()
            Return Nothing
        Finally
            dbConn.Close()
            dbConn.Dispose()
        End Try

        Return result

    End Function


    Public Function GetDatatable(ByVal dbConnStr As String, ByVal spName As String) As DataTable
        Dim result As New DataTable

        Dim dbConn As New SqlClient.SqlConnection(dbConnStr)
        Try
            dbConn.Open()
        Catch ex As Exception
            dbConn.Dispose()
            Return result
        End Try

        Dim da As New SqlClient.SqlDataAdapter(spName, dbConn)
        da.SelectCommand.CommandType = CommandType.StoredProcedure

        Try
            da.Fill(result)
        Catch ex As Exception
            dbConn.Dispose()
            Return result
        Finally
            dbConn.Close()
            dbConn.Dispose()
        End Try

        Return result
    End Function
    Public Function GetDatatable(ByVal dbConnStr As String, ByVal spName As String, ByVal Params() As SqlClient.SqlParameter) As DataTable

        Dim result As New DataTable

        Dim dbConn As New SqlClient.SqlConnection(dbConnStr)
        Try
            dbConn.Open()
        Catch ex As Exception
            dbConn.Dispose()
            RaiseEvent SQLError(ex.Message)
            Return result
        End Try

        Dim da As New SqlClient.SqlDataAdapter(spName, dbConn)
        da.SelectCommand.CommandType = CommandType.StoredProcedure
        For Each p In Params
            da.SelectCommand.Parameters.Add(p)
        Next

        Try
            da.Fill(result)
        Catch ex As Exception
            dbConn.Dispose()
            RaiseEvent SQLError(ex.Message)
            Return result
        Finally
            dbConn.Close()
            dbConn.Dispose()
        End Try

        Return result
    End Function

    Public Function GetScalarValue(ByVal dbConnStr As String, ByVal spName As String, ByVal IsStoredProcedure As Boolean, Optional Params() As SqlClient.SqlParameter = Nothing) As String

        Dim result As String = String.Empty

        Dim dbConn As New SqlClient.SqlConnection(dbConnStr)
        Try
            dbConn.Open()
        Catch ex As Exception
            dbConn.Dispose()
            RaiseEvent SQLError(ex.Message)
            Return result
        End Try

        Dim dbComm As New SqlClient.SqlCommand(spName, dbConn)
        If IsStoredProcedure = True Then
            dbComm.CommandType = CommandType.StoredProcedure
            For Each p In Params
                dbComm.Parameters.Add(p)
            Next
        Else
            dbComm.CommandType = CommandType.Text
        End If
        Try
            result = dbComm.ExecuteScalar()
        Catch ex As Exception
            result = ""
        End Try
        Return result

    End Function
    Public Function GetDataset_NoParameters(ByVal dbConnStr As String, ByVal spName As String) As DataSet
        Dim result As New DataSet

        Dim dbConn As New SqlClient.SqlConnection(dbConnStr)
        Try
            dbConn.Open()
        Catch ex As Exception
            dbConn.Dispose()
            Return result
        End Try

        Dim da As New SqlClient.SqlDataAdapter(spName, dbConn)
        da.SelectCommand.CommandType = CommandType.StoredProcedure
        da.AcceptChangesDuringFill = True
        da.SelectCommand.CommandTimeout = 240

        Try
            da.Fill(result)
        Catch ex As Exception
            dbConn.Dispose()
            Return result
        Finally
            dbConn.Close()
            dbConn.Dispose()
        End Try

        Return result
    End Function

    Public Function ExecSQL(ByVal dbConnStr As String, ByVal SQLText As String) As twk_SQLResults
        Dim result As New DataSet

        Dim dbConn As New SqlClient.SqlConnection(dbConnStr)
        Try
            dbConn.Open()
        Catch ex As Exception
            dbConn.Dispose()
            Return twk_SQLResults.SQLError
        End Try


        Try
            Dim dbcomm As New SqlClient.SqlCommand(SQLText, dbConn)
            dbcomm.CommandType = CommandType.Text
            dbcomm.CommandTimeout = 240
            dbcomm.ExecuteNonQuery()
        Catch ex As Exception
            dbConn.Dispose()
            Return twk_SQLResults.SQLError
        Finally
            dbConn.Close()
            dbConn.Dispose()
        End Try

        Return twk_SQLResults.Complete
    End Function
End Class
