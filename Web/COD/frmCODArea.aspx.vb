'Created By : radha
'date : 09-10-2008
Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data
Partial Class frmCODArea
    Inherits System.Web.UI.Page
    Dim objdo As New ETCODArea
    Dim objbo As New BOCODArea
    Dim objdl As New BODDLs
    Dim dt As New DataTable
    Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
        tblArea.Visible = True
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~/SessionTimeout.aspx")
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")
        If Page.IsPostBack = False Then
            objdl.fillDDL(DDLJava, "CODJAVA", True, Constants._DDL_Default_Select)
            objdl.fillDDL(DDLJVA, "CODJAVA", True, Constants._DDL_Default_Select)
            binddata()
            If Not Request.QueryString("id") Is Nothing Then
                tdTitle.InnerText = ""
                addrow.InnerText = "Edit"
                btnSave.Text = "Update"
                btnNewGroup.Disabled = True
                Binddetails()
            End If
        End If
    End Sub
    Sub binddata()
        dt = objbo.uspCODareaList(, DDLJVA.SelectedValue, ddlSelect.SelectedValue, txtSearch.Text, hdnSort.Value)
        grdArea.DataSource = dt
        grdArea.PageSize = Session("Page_size")
        grdArea.DataBind()
    End Sub
    Sub Binddetails()
        dt = objbo.uspCODareaList(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            txtARAName.Value = dt.Rows(0).Item("ARA_Name").ToString
            txtDetails.InnerText = dt.Rows(0).Item("ARA_Desc").ToString
            DDLJava.SelectedValue = dt.Rows(0).Item("JV_ID")
            tblArea.Visible = True
            btnDelete.Visible = True
        End If
    End Sub

    Protected Sub grdArea_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdArea.PageIndexChanging
        grdArea.PageIndex = e.NewPageIndex
        binddata()
    End Sub
    Sub filldetails()
        objdo.AT.RStatus = Constants.STATUS_ACTIVE
        objdo.AT.LMBY = Session("User_Name")
        objdo.ARAName = txtARAName.Value
        objdo.ARADetails = txtDetails.Value
        objdo.JV_Id = DDLJava.SelectedValue
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        filldetails()
        If Request.QueryString("id") Is Nothing Then
            BOcommon.result(objbo.uspCODAreaIU(objdo), True, "frmCODArea.aspx", "Area Name", Constants._INSERT)
        Else
            objdo.ARA_ID = Request.QueryString("id")
            BOcommon.result(objbo.uspCODAreaIU(objdo), True, "frmCODArea.aspx", "Area Name", Constants._UPDATE)
        End If
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        binddata()
    End Sub
    Protected Sub grdArea_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdArea.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdArea.PageIndex * grdArea.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"

        End Select
    End Sub
    Protected Sub grdArea_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdArea.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        binddata()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        BOcommon.result(objbo.uspDelete("CODArea", "ARA_ID", Request.QueryString("id")), True, "frmCODArea.aspx", " ", Constants._DELETE)
    End Sub
    
    Protected Sub DDLJVA_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLJVA.SelectedIndexChanged
        binddata()
    End Sub

    Protected Sub btnCanel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanel.ServerClick
        Response.Redirect("frmCodArea.aspx")
    End Sub
    Protected Sub btnNewGroup_ServerClick1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
        tdTitle.InnerText = ""
        tblArea.Visible = True
    End Sub
End Class
