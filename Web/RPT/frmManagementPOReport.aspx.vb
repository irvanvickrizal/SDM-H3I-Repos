Imports Common
Imports System.Data
Imports BusinessLogic
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.IO.StreamWriter

Partial Class RPT_frmManagementPOReport
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim objBO As New BOSiteDocs
    Dim rpt As New ReportDocument

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not Session("ManagementPOReport") Is Nothing Then
            CrystalReportViewer1.ReportSource = CType(Session("doc"), ReportDocument)
            CrystalReportViewer1.DataBind()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            objBOD.fillDDL(ddlPO, "PoNo", True, Constants._DDL_Default_All)
            If Not Request("pono") Is Nothing And Not Request("pono") = "0" Then
                ddlPO.SelectedValue = Request("pono")
            End If
        End If

        If Not IsPostBack Then

            Dim con As New SqlConnection(ConfigurationManager.AppSettings("conn"))
            Dim Conninfo As New ConnectionInfo
            Dim cmd As New SqlCommand("uspRPTPO", con)

            cmd.CommandText = "Exec dbo.uspManagementPOReport "

            Dim da As New SqlDataAdapter(cmd)
            Dim ds As New ManagementPOReport

            da.Fill(ds, "ManagementPOReport")

            Conninfo.ServerName = ConfigurationManager.AppSettings("SName")
            Conninfo.DatabaseName = ConfigurationManager.AppSettings("RDB")
            Conninfo.UserID = ConfigurationManager.AppSettings("RUsrName")
            Conninfo.Password = ConfigurationManager.AppSettings("RUsrPwd")

            rpt = New ReportDocument
            rpt.Load(Server.MapPath("ManagementPOReport.rpt"))
            SetDBLogonForReport(Conninfo, rpt)
            rpt.SetDataSource(ds)
            Session("ManagementPOReport") = rpt

            CrystalReportViewer1.ReportSource = rpt

            Dim prmFld1 As ParameterField = CrystalReportViewer1.ParameterFieldInfo(0)
            Dim prmVal1 As ParameterDiscreteValue = New ParameterDiscreteValue()
            prmVal1.Value = Request.ServerVariables("SERVER_NAME")
            prmFld1.CurrentValues.Add(prmVal1)

            Dim prmFld2 As ParameterField = CrystalReportViewer1.ParameterFieldInfo(1)
            Dim prmVal2 As ParameterDiscreteValue = New ParameterDiscreteValue()
            prmVal2.Value = Request.ServerVariables("URL")
            prmFld2.CurrentValues.Add(prmVal2)

            Dim prmFld3 As ParameterField = CrystalReportViewer1.ParameterFieldInfo(2)
            Dim prmVal3 As ParameterDiscreteValue = New ParameterDiscreteValue()
            prmVal3.Value = Request.ServerVariables("SERVER_PORT")
            prmFld3.CurrentValues.Add(prmVal3)

            If Not Request("pono") Is Nothing And Not Request("pono") = "0" Then
                CrystalReportViewer1.SelectionFormula = "{ManagementPOReport.PoNo}='" & Request("pono") & "'"
            End If

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
            Session("ManagementPOReport") = Nothing
        End If
    End Sub

    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("http://" & Request.ServerVariables("SERVER_NAME") & ":" & Request.ServerVariables("SERVER_PORT") & _
            Request.ServerVariables("URL") & "?pono=" & ddlPO.SelectedValue)
    End Sub
End Class
