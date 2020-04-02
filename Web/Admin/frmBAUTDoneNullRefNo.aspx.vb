Imports System.Data
Imports System.Text
Imports System.IO
Partial Class Admin_frmBAUTDoneNullRefNo
    Inherits System.Web.UI.Page
    Private controller As New BAUTMasterController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SM1.RegisterPostBackControl(Me.BtnExportExcel)
        If Not Page.IsPostBack Then
            'BindData(GetPoNo(DdlPoNo), GetPackageIdInput(TxtPackageid), CommonSite.UserId)
            BindPoNo(DdlPoNo, CommonSite.UserId)
        End If
    End Sub

    Protected Sub BtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        BindData(GetPoNo(DdlPoNo), GetPackageIdInput(TxtPackageid), CommonSite.UserId)
    End Sub

    Protected Sub GvDocBAUT_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        GvDocBAUT.EditIndex = -1
        BindData(GetPoNo(DdlPoNo), GetPackageIdInput(TxtPackageid), CommonSite.UserId)
    End Sub

    Protected Sub GvDocBAUT_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        GvDocBAUT.EditIndex = e.NewEditIndex
       BindData(GetPoNo(DdlPoNo), GetPackageIdInput(TxtPackageid), CommonSite.UserId)
    End Sub

    Protected Sub GvDocBAUT_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim ddlBAUTStatus As DropDownList = CType(GvDocBAUT.Rows(e.RowIndex).FindControl("DdlBAUTStatus"), DropDownList)
        Dim txtReferenceNo As TextBox = CType(GvDocBAUT.Rows(e.RowIndex).FindControl("TxtRefNo"), TextBox)
        Dim LblSiteId As Label = CType(GvDocBAUT.Rows(e.RowIndex).FindControl("LblSiteId"), Label)
        Dim LblVersion As Label = CType(GvDocBAUT.Rows(e.RowIndex).FindControl("LblVersion"), Label)
        Dim LblPono As Label = CType(GvDocBAUT.Rows(e.RowIndex).FindControl("LblPONO"), Label)
        Dim LblPackageId As Label = CType(GvDocBAUT.Rows(e.RowIndex).FindControl("LblPackageId"), Label)

        If Not ddlBAUTStatus Is Nothing And Not LblSiteId Is Nothing And Not txtReferenceNo Is Nothing _
           And Not LblVersion Is Nothing And Not LblPono Is Nothing And Not LblPackageId Is Nothing Then
            controller.UpdateBautRefNo(Convert.ToInt32(LblSiteId.Text), Integer.Parse(LblVersion.Text), LblPono.Text, LblPackageId.Text, txtReferenceNo.Text, ConfigurationManager.AppSettings("BAUTID"), Convert.ToBoolean(Integer.Parse(ddlBAUTStatus.SelectedValue)))
            GvDocBAUT.EditIndex = -1
            BindData(GetPoNo(DdlPoNo), GetPackageIdInput(TxtPackageid), CommonSite.UserId)
        End If

    End Sub

    Protected Sub GvDocBAUT_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)

    End Sub

    Protected Sub GvDocBAUT_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvDocBAUT.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim bautstatus As Label = CType(e.Row.FindControl("LblBautStatusEdit"), Label)
            If Not bautstatus Is Nothing Then
                Dim ddlBautStatus As DropDownList = CType(e.Row.FindControl("DdlBAUTStatus"), DropDownList)
                If Not ddlBautStatus Is Nothing Then
                    If bautstatus.Text.Equals("True") Then
                        ddlBautStatus.SelectedValue = "1"
                    Else
                        ddlBautStatus.SelectedValue = "0"
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub BtnExportExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportExcel.Click
        If (GvDocBAUTPrint.Rows.Count() > 0) Then
            Dim tw As New StringWriter()
            Dim strFilename As String = "BAUTDoneReportNullRefNo_" + DateTime.Now.ToShortDateString + ".xls"
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" + strFilename)
            Response.Charset = ""
            EnableViewState = False
            Controls.Add(frm)
            frm.Controls.Add(GvDocBAUTPrint)
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

    Protected Sub GvDocBAUT_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GvDocBAUT.PageIndexChanging
        GvDocBAUT.PageIndex = e.NewPageIndex
        BindData(GetPoNo(DdlPoNo), GetPackageIdInput(TxtPackageid), CommonSite.UserId)
    End Sub


#Region "Custom Methods"
    Private Sub BindData(ByVal pono As String, ByVal packageid As String, ByVal userid As Integer)
        Dim ds As DataSet = controller.GetBAUTDoneMaster_DS(pono, packageid, userid)
        If (ds.Tables(0).Rows.Count > 0) Then
            GvDocBAUT.DataSource = ds
            GvDocBAUT.DataBind()
            GvDocBAUTPrint.DataSource = ds
            GvDocBAUTPrint.DataBind()
        Else
            ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
            GvDocBAUT.DataSource = ds
            GvDocBAUT.DataBind()
            Dim columncount As Integer = GvDocBAUT.Rows(0).Cells.Count
            GvDocBAUT.Rows(0).Cells.Clear()
            GvDocBAUT.Rows(0).Cells.Add(New TableCell())
            GvDocBAUT.Rows(0).Cells(0).ColumnSpan = columncount
            GvDocBAUT.Rows(0).Cells(0).Text = "No Records Found"

            GvDocBAUTPrint.DataSource = Nothing
            GvDocBAUTPrint.DataBind()
        End If

    End Sub

    Private Function GetPoNo(ByVal ddl As DropDownList) As String
        If ddl.SelectedIndex > 0 Then
            Return ddl.SelectedValue
        Else
            Return String.Empty
        End If
    End Function

    Private Function GetPackageIdInput(ByVal txt As TextBox) As String
        If Not String.IsNullOrEmpty(txt.Text) Then
            Return txt.Text
        Else
            Return String.Empty
        End If
    End Function

    Private Sub BindPoNo(ByVal ddl As DropDownList, ByVal userid As Integer)
        ddl.DataSource = controller.GetPoNoBaseRole(userid)
        ddl.DataTextField = "PoNo"
        ddl.DataValueField = "PoNo"
        ddl.DataBind()

        ddl.Items.Insert(0, "--select pono--")
    End Sub

    Private Sub upgATPReport_Load(sender As Object, e As EventArgs) Handles upgATPReport.Load
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "adjustSize();", True)
    End Sub

    Private Sub upgATPReport_Init(sender As Object, e As EventArgs) Handles upgATPReport.Init
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "adjustSize();", True)
    End Sub

#End Region

End Class
