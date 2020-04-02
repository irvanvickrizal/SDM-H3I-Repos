Imports System.Data
Imports Common
Imports System.IO
Imports NSNCustomizeConfiguration

Partial Class RPT_frmEBASTDone
    Inherits System.Web.UI.Page
    Dim objutil As New DBUtil
    Dim dtra As New DataTable
    Dim dtra_2 As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            LblErrorMessage.Visible = False
            Dim dates As DataTable = objutil.ExeQueryDT("select datename(dd,DATEADD(day,-30,GETDATE())) + '-' + datename(month,DATEADD(day,-30,GETDATE())) + '-' + datename(year,DATEADD(day,-30,GETDATE())), datename(dd,GETDATE()) + '-' + datename(month,GETDATE()) + '-' + datename(year,GETDATE()) ", "dates")
            TxtATPStartDate.Text = dates.Rows(0).Item(0).ToString()
            TxtEndDateTime.Text = dates.Rows(0).Item(1).ToString()
        End If
    End Sub

    Protected Sub LbtRefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtRefresh.Click
        LblWarningMessage.Visible = False
        If ((String.IsNullOrEmpty(TxtATPStartDate.Text) = False) And (String.IsNullOrEmpty(TxtEndDateTime.Text) = False)) Then
            Dim isValidDate As Boolean = ValidateDateRange(DateTime.ParseExact(TxtATPStartDate.Text, "dd-MMMM-yyyy", Nothing), DateTime.ParseExact(TxtEndDateTime.Text, "dd-MMMM-yyyy", Nothing))
            If isValidDate Then
                dtra = objutil.ExeQueryDT("select ara_id,rgn_id from ebastuserrole where usr_id=" & Session("User_Id") & "", "RA")
                If dtra.Rows.Count = 0 Then
                    GvBASTReport.DataSource = GetBASTDoneReportByDate(DateTime.ParseExact(TxtATPStartDate.Text, "dd-MMMM-yyyy", Nothing), DateTime.ParseExact(TxtEndDateTime.Text, "dd-MMMM-yyyy", Nothing), 0, ConfigurationManager.AppSettings("BAUTID"), ConfigurationManager.AppSettings("BASTID"))
                    GvBASTReport.DataBind()
                Else
                    Dim area As Integer = dtra.Rows(0).Item(0).ToString
                    Dim region As Integer = dtra.Rows(0).Item(1).ToString
                    If region <> 0 Or area <> 0 Then
                        GvBASTReport.DataSource = GetBASTDoneReportByDate(DateTime.ParseExact(TxtATPStartDate.Text, "dd-MMMM-yyyy", Nothing), DateTime.ParseExact(TxtEndDateTime.Text, "dd-MMMM-yyyy", Nothing), Convert.ToInt16(CommonSite.UserId), ConfigurationManager.AppSettings("BAUTID"), ConfigurationManager.AppSettings("BASTID"))
                        GvBASTReport.DataBind()
                    Else
                        GvBASTReport.DataSource = GetBASTDoneReportByDate(DateTime.ParseExact(TxtATPStartDate.Text, "dd-MMMM-yyyy", Nothing), DateTime.ParseExact(TxtEndDateTime.Text, "dd-MMMM-yyyy", Nothing), 0, ConfigurationManager.AppSettings("BAUTID"), ConfigurationManager.AppSettings("BASTID"))
                        GvBASTReport.DataBind()
                    End If
                End If
            Else
                GvBASTReport.Visible = False
                LblErrorMessage.Visible = True
            End If
        End If
    End Sub

    Protected Sub BtnExportExcelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportExcel.Click
        If (GvBASTReport.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "BASTDoneReport_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GvBASTReport)
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

#Region "Custom Methods"
    Private Sub GetTotalBAST()
        dtra_2 = objutil.ExeQueryDT("select ara_id,rgn_id from ebastuserrole where usr_id=" & Session("User_Id") & "", "RA")
        If dtra_2.Rows.Count = 0 Then
            LblBASTDoneTotal.Text = "BAST Done Total (all) :" & objutil.ExeQueryScalar("exec uspGetCountBASTDoneNational " & CommonSite.UserId & ", " & ConfigurationManager.AppSettings("BAUTID") & ", " & ConfigurationManager.AppSettings("BASTID"))
        Else
            Dim area As Integer = dtra_2.Rows(0).Item(0).ToString
            Dim region As Integer = dtra_2.Rows(0).Item(1).ToString

            If area <> 0 Or region <> 0 Then
                LblBASTDoneTotal.Text = "BAST Done Total (all) :" & objutil.ExeQueryScalar("exec uspGetCountBASTDone " & CommonSite.UserId & ", " & ConfigurationManager.AppSettings("BAUTID") & ", " & ConfigurationManager.AppSettings("BASTID"))
            Else
                LblBASTDoneTotal.Text = "BAST Done Total (all) :" & objutil.ExeQueryScalar("exec uspGetCountBASTDoneNational " & CommonSite.UserId & ", " & ConfigurationManager.AppSettings("BAUTID") & ", " & ConfigurationManager.AppSettings("BASTID"))
            End If
        End If
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
