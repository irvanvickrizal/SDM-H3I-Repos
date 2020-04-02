Imports System.Data
Imports BusinessLogic
Imports common
Imports Entities
Imports System.Web.Security

Partial Class _Default
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objbo As New BOUserSetup
    Dim objET As New ETMessageBoard
    Dim objBOM As New BOMessageBoard
    Dim objUtil As New DBUtil
    Dim hcptcontrol As New HCPTController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'enable this during maintenance and pass &bypass=1 on the query
        'If Not Request.QueryString("bypass") Is Nothing Then
        '    Response.Cookies("bypass").Value = Request.QueryString("bypass")
        'End If
        'If Not Request.Cookies("bypass") Is Nothing Then
        '    If Server.HtmlEncode(Request.Cookies("bypass").Value) <> "1" Then
        '        Response.Redirect("frmUnderMaintenance.aspx")
        '    End If
        'Else
        '    Response.Redirect("frmUnderMaintenance.aspx")
        'End If
        '--
        If Request.QueryString("pchecked") = "yes" Then
            btnLogin_Click(Nothing, Nothing)
        End If
        btnLogin.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        If Page.IsPostBack = False Then
            If Not Page.IsPostBack = True Then
                Dim password As String = ""
                If Not Request.QueryString("S") Is Nothing Then
                    Dim strpassword As String = Request.QueryString("P").ToString.Replace("!", " ")
                    Dim strUser As String = Request.QueryString("S").ToString.Replace("!", " ")
                    If Not String.IsNullOrEmpty(Request.QueryString("tskid")) Then
                        Session("tskid") = Request.QueryString("tskid")
                    Else
                        Session("tskid") = "0"
                    End If
                    Session("Res") = Request.QueryString("R")
                    Session("User_Login") = strUser
                    Session("User_Pwd") = strpassword
                    password = Session("User_Pwd")
                    dt = objbo.uspValidateLogin(strUser, password)
                    If dt.Rows.Count() > 0 Then
                        If Trim(strUser) <> "" And Trim(strpassword) <> "" Then
                            If dt.Rows(0).Item(0).ToString = "valid" Then '@@@@@@123
                                If dt.Rows.Count > 0 Then
                                    Dim dr As DataRow = dt.Rows(0)
                                    Session("User_Name") = dr.Item("Name").ToString
                                    Session("User_Type") = dr.Item("USRType").ToString
                                    Session("SRCId") = dr.Item("SRCID").ToString
                                    Session("User_Id") = dr.Item("USR_Id").ToString
                                    Session("Role_Id") = dr.Item("UsrRole").ToString
                                    Session("Area_Id") = dr.Item("ARA_Id").ToString
                                    Session("Region_Id") = dr.Item("RGN_Id").ToString
                                    Session("Zone_Id") = dr.Item("ZN_Id").ToString
                                    Session("Site_Id") = dr.Item("Site_Id").ToString
                                    Session("FLogin") = dr.Item("FirstTime_Login").ToString
                                    Session("lvlcode") = dr.Item("LVLCode").ToString
                                    Session("phone") = dr.Item("phoneno").ToString
                                    If UCase(strUser) = "SYSADMIN" Then
                                        Session("Page_size") = 30
                                        Session("User_Type") = Constants.User_Type_NSN
                                        Session("Role_Id") = Constants.Sysadmin_RoleID
                                    Else
                                        Session("Page_size") = 15
                                    End If
                                    Response.Redirect("Main.aspx")
                                Else
                                    Response.Redirect(ConfigurationManager.AppSettings("TIURL"))
                                End If
                            End If
                        Else
                            If (dt.Rows(0).Item("acc_status").ToString = "I") Then
                                Response.Write("<script language='javascript'>alert('Sorry your account has been inactive');</script>")
                            Else
                                Response.Write("<script language='javascript'>alert('Invalid userid');</script>")
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        If Request.Browser.Browser = "IE" Or Request.Browser.Browser = "AppleMAC-Safari" Or Request.Browser.Browser = "Opera" Or _
            Request.Browser.Browser = "Firefox" Or Request.Browser.Browser = "Chrome" Then
            'passwordexpirydate cheking added here , can add in validate login also but, since it is already using in all scopes..we are adding here)
            Dim pstatus As Integer
            Dim p As String = Trim(FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Value.Replace("'", "''") & "TAKE", "MD5"))
            If Request.QueryString("pchecked") <> "yes" Then
                pstatus = objUtil.ExeQueryScalar("exec uspValidatePasswordDate '" & txtUserName.Value & "','" & p & "'")
                Select Case pstatus
                    Case 7
                        Session("u") = txtUserName.Value
                        Session("p") = txtPassword.Value
                        Session("t") = "2G"
                        Session("d") = "TI"
                        Dim usrid As Integer = GetUserIdByUserLogin(txtUserName.Value)
                        If usrid > 0 Then
                            ActivityLog_I(usrid, "Login succeed")
                        End If
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "MyScript", "showmsg();", True)
                        Exit Sub
                    Case 0
                        Response.Write("<script language='javascript'>alert('Your password is expired,please contact Administrator')</script>")
                        Exit Sub
                End Select
            Else
                txtUserName.Value = Session("u")
                txtPassword.Value = Session("p")
                'rblType.SelectedValue = Session("t")
                'ddlType.Value = Session("d")
            End If
            'to save button clicks in view state
            Session("Res") = hdnRes.Value
            If Trim(txtUserName.Value) <> "" And Trim(txtPassword.Value) <> "" Then
                Dim strUser As String = ""
                Dim strPassword As String = ""
                Session("User_Login") = txtUserName.Value
                'Session("PType") = rblType.SelectedValue.ToString
                Dim password As String
                password = Trim(FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Value.Replace("'", "''") & "TAKE", "MD5"))
                Session("User_Pwd") = password
                dt = objbo.uspValidateLogin(txtUserName.Value, password)
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item(0).ToString = "valid" Then '@@@@@@123
                        Dim dr As DataRow = dt.Rows(0)
                        'if password is being reset to abcd1234
                        If dr.Item("usrpassword").ToString = "D75281413C623EA60E60A8C43DB6A5BF" And dr.Item("usrlogin").ToString <> "sysadmin" Then
                            Response.Write("<script language='javascript'>alert('Sorry.. Your account has been blocked!!!');</script>")
                        Else
                            If txtUserName.Value = dr.Item("usrlogin").ToString Then
                                Session("User_Name") = dr.Item("Name").ToString
                                Session("User_Type") = dr.Item("USRType").ToString
                                Session("SRCId") = dr.Item("SRCID").ToString
                                Session("User_Id") = dr.Item("USR_Id").ToString
                                Session("Role_Id") = dr.Item("UsrRole").ToString
                                Session("Area_Id") = dr.Item("ARA_Id").ToString
                                Session("Region_Id") = dr.Item("RGN_Id").ToString
                                Session("Zone_Id") = dr.Item("ZN_Id").ToString
                                Session("Site_Id") = dr.Item("Site_Id").ToString
                                Session("FLogin") = dr.Item("FirstTime_Login").ToString
                                Session("lvlcode") = dr.Item("LVLCode").ToString
                                Session("PType") = "2G"
                                If UCase(txtUserName.Value) = "SYSADMIN" Then
                                    Session("Page_size") = 30
                                    Session("User_Type") = Constants.User_Type_NSN
                                    Session("Role_Id") = Constants.Sysadmin_RoleID
                                Else
                                    Session("Page_size") = 15
                                End If
								ActivityLog_I(CommonSite.UserId, "Login Succeed")
                                If Session("FLogin") = False Then
                                    'bugfix100924 to switch between localhost and live
                                    If Request.Url.ToString.IndexOf("localhost") <> -1 Then
                                        Response.Redirect("USR/frmChangePwd.aspx")
                                    Else
                                        Response.Redirect(ConfigurationManager.AppSettings("TIWWW") & "/USR/frmChangePwd.aspx")
                                    End If
                                Else
                                    If Request.Url.ToString.IndexOf("localhost") <> -1 Then
                                        Response.Redirect("~/main.aspx")
                                    Else
                                        Response.Redirect(ConfigurationManager.AppSettings("TIWWW") & "/main.aspx")
                                    End If
                                End If
                            Else
                                Response.Write("<script language='javascript'>alert('Invalid User/Password');</script>")
                            End If
                        End If
                    Else '@123 means dt>0 and validuser password wrong
                        If (dt.Rows(0).Item("acc_status").ToString = "I") Then
                            Response.Write("<script language='javascript'>alert('Sorry your account has been inactive');</script>")
                        Else
                            If ViewState("count") = Nothing Then
                                ViewState("count") = 1
                                Dim usrid As Integer = GetUserIdByUserLogin(txtUserName.Value)
                                If usrid > 0 Then
                                    ActivityLog_I(usrid, BaseConfiguration.Activity_Login_Failed)
                                End If
                                Response.Write("<script language='javascript'>alert('Invalid Password Entered  " + ViewState("count").ToString + "');</script>")
                            ElseIf ViewState("count") < 3 Then
                                ViewState("count") = ViewState("count") + 1
                                If ViewState("count") = 3 Then
                                    Dim usrid As Integer = GetUserIdByUserLogin(txtUserName.Value)
                                    If usrid > 0 Then
                                        ActivityLog_I(usrid, BaseConfiguration.Activity_Login_Blocked)
                                    End If
                                    objUtil.ExeNonQuery("UPDATE EBASTUSERS_1 SET usrpassword='D75281413C623EA60E60A8C43DB6A5BF' where usr_id= " & dt.Rows(0).Item(1).ToString & " ")
                                    Response.Write("<script language='javascript'>alert('Your ID became inactive, since you entered wrong password 3 times');</script>")
                                Else
                                    Response.Write("<script language='javascript'>alert('Invalid Password Entered  " + ViewState("count").ToString + "');</script>")
                                End If
                            End If
                        End If
                    End If
                Else
                    Response.Write("<script language='javascript'>alert('Invalid userid');</script>")
                End If
               
            End If
        Else
            Response.Write("<script language='javascript'>alert('Sorry.. eBAST Only Support IE');</script>")
        End If
    End Sub

    'Protected Sub LbtResetPasswordClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtResetPassword.Click
        'Response.Redirect("frmResetPassword.aspx")
    'End Sub

#Region "Activity Log"
    Private Sub ActivityLog_I(ByVal userid As Integer, ByVal activitydesc As String)
        'Dim ipaddress As String = HttpContext.Current.Request.UserHostAddress
        Dim ipaddress As String = Me.Page.Request.ServerVariables("REMOTE_ADDR")
        Dim info As New UserActivityLogInfo
        info.UserId = userid
        info.IPAddress = ipaddress
        info.Description = activitydesc

        hcptcontrol.UserLogActivity_I(info)
    End Sub

    Private Function GetUserIdByUserLogin(ByVal usrLogin As String) As Integer
        Return hcptcontrol.GetUserIDBaseUserLogin(usrLogin)
    End Function
#End Region
End Class