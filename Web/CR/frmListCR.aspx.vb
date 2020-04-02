Imports Common
Imports BusinessLogic
Imports DAO
Imports System.Data

Imports CRFramework

Partial Class CR_frmListCR
    Inherits System.Web.UI.Page
    Dim objdb As New DBUtil
    Dim controller As New CRController
    Dim ft As String = String.Empty
    Dim path As String = String.Empty
    Dim sitedocinf As New CRSitedocInfo

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
                TxtPackageId.Text = Request.QueryString("wpid")
                HdnCRIDAsFinal.Value = controller.GetCRIDInFinalCR(TxtPackageId.Text)
                BindListCR(TxtPackageId.Text)
                CheckingFinalCR(HdnCRIDAsFinal.Value)
            End If
        End If
    End Sub

    Protected Sub BtnSearchClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        If Not String.IsNullOrEmpty(TxtPackageId.Text) Then
            If (objdb.ExeQueryScalar("select count(*) from poepmsitenew where workpackageid='" & TxtPackageId.Text & "' and status='active'")) > 0 Then
                Dim isCRNeeded = objdb.ExeQueryScalar("select count(*) from sitedoc sd " & _
                " inner join poepmsitenew po on po.siteid = sd.siteid and po.siteversion = sd.version where po.workpackageid ='" & TxtPackageId.Text & "' and docid =" & ConfigurationManager.AppSettings("CRDocId"))
                If isCRNeeded > 0 Then
                    HdnCRIDAsFinal.Value = controller.GetCRIDInFinalCR(TxtPackageId.Text)
                    CheckingFinalCR(HdnCRIDAsFinal.Value)
                    BindListCR(TxtPackageId.Text)
                Else
                    MvMainPanel.SetActiveView(vwNoCRNeeded)
                End If
            Else
                MvMainPanel.SetActiveView(vwNoFound)
            End If
        End If
    End Sub

    Protected Sub BtnAddCRClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtAddCR.Click
        HdnCRID.Value = Nothing
        AddNewCR()
    End Sub

    Protected Sub BtnMOMUploadClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnview.Click
        If FUMOMUpload.HasFile Then
            uploaddocument(TxtPackageId.Text)
        End If
    End Sub

    Protected Sub GvListCRItemCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvListCR.RowCommand
        If e.CommandName.Equals("addmom") Then
            HdnCRID.Value = e.CommandArgument.ToString()
            AddNewCR()
            BindDocumentMOM(GvListDocMOM, Convert.ToInt32(HdnCRID.Value))
        ElseIf e.CommandName.Equals("generate") Then
            Response.Redirect("frmNewChangeRequest.aspx?id=" & e.CommandArgument.ToString() & "&wpid=" & TxtPackageId.Text)
        ElseIf e.CommandName.Equals("deletecr") Then
            DeleteCR(Convert.ToInt32(e.CommandArgument.ToString()))
        End If
    End Sub

    Protected Sub GVListCRRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvListCR.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Or e.Row.RowType = DataControlRowType.Footer Then
            Dim gvlistadddoc As GridView = CType(e.Row.FindControl("GvListCRAdditionalDoc"), GridView)
            If Not gvlistadddoc Is Nothing Then
                Dim lblCRID As Label = CType(e.Row.FindControl("LblCRID"), Label)
                Dim ImgGenerateCRForm As ImageButton = CType(e.Row.FindControl("ImgGenerateCRForm"), ImageButton)
                Dim ImgDelete As ImageButton = CType(e.Row.FindControl("ImgDelete"), ImageButton)
                Dim ImgAddMom As ImageButton = CType(e.Row.FindControl("ImgAddMom"), ImageButton)
                Dim LblCRStatus As Label = CType(e.Row.FindControl("LblCRStatus"), Label)
                If LblCRStatus.Text.ToLower().Equals("document approved") Then
                    ImgAddMom.Visible = False
                End If
                If Not lblCRID Is Nothing And Not String.IsNullOrEmpty(lblCRID.Text) Then
                    BindDocumentMOM(gvlistadddoc, Convert.ToInt32(lblCRID.Text))
                End If
                If Not ImgGenerateCRForm Is Nothing And Not ImgDelete Is Nothing And Not ImgAddMom Is Nothing Then
                    If Convert.ToInt32(HdnCRIDAsFinal.Value) > 0 Then
                        ImgGenerateCRForm.Visible = False
                        ImgDelete.Visible = False
                        ImgAddMom.Visible = False
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub GvListMOMItemCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvListDocMOM.RowCommand
        If e.CommandName.Equals("deletedoc") Then
            DeleteAdditonalDocument(Convert.ToInt32(e.CommandArgument.ToString()))
        End If
    End Sub

    Protected Sub LbtGenerateCRClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtGenerateCR.Click
        If String.IsNullOrEmpty(HdnCRID.Value) Then
            Response.Redirect("frmNewChangeRequest.aspx?wpid=" & TxtPackageId.Text)
        Else
            Response.Redirect("frmNewChangeRequest.aspx?id=" & HdnCRID.Value.ToString() & "&wpid=" & TxtPackageId.Text)
        End If
    End Sub

    Protected Sub lbtCancelUploadMOMClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtCancelUploadMOM.Click
        FUMOMUpload.Controls.Clear()
        TxtPackageId.ReadOnly = False
        BtnSearch.Enabled = True
        lblStatusUpload.Text = ""
        MvPanel.SetActiveView(VwListCR)
    End Sub

    Protected Sub LbtViewCRFinalClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtViewCRFinal.Click
        Response.Redirect("frmViewCRFinal.aspx?wpid=" & TxtPackageId.Text)
    End Sub

#Region "Custom Methods"
    Private Sub BindListCR(ByVal packageid As String)
        BindSiteAtt(packageid)
        GvListCR.DataSource = controller.GetListCR(packageid)
        GvListCR.DataBind()
        MvMainPanel.SetActiveView(vwCorePanel)
        MvPanel.SetActiveView(VwListCR)
    End Sub

    Private Sub CheckingFinalCR(ByVal cridasfinal As String)
        If Convert.ToInt32(cridasfinal) > 0 Then
            LbtAddCR.Visible = False
            LbtViewCRFinal.Visible = True
        Else
            LbtAddCR.Visible = True
            LbtViewCRFinal.Visible = False
        End If
    End Sub

    Private Sub AddNewCR()
        TxtPackageId.ReadOnly = True
        BtnSearch.Enabled = False
        GvListDocMOM.DataSource = Nothing
        GvListDocMOM.DataBind()
        MvPanel.SetActiveView(VwUploadDocumentMOMCR)
    End Sub

    Private Sub BindSiteAtt(ByVal packageid As String)
        Dim getSiteAttributeQuery As String = "select  TselProjectID,SiteNo, PoNo,POName, Workpackageid,Scope,sitename from poepmsitenew where workpackageid = '" & packageid & "'"
        Dim dtSiteAtt As DataTable = objdb.ExeQueryDT(getSiteAttributeQuery, "siteAtt")
        If dtSiteAtt.Rows.Count > 0 Then
            tdpono.InnerText = dtSiteAtt.Rows(0).Item(2)
            tdponame.InnerText = dtSiteAtt.Rows(0).Item(3)
            tdsiteno.InnerText = dtSiteAtt.Rows(0).Item(1)
            tdsitename.InnerText = dtSiteAtt.Rows(0).Item(6)
            tdscope.InnerText = dtSiteAtt.Rows(0).Item(5)
            tdwpid.InnerText = dtSiteAtt.Rows(0).Item(4)
            tdprojectId.InnerText = dtSiteAtt.Rows(0).Item(0)
        End If
    End Sub

    Sub uploaddocument(ByVal packageid As String)
        Dim FileNamePath As String = String.Empty
        Dim FileNameOnly As String = String.Empty
        Dim ReFileName As String = String.Empty

        ft = ConfigurationManager.AppSettings("Type") & tdpono.InnerText & "-" & "CR" & "\"
        Dim fpath As String = ConfigurationManager.AppSettings("BufferPath")
        path = ConfigurationManager.AppSettings("Fpath") & tdsiteno.InnerText & ft
        Dim DocPath As String = ""
        Dim err As Boolean = False
        'Dim strResult As String = DOInsertTrans(packageid, path)

        FileNamePath = FUMOMUpload.PostedFile.FileName
        FileNameOnly = System.IO.Path.GetFileName(FUMOMUpload.PostedFile.FileName)
        ReFileName = Format(CDate(DateTime.Now), "ddMMyyyyHHss") & FileNameOnly
        DocPath = tdsiteno.InnerText & ft & Common.TakeFileUpload.ConvertAnyFormatToPDF(FUMOMUpload, path)

        If Not err Then
            Dim strCRID As Int32
            If HdnCRID.Value = "" Or String.IsNullOrEmpty(HdnCRID.Value) Then
                strCRID = 0
            Else
                strCRID = Convert.ToInt32(HdnCRID.Value)
            End If
            With sitedocinf
                .CRID = strCRID
                .PackageId = packageid
                .DocPath = DocPath
                .OrgDocPath = tdsiteno.InnerText & ft & ReFileName
                .LMDT = DateTime.Now
                .LMBY = CommonSite.UserId
                .DocName = ReFileName
            End With
            Try
                HdnCRID.Value = controller.InsertNewCRSiteDoc(sitedocinf)
            Catch ex As Exception
                Response.Write("<script language='javascript'>alert('Update failed please check your uploaded file name..');</script>")
                Exit Sub
            End Try
            lblStatusUpload.Text = "Document Uploaded Successfully"
            BindDocumentMOM(GvListDocMOM, Convert.ToInt32(HdnCRID.Value))
        Else
            Response.Write("<script language='javascript'>alert('Document upload failed');</script>")
        End If
    End Sub

    Private Sub BindDocumentMOM(ByVal gv As GridView, ByVal crid As Int32)
        gv.DataSource = controller.GetMOMCRDocumentList(crid)
        gv.DataBind()
    End Sub

    Private Sub DeleteCR(ByVal crid As Int32)
        controller.DeleteCR(crid)
        BindListCR(TxtPackageId.Text)
    End Sub

    Private Sub DeleteAdditonalDocument(ByVal docid As Int32)
        controller.DeleteDocCRAdditional(docid)
        BindDocumentMOM(GvListDocMOM, Convert.ToInt32(HdnCRID.Value))
    End Sub

#End Region

End Class
