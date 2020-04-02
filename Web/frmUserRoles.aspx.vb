Imports Common
Imports Entities
Imports BusinessLogic
Imports System.Data

Partial Class frmUserRoles
    Inherits System.Web.UI.Page
    Dim objdl As New BOMenus
    Dim objbo As New BODDLs
    Dim dt, dt1, dt2, dt3 As DataTable
    Dim dv1, dv2, dv3 As DataView
    Dim i, j, k As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~/SessionTimeout.aspx")

        If Page.IsPostBack = False Then

            objdl.fillDDLDrop(ddlUserGroup, "TRole", True, Constants._DDL_Default_Select)
            objdl.UsersList(ddlUser, "EBASTUsers_1", True, Constants._DDL_Default_Select)
            BindTree()            
        End If
        'TreeView1.Attributes.Add("onclick", "OnCheckBoxCheckChanged(event)")

    End Sub

    Sub BindTree()
        TreeView1.Nodes.Clear()
        dt = objdl.ParentMenusU()
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
        dt = objdl.SubMenusU(parentid)
        PopulateNodes(dt, parentNode.ChildNodes)
    End Sub
    Protected Sub TreeView1_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles TreeView1.TreeNodePopulate
        PopulateSubMenus(CInt(e.Node.Value), e.Node)
    End Sub
    Sub CheckTreeNodeRecursive(ByVal parent As TreeNode, ByVal fCheck As Boolean)
        Dim child As TreeNode
        For Each child In parent.ChildNodes
            If child.Checked <> fCheck Then
                child.Checked = fCheck

            End If
            If child.ChildNodes.Count > 0 Then
                CheckTreeNodeRecursive(child, fCheck)
            End If
        Next
    End Sub

    Protected Sub TreeView1_TreeNodeCheckChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles TreeView1.TreeNodeCheckChanged
        If Not e Is Nothing And Not e.Node Is Nothing Then
            If e.Node.ChildNodes.Count > 0 Then
                CheckTreeNodeRecursive(e.Node, e.Node.Checked)                
            End If
        End If    
    End Sub
    Protected Sub checkedNodes(Optional ByVal val As Integer = 0)
        If ddlUserGroup.SelectedItem.Value > 0 AndAlso ddlUser.SelectedItem.Value > 0 Then
            Dim str As String = ""
            
            For Each pchild As TreeNode In TreeView1.CheckedNodes
                str = str & IIf(str = "", "", ",") & "'" & pchild.Value & "'"
            Next
            'Response.Write(str)
            If str <> "" Then
                str = str.Replace("'", "''")
                BOcommon.result(objdl.uspSysmenuRightsInsertU(ddlUserGroup.SelectedValue, ddlUser.SelectedValue, Constants.STATUS_ACTIVE, "SYSADMIN", str), True, "frmUserRoles.aspx", "", Constants._INSERT)
                ' TreeView1.Visible = False
            Else
                Response.Write("<script language='javascript'>alert('Please select menus')</script>")            
            End If
        Else
            Response.Write("<script language='javascript'>alert('Please select Role \n Please select User')</script>")
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If ddlUserGroup.SelectedItem.Value > 0 AndAlso ddlUser.SelectedItem.Value > 0 Then
            checkedNodes()
        Else
            Response.Write("<script language='javascript'>alert('Please select Role/User')</script>")
        End If
    End Sub
    Sub bindEditTree()
        For i As Integer = 0 To dt.Rows.Count - 1
            For Each pNode As TreeNode In TreeView1.Nodes                
                If pNode.Value = dt.Rows(i).Item(0) Then
                    pNode.Expand()
                    pNode.Checked = True                    
                    'pNode.ShowCheckBox = False               
                    Exit For
                End If
                If pNode.ChildNodes.Count > 0 Then
                    For Each cNode As TreeNode In pNode.ChildNodes
                        Dim a As Integer = SelectChildrenRecursive(cNode, dt.Rows(i).Item(0))
                        If a = 1 Then
                            cNode.Expand()
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
            'tn.ShowCheckBox = False
            Return 1
        End If
        If tn.ChildNodes.Count > 0 Then
            For Each tnc As TreeNode In tn.ChildNodes               
                Dim a As Integer = SelectChildrenRecursive(tnc, searchvalue)
                If a = 1 Then
                    tnc.Expand()
                    tnc.Checked = True
                End If
            Next
        End If
    End Function

    Protected Sub ddlUser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUser.SelectedIndexChanged
        TreeView1.Visible = True
        BindTree()
        If ddlUser.SelectedItem.Value > 0 Then
            dt = objdl.uspSystemMenuRightsUserList(ddlUserGroup.SelectedItem.Value, ddlUser.SelectedItem.Value)
            If dt.Rows.Count > 0 Then
                bindEditTree()
            Else
                TreeView1.ExpandAll()
            End If
        End If
    End Sub

    Protected Sub ddlUserGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUserGroup.SelectedIndexChanged
        ddlUser.Items.Clear()
        objdl.fillDDL(ddlUser, "EBASTUsers_1", ddlUserGroup.SelectedItem.Value, True, Constants._DDL_Default_Select)

        'If TreeView1.Visible = False Then
        '    TreeView1.Visible = True
        'End If
        BindTree()
        If ddlUserGroup.SelectedItem.Value > 0 Then
            dt = objdl.uspSystemMenuRightsListU(ddlUserGroup.SelectedValue)
            'dt = objdl.uspSystemMenuRightsList(ddlUserGroup.SelectedValue)
            If dt.Rows.Count > 0 Then               
                bindEditTree()
                'BindTree()
            Else
                TreeView1.CollapseAll()
                
            End If
        End If
        'BindTree()        
    End Sub

End Class
