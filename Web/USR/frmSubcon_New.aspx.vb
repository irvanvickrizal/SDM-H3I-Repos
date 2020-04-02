Imports Common
Imports System.Collections.Generic
Imports System.Data

Partial Class USR_frmSubcon_New
    Inherits System.Web.UI.Page

    Dim controller As New PartnerController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub BtnAddClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        If Not String.IsNullOrEmpty(TxtSubconName.Text) Then
            AddUpdateData(0, TxtSubconName.Text, TxtSubconDescription.Text)
            ClearTextField()
        End If
    End Sub

    Protected Sub GvSubcon_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvSubcon.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim desc As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Subcon_Name"))
            Dim lnkbtnresult As ImageButton = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
            If Not lnkbtnresult Is Nothing Then
                lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + desc + "')")
            End If
        End If
    End Sub

    Protected Sub gvsubcon_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim subconid As Integer = Integer.Parse(GvSubcon.DataKeys(e.RowIndex).Values("subcon_Id").ToString())
        BindData()
    End Sub

    Protected Sub gvsubcon_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim subconid As Integer = Integer.Parse(GvSubcon.DataKeys(e.RowIndex).Value.ToString())
        Dim txtGvSubconName As TextBox = CType(GvSubcon.Rows(e.RowIndex).FindControl("TxtGvSubconName"), TextBox)
        Dim txtGvSubconDesc As TextBox = CType(GvSubcon.Rows(e.RowIndex).FindControl("TxtGvSubconDesc"), TextBox)
        GvSubcon.EditIndex = -1
        AddUpdateData(subconid, txtGvSubconName.Text, txtGvSubconDesc.Text)
    End Sub

    Protected Sub gvsubcon_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        GvSubcon.EditIndex = -1
        BindData()
    End Sub

    Protected Sub gvsubcon_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        GvSubcon.EditIndex = e.NewEditIndex
        BindData()
    End Sub

#Region "Custom Methods"

    Private Sub BindData()
        Dim ds As DataSet = PartnerController.GetSubcons_DS(False)
        If (ds.Tables(0).Rows.Count > 0) Then
            GvSubcon.DataSource = ds
            GvSubcon.DataBind()
        Else
            GvSubcon.DataSource = Nothing
            GvSubcon.DataBind()
        End If
    End Sub

    Private Sub AddUpdateData(ByVal subconid As Integer, ByVal subconname As String, ByVal subcondesc As String)
        Dim info As New SubconInfo
        info.SubconId = subconid
        info.SubconName = subconname
        info.SubconDesc = subcondesc
        info.LMBY = CommonSite.UserName
        info.IsDeleted = False
        PartnerController.InsertUpdateSubcon(info)
        BindData()
    End Sub

    Private Sub DeleteData(ByVal subconid As Integer)
        PartnerController.DeleteSubconTemp(subconid)
    End Sub

    Private Sub ClearTextField()
        TxtSubconName.Text = ""
        TxtSubconDescription.Text = ""
    End Sub
#End Region

End Class
