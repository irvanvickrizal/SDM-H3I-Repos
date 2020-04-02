Imports CRFramework
Imports System.Data
Imports System.IO

Partial Class PO_frmCRViewLog
    Inherits System.Web.UI.Page

    Dim controller As New CRController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindSiteAtt()
            If Not String.IsNullOrEmpty(Request.QueryString("logtype")) Then
                BindFinalCRLog()
            Else
                GetViewLog()
            End If

        End If
    End Sub

#Region "Custom Methods"
    Private Sub GetViewLog()
        If String.IsNullOrEmpty(Request.QueryString("subdocid")) Then
            MvCorePanel.SetActiveView(VwGeneralLog)
            gvViewLog.DataSource = controller.GetCRAuditTrail(Convert.ToInt32(Request.QueryString("id")), 0)
            gvViewLog.DataBind()
        Else
            gvViewLog.DataSource = controller.GetCRAuditTrail(Convert.ToInt32(Request.QueryString("id")), Integer.Parse(Request.QueryString("subdocid")))
            gvViewLog.DataBind()
        End If

    End Sub

    Private Sub BindSiteAtt()
        Dim info As CRInfo = controller.GetCRSiteAttribute(Convert.ToInt32(Request.QueryString("id")), Request.QueryString("wpid"))
        If Not String.IsNullOrEmpty(Request.QueryString("logtype")) Then
            tdcrno.InnerText = info.CRNo & " as Final CR"
        Else
            tdcrno.InnerText = info.CRNo
        End If
        tdpono.InnerText = info.PONO
        tdponame.InnerText = info.EOName
        tdsiteno.InnerText = info.SiteNo
        tdsitename.InnerText = info.SiteName
        tdscope.InnerText = info.Scope
        tdwpid.InnerText = info.PackageId
    End Sub

    Private Sub BindFinalCRLog()
        MvCorePanel.SetActiveView(VwSummaryLog)
        GvSummaryViewLog.DataSource = controller.GetFinalCRLogSummary(Request.QueryString("wpid"))
        GvSummaryViewLog.DataBind()
    End Sub

#End Region

End Class
