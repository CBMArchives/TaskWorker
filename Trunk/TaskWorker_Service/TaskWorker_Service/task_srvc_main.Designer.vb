Imports System.ServiceProcess

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class task_srvc_main
    Inherits System.ServiceProcess.ServiceBase



    'UserService overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    ' The main entry point for the process
     _
    '<System.Diagnostics.DebuggerNonUserCode()> _






    <MTAThread()> Public Shared Sub Main()


        If (System.Diagnostics.EventLog.SourceExists("TaskWorker")) = False Then
            EventLog.CreateEventSource("TaskWorker", "Application")
        End If


        System.Diagnostics.EventLog.WriteEntry("TaskWorker", "About to start")


        Dim args() As String = My.Application.CommandLineArgs.ToArray

        Dim tbase = New taskworker_base


        If My.Settings.RunAsService = True Then
            tbase.GetSettings_FromMySettings()
        Else
            Try
                tbase.GetSettings(args)
            Catch ex As Exception
                System.Diagnostics.EventLog.WriteEntry("TaskWorker", "[GetSettings] Error loading settings: " & ex.Message)
            End Try

        End If





        If tbase.LoadInDBSettings = True Then
            Try
                tbase.LoadDBSettings()
            Catch ex As Exception
                System.Diagnostics.EventLog.WriteEntry("TaskWorker", "[LoadDBSettings] Error loading settings: " & ex.Message)
            End Try

        End If



        If tbase.RunAsService = False Then
            Dim service As New task_srvc_main(tbase)
            service.OnStart(args)
        Else

            Try
                Dim ServicesToRun() As System.ServiceProcess.ServiceBase
                ' More than one NT Service may run within the same process. To add
                ' another service to this process, change the following line to
                ' create a second service object. For example,
                '
                '   ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
                '
                Dim service As New task_srvc_main(tbase)

                ServicesToRun = New System.ServiceProcess.ServiceBase() {service}

                System.ServiceProcess.ServiceBase.Run(ServicesToRun)

            Catch ex As Exception
                System.Diagnostics.EventLog.WriteEntry("TaskWorker", "Service faild to load: " & ex.Message)
            End Try

        End If


        'Dim ServicesToRun() As System.ServiceProcess.ServiceBase

        '' More than one NT Service may run within the same process. To add
        '' another service to this process, change the following line to
        '' create a second service object. For example,
        ''
        ''   ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
        ''
        'ServicesToRun = New System.ServiceProcess.ServiceBase() {New task_srvc_main}

        'System.ServiceProcess.ServiceBase.Run(ServicesToRun)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  
    ' Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
        Me.ServiceName = "TaskWorker"
    End Sub

End Class
