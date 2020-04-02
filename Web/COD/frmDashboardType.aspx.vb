Imports Common
Imports BusinessLogic
Imports Entities
Imports System.Data

Partial Class COD_frmDashboardType
    Inherits System.Web.UI.Page
    Dim objutil As New DBUtil
    Dim dtResults As New DataTable
    Dim dtRoles As New DataTable
    Dim dhcontroller As New DashboardController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindUserType()
            LoadDataConfiguration()
            BindDashboardType(DDlDashboardType, "0")
        End If
    End Sub

    Protected Sub BtnAddRoleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAddRole.Click
        AddNewConfiguration()
        LoadDataConfiguration()
        BindUserType()
        BindDashboardType(DDlDashboardType, "0")
    End Sub

    Protected Sub GvDashboardTypeRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvDashboardType.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim DdlSaveDashboardType As DropDownList = CType(e.Row.FindControl("DdlSaveDashboardType"), DropDownList)
            Dim LblMasterDashboardId As Label = CType(e.Row.FindControl("LblMasterDashboardId"), Label)
            BindDashboardType(DdlSaveDashboardType, LblMasterDashboardId.Text)
        End If
    End Sub

    Protected Sub GvDashboardTypeItemCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvDashboardType.RowCommand
        If e.CommandName.Equals("deletedashboard") Then
            DeleteConfiguration(Integer.Parse(e.CommandArgument.ToString()))
            Page.ClientScript.RegisterStartupScript _
            (Me.GetType(), "alert", "DeleteConfig();", True)
            LoadDataConfiguration()
            BindUserType()
            BindDashboardType(DDlDashboardType, "0")
        ElseIf e.CommandName.Equals("savedashboard") Then
            Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            'Dim DdlDashboardType As DropDownList = DirectCast(row.Cells(2).FindControl("DdlDashboardType"), DropDownList)  'Commented by Fauzan, 11 Dec 2018
            'Added by Fauzan, 11 Dec 2018
            Dim DdlDashboardType As DropDownList = New DropDownList()
            'If set Manual follow below code
            '= DirectCast(row.Cells(2).Controls.Item(3), DropDownList) 
            For Each obj As Object In row.Cells(2).Controls
                If obj.GetType() Is GetType(DropDownList) Then
                    DdlDashboardType = obj
                    Exit For
                End If
            Next
            'END
            'UpdateConfiguration(Integer.Parse(e.CommandArgument.ToString()), dashType)     'Commented by Fauzan 11 Dec 2018
            UpdateConfiguration(Integer.Parse(e.CommandArgument.ToString()), DdlDashboardType.SelectedItem.Text, DdlDashboardType.SelectedValue)    'Added by Fauzan, 11 Dec 2018
            Page.ClientScript.RegisterStartupScript _
            (Me.GetType(), "alert", "UpdateConfig();", True)
            LoadDataConfiguration()
        End If
    End Sub

    Protected Sub DdlUserType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlUserType.SelectedIndexChanged
        If DdlUserType.SelectedIndex > 0 Then
            BindRole(DdlRole, Integer.Parse(DdlUserType.SelectedValue))
        Else
            BindRole(DdlRole, 0)
        End If
    End Sub

#Region "Custom Methods"
    Private Sub LoadDataConfiguration()
        GvDashboardType.DataSource = dhcontroller.GetDashboardConfigs()
        GvDashboardType.DataBind()
    End Sub

    Private Sub BindUserType()
        Dim dtusertypes As DataTable = objutil.ExeQueryDT("exec uspDDLTUserType", "dttypes")
        If dtusertypes.Rows.Count > 0 Then
            DdlUserType.DataSource = dtusertypes
            DdlUserType.DataTextField = "txt"
            DdlUserType.DataValueField = "val"
            DdlUserType.DataBind()

            DdlUserType.Items.Insert(0, "--Select User Type--")
        End If
        BindRole(DdlRole, 0)
    End Sub

    Private Sub BindDashboardType(ByVal ddl As DropDownList, ByVal mdashboardid As String)
        ddl.DataSource = dhcontroller.GetDashboardMaster()
        ddl.DataTextField = "DashboardName"
        ddl.DataValueField = "MDashboardId"
        ddl.DataBind()
        ddl.Items.Insert(0, "--select dashboard--")
        If (Integer.Parse(mdashboardid) > 0) Then
            ddl.SelectedValue = mdashboardid
        End If
    End Sub

    Private Sub AddNewConfiguration()
        'Modified by Fauzan 11 Dec 2018. Add single quotes on dashboard type value if the data type is varchar/string
        Dim strSql As String = "exec [uspInsertDashboardConfig] " & DdlRole.SelectedValue & ", '" & DDlDashboardType.SelectedItem.Text & "', " & DDlDashboardType.SelectedValue
        objutil.ExeNonQuery(strSql)
    End Sub

    Private Sub UpdateConfiguration(ByVal id As Integer, ByVal configType As String, ByVal mDashboardId As Integer)
        Dim strSql As String = "exec [uspUpdateDashboardUser] " & id & ", '" & configType & "', " & mDashboardId
        objutil.ExeNonQuery(strSql)
    End Sub

    Private Sub DeleteConfiguration(ByVal id As Integer)
        Dim strSql As String = "delete DashboardUser where Dashboard_id=" & id
        objutil.ExeNonQuery(strSql)
    End Sub


    Private Sub BindRole(ByVal ddl As DropDownList, ByVal grpid As Integer)
        dtRoles = objutil.ExeQueryDT("select RoleId, RoleDesc from trole where roleid not in(select roleid from dashboarduser) and rstatus = 2 and grpid=" & grpid, "roles")
        If dtRoles.Rows.Count > 0 Then
            ddl.DataSource = dtRoles
            ddl.DataTextField = "RoleDesc"
            ddl.DataValueField = "RoleId"
            ddl.DataBind()
        End If
        ddl.Items.Insert(0, "--select role--")
    End Sub

    Private Sub GvDashboardType_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvDashboardType.PageIndexChanging
        GvDashboardType.PageIndex = e.NewPageIndex
        LoadDataConfiguration()
    End Sub

#End Region

End Class
