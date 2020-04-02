
Partial Class COD_frmCODDocumentSubtitute
    Inherits System.Web.UI.Page

    Dim dcontroller As New DocController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            BindDocumentOnline()
            BindDocumentsI()
            BindSubtituteDocumentsI()
            BindData(GetDocId())
        End If
    End Sub

    Protected Sub DdlDocuments_IndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlDocuments.SelectedIndexChanged
        BindData(GetDocId())
    End Sub

    Protected Sub GvDocumentSubtitutes_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvDocumentSubtitutes.RowCommand
        If (e.CommandName.Equals("DeleteDoc")) Then
            Dim value As Integer = Convert.ToInt32(e.CommandArgument)
            If value > -1 Then
                DeleteSubtituteDocuments(value)
            End If
        End If
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        Dim info As New DocChangeInfo
        info.DocId = Integer.Parse(DdlDocumentsI.SelectedValue)
        info.DocChangeId = Integer.Parse(DdlSubtituteDocumentsI.SelectedValue)
        info.LastModifiedBy = "Administrator"
        dcontroller.ChangeDocument_I(info)
        BindData(GetDocId())
        BindDocumentsI()
        BindSubtituteDocumentsI()
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal docid As Integer)
        GvDocumentSubtitutes.DataSource = dcontroller.GetSubtituteDocuments(docid)
        GvDocumentSubtitutes.DataBind()
    End Sub

    Private Sub BindDocumentOnline()
        DdlDocuments.DataSource = dcontroller.GetDocuments("O")
        DdlDocuments.DataTextField = "DocName"
        DdlDocuments.DataValueField = "DocId"
        DdlDocuments.DataBind()
        DdlDocuments.Items.Insert(0, "--All Documents--")

        
    End Sub

    Private Sub BindDocumentsI()
        DdlDocumentsI.DataSource = dcontroller.GetDocuments("O")
        DdlDocumentsI.DataTextField = "DocName"
        DdlDocumentsI.DataValueField = "DocId"
        DdlDocumentsI.DataBind()
        DdlDocumentsI.Items.Insert(0, "--Select Document--")
    End Sub

    Private Sub BindSubtituteDocumentsI()
        DdlSubtituteDocumentsI.DataSource = dcontroller.GetSubtituteDocuments("D")
        DdlSubtituteDocumentsI.DataTextField = "DocName"
        DdlSubtituteDocumentsI.DataValueField = "DocId"
        DdlSubtituteDocumentsI.DataBind()
        DdlSubtituteDocumentsI.Items.Insert(0, "--Select Document--")
    End Sub

    Private Sub DeleteSubtituteDocuments(ByVal subtitutedocid As Integer)
        dcontroller.DeleteSubtituteDocument(subtitutedocid)
        BindData(GetDocId())
        'Commented by Fauzan, 24 Dec 2018. No need to load the dropdownlist, since we ald defined at the first time
        'BindDocumentsI()
        'BindSubtituteDocumentsI()
    End Sub

    Private Sub GvDocumentSubtitutes_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvDocumentSubtitutes.PageIndexChanging
        GvDocumentSubtitutes.PageIndex = e.NewPageIndex
        BindData(GetDocId())
    End Sub

    Private Function GetDocId() As Integer
        Dim docid As Integer = 0
        If DdlDocuments.SelectedIndex > 0 Then
            docid = Integer.Parse(DdlDocuments.SelectedValue)
        End If
        Return docid
    End Function

#End Region

End Class
