Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class USR_frmChangeUsrDetails
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim objET As New ETEBASTUsers
    Dim objBO As New BOUserLD
    Dim objBD As New BODelete
    Dim objED As New ETDelete
    Dim objETR As New ETEBASTUserRole
    Dim dt As New DataTable
    Dim objBOS As New BOUserSetup
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnSave.Attributes.Add("onclick", "javascript:return checkIsSetupEmpty()")
        btnAdd.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        If Not IsPostBack Then
            hdnId.Value = Request.QueryString("ID")
            objBOD.fillDDL(ddlUsertype, "TUserType1", False, "")
            binddata()
            tblRole.Visible = False
            getlist()
            ddlRole1.Enabled = False
            ddlLevel.Enabled = False
            If ddlRole1.SelectedValue = Constants.Subcon_SubAdmin_RoleID Or ddlRole1.SelectedValue = Constants.Customer_SubAdmin_RoleID Then
                ddlLevel.Enabled = False
                ddlRole1.Enabled = False
                grdRoleList.Visible = False
            End If
            If rblEdit.SelectedValue = "R" Then
                ddlRole1.Enabled = True
                ddlLevel.Enabled = True
            End If
            If ddlLevel.SelectedValue = "N" Then
                rblEdit.Items.RemoveAt(1)
            End If
            'If ddlRole1.SelectedValue = Constants.NSN_SS_RoleID Or ddlRole1.SelectedValue = Constants.Customer_SS_RoleID Or ddlRole1.SelectedValue = Constants.Subcon_SS_RoleID Then
            'rblEdit.Items.RemoveAt(1)
            'End If
        End If
    End Sub
    Sub binddata()
        dt = New DataTable
        dt = objBO.uspEBASTUsersLD1(Request.QueryString("ID"), "", "", "", "", 0, )
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdnUId.Value = .Item("USR_ID").ToString
                ddlUsertype.SelectedValue = .Item("USRTYPE").ToString
                hdnUsertype.Value = ddlUsertype.SelectedValue
                lblUser.InnerText = ddlUsertype.SelectedItem.Text
                objBOD.fillDDL(ddlLevel, "LVLMaster", False, "")
                objBOD.fillDDL(ddlLvl, "LVLMaster", False, "")
                ddlLevel.SelectedValue = .Item("LVLCODE")
                hdnSRCID.Value = .Item("SRCID")
                Dim i As Integer
                If hdnUsertype.Value = "N" Then
                    i = 1
                ElseIf hdnUsertype.Value = "S" Then
                    i = 2
                ElseIf hdnUsertype.Value = "C" Then
                    i = 4
                End If
                Dim a As String = ddlLevel.SelectedValue
                objBOD.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
                objBOD.fillDDL(ddlRole, "TRole2", i, a, True, Constants._DDL_Default_Select)
                'objBOD.fillDDL(ddlRole1, "TRole1", ddlUsertype.SelectedValue, False, "")
                ddlRole1.SelectedValue = .Item("USRROLE")
                lblName.InnerText = .Item("NAME").ToString
                lblLogin.InnerText = .Item("USRLOGIN").ToString
            End With
        End If
    End Sub
    Sub SaveData()
        dt = New DataTable
        dt = objBO.uspEBASTUsersLD1(hdnId.Value, "", "", "", "", 0, )
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                objET.USR_ID = .Item("USR_ID")
                objET.EPM_ID = .Item("EPM_ID")
                objET.USRType = .Item("USRTYPE")
                objET.SRCID = .Item("SRCID")
                objET.Name = .Item("NAME")
                objET.USRLogin = .Item("USRLOGIN")
                objET.USRPassword = FormsAuthentication.HashPasswordForStoringInConfigFile("ebast123TAKE", "MD5")
                objET.Email = .Item("EMAIL")
                objET.PhoneNo = .Item("PHONENO")
                If Session("User_Type") = Constants.User_Type_NSN Then
                    objET.Approved = 1
                Else
                    objET.Approved = 0
                End If
                objET.ACC_Status = "A"
                objET.Firsttime_Login = 0
                objET.AT.RStatus = Constants.STATUS_ACTIVE
                objET.AT.LMBY = Session("User_Name")
            End With
        End If
        objET.LVLCode = ddlLevel.SelectedValue
        objET.USRRole = ddlRole1.SelectedValue
    End Sub

    Protected Sub ddlLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLevel.SelectedIndexChanged
        Dim i As Integer
        If hdnUsertype.Value = "N" Then
            i = 1
        ElseIf hdnUsertype.Value = "S" Then
            i = 2
        ElseIf hdnUsertype.Value = "C" Then
            i = 4
        End If
        Dim a As String = ddlLevel.SelectedValue
        Dim li As New ListItem
        Dim j As Integer
        If ddlLevel.SelectedValue = "N" Then
            objBOD.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            If Session("Role_Id") = Constants.Subcon_SubAdmin_RoleID Then
                li = ddlRole1.Items.FindByValue(Constants.Subcon_SubAdmin_RoleID)
                j = ddlRole1.Items.IndexOf(li)
                ddlRole1.Items.RemoveAt(j)
            ElseIf Session("Role_Id") = Constants.Customer_SubAdmin_RoleID Then
                li = ddlRole1.Items.FindByValue(Constants.Customer_SubAdmin_RoleID)
                j = ddlRole1.Items.IndexOf(li)
                ddlRole1.Items.RemoveAt(j)
            ElseIf Session("Role_Id") = Constants.Sysadmin_RoleID Then
                If ddlUsertype.SelectedValue = "N" Then
                    li = ddlRole1.Items.FindByValue(Constants.Sysadmin_RoleID)
                    j = ddlRole1.Items.IndexOf(li)
                    ddlRole1.Items.RemoveAt(j)
                End If
            End If
        ElseIf ddlLevel.SelectedValue = "A" Then
            ddlRole1.Items.Clear()
            objBOD.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBOD.fillDDL(ddlRole, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBOD.fillDDL(ddlJava, "CODJAVA", True, Constants._DDL_Default_Select)
            trRegion.Visible = False
            trZone.Visible = False
            trSite.Visible = False
        ElseIf ddlLevel.SelectedValue = "R" Then
            ddlRole1.Items.Clear()
            objBOD.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBOD.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBOD.fillDDL(ddlJava, "CODJAVA", True, Constants._DDL_Default_Select)
            ddlArea.SelectedValue = 0
            trRegion.Visible = True
            ddlRegion.SelectedValue = 0
            trZone.Visible = False
            trSite.Visible = False
        ElseIf ddlLevel.SelectedValue = "Z" Then
            ddlRole1.Items.Clear()
            objBOD.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBOD.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBOD.fillDDL(ddlJava, "CODJAVA", True, Constants._DDL_Default_Select)
            ddlArea.SelectedValue = 0
            trRegion.Visible = True
            ddlRegion.SelectedValue = 0
            trZone.Visible = True
            ddlZone.SelectedValue = 0
            trSite.Visible = False
        ElseIf ddlLevel.SelectedValue = "S" Then
            ddlRole1.Items.Clear()
            objBOD.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBOD.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBOD.fillDDL(ddlJava, "CODJAVA", True, Constants._DDL_Default_Select)
            ddlArea.SelectedValue = 0
            trRegion.Visible = True
            ddlRegion.SelectedValue = 0
            trZone.Visible = True
            ddlZone.SelectedValue = 0
            trSite.Visible = True
            ddlSite.SelectedValue = 0
        ElseIf ddlLevel.SelectedValue = "J" Then
            ddlRole1.Items.Clear()
            objBOD.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBOD.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBOD.fillDDL(ddlJava, "CODJAVA", True, Constants._DDL_Default_Select)
            trArea.Visible = False
            ddlArea.SelectedValue = 0
            trRegion.Visible = False
            ddlRegion.SelectedValue = 0
            trZone.Visible = False
            ddlZone.SelectedValue = 0
            trSite.Visible = False
            ddlSite.SelectedValue = 0

        End If
    End Sub

    Protected Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Response.Redirect("frmUserList.aspx?Type=U2")
        'Page.ClientScript.RegisterStartupScript(GetType(UpdatePanel), "redirect", "location.href = 'frmUserList.aspx?Type=U2'", True)
    End Sub

    Sub getlist()
        dt = objBOS.uspEBASTUserRoleL(Request.QueryString("ID"), 0)
        'grdRoleList.PageSize = Session("Page_size")
        grdRoleList.DataSource = dt
        grdRoleList.DataBind()
        'If trZone.Visible = True Then
        '    grdRoleList.Columns(4).Visible = False
        'ElseIf trRegion.Visible = True And trZone.Visible = False Then
        '    grdRoleList.Columns(4).Visible = False
        '    grdRoleList.Columns(5).Visible = False
        'ElseIf trArea.Visible = True And trRegion.Visible = False Then
        '    grdRoleList.Columns(3).Visible = False
        '    grdRoleList.Columns(4).Visible = False
        '    grdRoleList.Columns(5).Visible = False
        'ElseIf trJava.Visible = True And trArea.Visible = False Then
        '    grdRoleList.Columns(2).Visible = False
        '    grdRoleList.Columns(3).Visible = False
        '    grdRoleList.Columns(4).Visible = False
        '    grdRoleList.Columns(5).Visible = False
        'End If
    End Sub

    Protected Sub grdRoleList_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdRoleList.RowDeleting
        Dim sno As Integer = grdRoleList.DataKeys(e.RowIndex).Value
        objED.TableName = "ebastuserrole"
        objED.FieldName = "sno"
        objED.FieldValue = sno
        BOcommon.result(objBD.uspDelete(objED), False, "", "", Constants._DELETE)
        getlist()
    End Sub

    Protected Sub grdRoleList_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdRoleList.RowCreated
        If e.Row.RowType = DataControlRowType.Header Or e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(6).Visible = False
        End If
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("frmUserList.aspx?Type=U2")
        'Page.ClientScript.RegisterStartupScript(GetType(UpdatePanel), "redirect", "location.href = 'frmUserList.aspx?Type=U2'", True)

    End Sub

    'Sub go(ByVal sender As Object, ByVal e As System.EventArgs)
    '    btnAdd.Visible = True
    '    btnAdd.Text = "Update"
    '    Dim lnk As LinkButton = CType(sender, LinkButton)
    '    Dim row As GridViewRow = CType(lnk.NamingContainer, GridViewRow)
    '    Dim sno As String
    '    sno = grdRoleList.Rows(row.RowIndex).Cells(5).Text
    '    hdnID.Value = Int32.Parse(sno)
    '    dt = objBOS.uspEBASTUserRoleL(0, hdnId.Value)
    '    If dt.Rows.Count > 0 Then
    '        With dt.Rows(0)
    '            tblRole.Visible = True
    '            trLevel.Visible = False
    '            trRole.Visible = False
    '            'trArea.Visible = True
    '            'trRegion.Visible = True
    '            'trZone.Visible = True
    '            'trSite.Visible = True
    '            objBOD.fillDDL(ddlJava, "CODJAVA", True, Constants._DDL_Default_Select)
    '            If IsDBNull(.Item("ARA_ID")) Then
    '                trArea.Visible = False
    '                trRegion.Visible = False
    '                trZone.Visible = False
    '                trSite.Visible = False
    '            Else
    '                If .Item("ARA_ID").ToString <> 0 Then
    '                    ddlJava.SelectedValue = .Item("JV_ID").ToString
    '                    ddlJava_SelectedIndexChanged(Nothing, Nothing)
    '                    ddlArea.SelectedValue = .Item("ARA_ID").ToString
    '                    ddlArea_SelectedIndexChanged(Nothing, Nothing)
    '                    If .Item("RGN_ID").ToString <> 0 Then
    '                        ddlRegion.SelectedValue = .Item("RGN_ID").ToString
    '                        ddlRegion_SelectedIndexChanged(Nothing, Nothing)
    '                        If .Item("ZN_ID").ToString <> 0 Then
    '                            ddlZone.SelectedValue = .Item("ZN_ID").ToString
    '                            ddlZone_SelectedIndexChanged(Nothing, Nothing)
    '                            If .Item("SITE_ID").ToString <> 0 Then
    '                                ddlSite.SelectedValue = .Item("SITE_ID").ToString
    '                            Else
    '                                trSite.Visible = False
    '                            End If
    '                        Else
    '                            trZone.Visible = False
    '                            trSite.Visible = False
    '                        End If
    '                    Else
    '                        trRegion.Visible = False
    '                        trZone.Visible = False
    '                        trSite.Visible = False
    '                    End If
    '                Else
    '                    trArea.Visible = False
    '                    trRegion.Visible = False
    '                    trZone.Visible = False
    '                    trSite.Visible = False
    '                End If
    '            End If
    '        End With
    '    End If
    '    If ddlRole.SelectedValue = Constants.NSN_SS_RoleID Or ddlRole.SelectedValue = Constants.Subcon_SS_RoleID Or ddlRole.SelectedValue = Constants.Customer_SS_RoleID Then
    '        trSite.Style.Add("display", "none")
    '    Else
    '        trSite.Style.Add("display", "")
    '    End If
    'End Sub

    Protected Sub ddlJava_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlJava.SelectedIndexChanged
        ddlArea.Items.Clear()
        ddlRegion.Items.Clear()
        ddlZone.Items.Clear()
        ddlSite.Items.Clear()
        If ddlJava.SelectedIndex <> 0 Then
            objBOD.fillDDL(ddlArea, "CODArea", ddlJava.SelectedValue, True, Constants._DDL_Default_Select)
        End If
    End Sub

    Protected Sub ddlArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlArea.SelectedIndexChanged
        ddlRegion.Items.Clear()
        ddlZone.Items.Clear()
        ddlSite.Items.Clear()
        If ddlArea.SelectedIndex <> 0 Then
            objBOD.fillDDL(ddlRegion, "CODRegion", ddlArea.SelectedValue, True, Constants._DDL_Default_Select)
        End If
    End Sub

    Protected Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged
        ddlZone.Items.Clear()
        ddlSite.Items.Clear()
        If ddlRegion.SelectedIndex <> 0 Then
            objBOD.fillDDL(ddlZone, "CODZone", ddlRegion.SelectedValue, True, Constants._DDL_Default_Select)
        End If
    End Sub

    Protected Sub ddlZone_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlZone.SelectedIndexChanged
        ddlSite.Items.Clear()
        If ddlZone.SelectedIndex <> 0 Then
            objBOD.fillDDL(ddlSite, "CODSite", ddlZone.SelectedValue, True, Constants._DDL_Default_Select)
        End If
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        updateRole()
        Dim i As Integer = objBOS.uspEBASTUserRoleI(objETR, hdnSRCID.Value)
        If i = 0 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
        ElseIf i = -2 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Supervisor already exists');", True)
        ElseIf i = -3 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Current user already exists in this location');", True)
        ElseIf i = 1 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Data Updated successfully');", True)
            ddlArea.SelectedIndex = 0
            ddlRegion.Items.Clear()
            ddlZone.Items.Clear()
            ddlSite.Items.Clear()
            tblRole.Visible = False
            trTask.Visible = True
            hdnId.Value = i
        End If
        getlist()
        rblEdit.SelectedValue = Nothing
    End Sub

    Sub updateRole()
        If btnAdd.Text = "Update" Then
            objETR.sno = hdnId.Value
            objETR.RoleType = ddlRole1.SelectedValue
        Else
            objETR.RoleType = ddlRole1.SelectedValue
        End If
        objETR.USR_ID = hdnUID.Value
        objETR.USRType = hdnUsertype.Value
        objETR.LVLCode = ddlLvl.SelectedValue
        objETR.JV_ID = IIf(ddlJava.SelectedValue <> "", ddlJava.SelectedValue, 0)
        objETR.ARA_ID = IIf(ddlArea.SelectedValue <> "", ddlArea.SelectedValue, 0)
        objETR.RGN_ID = IIf(ddlRegion.SelectedValue <> "", ddlRegion.SelectedValue, 0)
        objETR.ZN_ID = IIf(ddlZone.SelectedValue <> "", ddlZone.SelectedValue, 0)
        objETR.Site_ID = IIf(ddlSite.SelectedValue <> "", ddlSite.SelectedValue, 0)
        objETR.AT.RStatus = Constants.STATUS_ACTIVE
        objETR.AT.LMBY = Session("User_Name")
    End Sub

    Protected Sub rblEdit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblEdit.SelectedIndexChanged
        If rblEdit.SelectedValue = "R" Then
            trTask.Visible = True
            dt = objBOS.uspEBASTUserRoleL(hdnUId.Value)
            If dt.Rows.Count > 0 Then
                Response.Write("<script>alert('Cannot change the Role.')</script>")
                rblEdit.SelectedValue = Nothing
            Else
                ddlLevel.Enabled = True
                ddlRole1.Enabled = True
                tblRole.Visible = False
            End If
        ElseIf rblEdit.SelectedValue = "P" Then
            grdRoleList.Columns(6).Visible = True
            trTask.Visible = False
            btnAdd.Text = "Add"
            ddlLevel.Enabled = False
            ddlRole1.Enabled = False
            tblRole.Visible = True
            ddlLvl.SelectedValue = ddlLevel.SelectedValue
            ddlLvl.Enabled = False
            objBOD.fillDDL(ddlJava, "CODJAVA", True, Constants._DDL_Default_Select)
            If ddlLevel.SelectedValue = "J" Then
                trArea.Visible = False
                trRegion.Visible = False
                trZone.Visible = False
                trSite.Visible = False
            ElseIf ddlLevel.SelectedValue = "A" Then
                trRegion.Visible = False
                trZone.Visible = False
                trSite.Visible = False
            ElseIf ddlLevel.SelectedValue = "R" Then
                ddlArea.SelectedValue = 0
                trRegion.Visible = True
                ddlRegion.SelectedValue = 0
                trZone.Visible = False
                trSite.Visible = False
            ElseIf ddlLevel.SelectedValue = "Z" Then
                ddlArea.SelectedValue = 0
                trRegion.Visible = True
                ddlRegion.SelectedValue = 0
                trZone.Visible = True
                ddlZone.SelectedValue = 0
                trSite.Visible = False
            ElseIf ddlLevel.SelectedValue = "S" Then
                ddlArea.SelectedValue = 0
                trRegion.Visible = True
                ddlRegion.SelectedValue = 0
                trZone.Visible = True
                ddlZone.SelectedValue = 0
                trSite.Visible = True
                ddlSite.SelectedValue = 0
            End If
            ddlRole.SelectedValue = ddlRole1.SelectedValue
            ddlRole.Enabled = False
            If ddlRole1.SelectedValue = Constants.NSN_SS_RoleID Or ddlRole1.SelectedValue = Constants.Subcon_SS_RoleID Or ddlRole1.SelectedValue = Constants.Customer_SS_RoleID Then
                trSite.Style.Add("display", "none")
            Else
                trSite.Style.Add("display", "")
            End If
        End If
        getlist()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
        Dim i As Integer = objBOS.uspEBASTUsersI(objET)
        If i = 0 Then
            Response.Write("<script>alert('Error while doing the transaction.')</script>")
        Else
            hdnUId.Value = i
            tblRole.Visible = True
            objBOD.fillDDL(ddlLvl, "LVLMaster", False, "")
            ddlLvl.SelectedValue = ddlLevel.SelectedValue
            Dim j As Integer
            If hdnUsertype.Value = "N" Then
                j = 1
            ElseIf hdnUsertype.Value = "S" Then
                j = 2
            ElseIf hdnUsertype.Value = "C" Then
                j = 4
            End If
            Dim a As String = ddlLevel.SelectedValue
            objBOD.fillDDL(ddlRole, "TRole2", j, a, True, Constants._DDL_Default_Select)
            objBOD.fillDDL(ddlJava, "CODJava", True, Constants._DDL_Default_Select)
            If ddlLevel.SelectedValue = "N" Or ddlRole1.SelectedValue = Constants.NSN_SS_RoleID Or ddlRole1.SelectedValue = Constants.Subcon_SS_RoleID Or ddlRole1.SelectedValue = Constants.Customer_SS_RoleID Then
                Response.Redirect("frmUserList.aspx?Type=U2")
            ElseIf ddlLevel.SelectedValue = "A" Then
                trRegion.Visible = False
                trZone.Visible = False
                trSite.Visible = False
            ElseIf ddlLevel.SelectedValue = "R" Then
                ddlArea.SelectedValue = 0
                trRegion.Visible = True
                ddlRegion.SelectedValue = 0
                trZone.Visible = False
                trSite.Visible = False
            ElseIf ddlLevel.SelectedValue = "Z" Then
                ddlArea.SelectedValue = 0
                trRegion.Visible = True
                ddlRegion.SelectedValue = 0
                trZone.Visible = True
                ddlZone.SelectedValue = 0
                trSite.Visible = False
            ElseIf ddlLevel.SelectedValue = "S" Then
                ddlArea.SelectedValue = 0
                trRegion.Visible = True
                ddlRegion.SelectedValue = 0
                trZone.Visible = True
                ddlZone.SelectedValue = 0
                trSite.Visible = True
                ddlSite.SelectedValue = 0
            End If
            ddlRole.SelectedValue = ddlRole1.SelectedValue
            btnAdd.Text = "Add"
            ddlLvl.Enabled = False
            ddlRole.Enabled = False
            ddlLevel.Enabled = False
            ddlRole1.Enabled = False
            If ddlRole1.SelectedValue = Constants.NSN_SS_RoleID Or ddlRole1.SelectedValue = Constants.Subcon_SS_RoleID Or ddlRole1.SelectedValue = Constants.Customer_SS_RoleID Then
                trSite.Style.Add("display", "none")
            Else
                trSite.Style.Add("display", "")
            End If
        End If
    End Sub

    Protected Sub grdRoleList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdRoleList.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = grdRoleList.PageIndex * grdRoleList.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Private Sub grdRoleList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdRoleList.PageIndexChanging
        grdRoleList.PageIndex = e.NewPageIndex
        getlist()
    End Sub
End Class
