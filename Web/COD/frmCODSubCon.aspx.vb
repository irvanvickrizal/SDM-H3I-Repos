Imports Common
Imports BusinessLogic
Imports Entities
Imports System.Data

Partial Class COD_frmCODSubCon
    Inherits System.Web.UI.Page
    Dim objdl As New BODDLs
    Dim objdo As New ETCODSite
    Dim objbo As New BOCODSite
    Dim dt As New DataTable
    Dim UsrId As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
            objdl.fillDDL(DDLZone, "CODZone", True, Constants._DDL_Default_Select)
            BindData()
            If Not Request.QueryString("id") Is Nothing Then
                rowadd.InnerText = "Edit"
                binddetails()
            End If
        End If
        If hdnSSName.Value <> "" Then
            txtSSName.Value = hdnSSName.Value
        End If
    End Sub
    Sub BindData()
        dt = objbo.uspCODSubConSiteListSup(, DDLZone.SelectedValue, ddlSelect.SelectedValue, txtSearch.Text, hdnSort.Value)
        grdSubCon.PageSize = Session("Page_size")
        grdSubCon.DataSource = dt
        grdSubCon.DataBind()
    End Sub
    Sub binddetails()
        dt = objbo.uspCODSubConSiteList(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            txtNo.Value = dt.Rows(0).Item("Site_No").ToString
            txtName.Value = dt.Rows(0).Item("Site_Name").ToString
            txtSTDesc.Value = dt.Rows(0).Item("Site_Desc").ToString
            txtSSName.Value = dt.Rows(0).Item("Name").ToString
            tblSite.Visible = True
        End If
    End Sub

    Protected Sub grdSubCon_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSubCon.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow AndAlso e.Row.RowState = DataControlRowState.Normal OrElse e.Row.RowState = DataControlRowState.Alternate Then

            Dim chkBxSelect As HtmlInputCheckBox = DirectCast(e.Row.Cells(1).FindControl("chkBxSelect"), HtmlInputCheckBox)
            Dim chkBxHeader As CheckBox = DirectCast(Me.grdSubCon.HeaderRow.FindControl("chkBxHeader"), CheckBox)

            chkBxSelect.Attributes("onclick") = String.Format("javascript:ChildClick(this,'{0}')", chkBxHeader.ClientID)
        End If
    End Sub

    Protected Sub grdSubCon_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdSubCon.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        BindData()
    End Sub

    Protected Sub grdSubCon_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSubCon.PageIndexChanging
        grdSubCon.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
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

        For i As Integer = 0 To grdSubCon.Rows.Count - 1
            chk = CType(grdSubCon.Rows(i).Cells(1).FindControl("chkBxSelect"), HtmlInputCheckBox)

            If chk.Checked = True Then
                strid = strid & IIf(strid = "", "", ",") & "'" & chk.Value & "'"
            End If
        Next
        If strid <> "" Then
            strid = strid.Replace("'", "''")
            BOcommon.result(objbo.uspCODSubConIU(hdnSupId.Value, strid, objdo.AT.RStatus, objdo.AT.LMBY), True, "frmCODSubConnList.aspx", "Site_Name", Constants._INSERT)
            'Response.Redirect("frmCODSubConnList.aspx")
        Else
            Response.Write("<script language='javascript'>alert('please select site')</script>")
        End If
    End Sub
    Protected Function SelectionEnabled(ByVal Site_ID As Object) As Boolean

        Dim returnValue As Boolean = True
        Try
            If Not (Site_ID Is DBNull.Value) Then
                Dim recCnt As Integer = 0
                recCnt = objbo.uspCODSubConCheckList(Site_ID)
                If recCnt > 0 Then
                    returnValue = False
                End If
            End If
        Catch ex As Exception

        End Try

        Return returnValue

    End Function


    Protected Sub grdSubCon_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSubCon.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.Cells(0).FindControl("lblNo"), Label)
                lbl.Text = grdSubCon.PageIndex * grdSubCon.PageSize + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub btnCancel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.ServerClick
        Response.Redirect("frmCODSubConnList.aspx")
    End Sub

    Protected Sub DDLZone_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLZone.SelectedIndexChanged
        BindData()
    End Sub
End Class
