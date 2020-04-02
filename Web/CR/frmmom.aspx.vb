Imports Entities
Imports BusinessLogic
Imports Common
Imports System.Data
Partial Class CR_frmmom
    Inherits System.Web.UI.Page
    Dim dtUser As New DataTable, dtSites As New DataTable, dtOthers As New DataTable
    Dim objBo As New BOMOM
    Dim objdo As New ETMomUsers
    Private objDLL As New BODDLs
    Dim objBLPO As New BOPODetails
    Private objDoHead As New ETMOMHead
    Private objEMOMD As New ETMOMDetails
    Private objBLMOM As New BOMOM
    Dim objdoUsers As New ETMomUsers
    Private objMOMO As New ETMOMOthers

    Dim cst As New Common.Constants
    Dim objUtil As New DBUtil
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("User_Name") = "sysadmin"
        If Not Page.IsPostBack Then
            btnSiteCreateFDate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtMomDate,'dd/mm/yyyy');return false;")
            btnAddSites.Attributes.Add("onclick", "javascript:return CheckADDSites();")
            BtnSubmit.Attributes.Add("onclick", "javascript:return CheckMOMDetails();")
            ddlSite.Attributes.Add("onclick", "javascript:return OnChangedSites();")
            objDLL.fillDDL(ddlPONo, "PoNo", True, Constants._DDL_Default_Select)
            objDLL.fillRBL(rblType, "CODLookup", 1)
            tblSiteDetails.Style.Add("display", "none")
            If Request.QueryString("Type") <> Nothing Then
                If Request.QueryString("id") <> Nothing Then
                    GetMOMHead(Request.QueryString("id"))
                    GetUserDetails(Request.QueryString("id"))
                    GetOthersDetails(Request.QueryString("id"))
                    GetMOMDetails(Request.QueryString("id"))
                End If
            End If
        End If
        FillAttendees()
        ddlbandtype.Attributes.Add("onchange", "return OnChangedBandType();")
        ddlband.Attributes.Add("onchange", "return OnChangedBand();")
        ddlconfig.Attributes.Add("onchange", "return OnChangedConfig();")
        If (GrdPo.Rows.Count <= 0) Then
            tdGenerate.Style.Add("display", "none")
        End If

    End Sub
    Sub FillAttendees()
        If (hdnUsersId.Value <> "") Then
            CreateNewRow()
        End If
    End Sub
    Public Sub CreateNewRow()
        ' If grdUsers.Rows.Count = 0 Then
        dtUser.Columns.Add("UsrName", Type.GetType("System.String"))
        dtUser.Columns.Add("UsrType", Type.GetType("System.String"))
        dtUser.Columns.Add("UsrEmail", Type.GetType("System.String"))
        dtUser.Columns.Add("Usr_Id", Type.GetType("System.Int32"))
        dtUser.Columns.Add("Sno", Type.GetType("System.Int32"))
        ' End If
        For intCount As Integer = 0 To grdUsers.Rows.Count - 1
            Dim itxtSno As HiddenField = CType(grdUsers.Rows(intCount).FindControl("hdSno"), HiddenField)

            Dim ihduserId As HiddenField = CType(grdUsers.Rows(intCount).FindControl("hduserId"), HiddenField)
            Dim ihdUserName As HiddenField = CType(grdUsers.Rows(intCount).FindControl("hdUserName"), HiddenField)

            Dim dr As DataRow = dtUser.NewRow()
            dr(0) = ihdUserName.Value
            dr(1) = grdUsers.Rows(intCount).Cells(2).Text
            dr(2) = grdUsers.Rows(intCount).Cells(3).Text
            dr(3) = Convert.ToInt32(ihduserId.Value)
            dr(4) = Convert.ToInt32(itxtSno.Value)
            dtUser.Rows.InsertAt(dr, 0)
        Next
        Dim dtNewUser As DataTable = objBo.uspGetAttendes(hdnUsersId.Value)
        For intCount As Integer = 0 To dtNewUser.Rows.Count - 1


            Dim dr1 As DataRow = dtUser.NewRow()
            dr1(0) = dtNewUser.Rows(intCount)("Attendesname")
            dr1(1) = dtNewUser.Rows(intCount)("UsrType")
            dr1(2) = dtNewUser.Rows(intCount)("Email")
            dr1(3) = Convert.ToInt32(dtNewUser.Rows(intCount)("Usr_Id"))
            If ViewState("SNO") = Nothing Then
                ViewState("SNO") = 0
            End If
            dr1(4) = Convert.ToInt32(ViewState("SNO")) + 1
            ViewState("SNO") = Convert.ToInt32(ViewState("SNO")) + 1

            If (DistinctUser(dtUser, dtNewUser.Rows(intCount)("Usr_Id")) = 1) Then
                dtUser.Rows.InsertAt(dr1, 0)
            End If
        Next
        hdnUsersId.Value = ""

        grdUsers.DataSource = dtUser
        Try
            grdUsers.DataBind()
        Catch ex As Exception
            grdUsers.PageIndex = 0
        End Try
        ViewState("dtGrid") = dtUser
    End Sub
    Protected Sub grdUsers_PageIndexChanging1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        grdUsers.PageIndex = e.NewPageIndex
        dtUser = CType(ViewState("dtGrid"), DataTable)
        grdUsers.DataSource = dtUser
        grdUsers.DataBind()

    End Sub
    Protected Sub grdUsers_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbl As Label = CType(e.Row.FindControl("lblno"), Label)
            lbl.Text = (grdUsers.PageIndex * grdUsers.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"

            Dim ihduserid As HiddenField = CType(e.Row.FindControl("hduserId"), HiddenField)

            If ihduserid.Value > 0 Then
                Dim url As String, strName As String = ""
                url = "../Usr/frmUserView.aspx?id=" & ihduserid.Value
                strName = e.Row.Cells(1).Text
                e.Row.Cells(1).Text = "<a href=# onclick=""window.open('" & url & "','','width=650,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');"">" & strName & "</a>"
            End If
        End If
    End Sub
    Public Function DistinctAddUser(ByVal dt As DataTable, ByVal strDesc As String) As Integer
        Dim intReturn As Integer = 0
        If dt.Rows.Count <> 0 Then
            Dim intCount As Integer
            For intCount = 0 To dt.Rows.Count - 1 Step intCount + 1
                If dt.Rows(intCount)("Usr_Id").ToString() = strDesc Then
                    intReturn = intCount
                    Exit For
                Else
                    intReturn = 100000000
                End If

            Next
        Else
            intReturn = 100000000
        End If
        Return intReturn
    End Function
    Public Function DistinctUser(ByVal dt As DataTable, ByVal strDesc As String) As Integer
        Dim intReturn As Integer = 0
        If dt.Rows.Count <> 0 Then
            Dim intCount As Integer
            For intCount = 0 To dt.Rows.Count - 1 Step intCount + 1
                If dt.Rows(intCount)("Usr_Id").ToString() = strDesc Then
                    intReturn = 0
                    Exit For
                Else
                    intReturn = 1
                End If

            Next
        Else
            intReturn = 1
        End If
        Return intReturn
    End Function
    Protected Sub grdUsers_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)
        dtUser = CType(ViewState("dtGrid"), DataTable)
        Dim ihduserId As HiddenField = CType(grdUsers.Rows(e.RowIndex).FindControl("hduserId"), HiddenField)
        Dim intCartId As Integer = DistinctAddUser(dtUser, Convert.ToInt32(ihduserId.Value))

        If intCartId <> 100000000 Then
            dtUser.Rows.RemoveAt(intCartId)
            dtUser.DefaultView.Sort = "Sno asc"
            grdUsers.DataSource = dtUser
            grdUsers.DataBind()
            ViewState("dtGrid") = dtUser
        End If
    End Sub
    Protected Sub ddlPONo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ddlSite.Items.Clear()
        Dim strPO As String = ""
        strPO = ddlPONo.SelectedValue.ToString
        'objDLL.fillDDL(ddlSite, "MOMPOSiteNo", Session("Mom_Id"), strPO.ToString, True, Constants._DDL_Default_Select)
        objDLL.fillDDL(ddlSite, "MOMPositeNo", ddlPONo.SelectedItem.Value, True, Constants._DDL_Default_Select)

    End Sub
    Protected Sub ddlSite_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        BindDetails()
        tblSiteDetails.Style.Add("display", "")
        tdGenerate.Style.Add("display", "none")

        txtSiteNo.Attributes.Remove("readonly")
        txtSiteName.Attributes.Remove("readonly")
        txtAntennaQty.Attributes.Remove("readonly")
        txtAntName.Attributes.Remove("readonly")
        txtBand.Attributes.Remove("readonly")
        txtBandType1.Attributes.Remove("readonly")
        txtBSSCode.Attributes.Remove("readonly")
        txtBSSHW.Attributes.Remove("readonly")
        txtconfig.Attributes.Remove("readonly")
        txtDesc.Attributes.Remove("readonly")
        txtFeederLength.Attributes.Remove("readonly")
        txtFeederQty.Attributes.Remove("readonly")
        txtFeederType.Attributes.Remove("readonly")
        txtFldType.Attributes.Remove("readonly")
        txtPoName.Attributes.Remove("readonly")
        txtPoType.Attributes.Remove("readonly")
        txtPurchase1800.Attributes.Remove("readonly")
        txtPurchase900.Attributes.Remove("readonly")
        txtQty.Attributes.Remove("readonly")
        txtVendor.Attributes.Remove("readonly")
        txtWorkPkgID.Attributes.Remove("readonly")
        txtWorkPkgName.Attributes.Remove("readonly")
        ddlAntennaName.Enabled = True
        ddlband.Enabled = True
        ddlbandtype.Enabled = True
        ddlconfig.Enabled = True
        ddlFeederLength.Enabled = True
        ddlFeederType.Enabled = True

        If rblType.SelectedItem.Text.ToLower = "change configuration" Then
            grdOthers.Visible = True
            txtPoName.Attributes.Add("readonly", "readonly")
            txtPoType.Attributes.Add("readonly", "readonly")
            txtVendor.Attributes.Add("readonly", "readonly")
            txtWorkPkgID.Attributes.Add("readonly", "readonly")
            txtWorkPkgID.Attributes.Add("readonly", "readonly")
            txtSiteNo.Attributes.Add("readonly", "readonly")
            txtSiteName.Attributes.Add("readonly", "readonly")
            CreateOthersNewRow()
        ElseIf rblType.SelectedItem.Text.ToLower = "cancel" Then
            grdOthers.Visible = False
            txtSiteNo.Attributes.Add("readonly", "readonly")
            txtSiteName.Attributes.Add("readonly", "readonly")
            txtAntennaQty.Attributes.Add("readonly", "readonly")
            txtAntName.Attributes.Add("readonly", "readonly")
            txtBand.Attributes.Add("readonly", "readonly")
            txtBandType1.Attributes.Add("readonly", "readonly")
            txtBSSCode.Attributes.Add("readonly", "readonly")
            txtBSSHW.Attributes.Add("readonly", "readonly")
            txtconfig.Attributes.Add("readonly", "readonly")
            txtDesc.Attributes.Add("readonly", "readonly")
            txtFeederLength.Attributes.Add("readonly", "readonly")
            txtFeederQty.Attributes.Add("readonly", "readonly")
            txtFeederType.Attributes.Add("readonly", "readonly")
            txtFldType.Attributes.Add("readonly", "readonly")
            txtPoName.Attributes.Add("readonly", "readonly")
            txtPoType.Attributes.Add("readonly", "readonly")
            txtPurchase1800.Attributes.Add("readonly", "readonly")
            txtPurchase900.Attributes.Add("readonly", "readonly")
            txtQty.Attributes.Add("readonly", "readonly")
            txtVendor.Attributes.Add("readonly", "readonly")
            txtWorkPkgID.Attributes.Add("readonly", "readonly")
            txtWorkPkgName.Attributes.Add("readonly", "readonly")
            ddlband.Enabled = False
            ddlAntennaName.Enabled = False
            ddlband.Enabled = False
            ddlbandtype.Enabled = False
            ddlconfig.Enabled = False
            ddlFeederLength.Enabled = False
            ddlFeederType.Enabled = False
        Else
            grdOthers.Visible = False
            txtPoName.Attributes.Add("readonly", "readonly")
            txtPoType.Attributes.Add("readonly", "readonly")
            txtVendor.Attributes.Add("readonly", "readonly")
        End If

    End Sub
    Sub BindDetails()

        objDLL.fillDDL(ddlbandtype, "CODLookup", Constants._PO_Band_Type, True, Constants._DDL_Default_Select)
        objDLL.fillDDL(ddlband, "CODLookup", Constants._PO_Band, True, Constants._DDL_Default_Select)
        objDLL.fillDDL(ddlconfig, "CODLookup", Constants._PO_Configuration, True, Constants._DDL_Default_Select)
        objDLL.fillDDL(ddlAntennaName, "CODLookup", Constants._AntennaName, True, Constants._DDL_Default_Select)
        objDLL.fillDDL(ddlFeederLength, "CODLookup", Constants._FeederLength, True, Constants._DDL_Default_Select)
        objDLL.fillDDL(ddlFeederType, "CODLookup", Constants._FeederType, True, Constants._DDL_Default_Select)

        Dim ddlll As String() = ddlSite.SelectedItem.Text.Split("-")
        Dim dl As String() = ddlll(0).Split("-"), dt As DataTable
        dl(0) = ddlll(0)
        dt = objBLPO.uspGetMOMconfig(ddlPONo.SelectedValue, ddlll(0), ddlll(1), 0)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            DataList1.DataSource = dt
            DataList1.DataBind()
            'Change Request
            txtSiteNo.Value = dr.Item("SiteNo").ToString()
            txtSiteName.Value = dr.Item("SiteName").ToString()
            If (rblType.SelectedItem.Text.ToLower() = "remapping") Then
                txtWorkPkgID.Value = Nothing
                txtWorkPkgName.Value = Nothing
            Else
                txtWorkPkgID.Value = dr.Item("WorkPkgId").ToString()
                txtWorkPkgName.Value = dr.Item("workPKGName").ToString()
            End If

            txtFldType.Value = dr.Item("Scope").ToString()
            txtDesc.Value = dr.Item("Description").ToString()
            'txtBandType.Value = dr.Item("Band_Type").ToString()
            'txtBand.Value = dr.Item("Band").ToString()
            'txtConfig.Value = dr.Item("Config").ToString()
            ddlbandtype.SelectedItem.Text = dr.Item("Band_Type").ToString()
            ddlband.SelectedItem.Text = dr.Item("Band").ToString()
            ddlconfig.SelectedItem.Text = dr.Item("Config").ToString()
            txtPurchase900.Value = dr.Item("Purchase900").ToString()
            txtPurchase1800.Value = dr.Item("Purchase1800").ToString()
            txtBSSHW.Value = dr.Item("BSSHW").ToString()
            txtBSSCode.Value = dr.Item("BSSCode").ToString()
            txtQty.Value = dr.Item("Qty").ToString()
            ddlAntennaName.SelectedItem.Text = dr.Item("AntennaName").ToString()
            txtAntennaQty.Value = dr.Item("AntennaQty").ToString()
            ddlFeederLength.SelectedItem.Text = dr.Item("FeederLen").ToString()
            ddlFeederType.SelectedItem.Text = dr.Item("FeederType").ToString()
            txtFeederQty.Value = dr.Item("FeederQty").ToString()
            txtPoType.Value = dr.Item("poType").ToString()
            txtPoName.Value = dr.Item("poname").ToString()
            txtVendor.Value = dr.Item("vendor").ToString()

            'txtValue1.Value = Format(Val(dr.Item("Value1").ToString()), "#,###.00")
            'txtValue2.Value = Format(Val(dr.Item("Value2").ToString()), "#,###.00")

        End If
    End Sub
    Protected Sub btnAddSites_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        CreateNewRowSites()
        CreateOthers()
        txtSubject.Value = ""
        rblType.ClearSelection()
        ddlSite.ClearSelection()
        txtRemarks.Value = ""
        chkCost.Checked = False
        objDLL.fillDDL(ddlPONo, "PoNo", True, Constants._DDL_Default_Select)
        tblSiteDetails.Style.Add("display", "none")
        tdGenerate.Style.Add("display", "")
    End Sub
    Public Sub CreateNewRowSites()
        dtSites.Columns.Add("PoNo", Type.GetType("System.String"))
        dtSites.Columns.Add("Scope", Type.GetType("System.String"))
        dtSites.Columns.Add("siteno", Type.GetType("System.String"))
        dtSites.Columns.Add("sitename", Type.GetType("System.String"))
        dtSites.Columns.Add("workpkgid", Type.GetType("System.String"))
        dtSites.Columns.Add("workpackagename", Type.GetType("System.String"))
        dtSites.Columns.Add("fldtype", Type.GetType("System.String"))
        dtSites.Columns.Add("description", Type.GetType("System.String"))
        dtSites.Columns.Add("band_type", Type.GetType("System.String"))
        dtSites.Columns.Add("band", Type.GetType("System.String"))
        dtSites.Columns.Add("band_type_Other", Type.GetType("System.String"))
        dtSites.Columns.Add("band_Other", Type.GetType("System.String"))
        dtSites.Columns.Add("config", Type.GetType("System.String"))
        dtSites.Columns.Add("config_Other", Type.GetType("System.String"))
        dtSites.Columns.Add("purchase900", Type.GetType("System.String"))
        dtSites.Columns.Add("purchase1800", Type.GetType("System.String"))
        dtSites.Columns.Add("bsshw", Type.GetType("System.String"))
        dtSites.Columns.Add("bsscode", Type.GetType("System.String"))
        dtSites.Columns.Add("qty", Type.GetType("System.String"))
        dtSites.Columns.Add("antennaname", Type.GetType("System.String"))
        dtSites.Columns.Add("antennaname_Other", Type.GetType("System.String"))
        dtSites.Columns.Add("antennaqty", Type.GetType("System.String"))
        dtSites.Columns.Add("feederlen", Type.GetType("System.String"))
        dtSites.Columns.Add("feederlen_Other", Type.GetType("System.String"))
        dtSites.Columns.Add("feedertype", Type.GetType("System.String"))
        dtSites.Columns.Add("feedertype_Other", Type.GetType("System.String"))
        dtSites.Columns.Add("feederqty", Type.GetType("System.String"))
        dtSites.Columns.Add("potype", Type.GetType("System.String"))
        dtSites.Columns.Add("poname", Type.GetType("System.String"))
        dtSites.Columns.Add("vendor", Type.GetType("System.String"))
        dtSites.Columns.Add("LKPDesc", Type.GetType("System.String"))
        dtSites.Columns.Add("Sno", Type.GetType("System.Int32"))
        dtSites.Columns.Add("Momd_Id", Type.GetType("System.Int32"))
        dtSites.Columns.Add("Remarks", Type.GetType("System.String"))
        dtSites.Columns.Add("IsCostImp", Type.GetType("System.String"))
        dtSites.Columns.Add("OldSiteNo", Type.GetType("System.String"))
        dtSites.Columns.Add("LKPDescNEW", Type.GetType("System.String"))
        dtSites.Columns.Add("subject", Type.GetType("System.String"))
        ' End If
        For intCount As Integer = 0 To GrdPo.Rows.Count - 1
            Dim ihdSno As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdSno"), HiddenField)
            Dim ihdMomd_Id As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdMomd_Id"), HiddenField)
            Dim ihdvendor As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdvendor"), HiddenField)
            Dim ihdPoName As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdPoName"), HiddenField)
            Dim ihdPotype As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdPotype"), HiddenField)
            Dim ihdfeederqty As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdfeederqty"), HiddenField)
            Dim ihdfeedertype_Other As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdfeedertype_Other"), HiddenField)
            Dim ihdfeedertype As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdfeedertype"), HiddenField)
            Dim ihdfeederlen_Other As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdfeederlen_Other"), HiddenField)
            Dim ihdfeederlen As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdfeederlen"), HiddenField)
            Dim ihdantennaqty As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdantennaqty"), HiddenField)
            Dim ihdantennaname_Other As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdantennaname_Other"), HiddenField)
            Dim ihdantennaname As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdantennaname"), HiddenField)
            Dim ihdqty As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdqty"), HiddenField)
            Dim ihdbsscode As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdbsscode"), HiddenField)
            Dim ihdbsshw As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdbsshw"), HiddenField)
            Dim ihdpurchase1800 As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdpurchase1800"), HiddenField)
            Dim ihdpurchase900 As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdpurchase900"), HiddenField)
            Dim ihdconfig_Other As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdconfig_Other"), HiddenField)
            Dim ihdconfig As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdconfig"), HiddenField)
            Dim ihdband_Other As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdband_Other"), HiddenField)
            Dim ihdband_type_Other As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdband_type_Other"), HiddenField)
            Dim ihdband As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdband"), HiddenField)
            Dim ihdband_type As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdband_type"), HiddenField)
            Dim ihddescription As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hddescription"), HiddenField)
            Dim ihdfldtype As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdfldtype"), HiddenField)
            Dim ihdworkpackagename As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdworkpackagename"), HiddenField)
            Dim ihdsitename As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdsitename"), HiddenField)
            Dim ihdScope As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdScope"), HiddenField)
            Dim ihdRemarks As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdRemarks"), HiddenField)
            Dim ihdIsCostImp As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdIsCostImp"), HiddenField)
            Dim ihdOldSiteNo As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdOldSiteNo"), HiddenField)
            Dim ihdLKPDesc As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdLKPDesc"), HiddenField)
            Dim ihdsubject As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdsubject"), HiddenField)
            Dim dr As DataRow = dtSites.NewRow()
            dr(0) = GrdPo.Rows(intCount).Cells(2).Text
            dr(1) = ihdScope.Value
            dr(2) = GrdPo.Rows(intCount).Cells(1).Text
            dr(3) = ihdsitename.Value
            If GrdPo.Rows(intCount).Cells(3).Text.Trim = Nothing Then
                dr(4) = Nothing
            Else
                dr(4) = GrdPo.Rows(intCount).Cells(3).Text
            End If
            dr(5) = ihdworkpackagename.Value
            dr(6) = ihdfldtype.Value
            dr(7) = ihddescription.Value
            dr(8) = ihdband_type.Value
            dr(9) = ihdband.Value
            dr(10) = ihdband_type_Other.Value
            dr(11) = ihdband_Other.Value
            dr(12) = ihdconfig.Value
            dr(13) = ihdconfig_Other.Value
            dr(14) = ihdpurchase900.Value
            dr(15) = ihdpurchase1800.Value
            dr(16) = ihdbsshw.Value
            dr(17) = ihdbsscode.Value
            dr(18) = ihdqty.Value
            dr(19) = ihdantennaname.Value
            dr(20) = ihdantennaname_Other.Value
            dr(21) = ihdantennaqty.Value
            dr(22) = ihdfeederlen.Value
            dr(23) = ihdfeederlen_Other.Value
            dr(24) = ihdfeedertype.Value
            dr(25) = ihdfeedertype_Other.Value
            dr(26) = ihdfeederqty.Value
            dr(27) = ihdPotype.Value
            dr(28) = ihdPoName.Value
            dr(29) = ihdvendor.Value
            dr(30) = ihdLKPDesc.Value
            dr(31) = ihdSno.Value
            dr(32) = ihdMomd_Id.Value
            dr(33) = ihdRemarks.Value
            dr(34) = ihdIsCostImp.Value
            dr(35) = ihdOldSiteNo.Value
            dr(36) = GrdPo.Rows(intCount).Cells(4).Text
            dr(37) = ihdsubject.Value
            dtSites.Rows.InsertAt(dr, 0)
        Next
        Dim ddlll As String() = ddlSite.SelectedItem.Text.Split("-")
        Dim dr1 As DataRow = dtSites.NewRow()
        dr1(0) = ddlPONo.SelectedItem.Value
        dr1(1) = ddlll(1)
        dr1(2) = txtSiteNo.Value
        dr1(3) = txtSiteName.Value
        If txtWorkPkgID.Value = Nothing Then
            dr1(4) = Nothing

        Else
            dr1(4) = txtWorkPkgID.Value

        End If
        dr1(5) = txtWorkPkgName.Value
        dr1(6) = txtFldType.Value
        dr1(7) = txtDesc.Value
        dr1(8) = ddlbandtype.SelectedItem.Text
        dr1(9) = ddlband.SelectedItem.Text
        dr1(10) = txtBandType1.Value
        dr1(11) = txtBand.Value
        dr1(12) = ddlconfig.SelectedItem.Text
        dr1(13) = txtconfig.Value
        dr1(14) = txtPurchase900.Value
        dr1(15) = txtPurchase1800.Value
        dr1(16) = txtBSSHW.Value
        dr1(17) = txtBSSCode.Value
        dr1(18) = txtQty.Value
        dr1(19) = ddlAntennaName.SelectedItem.Text
        dr1(20) = txtAntName.Value
        dr1(21) = txtAntennaQty.Value
        dr1(22) = ddlFeederLength.SelectedItem.Text
        dr1(23) = txtFeederLength.Value
        dr1(24) = ddlFeederType.SelectedItem.Text
        dr1(25) = txtFeederType.Value
        dr1(26) = txtFeederQty.Value
        dr1(27) = txtPoType.Value
        dr1(28) = txtPoName.Value
        dr1(29) = txtVendor.Value
        dr1(30) = rblType.SelectedItem.Value
        If ViewState("SNOSite") = Nothing Then
            ViewState("SNOSite") = 0
        End If
        dr1(31) = Convert.ToInt32(ViewState("SNOSite")) + 1
        ViewState("SNOSite") = Convert.ToInt32(ViewState("SNOSite")) + 1
        dr1(32) = 0
        dr1(33) = txtRemarks.Value
        If (chkCost.Checked) Then
            dr1(34) = 1
        Else
            dr1(34) = 0
        End If
        dr1(35) = ddlll(0)
        dr1(36) = rblType.SelectedItem.Text
        dr1(37) = txtSubject.Value
        If (DistinctSites(dtUser, ddlPONo.SelectedItem.Text, txtSiteNo.Value, ddlll(1)) = 1) Then
            dtSites.Rows.InsertAt(dr1, 0)
        End If
        GrdPo.Columns(5).Visible = True
        GrdPo.DataSource = dtSites
        Try
            GrdPo.DataBind()
            'grdpo.Columns(5).Visible = False
        Catch ex As Exception
            GrdPo.PageIndex = 0
        End Try
        ViewState("dtGridSites") = dtSites
    End Sub
    Public Function DistinctAddSites(ByVal dt As DataTable, ByVal strPoNo As String, ByVal strScope As String, ByVal strSiteno As String) As Integer
        Dim intReturn As Integer = 0
        If dt.Rows.Count <> 0 Then
            Dim intCount As Integer
            For intCount = 0 To dt.Rows.Count - 1 Step intCount + 1
                If dt.Rows(intCount)("PoNo").ToString() = strPoNo And dt.Rows(intCount)("Scope").ToString() = strScope And dt.Rows(intCount)("siteno").ToString() = strSiteno Then
                    intReturn = intCount
                    Exit For
                Else
                    intReturn = 100000000
                End If

            Next
        Else
            intReturn = 100000000
        End If
        Return intReturn
    End Function
    Public Function DistinctSites(ByVal dt As DataTable, ByVal strPoNo As String, ByVal strScope As String, ByVal strSiteno As String) As Integer
        Dim intReturn As Integer = 0
        If dt.Rows.Count <> 0 Then
            Dim intCount As Integer
            For intCount = 0 To dt.Rows.Count - 1 Step intCount + 1
                If dt.Rows(intCount)("PoNo").ToString() = strPoNo And dt.Rows(intCount)("Scope").ToString() = strScope And dt.Rows(intCount)("siteno").ToString() = strSiteno Then
                    intReturn = 0
                    Exit For
                Else
                    intReturn = 1
                End If

            Next
        Else
            intReturn = 1
        End If
        Return intReturn
    End Function
    Protected Sub GrdPo_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        GrdPo.PageIndex = e.NewPageIndex
        dtSites = CType(ViewState("dtGridSites"), DataTable)
        GrdPo.Columns(5).Visible = True
        GrdPo.DataSource = dtSites
        GrdPo.DataBind()
        ' grdpo.Columns(5).Visible = False
    End Sub
    Protected Sub GrdPo_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbl As Label = CType(e.Row.FindControl("lblno"), Label)
            lbl.Text = (GrdPo.PageIndex * GrdPo.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End If
    End Sub
    Protected Sub GrdPo_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)
        dtSites = CType(ViewState("dtGridSites"), DataTable)
        Dim ihdScope As HiddenField = CType(GrdPo.Rows(e.RowIndex).FindControl("hdScope"), HiddenField)
        Dim intCartId As Integer = DistinctAddSites(dtSites, GrdPo.Rows(e.RowIndex).Cells(2).Text, ihdScope.Value, GrdPo.Rows(e.RowIndex).Cells(1).Text)

        If intCartId <> 100000000 Then
            dtSites.Rows.RemoveAt(intCartId)
            dtSites.DefaultView.Sort = "Sno asc"
            GrdPo.Columns(5).Visible = True
            GrdPo.DataSource = dtSites
            GrdPo.DataBind()
            'grdpo.Columns(5).Visible = False
            ViewState("dtGridSites") = dtSites
        End If
    End Sub
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        fillMomDetails()
        If (Request.QueryString("id") = Nothing) Then
            Dim intResult = objBLMOM.uspMOMHeadIU(objDoHead)
            If (intResult > 0) Then
                fillUserDetails(intResult)
                ADDSites(intResult)
                fillOthersDetails(intResult)
            End If
        Else
            objDoHead.MOM_ID = Request.QueryString("id")
            Dim intResult = objBLMOM.uspMOMHeadIU(objDoHead)
            objUtil.ExeQueryScalar("delete from momdetails where mom_id=" + Request.QueryString("id"))
            objUtil.ExeQueryScalar("delete from momparticipants where mom_id=" + Request.QueryString("id"))
            objUtil.ExeQueryScalar("delete from MOMOtherChange where mom_id=" + Request.QueryString("id"))
            fillUserDetails(Request.QueryString("id"))
            fillOthersDetails(Request.QueryString("id"))
            ADDSites(Request.QueryString("id"))
        End If
        Response.Redirect("frmMomList.aspx")
    End Sub
    Protected Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("frmMomList.aspx")
    End Sub
    Sub fillOthersDetails(ByVal MOMId As Integer)
        If Not ViewState("dtOthers") Is Nothing Then
            dtOthers = CType(ViewState("dtOthers"), DataTable)
            For intCount As Integer = 0 To dtOthers.Rows.Count - 1
                objMOMO.MOM_ID = MOMId
                objMOMO.SiteNo = dtOthers.Rows(intCount)("siteid")
                objMOMO.Scope = dtOthers.Rows(intCount)("scope")
                objMOMO.Pono = dtOthers.Rows(intCount)("pono")
                objMOMO.Input = dtOthers.Rows(intCount)("contents")
                objMOMO.AT.RStatus = Constants.STATUS_ACTIVE
                objMOMO.AT.LMBY = Session("User_Name")
                Dim ii As Integer = objBLMOM.uspMOMOtherIUNew(objMOMO)
            Next
        End If
    End Sub
    Sub ADDSites(ByVal MOMId As Integer)

        For intCount As Integer = 0 To GrdPo.Rows.Count - 1
            Dim ihdSno As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdSno"), HiddenField)
            Dim ihdMomd_Id As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdMomd_Id"), HiddenField)
            Dim ihdvendor As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdvendor"), HiddenField)
            Dim ihdPoName As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdPoName"), HiddenField)
            Dim ihdPotype As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdPotype"), HiddenField)
            Dim ihdfeederqty As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdfeederqty"), HiddenField)
            Dim ihdfeedertype_Other As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdfeedertype_Other"), HiddenField)
            Dim ihdfeedertype As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdfeedertype"), HiddenField)
            Dim ihdfeederlen_Other As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdfeederlen_Other"), HiddenField)
            Dim ihdfeederlen As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdfeederlen"), HiddenField)
            Dim ihdantennaqty As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdantennaqty"), HiddenField)
            Dim ihdantennaname_Other As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdantennaname_Other"), HiddenField)
            Dim ihdantennaname As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdantennaname"), HiddenField)
            Dim ihdqty As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdqty"), HiddenField)
            Dim ihdbsscode As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdbsscode"), HiddenField)
            Dim ihdbsshw As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdbsshw"), HiddenField)
            Dim ihdpurchase1800 As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdpurchase1800"), HiddenField)
            Dim ihdpurchase900 As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdpurchase900"), HiddenField)
            Dim ihdconfig_Other As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdconfig_Other"), HiddenField)
            Dim ihdconfig As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdconfig"), HiddenField)
            Dim ihdband_Other As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdband_Other"), HiddenField)
            Dim ihdband_type_Other As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdband_type_Other"), HiddenField)
            Dim ihdband As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdband"), HiddenField)
            Dim ihdband_type As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdband_type"), HiddenField)
            Dim ihddescription As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hddescription"), HiddenField)
            Dim ihdfldtype As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdfldtype"), HiddenField)
            Dim ihdworkpackagename As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdworkpackagename"), HiddenField)
            Dim ihdsitename As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdsitename"), HiddenField)
            Dim ihdScope As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdScope"), HiddenField)
            Dim ihdRemarks As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdRemarks"), HiddenField)
            Dim ihdIsCostImp As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdIsCostImp"), HiddenField)
            Dim ihdOldSiteNo As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdOldSiteNo"), HiddenField)
            Dim ihdLKPDesc As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdLKPDesc"), HiddenField)
            Dim ihdsubject As HiddenField = CType(GrdPo.Rows(intCount).FindControl("hdsubject"), HiddenField)
            objEMOMD.MOM_ID = MOMId 'Request.QueryString("mom")
            objEMOMD.PoNo = GrdPo.Rows(intCount).Cells(2).Text



            objEMOMD.SiteNo = GrdPo.Rows(intCount).Cells(1).Text
            objEMOMD.OldSiteNo = ihdOldSiteNo.Value
            objEMOMD.LKP_Type = ihdLKPDesc.Value
            objEMOMD.SDescription = ihddescription.Value
            objEMOMD.SiteName = ihdsitename.Value.Replace("'", "''")
            If GrdPo.Rows(intCount).Cells(3).Text.Trim() = Nothing Then
                objEMOMD.WorkPkgID = Nothing

            Else
                objEMOMD.WorkPkgID = GrdPo.Rows(intCount).Cells(3).Text.Trim()

            End If

            objEMOMD.WorkPkgName = ihdworkpackagename.Value.Replace("'", "''")
            objEMOMD.FldType = ihdfldtype.Value.Replace("'", "''")
            objEMOMD.Description = ihddescription.Value
            objEMOMD.BandOthers = ihdband_Other.Value.Replace("'", "''")
            objEMOMD.Band = ihdband.Value
            objEMOMD.BandTypeOthers = ihdband_type_Other.Value.Replace("'", "''")
            objEMOMD.BandType = ihdband_type.Value

            objEMOMD.ConfigOthers = ihdconfig_Other.Value.Replace("'", "''")
            objEMOMD.Config = ihdconfig.Value
            objEMOMD.Purchase900 = ihdpurchase900.Value.Replace("'", "''")
            objEMOMD.Purchase1800 = ihdpurchase1800.Value.Replace("'", "''")
            objEMOMD.BSSHW = ihdbsshw.Value.Replace("'", "''")
            objEMOMD.BSSCode = ihdbsscode.Value.Replace("'", "''")
            objEMOMD.Qty = ihdqty.Value
            objEMOMD.AntennaNameOthers = ihdantennaname_Other.Value.Replace("'", "''")
            objEMOMD.AntennaName = ihdantennaname.Value
            objEMOMD.AntennaQty = ihdantennaqty.Value.Replace("'", "''")
            objEMOMD.FeederLenthOthers = ihdfeederlen_Other.Value.Replace("'", "''")
            objEMOMD.FeederLen = ihdfeederlen.Value
            objEMOMD.FeederTypeOthers = ihdfeedertype_Other.Value.Replace("'", "''")
            objEMOMD.FeederType = ihdfeedertype.Value
            If ihdfeederqty.Value.Replace("'", "''") = "" Then
                objEMOMD.FeederQty = "0"

            Else
                objEMOMD.FeederQty = ihdfeederqty.Value.Replace("'", "''")

            End If

            objEMOMD.PoName = ihdPoName.Value.Replace("'", "''")
            objEMOMD.PoType = ihdPotype.Value
            objEMOMD.Vendor = ihdvendor.Value.Replace("'", "''")
            objEMOMD.subject = ihdsubject.Value.Replace("'", "''")
            If (ihdIsCostImp.Value) Then
                objEMOMD.IsCostImp = 1
            Else
                objEMOMD.IsCostImp = 0
            End If

            objEMOMD.Remarks = ihdRemarks.Value.Replace("'", "''")
            objEMOMD.AT.RStatus = Constants.STATUS_ACTIVE
            objEMOMD.AT.LMBY = Session("User_Name")
            objEMOMD.WorkPkgName = Trim(txtWorkPkgName.Value.Replace("'", "''"))
            objBLMOM.uspMOMDetailsNewIU(objEMOMD)
        Next
    End Sub
    Sub fillUserDetails(ByVal MOMId As Integer)
        dtUser = CType(ViewState("dtGrid"), DataTable)
        For intCount As Integer = 0 To dtUser.Rows.Count - 1
            objdoUsers.MOM_Id = MOMId
            objdoUsers.UsrType = dtUser.Rows(intCount)("UsrType")
            objdoUsers.AT.RStatus = Constants.STATUS_ACTIVE
            objdoUsers.UsrName = dtUser.Rows(intCount)("UsrName")
            objdoUsers.USR_Id = dtUser.Rows(intCount)("Usr_Id")
            objdoUsers.UsrEmail = dtUser.Rows(intCount)("UsrEmail")
            objdoUsers.AT.LMBY = Session("User_Name")
            objBo.uspMomParticipantsNEWIU(objdoUsers)
        Next

    End Sub
    Sub fillMomDetails()

        If txtMomDate.Value <> "" Then
            objDoHead.MOMDate = cst.formatdate(txtMomDate.Value)
        Else
            objDoHead.MOMDate = txtMomDate.Value.Replace("'", "''")
        End If
        objDoHead.Time = txtTime.Value.Replace("'", "''")
        objDoHead.Moderator = txtModerator.Value.Replace("'", "''")
        objDoHead.Location = txtLocation.Value.Replace("'", "''")
        objDoHead.MOMWriter = txtMOMWriter.Value.Replace("'", "''")
        objDoHead.Subject = txtArea.Value.Replace("'", "''")
        objDoHead.AT.RStatus = Constants.STATUS_ACTIVE
        objDoHead.AT.LMBY = Session("User_Name")
    End Sub
    Sub GetMOMHead(ByVal intMOMId As Integer)
        Dim MOMHead As DataTable = objBLMOM.uspGetMomHead(intMOMId)
        If MOMHead.Rows.Count > 0 Then
            txtModerator.Value = MOMHead.Rows(0)("moderator").ToString
            txtTime.Value = MOMHead.Rows(0)("times").ToString
            txtMomDate.Value = MOMHead.Rows(0)("MomDate").ToString
            txtLocation.Value = MOMHead.Rows(0)("Location")
            txtMOMWriter.Value = MOMHead.Rows(0)("MOMWriter")
            txtArea.Value = MOMHead.Rows(0)("subject")
        End If
    End Sub
    Sub GetOthersDetails(ByVal intMOMId As Integer)

        If ViewState("dtOthers") Is Nothing Then
            dtOthers.Columns.Add("pono", Type.GetType("System.String"))
            dtOthers.Columns.Add("siteid", Type.GetType("System.String"))
            dtOthers.Columns.Add("scope", Type.GetType("System.String"))
            dtOthers.Columns.Add("contents", Type.GetType("System.String"))
            dtOthers.Columns.Add("mom_Id", Type.GetType("System.Int32"))
            dtOthers.Columns.Add("Sno", Type.GetType("System.Int32"))
        Else
            dtOthers = CType(ViewState("dtOthers"), DataTable)
        End If
        Dim dtOthers1 As DataTable = objBLMOM.uspMOMOtherLNew(intMOMId.ToString)
        For intCount As Integer = 0 To dtOthers1.Rows.Count - 1


            Dim dr As DataRow = dtOthers.NewRow()
            dr(0) = dtOthers1.Rows(intCount)("pono")
            dr(1) = dtOthers1.Rows(intCount)("siteno")
            dr(2) = dtOthers1.Rows(intCount)("scope")
            dr(3) = dtOthers1.Rows(intCount)("input")
            dr(4) = intMOMId

            If ViewState("OtherSNO") Is Nothing Then
                dr(5) = 1
                ViewState("OtherSNO") = 1
            Else
                dr(5) = Convert.ToInt32(ViewState("OtherSNO")) + 1
                ViewState("OtherSNO") = Convert.ToInt32(ViewState("OtherSNO")) + 1

            End If
            Dim intCartId As Integer = DistinctAddOthers(dtOthers, dtOthers1.Rows(intCount)("pono"), dtOthers1.Rows(intCount)("scope"), dtOthers1.Rows(intCount)("siteno"), Convert.ToInt32(ViewState("OtherSNO")))

            If intCartId <> 100000000 Then
                dtOthers.Rows.RemoveAt(intCartId)
            End If
            dtOthers.Rows.InsertAt(dr, 0)
        Next

        ViewState("dtOthers") = dtOthers
    End Sub
    Sub GetUserDetails(ByVal intMOMId As Integer)

        dtUser.Columns.Add("UsrName", Type.GetType("System.String"))
        dtUser.Columns.Add("UsrType", Type.GetType("System.String"))
        dtUser.Columns.Add("UsrEmail", Type.GetType("System.String"))
        dtUser.Columns.Add("Usr_Id", Type.GetType("System.Int32"))
        dtUser.Columns.Add("Sno", Type.GetType("System.Int32"))

        Dim dtNewUser As DataTable = objBo.uspGetAttendes(intMOMId)
        For intCount As Integer = 0 To dtNewUser.Rows.Count - 1
            Dim dr1 As DataRow = dtUser.NewRow()
            dr1(0) = dtNewUser.Rows(intCount)("Attendesname")
            dr1(1) = dtNewUser.Rows(intCount)("UsrType")
            dr1(2) = dtNewUser.Rows(intCount)("Email")
            dr1(3) = Convert.ToInt32(dtNewUser.Rows(intCount)("Usr_Id"))
            If ViewState("SNO") = Nothing Then
                ViewState("SNO") = 0
            End If
            dr1(4) = Convert.ToInt32(ViewState("SNO")) + 1

            If (DistinctUser(dtUser, dtNewUser.Rows(intCount)("Usr_Id")) = 1) Then
                dtUser.Rows.InsertAt(dr1, 0)
            End If
        Next
        grdUsers.DataSource = dtUser
        Try
            grdUsers.DataBind()
        Catch ex As Exception
            grdUsers.PageIndex = 0
        End Try
        ViewState("dtGrid") = dtUser
    End Sub
    Sub GetMOMDetails(ByVal intMOMId As Integer)
        dtSites.Columns.Add("PoNo", Type.GetType("System.String"))
        dtSites.Columns.Add("Scope", Type.GetType("System.String"))
        dtSites.Columns.Add("siteno", Type.GetType("System.String"))
        dtSites.Columns.Add("sitename", Type.GetType("System.String"))
        dtSites.Columns.Add("workpkgid", Type.GetType("System.String"))
        dtSites.Columns.Add("workpackagename", Type.GetType("System.String"))
        dtSites.Columns.Add("fldtype", Type.GetType("System.String"))
        dtSites.Columns.Add("description", Type.GetType("System.String"))
        dtSites.Columns.Add("band_type", Type.GetType("System.String"))
        dtSites.Columns.Add("band", Type.GetType("System.String"))
        dtSites.Columns.Add("band_type_Other", Type.GetType("System.String"))
        dtSites.Columns.Add("band_Other", Type.GetType("System.String"))
        dtSites.Columns.Add("config", Type.GetType("System.String"))
        dtSites.Columns.Add("config_Other", Type.GetType("System.String"))
        dtSites.Columns.Add("purchase900", Type.GetType("System.String"))
        dtSites.Columns.Add("purchase1800", Type.GetType("System.String"))
        dtSites.Columns.Add("bsshw", Type.GetType("System.String"))
        dtSites.Columns.Add("bsscode", Type.GetType("System.String"))
        dtSites.Columns.Add("qty", Type.GetType("System.String"))
        dtSites.Columns.Add("antennaname", Type.GetType("System.String"))
        dtSites.Columns.Add("antennaname_Other", Type.GetType("System.String"))
        dtSites.Columns.Add("antennaqty", Type.GetType("System.String"))
        dtSites.Columns.Add("feederlen", Type.GetType("System.String"))
        dtSites.Columns.Add("feederlen_Other", Type.GetType("System.String"))
        dtSites.Columns.Add("feedertype", Type.GetType("System.String"))
        dtSites.Columns.Add("feedertype_Other", Type.GetType("System.String"))
        dtSites.Columns.Add("feederqty", Type.GetType("System.String"))
        dtSites.Columns.Add("potype", Type.GetType("System.String"))
        dtSites.Columns.Add("poname", Type.GetType("System.String"))
        dtSites.Columns.Add("vendor", Type.GetType("System.String"))
        dtSites.Columns.Add("LKPDesc", Type.GetType("System.String"))
        dtSites.Columns.Add("Sno", Type.GetType("System.Int32"))
        dtSites.Columns.Add("Momd_Id", Type.GetType("System.Int32"))
        dtSites.Columns.Add("Remarks", Type.GetType("System.String"))
        dtSites.Columns.Add("IsCostImp", Type.GetType("System.String"))
        dtSites.Columns.Add("OldSiteNo", Type.GetType("System.String"))
        dtSites.Columns.Add("LKPDescNEW", Type.GetType("System.String"))
        dtSites.Columns.Add("Subject", Type.GetType("System.String"))
        ' End If
        Dim dtNewUser As DataTable = objBo.uspGetMOMDetalis(intMOMId)
        For intCount As Integer = 0 To dtNewUser.Rows.Count - 1

            Dim dr1 As DataRow = dtSites.NewRow()
            dr1(0) = dtNewUser.Rows(intCount)("Pono")
            dr1(1) = dtNewUser.Rows(intCount)("Scope")
            dr1(2) = dtNewUser.Rows(intCount)("siteno")
            dr1(3) = dtNewUser.Rows(intCount)("sitename")
            dr1(4) = dtNewUser.Rows(intCount)("workpkgid")
            dr1(5) = dtNewUser.Rows(intCount)("workpkgname")
            dr1(6) = dtNewUser.Rows(intCount)("fldtype")
            dr1(7) = dtNewUser.Rows(intCount)("description")
            dr1(8) = dtNewUser.Rows(intCount)("band_type")
            dr1(9) = dtNewUser.Rows(intCount)("band")
            dr1(10) = dtNewUser.Rows(intCount)("band_type")
            dr1(11) = dtNewUser.Rows(intCount)("band")
            dr1(12) = dtNewUser.Rows(intCount)("config")
            dr1(13) = dtNewUser.Rows(intCount)("config")
            dr1(14) = dtNewUser.Rows(intCount)("purchase900")
            dr1(15) = dtNewUser.Rows(intCount)("purchase1800")
            dr1(16) = dtNewUser.Rows(intCount)("bsshw")
            dr1(17) = dtNewUser.Rows(intCount)("bsscode")
            dr1(18) = dtNewUser.Rows(intCount)("qty")
            dr1(19) = dtNewUser.Rows(intCount)("antennaname")
            dr1(20) = dtNewUser.Rows(intCount)("antennaname")
            dr1(21) = dtNewUser.Rows(intCount)("antennaqty")
            dr1(22) = dtNewUser.Rows(intCount)("feederlen")
            dr1(23) = dtNewUser.Rows(intCount)("feederlen")
            dr1(24) = dtNewUser.Rows(intCount)("feedertype")
            dr1(25) = dtNewUser.Rows(intCount)("feedertype")
            dr1(26) = dtNewUser.Rows(intCount)("feederqty")
            dr1(27) = dtNewUser.Rows(intCount)("potype")
            dr1(28) = dtNewUser.Rows(intCount)("poname")
            dr1(29) = dtNewUser.Rows(intCount)("vendor")
            dr1(30) = dtNewUser.Rows(intCount)("LKPDesc")
            If ViewState("SNOSite") = Nothing Then
                ViewState("SNOSite") = 0
            End If
            dr1(31) = Convert.ToInt32(ViewState("SNOSite")) + 1
            dr1(32) = intMOMId
            dr1(33) = dtNewUser.Rows(intCount)("Remarks")
            dr1(34) = dtNewUser.Rows(intCount)("IsCostImp")

            dr1(35) = dtNewUser.Rows(intCount)("OldSiteNo")
            dr1(36) = dtNewUser.Rows(intCount)("LKPDescNEW")
            dr1(37) = dtNewUser.Rows(intCount)("subject")
            'If (DistinctSites(dtUser, dtNewUser.Rows(intCount)("pono"), dtNewUser.Rows(intCount)("siteno"), dtNewUser.Rows(intCount)("Scope")) = 1) Then
            dtSites.Rows.InsertAt(dr1, 0)
            'End If
        Next
        grdpo.Columns(5).Visible = True
        GrdPo.DataSource = dtSites
        Try
            GrdPo.DataBind()
            'grdpo.Columns(5).Visible = False
        Catch ex As Exception
            GrdPo.PageIndex = 0
        End Try
        ViewState("dtGridSites") = dtSites
    End Sub
    Protected Sub GrdPo_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs)
        tblSiteDetails.Style.Add("display", "")
        Dim ihdSno As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdSno"), HiddenField)
        Dim ihdMomd_Id As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdMomd_Id"), HiddenField)
        Dim ihdvendor As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdvendor"), HiddenField)
        Dim ihdPoName As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdPoName"), HiddenField)
        Dim ihdPotype As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdPotype"), HiddenField)
        Dim ihdfeederqty As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdfeederqty"), HiddenField)
        Dim ihdfeedertype_Other As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdfeedertype_Other"), HiddenField)
        Dim ihdfeedertype As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdfeedertype"), HiddenField)
        Dim ihdfeederlen_Other As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdfeederlen_Other"), HiddenField)
        Dim ihdfeederlen As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdfeederlen"), HiddenField)
        Dim ihdantennaqty As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdantennaqty"), HiddenField)
        Dim ihdantennaname_Other As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdantennaname_Other"), HiddenField)
        Dim ihdantennaname As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdantennaname"), HiddenField)
        Dim ihdqty As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdqty"), HiddenField)
        Dim ihdbsscode As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdbsscode"), HiddenField)
        Dim ihdbsshw As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdbsshw"), HiddenField)
        Dim ihdpurchase1800 As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdpurchase1800"), HiddenField)
        Dim ihdpurchase900 As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdpurchase900"), HiddenField)
        Dim ihdconfig_Other As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdconfig_Other"), HiddenField)
        Dim ihdconfig As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdconfig"), HiddenField)
        Dim ihdband_Other As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdband_Other"), HiddenField)
        Dim ihdband_type_Other As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdband_type_Other"), HiddenField)
        Dim ihdband As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdband"), HiddenField)
        Dim ihdband_type As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdband_type"), HiddenField)
        Dim ihddescription As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hddescription"), HiddenField)
        Dim ihdfldtype As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdfldtype"), HiddenField)
        Dim ihdworkpackagename As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdworkpackagename"), HiddenField)
        Dim ihdsitename As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdsitename"), HiddenField)
        Dim ihdScope As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdScope"), HiddenField)
        Dim ihdRemarks As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdRemarks"), HiddenField)
        Dim ihdIsCostImp As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdIsCostImp"), HiddenField)
        Dim ihdOldSiteNo As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdOldSiteNo"), HiddenField)
        Dim ihdLKPDesc As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdLKPDesc"), HiddenField)
        Dim ihdsubject As HiddenField = CType(GrdPo.Rows(e.NewSelectedIndex).FindControl("hdsubject"), HiddenField)
        rblType.ClearSelection()
        Dim li As ListItem
        li = rblType.Items.FindByValue(ihdLKPDesc.Value)
        If Not IsNothing(li) Then
            rblType.Items.FindByValue(ihdLKPDesc.Value).Selected = True
        Else
            rblType.SelectedItem.Value = ihdLKPDesc.Value
        End If


        li = ddlPONo.Items.FindByText(GrdPo.Rows(e.NewSelectedIndex).Cells(2).Text)
        If Not IsNothing(li) Then
            ddlPONo.ClearSelection()
            ddlPONo.Items.FindByText(GrdPo.Rows(e.NewSelectedIndex).Cells(2).Text).Selected = True
        Else
            ddlPONo.SelectedItem.Text = GrdPo.Rows(e.NewSelectedIndex).Cells(2).Text
        End If


        objDLL.fillDDL(ddlSite, "MOMPositeNo", ddlPONo.SelectedItem.Value, True, Constants._DDL_Default_Select)

        li = ddlSite.Items.FindByText(ihdOldSiteNo.Value & "-" & ihdScope.Value)
        If Not IsNothing(li) Then
            ddlSite.ClearSelection()
            ddlSite.Items.FindByText(ihdOldSiteNo.Value & "-" & ihdScope.Value).Selected = True
        Else
            ddlSite.SelectedItem.Text = ihdOldSiteNo.Value & "-" & ihdScope.Value
        End If


        objDLL.fillDDL(ddlbandtype, "CODLookup", Constants._PO_Band_Type, True, Constants._DDL_Default_Select)
        objDLL.fillDDL(ddlband, "CODLookup", Constants._PO_Band, True, Constants._DDL_Default_Select)
        objDLL.fillDDL(ddlconfig, "CODLookup", Constants._PO_Configuration, True, Constants._DDL_Default_Select)
        objDLL.fillDDL(ddlAntennaName, "CODLookup", Constants._AntennaName, True, Constants._DDL_Default_Select)
        objDLL.fillDDL(ddlFeederLength, "CODLookup", Constants._FeederLength, True, Constants._DDL_Default_Select)
        objDLL.fillDDL(ddlFeederType, "CODLookup", Constants._FeederType, True, Constants._DDL_Default_Select)

        Dim ddlll As String() = ddlSite.SelectedItem.Text.Split("-")
        Dim dl As String() = ddlll(0).Split("-"), dt As DataTable
        dl(0) = ddlll(0)
        dt = objBLPO.uspGetMOMconfig(ddlPONo.SelectedValue, ddlll(0), ddlll(1), 0)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            DataList1.DataSource = dt
            DataList1.DataBind()

        End If

        dtOthers = CType(ViewState("dtOthers"), DataTable)
        If dtOthers.Rows.Count > 0 Then



            Dim dtOtherView As New DataView
            dtOtherView = dtOthers.DefaultView

            dtOtherView.RowFilter = " pono='" & GrdPo.Rows(e.NewSelectedIndex).Cells(2).Text & "' and siteid='" & ihdOldSiteNo.Value & "' and scope='" & ihdScope.Value & "'"

            grdOthers.DataSource = dtOthers
            Try
                grdOthers.DataBind()
            Catch ex As Exception
                grdOthers.PageIndex = 0
            End Try
        End If
        txtSiteNo.Value = GrdPo.Rows(e.NewSelectedIndex).Cells(1).Text
        txtSiteName.Value = ihdsitename.Value
        If GrdPo.Rows(e.NewSelectedIndex).Cells(3).Text.Trim.ToString = "&nbsp;" Then
            txtWorkPkgID.Value = Nothing

        Else
            txtWorkPkgID.Value = GrdPo.Rows(e.NewSelectedIndex).Cells(3).Text.Trim

        End If
        txtWorkPkgName.Value = ihdworkpackagename.Value
        txtFldType.Value = ihdfldtype.Value
        txtDesc.Value = ihddescription.Value
        txtBandType1.Value = ihdband_type_Other.Value
        txtBand.Value = ihdband_Other.Value
        txtconfig.Value = ihdconfig_Other.Value
        If ihdband_type.Value <> Nothing Then
            li = ddlbandtype.Items.FindByText(ihdband_type.Value)
            If Not IsNothing(li) Then
                ddlbandtype.ClearSelection()
                ddlbandtype.Items.FindByText(ihdband_type.Value).Selected = True
            Else
                ddlbandtype.SelectedItem.Text = ihdband_type.Value
            End If


        End If
        If ihdband.Value <> Nothing Then
            li = ddlband.Items.FindByText(ihdband.Value)
            If Not IsNothing(li) Then
                ddlband.ClearSelection()
                ddlband.Items.FindByText(ihdband.Value).Selected = True
            Else
                ddlband.SelectedItem.Text = ihdband.Value
            End If

        End If
        If ihdconfig.Value <> Nothing Then
            li = ddlconfig.Items.FindByText(ihdconfig.Value)
            If Not IsNothing(li) Then
                ddlconfig.ClearSelection()
                ddlconfig.Items.FindByText(ihdconfig.Value).Selected = True
            Else
                ddlconfig.SelectedItem.Text = ihdconfig.Value
            End If

        End If
        txtPurchase900.Value = ihdpurchase900.Value
        txtPurchase1800.Value = ihdpurchase1800.Value
        txtBSSHW.Value = ihdbsshw.Value
        txtBSSCode.Value = ihdbsscode.Value
        txtQty.Value = ihdqty.Value
        If ihdantennaname.Value <> Nothing Then
            li = ddlAntennaName.Items.FindByText(ihdantennaname.Value)
            If Not IsNothing(li) Then
                ddlAntennaName.ClearSelection()
                ddlAntennaName.Items.FindByText(ihdantennaname.Value).Selected = True
            Else
                ddlAntennaName.SelectedItem.Text = ihdantennaname.Value
            End If

        End If
        txtAntName.Value = ihdantennaname_Other.Value
        txtAntennaQty.Value = ihdantennaqty.Value
        If ihdfeederlen.Value <> Nothing And ihdfeederlen.Value <> "0" Then
            li = ddlFeederLength.Items.FindByText(ihdfeederlen.Value)
            If Not IsNothing(li) Then
                ddlFeederLength.ClearSelection()
                ddlFeederLength.Items.FindByText(ihdfeederlen.Value).Selected = True
            Else
                ddlFeederLength.SelectedItem.Text = ihdfeederlen.Value
            End If
        End If
        txtFeederLength.Value = ihdfeederlen.Value
        If ihdfeedertype.Value <> Nothing And ihdfeedertype.Value <> "0" Then
            li = ddlFeederType.Items.FindByText(ihdfeedertype.Value)
            If Not IsNothing(li) Then
                ddlFeederType.ClearSelection()
                ddlFeederType.Items.FindByText(ihdfeedertype.Value).Selected = True
            Else
                ddlFeederType.SelectedItem.Text = ihdfeedertype.Value
            End If

        End If
        txtFeederType.Value = ihdfeedertype.Value
        txtFeederQty.Value = ihdfeederqty.Value
        txtPoType.Value = ihdPotype.Value
        txtPoName.Value = ihdPoName.Value
        txtVendor.Value = ihdvendor.Value
        txtSubject.Value = ihdsubject.Value
        txtRemarks.Value = ihdRemarks.Value
        If (ihdIsCostImp.Value) Then
            chkCost.Checked = True
        Else
            chkCost.Checked = False
        End If
        dtSites = CType(ViewState("dtGridSites"), DataTable)

        Dim intCartId As Integer = DistinctAddSites(dtSites, GrdPo.Rows(e.NewSelectedIndex).Cells(2).Text, ihdScope.Value, GrdPo.Rows(e.NewSelectedIndex).Cells(1).Text)

        If intCartId <> 100000000 Then
            dtSites.Rows.RemoveAt(intCartId)
            dtSites.DefaultView.Sort = "Sno asc"
            GrdPo.Columns(5).Visible = True
            GrdPo.DataSource = dtSites
            GrdPo.DataBind()
            'grdpo.Columns(5).Visible = False
            ViewState("dtGridSites") = dtSites
        End If
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Sub btn1Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        tblSiteDetails.Style.Add("display", "none")
        rblType.ClearSelection()
        ddlPONo.ClearSelection()
        ddlSite.ClearSelection()
        txtSubject.Value = ""
        tdGenerate.Style.Add("display", "")
    End Sub
    Public Sub CreateOthersNewRow()
        If ViewState("dtOthers") Is Nothing Then
            dtOthers.Columns.Add("pono", Type.GetType("System.String"))
            dtOthers.Columns.Add("siteid", Type.GetType("System.String"))
            dtOthers.Columns.Add("scope", Type.GetType("System.String"))
            dtOthers.Columns.Add("contents", Type.GetType("System.String"))
            dtOthers.Columns.Add("mom_Id", Type.GetType("System.Int32"))
            dtOthers.Columns.Add("Sno", Type.GetType("System.Int32"))
        Else
            dtOthers = CType(ViewState("dtOthers"), DataTable)

        End If
        For intCount As Integer = 0 To grdOthers.Rows.Count - 1
            Dim ihdSno As HiddenField = CType(grdOthers.Rows(intCount).FindControl("hdSno"), HiddenField)

            Dim ihdmomid As HiddenField = CType(grdOthers.Rows(intCount).FindControl("hdmomid"), HiddenField)
            Dim ihdPono As HiddenField = CType(grdOthers.Rows(intCount).FindControl("hdPono"), HiddenField)
            Dim ihdSite As HiddenField = CType(grdOthers.Rows(intCount).FindControl("hdSite"), HiddenField)

            Dim ihdScope As HiddenField = CType(grdOthers.Rows(intCount).FindControl("hdScope"), HiddenField)
            Dim itxtcontent As TextBox = CType(grdOthers.Rows(intCount).FindControl("txtcontent"), TextBox)

            Dim dr As DataRow = dtOthers.NewRow()
            dr(0) = ihdPono.Value
            dr(1) = ihdSite.Value
            dr(2) = ihdScope.Value
            dr(3) = itxtcontent.Text
            dr(4) = ihdmomid.Value
            dr(5) = ihdSno.Value
            Dim intCartId As Integer = DistinctAddOthers(dtOthers, ihdPono.Value, ihdScope.Value, ihdSite.Value, ihdSno.Value)

            If intCartId <> 100000000 Then
                dtOthers.Rows.RemoveAt(intCartId)
            End If
            dtOthers.Rows.InsertAt(dr, 0)
        Next
        Dim dr1 As DataRow = dtOthers.NewRow()
        Dim ddlll As String() = ddlSite.SelectedItem.Text.Split("-")
        dr1(0) = ddlPONo.SelectedItem.Text
        dr1(1) = ddlll(0)
        dr1(2) = ddlll(1)
        dr1(3) = ""
        If (Request.QueryString("id") Is Nothing) Then
            dr1(4) = 0
        Else
            dr1(4) = Request.QueryString("id")
        End If
        If ViewState("OtherSNO") Is Nothing Then
            dr1(5) = 1
            ViewState("OtherSNO") = 1
        Else
            dr1(5) = Convert.ToInt32(ViewState("OtherSNO")) + 1
            ViewState("OtherSNO") = Convert.ToInt32(ViewState("OtherSNO")) + 1

        End If

        If (DistinctOthers(dtOthers, ddlPONo.SelectedItem.Text, ddlll(0), ddlll(1), Convert.ToInt32(ViewState("OtherSNO"))) = 1) Then
            dtOthers.Rows.InsertAt(dr1, 0)
        End If
        ViewState("dtOthers") = dtOthers
        Dim dtOtherView As New DataView
        dtOtherView = dtOthers.DefaultView

        dtOtherView.RowFilter = " pono='" & ddlPONo.SelectedItem.Text & "' and siteid='" & ddlll(0) & "' and scope='" & ddlll(1) & "'"

        grdOthers.DataSource = dtOthers
        Try
            grdOthers.DataBind()
        Catch ex As Exception
            grdOthers.PageIndex = 0
        End Try

    End Sub
    Public Sub CreateOthers()
        If ViewState("dtOthers") Is Nothing Then
            dtOthers.Columns.Add("pono", Type.GetType("System.String"))
            dtOthers.Columns.Add("siteid", Type.GetType("System.String"))
            dtOthers.Columns.Add("scope", Type.GetType("System.String"))
            dtOthers.Columns.Add("contents", Type.GetType("System.String"))
            dtOthers.Columns.Add("mom_Id", Type.GetType("System.Int32"))
            dtOthers.Columns.Add("Sno", Type.GetType("System.Int32"))
        Else
            dtOthers = CType(ViewState("dtOthers"), DataTable)
        End If

        For intCount As Integer = 0 To grdOthers.Rows.Count - 1
            Dim ihdSno As HiddenField = CType(grdOthers.Rows(intCount).FindControl("hdSno"), HiddenField)

            Dim ihdmomid As HiddenField = CType(grdOthers.Rows(intCount).FindControl("hdmomid"), HiddenField)
            Dim ihdPono As HiddenField = CType(grdOthers.Rows(intCount).FindControl("hdPono"), HiddenField)
            Dim ihdSite As HiddenField = CType(grdOthers.Rows(intCount).FindControl("hdSite"), HiddenField)

            Dim ihdScope As HiddenField = CType(grdOthers.Rows(intCount).FindControl("hdScope"), HiddenField)
            Dim itxtcontent As TextBox = CType(grdOthers.Rows(intCount).FindControl("txtcontent"), TextBox)

            Dim dr As DataRow = dtOthers.NewRow()
            dr(0) = ihdPono.Value
            dr(1) = ihdSite.Value
            dr(2) = ihdScope.Value
            dr(3) = itxtcontent.Text
            dr(4) = ihdmomid.Value
            dr(5) = ihdSno.Value
            Dim intCartId As Integer = DistinctAddOthers(dtOthers, ihdPono.Value, ihdScope.Value, ihdSite.Value, ihdSno.Value)

            If intCartId <> 100000000 Then
                dtOthers.Rows.RemoveAt(intCartId)
            End If
            dtOthers.Rows.InsertAt(dr, 0)
        Next

        ViewState("dtOthers") = dtOthers

    End Sub
    Public Function DistinctOthers(ByVal dt As DataTable, ByVal strPono As String, ByVal strSite As String, ByVal strScope As String, ByVal intSno As Integer) As Integer
        Dim intReturn As Integer = 0
        If dt.Rows.Count <> 0 Then
            Dim intCount As Integer
            For intCount = 0 To dt.Rows.Count - 1 Step intCount + 1
                If dt.Rows(intCount)("pono").ToString() = strPono And dt.Rows(intCount)("scope").ToString() = strScope And dt.Rows(intCount)("siteid").ToString() = strSite And dt.Rows(intCount)("sno").ToString() = intSno.ToString Then
                    intReturn = 0
                    Exit For
                Else
                    intReturn = 1
                End If

            Next
        Else
            intReturn = 1
        End If
        Return intReturn
    End Function
    Public Function DistinctAddOthers(ByVal dt As DataTable, ByVal strPoNo As String, ByVal strScope As String, ByVal strSiteno As String, ByVal intSno As Integer) As Integer
        Dim intReturn As Integer = 0
        If dt.Rows.Count <> 0 Then
            Dim intCount As Integer
            For intCount = 0 To dt.Rows.Count - 1 Step intCount + 1
                If dt.Rows(intCount)("PoNo").ToString() = strPoNo And dt.Rows(intCount)("scope").ToString() = strScope And dt.Rows(intCount)("siteid").ToString() = strSiteno And dt.Rows(intCount)("sno").ToString() = intSno.ToString Then
                    intReturn = intCount
                    Exit For
                Else
                    intReturn = 100000000
                End If

            Next
        Else
            intReturn = 100000000
        End If
        Return intReturn
    End Function
    Protected Sub grdOthers_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbl As Label = CType(e.Row.FindControl("lblno"), Label)
            lbl.Text = (grdOthers.PageIndex * grdOthers.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End If
    End Sub
    Protected Sub grdOthers_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        dtOthers = CType(ViewState("dtOthers"), DataTable)
        grdOthers.PageIndex = e.NewPageIndex
        Dim ddlll As String() = ddlSite.SelectedItem.Text.Split("-")
        Dim dtOtherView As New DataView
        dtOtherView = dtOthers.DefaultView

        dtOtherView.RowFilter = " pono='" & ddlPONo.SelectedItem.Text & "' and siteid='" & ddlll(0) & "' and scope='" & ddlll(1) & "'"

        grdOthers.DataSource = dtOthers

        Try
            grdOthers.DataBind()
        Catch ex As Exception
            grdOthers.PageIndex = 0
        End Try
    End Sub
    Protected Sub grdOthers_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)
        dtOthers = CType(ViewState("dtOthers"), DataTable)
        Dim ihdSno As HiddenField = CType(grdOthers.Rows(e.RowIndex).FindControl("hdSno"), HiddenField)

        Dim ihdPono As HiddenField = CType(grdOthers.Rows(e.RowIndex).FindControl("hdPono"), HiddenField)
        Dim ihdSite As HiddenField = CType(grdOthers.Rows(e.RowIndex).FindControl("hdSite"), HiddenField)

        Dim ihdScope As HiddenField = CType(grdOthers.Rows(e.RowIndex).FindControl("hdScope"), HiddenField)
        Dim intCartId As Integer = DistinctAddOthers(dtOthers, ihdPono.Value, ihdScope.Value, ihdSite.Value, ihdSno.Value)

        If intCartId <> 100000000 Then
            dtOthers.Rows.RemoveAt(intCartId)

            Dim dtOtherView As New DataView
            dtOtherView = dtOthers.DefaultView

            dtOtherView.RowFilter = " pono='" & ddlPONo.SelectedItem.Text & "' and siteid='" & ihdSite.Value & "' and scope='" & ihdScope.Value & "'"

            grdOthers.DataSource = dtOthers
            Try
                grdOthers.DataBind()
            Catch ex As Exception
                grdOthers.PageIndex = 0
            End Try
            ViewState("dtOthers") = dtOthers
        End If
    End Sub
    Protected Sub grdOthers_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs)
        If rblType.SelectedItem.Text.ToLower = "change configuration" Then
            grdOthers.Visible = True
            CreateOthersNewRow()
        Else
            grdOthers.Visible = False
        End If
    End Sub

End Class
