Imports System.IO

Partial Class fancybox_Form_fb_WCCReadyCreation
    Inherits System.Web.UI.Page
    Private controller As New WCCController()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If (Not Page.IsPostBack) Then
            BindData(CommonSite.UserId)
        End If
    End Sub

    Protected Sub LbtExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtExport.Click
        ExportToExcel(GvWCCList)
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal userid As Integer)
        GvWCCList.DataSource = controller.GetWCCReadyCreation(userid)
        GvWCCList.DataBind()
    End Sub

    Private Sub ExportToExcel(ByVal gv As GridView)
        If (gv.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "WCCReadyCreation_" + DateTime.Now.ToShortDateString + ".xls"
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
