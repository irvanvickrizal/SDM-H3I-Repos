
Partial Class Dashboard_WCC_WCCDeletion
    Inherits System.Web.UI.Page

    Private controller As New WCCController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
    End Sub


    Protected Sub BtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not String.IsNullOrEmpty(TxtRemarks.Text) Then
            controller.WCCDeletionFlag(GetWCCID(), TxtRemarks.Text, True, CommonSite.UserId)
            Dim info As WCCInfo = controller.GetODWCCBaseId(GetWCCID())
            ScriptManager.RegisterClientScriptBlock(Me, Page.GetType(), "", "parent.$.fancybox.close();", True)
            'Response.Redirect("../WCC/frmWCCCreation.aspx?wpid=" & info.PackageId)
        End If
    End Sub

#Region "Custom Methods"
    Private Function GetWCCID() As Int32
        If Not String.IsNullOrEmpty(Request.QueryString("wid")) Then
            Return Convert.ToInt32(Request.QueryString("wid"))
        Else
            Return 0
        End If
    End Function
#End Region

End Class
