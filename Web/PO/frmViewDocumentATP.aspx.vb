Imports System
Imports BusinessLogic
Imports System.Data
Imports System.IO
Imports Common

Partial Class PO_frmViewDocumentATP
    Inherits System.Web.UI.Page
    Dim strpath As String
    Dim dt As DataTable
    Dim dtDocAdditional As DataTable
    Dim objbo As New BODashBoard
    Dim objBODocs As New BOSiteDocs
    Dim objutil As New DBUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            GetSiteAttribute()
            If IsHaveATPdocument() = True Then
                If String.IsNullOrEmpty(GetDocPathATP()) Then
                    LoadNotHaveATPDocument()
                Else
                    LoadHaveATPDocument()
                End If

            Else
                LoadDefaultDocument()
            End If
        End If
    End Sub

#Region "custom methods"
    Private Sub GetSiteAttribute()
        Dim siteatt As DataTable = objutil.ExeQueryDT("select siteid, version from sitedoc where sw_id=" & Convert.ToInt32(Request.QueryString("id")), "dt")
        HdnSiteid.Value = siteatt.Rows(0).Item(0)
        HdnSiteVersion.Value = siteatt.Rows(0).Item(1)
    End Sub

    Private Function IsHaveATPdocument() As Boolean
        Dim rowCount As Integer = objutil.ExeQueryScalar("select count(*) from sitedoc where siteid=" & HdnSiteid.Value & " and version=" & HdnSiteVersion.Value & " and docid=" & ConfigurationManager.AppSettings("ATPDoc"))
        If rowCount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function GetDocPathATP() As String
        Dim strDocPath As String = objutil.ExeQueryScalarString("select count(*) from sitedoc where siteid=" & HdnSiteid.Value & " and version=" & HdnSiteVersion.Value & " and docid=" & ConfigurationManager.AppSettings("ATP"))
        Return strDocPath
    End Function

    Private Sub LoadDefaultDocument()
        MvMainPanel.SetActiveView(VwFirstPanel)
        dt = objbo.DigitalSign(Convert.ToInt32(Request.QueryString("id")), "1")
        strpath = dt.Rows(0)("docpath").ToString()
        docView.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strpath.Replace("\", "/"))
    End Sub

    Private Sub LoadNotHaveATPDocument()
        MvMainPanel.SetActiveView(VwHTMLPDF)
        Dim divRev As String = String.Empty

        Dim strGetSiteAttribute As String = "select top 1 siteno,sitename,pono,scope from poepmsitenew where siteid=" & HdnSiteid.Value & " and siteversion=" & HdnSiteVersion.Value
        Dim dtSiteAtt As DataTable = objutil.ExeQueryDT(strGetSiteAttribute, "siteatt")
        If dtSiteAtt.Rows.Count > 0 Then
            divPONO.InnerText = dtSiteAtt.Rows(0).Item(2)
            divSiteName.InnerText = dtSiteAtt.Rows(0).Item(1)
            divSiteID.InnerText = dtSiteAtt.Rows(0).Item(0)
            divScope.InnerText = dtSiteAtt.Rows(0).Item(3)
        End If

        Dim strsql = "select distinct case usrtype when 'C' then 'Telkomsel'when 'N' then 'NSN'else 'Partner'end usertype,name,SignTitle,enddatetime, tsk_id, w.sno from wftransaction  W" & _
                 " inner join ebastusers_1 E on e.usr_id=w.LMBY and w.docid = 2001 inner join trole tr on e.usrRole = tr.roleid " & _
                 " where site_id=(select site_id from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") " & _
                 " and siteversion=(select siteversion from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ")" & _
                 " and docid=(select docid from wftransaction where sno= " & Convert.ToInt32(Request.QueryString("id")) & ") and enddatetime is not null and tsk_id in(1,5,6)" & _
                 " and w.status = 0 and w.rstatus = 2 order by w.sno asc"
        Dim userdt As DataTable = objutil.ExeQueryDT(strsql, "ddd")
        Dim kt As Integer
        If userdt.Rows.Count <> 0 Then
            dvPrint.Visible = True
            Dim dates As String = ""
            For kt = 0 To userdt.Rows.Count - 1
                'new code irvan vickrizal
                Dim lvldesc As String = String.Empty
                If (userdt.Rows(kt).Item(4).ToString = "1") Then
                    lvldesc = userdt.Rows(kt).Item(0).ToString & " prepared by "
                Else
                    lvldesc = userdt.Rows(kt).Item(0).ToString & " approved by "
                End If
                divRev += lvldesc & userdt.Rows(kt).Item(1).ToString & ", " & userdt.Rows(kt).Item(2).ToString & " On " & String.Format("{0:dd MMM yyyy}", userdt.Rows(kt).Item(3)) & " " & "<br />"
            Next
        End If
        divReviewer.InnerText = divRev


    End Sub

    Private Sub LoadHaveATPDocument()
        MvMainPanel.SetActiveView(VwSecondPanel)
        dt = objbo.DigitalSign(Convert.ToInt32(Request.QueryString("id")), "1")
        strpath = dt.Rows(0)("docpath").ToString()
        IframeApprovalSheet.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strpath.Replace("\", "/"))
        Dim atpdocpath As String = objutil.ExeQueryScalarString("select top 1 docpath from sitedoc where siteid=" & HdnSiteid.Value & " and version=" & HdnSiteVersion.Value & " and docid=" & ConfigurationManager.AppSettings("ATPDoc"))
        IframeATPDocument.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + atpdocpath.Replace("\", "/"))
    End Sub

#End Region
End Class
