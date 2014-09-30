
Public Class Job
    Public Property ProcID As Integer
    Public Property procsXML As XElement

    Public Sub New()

    End Sub
    Public Sub New(ByVal ID As Integer, ByVal xmlElement As XElement)
        ProcID = ID
        procsXML = xmlElement
    End Sub

    Public Property name As String
    Public Property source As String
    Public Property priority As Integer
    Public Property currentstatus As String

    Public Property maxretrys As Integer
    Public Property IsComplete As Boolean
    Public Property StartedOn As Date
    Public Property CompletedOn As Date
    Public Property ErrorCount As Integer
    Public Property ProcessingBy As String
    Public Property IsProcessing As Boolean
    Public Property HostName As String
    Public Property IPAddress As String
    Public Property Comments As String
    Public Property AddedBy As String
    Public Property AddedOn As Date
    Public Property TrackingNumber As String
    Public Property TrackingNumberType As Integer

End Class

