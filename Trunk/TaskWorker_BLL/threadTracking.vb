Public Class threadTrackingItem
    Public Property ManagedThreadId As Integer
    Public Property StartTime As DateTime
    Public Property TimeToLive_sec As Integer
End Class
Public Class threadTrackingItems
    Inherits Collections.Hashtable


End Class