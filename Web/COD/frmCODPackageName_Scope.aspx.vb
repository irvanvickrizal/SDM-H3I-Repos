
Partial Class COD_frmCODPackageName_Scope
    Inherits System.Web.UI.Page
    Dim pcontrol As New PackageNameController
    Dim scontrol As New ScopeController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            BindData()
            BindScopeDetails()
        End If
    End Sub

    Protected Sub GvPackageName_ItemCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvPackageNames.RowCommand
        If e.CommandName.Equals("editpackagename") Then

        ElseIf e.CommandName.Equals("deletepackagename") Then

        End If
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        If Integer.Parse(DdlScopeDetails.SelectedValue) > 0 Then
            'Commented by Fauzan, 20 Dec 2018. Define the PackageNameInfo object inside SaveEditData function
            'Dim info As New PackageNameInfo
            'info.PackageName = TxtPackageName.Text
            'info.Description = TxtDescription.Text
            'info.PackageNameId = 0
            'info.IsActive = True
            ''info.LMBY = CommonSite.UserName
            'info.LMBY = "Administrator"
            'info.LMDT = DateTime.Now
            'info.DScopeId = Integer.Parse(DdlScopeDetails.SelectedValue)
            'If SaveEditData(info) = False Then
            If SaveEditData(0, TxtPackageName.Text, TxtDescription.Text, Integer.Parse(DdlScopeDetails.SelectedValue)) = False Then
                If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                    Page.ClientScript.RegisterStartupScript _
                    (Me.GetType(), "alert", "FailedSaved();", True)
                End If
            Else
                BindData()
                ClearForm()
                If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                    Page.ClientScript.RegisterStartupScript _
                    (Me.GetType(), "alert", "SucceedSaved();", True)
                End If
            End If
        End If
    End Sub

    Protected Sub BtnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        ClearForm()
    End Sub

    'Commented by Fauzan, 20 Dec 2018
    'Protected Sub BtnCancelUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancelUpdate.Click

    'End Sub

    'Protected Sub BtnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnUpdate.Click

    'End Sub


#Region "Custom Methods"
    Private Sub BindData()
        'MvCorePanel.SetActiveView(VwMainPanel)     'Commented by Fauzan, 20 Dec 2018
        GvPackageNames.DataSource = pcontrol.GetPackageNames(True)
        GvPackageNames.DataBind()
    End Sub

    Private Sub BindScopeDetails()
        DdlScopeDetails.DataSource = scontrol.GetAllDetailScopes(False, 0)
        DdlScopeDetails.DataTextField = "DScopeName"
        DdlScopeDetails.DataValueField = "DScopeId"
        DdlScopeDetails.DataBind()
        DdlScopeDetails.Items.Insert(0, "-- Select Scope Detail --")
    End Sub

    Private Sub ClearForm()
        TxtPackageName.Text = ""
        TxtDescription.Text = ""
        BindScopeDetails()
    End Sub

    'Modified by Fauzan, 20 Dec 2018. Define the object PackageNameInfo inside here.
    'Private Function SaveEditData(ByVal info as PackageNameInfo) As Boolean
    Private Function SaveEditData(ByVal id As Integer, ByVal name As String, ByVal desc As String, ByVal dsScopeId As Integer) As Boolean
        Dim info As New PackageNameInfo
        info.PackageName = name
        info.Description = desc
        info.PackageNameId = id
        info.IsActive = True
        info.LMBY = "Administrator"
        info.LMDT = DateTime.Now
        info.DScopeId = dsScopeId
        Return pcontrol.PackageName_IU(info)
    End Function

    Private Sub DeleteData(ByVal packagenameid As Integer)
        pcontrol.DeletePackageNameTemp(packagenameid)
        BindData()
        BindScopeDetails()
    End Sub

    Private Sub GvPackageNames_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles GvPackageNames.RowCancelingEdit
        GvPackageNames.EditIndex = -1
        BindData()
    End Sub

    Private Sub GvPackageNames_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GvPackageNames.RowDeleting
        Dim scopeid As Integer = Integer.Parse(GvPackageNames.DataKeys(e.RowIndex).Values("PackageNameId").ToString())
        DeleteData(scopeid)
        BindData()
    End Sub

    Private Sub GvPackageNames_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GvPackageNames.RowEditing
        GvPackageNames.EditIndex = e.NewEditIndex
        BindData()
    End Sub

    Private Sub GvPackageNames_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GvPackageNames.RowUpdating
        Dim pckgId As Integer = Integer.Parse(GvPackageNames.DataKeys(e.RowIndex).Value.ToString())
        Dim pckgName As TextBox = CType(GvPackageNames.Rows(e.RowIndex).FindControl("txtPackageName"), TextBox)
        Dim pckgDesc As TextBox = CType(GvPackageNames.Rows(e.RowIndex).FindControl("txtPackageDesc"), TextBox)
        Dim dsId As HiddenField = DirectCast(GvPackageNames.Rows(e.RowIndex).FindControl("hiddenId"), HiddenField)
        GvPackageNames.EditIndex = -1
        SaveEditData(pckgId, pckgName.Text, pckgDesc.Text, dsId.Value)
        BindData()
    End Sub

    Private Sub GvPackageNames_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvPackageNames.PageIndexChanging
        GvPackageNames.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub GvPackageNames_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvPackageNames.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim desc As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PackageName"))
            Dim lnkbtnresult As ImageButton = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
            If Not lnkbtnresult Is Nothing Then
                lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + desc + "')")
            End If
        End If
    End Sub
#End Region

End Class
