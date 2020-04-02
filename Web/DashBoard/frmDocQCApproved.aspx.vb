Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data

Partial Class DashBoard_frmDocQCApproved
    Inherits System.Web.UI.Page
    Dim objDL As New BODDLs
    Dim dt As New DataTable
    Dim objBo As New BODashBoard
    Dim objutil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then

            grddocuments.Columns(9).Visible = True
            grddocuments.Columns(10).Visible = True
            grddocuments.Columns(11).Visible = True

            dt = objutil.ExeQueryDT("exec uspSiteDocListTaskNewSp " & CommonSite.UserId() & ",'" & hdnSort.Value & "','" & ConfigurationManager.AppSettings("APPTASKID") & "','" & Request.QueryString("Id") & "','" & Request.QueryString("wpid") & "'", "ser")
            grddocuments.DataSource = dt
            grddocuments.DataBind()
            
            '--

            'grddocuments2.Columns(9).Visible = True
            'grddocuments2.Columns(10).Visible = True
            'grddocuments2.Columns(11).Visible = True

            dt = objutil.ExeQueryDT("exec [uspSiteDocListReviewerTaskNewSp] " & CommonSite.UserId() & ",'" & ConfigurationManager.AppSettings("REVTASKID") & "','" & Request.QueryString("Id") & "','" & Request.QueryString("wpid") & "'", "GetRecord")
            
            'grddocuments2.Columns(9).Visible = False
            'grddocuments2.Columns(10).Visible = False
            'grddocuments2.Columns(11).Visible = False
            'grddocuments2.Columns(13).Visible = False

            'If (CommonSite.RollId = (ConfigurationManager.AppSettings("ATPROLEAPPREV")) Or (CommonSite.RollId = (ConfigurationManager.AppSettings("QCRevTsel")))) Then
            '    grddocuments2.Columns(7).Visible = False
            '    grddocuments2.Columns(8).Visible = False
            '    grddocuments2.Columns(13).Visible = True
            'ElseIf CommonSite.RollId = (ConfigurationManager.AppSettings("ATPROLEAPP")) Then
            '    If Request.QueryString("doctype") IsNot Nothing Then
            '        If Request.QueryString("doctype") = "atp" Then
            '            grddocuments2.Columns(7).Visible = False
            '            grddocuments2.Columns(8).Visible = False
            '            grddocuments2.Columns(13).Visible = True
            '        End If
            '    End If
            'End If

            grddocuments2.DataSource = dt
            grddocuments2.DataBind()
            '--

            'grddocuments3.Columns(7).Visible = True
            'grddocuments3.Columns(8).Visible = True
            'grddocuments3.Columns(9).Visible = True

            dt = objutil.ExeQueryDT("exec uspSiteDocListApproved " & CommonSite.UserId() & ",'" & hdnSort.Value & "','" & ConfigurationManager.AppSettings("APPTASKID") & "','" & Request.QueryString("Id") & "','" & Request.QueryString("wpid") & "'", "ser")
            grddocuments3.DataSource = dt
            grddocuments3.DataBind()
            'grddocuments3.Columns(7).Visible = False
            'grddocuments3.Columns(8).Visible = False
            'grddocuments3.Columns(9).Visible = False
            '--
            If grddocuments.Rows.Count = 0 And grddocuments2.Rows.Count = 0 Then
                'Response.Redirect("../DashBoard/frmSiteDocCount.aspx")
                Dim returnpage As String = "defpage"
                If Request.QueryString("doctype") IsNot Nothing Then
                    If Request.QueryString("doctype") = "qcol" Then
                        returnpage = "qcpage"
                    End If
                End If
                If (returnpage = "qcpage") Then
                    Response.Redirect("../DashBoard/frmSiteDocCount_QC.aspx")
                Else
                    Response.Redirect("../DashBoard/frmSiteDocCount.aspx")
                End If
            End If
        End If
    End Sub

    Protected Sub grddocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim url As String = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(7).Text
            If url.Length > 14 Then
                e.Row.Cells(4).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
            Else
                e.Row.Cells(4).Text = e.Row.Cells(3).Text
            End If
        End If
    End Sub
    Protected Sub grddocuments_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
        ElseIf e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
        End If
    End Sub
    Protected Sub grddocuments2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments2.RowDataBound
        Dim strSwId As String = String.Empty
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grddocuments2.PageIndex * grddocuments2.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
                Dim lblSWId As Label = CType(e.Row.FindControl("LblSWId"), Label)
                strSwId = lblSWId.Text
        End Select
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim url As String = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(9).Text
            Dim url As String = "../PO/frmViewDocument.aspx?id=" & strSwId
            If url.Length > 14 Then
                e.Row.Cells(4).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
            Else
                e.Row.Cells(4).Text = e.Row.Cells(3).Text
            End If
        End If
    End Sub
    Protected Sub grddocuments3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments3.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grddocuments2.PageIndex * grddocuments2.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim url As String = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(7).Text
            If url.Length > 14 Then
                e.Row.Cells(4).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
            Else
                e.Row.Cells(4).Text = e.Row.Cells(3).Text
            End If
        End If
    End Sub
    Protected Sub grddocuments2_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments2.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
        ElseIf e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
        End If
    End Sub
    Protected Sub grddocuments3_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments3.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
        ElseIf e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
        End If
    End Sub

End Class
