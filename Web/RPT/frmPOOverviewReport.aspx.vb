Imports Common
Imports System.Data
Imports BusinessLogic
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.IO.StreamWriter

Partial Class frmPOOverviewReport
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim objBO As New BOSiteDocs
    Dim rpt As New ReportDocument

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not Session("POOverviewReport") Is Nothing Then
            CrystalReportViewer1.ReportSource = CType(Session("doc"), ReportDocument)
            CrystalReportViewer1.DataBind()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then

        End If

        Dim con As New SqlConnection(ConfigurationManager.AppSettings("conn"))
        Dim Conninfo As New ConnectionInfo
        Dim cmd As New SqlCommand("uspPOOverviewReport", con)

        cmd.CommandText = "Exec dbo.uspPOOverviewReport "

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New POOverviewReport

        da.Fill(ds, "POOverviewReport")

        Conninfo.ServerName = ConfigurationManager.AppSettings("SName")
        Conninfo.DatabaseName = ConfigurationManager.AppSettings("RDB")
        Conninfo.UserID = ConfigurationManager.AppSettings("RUsrName")
        Conninfo.Password = ConfigurationManager.AppSettings("RUsrPwd")

        Dim rptPath = Server.MapPath("POOverviewReport.rpt")
        rpt = New ReportDocument
        rpt.Load(rptPath)
        SetDBLogonForReport(Conninfo, rpt)
        rpt.SetDataSource(ds)
        Session("POOverviewReport") = rpt

        CrystalReportViewer1.ReportSource = rpt

        'Dim prmFld1 As ParameterField = CrystalReportViewer1.ParameterFieldInfo(0)
        'Dim prmVal1 As ParameterDiscreteValue = New ParameterDiscreteValue()
        'prmVal1.Value = Request.ServerVariables("SERVER_NAME")
        'prmFld1.CurrentValues.Add(prmVal1)

        'If Not Request("pono") Is Nothing And Not Request("pono") = "0" Then
        '    CrystalReportViewer1.SelectionFormula = "{POOverviewReport.PoNo}='" & Request("pono") & "'"
        'End If

        CrystalReportViewer1.DataBind()

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
            Session("POOverviewReport") = Nothing
        End If
    End Sub
End Class
