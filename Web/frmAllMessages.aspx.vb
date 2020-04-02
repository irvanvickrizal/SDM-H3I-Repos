Imports System.Data
Imports Entities
Imports BusinessLogic
Imports Common

Partial Class frmAllMessages
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objbo As New BOUserSetup
    Dim objET As New ETMessageBoard
    Dim objBOM As New BOMessageBoard
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack = True Then
            dt = objBOM.uspMessageBoardLD(, , 0)
            DataList1.DataSource = dt
            DataList1.DataBind()
        End If

    End Sub
End Class
