Imports System.Data
Partial Class COD_frmCODScopeNew
    Inherits System.Web.UI.Page
    Dim controller As New ScopeController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        If Not String.IsNullOrEmpty(TxtScopeName.Text) Then
            AddUpdateData(0, TxtScopeName.Text, TxtScopeDescription.Text)
            BindData()
            ClearTextField()
        End If
    End Sub

    Protected Sub GvSubcon_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvMasterScope.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim desc As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Scope_Name"))
            Dim lnkbtnresult As ImageButton = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
            If Not lnkbtnresult Is Nothing Then
                lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + desc + "')")
            End If
        End If
    End Sub

    Protected Sub gvmscope_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim scopeid As Integer = Integer.Parse(GvMasterScope.DataKeys(e.RowIndex).Values("GScope_Id").ToString())
        DeleteData(scopeid)
        BindData()
    End Sub

    Protected Sub gvmscope_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim subconid As Integer = Integer.Parse(GvMasterScope.DataKeys(e.RowIndex).Value.ToString())
        Dim txtGvScopeName As TextBox = CType(GvMasterScope.Rows(e.RowIndex).FindControl("TxtGvScopeName"), TextBox)
        Dim txtGvScopeDesc As TextBox = CType(GvMasterScope.Rows(e.RowIndex).FindControl("TxtGvScopeDesc"), TextBox)
        GvMasterScope.EditIndex = -1
        AddUpdateData(subconid, txtGvScopeName.Text, txtGvScopeDesc.Text)
        BindData()
    End Sub

    Protected Sub gvmscope_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        GvMasterScope.EditIndex = -1
        BindData()
    End Sub

    Protected Sub gvmscope_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        GvMasterScope.EditIndex = e.NewEditIndex
        BindData()
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        Dim ds As DataSet = controller.GetScopeMaster_DS(False)
        If (ds.Tables(0).Rows.Count > 0) Then
            GvMasterScope.DataSource = ds
            GvMasterScope.DataBind()
        Else
            GvMasterScope.DataSource = Nothing
            GvMasterScope.DataBind()
        End If
        
    End Sub

    Private Sub AddUpdateData(ByVal scopeid As Integer, ByVal scopename As String, ByVal scopedesc As String)
        Dim info As New ScopeInfo
        info.ScopeId = scopeid
        info.ScopeName = scopename
        info.ScopeDesc = scopedesc
        info.ScopeLMBY = CommonSite.UserName
        controller.ScopeMasterIU(info)
    End Sub

    Private Sub DeleteData(ByVal scopeid As Integer)
        controller.DeleteScopeMasterTemp(scopeid, CommonSite.UserName)
    End Sub

    Private Sub ClearTextField()
        TxtScopeName.Text = ""
        TxtScopeDescription.Text = ""
    End Sub

    Private Sub GvMasterScope_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvMasterScope.PageIndexChanging
        GvMasterScope.PageIndex = e.NewPageIndex
        BindData()
    End Sub
#End Region
End Class
