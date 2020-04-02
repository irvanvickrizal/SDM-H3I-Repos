Imports Common
Imports BusinessLogic
Imports Entities
Imports System.Data
Partial Class frmCODSite
    Inherits System.Web.UI.Page
    Dim objdl As New BODDLs
    Dim objdo As New ETCODSite
    Dim objbo As New BOCODSite
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")
        If Page.IsPostBack = False Then
            objdl.fillDDL(DDLZN, "CODZone", True, Constants._DDL_Default_Select)
            objdl.fillDDL(DDLZone, "CODZone", True, Constants._DDL_Default_Select)
            BindData()
            txtNo.Disabled = False
            If (Session("Role_Id") <> 1) Then
                A1.Disabled = True
                A1.Attributes.Clear()
            End If
            If Not Request.QueryString("id") Is Nothing Then
                tdTitle.InnerText = ""
                rowadd.InnerText = "Edit"
                txtNo.Disabled = True
                btnSave.Text = "Update"
                binddetails()
            End If
        End If
    End Sub
    Sub BindData()
        dt = objbo.uspCODSiteList(, DDLZone.SelectedValue, ddlSelect.SelectedValue, txtSearch.Text, hdnSort.Value)
        grdSt.PageSize = Session("Page_size")
        grdSt.DataSource = dt
        grdSt.DataBind()
    End Sub
    Sub binddetails()
        dt = objbo.uspCODSiteList(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            txtNo.Value = dt.Rows(0).Item("Site_No").ToString
            txtName.Value = dt.Rows(0).Item("Site_Name").ToString
            txtSTDesc.Value = dt.Rows(0).Item("Site_Desc").ToString
            DDLZN.SelectedValue = dt.Rows(0).Item("ZN_ID").ToString
            txtSSName.Value = dt.Rows(0).Item("Name").ToString
            tblSite.Visible = True
            'bugfix110127 not allowed the user to delete the site will cause the document checklist to be missing
            btnDelete.Visible = False
        End If
    End Sub
    Protected Sub grdSt_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSt.PageIndexChanging
        grdSt.PageIndex = e.NewPageIndex
        BindData()
    End Sub
    Protected Sub grdSt_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSt.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = grdSt.PageIndex * grdSt.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
    Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
        tdTitle.InnerText = ""
        tblSite.Visible = True
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindData()
    End Sub
    Sub fillDetails()
        With objdo
            .Site_No = txtNo.Value.Replace("'", "''")
            .Site_Name = txtName.Value.Replace("'", "''")
            .Site_Desc = txtSTDesc.Value.Replace("'", "''")
            .Zn_ID = DDLZN.SelectedValue
            .AT.RStatus = Constants.STATUS_ACTIVE
            .AT.LMBY = Session("User_Name")
        End With
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        fillDetails()
        If Request.QueryString("id") Is Nothing Then
            BOcommon.result(objbo.uspCODSiteIU(objdo, hdnSupId.Value), True, "frmCodSite.aspx", "Site_Name", Constants._INSERT)
        Else
            objdo.Site_ID = Request.QueryString("id")
            Dim strStatus As Integer = 0
            strStatus = objbo.uspCODSiteIU(objdo, hdnSupId.Value)
            If strStatus <> -1 Then
                BOcommon.result(strStatus, True, "frmCodSite.aspx", "Site_Name", Constants._UPDATE)
            Else
                Response.Write("<script language='javascript'>alert('Supervisor already exist');</script>")
            End If
        End If
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        BOcommon.result(objbo.uspDelete("CODSite", "Site_ID", Request.QueryString("id")), True, "frmCODSite.aspx", " ", Constants._DELETE)
    End Sub
    Protected Sub btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.ServerClick
        Response.Redirect("frmCodSite.aspx")
    End Sub
    Protected Sub grdSite_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdSt.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        BindData()
    End Sub
    Protected Sub DDLZone_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLZone.SelectedIndexChanged
        BindData()
    End Sub
End Class
