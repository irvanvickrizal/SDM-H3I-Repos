Imports Common
Imports BusinessLogic
Imports System.Data
Imports System.IO


Partial Class HCPT_Dashboard_frmCACReadyCreation
    Inherits System.Web.UI.Page
    Dim objutil As New DBUtil
    Dim dtDocs As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub GvCACReport_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvCACReport.PageIndexChanging
        GvCACReport.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub BtnExpt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportExcel.Click
        If GvCACReport.Rows.Count() > 0 Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "CACReadyCreation_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            GvCACReport.AllowPaging = False
            BindData()
            frm.Controls.Add(GvCACReport)
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
    Private Sub BindData()
        dtDocs = objutil.ExeQueryDT("exec HCPT_uspCAC_GetReadyCreation " & CommonSite.UserId & ", " & ConfigurationManager.AppSettings("BAUTID"), "dt")
        If dtDocs.Rows.Count > 0 Then
            GvCACReport.DataSource = dtDocs
            GvCACReport.DataBind()
        Else
            GvCACReport.DataSource = Nothing
            GvCACReport.DataBind()
        End If
    End Sub
#End Region

End Class
