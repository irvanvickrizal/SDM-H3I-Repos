Imports Common
Imports System.Data
Imports System.IO

Partial Class RPT_frmSiteApprovalStatus_NPO
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objutil As New DBUtil
    Dim strsql As String = ""
    Dim controller As New HCPTController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            bindData()
            If Not String.IsNullOrEmpty(Request.QueryString("from")) Then
                pnlSearch.Visible = False
            Else
                pnlSearch.Visible = True
                BindSearchAdvanced()
            End If
            DdlDocuments.Visible = False
        End If
    End Sub
    Sub bindData()
        strsql = "exec HCPT_uspTrans_GetSiteApprovalStatus_NPO " & CommonSite.UserId
        dt = objutil.ExeQueryDT(strsql, "uspSiteApprovalStatus")
        grdDB.DataSource = dt
        grdDB.DataBind()
    End Sub
    Protected Sub grdDB_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDB.PageIndexChanging
        grdDB.PageIndex = e.NewPageIndex
        bindData()
    End Sub
    Protected Sub grdDB_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDB.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdDB.PageIndex * grdDB.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub
    Protected Sub btnExport_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.ServerClick
        If grdDB.Rows.Count > 0 Then
            ExportToExcel()
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "invalidExportToExcel();", True)
            End If
        End If

    End Sub

    Protected Sub BtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        BindSearchDataAdvanced(ddlToIntValue(DdlDocuments), ddlToStrValue(DdlCompany), ddlToIntValue(DdlUserTask))
    End Sub

    Protected Sub DdlCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlCompany.SelectedIndexChanged
        If DdlCompany.SelectedIndex > 0 Then
            BindUserTaskPending(DdlCompany.SelectedValue)
        Else
            BindUserTaskPending(String.Empty)
        End If
    End Sub

    Public Sub ExportToExcel()
        Dim tw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()
        Dim strFilename As String = "SiteApprovalStatus_" + DateTime.Now.ToShortDateString + ".xls"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("content-disposition", "attachment;filename=" & strFilename)
        Response.Charset = ""
        EnableViewState = False
        Controls.Add(frm)
        grdDB.AllowPaging = False
        bindData()
        frm.Controls.Add(grdDB)
        frm.RenderControl(hw)
        Response.Write(tw.ToString())
        Response.End()
    End Sub
    Private Sub BindSearchAdvanced()
        BindDocument()
        BindUserType()
        BindUserTaskPending(String.Empty)
    End Sub

    Private Sub BindDocument()
        DdlDocuments.DataSource = controller.GetDocuments("O")
        DdlDocuments.DataTextField = "Docname"
        DdlDocuments.DataValueField = "DocId"
        DdlDocuments.DataBind()
        DdlDocuments.Items.Insert(0, "All Documents")
    End Sub

    Private Sub BindUserType()
        DdlCompany.DataSource = controller.GetUserTask()
        DdlCompany.DataTextField = "UserCompany"
        DdlCompany.DataValueField = "UserType"
        DdlCompany.DataBind()

        DdlCompany.Items.Insert(0, "--All Companies--")
    End Sub

    Private Sub BindUserTaskPending(ByVal usrtype As String)
        DdlUserTask.DataSource = controller.GetUserPending(usrtype)
        DdlUserTask.DataTextField = "fullname"
        DdlUserTask.DataValueField = "subconid"
        DdlUserTask.DataBind()

        DdlUserTask.Items.Insert(0, "--All Users--")
    End Sub

    Private Sub BindSearchDataAdvanced(ByVal docid As Integer, ByVal usrtype As String, ByVal roleid As Integer)
        strsql = "exec HCPT_uspTrans_GetSiteApprovalStatus_advancedsearch_NPO " & CommonSite.UserId & ", " & 0 & ", '" & usrtype & "', " & roleid
        dt = objutil.ExeQueryDT(strsql, "uspSiteApprovalStatus")
        grdDB.DataSource = dt
        grdDB.DataBind()
    End Sub

    Private Function ddlToIntValue(ByVal ddl As DropDownList) As Integer
        If ddl.SelectedIndex > 0 Then
            Return Integer.Parse(ddl.SelectedValue)
        Else
            Return 0
        End If
    End Function

    Private Function ddlToStrValue(ByVal ddl As DropDownList) As String
        If ddl.SelectedIndex > 0 Then
            Return ddl.SelectedValue
        Else
            Return String.Empty
        End If
    End Function

End Class
