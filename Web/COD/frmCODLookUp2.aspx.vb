'Created by : Radha
'Date : 13-10-2008
Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class frmLookUp2
    Inherits System.Web.UI.Page
    Dim objdl As New BODDLs
    Dim objdo As New ETCODLookup
    Dim objbo As New BOCODLookUp
    Dim dt As New DataTable
    Dim objdb As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")
        If Page.IsPostBack = False Then
            'objdl.fillDDL(DDLGRP, "SYSModuleGroup", True, Constants._DDL_Default_Select)
            objdl.fillDDL(DDLGroup, "SYSModuleGroup", True, Constants._DDL_Default_Select)
            BindData()
            If Not Request.QueryString("id") Is Nothing Then
                rowadd.InnerText = "Dashboard Milestone Names Edit"
                btnSave.Text = "Update"
                btnNewGroup.Disabled = True
                binddetails()
            End If
        End If
    End Sub
    Sub BindData()
        dt = objdb.ExeQueryDT("exec uspCodLookUpLD2", "CodLookUpLD2")
        'grdLookup.PageSize = Session("Page_size")
        grdLookup.DataSource = dt
        grdLookup.DataBind()
    End Sub
    Sub binddetails()
        dt = objbo.uspCODLookUpList(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            txtLKPCode.Value = dt.Rows(0).Item("LKPCode").ToString
            txtLKPDesc.Value = dt.Rows(0).Item("LKPDesc").ToString
            'DDLGRP.SelectedValue = dt.Rows(0).Item("GRP_ID")
            tblLookUp.Visible = True
            btnDelete.Visible = False 'disabled
        End If
    End Sub
    Protected Sub grdLookup_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdLookup.PageIndexChanging
        grdLookup.PageIndex = e.NewPageIndex
        BindData()
    End Sub
    Protected Sub grdLookup_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdLookup.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = grdLookup.PageIndex * grdLookup.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
        tblLookUp.Visible = True
    End Sub

    Protected Sub DDLGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLGroup.SelectedIndexChanged
        BindData()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindData()
    End Sub
    Sub fillDetails()
        With objdo
            .LKPCode = txtLKPCode.Value.Replace("'", "''")
            .LKPDesc = txtLKPDesc.Value.Replace("'", "''")
            '.GRP_ID = DDLGRP.SelectedValue
            .AT.RStatus = Constants.STATUS_ACTIVE
            .AT.LMBY = Session("User_Name")
        End With
    End Sub

    Protected Sub grdLookup_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdLookup.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        BindData()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        fillDetails()
        If Request.QueryString("id") Is Nothing Then
            BOcommon.result(objbo.uspCODLookUpIU(objdo), True, "frmCodLookUp2.aspx", "LKPCode", Constants._INSERT)
        Else
            objdo.LKP_ID = Request.QueryString("id")
            BOcommon.result(objbo.uspCODLookUpIU(objdo), True, "frmCodLookUp2.aspx", "LKPCode", Constants._UPDATE)
        End If
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        BOcommon.result(objbo.uspDelete("CODLookUp", "LKP_ID", Request.QueryString("id")), True, "frmCodLookUp2.aspx", " ", Constants._DELETE)
    End Sub
    Protected Sub btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Response.Redirect("frmCodLookUp2.aspx")
    End Sub
    'Protected Overloads Overrides Sub OnError(ByVal e As EventArgs)
    '    ' At this point we have information about the error 
    '    Dim ctx As HttpContext = HttpContext.Current
    '    Dim exception As Exception = ctx.Server.GetLastError()
    '    Dim objdo As New Entities.ETErrLogAppl
    '    'Dim objDO As New ETErrLogAppl
    '    objdo.ErrCode = exception.Source.Replace("'", "''")
    '    objdo.ErrDesc = exception.Message.Replace("'", "''")
    '    objdo.ErrURL = ctx.Request.Url.ToString().Replace("'", "''")
    '    objdo.ErrModule = Constants._APP_Coding_Management
    '    BOcommon.uspErrLogAppl(objDO)
    '    'sql2.InsertErrLog(exception.Source, exception.Message, "", "", ctx.Request.Url.ToString())
    '    ctx.Server.ClearError()
    '    Server.Transfer("../CommonErrorPage.aspx")
    '    MyBase.OnError(e)
    'End Sub

End Class
