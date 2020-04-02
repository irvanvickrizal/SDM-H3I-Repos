Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data

Partial Class USR_frmUserList
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim objBODP As New BODDLs
    Dim objET As New ETEBASTUsers
    Dim objBO As New BOUserLD
    Dim dt As New DataTable
    Dim strQuery As String
    Dim objutil As New DBUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        If Not IsPostBack Then
            objBODP.fillDDL(ddlUsertype, "TUserType1", True, Constants._DDL_Default_Select)
            objBOD.fillDDL(ddlRole, "TRole", True, Constants._DDL_Default_Select)
            If Session("User_Type") = Constants.User_Type_NSN Then
                'ddlUsertype.SelectedValue = Session("User_Type")
                lblsrcname.Visible = False
            ElseIf Session("User_Type") = Constants.User_Type_SubCon Then
                ddlUsertype.SelectedValue = Session("User_Type")
                ddlUsertype.Enabled = False
                lblsrcname.Visible = True
                objBOD.fillDDL(ddlSrcname, "SubCon", True, Constants._DDL_Default_Select)
                ddlSrcname.SelectedValue = Session("SRCId").ToString
                ddlSrcname.Enabled = False
            ElseIf Session("User_Type") = Constants.User_Type_Customer Then
                ddlUsertype.SelectedValue = Session("User_Type")
                ddlUsertype.Enabled = False
                lblsrcname.Visible = True
                objBOD.fillDDL(ddlSrcname, "Customer", True, Constants._DDL_Default_Select)
                ddlSrcname.SelectedValue = Session("SRCId").ToString
                ddlSrcname.Enabled = False
            End If
            If Request.QueryString("SelMode") = "True" Then
                ddlUsertype.SelectedValue = Session("User_Type")
                ddlRole.SelectedValue = Constants.NSN_SS_RoleID
                ddlUsertype.Enabled = False
                ddlRole.Enabled = False
            End If
            BindData()
            If Request.QueryString("id") > 0 And Request.QueryString("SelMode") = "True" Then
                Dim retVal As String = Request.QueryString("id") & "####" & Request.QueryString("SS") & "####" & ddlUsertype.SelectedValue
                Response.Write("<script>window.returnValue = '" + retVal + "';window.close();</script>")
            End If
        End If
    End Sub
    Protected Sub ddlUsertype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUsertype.SelectedIndexChanged
        If ddlUsertype.SelectedValue = Constants.User_Type_SubCon Or ddlUsertype.SelectedValue = Constants.User_Type_Customer Then
            lblsrcname.Visible = True
            If ddlUsertype.SelectedValue = Constants.User_Type_SubCon Then
                objBOD.fillDDL(ddlSrcname, "SubCon", True, Constants._DDL_Default_Select)
            ElseIf ddlUsertype.SelectedValue = Constants.User_Type_Customer Then
                objBOD.fillDDL(ddlSrcname, "Customer", True, Constants._DDL_Default_Select)
            End If
        Else
            lblsrcname.Visible = False
            ddlRole.SelectedIndex = 0
        End If
        objBOD.fillDDL(ddlRole, "TRole1", ddlUsertype.SelectedValue, True, Constants._DDL_Default_Select)
        ddlRole.SelectedIndex = 0
        BindData()
    End Sub
    Sub BindData()
        Dim src As Integer
        If ddlSrcname.SelectedValue = "" Or ddlUsertype.SelectedValue = Constants.User_Type_NSN Then
            src = 0
        Else
            src = ddlSrcname.SelectedValue
        End If
        dt = objBO.uspEBASTUsersLD1(0, ddlUsertype.SelectedValue, ddlSelect.SelectedValue, txtSearch.Value, hdnSort.Value, Session("User_Id"), src, ddlRole.SelectedValue)
        grdUserlist.PageSize = Session("Page_size")
        grdUserlist.DataSource = dt
        grdUserlist.DataBind()
        If Request.QueryString("Type") = "U1" Then
            grdUserlist.Columns(1).Visible = True
            grdUserlist.Columns(2).Visible = False
            grdUserlist.Columns(3).Visible = False
        ElseIf Request.QueryString("Type") = "U2" Then
            grdUserlist.Columns(1).Visible = False
            grdUserlist.Columns(2).Visible = True
            grdUserlist.Columns(3).Visible = False
            btnCreate.Visible = False
        Else
            grdUserlist.Columns(1).Visible = False
            grdUserlist.Columns(2).Visible = False
            grdUserlist.Columns(3).Visible = True
            btnCreate.Visible = False
        End If
    End Sub
    Protected Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        BindData()
    End Sub
    Protected Sub grdUserlist_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdUserlist.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        BindData()
    End Sub
    Protected Sub grdUserlist_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdUserlist.PageIndexChanging
        grdUserlist.PageIndex = e.NewPageIndex
        BindData()
    End Sub
    Protected Sub grdUserlist_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdUserlist.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = grdUserlist.PageIndex * grdUserlist.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        binddata()
    End Sub
    Protected Sub ddlSrcname_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSrcname.SelectedIndexChanged
        BindData()
    End Sub
    Protected Sub btnCreate_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.ServerClick
        Response.Redirect("frmUserSetup.aspx")
    End Sub
    Protected Sub ddlRole_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRole.SelectedIndexChanged
        BindData()
    End Sub

    Private Sub bindUserScopeAccess(ByVal userid As Int32)
        strQuery = "select TIScope, SISScope, CMEScope, SITACSCope from ebastusers_1 where usr_id=" & userid
        dt = objutil.ExeQueryDT(strQuery, "scopetable")
        If dt.Rows.Count > 0 Then
            Dim tiscope As Boolean = Convert.ToBoolean(dt.Rows(0).Item(0))
            Dim sisscope As Boolean = Convert.ToBoolean(dt.Rows(0).Item(1))
            Dim cmescope As Boolean = Convert.ToBoolean(dt.Rows(0).Item(2))
            Dim sitacscope As Boolean = Convert.ToBoolean(dt.Rows(0).Item(3))

            If tiscope = True Then
                ChkTIScope.Checked = True
            End If

            If sisscope = True Then
                ChkSISScope.Checked = True
            End If

            If cmescope = True Then
                ChkCMEScope.Checked = True
            End If

            If sitacscope = True Then
                ChkSitacScope.Checked = True
            End If

        End If
    End Sub

    Private Sub UpdateScope(ByVal tiscope As Boolean, ByVal sisscope As Boolean, ByVal sitacscope As Boolean, ByVal cmescope As Boolean, ByVal usrid As Integer)
        Dim strtiscope As String = IIf(tiscope = True, "true", "false")
        Dim strsisscope As String = IIf(sisscope = True, "true", "false")
        Dim strsitacscope As String = IIf(sitacscope = True, "true", "false")
        Dim strcmescope As String = IIf(cmescope = True, "true", "false")
        strQuery = "update ebastusers_1 set tiscope='" & strtiscope & "',SISScope='" & strsisscope & "',CMEScope='" &
                    strcmescope & "',SitacScope='" & strsitacscope & "' where usr_id=" & usrid
        objutil.ExeQuery(strQuery)
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "modalClose();", True)
        Response.Write("<script>alert('User scope has been updated successfully');</script>")
    End Sub

    Private Sub grdUserlist_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdUserlist.RowCommand
        If e.CommandName = "ScopeUser" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            If index > -1 Then
                Dim id As HiddenField = DirectCast(grdUserlist.Rows(index).FindControl("hiddenId"), HiddenField)
                Dim column As Integer
                If grdUserlist.Columns(1).Visible = True Then
                    column = 1
                ElseIf grdUserlist.Columns(2).Visible = True Then
                    column = 2
                Else
                    column = 3
                End If
                Dim name As HyperLink = DirectCast(grdUserlist.Rows(index).Cells(column).Controls.Item(0), HyperLink)
                bindUserScopeAccess(id.Value)
                modalUsrId.Value = id.Value
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "modelOpen('" & name.Text & "');", True)
            End If
        End If
    End Sub

    Private Sub LbtSave_Click(sender As Object, e As EventArgs) Handles LbtSave.Click
        If Not String.IsNullOrEmpty(modalUsrId.Value) Then
            UpdateScope(ChkTIScope.Checked, ChkSISScope.Checked, ChkSitacScope.Checked, ChkCMEScope.Checked, modalUsrId.Value)
        End If
    End Sub
End Class
