Imports System.Data
Imports BusinessLogic
Imports Common
Imports Entities
Imports System.IO


Partial Class DashBoard_frmRejectedDocNPO
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

    Protected Sub GvRejectedDocuments_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvRejectedDocuments.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblDocID As Label = CType(e.Row.FindControl("lblDocID"), Label)
            Dim imgSubmitOLDoc As ImageButton = CType(e.Row.FindControl("imgSubmitOLDoc"), ImageButton)
            Dim imgSubmitScanDoc As ImageButton = CType(e.Row.FindControl("imgSubmitScanDoc"), ImageButton)
            If lblDocID IsNot Nothing And imgSubmitOLDoc IsNot Nothing And imgSubmitScanDoc IsNot Nothing Then
                If lblDocID.Text = "2146" Then
                    imgSubmitScanDoc.Visible = True
                    imgSubmitOLDoc.Visible = False
                Else
                    imgSubmitScanDoc.Visible = False
                    imgSubmitOLDoc.Visible = True
                End If
            End If
        End If
    End Sub

    Protected Sub gvRejectedDocuments_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvRejectedDocuments.RowCommand
        Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
        Dim lblWPID As Label = DirectCast(row.FindControl("lblWPID"), Label)
        Dim lblSiteno As Label = DirectCast(row.FindControl("lblSiteno"), Label)
        If e.CommandName.Equals("submitform") Then
            Response.Redirect("frmOnlineKPIL0.aspx?id=" & e.CommandArgument.ToString() & "&wpid=" & lblWPID.Text & "&from=rc")
        ElseIf e.CommandName.Equals("submitmanualform") Then
            Response.Redirect("frmTreeDocUploadNPO.aspx?docid=" & ConfigurationManager.AppSettings("SSVDOCID") & "&wpid=" & lblWPID.Text & "&siteno=" & lblSiteno.Text & "&from=ssvrc")
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        Dim dtStatus As New DataTable
        sqlstr = "exec [HCPT_uspTrans_GetRejectedDocs_NPO] " & CommonSite.UserId().ToString
        dtStatus = objUtil.ExeQueryDT(sqlstr, "DashBoardUploadDoc")

        GvRejectedDocuments.DataSource = dtStatus
        GvRejectedDocuments.DataBind()
    End Sub

    Public Sub ExportToExcel()
        Dim tw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()
        Dim strFilename As String = "DocRejectionLists_" + DateTime.Now.ToShortDateString + ".xls"
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
