Imports BusinessLogic
Imports Common
Imports system.Data
Partial Class DocumentWrokFlow
    Inherits System.Web.UI.Page
    Dim objBO As New BODashBoard
    Dim objBL As New BODDLs
    Dim objBoWF As New BOWTDoc

    Dim dt As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then

            ' objBL.fillDDL(ddlWF, "TWorkFlow", False, Constants._DDL_Default_Select)
            BindData()
        End If

    End Sub
    Sub BindData()
        dt = objBO.uspDashBoardDocWF(CommonSite.GetDashBoardLevel(), CommonSite.GetDashBoardLevelId())
        grdDocument.DataSource = dt
        grdDocument.PageSize = CommonSite.PageSize()
        grdDocument.DataBind()
        BindDD()
    End Sub
    Sub BindDD()
        For i As Integer = 0 To grdDocument.Rows.Count - 1
            Dim ddWf As DropDownList
            ddWf = CType(grdDocument.Rows(i).Cells(1).FindControl("ddWF"), DropDownList)
            objBL.fillDDL(ddWf, "TWorkFlow", True, Constants._DDL_Default_Select)
        Next
    End Sub
    Protected Sub grdDocument_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdDocument.RowDeleting
        Dim GrpId As String = grdDocument.DataKeys(e.RowIndex).Value().ToString()
        Dim ddWf As DropDownList
        ddWf = CType(grdDocument.Rows(e.RowIndex).Cells(1).FindControl("ddWF"), DropDownList)
        Dim strDocName As String = ""
        If ddWf.SelectedItem.Value <> "0" Then
            dt = objBoWF.uspWFDocCheck(ddWf.SelectedValue, GrpId)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Already assign work flow for given document (" & GrpId.ToString() & ") .Please uncheck and then click into the save.');", True)
            Else
                Dim i As Integer = -1
                i = objBoWF.uspBoWTDocInsert(ddWf.SelectedValue, Constants.STATUS_ACTIVE, Session("UserName"), GrpId, "P")
                If i = 0 Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
                ElseIf i = 1 Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Data Saved Sucessfully.');", True)
                    Response.Redirect("DocumentWrokFlow.aspx")
                End If
            End If
        End If
       

    End Sub

    Protected Sub grdDocument_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDocument.PageIndexChanging
        grdDocument.PageIndex = e.NewPageIndex
        BindData()
    End Sub
End Class
