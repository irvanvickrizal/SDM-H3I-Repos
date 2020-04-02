
Partial Class COD_frmCODInjectionType
    Inherits System.Web.UI.Page

    Dim controller As New CODInjectionController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'LbtSave.Attributes.Add("onclick", "javascript:return ConfirmSave();")
        If Not Page.IsPostBack Then
            BindData(False)
        End If
    End Sub


    Protected Sub LbtSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSave.Click
        Dim info As New CODInjectionTypeInfo
        info.InjectionId = 0
        info.InjectionName = TxtInjectionName.Text
        info.InjectionDesc = TxtInjectionDesc.Text
        info.ParentDocId = Integer.Parse(DdlParentDoc.SelectedValue)
        info.LMBY = CommonSite.UserId
        info.IsDeleted = False
        Dim messageSaveData As String = SaveData(info)
        If messageSaveData.ToLower().Equals("succeed") Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "SucceedSave();", True)
            BindData(False)
            TxtInjectionName.Text = ""
            TxtInjectionDesc.Text = ""
            DdlParentDoc.SelectedValue = "0"
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "ErrorSave('" + messageSaveData + "');", True)
        End If
    End Sub

    Protected Sub GvInjectionType_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvInjectionType.RowCommand
        If e.CommandName.Equals("deleteinjection") Then
            Dim strMessage As String = DeleteData(Integer.Parse(e.CommandArgument.ToString()))
            If strMessage.ToLower().Equals("succeed") Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "SucceedDelete();", True)
                BindData(False)
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "ErrorSave('" + strMessage + "');", True)
            End If
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal isDeleted As Boolean)
        GvInjectionType.DataSource = controller.GetInjections(isDeleted)
        GvInjectionType.DataBind()
    End Sub

    Private Sub upgATPReport_Init(sender As Object, e As EventArgs) Handles upgATPReport.Init
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "adjustSize();", True)
    End Sub

    Private Sub upgATPReport_Load(sender As Object, e As EventArgs) Handles upgATPReport.Load
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "adjustSize();", True)
    End Sub

    Private Sub GvInjectionType_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvInjectionType.PageIndexChanging
        GvInjectionType.PageIndex = e.NewPageIndex
        BindData(False)
    End Sub

    Private Function SaveData(ByVal info As CODInjectionTypeInfo) As String
        Return controller.Injection_IU(info)
    End Function

    Private Function DeleteData(ByVal injectionid As Integer) As String
        Return controller.Injection_D(injectionid)
    End Function

#End Region

End Class
