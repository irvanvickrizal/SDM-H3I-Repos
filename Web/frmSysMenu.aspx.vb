Imports Common
Imports Entities
Imports BusinessLogic
Imports System.Data

Partial Class frmSysMenu
    Inherits System.Web.UI.Page
    Dim objdl As New BOMenus
    Dim dt, dt1, dt2, dt3 As DataTable
    Dim dv1, dv2, dv3 As DataView
    Dim i, j, k As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~/SessionTimeout.aspx")
        btnSave.Attributes.Add("onclick", "javascript:return SelectUser();")
        TreeView1.Attributes.Add("onclick", "OnCheckBoxCheckChanged(event)")
        If Page.IsPostBack = False Then
            objdl.fillDDLDrop(ddlUserGroup, "TRole", True, Constants._DDL_Default_Select)

            BindTree()
        End If
    End Sub

    Sub BindTree()
        TreeView1.Nodes.Clear()
        dt = objdl.ParentMenus()
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
        dt = objdl.SubMenus(parentid)
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
        If ddlUserGroup.SelectedValue > 0 Then
            Dim str As String = ""
            For Each pchild As TreeNode In TreeView1.CheckedNodes
                str = str & IIf(str = "", "", ",") & pchild.Value
            Next
            If str <> "" Then
                'Response.Write(str)
                BOcommon.result(objdl.uspSysmenuRightsInsert(ddlUserGroup.SelectedValue, Constants.STATUS_ACTIVE, Session("User_Name"), str), True, "frmSysMenu.aspx", "", Constants._INSERT)
                TreeView1.Visible = False
            Else
                Response.Write("<script language='javascript'>alert('please select menus')</script>")
            End If
        Else
            Response.Write("<script language='javascript'>alert('please select User Role')</script>")
        End If
    End Sub
    'Sub FillSection(ByVal tn As TreeNode)
    '    dt = objdl.ParentMenus()
    '    For Each drow As DataRow In dt.Rows
    '        Dim node As New TreeNode
    '        Dim str As New StringBuilder
    '        str.Append(drow.Item("txt").ToString & "    ")
    '        node.Text = str.ToString  'drow.Item("txt") & ""
    '        node.Value = drow.Item("val") & ""
    '        node.PopulateOnDemand = True
    '        node.SelectAction = TreeNodeSelectAction.Select   'TreeNodeSelectAction.SelectExpand
    '        tn.ChildNodes.Add(node)
    '    Next
    'End Sub
    'Sub FillSubSection(ByVal tn As TreeNode)
    '    dt = objdl.ParentMenus()
    '    For Each drow As DataRow In dt.Rows
    '        Dim node As New TreeNode
    '        node.Value = drow.Item("val") & ""
    '        If node.Value > 1000 Then
    '            Dim Sstr As New StringBuilder
    '            Sstr.Append(drow.Item("txt").ToString & "")
    '            i = dv1.Find(node.Value)
    '            j = dv2.Find(node.Value)
    '            k = dv3.Find(node.Value)
    '            If k >= 0 Then
    '                Sstr.Append("<font color=grey>" & drow.Item("txt").ToString & "   " & "</font>")
    '            Else
    '                Sstr.Append(drow.Item("txt").ToString & "   ")

    '            End If
    '            If i >= 0 Then
    '                Sstr.Append("<Img ID='Image1' src='Images/ok.jpg' style='border:none' />")
    '                node.ToolTip = "Already uploded"
    '            ElseIf j >= 0 Then
    '                Sstr.Append("<Img ID='Image1' src='Images/notok.jpg' style='border:none' />")
    '                node.ToolTip = "Pending"
    '            ElseIf k >= 0 Then
    '                Sstr.Append("<Img ID='Image1' src='Images/Rok.jpg' style='border:none' />")
    '                node.ToolTip = "Notrequired but uploaded"
    '            End If
    '            node.Text = Sstr.ToString
    '        Else
    '            node.Text = drow.Item("txt").ToString & ""
    '        End If

    '        node.PopulateOnDemand = True
    '        node.SelectAction = TreeNodeSelectAction.Select   'TreeNodeSelectAction.SelectExpand
    '        tn.ChildNodes.Add(node)
    '    Next
    'End Sub

   
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If ddlUserGroup.SelectedItem.Value > 0 Then
            checkedNodes()
        End If
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

    Protected Sub ddlUserGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUserGroup.SelectedIndexChanged
        If TreeView1.Visible = False Then
            TreeView1.Visible = True
        End If
        BindTree()
        If ddlUserGroup.SelectedItem.Value > 0 Then
            dt = objdl.uspSystemMenuRightsList(ddlUserGroup.SelectedValue)
            If dt.Rows.Count > 0 Then
                bindEditTree()
            Else
                TreeView1.CollapseAll()
                'BindTree()
            End If
        Else
            TreeView1.Visible = False
        End If
    End Sub
End Class
