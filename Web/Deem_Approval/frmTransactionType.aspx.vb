Imports System.Data

Partial Class Deem_Approval_frmTransactionType
    Inherits System.Web.UI.Page

    Dim controller As New DeemApprovalController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub LbtSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSave.Click
        If FormChecking() = True Then
            Dim info As New TransactionTypeInfo
            info.TransId = 0
            info.Description = TxtTransDescription.Text
            info.TransactionType = DdlTransactionType.SelectedValue
            info.TransTable = TxtTransTable.Text
            info.CMAInfo.ModifiedUser = CommonSite.UserName
            info.CodDocTable = TxtMasterDocTable.Text
            info.DocParentId = Integer.Parse(DdlParentDocs.SelectedValue)
            If controller.TransactionType_IU(info) = True Then
                LblErrMessageGv.Visible = False
                LblErrorMessage.Visible = True
                LblErrorMessage.Text = "Data Successfully Saved"
                LblErrorMessage.ForeColor = Drawing.Color.Green
                LblErrorMessage.Font.Italic = True
                ClearForm()
            Else
                LblErrMessageGv.Visible = False
                LblErrorMessage.Visible = True
                LblErrorMessage.Text = "Data failed to save"
                LblErrorMessage.ForeColor = Drawing.Color.Red
                LblErrorMessage.Font.Italic = True
            End If
            BindData()
        End If
        
    End Sub

#Region "Gridview SOAC Value"

    Protected Sub GvTransactionType_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvTransactionType.RowDataBound
       
        If e.Row.RowType = DataControlRowType.DataRow Then
            'getting username from particular row
            Dim desc As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Transaction_Type"))
            'identifying the control in gridview
            Dim lnkbtnresult As ImageButton = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
            'raising javascript confirmationbox whenver user clicks on link button
            If Not lnkbtnresult Is Nothing Then
                lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + desc + "')")
            End If


            ' Row Item Template
            Dim parentdocid As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Doc_Parent_Id"))
            Dim DdlParentDocsGv As DropDownList = CType(e.Row.FindControl("DdlParentDocsGv"), DropDownList)
            Dim DdlParentDocsGvEdit As DropDownList = CType(e.Row.FindControl("DdlParentDocsGvEdit"), DropDownList)

            If DdlParentDocsGvEdit IsNot Nothing Then
                DdlParentDocsGvEdit.SelectedValue = parentdocid
            End If

            If DdlParentDocsGv IsNot Nothing Then
                DdlParentDocsGv.SelectedValue = parentdocid
                DdlParentDocsGv.Enabled = False
            End If
        End If
    End Sub

    Protected Sub GvTransactionType_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        GvTransactionType.EditIndex = e.NewEditIndex
        BindData()
    End Sub

    Protected Sub GvTransactionType_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim valueid As Integer = Integer.Parse(GvTransactionType.DataKeys(e.RowIndex).Value.ToString())
        Dim TxtTransactionTypeEdit As TextBox = CType(GvTransactionType.Rows(e.RowIndex).FindControl("TxtTransactionTypeEdit"), TextBox)
        Dim TxtTransactionDescEdit As TextBox = CType(GvTransactionType.Rows(e.RowIndex).FindControl("TxtTransactionDescEdit"), TextBox)
        Dim TxtTransactionTableEdit As TextBox = CType(GvTransactionType.Rows(e.RowIndex).FindControl("TxtTransactionTableEdit"), TextBox)
        Dim TxtCODDocTableEdit As TextBox = CType(GvTransactionType.Rows(e.RowIndex).FindControl("TxtCODDocTableEdit"), TextBox)
        Dim DdlParentDocsGvEdit As DropDownList = CType(GvTransactionType.Rows(e.RowIndex).FindControl("DdlParentDocsGvEdit"), DropDownList)

        If TxtTransactionDescEdit IsNot Nothing And TxtTransactionTypeEdit IsNot Nothing And TxtTransactionTableEdit IsNot Nothing And TxtCODDocTableEdit IsNot Nothing And DdlParentDocsGvEdit IsNot Nothing Then
            Dim info As New TransactionTypeInfo
            info.TransId = valueid
            info.TransactionType = TxtTransactionTypeEdit.Text
            info.TransTable = TxtTransactionTableEdit.Text
            info.Description = TxtTransactionDescEdit.Text
            info.CMAInfo.ModifiedUser = CommonSite.UserName
            info.CodDocTable = TxtCODDocTableEdit.Text
            info.DocParentId = Integer.Parse(DdlParentDocsGvEdit.SelectedValue)
            If controller.TransactionType_IU(info) = True Then
                GvTransactionType.EditIndex = -1
                LblErrMessageGv.Visible = True
                LblErrorMessage.Visible = False
                LblErrMessageGv.Text = "Data Successfully Updated"
                LblErrMessageGv.ForeColor = Drawing.Color.Green
                LblErrMessageGv.Font.Italic = True
                BindData()
            Else
                LblErrMessageGv.Visible = True
                LblErrorMessage.Visible = False
                LblErrMessageGv.Text = "Data Failed to Update"
                LblErrMessageGv.ForeColor = Drawing.Color.Red
                LblErrMessageGv.Font.Italic = True
            End If
        End If
    End Sub

    Protected Sub GvTransactionType_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        GvTransactionType.EditIndex = -1
        BindData()
    End Sub

    Protected Sub GvTransactionType_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim valueid As Integer = Convert.ToInt32(GvTransactionType.DataKeys(e.RowIndex).Values("Trans_Id").ToString())
        If controller.TransactionType_D(valueid) = True Then
            LblErrMessageGv.Text = "Data Successfully deleted"
            LblErrMessageGv.Visible = True
            LblErrorMessage.Visible = False
            LblErrMessageGv.ForeColor = Drawing.Color.Green
            LblErrMessageGv.Font.Italic = True
        Else
            LblErrMessageGv.Text = "Data Fail is deleted"
            LblErrMessageGv.Visible = True
            LblErrorMessage.Visible = False
            LblErrMessageGv.ForeColor = Drawing.Color.Red
            LblErrMessageGv.Font.Italic = True
        End If
        BindData()
    End Sub
#End Region

#Region "Custom Methods"
    Private Sub BindData()
        Dim ds As DataSet = controller.GetTransactionTypes_DS()
        If ds.Tables(0).Rows.Count() > 0 Then
            GvTransactionType.DataSource = ds
            GvTransactionType.DataBind()
        Else
            ds.Tables(0).Rows.Clear()
            ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
            GvTransactionType.DataSource = ds
            GvTransactionType.DataBind()
            Dim columncount As Integer = GvTransactionType.Rows(0).Cells.Count
            GvTransactionType.Rows(0).Cells.Clear()
            GvTransactionType.Rows(0).Cells.Add(New TableCell())
            GvTransactionType.Rows(0).Cells(0).ColumnSpan = columncount
            GvTransactionType.Rows(0).Cells(0).Text = "No Records Found"
        End If
    End Sub

    Private Function FormChecking() As Boolean
        Dim isValid As Boolean = True
        Dim strErrMessage As String = String.Empty
        If DdlTransactionType.SelectedIndex = 0 And isValid = True Then
            isValid = False
            strErrMessage = "Please Choose Transaction Type!"
        End If
        If String.IsNullOrEmpty(TxtTransTable.Text) And isValid = True Then
            isValid = False
            strErrMessage = "Please fill Transaction Table field!"
        End If

        If (DdlParentDocs.SelectedValue = "-1") Then
            isValid = False
            strErrMessage = "Please choose Parent Doc option!"
        End If

        If isValid = False Then
            LblErrorMessage.Visible = True
            LblErrorMessage.Text = strErrMessage
            LblErrorMessage.ForeColor = Drawing.Color.Red
            LblErrorMessage.Font.Italic = True
        End If

        Return isValid
    End Function

    Private Sub ClearForm()
        DdlParentDocs.SelectedValue = "0"
        TxtTransDescription.Text = ""
        TxtTransTable.Text = ""
        TxtMasterDocTable.Text = ""
        DdlParentDocs.ClearSelection()
    End Sub
#End Region

End Class
