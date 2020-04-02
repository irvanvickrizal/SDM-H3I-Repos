Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data
Partial Class COD_frmCODScopeGrouping
    Inherits System.Web.UI.Page
    Dim objET As New ETCODScopeGrouping
    Dim objbo As New BOCODScopeGrouping
    Dim dt As New DataTable
    Dim str As String = String.Empty
    Dim i, j As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        If Page.IsPostBack = False Then
            binddata()
            If Not Request.QueryString("id") Is Nothing Then
                tdTitle.InnerText = ""
                addrow.InnerText = "Edit"
                btnSave.Text = "Update"
                BindList()
                Binddetails()
            End If
        End If
    End Sub

    Sub binddata()
        dt = objbo.uspCODScopeGroupingLD(hdnSort.Value)
        grdScope.DataSource = dt
        grdScope.PageSize = 5
        'grdScope.PageSize = Session("Page_size")
        grdScope.DataBind()
    End Sub
    Sub Binddetails()
        dt = objbo.uspCODScopeGroupingLD(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            txtGroup.Value = dt.Rows(0).Item("GroupName").ToString.Replace("'", "''")
            For i = 0 To dt.Rows.Count - 1
                For j = 0 To chklistSelect.Items.Count - 1
                    If dt.Rows(i).Item("Scope_id") = chklistSelect.Items(j).Value Then chklistSelect.Items(j).Selected = True
                Next
            Next
            tblScope.Visible = True
            btnDelete.Visible = True
        End If
    End Sub
    Sub savedata()
        For i = 0 To chklistSelect.Items.Count - 1
            If chklistSelect.Items(i).Selected = True Then
                str = IIf(str <> "", str & "," & chklistSelect.Items(i).Value, chklistSelect.Items(i).Value)
            End If
        Next
        objET.Scope_ids = str
        objET.GroupName = txtGroup.Value
        objET.AT.RStatus = Constants.STATUS_ACTIVE
        objET.AT.LMBY = "Subhash" 'Session("User_Name")
    End Sub

    Protected Sub btnCanel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanel.ServerClick
        Response.Redirect("frmCODScopeGrouping.aspx")
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        BOcommon.result(objbo.uspDelete("CODScopeGrouping", "GroupName", Request.QueryString("id")), True, "frmCODScopeGrouping.aspx", " ", Constants._DELETE)
    End Sub

    Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
        tdTitle.InnerText = ""
        tblScope.Visible = True
        txtGroup.Value = ""
        BindList()
    End Sub

    Protected Sub grdScope_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdScope.PageIndexChanging
        grdScope.PageIndex = e.NewPageIndex
        binddata()
    End Sub

    Protected Sub grdScope_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdScope.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdScope.PageIndex * grdScope.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub grdScope_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdScope.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        binddata()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        savedata()
        Dim strDocName As String = ""
        If str <> "" Then
            dt = objbo.uspCODScopeGrpCheck(objET.Scope_ids)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    strDocName = strDocName & IIf(strDocName = "", "", ",") & "" & dt.Rows(i).Item("Scope") & ""
                Next
            End If
            If strDocName <> "" Then
                Response.Write("<script>alert('Already Grouping is done for (" & strDocName & ") .Please uncheck and then click on the save.')</script>")
            Else
                If Request.QueryString("Mode") = "E" Then
                    BOcommon.result(objbo.uspCODScopeGroupingIU(objET, Request.QueryString("Mode")), True, "frmCODScopeGrouping.aspx", txtGroup.Value, Constants._UPDATE)
                Else
                    BOcommon.result(objbo.uspCODScopeGroupingIU(objET), True, "frmCODScopeGrouping.aspx", txtGroup.Value, Constants._INSERT)
                End If
            End If
        Else
            Response.Write("<script>alert('Scope must be Select')</script>")
        End If
    End Sub
    Sub BindList()
        dt = objbo.fillchklist("CODScope")
        If dt.Rows.Count > 0 Then
            chklistSelect.DataSource = dt.DefaultView
            chklistSelect.DataTextField = "txt"
            chklistSelect.DataValueField = "val"
            chklistSelect.DataBind()
        End If
    End Sub
End Class
