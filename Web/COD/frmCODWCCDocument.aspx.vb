Imports System.Collections.Generic
Imports Common
Imports BusinessLogic
Imports System.Data

Partial Class COD_frmCODWCCDocument
    Inherits System.Web.UI.Page
    Dim controller As New ScopeController
    Dim acontroller As New CODActivityController
    Dim wcccontrol As New WCCController
    Dim dtutils As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub DdlParentDocumentIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlParentDocument.SelectedIndexChanged
        If DdlParentDocument.SelectedIndex > 0 And DdlScopeDetail.SelectedIndex > 0 Then
            LblHeaderDocumentGrouping.InnerText = DdlScopeDetail.SelectedItem.Text & " Additional Document Grouping"
            BindDocumentChild(DdlParentDocument.SelectedValue(), DdlScopeDetail.SelectedValue())
            BindDocumentGrouping(Integer.Parse(DdlScopeDetail.SelectedValue()), GetActivities())

        Else
            LblHeaderDocumentGrouping.InnerText = "Additional Document Grouping"
            If DdlScopeDetail.SelectedIndex > 0 Then
                LblHeaderDocumentGrouping.InnerText = DdlScopeDetail.SelectedItem.Text & " Additional Document Grouping"
                BindDocumentGrouping(Integer.Parse(DdlScopeDetail.SelectedValue()), GetActivities())
                BindActivities()
            Else
                GvDocumentGrouping.DataSource = Nothing
                GvDocumentGrouping.DataBind()
                DdlActivities.Items.Clear()
            End If
            GvDocuments.DataSource = Nothing
            GvDocuments.DataBind()
        End If
    End Sub

    Protected Sub GvDocumentsItemCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvDocuments.RowCommand
        If e.CommandName.Equals("AddNewDoc") Then
            Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim DdlMandatoryStatus As DropDownList = CType(row.FindControl("DdlMandatoryStatus"), DropDownList)
            If DdlMandatoryStatus IsNot Nothing Then
                If DdlMandatoryStatus.SelectedValue() = "0" Then
                    AddDocumentGrouping(0, Integer.Parse(e.CommandArgument.ToString()), DdlParentDocument.SelectedValue(), DdlScopeDetail.SelectedValue(), False)
                Else
                    AddDocumentGrouping(0, Integer.Parse(e.CommandArgument.ToString()), DdlParentDocument.SelectedValue(), DdlScopeDetail.SelectedValue(), True)
                End If
                BindDocumentGrouping(Integer.Parse(DdlScopeDetail.SelectedValue()), GetActivities())
                If DdlParentDocument.SelectedIndex < 1 Then
                    BindDocumentChild(0, DdlScopeDetail.SelectedValue())
                Else
                    BindDocumentChild(DdlParentDocument.SelectedValue(), DdlScopeDetail.SelectedValue())
                End If

            End If
        End If
    End Sub

    Protected Sub GvDocumentGroupingItemCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvDocumentGrouping.RowCommand
        If e.CommandName.Equals("DeleteDoc") Then
            DeleteDocumentGrouping(Convert.ToInt32(e.CommandArgument.ToString()))
            BindDocumentGrouping(Integer.Parse(DdlScopeDetail.SelectedValue()), GetActivities())
            BindDocumentChild(DdlParentDocument.SelectedValue(), DdlScopeDetail.SelectedValue())
        End If
    End Sub

    Protected Sub DdlActivities_IndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlActivities.SelectedIndexChanged
        BindDocumentGrouping(Integer.Parse(DdlScopeDetail.SelectedValue()), GetActivities())
    End Sub

#Region "custom methods"
    Private Sub BindData()
        BindScopeDetail()
    End Sub

    Private Sub BindScopeDetail()
        DdlScopeDetail.DataSource = controller.GetAllDetailScopes(False, 0)
        DdlScopeDetail.DataTextField = "DScopeName"
        DdlScopeDetail.DataValueField = "DScopeId"
        DdlScopeDetail.DataBind()
        DdlScopeDetail.Items.Insert(0, "-- Detail Scope Option --")
    End Sub

    Private Sub BindActivities()
        DdlActivities.DataSource = acontroller.GetCODActivities(False)
        DdlActivities.DataTextField = "ActivityName"
        DdlActivities.DataValueField = "ActivityId"
        DdlActivities.DataBind()
        DdlActivities.Items.Insert(0, "--All--")
    End Sub

    Private Sub BindDocumentChild(ByVal doctype As String, ByVal dscopeid As String)
        If doctype.Equals("BAUT") Then
            Dim dsDocuments As DataTable = dtutils.ExeQueryDT("select doc_id, docname from coddoc where parent_id =" & ConfigurationManager.AppSettings("BAUTID") & _
            "  and rstatus = 2 and doc_id not in(select doc_id from CODDocumentWCC where dscope_id = " & dscopeid & " and parentDocType = 'BAUT')", "dt")
            If dsDocuments.Rows.Count > 0 Then
                GvDocuments.DataSource = dsDocuments
                GvDocuments.DataBind()
            Else
                GvDocuments.DataSource = Nothing
                GvDocuments.DataBind()
            End If
        Else
            Dim dsDocuments As DataTable = dtutils.ExeQueryDT("select WCCDocument_id 'doc_id', docname from WCC_CODDocument where isDeleted=0 " & _
                " and WCCdocument_id not in(select doc_id from CODDocumentWCC where dscope_id = " & dscopeid & " and parentDocType = 'WCC')", "dt")

            If dsDocuments.Rows.Count > 0 Then
                GvDocuments.DataSource = dsDocuments
                GvDocuments.DataBind()
            Else
                GvDocuments.DataSource = Nothing
                GvDocuments.DataBind()
            End If
        End If
    End Sub

    Private Sub BindDocumentGrouping(ByVal dscopeid As Integer, ByVal activityid As Integer)
        GvDocumentGrouping.DataSource = wcccontrol.GetWccDocumentGrouping(dscopeid, False, activityid)
        GvDocumentGrouping.DataBind()
    End Sub

    Private Sub AddDocumentGrouping(ByVal wccdocid As Int32, ByVal docid As Integer, ByVal parentDocType As String, ByVal dscopeid As Integer, ByVal isMandatory As Boolean)
        Dim info As New WCCDocumentInfo
        info.ParentDocType = parentDocType
        info.DOCWCCLMBY = CommonSite.UserName
        info.DScopeId = dscopeid
        info.WCCDOCId = wccdocid
        info.DocId = docid
        info.IsMandatory = isMandatory
        wcccontrol.WCCDocumentGroupingIU(info)
    End Sub

    Private Sub DeleteDocumentGrouping(ByVal wccdocid As Int32)
        wcccontrol.WCCDocumentGroupingDelete(wccdocid)
    End Sub

    Private Sub GvDocuments_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvDocuments.PageIndexChanging
        GvDocuments.PageIndex = e.NewPageIndex
        BindDocumentChild(DdlParentDocument.SelectedValue(), DdlScopeDetail.SelectedValue())
    End Sub

    Private Sub GvDocumentGrouping_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvDocumentGrouping.PageIndexChanging
        GvDocumentGrouping.PageIndex = e.NewPageIndex
        BindDocumentGrouping(Integer.Parse(DdlScopeDetail.SelectedValue()), GetActivities())
    End Sub

    Private Function GetActivities() As Integer
        If DdlActivities.SelectedIndex > 0 Then
            Return Integer.Parse(DdlActivities.SelectedValue)
        Else
            Return 0
        End If
    End Function

#End Region
End Class
