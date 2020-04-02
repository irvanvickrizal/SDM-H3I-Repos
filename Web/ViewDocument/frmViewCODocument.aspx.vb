Imports CRFramework
Imports System.Collections.Generic
Partial Class ViewDocument_frmViewCODocument
    Inherits System.Web.UI.Page
    Dim controller As New CRController
    Dim co_controller As New COController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            ConfigureHiddenField(Convert.ToInt32(Request.QueryString("id")))
            BindData()
            BindDocReviewed()
        End If
    End Sub

    Protected Sub grddocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim url As String
            Dim LblRegdocId As Label = CType(e.Row.FindControl("LblRegDocId"), Label)
            Dim LblDocType As Label = CType(e.Row.FindControl("LblDocType"), Label)
            If LblDocType.Text.ToLower().Equals("crfinal") Then
                url = "../CR/frmViewCRFinal_Popup.aspx?wpid=" & hdnwpid.Value
                e.Row.Cells(1).Text = "<a href='#' onclick=""window.open('" & url & "','mywindowopen','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=900px,height=600px')"">" & e.Row.Cells(1).Text & "</a>"
            Else
                url = "../PO/frmViewCODocument.aspx?swid=" & LblRegdocId.Text
                e.Row.Cells(1).Text = "<a href='#' onclick=""window.open('" & url & "','mywindowopen','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(1).Text & "</a>"
            End If

        End If
    End Sub

#Region "Custom Methods"
    Private Sub ConfigureHiddenField(ByVal sno As Int32)
        Dim info As COWFTransactionInfo = co_controller.GetCODetailBySNO(sno)
        If Not info Is Nothing Then
            hdnwpid.Value = info.PackageId
            hdncoid.Value = info.COID
            hdnwfid.Value = info.WFID
            hdnTaskid.Value = info.TSKID
            hdnDocPath.Value = info.DocPath
            hdndocid.Value = info.DocId
            hdnsiteid.Value = info.SiteId
            hdnVersion.Value = info.Version
            hdnTaskType.Value = IIf(controller.GetTaskDescByTaskId(Integer.Parse(hdnTaskid.Value)).ToLower().Equals("approver"), "app", "rev")
        End If
    End Sub

    Private Sub BindDocReviewed()
        Dim divRev As String = ""
        Dim reviewer As New ReviewerInfo
        Dim listReviewer As List(Of ReviewerInfo) = co_controller.GetCOReviewed(Convert.ToInt32(Request.QueryString("id")))
        If listReviewer.Count > 0 Then
            For Each reviewer In listReviewer
                divRev += "Reviewed by " & reviewer.UserName & " As " & reviewer.SignTitle & " On " & String.Format("{0:dd MMM yyyy}", reviewer.ExecuteDate) & " " & "<br />"
            Next
        Else
            divRev += "Nothing"
        End If
        divReviewer.InnerHtml = divRev
    End Sub

    Private Sub BindData()
        BindPDFDocument(hdnDocPath.Value)
        BindAdditionalDocument()
    End Sub

    Private Sub BindPDFDocument(ByVal strPath As String)
        PDFViwer.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strPath.Replace("\", "/"))
    End Sub

    Private Sub BindAdditionalDocument()
        grddocuments.DataSource = co_controller.GetCOAttachDocument(Integer.Parse(ConfigurationManager.AppSettings("codocid")), hdnwpid.Value, Convert.ToInt32(hdnsiteid.Value), Integer.Parse(hdnVersion.Value))
        grddocuments.DataBind()
    End Sub

    Protected Function GetCODetail(ByVal coid As Int32) As COInfo
        Return co_controller.GetODCO(coid)
    End Function

#End Region

End Class
