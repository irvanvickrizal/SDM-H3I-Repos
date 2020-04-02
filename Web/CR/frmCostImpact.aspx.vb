Imports Common
Imports BusinessLogic
Imports Entities
Imports System.Data
Partial Class CR_frmCostImpact
    Inherits System.Web.UI.Page
    Dim dt As DataTable
    Dim objBO As New BOMOM
    Dim objBL As New BOMOMTEST
    Private objEMOMD As New ETMOMDetails

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("Page_size") Is Nothing Then Response.Redirect("~/SessionTimeout.aspx")
        If Page.IsPostBack = False Then
            BindDataList()            
        End If
    End Sub
     Sub BindDataList()
        If Request.QueryString("id") <> Nothing Then
            Session("SiteNo") = Request.QueryString("id")
            dt = objBO.uspMOMEnterCostImpact(Session("SiteNo"))
            If dt.Rows.Count > 0 Then
                Dim dr As DataRow = dt.Rows(0)
                DataList1.DataSource = dt
                DataList1.DataBind()
                'Change Request
                Session("MOMId") = dr.Item("MOMID").ToString()
                txtSiteNo.InnerText = dr.Item("SiteNo").ToString()
                txtSiteName.InnerText = dr.Item("SiteName").ToString()
                txtWorkPkgID.InnerText = dr.Item("WorkPkgId").ToString()
                txtWorkPkgName.InnerText = dr.Item("workPKGName").ToString()
                txtFldType.InnerText = dr.Item("Scope").ToString()
                txtDesc.InnerText = dr.Item("Description").ToString()
                txtBandType1.InnerText = dr.Item("Band_Type").ToString()
                txtBand.InnerText = dr.Item("Band").ToString()
                txtconfig.InnerText = dr.Item("Config").ToString()
                txtPurchase900.InnerText = dr.Item("Purchase900").ToString()
                txtPurchase1800.InnerText = dr.Item("Purchase1800").ToString()
                txtBSSHW.InnerText = dr.Item("BSSHW").ToString()
                txtBSSCode.InnerText = dr.Item("BSSCode").ToString()
                txtQty.InnerText = dr.Item("Qty").ToString()
                txtAntName.InnerText = dr.Item("AntennaName").ToString()
                txtAntennaQty.InnerText = dr.Item("AntennaQty").ToString()
                txtFeederLength.InnerText = dr.Item("FeederLen").ToString()
                txtFeederType.InnerText = dr.Item("FeederType").ToString()
                txtFeederQty.InnerText = dr.Item("FeederQty").ToString()
                txtValue1.Value = Format(Val(dr.Item("Value1").ToString()), "#,###.00")
                txtValue2.Value = Format(Val(dr.Item("Value2").ToString()), "#,####.00")

            End If
        End If
    End Sub

    Protected Sub btnSave_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        objEMOMD.SiteNo = Session("SiteNo")
        objEMOMD.Value1 = txtValue1.Value.Replace("'", "''")
        objEMOMD.Value2 = txtValue2.Value.Replace("'", "''")
        Dim i As Integer = objBL.uspUpdateMomDetailValues(objEMOMD)
        If i = 1 Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Data Saved Sucessfully.');", True)
        End If
        Dim j As Integer = objBL.uspUpdateMomDetails(Session("MOMId"), "I")
        'BOcommon.result(objBL.uspUpdateMomDetailValues(objEMOMD), True, "frmChangeRequest.aspx", "MOMD_ID", Constants._UPDATE)
        Dim URL As String
        Dim U As String = "U"
        URL = "frmPendingforCostImpact.aspx?id=" & Session("MOMId") & "&Type=" & U

        Response.Redirect(URL)
    End Sub
End Class
