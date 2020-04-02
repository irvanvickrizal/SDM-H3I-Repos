Imports Common
Imports System.Data
Imports System.IO
Imports System.Xml
Partial Class PO_frmEPMSend
    Inherits System.Web.UI.Page
    Dim objutil As New DBUtil
    Dim dt As New DataTable
    Dim strsql As String
    Dim cst As New Constants
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnFrom.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtFrom,'dd/mm/yyyy');return false;")
        btnTo.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtTo,'dd/mm/yyyy');return false;")
        If Page.IsPostBack = False Then
        End If
    End Sub
    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim strfrom As String = ""
        Dim strTo As String = ""
        If txtFrom.Value <> "" And txtTo.Value <> "" Then
            strfrom = cst.formatDDMMYYYY(txtFrom.Value)
            strTo = cst.formatDDMMYYYY(txtTo.Value)
        End If
        strsql = "Select SiteId,WorkPackageId,convert(varchar(20),ACT_9500,103) ACT_9500,convert(varchar(20),ACT_9750,103) ACT_9750 from EPMData "
        If strfrom <> "" And strTo <> "" Then
            strsql = strsql & " where (Convert(varchar(8),ACT_9500,112) Between '" & strfrom & "' and '" & strTo & "') or (Convert(varchar(8),ACT_9750,112) Between '" & strfrom & "' and '" & strTo & "')"
        End If
        dt = objutil.ExeQueryDT(strsql, "PONo")
        grdExport.DataSource = dt
        grdExport.DataBind()
        grdExport.Visible = True
        If grdExport.Rows.Count > 0 Then
            Dim strfilename As String = Format(DateTime.Now, "yyyyMMddHHss") & ".xls"
            Dim sw As HtmlTextWriter = New HtmlTextWriter(New StreamWriter("C:/EPM/" & strfilename, False, System.Text.UnicodeEncoding.UTF8))
            grdExport.RenderControl(sw)
            sw.Close()
            sw.Dispose()
            lbl1.Text = "Note: File generated in C:/EPM/" & strfilename
        Else
            Response.Write("<script>alert('No records Found')</script>")

        End If

    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub
End Class
