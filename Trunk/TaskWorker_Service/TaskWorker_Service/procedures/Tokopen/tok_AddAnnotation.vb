Public Class tok_AddAnnotation
    Public Property ProcedureXML As Xml.Linq.XElement

    Public Sub New()
        ProcedureXML = <procedures initialstatus="pending" status_error="error_aborted" status_onretry="error_willretry" status_onsuccess="all_completed">
                           <procedure type="TOKOPEN_ANNOTATIONS_ADD" runorder="1" status="pending">
                               <actions>
                                   <action type="ANN_ADD_01" description="Add Annoation" runorder="1" status="pending">
                                       <settings>
                                           <setting name="archiveconnectionString" connectionstring="Data Source=DPSCRSQL1;Initial Catalog=CRArchiveTEST;Persist Security Info=True;User ID=fes; Password=fes;"/>
                                       </settings>
                                       <options>
                                           <option name="addtopage" value="first"/>
                                           <option name="position" value="cascadefromlast"/>
                                           <option name="ownerid" value="1674"/>
                                       </options>
                                       <parameters>
                                           <parameter name="annotationid" value=""/>
                                           <parameter name="docid" value="27749131"/>
                                           <parameter name="docpageid" value=""/>
                                           <parameter name="pagenumber" value="1"/>
                                           <parameter name="markgroup" value="Default"/>
                                           <parameter name="marktype" value="1"/>
                                           <parameter name="marktext" value=""/>
                                           <parameter name="detailtext" value="Initial Registration Packet not submitted; &#xd;&#xa;No Print card included or TRN reported; &#xd;&#xa;Missing Event date; &#xd;&#xa;No contributor ORI reported; &#xd;&#xa; this a jorney into sound I hope they wil 88686 rains, john anthony ee62348624 eee874387 L txeee828 Missing Name, sex or date of birth; &#xd;&#xa;Juvenile Non-Public marked but Order not included with reporting form; &#xd;&#xa; Missing 8-digit offense code; &#xd;&#xa;Disposition date not reported; &#xd;&#xa;Non-Tx offense title not reported; &#xd;&#xa; Incomplete e-mail address data set; &#xd;&#xa;Incomplete screen name/moniker data set; &#xd;&#xa; License Plate - Incomplete data set; &#xd;&#xa; Licensing authority not reported; &#xd;&#xa; Campus code not reported; &#xd;&#xa; Relative information incomplete; &#xd;&#xa; "/>
                                           <parameter name="createddate" value=""/>
                                           <parameter name="createbyid" value="1674"/>
                                           <parameter name="modifieddate" value=""/>
                                           <parameter name="modifiedbyid" value=""/>
                                           <parameter name="forecolour" value="0"/>
                                           <parameter name="backcolour" value="65535"/>
                                           <parameter name="xpos" value=""/>
                                           <parameter name="ypos" value=""/>
                                           <parameter name="width" value=""/>
                                           <parameter name="height" value="142"/>
                                           <parameter name="highlight" value=""/>
                                           <parameter name="linesize" value=""/>
                                           <parameter name="penstyle" value=""/>
                                           <parameter name="textalign" value=""/>
                                           <parameter name="arrowtype" value=""/>
                                           <parameter name="arrowlength" value=""/>
                                           <parameter name="bitmap" value=""/>
                                           <parameter name="measurementsunits" value=""/>
                                           <parameter name="decimalplaces" value=""/>
                                           <parameter name="transparent" value=""/>
                                           <parameter name="fontname" value="Arial"/>
                                           <parameter name="fontsize" value="28"/>
                                           <parameter name="fontbold" value=""/>
                                           <parameter name="fontitalic" value=""/>
                                           <parameter name="fontstrikethrough" value=""/>
                                           <parameter name="fontunderline" value=""/>
                                           <parameter name="fontweight" value="400"/>
                                           <parameter name="workflowannotation" value=""/>
                                       </parameters>
                                   </action>
                               </actions>
                           </procedure>
                       </procedures>

    End Sub

End Class
