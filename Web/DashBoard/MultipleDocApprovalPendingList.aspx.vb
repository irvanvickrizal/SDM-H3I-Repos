Imports System.Data
Imports Common
Imports Entities
Imports BusinessLogic
Imports Common_NSNFramework

Partial Class DashBoard_MultipleDocApprovalPendingList
    Inherits System.Web.UI.Page
    Dim objBo As New BODashBoard
    Dim dt As New DataTable
    Dim ObjUtil As New DBUtil
    Dim dbutils_nsn As New DBUtils_NSN
    Dim objsms As New SMSNew

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
            HdnIsAuthorized.Value = 0
            txtUserName.Text = Session("User_Login")
            txtUserName.ReadOnly = True

        End If
    End Sub

    Sub checkall(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim mainchk As New CheckBox
        mainchk = GvDocReview.HeaderRow.Cells(0).FindControl("chkall")
        Dim i As Integer
        For i = 0 To GvDocReview.Rows.Count - 1
            Dim chk As New HtmlInputCheckBox
            chk = GvDocReview.Rows(i).Cells(0).FindControl("ChkReview")
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

    Protected Sub GvDocReviewPageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GvDocReview.PageIndexChanging
        GvDocReview.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub GvDocReview_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvDocReview.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LblDocId As Label = CType(e.Row.FindControl("LblDocId"), Label)
            Dim LblSiteid As Label = CType(e.Row.FindControl("LblSiteid"), Label)
            Dim LblSiteVersion As Label = CType(e.Row.FindControl("LblSiteVersion"), Label)
            Dim LblRemarks As Label = CType(e.Row.FindControl("LblRemarks"), Label)
            Dim ChkReview As HtmlInputCheckBox = CType(e.Row.FindControl("ChkReview"), HtmlInputCheckBox)
            If LblDocId.Text = "1032" Then
                Dim rowcompleted As Integer = ObjUtil.ExeQueryScalar("select count(*) from wftransaction where site_id =" & LblSiteid.Text & " and siteversion=" & LblSiteVersion.Text & " and enddatetime is null")
                If rowcompleted = 1 Then
                    LblRemarks.Visible = False
                    ChkReview.Disabled = False
                Else
                    ChkReview.Disabled = True
                    LblRemarks.Text = "Not Yet Completed"
                    LblRemarks.Visible = True
                End If
            End If
        End If
    End Sub



    Protected Sub LbtGoDashboardClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtBackDashboard.Click
        DashboardLink()
    End Sub

    Protected Sub LbtReviewDocClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtReview.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        DocReviewed()
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

#Region "Custom Mehtods"
    Private Sub BindData()
        dt = ObjUtil.ExeQueryDT("exec uspGetAllDocsApproval " & CommonSite.UserId, "pendingdocs")
        GvDocReview.DataSource = dt
        GvDocReview.DataBind()
        If dt.Rows.Count > 0 Then
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
        Dim intSuccess As Integer = 0
        For j = 0 To GvDocReview.Rows.Count - 1
            Dim chk As New HtmlInputCheckBox
            chk = GvDocReview.Rows(j).Cells(0).FindControl("ChkReview")
            If chk.Checked = True Then
                Dim lblSNO As Label = GvDocReview.Rows(j).Cells(0).FindControl("LblSNO")
                Dim lblSiteid As Label = GvDocReview.Rows(j).Cells(0).FindControl("LblSiteid")
                Dim lblSiteVersion As Label = GvDocReview.Rows(j).Cells(0).FindControl("LblSiteVersion")
                If Not String.IsNullOrEmpty(lblSNO.Text) Then
                    Dim strQuery As String = "Exec [uspDocApproved_Multiple] " & Convert.ToInt32(lblSNO.Text) & "," & CommonSite.UserId() & ",'" & txtUserName.Text & "'," & CommonSite.RollId() & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & ConfigurationManager.AppSettings("BAST2ID")
                    intSuccess = ObjUtil.ExeQueryScalar(strQuery)
                End If
            End If
        Next
        If intSuccess = 0 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseErrorReviewer();", True)
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseReviewer();", True)
        End If
        BindData()
    End Sub

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
