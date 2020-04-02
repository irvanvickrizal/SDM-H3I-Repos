Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class USR_frmChangeProfile
    Inherits System.Web.UI.Page
    Dim objET As New ETEBASTUsers
    Dim objBO As New BOUserLD
    Dim dt As New DataTable
    Dim objBOS As New BOUserSetup
    Dim objUtil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnUpdate.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        If Not IsPostBack Then
            trUserTypeName.Visible = False
            binddata()
        End If
    End Sub
    Sub binddata()
        dt = New DataTable
        dt = objBO.uspUsrProfileD(Session("User_Id"))
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                lblUser.InnerText = .Item("UsrType").ToString
                If .Item("UsrType").ToString <> "NSN" Then
                    trUserTypeName.Visible = True
                    lblUserTypeDesc.InnerText = .Item("UsrTypeDesc").ToString
                End If
                lblRole.InnerText = .Item("ROLEDESC").ToString
                txtEpm.Value = .Item("EPM_ID").ToString
                txtName.Value = .Item("NAME").ToString
                lblLogin.InnerText = .Item("USRLOGIN").ToString
                txtEmail.Value = .Item("EMAIL").ToString
                txtPhnumber.Value = .Item("PHONENO").ToString
                txtSignTitle.Value = .Item("signtitle").ToString
            End With
        End If
    End Sub
    Sub saveProfile()
        objET.USR_ID = Session("User_Id")
        objET.Name = txtName.Value
        objET.EPM_ID = txtEpm.Value
        objET.Email = txtEmail.Value
        objET.PhoneNo = txtPhnumber.Value
        objET.AT.RStatus = Constants.STATUS_ACTIVE
        objET.AT.LMBY = Session("User_Name")
    End Sub
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        'saveProfile()
        Dim i As Integer
        Dim sqlstr As String
        'i = objBOS.uspChangeProfile(objET)
        sqlstr = "EXEC uspChangeProfile " & Session("User_Id") & ",'" & txtName.Value & "','" & txtEpm.Value & "','" & txtEmail.Value & "','" & txtPhnumber.Value & "','" & txtSignTitle.Value & "'," & Constants.STATUS_ACTIVE & ",'" & Session("User_Name") & "'"
        i = objUtil.ExeQueryScalar(sqlstr)
        If i = 1 Then
            Response.Write("<script>alert('Data Updated Successfully')</script>")
            Session("User_Name") = txtName.Value
            Response.Redirect("../Main.aspx")
        ElseIf i = -1 Then
            Response.Write("<script>alert('EPM Id already exists')</script>")
        Else
            Response.Write("<script>alert('Error While Doing Transaction')</script>")
        End If
    End Sub
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("../Main.aspx")
    End Sub
End Class
