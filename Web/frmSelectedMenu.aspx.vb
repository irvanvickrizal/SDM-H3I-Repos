Imports Common
Imports Entities
Imports BusinessLogic
Imports System.Data

Partial Class frmSelectedMenu
    Inherits System.Web.UI.Page
    Dim objdl As New BOMenus
    Dim dt As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("role") = "sys" Then
        If Page.IsPostBack = False Then
            objdl.fillDDLDrop(ddlUserGroup, "TRole", True, Constants._DDL_Default_Select)
            BindTree()

        End If
    End Sub
    Sub BindTree()
        TreeView1.Nodes.Clear()
        dt = objdl.ParentMenus(ddlUserGroup.SelectedItem.Value)
        PopulateNodes(dt, TreeView1.Nodes)
        TreeView1.ExpandAll()
    End Sub
    Private Sub PopulateNodes(ByVal dt As DataTable, ByVal nodes As TreeNodeCollection)
        For Each dr As DataRow In dt.Rows
            Dim tn As New TreeNode()
            tn.Text = dr("mnuname").ToString()
            tn.Value = dr("mnu_id").ToString()
            nodes.Add(tn)
            If CInt(dr("cnc")) > 0 Then tn.SelectAction = TreeNodeSelectAction.None
            tn.PopulateOnDemand = (CInt(dr("cnc")) > 0)
        Next
    End Sub
    Private Sub PopulateSubMenus(ByVal parentid As Integer, ByVal parentNode As TreeNode)
        dt = objdl.SubMenus(parentid, ddlUserGroup.SelectedItem.Value)
        PopulateNodes(dt, parentNode.ChildNodes)
        If ddlUserGroup.SelectedValue > 0 Then
            bindEditTree()
        End If

    End Sub
    Protected Sub TreeView1_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles TreeView1.TreeNodePopulate
        PopulateSubMenus(CInt(e.Node.Value), e.Node)


    End Sub

    Protected Sub TreeView1_TreeNodeCheckChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles TreeView1.TreeNodeCheckChanged
        If Not e Is Nothing And Not e.Node Is Nothing Then
            If e.Node.ChildNodes.Count > 0 Then
                CheckTreeNodeRecursive(e.Node, e.Node.Checked)
            End If
        End If
    End Sub
    Sub CheckTreeNodeRecursive(ByVal parent As TreeNode, ByVal fCheck As Boolean)
        Dim child As TreeNode
        For Each child In parent.ChildNodes
            If child.Checked <> fCheck Then
                child.Checked = fCheck
                child.ShowCheckBox = False
            End If
            If child.ChildNodes.Count > 0 Then
                CheckTreeNodeRecursive(child, fCheck)
            End If
        Next
    End Sub

    Protected Sub ddlUserGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUserGroup.SelectedIndexChanged
        'If TreeView1.Visible = False Then
        '    TreeView1.Visible = True
        '    'If TreeView1.CheckedNodes.Count > 0 Then

        '    'End If
        'End If
        'If ddlUserGroup.SelectedItem.Value > 0 Then
        '    dt = objdl.GetListofRoles(ddlUserGroup.SelectedValue)

        '    If dt.Rows.Count > 0 Then

        '    Else
        '        TreeView1.CollapseAll()
        '    End If
        'Else
        '    TreeView1.Visible = False

        'End If
        BindTree()

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
            'If tn.ChildNodes.Count > 0 Then
            tn.Expand()
            tn.Checked = True
            Return 1
            'Else
            'tn.Checked = True
            'End If
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
