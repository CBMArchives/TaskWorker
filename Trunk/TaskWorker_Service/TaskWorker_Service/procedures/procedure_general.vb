Public Class procedure_general
    Public Property ProcedureXML As Xml.Linq.XElement

    Public Sub New()
        ProcedureXML = <procedure initialstatus="" status_error="" status_onretry="" status_onsuccess="" type="" runorder="" status="">
                           <actions>
                           </actions>
                       </procedure>


    End Sub

End Class
