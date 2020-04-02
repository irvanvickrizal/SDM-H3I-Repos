Imports Common
Imports System.Data
Imports BusinessLogic
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.IO.StreamWriter

Partial Class RPT_frmManagementPOReportSub
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim objBO As New BOSiteDocs
    Dim rpt As New ReportDocument

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not Session("ManagementPOReportSub") Is Nothing Then
            CrystalReportViewer1.ReportSource = CType(Session("ManagementPOReportSub"), ReportDocument)
            CrystalReportViewer1.DataBind()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim con As New SqlConnection(ConfigurationManager.AppSettings("conn"))
            Dim Conninfo As New ConnectionInfo
            Dim cmd As New SqlCommand("uspRPTPO", con)

            If Request("rpt") = "0" Then
                cmd.CommandText = "Exec dbo.uspManagementPOReportSub "
            ElseIf Request("rpt") = "1" Then
                cmd.CommandText = "Exec dbo.uspManagementPOReportSub1 "
            Else
                cmd.CommandText = "Exec dbo.uspManagementPOReportSub2 "
            End If

            Dim da As New SqlDataAdapter(cmd)
            Dim ds As New ManagementPOReport

            da.Fill(ds, "ManagementPOReportSub")

            Conninfo.ServerName = ConfigurationManager.AppSettings("SName")
            Conninfo.DatabaseName = ConfigurationManager.AppSettings("RDB")
            Conninfo.UserID = ConfigurationManager.AppSettings("RUsrName")
            Conninfo.Password = ConfigurationManager.AppSettings("RUsrPwd")

            rpt.Load(Server.MapPath("ManagementPOReportSub.rpt"))
            SetDBLogonForReport(Conninfo, rpt)
            rpt.SetDataSource(ds)
            Session("ManagementPOReportSub") = rpt

            CrystalReportViewer1.ReportSource = rpt
            CrystalReportViewer1.SelectionFormula = "{ManagementPOReportSub.PoNo}='" + Request("pono") + "' and {ManagementPOReportSub.scope}='" + Request("scope") + "'"
            CrystalReportViewer1.DataBind()
        End If
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

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            Session("ManagementPOReportSub") = Nothing
        End If
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("http://" & Request.ServerVariables("SERVER_NAME") & ":" & Request.ServerVariables("SERVER_PORT") & _
            Request.ServerVariables("URL").Replace("Sub", "") & _
            "?pono=" & Request("pono") & "&scope=" & Request("scope"))
    End Sub
End Class
