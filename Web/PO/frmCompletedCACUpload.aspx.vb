Imports Common
Imports BusinessLogic
Imports System.Data
Imports System.IO

Partial Class PO_frmCompletedCACUpload
    Inherits System.Web.UI.Page
    Dim objutil As New DBUtil
    Dim dtDocs As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        dtDocs = objutil.ExeQueryDT("exec uspGetCACReadyCreation " & CommonSite.UserId & ", " & "2046", "dt")
        GvCACReport.DataSource = dtDocs
        GvCACReport.DataBind()
    End Sub

    Private Sub GvCACReport_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvCACReport.PageIndexChanging
        GvCACReport.PageIndex = e.NewPageIndex
        BindData()
    End Sub
#End Region

End Class
