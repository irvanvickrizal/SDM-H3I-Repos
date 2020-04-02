Imports System.Data
Imports System.Web
Imports System.Globalization

Partial Class Deem_Approval_frmMasterBusinessDate
    Inherits System.Web.UI.Page

    Dim controller As New BDController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

#Region "Gridview Business Day"

    Protected Sub GvBusinessDay_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvBusinessDay.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'getting username from particular row
            Dim desc As String = String.Format("{0:dd-MMMM-yyyy}", Convert.ToDateTime((DataBinder.Eval(e.Row.DataItem, "Off_date"))))
            'identifying the control in gridview
            Dim lnkbtnresult As ImageButton = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
            'raising javascript confirmationbox whenver user clicks on link button
            If Not lnkbtnresult Is Nothing Then
                lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + desc + "')")
            End If
        End If
    End Sub
    
    Protected Sub GvBusinessDay_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        GvBusinessDay.EditIndex = e.NewEditIndex
        BindData()
    End Sub

    Protected Sub GvBusinessDay_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim valueid As Integer = Integer.Parse(GvBusinessDay.DataKeys(e.RowIndex).Value.ToString())
        Dim TxtOffDayEdit As TextBox = CType(GvBusinessDay.Rows(e.RowIndex).FindControl("TxtOffDayEdit"), TextBox)
        Dim TxtDescEdit As TextBox = CType(GvBusinessDay.Rows(e.RowIndex).FindControl("TxtDescEdit"), TextBox)
        If TxtOffDayEdit IsNot Nothing And TxtDescEdit IsNot Nothing Then
            Dim info As New BusinessDayInfo
            info.BDID = valueid
            info.OffDate = Date.ParseExact(TxtOffDayEdit.Text, "dd-MMMM-yyyy", Nothing)
            info.Description = TxtDescEdit.Text
            info.CMAInfo.ModifiedUser = CommonSite.UserName
            If controller.BusinessDay_IsExist(info.OffDate) = False Then
                If controller.BussinessDay_IU(info) = True Then
                    GvBusinessDay.EditIndex = -1
                    LblErrMessageGv.Text = "Data Successfully Updated"
                    LblErrMessageGv.Visible = True
                    LblErrorMessage.Visible = False
                    LblErrMessageGv.ForeColor = Drawing.Color.Green
                    LblErrMessageGv.Font.Italic = True
                    BindData()
                Else
                    LblErrMessageGv.Text = "Data Fail to Updated"
                    LblErrMessageGv.Visible = True
                    LblErrorMessage.Visible = False
                    LblErrMessageGv.ForeColor = Drawing.Color.Red
                    LblErrMessageGv.Font.Italic = True
                End If
            Else
                LblErrMessageGv.Text = "Date Already Exist, please try again!"
                LblErrMessageGv.Visible = True
                LblErrorMessage.Visible = False
                LblErrMessageGv.ForeColor = Drawing.Color.Green
                LblErrMessageGv.Font.Italic = True
            End If
        End If
    End Sub

    Protected Sub GvBusinessDay_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        GvBusinessDay.EditIndex = -1
        BindData()
    End Sub

    Protected Sub GvBusinessDay_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim valueid As Integer = Convert.ToInt32(GvBusinessDay.DataKeys(e.RowIndex).Values("BD_Id").ToString())
        If controller.BusinessDay_D(valueid) = True Then
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

    Protected Sub LbtSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSave.Click
        If FormCValidation() = True Then
            Dim info As New BusinessDayInfo
            info.BDID = 0
            info.OffDate = Date.ParseExact(TxtOffDate.Text, "dd-MMMM-yyyy", Nothing)
            info.Description = TxtDescription.Text
            info.CMAInfo.ModifiedUser = CommonSite.UserName
            If controller.BusinessDay_IsExist(info.OffDate) = False Then
                If controller.BussinessDay_IU(info) = True Then
                    LblErrorMessage.Visible = True
                    LblErrorMessage.Text = "New Date is successfully added"
                    LblErrorMessage.ForeColor = Drawing.Color.Green
                    LblErrorMessage.Font.Italic = True
                    BindData()
                Else
                    LblErrorMessage.Visible = True
                    LblErrorMessage.Text = "Failed while saving Data, Please Try again"
                    LblErrorMessage.ForeColor = Drawing.Color.Red
                    LblErrorMessage.Font.Italic = True
                End If
            Else
                LblErrorMessage.Visible = True
                LblErrorMessage.Text = "Date already exist, please try again!"
                LblErrorMessage.ForeColor = Drawing.Color.Red
                LblErrorMessage.Font.Italic = True
            End If
            

        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        Dim ds As DataSet = controller.GetAllBusinessDay_DS()
        If ds.Tables.Count > 0 Then
            GvBusinessDay.DataSource = ds
            GvBusinessDay.DataBind()
        Else
            ds.Tables(0).Rows.Clear()
            ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
            GvBusinessDay.DataSource = ds
            GvBusinessDay.DataBind()
            Dim columncount As Integer = GvBusinessDay.Rows(0).Cells.Count
            GvBusinessDay.Rows(0).Cells.Clear()
            GvBusinessDay.Rows(0).Cells.Add(New TableCell())
            GvBusinessDay.Rows(0).Cells(0).ColumnSpan = columncount
            GvBusinessDay.Rows(0).Cells(0).Text = "No Records Found"
        End If
    End Sub

    Private Function FormCValidation() As Boolean
        Dim isvalid As Boolean = True
        Dim strErrMessage As String = String.Empty
        Dim thisCulture As Object = CultureInfo.CurrentCulture
        TxtOffDate.ReadOnly = False
        Dim dayOfWeek As DayOfWeek = thisCulture.Calendar.GetDayOfWeek(Date.ParseExact(TxtOffDate.Text, "dd-MMMM-yyyy", Nothing))
        If String.IsNullOrEmpty(TxtOffDate.Text) And isvalid = True Then
            isvalid = False
            strErrMessage = "Please fill Off Date Field"
        End If
        If thisCulture.DateTimeFormat.GetDayName(dayOfWeek).Equals("Saturday") And isvalid = True Then
            isvalid = False
            strErrMessage = "Saturday is default off day, please input another day"
        End If
        If thisCulture.DateTimeFormat.GetDayName(dayOfWeek).Equals("Sunday") And isvalid = True Then
            isvalid = False
            strErrMessage = "Sunday is default off day, please input another day"
        End If
        If controller.BusinessDay_IsExist(Date.ParseExact(TxtOffDate.Text, "dd-MMMM-yyyy", Nothing)) = True And isvalid = True Then
            isvalid = False
            strErrMessage = "Business day already exist"
        End If
        If isvalid = False Then
            LblErrorMessage.Visible = True
            LblErrorMessage.Text = strErrMessage
            LblErrorMessage.ForeColor = Drawing.Color.Red
            LblErrorMessage.Font.Italic = True
        End If
        Return isvalid
    End Function
#End Region
End Class
