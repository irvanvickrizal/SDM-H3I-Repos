Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Imports NSNCustomizeConfiguration

Partial Class DashBoard_frmSiteAllTaskPending_Delegation
    Inherits System.Web.UI.Page
    Dim controller As New DelegationController
    Dim dtTaskPendings As New DataTable
    Dim dtTaskPendingsATP As New DataTable
    Dim dtTaskPendingsQC As New DataTable
    Dim dtTaskPendingsCR As New DataTable
    Dim objBo As New BODashBoard
    Dim objdb As New DBUtil
    Dim mailbase As New TakeMail
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            BtnReActived.Visible = False
            BindDelegatedUsers(CommonSite.UserId)
        End If
    End Sub

    Protected Sub BtnViewTaskPending_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnViewTaskPending.Click
        If DdlDelegatedUser.SelectedIndex > 0 Then
            MvCorePanel.SetActiveView(VwTaskPending)
            BindTaskPending(Integer.Parse(DdlDelegatedUser.SelectedValue))
            HdnTaskPendingUserId.Value = DdlDelegatedUser.SelectedValue
        End If

    End Sub

    Protected Sub BtnReActived_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReActived.Click
        Dim info As New DelegationInfo
        info.UserId = Integer.Parse(DdlDelegatedUser.SelectedValue)
        info.UserDelegationId = CommonSite.UserId
        info.Status = "in-active"
        info.LMBY = CommonSite.UserId
        controller.Delegation_IU(info)
        BtnReActived.Visible = False
        BindDelegatedUsers(CommonSite.UserId)
        SendMailReactivation(CommonSite.UserId, info.UserId)
        MvCorePanel.SetActiveView(vwNotTaskPending)
    End Sub

    Protected Sub DdlDelegatedUser_IndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlDelegatedUser.SelectedIndexChanged
        If (DdlDelegatedUser.SelectedIndex > 0) Then
            BtnReActived.Visible = True
        Else
            BtnReActived.Visible = False
        End If
    End Sub

    Private Sub LbtTI2GClick(ByVal sender As Object, ByVal e As EventArgs) Handles LbtTI2GLink.Click
        Response.Redirect("frmSiteDocCount_Delegation.aspx?uid=" & HdnTaskPendingUserId.Value)
    End Sub

    Private Sub LbtTI2GATPClick(ByVal sender As Object, ByVal e As EventArgs) Handles LbtTI2GATPLink.Click
        Response.Redirect("frmSiteDocCount_ATP_Delegation.aspx?uid=" & HdnTaskPendingUserId.Value)
    End Sub

    Protected Sub LbtTI2GQC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtTI2GQCLink.Click
        Response.Redirect("frmSiteDocCount_QC_Delegation.aspx?uid=" & HdnTaskPendingUserId.Value)
    End Sub

    Protected Sub LbtTI2GCR_Link(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtTI2GCRLink.Click
        Response.Redirect("frmSiteDocCount_CR_Delegation.aspx?uid=" & HdnTaskPendingUserId.Value)
    End Sub

#Region "Custom Methods"
    Private Sub BindTaskPending(ByVal userid As Integer)
        LinkDisabled()
        HyperLinkDisabled()
        BindGeneralDocument(userid)
        BindDataATPTaskPending(userid)
        BindDataCRCOTaskPending(userid)
    End Sub

    Private Sub BindGeneralDocument(ByVal userid As Integer)
        dtTaskPendings = objdb.ExeQueryDT("exec uspGetAllTaskPendings " & userid & ", " & ConfigurationManager.AppSettings("QCID") & ", " & ConfigurationManager.AppSettings("QCIDTI3G"), "taskPendings")
        If dtTaskPendings.Rows.Count > 0 Then
            For intCount As Integer = 0 To dtTaskPendings.Rows.Count - 1
                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "TI2G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblTI2GLinkDisabled.Visible = True
                    Else
                        LbtTI2GLink.Visible = True
                        LbtTI2GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "TI3G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblTI3GLinkDisabled.Visible = True
                    Else
                        HpTI3GLink.Visible = True
                        HpTI3GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpTI3GLink.Target = "_top"
                        HpTI3GLink.NavigateUrl = ConfigurationManager.AppSettings("3GTIURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "SITAC2G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblSITAC2GLinkDisabled.Visible = True
                    Else
                        HpSITAC2GLink.Visible = True
                        HpSITAC2GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpSITAC2GLink.Target = "_top"
                        HpSITAC2GLink.NavigateUrl = ConfigurationManager.AppSettings("SITacURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=2G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "SITAC3G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblSITAC3GLinkDisabled.Visible = True
                    Else
                        HpSITAC3GLink.Visible = True
                        HpSITAC3GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpSITAC3GLink.Target = "_top"
                        HpSITAC3GLink.NavigateUrl = ConfigurationManager.AppSettings("3GSITacURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "CME2G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblCME2GLinkDisabled.Visible = True
                    Else
                        HpCME2GLink.Visible = True
                        HpCME2GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpCME2GLink.Target = "_top"
                        HpCME2GLink.NavigateUrl = ConfigurationManager.AppSettings("CMEURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=2G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "CME3G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblCME3GLinkDisabled.Visible = True
                    Else
                        HpCME3GLink.Visible = True
                        HpCME3GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpCME3GLink.Target = "_top"
                        HpCME3GLink.NavigateUrl = ConfigurationManager.AppSettings("3GCMEURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "SIS2G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblSIS2GLinkDisabled.Visible = True
                    Else
                        HpSIS2GLink.Visible = True
                        HpSIS2GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpSIS2GLink.Target = "_top"
                        HpSIS2GLink.NavigateUrl = ConfigurationManager.AppSettings("SISURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=2G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "SIS3G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblSIS3GLinkDisabled.Visible = True
                    Else
                        HpSIS3GLink.Visible = True
                        HpSIS3GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpSIS3GLink.Target = "_top"
                        HpSIS3GLink.NavigateUrl = ConfigurationManager.AppSettings("3GSISURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=1"
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub BindDataATPTaskPending(ByVal userid As Integer)
        dtTaskPendingsATP = objdb.ExeQueryDT("exec uspGetAllTaskPendings_ATP " & userid & ", " & ConfigurationManager.AppSettings("ATP"), "taskPendings")
        If dtTaskPendingsATP.Rows.Count > 0 Then
            For intCount As Integer = 0 To dtTaskPendingsATP.Rows.Count - 1
                If (dtTaskPendingsATP.Rows(intCount)("scope").ToString().ToUpper() = "TI2G") Then
                    If dtTaskPendingsATP.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblTI2GATPLink.Visible = True
                    Else
                        LbtTI2GATPLink.Visible = True
                        LbtTI2GATPLink.Text = Convert.ToString(dtTaskPendingsATP.Rows(intCount)("TotalTaskPending"))
                    End If
                End If

                If (dtTaskPendingsATP.Rows(intCount)("scope").ToString().ToUpper() = "TI3G") Then
                    If dtTaskPendingsATP.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblTI3GATPLink.Visible = True
                    Else
                        HpTI3GATPLink.Visible = True
                        HpTI3GATPLink.Text = Convert.ToString(dtTaskPendingsATP.Rows(intCount)("TotalTaskPending"))
                        HpTI3GATPLink.Target = "_top"
                        HpTI3GATPLink.NavigateUrl = ConfigurationManager.AppSettings("3GTIURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=3"
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub BindDataCRCOTaskPending(ByVal userid As Integer)
        dtTaskPendingsCR = objdb.ExeQueryDT("exec uspGetAllTaskPendings_CRCO " & userid, "taskPendings")
        If dtTaskPendingsCR.Rows.Count > 0 Then
            For intCount As Integer = 0 To dtTaskPendingsCR.Rows.Count - 1
                If (dtTaskPendingsCR.Rows(intCount)("scope").ToString().ToUpper() = "TI2G") Then
                    If dtTaskPendingsCR.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblTI2GCRLink.Visible = True
                    Else
                        LbtTI2GCRLink.Visible = True
                        LbtTI2GCRLink.Text = Convert.ToString(dtTaskPendingsCR.Rows(intCount)("TotalTaskPending"))
                    End If
                End If

                If (dtTaskPendingsCR.Rows(intCount)("scope").ToString().ToUpper() = "TI3G") Then
                    If dtTaskPendingsCR.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblTI3GCRLink.Visible = True
                    Else
                        HpTI3GCRLink.Visible = True
                        HpTI3GCRLink.Text = Convert.ToString(dtTaskPendingsCR.Rows(intCount)("TotalTaskPending"))
                        HpTI3GCRLink.Target = "_top"
                        HpTI3GCRLink.NavigateUrl = ConfigurationManager.AppSettings("3GTIURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=2"
                    End If
                End If

                If (dtTaskPendingsCR.Rows(intCount)("scope").ToString().ToUpper() = "CME2G") Then
                    If dtTaskPendingsCR.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblCME2GCRLink.Visible = True
                    Else
                        HpCME2GCRLink.Visible = True
                        HpCME2GCRLink.Text = Convert.ToString(dtTaskPendingsCR.Rows(intCount)("TotalTaskPending"))
                        HpCME2GCRLink.Target = "_top"
                        HpCME2GCRLink.NavigateUrl = ConfigurationManager.AppSettings("CMEURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=2"
                    End If
                End If

                If (dtTaskPendingsCR.Rows(intCount)("scope").ToString().ToUpper() = "CME3G") Then
                    If dtTaskPendingsCR.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblCME3GCRLink.Visible = True
                    Else
                        HpCME3GCRLink.Visible = True
                        HpCME3GCRLink.Text = Convert.ToString(dtTaskPendingsCR.Rows(intCount)("TotalTaskPending"))
                        HpCME3GCRLink.Target = "_top"
                        HpCME3GCRLink.NavigateUrl = ConfigurationManager.AppSettings("3GCMEURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=2"
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub BindDelegatedUsers(ByVal userid As Integer)
        DdlDelegatedUser.DataSource = controller.GetDelegationFrom(userid, "active")
        DdlDelegatedUser.DataTextField = "UserDelegationName"
        DdlDelegatedUser.DataValueField = "userid"
        DdlDelegatedUser.DataBind()
        DdlDelegatedUser.Items.Insert(0, "-- select user --")
    End Sub

    Private Sub LinkDisabled()
        LblTI2GLinkDisabled.Visible = False
        LblSIS2GLinkDisabled.Visible = False
        LblSITAC2GLinkDisabled.Visible = False
        LblCME2GLinkDisabled.Visible = False
        LblTI3GLinkDisabled.Visible = False
        LblSIS3GLinkDisabled.Visible = False
        LblSITAC3GLinkDisabled.Visible = False
        LblCME3GLinkDisabled.Visible = False

        LblTI2GATPLink.Visible = False
        LblTI3GATPLink.Visible = False

        lblTI2GQCLink.Visible = False
        lblTI2GQCLink.Visible = False

        LblSIS2GCRLink.Visible = False
        LblSITAC2GCRLink.Visible = False
        LblCME2GCRLink.Visible = False
        LblTI2GCRLink.Visible = False

        LblSIS3GCRLink.Visible = False
        LblSITAC3GCRLink.Visible = False
        LblCME3GCRLink.Visible = False
        LblTI3GCRLink.Visible = False

        'LblSIS2GATPLink.Visible = False
        'LblSITAC2GATPLink.Visible = False
        'LblCME2GATPLink.Visible = False
        'LblSIS3GATPLink.Visible = False
        'LblSITAC3GATPLink.Visible = False
        'LblCME3GATPLink.Visible = False
    End Sub

    Private Sub HyperLinkDisabled()
        LbtTI2GLink.Visible = False
        HpTI3GLink.Visible = False
        HpSIS2GLink.Visible = False
        HpSIS3GLink.Visible = False
        HpSITAC2GLink.Visible = False
        HpSITAC3GLink.Visible = False
        HpCME2GLink.Visible = False
        HpCME3GLink.Visible = False

        LbtTI2GATPLink.Visible = False
        HpSIS2GATPLink.Visible = False
        HpSITAC2GATPLink.Visible = False
        HpCME2GATPLink.Visible = False

        HpTI3GATPLink.Visible = False
        HpSIS3GATPLink.Visible = False
        HpSITAC3GATPLink.Visible = False
        HpCME3GATPLink.Visible = False

        LbtTI2GQCLink.Visible = False
        HpSIS2GQCLink.Visible = False
        HpSITAC2GQCLink.Visible = False
        HpCME2GQCLink.Visible = False
        HpTI3GQCLink.Visible = False
        HpSIS3GQCLink.Visible = False
        HpSITAC3GQCLink.Visible = False
        HpCME3GQCLink.Visible = False

        LbtTI2GCRLink.Visible = False
        HpCME2GCRLink.Visible = False
        HpSITAC2GCRLink.Visible = False
        HpSIS2GCRLink.Visible = False
        HpTI3GCRLink.Visible = False
        HpCME3GCRLink.Visible = False
        HpSITAC3GCRLink.Visible = False
        HpSIS3GCRLink.Visible = False


    End Sub

    Private Sub SendMailReactivation(ByVal delegateuserid As Integer, ByVal actualuserid As Integer)
        Dim actualuser As UserProfile = controller.GetUserDelegation(actualuserid)
        Dim delegateuser As UserProfile = controller.GetUserDelegation(delegateuserid)
        Dim sbBody As New StringBuilder
        sbBody.Append("Dear " & actualuser.Fullname & ",<br/> Your Task Pending re-actived due to delegation is finished by " & delegateuser.Fullname & "<br/><br/>thanks<br/>Powered By EBAST SYSTEM")
        mailbase.SendMailUser(actualuser.Email, sbBody.ToString(), "EBAST Delegation Finish " & delegateuser.Fullname)
    End Sub
#End Region

End Class
