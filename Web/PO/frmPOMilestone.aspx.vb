Imports System.Data
Imports BusinessLogic
Partial Class PO_frmPOMilestone
    Inherits System.Web.UI.Page
    Dim objutil As New Common.DBUtil
    Dim objBositedoc As New BOSiteDocs()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Here we need to get the workpackageid from requestobj or from session
        Dim workPkgId As String = ""
        If Page.IsPostBack <> True Then
            If (Request.QueryString("id") Is Nothing) Then

            Else
                workPkgId = Request.QueryString("id").ToString()
            End If
            'calling a method to bind the data 
            BindData(workPkgId)
            'BindData("2278944")
        End If
       
    End Sub
    Private Sub BindData(ByVal workpkgid As String)
        Dim strQuery As String = ""
        Dim dt As DataTable = objutil.ExeQueryDT("Select MileStone from TIMilestone", "TIMestone")
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                strQuery = strQuery & IIf(strQuery <> "", ",", "") & "ISNull(convert(varchar(10),(" & dt.Rows(i).Item("Milestone").ToString & "),103),'''') as " & dt.Rows(i).Item("Milestone").ToString()
            Next
        End If
        'getting data from database
        Dim strsql As String
        If strQuery <> "" Then
            strsql = "exec uspGetPOEPMDataMilestoneReport '" & workpkgid & "','" & strQuery & "'"
        Else
            strsql = "exec uspGetPOEPMDataMilestoneReport '" & workpkgid & "'"
        End If
        Dim dsPOEPMData As DataSet = objutil.ExeQuery(strsql, "Milestone")
        'objBositedoc.GetTIPOEPMDetaMilestoneReport(workpkgid, strQuery)
        'Dim dsPOData As DataSet = objBositedoc.GetTIPODetailsData(workpkgid)
        'Dim dsEPMData As DataSet = objBositedoc.GetTIEPMData(workpkgid)
        Dim dsMilestoneData As DataSet = objBositedoc.GetTIMileStoneDetails()

        Dim rowPOEPMData As DataRow
        'Dim rowPOData As DataRow
        'Dim rowEPMdata As DataRow
        Dim rowPOEPMdata1 As DataRow

        'binding data
        If dsPOEPMData.Tables(0).Rows.Count > 0 Then
            rowPOEPMData = dsPOEPMData.Tables(0).Rows(0)

            'binding section top

            lblPONo.Text = rowPOEPMData("PoNo").ToString()
            lblSiteno.Text = rowPOEPMData("SiteNo").ToString()

            'Binding Site Info data
            lblPOworkpkgId.Text = rowPOEPMData("workpkgid").ToString()
            lblworkPkgname.Text = rowPOEPMData("workpackagename").ToString()
            If rowPOEPMData("ContractDate").ToString() <> "" Then lblcontactdate.Text = IIf(rowPOEPMData("ContractDate").ToString() = "01/01/1900", "", Format(CDate(rowPOEPMData("ContractDate").ToString()), "dd/MMM/yyyy"))
            lblSitename.Text = rowPOEPMData("SiteName").ToString()
            'lblZone.Text = rowPOEPMData("Zone").ToString()
            'lblRegion.Text = rowPOEPMData("Region").ToString()

            'binding scope info
            lblScope.Text = rowPOEPMData("fldtype").ToString()
            lblDescription.Text = rowPOEPMData("description").ToString()

            'binding Value Info
            lblValueInUsd.Text = String.Format("{0:0.00}", Convert.ToDecimal(rowPOEPMData("value1").ToString()))
            lblvalueinidr.Text = String.Format("{0:0.00}", Convert.ToDecimal(rowPOEPMData("Value2").ToString()))

            'binding hardware info
            lblHardware.Text = rowPOEPMData("bsshw").ToString()
            lblHrdCode.Text = rowPOEPMData("bsscode").ToString()

            'binding band info
            lblBand.Text = rowPOEPMData("band").ToString()
            lblBandtype.Text = rowPOEPMData("band_type").ToString()
            lblConfiguration.Text = rowPOEPMData("config").ToString()
            lblPurch1800.Text = rowPOEPMData("purchase1800").ToString()
            lblPurch900.Text = rowPOEPMData("purchase900").ToString()

            'binding antenna info
            lblAntennaname.Text = rowPOEPMData("antennaname").ToString()
            lblAntennaqty.Text = rowPOEPMData("antennaqty").ToString()
            lblFeederlength.Text = rowPOEPMData("feederlen").ToString()
            lblFeedertype.Text = rowPOEPMData("feedertype").ToString()
            lblFeederqty.Text = rowPOEPMData("feederqty").ToString()

            'binding epmdata
            lblPHseTI.Text = rowPOEPMData("Phaseti").ToString()
            lblworkpkgId.Text = rowPOEPMData("workpkgid").ToString()
            If rowPOEPMData("siteintegration").ToString.Trim() <> "" Then
                lblSiteIntegration.Text = IIf(rowPOEPMData("siteintegration").ToString.Trim() <> "", Format(CDate(rowPOEPMData("siteintegration").ToString()), "dd/MMM/yyyy"), "")
            End If
            'lblSiteIntegration.Text = IIf(rowPOEPMData("siteintegration").ToString() <> "", Format(CDate(rowPOEPMData("siteintegration").ToString()), "dd/MMM/yyyy"), "")
            lblsiteacponair.Text = rowPOEPMData("siteacponair").ToString()
            lblSiteacponbast.Text = rowPOEPMData("Siteacponbast").ToString()
            lblPkgType.Text = rowPOEPMData("Packagetype").ToString()
            lblPkgName.Text = rowPOEPMData("Packagename").ToString()
            lblPkgStatus.Text = rowPOEPMData("packagestatus").ToString()
            lblcustporecvdt.Text = rowPOEPMData("custporecdt").ToString()
        End If

        'For Binding Milestonedata
        'get tbale 
        If dsMilestoneData.Tables(0).Rows.Count > 0 Then
            Dim count As Int32 = dsMilestoneData.Tables(0).Rows.Count
            Dim tblMilstoneData As DataTable = GetTableStruct()
            'Adding data to the local datatable
            For Each rowMData As DataRow In dsMilestoneData.Tables(0).Rows
                Dim datarow As DataRow = tblMilstoneData.NewRow()
                datarow("Milestone") = rowMData("INT_Desc").ToString()
                If dsPOEPMData.Tables(0).Rows.Count > 0 Then
                    rowPOEPMdata1 = dsPOEPMData.Tables(0).Rows(0)
                    datarow("Fortune") = rowPOEPMdata1("For_" & rowMData("Int_Code").ToString().Trim())
                    datarow("Actual") = rowPOEPMdata1("Act_" & rowMData("Int_Code").ToString().Trim())
                    'datarow("Planned") = rowPOEPMdata1("PLN_" & rowMData("Int_Code").ToString.Trim())
                End If
                tblMilstoneData.Rows.Add(datarow)
            Next

            'bind this data to Gridview
            grdMilestones.DataSource = tblMilstoneData
            grdMilestones.DataBind()

        End If
    End Sub

    Private Function GetTableStruct() As DataTable
        Dim tblData As New DataTable()
        tblData.Columns.Add(New DataColumn("Milestone"))
        tblData.Columns.Add(New DataColumn("Planned"))
        tblData.Columns.Add(New DataColumn("Fortune"))
        tblData.Columns.Add(New DataColumn("Actual"))
        Return tblData
    End Function
End Class
