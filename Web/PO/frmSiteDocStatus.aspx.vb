Imports BusinessLogic
Imports Common
Imports System.Data
Imports System.Web.UI
Partial Class frmsiteDocStatus
    Inherits System.Web.UI.Page
    Dim dt, dt1, dt2, dt3 As DataTable
    Dim objBo As New BOSiteDocs
    Dim strSql As String
    Dim i, j, k As Integer
    Dim siteid As Integer
    Dim dv1, dv2, dv3 As DataView
    Dim objDL As New BODDLs
    Dim objutil As New DBUtil
    Sub BindTree()
        TreeView1.Nodes.Clear()
        'Dim tn As New TreeNode
        'tn.Text = "Documents"
        'tn.Value = 0
        'TreeView1.Nodes.Add(tn)
        FillSection()
        TreeView1.ExpandAll()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            objDL.fillDDL(ddlPO1, "PoNo", True, Constants._DDL_Default_Select)
        End If
    End Sub
    Protected Sub ddlPO1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPO1.SelectedIndexChanged
        If ddlPO1.SelectedIndex > 0 Then
            'here  we have to show only sites for which BAST process not finished.
            Dim lvlcode As String = Session("lvlcode")
            Dim area, region, zone, site As Integer
            area = Session("Area_Id")
            region = Session("Region_Id")
            zone = Session("Zone_Id")
            site = Session("Site_Id")
            Dim ddldt As DataTable
            ddldt = objBo.uspDDLPOSiteNoByUser1(ddlPO1.SelectedItem.Value, lvlcode, area, region, zone, site, Session("User_Id"))
            If ddldt.Rows.Count > 0 Then
                ddlSite.DataSource = ddldt
                ddlSite.DataTextField = "txt"
                ddlSite.DataValueField = "VAL"
                ddlSite.DataBind()
                'ddlSite_SelectedIndexChanged(Nothing, Nothing)
            Else
                If site = 0 Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('Site information missing for this user');", True)
                End If
            End If
        End If
    End Sub

    Sub BindData()
        Dim stval() As String = ddlSite.SelectedValue.Split("-")
        Dim scope() As String = ddlSite.SelectedItem.Text.Split("-")
        Dim ver As Integer = objBo.getsiteversion(ddlPO1.SelectedItem.Text, scope(0), scope(1), scope(2))
        siteid = stval(0)
        dt1 = objBo.UploadedY(siteid, ver)
        dv1 = dt1.DefaultView
        dv1.Sort = "DocID"
        dt2 = objBo.UploadedN(siteid, ver)
        dv2 = dt2.DefaultView
        dv2.Sort = "DocID"
        dt3 = objBo.NOTReqButUploaded(siteid, ver)
        dv3 = dt3.DefaultView
        dv3.Sort = "Docid"
        TreeView1.Visible = True
        BindTree()
        checkTree(Integer.Parse(stval(0)), ddlPO1.SelectedItem.Text, scope(1), scope(2))
    End Sub
    Sub checkTree(ByVal siteid As Integer, ByVal pono As String, ByVal scope As String, ByVal workpkgid As String)
        'Dim scope() As String = ddlSite.SelectedItem.Text.Split("-")
        'dt = objBo.uspSiteDocList(siteid, pono, "", scope)
        dt = objutil.ExeQueryDT("Exec uspSiteDocList '" & siteid & "','site_no','" & pono & "','" & scope & "'," & workpkgid, "SiteDocList")
        If dt.Rows.Count > 0 Then
            For j As Integer = 0 To dt.Rows.Count - 1
                For Each Pnode As TreeNode In TreeView1.Nodes
                    If Pnode.Value = dt.Rows(j).Item("Doc_Id") Then
                        Pnode.Expand()
                        Pnode.Checked = True
                        Exit For
                    End If
                    If Pnode.ChildNodes.Count > 0 Then
                        For Each cNode As TreeNode In Pnode.ChildNodes
                            Dim a As Integer = SelectChildrenRecursive(cNode, dt.Rows(j).Item(0))
                            If a = 1 Then
                                cNode.Expand()
                            End If
                        Next
                    End If
                Next
            Next
        End If
    End Sub
    Private Function SelectChildrenRecursiveNew(ByVal tn As TreeNode, ByVal searchvalue As Integer) As Integer
        If tn.Value = searchvalue Then
            'tn.Expand()
            'tn.Checked = True
            Return 1
        Else
            Return 0
        End If

    End Function
    Private Function SelectChildrenRecursive(ByVal tn As TreeNode, ByVal searchvalue As Integer) As Integer
        If SelectChildrenRecursiveNew(tn, searchvalue) = 1 Then
            tn.Expand()
            tn.Checked = True
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
    Protected Sub TreeView1_TreeNodeCheckChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs)
        If Not e Is Nothing And Not e.Node Is Nothing Then
            If e.Node.ChildNodes.Count > 0 Then
                CheckTreeNodeRecursive(e.Node, e.Node.Checked)
            End If
        End If
    End Sub
    Sub FillSection()
        'dt = objBo.uspBOTaskMain()
        dt = objutil.ExeQueryDT("Select C.Doc_Id,DocName,DocType,IsNull(WFId,0)WFId,Appr_Required from coddoc C Left Outer join CodWFDoc W on W.Doc_Id=C.Doc_Id where Parent_id=0 order by SerialNo", "CODDoc")
        For Each drow As DataRow In dt.Rows
            Dim node As New TreeNode
            Dim str As New StringBuilder
            str.Append(drow.Item("DocName").ToString & "    ")
            node.Text = str.ToString  'drow.Item("txt") & ""
            node.Value = drow.Item("Doc_Id") & ""
            'node.PopulateOnDemand = True
            If node.Value > 1000 Then
                Dim Dstr As New StringBuilder
                'Dstr.Append(drow.Item("txt") & "  ")
                i = dv1.Find(node.Value)
                j = dv2.Find(node.Value)
                k = dv3.Find(node.Value)
                If k >= 0 Then
                    Dstr.Append("<font color=brown><b>" & drow.Item("txt") & "    " & "</b></font>")
                Else
                    Dstr.Append(drow.Item("DocName") & "    ")
                End If
                If i >= 0 Then
                    Dstr.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                    node.ToolTip = "Already uploded"
                ElseIf j >= 0 Then
                    Dstr.Append("<Img ID='Image1' src='../Images/notok.jpg' style='border:none' />")
                    node.ToolTip = "pending"
                ElseIf k >= 0 Then
                    Dstr.Append("<Img ID='Image1' src='../Images/Rok.jpg' style='border:none' />")
                    node.ToolTip = "Notrequired but uploaded"
                End If
                node.Text = Dstr.ToString
            Else
                node.Text = drow.Item("DocName").ToString & ""
            End If
            node.SelectAction = TreeNodeSelectAction.Select   'TreeNodeSelectAction.SelectExpand
            TreeView1.Nodes.Add(node)
            FillDoc(node)
        Next
    End Sub
   
    Sub FillSubSection(ByVal tn As TreeNode)
        dt = objBo.uspBOTaskSub(tn.Value)
        For Each drow As DataRow In dt.Rows
            Dim node As New TreeNode
            node.Value = drow.Item("val") & ""
            If node.Value > 1000 Then
                Dim Sstr As New StringBuilder
                ' Sstr.Append(drow.Item("txt").ToString & "")
                i = dv1.Find(node.Value)
                j = dv2.Find(node.Value)
                k = dv3.Find(node.Value)
                If k >= 0 Then
                    Sstr.Append("<font color=grey>" & drow.Item("txt").ToString & "   " & "</font>")
                Else
                    Sstr.Append(drow.Item("txt").ToString & "   ")

                End If
                If i >= 0 Then
                    Sstr.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                    node.ToolTip = "Already uploded"
                ElseIf j >= 0 Then
                    Sstr.Append("<Img ID='Image1' src='../Images/notok.jpg' style='border:none' />")
                    node.ToolTip = "Pending"
                ElseIf k >= 0 Then
                    Sstr.Append("<Img ID='Image1' src='../Images/Rok.jpg' style='border:none' />")
                    node.ToolTip = "Notrequired but uploaded"
                End If
                node.Text = Sstr.ToString
            Else
                node.Text = drow.Item("txt").ToString & ""
            End If

            'node.PopulateOnDemand = True
            node.SelectAction = TreeNodeSelectAction.Select   'TreeNodeSelectAction.SelectExpand
            tn.ChildNodes.Add(node)
        Next
    End Sub
    Sub FillDoc(ByVal tn As TreeNode)
        'dt = objBo.uspBOTaskSubDoc(tn.Value)
        dt = objBo.uspTICODSubSectionDoc(tn.Value)
        For Each drow As DataRow In dt.Rows
            Dim node As New TreeNode
            node.Value = drow.Item("Doc_Id") & ""
            If node.Value > 1000 Then
                Dim Dstr As New StringBuilder
                'Dstr.Append(drow.Item("txt") & "  ")
                i = dv1.Find(node.Value)
                j = dv2.Find(node.Value)
                k = dv3.Find(node.Value)
                If k >= 0 Then
                    Dstr.Append("<font color=brown><b>" & drow.Item("txt") & "    " & "</b></font>")
                Else
                    Dstr.Append(drow.Item("DocName") & "    ")
                End If
                If i >= 0 Then
                    Dstr.Append("<Img ID='Image1' src='../Images/ok.jpg' style='border:none' />")
                    node.ToolTip = "Already uploded"
                ElseIf j >= 0 Then
                    Dstr.Append("<Img ID='Image1' src='../Images/notok.jpg' style='border:none' />")
                    node.ToolTip = "pending"
                ElseIf k >= 0 Then
                    Dstr.Append("<Img ID='Image1' src='../Images/Rok.jpg' style='border:none' />")
                    node.ToolTip = "Notrequired but uploaded"
                End If
                node.Text = Dstr.ToString
            Else
                node.Text = drow.Item("DocName").ToString & ""
            End If
            'node.PopulateOnDemand = True
            node.SelectAction = TreeNodeSelectAction.Select   'TreeNodeSelectAction.SelectExpand
            tn.ChildNodes.Add(node)
            If drow.Item("cc") > 0 Then FillDoc(node)
        Next
    End Sub

    Protected Sub TreeView1_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles TreeView1.TreeNodePopulate
    '    Select Case e.Node.Depth
    '        Case 0
    '            FillSection(e.Node)
    '        Case 1
    '            FillSubSection(e.Node)
    '        Case 2
    '            FillDoc(e.Node)
    '    End Select
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

    Protected Sub ddlSite_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSite.SelectedIndexChanged
        BindData()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        For i = 0 To ddlSite.Items.Count - 1
            If UCase(ddlSite.Items(i).Text).Contains(UCase(txtSearch.Text)) = True Then
                ddlSite.SelectedIndex = i '+ 1
                ddlSite_SelectedIndexChanged(Nothing, Nothing)
                Exit For
            End If
        Next
    End Sub
End Class