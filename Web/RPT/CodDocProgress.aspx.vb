
Imports System.Data
Imports System.IO
Imports ClosedXML.Excel
Imports Common

Partial Class DashBoard_CodDocProgress
    Inherits System.Web.UI.Page

    Dim objutil As New DBUtil
    Dim strsql As String
    Dim dtDoc As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
            If Not IsPostBack Then
                strsql = "select 'All' 'Text', '' 'Value' union all select distinct PoNo 'Text', PoNo 'Value' from PODetails where PoNo <> '' and PoNo is not null order by 'Value'"
                dtDoc = objutil.ExeQueryDT(strsql, "poData")
                ddlPO.DataSource = dtDoc
                ddlPO.DataTextField = "Text"
                ddlPO.DataValueField = "Value"
                ddlPO.DataBind()
                bindDocProgress()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('" + ex.Message.Replace("'", "") + "');", True)
        End Try
    End Sub

    Private Sub grdDocuments_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdDocuments.RowCommand
        Try
            If e.CommandName = "ApprovedCount" Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                If index > -1 Then
                    Dim id As HiddenField = DirectCast(grdDocuments.Rows(index).FindControl("hiddenId"), HiddenField)
                    showModal(True, id.Value)
                End If
            ElseIf e.CommandName = "OnProgressCount" Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                If index > -1 Then
                    Dim id As HiddenField = DirectCast(grdDocuments.Rows(index).FindControl("hiddenId1"), HiddenField)
                    showModal(False, id.Value)
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('" + ex.Message.Replace("'", "") + "');", True)
        End Try
    End Sub

    Private Function getDocDetailProgress(ByVal isApproved As Boolean, ByVal docId As Integer) As DataTable
        strsql = "exec uspGetWorkflowProgress_Details " & Session("User_Id") & ", '" & ddlPO.SelectedValue & "'," & isApproved & "," & docId
        Return objutil.ExeQueryDT(strsql, "docProgressDetails")
    End Function

    Private Sub showModal(ByVal isApproved As Boolean, ByVal docId As Integer)
        Try
            grdDocDetail.DataSource = getDocDetailProgress(isApproved, docId)
            grdDocDetail.DataBind()
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "showModal();", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('" + ex.Message.Replace("'", "") + "');", True)
        End Try
    End Sub

    Protected Sub ddlPO_SelectedIndexChanged(sender As Object, e As EventArgs)
        bindDocProgress()
    End Sub

    Private Sub bindDocProgress()
        Try
            strsql = "exec uspGetWorkflowProgress " & Session("User_Id") & ", '" & ddlPO.SelectedValue & "'"
            dtDoc = objutil.ExeQueryDT(strsql, "docProgress")
            grdDocuments.DataSource = dtDoc
            grdDocuments.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs)
        strsql = "exec uspGetWorkflowProgress " & Session("User_Id") & ", '" & ddlPO.SelectedValue & "'"
        dtDoc = objutil.ExeQueryDT(strsql, "docProgress")
        Dim dtDocDup As New DataTable
        dtDocDup.Merge(dtDoc)

        dtDocDup.Columns.Remove("DOC_ID")
        dtDocDup.Columns.Remove("alias_docname")
        dtDocDup.Columns.Remove("SerialNo")
        Dim hFilename As String = "All Po's"

        If ddlPO.SelectedValue <> "" Then
            hFilename = ddlPO.SelectedValue
        End If

        Try
            Dim wb As New XLWorkbook
            Dim mStream As New MemoryStream

            wb.Worksheets.Add(dtDocDup, "Summarize_Progress")

            For i As Integer = 0 To grdDocuments.Rows.Count - 1
                Dim dtMerge As New DataTable
                dtMerge.Merge(getDocDetailProgress(True, CType(dtDoc.Rows(i)("DOC_ID"), Integer)))
                dtMerge.Merge(getDocDetailProgress(False, CType(dtDoc.Rows(i)("DOC_ID"), Integer)))

                If dtMerge.Rows.Count > 0 Then
                    wb.Worksheets.Add(dtMerge, CType(dtDoc.Rows(i)("alias_docname"), String))
                End If
            Next

            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=DocumentProgress_" & hFilename & "-" & DateTime.Now.ToString("ddMMyyyy HHmmss") & ".xlsx")
            wb.SaveAs(mStream)
            mStream.WriteTo(Response.OutputStream)
            Response.Flush()
            HttpContext.Current.Response.Flush()
            HttpContext.Current.Response.SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()

            wb.Dispose()
            mStream.Close()
            mStream.Dispose()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message.Replace("'", "") + "');", True)
        End Try
    End Sub

    Protected Sub grdDocuments_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim approvedLink As LinkButton = DirectCast(e.Row.FindControl("Approved_Document"), LinkButton)
            Dim onProgressLink As LinkButton = DirectCast(e.Row.FindControl("OnProgress_Document"), LinkButton)
            Dim lblApproved As Label = DirectCast(e.Row.FindControl("lblApprovedDoc"), Label)
            Dim lblOnProgress As Label = DirectCast(e.Row.FindControl("lblOnProgressDoc"), Label)

            If approvedLink.Text = "0" Then
                approvedLink.Visible = False
                lblApproved.Text = approvedLink.Text
                lblApproved.Visible = True
            Else
                approvedLink.Visible = True
                lblApproved.Visible = False
            End If

            If onProgressLink.Text = "0" Then
                onProgressLink.Visible = False
                lblOnProgress.Text = onProgressLink.Text
                lblOnProgress.Visible = True
            Else
                onProgressLink.Visible = True
                lblOnProgress.Visible = False
            End If
        End If
    End Sub
End Class
