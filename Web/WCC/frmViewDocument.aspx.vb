Imports System.Collections.Generic

Partial Class WCC_frmViewDocument
    Inherits System.Web.UI.Page

    Dim controller As New WCCController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then

        End If
    End Sub

    Protected Sub LbtSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSearch.Click
        Select Case Session("User_Type")
            Case "N"
                BindSubconChoice(TxtPackageId.Text)
            Case "S"
                MvDisplayResult.SetActiveView(VwSubconView)
                BindData(PartnerController.GetSubconIdByUser(CommonSite.UserId), TxtPackageId.Text, DdlSearchType.SelectedValue)
        End Select
    End Sub

    Protected Sub GvViewDocuments_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvViewDocuments.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim LblWCCID As Label = CType(e.Row.FindControl("LblWCCID"), Label)
            Dim LblDocId As Label = CType(e.Row.FindControl("LblDocId"), Label)
            Dim LblDocType As Label = CType(e.Row.FindControl("LblDocType"), Label)
            Dim LblSWID As Label = CType(e.Row.FindControl("LblSWID"), Label)
            Dim LblPackageId As Label = CType(e.Row.FindControl("LblPackageId"), Label)
            If Not LblDocType Is Nothing Then
                Dim viewdoclink As HtmlAnchor = CType(e.Row.FindControl("viewdoclink"), HtmlAnchor)
                If (LblDocType.Text.ToLower.Equals("baut")) Then
                    viewdoclink.HRef = "../fancybox_Form/fb_ViewDocument.aspx?swid=" & LblSWID.Text & "&parent=baut"
                Else
                    If (viewdoclink.Visible = True) Then
                        If LblDocId.Text.Equals(ConfigurationManager.AppSettings("WCCDOCID")) Then
                            If controller.GetWCCDocPath(Convert.ToInt32(LblWCCID.Text)).ToLower.Equals("html") Then
                                viewdoclink.HRef = "../fancybox_Form/fb_FormWCCDone.aspx?wid=" & LblWCCID.Text
                            Else
                                viewdoclink.HRef = "../fancybox_Form/fb_ViewDocument.aspx?swid=0&wid=" & LblWCCID.Text & "&parent=wcc"
                            End If
                        Else
                            viewdoclink.HRef = "../fancybox_Form/fb_ViewDocument.aspx?swid=" & LblSWID.Text & "&parent=wcc"
                        End If
                    End If
                End If
            End If
            If Not LblWCCID Is Nothing Then
                Dim viewlog As HtmlAnchor = CType(e.Row.FindControl("viewlog"), HtmlAnchor)
                If Not viewlog Is Nothing Then
                    viewlog.HRef = "../fancybox_Form/fb_WCCViewLog.aspx?wid=" & LblWCCID.Text & "&docid=" & LblDocId.Text & "&doctype=" & LblDocType.Text & "&wpid=" & LblPackageId.Text
                End If
            End If
        End If
    End Sub

  
    Protected Sub DdlSubconFilter_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlSubconFilter.SelectedIndexChanged
        If (DdlSubconFilter.SelectedIndex > 0) Then
            BindData(Integer.Parse(DdlSubconFilter.SelectedValue), TxtPackageId.Text, DdlSearchType.SelectedValue)
        Else
            GvViewDocuments.DataSource = Nothing
            GvViewDocuments.DataBind()
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal subconid As Integer, ByVal packageid As String, ByVal searchtype As String)
        If searchtype.Equals("1") Then
            GvViewDocuments.DataSource = controller.GetSiteDocumentsBaseWPIDSubcon(packageid, CommonSite.UserId)
            GvViewDocuments.DataBind()
        ElseIf searchtype.Equals("2") Then
            GvViewDocuments.DataSource = controller.GetSiteDocumentsBasePOSubcon(packageid, CommonSite.UserId)
            GvViewDocuments.DataBind()
        End If
        
    End Sub

    Private Sub BindSubconChoice(ByVal packageid As String)
        Dim listsubcons As List(Of WCCInfo) = controller.GetSubconInsideSiteDocumentBaseWPID(packageid)
        If listsubcons.Count > 0 Then
            MvDisplayResult.SetActiveView(VwSubconFilterSearch)
            DdlSubconFilter.DataSource = listsubcons
            DdlSubconFilter.DataTextField = "subconname " & "-" & " siteno"
            DdlSubconFilter.DataValueField = "subconid"
            DdlSubconFilter.DataBind()

            DdlSubconFilter.Items.Insert(0, "--Select Subcon--")
        Else
            MvDisplayResult.SetActiveView(VwWPIDNoResult)
        End If
    End Sub

#End Region

End Class
