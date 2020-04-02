Imports Common
Imports Entities
Imports BusinessLogic
Imports System.Data
Partial Class frmMenuDisplay
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objdl As New BOMenus
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            MenuTop()
        End If
    End Sub
    Sub MenuTop()
        dt = objdl.uspMenuTop(Session("Role_Id"))
        PopulateNodes(dt, TreeView1.Nodes)
    End Sub
    Private Sub PopulateNodes(ByVal dt As DataTable, ByVal nodes As TreeNodeCollection)
        For Each dr As DataRow In dt.Rows
            Dim tn As New TreeNode
            tn.Text = dr("MNUName").ToString()
            tn.Value = dr("MNU_ID").ToString()
            tn.NavigateUrl = dr("MNULink").ToString()
            tn.Target = "mainframe"
            nodes.Add(tn)
            If CInt(dr("cnc")) > 0 Then tn.SelectAction = TreeNodeSelectAction.None
            tn.PopulateOnDemand = (CInt(dr("cnc")) > 0)
        Next
    End Sub
    Protected Sub TreeView1_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs)
        PopulateSubMenus(CInt(e.Node.Value), e.Node)
    End Sub
    Private Sub PopulateSubMenus(ByVal parentid As Integer, ByVal parentNode As TreeNode)
        dt = objdl.SubMenusCheckedRoles(parentid, Session("Role_Id"), Session("User_Id"))
        PopulateNodes(dt, parentNode.ChildNodes)
    End Sub
End Class
