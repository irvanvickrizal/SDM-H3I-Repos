Imports System.Data
Imports BusinessLogic
Imports Common
Imports Common_NSNFramework
Partial Class PO_POCancellation
    Inherits System.Web.UI.Page
    Dim objbod As New BODDLs
    Dim objutil As New DBUtil
    Dim objutil_nsn As New DBUtils_NSN

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack = True Then
            bindpo()
        End If
    End Sub
    Protected Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        objutil.ExeNonQuery("exec USPPODelete '" & ddlPO.SelectedItem.Text & "'")
        bindpo()
        Response.Write("<script> alert('Selected PO Sucessfully deleted')</script>")
    End Sub
    Sub bindpo()
        objbod.fillDDL(ddlPO, "PoNo", True, "--Please Select--")
        objbod.fillDDL(ddlpono1, "PoNo", True, "--Please Select--")
        objbod.fillDDL(ddlpono2, "PoNo", True, "--Please Select--") 'dedy 091121
    End Sub
    Protected Sub btndoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndoc.Click
        Dim str() As String
        Dim siteattr() As String
        str = Split(ddlsite.SelectedItem.Value, "-")
        siteattr = Split(ddlsite.SelectedItem.Text, "-")
        objutil.ExeNonQuery("exec sitedocdelete " & str(0) & ",'" & str(1) & "'")
        objutil_nsn.DeleteATPOnSite(siteattr(2))
        BindSiteRelatedDocument(ddlpono1.SelectedItem.Text)
        Response.Write("<script>alert('Documents Sucessfully Deleted')</script>")
    End Sub
    Protected Sub ddlpono1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlpono1.SelectedIndexChanged
        BindSiteRelatedDocument(ddlpono1.SelectedItem.Text)
    End Sub

    Private Sub BindSiteRelatedDocument(ByVal pono As String)
        Dim dt As New DataTable
        dt = objutil.ExeQueryDT("exec uspDDLPOSite '" & pono & "'", "dd")
        ddlsite.DataSource = dt
        ddlsite.DataTextField = "txt"
        ddlsite.DataValueField = "val"
        ddlsite.DataBind()
    End Sub

    Protected Sub ddlpono2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlpono2.SelectedIndexChanged
        Dim dt As New DataTable
        dt = objutil.ExeQueryDT("exec uspDDLPOSite2 '" & ddlpono2.SelectedItem.Text & "'", "dd")
        ddlsite2.DataSource = dt
        ddlsite2.DataTextField = "txt"
        ddlsite2.DataValueField = "val"
        ddlsite2.DataBind()
    End Sub
    Protected Sub btndoc2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndoc2.Click
        objutil.ExeNonQuery("exec USPSitedelete '" & ddlpono2.SelectedValue.ToString & "','" & ddlsite2.SelectedValue.ToString & "'")
        Response.Write("<script> alert('Selected Site Sucessfully deleted')</script>")
        bindpo()
    End Sub
End Class
