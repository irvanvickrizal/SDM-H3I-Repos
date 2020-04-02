Imports System.IO
Partial Class RPT_frmWCCReport
    Inherits System.Web.UI.Page
    Dim rptcontroller As New WCCReportController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
    End Sub

    Protected Sub LbtSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSearch.Click
        If DdlReportType.SelectedValue = "1" Then
            GetReports("wccdone", CommonSite.UserId)
        ElseIf DdlReportType.SelectedValue = "2" Then
            GetReports("wccapprovalstatus", CommonSite.UserId)
        ElseIf DdlReportType.SelectedValue = "3" Then
            GetReports("wccpreparation", CommonSite.UserId)
        ElseIf DdlReportType.SelectedValue = "4" Then
            GetReports("wccrejection", CommonSite.UserId)
        ElseIf DdlReportType.SelectedValue = "5" Then
            GetReports("wcchistoricalreject", CommonSite.UserId)
        End If
    End Sub
    Protected Sub LbtReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtReset.Click
        TxtStartTime.Text = ""
        TxtEndTime.Text = ""
        DdlReportType.SelectedValue = "0"
        GetReports("nothing", CommonSite.UserId)
    End Sub

    Protected Sub BtnExportToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtExport.Click
        If DdlReportType.SelectedValue = "1" Then
            ExportToExcel(GvWCCReportExport, "WCCDone")
        ElseIf DdlReportType.SelectedValue = "2" Then
            ExportToExcel(GvWCCReportExport, "WCCOnTaskPending")
        ElseIf DdlReportType.SelectedValue = "3" Then
            ExportToExcel(GvWCCReportExport, "WCCPreparation")
        ElseIf DdlReportType.SelectedValue = "4" Then
            ExportToExcel(GvWCCReportExport, "WCCRejection")
        ElseIf DdlReportType.SelectedValue = "5" Then
            ExportToExcel(GvWCCReportExport, "WCCHistoricalRejection")
        End If
    End Sub

    Protected Sub DdlReportType_selectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlReportType.SelectedIndexChanged
        LbtSearch.Visible = True
        LbtReset.Visible = True
        TxtStartTime.Enabled = True
        TxtEndTime.Enabled = True
        If DdlReportType.SelectedValue = "0" Then
            LbtSearch.Visible = False
            LbtReset.Visible = False
            TxtStartTime.Enabled = False
            TxtEndTime.Enabled = False
        Else
            If DdlReportType.SelectedValue <> "1" And DdlReportType.SelectedValue <> "5" Then
                TxtStartTime.Enabled = False
                TxtEndTime.Enabled = False
                TxtStartTime.Text = ""
                TxtEndTime.Text = ""
            End If
        End If
    End Sub

#Region "Custom Methods"
    Private Sub GetReports(ByVal reporttype As String, ByVal userid As Integer)
        GvWCCReportExport.Columns(3).Visible = True
        GvWCCReportExport.Columns(4).Visible = True
        GvWCCReportExport.Columns(14).Visible = True 'waiting approval
        GvWCCReportExport.Columns(15).Visible = True 'approver name
        GvWCCReportExport.Columns(16).Visible = True 'approver date
        GvWCCReportExport.Columns(17).Visible = True 'Rejection name 
        GvWCCReportExport.Columns(18).Visible = True 'Rejection Date
        GvWCCReportExport.Columns(19).Visible = True 'Rejection Remarks
        GvWCCReportExport.Columns(20).Visible = True 'Rejection Category
        GvWCCReportExport.Columns(21).Visible = True 'Upload Name
        GvWCCReportExport.Columns(22).Visible = True 'Reupload Date 


        If reporttype.Equals("nothing") Then
            GvWCCReportExport.DataSource = Nothing
            GvWCCReportExport.DataBind()

        End If
        If reporttype.Equals("wccdone") Then
            GvWCCReportExport.DataSource = rptcontroller.GetWCCDone(GetStartTime(), GetEndTime(), userid)
            GvWCCReportExport.DataBind()
            GvWCCReportExport.Columns(14).Visible = False 'waiting approval
            GvWCCReportExport.Columns(17).Visible = False 'Rejection name 
            GvWCCReportExport.Columns(18).Visible = False 'Rejection Date
            GvWCCReportExport.Columns(19).Visible = False 'Rejection Remarks
            GvWCCReportExport.Columns(20).Visible = False 'Rejection Category
            GvWCCReportExport.Columns(21).Visible = False 'Upload Name
            GvWCCReportExport.Columns(22).Visible = False 'Reupload Date 
        End If
        If reporttype.Equals("wccapprovalstatus") Then
            GvWCCReportExport.DataSource = rptcontroller.GetWCCOnTaskPending(GetStartTime(), GetEndTime(), userid)
            GvWCCReportExport.DataBind()
            GvWCCReportExport.Columns(15).Visible = False 'approver name
            GvWCCReportExport.Columns(16).Visible = False 'approver date
            GvWCCReportExport.Columns(17).Visible = False 'Rejection name 
            GvWCCReportExport.Columns(18).Visible = False 'Rejection Date
            GvWCCReportExport.Columns(19).Visible = False 'Rejection Remarks
            GvWCCReportExport.Columns(20).Visible = False 'Rejection Category
            GvWCCReportExport.Columns(21).Visible = False 'Upload Name
            GvWCCReportExport.Columns(22).Visible = False 'Reupload Date 
        End If
        If reporttype.Equals("wccpreparation") Then
            GvWCCReportExport.DataSource = rptcontroller.GetWCCPreparation(GetStartTime(), GetEndTime(), userid)
            GvWCCReportExport.DataBind()
            GvWCCReportExport.Columns(14).Visible = False 'waiting approval
            GvWCCReportExport.Columns(15).Visible = False 'approver name
            GvWCCReportExport.Columns(16).Visible = False 'approver date
            GvWCCReportExport.Columns(17).Visible = False 'Rejection name 
            GvWCCReportExport.Columns(18).Visible = False 'Rejection Date
            GvWCCReportExport.Columns(19).Visible = False 'Rejection Remarks
            GvWCCReportExport.Columns(20).Visible = False 'Rejection Category
            GvWCCReportExport.Columns(21).Visible = False 'Upload Name
            GvWCCReportExport.Columns(22).Visible = False 'Reupload Date 
        End If
        If reporttype.Equals("wccrejection") Then
            GvWCCReportExport.DataSource = rptcontroller.GetWCCRejection(GetStartTime(), GetEndTime(), userid)
            GvWCCReportExport.DataBind()
            GvWCCReportExport.Columns(3).Visible = False
            GvWCCReportExport.Columns(4).Visible = False
            GvWCCReportExport.Columns(14).Visible = False 'waiting approval
            GvWCCReportExport.Columns(15).Visible = False 'approver name
            GvWCCReportExport.Columns(16).Visible = False 'approver date
            GvWCCReportExport.Columns(21).Visible = False 'Upload Name
            GvWCCReportExport.Columns(22).Visible = False 'Reupload Date 
            GvWCCReportExport.Columns(20).Visible = False 'Rejection Category
        End If
        If reporttype.Equals("wcchistoricalreject") Then
            GvWCCReportExport.DataSource = rptcontroller.GetWCCHistoricalRejection(GetStartTime(), GetEndTime(), userid)
            GvWCCReportExport.DataBind()
            GvWCCReportExport.Columns(3).Visible = False
            GvWCCReportExport.Columns(4).Visible = False
            GvWCCReportExport.Columns(14).Visible = False 'waiting approval
            GvWCCReportExport.Columns(15).Visible = False 'approver name
            GvWCCReportExport.Columns(16).Visible = False 'approver date
        End If
    End Sub

    Private Function GetStartTime() As System.Nullable(Of DateTime)
        If String.IsNullOrEmpty(TxtStartTime.Text) Then
            Return Nothing
        Else
            Return DateTime.ParseExact(TxtStartTime.Text, "dd-MMMM-yyyy", Nothing)
        End If
    End Function

    Private Function GetEndTime() As System.Nullable(Of DateTime)
        If String.IsNullOrEmpty(TxtEndTime.Text) Then
            Return Nothing
        Else
            Return DateTime.ParseExact(TxtEndTime.Text, "dd-MMMM-yyyy", Nothing)
        End If
    End Function

    Private Sub ExportToExcel(ByVal gv As GridView, ByVal reporttype As String)
        If (gv.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = reporttype + "_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(gv)
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
#End Region

End Class
