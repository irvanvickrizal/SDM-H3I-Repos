Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data
Imports System.Web.Security
Partial Class USR_frmUserSetup
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim objBODP As New BODDLs
    Dim objET As New ETEBASTUsers
    Dim objETR As New ETEBASTUserRole
    Dim objBO As New BOUserSetup
    Dim dt As New DataTable
    Dim objutil As New DBUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        BtnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnCheck.Attributes.Add("onclick", "javascript:return checkEmail();")
        btnAdd.Attributes.Add("onclick", "javascript:return checkRole();")
        lblStatusForm.Visible = False       'Added by Fauzan, 14 Dec 2018.
        If Not IsPostBack Then
            tblRole.Visible = False
            trLevel.Visible = True
            objBOD.fillDDL(ddlLevel, "LVLMaster", False, "")
            objBOD.fillDDL(ddlLvl, "LVLMaster", False, "")
            objBODP.fillDDL(ddlUsertype, "TUserType", False, "")
            Dim i As Integer
            Dim a As String
            Select Case Session("User_Type").ToString
                Case Constants.User_Type_NSN
                    ddlUsertype.SelectedValue = 1
                    a = ddlLevel.SelectedValue
                    name.Visible = False
                    i = ddlUsertype.SelectedValue
                    objBODP.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
                    objBODP.fillDDL(ddlRole, "TRole2", i, a, True, Constants._DDL_Default_Select)
                    Dim li As New ListItem
                    li = ddlRole1.Items.FindByValue(Constants.Sysadmin_RoleID)
                    Dim j As Integer
                    j = ddlRole1.Items.IndexOf(li)
                    'bugfix110104 the array off limit
                    If j <> -1 Then
                        ddlRole1.Items.RemoveAt(j)
                    End If
                Case Constants.User_Type_SubCon
                    ddlUsertype.SelectedValue = 2
                    a = ddlLevel.SelectedValue
                    ddlUsertype.Enabled = False
                    name.Visible = True
                    lblName.InnerText = "SubCon Name"
                    objBOD.fillDDL(ddlSelect, "SubCon", True, Constants._DDL_Default_Select)
                    ddlSelect.Enabled = False
                    ddlSelect.SelectedValue = Session("SRCId").ToString
                    i = ddlUsertype.SelectedValue
                    objBODP.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
                    objBODP.fillDDL(ddlRole, "TRole2", i, a, True, Constants._DDL_Default_Select)
                    Dim li As New ListItem
                    li = ddlRole1.Items.FindByValue(Constants.Subcon_SubAdmin_RoleID)
                    Dim j As Integer
                    j = ddlRole1.Items.IndexOf(li)
                    'bugfix110104 the array off limit
                    If j <> -1 Then
                        ddlRole1.Items.RemoveAt(j)
                    End If
                Case Constants.User_Type_Customer
                    ddlUsertype.SelectedValue = 4
                    ddlLevel.SelectedValue = Constants.User_Type_Customer
                    a = ddlLevel.SelectedValue
                    name.Visible = True
                    lblName.InnerText = "Customer Name"
                    objBOD.fillDDL(ddlSelect, "Customer", False, "")
                    ddlSelect.Enabled = False
                    ddlSelect.SelectedValue = Session("SRCId").ToString
                    i = ddlUsertype.SelectedValue
                    objBODP.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
                    objBODP.fillDDL(ddlRole, "TRole2", i, a, True, Constants._DDL_Default_Select)
                    Dim li As New ListItem
                    li = ddlRole1.Items.FindByValue(Constants.Customer_SubAdmin_RoleID)
                    Dim j As Integer
                    j = ddlRole1.Items.IndexOf(li)
                    'bugfix110104 the array off limit
                    If j <> -1 Then
                        ddlRole1.Items.RemoveAt(j)
                    End If
            End Select
        End If
        If isUserSetupAble.Value = "no" Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "blockColForm('tblSetup');", True)
        End If
    End Sub

    Sub saveebastusers()
        With objET
            Select Case Session("User_Type").ToString
                Case Constants.User_Type_NSN
                    If ddlUsertype.SelectedValue = 1 Then
                        .USRType = Constants.User_Type_NSN
                    ElseIf ddlUsertype.SelectedValue = 2 Then
                        .USRType = Constants.User_Type_SubCon
                        .SRCID = hdnID.Value
                    ElseIf ddlUsertype.SelectedValue = 4 Then
                        .USRType = Constants.User_Type_Customer
                        .SRCID = hdnID.Value
                    ElseIf ddlUsertype.SelectedValue = 11 Then
                        .USRType = "H"
                        .SRCID = hdnID.Value
                    End If
                Case Constants.User_Type_SubCon
                    .USRType = Session("User_Type")
                    .SRCID = Session("SRCId")
                Case Constants.User_Type_Customer
                    .USRType = Session("User_Type")
                    .SRCID = Session("SRCId")
            End Select
            '.EPM_ID = txtEpmid.Value.Replace("'", "''")
            .Name = txtName.Value.Replace("'", "''")
            .LVLCode = ddlLevel.SelectedValue
            .USRRole = ddlRole1.SelectedValue
            .USRLogin = txtLodinId.Value.Replace("'", "''")
            .USRPassword = FormsAuthentication.HashPasswordForStoringInConfigFile("ebast123TAKE", "MD5")
            .Email = txtEmail.Value.Replace("'", "''")
            .PhoneNo = txtPhoneOff.Value.Replace("'", "''")
            If Session("User_Type") = Constants.User_Type_NSN Then
                .Approved = 1
            Else
                .Approved = 0
            End If
            .ACC_Status = "A"
            .Firsttime_Login = 0
            .AT.RStatus = Constants.STATUS_ACTIVE
            .AT.LMBY = Session("User_Name")
            .DGsign = rblsign.SelectedItem.Value
        End With
        hdnSRCID.Value = objET.SRCID
    End Sub
    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Dim objdg As New DGSignPWD.UserInfo
        saveebastusers()
        Dim i As Integer
        i = objBO.uspEBASTUsersI(objET)
        If i = 0 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
        ElseIf i = -2 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Login Name already exists');", True)
        Else
            If Not String.IsNullOrEmpty(TxtTitle.Text) Then
                objutil.ExeQuery("exec uspTitleUpdatedByUserId " & i & ", '" & TxtTitle.Text & "'")
            End If
            If rblsign.SelectedItem.Value = 0 Then
                'insert to Digital sign database..
                objdg.LoginName = txtLodinId.Value.Replace("'", "''")
                objdg.Password = "ebast123"
                objdg.Email = txtEmail.Value.Replace("'", "''")
                objdg.CommonName = txtName.Value.Replace("'", "''")
                'DGSignPWD.CreateUser("cosignadmin", "12345678", "117.102.80.44", objdg)
            End If
            If ddlLevel.SelectedValue = "N" Or ddlRole1.SelectedValue = Constants.NSN_SS_RoleID Or ddlRole1.SelectedValue = Constants.Subcon_SS_RoleID Or ddlRole1.SelectedValue = Constants.Customer_SS_RoleID Then
                Response.Redirect("frmUserList.aspx?Type=U1")
            Else
                hdnUID.Value = i
                'Modified by Fauzan, 14 Dec 2018. Block the column form from Script
                'tblSetup.Disabled = True
                isUserSetupAble.Value = "no"
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "blockColForm('tblSetup');", True)
                'END
                tblRole.Visible = True
                ddlLvl.SelectedValue = ddlLevel.SelectedValue
                ddlLvl.Enabled = False
                ddlRole.SelectedValue = ddlRole1.SelectedValue
                ddlRole.Enabled = False
            End If
        End If
        'ViewState("epmid") = txtEpmid.Value
        ViewState("name") = txtName.Value
        ViewState("login") = txtLodinId.Value
        ViewState("email") = txtEmail.Value
        ViewState("phone") = txtPhoneOff.Value
    End Sub
    Protected Sub ddlSelect_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSelect.SelectedIndexChanged
        If ddlUsertype.SelectedValue = 2 Then
            dt = objBO.uspSubConD(ddlSelect.SelectedValue)
            If dt.Rows.Count > 0 Then
                hdnID.Value = dt.Rows(0).Item("SCON_ID")
            End If
        ElseIf ddlUsertype.SelectedValue = 4 Then
            dt = objBO.uspCustomerD(ddlSelect.SelectedValue)
            If dt.Rows.Count > 0 Then
                hdnID.Value = dt.Rows(0).Item("CUS_ID")
            End If
        End If
    End Sub
    Protected Sub ddlJava_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlJava.SelectedIndexChanged
        ddlArea.Items.Clear()
        ddlRegion.Items.Clear()
        ddlZone.Items.Clear()
        ddlSite.Items.Clear()
        If ddlJava.SelectedIndex <> 0 Then
            objBOD.fillDDL(ddlArea, "CODArea", ddlJava.SelectedValue, True, Constants._DDL_Default_Select)
        End If
        viewdata()
    End Sub
    Protected Sub ddlArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlArea.SelectedIndexChanged
        ddlRegion.Items.Clear()
        ddlZone.Items.Clear()
        ddlSite.Items.Clear()
        If ddlArea.SelectedIndex <> 0 Then
            objBOD.fillDDL(ddlRegion, "CODRegion", ddlArea.SelectedValue, True, Constants._DDL_Default_Select)
        End If
        viewdata()
    End Sub
    Protected Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged
        ddlZone.Items.Clear()
        ddlSite.Items.Clear()
        If ddlRegion.SelectedIndex <> 0 Then
            objBOD.fillDDL(ddlZone, "CODZone", ddlRegion.SelectedValue, True, Constants._DDL_Default_Select)
        End If
        viewdata()
    End Sub
    Protected Sub ddlZone_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlZone.SelectedIndexChanged
        ddlSite.Items.Clear()
        If ddlZone.SelectedIndex <> 0 Then
            objBOD.fillDDL(ddlSite, "CODSite", ddlZone.SelectedValue, True, Constants._DDL_Default_Select)
        End If
        viewdata()
    End Sub
    Protected Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Response.Redirect("frmUserList.aspx?Type=U1")
    End Sub
    Protected Sub btnCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheck.Click
        Dim i As Integer
        i = objBO.uspEBASTUsersC(txtLodinId.Value)
        If i = 0 Then
            lblStatus.InnerText = "available"
        Else
            lblStatus.InnerText = "not available"
        End If
        lblStatusForm.Visible = True
    End Sub
    Sub cleardata()
        txtName.Value = ""
        txtLodinId.Value = ""
        ddlArea.Items.Clear()
        ddlRegion.Items.Clear()
        ddlZone.Items.Clear()
        ddlSite.Items.Clear()
        txtEmail.Value = ""
        txtPhoneOff.Value = ""
    End Sub
    Protected Sub ddlUsertype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUsertype.SelectedIndexChanged
        Dim i As Integer
        Dim a As String
        ddlLevel.SelectedValue = "N"
        If ddlUsertype.SelectedValue = 1 Or ddlUsertype.SelectedValue = 11 Then
            BtnSave.Text = "Proceed"
            cleardata()
            ddlRole1.Enabled = True
            name.Visible = False
            trLevel.Visible = True
            ddlLevel.SelectedValue = "N"
            i = ddlUsertype.SelectedValue
            a = ddlLevel.SelectedValue
            objBODP.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBODP.fillDDL(ddlRole, "TRole2", i, a, True, Constants._DDL_Default_Select)
            Dim li As New ListItem
            li = ddlRole1.Items.FindByValue(Constants.Sysadmin_RoleID)
            Dim j As Integer
            j = ddlRole1.Items.IndexOf(li)
            'ddlRole1.Items.RemoveAt(j)
        ElseIf ddlUsertype.SelectedValue = 2 Then
            cleardata()
            name.Visible = True
            lblName.InnerText = "SubCon Name"
            ddlSelect.Enabled = True
            objBOD.fillDDL(ddlSelect, "SubCon", True, Constants._DDL_Default_Select)
            i = ddlUsertype.SelectedValue
            a = ddlLevel.SelectedValue
            objBODP.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
        ElseIf ddlUsertype.SelectedValue = 4 Then
            cleardata()
            name.Visible = True
            lblName.InnerText = "Customer Name"
            objBOD.fillDDL(ddlSelect, "Customer", False, "")
            ddlSelect.Enabled = False
            i = ddlUsertype.SelectedValue
            a = ddlLevel.SelectedValue
            objBODP.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            dt = objBO.uspCustomerD(ddlSelect.SelectedValue)
            If dt.Rows.Count > 0 Then
                hdnID.Value = dt.Rows(0).Item("CUS_ID")
            End If
        End If
    End Sub
    Protected Sub ddlLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLevel.SelectedIndexChanged
        Dim i As Integer = ddlUsertype.SelectedValue
        Dim a As String = ddlLevel.SelectedValue
        Dim li As New ListItem
        Dim j As Integer
        If ddlLevel.SelectedValue = "N" Then
            objBODP.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBODP.fillDDL(ddlRole, "TRole2", i, a, True, Constants._DDL_Default_Select)
            If Session("Role_Id") = Constants.Subcon_SubAdmin_RoleID Then
                li = ddlRole1.Items.FindByValue(Constants.Subcon_SubAdmin_RoleID)
                j = ddlRole1.Items.IndexOf(li)
                ddlRole1.Items.RemoveAt(j)
            ElseIf Session("Role_Id") = Constants.Customer_SubAdmin_RoleID Then
                li = ddlRole1.Items.FindByValue(Constants.Customer_SubAdmin_RoleID)
                j = ddlRole1.Items.IndexOf(li)
                ddlRole1.Items.RemoveAt(j)
            ElseIf Session("Role_Id") = Constants.Sysadmin_RoleID Then
                If ddlUsertype.SelectedValue = 1 Then
                    li = ddlRole1.Items.FindByValue(Constants.Sysadmin_RoleID)
                    j = ddlRole1.Items.IndexOf(li)
                    ddlRole1.Items.RemoveAt(j)
                End If
            End If
        ElseIf ddlLevel.SelectedValue = "A" Then
            ddlRole1.Items.Clear()
            objBODP.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBODP.fillDDL(ddlRole, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBOD.fillDDL(ddlJava, "CODJAVA", True, Constants._DDL_Default_Select)
            trRegion.Visible = False
            trZone.Visible = False
            trSite.Visible = False
        ElseIf ddlLevel.SelectedValue = "R" Then
            ddlRole1.Items.Clear()
            objBODP.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBODP.fillDDL(ddlRole, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBOD.fillDDL(ddlJava, "CODJAVA", True, Constants._DDL_Default_Select)
            ddlArea.SelectedValue = 0
            trRegion.Visible = True
            ddlRegion.SelectedValue = 0
            trZone.Visible = False
            trSite.Visible = False
        ElseIf ddlLevel.SelectedValue = "Z" Then
            ddlRole1.Items.Clear()
            objBODP.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBODP.fillDDL(ddlRole, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBOD.fillDDL(ddlJava, "CODJAVA", True, Constants._DDL_Default_Select)
            ddlArea.SelectedValue = 0
            trRegion.Visible = True
            ddlRegion.SelectedValue = 0
            trZone.Visible = True
            ddlZone.SelectedValue = 0
            trSite.Visible = False
        ElseIf ddlLevel.SelectedValue = "S" Then
            ddlRole1.Items.Clear()
            objBODP.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBODP.fillDDL(ddlRole, "TRole2", i, a, True, Constants._DDL_Default_Select)
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
            objBODP.fillDDL(ddlRole1, "TRole2", i, a, True, Constants._DDL_Default_Select)
            objBODP.fillDDL(ddlRole, "TRole2", i, a, True, Constants._DDL_Default_Select)
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
    Sub saverole()
        objETR.USR_ID = hdnUID.Value
        If ddlUsertype.SelectedValue = 1 Then
            objETR.USRType = Constants.User_Type_NSN
        ElseIf ddlUsertype.SelectedValue = 2 Then
            objETR.USRType = Constants.User_Type_SubCon
        ElseIf ddlUsertype.SelectedValue = 4 Then
            objETR.USRType = Constants.User_Type_Customer
        End If
        objETR.LVLCode = ddlLvl.SelectedValue
        objETR.RoleType = ddlRole.SelectedValue
        objETR.JV_ID = IIf(ddlJava.SelectedValue <> "", ddlJava.SelectedValue, 0)
        objETR.ARA_ID = IIf(ddlArea.SelectedValue <> "", ddlArea.SelectedValue, 0)
        objETR.RGN_ID = IIf(ddlRegion.SelectedValue <> "", ddlRegion.SelectedValue, 0)
        objETR.ZN_ID = IIf(ddlZone.SelectedValue <> "", ddlZone.SelectedValue, 0)
        objETR.Site_ID = IIf(ddlSite.SelectedValue <> "", ddlSite.SelectedValue, 0)
        objETR.AT.RStatus = Constants.STATUS_ACTIVE
        objETR.AT.LMBY = Session("User_Name")
    End Sub
    Sub viewdata()
        'txtEpmid.Value = ViewState("epmid")
        txtName.Value = ViewState("name")
        txtLodinId.Value = ViewState("login")
        txtEmail.Value = ViewState("email")
        txtPhoneOff.Value = ViewState("phone")
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        saverole()
        Dim i As Integer = objBO.uspEBASTUserRoleI(objETR, hdnSRCID.Value)
        If i = 0 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
        ElseIf i = -2 Then
            'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Supervisor already exists');", True)
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('" & ddlRole1.SelectedItem.Text & " already exists');", True)
        ElseIf i = -3 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('User already exists in this location');", True)
        End If
        viewdata()
        getlist()
        ddlJava.SelectedIndex = 0
        ddlArea.Items.Clear()
        ddlRegion.Items.Clear()
        ddlZone.Items.Clear()
        ddlSite.Items.Clear()
    End Sub
    Sub getlist()
        dt = objBO.uspEBASTUserRoleL(hdnUID.Value)
        'grdRoleList.PageSize = Session("Page_size")
        grdRoleList.DataSource = dt
        grdRoleList.DataBind()
        If trZone.Visible = True And trSite.Visible = False Then
            grdRoleList.Columns(4).Visible = False
        ElseIf trRegion.Visible = True And trZone.Visible = False Then
            grdRoleList.Columns(4).Visible = False
            grdRoleList.Columns(5).Visible = False
        ElseIf trArea.Visible = True And trRegion.Visible = False Then
            grdRoleList.Columns(3).Visible = False
            grdRoleList.Columns(4).Visible = False
            grdRoleList.Columns(5).Visible = False
        ElseIf trJava.Visible = True And trArea.Visible = False Then
            grdRoleList.Columns(2).Visible = False
            grdRoleList.Columns(3).Visible = False
            grdRoleList.Columns(4).Visible = False
            grdRoleList.Columns(5).Visible = False
        End If
    End Sub
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("frmUserList.aspx?Type=U1")
    End Sub
    Protected Sub grdRoleList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdRoleList.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = grdRoleList.PageIndex * grdRoleList.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
    Protected Sub ddlRole1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRole1.SelectedIndexChanged
        If ddlRole1.SelectedValue = Constants.Customer_SubAdmin_RoleID Or ddlRole1.SelectedValue = Constants.Subcon_SubAdmin_RoleID Then
            BtnSave.Text = "Save"
        Else
            BtnSave.Text = "Proceed"
        End If
    End Sub

    Private Sub grdRoleList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdRoleList.PageIndexChanging
        grdRoleList.PageIndex = e.NewPageIndex
        getlist()
    End Sub
End Class
