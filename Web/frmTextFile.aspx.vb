Imports System
Imports System.IO
Imports System.Data
Imports common
Imports System.Configuration
Imports FileTransfer.FileTransfer

Partial Class frmTextFile
    Inherits System.Web.UI.Page
    Dim dt0 As New DataTable
    Dim dt1 As New DataTable
    Dim ObjUtil As New DBUtil
    Dim str As String = ""
    Dim dates As String
    Shared txtfn As String
    Shared csvfn As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            link.Visible = False
            Dim d As DateTime = DateTime.Now
            dates = d.Date.Day.ToString() & "-" & d.Date.Day.ToString & "-" & d.Date.Month.ToString() & "-" & d.Date.Year.ToString() & "-" & d.TimeOfDay.Hours.ToString() & d.TimeOfDay.Duration().Minutes.ToString() & d.TimeOfDay.Duration().Seconds.ToString()
            txtfn = "NSN-" + dates + ".txt"
            csvfn = "NSN-" + dates + ".csv"
        End If
    End Sub

    Public Function CreateTextFile() As Boolean
        Dim fs As FileStream = Nothing
        Dim sw As StreamWriter = Nothing
        Dim fs2 As FileStream = Nothing
        Dim sw2 As StreamWriter = Nothing
        Dim delimiter As String = ","
        Dim excludeColumnNumber As Integer = -1
        Dim columnCount As Integer = 0
        Dim directoryName As String = txtfn
        Dim isTextFileCreationSuccessful As Boolean = False
        Dim dirIndex As Integer = 0
        Try
            For Each drive As DriveInfo In My.Computer.FileSystem.Drives
                If drive.RootDirectory.Exists Then
                    If Not Directory.Exists(Server.MapPath("TXTFiles")) Then
                        Try
                            Directory.CreateDirectory(Server.MapPath("TXTFiles"))
                        Catch ex As Exception
                            Response.Write("<script>alert('Drive does not exist')</script>")
                        End Try
                    End If
                End If
            Next
            If dt1 IsNot Nothing Then
                Dim areastr As String
                Dim bastdt As String
                Dim scopestr As String
                fs = New FileStream(Server.MapPath("TXTFiles") & "/" & txtfn, FileMode.CreateNew, FileAccess.Write)
                sw = New StreamWriter(fs)
                sw.BaseStream.Seek(0, SeekOrigin.[End])
                fs2 = New FileStream(Server.MapPath("TXTFiles") & "/" & csvfn, FileMode.CreateNew, FileAccess.Write)
                sw2 = New StreamWriter(fs2)
                sw2.BaseStream.Seek(0, SeekOrigin.[End])
                str = "select cs.site_no, sd.pono " & _
                    "from sitedoc sd " & _
                    "inner join codsite cs on cs.site_id=sd.siteid " & _
                    "where docid=1032 and " & _
                    "  sd.lmdt>'2009-10-27 00:00:00.000' and  " & _
                    "  sd.lmdt<'2009-10-27 23:59:59.999'" & _
                    "group by cs.site_no, sd.pono " & _
                    "order by site_no asc"
                dt0 = ObjUtil.ExeQueryDT(str, "wf-cs")
                Dim idx As Integer
                For Each dtr As DataRow In dt0.Rows
                    idx += 1
                    'write to txt file
                    sw.Write("Action=insert" & vbCrLf)
                    sw.Write("dDocTitle=NETWORK PROJECT - NSN - BAST" & vbCrLf)
                    sw.Write("dDocAuthor=sysadmin" & vbCrLf)
                    sw.Write("dSecurityGroup=OP - 0" & vbCrLf)
                    str = "select ca.ara_name from codsite cs inner join codarea ca on ca.ara_id=cs.ara_id where site_no='" & dtr.Item("site_no").ToString & "'"
                    dt1 = ObjUtil.ExeQueryDT(str, "codsite")
                    areastr = dt1.Rows(0).Item("ara_name").ToString
                    If areastr = "Area1" Or areastr = "Area2" Then
                        sw.Write("dDocAccount=OP-00/NT-01/NT-02" & vbCrLf)
                    ElseIf areastr = "Area3" Or areastr = "Area4" Then
                        sw.Write("dDocAccount=OP-00/NT-01/NT-03" & vbCrLf)
                    End If
                    If areastr = "Area1" Then
                        sw.Write("xdescAccount=RADIO ACCESS ENGINEERING & IMPLEMENTATION CONTROL AREA1 DIVISION" & vbCrLf)
                    ElseIf areastr = "Area2" Then
                        sw.Write("xdescAccount=RADIO ACCESS ENGINEERING & IMPLEMENTATION CONTROL AREA2 DIVISION" & vbCrLf)
                    ElseIf areastr = "Area3" Then
                        sw.Write("xdescAccount=RADIO ACCESS ENGINEERING & IMPLEMENTATION CONTROL AREA3 DIVISION" & vbCrLf)
                    ElseIf areastr = "Area4" Then
                        sw.Write("xdescAccount=RADIO ACCESS ENGINEERING & IMPLEMENTATION CONTROL AREA4 DIVISION" & vbCrLf)
                    End If
                    str = "select distinct Scope from podetails where pono='" & dtr.Item("pono").ToString & "' and siteno='" & dtr.Item("site_no").ToString & "'"
                    dt1 = ObjUtil.ExeQueryDT(str, "podetails")
                    scopestr = dt1.Rows(0).Item("Scope").ToString
                    sw.Write("xringkasan=" & dtr.Item("site_no").ToString & "_" & scopestr & vbCrLf)
                    sw.Write("dDocType=DOC" & vbCrLf)
                    sw.Write("xCategoryID=IST 03.01" & vbCrLf)
                    str = "select convert(varchar(20),lmdt,103) bastdate from sitedoc " & _
                    "where siteid=(select site_id from codsite where site_no='" & dtr.Item("site_no").ToString & "') and " & _
                    "docid = 1032"
                    dt1 = ObjUtil.ExeQueryDT(str, "sitedoc")
                    bastdt = dt1.Rows(0).Item("bastdate").ToString
                    sw.Write("xtglDoc=" & bastdt & vbCrLf)
                    sw.Write("dStatus=RELEASED" & vbCrLf)
                    sw.Write("xBarcode=" & vbCrLf)
                    sw.Write("dReleaseState=Y" & vbCrLf)
                    sw.Write("dRmaProcessState=Y" & vbCrLf)
                    sw.Write("dVitalState=Y" & vbCrLf)
                    sw.Write("xIsRecord=1" & vbCrLf)
                    sw.Write("xVitalPeriodUnits=wwRmaCalendarQuarter" & vbCrLf)
                    sw.Write("xRmProfileTrigger=record" & vbCrLf)
                    sw.Write("xDifID=" & txtfn & vbCrLf)
                    sw.Write("primaryFile=/data/ucm/" & txtfn & vbCrLf)
                    sw.Write("<<EOD>>" & vbCrLf)
                    'write to csv file
                    sw2.Write(idx.ToString & ",")
                    sw2.Write("IST 03.01")
                    sw2.Write("<no document (bast no)>" & ",")
                    sw2.Write("<tanggal documen (bast date)>" & ",")
                    sw2.Write("NETWORK PROJECT - NSN - BAST" & ",")
                    sw2.Write("<lokasi simpan???>" & ",")
                    sw2.Write("<PL-00/IT-06/IT-05/IT-54>" & ",")
                    sw2.Write(csvfn & ",")
                    sw2.Write("<remarks???>,")
                    sw2.Write(vbCrLf)
                Next
                isTextFileCreationSuccessful = True
                link.Visible = True
            End If
        Catch exError As Exception
            isTextFileCreationSuccessful = False
            Response.Write(exError.Message)
        Finally
            If fs IsNot Nothing AndAlso sw IsNot Nothing Then
                sw.Flush()
                sw.Close()
                fs.Close()
                sw2.Flush()
                sw2.Close()
                fs2.Close()
            End If
        End Try
        Return isTextFileCreationSuccessful
    End Function

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        'str = "select * from EPMOracle WHERE PROJECT_ID <>''"
        'dt1 = ObjUtil.ExeQueryDT(str, "codsite")
        CreateTextFile()
    End Sub

    Protected Sub lnkDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDownload.Click
        Dim strpath As String = Server.MapPath("TXTFiles") & "\"
        Dim rs As New FileTransfer.FileTransfer.File
        Dim Downloads As String = rs.Download(txtfn, strpath, 100000)
    End Sub
End Class
