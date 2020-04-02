Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data
Imports System.IO
Imports System.Web.UI.WebControls
Partial Class COD_frmsitereasonlisting
    Inherits System.Web.UI.Page
    Dim objET As New ETSiteReason
    Dim objBO As New BOSiteReason
    Dim objBOSD As New BOSiteDocs
    Dim dt As DataTable
    Dim objdl As New BODDLs
    Dim objutil As New DBUtil
    Dim dgItem As DataGridItem
    Dim chkSelected As CheckBox
    Dim qryStr As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            objdl.fillDDL(ddlReasonCategory, "ReasonCategory", True, Constants._DDL_Default_Select)
            objdl.fillDDL(ddlPO, "PoNo", True, "--Please Select--")
            binddata()
        End If
    End Sub
    Sub binddata()
        dt = objutil.ExeQueryDT("Exec siteReasonListing", "SSS")
        If dt.Rows.Count > 0 Then
            grdSiteReason.DataSource = dt
            grdSiteReason.DataBind()
            grdSiteReason1.DataSource = dt
            grdSiteReason1.DataBind()
        End If
    End Sub
    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Response.Redirect("frmSiteReason.aspx")
    End Sub
    Protected Sub ddlReasonCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlReasonCategory.SelectedIndexChanged
        dt = objBO.uspSitereasonCatlist(ddlReasonCategory.SelectedValue)
        grdSiteReason.DataSource = dt
        grdSiteReason.DataBind()
        grdSiteReason1.DataSource = dt
        grdSiteReason1.DataBind()
    End Sub
    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPO.SelectedIndexChanged
        ddlsite.Items.Clear()
        If ddlPO.SelectedItem.Text <> "--Please Select--" Then
            Dim ddldt As DataTable
            ddldt = objBOSD.uspDDLPOSiteNo(ddlPO.SelectedItem.Value)
            If ddldt.Rows.Count > 0 Then
                ddlsite.DataSource = ddldt
                ddlsite.DataTextField = "txt"
                ddlsite.DataValueField = "VAL"
                ddlsite.DataBind()
                ddlsite.Items.Insert(0, "--Select--")
            End If
        End If
    End Sub
    Protected Sub lnkAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAll.Click
        dt = objutil.ExeQueryDT("Exec siteReasonListing", "SSS")
        grdSiteReason.DataSource = dt
        grdSiteReason.DataBind()
        grdSiteReason1.DataSource = dt
        grdSiteReason1.DataBind()
    End Sub
    Protected Sub ddlsite_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlsite.SelectedIndexChanged
        dt = objBO.uspSitereasonCatlist2(ddlPO.SelectedValue, ddlsite.SelectedValue)
        grdSiteReason.DataSource = dt
        grdSiteReason.DataBind()
        grdSiteReason1.DataSource = dt
        grdSiteReason1.DataBind()
    End Sub
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        For Each dgItem In grdSiteReason.Items
            chkSelected = dgItem.FindControl("chkSelection")
            If chkSelected.Checked Then
                qryStr = "update siteReason set checked=1 where sno=" & CType(dgItem.FindControl("lblReasonID"), Label).Text
            Else
                qryStr = "update siteReason set checked=0 where sno=" & CType(dgItem.FindControl("lblReasonID"), Label).Text
            End If
            objutil.ExeQuery(qryStr)
            chkSelected = dgItem.FindControl("chkSelection1")
            If chkSelected.Checked Then
                qryStr = "update siteReason set isbaut=1 where sno=" & CType(dgItem.FindControl("lblReasonID"), Label).Text
            Else
                qryStr = "update siteReason set isbaut=0 where sno=" & CType(dgItem.FindControl("lblReasonID"), Label).Text
            End If
            objutil.ExeQuery(qryStr)
            chkSelected = dgItem.FindControl("chkSelection2")
            If chkSelected.Checked Then
                qryStr = "update siteReason set isbast=1 where sno=" & CType(dgItem.FindControl("lblReasonID"), Label).Text
            Else
                qryStr = "update siteReason set isbast=0 where sno=" & CType(dgItem.FindControl("lblReasonID"), Label).Text
            End If
            objutil.ExeQuery(qryStr)
        Next
    End Sub
    Protected Sub btnExport_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.ServerClick
        ExportToExcel()
    End Sub
    Public Sub ExportToExcel()
        Dim tw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("content-disposition", "attachment;filename=Milestone.xls")
        Response.Charset = ""
        EnableViewState = False
        Controls.Add(frm)
        frm.Controls.Add(grdSiteReason1)
        frm.RenderControl(hw)
        Response.Write(tw.ToString())
        Response.End()
    End Sub
End Class
