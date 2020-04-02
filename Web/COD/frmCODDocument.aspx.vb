Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data
Partial Class frmCODDocument
    Inherits System.Web.UI.Page
    Dim objdo As New ETCODDocument
    Dim objbo As New BOCODDocument
    Dim objBL As New BODDLs
    Dim dt As New DataTable
    Dim dtdg As New DataTable
    Dim objutil As New DBUtil
    Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
        tdTitle.InnerText = ""
        tblSection.Visible = True
        TVDoc.Nodes.Clear()
        BindTree()
        TVDoc.CollapseAll()
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        binddata()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")
        If Page.IsPostBack = False Then
            sec_binddata()
            binddata()
            BindTree()
            If Not Request.QueryString("id") Is Nothing Then
                tdTitle.InnerText = ""
                addrow.InnerText = "Edit "
                btnSave.Text = "Update"
                objBL.fillDDL(ddlScope, "CODScope", True, Constants._DDL_Default_Select)
                btnNewGroup.Disabled = True
                BindDescription()
            End If
        End If
    End Sub
    Sub BindTree()
        dt = objutil.ExeQueryDT("Select Doc_Id, DocName from coddoc where Parent_id=0 order by DocName", "CODDoc")
        For Each drow As DataRow In dt.Rows
            Dim node As New TreeNode
            Dim str As New StringBuilder
            str.Append(drow.Item("DocName").ToString)
            node.Text = str.ToString
            node.Value = drow.Item("Doc_Id") & ""
            node.SelectAction = TreeNodeSelectAction.Select
            node.ShowCheckBox = True
            TVDoc.Nodes.Add(node)
            fillchild(node)
        Next
    End Sub
    Sub fillchild(ByVal tn As TreeNode)
        dt = objutil.ExeQueryDT("Select Doc_Id, DocName,(select count(*) from coddoc as c where c.parent_id=parent_Id) as cc from codDoc where parent_Id = " & tn.Value & " and rstatus=2 group by parent_id, Doc_Id, DocName order by DocName ", "CODDoc")
        For Each drow As DataRow In dt.Rows
            Dim node As New TreeNode
            Dim str As New StringBuilder
            str.Append(drow.Item("DocName").ToString)
            node.Text = str.ToString
            node.Value = drow.Item("Doc_Id")
            node.SelectAction = TreeNodeSelectAction.Select
            node.ShowCheckBox = True
            tn.ChildNodes.Add(node)
            If drow.Item("cc").ToString > 0 Then fillchild(node)
        Next
    End Sub
    Sub binddata()
        dt = objbo.uspTICODDocumentList(0, ddlSelect.SelectedValue, txtSearch.Text, ddlSectionSrc.SelectedValue, hdnSort.Value, 0)
        grdSection.DataSource = dt
        grdSection.PageSize = Session("Page_size")
        grdSection.DataBind()
    End Sub
    Sub sec_binddata()
        objBL.fillDDL(ddlScopeSrc, "CODScope", True, Constants._DDL_Default_Select)
        objBL.fillDDL(ddlScope, "CODScope", True, Constants._DDL_Default_Select)
        objBL.fillDDL(ddlSectionSrc, "CodDocNew", True, Constants._DDL_Default_Select)
    End Sub
    Sub BindDescription()
        dt = objbo.uspTICODDocumentList(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            ddlScope.SelectedValue = dt.Rows(0).Item("Scope_Id").ToString
            rdoDoc.SelectedValue = dt.Rows(0).Item("DOCType").ToString
            rdoDoc_SelectedIndexChanged(Nothing, Nothing)
            If rdoDoc.SelectedIndex = 1 Then
                rowForm.Visible = True
                txtOnlineForm.Value = dt.Rows(0).Item("OnlineForm").ToString
            End If
            If dt.Rows(0).Item("Appr_Required").ToString = True Then
                chkRequired.Checked = True
            Else
                chkRequired.Checked = False
            End If
            If dt.Rows(0).Item("Allow").ToString = True Then
                chkAllow.Checked = True
            Else
                chkAllow.Checked = False
            End If
            txtSOrder.Value = dt.Rows(0).Item("SerialNo").ToString
            txtDoc_Name.Value = dt.Rows(0).Item("DOCName").ToString
            tblSection.Visible = True
            btnDelete.Visible = True
            If dt.Rows(0).Item("DGBox").ToString = True Then
                chkDG.Checked = True
                chkDG_CheckedChanged(Nothing, Nothing)
                Tr1.Visible = True
            Else
                chkDG.Checked = False
                Tr1.Visible = False
            End If
            dtdg = objbo.uspCODDGSignList(Request.QueryString("id"))
            If dtdg.Rows.Count > 0 Then
                GVXY.DataSource = dtdg
                GVXY.DataBind()
                ViewState("dtGrid") = dtdg
            End If
            checkTree(dt.Rows(0).Item("Parent_Id").ToString)
            TVDoc.CollapseAll()
        End If
    End Sub
    Sub ClearNodes()
        For Each aa As TreeNode In TVDoc.Nodes
            If aa.ChildNodes.Count > 0 Then
                aa.ExpandAll()
                aa.Checked = False
                For Each bb As TreeNode In aa.ChildNodes
                    bb.ExpandAll()
                    bb.Checked = False
                    If bb.ChildNodes.Count > 0 Then
                        For Each cc As TreeNode In bb.ChildNodes
                            cc.ExpandAll()
                            cc.Checked = False
                            If cc.ChildNodes.Count > 0 Then
                                subNodeclear(cc, cc.Value)
                            Else
                                cc.Checked = False
                            End If
                        Next
                    Else
                        bb.Checked = False
                    End If
                Next
            Else
                aa.Checked = False
            End If
            aa.Checked = False
        Next
    End Sub
    Sub subNodeclear(ByVal tn As TreeNode, ByVal tnVal As Integer)
        For Each a1 As TreeNode In tn.ChildNodes
            a1.ExpandAll()
            If a1.ChildNodes.Count > 0 Then
                subNodeclear(a1, a1.Value)
            Else
                a1.Checked = False
            End If
        Next
    End Sub
    Sub checkTree(ByVal Parent_Id As Integer)
        ClearNodes()
        For Each Pnode As TreeNode In TVDoc.Nodes
            If Pnode.Value = Parent_Id Then
                Pnode.Expand()
                Pnode.Checked = True
                Exit For
            End If
            If Pnode.ChildNodes.Count > 0 Then
                For Each cNode As TreeNode In Pnode.ChildNodes
                    If cNode.ChildNodes.Count > 0 Then
                        Dim a As Integer = SelectChildrenRecursive(cNode, Parent_Id)
                        If a = 1 Then
                            Exit For
                        End If
                    Else
                        If cNode.Value = Parent_Id Then
                            cNode.Expand()
                            cNode.Checked = True
                            Exit For
                        End If
                    End If
                Next
            End If
        Next
    End Sub
    Private Function SelectChildrenRecursiveNew(ByVal tn As TreeNode, ByVal searchvalue As Integer) As Integer
        If tn.Value = searchvalue Then
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
    Protected Sub grdSection_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSection.PageIndexChanging
        grdSection.PageIndex = e.NewPageIndex
        binddata()
    End Sub
    Protected Sub grdSection_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSection.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdSection.PageIndex * grdSection.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
    Protected Sub grdSection_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdSection.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        binddata()
    End Sub
    Sub fillDescription()
        objdo.AT.RStatus = Constants.STATUS_ACTIVE
        objdo.AT.LMBY = Session("User_Name")
        objdo.DOC_NAME = txtDoc_Name.Value
        objdo.Scope = ddlScope.SelectedValue
        If chkRequired.Checked = True Then
            objdo.Appr_Required = 1
        Else
            objdo.Appr_Required = 0
        End If
        If chkAllow.Checked = True Then
            objdo.Allow_Before_Integration = 1
        Else
            objdo.Allow_Before_Integration = 0
        End If
        objdo.DOC_TYPE = rdoDoc.SelectedValue
        If rdoDoc.SelectedIndex = 1 Then
            objdo.OnlineForm = txtOnlineForm.Value
        End If
        If chkDG.Checked = True Then
            objdo.DGBox = 1
        End If
        objdo.SerialNo = 0
        If Not txtSOrder.Value = "" Then
            objdo.SerialNo = txtSOrder.Value
        End If
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        fillDescription()
        If TVDoc.Visible = True Then
            If TVDoc.CheckedNodes.Count > 1 Then
                Response.Write("<script language='javascript'>alert('Please select single document as parent');</script>")
                Exit Sub
            ElseIf TVDoc.CheckedNodes.Count = 1 Then
                objdo.Parent_ID = TVDoc.CheckedNodes.Item(0).Value
            End If
            Dim i As Integer
            If Request.QueryString("id") Is Nothing Then
                i = objbo.uspTICODDocumentIU(objdo)
                If i > 2 Then
                    ADDXY(i)
                    BOcommon.result(1, True, "frmCODDocument.aspx", "Document Name", Constants._INSERT)
                ElseIf i < 0 Then
                    BOcommon.result(2, True, "frmCODDocument.aspx", "Document Name", Constants._INSERT)
                Else
                    BOcommon.result(1, True, "frmCODDocument.aspx", "Document Name", Constants._INSERT)
                End If
            Else
                objdo.DOC_ID = Request.QueryString("id")
                ADDXY(objdo.DOC_ID)
                BOcommon.result(objbo.uspTICODDocumentIU(objdo), True, "frmCODDocument.aspx", "Document Name", Constants._UPDATE)
            End If
        End If
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        binddata()
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        BOcommon.result(objbo.uspDelete("CODDoc", "DOC_ID", Request.QueryString("id")), True, "frmCODDocument.aspx", " ", Constants._DELETE)
    End Sub
    Protected Sub btnCanel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanel.ServerClick
        Response.Redirect("frmCODDocument.aspx")
    End Sub
    Protected Sub ddlScopeSrc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlScopeSrc.SelectedIndexChanged
        binddata()
    End Sub
    Protected Sub chkDG_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDG.CheckedChanged
        If chkDG.Checked = True Then
            If ViewState("dtGrid") Is Nothing Then CreateNewRow()
            dtdg = objbo.uspCODDGSignList(Request.QueryString("id"))
            Tr1.Visible = True
            If dtdg.Rows.Count > 0 Then
                GVXY.DataSource = dtdg
                GVXY.DataBind()
                ViewState("dtGrid") = dtdg
            End If
        Else
            Tr1.Visible = False
        End If
    End Sub
    Public Function DistinctCart(ByVal dt As DataTable, ByVal strDesc As String) As Integer
        Dim intReturn As Integer = 0
        If dt.Rows.Count <> 0 Then
            Dim intCount As Integer
            For intCount = 0 To dt.Rows.Count - 1 Step intCount + 1
                If dt.Rows(intCount)("DGS_Id").ToString() = strDesc Then
                    intReturn = intCount
                    Exit For
                Else
                    intReturn = 100000000
                End If
            Next
        Else
            intReturn = 100000000
        End If
        Return intReturn
    End Function
    Public Sub CreateNewRow()
        dtdg.Columns.Add("X_Coordinate", Type.GetType("System.Int32"))
        dtdg.Columns.Add("Y_Coordinate", Type.GetType("System.Int32"))
        dtdg.Columns.Add("PageNo", Type.GetType("System.Int32"))
        dtdg.Columns.Add("Sno", Type.GetType("System.Int32"))
        dtdg.Columns.Add("DGS_Id", Type.GetType("System.Int32"))
        For intCount As Integer = 0 To GVXY.Rows.Count - 1
            Dim itxtx As TextBox = CType(GVXY.Rows(intCount).FindControl("txtX"), TextBox)
            Dim itxtY As TextBox = CType(GVXY.Rows(intCount).FindControl("txtY"), TextBox)
            Dim itxtPageno As TextBox = CType(GVXY.Rows(intCount).FindControl("txtPageno"), TextBox)
            Dim itxtSno As HiddenField = CType(GVXY.Rows(intCount).FindControl("HDSno"), HiddenField)
            Dim itxtDGID As HiddenField = CType(GVXY.Rows(intCount).FindControl("HDDGId"), HiddenField)
            Dim dr As DataRow = dtdg.NewRow()
            dr(0) = Convert.ToInt32(itxtx.Text)
            dr(1) = Convert.ToInt32(itxtY.Text)
            dr(2) = Convert.ToInt32(itxtPageno.Text)
            dr(3) = Convert.ToInt32(itxtSno.Value)
            dr(4) = Convert.ToInt32(itxtDGID.Value)
            dtdg.Rows.InsertAt(dr, 0)
        Next
        ViewState("dtGrid") = dtdg
        dtdg = CType(ViewState("dtGrid"), DataTable)
        Dim dr1 As DataRow = dtdg.NewRow()
        dr1(0) = 0
        dr1(1) = 0
        dr1(2) = 0
        If dtdg.Rows.Count = 0 Then
            dr1(3) = 1
            dr1(4) = 1
            ViewState("DGId") = 1
            ViewState("SNO") = 1
        Else
            dr1(3) = Convert.ToInt32(ViewState("DGId")) + 1
            dr1(4) = Convert.ToInt32(ViewState("SNO")) + 1
            ViewState("DGId") = Convert.ToInt32(ViewState("DGId")) + 1
            ViewState("SNO") = Convert.ToInt32(ViewState("SNO")) + 1
        End If
        dtdg.Rows.InsertAt(dr1, 0)
        dtdg.DefaultView.Sort = "SNO asc"
        GVXY.DataSource = dtdg
        GVXY.DataBind()
        ViewState("dtGrid") = dtdg
    End Sub
    Public Sub ADDXY(ByVal intDocid As Integer)
        If Request.QueryString("id") Is Nothing Then
        Else
            objbo.uspCODDeleteDGSignIU(intDocid)
        End If
        For intCount As Integer = 0 To GVXY.Rows.Count - 1
            Dim itxtx As TextBox = CType(GVXY.Rows(intCount).FindControl("txtX"), TextBox)
            Dim itxtY As TextBox = CType(GVXY.Rows(intCount).FindControl("txtY"), TextBox)
            Dim itxtPageno As TextBox = CType(GVXY.Rows(intCount).FindControl("txtPageno"), TextBox)
            Dim itxtSno As HiddenField = CType(GVXY.Rows(intCount).FindControl("HDSno"), HiddenField)
            Dim itxtDGID As HiddenField = CType(GVXY.Rows(intCount).FindControl("HDDGId"), HiddenField)
            objbo.uspCODDGSignIU(intDocid, Convert.ToInt32(itxtx.Text), Convert.ToInt32(itxtY.Text), Convert.ToInt32(itxtPageno.Text), Convert.ToInt32(itxtSno.Value), Session("User_Name"))
        Next
        ViewState("dtGrid") = Nothing
        ViewState("DGId") = Nothing
        ViewState("SNO") = Nothing
    End Sub
    Protected Sub GVXY_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GVXY.RowDeleting
        dtdg.Columns.Add("X_Coordinate", Type.GetType("System.Int32"))
        dtdg.Columns.Add("Y_Coordinate", Type.GetType("System.Int32"))
        dtdg.Columns.Add("PageNo", Type.GetType("System.Int32"))
        dtdg.Columns.Add("Sno", Type.GetType("System.Int32"))
        dtdg.Columns.Add("DGS_Id", Type.GetType("System.Int32"))
        For intCount As Integer = 0 To GVXY.Rows.Count - 1
            Dim itxtx As TextBox = CType(GVXY.Rows(intCount).FindControl("txtX"), TextBox)
            Dim itxtY As TextBox = CType(GVXY.Rows(intCount).FindControl("txtY"), TextBox)
            Dim itxtPageno As TextBox = CType(GVXY.Rows(intCount).FindControl("txtPageno"), TextBox)
            Dim itxtSno As HiddenField = CType(GVXY.Rows(intCount).FindControl("HDSno"), HiddenField)
            Dim itxtDGID As HiddenField = CType(GVXY.Rows(intCount).FindControl("HDDGId"), HiddenField)
            Dim dr As DataRow = dtdg.NewRow()
            dr(0) = Convert.ToInt32(itxtx.Text)
            dr(1) = Convert.ToInt32(itxtY.Text)
            dr(2) = Convert.ToInt32(itxtPageno.Text)
            dr(3) = Convert.ToInt32(itxtSno.Value)
            dr(4) = Convert.ToInt32(itxtDGID.Value)
            dtdg.Rows.InsertAt(dr, 0)
        Next
        Dim itxtDGID1 As HiddenField = CType(GVXY.Rows(e.RowIndex).FindControl("HDDGId"), HiddenField)
        Dim intCartId As Integer = DistinctCart(dtdg, Convert.ToInt32(itxtDGID1.Value))
        If intCartId <> 100000000 Then
            dtdg.Rows.RemoveAt(intCartId)
            dtdg.DefaultView.Sort = "sno asc"
            GVXY.DataSource = dtdg
            GVXY.DataBind()
            ViewState("dtGrid") = dtdg
        End If
    End Sub
    Protected Sub GVXY_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles GVXY.SelectedIndexChanging
        tdTitle.InnerText = ""
        tblSection.Visible = True
        CreateNewRow()
    End Sub
    Protected Sub rdoDoc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoDoc.SelectedIndexChanged
        If rdoDoc.SelectedIndex = 1 Then
            rowForm.Visible = True
        Else
            rowForm.Visible = False
        End If
    End Sub
    Protected Sub ddlSectionSrc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSectionSrc.SelectedIndexChanged
        binddata()
    End Sub
End Class