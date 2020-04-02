Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class frmshowsites
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    'Dim objBODP As New BOPDDLs
    Dim objET As New ETEBASTUsers
    Dim objBO As New BODashBoard
    Dim dt, dtdg As New DataTable
    Dim objUtil As New DBUtil
    Dim strsql, strSiteID, strWPID As String
    Private strExpr As String = ViewState("SortExpression")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            If Request.QueryString("id") <> "" Then
                WPDataBind()
            End If
        End If
    End Sub
    Sub WPDataBind()
        strsql = "SELECT distinct Reason.Reason, Reason.Remark, Reason.AddRemarks,siteReason.noofdays, siteReason.siteid, siteReason.SNO, siteReason.reasoncatID FROM   Reason INNER JOIN siteReason ON Reason.PK_Reason = siteReason.reasonid where siteReason.reasoncatID = " & Request.QueryString("id") & ""
        dt = objUtil.ExeQueryDT(strsql, "asdf")
        grdSite.DataSource = dt
        grdSite.DataBind()
    End Sub

    Protected Sub btnBack_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.ServerClick
        Response.Redirect("frmSiteReason.aspx")
    End Sub

    Protected Sub grdSite_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdSite.Sorting
        GridViewSortExpression = e.SortExpression
        Dim pageIndex As Integer = grdSite.PageIndex
        WPDataBind()
        grdSite.DataSource = SortDataTable(TryCast(grdSite.DataSource, DataTable), False)
        grdSite.DataBind()
    End Sub
    Protected Function SortDataTable(ByVal dataTable As DataTable, ByVal isPageIndexChanging As Boolean) As DataView
        If dataTable IsNot Nothing Then
            Dim dataView As New DataView(dataTable)
            If GridViewSortExpression <> String.Empty Then
                If isPageIndexChanging Then
                    dataView.Sort = String.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection)
                Else
                    dataView.Sort = String.Format("{0} {1}", GridViewSortExpression, GetSortDirection())
                End If
            End If
            Return dataView
        Else
            Return New DataView()
        End If
    End Function
    Private Property GridViewSortExpression() As String
        Get
            Return IIf(TryCast(strExpr, String) Is Nothing, String.Empty, TryCast(strExpr, String))
        End Get

        Set(ByVal value As String)
            'ViewState("SortExpression") = value
            strExpr = value
        End Set
    End Property
    Private Property GridViewSortDirection() As String
        Get
            Return IIf(TryCast(ViewState("SortDirection"), String) Is Nothing, "ASC", TryCast(ViewState("SortDirection"), String))
        End Get

        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Function GetSortDirection() As String
        Select Case GridViewSortDirection
            Case "ASC"
                GridViewSortDirection = "DESC"
                Exit Select
            Case "DESC"
                GridViewSortDirection = "ASC"
                Exit Select
        End Select
        Return GridViewSortDirection
    End Function

    Protected Sub grdSite_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSite.PageIndexChanging
        WPDataBind()
        grdSite.DataSource = SortDataTable(TryCast(grdSite.DataSource, DataTable), True)
        grdSite.PageIndex = e.NewPageIndex
        grdSite.DataBind()
    End Sub
End Class
