Imports Common
Imports System.Data
Imports Entities
Imports BusinessLogic
Imports Common_NSNFramework
Partial Class COD_frmCODDocumentUserAuthorized
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim dtdoc As New DataTable
    Dim objBO As New BOUserType
    Dim objutil As New DBUtil
    Dim docid As Integer
    Dim objutil_nsn As New DBUtils_NSN
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        If Page.IsPostBack = False Then
            If Not Request.QueryString("id") Is Nothing Then
                MvPanelDocAuth.SetActiveView(VwDocUpdated)
                docid = Request.QueryString("id")
                BindGroup()
                dtdoc = objutil.ExeQueryDT("select top 1 DocName from coddoc where doc_id =" & docid, "ddd")
                LblDocumentName.Text = dtdoc.Rows(0).Item(0)
            Else
                LblErrorMessage.Text = "Please re-open this form. In seems like can't load data completely or you don't have any permissions or session timeout"
                MvPanelDocAuth.SetActiveView(VwMessagePanel)
            End If
        End If
    End Sub

    Protected Sub BtnSaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Dim strResult = SaveAuthorized()
        MvPanelDocAuth.SetActiveView(VwMessagePanel)
        If (strResult.Equals("succeed")) Then
            LblErrorMessage.Text = "Data has been updated successfully"
        Else
            LblErrorMessage.Text = "Failed Message :" & strResult
        End If
    End Sub

    Protected Sub LbtSaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSave.Click
        Dim strResult = SaveAuthorized()
        MvPanelDocAuth.SetActiveView(VwMessagePanel)
        If (strResult.Equals("succeed")) Then
            LblErrorMessage.Text = "Data has been updated successfully"
        Else
            LblErrorMessage.Text = "Failed Message :" & strResult
        End If
    End Sub


    'Protected Sub DLGroupAuthorizedDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DLGroupAuthorized.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim chkChecked As CheckBox = CType(e.Item.FindControl("ChkChecked"), CheckBox)
    '        Dim grpId As Label = CType(e.Item.FindControl("LblGrpId"), Label)
    '        chkChecked.Checked = IsAuthorized(Integer.Parse(grpId.Text))
    '        BindRoles(Integer.Parse(grpId.Text), DirectCast(e.Item.FindControl("GvRoles"), GridView))
    '    End If
    'End Sub

    Protected Sub RpDocAuthorizedDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RpDocAuthorized.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim chkChecked As CheckBox = CType(e.Item.FindControl("ChkChecked"), CheckBox)
            Dim grpId As Label = CType(e.Item.FindControl("LblGrpId"), Label)
            'chkChecked.Checked = IsAuthorized(Integer.Parse(grpId.Text))
            BindRoles(Integer.Parse(grpId.Text), DirectCast(e.Item.FindControl("GvRoles"), GridView))
        End If
    End Sub


#Region "Custom Methods"
    Private Sub BindGroup()
        dt = objBO.uspTUserTypeLD(, "GrpCode", "", "")
        RpDocAuthorized.DataSource = dt
        RpDocAuthorized.DataBind()
    End Sub

    Private Sub BindRoles(ByVal grpid As Integer, ByVal dlroles As GridView)
        Dim dtroles As DataTable = Nothing
        Dim str As String = "select * from trole where grpid =" & grpid & " and rstatus = 2 order by RoleDesc asc"
        dtroles = objutil.ExeQueryDT(str, "tbrolesbygrouping")
        dlroles.DataSource = dtroles
        dlroles.DataBind()
        For Each roles As GridViewRow In dlroles.Rows
            Dim role As Label = DirectCast(roles.FindControl("LblRoleId"), Label)
            Dim chk As CheckBox = DirectCast(roles.FindControl("ChkChecked"), CheckBox)
            If DocCheckAuthorized(Integer.Parse(role.Text)) = True Then
                chk.Checked = True
            End If
        Next
    End Sub

    Private Function SaveAuthorized() As String
        If Not Request.QueryString("id") Is Nothing Then
            docid = Integer.Parse(Request.QueryString("id"))
        End If
        Dim rtnMessage As String = "succeed"
        Dim countAuth As Integer = objutil.ExeQueryScalar("select count(*) from CODDOC_Auth where doc_id=" & docid)
        Try
            If countAuth > 0 Then
                objutil_nsn.DeleteDocAuthByDocId(docid)
            End If
            For Each Rpgroup As RepeaterItem In RpDocAuthorized.Items
                Dim lblgrpid As Label = DirectCast(Rpgroup.FindControl("LblGrpId"), Label)
                Dim gvRoles As GridView = DirectCast(Rpgroup.FindControl("GvRoles"), GridView)
                For Each roles As GridViewRow In gvRoles.Rows
                    Dim chk As CheckBox = DirectCast(roles.FindControl("chkChecked"), CheckBox)
                    If chk.Checked = True Then
                        Dim role As Label = DirectCast(roles.FindControl("LblRoleId"), Label)
                        If Not String.IsNullOrEmpty(role.Text) Then
                            Dim authinfo As New DocAuthorizedInfo
                            authinfo.DocId = docid
                            authinfo.GrpId = Integer.Parse(lblgrpid.Text)
                            authinfo.RoleId = Integer.Parse(role.Text)
                            authinfo.CreatedDate = DateTime.Now
                            authinfo.CreatedBy = CommonSite.UserName
                            objutil_nsn.InsertDocAuth(authinfo)
                        End If
                    End If
                Next
            Next

            'For Each dli As DataListItem In DLGroupAuthorized.Items
            '    Dim chk As CheckBox = DirectCast(dli.FindControl("chkChecked"), CheckBox)
            '    If chk.Checked = True Then
            '        Dim lblgrpid As Label = DirectCast(dli.FindControl("LblGrpId"), Label)
            '        If Not lblgrpid Is Nothing Then
            '            Dim authinfo As New DocAuthorizedInfo
            '            authinfo.DocId = docid
            '            authinfo.GrpId = Integer.Parse(lblgrpid.Text)
            '            authinfo.CreatedDate = DateTime.Now
            '            authinfo.CreatedBy = CommonSite.UserName
            '            objutil_nsn.InsertDocAuth(authinfo)
            '        End If
            '    End If
            'Next
        Catch ex As Exception
            rtnMessage = ex.Message.ToString()
        End Try

        Return rtnMessage
    End Function

    Private Function DocCheckAuthorized(ByVal roleid As Integer) As Boolean
        If Not Request.QueryString("id") Is Nothing Then
            docid = Integer.Parse(Request.QueryString("id"))
        End If
        Dim isBlocked As Boolean = False
        Dim count As Integer = objutil.ExeQueryScalar("select count(*) from CODDOC_Auth where role_id=" & roleid & "and doc_id=" & docid)
        If count > 0 Then
            isBlocked = True
        End If
        Return isBlocked
    End Function

    Private Function SaveRoleAuthorized(ByVal roleid As Integer) As Boolean
        Dim isSucceed As Boolean
        If Not Request.QueryString("id") Is Nothing Then
            docid = Integer.Parse(Request.QueryString("id"))
        End If

        Return isSucceed
    End Function
#End Region
End Class
