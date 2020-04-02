Partial Class frmmenu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("role") = "sys" Then
            TreeView1.Visible = True
            TreeView2.Visible = False
            TreeView3.Visible = False
            TreeView4.Visible = False
        ElseIf Session("role") = "site" Then
            TreeView1.Visible = False
            TreeView2.Visible = False
            TreeView3.Visible = False
            TreeView4.Visible = True
        ElseIf Session("role") = "SubAdmin" Then
            TreeView1.Visible = False
            TreeView2.Visible = False
            TreeView3.Visible = True
            TreeView4.Visible = False
        Else
            TreeView1.Visible = False
            TreeView2.Visible = False
            TreeView3.Visible = False
            TreeView4.Visible = True
        End If

    End Sub
End Class
