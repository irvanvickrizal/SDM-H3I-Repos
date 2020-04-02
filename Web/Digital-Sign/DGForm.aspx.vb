Imports System.Data
Imports BusinessLogic
Imports Common
Partial Class Digital_Sign_DGForm
    Inherits System.Web.UI.Page
    Dim objBo As New BODashBoard
    Dim dt, dtn As New DataTable
    Dim strpath As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dt As DataTable
        dt = objBo.DigitalSign(Convert.ToInt32(Request.QueryString("id")))
        If (dt.Rows.Count > 0) Then
            strpath = dt.Rows(0)("docpath").ToString()
            PDFViwer.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strpath)
        End If
    End Sub
End Class
