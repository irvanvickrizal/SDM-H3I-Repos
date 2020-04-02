
Partial Class fancybox_Form_fb_ViewLog
    Inherits System.Web.UI.Page

    Dim controller As New WCCController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            If (Not String.IsNullOrEmpty(Request.QueryString("wid"))) And (Not String.IsNullOrEmpty(Request.QueryString("docid"))) And (Not String.IsNullOrEmpty(Request.QueryString("doctype"))) Then
                BindData(Convert.ToInt32(Request.QueryString("wid")), Integer.Parse(Request.QueryString("docid")), Request.QueryString("doctype"))
            End If
        End If
    End Sub

    Protected Sub GvAuditLog_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvAuditLog.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LblDocName As Label = CType(e.Row.FindControl("LblDocName"), Label)
            If Not LblDocName Is Nothing Then
                LblViewLogDocName.Text = LblDocName.Text & " Log"
            End If
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal wccid As Int32, ByVal docid As Integer, ByVal doctype As String)
        If doctype.ToLower().Equals("wcc") Then
            GvAuditLog.DataSource = controller.WCCAuditTrail_Get(wccid, docid, doctype)
            GvAuditLog.DataBind()
        Else
            GvAuditLog.DataSource = controller.WCCAuditTrail_BasePackageId(docid, Convert.ToString(Request.QueryString("wpid")))
            GvAuditLog.DataBind()
        End If
        
    End Sub


#End Region


End Class
