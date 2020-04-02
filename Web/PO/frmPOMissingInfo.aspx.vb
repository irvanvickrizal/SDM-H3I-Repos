Imports Common
Imports System.Data
Imports BusinessLogic
Imports System.IO

Partial Class PO_frmPOMissingInfo
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        If Page.IsPostBack = False Then
            If Request.QueryString("id") <> "" Then
                Session("RawPONo") = Trim(Request.QueryString("id"))
            End If
            If Request.QueryString("DB") = 0 Or Request.QueryString("DB") = 1 Or Request.QueryString("DB") = 2 Then
                objBOD.fillDDL(ddlPO, "PODetails", True, Constants._DDL_Default_All)
            Else
                trPono.Visible = False
            End If
            binddata()
            '  GridView2.Visible = False
        End If
    End Sub
    Sub binddata()
        Dim objutil As New DBUtil
        Dim dt As New DataTable
        Dim strCondition As String = ""
        Dim strsql As String = ""
        If Request.QueryString("Type") = 1 Then
            strCondition = " ECase1 = 1"
        ElseIf Request.QueryString("Type") = 2 Then
            strCondition = " ECase2 = 1"
        ElseIf Request.QueryString("Type") = 3 Then
            strCondition = " ECase3 = 1"
        ElseIf Request.QueryString("Type") = 4 Then
            strCondition = " ECase4 = 1"
        ElseIf Request.QueryString("Type") = 5 Then
            strCondition = " ECase5 = 1"
        End If
        If Request.QueryString("Type") = 1 Then
            If Request.QueryString("DB") = 0 Then 'And Request.QueryString("pono") <> "" Then
                btnExport.Visible = True
                If ddlPO.SelectedIndex <> 0 Then
                    strsql = "Select a.PONo,a.SiteNo,a.SiteName,A.Fldtype Scope,a.WorkPkgId,a.FldType,a.Description,a.Band_Type,a.Band,a.Config,a.purchase900 purchase900,a.purchase900 Cpurchase900,a.purchase1800, a.purchase1800 Cpurchase1800,A.Qty, A.Qty CQty  from porawdata a where ECase1 = 1 and a.pono='" & ddlPO.SelectedValue & "'"
                Else
                    strsql = "Select a.PONo,a.SiteNo,a.SiteName,A.Fldtype Scope,a.WorkPkgId,a.FldType,a.Description,a.Band_Type,a.Band,a.Config,a.purchase900 purchase900,a.purchase900 Cpurchase900,a.purchase1800, a.purchase1800 Cpurchase1800,A.Qty, A.Qty CQty  from porawdata a where ECase1 = 1 "
                End If
            ElseIf Request.QueryString("DB") = 1 Then
                btnExport.Visible = True
                If ddlPO.SelectedIndex <> "0" Then
                    strsql = "select a.PONo,a.SiteNo,a.SiteName,A.Fldtype Scope,a.WorkPkgId,a.FldType,a.Description,a.Band_Type,a.Band,a.Config,a.purchase900 purchase900,a.purchase900 Cpurchase900,a.purchase1800, a.purchase1800 Cpurchase1800,A.Qty,A.Qty CQty from podetails a where a.pono = '" & ddlPO.SelectedValue & "' and a.WorkPkgId not in (select workpackageid from epmdata)"
                Else
                    strsql = "select a.PONo,a.SiteNo,a.SiteName,A.Fldtype Scope,a.WorkPkgId,a.FldType,a.Description,a.Band_Type,a.Band,a.Config,a.purchase900 purchase900,a.purchase900 Cpurchase900,a.purchase1800, a.purchase1800 Cpurchase1800,A.Qty,A.Qty CQty from podetails a where a.WorkPkgId not in (select workpackageid from epmdata)"
                End If
            ElseIf Request.QueryString("DB") = 2 Then
                btnExport.Visible = True
                If ddlPO.SelectedIndex <> "0" Then
                    strsql = " select a.PONo,a.SiteNO,cs.site_name sitename,a.FldType Scope,a.WorkPkgId,a.Band_Type,a.Band,a.Config,a.purchase900 purchase900,a.purchase900 Cpurchase900,a.purchase1800, a.purchase1800 Cpurchase1800,A.Qty,A.Qty CQty from poduplicatesite a inner join codsite cs on a.siteno= cs.site_no where a.PONo= '" & ddlPO.SelectedValue & "' and a.Rstatus=2 "
                Else
                    strsql = " select a.PONo,a.SiteNO,cs.site_name sitename,a.FldType Scope,a.WorkPkgId,a.Band_Type,a.Band,a.Config,a.purchase900 purchase900,a.purchase900 Cpurchase900,a.purchase1800, a.purchase1800 Cpurchase1800,A.Qty,A.Qty CQty from poduplicatesite a inner join codsite cs on a.siteno= cs.site_no where a.Rstatus=2 "
                End If
            Else
                strsql = "Select a.PONo,a.SiteNo,a.SiteName,A.Fldtype Scope,a.WorkPkgId,a.FldType,a.Description,a.Band_Type,a.Band,a.Config,a.purchase900 purchase900,a.purchase900 Cpurchase900,a.purchase1800, a.purchase1800 Cpurchase1800,A.Qty, A.Qty CQty  from porawdata a where ECase1 = 1 and A.PONo='" & Session("RawPONo") & "'Order by A.SiteNo"
            End If
        ElseIf Request.QueryString("Type") = 6 Then
            strsql = "Select a.PONo,a.SiteNo,a.SiteName,A.Fldtype Scope,a.WorkPkgId,a.FldType,a.Description,a.Band_Type,a.Band,a.Config,a.purchase900 purchase900,a.purchase900 Cpurchase900,a.purchase1800, a.purchase1800 Cpurchase1800,A.Qty, A.Qty CQty  from poDetails a where A.PONo='" & Session("RawPONo") & "' and A.SiteNo Not in(Select Site_No from CodSite where RStatus = 2) Order by A.SiteNo"
        Else
            strsql = "Select a.PONo,a.SiteNo,a.SiteName,A.Fldtype Scope,a.WorkPkgId,a.FldType,a.Description,a.Band_Type,a.Band,a.Config,a.purchase900,a.purchase1800,b.Config,b.purchase900 Cpurchase900,b.purchase1800 Cpurchase1800,A.Qty, B.Qty CQty from porawdata a Left Outer Join poDetails b on b.sno=a.sno where A.PONo='" & Session("RawPONo") & "' And ECase1 = 0 "
            strsql = strsql & " and " & strCondition & " Order by A.SiteNo"
        End If
        dt = objutil.ExeQueryDT(strsql, "Po")
        'DataList1.DataSource = dt
        'DataList1.DataBind()
        GridView1.DataSource = dt
        GridView1.PageSize = Session("Page_size")
        GridView1.DataBind()
        GridView2.DataSource = dt
        GridView2.DataBind()
        If Request.QueryString("Type") = 1 Or Request.QueryString("Type") = 6 Then
            GridView1.Columns(7).Visible = False
            GridView1.Columns(8).Visible = False
            GridView1.Columns(9).Visible = False
            GridView1.Columns(10).Visible = False
            GridView1.Columns(11).Visible = False
            GridView2.Columns(7).Visible = False
            GridView2.Columns(8).Visible = False
            GridView2.Columns(9).Visible = False
            GridView2.Columns(10).Visible = False
            GridView2.Columns(11).Visible = False
        ElseIf Request.QueryString("Type") = 4 Then
            GridView1.Columns(8).Visible = False
            GridView1.Columns(9).Visible = False
            GridView1.Columns(12).Visible = False
        ElseIf Request.QueryString("Type") = 5 Or Request.QueryString("Type") = 3 Or Request.QueryString("Type") = 2 Then
            GridView1.Columns(10).Visible = False
            GridView1.Columns(12).Visible = False
        End If
        dt = Nothing
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        binddata()
    End Sub

    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPO.SelectedIndexChanged
        binddata()
    End Sub

    Protected Sub btnExport_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.ServerClick
        ExportToExcel()
    End Sub
    Public Sub ExportToExcel()
        Dim tw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("content-disposition", "attachment;filename=AuditLogInfo.xls")
        Response.Charset = ""
        EnableViewState = False
        Controls.Add(frm)
        frm.Controls.Add(GridView2)
        frm.RenderControl(hw)
        Response.Write(tw.ToString())
        Response.End()
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub
End Class
