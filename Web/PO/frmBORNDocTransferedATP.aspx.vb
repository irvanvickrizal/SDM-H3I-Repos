Imports System.Data

Partial Class PO_frmBORNDocTransferedATP
    Inherits System.Web.UI.Page

    Dim borncontrol As New BORNController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindATPDetail(GetSubmissionSNO())
        End If
    End Sub

    Protected Sub btnConfirm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConfirm.Click
        If borncontrol.BORN_ATPMigration(GetSubmissionSNO(), GetDocID(), GetSWID(), GetSiteID) Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScripta", "Showmain('success');", True)
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindATPDetail(ByVal sno As Int32)
        Dim dtGetDetail As DataTable = borncontrol.BORN_GetATPDetail(sno)
        lblDocpath.Text = "Test 01"
        If (dtGetDetail.Rows.Count > 0) Then
            For Each drw As DataRow In dtGetDetail.Rows
                PDFViwer.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + drw.Item("atp_docpath"))
                hdnDocpath.Value = drw.Item("atp_docpath")
                lblDocpath.Text = ConfigurationManager.AppSettings("Vpath") + drw.Item("atp_docpath")
                Exit For
            Next
        End If
    End Sub

    Private Function GetWPID() As String
        If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            Return Request.QueryString("wpid")
        Else
            Return "0"
        End If
    End Function

    Private Function GetSubmissionSNO() As Int32
        If Not String.IsNullOrEmpty(Request.QueryString("ssno")) Then
            Return Request.QueryString("ssno")
        Else
            Return 0
        End If
    End Function

    Private Function GetDocID() As Integer
        If Not String.IsNullOrEmpty(Request.QueryString("docid")) Then
            Return Request.QueryString("docid")
        Else
            Return 0
        End If
    End Function

    Private Function GetSWID() As Int32
        If String.IsNullOrEmpty(Request.QueryString("swid")) Then
            Return 0
        Else
            Return Convert.ToInt32(Request.QueryString("swid"))
        End If
    End Function

    Private Function GetSiteID() As Int32
        If String.IsNullOrEmpty(Request.QueryString("sid")) Then
            Return 0
        Else
            Return Convert.ToInt32(Request.QueryString("sid"))
        End If
    End Function
#End Region

End Class
