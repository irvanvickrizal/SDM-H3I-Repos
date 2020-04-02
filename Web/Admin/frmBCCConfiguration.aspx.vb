
Partial Class Admin_frmBCCConfiguration
    Inherits System.Web.UI.Page

    Dim controller As New CODBCCConfigController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData(GetDdlEmailDocTypeValue(DdlEmailDocType_Short))
            BindRoles(DdlRole)
        End If
    End Sub

    Protected Sub DdlEmailDocType_SelectIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlEmailDocType.SelectedIndexChanged
        If DdlEmailDocType.SelectedValue.Equals("0") Then
            GvUsers.DataSource = Nothing
            GvUsers.DataBind()
        Else
            Dim roleid As Integer = 0
            If DdlRole.SelectedIndex > 0 Then
                roleid = Integer.Parse(DdlRole.SelectedValue)
            End If
            BindUser(TxtSearchName.Text, roleid, DdlEmailDocType.SelectedValue)
            'LblWarningMessage.Text = DdlRole.SelectedIndex
        End If
    End Sub

    Protected Sub LbtSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSearch.Click
        If DdlEmailDocType.SelectedIndex > 0 Then
            Dim roleid As Integer = 0
            If DdlRole.SelectedIndex > 0 Then
                roleid = Integer.Parse(DdlRole.SelectedValue)
            End If
            BindUser(TxtSearchName.Text, roleid, DdlEmailDocType.SelectedValue)
        Else
            LblWarningMessage.Text = "Please select Email Doc Type first"
            LblWarningMessage.Font.Italic = True
            LblWarningMessage.ForeColor = Drawing.Color.Red
        End If
        
    End Sub

    Protected Sub DdlemailDocTypeShort_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlEmailDocType_Short.SelectedIndexChanged
        BindData(GetDdlEmailDocTypeValue(DdlEmailDocType_Short))
    End Sub

    Protected Sub GvBCCConfiguration_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvBCCConfiguration.RowCommand
        If e.CommandName.Equals("DeleteConfig") Then
            DeleteConfiguration(Integer.Parse(e.CommandArgument.ToString()))
            LblWarningMessage.Text = ""
        End If
    End Sub

    Protected Sub GvBCCConfiguration_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvBCCConfiguration.PageIndexChanging
        GvBCCConfiguration.PageIndex = e.NewPageIndex
        BindData(GetDdlEmailDocTypeValue(DdlEmailDocType_Short))
    End Sub

    Protected Sub GvUsers_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvUsers.RowCommand
        If e.CommandName.Equals("AddUser") Then
            AddBCCConfiguration(Integer.Parse(e.CommandArgument.ToString()), DdlEmailDocType.SelectedValue)

        End If
    End Sub

    Protected Sub GvUser_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvUsers.PageIndexChanging
        GvUsers.PageIndex = e.NewPageIndex
        Dim roleid As Integer = 0
        If DdlRole.SelectedIndex > 0 Then
            roleid = Integer.Parse(DdlRole.SelectedValue)
        End If
        BindUser(TxtSearchName.Text, roleid, DdlEmailDocType.SelectedValue)
    End Sub

#Region "Custom Methods"
    Private Function GetDdlEmailDocTypeValue(ByVal ddl As DropDownList) As String
        If ddl.SelectedValue.Equals("0") Then
            Return String.Empty
        Else
            Return ddl.SelectedValue
        End If
    End Function

    Private Sub BindData(ByVal shorttype As String)
        GvBCCConfiguration.DataSource = controller.GetBCCConfiguration(shorttype)
        GvBCCConfiguration.DataBind()
    End Sub

    Private Sub AddBCCConfiguration(ByVal userid As Integer, ByVal emaildoctype As String)
        If controller.BCCConfiguration_I(emaildoctype, userid, CommonSite.UserName) = True Then
            BindData(GetDdlEmailDocTypeValue(DdlEmailDocType_Short))
            Dim roleid As Integer = 0
            If DdlRole.SelectedIndex > 0 Then
                roleid = Integer.Parse(DdlRole.SelectedValue)
            End If
            BindUser(TxtSearchName.Text, roleid, DdlEmailDocType.SelectedValue)
            LblWarningMessage.Text = "BCC Configuration successfully added"
            LblWarningMessage.Visible = True
            LblWarningMessage.Font.Italic = True
            LblWarningMessage.ForeColor = Drawing.Color.Green
        Else
            LblWarningMessage.Text = "BCC Configuration added failure, please try again"
            LblWarningMessage.Visible = True
            LblWarningMessage.Font.Italic = True
            LblWarningMessage.ForeColor = Drawing.Color.Red
        End If
        LblGridviewWarningMessage.Text = ""
    End Sub

    Private Sub DeleteConfiguration(ByVal sno As Integer)
        If controller.BCCConfiguration_D(sno) = True Then
            BindData(GetDdlEmailDocTypeValue(DdlEmailDocType_Short))
            Dim roleid As Integer = 0
            If DdlRole.SelectedIndex > 0 Then
                roleid = Integer.Parse(DdlRole.SelectedValue)
            End If
            BindUser(TxtSearchName.Text, roleid, DdlEmailDocType.SelectedValue)
            LblGridviewWarningMessage.Text = "BCC Configuration successfully deleted"
            LblGridviewWarningMessage.ForeColor = Drawing.Color.Green
            LblGridviewWarningMessage.Font.Italic = True
            LblGridviewWarningMessage.Visible = True
        Else
            LblGridviewWarningMessage.Text = "BCC Configuration deleted failure, please try again!"
            LblGridviewWarningMessage.ForeColor = Drawing.Color.Red
            LblGridviewWarningMessage.Font.Italic = True
            LblGridviewWarningMessage.Visible = True
        End If
    End Sub

    Private Sub BindUser(ByVal fullname As String, ByVal roleid As Integer, ByVal emaildoctype As String)
        GvUsers.DataSource = controller.GetUserMappingBCCConfig(fullname, roleid, emaildoctype)
        GvUsers.DataBind()
    End Sub

    Private Sub BindRoles(ByVal ddl As DropDownList)
        ddl.DataSource = controller.GetRoles()
        ddl.DataTextField = "rolename"
        ddl.DataValueField = "roleid"
        ddl.DataBind()

        ddl.Items.Insert(0, "--select--")
    End Sub

    Private Sub upgATPReport_Init(sender As Object, e As EventArgs) Handles upgATPReport.Init
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "adjustSize();", True)
    End Sub

    Private Sub upgATPReport_Load(sender As Object, e As EventArgs) Handles upgATPReport.Load
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "adjustSize();", True)
    End Sub
#End Region

End Class
