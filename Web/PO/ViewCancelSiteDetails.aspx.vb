Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class ViewCancelSiteDetails
    Inherits System.Web.UI.Page
    Dim objbo As New BOPODetails
    Dim objdo As New ETPODetails
    Dim dt As DataTable
    Dim cst As New Common.Constants
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            If Not Request.QueryString("Sno") Is Nothing Then
                bindData()
            End If
        End If
    End Sub
    Sub bindData()
        dt = objbo.uspSiteDetails(Request.QueryString("Sno"))
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                If IsDBNull(dt.Rows(0).Item("mom_id")) Then
                    tblMom.Visible = False
                End If
                lblPONo.InnerText = .Item("PONo").ToString
                txtSiteno.InnerText = .Item("SiteNo").ToString
                txtSiteName.InnerText = .Item("site_name").ToString
                txtWPKGId.InnerText = .Item("WorkPkgId").ToString
                txtFldType.InnerText = .Item("FldType").ToString
                txtDesc.InnerText = .Item("Description").ToString
                txtBandType.InnerText = .Item("Band_type").ToString
                txtBand.InnerText = .Item("Band").ToString
                txtConfig.InnerText = .Item("Config").ToString
                txtP900.InnerText = .Item("Purchase900").ToString
                txtP1800.InnerText = .Item("Purchase1800").ToString
                txtHW.InnerText = .Item("BSSHW").ToString
                txtCode.InnerText = .Item("BSSCode").ToString
                txtAntName.InnerText = .Item("AntennaName").ToString
                txtAntQty.InnerText = .Item("AntennaQty").ToString

                txtFedLen.InnerText = .Item("FeederLen").ToString
                txtFedType.InnerText = .Item("FeederType").ToString
                txtFedQty.InnerText = .Item("FeederQty").ToString
                txtValue1.InnerText = Format(Val(.Item("Value1").ToString), "###,#.00")
                txtValue2.InnerText = Format(Val(.Item("Value2").ToString), "###,#.00")
                txtWPName.InnerText = .Item("WorkPackageName").ToString
                If cst.formatDDMMYYYY(Format(CDate(.Item("ContractDate").ToString), "dd/MM/yyyy")) <> Constants._EmptyDate Then txtContractDt.InnerText = Format(CDate(.Item("ContractDate").ToString), "dd/MM/yyyy")
                txtmfldtype.InnerText = .Item("mFldType").ToString
                txtmdesc.InnerText = .Item("mDescription").ToString
                txtmbandtype.InnerText = .Item("mBand_type").ToString
                txtmband.InnerText = .Item("mBand").ToString
                txtmConfig.InnerText = .Item("mConfig").ToString
                txtmp900.InnerText = .Item("mPurchase900").ToString
                txtmp1800.InnerText = .Item("mPurchase1800").ToString
                txtmhw.InnerText = .Item("mBSSHW").ToString
                txtmcode.InnerText = .Item("mBSSCode").ToString
                txtmantname.InnerText = .Item("mAntennaName").ToString
                txtmantqty.InnerText = .Item("mAntennaQty").ToString
                txtmfedlen.InnerText = .Item("mFeederLen").ToString
                txtmfedtype.InnerText = .Item("mFeederType").ToString
                txtmfedqty.InnerText = .Item("mFeederQty").ToString
                txtmvalue1.InnerText = Format(Val(.Item("mValue1").ToString), "###,#.00")
                txtmvalue2.InnerText = Format(Val(.Item("mValue2").ToString), "###,#.00")
                txtephaseti.InnerText = .Item("phaseti").ToString
                txtetisubcon.InnerText = .Item("tisubcon").ToString
                ' If cst.formatDDMMYYYY(Format(CDate(.Item("siteintegration").ToString), "dd/MM/yyyy")) <> Constants._EmptyDate Then 
                txteSiteIntegration.InnerText = .Item("siteintegration").ToString
                txteSiteAcpOnAir.InnerText = .Item("siteacponair").ToString
                txteSiteAcpOnBast.InnerText = .Item("siteacponbast").ToString
                If txteSiteIntegration.InnerText = "01/01/1900" Then txteSiteIntegration.InnerText = ""
                If txteSiteAcpOnAir.InnerText = "01/01/1900" Then txteSiteAcpOnAir.InnerText = ""
                If txteSiteAcpOnBast.InnerText = "01/01/1900" Then txteSiteAcpOnBast.InnerText = ""
                'If cst.formatDDMMYYYY(Format(CDate(.Item("siteacponair").ToString), "dd/MM/yyyy")) <> Constants._EmptyDate Then txteSiteAcpOnAir.InnerText = Format(CDate(.Item("siteacponair").ToString), "dd/MM/yyyy")
                'If cst.formatDDMMYYYY(Format(CDate(.Item("siteacponbast").ToString), "dd/MM/yyyy")) <> Constants._EmptyDate Then txteSiteAcpOnBast.InnerText = Format(CDate(.Item("siteacponbast").ToString), "dd/MM/yyyy")
                txtePackageType.InnerText = .Item("packagetype").ToString
                txtePackageName.InnerText = .Item("packagename").ToString
                txtePackageStatus.InnerText = .Item("packagestatus").ToString
                txteCustomerPONO.InnerText = .Item("custpono").ToString
                txteCustomerPORecDate.InnerText = .Item("custporecdt").ToString
                txtRemarks.InnerText = .Item("remarks").ToString
                txteRemarks.InnerText = .Item("mremarks").ToString
                txtmwpname.InnerText = .Item("mworkpkgname").ToString
                txteWorkPackageID.InnerText = .Item("eworkpkgid").ToString
                txtmwpkgid.InnerText = .Item("mworkpkgid").ToString
            End With
        End If
    End Sub
End Class
