Imports Common
Partial Class DashBoard_MultipleCRCOApproval
    Inherits System.Web.UI.Page
    Dim objsms As New SMSNew
    Private controller As New CRCOMultipleController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If (Not Page.IsPostBack) Then
            HdnIsAuthorized.Value = 0
            txtUserName.Text = Session("User_Login")
            txtUserName.ReadOnly = True
            BindData(CommonSite.UserId, GetDocType(), Integer.Parse(ConfigurationManager.AppSettings("codocid")), "approver", Integer.Parse(DdlPricingGrouping.SelectedValue))
        End If
    End Sub

    Protected Sub LbtSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSearch.Click
        BindData(CommonSite.UserId, GetDocType(), Integer.Parse(ConfigurationManager.AppSettings("codocid")), "approver", Integer.Parse(DdlPricingGrouping.SelectedValue))
    End Sub

    Protected Sub lnkrequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkrequest.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        Dim npwd As String = ""
        Dim msgdata As New StringBuilder
        If txtUserName.Text <> "" Then
            objsms.requestSMS(Session("User_Name"), Session("User_Login"), Request.QueryString("siteno"), Request.QueryString("pono"), Request.QueryString("docname"))
            'loadingdiv.Style("display") = "none"
            Response.Write("<script>alert('Please check for your password in your phone');</script>")
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please keyin the user name and click request password');", True)
        End If
    End Sub

    Protected Sub btnProceed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Dim j As Integer
        Dim sno As String = ""
        Dim checkedSites As String = ""
        Dim objdb As New DBUtil
        j = objdb.ExeQueryScalar("select count(*) from dgpassword where username='" & txtUserName.Text & "' and password='" & txtPassword.Text & "'")
        If j = 0 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('invalid  userid or password');", True)
        Else
            MvDigitalSignatureLogin.SetActiveView(VwLogin)
            HdnIsAuthorized.Value = "1"
        End If
    End Sub

    Sub checkall(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim mainchk As New CheckBox
        mainchk = GvDocuments.HeaderRow.Cells(0).FindControl("chkall")
        Dim i As Integer
        For i = 0 To GvDocuments.Rows.Count - 1
            Dim chk As New HtmlInputCheckBox
            chk = GvDocuments.Rows(i).Cells(0).FindControl("ChkReview")
            If mainchk.Checked = True Then
                If chk.Disabled = False Then
                    chk.Checked = True
                Else
                    chk.Checked = False
                End If
            Else
                chk.Checked = False
            End If
        Next
    End Sub

    Protected Sub GvDocuments_PageIndexing(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvDocuments.PageIndexChanging
        GvDocuments.PageIndex = e.NewPageIndex
        BindData(CommonSite.UserId, GetDocType(), Integer.Parse(ConfigurationManager.AppSettings("codocid")), "approver", Integer.Parse(DdlPricingGrouping.SelectedValue))
    End Sub

    Protected Sub GvDocuments_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvDocuments.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim doctype As Label = CType(e.Row.FindControl("LblDocType"), Label)
            If doctype IsNot Nothing Then
                Dim pnlCRDocView As HtmlGenericControl = CType(e.Row.FindControl("pnlCRDocView"), HtmlGenericControl)
                Dim pnlCODocView As HtmlGenericControl = CType(e.Row.FindControl("pnlCODocView"), HtmlGenericControl)
                If pnlCRDocView IsNot Nothing And pnlCODocView IsNot Nothing Then
                    pnlCRDocView.Visible = False
                    pnlCODocView.Visible = False
                    If doctype.Text.ToLower().Equals("co online") Then
                        pnlCODocView.Visible = True
                    Else
                        pnlCRDocView.Visible = True
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub LbtGoDashboardClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtBackDashboard.Click
        DashboardLink()
    End Sub

    Protected Sub LbtReview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtReview.Click
        DocReviewed()
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal userid As Integer, ByVal doctype As String, ByVal codocid As Integer, ByVal taskdesc As String, ByVal indicativeprice As Integer)
        GvDocuments.DataSource = controller.GetCRCOTaskPendings_Multiple(doctype, userid, codocid, taskdesc, indicativeprice)
        GvDocuments.DataBind()

        If (GvDocuments.Rows.Count > 0) Then
            If HdnIsAuthorized.Value = "1" Then
                MvDigitalSignatureLogin.SetActiveView(VwLogin)
            Else
                MvDigitalSignatureLogin.SetActiveView(VwNotLogin)
            End If
        Else
            MvDigitalSignatureLogin.SetActiveView(VwEmptyDataRow)
        End If

    End Sub

    Private Sub DocReviewed()
        Dim j As Integer
        Dim intSuccess As Integer = 1
        For j = 0 To GvDocuments.Rows.Count - 1
            Dim chk As New HtmlInputCheckBox
            chk = GvDocuments.Rows(j).Cells(0).FindControl("ChkReview")
            If chk.Checked = True Then
                Dim lblSNO As Label = GvDocuments.Rows(j).Cells(0).FindControl("LblSNO")
                Dim LblDocType As Label = GvDocuments.Rows(j).Cells(0).FindControl("LblDocType")
                If Not String.IsNullOrEmpty(lblSNO.Text) And LblDocType IsNot Nothing Then
                    If LblDocType.Text.ToLower.Equals("co online") Then
                        Dim isSucceed As Boolean = controller.COMultipleApproval_Process(CommonSite.UserId, Convert.ToInt32(lblSNO.Text))
                        If isSucceed = False Then
                            intSuccess = 0
                            Exit For
                        End If
                    Else
                        Dim isSucceed As Boolean = controller.CRMultipleApproval_Process(CommonSite.UserId, Convert.ToInt32(lblSNO.Text))
                        If isSucceed = False Then
                            intSuccess = 0
                            Exit For
                        End If
                    End If
                End If
            End If
        Next
        If intSuccess = 0 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseErrorReviewer();", True)
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseReviewer();", True)
        End If
        BindData(CommonSite.UserId, GetDocType(), Integer.Parse(ConfigurationManager.AppSettings("codocid")), "approver", Integer.Parse(DdlPricingGrouping.SelectedValue))
    End Sub

    Private Function GetDocType() As String
        If DdlDocumentGrouping.SelectedValue.Equals("all") Then
            Return String.Empty
        Else
            Return DdlDocumentGrouping.SelectedValue
        End If
    End Function

    Private Sub DashboardLink()
        Select Case Session("User_Type")
            Case "N"
                Response.Redirect("../frmDashboard_Temp.aspx")
            Case "C"
                Response.Redirect("frmSiteAllTaskPending.aspx")
            Case "S"
                Response.Redirect("../frmDashboard_Temp.aspx")
        End Select
    End Sub
#End Region

End Class
