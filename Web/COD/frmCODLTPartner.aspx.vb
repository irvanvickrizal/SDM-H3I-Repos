Imports System.Data

Partial Class COD_frmCODLTPartner
    Inherits System.Web.UI.Page

    Dim controller As New ScopeController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindData(0)
            BindActivities(DdlActivity, 0)
        End If
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        Dim datavalidation As Boolean = True
        Dim strErrMessage As String = String.Empty
        If DdlActivity.SelectedIndex < 1 And datavalidation = True Then
            datavalidation = False
            strErrMessage = "Please select Type of work First!"
        End If
        If DdlDScope.SelectedIndex < 0 And datavalidation = True Then
            datavalidation = False
            strErrMessage = "Please select Detail Scope First!"
        End If
        If String.IsNullOrEmpty(TxtLTValue.Value) And datavalidation = True Then
            datavalidation = False
            strErrMessage = "Please fill LT Value field!"
        End If

        If datavalidation = True Then
            If controller.AvailableCheckingBaseOnScopeActivity(Integer.Parse(DdlDScope.SelectedValue), Integer.Parse(DdlActivity.SelectedValue)) = False Then
                datavalidation = False
                strErrMessage = "Scope " & DdlDScope.SelectedItem.Text & " with " & DdlActivity.SelectedItem.Text & " already exist"
            End If
        End If
        
        
        ErrorMessagePanel(True)
        If datavalidation = True Then
           
            Dim info As New DetailScopeInfo
            info.DScopeId = Integer.Parse(DdlDScope.SelectedValue)
            info.ScopeLTPartnerInfo.LTID = 0
            info.ScopeLTPartnerInfo.LTValue = TxtLTValue.Value
            info.ScopeLTPartnerInfo.CMAInfo.ModifiedUser = CommonSite.UserName
            info.ScopeLTPartnerInfo.ActivityInfo.ActivityId = Integer.Parse(DdlActivity.SelectedValue)
            LTPartner_IU(info)
        Else
            LblErrMessage.Text = strErrMessage
            LblErrMessage.ForeColor = Drawing.Color.Red
            LblErrMessage.Font.Italic = True
        End If

    End Sub

    Protected Sub DdlActivities_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlActivity.SelectedIndexChanged
        If DdlActivity.SelectedIndex > 0 Then
            BindMasterScope(ddlMasterScope, 1)
            BindDetailScope(DdlDScope, 0, 0)
        Else
            BindMasterScope(ddlMasterScope, 0)
            BindDetailScope(DdlDScope, 0, 0)
        End If
    End Sub

    Protected Sub ddlMasterScope_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMasterScope.SelectedIndexChanged
        If ddlMasterScope.SelectedIndex > 0 Then
            BindDetailScope(DdlDScope, Integer.Parse(ddlMasterScope.SelectedValue), Integer.Parse(DdlActivity.SelectedValue))
        Else
            BindDetailScope(DdlDScope, 0, 0)
        End If
    End Sub

#Region "Gridview event"
    Protected Sub GvScopeLTPartner_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvScopeLTPartner.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim desc As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DScope_Name"))
            'Dim lnkbtnresult As ImageButton = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
            'Dim Ddl As DropDownList = CType(e.Row.FindControl("DdlScope"), DropDownList)
            'Dim lblScopeId As Label = CType(e.Row.FindControl("LblScopeId"), Label)
            'If Ddl IsNot Nothing Then
            '    BindScopeDDL(Ddl, lblScopeId.Text)
            'End If

            'If Not lnkbtnresult Is Nothing Then
            '    lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + desc + "')")
            'End If
            Dim DdlActivitiesEdit As DropDownList = CType(e.Row.FindControl("DdlActivitiesEdit"), DropDownList)
            Dim LblActivityId As Label = CType(e.Row.FindControl("LblActivityIdEdit"), Label)
            If Not DdlActivitiesEdit Is Nothing And Not LblActivityId Is Nothing Then
                BindActivities(DdlActivitiesEdit, Integer.Parse(LblActivityId.Text))
            End If
        End If
    End Sub

    Protected Sub GvScopeLTPartner_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim LTID As Integer = Integer.Parse(GvScopeLTPartner.DataKeys(e.RowIndex).Values("LT_ID").ToString())
        LTPartner_D(LTID)
    End Sub

    Protected Sub GvScopeLTPartner_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim LTID As Integer = Integer.Parse(GvScopeLTPartner.DataKeys(e.RowIndex).Value.ToString())
        Dim TxtLeadTimeEdit As TextBox = CType(GvScopeLTPartner.Rows(e.RowIndex).FindControl("TxtLeadTimeEdit"), TextBox)
        Dim LblDScopeID As Label = CType(GvScopeLTPartner.Rows(e.RowIndex).FindControl("LblDScopeID"), Label)
        Dim DdlActivitiesEdit As DropDownList = CType(GvScopeLTPartner.Rows(e.RowIndex).FindControl("DdlActivitiesEdit"), DropDownList)
        Dim LblActivityIdEdit As Label = CType(GvScopeLTPartner.Rows(e.RowIndex).FindControl("LblActivityIdEdit"), Label)

        If DdlActivitiesEdit.SelectedIndex > 0 Then
            If Integer.Parse(LblActivityIdEdit.Text) = Integer.Parse(DdlActivitiesEdit.SelectedValue) Then
                GvScopeLTPartner.EditIndex = -1
                Dim info As New DetailScopeInfo
                info.DScopeId = Integer.Parse(LblDScopeID.Text)
                info.ScopeLTPartnerInfo.LTID = LTID
                info.ScopeLTPartnerInfo.LTValue = TxtLeadTimeEdit.Text
                info.ScopeLTPartnerInfo.CMAInfo.ModifiedUser = CommonSite.UserName
                info.ScopeLTPartnerInfo.ActivityInfo.ActivityId = Integer.Parse(DdlActivitiesEdit.SelectedValue)
                LTPartner_IU(info)
            Else
                If controller.AvailableCheckingBaseOnScopeActivity(Integer.Parse(LblDScopeID.Text), Integer.Parse(DdlActivitiesEdit.SelectedValue)) = True Then
                    GvScopeLTPartner.EditIndex = -1
                    Dim info As New DetailScopeInfo
                    info.DScopeId = Integer.Parse(LblDScopeID.Text)
                    info.ScopeLTPartnerInfo.LTID = LTID
                    info.ScopeLTPartnerInfo.LTValue = TxtLeadTimeEdit.Text
                    info.ScopeLTPartnerInfo.CMAInfo.ModifiedUser = CommonSite.UserName
                    info.ScopeLTPartnerInfo.ActivityInfo.ActivityId = Integer.Parse(DdlActivitiesEdit.SelectedValue)
                    LTPartner_IU(info)
                Else
                    LblErrMessageGv.Text = "Data Already Exist, please try again"
                    LblErrMessageGv.Visible = True
                    LblErrMessageGv.ForeColor = Drawing.Color.Red
                    LblErrMessageGv.Font.Italic = True
                End If
            End If
        Else
            LblErrMessageGv.Text = "Please select Type of Work!"
            LblErrMessageGv.Visible = True
            LblErrMessageGv.ForeColor = Drawing.Color.Red
            LblErrMessageGv.Font.Italic = True
        End If
       
    End Sub

    Protected Sub GvScopeLTPartner_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        GvScopeLTPartner.EditIndex = -1
        Dim grpScopeid As Integer = 0
        BindData(0)
    End Sub

    Protected Sub GvScopeLTPartner_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        GvScopeLTPartner.EditIndex = e.NewEditIndex
        BindData(0)
    End Sub
#End Region

#Region "Custom Methods"
    Private Sub BindData(ByVal dscopeid As Integer)
        Dim ds As DataSet = controller.GetLTPartner_DS(dscopeid)
        If ds.Tables.Count > 0 Then
            GvScopeLTPartner.DataSource = controller.GetLTPartner_DS(dscopeid)
            GvScopeLTPartner.DataBind()
        Else
            GvScopeLTPartner.DataSource = Nothing
            GvScopeLTPartner.DataBind()
        End If
    End Sub

    Private Sub LTPartner_IU(ByVal info As DetailScopeInfo)

        If controller.LTPartner_IU(info) = True Then
            If info.ScopeLTPartnerInfo.LTID = 0 Then
                ErrorMessagePanel(True)
                LblErrMessage.Text = "Data Successfully Added"
                LblErrMessage.ForeColor = Drawing.Color.Green
                LblErrMessage.Font.Italic = True
            Else
                ErrorMessagePanel(False)
                LblErrMessageGv.Text = "Data Successfully Updated"
                LblErrMessageGv.ForeColor = Drawing.Color.Green
                LblErrMessageGv.Font.Italic = True
            End If
            BindData(0)
            ClearForm()
        Else
            ErrorMessagePanel(True)
            LblErrMessage.Text = "Data Failed to Added"
            LblErrMessage.ForeColor = Drawing.Color.Red
            LblErrMessage.Font.Italic = True
        End If
    End Sub

    Private Sub LTPartner_D(ByVal ltid As Integer)
        ErrorMessagePanel(False)
        If controller.LTPartner_D(ltid) = True Then
            LblErrMessageGv.Text = "Data Successfully Deleted"
            LblErrMessageGv.ForeColor = Drawing.Color.Green
            LblErrMessageGv.Font.Italic = True
            BindData(0)
        Else
            LblErrMessageGv.Text = "Data Fail Deleted"
            LblErrMessageGv.ForeColor = Drawing.Color.Red
            LblErrMessageGv.Font.Italic = True
        End If
    End Sub

    Private Sub BindMasterScope(ByVal ddl As DropDownList, ByVal activityid As Integer)
        If activityid > 0 Then
            ddl.DataSource = controller.GetScopeMaster(False)
            ddl.DataTextField = "ScopeName"
            ddl.DataValueField = "ScopeId"
            ddl.DataBind()
            ddl.Items.Insert(0, "-- Select Scope --")
        Else
            ddl.Items.Clear()
            'ddl.DataSource = Nothing
            'ddl.DataBind()
            'ddl.Items.Insert(0, "-- Select Scope --")
        End If
        
    End Sub

    Private Sub BindDetailScope(ByVal ddl As DropDownList, ByVal scopeid As Integer, ByVal activityid As Integer)
        If activityid > 0 Then
            ddl.DataSource = controller.GetListDetailScopesBaseLTPartner(False, scopeid, activityid)
            ddl.DataTextField = "DscopeName"
            ddl.DataValueField = "DScopeId"
            ddl.DataBind()
            ddl.Items.Insert(0, "-- select detail scope --")
        Else
            ddl.Items.Clear()
            'ddl.Items.Insert(0, "-- select detail scope --")
        End If
        
    End Sub

    Private Sub BindActivities(ByVal ddl As DropDownList, ByVal activityid As Integer)
        ddl.DataSource = New CODActivityController().GetCODActivities(False)
        ddl.DataTextField = "ActivityName"
        ddl.DataValueField = "ActivityId"
        ddl.DataBind()

        ddl.Items.Insert(0, "--select--")

        If activityid > 0 Then
            ddl.SelectedValue = Convert.ToString(activityid)
        End If
    End Sub

    Private Sub ClearForm()
        BindMasterScope(ddlMasterScope, 0)
        BindDetailScope(DdlDScope, 0, 0)
        BindActivities(DdlActivity, 0)
        TxtLTValue.Value = ""
    End Sub

    Private Sub ErrorMessagePanel(ByVal isVisible As Boolean)
        LblErrMessage.Visible = isVisible
        LblErrMessageGv.Visible = Not isVisible
    End Sub
#End Region
End Class
