Imports System.IO
Imports System.Configuration
Imports System.Net
Imports System
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data
Imports System.Data.SqlClient
Imports Common
Partial Class ManagementReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack = True Then
            Dim ConnInfo As New ConnectionInfo
            Dim ds As New dsMgmtFAT
            Dim ds1 As New dsMgmtBAST
            Dim ds2 As New PlanAct
            Dim con As New SqlConnection(ConfigurationManager.AppSettings("conn"))
            con.Open()
            Dim y As Integer = Year(DateTime.Now())
            Dim da As New SqlDataAdapter("exec MANAGEMENTFINALBASTNEW " & y & " ", con)
            da.Fill(ds, "dsFAT")
            da.SelectCommand = New SqlCommand("exec MANAGEMENTPOTENTIALBAST " & y & " ", con)
            da.Fill(ds1, "dsBAST")
            da.SelectCommand = New SqlCommand("exec MANAGEMENTPLANACTBAST " & y & " ", con)
            da.Fill(ds2, "PlanAct")
            con.Close()
            Dim rpt As New ReportDocument
            rpt.Load(Server.MapPath("rptfinalaccsigned.rpt"))
            Conninfo.ServerName = ConfigurationManager.AppSettings("SName")
            Conninfo.DatabaseName = ConfigurationManager.AppSettings("RDB")
            Conninfo.UserID = ConfigurationManager.AppSettings("RUsrName")
            Conninfo.Password = ConfigurationManager.AppSettings("RUsrPwd")
            SetDBLogonForReport(ConnInfo, rpt)
            rpt.SetDataSource(ds)
            rpt.Subreports.Item("rptPotentialbast.rpt").SetDataSource(ds1)
            rpt.Subreports.Item("rptbastplanvsact.rpt").SetDataSource(ds2)
            CrystalReportViewer1.ReportSource = rpt
            CrystalReportViewer1.DataBind()
            rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("RPTPDF") & "/" & "managementgraph.pdf")
            convertone()
            converttwo()

            Common.TakeFileUpload.MergePdfNew(Server.MapPath("RPTPDF") & "\" & "Reportmanagement1.pdf", Server.MapPath("RPTPDF") & "\" & "Reportmanagement2.pdf", Server.MapPath("RPTPDF") & "\" & "Reportmanagement.pdf")
            Common.TakeFileUpload.MergePdfNew(Server.MapPath("RPTPDF") & "\" & "Reportmanagement.pdf", Server.MapPath("RPTPDF") & "\" & "managementgraph.pdf", Server.MapPath("RPTPDF") & "\" & ConfigurationManager.AppSettings("RDB") & "ManagementReport.pdf")
        End If

    End Sub
    Public Function getdata() As String
        Dim tmp As String = ""
        Try
            Dim uri As New Uri("http://www.telkomsel.nsnebast.com/ReportManagement.aspx")
            If (uri.Scheme = uri.UriSchemeHttp) Then
                Dim request As HttpWebRequest = HttpWebRequest.Create(uri)
                request.Method = WebRequestMethods.Http.Get
                Dim response1 As HttpWebResponse = request.GetResponse()
                Dim reader As New StreamReader(response1.GetResponseStream())
                tmp = reader.ReadToEnd()
                response1.Close()
            End If
        Catch ex As Exception
            tmp = ex.Message
        End Try
        Return tmp
    End Function
    Public Function getdata1() As String
        Dim tmp As String = ""
        Try
            Dim uri As New Uri("http://www.telkomsel.nsnebast.com/ReportManagement2.aspx")
            If (uri.Scheme = uri.UriSchemeHttp) Then
                Dim request As HttpWebRequest = HttpWebRequest.Create(uri)
                request.Method = WebRequestMethods.Http.Get
                Dim response1 As HttpWebResponse = request.GetResponse()
                Dim reader As New StreamReader(response1.GetResponseStream())
                tmp = reader.ReadToEnd()
                response1.Close()
            End If
        Catch ex As Exception
            tmp = ex.Message
        End Try
        Return tmp
    End Function
    Sub convertone()
        div123.InnerHtml = getdata()
        Dim filenameorg As String
        Dim ReFileName As String
        filenameorg = "Reportmanagement1"
        ReFileName = filenameorg & ".htm"
        Dim strpath = Server.MapPath("RPTPDF") & "\"
        Dim sw As HtmlTextWriter = New HtmlTextWriter(New StreamWriter(strpath & ReFileName, False, System.Text.UnicodeEncoding.UTF8))
        sw.WriteLine("<html><head><style type=""text/css"">")
        sw.WriteLine("tr{padding: 3px;}")
        sw.WriteLine(".MainCSS{margin-bottom: 0px;margin-left: 20px;margin-right: 20px;margin-top: 0px;width: 800px;height: 700px;text-align: center;}")
        sw.WriteLine(".lblText{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;}")
        sw.WriteLine(".lblBText{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: bold;}")
        sw.WriteLine(".lblBold{font-family: verdana;font-size: 12pt;color: #000000;font-weight: bold;}")
        sw.WriteLine(".textFieldStyle{background-color: white;border: 1px solid;color: black;font-family: verdana;font-size: 9pt;}")
        sw.WriteLine(".GridHeader{color: #0e1b42;background-color: Orange;font-size: 9pt;font-family: verdana;text-align: Left;vertical-align: bottom;font-weight: bold;}")
        sw.WriteLine(".GridEvenRows{background-color: #e3e3e3;vertical-align: top;font-family: verdana;font-size: 8pt;color: #000000;}")
        sw.WriteLine(".GridOddRows{background-color: white;vertical-align: top;font-family: verdana;font-size: 8pt;color: #000000;}")
        sw.WriteLine(".PagerTitle{font-size: 8pt;background-color: #cddbbf;text-align: Right;vertical-align: middle;color: #25375b;font-weight: bold;}")
        sw.WriteLine(".Hcap{height: 5px;}")
        sw.WriteLine(".VCap{width: 10px;}")
        sw.WriteLine(".lblBBTextL{border-bottom: 1px #000 solid;border-left: 1px #000 solid;border-top: 1px #000 solid;font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: bold;text-decoration: none;}")
        sw.WriteLine(".lblBBTextM{border-bottom: 1px #000 solid;border-top: 1px #000 solid;font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: normal;text-decoration: none;width: 1%;}")
        sw.WriteLine(".lblBBTextR{border-right: 1px #000 solid;border-bottom: 1px #000 solid;border-top: 1px #000 solid;font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: normal;text-decoration: none;}")
        sw.WriteLine("#lblTotalA{font-weight: bold;}")
        sw.WriteLine("#lblJobDelay{font-weight: bold;}")
        sw.WriteLine("</style></head>")
        sw.WriteLine("<body class=""MainCSS"">")
        div123.RenderControl(sw)
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(strpath & ReFileName, strpath, filenameorg)
    End Sub
    Sub converttwo()
        div456.InnerHtml = getdata1()
        Dim filenameorg As String
        Dim ReFileName As String
        filenameorg = "Reportmanagement2"
        ReFileName = filenameorg & ".htm"
        Dim strpath = Server.MapPath("RPTPDF") & "\"
        Dim sw As HtmlTextWriter = New HtmlTextWriter(New StreamWriter(strpath & ReFileName, False, System.Text.UnicodeEncoding.UTF8))
        sw.WriteLine("<html><head><style type=""text/css"">")
        sw.WriteLine("tr{padding: 3px;}")
        sw.WriteLine(".MainCSS{margin-bottom: 0px;margin-left: 20px;margin-right: 20px;margin-top: 0px;width: 800px;height: 700px;text-align: center;}")
        sw.WriteLine(".lblText{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;}")
        sw.WriteLine(".lblBText{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: bold;}")
        sw.WriteLine(".lblBold{font-family: verdana;font-size: 12pt;color: #000000;font-weight: bold;}")
        sw.WriteLine(".textFieldStyle{background-color: white;border: 1px solid;color: black;font-family: verdana;font-size: 9pt;}")
        sw.WriteLine(".GridHeader{color: #0e1b42;background-color: Orange;font-size: 9pt;font-family: verdana;text-align: Left;vertical-align: bottom;font-weight: bold;}")
        sw.WriteLine(".GridEvenRows{background-color: #e3e3e3;vertical-align: top;font-family: verdana;font-size: 8pt;color: #000000;}")
        sw.WriteLine(".GridOddRows{background-color: white;vertical-align: top;font-family: verdana;font-size: 8pt;color: #000000;}")
        sw.WriteLine(".PagerTitle{font-size: 8pt;background-color: #cddbbf;text-align: Right;vertical-align: middle;color: #25375b;font-weight: bold;}")
        sw.WriteLine(".Hcap{height: 5px;}")
        sw.WriteLine(".VCap{width: 10px;}")
        sw.WriteLine(".lblBBTextL{border-bottom: 1px #000 solid;border-left: 1px #000 solid;border-top: 1px #000 solid;font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: bold;text-decoration: none;}")
        sw.WriteLine(".lblBBTextM{border-bottom: 1px #000 solid;border-top: 1px #000 solid;font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: normal;text-decoration: none;width: 1%;}")
        sw.WriteLine(".lblBBTextR{border-right: 1px #000 solid;border-bottom: 1px #000 solid;border-top: 1px #000 solid;font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: normal;text-decoration: none;}")
        sw.WriteLine("#lblTotalA{font-weight: bold;}")
        sw.WriteLine("#lblJobDelay{font-weight: bold;}")
        sw.WriteLine("</style></head>")
        sw.WriteLine("<body class=""MainCSS"">")
        div456.RenderControl(sw)
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(strpath & ReFileName, strpath, filenameorg)
    End Sub
    Public Sub SetDBLogonForReport(ByVal ConnectionInfo As ConnectionInfo, ByVal reportDocument As ReportDocument)
        Dim rpttables As Tables = reportDocument.Database.Tables
        Dim tab As CrystalDecisions.CrystalReports.Engine.Table
        For Each tab In rpttables
            Dim rpttableslogoninfo As TableLogOnInfo = tab.LogOnInfo
            tab.LogOnInfo.ConnectionInfo = ConnectionInfo
            tab.ApplyLogOnInfo(tab.LogOnInfo)
        Next
    End Sub
End Class
