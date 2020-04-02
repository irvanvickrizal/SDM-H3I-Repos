Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Imports System.Web.Security

Partial Class USR_frmUserDetails
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim objBODP As New BODDLs
    Dim objET As New ETEBASTUsers
    Dim objBO As New BOUserLD
    Dim dt As New DataTable
    Dim objBOS As New BOUserSetup
    Dim objdb As New DBUtil
    Dim objmail As New TakeMail

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        If Not IsPostBack Then
            'objBODP.fillDDL(ddlUsertype, "TUserType1", False, "")      'Commented by Fauzan, 13 Dec 2018.
            'objBODP.fillDDL(ddlRole, "TRole1", ddlUsertype.SelectedValue, False, "")
            hdnUsrId.Value = Request.QueryString("ID")
            binddata()
            getlist()
            'If ddlRole.SelectedValue = Constants.Subcon_SubAdmin_RoleID Or ddlRole.SelectedValue = Constants.Customer_SubAdmin_RoleID Or ddlRole.SelectedValue = Constants.Customer_IJ Or ddlRole.SelectedValue = Constants.Customer_OJ Then
            '    btnRole.Visible = False
            'End If
        End If
    End Sub
    Sub binddata()
        dt = New DataTable
        dt = objBO.uspEBASTUsersLD1(Request.QueryString("ID"), "", "", "", "", 0, )
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                'Modified by Fauzan, 13 Dec 2018
                'ddlUsertype.SelectedValue = .Item("USRTYPE").ToString
                'lblUser.Text = ddlUsertype.SelectedItem.Text
                lblUser.Text = .Item("USRTYPE").ToString
                'End

                'lblUser.InnerHtml = ddlUsertype.SelectedItem.Text & " <a id='A1' href='#' onclick='viewUser();' runat='server'>View Details</a>"
                'objBODP.fillDDL(ddlRole, "TRole1", ddlUsertype.SelectedValue, False, "")
                'ddlRole.SelectedValue = .Item("USRROLE")
                'lblRole.InnerText = ddlRole.SelectedItem.Text
                lblName.Text = .Item("NAME").ToString
                lblLogin.Text = .Item("USRLOGIN").ToString
                lblEmail.Text = .Item("EMAIL").ToString
                txtPhnumber.Value = .Item("PHONENO").ToString
                TxtTitle.Text = .Item("SignTitle").ToString
                If .Item("APPROVED") = True Then
                    rblistApproved.SelectedValue = 1
                Else
                    rblistApproved.SelectedValue = 0
                End If
                If Session("role") = "sys" Then
                    trstatus.Visible = True
                Else
                    trstatus.Visible = False
                End If
                rblistStatus.SelectedValue = .Item("ACC_STATUS")
            End With
        End If
    End Sub
    Sub savedata()
        objET.USR_ID = Request.QueryString("ID")
        objET.Approved = rblistApproved.SelectedValue
        objET.ACC_Status = rblistStatus.SelectedValue
    End Sub

    Protected Sub btnPwdreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPwdreset.Click
        savedata()
        'objET.USRPassword = FormsAuthentication.HashPasswordForStoringInConfigFile("ebast123TAKE", "MD5")
        'BOcommon.result(objBO.uspEBASTUsersStatusU(objET, "R"), True, "frmUserList.aspx?Type=U1", "", Constants._UPDATE)

        '---- New Random Password [Irvan Code]---'
        Dim randomkey As RandomKeyGenerator = New RandomKeyGenerator()
        Dim NumKeys As Integer = 20

        randomkey.KeyLetters = "abcdefghijklmnopqrstuvwxyz"
        randomkey.KeyNumbers = "0123456789"
        randomkey.KeyChars = 6
        Dim strRandomKey As String = randomkey.Generate()
        'HdfPassword.Value = randomkey.Generate()
        'objET.USR_ID = personal(2)
        'objET.Approved = Convert.ToInt16(Convert.ToBoolean(personal(3)))
        'objET.ACC_Status = personal(4)
        objET.USRPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(strRandomKey & "TAKE", "MD5")
        BOcommon.result(objBO.uspEBASTUsersStatusU(objET, "R"), True, "frmUserList.aspx?Type=U1", "", Constants._UPDATE)
        SendMailPassReset(strRandomKey)
    End Sub
	
	Protected Sub btnPwdResetEBOQ_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPwdResetEBOQ.Click
        savedata()
        'objET.USRPassword = FormsAuthentication.HashPasswordForStoringInConfigFile("ebast123TAKE", "MD5")
        'BOcommon.result(objBO.uspEBASTUsersStatusU(objET, "R"), True, "frmUserList.aspx?Type=U1", "", Constants._UPDATE)

        '---- New Random Password [Irvan Code]---'
        Dim randomkey As RandomKeyGenerator = New RandomKeyGenerator()
        Dim NumKeys As Integer = 20

        randomkey.KeyLetters = "abcdefghijklmnopqrstuvwxyz"
        randomkey.KeyNumbers = "0123456789"
        randomkey.KeyChars = 6
        Dim strRandomKey As String = randomkey.Generate()
        'HdfPassword.Value = randomkey.Generate()
        'objET.USR_ID = personal(2)
        'objET.Approved = Convert.ToInt16(Convert.ToBoolean(personal(3)))
        'objET.ACC_Status = personal(4)
        objET.USRPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(strRandomKey & "TAKE", "MD5")
        BOcommon.result(objBO.uspEBASTUsersStatusU(objET, "R"), True, "frmUserList.aspx?Type=U1", "", Constants._UPDATE)
        SendMailPassResetEBOQ(strRandomKey)
    End Sub

    Protected Sub btnPwdResetEMORE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPwdResetEMORE.Click
        savedata()
        'objET.USRPassword = FormsAuthentication.HashPasswordForStoringInConfigFile("ebast123TAKE", "MD5")
        'BOcommon.result(objBO.uspEBASTUsersStatusU(objET, "R"), True, "frmUserList.aspx?Type=U1", "", Constants._UPDATE)

        '---- New Random Password [Irvan Code]---'
        Dim randomkey As RandomKeyGenerator = New RandomKeyGenerator()
        Dim NumKeys As Integer = 20

        randomkey.KeyLetters = "abcdefghijklmnopqrstuvwxyz"
        randomkey.KeyNumbers = "0123456789"
        randomkey.KeyChars = 6
        Dim strRandomKey As String = randomkey.Generate()
        'HdfPassword.Value = randomkey.Generate()
        'objET.USR_ID = personal(2)
        'objET.Approved = Convert.ToInt16(Convert.ToBoolean(personal(3)))
        'objET.ACC_Status = personal(4)
        objET.USRPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(strRandomKey & "TAKE", "MD5")
        BOcommon.result(objBO.uspEBASTUsersStatusU(objET, "R"), True, "frmUserList.aspx?Type=U1", "", Constants._UPDATE)
        SendMailPassResetEMORE(strRandomKey)
    End Sub
	
    'UPDATE ACTIVE , INACTIVE
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        savedata()
        objET.USRPassword = FormsAuthentication.HashPasswordForStoringInConfigFile("TAKE", "MD5")
        BOcommon.result(objBO.uspEBASTUsersStatusU(objET, "AI"), True, "frmUserList.aspx?Type=U1", "", Constants._UPDATE)
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        savedata()
        BOcommon.result(objBO.uspEBASTUsersStatusU(objET, "D"), True, "frmUserList.aspx?Type=U1", "", Constants._UPDATE)
        Response.Redirect("frmUserList.aspx?Type=U1")
    End Sub

    Protected Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Response.Redirect("frmUserList.aspx?Type=U1")
    End Sub

    Sub getlist()
        dt = objBOS.uspEBASTUserRoleL(Request.QueryString("ID"))
        grdRoleList.PageSize = Session("Page_size")
        grdRoleList.DataSource = dt
        grdRoleList.DataBind()
    End Sub

    Protected Sub grdRoleList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdRoleList.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = grdRoleList.PageIndex * grdRoleList.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    'Protected Sub btnRole_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRole.Click
    '    Response.Redirect("frmChangeRole.aspx?ID=" & Request.QueryString("ID"))
    'End Sub
    'UPDATE ONLYS APPROVED STATUS
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        savedata()
        BOcommon.result(objBO.uspEBASTUsersStatusU(objET, "AR"), True, "frmUserList.aspx?Type=U1", "", Constants._UPDATE)
    End Sub
    'update phone no.
    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        objdb.ExeNonQuery("update ebastusers_1 set phoneno='" & txtPhnumber.Value & "' where USR_ID = " & Request.QueryString("ID") & " ")
        Response.Write("<script>alert('Phone number changed successfully')</script>")
    End Sub
    Protected Sub BtnUpdateTitle_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnUpdateTitle.Click
        objdb.ExeQuery("exec uspTitleUpdatedByUserId " & Request.QueryString("ID") & ", '" & TxtTitle.Text & "'")
        Response.Write("<script>alert('Sign Title changed successfully')</script>")
    End Sub

#Region "Custom Methods"
    Private Sub SendMailPassReset(ByVal strnewPass As String)
        Dim sbBody As New StringBuilder
        sbBody.Append("Dear " & lblName.Text & ", <br/><br/>")
        'sbBody.Append("Your E-SDM Password has been Reset, and new login detail as below: <br/> ")
        sbBody.Append("Your new E-SDM login details as below: <br/> ")
        sbBody.Append("username : " & lblLogin.Text & "<br/>")
        sbBody.Append("Password : " & strnewPass & "<br />")
        sbBody.Append("<a href='https://sdmthree.nsnebast.com'>Click here</a>" & " to go to E-SDM <br/>")
        sbBody.Append("<br/>")
        sbBody.Append("<img src='https://sdmthree.nsnebast.com/images/nokia.png' alt='companylogo' /><br/><br/>")
        sbBody.Append("If you have a problem with your login account, please contact E-SDM Technical Support <br/>")
        sbBody.Append("michael.pelupessy.ext@nokia.com <br/>")
        sbBody.Append("irvan.vickrizal@nokia.com <br/>")
        objmail.SendMailUser(lblEmail.Text, sbBody.ToString(), "Your new E-SDM Password")
    End Sub
	
	Private Sub SendMailPassResetEBOQ(ByVal strnewPass As String)
        Dim sbBody As New StringBuilder
        sbBody.Append("Dear " & lblName.Text & ", <br/><br/>")
        'sbBody.Append("Your Password in eBAST has been Reset. Your new login details are as follows: <br/> ")
        sbBody.Append("Your new BORN-3 (BOQ Online & Report for Nokia H3I) login details as follows: <br/> ")
        sbBody.Append("username : " & lblLogin.Text & "<br/>")
        sbBody.Append("Password : " & strnewPass & "<br />")
        sbBody.Append("<a href='https://eboqh3i.nsnebast.com'>Click here</a>" & " to go to BORN-3 <br/>")
        sbBody.Append("Powered By EBAST" & "<br/>")
        sbBody.Append("<img src='https://eboqh3i.telkomsel.nsnebast.com/images/logo.png' alt='companylogo' /><br/><br/>")
        sbBody.Append("Please contact Technical Support / EBOQ Administrator if you are facing technical Issue <br/>")
        objmail.SendMailBORN(lblEmail.Text, sbBody.ToString(), "Your new E-BOQ Nokia (BORN-3) Password")
    End Sub

    Private Sub SendMailPassResetEMORE(ByVal strnewPass As String)
        Dim sbBody As New StringBuilder
        sbBody.Append("Dear " & lblName.Text & ", <br/><br/>")
        'sbBody.Append("Your Password in eBAST has been Reset. Your new login details are as follows: <br/> ")
        sbBody.Append("Your new EMORE (EMORE for H3I Project) login details as follows: <br/> ")
        sbBody.Append("username : " & lblLogin.Text & "<br/>")
        sbBody.Append("Password : " & strnewPass & "<br />")
        sbBody.Append("<a href='https://emoreh3i.nsnebast.com'>Click here</a>" & " to go to EMORE-H3I <br/>")
        sbBody.Append("Powered By EBAST" & "<br/>")
        sbBody.Append("<img src='https://emoreh3i.nsnebast.com/images/omr-new-logo.png' alt='companylogo' /><br/><br/>")
        sbBody.Append("Please contact Technical Support / PDC Online Tool Administrator if you are facing technical Issue <br/>")
        objmail.SendMailCustom(lblEmail.Text, sbBody.ToString(), "Your new EMORE-H3I Nokia Password", "EMORE-H3I of NOKIA")
    End Sub
#End Region
End Class
