Imports TaskWorker_BLL
Imports System.Reflection

Public Class taskworker_base
    Public Property RunAsService As Boolean = True
    Public Property ShowForm As Boolean = False
    Public Property MaxTasks As Integer = 0
    Public Property initStr As String = String.Empty
    Public Property procRunName As String = String.Empty

    Public Property appConnectionStr As String = String.Empty
    Public Property appCode As String = String.Empty
    Public Property appSection As String = String.Empty
    Public Property appVersion As String = String.Empty
    Public Property LoadInDBSettings As Boolean = False

    Public Event LogItem(ByVal Msg As String, ByVal Level As twk_LogLevels, ByVal LogTo As twk_LogTo, ByVal exMethod As MethodBase)
    Public Event ProcessError(ByVal Msg As String)
    Public Property RespawnSecs As Integer
    Public Property ThreadTotal As Integer
    Public Property TimeToLive As Integer
    Public Property WaitOnThreads As Boolean = True
    Public Property MaxErrorCount As Integer = 0
    Public Property logpath As String = String.Empty
    Public Property logToFile As Boolean = False
    Public Property loggingValues As String = String.Empty
    Public Property loggingFormat As String = String.Empty
    Public Property loggingCSS As String = String.Empty

    Public Property load_procs_from_dir As Boolean = False
    Public Property load_procs_path As String = String.Empty
    Public Property load_db_jobs As Boolean = False

    Public Property use_assemblies_config As Boolean = False
    Public Property procsXML_path As String = String.Empty

    Public Property currentBackColor As System.ConsoleColor

    Public DoQuit As Boolean = False

    Public Sub LoadDBSettings()
        RaiseEvent LogItem("***** Loading settings from database", twk_LogLevels.Info_Detail, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)

        Dim xSettings As Xml.Linq.XElement
        Try
            xSettings = SettingsGetter.GetApplicationConfiguration(appConnectionStr, appCode, appVersion, appSection)
        Catch sqlex As SqlClient.SqlException
            MsgBox("** SQL CONNECTION ERROR.  Did not find table record for: " & appCode & " - " & appVersion & " - " & appSection & "  " & sqlex.Message, vbOKOnly, "Error")
            End
        Catch ex As Exception
            MsgBox("Check flags or configuration values.  Did not find table record for: " & appCode & " - " & appVersion & " - " & appSection, vbOKOnly, "Error")
            End
        End Try

        Dim GettingValue As String = String.Empty
        Try
            GettingValue = "procRunName"
            procRunName = (From s In xSettings.Element("Constants").Elements Where s.Attribute("name").Value = "procrunname").First.Attribute("value").Value
            RaiseEvent LogItem("** " & GettingValue & " value set to " & procRunName, twk_LogLevels.Info_Detail, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
            GettingValue = "initStr"
            initStr = (From s In xSettings.Element("Constants").Elements Where s.Attribute("name").Value = "initstr").First.Attribute("value").Value
            RaiseEvent LogItem("** " & GettingValue & " value set to " & initStr, twk_LogLevels.Info_Detail, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
            GettingValue = "MaxTasks"
            MaxTasks = (From s In xSettings.Element("Constants").Elements Where s.Attribute("name").Value = "maxtasks").First.Attribute("value").Value
            RaiseEvent LogItem("** " & GettingValue & " value set to " & MaxTasks, twk_LogLevels.Info_Detail, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
            GettingValue = "ShowForm"
            ShowForm = (From s In xSettings.Element("Constants").Elements Where s.Attribute("name").Value = "showform").First.Attribute("value").Value
            RaiseEvent LogItem("** " & GettingValue & " value set to " & ShowForm, twk_LogLevels.Info_Detail, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
            GettingValue = "RunAsService"
            RunAsService = (From s In xSettings.Element("Constants").Elements Where s.Attribute("name").Value = "runasservice").First.Attribute("value").Value
            RaiseEvent LogItem("** " & GettingValue & " value set to " & RunAsService, twk_LogLevels.Info_Detail, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)

            GettingValue = "SleepSeconds"
            RespawnSecs = (From s In xSettings.Element("Constants").Elements Where s.Attribute("name").Value = "sleepseconds").First.Attribute("value").Value
            RaiseEvent LogItem("** " & GettingValue & " value set to " & RespawnSecs, twk_LogLevels.Info_Detail, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)

        Catch ex As Exception
            RaiseEvent LogItem("** Failed getting value for: " & GettingValue & " " & ex.Message, twk_LogLevels.ProcessError, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Failed getting value for: " & GettingValue)
            RaiseEvent ProcessError(ex.Message)
        End Try




        'If xSettings.Elements("ControlOverrides").Count > 0 Then
        '    For Each cOver In xSettings.Element("ControlOverrides").Elements("Control")
        '        Dim ctrlname As String = cOver.Attribute("name").Value
        '        Dim ctrltype As String = cOver.Attribute("type").Value
        '        For Each pOver In cOver.Elements("Property")
        '            Dim pName As String = pOver.Attribute("name").Value
        '            Dim pValue As String = pOver.Attribute("value").Value
        '            Dim puMethod As String = String.Empty
        '            If pOver.Attributes("usemethod").Count = 0 Then

        '            Else
        '                puMethod = pOver.Attribute("usemethod").Value
        '            End If
        '            If ctrltype = Me.GetType.ToString Then
        '                Dim pInfo As System.Reflection.PropertyInfo = Me.GetType.GetProperty(pName)
        '                If IsNothing(pInfo) = False Then
        '                    pInfo.SetValue(Me, pValue, Nothing)
        '                End If
        '            Else
        '                Dim ctrlAry = Me.Controls.Find(ctrlname, True)
        '                For Each ctrl In ctrlAry
        '                    If IsNothing(ctrl) = False Then
        '                        Dim pInfo As System.Reflection.PropertyInfo = ctrl.GetType.GetProperty(pName)
        '                        If IsNothing(pInfo) = False Then
        '                            Select Case pInfo.PropertyType.FullName
        '                                Case "System.Drawing.Color"
        '                                    If puMethod = "FromName" Then
        '                                        pInfo.SetValue(ctrl, Color.FromName(pValue), Nothing)
        '                                    ElseIf puMethod = "FromArgb" Then
        '                                        pInfo.SetValue(ctrl, Color.FromArgb(pValue), Nothing)
        '                                    End If
        '                                Case Else
        '                                    pInfo.SetValue(ctrl, pValue, Nothing)
        '                            End Select
        '                        End If
        '                    End If
        '                Next ctrl
        '            End If
        '        Next pOver
        '    Next cOver
        'End If

    End Sub


    Public Sub GetSettings_FromMySettings()
        appConnectionStr = My.Settings.appconnectionstr
        RunAsService = My.Settings.RunAsService
        ShowForm = Boolean.Parse(My.Settings.ShowForm)
        procRunName = My.Settings.procRunName
        MaxTasks = My.Settings.MaxTasks
        appCode = My.Settings.appcode
        appVersion = My.Settings.appVersion
        appSection = My.Settings.appSection
        LoadInDBSettings = My.Settings.LoadDBSettings
        RespawnSecs = My.Settings.RespawnSecs
        TimeToLive = My.Settings.TimeToLive
        WaitOnThreads = My.Settings.WaitOnThreads
        MaxErrorCount = My.Settings.MaxErrorCount
        logpath = My.Settings.logpath
        loggingValues = My.Settings.loggingvalues
        logToFile = My.Settings.logtofile
        loggingFormat = My.Settings.loggingFormat
        loggingCSS = My.Settings.loggingCSS
        procsXML_path = My.Settings.procsXML_path
        load_procs_from_dir = My.Settings.load_procs_from_dir
        load_procs_path = My.Settings.load_procs_path
        load_db_jobs = My.Settings.load_db_jobs

        use_assemblies_config = My.Settings.use_assemblies_config
    End Sub
    Public Sub GetSettings(ByVal StartupArgs() As String)



        Dim qANS As String = ""
        Do Until qANS.ToUpper = "R"
            'first get from settings xml
            GetSettings_FromMySettings()


            'get db connection args
            For Each argItem As String In StartupArgs
                If argItem.StartsWith("/") Then
                    Dim Flagname As String = Split(argItem, ":")(0)
                    Dim Flagvalue As String = String.Empty
                    Select Case Flagname.ToLower
                        Case "/runasservice"
                            RunAsService = Boolean.Parse(Split(argItem, ":")(1))
                        Case "/showform"
                            ShowForm = Boolean.Parse(Split(argItem, ":")(1))
                        Case "/appconnectionstr"
                            appConnectionStr = Split(argItem, ":")(1)
                        Case "/proc"
                            procRunName = Split(argItem, ":")(1)
                        Case "/initstr"
                            initStr = Split(argItem, ":")(1)
                        Case "/maxtasks"
                            MaxTasks = Integer.Parse(Split(argItem, ":")(1))
                        Case "/appcode"
                            appCode = Split(argItem, ":")(1)
                        Case "/appversion"
                            appVersion = Split(argItem, ":")(1)
                        Case "/appsection"
                            appSection = Split(argItem, ":")(1)
                        Case "/loaddbsettings"
                            LoadInDBSettings = Boolean.Parse(Split(argItem, ":")(1))
                        Case "/respawnsecs"
                            RespawnSecs = Split(argItem, ":")(1)
                        Case "/logpath"
                            logpath = Split(argItem, ":")(1)
                        Case "/loggingformat"
                            loggingFormat = Split(argItem, ":")(1)
                        Case "/loggingcss"
                            loggingCSS = Split(argItem, ":")(1)
                        Case "/loadprocsfromdir"
                            load_procs_from_dir = Boolean.Parse(Split(argItem, ":")(1))
                        Case "/loadprocspath"
                            load_procs_path = Split(argItem, ":")(1)
                        Case "/loaddbjobs"
                            load_db_jobs = Boolean.Parse(Split(argItem, ":")(1))
                        Case "/useassembliesconfig"
                            use_assemblies_config = Boolean.Parse(Split(argItem, ":")(1))
                    End Select
                End If
            Next

            Console.ForegroundColor = ConsoleColor.Green
            Console.WindowHeight = 45

            Dim defaultColor = Console.ForegroundColor
            currentBackColor = Console.BackgroundColor


            Console.Title = "TASKWORKER"
            If IO.File.Exists(My.Application.Info.DirectoryPath & "\" & "banner.txt") = True Then

                Console.ForegroundColor = ConsoleColor.Red
                Console.Write(IO.File.ReadAllText(My.Application.Info.DirectoryPath & "\" & "banner.txt"))
                Console.WriteLine("")
                Console.WriteLine("")
            End If
            Console.WriteLine("Version: " & My.Application.Info.Version.ToString)
            'CALL:ECHORED "Print me in red!"
            Console.ForegroundColor = defaultColor


            Console.WriteLine("****TASKWORKER SETTINGS****")
            Console.WriteLine("")



            Console.WriteLine("1.  RUNASSERVICE? (" & RunAsService.ToString & ")")
            Console.WriteLine("2.  Use form? (" & ShowForm.ToString & ")")
            Console.WriteLine("3.  appConnectionStr (" & appConnectionStr & ")")
            Console.WriteLine("4.  procRunName (" & procRunName.ToString & ")")
            Console.WriteLine("5.  initstr (" & initStr.ToString & ")")
            Console.WriteLine("6.  maxtasks (" & MaxTasks.ToString & ")")
            Console.WriteLine("7.  appCode (" & appCode.ToString & ")")
            Console.WriteLine("8.  appversion (" & appVersion.ToString & ")")
            Console.WriteLine("9.  appSection (" & appSection.ToString & ")")
            Console.WriteLine("10.  LoadDBSettings? (" & LoadInDBSettings.ToString & ")")
            Console.WriteLine("11.  Respawn in secs (" & RespawnSecs.ToString & ")")
            Console.WriteLine("12.  Action Time To Live (sec) (" & TimeToLive.ToString & ")")
            Console.WriteLine("13.  Wait for each thread to end? (" & WaitOnThreads.ToString & ")")
            Console.WriteLine("14.  Max Error Count (" & MaxErrorCount.ToString & ")")
            Console.WriteLine("15.  Logs directory (" & logpath.ToString & ")")
            Console.WriteLine("16.  Log to file? (" & logToFile.ToString & ")")
            Console.WriteLine("17.  Log values [separate by commas] (" & loggingValues.ToString & ")")
            Console.WriteLine("18.  Logging format (" & loggingFormat.ToString & ")")
            Console.WriteLine("19.  Logging CSS path (" & loggingCSS.ToString & ")")
            Console.WriteLine("20.  Load procedures from directory? (" & load_procs_from_dir.ToString & ")")
            Console.WriteLine("21.  Run Procedures XML (" & procsXML_path.ToString & ")")
            Console.WriteLine("22.  Load procedures path: (" & load_procs_path.ToString & ")")
            Console.WriteLine("23.  Load procedures from database? (" & load_db_jobs.ToString & ")")
            Console.WriteLine("24.  Use assemblies.config? (" & use_assemblies_config.ToString & ")")


            Console.WriteLine("")
            Console.WriteLine("Q. Quit ")
            Console.WriteLine("R. Run ")
            Console.WriteLine("")
            Console.Write("Enter an option. >")
            Console.Write("Enter an option. >")
            Console.ForegroundColor = ConsoleColor.Yellow
            Console.BackgroundColor = ConsoleColor.Blue
            qANS = Console.ReadLine
            Console.ForegroundColor = ConsoleColor.Green
            Console.BackgroundColor = currentBackColor

            Select Case qANS.ToUpper
                Case "N", "NO", "Q", "QUIT"
                    End
                Case "1"
                    Console.Write("Enter Y/N > ")
                    Select Case Console.ReadKey.Key
                        Case ConsoleKey.Y
                            My.Settings.RunAsService = True
                        Case Else
                            My.Settings.RunAsService = False
                    End Select
                Case "2"
                    Console.Write("Enter Y/N > ")
                    My.Settings.ShowForm = IIf(Console.ReadKey.Key = ConsoleKey.Y, True, False)
                Case "3"
                    Console.Write("Enter a value > ")
                    Dim mAns As String = Console.ReadLine
                    If mAns = "" Then

                    Else
                        My.Settings.appconnectionstr = mAns
                    End If

                Case "4"
                    Console.Write("Enter a value > ")
                    Dim mAns As String = Console.ReadLine
                    If mAns = "" Then

                    Else
                        My.Settings.procRunName = mAns
                    End If
                Case "7"
                    Console.Write("Enter a value > ")
                    Dim mAns As String = Console.ReadLine
                    If mAns = "" Then

                    Else
                        My.Settings.appcode = mAns
                    End If
                Case "6"
                    Console.Write("Enter a value > ")
                    Dim q = Console.ReadLine
                    If IsNumeric(q) Then
                        My.Settings.MaxTasks = q
                    End If
                Case "8"
                    Console.Write("Enter a value > ")
                    Dim mAns As String = Console.ReadLine
                    If mAns = "" Then

                    Else
                        My.Settings.appVersion = mAns
                    End If
                Case "9"
                    Console.Write("Enter a value > ")
                    Dim mAns As String = Console.ReadLine
                    If mAns = "" Then

                    Else
                        My.Settings.appSection = mAns
                    End If
                Case "10"
                    Console.Write("Enter Y/N > ")
                    Select Case Console.ReadKey.Key
                        Case ConsoleKey.Y
                            My.Settings.LoadDBSettings = True
                        Case Else
                            My.Settings.LoadDBSettings = False
                    End Select
                Case "11"
                    Console.Write("Enter a value > ")
                    Dim q = Console.ReadLine
                    If IsNumeric(q) Then
                        My.Settings.RespawnSecs = q
                    End If
                Case "12"
                    Console.Write("Enter a value > ")
                    Dim q = Console.ReadLine
                    If IsNumeric(q) Then
                        My.Settings.TimeToLive = q
                    End If
                Case "13"
                    Console.Write("Enter Y/N > ")
                    Select Case Console.ReadKey.Key
                        Case ConsoleKey.Y
                            My.Settings.WaitOnThreads = True
                        Case Else
                            My.Settings.WaitOnThreads = False
                    End Select
                Case "14"
                    Console.Write("Enter a value > ")
                    Dim q = Console.ReadLine
                    If IsNumeric(q) Then
                        My.Settings.MaxErrorCount = q
                    End If
                Case "15"
                    Console.Write("Enter a value > ")
                    Dim mAns As String = Console.ReadLine
                    If mAns = "" Then

                    Else
                        My.Settings.logpath = mAns
                    End If
                Case "16"
                    Console.Write("Enter Y/N > ")
                    Select Case Console.ReadKey.Key
                        Case ConsoleKey.Y
                            My.Settings.logtofile = True
                        Case Else
                            My.Settings.logtofile = False
                    End Select
                Case "17"
                    Console.Write("Enter a value > ")
                    Dim mAns As String = Console.ReadLine
                    If mAns = "" Then

                    Else
                        My.Settings.loggingvalues = mAns
                    End If
                Case "18"
                    Console.Write("Enter a value > ")
                    Dim mAns As String = Console.ReadLine
                    If mAns = "" Then

                    Else
                        My.Settings.loggingFormat = mAns
                    End If
                Case "19"
                    Console.Write("Enter a value > ")
                    Dim mAns As String = Console.ReadLine
                    If mAns = "" Then

                    Else
                        My.Settings.loggingCSS = mAns
                    End If
                Case "20"


                    Dim mAns As String = GetLineAnswer("Enter a value> ")
                    If mAns = "" Then

                    Else
                        My.Settings.procsXML_path = mAns
                    End If
                Case "21"
                    Console.Write("Enter Y/N > ")
                    Select Case Console.ReadKey.Key
                        Case ConsoleKey.Y
                            My.Settings.load_procs_from_dir = True
                        Case Else
                            My.Settings.load_procs_from_dir = False
                    End Select
                Case "22"
                    Console.Write("Enter a value > ")
                    Dim mAns As String = Console.ReadLine
                    If mAns = "" Then

                    Else
                        My.Settings.load_procs_path = mAns
                    End If
                Case "23"
                    Console.Write("Enter Y/N > ")
                    Select Case Console.ReadKey.Key
                        Case ConsoleKey.Y
                            My.Settings.load_db_jobs = True
                        Case Else
                            My.Settings.load_db_jobs = False
                    End Select
                Case "24"
                    Console.Write("Enter Y/N > ")
                    Select Case Console.ReadKey.Key
                        Case ConsoleKey.Y
                            My.Settings.use_assemblies_config = True
                        Case Else
                            My.Settings.use_assemblies_config = False
                    End Select

            End Select
            Console.Clear()
            My.Settings.Save()
        Loop









        If appConnectionStr = "" Then
            Console.WriteLine("AppConnectionStr parameter is not set. Please enter a connection string.")
            My.Settings.appconnectionstr = Console.ReadLine()
            My.Settings.Save()
        End If






    End Sub
    Public Function GetLineAnswer(ByVal Question As String) As String
        Console.Write(Question)

        Console.ForegroundColor = ConsoleColor.Yellow
        Console.BackgroundColor = ConsoleColor.Blue
        Dim mAns As String = Console.ReadLine
        Console.ForegroundColor = ConsoleColor.Green
        Console.BackgroundColor = currentBackColor

        Return mAns


    End Function

    Public Function GetNextJobs() As Collections.Generic.List(Of Job)
        If (System.Diagnostics.EventLog.SourceExists("TaskWorker")) = False Then
            EventLog.CreateEventSource("TaskWorker", "Application")
        End If

        System.Diagnostics.EventLog.WriteEntry("TaskWorker", "GetNextJobs... Looking...")
        Dim result As New Collections.Generic.List(Of Job)
        If load_db_jobs = True Then
            System.Diagnostics.EventLog.WriteEntry("TaskWorker", "GetNext_SQL_Jobs")
            result.AddRange(GetNext_SQL_Jobs)
        End If
        If load_procs_from_dir = True Then
            System.Diagnostics.EventLog.WriteEntry("TaskWorker", "GetNext_FromDir_Jobs")
            result.AddRange(GetNext_FromDir_Jobs)
        End If

        System.Diagnostics.EventLog.WriteEntry("TaskWorker", "GetNextJobs... Found [" & result.Count & "]")

        Return result
    End Function
    Public Function GetNext_FromDir_Jobs() As Collections.Generic.List(Of Job)
        Dim result_sql_jobs As New Collections.Generic.List(Of Job)
        If load_procs_path = "" Then
            RaiseEvent LogItem("ERROR: NOTHING SPECIFIED FOR [Load procedures path].  Please check your configuration." & load_procs_path & "] ", twk_LogLevels.Warning, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
            Return result_sql_jobs
        End If

        RaiseEvent LogItem("[Getting jobs from disk: " & load_procs_path & "]", twk_LogLevels.Info_Detail, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
        If IO.Directory.Exists(load_procs_path) = False Then
            RaiseEvent LogItem("ERROR: DIRECTORY DOES NOT EXISTS OR IS NOT ACCESSIBLE [Load procedures path: " & load_procs_path & "].  Please check your configuration. ", twk_LogLevels.Warning, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
            Return result_sql_jobs
        End If

        Dim xmlFileList() As String = IO.Directory.GetFiles(load_procs_path, "*.xml")
        RaiseEvent LogItem("Found [" & xmlFileList.Count & "] XML configurations to process.", twk_LogLevels.Info_Detail, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)

        For Each xFile As String In xmlFileList
            Dim xfInfo As New IO.FileInfo(xFile)
            Dim nJob As New Job(0, XElement.Parse(IO.File.ReadAllText(xFile)))
            nJob.AddedOn = Now
            nJob.source = xfInfo.Name
            nJob.TrackingNumber = Guid.NewGuid.ToString
            nJob.currentstatus = "pending"
            nJob.AddedBy = "TASKWORKER"
            nJob.HostName = My.Computer.Name
            result_sql_jobs.Add(nJob)
        Next
        Return result_sql_jobs
    End Function
    Public Function GetNext_SQL_Jobs() As Collections.Generic.List(Of Job)
        RaiseEvent LogItem("[Getting jobs from database] " & procRunName, twk_LogLevels.Info_Detail, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
        Dim result_sql_jobs As New Collections.Generic.List(Of Job)
        Dim dbConn As SqlClient.SqlConnection
        Try
            dbConn = New SqlClient.SqlConnection(appConnectionStr)
        Catch ex As Exception
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!  SQL Connection String Issue !!!!!!!!!!!!!!!!!!!!!!")
            Console.WriteLine(ex.Message.ToUpper)
            Console.WriteLine("")
            Return result_sql_jobs
        End Try

        Dim dbComm As New SqlClient.SqlCommand(procRunName, dbConn)
        dbComm.CommandType = CommandType.StoredProcedure
        dbComm.Parameters.Add(New SqlClient.SqlParameter("@ownername", appSection))
        dbComm.Parameters.Add(New SqlClient.SqlParameter("@initStr", ""))
        dbComm.Parameters.Add(New SqlClient.SqlParameter("@return_maxtasks", MaxTasks))

        Dim da As New SqlClient.SqlDataAdapter(dbComm)

        Try
            dbConn.Open()
        Catch ex As Exception
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!  SQL Connection Problem !!!!!!!!!!!!!!!!!!!!!!")
            Console.WriteLine(ex.Message.ToUpper)
            Console.WriteLine("")
            Return result_sql_jobs
        End Try


        Dim dt As New DataTable
        Try
            da.Fill(dt)
        Catch ex As Exception
            Console.WriteLine("SQL error getting jobs: " & ex.Message)
            Return result_sql_jobs
        End Try

        For Each dr As DataRow In dt.Rows
            Dim nJob As New Job(dr("procedureID"), XElement.Parse(dr("procsXML")))
            result_sql_jobs.Add(nJob)
        Next
        dbConn.Close()
        dbConn.Dispose()
        dbComm.Dispose()
        RaiseEvent LogItem(" --> Returning #" & dt.Rows.Count & " job(s).", twk_LogLevels.Info_Detail, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
        Return result_sql_jobs
    End Function

    Public Function GetTypeInfo(ByVal TypeName As String) As TypeInfo
        Dim result As New TypeInfo
        If use_assemblies_config = True Then
            RaiseEvent LogItem("Getting assembly info from assemblies.config for typename: " & TypeName, twk_LogLevels.Info_Detail, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
            If IO.File.Exists(load_procs_path & "\assemblies.config") = True Then
                Dim xAData As XElement = XElement.Load(load_procs_path & "\assemblies.config")
                For Each xType As XElement In (From x1 As XElement In xAData.Elements("assembly") Where x1.Attribute("typename").Value = TypeName)
                    result.AssemblyPath = xType.Attribute("assemblypath").Value
                    result.ClassName = xType.Attribute("classname").Value
                    result.IsActive = True
                    result.TypeName = TypeName
                    Return result
                Next

            Else
                RaiseEvent LogItem("ERROR - NO INFO FOR ASSEMBLY FOR TYPENAME: " & TypeName, twk_LogLevels.ProcessError, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
                Return result
            End If
        Else
            RaiseEvent LogItem("Getting assembly path from database: " & procRunName & " for typename: " & TypeName, twk_LogLevels.Info_Detail, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
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
        End If


    End Function

    Public Sub Job_SetComplete(ByVal Status As String, ByVal xData As XDocument, ByVal action As TaskWorker_BLL.actionGeneral, ByVal Comments As String)

        Try
            Dim dbConn As New SqlClient.SqlConnection(appConnectionStr)
            Dim dbComm As New SqlClient.SqlCommand("[taskworker_Job_Complete]", dbConn)
            dbComm.CommandType = CommandType.StoredProcedure
            dbComm.Parameters.Add(New SqlClient.SqlParameter("@ProcedureID", action.ProcID))
            dbComm.Parameters.Add(New SqlClient.SqlParameter("@currentstatus", Status))
            dbComm.Parameters.Add(New SqlClient.SqlParameter("@xAction", action.ActionXML.ToString))
            dbComm.Parameters.Add(New SqlClient.SqlParameter("@xData", xData.ToString))
            dbComm.Parameters.Add(New SqlClient.SqlParameter("@comments", Comments))
            Dim da As New SqlClient.SqlDataAdapter(dbComm)
            dbConn.Open()
            dbComm.ExecuteScalar()

            dbConn.Close()
            dbConn.Dispose()
            dbComm.Dispose()
            RaiseEvent LogItem("Close procID #" & action.ProcID, twk_LogLevels.Info_Detail, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
        Catch ex As Exception
            RaiseEvent LogItem("Could not close procID #" & action.ProcID & " - ERROR: " & ex.Message, twk_LogLevels.Warning, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)

        Finally


        End Try

    End Sub

    Public Sub Job_AddLog_SQL(ByVal TLog As TaskLog)

        Try
            Dim dbConn As New SqlClient.SqlConnection(appConnectionStr)
            Dim dbComm As New SqlClient.SqlCommand("[taskworker_Job_AddLog]", dbConn)
            dbComm.CommandType = CommandType.StoredProcedure
            dbComm.Parameters.Add(New SqlClient.SqlParameter("@ProcedureID", TLog.ProcedureID))
            dbComm.Parameters.Add(New SqlClient.SqlParameter("@ActionName", TLog.ActionName))
            dbComm.Parameters.Add(New SqlClient.SqlParameter("@LogType", TLog.LogType))
            dbComm.Parameters.Add(New SqlClient.SqlParameter("@LogLevel", TLog.LogLevel))
            dbComm.Parameters.Add(New SqlClient.SqlParameter("@Description", TLog.Description))
            dbComm.Parameters.Add(New SqlClient.SqlParameter("@LogDate", TLog.LogDate))
            dbComm.Parameters.Add(New SqlClient.SqlParameter("@TrackingType", TLog.TrackingType))
            dbComm.Parameters.Add(New SqlClient.SqlParameter("@TrackingValue", TLog.TrackingValue))
            Dim da As New SqlClient.SqlDataAdapter(dbComm)
            dbConn.Open()
            dbComm.ExecuteScalar()

            dbConn.Close()
            dbConn.Dispose()
            dbComm.Dispose()
        Catch ex As Exception
            RaiseEvent LogItem("Could log to SQL procID #" & TLog.ProcedureID & " - ERROR: " & ex.Message, twk_LogLevels.Warning, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
        Finally

        End Try

    End Sub
    Public Function Recycle() As Boolean
        'launch task worker threads



        If DoQuit Then
            Return False
        Else
            Dim ts1 As New TimeSpan(0, 0, RespawnSecs)
            Dim d2 As Date = Now
            RaiseEvent LogItem("Sleeping for " & ts1.TotalSeconds & " seconds", twk_LogLevels.DebugInfo_L3, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)

            Dim diff As Long = DateDiff(DateInterval.Second, d2, Now)
            Do While RespawnSecs > diff
                diff = DateDiff(DateInterval.Second, d2, Now)

                If RespawnSecs - diff = 300 Then
                    RaiseEvent LogItem("Starting in 5 minutes ", twk_LogLevels.DebugInfo_L3, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
                End If
                If RespawnSecs - diff = 120 Then
                    RaiseEvent LogItem("Starting in 2 minutes ", twk_LogLevels.DebugInfo_L3, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
                End If
                If RespawnSecs - diff = 30 Then
                    RaiseEvent LogItem("Starting in 30 seconds", twk_LogLevels.DebugInfo_L3, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
                End If
                If RespawnSecs - diff = 10 Then
                    RaiseEvent LogItem("Starting in 10 seconds", twk_LogLevels.DebugInfo_L3, twk_LogTo.only_to_console, MethodBase.GetCurrentMethod)
                End If


                Threading.Thread.Sleep(1000)
            Loop

            'Threading.Thread.Sleep(ts1)
        End If
        Return True
    End Function

End Class

Public Class threadTrackingItem
    Public Property ManagedThreadId As Integer
    Public Property StartTime As DateTime
    Public Property TimeToLive_sec As Integer
End Class
Public Class threadTrackingItems
    Inherits Collections.Hashtable


End Class

