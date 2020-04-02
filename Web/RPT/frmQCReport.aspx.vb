Imports Common
Imports System.IO
Imports NSNCustomizeConfiguration
Imports System.Data


Partial Class RPT_frmQCReport
    Inherits System.Web.UI.Page
    Dim objutil As New DBUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            LblErrorMessage.Visible = False
            Dim dates As DataTable = objutil.ExeQueryDT("select datename(dd,DATEADD(day,-30,GETDATE())) + '-' + datename(month,DATEADD(day,-30,GETDATE())) + '-' + datename(year,DATEADD(day,-30,GETDATE())), datename(dd,GETDATE()) + '-' + datename(month,GETDATE()) + '-' + datename(year,GETDATE()) ", "dates")
            TxtATPStartDate.Text = dates.Rows(0).Item(0).ToString()
            TxtEndDateTime.Text = dates.Rows(0).Item(1).ToString()
            BindRegionData()
        End If
    End Sub

    Protected Sub BtnExportExcelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportExcel2.Click
        If (GvQCReport.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "QCDoneReport_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GvQCReport)
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

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub


    Protected Sub LbtRefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtRefresh.Click
        If ((String.IsNullOrEmpty(TxtATPStartDate.Text) = False) And (String.IsNullOrEmpty(TxtEndDateTime.Text) = False)) Then
            Dim isValidDate As Boolean = ValidateDateRange(DateTime.ParseExact(TxtATPStartDate.Text, "dd-MMMM-yyyy", Nothing), DateTime.ParseExact(TxtEndDateTime.Text, "dd-MMMM-yyyy", Nothing))
            If isValidDate Then
                If ChkStartDate.Checked = True And ChkEndDateTime.Checked = True And ChkRegion.Checked = False Then
                    LoadReportByDate(CommonSite.UserId, DateTime.ParseExact(TxtATPStartDate.Text, "dd-MMMM-yyyy", Nothing), DateTime.ParseExact(TxtEndDateTime.Text, "dd-MMMM-yyyy", Nothing))
                ElseIf ChkStartDate.Checked = True And ChkEndDateTime.Checked = False And ChkRegion.Checked = False Then
                    LoadReportByApprovedDate(CommonSite.UserId, DateTime.ParseExact(TxtATPStartDate.Text, "dd-MMMM-yyyy", Nothing))
                ElseIf ChkStartDate.Checked = True And ChkEndDateTime.Checked = False And ChkRegion.Checked = True Then
                    If DdlRegion.SelectedIndex = -1 Then
                        LoadReportByApprovedDate(CommonSite.UserId, DateTime.ParseExact(TxtATPStartDate.Text, "dd-MMMM-yyyy", Nothing))
                    Else
                        LoadReportByApprovedDateRegion(CommonSite.UserId, DateTime.ParseExact(TxtATPStartDate.Text, "dd-MMMM-yyyy", Nothing), Integer.Parse(DdlRegion.SelectedValue))
                    End If
                ElseIf ChkStartDate.Checked = False And ChkEndDateTime.Checked = False And ChkRegion.Checked = True Then
                    If DdlRegion.SelectedIndex = -1 Then
                        LoadReport(CommonSite.UserId)
                    Else
                        LoadReportByRegion(CommonSite.UserId, Integer.Parse(DdlRegion.SelectedValue))
                    End If

                End If
                GvQCReport.Visible = True
                LblErrorMessage.Visible = False
            Else
                GvQCReport.Visible = False
                LblErrorMessage.Visible = True
            End If
        End If
    End Sub


    Protected Sub LbtSearchClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSearch.Click
        If (Not String.IsNullOrEmpty(TxtSearch.Text)) Then
            LoadReportBySearch(Convert.ToInt32(CommonSite.UserId), TxtSearch.Text)
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindRegionData()
        Dim dtTable As DataTable = objutil.ExeQueryDT("exec uspGetRegionByUserId " & CommonSite.UserId, "dt")
        DdlRegion.DataSource = dtTable
        DdlRegion.DataTextField = "RgnName"
        DdlRegion.DataValueField = "rgn_id"
        DdlRegion.DataBind()
        DdlRegion.Items.Insert(0, "--All--")
    End Sub

    Protected Sub LoadReport(ByVal userid As Int16)
        GvQCReport.DataSource = GetQCDone(userid, Integer.Parse(ConfigurationManager.AppSettings("QCID")))
        GvQCReport.DataBind()
    End Sub

    Protected Sub LoadReportByDate(ByVal userid As Int16, ByVal startdate As DateTime, ByVal enddate As DateTime)
        GvQCReport.DataSource = GetQCDoneByDate(userid, startdate, enddate, Integer.Parse(ConfigurationManager.AppSettings("QCID")))
        GvQCReport.DataBind()
    End Sub

    Protected Sub LoadReportByApprovedDate(ByVal userid As Int16, ByVal startdate As DateTime)
        GvQCReport.DataSource = GetQCDoneByApprovedDate(userid, startdate, Integer.Parse(ConfigurationManager.AppSettings("QCID")))
        GvQCReport.DataBind()
    End Sub

    Protected Sub LoadReportByApprovedDateRegion(ByVal userid As Int16, ByVal startdate As DateTime, ByVal rgnid As Integer)
        GvQCReport.DataSource = GetQCDoneByApprovedDateRegion(userid, startdate, rgnid, Integer.Parse(ConfigurationManager.AppSettings("QCID")))
        GvQCReport.DataBind()
    End Sub

    Protected Sub LoadReportByRegion(ByVal userid As Int16, ByVal rgnid As Integer)
        GvQCReport.DataSource = GetQCDoneByRegion(userid, rgnid, Integer.Parse(ConfigurationManager.AppSettings("QCID")))
        GvQCReport.DataBind()
    End Sub

    Protected Sub LoadReportBySearch(ByVal userid As Int32, ByVal strSearch As String)
        GvQCReport.DataSource = GetQCDoneBySearch(userid, strSearch, Integer.Parse(ConfigurationManager.AppSettings("QCID")))
        GvQCReport.DataBind()
    End Sub

    Private Function ValidateDateRange(ByVal startdate As DateTime, ByVal enddate As DateTime) As Boolean
        Dim isValid As Boolean = True

        Dim dateState As Integer = Date.Compare(startdate, enddate)

        If dateState > 0 Then
            isValid = False
        End If
        Return isValid
    End Function

#End Region

End Class
