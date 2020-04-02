Imports Common
Imports System.Data

Partial Class fancybox_Form_fb_ViewDocument
    Inherits System.Web.UI.Page
    Dim dcontroller As New DocController
    Dim wcontroller As New WCCController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (Not String.IsNullOrEmpty(Request.QueryString("swid"))) Then
                Dim swid As Int32 = Convert.ToInt32(Request.QueryString("swid"))
                If swid > 0 Then
                    BindDocument(Convert.ToInt32(Request.QueryString("swid")))
                Else
                    If Not String.IsNullOrEmpty(Request.QueryString("wid")) Then
                        BindWCCDocument(Convert.ToInt32(Request.QueryString("wid")))
                    End If
                End If

            Else
                If (Not String.IsNullOrEmpty(Request.QueryString("docid")) And Not String.IsNullOrEmpty(Request.QueryString("wpid"))) Then
                    BindDocument(Request.QueryString("wpid"), Integer.Parse(Request.QueryString("docid")))
                End If
            End If
            
        End If
    End Sub


#Region "Custom Methods"
    Private Sub BindDocument(ByVal packageid As String, ByVal docid As Integer)
        Dim docinfo As WCCSitedocInfo = dcontroller.GetDocPathBaseFb(packageid, docid)
        Dim docpath As String = docinfo.DocPath
        spDocumentName.InnerHtml = docinfo.DocName
        If GetParentType().Equals("baut") Then
            docView.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + docpath.Replace("\", "/"))
        Else
            docView.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + docpath.Replace("\", "/"))
        End If

    End Sub

    Private Sub BindWCCDocument(ByVal wccid As Int32)
        Dim docpath = wcontroller.GetWCCDocPath(wccid)
        spDocumentName.InnerHtml = "WCC Online Document"
        docView.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + docpath.Replace("\", "/"))
    End Sub

    Private Sub BindDocument(ByVal swid As Int32)
        Dim docinfo As WCCSitedocInfo = wcontroller.GetSiteDocBaseSWID(swid)
        Dim docpath As String = docinfo.DocPath
        spDocumentName.InnerHtml = docinfo.DocName
        If GetParentType().Equals("baut") Or GetParentType().Equals("bast") Then
            docView.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + docpath.Replace("\", "/"))
        Else
            docView.Attributes.Add("src", ConfigurationManager.AppSettings("VpathLocal") + docpath.Replace("\", "/"))
        End If

    End Sub

    Private Function GetParentType() As String
        If Not String.IsNullOrEmpty(Request.QueryString("parent")) Then
            If Request.QueryString("parent").Equals("baut") Then
                Return "baut"
            ElseIf Request.QueryString("parent").Equals("bast") Then
                Return "bast"
            Else
                Return "wcc"
            End If
        End If
        Return String.Empty
    End Function

#End Region

End Class
