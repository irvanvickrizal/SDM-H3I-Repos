Imports Common
Imports BusinessLogic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Printing
Imports ICSharpCode.SharpZipLib.Zip
Imports System.Net

Partial Class ArchiveSingle
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim objBo As New BOSiteDocs
    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("NSN_DemoConnectionString").ConnectionString)
    Private streamToPrint As StreamReader
    Private printFont As Font
    Dim siteno As String
    Dim Fpath As String
    Dim type As String
    Dim filename As String
    Dim name As String
    Dim folderpath As String
    Dim newfolderpath As String
    Dim inStream As FileStream
    Dim storeStream As New MemoryStream()
    Dim FTPurl As String
    Dim FTPUserid As String
    Dim FTPPwd As String

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
        'objBOD.fillDDL(ddlsiteno, "SiteNoByPODetails", ddlpono.SelectedValue, True, Constants._DDL_Default_Select)
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
            Else
                ddlsiteno.Items.Insert(0, "No Values")
                ddlsiteno.Items(0).Value = 0

            End If
        End If

    End Sub

    Protected Sub ddlsiteno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlsiteno.SelectedIndexChanged
    End Sub

    Protected Sub btnArchive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnArchive.Click

        siteno = ddlsiteno.SelectedItem.Text
        Dim siteno1 As String() = siteno.Split("-")
        Dim site As String = siteno1(0).Trim()
        'If rbtnzip.Checked Then
        Fpath = ConfigurationManager.AppSettings("Fpath")
        type = ConfigurationManager.AppSettings("Type")
        Try
            Dim dir As New DirectoryInfo("" & Fpath & "" & site & " " & type & " ")
            'Dim pdfList As New List(Of String)
            If dir.Exists Then
                Dim arrlist As New ArrayList
                Dim subdir As DirectoryInfo()
                subdir = New DirectoryInfo(dir.FullName).GetDirectories("*.*", SearchOption.AllDirectories)
                For Each d As DirectoryInfo In subdir
                    Dim files As FileInfo() = d.GetFiles("*.pdf")
                    Dim i As Integer
                    If files.Length > 0 Then
                        For i = 0 To files.Length - 1
                            filename = files(i).FullName
                            name = files(i).Name
                            'folderpath = "D:\Archive\" & siteno & " "
                            'newfolderpath = "D:\Archive\" & siteno & "\" & name & " "
                            For Each drive As DriveInfo In My.Computer.FileSystem.Drives
                                'Response.Write(drive.RootDirectory.)
                                If drive.RootDirectory.Exists Then
                                    'If drive.Name = ConfigurationManager.AppSettings("ArPath") Then
                                    If Not Directory.Exists(ConfigurationManager.AppSettings("ArPath")) Then
                                        Try
                                            Directory.CreateDirectory(ConfigurationManager.AppSettings("ArPath"))
                                        Catch ex As Exception
                                            Response.Write("<script>alert('Drive does not exist')</script>")
                                        End Try
                                    End If
                                    folderpath = ConfigurationManager.AppSettings("ArPath") & site
                                    newfolderpath = folderpath & "\" & name
                                    Directory.CreateDirectory(folderpath)
                                    inStream = File.OpenRead(filename)
                                    storeStream.SetLength(inStream.Length)
                                    inStream.Read(storeStream.GetBuffer(), 0, inStream.Length)
                                    storeStream.Flush()
                                    inStream.Close()
                                    SaveMemoryStream(storeStream, newfolderpath)
                                End If
                            Next
                        Next
                    End If
                Next
                folderpath = ConfigurationManager.AppSettings("ArPath") & site
                'Dim zipfilename As String = "" & Fpath & "" & site & " " & type & " " & site & ".zip"
                StartZip("" & Fpath & "" & site & " " & type & " ", folderpath)
                If (Directory.Exists(folderpath)) Then
                    For Each fName As String In Directory.GetFiles(folderpath)
                        If File.Exists(fName) Then
                            File.Delete(fName)
                        End If

                    Next
                    Directory.Delete(folderpath)
                End If
                If rbtnlist.SelectedValue = 0 Then
                    Response.Write("<script language='javascript'>alert('Archived Successfully')</script>")
                End If
            Else
                Response.Write("<script language='javascript'>alert('No Documents Uploaded For This Site No')</script>")
                Exit Sub
            End If
            'End If

            Dim isexists As Boolean = False

            If rbtnlist.Items(1).Selected Then
                'siteno = ddlsiteno.SelectedItem.Text.Trim()
                folderpath = ConfigurationManager.AppSettings("ArPath")
                'Dim dir As New DirectoryInfo(folderpath)
                Dim FileEntries As String() = Directory.GetFiles(folderpath)
                Dim filename As String
                For Each filename In FileEntries
                    Dim zipfilename As String = folderpath & site & ".zip"
                    If File.Exists(filename) Then
                        If filename = zipfilename Then
                            Dim aa As String = filename.Substring(11)
                            Dim a As String = filename.Substring(11)
                            Dim d As DateTime = DateTime.Now
                            Dim zname As String = d.Date.Day.ToString() & d.Date.Month.ToString() & d.Date.Year.ToString() & d.TimeOfDay.Hours.ToString() & d.TimeOfDay.Duration().Minutes.ToString() & d.TimeOfDay.Duration().Seconds.ToString()
                            FTPurl = ConfigurationManager.AppSettings("FTPURL")
                            FTPUserid = ConfigurationManager.AppSettings("FTPUserID")
                            FTPPwd = ConfigurationManager.AppSettings("FTPPWD")
                            'uploadFileUsingFTP("ftp://ftp.takeunited.com/Archive/" & zname & a, filename)
                            uploadFileUsingFTP("ftp://" & txtftpurl.Text & zname & a, filename, txtuserid.Text.Trim(), txtpwd.Text.Trim())
                            Response.Write("<script languate='javascript'>alert('Uploaded SuccessFully')</script>")
                            isexists = True
                            If (Directory.Exists(folderpath)) Then
                                For Each fName As String In Directory.GetFiles(folderpath)
                                    If File.Exists(fName) Then
                                        File.Delete(fName)
                                    End If

                                Next
                            End If
                        End If
                    End If

                Next
                If Not isexists Then
                    Response.Write("<script languate='javascript'>alert('No Documents To FTPUoading For This Site No')</script>")
                End If
                'Response.Write("<script languate='javascript'>alert('Uploaded Successfully')</script>")
                'Response.Redirect("ArchiveSingle.aspx")
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)

            Response.Write("<script>alert(" & ex.Message & ")</script>")

            'Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "Script1", "<script>alert('" + strmsg + "')</script> ")
            'Throw ex
            'Exit Sub
        End Try
    End Sub
    Private Sub SaveMemoryStream(ByVal ms As MemoryStream, ByVal filename As String)
        Dim outStream As FileStream = File.OpenWrite(filename)
        ms.WriteTo(outStream)
        outStream.Flush()
        outStream.Close()
    End Sub
    Public Sub StartZip(ByVal directory__1 As String, ByVal zipfile_path As String)
        Dim filenames As String() = Directory.GetFiles(directory__1)
        Dim s As New ZipOutputStream(File.Create(zipfile_path & ".zip"))
        For Each filename As String In filenames
            Dim fs As FileStream = File.OpenRead(filename.ToString())
            Dim buffer As Byte() = New Byte(fs.Length - 1) {}
            fs.Read(buffer, 0, buffer.Length)
            filename = filename.Substring(21)
            Dim entry As New ZipEntry(filename)
            s.PutNextEntry(entry)
            s.Write(buffer, 0, buffer.Length)
            fs.Close()
        Next
        s.SetLevel(5)
        s.Finish()
        s.Close()
    End Sub

    Public Sub uploadFileUsingFTP(ByVal CompleteFTPPath As String, ByVal CompleteLocalPath As String, ByVal UName As String, ByVal PWD As String)
        Try
            Dim reqObj As FtpWebRequest = WebRequest.Create(CompleteFTPPath)

            reqObj.Method = WebRequestMethods.Ftp.UploadFile
            reqObj.Credentials = New NetworkCredential(UName, PWD)
            reqObj.UseBinary = True
            reqObj.UsePassive = True
            Dim streamObj As FileStream = File.OpenRead(CompleteLocalPath)
            Dim buffer(streamObj.Length) As Byte
            streamObj.Read(buffer, 0, buffer.Length)
            streamObj.Close()
            streamObj = Nothing
            reqObj.GetRequestStream().Write(buffer, 0, buffer.Length)
            reqObj = Nothing
        Catch ex As Exception
            'MsgBox("unable to upload Files" & ex.Message, MsgBoxStyle.OkOnly)
            Response.Write("<script language='javascript'>alert('" & ex.Message & "')</script>")
        End Try

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
End Class
