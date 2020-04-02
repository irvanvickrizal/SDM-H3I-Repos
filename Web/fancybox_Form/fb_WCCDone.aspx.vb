Imports System.Text
Imports System.IO

Partial Class fancybox_Form_fb_WCCDone
    Inherits System.Web.UI.Page

    Private controller As New WCCController
    Private scontroller As New ScopeController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            BindScope()
            HdnSearchId.Value = "0"
            If (Not String.IsNullOrEmpty(Request.QueryString("dtype"))) Then
                If Request.QueryString("dtype").Equals("subcon") Then
                    BindData(CommonSite.UserId, True)
                Else
                    BindData(CommonSite.UserId, False)
                End If
            Else
                BindData(CommonSite.UserId, False)
            End If
        End If
    End Sub

    Protected Sub LbtExportToExcel(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtExport.Click
        ExportToExcel(GvWCCDoneExport)
    End Sub

    Protected Sub GvWCCList_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GvWCCList.PageIndexChanging
        GvWCCList.PageIndex = e.NewPageIndex
        If HdnSearchId.Value = "0" Then
            If (Not String.IsNullOrEmpty(Request.QueryString("dtype"))) Then
                If Request.QueryString("dtype").Equals("subcon") Then
                    BindData(CommonSite.UserId, True)
                Else
                    BindData(CommonSite.UserId, False)
                End If
            Else
                BindData(CommonSite.UserId, False)
            End If
        Else
            BindDataAdvanceSearch(CommonSite.UserId, IsSubcon(), ReturnDateFormat(TxtStartDate.Text), ReturnDateFormat(TxtEndDate.Text), GetScopeSearch(DdlScope))
        End If
    End Sub

    Protected Sub LbtSearch_Click(ByVal sender As Object, ByVal byvale As System.EventArgs) Handles LbtSearch.Click
        HdnSearchId.Value = "1"
        If (Not String.IsNullOrEmpty(TxtEndDate.Text)) Then
            If (String.IsNullOrEmpty(TxtStartDate.Text)) Then
                If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                    Page.ClientScript.RegisterStartupScript _
                    (Me.GetType(), "alert", "invalidDateSearch();", True)
                Else
                    BindDataAdvanceSearch(CommonSite.UserId, IsSubcon(), ReturnDateFormat(TxtStartDate.Text), ReturnDateFormat(TxtEndDate.Text), GetScopeSearch(DdlScope))
                End If
            End If
        Else
            BindDataAdvanceSearch(CommonSite.UserId, IsSubcon(), ReturnDateFormat(TxtStartDate.Text), ReturnDateFormat(TxtEndDate.Text), GetScopeSearch(DdlScope))
        End If
    End Sub

    Protected Sub GVWccList_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvWCCList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ViewWCCDone As HtmlAnchor = CType(e.Row.FindControl("ViewWCCDone"), HtmlAnchor)
            If Not ViewWCCDone Is Nothing Then
                Dim LblWCCID As Label = CType(e.Row.FindControl("LblWCCID"), Label)
                ViewWCCDone.HRef = "fb_FormWCCDone.aspx?wid=" & LblWCCID.Text
            End If
        End If
    End Sub

#Region "Custom Methods"

    Private Sub BindScope()
        DdlScope.DataSource = scontroller.GetAllDetailScopes(False, 0)
        DdlScope.DataValueField = "DScopeId"
        DdlScope.DataTextField = "DscopeName"
        DdlScope.DataBind()

        DdlScope.Items.Insert(0, "--all--")

    End Sub

    Private Sub BindDataAdvanceSearch(ByVal userid As Integer, ByVal isSubcon As Boolean, ByVal starttime As Nullable(Of DateTime), ByVal endtime As Nullable(Of DateTime), ByVal dscopeid As Integer)
        GvWCCList.DataSource = controller.GetWCCDone_AdvanceSearch(userid, starttime, endtime, dscopeid, isSubcon)
        GvWCCList.DataBind()
        GvWCCDoneExport.DataSource = controller.GetWCCDone_AdvanceSearch(userid, starttime, endtime, dscopeid, isSubcon)
        GvWCCDoneExport.DataBind()
    End Sub

    Private Sub BindData(ByVal userid As Integer, ByVal isSubcon As Boolean)
        GvWCCList.DataSource = controller.GetWCCDone(userid, isSubcon)
        GvWCCList.DataBind()
        GvWCCDoneExport.DataSource = controller.GetWCCDone(userid, isSubcon)
        GvWCCDoneExport.DataBind()
    End Sub

    Private Sub ExportToExcel(ByVal gv As GridView)
        If (gv.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "WCCDone_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(gv)
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

    Private Function IsSubcon() As Boolean
        If Not String.IsNullOrEmpty(Request.QueryString("dtype")) Then
            If Request.QueryString("dtype") = "subcon" Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Function GetScopeSearch(ByVal ddl As DropDownList) As Integer
        If ddl.SelectedIndex > 0 Then
            Return Integer.Parse(ddl.SelectedValue)
        Else
            Return 0
        End If
    End Function

    Private Function ReturnDateFormat(ByVal strDate As String) As Nullable(Of DateTime)
        If String.IsNullOrEmpty(strDate) Then
            Return Nothing
        Else
            Return DateTime.ParseExact(strDate, "dd-MMMM-yyyy", Nothing)
        End If
    End Function
#End Region

End Class
