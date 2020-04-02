Imports System.Data
Imports Common
Imports Entities
Imports BusinessLogic
Imports Common_NSNFramework


Partial Class DashBoard_TaskPendingMAProcess
    Inherits System.Web.UI.Page
    Dim objBo As New BODashBoard
    Dim dt As New DataTable
    Dim ObjUtil As New DBUtil
    Dim dbutils_nsn As New DBUtils_NSN

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        dt = ObjUtil.ExeQueryDT("exec uspGetAllPendingDocMultipleApproval " & CommonSite.UserId, "pendingdocs")
        GvDocReview.DataSource = dt
        GvDocReview.DataBind()
    End Sub
#End Region

End Class
