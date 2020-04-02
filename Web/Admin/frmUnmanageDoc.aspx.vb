
Partial Class Admin_frmUnmanageDoc
    Inherits System.Web.UI.Page

    Dim controller As New UnManageDocController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindDocument(GetParentDocType(DdlParentType))
            BindData(GetParentDocType(DdlFilterParentDocType))
        End If
    End Sub

    Protected Sub DdlFilterParentDocType_SelectIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlFilterParentDocType.SelectedIndexChanged
        BindData(GetParentDocType(DdlFilterParentDocType))
    End Sub

    Protected Sub DdlParentDocType_SelectedIndexChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlParentType.SelectedIndexChanged
        If DdlParentType.SelectedIndex > 0 Then
            BindDocument(DdlParentType.SelectedValue)
        Else
            GvDocuments.DataSource = Nothing
            GvDocuments.DataBind()
        End If
    End Sub

    Protected Sub GvDocuments_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvDocuments.RowCommand
        If e.CommandName.Equals("adddoc") Then
            Dim info As New UnManageDocInfo
            info.DocId = Integer.Parse(e.CommandArgument.ToString())
            info.ParentDocType = DdlParentType.SelectedValue
            info.ModifiedUser = CommonSite.UserName
            Dim isSucceed As Boolean = AddUnManageDoc(info)
            If isSucceed = True Then
                LblWarningMessage.Text = "Document Added successfully"
                LblWarningMessage.Font.Italic = True
                LblWarningMessage.ForeColor = Drawing.Color.Green
                BindData(GetParentDocType(DdlFilterParentDocType))
                BindDocument(GetParentDocType(DdlParentType))
            Else
                LblWarningMessage.Text = "Document Inserted fail"
                LblWarningMessage.Font.Italic = True
                LblWarningMessage.ForeColor = Drawing.Color.Red
            End If
            LblGvWarningMessage.Visible = False
            LblWarningMessage.Visible = True
        End If
    End Sub

    Protected Sub GvDocuments_PageIndexChange(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvDocuments.PageIndexChanging
        GvDocuments.PageIndex = e.NewPageIndex
        BindDocument(GetParentDocType(DdlParentType))
    End Sub

    Protected Sub GvUnmanageDocs_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvUnManageDocs.RowCommand
        If e.CommandName.Equals("deletedoc") Then
            Dim isSucceed As Boolean = DeleteUnManageDoc(Integer.Parse(e.CommandArgument.ToString()))
            If isSucceed = True Then
                LblGvWarningMessage.Text = "Document Deleted Successfully"
                LblGvWarningMessage.Font.Italic = True
                LblGvWarningMessage.ForeColor = Drawing.Color.Green
                BindDocument(GetParentDocType(DdlParentType))
                BindData(GetParentDocType(DdlFilterParentDocType))
            Else
                LblGvWarningMessage.Text = "Document Deleted fail"
                LblGvWarningMessage.Font.Italic = True
                LblGvWarningMessage.ForeColor = Drawing.Color.Red
            End If
            LblGvWarningMessage.Visible = True
            LblWarningMessage.Visible = False
        End If
    End Sub

    Protected Sub GvUnManageDocs_PageIndexChange(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvUnManageDocs.PageIndexChanging
        GvUnManageDocs.PageIndex = e.NewPageIndex
        BindData(GetParentDocType(DdlFilterParentDocType))
    End Sub
#Region "Custom Methods"
    Private Sub BindData(ByVal parentdoctype As String)
        GvUnManageDocs.DataSource = controller.GetUnmanageDocs(parentdoctype)
        GvUnManageDocs.DataBind()
    End Sub

    Private Sub BindDocument(ByVal parentdoctype As String)
        GvDocuments.DataSource = controller.GetDocumentsBaseonParentDocType(parentdoctype)
        GvDocuments.DataBind()
    End Sub

    Private Function AddUnManageDoc(ByVal info As UnManageDocInfo) As Boolean
        Dim isSucceed As Boolean = controller.UnManageDoc_I(info)
        Return isSucceed
    End Function

    Private Function DeleteUnManageDoc(ByVal undocid As Integer) As Boolean
        Dim isSucceed As Boolean = controller.UnManageDoc_D(undocid)
        Return isSucceed
    End Function

    Private Function GetParentDocType(ByVal ddl As DropDownList) As String
        If ddl.SelectedIndex > 0 Then
            Return ddl.SelectedValue
        Else
            Return String.Empty
        End If
    End Function
#End Region

End Class
