Imports System.Data

Partial Class COD_frmCODBAUTSIT
    Inherits System.Web.UI.Page

    Dim controller As New ScopeController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
            BindMasterScope(ddlMasterScope)
        End If
    End Sub


    Protected Sub ddlMasterScope_SelectIndexChanging(ByVal sender As Object, ByVal e As EventArgs) Handles ddlMasterScope.SelectedIndexChanged
        If ddlMasterScope.SelectedIndex > 0 Then
            BindDetailScope(DdlDScope, Integer.Parse(ddlMasterScope.SelectedValue))
        Else
            BindDetailScope(DdlDScope, 0)
        End If
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnAdd.Click
        If DdlDScope.SelectedIndex > 0 Then
            Dim info As New DetailScopeInfo
            info.ScopeBAUTSITInfo.MSIT = 0
            info.ScopeBAUTSITInfo.DocDependencyInfo.DocId = Integer.Parse(DdlDocs.SelectedValue)
            info.DScopeId = Integer.Parse(DdlDScope.SelectedValue)
            info.ScopeBAUTSITInfo.CMAInfo.LMBY = CommonSite.UserName
            BAUTSIT_IU(info)
        Else
            ErrorMessageDisplay(True)
            LblErrMessage.Text = "Please select Detail scope First!"
            LblErrMessage.ForeColor = Drawing.Color.Red
            LblErrMessage.Font.Italic = True
        End If
    End Sub

#Region "Gridview event"
    Protected Sub GvScopeGrouping_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvScopeBAUTSIT.RowDataBound
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

            Dim DdlApprovedDoc As DropDownList = CType(e.Row.FindControl("DdlApprovedDoc"), DropDownList)
            Dim docapprovedid As Integer = Integer.Parse(DataBinder.Eval(e.Row.DataItem, "approved_doc_id"))

            If DdlApprovedDoc IsNot Nothing Then
                DdlApprovedDoc.SelectedValue = Convert.ToString(docapprovedid)
            End If

        End If
    End Sub

    Protected Sub GvScopeBAUTSIT_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim msitid As Integer = Integer.Parse(GvScopeBAUTSIT.DataKeys(e.RowIndex).Values("MSIT_Id").ToString())
        BAUTSIT_D(msitid)
        BindData()
        ErrorMessageDisplay(False)
        LblErrMessageGv.Text = "Data Successfully Deleted"
        LblErrMessageGv.ForeColor = Drawing.Color.Green
        LblErrMessageGv.Font.Italic = True
    End Sub

    Protected Sub GvScopeBAUTSIT_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim msitid As Integer = Integer.Parse(GvScopeBAUTSIT.DataKeys(e.RowIndex).Value.ToString())
        Dim ChkEditChecked As CheckBox = CType(GvScopeBAUTSIT.Rows(e.RowIndex).FindControl("ChkEditChecked"), CheckBox)
        Dim LblDScopeID As Label = CType(GvScopeBAUTSIT.Rows(e.RowIndex).FindControl("LblDScopeID"), Label)
        Dim DdlApprovedDocEdit As DropDownList = CType(GvScopeBAUTSIT.Rows(e.RowIndex).FindControl("DdlApprovedDocEdit"), DropDownList)
        GvScopeBAUTSIT.EditIndex = -1
        Dim info As New DetailScopeInfo
        info.ScopeBAUTSITInfo.MSIT = msitid
        info.ScopeBAUTSITInfo.DocDependencyInfo.DocId = Integer.Parse(DdlApprovedDocEdit.SelectedValue)
        info.DScopeId = Integer.Parse(LblDScopeID.Text)
        ErrorMessageDisplay(False)
        If controller.MasterBAUTSIT_IU(info) = True Then
            LblErrMessageGv.Text = "Data Successfully Updated"
            LblErrMessageGv.ForeColor = Drawing.Color.Green
            LblErrMessageGv.Font.Italic = True
            BindData()
        Else
            LblErrMessageGv.Text = "Data Fail Updated"
            LblErrMessageGv.ForeColor = Drawing.Color.Red
            LblErrMessageGv.Font.Italic = True
        End If
    End Sub

    Protected Sub GvScopeBAUTSIT_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        GvScopeBAUTSIT.EditIndex = -1
        Dim grpScopeid As Integer = 0
        BindData()
    End Sub

    Protected Sub GvScopeBAUTSIT_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        GvScopeBAUTSIT.EditIndex = e.NewEditIndex
        BindData()
    End Sub


#End Region

#Region "Custom methods"

    Private Sub BindData()
        Dim ds As DataSet = controller.GetMasterBAUTSIT_DS(0)
        If ds.Tables(0).Rows.Count > 0 Then
            GvScopeBAUTSIT.DataSource = ds
            GvScopeBAUTSIT.DataBind()
        Else
            GvScopeBAUTSIT.DataSource = Nothing
            GvScopeBAUTSIT.DataBind()
        End If
    End Sub
    Private Sub BindMasterScope(ByVal ddl As DropDownList)
        ddl.DataSource = controller.GetScopeMaster(False)
        ddl.DataTextField = "ScopeName"
        ddl.DataValueField = "ScopeId"
        ddl.DataBind()
        ddl.Items.Insert(0, "-- Select Scope --")
    End Sub

    Private Sub BindDetailScope(ByVal ddl As DropDownList, ByVal scopeid As Integer)
        ddl.DataSource = controller.GetListDetailScopesBaseBAUTSIT(False, scopeid)
        ddl.DataTextField = "DscopeName"
        ddl.DataValueField = "DScopeId"
        ddl.DataBind()
        ddl.Items.Insert(0, "-- select detail scope --")
    End Sub

    Private Sub BAUTSIT_IU(ByVal info As DetailScopeInfo)
        If controller.MasterBAUTSIT_IU(info) = True Then
            ClearForm()
            If info.ScopeBAUTSITInfo.MSIT = 0 Then
                LblErrMessage.Text = "Data Successfully Added"
            Else
                LblErrMessage.Text = "Data Successfully Updated"
            End If
            LblErrMessage.ForeColor = Drawing.Color.Green
            LblErrMessage.Font.Italic = True
            BindData()
        Else
            LblErrMessage.Text = "Data fail to Added"
            LblErrMessage.ForeColor = Drawing.Color.Red
            LblErrMessage.Font.Italic = True
        End If
    End Sub

    Private Sub BAUTSIT_D(ByVal msitid As Integer)
        controller.MasterBAUTSIT_D(msitid)
        LblErrMessage.Text = "Data Successfully Added"
        LblErrMessage.ForeColor = Drawing.Color.Green
        LblErrMessage.Font.Italic = True
    End Sub

    Private Sub ErrorMessageDisplay(ByVal isVisible As Boolean)
        LblErrMessage.Visible = isVisible
        LblErrMessageGv.Visible = Not isVisible
    End Sub

    Private Sub ClearForm()
        BindMasterScope(ddlMasterScope)
        BindDetailScope(DdlDScope, 0)
        DdlDocs.SelectedValue = "0"
    End Sub

    Public Function GetStatus(ByVal str As String) As Boolean
        If String.IsNullOrEmpty(str) Then
            Return False
        Else
            If str.Equals("1") Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

#End Region
End Class
