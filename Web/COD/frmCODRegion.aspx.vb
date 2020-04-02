Imports Common
Imports Entities
Imports System.Data
Imports BusinessLogic
Partial Class frmCODRegion
    Inherits System.Web.UI.Page
    Dim objdl As New BODDLs
    Dim objdo As New ETCODRegion
    Dim objbo As New BOCODRegion
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")
        If Page.IsPostBack = False Then
            objdl.fillDDL(DDLArea, "CodArea", True, Constants._DDL_Default_Select)
            objdl.fillDDL(ddlARA, "CodArea", True, Constants._DDL_Default_Select)
            binddata()
            If Not Request.QueryString("id") Is Nothing Then
                tdTitle.InnerText = ""
                rowadd.InnerText = "Edit"
                btnSave.Text = "Update"
                btnNewGroup.Enabled = False
                binddetails()
            End If
        End If
    End Sub
    Sub binddata()
        dt = objbo.uspCODRegionList(, DDLArea.SelectedValue, ddlSelect.SelectedValue, txtSearch.Text, hdnSort.Value)
        grdRGN.DataSource = dt
        grdRGN.PageSize = Session("Page_size")
        grdRGN.DataBind()
    End Sub
    Sub binddetails()
        dt = objbo.uspCODRegionList(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            txtRGNName.Value = dt.Rows(0).Item("RGNName").ToString
            txtRGNDesc.Value = dt.Rows(0).Item("RGNDesc").ToString
            DDLARA.SelectedValue = dt.Rows(0).Item("ARA_ID")
            tblRegion.Visible = True
            'bugfix110221 not allowed to delete regions will cause process flow not stable
            btnDelete.Visible = False
        End If
    End Sub
    Protected Sub GrdState_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdRGN.PageIndexChanging
        grdRGN.PageIndex = e.NewPageIndex
        binddata()
    End Sub
    Sub filldetails()
        With objdo
            .RGNName = txtRGNName.Value.Replace("'", "''")
            .RGNDesc = txtRGNDesc.Value.Replace("'", "''")
            .ARA_ID = DDLARA.SelectedValue
            .AT.RStatus = Constants.STATUS_ACTIVE
            .AT.LMBY = Session("User_Name")
        End With
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        filldetails()
        If Request.QueryString("id") Is Nothing Then
            BOcommon.result(objbo.uspCODRegionIU(objdo), True, "frmCodRegion.aspx", "Region Name", Constants._INSERT)
        Else
            objdo.RGN_ID = Request.QueryString("id")
            BOcommon.result(objbo.uspCODRegionIU(objdo), True, "frmCodRegion.aspx", "Region Name", Constants._UPDATE)
        End If
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        binddata()
    End Sub
    Protected Sub grdRGN_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdRGN.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdRGN.PageIndex * grdRGN.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"

        End Select
    End Sub
    Protected Sub grdState_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdRGN.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        binddata()
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        BOcommon.result(objbo.uspDelete("CODRegion", "RGN_ID", Request.QueryString("id")), True, "frmCODRegion.aspx", " ", Constants._DELETE)
    End Sub
    Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.Click
        tdTitle.InnerText = ""
        tblRegion.Visible = True
    End Sub
    Protected Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Response.Redirect("frmCodRegion.aspx")
    End Sub
    Protected Sub DDLArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLArea.SelectedIndexChanged
        binddata()
    End Sub
End Class