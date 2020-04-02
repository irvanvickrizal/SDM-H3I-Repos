Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data

Partial Class USR_frmChangeSupervisor
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim dt As New DataTable
    Dim objBO As New BOUserSetup

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnSave.Attributes.Add("onClick", "javascript:return checkIsEmpty();")
        If Not IsPostBack Then
            objBOD.fillDDL(ddlUsertype, "TUserType1", False, "")
            If Request.QueryString("SelMode") = "N" Then
                ddlUsertype.SelectedValue = Constants.User_Type_NSN
                objBOD.fillDDL(ddlCurSup, "SupervisorsL", Constants.NSN_SS_RoleID, True, Constants._DDL_Default_Select)
            ElseIf Request.QueryString("SelMode") = "S" Then
                ddlUsertype.SelectedValue = Constants.User_Type_SubCon
                objBOD.fillDDL(ddlCurSup, "SupervisorsL", Constants.Subcon_SS_RoleID, True, Constants._DDL_Default_Select)
            ElseIf Request.QueryString("SelMode") = "C" Then
                ddlUsertype.SelectedValue = Constants.User_Type_Customer
                objBOD.fillDDL(ddlCurSup, "SupervisorsL", Constants.Customer_SS_RoleID, True, Constants._DDL_Default_Select)
            End If
        End If
    End Sub

    Protected Sub ddlCurSup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCurSup.SelectedIndexChanged
        ddlNewSup.Items.Clear()
        If ddlCurSup.SelectedIndex > 0 Then
            If ddlUsertype.SelectedValue = Constants.User_Type_NSN Then
                objBOD.fillDDL(ddlNewSup, "SupervisorsL", Constants.NSN_SS_RoleID, ddlCurSup.SelectedValue, True, Constants._DDL_Default_Select)
            ElseIf ddlUsertype.SelectedValue = Constants.User_Type_SubCon Then
                objBOD.fillDDL(ddlNewSup, "SupervisorsL", Constants.Subcon_SS_RoleID, ddlCurSup.SelectedValue, True, Constants._DDL_Default_Select)
            ElseIf ddlUsertype.SelectedValue = Constants.User_Type_Customer Then
                objBOD.fillDDL(ddlNewSup, "SupervisorsL", Constants.Customer_SS_RoleID, ddlCurSup.SelectedValue, True, Constants._DDL_Default_Select)
            End If
        End If
        GetList()
    End Sub

    Protected Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Response.Redirect("frmChangeSupervisor.aspx?SelMode=" & Request.QueryString("SelMode"))
    End Sub

    Sub GetList()
        dt = objBO.uspEBASTUserRoleL(ddlCurSup.SelectedValue)
        grdRoleList.PageSize = Session("Page_size")
        grdRoleList.DataSource = dt
        grdRoleList.DataBind()
    End Sub

    Protected Sub grdRoleList_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdRoleList.PageIndexChanging
        grdRoleList.PageIndex = e.NewPageIndex
        GetList()
    End Sub

    Protected Sub grdRoleList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdRoleList.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = grdRoleList.PageIndex * grdRoleList.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        BOcommon.result(objBO.uspChangeSupervisor(ddlCurSup.SelectedValue, ddlNewSup.SelectedValue), True, "frmChangeSupervisor.aspx", "", Constants._UPDATE)
    End Sub
End Class
