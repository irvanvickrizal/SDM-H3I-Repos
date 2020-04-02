Imports System
Imports System.IO
Imports System.IO.StringReader
Partial Class ViewLog
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            lblid.Text = Request.QueryString("Type")
            Dim y As Integer = DateTime.Now.Year
            Dim i, j As Integer
            For i = 0 To 6
                j = y - i
                ddlYear.Items.Insert(i, j)
            Next
        End If
        tbldisplay.Visible = False
    End Sub
    Protected Sub btnViewLog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewLog.Click
        Dim sr As System.IO.StreamReader
        Dim path As String
        If Request.QueryString("Type") = "EPM" Then
            path = Server.MapPath(ConfigurationManager.AppSettings("EPMFolder") & "\" & ddlYear.SelectedItem.Text & "-" & ddlMonth.SelectedItem.Text & "-" & ConfigurationManager.AppSettings("EPMFile") & ".txt")
        Else
            path = Server.MapPath(ConfigurationManager.AppSettings("POFolder") & "\" & ddlYear.SelectedItem.Text & "-" & ddlMonth.SelectedItem.Text & "-" & ConfigurationManager.AppSettings("POFile") & ".txt")
        End If
        If File.Exists(path) = True Then
            sr = IO.File.OpenText(path)
            Dim k As String = sr.ReadToEnd
            txtViewLog.Text = k
            sr.Close()
            tbldisplay.Visible = True
        Else
            tbldisplay.Visible = False
            Response.Write("<script>alert('No Log Available For Selected  Month And Year')</script>")
        End If
    End Sub
End Class
