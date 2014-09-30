Imports TaskWorker_BLL
Imports System.Reflection


Public Class task_srvc_main

    Public WithEvents twBase As taskworker_base
    Public HasError As Boolean = False
    Public Property threadedItems As New threadTrackingItems
    Public Property errCount As Integer = 0
    Public Property errMaxCount As Integer = 5
    Public Property ItemsToLog As New Collections.Generic.List(Of String)
    Private LogText As String

    'Private Jobs As New Generic.List(Of Job)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByRef taskworkerBase As taskworker_base)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        twBase = taskworkerBase

        ItemsToLog.AddRange(Split(twBase.loggingValues, ","))


        AddHandler twBase.LogItem, AddressOf twBase_LogItem
    End Sub
    Private Function ReadJobsFromFiles(ByVal procsXML_path As String) As Generic.List(Of Job)
        Dim result As New List(Of Job)

        Dim DoesExist As Boolean = False

        If IO.File.Exists(twBase.procsXML_path) = False Then
            DoesExist = True
        End If
        If IO.Directory.Exists(DoesExist) Then
            DoesExist = True
        End If


        If DoesExist = False Then
            twBase_LogItem("CONFIGURATION FILE(S) NOT FOUND: " & twBase.procsXML_path, twk_LogLevels.ProcessError, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
            Return result
        End If

        Dim fa As IO.FileAttributes = IO.File.GetAttributes(procsXML_path)

        If (fa.HasFlag(IO.FileAttributes.Directory)) Then
            For Each procsFile In IO.Directory.GetFiles(procsXML_path, "*.xml")
                Dim nJob As New Job
                nJob.AddedBy = "TWK-" & My.Computer.Name
                nJob.AddedOn = Now
                nJob.Comments = ""
                nJob.currentstatus = "pending"
                nJob.ErrorCount = 0
                nJob.name = ""
                nJob.priority = 1
                nJob.ProcID = 0
                nJob.source = ""
                nJob.procsXML = XElement.Parse(IO.File.ReadAllText(procsFile))
                nJob.TrackingNumber = ""
                nJob.TrackingNumberType = 0
                result.Add(nJob)
            Next
        Else
            Dim nJob As New Job
            nJob.AddedBy = "TWK-" & My.Computer.Name
            nJob.AddedOn = Now
            nJob.Comments = ""
            nJob.currentstatus = "pending"
            nJob.ErrorCount = 0
            nJob.name = ""
            nJob.priority = 1
            nJob.ProcID = 0
            nJob.source = ""
            nJob.procsXML = XElement.Parse(IO.File.ReadAllText(procsXML_path))
            nJob.TrackingNumber = ""
            nJob.TrackingNumberType = 0
            result.Add(nJob)
        End If


        Return result
    End Function

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.





        If (System.Diagnostics.EventLog.SourceExists("TaskWorker")) = False Then
            EventLog.CreateEventSource("TaskWorker", "Application")
        End If






        System.Diagnostics.EventLog.WriteEntry("TaskWorker", "AT 329")



        Dim serviceThread As New Threading.Thread(AddressOf ServiceThreader)
        serviceThread.Start()



    End Sub

    Public Sub ServiceThreader()


        Dim Jobs As New Generic.List(Of Job)


        If twBase.procsXML_path.Trim <> "" Then
            twBase_LogItem("Configuration will run from file/directory: " & twBase.procsXML_path, twk_LogLevels.Info_Detail, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
            Jobs = ReadJobsFromFiles(twBase.procsXML_path)
            'If IO.File.Exists(twBase.procsXML_path) = False Then
            '    twBase_LogItem("CONFIGURATION FILE(S) NOT FOUND: " & twBase.procsXML_path, twk_LogLevels.ProcessError, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
            'Else
        End If


        Try
            System.Diagnostics.EventLog.WriteEntry("TaskWorker", "Starting.... Getting Initial List Of Jobs[2]")
            Jobs = twBase.GetNextJobs
        Catch ex As Exception
            System.Diagnostics.EventLog.WriteEntry("TaskWorker", "Error with getting job list: " & ex.Message)
        End Try
        System.Diagnostics.EventLog.WriteEntry("TaskWorker", "AT 301")
        Try
            If Jobs.Count = 0 Then
                Try
                    twBase_LogItem("Zero jobs loaded.  Nothing to do.", twk_LogLevels.Info_Detail, twk_LogTo.sql_and_file_and_console, MethodBase.GetCurrentMethod)
                Catch ex As Exception
                    System.Diagnostics.EventLog.WriteEntry("TaskWorker", "Error [720] " & ex.Message)
                End Try

            Else
                Try
                    twBase_LogItem("Found [" & Jobs.Count & "] jobs for TASKWORKER to process.", twk_LogLevels.Info_Detail, twk_LogTo.sql_and_file_and_console, MethodBase.GetCurrentMethod)
                Catch ex As Exception
                    System.Diagnostics.EventLog.WriteEntry("TaskWorker", "Error [721] " & ex.Message)
                End Try

                ProcesseJobs(Jobs)
                Jobs.Clear()
            End If
        Catch ex As Exception
            System.Diagnostics.EventLog.WriteEntry("TaskWorker", "Error [801] " & ex.Message)
        End Try

        While twBase.Recycle

            Jobs = twBase.GetNextJobs
            If Jobs.Count = 0 Then
                twBase_LogItem("Zero jobs loaded.  Nothing to do.", twk_LogLevels.Info_Detail, twk_LogTo.sql_and_file_and_console, MethodBase.GetCurrentMethod)
            Else

                ProcesseJobs(Jobs)
                twBase_LogItem("Managing [" & threadedItems.Count & "] job threads", twk_LogLevels.DebugInfo_L4, twk_LogTo.sql_and_file_and_console, MethodBase.GetCurrentMethod)

                Jobs.Clear()
            End If


        End While
    End Sub

    Public Sub ProcesseJobs(ByVal Jobs As Collections.Generic.List(Of Job))

        System.Diagnostics.EventLog.WriteEntry("TaskWorker", "AT 405")
        For Each jobItem As Job In Jobs
            HasError = False 'Reset on new job

            twBase_LogItem("Loading job#" & jobItem.ProcID, twk_LogLevels.Info_Detail, False, MethodBase.GetCurrentMethod)

            For Each xProc As XElement In jobItem.procsXML.Elements("procedure")
                '  'exit job if errors  just remaining procedure
                If HasError = True Then
                    Exit For
                End If

                'check for runorder
                Dim EachHasRunOrder As Boolean = True
                For Each xAction As XElement In xProc.Element("actions").Elements("action")
                    If xAction.Attributes("runorder").Count = 0 Then
                        EachHasRunOrder = False
                    End If
                Next


                Dim xActions_result As Collections.Generic.IEnumerable(Of XElement)

                If EachHasRunOrder = True Then
                    xActions_result = xProc.Element("actions").Elements("action").OrderBy(Function(c) c.Attribute("runorder").Value)
                Else
                    xActions_result = xProc.Element("actions").Elements("action")
                End If

                For Each xAction As XElement In xActions_result

                    '  'exit job if errors  just remaining actions
                    If HasError = True Then
                        Exit For
                    End If

                    'Dim assemblyPath As String = xAction.Attribute("assemblypath").Value
                    'Dim assemblyName As String = xAction.Attribute("assemblyname").Value
                    'Dim typename As String = xAction.Attribute("typename").Value

                    Dim typename As String = xAction.Attribute("type").Value
                    Dim tInfo As TypeInfo = twBase.GetTypeInfo(typename)


                    If IsNothing(tInfo) Then

                        ProcessLogItem("ASSEMBLY COULD NOT BE LOADED: " & typename & " tInfo returned NULL", twk_LogLevels.ProcessError, True, My.Settings.logpath, MethodBase.GetCurrentMethod)
                        If My.Settings.RunAsService = False Then
                            ' Console.ReadKey()
                        End If
                        Exit Sub 'End
                    End If

                    ProcessLogItem("Ready to load assembly CLASS: " & tInfo.ClassName, twk_LogLevels.Info_Detail, True, My.Settings.logpath, MethodBase.GetCurrentMethod)
                    If tInfo.IsActive = False Then
                        twBase_LogItem("ASSEMBLY SET TO INACTIVE: " & typename, twk_LogLevels.Warning, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
                        'twBase.Job_SetComplete("SKIPPED", XDocument.Parse("<nodata></nodata>"), Nothing, "ASSEMBLY SET TO INACTIVE: " & typename)
                    Else
                        Dim threadProcess As Boolean
                        If xAction.Attributes("threadprocess").Count = 0 Then
                            threadProcess = False
                        Else
                            threadProcess = Boolean.Parse(xAction.Attribute("threadprocess").Value)
                        End If
                        Dim errMsg As String = String.Empty
                        If IsNothing(tInfo.AssemblyPath) Then
                            errMsg = "ASSEMBLY COULD NOT BE LOADED: " & typename
                            'MsgBox(errMsg, MsgBoxStyle.OkOnly, "Error")
                            Dim LogText As String = Now.ToString("yyyyMMdd hh:mm:ss") & " - " & errMsg
                            ProcessLogItem(errMsg, twk_LogLevels.ProcessError, True, My.Settings.logpath, MethodBase.GetCurrentMethod)
                            If My.Settings.RunAsService = False Then
                                Console.ReadKey()
                            End If
                            Exit Sub 'End
                        Else
                            If tInfo.AssemblyPath.StartsWith(".\") Then
                                tInfo.AssemblyPath = tInfo.AssemblyPath.Replace(".\", My.Application.Info.DirectoryPath & "\")
                            End If
                            If IO.File.Exists(tInfo.AssemblyPath) = False Then
                                errMsg = "File not found! " & tInfo.AssemblyPath
                                Dim LogText As String = Now.ToString("yyyyMMdd hh:mm:ss") & " - " & errMsg
                                ProcessLogItem(errMsg, twk_LogLevels.ProcessError, True, My.Settings.logpath, MethodBase.GetCurrentMethod)
                                If My.Settings.RunAsService = False Then
                                    Console.ReadKey()
                                End If
                                'End
                                Exit Sub
                            End If

                        End If
                        Dim actionClass As TaskWorker_BLL.actionGeneral = Nothing
                        ProcessLogItem("Loading assembly from: [" & tInfo.AssemblyPath & "]", twk_LogLevels.Info_Detail, True, My.Settings.logpath, MethodBase.GetCurrentMethod)

                        Dim nAss As System.Reflection.Assembly
                        Try
                            nAss = System.Reflection.Assembly.LoadFrom(tInfo.AssemblyPath)



                            actionClass = nAss.CreateInstance(tInfo.ClassName)
                            System.Diagnostics.EventLog.WriteEntry("TaskWorker", "AT 481.. nAss.CreateInstance(tInfo.ClassName) [" & tInfo.ClassName & "]" )
                        Catch ex As Exception
                            errMsg = "ERROR loading assembly: " & ex.Message
                            Console.WriteLine(errMsg)
                            System.Diagnostics.EventLog.WriteEntry("TaskWorker", errMsg)
                            ProcessLogItem(errMsg, twk_LogLevels.ProcessError, True, My.Settings.logpath, MethodBase.GetCurrentMethod)


                            If My.Settings.RunAsService = False Then
                                Console.WriteLine("PRESS A KEY - PROGRAM WILL END")
                                Console.ReadKey()
                            End If
                            Exit Sub
                        End Try


                        '#If DEBUG Then
                        '                        Dim nAss As System.Reflection.Assembly
                        '                        nAss = System.Reflection.Assembly.LoadFrom(tInfo.AssemblyPath)
                        '                        actionClass = nAss.CreateInstance(tInfo.ClassName)
                        '#Else
                        '                        Dim nAss As System.Reflection.Assembly
                        '                        nAss = System.Reflection.Assembly.LoadFile(tInfo.AssemblyPath)
                        '                        actionClass = nAss.CreateInstance(tInfo.ClassName)
                        '#End If

                        If IsNothing(actionClass) Then
                            System.Diagnostics.EventLog.WriteEntry("TaskWorker", "IsNothing is true - AT 482..")
                            Try
                                LogText = "ERROR actionclass: actionClass.ToString  TYPE IS NOT DEFINED!!!"
                            Catch ex As Exception
                                System.Diagnostics.EventLog.WriteEntry("TaskWorker", "BADLOG - AT 482.2: " & ex.Message)
                            End Try

                            System.Diagnostics.EventLog.WriteEntry("TaskWorker", LogText)
                            ProcessLogItem(LogText, twk_LogLevels.ProcessError, True, My.Settings.logpath, MethodBase.GetCurrentMethod)

                            If My.Settings.RunAsService = False Then
                                Console.WriteLine("PRESS A KEY - PROGRAM WILL END")
                                Console.ReadKey()
                            End If
                            'End
                            Exit Sub
                        Else
                            System.Diagnostics.EventLog.WriteEntry("TaskWorker", "AT 483..")
                            Console.WriteLine("actionClass is okay")
                        End If


                        Try
                            System.Diagnostics.EventLog.WriteEntry("TaskWorker", "AT 484..")
                            'actionClass = MyDomain.CreateInstanceFrom(assemblyPath, typename).Unwrap
                            AddHandler actionClass.EventLog, AddressOf action_LogItem
                            AddHandler actionClass.Completed, AddressOf actionProcessCompleted
                            AddHandler actionClass.SQLLog, AddressOf SQLLog
                            AddHandler actionClass.Process_Error, AddressOf actionProcessError
                        Catch ex As Exception
                            LogText = "ERROR adding handlers - " & actionClass.ToString
                            System.Diagnostics.EventLog.WriteEntry("TaskWorker", LogText)
                            ProcessLogItem(LogText, twk_LogLevels.ProcessError, True, My.Settings.logpath, MethodBase.GetCurrentMethod)
                            Exit Sub
                        End Try




                        System.Diagnostics.EventLog.WriteEntry("TaskWorker", "Handlers created AT 485")
                        LogText = Now.ToString("yyyyMMdd hh:mm:ss") & " - " & "Created actionclass: " & actionClass.ToString
                        ProcessLogItem(LogText, twk_LogLevels.Info_Detail, True, My.Settings.logpath, MethodBase.GetCurrentMethod)
                        System.Diagnostics.EventLog.WriteEntry("TaskWorker", LogText)

                        actionClass.ActionXML = xAction
                        actionClass.ProcID = jobItem.ProcID

                        If threadProcess = True Then
                            Dim nT As New Threading.Thread(AddressOf actionClass.Start)

                            Try
                                nT.Start()
                            Catch thrdex As Threading.ThreadAbortException
                                errCount += 1
                                'keep going
                            Catch ex As Exception
                                errCount += 1
                                'keep going
                            End Try

                            actionClass.ManagedThreadId = nT.ManagedThreadId
                            Dim newThreadItem As threadTrackingItem = New threadTrackingItem With {.ManagedThreadId = nT.ManagedThreadId, .StartTime = Now, .TimeToLive_sec = actionClass.TimeToLive_sec}
                            Try
                                threadedItems.Add(nT.ManagedThreadId, newThreadItem)
                            Catch ex As Exception
                                LogText = Now.ToString("yyyyMMdd hh:mm:ss") & " - " & "   ERROR ADDING THREAD:" & nT.ManagedThreadId & " is " & nT.ThreadState.ToString & " "
                                ProcessLogItem(LogText, twk_LogLevels.ProcessError, True, My.Settings.logpath, MethodBase.GetCurrentMethod)
                            End Try

                            LogText = Now.ToString("yyyyMMdd hh:mm:ss") & " - " & "   Thread ID:" & nT.ManagedThreadId & " is " & nT.ThreadState.ToString & " "
                            ProcessLogItem(LogText, twk_LogLevels.DebugInfo_L2, True, My.Settings.logpath, MethodBase.GetCurrentMethod)

                            If My.Settings.WaitOnThreads = False Then
                                '
                            Else
                                'wait for thread to end. do not process another until it is finshed
                                'Dim col_pos As Integer = 0

                                Dim states As New List(Of Integer)
                                states.Add(Threading.ThreadState.Background)
                                states.Add(Threading.ThreadState.Running)
                                states.Add(Threading.ThreadState.WaitSleepJoin)
                                While states.Contains(nT.ThreadState)
                                    'col_pos += 1
                                    Threading.Thread.Sleep(100)
                                    Console.ForegroundColor = ConsoleColor.Cyan
                                    Console.Write("*")
                                    Dim secDiff As Integer = DateDiff(DateInterval.Second, Now, newThreadItem.StartTime)
                                    If secDiff > twBase.TimeToLive Then
                                        nT.Join(1000)
                                    End If
                                End While
                                LogText = Now.ToString("yyyyMMdd hh:mm:ss") & " - " & "* Thread Complete. Number of managed threads: " & threadedItems.Count
                                ProcessLogItem(LogText, twk_LogLevels.DebugInfo_L2, True, My.Settings.logpath, MethodBase.GetCurrentMethod)
                            End If
                        Else
                            Try
                                actionClass.Start()
                            Catch ex As Exception
                                ProcessLogItem("Unhandled ACTION ERROR:" & ex.Message, twk_LogLevels.ProcessError, True, My.Settings.logpath, MethodBase.GetCurrentMethod)
                            End Try

                        End If
                    End If


                Next
            Next
        Next

    End Sub
    Public Sub actionProcessCompleted(ByVal sender As Object, ByVal UpdateStatus As String, ByVal Comment As String)
        'UpdateStatus = "Complete"
        Console.ForegroundColor = ConsoleColor.Blue
        Console.WriteLine(Comment)

        Dim a1 As TaskWorker_BLL.actionGeneral = sender
        If threadedItems.Contains(a1.ManagedThreadId) Then
            threadedItems.Remove(a1.ManagedThreadId)
        End If
        twBase.Job_SetComplete(UpdateStatus, XDocument.Parse(a1.ActionXML.ToString), a1, Comment)
        sender = Nothing

        'remove tasktrackingitem

    End Sub

    Public Sub actionProcessError(ByVal sender As Object, ByVal UpdateStatus As String, ByVal Comment As String)
        Dim a1 As TaskWorker_BLL.actionGeneral = sender
        If threadedItems.Contains(a1.ManagedThreadId) Then
            threadedItems.Remove(a1.ManagedThreadId)
        End If
        twBase.Job_SetComplete(UpdateStatus, XDocument.Parse(a1.ActionXML.ToString), a1, Comment)
        sender = Nothing
        HasError = True
    End Sub

    Public Sub SQLLog(ByVal sender As Object, ByVal Tlog As TaskLog)
        twBase.Job_AddLog_SQL(Tlog)
    End Sub
    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.


        twBase.DoQuit = True

    End Sub


    Private Sub twBase_LogItem(ByVal Msg As String, ByVal LogLevel As twk_LogLevels, ByVal LogTo As twk_LogTo, ByVal exMethod As MethodBase)
        'Dim LogText As String = Now.ToString("yyyyMMdd hh:mm:ss") & " - " & Msg
        ProcessLogItem(Msg, LogLevel, True, My.Settings.logpath, exMethod)
    End Sub
    Private Sub action_LogItem(ByRef sender As TaskWorker_BLL.actionGeneral, ByVal Msg As String, ByVal LogLevel As twk_LogLevels, ByVal exMethod As MethodBase)
        'Dim LogText As String = Now.ToString("yyyyMMdd hh:mm:ss") & " - " & Msg
        Dim lPath As String = String.Empty

        If My.Settings.logpath <> "" Then
            lPath = My.Settings.logpath
        End If
        If sender.LogFilepath <> "" Then 'action path set will override the main twk setting
            lPath = sender.LogFilepath
        End If

        If sender.log_by_action_attribute <> "" Then
            If sender.ActionXML.Attributes(sender.log_by_action_attribute).Count > 0 Then
                Dim attribValue As String = sender.ActionXML.Attribute(sender.log_by_action_attribute).Value
                lPath &= attribValue
                If IO.Directory.Exists(lPath) = False Then
                    IO.Directory.CreateDirectory(lPath)

                End If
            End If

        End If

        ProcessLogItem(Msg, LogLevel, sender.LogToFile, lPath, exMethod)
    End Sub
    Private Sub ProcessLogItem(ByVal Msg As String, ByVal LogLevel As twk_LogLevels, LogToFile As Boolean, LogFilePath As String, exMethod As MethodBase)
        Try

            Dim LogText As String
            Dim consoleText As String

            Try
                'Console.WriteLine("Logging format set to: " & My.Settings.loggingFormat.ToUpper)
                Select Case My.Settings.loggingFormat.ToUpper
                    Case "HTML"
                        Try
                            If String.IsNullOrEmpty(Msg) = False Then
                                Dim fMSG As String = Msg
                                fMSG = fMSG.Replace(Chr(10), "")
                                fMSG = fMSG.Replace(Chr(13), "")
                                fMSG = fMSG.Replace(Chr(0), "")

                                If IsNothing(exMethod) Then
                                    Console.WriteLine("exMethod not defined")
                                    Exit Sub
                                End If
                                If IsNothing(LogLevel) Then
                                    Console.WriteLine("LogLevel not defined")
                                    Exit Sub
                                End If

                                Dim divXML = <div class=<%= exMethod.DeclaringType.Name %>>
                                                 <div class=<%= LogLevel.ToString %>>
                                                     <span class="timespan"><%= Now.ToString("yyyyMMdd HH:mm:ss") %></span><span class="msg"><%= fMSG %></span>
                                                 </div>
                                             </div>
                                LogText = divXML.ToString
                            Else
                                Console.WriteLine("MSG is null or empty!")
                                Exit Sub
                            End If

                        Catch ex As Exception
                            Console.WriteLine("ProcessLogItem EXCEPTION[1.1]: " & ex.Message)
                            Exit Sub
                        End Try

                    Case "TXT"
                        Try
                            LogText = Now.ToString("yyyyMMdd HH:mm:ss") & " [" & exMethod.DeclaringType.Name & "] " & " - " & Msg
                        Catch ex As Exception
                            Console.WriteLine("ProcessLogItem EXCEPTION[1.2]: " & ex.Message)
                            Exit Sub
                        End Try
                    Case Else
                        Try
                            LogText = Now.ToString("yyyyMMdd HH:mm:ss") & " [" & exMethod.DeclaringType.Name & "] " & " - " & Msg
                        Catch ex As Exception
                            Console.WriteLine("ProcessLogItem EXCEPTION[1.2]: " & ex.Message)
                            Exit Sub
                        End Try

                End Select

                consoleText = Now.ToString("yyyyMMdd HH:mm:ss") & " [" & exMethod.DeclaringType.Name & "] " & " - " & Msg
                'Console.WriteLine("divXML is set " & LogText.Length & " character length.")


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
            Catch ex As Exception
                Console.WriteLine("ProcessLogItem EXCEPTION[2]: " & ex.Message)
                Exit Sub
            End Try


            'Console.SetCursorPosition(0, 30)
            Console.WriteLine(consoleText)
            Try
                If ItemsToLog.Contains(LogLevel) Then
                    If LogToFile = True And LogFilePath <> String.Empty Then
                        Try
                            LogFilePath &= "\" & Now.ToString("yyyyMM")
                            If IO.Directory.Exists(LogFilePath) = False Then
                                IO.Directory.CreateDirectory(LogFilePath)
                            End If

                            Dim fileExt As String = String.Empty
                            Select Case My.Settings.loggingFormat.ToUpper
                                Case "HTML"
                                    fileExt = ".html"
                                    If IO.File.Exists(LogFilePath & "\" & Now.ToString("yyyyMMdd") & fileExt) = False Then
                                        Dim logFileHDR As New IO.StreamWriter(LogFilePath & "\" & Now.ToString("yyyyMMdd") & fileExt, True)
                                        logFileHDR.WriteLine(<link href=<%= My.Settings.loggingCSS %> rel="stylesheet" type="text/css"></link>)
                                        '"<link href="Styles/logFiles.css" rel="stylesheet" type="text/css" />"
                                        logFileHDR.Close()
                                    End If
                                Case "TXT"
                                    fileExt = ".txt"
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
            Catch ex As Exception
                Console.WriteLine("ProcessLogItem EXCEPTION[3]: " & ex.Message)
                Exit Sub
            End Try

        Catch ex As Exception
            Console.WriteLine("EXCEPTION while logging: " & ex.Message)
            Exit Sub
        End Try





    End Sub

End Class
