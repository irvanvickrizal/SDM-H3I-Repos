
Partial Class COD_frmCODDocumentScopeGrouping
    Inherits System.Web.UI.Page
    Dim pcontroller As New PackageNameController
    Dim scontroller As New ScopeController
    Dim dcontroller As New DocController
    Dim dgcontroller As New DocGroupingController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            BindDocumentGrouping()
            BindScopeDetail()
            BindParentDocument()
        End If
    End Sub

    Protected Sub BtnAddParentDocument_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAddParentDocument.Click
        'Added by Fauzan, 24 Dec 2018.
        Dim info As DocScopeGroupingInfo = New DocScopeGroupingInfo
        info.DocId = DdlParentDocument.SelectedValue
        info.DScopeId = DdlScopeDetails.SelectedValue
        info.LMBY = CommonSite.UserId
        If dgcontroller.CODDOC_ScopeGrouping_ExecuteNonQuery(info, "insert") Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "SucceedSaved();", True)
            BindDocumentGrouping()
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "FailedSaved();", True)
        End If
    End Sub


#Region "Custom Methods"
    Private Sub BindDocumentGrouping()
        GvDocumentGroupings.DataSource = dgcontroller.GetCODDocuments_ScopeGrouping()
        GvDocumentGroupings.DataBind()
    End Sub

    Private Sub BindScopeDetail()
        'Modified by Fauzan, 24 Dec 2018.
        DdlScopeDetails.DataSource = scontroller.GetAllDetailScopes(False, 0)
        DdlScopeDetails.DataTextField = "DScopeName"
        DdlScopeDetails.DataValueField = "DScopeId"
        DdlScopeDetails.DataBind()
        DdlScopeDetails.Items.Insert(0, "-- Scope Details --")
    End Sub

    Private Sub BindParentDocument()
        DdlParentDocument.DataSource = dcontroller.GetParentDocuments()
        DdlParentDocument.DataTextField = "docname"
        DdlParentDocument.DataValueField = "docid"
        DdlParentDocument.DataBind()
        DdlParentDocument.Items.Insert(0, "-- Parent Document --")
    End Sub

    Private Sub GvDocumentGroupings_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvDocumentGroupings.PageIndexChanging
        GvDocumentGroupings.PageIndex = e.NewPageIndex
        BindDocumentGrouping()
    End Sub

    Private Sub GvDocumentGroupings_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvDocumentGroupings.RowCommand
        If (e.CommandName.Equals("deletedoc")) Then
            Dim value As Integer = Convert.ToInt32(e.CommandArgument)
            If value > -1 Then
                Dim info As DocScopeGroupingInfo = New DocScopeGroupingInfo()
                info.DocGroupId = value
                If dgcontroller.CODDOC_ScopeGrouping_ExecuteNonQuery(info, "delete") Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Successfully deleted Document Grouping of Scope');", True)
                    BindDocumentGrouping()
                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Failed to delete Document Grouping of Scope');", True)
                End If
            End If
        End If
    End Sub
#End Region

End Class
