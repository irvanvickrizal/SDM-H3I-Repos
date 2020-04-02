Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class frmDocApprove
    Inherits System.Web.UI.Page
    Dim objDL As New BODDLs
    Dim dt As New DataTable
    Dim objBo As New BODashBoard
    Dim objutil As New DBUtil
    Dim delegatecontroller As New DelegationController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            'pnlWarningDelegationUserApprover.Visible = False       'Commented by Fauzan, 8 Nov. Change to bootstrap template
            'Modified by Fauzan, 9 Nov. Move the process into a function
            bindApprovalDocument()
            binReviewDocument()
            bindApprovedDocument()
            'End Fauzan
            If grddocuments.Rows.Count = 0 And grddocuments2.Rows.Count = 0 Then
                'Response.Redirect("../DashBoard/frmSiteDocCount.aspx")
                Dim returnpage As String = "defpage"
                If Request.QueryString("doctype") IsNot Nothing Then
                    If Request.QueryString("doctype") = "atp" Then
                        returnpage = "atppage"
                    End If
                End If
                If (returnpage = "atppage") Then
                    Response.Redirect("../DashBoard/frmSiteDocCount_ATP.aspx")
                Else
                    Response.Redirect("../DashBoard/frmSiteDocCount.aspx")
                End If
            End If
        End If
    End Sub

    Public Sub doapp(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim docid As Integer
        Dim tbtn As Button = CType(sender, Button)
        Dim row As GridViewRow = CType(tbtn.NamingContainer, GridViewRow)
        Dim i As Integer = row.RowIndex
        docid = grddocuments.Rows(i).Cells(1).Text.ToString
    End Sub

    Public Sub dorej(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim docid As Integer
        Dim tbtn As Button = CType(sender, Button)
        Dim row As GridViewRow = CType(tbtn.NamingContainer, GridViewRow)
        Dim i As Integer = row.RowIndex
        docid = grddocuments.Rows(i).Cells(1).Text.ToString
        Response.Write(docid)
    End Sub

    Protected Sub grddocuments_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grddocuments.PageIndexChanging
        grddocuments.PageIndex = e.NewPageIndex
        bindApprovalDocument()      'Added by Fauzan, 9 Nov 2018. Retviev data for paging
    End Sub

    Protected Sub grddocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grddocuments.PageIndex * grddocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim url As String = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(9).Text
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

    Protected Sub grddocuments_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grddocuments.RowDeleting
    End Sub

    Protected Sub BtnClose_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClose.ServerClick
        Response.Redirect("../DashBoard/frmSiteDocCount.aspx")
    End Sub

    Public Sub doapp2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim docid As Integer
        Dim tbtn As Button = CType(sender, Button)
        Dim row As GridViewRow = CType(tbtn.NamingContainer, GridViewRow)
        Dim i As Integer = row.RowIndex
        docid = grddocuments2.Rows(i).Cells(1).Text.ToString
    End Sub

    Public Sub dorej2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim docid As Integer
        Dim tbtn As Button = CType(sender, Button)
        Dim row As GridViewRow = CType(tbtn.NamingContainer, GridViewRow)
        Dim i As Integer = row.RowIndex
        docid = grddocuments2.Rows(i).Cells(1).Text.ToString
        Response.Write(docid)
    End Sub

    Protected Sub grddocuments2_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grddocuments2.PageIndexChanging
        grddocuments2.PageIndex = e.NewPageIndex
        binReviewDocument()     'Added by Fauzan, 9 Nov 2018. Retviev data for paging
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

    ''' <summary>
    ''' Added by Fauzan, 9 Nov 2018. Create a function to retrieve approval document.
    ''' </summary>
    Sub bindApprovalDocument()
        dt = New DataTable()
        'dt = objBo.uspSiteDocListTask(CommonSite.UserId(), taskid, hdnSort.Value)
        dt = objutil.ExeQueryDT("exec HCPT_uspSiteDocListTaskNewSp " & CommonSite.UserId() & ",'" & Request.QueryString("wpid") & "','" & "Approver'", "ser")
        grddocuments.Columns(9).Visible = True
        grddocuments.Columns(10).Visible = True
        grddocuments.Columns(11).Visible = True
        grddocuments.DataSource = dt
        grddocuments.DataBind()
        grddocuments.Columns(7).Visible = False
        grddocuments.Columns(8).Visible = False
        grddocuments.Columns(9).Visible = False
        grddocuments.Columns(10).Visible = False
        grddocuments.Columns(13).Visible = True

        'If delegatecontroller.IsInDelegation(CommonSite.UserId) = True Then
        '    pnlWarningDelegationUserApprover.Visible = True
        '    grddocuments.Columns(7).Visible = False
        '    grddocuments.Columns(8).Visible = False
        'End If
    End Sub

    ''' <summary>
    ''' Added by Fauzan, 9 Nov 2018. Create a function to retrieve review document.
    ''' </summary>
    Sub binReviewDocument()
        dt = New DataTable()
        dt = objutil.ExeQueryDT("exec HCPT_uspSiteDocListTaskNewSp " & CommonSite.UserId() & ",'" & Request.QueryString("wpid") & "','" & "Reviewer'", "getrevtask")
        grddocuments2.Columns(9).Visible = True
        grddocuments2.Columns(10).Visible = True
        grddocuments2.Columns(11).Visible = True
        If (dt.Rows.Count > 0) Then
            If Request.QueryString("doctype") IsNot Nothing Then
                If Request.QueryString("doctype") = "atp" Or CommonSite.RollId = (ConfigurationManager.AppSettings("ATPROLEAPPREV")) Then
                    Dim docid As Integer = dt.Rows(0).Item("Doc_Id")
                    If (docid = 2001) Then
                        ''PnlDocumentHasBeenApproved.Visible = False
                    End If
                End If
            End If
        End If
        grddocuments2.Columns(9).Visible = False
        grddocuments2.Columns(10).Visible = False
        grddocuments2.Columns(11).Visible = False
        grddocuments2.Columns(13).Visible = False
        grddocuments2.Columns(7).Visible = False
        grddocuments2.Columns(8).Visible = False
        grddocuments2.Columns(13).Visible = True

        grddocuments2.DataSource = dt
        grddocuments2.DataBind()
        '--
        If delegatecontroller.IsInDelegation(CommonSite.UserId) = True Then
            grddocuments2.Columns(7).Visible = False
            grddocuments2.Columns(8).Visible = False
            grddocuments2.Columns(9).Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Added by Fauzan, 9 Nov 2018. Create a function to retrieve document that has been approved.
    ''' </summary>
    Sub bindApprovedDocument()
        dt = New DataTable()
        dt = objutil.ExeQueryDT("exec uspSiteDocListApproved " & CommonSite.UserId() & ",'" & hdnSort.Value & "','" & ConfigurationManager.AppSettings("APPTASKID") & "','" & Request.QueryString("Id") & "','" & Request.QueryString("wpid") & "'", "ser")
        grddocuments3.Columns(7).Visible = True
        grddocuments3.Columns(8).Visible = True
        grddocuments3.Columns(9).Visible = True
        grddocuments3.DataSource = dt
        grddocuments3.DataBind()
        grddocuments3.Columns(7).Visible = False
        grddocuments3.Columns(8).Visible = False
        grddocuments3.Columns(9).Visible = False
    End Sub
End Class
