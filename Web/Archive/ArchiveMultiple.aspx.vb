
Imports Common
Imports BusinessLogic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Collections.Generic
Imports ICSharpCode.SharpZipLib.Zip
Imports System.Net

Partial Class ArchiveMultiple
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim objBo As New BOSiteDocs
    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("NSN_DemoConnectionString").ConnectionString)
    Dim siteno As ListItem
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
        txtuserid.Text = ConfigurationManager.AppSettings("FTPUserID")
        txtpwd.Text = ConfigurationManager.AppSettings("FTPPWD")
        txtftpurl.Text = ConfigurationManager.AppSettings("FTPURL")
        If Not IsPostBack Then
            objBOD.fillDDL(ddlpono, "podetails", True, Constants._DDL_Default_Select)
        End If
    End Sub

    Protected Sub ddlpono_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlpono.SelectedIndexChanged

        'objBOD.fillDLL(lstsiteno, "POSiteNo", ddlpono.SelectedValue, True, Constants._DDL_Default_Select)
       
        lstsiteno.Items.Clear()
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
                lstsiteno.DataSource = ddldt
                lstsiteno.DataTextField = "txt"
                lstsiteno.DataValueField = "VAL"
                lstsiteno.DataBind()
                'lstsiteno.Items.Insert(0, "--Select--")

                'Else
                '    If site = 0 Then
                '        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('No Site information for this  user');", True)
                '    End If
            End If
        End If

    End Sub

    Protected Sub btnArchive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnArchive.Click
        Dim exists As Boolean = False
        Try
            For Each siteno In lstsiteno.Items
                If siteno.Selected Then
                    Dim siteno1 As String() = siteno.Text.Split("-")
                    Dim site As String = siteno1(0).Trim()
                    Fpath = ConfigurationManager.AppSettings("Fpath")
                    type = ConfigurationManager.AppSettings("Type")
                    Dim dir As New DirectoryInfo("" & Fpath & "" & site & " " & type & " ")
                    Dim pdfList As New List(Of String)
                    If dir.Exists Then
                        Dim subdir As DirectoryInfo() = New DirectoryInfo(dir.FullName).GetDirectories("*.*", SearchOption.AllDirectories)
                        For Each d As DirectoryInfo In subdir
                            Dim files As FileInfo() = d.GetFiles("*.pdf")
                            Dim i As Integer
                            If files.Length > 0 Then
                                For i = 0 To files.Length - 1
                                    filename = files(i).FullName
                                    name = files(i).Name
                                    'folderpath = "D:\Archive\" & siteno.Text & " "
                                    'newfolderpath = "D:\Archive\" & siteno.Text & "\" & name & " "
                                    folderpath = ConfigurationManager.AppSettings("ArPath").ToString & site
                                    newfolderpath = folderpath & "\" & name
                                    If Not Directory.Exists(ConfigurationManager.AppSettings("ArPath")) Then
                                        Directory.CreateDirectory(ConfigurationManager.AppSettings("ArPath"))
                                    End If
                                    Directory.CreateDirectory(folderpath)
                                    inStream = File.OpenRead(filename)
                                    storeStream.SetLength(inStream.Length)
                                    inStream.Read(storeStream.GetBuffer(), 0, inStream.Length)
                                    storeStream.Flush()
                                    inStream.Close()
                                    SaveMemoryStream(storeStream, newfolderpath)
                                    exists = True
                                Next
                            End If
                        Next
                        folderpath = ConfigurationManager.AppSettings("ArPath") & site
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
                            Response.Write("<script languate='javascript'>alert('Archived Successfully for the SiteNo " + site + "')</script>")
                        End If
                    Else
                        Response.Write("<script languate='javascript'>alert('No Documents Related For The SiteNo " + site + "')</script>")
                    End If
                End If
            Next
            If rbtnlist.Items(1).Selected Then
                Dim isexists As Boolean = False
                For Each siteno In lstsiteno.Items
                    If siteno.Selected Then
                        Dim siteno1 As String() = siteno.Text.Split("-")
                        Dim site As String = siteno1(0).Trim()
                        folderpath = ConfigurationManager.AppSettings("ArPath")
                        'Dim dir As New DirectoryInfo(folderpath)
                        Dim FileEntries As String() = Directory.GetFiles(folderpath)
                        Dim filename As String
                        For Each filename In FileEntries
                            Dim zipfilename As String = folderpath & site & ".zip"
                            If filename = zipfilename Then
                                Dim a As String = filename.Substring(11)
                                Dim d As DateTime = DateTime.Now
                                Dim zname As String = d.Date.Day.ToString() & d.Date.Month.ToString() & d.Date.Year.ToString() & d.TimeOfDay.Hours.ToString() & d.TimeOfDay.Duration().Minutes.ToString() & d.TimeOfDay.Duration().Seconds.ToString()
                                FTPurl = ConfigurationManager.AppSettings("FTPURL")
                                FTPUserid = ConfigurationManager.AppSettings("FTPUserID")
                                FTPPwd = ConfigurationManager.AppSettings("FTPPWD")
                                uploadFileUsingFTP("ftp://" & txtftpurl.Text.Trim() & zname & a, filename, txtuserid.Text.Trim(), txtpwd.Text.Trim())
                                If (Directory.Exists(folderpath)) Then
                                    For Each fName As String In Directory.GetFiles(folderpath)
                                        If File.Exists(fName) Then
                                            File.Delete(fName)
                                        End If
                                    Next
                                    'Directory.Delete(folderpath)
                                End If
                                isexists = True
                            End If
                        Next
                        If isexists Then
                            Response.Write("<script>alert('Uploaded SuccessFully" + site + "')</script>")
                        Else
                            Response.Write("<script>alert('No Documents To FTPUoading For This Site No " + site + "')</script>")
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Response.Write("<script>alert(" & ex.Message & ")</script>")
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
            Dim fs As FileStream = File.OpenRead(filename)
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
            Dim streamObj As FileStream = File.OpenRead(CompleteLocalPath)
            Dim buffer(streamObj.Length) As Byte
            streamObj.Read(buffer, 0, buffer.Length)
            streamObj.Close()
            streamObj = Nothing
            reqObj.GetRequestStream().Write(buffer, 0, buffer.Length)
            reqObj = Nothing
        Catch ex As Exception
            Response.Write("<script languate='javascript'>alert('" & ex.Message & "')</script>")
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
