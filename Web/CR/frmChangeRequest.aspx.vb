Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports BusinessLogic
Imports Common
Imports Entities
Imports System.IO


Partial Class frmChangeRequest
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objBL As New BOMOMTEST
    Dim objBLL As New BOMOM
    Dim objDL As New BODDLs
    Private objEMOMD As New ETMOMDetails

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("Page_size") Is Nothing Then Response.Redirect("~/SessionTimeout.aspx")
            'objBL.UsersList(ddlMomRefNo, "MOMHEAD", True, Constants._DDL_Default_Select)
            ddlStatus_SelectedIndexChanged(Nothing, Nothing)
            ' BindData()
            'BindGrid()
            refNo.Visible = False
        End If
    End Sub

    Sub BindGrid()
        dt = objBLL.uspMOMList(0, ddlselect.SelectedValue, txtSearch.Text.Trim(), hdnSort.Value, ddlStatus.SelectedValue)
        GrdMOM.PageIndex = Session("Page_size")
        GrdMOM.DataSource = dt
        GrdMOM.DataBind()
        If GrdMOM.Rows.Count > 0 And ddlStatus.SelectedIndex = 0 Then
            generateCR.Visible = False
            'GrdMOM.Columns(1).Visible = True
            GrdMOM.Columns(2).Visible = False
            GrdMOM.Columns(3).Visible = False
            GrdMOM.Columns(4).Visible = False
            'Else
            '    generateCR.Visible = False
            '    'GrdMOM.Columns(1).Visible = True
        End If
    End Sub

    Protected Sub BtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        'If txtSearch.Text.Trim() <> "" Then
        '    dt = objBL.uspMOMListH(ddlselect.SelectedValue, txtSearch.Text.Trim(), 0, hdnSort.Value)
        'End If

        'GrdPo.DataSource = dt
        'GrdPo.DataBind()
        ''BindData()
        BindGrid()
    End Sub

    
    Protected Sub generateCR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles generateCR.Click
        If GrdMOM.Rows.Count > 0 Then
            Dim j As Integer = objBL.uspUpdateMomDetails(Session("MOMID"), "CR")
        Else
            Response.Write("<script language='javascript'>alert('MOM should not be Empty');</script>")
        End If
        BindGrid()
    End Sub

    Protected Sub ddlMomRefNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMomRefNo.SelectedIndexChanged
        'If ddlStatus.SelectedIndex = 0 And ddlMomRefNo.SelectedValue > 0 Then
        '    generateCR.Visible = True
        'Else
        '    generateCR.Visible = False
        'End If
        'If ddlMomRefNo.SelectedValue > 0 Then
        '    BindGrid()
        'End If
        If ddlStatus.SelectedItem.Value = "P" Then
            generateCR.Visible = False
            GrdMOM.Columns(1).Visible = False
            GrdMOM.Columns(2).Visible = True
            GrdMOM.Columns(8).Visible = True
            GrdMOM.Columns(3).Visible = False
            GrdMOM.Columns(4).Visible = False
            GrdMOM.Columns(9).Visible = False
            GrdMOM.Columns(10).Visible = False
        End If
        If ddlStatus.SelectedItem.Value = "C" Then
            generateCR.Visible = False
            GrdMOM.Columns(1).Visible = False
            GrdMOM.Columns(2).Visible = False
            GrdMOM.Columns(3).Visible = True
            GrdMOM.Columns(4).Visible = False
            GrdMOM.Columns(9).Visible = False
            GrdMOM.Columns(10).Visible = False
        End If
        If ddlStatus.SelectedItem.Value = "I" Then
            generateCR.Visible = False
            GrdMOM.Columns(1).Visible = True
            GrdMOM.Columns(2).Visible = False
            GrdMOM.Columns(3).Visible = False
            GrdMOM.Columns(4).Visible = False
            GrdMOM.Columns(9).Visible = False
            GrdMOM.Columns(10).Visible = False
        End If
        If Request.QueryString("Type") = "L" Then
            Session("MOMID") = Request.QueryString("MId")
            ddlStatus.SelectedIndex = 3
            generateCR.Visible = False
            btnSndApproval.Visible = True
            GrdMOM.Columns(1).Visible = True
            GrdMOM.Columns(2).Visible = False
            GrdMOM.Columns(3).Visible = False
            GrdMOM.Columns(4).Visible = False
            GrdMOM.Columns(9).Visible = False
            GrdMOM.Columns(10).Visible = False
        End If
        If Request.QueryString("Type") = "SA" Then
            ddlStatus.SelectedIndex = 5
            generateCR.Visible = True
            btnSndApproval.Visible = False
            GrdMOM.Columns(1).Visible = True
            GrdMOM.Columns(2).Visible = False
            GrdMOM.Columns(3).Visible = False
            GrdMOM.Columns(4).Visible = False
            GrdMOM.Columns(9).Visible = False
            GrdMOM.Columns(10).Visible = False
        End If
        If Request.QueryString("Type") = "H" Then
            generateCR.Visible = False
            ddlStatus.SelectedIndex = 2
            'Session("MOMID") = Request.QueryString("MId")
            GrdMOM.Columns(1).Visible = False
            GrdMOM.Columns(2).Visible = False
            GrdMOM.Columns(3).Visible = True
            GrdMOM.Columns(4).Visible = False
            GrdMOM.Columns(9).Visible = False
            GrdMOM.Columns(10).Visible = False
        End If
        If ddlStatus.SelectedItem.Value = "A" Then
            generateCR.Visible = False
            btnSndApproval.Visible = False
            GrdMOM.Columns(1).Visible = False
            GrdMOM.Columns(2).Visible = False
            GrdMOM.Columns(3).Visible = False
            GrdMOM.Columns(4).Visible = True
            GrdMOM.Columns(9).Visible = False
            GrdMOM.Columns(10).Visible = False
        End If
        If ddlStatus.SelectedItem.Value = "R" Then
            generateCR.Visible = True
            btnSndApproval.Visible = False
            GrdMOM.Columns(1).Visible = True
            GrdMOM.Columns(2).Visible = False
            GrdMOM.Columns(3).Visible = False
            GrdMOM.Columns(4).Visible = False
            GrdMOM.Columns(9).Visible = False
            GrdMOM.Columns(10).Visible = False
        End If
        BindGrid()
    End Sub
    Protected Sub ddlStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStatus.SelectedIndexChanged
        'objBL.UsersList(ddlMomRefNo, "MOMHEAD", True, Constants._DDL_Default_Select)
        ddlMomRefNo.Items.Clear()
        objDL.fillDDL(ddlMomRefNo, "MOMHead", ddlStatus.SelectedValue, False, Constants._DDL_Default_Select)       
        ddlMomRefNo_SelectedIndexChanged(Nothing, Nothing)
        
    End Sub

    Protected Sub GrdMOM_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdMOM.PageIndexChanging
        GrdMOM.PageIndex = e.NewPageIndex()
        BindGrid()
    End Sub

    Protected Sub GrdMOM_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdMOM.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
            lbl.Text = (GrdMOM.PageIndex * GrdMOM.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End If
    End Sub

    Protected Sub GrdMOM_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GrdMOM.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        BindGrid()
    End Sub    
    'Public Sub Generate(ByVal sender As Object, ByVal e As System.EventArgs)

    '    'Response.Write("<script javascript:return confirm('Are you sure you want to delete');</script>")
    '    'Dim momid As Integer = DirectCast(GrdMOM.Rows(0)(0).FindControl("hdnMomId"), HiddenField)
    '    'Dim momid As Integer = Me.GrdMOM.SelectedRow.Cells(8).FindControl("hdnMomId").ToString()
    '    'Dim momid As Integer = CType(GrdMOM.Rows(0).Cells(8).FindControl("hdnMomId"), HiddenField).Value()


    '    GrdMOM.Columns(1).Visible = False
    '    GrdMOM.Columns(2).Visible = True
    '    GrdMOM.Columns(3).Visible = False
    '    GrdMOM.Columns(7).Visible = False
    '    GrdMOM.Columns(8).Visible = False
    '    ddlStatus.SelectedIndex = 1




    '    BindGrid()
    'End Sub
    'Sub Update()

    '    GrdMOM.DataKeys(
    '   
    'End Sub

    Protected Sub GrdMOM_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GrdMOM.RowCommand
        If e.CommandName.ToLower() = "edt" Then
            Dim lb As LinkButton = CType(e.CommandSource, LinkButton)
            Dim gvRow As GridViewRow = lb.BindingContainer 'Getting current row to get index
            Dim strKey As String = GrdMOM.DataKeys(gvRow.RowIndex)(0).ToString()
            Dim i As Integer = objBL.uspUpdateMomDetails(strKey, "P")
            ddlStatus.SelectedIndex = 1
            generateCR.Visible = False
            GrdMOM.Columns(1).Visible = False
            GrdMOM.Columns(2).Visible = True
            GrdMOM.Columns(3).Visible = False
            GrdMOM.Columns(4).Visible = False
            GrdMOM.Columns(8).Visible = True
            GrdMOM.Columns(9).Visible = False
            GrdMOM.Columns(10).Visible = False

            BindGrid()
        End If
    End Sub

    Protected Sub btnSndApproval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSndApproval.Click
        ddlStatus.SelectedIndex = 4
        btnSndApproval.Visible = False
        Dim i As Integer = objBL.uspUpdateMomDetails(Session("MOMID"), "A")
        GrdMOM.Columns(1).Visible = False
        GrdMOM.Columns(2).Visible = False
        GrdMOM.Columns(3).Visible = False
        GrdMOM.Columns(4).Visible = True
        GrdMOM.Columns(8).Visible = True
        GrdMOM.Columns(9).Visible = False
        GrdMOM.Columns(10).Visible = False
        BindGrid()
    End Sub
End Class
