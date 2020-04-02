Imports System.Data
Imports BusinessLogic
Imports Common
Partial Class PO_PoNameChange
    Inherits System.Web.UI.Page
    Dim objbod As New BODDLs
    Dim objutil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack = True Then
            bindpo()
        End If
    End Sub
    Sub bindpo()
        objbod.fillDDL(ddlPO, "PoNo", True, "--Please Select--")
    End Sub
    Protected Sub btnupdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If txtponame.Text <> "" Then
            objutil.ExeNonQuery("update podetails set poname='" & txtponame.Text & "' where pono='" & ddlPO.SelectedItem.Text & "'")
            Response.Write("<script>alert('POName Changed Successfully')</script>")
            txtponame.Text = ""
            ddlPO.SelectedIndex = -1
        Else
            Response.Write("<script>alert('Please Keyin POName')</script>")


        End If
        
    End Sub

    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPO.SelectedIndexChanged
        txtponame.Text = ""
        txtponame.Text = objutil.ExeQueryScalarString("select poname from podetails where pono='" & ddlPO.SelectedItem.Text & "'")
    End Sub
End Class
