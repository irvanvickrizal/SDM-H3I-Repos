Imports Common
Imports BusinessLogic
Imports System.Data
Imports System.IO
Imports System.IO.DirectoryInfo
Imports System.Collections.Generic
Imports System.Net
Imports System.Uri
Imports System.Net.FtpWebRequest
Partial Class Archive_ArchiveDownload
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim objBo As New BOSiteDocs
    Dim FTPUserid As String = ConfigurationManager.AppSettings("FTPUserID")
    Dim FTPPwd As String = ConfigurationManager.AppSettings("FTPPWD")
    Dim FTPURL As String = ConfigurationManager.AppSettings("FTPURL")
    Dim Host As String = ConfigurationManager.AppSettings("FTPDURL")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnArchive.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        txtuserid.Text = ConfigurationManager.AppSettings("FTPUserID")
        txtpwd.Text = ConfigurationManager.AppSettings("FTPPWD")
        txtftpurl.Text = ConfigurationManager.AppSettings("FTPURL")
        If Not IsPostBack Then
            objBOD.fillDDL(ddlpono, "podetails", True, Constants._DDL_Default_Select)
        End If
    End Sub
    Protected Sub ddlpono_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlpono.SelectedIndexChanged
        ddlsiteno.Items.Clear()
        If ddlpono.SelectedItem.Text <> "--Please Select--" Then
            'here  we have to show only sites for which BAST process not finished.
            Dim lvlcode As String = Session("lvlcode")
            Dim area, region, zone, site As Integer
            area = Session("Area_Id")
            region = Session("Region_Id")
            zone = Session("Zone_Id")
            site = Session("Site_Id")
            Dim ddldt As DataTable
            ddldt = objBo.uspDDLPOSiteNoByUser1(ddlpono.SelectedItem.Value, lvlcode, area, region, zone, site, Session("User_Id"))
            If ddldt.Rows.Count > 0 Then
                ddlsiteno.DataSource = ddldt
                ddlsiteno.DataTextField = "txt"
                ddlsiteno.DataValueField = "VAL"
                ddlsiteno.DataBind()
                ddlsiteno.Items.Insert(0, "--Select--")
            End If
        End If
    End Sub
    Protected Sub btnArchive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnArchive.Click
        Dim siteno As String
        Dim Fpath As String
        siteno = ddlsiteno.SelectedItem.Text
        Dim siteno1 As String() = siteno.Split("-")
        Dim site As String = siteno1(0).Trim()
        Dim Type1 As String = ".zip"
        If rbtnlist.Items(0).Selected Then
            Try
                Fpath = ConfigurationManager.AppSettings("ArPath")
                Dim str As String = "" & Fpath & "" & site & "" & Type1 & ""
                If System.IO.File.Exists(str) Then
                    DownlaodFile(str, True)
                Else
                    Response.Write("<script>alert('File Does not exist to download ')</script>")
                End If
            Catch ex As Exception
                Response.Write("<script>alert('" & ex.Message & "')</script>")
            End Try
        Else
            Dim f As String = " ftp://"
            Dim filename As String = "" & site & "" & Type1 & ""
            'Dim URI As String = host & remoteFile
            Dim str As String = "" & f & "" & FTPURL & "" & site & "" & Type1 & ""
            Const FilePath As String = "/Archive/"
            'Dim FilePath As String = FilePath.GetType(Right("" & site & "" & Type2 & "", 10))
            FileList(FilePath)
        End If
    End Sub
    Private Sub Download(ByVal FilePath As String)
        Dim reqFTP As FtpWebRequest
        Dim Host As String = ConfigurationManager.AppSettings("FTPDURL")
        Dim folder As String = ConfigurationManager.AppSettings("FOLDER")
        Dim str As String = Session("somu")
        Dim FilePath1 As String = folder & Session("somu")
        Dim FTPUserid As String = ConfigurationManager.AppSettings("FTPUserID")
        Dim FTPPwd As String = ConfigurationManager.AppSettings("FTPPWD")
        Try
            reqFTP = DirectCast(FtpWebRequest.Create(New Uri(("ftp://" & Host) + FilePath1)), FtpWebRequest)
            reqFTP.Method = WebRequestMethods.Ftp.DownloadFile
            reqFTP.UseBinary = True
            reqFTP.KeepAlive = False
            reqFTP.Credentials = New NetworkCredential(FTPUserid, FTPPwd)
            Dim response__1 As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)
            Dim ftpStream As Stream = response__1.GetResponseStream()
            Dim cl As Long = response__1.ContentLength
            Dim bufferSize As Integer = 2048
            Dim readCount As Integer
            Dim fileName As String() = FilePath1.Split(New Char() {"\"c})
            Response.Clear()
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment;filename=" & fileName(fileName.Length - 1).ToString())
            Dim buffer As Byte() = New Byte(bufferSize - 1) {}
            readCount = ftpStream.Read(buffer, 0, bufferSize)
            While readCount > 0
                Response.OutputStream.Write(buffer, 0, readCount)
                readCount = ftpStream.Read(buffer, 0, bufferSize)
            End While
            ftpStream.Close()
            response__1.Close()
        Catch ex As Exception
            Response.Write("<script>alert('" & ex.Message & "')</script>")
        End Try
    End Sub
    Private Sub DownlaodFile(ByVal filename As String, ByVal forcedownload As Boolean)
        Dim ext As String = Path.GetExtension(filename)
        Dim type As String = ""
        If ext <> "" Then
            Select Case ext.ToLower()
                Case ".zip"
                    type = "text/zip"
                Case ".pdf"
                    type = "text/pdf"
                Case ".htm"
                    type = "text/HTML"
                Case ".txt"
                    type = "text/plain"
                Case ".tif"
                    type = "text/tif"
                Case ".doc"
                    type = "text/doc"
                Case ".rtf"
                    type = "Application/msword"
                Case ".xls"
                    type = "text/xls"
                Case ".rar"
                    type = "text/rar"
            End Select
        End If
        If forcedownload Then
            Response.AppendHeader("content-disposition", "attachment; filename=" + filename)
        End If
        If type <> "" Then
            Response.WriteFile(filename)
            Response.End()
        End If
    End Sub
    Protected Sub rbtnlist_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnlist.SelectedIndexChanged
        If rbtnlist.Items(0).Selected Then
            rowUser.Visible = False
            rowPwd.Visible = False
            rowURL.Visible = False
        End If
        If rbtnlist.Items(1).Selected Then
            rowUser.Visible = True
            rowPwd.Visible = True
            rowURL.Visible = True
        End If
    End Sub
    Private Sub FileList(ByVal FilePath As String)
        Dim siteno As String = ddlsiteno.SelectedItem.Text
        Dim siteno1 As String() = siteno.Split("-")
        Dim site As String = siteno1(0).Trim()
        Dim Type2 As String = ".zip"
        Dim RemoteFile As String = "" & site & "" & Type2 & ""
        Try
            Dim ftpWebRequest__1 As FtpWebRequest = DirectCast(FtpWebRequest.Create(New Uri(("ftp://" & Host) + FilePath)), FtpWebRequest)
            ftpWebRequest__1.Credentials = New NetworkCredential(FTPUserid, FTPPwd)
            ftpWebRequest__1.KeepAlive = False
            ftpWebRequest__1.Method = WebRequestMethods.Ftp.ListDirectory
            Using sReader As New StreamReader(ftpWebRequest__1.GetResponse().GetResponseStream())
                Dim str As String = sReader.ReadLine()
                Dim str1 As String
                While str IsNot Nothing
                    str1 = str.Substring(12, 10)
                    If str1 = RemoteFile Then
                        Session("somu") = str
                        Download(str)
                    End If
                    Response.Write(str)
                    str = sReader.ReadLine()
                End While
            End Using
            ftpWebRequest__1 = Nothing
        Catch ex As Exception
            Response.Write("<script>alert('" & ex.Message & "')</script>")
        End Try
    End Sub
End Class
