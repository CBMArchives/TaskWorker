Imports System.Reflection

Public Class TaskLog
    Public Property ProcedureID As Integer
    Public Property ActionName As String
    Public Property LogType As Integer
    Public Property LogLevel As Integer
    Public Property Description As String
    Public Property LogDate As DateTime
    Public Property TrackingType As Integer
    Public Property TrackingValue As String

    Public Shared Sub ProcessLogItem(ByVal Msg As String, ByVal LogLevel As twk_LogLevels, LogToFile As Boolean, LogFilePath As String, exMethod As MethodBase, ByVal ItemsToLog As List(Of twk_LogLevels), ByVal loggingFormat As String, Optional ByVal loggingCSS As String = "default")



        Dim LogText As String
        Dim consoleText As String
        Select Case loggingFormat.ToUpper
            Case "HTML"
                Dim fMSG As String = Msg
                fMSG = fMSG.Replace(Chr(10), "")
                fMSG = fMSG.Replace(Chr(13), "")
                fMSG = fMSG.Replace(Chr(0), "")

                Dim divXML = <div class=<%= exMethod.DeclaringType.Name %>>
                                 <div class=<%= LogLevel.ToString %>>
                                     <span class="timespan"><%= Now.ToString("yyyyMMdd HH:mm:ss") %></span><span class="msg"><%= fMSG %></span>
                                 </div>
                             </div>
                LogText = divXML.ToString
            Case Else
                LogText = Now.ToString("yyyyMMdd HH:mm:ss") & " [" & exMethod.DeclaringType.Name & "] " & " - " & Msg
        End Select

        consoleText = Now.ToString("yyyyMMdd HH:mm:ss") & " [" & exMethod.DeclaringType.Name & "] " & " - " & Msg

        Select Case LogLevel
            Case twk_LogLevels.ProcessError
                Console.ForegroundColor = ConsoleColor.Red
            Case twk_LogLevels.Warning
                Console.ForegroundColor = ConsoleColor.DarkMagenta
            Case twk_LogLevels.Info_Success
                Console.ForegroundColor = ConsoleColor.Blue
            Case twk_LogLevels.DebugInfo_L1, twk_LogLevels.DebugInfo_L2, twk_LogLevels.DebugInfo_L3, twk_LogLevels.DebugInfo_L4
                Console.ForegroundColor = ConsoleColor.DarkYellow
            Case Else
                Console.ForegroundColor = ConsoleColor.Yellow

        End Select
        Console.WriteLine(consoleText)

        If ItemsToLog.Contains(LogLevel) Then
            If LogToFile = True And LogFilePath <> String.Empty Then
                Try
                    LogFilePath &= "\" & Now.ToString("yyyyMM")
                    If IO.Directory.Exists(LogFilePath) = False Then
                        IO.Directory.CreateDirectory(LogFilePath)
                    End If

                    Dim fileExt As String = String.Empty
                    Select Case loggingFormat.ToUpper
                        Case "HTML"
                            fileExt = ".html"
                            If IO.File.Exists(LogFilePath & "\" & Now.ToString("yyyyMMdd") & fileExt) = False Then
                                Dim logFileHDR As New IO.StreamWriter(LogFilePath & "\" & Now.ToString("yyyyMMdd") & fileExt, True)
                                logFileHDR.WriteLine(<link href=<%= loggingCSS %> rel="stylesheet" type="text/css"></link>)
                                '"<link href="Styles/logFiles.css" rel="stylesheet" type="text/css" />"
                                logFileHDR.Close()
                            End If

                        Case Else
                            fileExt = ".log"
                    End Select

                    Dim logFile As New IO.StreamWriter(LogFilePath & "\" & Now.ToString("yyyyMMdd") & fileExt, True)
                    logFile.WriteLine(LogText)
                    logFile.Close()
                Catch ex As Exception
                    Console.WriteLine("Error while logging: " & LogFilePath & vbNewLine & ex.Message)
                End Try
            End If
        End If



    End Sub
End Class
