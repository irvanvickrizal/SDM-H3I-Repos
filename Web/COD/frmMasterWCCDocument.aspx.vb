Imports System.Data
Imports System.Collections.Generic

Partial Class COD_frmMasterWCCDocument
    Inherits System.Web.UI.Page
    Dim controller As New WCCController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
            BindParentDocument()
        End If
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnAdd.Click
        If Not String.IsNullOrEmpty(TxtDocName.Text) Then
            Dim parentid As Integer = 0
            If DdlParentDocument.SelectedIndex > 0 Then
                parentid = Integer.Parse(DdlParentDocument.SelectedValue)
            End If
            AddUpdateData(0, TxtDocName.Text, TxtDocDescription.Text, parentid, DdlDocType.SelectedValue)
            ClearTextField()
            BindData()
            BindParentDocument()
        End If
    End Sub

    Protected Sub GvMasterWCCDocument_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvMasterWCCDocument.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow And (e.Row.RowState And DataControlRowState.Edit) = DataControlRowState.Edit) Then
            Dim hdnDocId As HiddenField = CType(e.Row.FindControl("HdnDocId"), HiddenField)
            Dim hdnParentId As HiddenField = CType(e.Row.FindControl("HdnParentId"), HiddenField)
            Dim ddlParentDoc As DropDownList = CType(e.Row.FindControl("DdlParentDocs"), DropDownList)
            If Not hdnDocId Is Nothing And Not hdnParentId Is Nothing And Not ddlParentDoc Is Nothing Then
                BindEditParentDocument(ddlParentDoc, Integer.Parse(hdnDocId.Value), Integer.Parse(hdnParentId.Value))
            End If
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim desc As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DocName"))
            Dim lnkbtnresult As ImageButton = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)

            If Not lnkbtnresult Is Nothing Then
                lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + desc + "')")
            End If
        End If
    End Sub

    Protected Sub gvmasterwccdoc_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim docid As Integer = Integer.Parse(GvMasterWCCDocument.DataKeys(e.RowIndex).Values("WCCDocument_Id").ToString())
        DeleteData(docid)
        BindData()
        BindParentDocument()
    End Sub

    Protected Sub gvmasterwccdoc_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim docid As Integer = Integer.Parse(GvMasterWCCDocument.DataKeys(e.RowIndex).Value.ToString())
        Dim TxtGvDocName As TextBox = CType(GvMasterWCCDocument.Rows(e.RowIndex).FindControl("TxtGvDocName"), TextBox)
        Dim TxtGvDocDesc As TextBox = CType(GvMasterWCCDocument.Rows(e.RowIndex).FindControl("TxtGvDocDesc"), TextBox)
        Dim docType As HiddenField = CType(GvMasterWCCDocument.Rows(e.RowIndex).FindControl("HdnDocType"), HiddenField)
        Dim parentId As HiddenField = CType(GvMasterWCCDocument.Rows(e.RowIndex).FindControl("HdnParentId"), HiddenField)
        GvMasterWCCDocument.EditIndex = -1
        AddUpdateData(docid, TxtGvDocName.Text, TxtGvDocDesc.Text, parentId.Value, docType.Value.Trim())
        BindData()
    End Sub

    Protected Sub gvmasterwccdoc_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        GvMasterWCCDocument.EditIndex = -1
        BindData()
    End Sub

    Protected Sub gvmasterwccdoc_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        GvMasterWCCDocument.EditIndex = e.NewEditIndex
        Dim editingRows As GridViewRow = GvMasterWCCDocument.Rows(e.NewEditIndex)
        BindData()
        
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        Dim ds As DataSet = controller.GetWCCDocument_DS(False)
        If ds.Tables(0).Rows.Count > 0 Then
            GvMasterWCCDocument.DataSource = ds
            GvMasterWCCDocument.DataBind()
        Else
            GvMasterWCCDocument.DataSource = Nothing
            GvMasterWCCDocument.DataBind()
        End If
    End Sub

    Private Sub BindParentDocument()
        DdlParentDocument.DataSource = controller.GetWCCDocument(False)
        DdlParentDocument.DataTextField = "DocName"
        DdlParentDocument.DataValueField = "DocId"
        DdlParentDocument.DataBind()
        
        DdlParentDocument.Items.Insert(0, "--Select Parent Document--")
    End Sub

    Private Sub BindEditParentDocument(ByVal ddl As DropDownList, ByVal currentdocid As Integer, ByVal parentid As Integer)
        ddl.DataSource = controller.GetWCCParentDocument(False, currentdocid)
        ddl.DataTextField = "DocName"
        ddl.DataValueField = "DocId"
        ddl.DataBind()
        
        ddl.Items.Insert(0, "--Select Parent Doc--")
        If parentid > 0 Then
            ddl.SelectedValue = Convert.ToString(parentid)
        End If
    End Sub

    Private Sub AddUpdateData(ByVal docid As Integer, ByVal docname As String, ByVal docdesc As String, ByVal parentid As Integer, ByVal doctype As String)
        Dim info As New WCCCODDocumentInfo
        info.DocId = docid
        info.DocName = docname
        info.DocDesc = docdesc
        info.ParentId = parentid
        info.DocType = doctype
        info.WCCDocLMBY = CommonSite.UserName
        info.WCCDocLMDT = Now
        controller.WCCDocumentIU(info)
    End Sub

    Private Sub DeleteData(ByVal docid As Integer)
        controller.WCCDocumentDeleteTemp(docid)
    End Sub

    Private Sub ClearTextField()
        TxtDocName.Text = ""
        TxtDocDescription.Text = ""
    End Sub

    Private Sub GvMasterWCCDocument_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvMasterWCCDocument.PageIndexChanging
        GvMasterWCCDocument.PageIndex = e.NewPageIndex
        BindData()
    End Sub

#End Region
End Class
