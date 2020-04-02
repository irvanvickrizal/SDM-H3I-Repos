Imports System.Collections.Generic
Imports System.IO
Partial Class fancybox_Form_fb_SOACViewLog
    Inherits System.Web.UI.Page

    Dim controller As New SOACController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not String.IsNullOrEmpty(GetWPID()) Then
                BindAttribute(GetWPID())
            End If
            If GetSOACID() > 0 Then
                BindData(GetSOACID(), GetSourceType(), GetDOCID())
            End If
        End If
    End Sub

    Protected Sub BtnExportExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportExcel.Click
        If GvSOACLog.Rows.Count > 0 Then
            Dim tw As New StringWriter()
            Dim getsiteatt As String = LblSiteNo.Text & "_" & LblPONO.Text & "_"
            Dim strFilename As String = LblDocType.Text + "_" + getsiteatt + "Log_" + DateTime.Now.ToShortDateString + ".xls"
            If Not String.IsNullOrEmpty(GetSourceType()) Then
                strFilename = DdlDocType.SelectedItem.Text + "_" + getsiteatt + "Log_" + DateTime.Now.ToShortDateString + ".xls"
            End If
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GvSOACLog)
            frm.RenderControl(hw)
            Response.Write(tw.ToString())
            Response.End()
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "invalidExportToExcel();", True)
            End If
        End If
    End Sub

    Protected Sub DdlDocType_IndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlDocType.SelectedIndexChanged
        BindLog(GetSOACID(), Integer.Parse(DdlDocType.SelectedValue()))
    End Sub

#Region "Custom Methods"
    Private Sub BindAttribute(ByVal packageid As String)
        Dim info As SiteInfo = controller.GetSiteDetail_WPID(packageid, 1855)
        LblSitename.Text = info.SiteName
        LblSiteNo.Text = info.SiteNo
        LblPONO.Text = info.PONO
        LblScope.Text = info.Scope
        LblWorkpackageid.Text = packageid
    End Sub

    Private Sub BindData(ByVal soacid As Int32, ByVal sourcetype As String, ByVal docid As Integer)
        If String.IsNullOrEmpty(sourcetype) Then
            DdlDocType.Visible = False
            LblDocType.Visible = True
            Dim docs As List(Of DocInfo) = New SOACDocController().GetDocBaseSOACLog(soacid)
            If docs.Count > 0 Then
                For Each doc As DocInfo In docs
                    If doc.DocId = docid Then
                        LblDocType.Text = doc.DocName
                        Exit For
                    End If
                Next
            End If
        Else
            DdlDocType.Visible = True
            LblDocType.Visible = False
            BindDoc(soacid, DdlDocType, docid)
        End If

        BindLog(soacid, docid)
    End Sub

    Private Sub BindLog(ByVal soacid As Int32, ByVal docid As Integer)
        GvSOACLog.DataSource = controller.GetSOACAuditTrail(soacid, docid)
        GvSOACLog.DataBind()
    End Sub

    Private Sub BindDoc(ByVal soacid As Int32, ByVal ddl As DropDownList, ByVal docid As Integer)
        ddl.DataSource = New SOACDocController().GetDocBaseSOACLog(soacid)
        ddl.DataTextField = "docname"
        ddl.DataValueField = "docid"
        ddl.DataBind()

        ddl.SelectedValue = Convert.ToString(docid)
    End Sub

    Private Function GetSourceType() As String
        If Not String.IsNullOrEmpty(Request.QueryString("st")) Then
            Return Request.QueryString("st")
        Else
            Return String.Empty
        End If
    End Function

    Private Function GetDOCID() As Integer
        If Not String.IsNullOrEmpty(Request.QueryString("doc")) Then
            Return Integer.Parse(Request.QueryString("doc"))
        Else
            Return 0
        End If
    End Function

    Private Function GetSOACID() As Int32
        If Not String.IsNullOrEmpty(Request.QueryString("sid")) Then
            Return Convert.ToInt32(Request.QueryString("sid"))
        Else
            Return 0
        End If
    End Function

    Private Function GetWPID() As String
        If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            Return Request.QueryString("wpid")
        Else
            Return String.Empty
        End If
    End Function
#End Region

End Class
