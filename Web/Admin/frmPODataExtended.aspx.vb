Imports System.Data
Imports System.IO

Partial Class Admin_frmPODataExtended
    Inherits System.Web.UI.Page


    Dim controller As New POExtendedController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            If Not String.IsNullOrEmpty(TxtPONOSearch.Text) Then
                BindData(TxtPONOSearch.Text)
            Else
                BindData(String.Empty)
            End If
        End If
    End Sub

    Protected Sub BtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        If Not String.IsNullOrEmpty(TxtPONOSearch.Text) Then
            BindData(TxtPONOSearch.Text)
        Else
            BindData(String.Empty)
        End If
    End Sub

    Protected Sub GvPOData_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvPOData.PageIndexChanging
        GvPOData.PageIndex = e.NewPageIndex
        If Not String.IsNullOrEmpty(TxtPONOSearch.Text) Then
            BindData(TxtPONOSearch.Text)
        Else
            BindData(String.Empty)
        End If
    End Sub


    Protected Sub LbtExportToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtExportToExcel.Click
        If (GvNullPODate.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "GvNullPODate_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GvNullPODate)
            frm.RenderControl(hw)
            Response.Write(tw.ToString())
            Response.End()
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "invalidExportToExcel();", True)
            End If
        End If
    End Sub

#Region "GVPOData Event"
    Protected Sub GvPOData_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvPOData.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            ' Row Item Template
            Dim lblPODate As Label = CType(e.Row.FindControl("LblPODate"), Label)
            


            If Not lblPODate Is Nothing Then
                If Not String.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PODate"))) Then
                    lblPODate.Text = String.Format("{0:dd-MMMM-yyyy}", Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "PODate").ToString()))
                Else
                    lblPODate.Text = "-"
                End If
            End If



            ' Row Edit Item Template
            Dim TxtPODate As TextBox = CType(e.Row.FindControl("TxtPODate"), TextBox)
            

            If Not TxtPODate Is Nothing Then
                If Not String.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PODate"))) Then
                    TxtPODate.Text = String.Format("{0:dd-MMMM-yyyy}", Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "PODate").ToString()))
                Else
                    TxtPODate.Text = ""
                End If
            End If

        End If
    End Sub

    

    Protected Sub GvPOData_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        GvPOData.EditIndex = e.NewEditIndex
        If Not String.IsNullOrEmpty(TxtPONOSearch.Text) Then
            BindData(TxtPONOSearch.Text)
        Else
            BindData(String.Empty)
        End If
    End Sub

    Protected Sub GvPOData_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim valueid As Int32 = Convert.ToInt32(GvPOData.DataKeys(e.RowIndex).Value.ToString())
        Dim LblPoNo As Label = CType(GvPOData.Rows(e.RowIndex).FindControl("LblPoNo"), Label)
        Dim TxtPODate As TextBox = CType(GvPOData.Rows(e.RowIndex).FindControl("TxtPODate"), TextBox)

        If Not LblPoNo Is Nothing AndAlso Not TxtPODate Is Nothing Then
            Dim info As New PODataExtendedInfo
            info.POID = valueid
            info.PONO = LblPoNo.Text
            info.ModifiedUser = CommonSite.UserName
            info.PODate = DateTime.ParseExact(TxtPODate.Text, "dd-MMMM-yyyy", Nothing)
            GvPOData.EditIndex = -1
            Dim isSucceed As Boolean = AddUpdatePODataValue(info)
            If isSucceed = True Then
                If Not String.IsNullOrEmpty(TxtPONOSearch.Text) Then
                    BindData(TxtPONOSearch.Text)
                Else
                    BindData(String.Empty)
                End If
                LblGvWarningMessage.Text = "PO Data Updated Successfully"
                LblGvWarningMessage.Visible = True
                LblGvWarningMessage.Font.Italic = True
                LblGvWarningMessage.ForeColor = Drawing.Color.Green
            Else
                LblGvWarningMessage.Text = "PO Data Update Failed"
                LblGvWarningMessage.Visible = True
                LblGvWarningMessage.Font.Italic = True
                LblGvWarningMessage.ForeColor = Drawing.Color.Red
            End If
        End If
        
    End Sub

    Protected Sub GvPOData_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        GvPOData.EditIndex = -1
        If Not String.IsNullOrEmpty(TxtPONOSearch.Text) Then
            BindData(TxtPONOSearch.Text)
        Else
            BindData(String.Empty)
        End If
    End Sub


#End Region

    Protected Sub LbtMassUpdated_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtMassUpdated.Click
        Response.Redirect("frmPODateMassUpdated.aspx")
    End Sub

#Region "Custom Methods"

    Private Sub BindData(ByVal pono As String)
        Dim ds As DataSet = controller.GetPODataExtended_DS(pono)

        If ds.Tables(0).Rows.Count() > 0 Then
            GvPOData.DataSource = ds
            GvPOData.DataBind()
        Else
            ds.Tables(0).Rows.Clear()
            ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
            GvPOData.DataSource = ds
            GvPOData.DataBind()
            Dim columncount As Integer = GvPOData.Rows(0).Cells.Count
            GvPOData.Rows(0).Cells.Clear()
            GvPOData.Rows(0).Cells.Add(New TableCell())
            GvPOData.Rows(0).Cells(0).ColumnSpan = columncount
            GvPOData.Rows(0).Cells(0).Text = "No Records Found"
        End If

        BindDataNullPODate()
    End Sub

    Private Sub BindDataNullPODate()
        GvNullPODate.DataSource = controller.GetPODataExtended_NullPODate()
        GvNullPODate.DataBind()
    End Sub

    Private Sub upgATPReport_Load(sender As Object, e As EventArgs) Handles upgATPReport.Load
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "adjustSize();", True)
    End Sub

    Private Sub upgATPReport_Init(sender As Object, e As EventArgs) Handles upgATPReport.Init
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "adjustSize();", True)
    End Sub

    Private Function AddUpdatePODataValue(ByVal info As PODataExtendedInfo) As Boolean
        Return controller.PODataExtended_IU(info)
    End Function
#End Region
End Class
