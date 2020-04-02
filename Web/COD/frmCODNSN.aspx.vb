Imports Common
Imports BusinessLogic
Imports Entities
Imports System.Data
Partial Class COD_frmNSN
    Inherits System.Web.UI.Page
    Dim objdl As New BODDLs
    Dim objdo As New ETCODSite
    Dim objbo As New BOCODSite
    Dim dt As New DataTable
    Dim UsrId As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            btnSave.Attributes.Add("onclick", "javascript:return(checkIsEmpty())")
            objdl.fillDDL(DDLZone, "CODZone", True, Constants._DDL_Default_Select)
            BindData()
        End If
        If hdnSSName.Value <> "" Then
            txtSSName.Value = hdnSSName.Value
        End If
    End Sub
    Sub BindData()
        dt = objbo.uspCODNSNSiteListSup(, DDLZone.SelectedValue, ddlSelect.SelectedValue, txtSearch.Text, hdnSort.Value)
        grdNSN.PageSize = Session("Page_size")
        grdNSN.DataSource = dt
        grdNSN.DataBind()
    End Sub

    Protected Sub grdNSN_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdNSN.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow AndAlso e.Row.RowState = DataControlRowState.Normal OrElse e.Row.RowState = DataControlRowState.Alternate Then

            Dim chkBxSelect As HtmlInputCheckBox = DirectCast(e.Row.Cells(1).FindControl("chkBxSelect"), HtmlInputCheckBox)
            Dim chkBxHeader As CheckBox = DirectCast(Me.grdNSN.HeaderRow.FindControl("chkBxHeader"), CheckBox)

            chkBxSelect.Attributes("onclick") = String.Format("javascript:ChildClick(this,'{0}')", chkBxHeader.ClientID)
        End If
    End Sub

    Protected Sub grdNSN_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdNSN.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        BindData()
    End Sub

    Protected Sub grdNSN_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdNSN.PageIndexChanging
        grdNSN.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindData()
    End Sub

    Protected Sub btnCancel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.ServerClick
        Response.Redirect("frmCODNSNList.aspx")
    End Sub

    Protected Sub DDLZone_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLZone.SelectedIndexChanged
        BindData()
    End Sub
    Sub fillDetails()
        With objdo
            .AT.RStatus = Constants.STATUS_ACTIVE
            .AT.LMBY = "SysAdmin" 'Session("User_Name")
            'txtSSName.Value = dt.Rows(0).Item("Name").ToString
            'UsrId = dt.Rows(0).Item("USR_ID").ToString          
        End With
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        fillDetails()
        Dim chk As HtmlInputCheckBox
        Dim strid As String = ""
        For i As Integer = 0 To grdNSN.Rows.Count - 1
            chk = CType(grdNSN.Rows(i).Cells(1).FindControl("chkBxSelect"), HtmlInputCheckBox)
            If chk.Checked = True Then
                strid = strid & IIf(strid = "", "", ",") & "'" & chk.Value & "'"
            End If
        Next
        If strid <> "" Then
            strid = strid.Replace("'", "''")
            BOcommon.result(objbo.uspCODNSNConIU(hdnSupId.Value, strid, objdo.AT.RStatus, objdo.AT.LMBY), True, "frmCODNSNList.aspx", "Site_Name", Constants._INSERT)
        Else
            Response.Write("<script language='javascript'>alert('please select atleast one site')</script>")
        End If
    End Sub

    Protected Sub grdNSN_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdNSN.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = grdNSN.PageIndex * grdNSN.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
End Class
