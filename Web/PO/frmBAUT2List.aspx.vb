Imports System.Data
Imports BusinessLogic
Imports Common
Partial Class frmBAUT2List
    Inherits System.Web.UI.Page
    Dim objbo As New BOPoUpload
    Dim dt As New DataTable
    Dim objutil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            bindData()
        End If
    End Sub
    Sub bindData()
        Dim str As String
        str = "select distinct s.sw_id, pd.siteno,cs.site_name,pd.pono,pd.workpkgid" & _
        " from bastmaster B " & _
        " inner join codsite cs on cs.site_id=B.site_id" & _
        " inner join podetails pd on pd.siteno=cs.site_no and pd.siteversion=B.siteversion" & _
        " inner join sitedoc S ON B.SITE_ID=S.SITEID AND B.SITEVERSION=S.VERSION " & _
        " Where B.pstatus=1 and S.isuploaded=0 and S.docid=" & ConfigurationManager.AppSettings("BAST2ID")
        grdBAST2List.DataSource = objutil.ExeQueryDT(str, "bindData")
        grdBAST2List.DataBind()
        grdBAST2List.Columns(0).Visible = False
    End Sub
    Protected Sub grdBAST2List_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBAST2List.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnk As New HyperLink
            lnk.NavigateUrl = "~/BAUT/frmTI_BAUT2.aspx?ID=" & e.Row.Cells(0).Text & ""
            lnk.Text = "Ready to Generate BAST2"
            e.Row.Cells(5).Controls.Add(lnk)
        End If
    End Sub
End Class
