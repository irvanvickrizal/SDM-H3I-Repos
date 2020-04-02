Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data
Imports Common_NSNFramework

Partial Class DashBoard_MultipleDocReviewPendingList
    Inherits System.Web.UI.Page
    Dim objBo As New BODashBoard
    Dim dt As New DataTable
    Dim ObjUtil As New DBUtil
    Dim dbutils_nsn As New DBUtils_NSN
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
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
                chk.Checked = True
            Else
                chk.Checked = False
            End If
        Next
    End Sub

    Protected Sub GvDocReviewPageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GvDocReview.PageIndexChanging
        GvDocReview.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub LbtGoDashboardClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtBackDashboard.Click
        Select Case Session("User_Type")
            Case "N"
                Response.Redirect("../frmDashboard_Temp.aspx")
            Case "C"
                Response.Redirect("frmSiteAllTaskPending.aspx")
            Case "S"
                Response.Redirect("../frmDashboard_Temp.aspx")
        End Select
    End Sub

    Protected Sub LbtReviewDocClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtReview.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        DocReviewed()
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        dt = ObjUtil.ExeQueryDT("exec uspGetAllDocsReview " & CommonSite.UserId, "pendingdocs")
        GvDocReview.DataSource = dt
        GvDocReview.DataBind()
    End Sub

    Private Sub DocReviewed()
        Dim j As Integer
        For j = 0 To GvDocReview.Rows.Count - 1
            Dim chk As New HtmlInputCheckBox
            chk = GvDocReview.Rows(j).Cells(0).FindControl("ChkReview")
            If chk.Checked = True Then
                Dim lblSNO As Label = GvDocReview.Rows(j).Cells(0).FindControl("LblSNO")
                Dim lblSiteid As Label = GvDocReview.Rows(j).Cells(0).FindControl("LblSiteid")
                Dim lblSiteVersion As Label = GvDocReview.Rows(j).Cells(0).FindControl("LblSiteVersion")
                If Not String.IsNullOrEmpty(lblSNO.Text) Then
                    Dim i As Integer = ObjUtil.ExeQueryScalar("Exec [uspDocApproved] " & Convert.ToInt32(lblSNO.Text) & "," & CommonSite.UserId() & ",'" & CommonSite.UserName() & "'," & CommonSite.RollId() & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & ConfigurationManager.AppSettings("BAST2ID") & "")
                    dbutils_nsn.UpdateDocBAST(Integer.Parse(lblSiteid.Text), Integer.Parse(lblSiteVersion.Text), ConfigurationManager.AppSettings("BASTID"), ConfigurationManager.AppSettings("WCTRBASTID"), ConfigurationManager.AppSettings("BOQDOCID"))
                End If
            End If
        Next
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseReviewer();", True)
        BindData()
    End Sub
#End Region
End Class
