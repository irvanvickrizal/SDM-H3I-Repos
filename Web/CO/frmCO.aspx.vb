Imports CRFramework
Imports System.Data
Imports Common
Imports BusinessLogic

Partial Class CO_frmCO
    Inherits System.Web.UI.Page
    Dim objbl As New BODDLs
    Dim controller As New CRController
    Dim co_controller As New COController
    Dim objdb As New DBUtil
    Dim objBo As New BOSiteDocs
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        Page.Form.Attributes.Add("enctype", "multipart/form-data")
        If Not Page.IsPostBack Then
            HdnCOID.Value = "0"
            BindSupportingDocument()
            BindWorkFlow()
            BindFormAttribute()
            BindPricing()

        End If
    End Sub

#Region "GridView for CO List Description Event"

    Protected Sub gvDetails_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName.Equals("AddNew") Then
            Dim txtDescription As TextBox = CType(gvDetails.FooterRow.FindControl("txtftDescription"), TextBox)
            Dim txtContractType As TextBox = CType(gvDetails.FooterRow.FindControl("txtftContractType"), TextBox)
            Dim txtContractConfiguration As TextBox = CType(gvDetails.FooterRow.FindControl("txtftContractConfiguration"), TextBox)
            Dim txtCRType As TextBox = CType(gvDetails.FooterRow.FindControl("txtftCRType"), TextBox)
            Dim txtCRConfiguration As TextBox = CType(gvDetails.FooterRow.FindControl("txtftCRConfiguration"), TextBox)
            AddUpdateCOList(0, txtDescription.Text, txtContractType.Text, txtContractConfiguration.Text, txtCRType.Text, txtCRConfiguration.Text)
            BindCOBudget()
            BindPercentageImpact()
        End If
    End Sub

    Protected Sub gvDetails_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvDetails.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'getting username from particular row
            Dim desc As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Description"))
            'identifying the control in gridview
            Dim lnkbtnresult As ImageButton = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
            'raising javascript confirmationbox whenver user clicks on link button
            If Not lnkbtnresult Is Nothing Then
                lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + desc + "')")
            End If
        End If
    End Sub

    Protected Sub gvDetails_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim colistid As Int32 = Convert.ToInt32(gvDetails.DataKeys(e.RowIndex).Values("Value_Id").ToString())
        DeleteCOListDescription(colistid)
        BindCOBudget()
        BindPercentageImpact()
    End Sub

    Protected Sub gvDetails_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim colistid As Int32 = Convert.ToInt32(gvDetails.DataKeys(e.RowIndex).Value.ToString())
        Dim txtDescription As TextBox = CType(gvDetails.Rows(e.RowIndex).FindControl("txtDescription"), TextBox)
        Dim txtContractType As TextBox = CType(gvDetails.Rows(e.RowIndex).FindControl("txtContractType"), TextBox)
        Dim txtContractConfiguration As TextBox = CType(gvDetails.Rows(e.RowIndex).FindControl("txtContractConfiguration"), TextBox)
        Dim txtCRType As TextBox = CType(gvDetails.Rows(e.RowIndex).FindControl("txtCRType"), TextBox)
        Dim txtCRConfiguration As TextBox = CType(gvDetails.Rows(e.RowIndex).FindControl("txtCRConfiguration"), TextBox)
        AddUpdateCOList(colistid, txtDescription.Text, txtContractType.Text, txtContractConfiguration.Text, txtCRType.Text, txtCRConfiguration.Text)
        LblResult.ForeColor = System.Drawing.Color.Green
        LblResult.Text = txtDescription.Text + " Details Updated successfully"
        gvDetails.EditIndex = -1
        BindDescriptionofChange()
        BindCOBudget()
        BindPercentageImpact()
    End Sub

    Protected Sub gvDetails_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        gvDetails.EditIndex = -1
        BindDescriptionofChange()
        BindCOBudget()
        BindPercentageImpact()
    End Sub

    Protected Sub gvDetails_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        gvDetails.EditIndex = e.NewEditIndex
        BindDescriptionofChange()
        BindCOBudget()
        BindPercentageImpact()
    End Sub

    Protected Sub gvDetails_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvDetails.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            'Creating a gridview object            
            Dim objGridView As GridView = CType(sender, GridView)

            'Creating a gridview row object
            Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)

            'Creating a table cell object
            Dim objtablecell As New TableCell

            AddMergedCells(objgridviewrow, objtablecell, 2, "", System.Drawing.Color.White.Name)
            AddMergedCells(objgridviewrow, objtablecell, 2, "Contract", System.Drawing.Color.Orange.Name)
            AddMergedCells(objgridviewrow, objtablecell, 2, "Change Request", System.Drawing.Color.Orange.Name)
            'Lastly add the gridrow object to the gridview object at the 0th position
            'Because, the header row position is 0.
            objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
        End If
    End Sub

#End Region

#Region "GridView for CO Budget"
    Protected Sub GvBudgetImpact_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName.Equals("AddNew") Then
            Dim txtPONO As TextBox = CType(GvBudgetImpact.FooterRow.FindControl("txtftPONO"), TextBox)
            Dim txtDescription As TextBox = CType(GvBudgetImpact.FooterRow.FindControl("txtftDescription"), TextBox)
            Dim txtContractUSD As HtmlControls.HtmlInputText = CType(GvBudgetImpact.FooterRow.FindControl("txtftContractUSD"), HtmlControls.HtmlInputText)
            Dim txtContractIDR As HtmlControls.HtmlInputText = CType(GvBudgetImpact.FooterRow.FindControl("txtftContractIDR"), HtmlControls.HtmlInputText)
            Dim txtCRUSD As HtmlControls.HtmlInputText = CType(GvBudgetImpact.FooterRow.FindControl("txtftCRUSD"), HtmlControls.HtmlInputText)
            Dim txtCRIDR As HtmlControls.HtmlInputText = CType(GvBudgetImpact.FooterRow.FindControl("txtftCRIDR"), HtmlControls.HtmlInputText)
            Dim info As New COBudgetInfo
            info.COBudgetId = 0
            info.PONO = txtPONO.Text
            info.BudgetDescription = txtDescription.Text
            info.ContractEUR = 0
            info.ContractUSD = Convert.ToDouble(txtContractUSD.Value)
            info.ContractIDR = Convert.ToDouble(txtContractIDR.Value)
            info.CREUR = 0
            info.CRUSD = Convert.ToDouble(txtCRUSD.Value)
            info.CRIDR = Convert.ToDouble(txtCRIDR.Value)
            info.DeltaEUR = 0
            info.DeltaUSD = info.CRUSD - info.ContractUSD
            info.DeltaIDR = info.CRIDR - info.ContractIDR
            info.PackageId = Request.QueryString("wpid")
            info.LMBYBudget = CommonSite.UserName
            AddUpdateCOBudget(info)
            BindPricing()
            BindDescriptionofChange()
            BindPercentageImpact()
        End If
    End Sub

    Protected Sub GvBudgetImpact_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvBudgetImpact.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'getting username from particular row
            Dim desc As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Description"))
            'identifying the control in gridview
            Dim lnkbtnresult As ImageButton = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
            'raising javascript confirmationbox whenver user clicks on link button
            If Not lnkbtnresult Is Nothing Then
                lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + desc + "')")
            End If

            'Dim lblContractUSD As Label = CType(e.Row.FindControl("lblContractUSD"), Label)
            'Dim lblContractIDR As Label = CType(e.Row.FindControl("lblContractIDR"), Label)
            'Dim lblCRUSD As Label = CType(e.Row.FindControl("lblCRUSD"), Label)
            'Dim lblCRIDR As Label = CType(e.Row.FindControl("lblCRIDR"), Label)
            'Dim lblDeltaUSD As Label = CType(e.Row.FindControl("lblDeltaUSD"), Label)
            'Dim lblDeltaIDR As Label = CType(e.Row.FindControl("lblDeltaIDR"), Label)

            'lblContractUSD.Text = String.Format("{0:###,##.#0}", DataBinder.Eval(e.Row.DataItem, "Contract_USD"))
            'lblContractIDR.Text = String.Format("{0:###,##.#0}", DataBinder.Eval(e.Row.DataItem, "Contract_IDR"))
            'lblCRUSD.Text = String.Format("{0:###,##.#0}", DataBinder.Eval(e.Row.DataItem, "CR_USD"))
            'lblCRIDR.Text = String.Format("{0:###,##.#0}", DataBinder.Eval(e.Row.DataItem, "CR_IDR"))
            'lblDeltaUSD.Text = String.Format("{0:###,##.#0}", DataBinder.Eval(e.Row.DataItem, "Delta_USD"))
            'lblDeltaIDR.Text = String.Format("{0:###,##.#0}", DataBinder.Eval(e.Row.DataItem, "Delta_IDR"))

        End If
    End Sub

    Protected Sub GvBudgetImpact_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim cobudgetid As Int32 = Convert.ToInt32(GvBudgetImpact.DataKeys(e.RowIndex).Values("COBudget_Id").ToString())
        DeleteRowCoBudget(cobudgetid)
        BindPricing()
        BindDescriptionofChange()
        BindPercentageImpact()
    End Sub

    Protected Sub GvBudgetImpact_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim cobudgetid As Int32 = Convert.ToInt32(GvBudgetImpact.DataKeys(e.RowIndex).Value.ToString())
        Dim txtPONO As TextBox = CType(GvBudgetImpact.Rows(e.RowIndex).FindControl("txtPONO"), TextBox)
        Dim txtDescription As TextBox = CType(GvBudgetImpact.Rows(e.RowIndex).FindControl("txtDescription"), TextBox)
        Dim txtContractUSD As HtmlControls.HtmlInputText = CType(GvBudgetImpact.Rows(e.RowIndex).FindControl("txtContractUSD"), HtmlControls.HtmlInputText)
        Dim txtContractIDR As HtmlControls.HtmlInputText = CType(GvBudgetImpact.Rows(e.RowIndex).FindControl("txtContractIDR"), HtmlControls.HtmlInputText)
        Dim txtCRUSD As HtmlControls.HtmlInputText = CType(GvBudgetImpact.Rows(e.RowIndex).FindControl("txtCRUSD"), HtmlControls.HtmlInputText)
        Dim txtCRIDR As HtmlControls.HtmlInputText = CType(GvBudgetImpact.Rows(e.RowIndex).FindControl("txtCRIDR"), HtmlControls.HtmlInputText)
        Dim info As New COBudgetInfo
        info.COBudgetId = cobudgetid
        info.PONO = txtPONO.Text
        info.BudgetDescription = txtDescription.Text
        info.ContractEUR = 0
        info.ContractUSD = Convert.ToDouble(txtContractUSD.Value)
        info.ContractIDR = Convert.ToDouble(txtContractIDR.Value)
        info.CREUR = 0
        info.CRUSD = Convert.ToDouble(txtCRUSD.Value)
        info.CRIDR = Convert.ToDouble(txtCRIDR.Value)
        info.DeltaEUR = 0
        info.DeltaUSD = info.CRUSD - info.ContractUSD
        info.DeltaIDR = info.CRIDR - info.ContractIDR
        info.PackageId = Request.QueryString("wpid")
        info.LMBYBudget = CommonSite.UserName
        AddUpdateCOBudget(info)
        lblResultBudgetImpact.ForeColor = System.Drawing.Color.Green
        lblResultBudgetImpact.Text = txtDescription.Text + " Details Updated successfully"
        GvBudgetImpact.EditIndex = -1
        BindCOBudget()
        BindPricing()
        BindDescriptionofChange()
        BindPercentageImpact()
    End Sub

    Protected Sub GvBudgetImpact_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        GvBudgetImpact.EditIndex = -1
        BindCOBudget()
        BindPricing()
        BindDescriptionofChange()
        BindPercentageImpact()
    End Sub

    Protected Sub GvBudgetImpact_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        GvBudgetImpact.EditIndex = e.NewEditIndex
        BindCOBudget()
        BindPricing()
        BindDescriptionofChange()
        BindPercentageImpact()
    End Sub

    Protected Sub GvBudgetImpact_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvBudgetImpact.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            'Creating a gridview object            
            Dim objGridView As GridView = CType(sender, GridView)

            'Creating a gridview row object
            Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)

            'Creating a table cell object
            Dim objtablecell As New TableCell
            AddMergedCells(objgridviewrow, objtablecell, 3, "", System.Drawing.Color.White.Name)
            AddMergedCells(objgridviewrow, objtablecell, 2, "Contract", System.Drawing.Color.Orange.Name)
            AddMergedCells(objgridviewrow, objtablecell, 2, "Change Request", System.Drawing.Color.Orange.Name)
            AddMergedCells(objgridviewrow, objtablecell, 2, "Delta", System.Drawing.Color.Orange.Name)
            'Lastly add the gridrow object to the gridview object at the 0th position
            'Because, the header row position is 0.
            objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
        End If
    End Sub
#End Region

#Region "Percentage GridView"
    Protected Sub GvPercentageImpact_RowUpdating(ByVal sender As Object, ByVal ex As GridViewUpdateEventArgs)
        Dim percChangeId As Int32 = Convert.ToInt32(GvPercentageImpact.DataKeys(ex.RowIndex).Value.ToString())
        Dim txtContractUSD As HtmlControls.HtmlInputText = CType(GvPercentageImpact.Rows(ex.RowIndex).FindControl("txtContractUSD"), HtmlControls.HtmlInputText)
        Dim txtContractIDR As HtmlControls.HtmlInputText = CType(GvPercentageImpact.Rows(ex.RowIndex).FindControl("txtContractIDR"), HtmlControls.HtmlInputText)
        Dim txtCRUSD As HtmlControls.HtmlInputText = CType(GvPercentageImpact.Rows(ex.RowIndex).FindControl("txtCRUSD"), HtmlControls.HtmlInputText)
        Dim txtCRIDR As HtmlControls.HtmlInputText = CType(GvPercentageImpact.Rows(ex.RowIndex).FindControl("txtCRIDR"), HtmlControls.HtmlInputText)
        Dim txtDeltaUSD As HtmlControls.HtmlInputText = CType(GvPercentageImpact.Rows(ex.RowIndex).FindControl("txtDeltaUSD"), HtmlControls.HtmlInputText)
        Dim txtDeltaIDR As HtmlControls.HtmlInputText = CType(GvPercentageImpact.Rows(ex.RowIndex).FindControl("txtDeltaIDR"), HtmlControls.HtmlInputText)
        Dim info As New COBudgetPercentageInfo
        info.PercChangeId = percChangeId
        info.PackageId = Request.QueryString("wpid")
        info.PercContractEUR = 0
        info.PercContractIDR = Convert.ToDouble(txtContractIDR.Value)
        info.PercContractUSD = Convert.ToDouble(txtContractUSD.Value)
        info.PercImplementationEUR = 0
        info.PercImplementationIDR = Convert.ToDouble(txtCRIDR.Value)
        info.PercImplementationUSD = Convert.ToDouble(txtCRUSD.Value)
        info.PercDeltaEUR = 0
        info.PercDeltaIDR = Convert.ToDouble(txtDeltaIDR.Value)
        info.PercDeltaUSD = Convert.ToDouble(txtDeltaUSD.Value)
        info.LMBY = CommonSite.UserName
        co_controller.InsertUpdatePercentageImpact(info)
        GvPercentageImpact.EditIndex = -1
        BindPercentageImpact()
        BindDescriptionofChange()
        BindCOBudget()
    End Sub

    Protected Sub GvPercentageImpact_RowCancelingEdit(ByVal sender As Object, ByVal ex As GridViewCancelEditEventArgs)
        GvPercentageImpact.EditIndex = -1
        BindPercentageImpact()
        BindDescriptionofChange()
        BindCOBudget()
    End Sub

    Protected Sub GvPercentageImpact_RowEditing(ByVal sender As Object, ByVal ex As GridViewEditEventArgs)
        GvPercentageImpact.EditIndex = ex.NewEditIndex
        BindPercentageImpact()
        BindDescriptionofChange()
        BindCOBudget()
    End Sub

    Protected Sub GvPercentageImpact_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvPercentageImpact.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            'Creating a gridview object            
            Dim objGridView As GridView = CType(sender, GridView)

            'Creating a gridview row object
            Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)

            'Creating a table cell object
            Dim objtablecell As New TableCell
            AddMergedCells(objgridviewrow, objtablecell, 2, "", System.Drawing.Color.White.Name)
            AddMergedCells(objgridviewrow, objtablecell, 2, "Contract", System.Drawing.Color.Orange.Name)
            AddMergedCells(objgridviewrow, objtablecell, 2, "Change Request", System.Drawing.Color.Orange.Name)
            AddMergedCells(objgridviewrow, objtablecell, 2, "Delta", System.Drawing.Color.Orange.Name)
            'Lastly add the gridrow object to the gridview object at the 0th position
            'Because, the header row position is 0.
            objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
        End If
    End Sub
#End Region

    Protected Sub DdlWorkflowType_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlWorkflowType.SelectedIndexChanged
        If Integer.Parse(DdlWorkflowType.SelectedValue) > 0 Then
            BindTselSignature(4, Request.QueryString("wpid"), Integer.Parse(DdlWorkflowType.SelectedValue))
            BindNSNSignature(1, Request.QueryString("wpid"), Integer.Parse(DdlWorkflowType.SelectedValue))
        End If
    End Sub

    Protected Sub LbtSaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSave.Click
        If co_controller.CheckDocSupportingCOIsCompleted(Request.QueryString("wpid"), Integer.Parse(ConfigurationManager.AppSettings("codocid"))) = 0 Then
            If RptDigitalSignTelkomsel.Items.Count > 0 Or RptDigitalSignNSN.Items.Count > 0 Then
                LblErrorMessageWorkflow.Visible = False
                Dim coid As Int32 = SaveData()
                If coid > 0 Then
                    Response.Redirect("../BAUT/frmTI_CO.aspx?id=" & coid & "&listtype=" & Request.QueryString("listtype"))
                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('error during Transaction Process, Please Try Again');", True)
                End If
            Else
                LblErrorMessageWorkflow.Visible = True
                LblErrorMessageWorkflow.Text = "Please define workflow first!"
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please upload document support completely!');", True)
        End If
        
    End Sub

    Protected Sub LbtCancelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtCancel.Click
        Response.Redirect("frmCOReadyCreation.aspx")
    End Sub

    Protected Sub GvDocAttachmentRowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvDocAttachment.RowCommand
        If e.CommandName.Equals("AddDoc") Then
            AddSupportingDocument(Integer.Parse(e.CommandArgument.ToString()))
        End If
    End Sub

    Protected Sub GvDocAttachmentNeedUploadRowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvUploadDocAttachment.RowCommand
        If e.CommandName.Equals("deletedoc") Then
            DeleteSupportingDocument(Convert.ToString(e.CommandArgument))
        ElseIf e.CommandName.Equals("uploaddoc") Then
            Dim row As GridViewRow = CType(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim LblDocName As Label = CType(row.FindControl("LblDocName"), Label)
            ChangePanelDocUpload(LblDocName.Text, Convert.ToInt32(e.CommandArgument.ToString()))
        End If
    End Sub

    Protected Sub LbtUploadDocSupportClick(ByVal sender As Object, ByVal e As EventArgs) Handles LbtUploadDocSupport.Click
        If FuDocument.HasFile Then
            LblErrorMessageUpload.Visible = True
            LblErrorMessageUpload.Text = "Document Upload Successfully!"
            LblErrorMessageUpload.ForeColor = Drawing.Color.Green
            uploaddocument(Convert.ToInt32(HdnSWID.Value), FuDocument, LblErrorMessageUpload)
        Else
            LblErrorMessageUpload.Text = "Please Choose File will be uploaded first!"
            LblErrorMessageUpload.Visible = True
            LblErrorMessageUpload.ForeColor = Drawing.Color.Red
        End If
    End Sub


#Region "custom methods"

    Private Sub BindPricing()
        If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            Dim info As CRInfo = controller.GetIndicativePriceCRFinal(Request.QueryString("wpid"))
            If Not info Is Nothing Then
                LblIndicativePriceUSD.Text = String.Format("{0:###,##.#0}", info.IndicativePriceCostUSD)
                lblIndicativePriceIDR.Text = String.Format("{0:###,##.#0}", info.IndicativePriceCostIDR)
                Dim i As Integer
                Dim deltaTotalUSD As Double = 0
                Dim deltaTotalIDR As Double = 0
                For i = 0 To GvBudgetImpact.Rows.Count - 1
                    Dim LblDeltaUSD As Label = CType(GvBudgetImpact.Rows(i).Cells(0).FindControl("lbDeltaUSD"), Label)
                    Dim LblDeltaIDR As Label = CType(GvBudgetImpact.Rows(i).Cells(0).FindControl("lbDeltaIDR"), Label)
                    If Not LblDeltaUSD Is Nothing Then
                        If Not String.IsNullOrEmpty(LblDeltaUSD.Text) Then
                            deltaTotalUSD += Convert.ToDouble(LblDeltaUSD.Text)
                        Else
                            deltaTotalUSD = 0
                        End If
                    Else
                        deltaTotalUSD = 0
                    End If

                    If Not LblDeltaIDR Is Nothing Then
                        If Not String.IsNullOrEmpty(LblDeltaIDR.Text) Then
                            deltaTotalIDR += Convert.ToDouble(LblDeltaIDR.Text)
                        Else
                            deltaTotalIDR = 0
                        End If
                    Else
                        deltaTotalIDR = 0
                    End If

                    
                Next
                LblTotalDeltaUSD.Text = String.Format("{0:###,##.#0}", deltaTotalUSD)
                LblTotalDeltaIDR.Text = String.Format("{0:###,##.#0}", deltaTotalIDR)
                LblTotalBalanceUSD.Text = String.Format("{0:###,##.#0}", (deltaTotalUSD - info.IndicativePriceCostUSD))
                LblTotalBalanceIDR.Text = String.Format("{0:###,##.#0}", (deltaTotalIDR - info.IndicativePriceCostIDR))
            End If
        End If
    End Sub

    Private Sub AddSupportingDocument(ByVal docid As Integer)
        Dim perc As Double = 0
        Dim sCount As Integer = objdb.ExeQueryScalar("select count(*) from sitedoc where siteid=" & HdnSiteId.Value & " and version=" & HdnVersion.Value)
        perc = Format(100 / (sCount + 1), "0.00")
        Dim i As Integer = 0
        i = objBo.uspSiteDocIU(HdnSiteId.Value, Constants.STATUS_ACTIVE, Session("User_Name"), docid, perc, 1, HdnVersion.Value, , LblPONo.Text)
        If i = 0 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
        End If
        BindSupportingDocument()
    End Sub

    Private Sub DeleteSupportingDocument(ByVal docregid As String)
        Dim perc As Double = 0
        Dim sCount As Integer = objdb.ExeQueryScalar("select count(*) from sitedoc where siteid=" & HdnSiteId.Value & " and version=" & HdnVersion.Value)
        perc = Format(100 / (sCount - 1), "0.00")
        'objdb.ExeNonQuery("delete sitedoc where sw_id=" & docregid)
        'objdb.ExeNonQuery("update sitedoc set percentage=" & perc & " where siteid=" & HdnSiteId.Value & " and version=" & HdnVersion.Value)
        co_controller.DeleteCOSupportingDocument(Convert.ToInt32(docregid), perc)
        BindSupportingDocument()
    End Sub

    Private Sub BindSupportingDocument()
        BindDocumentAttachment()
        BindDocumentNeedUpload()
    End Sub

    Private Sub BindDocumentAttachment()
        If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            GvDocAttachment.DataSource = co_controller.GetListDocAttachment(Integer.Parse(ConfigurationManager.AppSettings("codocid")), Request.QueryString("wpid"))
            GvDocAttachment.DataBind()
        End If
    End Sub

    Private Sub BindDocumentNeedUpload()
        If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            MvCoreUploadDocAttachment.SetActiveView(VwListDocUpload)
            GvUploadDocAttachment.DataSource = co_controller.GetDocAttachmentNeedUpload(Integer.Parse(ConfigurationManager.AppSettings("codocid")), Request.QueryString("wpid"))
            GvUploadDocAttachment.DataBind()
        End If
    End Sub

    Private Sub ChangePanelDocUpload(ByVal docname As String, ByVal swid As Int32)
        MvCoreUploadDocAttachment.SetActiveView(vwDocUpload)
        LblDocName.Text = docname
        HdnSWID.Value = swid
    End Sub

    Sub uploaddocument(ByVal swid As Int32, ByVal fuDoc As FileUpload, ByVal errorMessage As Label)
        If fuDoc.HasFile Then
            Dim FileNamePath As String = String.Empty
            Dim FileNameOnly As String = String.Empty
            Dim ReFileName As String = String.Empty
            Dim ft As String = String.Empty
            Dim path As String = String.Empty

            ft = ConfigurationManager.AppSettings("Type") & LblPONo.Text & "-" & Request.QueryString("wpid") & "\" & "CO\AttachmentDoc\"
            Dim fpath As String = ConfigurationManager.AppSettings("BufferPath")
            path = ConfigurationManager.AppSettings("Fpath") & LblSiteID.Text & ft
            Dim DocPath As String = ""
            Dim err As Boolean = False
            'Dim strResult As String = DOInsertTrans(packageid, path)

            FileNamePath = fuDoc.PostedFile.FileName
            FileNameOnly = System.IO.Path.GetFileName(fuDoc.PostedFile.FileName)
            ReFileName = Format(CDate(DateTime.Now), "ddMMyyyyHHss") & FileNameOnly
            Dim orgDocpath As String = LblSiteID.Text & ft & ReFileName
            DocPath = LblSiteID.Text & ft & Common.TakeFileUpload.ConvertAnyFormatToPDF(fuDoc, path)

            If Not err Then
                Dim isSucceed As Boolean = co_controller.UploadAttachmentDocument(swid, DocPath, orgDocpath, Convert.ToInt32(CommonSite.UserId), Convert.ToInt32(HdnCOID.Value))
                If isSucceed = False Then
                    Response.Write("<script language='javascript'>alert('Update failed please check your uploaded file name..');</script>")
                    Exit Sub
                Else
                    BindDocumentNeedUpload()
                End If
            Else
                Response.Write("<script language='javascript'>alert('Document upload failed');</script>")
            End If
        Else
            errorMessage.Text = "Please choose File first!"
            errorMessage.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Private Sub BindWorkFlow()
        objbl.fillDDL(DdlWorkflowType, "TWorkFlow", False, Constants._DDL_Default_Select)
        DdlWorkflowType.Items.Insert(0, "-- Choose Workflow --")
    End Sub

    Private Sub BindTselSignature(ByVal grpid As Integer, ByVal packageid As String, ByVal wfid As Integer)
        RptDigitalSignTelkomsel.DataSource = controller.GetUserApproverByGrouping(packageid, grpid, wfid)
        RptDigitalSignTelkomsel.DataBind()
    End Sub

    Private Sub BindNSNSignature(ByVal grpid As Integer, ByVal packageid As String, ByVal wfid As Integer)
        RptDigitalSignNSN.DataSource = controller.GetUserApproverByGrouping(packageid, grpid, wfid)
        RptDigitalSignNSN.DataBind()
    End Sub

    Private Sub BindFormAttribute()
        BindSiteAtt()
        BindInitiator()
        BindOldData()
        BindDescriptionofChange()
        BindCOBudget()
        BindPercentageImpact()
    End Sub

    Private Sub BindSiteAtt()
        If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            Dim strQuery As String = "select top 1 siteNo, sitename,PoNo,poname,RGNName,TselProjectID,SiteId,SiteVersion from poepmsitenew where workpackageid='" & Request.QueryString("wpid") & "'"
            Dim dtSiteAtt As DataTable = objdb.ExeQueryDT(strQuery, "siteAtt")
            If dtSiteAtt.Rows.Count > 0 Then
                LblSiteID.Text = dtSiteAtt.Rows(0).Item(0).ToString()
                LblSiteName.Text = dtSiteAtt.Rows(0).Item(1).ToString()
                LblArea.Text = dtSiteAtt.Rows(0).Item(4).ToString()
                LblPONo.Text = dtSiteAtt.Rows(0).Item(2).ToString()
                LblEOName.Text = dtSiteAtt.Rows(0).Item(3).ToString()
                LblProjectType.Text = "2G"
                LblProjectID.Text = dtSiteAtt.Rows(0).Item(5).ToString()
                LblDateSubmitted.Text = String.Format("{0:dd-MMM-yyyy}", DateTime.Now)
                LblProjectCategory.Text = "TI"
                HdnSiteId.Value = dtSiteAtt.Rows(0).Item(6).ToString()
                HdnVersion.Value = dtSiteAtt.Rows(0).Item(7).ToString()
            End If
        End If
    End Sub

    Private Sub BindOldData()
        If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            Dim info As COInfo = GetCODetail(Request.QueryString("wpid"))
            ChkRegulatoryRequirement.Checked = info.IsRegulatoryRequirement
            ChkSiteCondition.Checked = info.IsSiteCondition
            ChkDesignChange.Checked = info.IsDesignChange
            ChkTechnicalError.Checked = info.IsTechnicalErrorOmission
            ChkOther.Checked = info.IsOther
            TxtDescription_JustificationComments.Text = info.DescriptionComment
            txtTechnicalImpact.Text = info.technicalImpact
            TxtScheduleImpact.Text = info.ScheduleImpact
            TxtOtherImpact.Text = info.OtherImpact
            DdlWorkflowType.SelectedValue = info.WFID
            HdnCOID.Value = Convert.ToString(info.COID)
            BindTselSignature(4, Request.QueryString("wpid"), info.WFID)
            BindNSNSignature(1, Request.QueryString("wpid"), info.WFID)
        End If
    End Sub

    Private Function GetCODetail(ByVal packageid As String) As COInfo
        Return co_controller.GetODCOByWPID(packageid)
    End Function

    Private Sub BindInitiator()
        Dim strQuery As String = "select top 1 name, usrType,usrRole from ebastusers_1 where usr_id=" & CommonSite.UserId
        Dim dtInitiator As DataTable = objdb.ExeQueryDT(strQuery, "init")
        If dtInitiator.Rows.Count > 0 Then
            LblInitiatorName.Text = dtInitiator.Rows(0).Item(0)
            LblInitiatorArea.Text = LblArea.Text
        End If
        If dtInitiator.Rows(0).Item(1).ToString.ToLower().Equals("n") Then
            LblInitiatorDepartment.Text = "Nokia Siemens Networks"
        Else
            LblInitiatorDepartment.Text = "Telkomsel"
        End If
    End Sub

    Private Sub BindDescriptionofChange()
        Dim ds As DataSet = co_controller.GetCOListDesc_DS(Request.QueryString("wpid"))
        If (ds.Tables(0).Rows.Count > 0) Then
            gvDetails.DataSource = ds
            gvDetails.DataBind()
        Else
            ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
            gvDetails.DataSource = ds
            gvDetails.DataBind()
            Dim columncount As Integer = gvDetails.Rows(0).Cells.Count
            gvDetails.Rows(0).Cells.Clear()
            gvDetails.Rows(0).Cells.Add(New TableCell())
            gvDetails.Rows(0).Cells(0).ColumnSpan = columncount
            gvDetails.Rows(0).Cells(0).Text = "No Records Found"
        End If

    End Sub

    Private Sub BindCOBudget()
        Dim ds As DataSet = co_controller.GetCOBudget_DS(Request.QueryString("wpid"))
        If (ds.Tables(0).Rows.Count > 0) Then
            GvBudgetImpact.DataSource = ds
            GvBudgetImpact.DataBind()
        Else
            ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
            GvBudgetImpact.DataSource = ds
            GvBudgetImpact.DataBind()
            Dim columncount As Integer = GvBudgetImpact.Rows(0).Cells.Count
            GvBudgetImpact.Rows(0).Cells.Clear()
            GvBudgetImpact.Rows(0).Cells.Add(New TableCell())
            GvBudgetImpact.Rows(0).Cells(0).ColumnSpan = columncount
            GvBudgetImpact.Rows(0).Cells(0).Text = "No Records Found"
        End If
    End Sub

    Private Sub AddUpdateCOList(ByVal colistid As Int32, ByVal description As String, ByVal contractType As String, ByVal contractConfig As String, ByVal crType As String, ByVal crConfig As String)
        Dim info As New COListChangeInfo
        info.COListId = colistid
        info.ListDescription = description
        info.ContractType = contractType
        info.ContractConfiguration = contractConfig
        info.CRType = crType
        info.CRConfiguration = crConfig
        info.PackageId = Request.QueryString("wpid")
        info.LMBY = CommonSite.UserName
        If (co_controller.InsertUpdateODCRList(info) = True) Then
            LblResult.Visible = False
            BindDescriptionofChange()
        Else
            LblResult.Visible = True
            LblResult.Text = "Failed during Insert new CO Description of Change"
        End If
    End Sub

    Private Sub DeleteCOListDescription(ByVal colistid As Int32)
        If co_controller.DeleteCOListDescription(colistid) = True Then
            LblResult.Visible = False
            BindDescriptionofChange()
        Else
            LblResult.Visible = True
            LblResult.Text = "Failed during Delete the Data"
        End If
    End Sub

    Private Sub AddMergedCells(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, ByVal celltext As String, ByVal backcolor As String)
        objtablecell = New TableCell
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan
        objtablecell.Style.Add("background-color", backcolor)
        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

    Private Sub AddUpdateCOBudget(ByVal info As COBudgetInfo)
        If co_controller.InsertUpdateODCOBudget(info) = True Then
            lblResultBudgetImpact.Visible = False
            BindCOBudget()
        Else
            lblResultBudgetImpact.Visible = True
            lblResultBudgetImpact.Text = "Failed during Insert new CO Budget"
            lblResultBudgetImpact.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Private Sub DeleteRowCoBudget(ByVal cobudgetid As Int32)
        If co_controller.DeleteODCOBudget(cobudgetid) = True Then
            lblResultBudgetImpact.Visible = False
            BindCOBudget()
        Else
            lblResultBudgetImpact.Visible = True
            lblResultBudgetImpact.Text = "Failed Delete Row of Budget"
            lblResultBudgetImpact.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Private Function SaveData() As Int32
        Dim info As New COInfo
        info.COID = Convert.ToInt32(HdnCOID.Value)
        info.DescriptionComment = TxtDescription_JustificationComments.Text
        info.IsDesignChange = IIf(ChkDesignChange.Checked = True, True, False)
        info.IsOther = IIf(ChkOther.Checked = True, True, False)
        info.IsRegulatoryRequirement = IIf(ChkRegulatoryRequirement.Checked = True, True, False)
        info.IsSiteCondition = IIf(ChkSiteCondition.Checked = True, True, False)
        info.IsTechnicalErrorOmission = IIf(ChkTechnicalError.Checked = True, True, False)
        info.LMBY = CommonSite.UserName
        info.OtherImpact = TxtOtherImpact.Text
        info.PackageId = Request.QueryString("wpid")
        info.ScheduleImpact = TxtScheduleImpact.Text
        info.technicalImpact = txtTechnicalImpact.Text
        info.WFID = Integer.Parse(DdlWorkflowType.SelectedValue)
        Return co_controller.InsertUpdateODCO(info, ConfigurationManager.AppSettings("CODOCID"))
    End Function


    Private Sub BindPercentageImpact()
        Dim ds As DataSet = co_controller.GetPercentageChangeCO_DS(Request.QueryString("wpid"))
        GvPercentageImpact.DataSource = ds
        GvPercentageImpact.DataBind()
    End Sub

#End Region

End Class
