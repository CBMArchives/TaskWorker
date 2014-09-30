Option Strict Off
Option Explicit On

Imports System.Xml.Serialization


'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://tempuri.org/actionsXMLSchema.xsd"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="http://tempuri.org/actionsXMLSchema.xsd", IsNullable:=False)> _
Partial Public Class actionClass

    Private pathsField() As actionPath

    Private parametersField As actionParameters

    Private contentField As actionContent

    Private workflowsField As actionWorkflows

    Private groupsField As actionGroups

    Private typeField As String

    Private descriptionField As String

    Private sequenceField As String

    Private statusField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute("path", IsNullable:=False)> _
    Public Property paths() As actionPath()
        Get
            Return Me.pathsField
        End Get
        Set(value As actionPath())
            Me.pathsField = value
        End Set
    End Property

    '''<remarks/>
    Public Property parameters() As actionParameters
        Get
            Return Me.parametersField
        End Get
        Set(value As actionParameters)
            Me.parametersField = value
        End Set
    End Property

    '''<remarks/>
    Public Property content() As actionContent
        Get
            Return Me.contentField
        End Get
        Set(value As actionContent)
            Me.contentField = value
        End Set
    End Property

    '''<remarks/>
    Public Property workflows() As actionWorkflows
        Get
            Return Me.workflowsField
        End Get
        Set(value As actionWorkflows)
            Me.workflowsField = value
        End Set
    End Property

    '''<remarks/>
    Public Property groups() As actionGroups
        Get
            Return Me.groupsField
        End Get
        Set(value As actionGroups)
            Me.groupsField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property type() As String
        Get
            Return Me.typeField
        End Get
        Set(value As String)
            Me.typeField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property description() As String
        Get
            Return Me.descriptionField
        End Get
        Set(value As String)
            Me.descriptionField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property sequence() As String
        Get
            Return Me.sequenceField
        End Get
        Set(value As String)
            Me.sequenceField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property status() As String
        Get
            Return Me.statusField
        End Get
        Set(value As String)
            Me.statusField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://tempuri.org/actionsXMLSchema.xsd")> _
Partial Public Class actionPath

    Private typeField As String

    Private nameField As String

    Private valueField As String

    Private valueField1 As String

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property type() As String
        Get
            Return Me.typeField
        End Get
        Set(value As String)
            Me.typeField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property name() As String
        Get
            Return Me.nameField
        End Get
        Set(value As String)
            Me.nameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Overloads Property value() As String
        Get
            Return Me.valueField
        End Get
        Set(value As String)
            Me.valueField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlTextAttribute()> _
    Public Overloads Property Value() As String
        Get
            Return Me.valueField1
        End Get
        Set(value As String)
            Me.valueField1 = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://tempuri.org/actionsXMLSchema.xsd")> _
Partial Public Class actionParameters

    Private parameterField As actionParametersParameter

    '''<remarks/>
    Public Property parameter() As actionParametersParameter
        Get
            Return Me.parameterField
        End Get
        Set(value As actionParametersParameter)
            Me.parameterField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://tempuri.org/actionsXMLSchema.xsd")> _
Partial Public Class actionParametersParameter

    Private nameField As String

    Private valueField As String

    Private valueField1 As String

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property name() As String
        Get
            Return Me.nameField
        End Get
        Set(value As String)
            Me.nameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Overloads Property value() As String
        Get
            Return Me.valueField
        End Get
        Set(value As String)
            Me.valueField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlTextAttribute()> _
    Public Overloads Property Value() As String
        Get
            Return Me.valueField1
        End Get
        Set(value As String)
            Me.valueField1 = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://tempuri.org/actionsXMLSchema.xsd")> _
Partial Public Class actionContent

    Private parametersField As actionContentParameters

    Private filesField As actionContentFiles

    Private typeField As String

    '''<remarks/>
    Public Property parameters() As actionContentParameters
        Get
            Return Me.parametersField
        End Get
        Set(value As actionContentParameters)
            Me.parametersField = value
        End Set
    End Property

    '''<remarks/>
    Public Property files() As actionContentFiles
        Get
            Return Me.filesField
        End Get
        Set(value As actionContentFiles)
            Me.filesField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property type() As String
        Get
            Return Me.typeField
        End Get
        Set(value As String)
            Me.typeField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://tempuri.org/actionsXMLSchema.xsd")> _
Partial Public Class actionContentParameters

    Private parameterField As actionContentParametersParameter

    '''<remarks/>
    Public Property parameter() As actionContentParametersParameter
        Get
            Return Me.parameterField
        End Get
        Set(value As actionContentParametersParameter)
            Me.parameterField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://tempuri.org/actionsXMLSchema.xsd")> _
Partial Public Class actionContentParametersParameter

    Private nameField As String

    Private valueField As String

    Private valueField1 As String

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property name() As String
        Get
            Return Me.nameField
        End Get
        Set(value As String)
            Me.nameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Overloads Property value() As String
        Get
            Return Me.valueField
        End Get
        Set(value As String)
            Me.valueField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlTextAttribute()> _
    Public Overloads Property Value() As String
        Get
            Return Me.valueField1
        End Get
        Set(value As String)
            Me.valueField1 = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://tempuri.org/actionsXMLSchema.xsd")> _
Partial Public Class actionContentFiles

    Private fileField As actionContentFilesFile

    '''<remarks/>
    Public Property file() As actionContentFilesFile
        Get
            Return Me.fileField
        End Get
        Set(value As actionContentFilesFile)
            Me.fileField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://tempuri.org/actionsXMLSchema.xsd")> _
Partial Public Class actionContentFilesFile

    Private sequenceField As SByte

    Private sequenceFieldSpecified As Boolean

    Private filepathField As String

    Private idField As SByte

    Private idFieldSpecified As Boolean

    Private formatField As String

    Private typeField As String

    Private descriptionField As String

    Private valueField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property sequence() As SByte
        Get
            Return Me.sequenceField
        End Get
        Set(value As SByte)
            Me.sequenceField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property sequenceSpecified() As Boolean
        Get
            Return Me.sequenceFieldSpecified
        End Get
        Set(value As Boolean)
            Me.sequenceFieldSpecified = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property filepath() As String
        Get
            Return Me.filepathField
        End Get
        Set(value As String)
            Me.filepathField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property id() As SByte
        Get
            Return Me.idField
        End Get
        Set(value As SByte)
            Me.idField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property idSpecified() As Boolean
        Get
            Return Me.idFieldSpecified
        End Get
        Set(value As Boolean)
            Me.idFieldSpecified = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property format() As String
        Get
            Return Me.formatField
        End Get
        Set(value As String)
            Me.formatField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property type() As String
        Get
            Return Me.typeField
        End Get
        Set(value As String)
            Me.typeField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property description() As String
        Get
            Return Me.descriptionField
        End Get
        Set(value As String)
            Me.descriptionField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlTextAttribute()> _
    Public Property Value() As String
        Get
            Return Me.valueField
        End Get
        Set(value As String)
            Me.valueField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://tempuri.org/actionsXMLSchema.xsd")> _
Partial Public Class actionWorkflows

    Private workflowField As actionWorkflowsWorkflow

    '''<remarks/>
    Public Property workflow() As actionWorkflowsWorkflow
        Get
            Return Me.workflowField
        End Get
        Set(value As actionWorkflowsWorkflow)
            Me.workflowField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://tempuri.org/actionsXMLSchema.xsd")> _
Partial Public Class actionWorkflowsWorkflow

    Private parametersField() As actionWorkflowsWorkflowParameter

    Private typeField As String

    Private nameField As String

    Private versionField As String

    Private idField As SByte

    Private idFieldSpecified As Boolean

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute("parameter", IsNullable:=False)> _
    Public Property parameters() As actionWorkflowsWorkflowParameter()
        Get
            Return Me.parametersField
        End Get
        Set(value As actionWorkflowsWorkflowParameter())
            Me.parametersField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property type() As String
        Get
            Return Me.typeField
        End Get
        Set(value As String)
            Me.typeField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property name() As String
        Get
            Return Me.nameField
        End Get
        Set(value As String)
            Me.nameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property version() As String
        Get
            Return Me.versionField
        End Get
        Set(value As String)
            Me.versionField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property id() As SByte
        Get
            Return Me.idField
        End Get
        Set(value As SByte)
            Me.idField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property idSpecified() As Boolean
        Get
            Return Me.idFieldSpecified
        End Get
        Set(value As Boolean)
            Me.idFieldSpecified = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://tempuri.org/actionsXMLSchema.xsd")> _
Partial Public Class actionWorkflowsWorkflowParameter

    Private nameField As String

    Private valueField As String

    Private valueField1 As String

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property name() As String
        Get
            Return Me.nameField
        End Get
        Set(value As String)
            Me.nameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Overloads Property value() As String
        Get
            Return Me.valueField
        End Get
        Set(value As String)
            Me.valueField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlTextAttribute()> _
    Public Overloads Property Value() As String
        Get
            Return Me.valueField1
        End Get
        Set(value As String)
            Me.valueField1 = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://tempuri.org/actionsXMLSchema.xsd")> _
Partial Public Class actionGroups

    Private groupField As actionGroupsGroup

    '''<remarks/>
    Public Property group() As actionGroupsGroup
        Get
            Return Me.groupField
        End Get
        Set(value As actionGroupsGroup)
            Me.groupField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://tempuri.org/actionsXMLSchema.xsd")> _
Partial Public Class actionGroupsGroup

    Private parametersField As actionGroupsGroupParameters

    Private idField As Short

    Private idFieldSpecified As Boolean

    Private nameField As String

    Private sequenceField As SByte

    Private sequenceFieldSpecified As Boolean

    '''<remarks/>
    Public Property parameters() As actionGroupsGroupParameters
        Get
            Return Me.parametersField
        End Get
        Set(value As actionGroupsGroupParameters)
            Me.parametersField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property id() As Short
        Get
            Return Me.idField
        End Get
        Set(value As Short)
            Me.idField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property idSpecified() As Boolean
        Get
            Return Me.idFieldSpecified
        End Get
        Set(value As Boolean)
            Me.idFieldSpecified = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property name() As String
        Get
            Return Me.nameField
        End Get
        Set(value As String)
            Me.nameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property sequence() As SByte
        Get
            Return Me.sequenceField
        End Get
        Set(value As SByte)
            Me.sequenceField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property sequenceSpecified() As Boolean
        Get
            Return Me.sequenceFieldSpecified
        End Get
        Set(value As Boolean)
            Me.sequenceFieldSpecified = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://tempuri.org/actionsXMLSchema.xsd")> _
Partial Public Class actionGroupsGroupParameters

    Private parameterField As actionGroupsGroupParametersParameter

    '''<remarks/>
    Public Property parameter() As actionGroupsGroupParametersParameter
        Get
            Return Me.parameterField
        End Get
        Set(value As actionGroupsGroupParametersParameter)
            Me.parameterField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://tempuri.org/actionsXMLSchema.xsd")> _
Partial Public Class actionGroupsGroupParametersParameter

    Private nameField As String

    Private valueField As Integer

    Private valueFieldSpecified As Boolean

    Private valueField1 As String

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property name() As String
        Get
            Return Me.nameField
        End Get
        Set(value As String)
            Me.nameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Overloads Property value() As Integer
        Get
            Return Me.valueField
        End Get
        Set(value As Integer)
            Me.valueField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property valueSpecified() As Boolean
        Get
            Return Me.valueFieldSpecified
        End Get
        Set(value As Boolean)
            Me.valueFieldSpecified = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlTextAttribute()> _
    Public Overloads Property Value() As String
        Get
            Return Me.valueField1
        End Get
        Set(value As String)
            Me.valueField1 = value
        End Set
    End Property
End Class

