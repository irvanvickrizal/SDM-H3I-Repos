Imports System.Data
Imports BusinessLogic
Imports Common
Imports Entities
Imports System.IO


Partial Class Dashboard_New_frmRejectedDocument_Initiator
    Inherits System.Web.UI.Page
    Dim objBO As New BODashBoard
    Dim sqlstr As String
    Dim objUtil As New DBUtil


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub
    Protected Sub GvRejectedDocuments_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvRejectedDocuments.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbtSubmitWCC As LinkButton = CType(e.Row.FindControl("lbtEditDoc"), LinkButton)
        End If
    End Sub
    Protected Sub GvRejectedDocuments_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvRejectedDocuments.RowCommand
        If e.CommandName.Equals("EditDoc") Then
            Response.Redirect("../PO/frmSiteDocUploadTree.aspx?wid=" + e.CommandArgument.ToString())
        End If
    End Sub

    Protected Sub GvRejectedDocuments_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvRejectedDocuments.PageIndexChanging
        GvRejectedDocuments.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub BtnExprtExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExport.Click
        If GvRejectedDocuments.Rows.Count > 0 Then
            ExportToExcel()
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "invalidExportToExcel();", True)
            End If
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        Dim dtStatus As New DataTable
        sqlstr = "exec [HCPT_uspTrans_GetRejectedDocs] " & CommonSite.UserId().ToString
        dtStatus = objUtil.ExeQueryDT(sqlstr, "DashBoardUploadDoc")

        GvRejectedDocuments.DataSource = dtStatus
        GvRejectedDocuments.DataBind()
    End Sub

    Public Sub ExportToExcel()
        Dim tw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()
        Dim strFilename As String = "2G_DocRejectionLists_" + DateTime.Now.ToShortDateString + ".xls"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("content-disposition", "attachment;filename=" & strFilename)
        Response.Charset = ""
        EnableViewState = False
        Controls.Add(frm)
        GvRejectedDocuments.AllowPaging = False
        bindData()
        frm.Controls.Add(GvRejectedDocuments)
        frm.RenderControl(hw)
        Response.Write(tw.ToString())
        Response.End()
    End Sub
#End Region

End Class
