Imports Common
Imports Entities
Imports BusinessLogic
Imports System.Data

Partial Class MenuDisplay
    Inherits System.Web.UI.Page
    Dim objdl As New BOMenus
    Dim dt As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("role") = "sys" Then
            'Dim ID As Integer = Session("Role_Id")
            'Session("Role_Id") = 19
            If Page.IsPostBack = False Then
                BindTree()
                TreeView1.CollapseAll()
            End If
        End If
    End Sub
    Sub BindTree()
        TreeView1.Nodes.Clear()
        dt = objdl.ParentMenus()
        PopulateNodes(dt, TreeView1.Nodes)
        'TreeView1.ExpandAll()
    End Sub
    Private Sub PopulateNodes(ByVal dt As DataTable, ByVal nodes As TreeNodeCollection)
        For Each dr As DataRow In dt.Rows
            Dim tn As New TreeNode()
            tn.Text = dr("mnuname").ToString()
            tn.Value = dr("mnu_id").ToString()
            tn.Target = "mainframe"
            tn.NavigateUrl = dr("mnulink").ToString()
            nodes.Add(tn)
            If CInt(dr("cnc")) > 0 Then tn.SelectAction = TreeNodeSelectAction.None
            tn.PopulateOnDemand = (CInt(dr("cnc")) > 0)
        Next
    End Sub
    Private Sub PopulateSubMenus(ByVal PMNUID As Integer, ByVal UGP_ID As Integer, ByVal parentNode As TreeNode)
        dt = objdl.SubMenusCheckedRoles(PMNUID, Session("Role_Id"))
        PopulateNodes(dt, parentNode.ChildNodes)
        bindEditTree()

    End Sub
    Protected Sub TreeView1_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles TreeView1.TreeNodePopulate
        PopulateSubMenus(CInt(e.Node.Value), CInt(e.Node.Value), e.Node)
    End Sub
    Sub bindEditTree()
        For i As Integer = 0 To dt.Rows.Count - 1
            For Each pNode As TreeNode In TreeView1.Nodes
                If pNode.Value = dt.Rows(i).Item(0) Then
                    pNode.Expand()
                    pNode.Checked = True
                    Exit For
                End If
                If pNode.ChildNodes.Count > 0 Then
                    For Each cNode As TreeNode In pNode.ChildNodes
                        Dim a As Integer = SelectChildrenRecursive(cNode, dt.Rows(i).Item(0))
                        If a = 1 Then
                            cNode.Expand()
                            'cNode.Checked = True
                        End If
                    Next

                End If
            Next
        Next
    End Sub
    Private Function SelectChildrenRecursive(ByVal tn As TreeNode, ByVal searchvalue As Integer) As Integer
        If tn.Value = searchvalue Then            
            tn.Expand()
            tn.Checked = True
            Return 1            
        End If
        If tn.ChildNodes.Count > 0 Then
            For Each tnc As TreeNode In tn.ChildNodes
                Dim a As Integer = SelectChildrenRecursive(tnc, searchvalue)
                If a = 1 Then
                    tnc.Expand()
                    tnc.Checked = True
                    Return 1
                End If
            Next
        End If
    End Function

    

End Class
