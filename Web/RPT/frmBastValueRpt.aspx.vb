Imports Common_NSNFramework
Imports System.IO
Imports Common
Imports CommonSite
Imports BusinessLogic
Imports System.Data

Partial Class RPT_frmBastValueRpt
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim objBO As New BOUserType
    Dim objutil As New DBUtil
    Dim dbutils_nsn As New DBUtils_NSN
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("user_name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BtnExportExcel2.Enabled = False
            GetReports(CommonSite.UserId)
        End If
    End Sub

    Protected Sub BtnExportExcelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportExcel2.Click
        If GvBastValueReport.Rows.Count > 0 Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "BastValueReport_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GvBastValueReport)
            frm.RenderControl(hw)
            Response.Write(tw.ToString())
            Response.End()
        End If
    End Sub


#Region "custom methods"
    Private Sub GetReports(ByVal userid As Integer)
        Dim strSql As String = "select ARA_Id, RGN_Id from ebastuserrole where usr_id=" & userid
        dt = objutil.ExeQueryDT(strSql, "userattr")
        GvBastValueReport.DataSource = dbutils_nsn.GetBastValueReport(dt.Rows(0).Item(0), dt.Rows(0).Item(1))
        GvBastValueReport.DataBind()

        If GvBastValueReport.Rows.Count > 0 Then
            BtnExportExcel2.Enabled = True
        End If
    End Sub
#End Region
End Class
