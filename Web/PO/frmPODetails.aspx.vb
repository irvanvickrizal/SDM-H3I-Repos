Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class PO_frmPODetails
    Inherits System.Web.UI.Page
    Dim objbo As New BOPODetails
    Dim objdo As New ETPODetails
    Dim objDL As New BODDLs
    Dim dt As DataTable
    Dim cst As New Common.Constants
    Dim objboL As New BOCODLookUp
    Dim ETLook As New ETCODLookup
    Dim objutil As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDateOfProduction.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtContractDt,'dd/mm/yyyy');return false;")
        If Page.IsPostBack = False Then
            If Not Request.QueryString("Sno") Is Nothing Then
                bindData()
                If Request.QueryString("C") Is Nothing Then
                    pnlCostInfo.Visible = True
                    rowMilestonesreport.Visible = True
                Else
                    pnlCostInfo.Visible = False
                    rowMilestonesreport.Visible = False
                End If
            End If
        End If
        If Request.QueryString("from") = "epm" Then
            remaprow.Visible = True
            txtfldtype.Visible = False
            ddlscope.Visible = True

        Else
            txtfldtype.Visible = True
            ddlscope.Visible = False
            remaprow.Visible = False
        End If
        'txtBand.Visible = False
        'txtBType.Visible = False
        'txtConfig.Visible = False
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Request.QueryString("from") = "epm" Then
            If txtremapsite.Text = "" Then
                Response.Write("<script> alert('Please enter remapsite')</script>")
                Exit Sub
            End If
        End If
        Dim strsql As String
        fillDetails()
        Dim url As String
        If Request.QueryString("from") = "epm" Then
            url = "frmPOfromEPMList.aspx"
        Else
            url = "frmPOList.aspx?type=P"
        End If
        If Request.QueryString("TT") = "R" Then
            BOcommon.result(objbo.uspTIPODetailsIU(objdo, Request.QueryString("TT")), True, url, "", Constants._INSERT)
        Else
            With objdo
                objdo.PO_Id = Request.QueryString("Sno")
                'strsql = "Exec uspTIPODetailsIU " & .PO_Id & "," & .Sno & ",'" & .PONo & "','" & .SiteNo & "','" & .SiteName & "','" & .WorkPKGId & "','" & .FldType & "'," & _
                '.AT.RStatus & ",'" & .AT.LMBY & "','" & .ContractDate & "','" & txtprojectid.Value.Replace("'", "''") & "','" & txtremapsite.Text & "','" & txtsitenamepo.Text & "','" & txtsiteidpo.Text & "', '" & _
                'TxtHOTAsPerPo.Text & "'"
                strsql = "Exec uspTIPODetailsIU " & .PO_Id & "," & .Sno & ",'" & .PONo & "','" & .SiteNo & "','" & .SiteName & "','" & .WorkPKGId & "','" & .FldType.TrimEnd() & "'," & _
                .AT.RStatus & ",'" & .AT.LMBY & "','" & .ContractDate & "','" & txtprojectid.Value.Replace("'", "''") & "','" & txtremapsite.Text & "','" & txtsitenamepo.Text & "','" & txtsiteidpo.Text & "','" & _
                TxtHOTAsPerPo.Text & "','" & .Band & "','" & .Config & "','" & txtDesc.Value & "'"
            End With
            BOcommon.result(objutil.ExeQueryScalar(strsql), True, url, "", Constants._UPDATE)
        End If
    End Sub

    Protected Sub btnBack_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.ServerClick
        Response.Redirect("frmPOList.aspx?type=P")
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim kk As Integer
        kk = objutil.ExeQueryScalar("exec uspdeletesite4PO " & hdnpoid.Value & ",'" & lblPONo.InnerText & "','" & lblSiteNo.InnerText & "'")
        If kk = 99 Then
            Response.Write("<script>alert('Cannot Delete this Site since documents are already uploaded')</script>")
        ElseIf kk = 2 Then
            Response.Write("<script>alert('Site Deleted from PO')</script>")
            Response.Redirect("frmPOList.aspx?type=P")
        Else
            Response.Write("<script>alert('Error while doing Transaction')</script>")

        End If
    End Sub

#Region "Custom Methods"
    Sub bindData()
        dt = objbo.uspGetPODetails(Request.QueryString("Sno"), Request.QueryString("TT"))
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                lblPONo.InnerText = .Item("PONo").ToString
                objDL.fillDDL(ddlscope, "scopebypo", lblPONo.InnerText, True, Constants._DDL_Default_Select)
                lblSiteNo.InnerText = .Item("SiteNo").ToString
                txtsitenamepo.Text = .Item("SiteNamepo").ToString
                txtWPKGId.Value = .Item("WorkPkgId").ToString
                txtsiteidpo.Text = .Item("siteidpo").ToString
                lblsitenameepm.InnerText = .Item("sitenameepm").ToString
                lblponame.InnerText = .Item("poname").ToString
                TxtHOTAsPerPo.Text = .Item("HOTAsPerPO").ToString
                TxtBand.Text = .Item("Band").ToString
                TxtConfig.Text = .Item("Config").ToString
                'BindScopeMaster(Convert.ToString(.Item("MasterScope")), DdlScopeMaster)
                'BindTypeofWork(Convert.ToString(.Item("typeofwork")), DdlTypeofwork)
                If Request.QueryString("from") = "epm" Then

                Else
                    txtfldtype.Value = .Item("FldType").ToString 'this filed displayed as scope
                End If

                txtDesc.Value = .Item("Description").ToString

                'binding the hiddenfield to pass Milestones report
                hdnwpid.Value = .Item("WorkPkgId").ToString


                txtHW.Value = .Item("BSSHW").ToString
                txtCode.Value = .Item("BSSCode").ToString

                txtValue1.Value = Format(Val(.Item("Value1").ToString), "###,#.00")
                txtValue2.Value = Format(Val(.Item("Value2").ToString), "###,#.00")
                txtWPName.Value = .Item("WorkPackageName").ToString
                'Dim test As String = .Item("ContractDate").ToString()
                'Dim dt As Date = CDate(.Item("ContractDate").ToString())
                lblpodate.InnerText = .Item("podate").ToString
                If .Item("remappedfrom").ToString = "" Then
                    lblsitefrom.InnerText = "PO"
                Else
                    lblsitefrom.InnerText = .Item("remappedfrom").ToString
                End If

                hdnpoid.Value = .Item("po_id")
                'If cst.formatDDMMYYYY(Format(CDate(.Item("ContractDate").ToString), "dd/MM/yyyy")) <> Constants._EmptyDate Then txtContractDt.Value = Format(CDate(.Item("ContractDate").ToString), "dd/MM/yyyy")
                txtprojectid.Value = .Item("TSELProjectID").ToString

            End With
        End If
    End Sub

    Sub fillDetails()
        With objdo
            .Sno = Request.QueryString("Sno")
            .PONo = lblPONo.InnerText
            .SiteNo = lblSiteNo.InnerText
            .SiteName = txtsitenamepo.Text
            .Band = TxtBand.Text
            .Config = TxtConfig.Text

            .WorkPKGId = txtWPKGId.Value.Replace("'", "''")
            If Request.QueryString("from") = "epm" Then
                .FldType = ddlscope.SelectedItem.Text
            Else
                .FldType = txtfldtype.Value.TrimEnd() 'this filed displayed as scope
            End If

            .Description = txtDesc.Value



            .BSSHW = txtHW.Value
            .BSSCode = txtCode.Value

            .Value1 = IIf(txtValue1.Value = "", 0, txtValue1.Value)
            .Value2 = IIf(txtValue2.Value = "", 0, txtValue2.Value)
            .AT.RStatus = Constants.STATUS_ACTIVE
            .AT.LMBY = Session("User_Name")
            .WorkPackageName = txtWPName.Value

            'If txtContractDt.Value <> "" Then
            '    .ContractDate = cst.formatdateDDMMYYYY(txtContractDt.Value)
            'End If


        End With
    End Sub

    Private Sub BindScopeMaster(ByVal mscope As String, ByVal ddl As DropDownList)
        Dim dtMasterScopes As DataTable = objutil.ExeQueryDT("exec uspGetScopeofwork", "masterscope")
        ddl.DataSource = dtMasterScopes
        ddl.DataTextField = "Scope"
        ddl.DataValueField = "SNO"
        ddl.DataBind()
        ddl.Items.Insert(0, "--select--")
        If (Not String.IsNullOrEmpty(mscope)) Then
            ddl.SelectedItem.Text = mscope
        End If

    End Sub

    Private Sub BindTypeofWork(ByVal typeofwork As String, ByVal ddl As DropDownList)
        Dim dtTypeofwork As DataTable = objutil.ExeQueryDT("exec uspGetTypeofwork", "mastertypeofwork")
        ddl.DataSource = dtTypeofwork
        ddl.DataTextField = "Reason"
        ddl.DataValueField = "SNO"
        ddl.DataBind()

        ddl.Items.Insert(0, "--select--")
        If (Not String.IsNullOrEmpty(typeofwork)) Then
            ddl.SelectedItem.Text = typeofwork
        End If
    End Sub
#End Region
    
    
    'Sub InserttoLookup()
    '    If UCase(ddlBand.SelectedItem.Text) = UCase(Constants._Others) Then
    '        'insert to LOOKUP FOR BAND
    '        With ETLook
    '            .LKPCode = txtBand.Value
    '            .LKPDesc = txtBand.Value
    '            .GRP_ID = Constants._PO_Band
    '            .AT.RStatus = Constants.STATUS_ACTIVE
    '            .AT.LMBY = Session("User_Name")
    '        End With
    '        objboL.uspCODLookUpIU(ETLook)
    '    End If
    '    If UCase(ddlBType.SelectedItem.Text) = UCase(Constants._Others) Then
    '        'insert to LOOKUP FOR BANDTYPE
    '        With ETLook
    '            .LKPCode = txtBType.Value
    '            .LKPDesc = txtBType.Value
    '            .GRP_ID = Constants._PO_Band_Type
    '            .AT.RStatus = Constants.STATUS_ACTIVE
    '            .AT.LMBY = Session("User_Name")
    '        End With
    '        objboL.uspCODLookUpIU(ETLook)
    '    End If

    '    If UCase(ddlConfig.SelectedItem.Text) = UCase(Constants._Others) Then

    '        'insert to LOOKUP FOR CONFIG
    '        With ETLook
    '            .LKPCode = txtConfig.Value
    '            .LKPDesc = txtConfig.Value
    '            .GRP_ID = Constants._PO_Configuration
    '            .AT.RStatus = Constants.STATUS_ACTIVE
    '            .AT.LMBY = Session("User_Name")
    '        End With
    '        objboL.uspCODLookUpIU(ETLook)
    '    End If

    'End Sub

    
End Class
