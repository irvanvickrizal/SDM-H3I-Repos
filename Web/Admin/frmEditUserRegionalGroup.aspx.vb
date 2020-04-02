
Partial Class Admin_frmEditUserRegionalGroup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindData(GetUserId())
        End If
    End Sub

    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click

    End Sub

    Protected Sub GvRegions_DataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvRegions.DataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal userid As Integer)

    End Sub

    Private Sub AddRegional(ByVal userid As Integer)

    End Sub

    Private Function GetUserId() As Integer
        If String.IsNullOrEmpty(Request.QueryString("uid")) Then
            Return 0
        Else
            Return Integer.Parse(Request.QueryString("uid"))
        End If
    End Function
#End Region

End Class
