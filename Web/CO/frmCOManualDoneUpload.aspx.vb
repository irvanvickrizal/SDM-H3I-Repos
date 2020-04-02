
Partial Class CO_frmCOManualDoneUpload
    Inherits System.Web.UI.Page

    Dim controller As New COTransactionNAController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData(GetPackageId(), CommonSite.UserId)
        End If
    End Sub


    Protected Sub GvCOTransaction_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvCOTransaction.RowCommand
        If e.CommandName.Equals("updatetran") Then
            Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim LblSWID As Label = CType(row.Cells(0).FindControl("LblSWID"), Label)
            If Not LblSWID Is Nothing Then
                Response.Redirect("frmCOTransactionDetail.aspx?coid=" & e.CommandArgument.ToString() & "&swid=" & LblSWID.Text)
            End If

        End If
    End Sub

    Protected Sub GvCOTransaction_PageIndex(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GvCOTransaction.PageIndexChanging
        GvCOTransaction.PageIndex = e.NewPageIndex
        BindData(GetPackageId(), CommonSite.UserId)
    End Sub

    Protected Sub LbtSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LbtSearch.Click
        BindData(GetPackageId(), CommonSite.UserId)
    End Sub

    Protected Sub GvCOTransaction_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvCOTransaction.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LblDocPathStatus As Label = CType(e.Row.FindControl("LblDocPathStatus"), Label)
            Dim LblDocStatus As Label = CType(e.Row.FindControl("LblDocStatus"), Label)
            Dim viewdoclink As HtmlAnchor = CType(e.Row.FindControl("viewdoclink"), HtmlAnchor)
            Dim LblSWID As Label = CType(e.Row.FindControl("LblSWID"), Label)
            Dim ImgUpdateTransaction As ImageButton = CType(e.Row.FindControl("ImgUpdateTransaction"), ImageButton)
            If Not LblDocPathStatus Is Nothing And Not LblDocStatus Is Nothing And Not viewdoclink Is Nothing And Not LblSWID Is Nothing And Not ImgUpdateTransaction Is Nothing Then
                If LblDocPathStatus.Text.Equals("NA") Then
                    LblDocStatus.Text = "Document Missing"
                    viewdoclink.Visible = False
                    LblDocStatus.Visible = True
                    ImgUpdateTransaction.Visible = False
                Else
                    LblDocStatus.Visible = False
                    viewdoclink.Visible = True
                    viewdoclink.HRef = "../PO/frmViewCODocument.aspx?swid=" & LblSWID.Text & "&parent=bast"
                    ImgUpdateTransaction.Visible = True
                End If
            End If
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal packageid As String, ByVal userid As Integer)
        GvCOTransaction.DataSource = controller.GetCOTransactionNC(packageid, userid)
        GvCOTransaction.DataBind()
    End Sub

    Private Function GetPackageId() As String
        If String.IsNullOrEmpty(TxtPackageId.Text) Then
            Return Nothing
        Else
            Return TxtPackageId.Text
        End If
    End Function

#End Region

End Class
