'Created By : Radha
'Date : 23-10-2008
'Updated By : Dedy
'Updated Date : 10-11-2008
Imports Common
Imports Entities
Imports BusinessLogic
Imports System.Data

Partial Class COD_frmCodZone
    Inherits System.Web.UI.Page
    Dim objdl As New BODDLs
    Dim objdo As New ETCODZone
    Dim objbo As New BOCODZone
    Dim dt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        BtnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        BtnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")
        If Page.IsPostBack = False Then
            objdl.fillDDL(DDLRgn, "CodRegion", True, Constants._DDL_Default_Select)
            objdl.fillDDL(ddlRegion, "CodRegion", True, Constants._DDL_Default_Select)
            binddata()
            If Not Request.QueryString("id") Is Nothing Then
                tdTitle.InnerText = ""
                rowadd.InnerText = "Edit"
                BtnSave.Text = "Update"
                BtnCreate.Enabled = False
                binddetails()
            End If
        End If
    End Sub

    Sub binddata()
        dt = objbo.uspCodZoneLD(, ddlRegion.SelectedValue, ddlSelect.SelectedValue, txtSearch.Text, hdnSort.Value)
        GrdZone.DataSource = dt
        GrdZone.PageSize = Session("Page_size")
        GrdZone.DataBind()
    End Sub

    Sub binddetails()
        dt = objbo.uspCodZoneLD(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            txtName.value = dt.Rows(0).Item("ZNName").ToString
            txtDesc.Value = dt.Rows(0).Item("ZNDesc").ToString
            DDLRgn.SelectedValue = dt.Rows(0).Item("RGN_ID")
            tblzone.Visible = True
            BtnDelete.Visible = True
        End If
    End Sub

    Protected Sub Grdzone_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdZone.PageIndexChanging
        GrdZone.PageIndex = e.NewPageIndex
        binddata()
    End Sub

    Sub filldetails()
        With objdo
            .ZNName = txtName.Value.Replace("'", "''")
            .ZNDesc = txtDesc.Value.Replace("'", "''")
            .RGN_ID = DDLRgn.SelectedValue
            .AT.RStatus = Constants.STATUS_ACTIVE
            .AT.LMBY = Session("User_Name")
        End With
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        filldetails()
        If Request.QueryString("id") Is Nothing Then
            BOcommon.result(objbo.uspCodZoneIU(objdo), True, "frmCodZone.aspx", "Zone", Constants._INSERT)
        Else
            objdo.ZN_ID = Request.QueryString("id")
            BOcommon.result(objbo.uspCodZoneIU(objdo), True, "frmCodZone.aspx", "Zone", Constants._UPDATE)
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        binddata()
    End Sub

    Protected Sub grdZone_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdZone.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (GrdZone.PageIndex * GrdZone.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub grdZone_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GrdZone.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        binddata()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        BOcommon.result(objbo.uspDelete("CODZone", "ZN_ID", Request.QueryString("id")), True, "frmCODZone.aspx", " ", Constants._DELETE)
    End Sub

    Protected Sub btnCreate_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCreate.Click
        tdTitle.InnerText = ""
        tblZone.Visible = True
    End Sub

    Protected Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("frmCodZone.aspx")
    End Sub

    Protected Sub DDLRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged
        binddata()
    End Sub
End Class


