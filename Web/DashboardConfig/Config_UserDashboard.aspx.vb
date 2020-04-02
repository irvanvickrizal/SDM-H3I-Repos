Imports Dashboard_Config
Partial Class DashboardConfig_Config_UserDashboard
    Inherits System.Web.UI.Page
    Dim configcontroller As New ConfigController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindRoles(DdlNewRoles)
        End If
    End Sub

    Protected Sub BtnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAddNew.Click

    End Sub

    Protected Sub TaskPendingCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

#Region "Custom GridView"
    Protected Sub GvDashboardConfig_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvDashboardConfig.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim desc As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RoleName"))
            Dim lnkbtnresult As ImageButton = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)

            If Not lnkbtnresult Is Nothing Then
                lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + desc + "')")
            End If
        End If
    End Sub

    Protected Sub GvDashboardConfig_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        'Dim docid As Integer = Integer.Parse(GvMasterWCCDocument.DataKeys(e.RowIndex).Values("WCCDocument_Id").ToString())
        'DeleteData(docid)
        BindData()
    End Sub

    Protected Sub GvDashboardConfig_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        'Dim docid As Integer = Integer.Parse(GvMasterWCCDocument.DataKeys(e.RowIndex).Value.ToString())
        'Dim TxtGvDocName As TextBox = CType(GvMasterWCCDocument.Rows(e.RowIndex).FindControl("TxtGvDocName"), TextBox)
        'Dim TxtGvDocDesc As TextBox = CType(GvMasterWCCDocument.Rows(e.RowIndex).FindControl("TxtGvDocDesc"), TextBox)
        'GvMasterWCCDocument.EditIndex = -1
        'AddUpdateData(docid, TxtGvDocName.Text, TxtGvDocDesc.Text)
        'BindData()
    End Sub

    Protected Sub GvDashboardConfig_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        'GvMasterWCCDocument.EditIndex = -1
        'BindData()
    End Sub

    Protected Sub GvDashboardConfig_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        'GvMasterWCCDocument.EditIndex = e.NewEditIndex
        'BindData()
    End Sub
#End Region

#Region "Custom Methods"

    Private Sub BindData()

    End Sub

    Private Sub BindRoles(ByVal ddl As DropDownList)
        ddl.DataSource = configcontroller.GetRolesByDashboardConfig()
        ddl.DataTextField = "RoleName"
        ddl.DataValueField = "RoleId"
        ddl.DataBind()
    End Sub



    Private Sub SaveUpdateData()

    End Sub

#End Region

End Class
