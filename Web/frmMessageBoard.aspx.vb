Imports Entities
Imports BusinessLogic
Imports Common
Imports System.Data
Partial Class frmMessageBoard
    Inherits System.Web.UI.Page
    Dim objET As New ETMessageBoard
    Dim objBO As New BOMessageBoard
    Dim objED As New ETDelete
    Dim objBD As New BODelete
    'Dim objBOD As New BODDLs
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnUpdate.Attributes.Add("onClick", "javascript:return checkIsEmpty();")
        If Page.IsPostBack = False Then
            GetList()
            If Not Request.QueryString("id") Is Nothing Then
                BindData()
            End If
        End If
    End Sub
    Sub GetList()
        dt = objBO.uspMessageBoardLD(, hdnSort.Value)
        grdMessageBoard.DataSource = dt
        grdMessageBoard.PageSize = Session("Page_size")
        grdMessageBoard.DataBind()
    End Sub
    Sub UpDate()
        objET.MsgId = Request.QueryString("id")
        objET.Title = txtTitle.Text
        objET.Message = txtMessage.Text
        objET.AT.RStatus = Constants.STATUS_ACTIVE
        objET.AT.LMBY = Session("User_Name")
        objET.PostedBy = Session("User_Name")
    End Sub
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        UpDate()
        BOcommon.result(objBO.uspMessageBoardIU(objET), True, "frmMessageBoard.aspx", "", Constants._UPDATE)
    End Sub
    Sub BindData()
        dt = objBO.uspMessageBoardLD(Request.QueryString("id"))
        txtTitle.Text = dt.Rows(0).Item("Title").ToString
        txtMessage.Text = dt.Rows(0).Item("Message").ToString
    End Sub
    Protected Sub grdMessageBoard_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMessageBoard.PageIndexChanging
        grdMessageBoard.PageIndex = e.NewPageIndex
        GetList()
    End Sub

    Protected Sub grdMessageBoard_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMessageBoard.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
            lbl.Text = (grdMessageBoard.PageIndex * grdMessageBoard.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End If
    End Sub

    Protected Sub grdMessageBoard_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdMessageBoard.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        GetList()
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        objED.TableName = "MessageBoard"
        objED.FieldName = "MsgId"
        objED.FieldValue = Request.QueryString("id")
        BOcommon.result(objBD.uspDelete(objED), False, "", "", Constants._DELETE)
        clear()
        GetList()
    End Sub
    Sub Clear()
        txtTitle.Text = "'"
        txtMessage.Text = "'"
    End Sub
End Class



