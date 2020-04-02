Imports System.Data
Imports CRFramework
Imports System.IO
Imports Common

Partial Class PO_frmCOViewLog
    Inherits System.Web.UI.Page
    Dim objdb As New DBUtil
    Dim co_controller As New COController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindSiteAtt()
            BindLog()
        End If
    End Sub

#Region "Custom Methods"

    Private Sub BindSiteAtt()
        Dim strQuery As String = "select top 1 siteNo, sitename,PoNo,poname,RGNName,TselProjectID,scope from poepmsitenew where workpackageid='" & GetWPID() & "'"
        Dim dtSiteAtt As DataTable = objdb.ExeQueryDT(strQuery, "siteAtt")
        Dim docname As String = objdb.ExeQueryScalarString("select docname from coddoc where doc_id=" & GetDOCID())
        If dtSiteAtt.Rows.Count > 0 Then
            tddocname.InnerText = docname
            tdpono.InnerText = dtSiteAtt.Rows(0).Item(2).ToString()
            tdponame.InnerText = dtSiteAtt.Rows(0).Item(3).ToString()
            tdsiteno.InnerText = dtSiteAtt.Rows(0).Item(0).ToString()
            tdsitename.InnerText = dtSiteAtt.Rows(0).Item(1).ToString()
            tdscope.InnerText = dtSiteAtt.Rows(0).Item(6).ToString()
            tdwpid.InnerText = GetWPID()
        End If
    End Sub

    Private Sub BindLog()
        gvViewLog.DataSource = co_controller.GetCOAuditTrail(GetCOID(), GetDOCID())
        gvViewLog.DataBind()
    End Sub


    Private Function GetCOID() As Int32
        If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
            Return Convert.ToInt32(Request.QueryString("id"))
        Else
            Return 0
        End If
    End Function

    Private Function GetDOCID() As Integer
        If Not String.IsNullOrEmpty(Request.QueryString("docid")) Then
            Return Integer.Parse(Request.QueryString("docid"))
        Else
            Return 0
        End If
    End Function

    Private Function GetWPID() As String
        If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            Return Request.QueryString("wpid")
        Else
            Return "0"
        End If
    End Function

#End Region

End Class
