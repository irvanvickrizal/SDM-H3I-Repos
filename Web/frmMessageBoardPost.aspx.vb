Imports Entities
Imports BusinessLogic
Imports Common
Imports System.Data
Partial Class frmMessageBoardPost
    Inherits System.Web.UI.Page
    Dim objET As New ETMessageBoard
    Dim objBO As New BOMessageBoard
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnPost.Attributes.Add("onClick", "javascript:return checkIsEmpty();")
        If Page.IsPostBack = False Then
            SaveData()
        End If
    End Sub
    Sub SaveData()
        objET.Title = txtTitle.Text
        objET.Message = txtMessage.Text
        objET.PostedBy = Session("User_Name")
        objET.AT.RStatus = Constants.STATUS_ACTIVE
        objET.AT.LMBY = Session("User_Name")
    End Sub

    Protected Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        SaveData()
        BOcommon.result(objBO.uspMessageBoardIU(objET), True, "frmMessageBoardPost.aspx", "", Constants._INSERT)
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        txtTitle.Text = ""
        txtMessage.Text = ""
    End Sub
End Class
