Imports CRFramework
Imports System.IO

Partial Class CR_CRCORejection
    Inherits System.Web.UI.Page
    Dim controller As New CRController
    Dim co_controller As New COController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub



    Protected Sub BtnExportExcelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportExcel.Click
        If (GvListCRCORejection.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "DocumentRejection_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GvListCRCORejection)
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

    Protected Sub GvListCRCORejectionRowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvListCRCORejection.RowCommand
        If e.CommandName.Equals("generatecr") Then
            Dim att() As String = e.CommandArgument.ToString().Split("-")
            Response.Redirect("frmNewChangeRequest.aspx?id=" & att(0) & "&wpid=" & att(1))
        ElseIf e.CommandName.Equals("generateco") Then
            Dim att() As String = e.CommandArgument.ToString().Split("-")
            Response.Redirect("../CO/frmCO.aspx?wpid=" & att(1) & "&listtype=rejection")
        End If
    End Sub

    Protected Sub GvListCRCORejectionRowBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvListCRCORejection.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If GetTypeId().Equals("cr") Then
                e.Row.Cells(11).Visible = True
                e.Row.Cells(12).Visible = False
            Else
                e.Row.Cells(11).Visible = False
                e.Row.Cells(12).Visible = True
            End If
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        If GetTypeId().Equals("cr") Then
            GvListCRCORejection.DataSource = controller.GetCRRejection(Convert.ToInt32(CommonSite.UserId))
            GvListCRCORejection.DataBind()
        Else
            GvListCRCORejection.DataSource = co_controller.GetCORejection(Convert.ToInt32(CommonSite.UserId))
            GvListCRCORejection.DataBind()
        End If
        
    End Sub

    Private Function GetTypeId() As String
        Return Request.QueryString("type")
    End Function

#End Region
End Class
