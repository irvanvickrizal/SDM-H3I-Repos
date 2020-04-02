Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data
Imports System.Web.Security
Partial Class USR_frmChangePwd
    Inherits System.Web.UI.Page
    Dim objBo As New BOUserSetup
    Dim objDo As New ETEBASTUsers

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        If Page.IsPostBack = False Then
            If Session("FLogin") = True Then btnCancel.Visible = True
        End If
    End Sub

    Protected Sub btnCancel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.ServerClick
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtPwd.Value <> txtNpwd.Value Then
            saveData()
            Dim i As Integer
            i = objBo.uspCPwd(objDo, FormsAuthentication.HashPasswordForStoringInConfigFile(txtPwd.Value.Replace("'", "''") & "TAKE", "MD5"))
            'i = objBo.uspCPwd(objDo, txtPwd.Value.Replace("'", "''"))

            If i <> 2 Then
                BOcommon.result(i, True, "../Main.aspx", "", Constants._UPDATE)
            Else
                Response.Write("<script language='javascript'>alert('Curent password is wrong');</script>")
            End If
        Else
            Response.Write("<script language='javascript'>alert('New Password must not be the same as current password');</script>")
        End If
    End Sub
    Sub saveData()
        objDo.USR_ID = Session("User_Id")
        objDo.USRPassword = Trim(FormsAuthentication.HashPasswordForStoringInConfigFile(txtNpwd.Value.Replace("'", "''") & "TAKE", "MD5"))
        objDo.USRSQ = ddlQA.SelectedItem.Text.Replace("'", "''")
        objDo.USRSA = txtAns.Value.Replace("'", "''")
        objDo.AT.RStatus = Constants.STATUS_ACTIVE
        objDo.AT.LMBY = Session("User_Name").Replace("'", "''")
    End Sub
   
End Class
