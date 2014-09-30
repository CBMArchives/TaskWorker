Imports System.Reflection
<Serializable()> Public Class actionGeneral
    Public Event Completed(ByVal Sender As Object, ByVal UpdateStatus As String, ByVal Comment As String)
    Public Event Process_Error(ByVal Sender As Object, ByVal Msg As String, ByVal Comment As String)
    Public Property LogToFile As Boolean = False
    Public Property LogFilepath As String
    Public Property TimeToLive_sec As Integer = 120
    Public Property ManagedThreadId As Integer
    Public Property log_by_action_attribute As String = String.Empty


    Public ProcID As Integer
    Private _Status As String
    Private _StopProcess As Boolean = False

    Public Property StopProcess As Boolean
        Set(value As Boolean)
            _StopProcess = value

            If value = True Then
                _Status = "Stopping..."
            End If
        End Set
        Get
            Return _StopProcess
        End Get
    End Property
    Public ReadOnly Property Status As String
        Get
            Return _Status
        End Get
    End Property

    Public Event EventLog(ByRef sender As TaskWorker_BLL.actionGeneral, ByVal Msg As String, ByVal Level As twk_LogLevels, ByVal exMethod As MethodBase)
    Public Event EventLog_MessageOnly(ByVal Msg As String)
    Public Event SQLLog(ByVal Sender As Object, ByVal TLog As TaskLog)

    Public Sub New()

    End Sub

    Public Property ActionXML As XElement


    Public Overridable Sub Start()
        _Status = "Started"
        RaiseEvent EventLog(Me, _Status, twk_LogLevels.Info_Detail, MethodBase.GetCurrentMethod)
    End Sub

    Public Sub LogItem(ByVal Msg As String, ByVal Level As twk_LogLevels, ByVal exMethod As MethodBase)
        RaiseEvent EventLog(Me, Msg, Level, exMethod)
        RaiseEvent EventLog_MessageOnly(Msg)
    End Sub
    Public Sub ProcessError(ByVal UpdateStatus As String, ByVal Msg As String)
        _Status = "ERROR"

        RaiseEvent Process_Error(Me, UpdateStatus, Msg)
    End Sub
    Public Sub ProcessComplete(ByVal UpdateStatus As String, ByVal Msg As String)
        _Status = "Complete"
        RaiseEvent Completed(Me, UpdateStatus, Msg)
        MyBase.Finalize()

    End Sub

    Public Sub LogSQL_Add(ByVal TLog As TaskLog)
        RaiseEvent SQLLog(Me, TLog)
    End Sub


    Public Sub LoadParameters(ByVal obj As Object)
        'This will autoload parameters
        Dim n As String = String.Empty
        If Me.ActionXML.GetDefaultNamespace.NamespaceName = "" Then

        Else
            n = "{" & Me.ActionXML.GetDefaultNamespace.NamespaceName & "}"
        End If


        'TOD:Need to try catch errors here!!!!!!!!


        If Me.ActionXML.Elements(n & "parameters").Count = 1 Then
            For Each xParam In Me.ActionXML.Element(n & "parameters").Elements(n & "parameter")
                If (xParam.Attributes("name").Count = 1 And xParam.Attributes("value").Count = 1) Then
                    Dim paramName As String = xParam.Attribute("name").Value
                    Dim paramValue As String = xParam.Attribute("value").Value
                    Dim p1 As Reflection.PropertyInfo = obj.GetType().GetProperty(paramName)
                    If IsNothing(p1) = False Then
                        Try
                            Select Case p1.PropertyType.Name.ToLower
                                Case "boolean"
                                    p1.SetValue(obj, Boolean.Parse(paramValue), Nothing)
                                Case "string"
                                    p1.SetValue(obj, paramValue, Nothing)
                                Case "int32"
                                    p1.SetValue(obj, System.Convert.ToInt32(paramValue), Nothing)
                                Case "int64"
                                    p1.SetValue(obj, System.Convert.ToInt64(paramValue), Nothing)
                                Case "decimal"
                                    p1.SetValue(obj, System.Convert.ToDecimal(paramValue), Nothing)
                                Case "datetime"
                                    p1.SetValue(obj, System.Convert.ToDateTime(paramValue), Nothing)
                                Case "double"
                                    p1.SetValue(obj, System.Convert.ToDouble(paramValue), Nothing)
                                Case "single"
                                    p1.SetValue(obj, System.Convert.ToSingle(paramValue), Nothing)
                                Case "char"
                                    p1.SetValue(obj, System.Convert.ToChar(paramValue), Nothing)
                                Case Else
                                    p1.SetValue(obj, paramValue, Nothing)
                            End Select
                        Catch ex As Exception
                            Throw New Exception("Error loading parameter: " & paramName & " with value of [" & paramValue & "] " & ex.Message, ex)
                        End Try


                    End If
                End If
            Next
        End If

        If Me.ActionXML.Elements(n & "settings").Count = 1 Then
            For Each xParam In Me.ActionXML.Element(n & "settings").Elements(n & "setting")
                If (xParam.Attributes("name").Count = 1 And xParam.Attributes("value").Count = 1) Then
                    Dim paramName As String = xParam.Attribute("name").Value
                    Dim paramValue As String = xParam.Attribute("value").Value
                    Dim p1 As Reflection.PropertyInfo = obj.GetType().GetProperty(paramName)
                    If IsNothing(p1) = False Then
                        p1.SetValue(obj, paramValue, Nothing)
                    End If
                End If

            Next
        End If

        If Me.ActionXML.Elements(n & "connectionstrings").Count = 1 Then
            For Each xParam In Me.ActionXML.Element(n & "connectionstrings").Elements(n & "connectionstring")
                If (xParam.Attributes("name").Count = 1 And xParam.Attributes("connectionstring").Count = 1) Then
                    Dim paramName As String = xParam.Attribute("name").Value
                    Dim paramValue As String = xParam.Attribute("connectionstring").Value
                    Dim p1 As Reflection.PropertyInfo = obj.GetType().GetProperty(paramName)
                    If IsNothing(p1) = False Then
                        p1.SetValue(obj, paramValue, Nothing)
                    End If
                End If

            Next
        End If

    End Sub
End Class
