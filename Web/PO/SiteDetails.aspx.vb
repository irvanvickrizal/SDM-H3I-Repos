Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class PO_SiteDetails
    Inherits System.Web.UI.Page
    Dim objbo As New BOPODetails
    Dim objdo As New ETPODetails
    Dim dt As DataTable
    Dim cst As New Common.Constants
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnCancel.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure to Cancel the site')")
        If Page.IsPostBack = False Then
            If Not Request.QueryString("Sno") Is Nothing Then
                bindData()
                Panel7.Visible = False
            End If
            If Request.QueryString("TT") = "D" Then
                Panel8.Visible = False
                btnCancel.Visible = False
            End If
        End If
    End Sub
    Sub bindData()
        dt = objbo.uspGetPODetails(Request.QueryString("Sno"), Request.QueryString("TT"))
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                lblPONo.InnerText = .Item("PONo").ToString
                txtSiteNo.InnerText = .Item("SiteNo").ToString
                txtSiteName.InnerText = .Item("SiteName").ToString
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
            End With
        End If
    End Sub
    Protected Sub btnBack_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.ServerClick
        If Request.QueryString("TT") = "D" Then
            Response.Redirect("frmSiteReActivation.aspx")
        Else
            Response.Redirect("frmSiteCancellation.aspx?type=P")
        End If
    End Sub
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            If txtremarks.Text = "" Then
                Response.Write("<script>alert('Please enter Remarks')</script>")
            Else
                BOcommon.result(objbo.uspSiteCancel(Request.QueryString("Sno"), txtSiteNo.InnerText, "Cancel", txtremarks.Text), True, "frmSiteCancellation.aspx?type=P", "", Constants._CANCELLED)
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
