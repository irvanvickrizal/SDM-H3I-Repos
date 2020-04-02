Imports System.Data
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Collections
Imports System.Web.Security
Imports BusinessLogic
Imports Common
Imports Entities

Partial Class DashBoard_GraphicalReport
    Inherits System.Web.UI.UserControl
    Dim objBO As New BODashBoard
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strImageName As String = "Pichart" + CommonSite.UserName() + DateTime.Now.Minute.ToString + ".jpg"
        PiChart.Src = "../pichart/" + strImageName
    End Sub
End Class
