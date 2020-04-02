
Partial Class fancybox_Form_fb_ViewHistoricalWCCDeletion
    Inherits System.Web.UI.Page

    Dim controller As New WCCController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        End If
    End Sub

    Protected Sub GvWCCDeletion_Paging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvWCCDeletion.PageIndexChanging
        GvWCCDeletion.PageIndex = e.NewPageIndex
        BindHistoricalDeletion(GetTextBoxValue(TxtPackageId), GetTextBoxValue(TxtPOSubcontractor))
    End Sub

    Protected Sub LbtSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSearch.Click
        BindHistoricalDeletion(GetTextBoxValue(TxtPackageId), GetTextBoxValue(TxtPOSubcontractor))
    End Sub

#Region "Custom Methods"
    Private Sub BindHistoricalDeletion(ByVal packageid As String, ByVal posubcontractor As String)
        GvWCCDeletion.DataSource = controller.GetWCCHistoricalDeletion(packageid, posubcontractor)
        GvWCCDeletion.DataBind()
    End Sub

    Private Function GetTextBoxValue(ByVal txt As TextBox) As String
        If Not String.IsNullOrEmpty(txt.Text) Then
            Return txt.Text
        Else
            Return Nothing
        End If
    End Function
#End Region
End Class
