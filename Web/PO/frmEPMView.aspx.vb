Imports BusinessLogic
Imports System.Data
Imports Common
Imports System.IO
Partial Class PO_frmEPMView
    Inherits System.Web.UI.Page
    Dim objdl As New BODDLs
    Dim objtipoview As New BOPODetails
    Dim dt1 As DataTable
    Dim objUtil As New DBUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not IsPostBack Then
            'objdl.fillDDL(ddlPO, "PONoView", True, Constants._DDL_Default_All)
            'dedy 091121 
            objdl.fillDDL(ddlPO, "PoNo", False, Constants._DDL_Default_All)
            BindGridView()
        End If
    End Sub

    Sub BindGridView()
        Dim strsql As String = ""
        Dim strFields As String = ""
        strsql = "select c.name from sysobjects A inner join syscolumns c on a.id= c.id inner join systypes T on c.xtype=T.xusertype where A.xtype='U'  and a.name='EPMDATA'  " & _
            "and not (c.name like 'Act_%' or c.name like 'for_%' or c.name like 'pln_%' or c.name='epmid' or c.name='PStatus' or c.name='RStatus' or C.name='lmby' or c.name='lmdt' or C.name='Address' or c.name='City' )"
        dt1 = objUtil.ExeQueryDT(strsql, "EpmData")
        If dt1.Rows.Count > 0 Then
            For i As Integer = 0 To dt1.Rows.Count - 1
                strFields = strFields & IIf(strFields <> "", ",", "") & dt1.Rows(i).Item("name").ToString
            Next
        End If
        strFields = strFields.Replace("SiteIntegration", "IsNull(convert(varchar(10),SiteIntegration,103),'')SiteIntegration")
        strFields = strFields.Replace("SiteAcpOnAir", "IsNull(convert(varchar(10),SiteAcpOnAir,103),'')SiteAcpOnAir")
        strFields = strFields.Replace("SiteAcpOnBAST", "IsNull(convert(varchar(10),SiteAcpOnBAST,103),'')SiteAcpOnBAST")
        strFields = strFields.Replace("CustPORecDt", "IsNull(convert(varchar(10),CustPORecDt,103),'')CustPORecDt")
        strFields = strFields.Replace("Equipment_Arrived", "IsNull(convert(varchar(10),Equipment_Arrived,103),'')Equipment_Arrived")
        strFields = strFields.Replace("Equipment_On_Site", "IsNull(convert(varchar(10),Equipment_On_Site,103),'')Equipment_On_Site")
        strFields = strFields.Replace("ATP", "IsNull(convert(varchar(10),ATP,103),'')ATP")

        dt1 = Nothing
        strsql = "select c.name from sysobjects A inner join syscolumns c on a.id= c.id inner join systypes T on c.xtype=T.xusertype where A.xtype='U'  and a.name='EPMDATA'  " & _
            "and (c.name like 'Act_%' or c.name like 'for_%' or c.name like 'pln_%')"
        dt1 = objUtil.ExeQueryDT(strsql, "EPMData")
        If dt1.Rows.Count > 0 Then
            For i As Integer = 0 To dt1.Rows.Count - 1
                strFields = strFields & IIf(strFields <> "", ",", "") & "IsNull(convert(varchar(10)," & dt1.Rows(i).Item("name").ToString & ",103),'') " & dt1.Rows(i).Item("name").ToString
            Next
        End If
        strFields = "row_number() over (order by epmid) Sno," & strFields
        If ddlPO.SelectedValue <> "0" Then
            strsql = "Select " & strFields & " from EPMData where CustPONo = '" & ddlPO.SelectedItem.Value & "'"
        Else
            strsql = "Select " & strFields & " from EPMData "
        End If
        dt1 = objUtil.ExeQueryDT(strsql, "EPMDATa")
        btnExport.Visible = IIf(dt1.Rows.Count > 0, True, False)
        grdPOrawdata.DataSource = dt1
        grdPOrawdata.PageSize = Session("Page_size")
        grdPOrawdata.DataBind()
        grdExport.DataSource = dt1
        grdExport.DataBind()
    End Sub

    Protected Sub grdPOrawdata_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdPOrawdata.PageIndexChanging
        grdPOrawdata.PageIndex = e.NewPageIndex
        BindGridView()
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ExportToExcel()
    End Sub

    Public Sub ExportToExcel()

        grdExport.Visible = True
        Dim attachment As String = "attachment;filename=" + IIf(Me.ddlPO.SelectedIndex = 0, "PO-All.xls", ddlPO.SelectedValue + ".xls")

        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sw As StringWriter = New StringWriter()
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)

        Dim frm As HtmlForm = New HtmlForm()
        grdPOrawdata.Parent.Controls.Add(frm)
        frm.Attributes("runat") = "server"
        frm.Controls.Add(grdExport)

        frm.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.End()
        grdExport.Visible = False

    End Sub
    Protected Sub ddlPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPO.SelectedIndexChanged
        BindGridView()
    End Sub

End Class
