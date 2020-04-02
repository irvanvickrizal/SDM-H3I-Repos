Imports CRFramework

Partial Class ViewDocument_frmViewCRDocument
    Inherits System.Web.UI.Page

    Private controller As New CRController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ConfigureHiddenField(Convert.ToInt32(Request.QueryString("id")))
            BindDocReviewed()
            BindData()
        End If
    End Sub

    Protected Sub grddocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim url As String
            Dim lblSubdocid As Label = CType(e.Row.FindControl("LblSubDocid"), Label)
            url = "../PO/frmViewCRDocument.aspx?crid=" & hdncrid.Value & "&subdocid=" & lblSubdocid.Text
            e.Row.Cells(1).Text = "<a href='#' onclick=""window.open('" & url & "','mywindowopen','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(1).Text & "</a>"
        End If
    End Sub

#Region "Custom Methods"
    Private Sub ConfigureHiddenField(ByVal sno As Int32)
        Dim info As CRWFTransactionInfo = controller.GetCRTransactionBySNO(sno)
        If Not info Is Nothing Then
            Dim CRDetail As CRInfo = GetCRDetail(info.CRID)
            hdncrid.Value = info.CRID
            hdnDocPath.Value = CRDetail.DocPath
            hdnpackageid.Value = CRDetail.PackageId
        End If
    End Sub

    Private Sub BindDocReviewed()
        Dim divRev As String = ""
        Dim reviewer As New ReviewerInfo

        For Each reviewer In controller.GetCRReviewed(Convert.ToInt32(Request.QueryString("id")))
            divRev += "Reviewed by " & reviewer.UserName & " As " & reviewer.SignTitle & " On " & String.Format("{0:dd MMM yyyy}", reviewer.ExecuteDate) & " " & "<br />"
        Next
        divReviewer.InnerHtml = divRev
    End Sub

    Private Sub BindData()
        BindPDFDocument(hdnDocPath.Value)
        BindAdditionalDocument(Convert.ToInt32(hdncrid.Value))
    End Sub

    Private Sub BindPDFDocument(ByVal strPath As String)
        PDFViwer.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strPath.Replace("\", "/"))
    End Sub

    Private Sub BindAdditionalDocument(ByVal crid As Int32)
        grddocuments.DataSource = controller.GetMOMCRDocumentList(crid)
        grddocuments.DataBind()
    End Sub

    Protected Function GetCRDetail(ByVal crid As Int32) As CRInfo
        Return controller.GetCRDetail(crid)
    End Function

#End Region

End Class
