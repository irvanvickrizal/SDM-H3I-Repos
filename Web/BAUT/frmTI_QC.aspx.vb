Imports System.Data
Imports System.IO
Imports Common
Imports Entities
Imports BusinessLogic
Imports QCFramework
Imports System.Collections.Generic
Imports NSNCustomizeConfiguration
Imports CRFramework
Imports Common_NSNFramework

Partial Class BAUT_frmTI_QC
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim objUtil As New DBUtil
    Dim str As String
    Dim cst As New Constants
    Dim objBO As New BOSiteDocs
    Dim docId As Integer = 0
    Dim i, j, k, l As Integer
    Dim strsql As String
    Dim objdo As New ETWFTransaction
    Dim dt1 As New DataTable
    Dim objET As New ETAuditTrail
    Dim objET1 As New ETSiteDoc
    Dim objBOAT As New BoAuditTrail
    Dim roleid As Integer
    Dim grp As String
    Dim FileNamePath As String
    Dim FileNameOnly As String
    Dim ReFileName As String
    Dim objmail As New TakeMail
    Dim objCommon As New CommonSite
    Dim kpicontrol As New KPIController
    Dim errcontroller As New HCPTController
    Dim wfcontroller As New WFGroupController
    Dim controller As New HCPTController
#Region "NSNFramework"
    Dim dbutils_nsn As New DBUtils_NSN
#End Region
    
    Public Sub RegisterScriptDescriptors(ByVal extenderControl As IExtenderControl)

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not IsPostBack Then
            If Not Request.QueryString("id") Is Nothing Then
                btnSubmitReject.Visible = False
                If Not String.IsNullOrEmpty(GetWPID()) Then
                    Binddata(GetWPID())
                End If

                'Added by Fauzan, 3 Dec 2018. Make The title report dynamic
                If hdndocid.Value = ConfigurationManager.AppSettings("KPIL2DOCID") Then
                    lblTitle.Text = "LEVEL 2"
                    lblSiteDesc.Text = "The contractor hereby certifies that relevant parts of the Works referred to this Level 2 Acceptance complies with the KPI Level 2 target passed, in particular the specification and this has successfully passed the Acceptance Tests(as defined in Annex E) on the Site."
                Else
                    lblTitle.Text = "LEVEL 0"
                    lblSiteDesc.Text = "The contractor hereby certifies that relevant parts of the Works referred to this Level 0 Acceptance complies with the KPI Level 0 target passed, in particular the specification and this has successfully passed the Acceptance Tests(as defined in Annex E) on the Site."
                End If

                dt = objBO.uspSiteTIDocList(Request.QueryString("id"))
                grdDocuments.DataSource = dt
                grdDocuments.DataBind()
                Dim dtGetATPDocList As DataTable = objUtil.ExeQueryDT("exec uspSiteGetATPDocList " & Request.QueryString("id"), "dt")
                grdDocuments.Columns(1).Visible = False
                grdDocuments.Columns(2).Visible = False
                grdDocuments.Columns(4).Visible = False
              
            End If
        End If
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
    End Sub

    Protected Sub BtnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        BindGVSiteDetailInformation(GetWPID())
        Dim getcoordinate As KPIInfo = kpicontrol.GetKPIInfo(GetWPID())
        If getcoordinate IsNot Nothing Then
            TxtLatitude.Text = getcoordinate.Latitude
            TxtLongitude.Text = getcoordinate.Longitude
        Else
            TxtLatitude.Text = ""
            TxtLongitude.Text = ""
        End If
    End Sub

    Protected Sub BtnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnClose.Click
        Dim info As New KPIInfo
        info.CMAInfo.LMBY = CommonSite.UserId
        info.Longitude = TxtLongitude.Text
        info.Latitude = TxtLatitude.Text
        info.SiteInf.PackageId = GetWPID()
        If kpicontrol.KPICoordinate_IU(info) = True Then
            BindSiteDetailInformation(GetWPID())
        End If
    End Sub

    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
        Dim strQuerCountNotYetUploaded As String = "select count(*) from sitedoc where isuploaded = 0 and siteid =" & hdnsiteid.Value & " and version=" & hdnversion.Value & _
            " and docid in(select doc_id from coddoc where parent_id = (select docid from sitedoc where sw_id=" & Request.QueryString("id") & "))"
        Dim countNotYetUploaded As Integer = objUtil.ExeQueryScalar(strQuerCountNotYetUploaded)
        Dim ATPAlreadyDone As Boolean = IIf(IsATPAlreadyDone(Convert.ToInt32(hdnsiteid.Value), Integer.Parse(hdnversion.Value)) = 0, False, True)
        ATPAlreadyDone = True
        If ATPAlreadyDone = True Then
            If Request.Browser.Browser = "IE" Then
                If countNotYetUploaded = 0 Then
                    lblPreBy.Text = Session("User_Name")
                    hdnRole.Value = Session("Role_Id")
                    If (hdnDGBox.Value = "") Then hdnDGBox.Value = 0
                    If (hdnKeyVal.Value = "") Then hdnKeyVal.Value = 0
                    If (hdnversion.Value = "") Then hdnversion.Value = 0
                    If hdnDGBox.Value = True Then
                        strsql = "select count(*) from docSignPositon where doc_id=" & hdndocid.Value
                        If objUtil.ExeQueryScalar(strsql) > 0 Then
                            BtnEdit.Visible = False
                            MvDetailCore.SetActiveView(VwList)
                            btnGenerate.Visible = False
                            grdDocuments.Visible = False
                            
                            uploaddocument(Convert.ToInt32(hdnversion.Value), Convert.ToInt32(hdnKeyVal.Value))

                            ActivityLog_I(CommonSite.UserId, BaseConfiguration.Activity_Generate_Doc & " KPI")
                            'Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
                        Else
                            Response.Write("<script>alert('please Set X,Y coordinates for Digital sign in document Template..')</script>")
                        End If
                    Else
                        BtnEdit.Visible = False
                        MvDetailCore.SetActiveView(VwList)
                        btnGenerate.Visible = False
                        grdDocuments.Visible = False
                        'GrdDocPanel.Visible = False
                        'GrdDocPanelPrint.Visible = True

                        Dim strLUpdate As String
                        If (txtLUpdate.Value.ToString() = "") Then
                            strLUpdate = "NULL"
                        Else
                            strLUpdate = txtLUpdate.Value.ToString()
                        End If
                        'If (hdSno.Value = 0) Then
                        '    str = "Exec uspODQCIU  0," & Request.QueryString("id") & ",'" & lblPO.Text & "','" & lblSiteID.Text & "'," & hdnversion.Value & ",'" & txtTWork.Text & "','" & txtNEType.Text & "','" & txtBSCName.Text & "','" & txtNSiteID.Text & "','" & txtLAC.Text & "','" & txtCI.Text & "','" & txtOnAirCon.Text & "'," & ChkDrive.Checked & "," & ChkKPI.Checked & "," & ChkFAlarm.Checked & "," & IIf(strLUpdate = "", "null", "'" & strLUpdate & "'") & ",'" & objCommon.UserName() & "','" & lblVer.Text & "'"
                        'Else
                        '    str = "Exec uspODQCIU  " & hdSno.Value & "," & Request.QueryString("id") & ",'" & lblPO.Text & "','" & lblSiteID.Text & "'," & hdnversion.Value & ",'" & txtTWork.Text & "','" & txtNEType.Text & "','" & txtBSCName.Text & "','" & txtNSiteID.Text & "','" & txtLAC.Text & "','" & txtCI.Text & "','" & txtOnAirCon.Text & "'," & ChkDrive.Checked & "," & ChkKPI.Checked & "," & ChkFAlarm.Checked & "," & IIf(strLUpdate = "", "null", "'" & strLUpdate & "'") & ",'" & objCommon.UserName() & "','" & lblVer.Text & "'"
                        'End If
                        'objUtil.ExeQueryScalar(str) 
                        uploaddocument(Integer.Parse(hdnversion.Value), Integer.Parse(hdnKeyVal.Value))
                    End If

                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Document not yet Completed');", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please use Internet Explorer');", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('ATP Document Not Yet Completed');", True)
        End If

    End Sub

#Region "Gridview Site Detail Information"

    Protected Sub GvSiteDetails_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvSiteDetails.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'getting username from particular row
            Dim desc As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Sector"))
            'identifying the control in gridview
            Dim lnkbtnresult As ImageButton = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
            'raising javascript confirmationbox whenver user clicks on link button
            If Not lnkbtnresult Is Nothing Then
                lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + desc + "')")
            End If

            ' Row Item Template
        End If
    End Sub

    Protected Sub GvSiteDetails_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName.Equals("AddNew") Then
            Dim txtSector As TextBox = CType(GvSiteDetails.FooterRow.FindControl("txtftSector"), TextBox)
            Dim txtCellID As TextBox = CType(GvSiteDetails.FooterRow.FindControl("txtftCellID"), TextBox)
            Dim txtAntennaType As TextBox = CType(GvSiteDetails.FooterRow.FindControl("txtftAntennaType"), TextBox)
            Dim txtAntennaHeight As TextBox = CType(GvSiteDetails.FooterRow.FindControl("txtftAntennaHeight"), TextBox)
            Dim txtAzimuth As TextBox = CType(GvSiteDetails.FooterRow.FindControl("txtftAzimuth"), TextBox)
            Dim txtMechTilt As TextBox = CType(GvSiteDetails.FooterRow.FindControl("txtftMechTilt"), TextBox)
            Dim txtElecTilt As TextBox = CType(GvSiteDetails.FooterRow.FindControl("txtftElecTilt"), TextBox)

            Dim info As New KPISiteDetailInfo
            info.DetailId = 0
            info.SiteInf.PackageId = GetWPID()
            info.Sector = Integer.Parse(txtSector.Text)
            info.CellId = txtCellID.Text
            info.AntennaType = txtAntennaType.Text
            info.AntennaHeight = Integer.Parse(txtAntennaHeight.Text)
            info.Azimuth = Integer.Parse(txtAzimuth.Text)
            info.MechTilt = Integer.Parse(txtMechTilt.Text)
            info.ElecTilt = Integer.Parse(txtElecTilt.Text)
            info.CMAInfo.LMBY = CommonSite.UserId
            If kpicontrol.SiteDetailInformation_IU(info) = True Then
                BindGVSiteDetailInformation(GetWPID())
            End If

        End If
    End Sub

    Protected Sub GvSiteDetails_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        GvSiteDetails.EditIndex = e.NewEditIndex
        BindGVSiteDetailInformation(GetWPID())
    End Sub

    Protected Sub GvSiteDetails_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim valueid As Int32 = Convert.ToInt32(GvSiteDetails.DataKeys(e.RowIndex).Value.ToString())
        Dim txtSector As TextBox = CType(GvSiteDetails.Rows(e.RowIndex).FindControl("txtSector"), TextBox)
        Dim txtCellID As TextBox = CType(GvSiteDetails.Rows(e.RowIndex).FindControl("txtCellID"), TextBox)
        Dim txtAntennaType As TextBox = CType(GvSiteDetails.Rows(e.RowIndex).FindControl("txtAntennaType"), TextBox)
        Dim txtAntennaHeight As TextBox = CType(GvSiteDetails.Rows(e.RowIndex).FindControl("txtAntennaHeight"), TextBox)
        Dim txtAzimuth As TextBox = CType(GvSiteDetails.Rows(e.RowIndex).FindControl("txtAzimuth"), TextBox)
        Dim txtMechTilt As TextBox = CType(GvSiteDetails.Rows(e.RowIndex).FindControl("txtMechTilt"), TextBox)
        Dim txtElecTilt As TextBox = CType(GvSiteDetails.Rows(e.RowIndex).FindControl("txtElecTilt"), TextBox)

        Dim info As New KPISiteDetailInfo
        info.DetailId = valueid
        info.SiteInf.PackageId = GetWPID()
        info.Sector = Integer.Parse(txtSector.Text)
        info.CellId = txtCellID.Text
        info.AntennaType = txtAntennaType.Text
        info.AntennaHeight = Integer.Parse(txtAntennaHeight.Text)
        info.Azimuth = Integer.Parse(txtAzimuth.Text)
        info.MechTilt = Integer.Parse(txtMechTilt.Text)
        info.ElecTilt = Integer.Parse(txtElecTilt.Text)
        info.CMAInfo.LMBY = CommonSite.UserId
        GvSiteDetails.EditIndex = -1
        If kpicontrol.SiteDetailInformation_IU(info) = True Then
            BindGVSiteDetailInformation(GetWPID())
        End If
    End Sub

    Protected Sub GvSiteDetails_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        GvSiteDetails.EditIndex = -1
        BindGVSiteDetailInformation(GetWPID())
    End Sub

    Protected Sub GvSiteDetails_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim valueid As Int32 = Convert.ToInt32(GvSiteDetails.DataKeys(e.RowIndex).Values("Detail_Id").ToString())
        If kpicontrol.SiteDetailInformation_D(valueid) = True Then
            BindGVSiteDetailInformation(GetWPID())
        End If
    End Sub

    Protected Sub GvSiteDetails_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvSiteDetails.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            'Creating a gridview object          
            Dim objGridView As GridView = CType(sender, GridView)
            'Creating a gridview row object
            Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)

            'Creating a table cell object
            Dim objtablecell As New TableCell
            AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.White.Name)
            AddMergedCells(objgridviewrow, objtablecell, 7, "Site Detail Information", System.Drawing.Color.Black.Name)
            'Lastly add the gridrow object to the gridview object at the 0th position
            'Because, the header row position is 0.
            objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
        End If
    End Sub

    Private Sub AddMergedCells(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, ByVal celltext As String, ByVal backcolor As String)
        objtablecell = New TableCell
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan
        objtablecell.Style.Add("background-color", backcolor)
        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub
#End Region

#Region "Custom Methods"
    Private Sub GetSiteAttribute(ByVal wpid As String)
        Dim info As SiteInfo = kpicontrol.GetSiteDetail_WPID(wpid, CommonSite.UserId)
        If info IsNot Nothing Then
            LblPono.Text = info.PONO
            lblSiteID.Text = info.SiteNo
            lblSiteName.Text = info.SiteName
            LblRegion.Text = info.RegionName
            LblBTSType.Text = info.ProjectID
            hdnScope.Value = info.Scope
        End If
    End Sub

    Private Sub GetSiteDOCAttribute(ByVal swid As String)
        str = "Exec HCPT_uspGeneral_GetDetailDoc " & swid
        Dim dtSiteDOCDetail As DataTable = objUtil.ExeQueryDT(str, "SiteDoc1")

        If dtSiteDOCDetail.Rows.Count > 0 Then
            hdnSiteno.Value = lblSiteID.Text
            hdnsiteid.Value = dtSiteDOCDetail.Rows(0).Item("SiteId").ToString
            hdnversion.Value = dtSiteDOCDetail.Rows(0).Item("version").ToString
            hdnWfId.Value = dtSiteDOCDetail.Rows(0).Item("WF_Id").ToString
            hdndocid.Value = dtSiteDOCDetail.Rows(0).Item("docid").ToString
            hdnDGBox.Value = dtSiteDOCDetail.Rows(0).Item("DGBox").ToString
        End If
    End Sub

    Private Sub BindNSNDGSignature(ByVal wfid As Integer, ByVal wpid As String)
        ' i = objBO.uspCheckIntegration(hdndocid.Value, hdnSiteno.Value)
        If New DocController().IsParentAlreadyUploaded(Convert.ToInt32(hdnsiteid.Value), Integer.Parse(hdnversion.Value), Integer.Parse(hdndocid.Value)) = True Then
            If New CODInjectionController().IsAvalaibleDocInjection(Integer.Parse(hdndocid.Value), Request.QueryString("wpid")) = False Then
                hdnKeyVal.Value = 0
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('nyreg');", True)
            End If
        End If

        str = "Exec [uspGetOnLineFormBind] " & dt.Rows(0).Item("WF_Id").ToString & "," & dt.Rows(0).Item("SiteId").ToString
        dt = objUtil.ExeQueryDT(str, "SiteDoc1")
        Dim dtSignPosition As DataView = dt.DefaultView
        dtSignPosition.Sort = "tsk_id desc"
        'DLDigitalSign.DataSource = dtSignPosition
        'DLDigitalSign.DataBind()
        HDDgSignTotal.Value = dt.Rows.Count
    End Sub

    Private Sub Binddata(ByVal wpid As String)
        'Dim info As KPIInfo = kpicontrol.GetKPIInfo(wpid)
        LblDateofCreation.Text = String.Format("{0:dd-MMM-yyyy}", Date.Now())
        GetSiteAttribute(GetWPID())
        GetSiteDOCAttribute(Request.QueryString("id"))
        BindSiteDetailInformation(GetWPID())
        BindFinalApprovers(GetWPID(), 1, 0, 0, Integer.Parse(hdnWfId.Value), "approver", DLDigitalSign_NSNOnly)
        BindFinalApprovers(GetWPID(), 11, 4, 0, Integer.Parse(hdnWfId.Value), "approver", DdlDigitalSignature_Customer)
        i = 1
        Select Case i
            Case 1
                Dochecking()
            Case 2
                hdnKeyVal.Value = 0
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('Int');", True)
            Case 3
                Dochecking()
            Case 4
                hdnKeyVal.Value = 0
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptI", "Showmain('IntD');", True)
            Case 3
        End Select

        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
    End Sub

    Private Sub BindSiteDetailInformation(ByVal wpid As String)
        MvDetailCore.SetActiveView(VwList)
        Dim strTableSiteDetailInfo As New StringBuilder
        strTableSiteDetailInfo.Append("<table cellspacing=""0"" rules=""all"" border=""1"" id=""GvSiteDetails"" style=""border-color:Black;width:750px;border-collapse:collapse;"">")
        strTableSiteDetailInfo.Append("<tr class=""HeaderGrid2"" style=""font-weight:bold;"">")
        strTableSiteDetailInfo.Append("<td align=""center"" colspan=""7"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;"">Site Detail Information</td>")
        strTableSiteDetailInfo.Append("</tr>")
        strTableSiteDetailInfo.Append("<tr class=""HeaderGrid2"" style=""font-weight:bold;"">")
        strTableSiteDetailInfo.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;"">Sector</td>")
        strTableSiteDetailInfo.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;"">Cell ID</td>")
        strTableSiteDetailInfo.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;"">Antenna Type</td>")
        strTableSiteDetailInfo.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;"">Antenna Height(M)</td>")
        strTableSiteDetailInfo.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;"">Azimuth</td>")
        strTableSiteDetailInfo.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;"">Mech Tilt</td>")
        strTableSiteDetailInfo.Append("<td align=""center"" style=""background-color:White;border-color:black;border-style:solid;border-width:1px;"">Elec Tilt</td>")
        strTableSiteDetailInfo.Append("</tr>")

        Dim getdetails As List(Of KPISiteDetailInfo) = kpicontrol.GetSiteDetailInformations(wpid, 0)

        If getdetails.Count > 0 Then
            For Each detailinfo As KPISiteDetailInfo In getdetails
                strTableSiteDetailInfo.Append("<tr class=""oddGrid2"">")
                strTableSiteDetailInfo.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:80px;""><span class=""lblFieldSmall"">" & detailinfo.Sector & "</span></td>")
                strTableSiteDetailInfo.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:80px;""><span class=""lblFieldSmall"">" & detailinfo.CellId & "</span></td>")
                strTableSiteDetailInfo.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:100px;""><span class=""lblFieldSmall"">" & detailinfo.AntennaType & "</span></td>")
                strTableSiteDetailInfo.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:40px;""><span class=""lblFieldSmall"">" & detailinfo.AntennaHeight & "</span></td>")
                strTableSiteDetailInfo.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:40px;""><span class=""lblFieldSmall"">" & detailinfo.Azimuth & "</span></td>")
                strTableSiteDetailInfo.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:40px;""><span class=""lblFieldSmall"">" & detailinfo.MechTilt & "</span></td>")
                strTableSiteDetailInfo.Append("<td style=""border-color:Black;border-width:1px;border-style:solid;width:40px;""><span class=""lblFieldSmall"">" & detailinfo.ElecTilt & "</span></td>")
                strTableSiteDetailInfo.Append("</tr>")
            Next
        Else
            strTableSiteDetailInfo.Append("<tr class=""oddGrid2"">")
            strTableSiteDetailInfo.Append("<td colspan=""7"" style=""border-color:Black;border-width:1px;border-style:solid;width:85px;""><span class=""lblFieldSmall"">No Site Detail Information list</span></td>")
            strTableSiteDetailInfo.Append("</tr>")
        End If
        Dim getcoordinate As KPIInfo = kpicontrol.GetKPIInfo(GetWPID())
        strTableSiteDetailInfo.Append("<tr class=""oddGrid2"">")
        If getcoordinate IsNot Nothing Then
            strTableSiteDetailInfo.Append("<td colspan=""3"" style=""border-color:Black;border-width:1px;border-style:solid;width:85px;text-align:center;""><span class=""lblFieldSmall"">Lon:" & getcoordinate.Longitude & "</span></td>")
            strTableSiteDetailInfo.Append("<td colspan=""4"" style=""border-color:Black;border-width:1px;border-style:solid;width:85px;text-align:center;""><span class=""lblFieldSmall"">Lat:" & getcoordinate.Latitude & "</span></td>")
        Else
            strTableSiteDetailInfo.Append("<td colspan=""3"" style=""border-color:Black;border-width:1px;border-style:solid;width:85px;text-align:center;""><span class=""lblFieldSmall"">Lon:0</span></td>")
            strTableSiteDetailInfo.Append("<td colspan=""4"" style=""border-color:Black;border-width:1px;border-style:solid;width:85px;text-align:center;""><span class=""lblFieldSmall"">Lat:0</span></td>")
        End If
        
        strTableSiteDetailInfo.Append("</tr>")
        strTableSiteDetailInfo.Append("</table>")
        SiteDetailInfoTables.InnerHtml = strTableSiteDetailInfo.ToString()
    End Sub

    Private Sub BindGVSiteDetailInformation(ByVal wpid As String)
        MvDetailCore.SetActiveView(VwEdit)
        Dim ds As DataSet = kpicontrol.GetSiteDetailInformations_DS(wpid, 0)
        If ds.Tables(0).Rows.Count() > 0 Then
            GvSiteDetails.DataSource = ds
            GvSiteDetails.DataBind()
        Else
            ds.Tables(0).Rows.Clear()
            ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
            GvSiteDetails.DataSource = ds
            GvSiteDetails.DataBind()
            Dim columncount As Integer = GvSiteDetails.Rows(0).Cells.Count
            GvSiteDetails.Rows(0).Cells.Clear()
            GvSiteDetails.Rows(0).Cells.Add(New TableCell())
            GvSiteDetails.Rows(0).Cells(0).ColumnSpan = columncount
            GvSiteDetails.Rows(0).Cells(0).Text = "No Records Found"
        End If
    End Sub

    Private Sub Dochecking()
        roleid = Session("Role_Id")
        grp = Session("User_Type")
        If objBO.uspApprRequired(hdnsiteid.Value, hdndocid.Value, hdnversion.Value) <> 0 Then
            If objBO.verifypermission(hdndocid.Value, roleid, grp) <> 0 Then
                Select Case objBO.DocUploadverify(hdnsiteid.Value, hdndocid.Value, hdnversion.Value).ToString
                    Case 1  ''This document not attached to this site
                        hdnKeyVal.Value = 1
                        btnGenerate.Attributes.Clear()
                        'belowcase not going to happen we need to test the scenariao.
                        btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to upload?')")
                    Case 2 '' means document was already uploaded for this version of site,do u want to overwrite
                        hdnKeyVal.Value = 2
                        btnGenerate.Attributes.Clear()
                        btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                    Case 3 '' means document was not yet uploaded for thissite
                        hdnKeyVal.Value = 3
                        btnGenerate.Attributes.Clear()
                    Case 4 'means document already processed for sencod stage cannot upload
                        hdnKeyVal.Value = 0
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScripta", "Showmain('2sta');", True)
                        'makevisible()
                        Exit Sub
                        'Response.Redirect("frmSiteDocUploadTree.aspx")
                End Select
            Else
                If Session("Role_Id") = 1 Then ''if role is sysadmin then no need to verify permission everthing permitted.
                    Select Case objBO.DocUploadverify(hdnsiteid.Value, hdndocid.Value, hdnversion.Value).ToString
                        Case 1  ''This document not attached to this site
                            hdnKeyVal.Value = 1
                            btnGenerate.Attributes.Clear()
                            'belowcase not going to happen we need to test the scenariao.
                            btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to  upload?')")
                        Case 2 '' means document was already uploaded for this version of site,do u want to overwrite
                            hdnKeyVal.Value = 2
                            btnGenerate.Attributes.Clear()
                            btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                        Case 3 '' means document was not yet uploaded for thissite
                            hdnKeyVal.Value = 3
                            btnGenerate.Attributes.Clear()
                        Case 4 'means document already processed for sencod stage cannot upload
                            hdnKeyVal.Value = 0
                            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptb", "Showmain('2sta');", True)
                            Exit Sub
                            'Response.Redirect("frmSiteDocUploadTree.aspx")
                    End Select
                Else
                    'Page.FindControl("Panel1").Visible = False
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptc", "Showmain('nop');", True)
                End If
            End If
        Else 'Seeta 20081230 Appr Not Required
            Select Case objBO.DocUploadverify(hdnsiteid.Value, hdndocid.Value, hdnversion.Value).ToString
                Case 1  ''This document not attached to this site
                    hdnKeyVal.Value = 1
                    btnGenerate.Attributes.Clear()
                    'belowcase not going to happen we need to test the scenariao.
                    btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to  upload?')")
                Case 2 '' means document was already uploaded for this version of site,do u want to overwrite
                    hdnKeyVal.Value = 2
                    btnGenerate.Attributes.Clear()
                    btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                Case 3 '' means document was not yet uploaded for thissite
                    hdnKeyVal.Value = 3
                    btnGenerate.Attributes.Clear()
                Case 4 'means document already processed for sencod stage cannot upload
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptd", "Showmain('2sta');", True)
                    Exit Sub
            End Select
        End If
    End Sub

    Private Function CreatePDFFile(ByVal StrPath As String) As String
        StrPath = StrPath.Replace(" \", "\").ToString 'bugfix100816 causing error when the path containing space before \
        Dim dtba As New DataTable
        Dim filenameorg As String
        Dim ReFileName As String
        filenameorg = hdnSiteno.Value & "-" & "KPILVL" & "-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        ReFileName = filenameorg & ".htm"
        If (System.IO.File.Exists(StrPath & ReFileName)) Then
            System.IO.File.Delete(StrPath & ReFileName)
        End If
        If Not System.IO.Directory.Exists(StrPath) Then
            System.IO.Directory.CreateDirectory(StrPath)
        End If
        Dim sw As HtmlTextWriter = New HtmlTextWriter(New StreamWriter(StrPath & ReFileName, False, System.Text.UnicodeEncoding.UTF8))
        sw.WriteLine("<html><head><style type=""text/css"">")
        sw.WriteLine("tr{padding: 3px;}")
        sw.WriteLine(".MainCSS{margin-bottom: 0px;margin-left: 20px;margin-right: 20px;margin-top: 0px;width: 800px;height: 700px;text-align: center;}")
        sw.WriteLine("#dvPrint{width:800px;}")
        sw.WriteLine(".lblText{font-family: verdana;font-size: 7.5pt;color: #000000;text-align: left;}")
        sw.WriteLine(".lblBText{font-family: verdana;font-size: 7.5pt;color: #000000;text-align: left;font-weight: bold;}")
        sw.WriteLine(".lblBold{font-family: verdana;font-size: 12pt;color: #000000;font-weight: bold;}")
        sw.WriteLine(".lblTextHeader{font-family: verdana;font-size: 18px;color: #000000;font-weight: bolder;}")
        sw.WriteLine(".lblTextField{font-family: verdana;font-size: 11px;color: #000000;border-style: none; border-width:0px;border-color:White;}")
        sw.WriteLine(".lblTextSmall{font-family: verdana;font-size: 10px;color: #000000;}")
        sw.WriteLine(".lblField{font-family: verdana;font-size: 11px;font-weight: bolder;color: #000;}")
        sw.WriteLine(".lblFieldSmall{font-family: verdana;font-size: 9px;color: #000000;}")
        sw.WriteLine(".lblFieldSmallFooter{font-family: verdana;font-size: 9px;font-weight: bolder;color: #000000;}")
        sw.WriteLine(".textFieldStyle{background-color: white;border: 1px solid;color: black;font-family: verdana;font-size: 9pt;}")
        sw.WriteLine(".GridHeader{color: #0e1b42;background-color: Orange;font-size: 9pt;font-family: Verdana;text-align: Left;vertical-align: bottom;font-weight: bold;}")
        sw.WriteLine(".GridEvenRows{background-color: #e3e3e3;vertical-align: top;font-family: verdana;font-size: 7.5pt;color: #000000;}")
        sw.WriteLine(".GridOddRows{background-color: white;vertical-align: top;font-family: verdana;font-size: 7.5pt;color: #000000;}")
        sw.WriteLine(".GrdDocPanelRows{background-color: white;vertical-align: middle;font-family: verdana;font-size: 7.5pt;color: #000000;}")
        sw.WriteLine(".HeaderGrid2{font-family: Verdana;font-size: 10px;font-weight: bold;color: #000000;background-color: #ffffff;border-color: black;border-style: solid;border-width: 1px;vertical-align: middle;padding-top: 5px;padding-bottom: 5px;}")
        sw.WriteLine(".oddGrid2{font-family: Verdana;font-size: 8px;background-color: White;border-color: black;border-style: solid;border-width: 1px;}")
        sw.WriteLine(".evenGrid2{font-family: Verdana;font-size: 8px;background-color: White;border-color: black;border-style: solid;border-width: 1px;}")
        sw.WriteLine(".PagerTitle{font-size: 8pt;background-color: #cddbbf;text-align: Right;vertical-align: middle;color: #25375b;font-weight: bold;}")
        sw.WriteLine(".Hcap{height: 5px;}")
        sw.WriteLine(".VCap{width: 10px;}")
        sw.WriteLine(".siteATTPanel{margin-top:10px;  height:120px; text-align:left;}")
        sw.WriteLine(".sitedescription{height:30px;margin-top: 10px;width: 800px; text-align:left;}")
        sw.WriteLine(".SiteDetailInfoPanel{margin-top: 10px;width: 100%;text-align: left;height: 150px;}")
        sw.WriteLine(".headerform{margin-top: 15px;height: 60px;}")
        sw.WriteLine(".pnlremarks{width: 100%; margin-top: 5px;text-align:left; height:10px; font-family:verdana; font-size:7.5pt;}")
        sw.WriteLine(".footerPanel{margin-top: 10px; height: 350px;text-align:left;}")
        sw.WriteLine("</style></head>")
        sw.WriteLine("<body class=""MainCSS"">")
        dvPrint.RenderControl(sw)
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        Return EBASTFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName, StrPath, filenameorg)
        'Return ReFileName & ".pdf"
    End Function

    Private Sub uploaddocument(ByVal vers As Integer, ByVal keyval As Integer)
        dt = objBO.getbautdocdetailsNEW(hdndocid.Value) '(Constants._Doc_SSR)
        Dim sec As String = dt.Rows(0).Item("sec_name").ToString
        Dim subsec As String = dt.Rows(0).Item("subsec_name").ToString
        Dim secpath As String = ""
        Dim ft As String = ""
        Dim path As String = ""
        Dim filenameorg As String
        FileNameOnly = "-" & "KPILVL" & "-"
        filenameorg = hdnSiteno.Value & FileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        ReFileName = filenameorg & ".htm"
        secpath = IIf(subsec = "" Or subsec = "0", sec & "\", sec & "\" & subsec & "\")
        ft = ConfigurationManager.AppSettings("Type") & hdnScope.Value & "-" & GetWPID() & "\"
        path = ConfigurationManager.AppSettings("Fpath") & hdnSiteno.Value & ft & secpath
        Dim strResult As String = DOInsertTrans(hdnsiteid.Value, hdndocid.Value, vers, path)
        'Dim strResult As String = "1"
        Dim DocPath As String = ""
        If strResult = "0" Then
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        ElseIf strResult = "1" Then
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        Else
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        End If
        'For offline testing mode
        'DocPath = hdnSiteno.Value & ft & secpath

        Response.Write(DocPath)
        With objET1
            .SiteID = hdnsiteid.Value
            .DocId = hdndocid.Value
            .IsUploded = 1
            .Version = vers
            .keyval = keyval
            .DocPath = DocPath
            .AT.RStatus = Constants.STATUS_ACTIVE
            .AT.LMBY = Session("User_Id")
            .orgDocPath = DocPath 'hdnSiteno.Value & ft & secpath & ReFileName
            .PONo = LblPono.Text
        End With
        objBO.updatedocupload(objET1)
        'Dim strsql As String = "Update bautmaster set Pstatus=1 where Site_ID = " & hdnsiteid.Value & " and SiteVersion =" & hdnversion.Value & " and PONO = '" & lblPO.Text & "'"
        'objUtil.ExeUpdate(strsql)
        'sendmail2()
        chek4alldoc() ' for messaage to previous screen ' and saving final docupload date in reporttable
        'for BAUT
        'objBo.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 1, lblPONO.Text, Session("User_Name"))
        'objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 1 & ",'" & lblPO.Text & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
        'for BAST1
        'objBo.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 2, lblPONO.Text, Session("User_Name"))
        'objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 2 & ",'" & lblPO.Text & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
        'Fill Transaction table
        AuditTrail()
        SendGenerateMail()
        objmail.SendApprovalMailNotification(GetWPID(), hdndocid.Value)         'Added by Fauzan 28 Nov 2018. Email Approver.
        If hdnready4baut.Value = 1 Then
            If String.IsNullOrEmpty(Request.QueryString("from")) Then
                Response.Redirect("../PO/frmSiteDocUploadTree.aspx?ready=yes")
            Else
                Response.Redirect("../PO/frmQCReadyCreation.aspx")
            End If
        Else
            If String.IsNullOrEmpty(Request.QueryString("from")) Then
                Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
            Else
                Response.Redirect("../PO/frmQCReadyCreation.aspx")
            End If
        End If
    End Sub

    Private Function DOInsertTrans(ByVal siteid As String, ByVal docName As String, ByVal version As Integer, ByVal path As String) As String
        docId = hdndocid.Value
        Dim wfcontrol As New CRController
        Dim isSucceed As Boolean = True
        Dim dtNew As DataTable
        If hdnWfId.Value <> 0 Then
            dtNew = wfcontrol.GetWorkflowDetail(Integer.Parse(hdnWfId.Value))
            If hdnDGBox.Value = False Then
                If dtNew.Rows.Count > 0 Then
                    'objBO.DelWFTransaction(hdndocid.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
                    If controller.WFTransaction_D(GetWPID(), Integer.Parse(hdndocid.Value)) = True Then
                        Dim aa As Integer = 0
                        Dim sorder As Integer
                        For aa = 0 To dtNew.Rows.Count - 1
                            'fillDetails()
                            sorder = dtNew.Rows(aa).Item("sorder")
                            Dim transinfo As New DOCTransactionInfo
                            transinfo.TaskId = Integer.Parse(dtNew.Rows(aa).Item("Tsk_id").ToString())
                            transinfo.SiteInf.PackageId = GetWPID()
                            transinfo.DocInf.DocId = docId
                            transinfo.WFID = Integer.Parse(hdnWfId.Value)
                            transinfo.UGPID = Integer.Parse(dtNew.Rows(aa).Item("GrpId").ToString())
                            transinfo.PageNo = 1
                            transinfo.RStatus = 2
                            transinfo.RoleInf.RoleId = Integer.Parse(dtNew.Rows(aa).Item("RoleId").ToString())
                            transinfo.CMAInfo.LMBY = CommonSite.UserId
                            transinfo.Status = 1
                            If sorder = 1 Then
                                transinfo.StartDateTime = Date.Now()
                                transinfo.EndDateTime = Date.Now()
                                transinfo.Status = 0
                            ElseIf sorder = 2 Then
                                transinfo.StartDateTime = Date.Now()
                            Else
                                transinfo.StartDateTime = Nothing
                                transinfo.EndDateTime = Nothing
                            End If

                            If New WFGroupController().IsTaskApprover(transinfo.TaskId) = True Then
                                If transinfo.UGPID = 1 Then
                                    transinfo.Xval = 65
                                    transinfo.Yval = 375
                                ElseIf transinfo.UGPID = 4 Then
                                    transinfo.Xval = 65
                                    transinfo.Yval = 355
                                ElseIf transinfo.UGPID = 11 Then
                                    transinfo.Xval = 315
                                    transinfo.Yval = 255
                                Else
                                    transinfo.Xval = 0
                                    transinfo.Yval = 0
                                End If
                            End If

                            If controller.WFTransaction_I(transinfo) = False Then
                                isSucceed = False
                                controller.WFTransaction_D(GetWPID, Integer.Parse(hdndocid.Value))
                                Exit For
                            End If
                        Next
                    End If
                End If
                Return "1"
            Else
                CreateXY()
                Return "1"
            End If
        Else
            Dim status As Integer = 99
            objBO.uspwftransactionNOTWFI(docId, 0, hdnsiteid.Value, hdnversion.Value, 1, Session("User_Id"), status, 2, Session("User_Name"))
            Return "0"
        End If
    End Function

    Private Sub BindFinalApprovers(ByVal packageid As String, ByVal grpid1 As Integer, ByVal grpid2 As Integer, ByVal grpid3 As Integer, ByVal wfid As Integer, ByVal taskdesc As String, ByVal dglist As DataList)
        dglist.DataSource = controller.GetFinalApprovers(packageid, grpid1, grpid2, grpid3, wfid, taskdesc)
        dglist.DataBind()
    End Sub

#End Region


    

    Protected Sub DOThis(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rad As RadioButtonList = CType(sender, RadioButtonList)
        Dim grdr As GridViewRow = CType(rad.NamingContainer, GridViewRow)
        Dim x As Integer = grdr.RowIndex
        Dim rdl As RadioButtonList = grdDocuments.Rows(x).Cells(5).FindControl("rdbstatus")
        Dim txt1 As TextBox = grdDocuments.Rows(x).Cells(6).FindControl("txtremarks")
        'txt1.Text = "Please enter remarks here......"
        If rdl.SelectedValue = 0 Then  'approve
            txt1.Visible = False
            btnSubmitReject.Visible = False
            btnGenerate.Visible = True
        Else 'reject
            txt1.Visible = True
            btnSubmitReject.Visible = True
            btnGenerate.Visible = False
        End If
    End Sub

    Protected Sub BtnSubmitReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmitReject.Click
        Dim docReject As Boolean = False
        Dim isCorrectLoop As Boolean = True
        Dim strWarningMessage As String = String.Empty
        Dim listdocrejects As New List(Of DocumentRejectInfo)
        For intCount As Integer = 0 To grdDocuments.Rows.Count - 1
            Dim rdl1 As RadioButtonList = CType(grdDocuments.Rows(intCount).FindControl("rdbstatus"), RadioButtonList)
            If rdl1.SelectedValue = 1 Then
                Dim swid As Label = CType(grdDocuments.Rows(intCount).FindControl("LblSWId"), Label)
                Dim docname As Label = CType(grdDocuments.Rows(intCount).FindControl("LblDocName"), Label)
                Dim docid As Label = CType(grdDocuments.Rows(intCount).FindControl("LblDocid"), Label)
                Dim txtRemarks As TextBox = CType(grdDocuments.Rows(intCount).FindControl("txtRemarks"), TextBox)
                If String.IsNullOrEmpty(txtRemarks.Text) Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptd", "alert('Please put remarks of rejection for " & docname.Text & " document');", True)
                    isCorrectLoop = False
                    Exit For
                Else
                    Dim docrejectinfo As New DocumentRejectInfo
                    docrejectinfo.Docid = Integer.Parse(docid.Text)
                    docrejectinfo.Docname = docname.Text
                    docrejectinfo.SWId = Convert.ToInt32(swid.Text)
                    docrejectinfo.Remarks = txtRemarks.Text
                    docrejectinfo.LMBY = objUtil.ExeQueryScalarString("select top 1 lmby from wftransaction where site_id=" & hdnsiteid.Value & " and siteversion=" & hdnversion.Value & " and rstatus = 2 and docid=" & docid.Text & " order by enddatetime desc")
                    listdocrejects.Add(docrejectinfo)
                End If
            End If
        Next

        If isCorrectLoop = True Then
            If listdocrejects.Count > 0 Then
                DocumentReject(listdocrejects)
            End If
        End If
    End Sub

    Protected Sub grddocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdDocuments.PageIndex * grdDocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim url As String = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(4).Text
            If e.Row.Cells(2).Text <> "" And e.Row.Cells(2).Text <> "&nbsp;" Then
                e.Row.Cells(3).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
            Else
                e.Row.Cells(3).Text = e.Row.Cells(3).Text
            End If
        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub

    Private Sub DocumentReject(ByVal listdocrejects As List(Of DocumentRejectInfo))
        Dim data As DocumentRejectInfo
        For Each data In listdocrejects
            Try
                objUtil.ExeNonQuery("Exec uspBAUTDocReject " & data.SWId & ",'" & data.Remarks & "'," & CommonSite.UserId & "," & CommonSite.RollId & ",'" & CommonSite.UserName & "','" & LblPono.Text & "','" & hdnsiteid.Value & "'," & data.Docid & "," & ConfigurationManager.AppSettings("WCTRBASTID") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID"))
                SendMailDocumentReject(data.Docname, data.Remarks, data.LMBY)
            Catch ex As Exception
                objUtil.ExeNonQuery("exec uspErrLog '', 'Document Reject','" & ex.Message.ToString.Replace("'", "''") & "','sendmailrejectedQCdoc'")
            End Try
        Next

        If String.IsNullOrEmpty(Request.QueryString("from")) Then
            Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
        Else
            Response.Redirect("../PO/frmQCReadyCreation.aspx")
        End If

    End Sub

    Private Sub SendMailDocumentReject(ByVal docname As String, ByVal remarks As String, ByVal lmby As String)
        Dim sbBody As New StringBuilder
        Dim strQuery As String = "select name,phoneNo,email from ebastusers_1 where usr_id=" & lmby
        Dim dtUsers As DataTable = objUtil.ExeQueryDT(strQuery, "dt")
        Dim strCompanyRejected As String = String.Empty
        If CommonSite.UserType.ToLower() = "c" Then
            strCompanyRejected = "Telkomsel"
        ElseIf CommonSite.UserType.ToLower() = "n" Then
            strCompanyRejected = "NSN"
        Else
            strCompanyRejected = "Subcon"
        End If
        If dtUsers.Rows.Count > 0 Then
            sbBody.Append("Dear " & dtUsers.Rows(0).Item(0) & ", <br/><br/>")
            sbBody.Append("There is " & docname & " document of " & LblPono.Text & "-" & lblSiteID.Text & "-" & GetWPID() & " is Rejected by " & CommonSite.UserName & " " & strCompanyRejected & "<br/>")
            sbBody.Append("Reason of Rejection : " & remarks & "<br/><br/>")
            sbBody.Append("<a href='http://www.telkomsel.nsnebast.com'>Click here</a>" & " to login to e-BAST <br/>")
            sbBody.Append("Powered By EBAST" & "<br/>")
            sbBody.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
            objmail.SendMailQC(dtUsers.Rows(0).Item(2).ToString(), sbBody.ToString(), docname & " document Rejected")
        End If
    End Sub

    Public Sub chek4alldoc()
        Dim i As Integer
        i = objBO.chek4alldoc(hdnsiteid.Value, hdnversion.Value)
        If i = 0 Then
            hdnready4baut.Value = 1
            'insert to new table for report as final document uploaded
            objBO.uspRPTUpdate(LblPono.Text, hdnSiteno.Value)
        Else
            hdnready4baut.Value = 0
        End If
    End Sub

    Sub AuditTrail()
        objET.PoNo = LblPono.Text
        objET.SiteId = hdnsiteid.Value
        objET.DocId = hdndocid.Value
        objET.Version = hdnversion.Value
        objET.Task = "1"
        objET.Status = "1"
        objET.Userid = Session("User_Id")
        objET.Roleid = Session("Role_Id")
        objET.fldtype = hdnScope.Value
        dbutils_nsn.InsertAuditTrailNew(objET, GetWPID())
    End Sub

    

    Sub XYCo()
        'For IncrMentX As Integer = 0 To DLDigitalSign.Items.Count - 1
        '    Dim iHDX As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdXCoordinate"), HiddenField)
        '    Dim iHDY As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdYCoordinate"), HiddenField)
        '    Dim Y As Integer = (Math.Ceiling(iHDY.Value / 800))
        '    If Y = 0 Then Y = 1
        '    Response.Write(iHDX.Value.ToString + "Y-" + iHDY.Value.ToString + "p-" + Y.ToString + "<br>")
        'Next
    End Sub

    Private Sub SendGenerateMail()
        Dim sbBody As New StringBuilder
        Dim strQuery As String = String.Empty
        strQuery = "select usr.usr_id, email,Name,phoneNo from ebastusers_1 usr " & _
                   "inner join ebastuserrole rol on usr.usr_id = rol.usr_id where usrRole in(" & _
                   "select roleid from twfdefinition where wfid = (select wf_id from sitedoc where sw_id = " & Request.QueryString("id") & ") and sorder = 2) " & _
                   "and rgn_id in(select rgn_id from codsite where site_id = (select siteid from sitedoc where sw_id =" & Request.QueryString("id") & "))"

        Dim dtuseremail As DataTable = objUtil.ExeQueryDT(strQuery, "useratt")
        If dtuseremail.Rows.Count > 0 Then
            sbBody.Append("Dear " & dtuseremail.Rows(0).Item(2) & ", <br/><br/>")
            sbBody.Append("There is KPI Certificate of " & LblPono.Text & "-" & lblSiteID.Text & "-" & GetWPID() & " Waiting for your approval " & "<br/><br />")
            sbBody.Append("<a href='http://www.telkomsel.nsnebast.com'>Click here</a>" & " to login to e-RFT <br/>")
            sbBody.Append("Powered By EBAST" & "<br/>")
            sbBody.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
            objmail.SendMailQC(dtuseremail.Rows(0).Item(1).ToString(), sbBody.ToString(), "KPI Document Waiting")
        End If

    End Sub

#Region "Custom Methods"
    Private Sub CreateXY()
        Dim dtNew As DataTable
        Dim strSql1 As String = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
        "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        " where tt.TSK_Id=1 and wfid=" & hdnWfId.Value & " order by wfdid"
        dtNew = objUtil.ExeQueryDT(strSql1, "dd")
        ' dtNew = objdbutil.ExeQueryScalar("select wf_id from sitedoc where siteid='" & siteid & "' and docid=" & docid & " and version =" & version & "")
        If dtNew.Rows.Count > 0 Then
            objBO.DelWFTransaction(hdndocid.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
        End If
        Dim status As Integer = 0
        Dim DtNewOne As DataTable
        Dim dvIn As New DataView
        Dim dvNotIn As New DataView
        Dim J As Integer = 1
        Dim Y As Integer = 0, TopK As Integer = 0
        dvIn = dtNew.DefaultView
        dvIn.RowFilter = "TSK_Id=1"
        For TopK = 0 To dvIn.Count - 1
            fillDetails()
            objdo.Status = status
            objdo.UserType = dvIn(TopK).Item(3).ToString
            objdo.UsrRole = dvIn(TopK).Item(4).ToString
            objdo.WFId = dvIn(TopK).Item(1).ToString
            objdo.TSK_Id = dvIn(TopK).Item(5).ToString
            objdo.UGP_Id = dvIn(TopK).Item("grpId").ToString
            objdo.XVal = 0
            objdo.YVal = 0
            objdo.PageNo = 0
            objdo.Site_Id = hdnsiteid.Value
            objBO.uspwftransactionIU(objdo)
        Next
        strSql1 = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
        "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        " where tt.TSK_Id <> 1 and wfid=" & hdnWfId.Value & "   order by sorder"
        DtNewOne = objUtil.ExeQueryDT(strSql1, "dd")
        dvNotIn = DtNewOne.DefaultView
        dvNotIn.RowFilter = "TSK_Id <>1"
        status = 1
        Dim intCount As Integer = 0, IncrMentY As Integer = 0
        For IncrMentX As Integer = 0 To dvNotIn.Count - 1
            fillDetails()
            objdo.Status = status
            objdo.UserType = dvNotIn(IncrMentX).Item(3).ToString
            objdo.UsrRole = dvNotIn(IncrMentX).Item(4).ToString
            objdo.WFId = dvNotIn(IncrMentX).Item(1).ToString
            objdo.TSK_Id = dvNotIn(IncrMentX).Item(5).ToString
            objdo.UGP_Id = dvNotIn(IncrMentX).Item("grpId").ToString
            objdo.XVal = 0
            objdo.YVal = 0
            objdo.PageNo = 0
            objdo.Site_Id = hdnsiteid.Value
            objBO.uspwftransactionIU(objdo)
        Next

        Dim dtnew2 As DataTable
        Dim dvnotin2 As DataView

        strSql1 = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
        "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        " where tt.TSK_Id not in (1,5) and wfid=" & hdnWfId.Value & "   order by wfdid"
        dtnew2 = objUtil.ExeQueryDT(strSql1, "dd")
        dvnotin2 = dtnew2.DefaultView
        dvnotin2.RowFilter = "TSK_Id <>1"
        dvnotin2.Sort = "TSK_Id desc"
        status = 1

        For IncrMentX As Integer = 0 To dvnotin2.Count - 1
            'Dim iHDX As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdXCoordinate"), HiddenField)
            'Dim iHDY As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdYCoordinate"), HiddenField)
            fillDetails()
            objdo.Status = status
            objdo.UserType = dvnotin2(IncrMentX).Item(3).ToString
            objdo.UsrRole = dvnotin2(IncrMentX).Item(4).ToString
            objdo.WFId = dvnotin2(IncrMentX).Item(1).ToString
            objdo.TSK_Id = dvnotin2(IncrMentX).Item(5).ToString
            objdo.UGP_Id = dvnotin2(IncrMentX).Item("grpId").ToString
            objdo.PageNo = 1
            Dim strversion As String = Request.Browser.Type
            If InStr(strversion, "IE6") > 0 Or InStr(strversion, "IE7") > 0 Or InStr(strversion, "IE8") > 0 Or InStr(strversion, "IE9") > 0 Then
                If IncrMentX = 0 Then
                    objdo.XVal = 15
                Else
                    objdo.XVal = 300
                End If
            End If
            objdo.YVal = 210
            objUtil.ExeNonQuery("update wftransaction set xval=" & objdo.XVal & ",yval=" & objdo.YVal & ", pageno=" & objdo.PageNo & " where site_id= " & objdo.Site_Id & " and siteversion= " & objdo.SiteVersion & " and docid= " & objdo.DocId & " and tsk_id=" & objdo.TSK_Id & "")
        Next
    End Sub

    Private Sub fillDetails()
        objdo.DocId = hdndocid.Value
        objdo.Site_Id = hdnsiteid.Value
        objdo.SiteVersion = hdnversion.Value
        objdo.AT.RStatus = Constants.STATUS_ACTIVE
        objdo.AT.LMBY = Session("User_Id")
    End Sub
    Private Function GetWPID() As String
        If String.IsNullOrEmpty(Request.QueryString("wpid")) Then
            Return String.Empty
        Else
            Return Request.QueryString("wpid")
        End If
    End Function
#End Region

#Region "Activity Log"
    Private Sub ActivityLog_I(ByVal userid As Integer, ByVal activitydesc As String)
        'Dim ipaddress As String = HttpContext.Current.Request.UserHostAddress
        Dim ipaddress As String = Me.Page.Request.ServerVariables("REMOTE_ADDR")
        Dim info As New UserActivityLogInfo
        info.UserId = userid
        info.IPAddress = ipaddress
        info.Description = activitydesc

        controller.UserLogActivity_I(info)
    End Sub

    Private Function GetUserIdByUserLogin(ByVal usrLogin As String) As Integer
        Return controller.GetUserIDBaseUserLogin(usrLogin)
    End Function
#End Region

End Class

Public Class DocumentRejectInfo

    Private _swid As Int32
    Public Property SWId() As Int32
        Get
            Return _swid
        End Get
        Set(ByVal value As Int32)
            _swid = value
        End Set
    End Property


    Private _docid As Integer
    Public Property Docid() As Integer
        Get
            Return _docid
        End Get
        Set(ByVal value As Integer)
            _docid = value
        End Set
    End Property


    Private _docname As String
    Public Property Docname() As String
        Get
            Return _docname
        End Get
        Set(ByVal value As String)
            _docname = value
        End Set
    End Property


    Private _remarks As String
    Public Property Remarks() As String
        Get
            Return _remarks
        End Get
        Set(ByVal value As String)
            _remarks = value
        End Set
    End Property


    Private _lmby As String
    Public Property LMBY() As String
        Get
            Return _lmby
        End Get
        Set(ByVal value As String)
            _lmby = value
        End Set
    End Property



End Class