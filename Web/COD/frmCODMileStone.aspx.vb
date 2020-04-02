Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data

Partial Class COD_frmCODMileStone
    Inherits System.Web.UI.Page
    Dim objET As New ETCODMileStone
    Dim objBO As New BOCODMileStone
    Dim objED As New ETDelete
    Dim objBD As New BODelete
    Dim dt As New DataTable
    Dim ObjUtil As New DBUtil
    Dim strsql As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")
        If Page.IsPostBack = False Then
            binddata()
            If Not Request.QueryString("id") Is Nothing Then
                tdTitle.InnerText = ""
                addrow.InnerText = "Edit"
                btnSave.Text = "Update"
                Binddetails()
            End If
        End If
    End Sub
    Sub binddata()
        'dt = objBO.uspCODMileStoneLD(, ddlSelect.SelectedValue, txtSearch.Text, hdnSort.Value)
        strsql = "EXEC uspCODMileStoneLD 0,'" & ddlSelect.SelectedValue & "','" & txtSearch.Text & "','" & hdnSort.Value & "' "
        dt = ObjUtil.ExeQueryDT(strsql, "CODMilestone")
        grdMilestone.DataSource = dt
        grdMilestone.PageSize = Session("Page_size")
        grdMilestone.DataBind()
    End Sub
    Sub Binddetails()
        'dt = objBO.uspCODMileStoneLD(Request.QueryString("id"))
        Dim strsql As String = "EXEC uspCODMileStoneLD " & Request.QueryString("id")
        dt = ObjUtil.ExeQueryDT(strsql, "CODMilestone")
        If dt.Rows.Count > 0 Then
            txtCode.Value = dt.Rows(0).Item("INT_CODE").ToString
            txtDesc.InnerText = dt.Rows(0).Item("INT_DESC").ToString
            txtShortDesc.Value = dt.Rows(0).Item("INT_Short_Desc").ToString
            tblDetails.Visible = True
            btnDelete.Visible = True
        End If
    End Sub

    Sub savedata()
        objET.INT_Code = txtCode.Value.Replace("'", "''")
        objET.INT_Desc = txtDesc.Value.Replace("'", "''")
        objET.AT.RStatus = Constants.STATUS_ACTIVE
        objET.AT.LMBY = Session("User_Name")
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim id As Integer = Request.QueryString("id")
        objED.TableName = "CODMileStone"
        objED.FieldName = "INT_ID"
        objED.FieldValue = id
        BOcommon.result(objBD.uspDelete(objED), False, "", "", Constants._DELETE)
        binddata()
        tblDetails.Visible = False
    End Sub

    Protected Sub btnNew_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.ServerClick
        tdTitle.InnerText = ""
        tblDetails.Visible = True
        txtCode.Value = ""
        txtDesc.Value = ""
    End Sub

    Protected Sub btnCancel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.ServerClick
        Response.Redirect("frmCODMileStone.aspx")
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        binddata()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        savedata()
        Dim I As Integer
        If Request.QueryString("id") Is Nothing Then
            With objET
                strsql = "EXEC uspCODMileStoneIU " & .INT_ID & ",'" & .INT_Code & "','" & .INT_Desc & "'," & .AT.RStatus & ",'" & .AT.LMBY & "' "
            End With
            strsql = strsql & ",'" & txtShortDesc.Value.Replace("'", "''") & "'"
            i = ObjUtil.ExeQueryScalar(strsql)
            BOcommon.result(I, True, "frmCodMileStone.aspx", "INT_CODE", Constants._INSERT)
        Else
            objET.INT_ID = Request.QueryString("id")
            With objET
                strsql = "EXEC uspCODMileStoneIU " & .INT_ID & ",'" & .INT_Code & "','" & .INT_Desc & "'," & .AT.RStatus & ",'" & .AT.LMBY & "' "
            End With
            strsql = strsql & ",'" & txtShortDesc.Value.Replace("'", "''") & "'"
            I = ObjUtil.ExeQueryScalar(strsql)
            BOcommon.result(I, True, "frmCodMileStone.aspx", "INT_CODE", Constants._UPDATE)
            'BOcommon.result(objBO.uspCODMileStoneIU(objET), True, "frmCodMileStone.aspx", "INT_CODE", Constants._UPDATE)
        End If
    End Sub

    Protected Sub grdMilestone_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMilestone.PageIndexChanging
        grdMilestone.PageIndex = e.NewPageIndex
        binddata()
    End Sub

    Protected Sub grdMilestone_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMilestone.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdMilestone.PageIndex * grdMilestone.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub grdMilestone_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdMilestone.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        binddata()
    End Sub
End Class
