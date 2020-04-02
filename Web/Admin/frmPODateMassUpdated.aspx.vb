Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class Admin_frmPODateMassUpdated
    Inherits System.Web.UI.Page

    Dim controller As New POExtendedController



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub BtnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If POUpload.HasFile = True Then
            Dim myFile As HttpPostedFile = POUpload.PostedFile
            If System.IO.Path.GetExtension(myFile.FileName.ToLower) <> ".xls" Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Only Excel files Allowed');", True)
                Exit Sub
            End If

            Dim myDataset As New DataSet()
            Dim strFileName As String = POUpload.PostedFile.FileName
            strFileName = System.IO.Path.GetFileName(strFileName)
            Dim serverpath As String = Server.MapPath("POEXTENDEDDATA")
            POUpload.PostedFile.SaveAs(serverpath + "\" + strFileName)
            Dim filepathserver As String = serverpath & "\" & strFileName
            Dim strConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
            "Data Source=" & filepathserver & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
            Dim POSheet As String = ConfigurationManager.AppSettings("POSheet").ToString
            Dim myData As New OleDbDataAdapter("Select * from [" & POSheet & "$] ", strConn)
            Dim dv As DataView
            Try
                myData.TableMappings.Add("Table", "ExcelTest")
                myData.Fill(myDataset)
                dv = myDataset.Tables(0).DefaultView
                dv.Sort = "[PONO]"
                'dv.RowFilter = "[PODate] <> ''"
            Catch ex As Exception
                If ex.Message.IndexOf("Sheet1$") = 1 Then
                    Response.Write("<script> alert('Please check sheet name in selected Excel file,it should be Sheet1')</script>")
                Else
                    Response.Write("<script> alert('Please check Column names changed for \n\n [PONO] / [PODate] \n\n in selected Excel file.')</script>")
                End If
                Exit Sub
            End Try
            If File.Exists(filepathserver) Then
                File.Delete(filepathserver)
            End If

            Dim a As String = "/PONO/PODate"
            Dim cc1 As String = ""
            Dim msg As String = ""
            For m As Integer = 0 To 1
                Dim aa As DataColumn
                aa = dv.Table.Columns(m)
                cc1 = "/" & aa.ColumnName.ToString.ToUpper & "/"
                If a.ToString.ToUpper.IndexOf(cc1) >= 0 Then
                Else
                    msg = msg & IIf(msg <> "", ",", "") & aa.ColumnName
                End If
            Next
            Dim ErrCount As Integer = 0
            Dim testflag As Boolean = True
            Dim i As Integer
            Dim strErrMessage As String = String.Empty
            For i = 0 To dv.Count - 1
                Dim dr As DataRow
                dr = dv.Item(i).Row
                Dim info As New PODataExtendedInfo
                info.PONO = dr.Item("PONO").ToString()
                info.PODate = Convert.ToDateTime(dr.Item("PODate"))
                info.ModifiedUser = CommonSite.UserName
                info.POID = 0
                If controller.PODataExtended_IU(info) = False Then
                    If i > 0 Then
                        strErrMessage += ", "
                    End If
                    strErrMessage = info.PONO
                End If
            Next
            If Not String.IsNullOrEmpty(strErrMessage) Then
                LblWarningMessage.Text = "Error in :" & strErrMessage
            Else
                LblWarningMessage.Text = "PODate successfully uploaded"
                LblWarningMessage.ForeColor = Drawing.Color.Green
                LblWarningMessage.Font.Italic = True

            End If
        End If
    End Sub

    Protected Sub GvPOData_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvPOData.RowCommand
        If e.CommandName.Equals("updatepodate") Then
            Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim LblPoNoRaw As Label = CType(row.Cells(5).FindControl("LblPoNoRaw"), Label)
            Dim LblPOIDRaw As Label = CType(row.Cells(5).FindControl("LblPOIDRaw"), Label)
            Dim LblNewPODate As Label = CType(row.Cells(5).FindControl("LblNewPODate"), Label)

            If Not LblPoNoRaw Is Nothing AndAlso Not LblPOIDRaw Is Nothing AndAlso Not LblNewPODate Is Nothing Then
                Dim info As New PODataExtendedInfo
                info.POID = Convert.ToString(e.CommandArgument.ToString())
                info.ModifiedUser = CommonSite.UserName
                info.PONO = LblPoNoRaw.Text
                info.PODate = DateTime.ParseExact(LblNewPODate.Text, "dd-MMMM-yyyy", Nothing)
                UpdatePODate(info, Convert.ToInt32(LblPOIDRaw.Text))
            End If

        ElseIf e.CommandName.Equals("deletepodate") Then
            DeletePODataRaw(Convert.ToInt32(e.CommandArgument.ToString()))
        End If
    End Sub


    Protected Sub LbtRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtRefresh.Click
        BindData()
    End Sub
#Region "Custom Methods"

    Private Sub BindData()
        GvPOData.DataSource = controller.GetPODataRawExtended()
        GvPOData.DataBind()
    End Sub

    Private Sub UpdatePODate(ByVal info As PODataExtendedInfo, ByVal poidraw As Int32)
        Dim isSucceed As Boolean = True
        If controller.PODataExtended_IU(info) = True Then
            If controller.PODataRawExtended_D(poidraw) = True Then
                LblGvWarningMessage.Text = "New PO Date successfully updated"
                LblGvWarningMessage.Visible = True
                LblGvWarningMessage.ForeColor = Drawing.Color.Green
                LblGvWarningMessage.Font.Italic = True
            Else
                isSucceed = False
            End If
        Else
            isSucceed = False
        End If

        If isSucceed = True Then
            BindData()
        Else
            LblGvWarningMessage.Text = "New PO Date Fail updated"
            LblGvWarningMessage.Visible = True
            LblGvWarningMessage.ForeColor = Drawing.Color.Red
            LblGvWarningMessage.Font.Italic = True
        End If
    End Sub

    Private Sub DeletePODataRaw(ByVal poid As Int32)
        If controller.PODataRawExtended_D(poid) = True Then
            LblGvWarningMessage.Text = "New PO Date successfully cancelled"
            LblGvWarningMessage.Visible = True
            LblGvWarningMessage.ForeColor = Drawing.Color.Green
            LblGvWarningMessage.Font.Italic = True
            BindData()
        Else
            LblGvWarningMessage.Text = "New PO Date Fail cancelled"
            LblGvWarningMessage.Visible = True
            LblGvWarningMessage.ForeColor = Drawing.Color.Red
            LblGvWarningMessage.Font.Italic = True
        End If
    End Sub

    Private Sub upgATPReport_Init(sender As Object, e As EventArgs) Handles upgATPReport.Init
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "adjustSize();", True)
    End Sub

    Private Sub upgATPReport_Load(sender As Object, e As EventArgs) Handles upgATPReport.Load
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "adjustSize();", True)
    End Sub
#End Region

End Class
