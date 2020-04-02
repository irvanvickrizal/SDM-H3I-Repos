Imports BusinessLogic
Imports System.Data
Imports System.Net
Imports Common
Partial Class DashBoard_frmViewDocument_SiteHistory
    Inherits System.Web.UI.Page
    Dim kpicontrol As New KPIController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")

        If Page.IsPostBack = False Then
            'If Not String.IsNullOrEmpty(Request.QueryString("guid")) Then
            '    Dim docpath = BindDocPath(Convert.Tostring(Request.QueryString("guid")))
            Dim DocPath As String = kpicontrol.KPIViewDoc_SiteHistory(Request.QueryString("guid"))
            Dim Client As New WebClient()
            Dim Buffer As [Byte]() = Client.DownloadData(DocPath)

            If Buffer IsNot Nothing Then
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-length", Buffer.Length.ToString())
                    Response.BinaryWrite(Buffer)
                End If
            End If

    End Sub
    'Private Function BindDocPath(ByVal guid As String)
    '    kpicontrol.GetSiteHistoryDocPath(guid)
    '    'docView.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + docpath.Replace("\", "/"))
    'End Function
End Class

