Imports System.Data
Imports System.Collections.Generic
Imports System.Text

Partial Class COD_frmCODScopeGroupingNew
    Inherits System.Web.UI.Page

    Private controller As New ScopeController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            Dim scopeid As Integer = 0
            If DdlScopes.SelectedIndex > 0 Then
                scopeid = Integer.Parse(DdlScopes.SelectedValue)
            End If
            BindData(scopeid)
            BindScopesDDL(DdlMasterScope)
            BindScopesDDL(DdlScopes)
        End If
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        If Not String.IsNullOrEmpty(TxtDetailScopeName.Text) And DdlMasterScope.SelectedIndex > 0 Then
            AddUpdateData(0, TxtDetailScopeName.Text, TxtScopeDescription.Text, Integer.Parse(DdlMasterScope.SelectedValue))
            Dim scopeid As Integer = 0
            If DdlScopes.SelectedIndex > 0 Then
                scopeid = Integer.Parse(DdlScopes.SelectedValue)
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
        Dim scopeid As Integer = Integer.Parse(GvScopeGrouping.DataKeys(e.RowIndex).Values("DScope_Id").ToString())
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
        AddUpdateData(dscopeid, txtGvScopeName.Text, txtGvScopeDesc.Text, Integer.Parse(ddl.SelectedValue))
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
    Private Sub BindData(ByVal scopeid As Integer)
        Dim ds As DataSet = controller.GetAllDetailScopes_DS(False, scopeid)
        If ds.Tables(0).Rows.Count > 0 Then
            GvScopeGrouping.DataSource = ds
            GvScopeGrouping.DataBind()
        Else
            GvScopeGrouping.DataSource = Nothing
            GvScopeGrouping.DataBind()
        End If
    End Sub

    Private Sub BindScopeDDL(ByVal ddl As DropDownList, ByVal scopeid As String)
        ddl.DataSource = controller.GetScopeMaster(False)
        ddl.DataTextField = "ScopeName"
        ddl.DataValueField = "ScopeId"
        ddl.DataBind()

        If Integer.Parse(scopeid) > 0 Then
            ddl.SelectedValue = scopeid
        End If
    End Sub

    Private Sub BindScopesDDL(ByVal ddl As DropDownList)
        ddl.DataSource = controller.GetScopeMaster(False)
        ddl.DataTextField = "ScopeName"
        ddl.DataValueField = "ScopeId"
        ddl.DataBind()
        ddl.Items.Insert(0, "-- Select Scope --")
    End Sub

    Private Sub AddUpdateData(ByVal dscopeid As Integer, ByVal dscopename As String, ByVal dscopedesc As String, ByVal scopeid As Integer)
        Dim info As New DetailScopeInfo
        info.DScopeId = dscopeid
        info.DScopeName = dscopename
        info.DScopeDesc = dscopedesc
        info.ScopeId = scopeid
        info.DScopeLMBY = CommonSite.UserName
        controller.DetailScopeIU(info)
    End Sub

    Private Sub DeleteData(ByVal dscopeid As Integer)
        controller.DeleteDetailScopeTemp(dscopeid, CommonSite.UserName)
    End Sub

    Private Sub ClearTextField()
        BindScopesDDL(DdlMasterScope)
        TxtDetailScopeName.Text = ""
        TxtScopeDescription.Text = ""
    End Sub
#End Region
End Class
