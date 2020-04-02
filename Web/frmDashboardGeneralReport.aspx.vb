
Partial Class frmDashboardGeneralReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then
            Response.Redirect("~/SessionTimeOut.aspx")
        End If
    End Sub

#Region "BAST Section"
    Protected Sub LbtEbastTselUnderSignature_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtEbastTselUnderSignature.Click
        Response.Redirect("~/DashBoard_New/BastUnderSignature.aspx?id=1")
    End Sub

    Protected Sub LbtEbastNSNUnderSignature_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtEbastNSNUnderSignature.Click
        Response.Redirect("~/DashBoard_New/BastUnderSignature.aspx?id=2")
    End Sub

    Protected Sub LbtEbastReadyCreation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtEBASTReadyCreation.Click
        Response.Redirect("~/DashBoard_New/BastReadyCreation.aspx")
    End Sub

#End Region

#Region "BAUT Section"
    Protected Sub LbtBAUTNSNUnderSignature_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtBautNSNUnderSignature.Click
        Response.Redirect("~/DashBoard_New/BautUnderSignature.aspx?id=6")
    End Sub

    Protected Sub LbtBAUTTselUnderSignature_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtBautTselUnderSignature.Click
        Response.Redirect("~/DashBoard_New/BautUnderSignature.aspx?id=7")
    End Sub

    Protected Sub LbtBAUTReadyCreation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtBautReadyCreation.Click
        Response.Redirect("~/DashBoard_New/BautReadyCreation.aspx")
    End Sub
#End Region

    Protected Sub LbtRejectedDocuments_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtRejectedDocuments.Click
        Response.Redirect("~/DashBoard_New/RejectedDocument.aspx?id=2")
    End Sub


#Region "Custom Methods"

#End Region

End Class
