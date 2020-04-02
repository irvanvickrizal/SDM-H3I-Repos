Imports System.IO
Imports System.Data
Imports Common
Imports NSNCustomizeConfiguration

Partial Class RPT_frmEBAUTDone
    Inherits System.Web.UI.Page
    Dim objutil As New DBUtil
    Dim dtra As New DataTable
    Dim dtra_2 As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            LblDocPanel.Text = GetDOCNAME(GetDOCID()) & " DONE REPORT"
            LblErrorMessage.Visible = False
            Dim dates As DataTable = objutil.ExeQueryDT("select datename(dd,DATEADD(day,-30,GETDATE())) + '-' + datename(month,DATEADD(day,-30,GETDATE())) + '-' + datename(year,DATEADD(day,-30,GETDATE())), datename(dd,GETDATE()) + '-' + datename(month,GETDATE()) + '-' + datename(year,GETDATE()) ", "dates")
            Dim startdate As DateTime = Now.Date()
            Dim enddate As DateTime = DateAdd(DateInterval.Day, 30, Now)
            TxtStartDate.Text = dates.Rows(0).Item(0).ToString()
            TxtEndDateTime.Text = dates.Rows(0).Item(1).ToString()
            GetDocDoneReportByDate(startdate, enddate, CommonSite.UserId, GetDOCID())
        End If
    End Sub

    Protected Sub BtnExportExcelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportExcel.Click
        If (GvRFTReport.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "BAUTDoneReport_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GvRFTReport)
            frm.RenderControl(hw)
            Response.Write(tw.ToString())
            Response.End()
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "invalidExportToExcel();", True)
            End If
        End If
    End Sub

    Protected Sub LbtRefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtRefresh.Click
        If ValidationChecking() = True Then
            GvRFTReport.DataSource = GetDocDoneReportByDate(GetConvertDate(TxtStartDate.Text), GetConvertDate(TxtEndDateTime.Text), CommonSite.UserId, GetDOCID())
            GvRFTReport.DataBind()
        End If
    End Sub

    Protected Sub GvRFTReport_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvRFTReport.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LblLeadTime As Label = CType(e.Row.FindControl("LblLeadTime"), Label)
            Dim submitdate As DateTime = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "SubmitDate"))
            Dim approveddate As DateTime = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "ApprovedDate"))
            LblLeadTime.Text = DateDiff(DateInterval.Day, submitdate, approveddate)
        End If
    End Sub

#Region "custom methods"
    Private Function ValidateDateRange(ByVal startdate As DateTime, ByVal enddate As DateTime) As Boolean
        Dim isValid As Boolean = True

        Dim dateState As Integer = Date.Compare(startdate, enddate)

        If dateState > 0 Then
            isValid = False
        End If
        Return isValid
    End Function

    Private Function GetConvertDate(ByVal strDate As String) As Nullable(Of DateTime)
        If Not String.IsNullOrEmpty(strDate) Then
            Return DateTime.ParseExact(strDate, "dd-MMMM-yyyy", Nothing)
        End If
        Return Nothing
    End Function

    Private Function ValidationChecking() As Boolean
        Dim isValid As Boolean = True
        LblWarningMessage.Visible = False
        If String.IsNullOrEmpty(TxtStartDate.Text) And Not String.IsNullOrEmpty(TxtEndDateTime.Text) And isValid = True Then
            LblWarningMessage.Text = "Please define start time if you want to search with date range!"
            LblWarningMessage.Visible = True
            isValid = False
        End If

        Return isValid
    End Function

    Private Function GetDOCID() As Integer
        If String.IsNullOrEmpty(Request.QueryString("docid")) Then
            Return 0
        Else
            Return Integer.Parse(Request.QueryString("docid"))
        End If
    End Function

    Private Function GetDOCNAME(ByVal docid As Integer) As String
        If docid = 1031 Then
            Return "Final Acceptance Certificate (FAC)"
        ElseIf docid = 2047 Then
            Return "Provisional Acceptance Certificate (PAC)"
        ElseIf docid = 2001 Then
            Return "ATP CERTIFICATE"
        End If
        Return ""
    End Function
#End Region

End Class
