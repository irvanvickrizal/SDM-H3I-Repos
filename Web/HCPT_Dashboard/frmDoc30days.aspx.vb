Imports Common
Imports System.Data
Imports System.IO


Partial Class HCPT_Dashboard_frmDoc30days
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim objutil As New DBUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub GrdDB_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles grdDB.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblswid As Label = CType(e.Row.FindControl("LblSWID"), Label)
            Dim lbldocid As Label = CType(e.Row.FindControl("LblDocId"), Label)
            Dim url As String = String.Empty
            If lblswid IsNot Nothing And lbldocid IsNot Nothing Then
                If lbldocid.Text = ConfigurationManager.AppSettings("ATP") Then
                    url = "../PO/frmViewDocumentATP.aspx?id=" & lblswid.Text
                Else
                    url = "../PO/frmViewDocument.aspx?id=" & lblswid.Text
                End If
            End If
            If Not String.IsNullOrEmpty(url) Then
                e.Row.Cells(1).Text = "<a href='" & url & "' TARGET='_blank'>" & e.Row.Cells(1).Text & "</a>"
            End If

        End If
    End Sub


#Region "Custom methods"
    Private Sub BindData()
        Dim strQuery As String = "exec HCPT_uspTrans_GetDocApprovedByUser30Days " & CommonSite.UserId
        dt = objutil.ExeQueryDT(strQuery, "dtDocs")
        If dt.Rows.Count > 0 Then
            grdDB.DataSource = dt
            grdDB.DataBind()
        End If
    End Sub
#End Region

End Class
