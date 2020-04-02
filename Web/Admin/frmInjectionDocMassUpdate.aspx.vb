Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Imports System.Collections.Generic

Partial Class Admin_frmInjectionDocMassUpdate
    Inherits System.Web.UI.Page
    Dim controller As New CODInjectionController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindErrorData(Nothing)
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
            Dim serverpath As String = Server.MapPath("INJECTIONDOC")
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
                dv.Sort = "[WPID]"
                'dv.RowFilter = "[PODate] <> ''"
            Catch ex As Exception
                If ex.Message.IndexOf("Sheet1$") = 1 Then
                    Response.Write("<script> alert('Please check sheet name in selected Excel file,it should be Sheet1')</script>")
                Else
                    Response.Write("<script> alert('Please check Column names changed for \n\n [WPID] / [DOCID] / [Remarks] \n\n in selected Excel file.')</script>")
                End If
                Exit Sub
            End Try
            If File.Exists(filepathserver) Then
                File.Delete(filepathserver)
            End If

            Dim a As String = "/WPID/DOCID/Remarks/"
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
            Dim countSuccess As Integer = 0
            Dim countFailed As Integer = 0
            Dim listfailed As New List(Of CODInjectionDocInfo)
            For i = 0 To dv.Count - 1
                Dim dr As DataRow
                dr = dv.Item(i).Row
                Dim info As New CODInjectionDocInfo
                info.PackageId = Convert.ToString(dr.Item("WPID"))
                info.Docid = Integer.Parse(dr.Item("DOCID").ToString())
                info.Remarks = Convert.ToString(dr.Item("Remarks"))
                info.LMBY = CommonSite.UserId
                Dim docdetail As DocInfo = controller.GetDocDetail(info.Docid)
                If docdetail Is Nothing Then
                    info.Status = "Doc ID Not found"
                    info.Docname = "Undeclared"
                    countFailed += 1
                    listfailed.Add(info)
                Else
                    If info.PackageId IsNot Nothing Then
                        If controller.IsAvalaiblePackageId(info.PackageId) = True Then
                            If controller.IsAvalaibleDocInjection(info.Docid, info.PackageId) = False Then
                                info.InjectionId = controller.GetInjectionTypeIDBaseParentID(docdetail.ParentId)
                                If String.IsNullOrEmpty(controller.InjectionDoc_I(info)) = True Then
                                    info.Status = "Error while inserting data"
                                    countFailed += 1
                                    listfailed.Add(info)
                                Else
                                    countSuccess += 1
                                End If
                            Else
                                info.Docname = docdetail.DocName
                                info.Status = "Injection Doc Already exist for " & docdetail.DocName
                                countFailed += 1
                                listfailed.Add(info)
                            End If
                        Else
                            info.Status = "WPID Not Found"
                            countFailed += 1
                            listfailed.Add(info)
                        End If
                    End If
                End If
            Next
            LblWarningMessage.Text = countSuccess.ToString() & " Data successfully inserted, and " & countFailed.ToString() & " Data is Failed"
            LblWarningMessage.ForeColor = Drawing.Color.Green
            LblWarningMessage.Font.Italic = True
            If countFailed > 0 Then
                BindErrorData(listfailed)
            End If
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindErrorData(ByVal list As List(Of CODInjectionDocInfo))
        GvInjectionDocExist.DataSource = list
        GvInjectionDocExist.DataBind()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("../COD/frmCODInjectionDocument.aspx")
    End Sub
#End Region
End Class
