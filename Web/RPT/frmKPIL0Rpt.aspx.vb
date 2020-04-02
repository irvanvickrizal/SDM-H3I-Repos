﻿Imports Common
Imports System.IO
'Imports Common_NSNFramework
Imports NSNCustomizeConfiguration
Imports System.Data


Partial Class RPT_frmKPIL0Rpt
    Inherits System.Web.UI.Page
    Dim dbutilsnsn As New NSNCustomizeConfiguration
    Dim objutil As New DBUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LblErrorMessage.Visible = False
            Dim dates As DataTable = objutil.ExeQueryDT("select datename(dd,DATEADD(day,-30,GETDATE())) + '-' + datename(month,DATEADD(day,-30,GETDATE())) + '-' + datename(year,DATEADD(day,-30,GETDATE())), datename(dd,GETDATE()) + '-' + datename(month,GETDATE()) + '-' + datename(year,GETDATE()) ", "dates")
            TxtATPStartDate.Text = dates.Rows(0).Item(0).ToString()
            TxtEndDateTime.Text = dates.Rows(0).Item(1).ToString()
            If Not String.IsNullOrEmpty(GetFrom()) Then
                btnDashboard.Visible = True
            Else
                btnDashboard.Visible = False
            End If
        End If
    End Sub

    Protected Sub BtnExportExcelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportExcel2.Click
        If (gvDocReports.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "KPIL0DoneReport_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(gvDocReports)
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



    Protected Sub ImgRefreshClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgRefresh.Click
        If ((String.IsNullOrEmpty(TxtATPStartDate.Text) = False) And (String.IsNullOrEmpty(TxtEndDateTime.Text) = False)) Then
            Dim isValidDate As Boolean = ValidateDateRange(DateTime.ParseExact(TxtATPStartDate.Text, "dd-MMMM-yyyy", Nothing), DateTime.ParseExact(TxtEndDateTime.Text, "dd-MMMM-yyyy", Nothing))
            If isValidDate Then
                LoadReportByDate(CommonSite.UserId, DateTime.ParseExact(TxtATPStartDate.Text, "dd-MMMM-yyyy", Nothing), DateTime.ParseExact(TxtEndDateTime.Text, "dd-MMMM-yyyy", Nothing))
                gvDocReports.Visible = True
                LblErrorMessage.Visible = False
            Else
                gvDocReports.Visible = False
                LblErrorMessage.Visible = True
            End If
        End If
    End Sub

    Protected Sub GvAtpDoneDataRowBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDocReports.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow

        End Select
    End Sub

    Protected Sub btnDashboard_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDashboard.Click
        Response.Redirect("../dashboard/frmDashboardNPO.aspx")
    End Sub

#Region "Custom Methods"
    Protected Sub LoadReport(ByVal userid As Int16)
        If (gvDocReports.Rows.Count() > 0) Then
            BtnExportExcel2.Enabled = True
        Else
            BtnExportExcel2.Enabled = False
        End If
    End Sub

    Protected Sub LoadReportByDate(ByVal userid As Int16, ByVal startdate As DateTime, ByVal enddate As DateTime)
        gvDocReports.DataSource = GetDocOnlineDoneByDate(GetDocID(), startdate, enddate, userid)
        gvDocReports.DataBind()
    End Sub

    Private Function ValidateDateRange(ByVal startdate As DateTime, ByVal enddate As DateTime) As Boolean
        Dim isValid As Boolean = True

        Dim dateState As Integer = Date.Compare(startdate, enddate)

        If dateState > 0 Then
            isValid = False
        End If
        Return isValid
    End Function

    Private Function GetDocID() As Integer
        If (Not String.IsNullOrEmpty(Request.QueryString("docid"))) Then
            Return Integer.Parse(Request.QueryString("docid"))
        Else
            Return 0
        End If
    End Function

    Private Function GetFrom() As String
        If Not String.IsNullOrEmpty(Request.QueryString("from")) Then
            Return Request.QueryString("from")
        Else
            Return String.Empty
        End If
    End Function
#End Region

End Class
