Imports System.Data

Partial Class Deem_Approval_frmDAManagement
    Inherits System.Web.UI.Page

    Dim controller As New DeemApprovalController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
            BindTransactionTypes(DdlTransactionTypes)
            paneldocgroup.Visible = False
            panelconfirmdocgroup.Visible = False
        End If
    End Sub

    Protected Sub DdlTransactionTypes_SelectIndexChanging(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlTransactionTypes.SelectedIndexChanged
        If DdlTransactionTypes.SelectedIndex > 0 Then
            BindDocuments(DdlDocuments, Integer.Parse(DdlTransactionTypes.SelectedValue))
            BindUserTypes(DdlUserTypes)
        Else
            DdlDocuments.Items.Clear()
            DdlUserTypes.Items.Clear()
            panelconfirmdocgroup.Visible = False
            paneldocgroup.Visible = False
            DdlHasDocGroup.ClearSelection()
        End If
    End Sub

    Protected Sub DdlDocuments_SelectIndexChanging(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlDocuments.SelectedIndexChanged
        If DdlDocuments.SelectedIndex > 0 Then
            panelconfirmdocgroup.Visible = True
        Else
            panelconfirmdocgroup.Visible = False
        End If
        DdlHasDocGroup.ClearSelection()
    End Sub

    Protected Sub LbtSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSave.Click
        If FormChecking() = True Then
            Dim info As New DeemApprovalInfo
            info.ODDAID = 0
            info.DocInf.DocId = Integer.Parse(DdlDocuments.SelectedValue)
            info.TaskGroup = DdlTasks.SelectedValue
            info.TotalDoc = TxtTotalDoc.Text
            info.TransInfo.TransId = Integer.Parse(DdlTransactionTypes.SelectedValue)
            info.USRTypeInfo.UserTypeId = Integer.Parse(DdlUserTypes.SelectedValue)
            info.AutoExeAfterSLA = Integer.Parse(TxtExecuteAfterSLA.Text)
            info.SLADoc = Integer.Parse(TxtSLADoc.Text)
            info.WarningSLANotifDay = Integer.Parse(TxtWarningNotif.Text)
            info.CMAInfo.ModifiedUser = CommonSite.UserName
            If DdlHasDocGroup.SelectedValue = "1" Then
                info.HasDocGroup = True
                info.DocGroupInfo.DocId = Integer.Parse(DdlDocumentGroup.SelectedValue)
            Else
                info.HasDocGroup = False
                info.DocGroupInfo.DocId = 0
            End If
            If controller.DeemApproval_CheckConfigIsExist(info) = False Then
                If controller.DeemApproval_IU(info) = True Then
                    LblErrorMessage.Visible = True
                    LblErrMessageGv.Visible = False
                    LblErrorMessage.Text = "Data Successfully Saved"
                    LblErrorMessage.ForeColor = Drawing.Color.Green
                    LblErrorMessage.Font.Italic = True
                    BindData()
                    ClearForm()
                Else
                    LblErrorMessage.Visible = True
                    LblErrMessageGv.Visible = False
                    LblErrorMessage.Text = "Error While Saving Data, please Try again!"
                    LblErrorMessage.ForeColor = Drawing.Color.Red
                    LblErrorMessage.Font.Italic = True
                End If
            Else
                LblErrorMessage.Visible = True
                LblErrMessageGv.Visible = False
                LblErrorMessage.Text = "This config already exist, please try again!"
                LblErrorMessage.ForeColor = Drawing.Color.Red
                LblErrorMessage.Font.Italic = True
            End If
            
        End If
    End Sub

    Protected Sub DdlHasDocGroup_selectIndexChanging(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlHasDocGroup.SelectedIndexChanged
        If DdlHasDocGroup.SelectedValue.Equals("-1") Or DdlHasDocGroup.SelectedValue.Equals("0") Then
            DdlDocumentGroup.Items.Clear()
            paneldocgroup.Visible = False
        Else
            paneldocgroup.Visible = True
            BindDocuments(DdlDocumentGroup, Integer.Parse(DdlTransactionTypes.SelectedValue))
        End If
    End Sub

#Region "Gridview Deem Approval"

    Protected Sub GvDeemApproval_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvDeemApproval.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim docid As Integer = Integer.Parse(DataBinder.Eval(e.Row.DataItem, "Doc_Id"))
            Dim HasDocGroup As Boolean = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "HasDocGroup"))
            Dim DocWithGroup As Integer = Integer.Parse(DataBinder.Eval(e.Row.DataItem, "DocWithGroup"))
            Dim LblDocument As Label = CType(e.Row.FindControl("LblDocument"), Label)
            Dim LblDocumentGroup As Label = CType(e.Row.FindControl("LblDocumentGroup"), Label)
            Dim TransId As Integer = Integer.Parse(DataBinder.Eval(e.Row.DataItem, "Transaction_type_Id"))
            ''identifying the control in gridview
            Dim lnkbtnresult As ImageButton = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
            'raising javascript confirmationbox whenver user clicks on link button
            If LblDocument IsNot Nothing Then
                LblDocument.Text = GetDetailDoc(docid, TransId).DocName
            End If

            If LblDocumentGroup IsNot Nothing Then
                If HasDocGroup = True Then
                    LblDocumentGroup.Text = GetDetailDoc(DocWithGroup, TransId).DocName
                Else
                    LblDocumentGroup.Text = "-"
                End If

            End If

            If Not lnkbtnresult Is Nothing Then
                lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + LblDocument.Text + "')")
            End If

            '' Row Edit Template
            Dim LblDocumentEdit As Label = CType(e.Row.FindControl("LblDocumentEdit"), Label)
            If LblDocumentEdit IsNot Nothing Then
                LblDocumentEdit.Text = GetDetailDoc(docid, TransId).DocName
            End If

            Dim LblDocumentGroupEdit As Label = CType(e.Row.FindControl("LblDocumentGroupEdit"), Label)
            If LblDocumentGroupEdit IsNot Nothing Then
                If HasDocGroup = True Then
                    LblDocumentGroupEdit.Text = GetDetailDoc(docid, TransId).DocName
                Else
                    LblDocumentGroupEdit.Text = "-"
                End If

            End If
        End If
    End Sub

    Protected Sub GvDeemApproval_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName.Equals("AddNew") Then
            'Dim txtPONO As TextBox = CType(GvSOACBudget.FooterRow.FindControl("txtftPONO"), TextBox)
            'Dim txtDescription As TextBox = CType(GvSOACBudget.FooterRow.FindControl("txtftDescription"), TextBox)
            'Dim txtPOUSD As HtmlControls.HtmlInputText = CType(GvSOACBudget.FooterRow.FindControl("txtftPOUSD"), HtmlControls.HtmlInputText)
            'Dim txtPOIDR As HtmlControls.HtmlInputText = CType(GvSOACBudget.FooterRow.FindControl("txtftPOIDR"), HtmlControls.HtmlInputText)
            'Dim txtImpUSD As HtmlControls.HtmlInputText = CType(GvSOACBudget.FooterRow.FindControl("txtftImpUSD"), HtmlControls.HtmlInputText)
            'Dim txtImpIDR As HtmlControls.HtmlInputText = CType(GvSOACBudget.FooterRow.FindControl("txtftImpIDR"), HtmlControls.HtmlInputText)
            'Dim txtBastUSD As HtmlControls.HtmlInputText = CType(GvSOACBudget.FooterRow.FindControl("txtftBastUSD"), HtmlControls.HtmlInputText)
            'Dim txtBastIDR As HtmlControls.HtmlInputText = CType(GvSOACBudget.FooterRow.FindControl("txtftBastIDR"), HtmlControls.HtmlInputText)
            'Dim DdlDesc As DropDownList = CType(GvSOACBudget.FooterRow.FindControl("DdlDesc"), DropDownList)

            'Dim info As New ODSOACValueInfo
            'info.PONO = txtPONO.Text
            'info.ValueDesc = DdlDesc.SelectedValue.ToUpper()
            'info.POEuro = Convert.ToDouble(0)
            'info.POUSD = Convert.ToDouble(txtPOUSD.Value)
            'info.POIDR = Convert.ToDouble(txtPOIDR.Value)
            'info.ImplEuro = Convert.ToDouble(0)
            'info.BASTEuro = Convert.ToDouble(0)
            'If info.ValueDesc.Equals("SW") Then
            '    info.ImplUSD = info.POUSD
            '    info.ImplIDR = info.POIDR
            '    info.BASTUSD = info.POUSD / 2
            '    info.BASTIDR = info.POIDR / 2
            'ElseIf info.ValueDesc.Equals("HW") Then
            '    info.ImplUSD = info.POUSD
            '    info.ImplIDR = info.POIDR
            '    info.BASTUSD = info.POUSD
            '    info.BASTIDR = info.POIDR
            'End If

            'info.DeltaEURO = Math.Abs(info.ImplEuro - info.BASTEuro)
            'info.DeltaIDR = Math.Abs(info.ImplIDR - info.BASTIDR)
            'info.DeltaUSD = Math.Abs(info.ImplUSD - info.BASTUSD)
            'info.LMBY = CommonSite.UserId
            'info.SOACID = GetSOACID()
            'info.ValueId = 0
            'AddUpdateSOACValue(info)
        End If
    End Sub

    Protected Sub GvDeemApproval_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        GvDeemApproval.EditIndex = e.NewEditIndex
        BindData()
    End Sub

    Protected Sub GvDeemApproval_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim valueid As Integer = Convert.ToInt32(GvDeemApproval.DataKeys(e.RowIndex).Value.ToString())
        Dim dainfo As DeemApprovalInfo = controller.GetDeemApproval(valueid)
        Dim TxtSLADocEdit As TextBox = CType(GvDeemApproval.Rows(e.RowIndex).FindControl("TxtSLADocEdit"), TextBox)
        Dim TxtSLAWarningEdit As TextBox = CType(GvDeemApproval.Rows(e.RowIndex).FindControl("TxtSLAWarningEdit"), TextBox)
        Dim TxtSLAExecuteDayEdit As TextBox = CType(GvDeemApproval.Rows(e.RowIndex).FindControl("TxtSLAExecuteDayEdit"), TextBox)
        Dim TxtTotalDocEdit As TextBox = CType(GvDeemApproval.Rows(e.RowIndex).FindControl("TxtTotalDocEdit"), TextBox)
        dainfo.AutoExeAfterSLA = TxtSLAExecuteDayEdit.Text
        dainfo.SLADoc = TxtSLADocEdit.Text
        dainfo.WarningSLANotifDay = TxtSLAWarningEdit.Text
        dainfo.TotalDoc = TxtTotalDocEdit.Text
        dainfo.CMAInfo.ModifiedUser = CommonSite.UserName
        If controller.DeemApproval_IU(dainfo) = True Then
            GvDeemApproval.EditIndex = -1
            LblErrMessageGv.Text = "Data Updated Successfully"
            LblErrMessageGv.ForeColor = Drawing.Color.Green
            LblErrMessageGv.Font.Italic = True
            LblErrMessageGv.Visible = True
            LblErrorMessage.Visible = False
            BindData()
        Else
            LblErrMessageGv.Text = "Data Failed to update"
            LblErrMessageGv.ForeColor = Drawing.Color.Red
            LblErrMessageGv.Font.Italic = True
            LblErrMessageGv.Visible = True
            LblErrorMessage.Visible = False
        End If
    End Sub

    Protected Sub GvDeemApproval_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        GvDeemApproval.EditIndex = -1
        BindData()
    End Sub

    Protected Sub GvDeemApproval_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim valueid As Int32 = Convert.ToInt32(GvDeemApproval.DataKeys(e.RowIndex).Values("ODDA_Id").ToString())
        If controller.DeemApproval_D(valueid) = True Then
            LblErrMessageGv.Text = "Data Deleted Successfully"
            LblErrMessageGv.ForeColor = Drawing.Color.Green
            LblErrMessageGv.Font.Italic = True
            LblErrMessageGv.Visible = True
            LblErrorMessage.Visible = False
            BindData()
        Else
            LblErrMessageGv.Text = "Data Failed to delete"
            LblErrMessageGv.ForeColor = Drawing.Color.Red
            LblErrMessageGv.Font.Italic = True
            LblErrMessageGv.Visible = True
            LblErrorMessage.Visible = False
        End If
    End Sub

    Protected Sub GvDeemApproval_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvDeemApproval.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            'Creating a gridview object          
            Dim objGridView As GridView = CType(sender, GridView)
            'Creating a gridview row object
            Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)

            'Creating a table cell object
            Dim objtablecell As New TableCell
            AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.White.Name)
            AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.Orange.Name)
            AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.Orange.Name)
            AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.Orange.Name)
            AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.Orange.Name)
            AddMergedCells(objgridviewrow, objtablecell, 3, "SLA Configuration", System.Drawing.Color.Orange.Name)
            AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.Orange.Name)
            AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.Orange.Name)
            AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.Orange.Name)
            AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.Orange.Name)
            'Lastly add the gridrow object to the gridview object at the 0th position
            'Because, the header row position is 0.
            objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
        End If
    End Sub

#End Region

#Region "Custom Methods"
    Private Sub BindData()
        Dim ds As DataSet = controller.GetDeemApprovals_DS()
        If ds.Tables(0).Rows.Count() > 0 Then
            GvDeemApproval.DataSource = ds
            GvDeemApproval.DataBind()
        Else
            ds.Tables(0).Rows.Clear()
            ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
            GvDeemApproval.DataSource = ds
            GvDeemApproval.DataBind()
            Dim columncount As Integer = GvDeemApproval.Rows(0).Cells.Count
            GvDeemApproval.Rows(0).Cells.Clear()
            GvDeemApproval.Rows(0).Cells.Add(New TableCell())
            GvDeemApproval.Rows(0).Cells(0).ColumnSpan = columncount
            GvDeemApproval.Rows(0).Cells(0).Text = "No Records Found"
        End If
    End Sub

    Private Sub BindTransactionTypes(ByVal ddl As DropDownList)
        ddl.DataSource = controller.GetTransactionTypes()
        ddl.DataTextField = "TransactionType"
        ddl.DataValueField = "TransId"
        ddl.DataBind()

        ddl.Items.Insert(0, "--select--")
    End Sub

    Private Function GetDetailDoc(ByVal docid As Integer, ByVal transid As Integer) As DocInfo
        Dim info As TransactionTypeInfo = controller.GetTransactionTypeBaseId(transid)
        Dim strQuery As String = String.Empty
        If info.DocParentId = 0 Then
            strQuery = "select * from(select doc_id,doc_name as docname,doc_type as doctype from " & info.CodDocTable & " union all " & "select 0 as doc_id, '" & info.TransactionType & "' as docname, 'O' as doctype " & _
                " )as tb1 where doc_id=" & docid & " and doctype like '%" & BaseConfiguration.DOC_TYPE_ONLINE & "%' order by doc_id asc"
        Else
            strQuery = "select doc_id,docname, doctype from " & info.CodDocTable & " where doc_id =" & docid & " and doctype like '%" & BaseConfiguration.DOC_TYPE_ONLINE & "%' and rstatus = 2 order by doc_id asc"
        End If
        Return controller.GetDocumentDetailBaseQuery(strQuery)
    End Function

    Private Sub BindDocuments(ByVal ddl As DropDownList, ByVal transid As Integer)
        Dim info As TransactionTypeInfo = controller.GetTransactionTypeBaseId(transid)
        Dim strQuery As String = String.Empty
        If info.DocParentId = 0 Then
            strQuery = "select * from(select doc_id,doc_name as docname,doc_type as doctype from " & info.CodDocTable & " union all " & "select 0 as doc_id, '" & info.TransactionType & "' as docname, 'O' as doctype " & _
                " )as tb1 where (doctype like '%" & BaseConfiguration.DOC_TYPE_ONLINE & "%' or docid = 2001) order by doc_id asc"
        Else
            strQuery = "select doc_id,docname, doctype from " & info.CodDocTable & " where doctype like '%" & BaseConfiguration.DOC_TYPE_ONLINE & "%' and rstatus = 2 order by doc_id asc"
        End If

        ddl.DataSource = controller.GetDocumentBaseQuery(strQuery)
        ddl.DataTextField = "docname"
        ddl.DataValueField = "docid"
        ddl.DataBind()

        ddl.Items.Insert(0, "--select--")
    End Sub

    Private Sub BindUserTypes(ByVal ddl As DropDownList)
        ddl.DataSource = controller.GetUserTypes()
        ddl.DataTextField = "RoleName"
        ddl.DataValueField = "RoleId"
        ddl.DataBind()

        ddl.Items.Insert(0, "--select--")
    End Sub

    Private Function FormChecking() As Boolean
        Dim isvalid As Boolean = True
        Dim strErrMessage As String = String.Empty
        If DdlTransactionTypes.SelectedIndex = 0 And isvalid = True Then
            isvalid = False
            strErrMessage = "Please choose Transaction Type option!"
        End If
        If DdlDocuments.SelectedIndex = 0 And isvalid = True Then
            isvalid = False
            strErrMessage = "Please choose Document option!"
        End If
        If DdlHasDocGroup.SelectedIndex = 0 And isvalid = True Then
            isvalid = False
            strErrMessage = "Please confirm Document has a Group!"
        End If
        If DdlHasDocGroup.SelectedValue.Equals("1") And isvalid = True Then
            If DdlDocumentGroup.SelectedIndex = 0 And isvalid = True Then
                isvalid = False
                strErrMessage = "Please confirm Document will be linked as a group!"
            End If
        End If

        If DdlUserTypes.SelectedIndex < 0 And isvalid = True Then
            isvalid = False
            strErrMessage = "Please choose User Type option!"
        End If
        If DdlTasks.SelectedValue = "" Then
            isvalid = False
            strErrMessage = "Please choose Task Role option!"
        End If
        If isvalid = False Then
            LblErrorMessage.Visible = True
            LblErrMessageGv.Visible = False
            LblErrorMessage.Text = strErrMessage
            LblErrorMessage.ForeColor = Drawing.Color.Red
            LblErrorMessage.Font.Italic = True
        End If
        Return isvalid
    End Function

    Private Sub ClearForm()
        DdlTransactionTypes.ClearSelection()
        DdlTasks.ClearSelection()
        DdlDocuments.Items.Clear()
        DdlUserTypes.Items.Clear()
        TxtExecuteAfterSLA.Text = ""
        TxtSLADoc.Text = ""
        TxtTotalDoc.Text = ""
        TxtWarningNotif.Text = ""
    End Sub

    Private Sub AddMergedCells(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, ByVal celltext As String, ByVal backcolor As String)
        objtablecell = New TableCell
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan
        objtablecell.Style.Add("background-color", backcolor)
        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

#End Region

End Class
