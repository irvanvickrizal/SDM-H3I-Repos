Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class CR_frmUSRHelp
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objBo As New BOMOM
    Dim objdo As New ETMomUsers
    Dim retval As String
    Dim usrid As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Page_size") Is Nothing Then
            Response.Write("<script language=""JavaScript"">window.close();</script>")
        End If
        'Response.Redirect("~/SessionTimeout.aspx")
        If Page.IsPostBack = False Then
            bindData()
        End If
    End Sub
    Sub bindData()
        dt = objBo.uspGetUsers(Constants._UsrType, ddlSelect.SelectedValue, hdnsort.Value)
        grdUsers.DataSource = dt
        grdUsers.PageSize = Session("Page_size")
        grdUsers.DataBind()
    End Sub

    Protected Sub grdUsers_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdUsers.PageIndexChanging
        grdUsers.PageIndex = e.NewPageIndex
        bindData()
    End Sub

    Protected Sub grdUsers_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdUsers.Sorting
        If hdnsort.Value = e.SortExpression & " Desc" Or hdnsort.Value = "" Then
            hdnsort.Value = e.SortExpression & " Asc"
        Else
            hdnsort.Value = e.SortExpression & " Desc"
        End If
        bindData()
    End Sub

    Protected Sub btnAdd_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.ServerClick
        If grdUsers.Rows.Count > 0 Then
            Dim strId As String = ""
            Dim strName As String = ""
            For i As Integer = 0 To grdUsers.Rows.Count - 1
                Dim chk As HtmlInputCheckBox
                chk = CType(grdUsers.Rows(i).Cells(1).FindControl("EmpId"), HtmlInputCheckBox)
                If chk.Checked = True Then
                    strId = strId & IIf(strId <> "", ",", "") & chk.Value
                    strName = strName & IIf(strName <> "", ",", "") & grdUsers.Rows(i).Cells(2).Text
                End If
            Next
            If strId <> "" Then

                'Response.Write("<script>window.returnValue='" + retval + "';window.close();window.opener.location.reload();</script>")
                'Response.Write("<script>window.returnValue='" + retval + "';window.close();</script>")
                Response.Write("<script>window.opener.document.getElementById('hdnUsersId').value = '" + strId + "';window.opener.document.form1.submit();window.close();</script>")
                'Response.Write("<script>window.opener.document.form1.submit();window.close();</script>")
            Else

            End If
        End If
    End Sub
    Protected Sub ddlSelect_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSelect.SelectedIndexChanged
        bindData()
    End Sub

    Protected Sub btnClose_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.ServerClick
        Response.Write("<script language='javascript'>window.close();</script>")
    End Sub
End Class
