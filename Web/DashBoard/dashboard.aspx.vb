Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class RPT_dashboard
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    'Dim objBODP As New BOPDDLs
    Dim objET As New ETEBASTUsers
    Dim objBO As New BODashBoard
    Dim dt, dtdg As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
        End If
    End Sub
    Sub BindData()
        Dim dtRegion As New DataTable()
        Dim strQuery As String = "usp_Dashboard_region"
        If Convert.ToInt32(HdView.Value) <> 0 Then
            strQuery = "usp_Dashboard_Section"
        End If
        dtRegion = objBO.usp_Dashboard_Region(strQuery, Request.QueryString("pono").ToString(), CommonSite.GetDashBoardLevel(), CommonSite.UserId())
        grdDashBoard.PageSize = CommonSite.PageSize()
        grdDashBoard.DataSource = dtRegion
        Try
            grdDashBoard.DataBind()
            If Convert.ToInt32(HdView.Value) <> 0 Then

                grdDashBoard.Columns(1).Visible = False
            Else
                grdDashBoard.Columns(1).Visible = True
            End If
        Catch ex As Exception
            grdDashBoard.PageIndex = 0
        End Try
    End Sub
   
    Protected Sub grdDashBoard_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDashBoard.PageIndexChanging
        grdDashBoard.PageIndex = e.NewPageIndex
        BindData()

    End Sub

    Protected Sub btnSection_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSection.Click
        If (HdView.Value = "0") Then
            HdView.Value = 1
            btnSection.Text = "View Region Document Count "

        Else
            btnSection.Text = "View Section Document Count "
            HdView.Value = 0

        End If
        BindData()
    End Sub
End Class
