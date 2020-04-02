Imports CRFramework
Imports System.Data
Imports System.IO

Partial Class CO_frmCOTransactionDetail
    Inherits System.Web.UI.Page
    Dim controller As New COController
    Dim trancontroller As New COTransactionNAController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindSiteAttribute(GetCOID())
            BindTransaction(GetCOID())
        End If
    End Sub

    Protected Sub GvCOTransaction_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvCOTransaction.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LblStartDateTime As Label = CType(e.Row.FindControl("LblStartDateTime"), Label)
            Dim LblEndDateTime As Label = CType(e.Row.FindControl("LblEndDateTime"), Label)
            Dim LblExecutionUser As Label = CType(e.Row.FindControl("LblExecutionUser"), Label)
            Dim imgbtnEdit As ImageButton = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
            If Not LblStartDateTime Is Nothing And Not LblEndDateTime Is Nothing And Not LblExecutionUser Is Nothing And Not imgbtnEdit Is Nothing Then
                If String.IsNullOrEmpty(LblEndDateTime.Text) Then
                    LblExecutionUser.Text = "-"
                    imgbtnEdit.Visible = True
                Else
                    imgbtnEdit.Visible = False
                End If

                If String.IsNullOrEmpty(LblStartDateTime.Text) Then
                    imgbtnEdit.Visible = False
                End If
            End If
        End If
    End Sub

    Protected Sub GvCOTransaction_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        GvCOTransaction.EditIndex = -1
        BindTransaction(GetCOID())
    End Sub

    Protected Sub GvCOTransaction_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        GvCOTransaction.EditIndex = e.NewEditIndex
        
        BindTransaction(GetCOID())
        Dim lblRoleID As Label = CType(GvCOTransaction.Rows(e.NewEditIndex).FindControl("LblRoleID"), Label)
        Dim DdlUserRole As DropDownList = CType(GvCOTransaction.Rows(e.NewEditIndex).FindControl("DdlUserRole"), DropDownList)

        If Not lblRoleID Is Nothing And Not DdlUserRole Is Nothing Then
            DdlUserRole.DataSource = trancontroller.GetUserByRoleID_Location(Integer.Parse(lblRoleID.Text), tdwpid.InnerText)
            DdlUserRole.DataTextField = "Fullname"
            DdlUserRole.DataValueField = "userid"
            DdlUserRole.DataBind()

            DdlUserRole.Items.Insert(0, "--select user --")
        End If
    End Sub

    Protected Sub GvCOTransaction_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim DdlUserRole As DropDownList = CType(GvCOTransaction.Rows(e.RowIndex).FindControl("DdlUserRole"), DropDownList)
        Dim LblRoleID As Label = CType(GvCOTransaction.Rows(e.RowIndex).FindControl("LblRoleID"), Label)
        Dim LblStartDateTimeTest As Label = CType(GvCOTransaction.Rows(e.RowIndex).FindControl("LblStartDateTimeTest"), Label)
        Dim TxtEndDateTime As TextBox = CType(GvCOTransaction.Rows(e.RowIndex).FindControl("TxtEndDateTime"), TextBox)
        Dim LblErrorMessage As Label = CType(GvCOTransaction.Rows(e.RowIndex).FindControl("LblErrorMessage"), Label)
        Dim LblTaskID As Label = CType(GvCOTransaction.Rows(e.RowIndex).FindControl("LblTaskID"), Label)
        Dim LblSNO As Label = CType(GvCOTransaction.Rows(e.RowIndex).FindControl("LblSNO"), Label)
        Dim LblWFID As Label = CType(GvCOTransaction.Rows(e.RowIndex).FindControl("LblWFID"), Label)

        Dim strError As String = String.Empty
        If DdlUserRole Is Nothing Then
            strError += "DdlUserRole Nothing"
        End If

        If LblRoleID Is Nothing Then
            strError += "LblRoleID Nothing"
        End If

        If LblStartDateTimeTest Is Nothing Then
            strError += "LblStartDateTime is Nothing"
        End If

        If TxtEndDateTime Is Nothing Then
            strError += "TxtEndDAteTime is Nothing"
        End If

        If Not DdlUserRole Is Nothing And Not LblRoleID Is Nothing And Not LblStartDateTimeTest Is Nothing And Not TxtEndDateTime Is Nothing Then
            Dim dt1 As DateTime = DateTime.ParseExact(LblStartDateTimeTest.Text, "dd-MMM-yyyy", Nothing)
            Dim dt2 As DateTime = DateTime.ParseExact(TxtEndDateTime.Text, "dd-MMM-yyyy", Nothing)
            Dim df As Integer = DateDiff(DateInterval.Day, dt1, dt2)
            If df < 0 Then
                LblErrorMessage.Text = "Date can't be overlapped from Start Time!"
                LblErrorMessage.Visible = True
            Else
                LblErrorMessage.Visible = False
                LblErrorMessage.Text = ""
                UpdateDocTransaction(Convert.ToInt32(LblSNO.Text), GetCOID(), Integer.Parse(LblWFID.Text), Integer.Parse(LblTaskID.Text), Integer.Parse(LblRoleID.Text), Integer.Parse(DdlUserRole.SelectedValue), dt2, CommonSite.UserId)
                GvCOTransaction.EditIndex = -1
                BindTransaction(GetCOID())
            End If
            
        End If

    End Sub

    Protected Sub BtnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        uploaddocument(FUCOUpload, LblErrorMessage)
    End Sub

    Protected Sub ImgDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImgDocDelete.Click

    End Sub


#Region "Custom Methods"
    Private Sub BindSiteAttribute(ByVal coid As Int32)
        Dim info As COInfo = GetCODetail(coid)
        tdpono.InnerText = info.PONO
        tdponame.InnerText = info.EOName
        tdsiteno.InnerText = info.SiteNo
        tdsitename.InnerText = info.SiteName
        tdscope.InnerText = info.Scope
        tdwpid.InnerText = info.PackageId
        tdprojectId.InnerText = info.ProjectID
        viewdoclink.HRef = "../PO/frmViewCODocument.aspx?swid=" & Convert.ToString(GetSWID()) & "&parent=bast"
        If trancontroller.CheckIsReplacementCODocumentAlreadyUploaded(coid) = False Then
            viewreplacementdoclink.Visible = False
            LblReplacementStatus.Visible = True
            FUCOUpload.Visible = True
            btnUpload.Visible = True
        Else
            viewreplacementdoclink.Visible = True
            LblReplacementStatus.Visible = False
            FUCOUpload.Visible = False
            btnUpload.Visible = False
            viewreplacementdoclink.HRef = "../fancybox_form/fb_ViewDocumentCOReplacement.aspx?coid=" & Convert.ToString(GetCOID())
        End If
    End Sub

    Private Sub BindTransaction(ByVal coid As Int32)
        Dim ds As DataSet = trancontroller.GetCOTransaction_DS(coid)
        If (ds.Tables(0).Rows.Count > 0) Then
            GvCOTransaction.DataSource = trancontroller.GetCOTransaction_DS(coid)
            GvCOTransaction.DataBind()
        End If
    End Sub


    Private Function GetCODetail(ByVal coid As Int32) As COInfo
        Return controller.GetODCO(coid)
    End Function

    Private Function GetCOID() As Int32
        Dim coid As Int32 = 0
        If Not String.IsNullOrEmpty(Request.QueryString("coid")) Then
            coid = Convert.ToInt32(Request.QueryString("coid"))
        End If
        Return coid
    End Function

    Private Function GetSWID() As Int32
        Dim swid As Int32 = 0
        If Not String.IsNullOrEmpty(Request.QueryString("swid")) Then
            swid = Convert.ToInt32(Request.QueryString("swid"))
        End If
        Return swid
    End Function

    Private Sub UpdateDocTransaction(ByVal sno As Int32, ByVal coid As Int32, ByVal wfid As Integer, ByVal tskid As Integer, ByVal roleid As Integer, ByVal userid As Integer, ByVal enddatetime As DateTime, ByVal lmby As Integer)
        trancontroller.UpdateCOTransactionManualProcess(sno, coid, wfid, tskid, roleid, userid, enddatetime, lmby)
    End Sub

    
    Sub uploaddocument(ByVal fuDoc As FileUpload, ByVal errorMessage As Label)
        If fuDoc.HasFile Then
            Dim FileNamePath As String = String.Empty
            Dim FileNameOnly As String = String.Empty
            Dim ReFileName As String = String.Empty
            Dim ft As String = String.Empty
            Dim path As String = String.Empty

            ft = ConfigurationManager.AppSettings("Type") & tdpono.InnerText & "-" & tdwpid.InnerText & "\" & "CO\ManualProcess\"
            Dim fpath As String = ConfigurationManager.AppSettings("BufferPath")
            path = ConfigurationManager.AppSettings("Fpath") & tdsiteno.InnerText & ft
            Dim DocPath As String = ""
            Dim err As Boolean = False
            'Dim strResult As String = DOInsertTrans(packageid, path)

            FileNamePath = fuDoc.PostedFile.FileName
            FileNameOnly = System.IO.Path.GetFileName(fuDoc.PostedFile.FileName)
            ReFileName = Format(CDate(DateTime.Now), "ddMMyyyyHHss") & FileNameOnly
            Dim orgDocpath As String = tdsiteno.InnerText & ft & ReFileName
            DocPath = tdsiteno.InnerText & ft & Common.TakeFileUpload.ConvertAnyFormatToPDF(fuDoc, path)

            If Not err Then
                'Dim isSucceed As Boolean = co_controller.UploadAttachmentDocument(swid, DocPath, orgDocpath, Convert.ToInt32(CommonSite.UserId), Convert.ToInt32(HdnCOID.Value))
                Dim info As New CODocumentReplacementInfo
                info.COID = GetCOID()
                info.DocPath = DocPath
                info.IsUploaded = True
                info.LMBY = CommonSite.UserId
                Dim isSucceed As Boolean = trancontroller.CODocumentReplacement_IU(info)
                If isSucceed = False Then
                    Response.Write("<script language='javascript'>alert('Update failed please check your uploaded file name..');</script>")
                    Exit Sub
                Else
                    BindSiteAttribute(GetCOID())
                End If
            Else
                Response.Write("<script language='javascript'>alert('Document upload failed');</script>")
            End If
        Else
            errorMessage.Text = "Please choose File first with pdf format!"
            errorMessage.ForeColor = Drawing.Color.Red
        End If
    End Sub

#End Region
End Class
