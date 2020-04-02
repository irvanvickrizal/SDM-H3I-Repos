
Partial Class WCC_frmWCCDoc_Authorty
    Inherits System.Web.UI.Page

    Dim wcccontroller As New WCCController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            BindData()
            BtnAdd.Enabled = False
            BtnClear.Enabled = False
            pnlUserType.Visible = False
            PanelAuthorityBaseConfiguration(False, True)
        End If
    End Sub

    Protected Sub DdlAuthorityBase_IndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlAuthorityBase.SelectedIndexChanged
        If DdlAuthorityBase.SelectedValue = "0" Then
            pnlUserType.Visible = False
            BtnAdd.Enabled = False
            BtnClear.Enabled = False
            PanelAuthorityBaseConfiguration(False, False)
        Else
            pnlUserType.Visible = True
        End If
    End Sub

    Protected Sub DdlUserType_IndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlUserType.SelectedIndexChanged
        If DdlUserType.SelectedValue = "0" Then
            BindRoles(0)
            PanelAuthorityBaseConfiguration(False, False)
            BtnAdd.Enabled = False
            BtnClear.Enabled = False
        Else
            BindRoles(Integer.Parse(DdlUserType.SelectedValue))
            BtnAdd.Enabled = True
            BtnClear.Enabled = True
            PanelAuthorityBaseConfiguration(True, False)
        End If
    End Sub

    Protected Sub GvWCCCreatorAuthorties_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvWCCCreatorAuthorties.RowCommand
        If e.CommandName.Equals("deleteauth") Then
            DeleteWCCCreator(Integer.Parse(e.CommandArgument.ToString()))
        End If
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        If DdlAuthorityBase.SelectedValue = "1" Then
            Dim j As Integer
            Dim isTicked As Boolean = True
            Dim counter As Integer = 0
            For j = 0 To GvRoleBase.Rows.Count - 1
                Dim chk As New HtmlInputCheckBox
                chk = GvRoleBase.Rows(j).Cells(0).FindControl("ChkChecked")
                If chk.Checked = True Then
                    Dim hdnroleid As HiddenField = GvRoleBase.Rows(j).Cells(0).FindControl("HdnRoleId")
                    If Not String.IsNullOrEmpty(hdnroleid.Value) Then
                        Dim info As New WCCDocAuthorityInfo
                        info.UserId = 0
                        info.RoleId = Integer.Parse(hdnroleid.Value)
                        info.LMBY = CommonSite.UserName
                        wcccontroller.WCCDocumentAuthority_I(info)
                        counter += 1
                    End If
                
                End If
            Next
            If counter = 0 Then
                If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                    Page.ClientScript.RegisterStartupScript _
                    (Me.GetType(), "alert", "NoRoleChecked();", True)
                End If
            Else
                BindData()
                BindRoles(Integer.Parse(DdlUserType.SelectedValue))
                If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                    Page.ClientScript.RegisterStartupScript _
                    (Me.GetType(), "alert", "SucceedSaved();", True)
                End If
            End If

        End If
    End Sub

    Protected Sub BtnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        If DdlAuthorityBase.SelectedValue = "1" Then
            BindRoles(Integer.Parse(DdlUserType.SelectedValue))
        End If
    End Sub

    Protected Sub GvRoleBaseIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvRoleBase.PageIndexChanging
        GvRoleBase.PageIndex = e.NewPageIndex
        BindRoles(Integer.Parse(DdlUserType.SelectedValue))
    End Sub

    
#Region "Custom Methods"
    Private Sub BindData()
        GvWCCCreatorAuthorties.DataSource = wcccontroller.GetWCCCreators()
        GvWCCCreatorAuthorties.DataBind()
    End Sub

    Private Sub BindRoles(ByVal usertype As Integer)
        GvRoleBase.DataSource = wcccontroller.GetRolesContraintDocAuthority(usertype)
        GvRoleBase.DataBind()
    End Sub

    Private Sub PanelAuthorityBaseConfiguration(ByVal isVisible As Boolean, ByVal isPost As Boolean)
        pnlRoleBase.Visible = isVisible
        If isPost Then
            pnlUserBase.Visible = False 'temp config
        Else
            pnlUserBase.Visible = False 'temp config
        End If
    End Sub

    Private Sub DeleteWCCCreator(ByVal authid As Integer)
        wcccontroller.DeleteWCCCreators(authid)
        BindData()
    End Sub

#End Region

End Class
