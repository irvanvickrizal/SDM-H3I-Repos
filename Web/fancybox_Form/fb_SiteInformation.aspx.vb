Imports System.Data
Imports System.Collections.Generic
Imports Common

Partial Class fancybox_Form_fb_SiteInformation
    Inherits System.Web.UI.Page

    Dim objdb As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not String.IsNullOrEmpty(Request.QueryString("pid"))) Then
            BindSiteAtt(Request.QueryString("pid"))
        End If
    End Sub

#Region "Custom methods"
    Private Sub BindSiteAtt(ByVal packageid As String)
        Dim getSiteAttributeQuery As String = "select  TselProjectID,SiteNo, PoNo,POName, Workpackageid,Scope,sitename,siteidpo,sitenamepo from poepmsitenew where workpackageid = '" & packageid & "'"
        Dim dtSiteAtt As DataTable = objdb.ExeQueryDT(getSiteAttributeQuery, "siteAtt")
        DisplayPoNo.Text = dtSiteAtt.Rows(0).Item(2)
        DisplayPOName.Text = objdb.ExeQueryScalarString("select top 1 poname from poepmsitenew where pono='" & DisplayPoNo.Text & "' and potype is not null")
        DisplaySiteNo.Text = dtSiteAtt.Rows(0).Item(1)
        DisplaySiteName.Text = dtSiteAtt.Rows(0).Item(6)
        DisplaySiteNoPO.Text = dtSiteAtt.Rows(0).Item(7)
        DisplaySiteNamePO.Text = dtSiteAtt.Rows(0).Item(8)
        DisplayScope.Text = dtSiteAtt.Rows(0).Item(5)
        DisplayWPID.Text = dtSiteAtt.Rows(0).Item(4)
        DisplayProjectID.Text = dtSiteAtt.Rows(0).Item(0)
    End Sub
#End Region

End Class
