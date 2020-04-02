Imports BusinessLogic
Imports Entities
Imports Common
Partial Class CR_frmAUser
    Inherits System.Web.UI.Page
    Dim objbo As New BOMOM
    Dim objdo As New ETMomUsers
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim retval As String
        Filldetails()
        objbo.uspMomParticipantsIU(objdo)
        retval = txtAUsers.Value
        Response.Write("<script>window.opener.document.getElementById('hdnUsersId').value = '" + retval + "';window.opener.document.form1.submit();window.close();</script>")
    End Sub
    Sub Filldetails()
        objdo.MOM_Id = Session("Mom_Id")
        objdo.UsrName = txtAUsers.Value.Replace("'", "''")
        objdo.UsrEmail = txtAEmail.Value.Replace("'", "''")
        objdo.UsrType = Constants._UsrType_NonSystem
        objdo.AT.RStatus = Constants.STATUS_ACTIVE
        objdo.AT.LMBY = Session("User_Name")
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Write("<script>window.close();</script>")
    End Sub
End Class
