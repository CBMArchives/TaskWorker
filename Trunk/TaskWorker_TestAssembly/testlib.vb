Imports TaskWorker_BLL


'#######################################################
'##  THE FOLLOWING ARE TWO SIMPLE TWK ACTION CLASSES  ##
'#######################################################
<Serializable()> Public Class testlib1
    Inherits actionGeneral

    Public Sub New()
        MyBase.New()
    End Sub
    Public Overrides Sub Start()
        MyBase.LoadParameters(Me)

        For iLoop = 8700 To 9100
            Dim msg As String = "Processing file #" & iLoop
            Me.LogItem(msg, twk_LogLevels.Info_Detail, System.Reflection.MethodBase.GetCurrentMethod)

            'Dim tlog As New TaskLog With {.ActionName = Me.ActionXML.Attribute("type").Value, .Description = "", .LogDate = Now, .LogLevel = 1, .LogType = 1, .ProcedureID = ProcID}
            'tlog.TrackingValue = "TRNTEST" & iLoop.ToString("0000000000")
            'tlog.TrackingType = 1
            'LogSQL_Add(tlog)
            Threading.Thread.Sleep(5)
        Next
        ProcessComplete("Complete", "Process has completed")
    End Sub


End Class


<Serializable()> Public Class testlib2
    Inherits actionGeneral

    Public Sub New()
        MyBase.New()
    End Sub
    Public Overrides Sub Start()
        MyBase.LoadParameters(Me)
        For iLoop = 143134 To 145534 Step 2
            Dim msg As String = "Processing file #" & iLoop
            Me.LogItem(msg, twk_LogLevels.Info_Detail, System.Reflection.MethodBase.GetCurrentMethod)

            'Threading.Thread.Sleep(100)
            'Dim tlog As New TaskLog With {.ActionName = Me.ActionXML.Attribute("type").Value, .Description = "", .LogDate = Now, .LogLevel = 1, .LogType = 1, .ProcedureID = ProcID}
            'tlog.TrackingValue = "SIDTEST" & iLoop.ToString("00000000")
            'tlog.TrackingType = 2
            'LogSQL_Add(tlog)
        Next

        ProcessComplete("Complete", "Process has completed")
    End Sub


End Class



<Serializable()> Public Class testlib3
    Inherits actionGeneral

    Public Sub New()
        MyBase.New()
    End Sub
    Public Overrides Sub Start()
        For iLoop = 1 To 1000
            Dim msg As String = "Processing file #" & iLoop
            Me.LogItem(msg, twk_LogLevels.Info_Detail, System.Reflection.MethodBase.GetCurrentMethod)

            Threading.Thread.Sleep(1000)


            'Dim tlog As New TaskLog With {.ActionName = Me.ActionXML.Attribute("type").Value, .Description = "", .LogDate = Now, .LogLevel = 1, .LogType = 1, .ProcedureID = ProcID}
            'tlog.TrackingValue = "SIDTEST" & iLoop.ToString("00000000")
            'tlog.TrackingType = 2
            'LogSQL_Add(tlog)
        Next

        ProcessComplete("Complete", "Process has completed")
    End Sub


End Class