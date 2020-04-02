
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.IO

Partial Class PO_frmServerFiles
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
    End Sub

    Sub ShowFilesIn(ByVal dir As String)
        Dim dirInfo As New DirectoryInfo(Dir)
        Dim fileItem As FileInfo
        lstFiles.Items.Clear()
        For Each fileItem In dirInfo.GetFiles()
            Dim tString1() As String = Split(txtSearch.Text, "_")
            Dim tString2() As String = Split(fileItem.Name, "_")
            If tString1(0) = tString2(0) Then
                lstFiles.Items.Add(fileItem.Name)
            End If
        Next
    End Sub

    Protected Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Response.Write("<script>window.opener.document.getElementById('fileUpload1').value='" & lstFiles.SelectedValue & "';window.close();</script>")
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim path As String = "/PilotSites/Temp"
        Dim dir As String = Server.MapPath(path)
        ShowFilesIn(dir)
    End Sub
End Class
