Imports System.Data
Imports System.Collections.Generic
Imports System.Text
Partial Class COD_frmMasterList
    Inherits System.Web.UI.Page

    Private controller As New MasterListController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            Dim Parentid As Integer = 0
            If DdlScopes.SelectedIndex > 0 Then
                Parentid = Integer.Parse(DdlScopes.SelectedValue)
            End If
            BindData(Parentid)
            BindScopesDDL(DdlParentName)
            BindScopesDDL(DdlScopes)
        End If
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        If Not String.IsNullOrEmpty(TxtDocName.Text) And DdlParentName.SelectedIndex = 0 Then
            AddUpdateData(0, TxtSN.Text, TxtDocName.Text, TxtSerialOrder.Text, Integer.Parse(DdlParentName.SelectedValue))
            Dim Parentid As Integer = 0
            If DdlScopes.SelectedIndex > 0 Then
                Parentid = Integer.Parse(DdlParentName.SelectedValue)
            End If
            ClearTextField()
        End If
    End Sub

    Protected Sub GvScopeGrouping_IndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvScopeGrouping.PageIndexChanging
        GvScopeGrouping.PageIndex = e.NewPageIndex
        Dim scopeid As Integer = 0
        If DdlScopes.SelectedIndex > 0 Then
            scopeid = Integer.Parse(DdlScopes.SelectedValue)
        End If
        BindData(scopeid)
    End Sub

    Protected Sub GvScopeGrouping_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvScopeGrouping.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim desc As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DScope_Name"))
            Dim lnkbtnresult As ImageButton = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
            Dim Ddl As DropDownList = CType(e.Row.FindControl("DdlScope"), DropDownList)
            Dim lblScopeId As Label = CType(e.Row.FindControl("LblScopeId"), Label)
            If Ddl IsNot Nothing Then
                BindScopeDDL(Ddl, lblScopeId.Text)
            End If

            If Not lnkbtnresult Is Nothing Then
                lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + desc + "')")
            End If
        End If
    End Sub

    Protected Sub gvscopegrouping_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim scopeid As Integer = Integer.Parse(GvScopeGrouping.DataKeys(e.RowIndex).Values("GScope_Id").ToString())
        DeleteData(scopeid)
        Dim grpScopeid As Integer = 0
        If DdlScopes.SelectedIndex > 0 Then
            grpScopeid = Integer.Parse(DdlScopes.SelectedValue)
        End If
        BindData(grpScopeid)
    End Sub

    Protected Sub gvscopegrouping_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim dscopeid As Integer = Integer.Parse(GvScopeGrouping.DataKeys(e.RowIndex).Value.ToString())
        Dim txtGvScopeName As TextBox = CType(GvScopeGrouping.Rows(e.RowIndex).FindControl("TxtGvDScopeName"), TextBox)
        Dim txtGvScopeDesc As TextBox = CType(GvScopeGrouping.Rows(e.RowIndex).FindControl("TxtGvDScopeDesc"), TextBox)
        Dim ddl As DropDownList = CType(GvScopeGrouping.Rows(e.RowIndex).FindControl("DdlScope"), DropDownList)
        GvScopeGrouping.EditIndex = -1
        'AddUpdateData(LIstid, txtGvScopeName.Text, txtGvScopeDesc.Text, Integer.Parse(ddl.SelectedValue))
        Dim grpScopeid As Integer = 0
        If DdlScopes.SelectedIndex > 0 Then
            grpScopeid = Integer.Parse(DdlScopes.SelectedValue)
        End If
        BindData(grpScopeid)
    End Sub

    Protected Sub gvscopegrouping_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        GvScopeGrouping.EditIndex = -1
        Dim grpScopeid As Integer = 0
        If DdlScopes.SelectedIndex > 0 Then
            grpScopeid = Integer.Parse(DdlScopes.SelectedValue)
        End If
        BindData(grpScopeid)
    End Sub

    Protected Sub gvscopegrouping_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        GvScopeGrouping.EditIndex = e.NewEditIndex
        Dim grpScopeid As Integer = 0
        If DdlScopes.SelectedIndex > 0 Then
            grpScopeid = Integer.Parse(DdlScopes.SelectedValue)
        End If
        BindData(grpScopeid)
    End Sub

    Protected Sub DdlScopes_IndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlScopes.SelectedIndexChanged
        Dim grpScopeid As Integer = 0
        If DdlScopes.SelectedIndex > 0 Then
            grpScopeid = Integer.Parse(DdlScopes.SelectedValue)
        End If
        BindData(grpScopeid)
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal Listid As Integer)
        Dim ds As DataSet = controller.GetAllMasterList(False, Listid)
        If ds.Tables(0).Rows.Count > 0 Then
            GvScopeGrouping.DataSource = ds
            GvScopeGrouping.DataBind()
        Else
            GvScopeGrouping.DataSource = Nothing
            GvScopeGrouping.DataBind()
        End If
    End Sub

    Private Sub BindScopeDDL(ByVal ddl As DropDownList, ByVal parentid As Integer)
        ddl.DataSource = controller.GetParentLIst(False)
        ddl.DataTextField = "DocName"
        ddl.DataValueField = "ParentId"
        ddl.DataBind()

        If Integer.Parse(parentid) > 0 Then
            ddl.SelectedValue = parentid
        End If
    End Sub

    Private Sub BindScopesDDL(ByVal ddl As DropDownList)
        'ddl.DataSource = controller.GetScopeMaster(False)
        ddl.DataTextField = "DOCName"
        ddl.DataValueField = "ListId"
        ddl.DataBind()
        ddl.Items.Insert(0, "-- Select Parent --")
    End Sub

    Private Sub AddUpdateData(ByVal Listid As Integer, ByVal SN As String, ByVal DOCName As String, ByVal SerialOrder As Integer, ByVal Parent_id As Integer)
        Dim info As New MasterListInfo
        info.listId = Listid
        info.SN = SN
        info.DOCName = DOCName
        info.ParentId = Parent_id
        info.SerialOrder = SerialOrder
        info.ListLMBY = CommonSite.UserName
        controller.MasterListIU(info)
    End Sub

    Private Sub DeleteData(ByVal listid As Integer)
        controller.DeleteMasterLIstTemp(listid, CommonSite.UserName)
    End Sub

    Private Sub ClearTextField()
        BindScopesDDL(DdlParentName)
        TxtSN.Text = ""
        TxtDocName.Text = ""
        TxtSerialOrder.Text = ""
    End Sub
#End Region
End Class

