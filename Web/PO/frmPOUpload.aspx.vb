Imports System.Data
Imports System.Data.OleDb
Imports Businesslogic
Imports Entities
Imports Common
Imports System.IO
Partial Class frmPOUpload
    Inherits System.Web.UI.Page
    Dim objBo As New BOPoUpload
    Dim objET As New ETPoUpload
    Dim objdae As New BOEPMRawData
    Dim objete As New ETEPMData
    Dim cst As New Constants
    Dim objdo As New POErrLog
    Dim objdb As New DBUtil
    Dim dtTI As New DataTable
    Dim dtCME As New DataTable
    Dim dtSIS As New DataTable
    Dim dtSitac As New DataTable
    Dim dt4 As New DataTable
    Dim t, x As Integer
    Dim viewflag As Boolean = True
    Dim strTI1 As String = ""
    Dim strTI2 As String = ""
    Dim strTI3 As String = ""
    Dim strTIFinal As String = ""
    Dim strCME1 As String = ""
    Dim strCME2 As String = ""
    Dim strCME3 As String = ""
    Dim strCMEFinal As String = ""
    Dim strSIS1 As String = ""
    Dim strSIS2 As String = ""
    Dim strSIS3 As String = ""
    Dim strSISFinal As String = ""
    Dim strSitac1 As String = ""
    Dim strSitac2 As String = ""
    Dim strSitac3 As String = ""
    Dim strSitacFinal As String = ""
    Dim strfinal As String = ""
    Dim strsqlf As String = ""
    Dim tselpid As String = ""
    Dim siteidPO As String = ""
    Dim objdl As New BODDLs
    Dim CTaskCompleted As String = ""
    Dim CPlanBAUT As String = ""
    Dim CPlanBAST As String = ""
    Dim SiteNamePO As String = ""
    Dim qry As String = ""
    Dim HOTAsPerPO As String
    Dim pocontroller As New POExtendedController()
	Dim hcptcontrol As New HCPTController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
    End Sub
    Protected Sub btnview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnview.Click
        If POUpload.PostedFile.ContentLength <> 0 Then
            'bugfix101202 refreshing the epm temp table structure
            objdb.ExeNonQuery("exec refreshEPMTempTable")
            Dim myFile As HttpPostedFile = POUpload.PostedFile
            If System.IO.Path.GetExtension(myFile.FileName.ToLower) <> ".xls" Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Only Excel files Allowed');", True)
                Exit Sub
            End If
            Dim myDataset As New DataSet()
            Dim strFileName As String = POUpload.PostedFile.FileName
            strFileName = System.IO.Path.GetFileName(strFileName)
            Dim serverpath As String = Server.MapPath("PORAWDATA")
            POUpload.PostedFile.SaveAs(serverpath + "\" + strFileName)
            Dim filepathserver As String = serverpath & "\" & strFileName
            Dim strConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
            "Data Source=" & filepathserver & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
            Dim POSheet As String = ConfigurationManager.AppSettings("POSheet").ToString
            Dim myData As New OleDbDataAdapter("Select * from [" & POSheet & "$] ", strConn)
            Dim dv As DataView
            Try
                myData.TableMappings.Add("Table", "ExcelTest")
                myData.Fill(myDataset)
                dv = myDataset.Tables(0).DefaultView
                dv.Sort = "Total Sites"
                dv.RowFilter = "[Customer PO] <> ''"
            Catch ex As Exception
                If ex.Message.IndexOf("Sheet1$") = 1 Then
                    Response.Write("<script> alert('Please check sheet name in selected Excel file,it should be Sheet1')</script>")
                Else
                    Response.Write("<script> alert('Please check Column names changed for \n\n [Total Sites] / [Customer PO] \n\n in selected Excel file.')</script>")
                End If
                Exit Sub
            End Try
            'bugfix101203 delete excel sheet
            If File.Exists(filepathserver) Then
                File.Delete(filepathserver)
            End If
            Dim i As Integer
            Dim k As Integer = dv.Count
            Dim aa1 As Integer = 0
            Dim aa2 As Integer = 0
            Dim aa3 As Integer = 0
            Dim aa4 As Integer = 0
            Dim aa5 As Integer = 0
            Dim PCount As Integer = 0
            'Dim a As String = "/Total Sites/PO Type/Customer PO/PO Name/TSEL Project ID/WPId/Site ID EPM/Site ID PO/Site Name PO/Scope/Reason (1)/Reason (2)/Value In USD/Value In IDR/Value 2 in IDR/"
            Dim a As String = "/Total Sites/PO Type/Customer PO/PO Name/TSEL Project ID/WPId/Site ID EPM/Site ID PO/Site Name PO/Scope/Reason (1)/Reason (2)/Value In USD/Value In IDR/Value 2 in IDR/HOT As Per PO/Band/"
            Dim cc1 As String = ""
            Dim msg As String = ""
            For m As Integer = 0 To 14
                Dim aa As DataColumn
                aa = dv.Table.Columns(m)
                cc1 = "/" & aa.ColumnName.ToString.ToUpper & "/"
                If a.ToString.ToUpper.IndexOf(cc1) >= 0 Then
                Else
                    msg = msg & IIf(msg <> "", ",", "") & aa.ColumnName
                End If
            Next
            If msg <> "" Then
                Response.Write("<script language='javascript'>alert('Please check column Names : \n" & msg & "');</script>")
                Exit Sub
            End If
            Dim ErrCount As Integer = 0
            Dim testflag As Boolean
            For i = Constants.PO_Excel_Row_Start To dv.Count - 1
                objET = New ETPoUpload
                testflag = True
                With objET
                    Try
                        Dim dr As DataRow
                        dr = dv.Item(i).Row
                        .PoNo = LTrim(RTrim(dr.Item("Customer PO").ToString))
                        .WorkPkgId = IIf(dr.Item("WPId").ToString <> "", dr.Item("WPId").ToString, 0)
                        .SiteNo = dr.Item("Site ID EPM").ToString.Replace(":", "")
                        .FldType = Trim(dr.Item("Reason (1)").ToString.Replace("-", " ") & " " & dr.Item("Reason (2)").ToString.Replace("-", " "))
                        .POName = dr.Item("PO Name").ToString
                        .POType = dr.Item("PO Type").ToString
                        .Value1 = IIf(dr.Item("Value in USD").ToString <> "", dr.Item("Value in USD").ToString, 0)
                        .Value2 = IIf(dr.Item("Value in IDR").ToString <> "", dr.Item("Value in IDR").ToString, 0)
                        .Pstatus = Constants.PO_PStatus_Pending
                        .AT.RStatus = Constants.STATUS_ACTIVE
                        .AT.LMBY = Session("User_Name")
                        .Value2IDR = IIf(dr.Item("Value 2 in IDR").ToString <> "", dr.Item("Value 2 in IDR").ToString, 0) '
                        .scopegroup = dr.Item("scope").ToString.TrimEnd()
                        .Band = dr.Item("Band").ToString
                        tselpid = dr.Item("TSEL Project ID").ToString
                        siteidPO = dr.Item("Site ID PO").ToString.Replace(":", "")
                        SiteNamePO = dr.Item("Site Name PO").ToString
                        HOTAsPerPO = dr.Item("HOT As Per PO").ToString
                        'for case 1
                        If .WorkPkgId <> "" And .WorkPkgId = "0" Then
                            .ECase1 = 1
                            aa1 = aa1 + 1
                        End If
                    Catch ex As Exception
                        Dim terrcode As String = objET.SiteNo & "-" & objET.WorkPkgId & "-" & objET.PoNo
                        objdb.ExeNonQuery("exec uspErrLog '', '" & terrcode & "','" & ex.Message.ToString & "','pouploading'")
                        ErrCount = ErrCount + 1
                        testflag = False
                    End Try
                    If testflag = True Then ' to check datatype error mismatch or some expection
                        Try
                            With objET
                                strsqlf = "Exec uspPORawInsert '" & .PoNo & "','" & .SiteNo & "','" & .SiteName & "','" & .WorkPkgId & "','" & .FldType.TrimEnd() & "','" & .Description & "','" & _
                                .Band_Type & "','" & .Band & "','" & .Config & "'," & .Purchase900 & "," & .Purchase1800 & ",'" & .BSSHW & "','" & .BSSCode & "','" & _
                                .AntennaName & "'," & .AntennaQty & ",'" & .Feederlen & "','" & .FeederType & "','" & .FeederQty & "'," & .Pstatus & "," & .AT.RStatus & ",'" & _
                                .AT.LMBY & "'," & .Qty & "," & .Value1 & "," & .Value2 & "," & .ECase1 & "," & .ECase2 & "," & .ECase3 & "," & .ECase4 & "," & .ECase5 & "," & _
                                .CPurchase900 & "," & .CPurchase1800 & "," & .CQty & ",'" & .ExistConfig & "','" & .POType & "','" & .POName & "','" & .Vendor & "'," & .Value2IDR & "," & _
                                "'" & .scopegroup.TrimEnd() & "','" & tselpid & "','" & siteidPO & "','" & SiteNamePO & "', '" & HOTAsPerPO & "'"
                            End With
                            objdb.ExeQueryScalar(strsqlf)
                            tselpid = ""
                            If .ECase1 = 0 Then PCount = PCount + 1
                        Catch ex As Exception
                            PCount = PCount + 1
                            objdb.ExeNonQuery("exec uspErrLog '', 'pouploadq','" & ex.Message.ToString & "','pouploading'")
                        End Try
                    End If
                End With
            Next
            '@@@@@@@@@@@@@@@@
            Session("RawPONo") = dv.Table.Rows(0).Item("Customer PO").ToString
            DupSites.InnerText = ""
            Dim stCount As Integer = 0
            stCount = objBo.uspSiteCount(Session("RawPONo"))
            stCount = stCount
            Dim s1 As String = ""
            Dim s2 As String = ""
            Dim s3 As String = ""
            Dim s4 As String = ""
            Dim s5 As String = ""
            Dim s6 As String = ""
            If aa1 > 0 Then
                A1.Visible = True
                A1.InnerText = "Missing Workpackage Id : " & aa1
                s1 = A1.InnerText
            Else
                A1.Visible = False
                s1 = ""
            End If
            'If aa2 > 0 Then
            '    A2.Visible = True
            '    A2.InnerText = "Configuration Error (Band1800 - Purchased Shows in 900) : " & aa2
            '    s2 = A2.InnerText
            'Else
            '    A2.Visible = False
            '    s2 = ""
            'End If
            'If aa3 > 0 Then
            '    A3.Visible = True
            '    A3.InnerText = "Configuration Error (Band900 - Purchased Shows in 1800) : " & aa3
            '    s3 = A3.InnerText
            'Else
            '    A3.Visible = False
            '    s3 = ""
            'End If
            'If aa4 > 0 Then
            '    A4.Visible = True
            '    A4.InnerText = "Dual Band - Qty MisMatch : " & aa4
            '    s4 = A4.InnerText
            'Else
            '    A4.Visible = False
            '    s4 = ""
            'End If
            'If aa5 > 0 Then
            '    A5.Visible = True
            '    A5.InnerText = "Configuration Error (config shows 333+444, But Quantity is not matching with Config Total) : " & aa5
            '    s5 = A5.InnerText
            'Else
            '    A5.Visible = False
            '    s5 = ""
            'End If
            If stCount > 0 Then
                DupSites.InnerText = "Updated Sites : " & stCount
                s6 = DupSites.InnerText
            Else
                s6 = ""
            End If
            If ErrCount > 0 Then
                errrow.InnerHtml = "<font color='orangered'> Datatype Mismatch count : " & ErrCount & "</font>"
            End If
            PrCount.InnerHtml = "<font color='green'>Successfull PO Transactions : " & PCount & "</font>"
            uploadPOViewLog(strFileName, PCount, s1, s2, s3, s4, s5, s6)
            objdo.UPLFileName = strFileName
            objdo.TrCount = PCount - stCount
            objdo.MisIdCount = aa1
            objdo.Pur1800 = aa2
            objdo.Pur900 = aa3
            objdo.DualQty = aa4
            objdo.ConFigErr = aa5
            objdo.MisSite = stCount
            objdo.AT.RStatus = Constants.STATUS_ACTIVE
            objdo.AT.LMBY = Session("User_Name")
            objBo.uspPOErrLogInsert(objdo)
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('No file was Selected');", True)
        End If
    End Sub

    Sub uploadPOViewLog(ByVal strFileName As String, ByVal recCount As Integer, ByVal s1 As String, ByVal s2 As String, ByVal s3 As String, ByVal s4 As String, ByVal s5 As String, ByVal s6 As String)
        Dim strOFile As String = strFileName
        strFileName = Year(Today) & "-" & MonthName(Month(Today)) & "-" & "POData.txt"
        Dim serverpath As String = Server.MapPath("PORAWDATA")
        strFileName = serverpath.ToString & "\" & strFileName
        Dim oWrite As StreamWriter
        If Not File.Exists(strFileName) Then
            oWrite = File.CreateText(strFileName)
            oWrite.WriteLine("Uploaded File Name : " & strOFile & " | Date Time : " & Format(CDate(Now()), "dd/MM/yyyy HH:mm") & " | Uploaded by : " & Session("User_Name") & " | UserType : " & Session("User_Type"))
            oWrite.WriteLine("Processed Records : " & recCount)
            If s1 <> "" Then oWrite.WriteLine(s1)
            If s2 <> "" Then oWrite.WriteLine(s2)
            If s3 <> "" Then oWrite.WriteLine(s3)
            If s4 <> "" Then oWrite.WriteLine(s4)
            If s5 <> "" Then oWrite.WriteLine(s5)
            If s6 <> "" Then oWrite.WriteLine(s6)
        Else
            oWrite = File.AppendText(strFileName)
            oWrite.WriteLine()
            oWrite.WriteLine()
            oWrite.WriteLine("Uploaded File Name : " & strOFile & " | Date Time : " & Format(CDate(Now()), "dd/MM/yyyy HH:mm") & " | Uploaded by : " & Session("User_Name") & " | UserType : " & Session("User_Type"))
            oWrite.WriteLine("Processed Records : " & recCount)
            If s1 <> "" Then oWrite.WriteLine(s1)
            If s2 <> "" Then oWrite.WriteLine(s2)
            If s3 <> "" Then oWrite.WriteLine(s3)
            If s4 <> "" Then oWrite.WriteLine(s4)
            If s5 <> "" Then oWrite.WriteLine(s5)
            If s6 <> "" Then oWrite.WriteLine(s6)
        End If
        oWrite.Close()
    End Sub

    Protected Sub btnEview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEview.Click
        uploaddocument(0)
    End Sub
    Sub uploaddocument(ByVal type As Integer)
        Dim strcolumns As String = ""
        Dim Poname As String = ""
        Dim newrecords As Integer = 0
        Dim nochange As Integer = 0
        Dim noissues As Integer = 0
        Dim issues As Integer = 0
        Dim dtquery As New DataTable
        Dim dcq As New DataColumn
        Dim dcq1 As New DataColumn
        dtquery.Columns.Add(dcq)
        dtquery.Columns.Add(dcq1)
        Dim dcrow As DataRow
        Dim dtuquery As New DataTable
        Dim dcuq As New DataColumn
        Dim dcuq1 As New DataColumn
        dtuquery.Columns.Add(dcuq)
        dtuquery.Columns.Add(dcuq1)
        Dim dcurow As DataRow
        Dim strmsquery As String = ""
        Dim strmscolumn As String = ""
        Dim filepathserver As String = ""
        Dim myDataset As New DataSet()
        Dim strFileName As String = ""
        Dim err As Boolean = False
        Dim fpath As String = ConfigurationManager.AppSettings("Fpath")
        Dim serverpath As String = Server.MapPath("EPMRAWDATA")
        If type = 0 Then
            If EPMUpload.PostedFile.ContentLength <> 0 Then
                objdb.ExeNonQuery("truncate table EPMDATA_NOCHANGES;truncate table EPMDATA_NEW;truncate table EPMDATA_ISSUES")
                Dim myFile As HttpPostedFile = EPMUpload.PostedFile
                If System.IO.Path.GetExtension(myFile.FileName.ToLower) <> ".xls" Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Only Excel files Allowed');", True)
                    Exit Sub
                End If
                strFileName = EPMUpload.PostedFile.FileName
                strFileName = System.IO.Path.GetFileName(strFileName)
                EPMUpload.PostedFile.SaveAs(serverpath + "\" + strFileName)
                filepathserver = serverpath & "\" & strFileName
            Else
                err = True
            End If
        Else
            If fileUpload1.Text <> "" Then
                objdb.ExeNonQuery("truncate table EPMDATA_NOCHANGES;truncate table EPMDATA_NEW;truncate table EPMDATA_ISSUES")
                Dim fileToMove As String
                fileToMove = fpath + "tempEPM\" + fileUpload1.Text
                If File.Exists(fileToMove) Then
                    Dim locationToMove As String
                    locationToMove = serverpath & "\" & fileUpload1.Text
                    If File.Exists(locationToMove) Then
                        File.Delete(locationToMove)
                    End If
                    File.Move(fileToMove, locationToMove)
                    filepathserver = locationToMove
                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('File not found');", True)
                    err = True
                End If
            Else
                err = True
            End If
        End If
        If Not err Then
            Dim strConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
            "Data Source=" & filepathserver & ";Extended Properties=""Excel 8.0;"""
            Dim EPMSheet As String = ConfigurationManager.AppSettings("EPMSheet").ToString
            Dim dv As DataView
            Try
                Dim myData As New OleDbDataAdapter("SELECT  * FROM [" & EPMSheet & "$] ", strConn)
                myData.TableMappings.Add("Table", "ExcelTest")
                myData.Fill(myDataset)
                Try
                    dv = myDataset.Tables(0).DefaultView
                    dv.Sort = "Customer Site Code"
                    dv.RowFilter = "[Customer Site Code] <> ''"
                    If Len(dv.Table.Columns(12).ToString) = 8 Then
                        Response.Write("<script language='javascript'> alert('This is not the valid excel for this upload');</script>")
                        Exit Sub
                    End If
                Catch ex As Exception
                    Response.Write("<script language='javascript'> alert('Please check column Name : [Customer Site Code]');</script>")
                    Exit Sub
                End Try
            Catch ex As Exception
                If ex.Message.IndexOf("Sheet1$") = 1 Then
                    Response.Write("<script language='javascript'>alert('Please check sheet name in selected Excel file,it should be Sheet1');</script>")
                Else
                    Response.Write("<script language='javascript'>alert('Uploaded EPM failed during processing ');</script>")
                    Response.Write("<script language='javascript'>alert('" & ex.Message & "');</script>")
                End If
                Exit Sub
            End Try
            'bugfix101203 delete excel sheet
            If File.Exists(filepathserver) Then
                File.Delete(filepathserver)
            End If
            Dim a As String = "/Customer Site Code/Site Name/Project Region/Project Zone/SoW eBAST/Package Type/Package ID/Package Name/Package Status/Phase/Customer PO Received Date/Customer PO Number/PO Name/"
            Dim bc As String = "Customer Site Code/Site Name/Project Region/Project Zone/SoW eBAST/Package Type/Package ID/Package Name/Package Status/Phase/Customer PO Received Date/Customer PO Number/PO Name/"
            Dim cd() As String = bc.Split("/")
            Dim xy As Boolean
            Dim strmsg As String = ""
            For k As Integer = 0 To cd.Length - 1
                xy = False
                For k1 As Integer = 0 To 12
                    If dv.Table.Columns(k1).ColumnName.ToString.ToUpper = cd(k).ToUpper Then
                        xy = True
                        Exit For
                    End If
                Next
                If xy = False Then
                    strmsg = strmsg & IIf(strmsg <> "", ",", "") & cd(k)
                End If
            Next
            If strmsg <> "" Then
                Response.Write("<script language='javascript'>alert('Please check column Names : \n" & strmsg & "');</script>")
                Exit Sub
            Else
                Dim cc1 As String = ""
                Dim msg As String = ""
                For m As Integer = 0 To 12
                    Dim aa As DataColumn
                    aa = dv.Table.Columns(m)
                    cc1 = "/" & aa.ColumnName.ToString.ToUpper & "/"
                    If a.ToString.ToUpper.IndexOf(cc1) >= 0 Then
                    Else
                        msg = msg & IIf(msg <> "", ",", "") & aa.ColumnName
                    End If
                Next
                If msg <> "" Then
                    Response.Write("<script language='javascript'>alert('Please check column Names : \n" & msg & "');</script>")
                    Exit Sub
                End If
                Dim i As Integer
                Dim dr As DataRow
                Dim EPMRecCount As Integer = 0
                Dim ErrCount As Integer = 0
                'Cheking the new column and adding new column
                For r As Integer = 13 To dv.Table.Columns.Count - 1
                    Dim aa As DataColumn
                    aa = dv.Table.Columns(r)
                    Dim sh As String = milestoneName(aa.ColumnName)
                    Dim k As Integer = objdb.ExeUpdate("Exec uspTIMilestoneInsert '" & sh & "'")
                    If k = 1 Then
                        viewflag = True
                        Try
                            qry = "Alter table EPMDATA add " & sh & " Datetime"
                            objdb.ExeUpdate(qry)
                        Catch ex As Exception
                            Response.Write("<script language='javascript'>alert('Error Alter table EPMDATA');</script>")
                        End Try
                        Try
                            qry = "Alter table EPMDATABACKUP add " & sh & " Datetime"
                            objdb.ExeUpdate(qry)
                            objdb.ExeUpdate("Alter table EPMDATA_NEW add " & sh & " Datetime")
                            objdb.ExeUpdate("Alter table EPMDATA_NOCHANGES add " & sh & " Datetime")
                            objdb.ExeUpdate("Alter table EPMDATA_NOISSUES add " & sh & " Datetime")
                            objdb.ExeUpdate("Alter table EPMDATA_ISSUES add " & sh & " Datetime")
                        Catch ex As Exception
                            Response.Write("<script language='javascript'>alert('Please check column Names : \n" & msg & "');</script>")
                        End Try
                    ElseIf k = 3 Then
                        viewflag = True
                    End If
                Next
                objdb.ExeUpdate("Exec USPMILESTONE")
                For i = Constants.EPM_Excel_Row_Start To dv.Count - 1
                    dr = myDataset.Tables(0).Rows(i)
                    objete = New ETEPMData
                    If Trim(myDataset.Tables(0).Rows(i).Item("Customer Site Code").ToString) <> "" Then
                        objete.SiteId = myDataset.Tables(0).Rows(i).Item("Customer Site Code").ToString.Replace(":", "")
                        objete.SiteName = dr.Item("Site Name").ToString.Replace("'", " ")
                        objete.Region = dr.Item("Project Region").ToString
                        objete.Zone = dr.Item("Project Zone").ToString 'Zone have in 2 col Merge
                        objete.PackageType = dr.Item("Package Type").ToString
                        objete.WorkPackageId = dr.Item("Package ID").ToString
                        objete.PackageName = dr.Item("Package Name").ToString
                        objete.PhaseTI = Convert.ToString(dr.Item("Phase"))
                        'bugfix101210 epm v2 update
                        objete.PackageStatus = dr.Item("Package Status").ToString.Replace("Work In Progress", "Planned").Replace("Created", "Planned")
                        If dr.Item("Customer PO Received Date").ToString <> "" And Trim(dr.Item("Customer PO Received Date").ToString).Length >= 10 Then
                            objete.CustPORecDt = cst.formatdateDDMMYYYY(Format(CDate(dr.Item("Customer PO Received Date").ToString), "dd/MM/yyyy"))
                        End If
                        objete.CustPONo = dr.Item("Customer PO Number").ToString

                        Dim mainq As String = ""
                        Dim valueq As String = ""
                        Dim updateQ As String = ""
                        Dim uq As String = ""
                        'It should start after last site info
                        For m As Integer = 13 To dv.Table.Columns.Count - 1
                            Dim aa As DataColumn
                            aa = dv.Table.Columns(m)
                            Dim aa1 As String = ""
                            Dim aa2 As String = ""
                            If dv.Table.Rows(i).Item(m).ToString <> "" Then
                                aa1 = cst.formatdateDDMMYYYY(Format(CDate(dv.Table.Rows(i).Item(m)), "dd/MM/yyyy"))
                                aa2 = "'" & cst.formatdateDDMMYYYY(Format(CDate(dv.Table.Rows(i).Item(m)), "dd/MM/yyyy")) & "'"
                            Else
                                aa1 = "''"
                                aa2 = "''"
                            End If
                            Dim sh1 As String = milestoneName(aa.ColumnName)

                            If sh1 = "ACT_3170" And aa1 <> "''" Then
                                objete.SiteIntegration = aa1
                            End If
                            uq = sh1 & "=" & aa2
                            updateQ = IIf(updateQ <> "", updateQ & uq & ",", uq & ",")
                            mainq = IIf(mainq <> "", mainq & sh1 & ",", sh1 & ",")
                            valueq = IIf(valueq <> "", valueq & aa2 & ",", aa2 & ",")
                        Next
                        mainq = mainq.Substring(0, Len(mainq) - 1)
                        valueq = valueq.Substring(0, Len(valueq) - 1)
                        updateQ = updateQ.Substring(0, Len(updateQ) - 1)
                        '@@@@@@@@@@@@@@@@@@@@@ preparing insert statement based on columns@@@@@@@@@@@@@@@@@@@@@@@
                        Dim strst As String = ""
                        Dim strstupdate As String = ""
                        Dim st As Integer = 0
                        st = objdb.ExeQueryScalar("SELECT EPMID FROM EPMData where SiteID='" & objete.SiteId.TrimStart().TrimEnd() & "' and WorkPackageId='" & objete.WorkPackageId.TrimEnd().TrimStart() & "'")
                        If st = 0 Then
                            strst = "INSERT INTO EPMDATA_NEW(SiteId,SiteName,Region,Zone,Address,City,PhaseTI,TISubcon,WorkPackageId,SiteIntegration," & _
                               "SiteAcpOnAir,SiteAcpOnBAST,Pstatus,RStatus,LMBY,LMDT,PackageType,PackageName,PackageStatus,CustPONo,CustPORecDt,Equipment_Arrived, Equipment_On_Site, ATP,CTaskComp,CPlanBAUT,CPlanBAST," & mainq & " )" & _
                               " VALUES ('" & objete.SiteId.TrimEnd().TrimStart() & "','" & objete.SiteName & "','" & objete.Region & "','" & objete.Zone & "','" & objete.Address & "','" & objete.City & "'," & _
                               "'" & objete.PhaseTI & "','" & objete.SubconTI & "','" & objete.WorkPackageId.TrimEnd().TrimStart() & "','" & objete.SiteIntegration & "','" & objete.SiteAcpOnAir & "','" & objete.SiteAcpOnBAST & "'," & _
                               "'" & objete.pstatus & "','" & objete.AT.RStatus & "','" & objete.AT.LMBY & "',GetDate(),'" & objete.PackageType & "','" & objete.PackageName & "'," & _
                               "'" & objete.PackageStatus & "','" & objete.CustPONo & "','" & objete.CustPORecDt & "','" & objete.Equipment_Arrived & "','" & objete.Equipment_On_Site & "','" & objete.ATP & "','" & CTaskCompleted & "','" & CPlanBAUT & "','" & CPlanBAST & "'," & valueq & ")"
                            If objete.SubconTI <> "" Then
                                objdb.ExeNonQuery("Exec uspSubConIUfromEPM '" & objete.SubconTI & "','" & objete.AT.LMBY & "'")
                            End If
                            newrecords = newrecords + 1
                        Else
                            Dim dtebast As New DataTable
                            Dim strebast As String
                            strebast = "Select " & mainq & " from epmdata where SiteID='" & objete.SiteId.TrimEnd().TrimStart() & "' and WorkPackageId='" & objete.WorkPackageId.TrimEnd().TrimStart() & "'"
                            dtebast = objdb.ExeQueryDT(strebast, "dtebasttab")
                            Dim mainepm() As String = mainq.Split(",")
                            Dim valueepm() As String = valueq.Split(",")
                            Dim icount As Integer
                            Dim checktag_nochanges As Integer = 0
                            Dim checktag_noissues As Integer = 0
                            Dim checktag_issues As Integer = 0
                            Dim xyrow As String = ""
                            For icount = 0 To mainepm.Length - 1
                                If IsDBNull(dtebast.Rows(0).Item(mainepm(icount))) Then
                                    xyrow = "''"
                                Else
                                    xyrow = "'" & cst.formatdateDDMMYYYY(Format(CDate(dtebast.Rows(0).Item(mainepm(icount))), "dd/MM/yyyy")) & "'"
                                End If
                                If xyrow = valueepm(icount) Then
                                    'data is same in ebast and epm  means no chnage in data
                                    checktag_nochanges = checktag_nochanges + 1
                                Else
                                    'means data is not same in ebast and epm
                                    Select Case xyrow
                                        Case "''"
                                            'epm overwrting ebast since ebast there is no date no issues
                                            checktag_noissues = checktag_noissues + 1
                                        Case Else
                                            'epm and epm both having dates  means data with issues
                                            strmscolumn = mainepm(icount)
                                            If strmsquery = "" Then
                                                strmsquery = IIf(strmsquery = "", strmscolumn, strmsquery & " , " & strmscolumn)
                                            Else
                                                If strmsquery.IndexOf(mainepm(icount)) < 0 Then
                                                    strmsquery = IIf(strmsquery = "", strmscolumn, strmsquery & " , " & strmscolumn)
                                                End If
                                            End If
                                            checktag_issues = checktag_issues + 1
                                    End Select
                                End If
                            Next
                            If strmsquery <> "" Then
                                pocontroller.POMissMatch_IU(strmsquery, objete.SiteId, objete.WorkPackageId)
                                'objdb.ExeNonQuery("insert into tblmismatch values('" & objete.SiteId & "','" & objete.WorkPackageId & "','" & strmsquery & "')")
                            End If
                            strmsquery = ""
                            If checktag_nochanges > 0 And checktag_noissues = 0 And checktag_issues = 0 Then 'means full row nochanges
                                strst = " INSERT INTO EPMDATA_NOCHANGES(SiteId,SiteName,Region,Zone,Address,City,PhaseTI,TISubcon,WorkPackageId,SiteIntegration," + _
                                "SiteAcpOnAir,SiteAcpOnBAST,Pstatus,RStatus,LMBY,LMDT,PackageType,PackageName,PackageStatus,CustPONo,CustPORecDt,Equipment_Arrived, Equipment_On_Site, ATP,CTaskComp,CPlanBAUT,CPlanBAST," & mainq & " )" + _
                                " VALUES ('" & objete.SiteId & "','" & objete.SiteName & "','" & objete.Region & "','" & objete.Zone & "','" & objete.Address & "','" & objete.City & "'," + _
                                "'" & objete.PhaseTI & "','" & objete.SubconTI & "','" & objete.WorkPackageId & "','" & objete.SiteIntegration & "','" & objete.SiteAcpOnAir & "','" & objete.SiteAcpOnBAST & "'," + _
                                "'" & objete.pstatus & "','" & objete.AT.RStatus & "','" & objete.AT.LMBY & "',GetDate(),'" & objete.PackageType & "','" & objete.PackageName & "'," + _
                                "'" & objete.PackageStatus & "','" & objete.CustPONo & "','" & objete.CustPORecDt & "','" & objete.Equipment_Arrived & "','" & objete.Equipment_On_Site & "','" & objete.ATP & "','" & CTaskCompleted & "','" & CPlanBAUT & "','" & CPlanBAST & "'," & valueq & ")"
                                nochange = nochange + 1
                            ElseIf checktag_noissues > 0 And checktag_issues = 0 Then 'means no issues data
                                strst = " INSERT INTO EPMDATA_NOISSUES(SiteId,SiteName,Region,Zone,Address,City,PhaseTI,TISubcon,WorkPackageId,SiteIntegration," + _
                                "SiteAcpOnAir,SiteAcpOnBAST,Pstatus,RStatus,LMBY,LMDT,PackageType,PackageName,PackageStatus,CustPONo,CustPORecDt,Equipment_Arrived, Equipment_On_Site, ATP,CTaskComp,CPlanBAUT,CPlanBAST," & mainq & " )" + _
                                " VALUES ('" & objete.SiteId & "','" & objete.SiteName & "','" & objete.Region & "','" & objete.Zone & "','" & objete.Address & "','" & objete.City & "'," + _
                                "'" & objete.PhaseTI & "','" & objete.SubconTI & "','" & objete.WorkPackageId & "','" & objete.SiteIntegration & "','" & objete.SiteAcpOnAir & "','" & objete.SiteAcpOnBAST & "'," + _
                                "'" & objete.pstatus & "','" & objete.AT.RStatus & "','" & objete.AT.LMBY & "',GetDate(),'" & objete.PackageType & "','" & objete.PackageName & "'," + _
                                "'" & objete.PackageStatus & "','" & objete.CustPONo & "','" & objete.CustPORecDt & "','" & objete.Equipment_Arrived & "','" & objete.Equipment_On_Site & "','" & objete.ATP & "','" & CTaskCompleted & "','" & CPlanBAUT & "','" & CPlanBAST & "'," & valueq & ")"
                                strstupdate = " Update EPMData Set CustPONo = '" & objete.CustPONo & "',SiteIntegration= '" & objete.SiteIntegration & "', SiteAcpOnAir='" & objete.SiteAcpOnAir & "',SiteAcpOnBAST = '" & objete.SiteAcpOnBAST & "'," + _
                                " Equipment_Arrived='" & objete.Equipment_Arrived & "',Equipment_On_Site='" & objete.Equipment_On_Site & "',CTaskComp = '" & CTaskCompleted & "' ,CPlanBAUT = '" & CPlanBAST & "',CPlanBAST='" & CPlanBAST & "' ,ATP='" & objete.ATP & "',PackageStatus='" & objete.PackageStatus & "'," & updateQ & "  where EPMId = '" & st & "'"
                                noissues = noissues + 1
                            ElseIf checktag_issues > 0 Then 'means issues data
                                strst = " INSERT INTO EPMDATA_ISSUES(SiteId,SiteName,Region,Zone,Address,City,PhaseTI,TISubcon,WorkPackageId,SiteIntegration," + _
                                "SiteAcpOnAir,SiteAcpOnBAST,Pstatus,RStatus,LMBY,LMDT,PackageType,PackageName,PackageStatus,CustPONo,CustPORecDt,Equipment_Arrived, Equipment_On_Site, ATP,CTaskComp,CPlanBAUT,CPlanBAST," & mainq & " )" + _
                                " VALUES ('" & objete.SiteId & "','" & objete.SiteName & "','" & objete.Region & "','" & objete.Zone & "','" & objete.Address & "','" & objete.City & "'," + _
                                "'" & objete.PhaseTI & "','" & objete.SubconTI & "','" & objete.WorkPackageId & "','" & objete.SiteIntegration & "','" & objete.SiteAcpOnAir & "','" & objete.SiteAcpOnBAST & "'," + _
                                "'" & objete.pstatus & "','" & objete.AT.RStatus & "','" & objete.AT.LMBY & "',GetDate(),'" & objete.PackageType & "','" & objete.PackageName & "'," + _
                                "'" & objete.PackageStatus & "','" & objete.CustPONo & "','" & objete.CustPORecDt & "','" & objete.Equipment_Arrived & "','" & objete.Equipment_On_Site & "','" & objete.ATP & "','" & CTaskCompleted & "','" & CPlanBAUT & "','" & CPlanBAST & "'," & valueq & ")"
                                issues = issues + 1
                            End If
                            strcolumns = mainq
                            'bugfix:110531 updating site name
                            objdb.ExeNonQuery("Update EPMData Set SiteName = '" & objete.SiteName & "', PhaseTI ='" & objete.PhaseTI & "' where EPMId = '" & st & "'")
                        End If
                        objete.AT.RStatus = Constants.STATUS_ACTIVE
                        objete.AT.LMBY = Session("User_Name")
                        Dim kkt As Boolean = False
                        Dim sh As String = ""
                        Try
                            Dim strq As String = "exec uspEPMDInsert '" & objete.SiteId & "','" & objete.SiteName & "','" & objete.Region & "','" & objete.Zone & "','" & objete.WorkPackageId & "','" & objete.AT.LMBY & "','" & objete.PackageName & "','" & objete.PackageStatus.ToString.ToLower & "','" & objete.CustPONo & "'"
                            objdb.ExeNonQuery(strq)
                            If strst.IndexOf("EPMDATA_NEW") > 0 Then
                                'save this query form future.
                                dcrow = dtquery.NewRow
                                dcrow.Item(0) = strst.ToString.Replace("EPMDATA_NEW", "EPMDATA")
                                dtquery.Rows.Add(dcrow)
                            ElseIf strst.IndexOf("EPMDATA_NOISSUES") > 0 Then
                                'save this query form future.
                                dcurow = dtuquery.NewRow
                                dcurow.Item(0) = strstupdate
                                dtuquery.Rows.Add(dcurow)
                            End If
                            If strst.IndexOf("EPMDATA_NOCHANGES") < 0 Then
                                objdb.ExeNonQuery(strst)
                            End If
                        Catch ex As Exception
                            'objdb.ExeNonQuery("exec uspErrLog '', '" & objete.SiteId & "' ,'" & ex.Message.ToString & "','EPMuploading'")
                            'ErrCount = ErrCount + 1
							Dim hcptcontrol As New HCPTController
                            hcptcontrol.ErrorLogInsert(100, "frmPOUpload.aspx:UploadDocument()", ex.Message.ToString(), "NON-SP")
                            Response.Write("<script language='javascript'> alert('EPM Uploading Eror');</script>")
                            Response.Write("<script language='javascript'>alert('" & ex.Message & "');</script>")
                        End Try
                        ' EPMRecCount = EPMRecCount + 1
                    End If
                    objete = Nothing
                Next
                'bugfix101129 updating the package status
                objdb.ExeNonQuery("update epmdata set packagestatus='Planned' where packagestatus in ('Work In Progress','Created')")
                objdb.ExeNonQuery("update EPMDATA_ISSUES set packagestatus='Planned' where packagestatus in ('Work In Progress','Created')")
                objdb.ExeNonQuery("update EPMDATA_NEW set packagestatus='Planned' where packagestatus in ('Work In Progress','Created')")
                objdb.ExeNonQuery("update EPMDATA_NOCHANGES set packagestatus='Planned' where packagestatus in ('Work In Progress','Created')")
                objdb.ExeNonQuery("update EPMDATA_NOISSUES set packagestatus='Planned' where packagestatus in ('Work In Progress','Created')")
                'Updates IntegrationDate,Site Accept onAir, Site Accept on BAST
                objdae.uspEPMDataUpdate(Constants._EmptyDate, Session("User_Name"))
                'bugfix110111 new requirement to update the remapping site
                objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='IN Active', REMAPPEDFROM='EPM1' where workpkgid in (select workpackageid from epmdata where phaseti like '%Remapping%')")
                'updating the po
                objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='Active', REMAPPEDFROM='EPM1' where workpkgid not in (select workpackageid from epmdata where PackageStatus in ('Cancelled'))")
                objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='In Active', REMAPPEDFROM='EPM1' where workpkgid in (select workpackageid from epmdata where PackageStatus not in ('Planned','Completed'))")
                objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='IN ACTIVE', REMAPPEDFROM='EPM1' where workpkgid not in (select workpackageid from epmdata) and status='Active'")
                objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='IN Active', REMAPPEDFROM='EPM1' where workpkgid in (select workpackageid from epmdata where phaseti like '%cancelled%')")
                'bugfix100427 Cleaning TIMilestone
                objdb.ExeQuery("delete from timilestone where milestone not in (select column_name from table_field_name where table_name='epmdata')")
                'Updating with NULL 
                Dim dtmile As New DataTable
                Dim p As Integer
                dtmile = objdb.ExeQueryDT("select Milestone from TIMilestone", "dtmile1")
                For p = 0 To dtmile.Rows.Count - 1
                    objdb.ExeNonQuery("Update EPMDATA_NEW Set " & dtmile.Rows(p).Item(0).ToString & " = NULL where Convert(varchar(8)," & dtmile.Rows(p).Item(0).ToString & " ,112) = " & Constants._EmptyDate & "")
                    objdb.ExeNonQuery("Update EPMDATA_NOCHANGES Set " & dtmile.Rows(p).Item(0).ToString & " = NULL where Convert(varchar(8)," & dtmile.Rows(p).Item(0).ToString & " ,112) = " & Constants._EmptyDate & "")
                    objdb.ExeNonQuery("Update EPMDATA_NOISSUES Set " & dtmile.Rows(p).Item(0).ToString & " = NULL where Convert(varchar(8)," & dtmile.Rows(p).Item(0).ToString & " ,112) = " & Constants._EmptyDate & "")
                    objdb.ExeNonQuery("Update EPMDATA_ISSUES Set " & dtmile.Rows(p).Item(0).ToString & " = NULL where Convert(varchar(8)," & dtmile.Rows(p).Item(0).ToString & " ,112) = " & Constants._EmptyDate & "")
                Next
                nochangescount.InnerHtml = "No Changes Count " & nochange
                Dim noissues_new As Integer = newrecords + noissues
                noissuescount.InnerHtml = "No Issues " & noissues_new
                If noissues_new > 0 Then
                    btnproceed.Visible = True
                Else
                    btnproceed.Visible = False
                End If
                issuescount.InnerHtml = "Issues " & issues
                If issues > 0 Then
                    btnDownload.Visible = True
                Else
                    btnDownload.Visible = False
                End If
                Dim dtfinal As New DataTable
                If issues > 0 Then
                    Dim strdispute As String
                    strdispute = "SELECT ei.SiteId as [Customer Site Code], SiteName as [Site Name] , Region as [Project Region] , Zone as [Project Zone],PackageType as [Package Type]," & _
                    " ei.WorkPackageId as [Package ID],PackageName as [Package Name],PackageStatus as [Package Status]," & _
                    " PhaseTI as Phase, CustPORecDt as [Customer PO Received Date], CustPONo as [Customer PO Number],POName as [PO Name],TISubCon as [Subcontractor]," & _
                    " CTaskComp as [Contract Task Completed], CPlanBAUT as [Contract Plan BAUT], CPlanBAST as [Contract Plan BAST], " & _
                    " " & strcolumns & " from EPMDATA_ISSUES ei inner join tblmismatch tm on ei.WorkPackageId=tm.workpackageid"
                    Response.Write(strmsquery)
                    dtfinal = objdb.ExeQueryDT(strdispute, "s1s")
                    EPMAfterValidating.DataSource = dtfinal
                    EPMAfterValidating.DataBind()
                    'highlight the rows
                    Dim dt As New DataTable
                    dt = objdb.ExeQueryDT("select * from tblmismatch", "mis")
                    Dim x, y As Integer
                    For x = 0 To dt.Rows.Count - 1
                        Dim marray() As String = dt.Rows(x).Item(3).ToString.Split(",")

                        For y = 0 To marray.Length - 1
                            '    EPMAfterValidating.Rows(x).Cells.FromKey(marray(y)).Style.BackColor = Drawing.Color.Yellow
                            LblCheck.Text = marray(y)
                        Next
                    Next
                End If
                objdb.ExeNonQuery("truncate table tblmismatch")
                Session("dtquerys") = dtquery
                Session("dtuquerys") = dtuquery
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('No file was Selected');", True)
        End If
    End Sub
    Sub uploadViewLog(ByVal strFileName As String, ByVal recCount As Integer)
        Dim strOFile As String = strFileName
        strFileName = Year(Today) & "-" & MonthName(Month(Today)) & "-" & "EPMData.txt"
        Dim serverpath As String = Server.MapPath("EPMRAWDATA")
        strFileName = serverpath.ToString & "\" & strFileName
        Dim oWrite As StreamWriter
        If Not File.Exists(strFileName) Then
            oWrite = File.CreateText(strFileName)
            oWrite.WriteLine("Uploaded File Name : " & strOFile & " | Date Time : " & Format(CDate(Now()), "dd/MM/yyyy HH:mm") & " Uploaded by : " & Session("User_Name") & " | UserType : " & Session("User_Type"))
            oWrite.WriteLine("Processed Records : " & recCount)
        Else
            oWrite = File.AppendText(strFileName)
            oWrite.WriteLine()
            oWrite.WriteLine()
            oWrite.WriteLine("Uploaded File Name : " & strOFile & " | Date Time : " & Format(CDate(Now()), "dd/MM/yyyy HH:mm") & " | Uploaded by : " & Session("User_Name") & " | UserType : " & Session("User_Type"))
            oWrite.WriteLine("Processed Records : " & recCount)
        End If
        oWrite.Close()
    End Sub
    Private Function configTot(ByVal strConfig As String) As Integer
        If strConfig = "" Then
            Return 0
        Else
            Dim ab() As String
            Dim st As String = strConfig
            st = st.Replace(".", "+")
            ab = st.Split("+")
            Dim sat As Integer
            Dim tot As Integer = 0
            For t As Integer = 0 To ab.GetLength(0) - 1
                sat = ab(t)
                Dim kkk As String
                kkk = sat.ToString

                For jj As Integer = 0 To kkk.ToString.Length - 1
                    tot = tot + CInt(kkk.Substring(jj, 1))
                Next
            Next
            Return tot
        End If
    End Function
    Function milestoneName(ByVal mileText As String) As String
        Dim rtn As String = ""
        If UCase(Right(mileText, 3)) <> "ACT" And UCase(Right(mileText, 3)) <> "FOR" Then
            If UCase(Right(mileText, 1)) = "A" Then
                rtn = "ACT_" & Left(mileText, 4)
            ElseIf UCase(Right(mileText, 1)) = "F" Then
                rtn = "FOR_" & Left(mileText, 4)
            ElseIf UCase(Right(mileText, 2)) = "AC" Then
                rtn = "ACT_" & Left(mileText, 4)
            ElseIf UCase(Right(mileText, 2)) = "FO" Then
                rtn = "FOR_" & Left(mileText, 4)
            End If
        Else
            rtn = UCase(Right(mileText, 3)) & "_" & Left(mileText, 4)
        End If
        Return rtn
    End Function
    Protected Sub btnupload1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnupload1.Click
        uploaddocument(1)
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        objdb.ExeQuery("exec uspBackup")
        Response.Write("<script language='javascript'>alert('DB and ePM data backuped successfully');</script>")
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Response.Write("<script language='javascript'>alert('ePM data backuped successfully');</script>")
    End Sub
    Protected Sub btnEviewnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEviewnew.Click
        Dim Poname As String = ""
        Dim filepathserver As String = ""
        Dim myDataset As New DataSet()
        Dim strFileName As String = ""
        Dim err As Boolean = False
        Dim fpath As String = ConfigurationManager.AppSettings("Fpath")
        Dim serverpath As String = Server.MapPath("EPMRAWDATA")
        If EPMUploadnew.PostedFile.ContentLength <> 0 Then
            Dim myFile As HttpPostedFile = EPMUploadnew.PostedFile
            If System.IO.Path.GetExtension(myFile.FileName.ToLower) <> ".xls" Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Only Excel files Allowed');", True)
                Exit Sub
            End If
            strFileName = EPMUploadnew.PostedFile.FileName
            strFileName = System.IO.Path.GetFileName(strFileName)
            EPMUploadnew.PostedFile.SaveAs(serverpath + "\" + strFileName)
            filepathserver = serverpath & "\" & strFileName
        Else
            err = True
        End If
        If Not err Then
            Dim strConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
            "Data Source=" & filepathserver & ";" & _
            "Extended Properties=""Excel 8.0;"""
            Dim EPMSheet As String = ConfigurationManager.AppSettings("EPMSheet").ToString
            Dim dv As DataView
            Try
                Dim myData As New OleDbDataAdapter("SELECT  * FROM [" & EPMSheet & "$] ", strConn)
                myData.TableMappings.Add("Table", "ExcelTest")
                myData.Fill(myDataset)
                Try
                    dv = myDataset.Tables(0).DefaultView
                    dv.Sort = "Customer Site Code"
                    dv.RowFilter = "[Customer Site Code] <> ''"
                    If Len(dv.Table.Columns(16).ToString) > 8 Then
                        Response.Write("<script language='javascript'> alert('This is not the valid excel for this upload');</script>")
                        Exit Sub
                    End If
                Catch ex As Exception
                    Response.Write("<script language='javascript'> alert('Please check column Name : [Customer Site Code]');</script>")
                    Exit Sub
                End Try
            Catch ex As Exception
                If ex.Message.IndexOf("Sheet1$") = 1 Then
                    Response.Write("<script language='javascript'>alert('Please check sheet name in selected Excel file,it should be Sheet1');</script>")
                Else
                    Response.Write("<script language='javascript'>alert('Uploaded EPM failed during processing ');</script>")
                    Response.Write("<script language='javascript'>alert('" & ex.Message & "');</script>")
                End If
                Exit Sub
            End Try
            Dim a As String = "/Customer Site Code/Site Name/Project Region/Project Zone/Package Type/Package ID/Package Name/Package Status/Phase/Customer PO Received Date/Customer PO Number/PO Name/"
            Dim bc As String = "Customer Site Code/Site Name/Project Region/Project Zone/Package Type/Package ID/Package Name/Package Status/Phase/Customer PO Received Date/Customer PO Number/PO Name/"
            Dim cd() As String = bc.Split("/")
            Dim xy As Boolean
            Dim strmsg As String = ""
            For k As Integer = 0 To cd.Length - 1
                xy = False
                For k1 As Integer = 0 To 15
                    If dv.Table.Columns(k1).ColumnName.ToString.ToUpper = cd(k).ToUpper Then
                        xy = True
                        Exit For
                    End If
                Next
                If xy = False Then
                    strmsg = strmsg & IIf(strmsg <> "", ",", "") & cd(k)
                End If
            Next
            If strmsg <> "" Then
                Response.Write("<script language='javascript'>alert('Please check column Names : \n" & strmsg & "');</script>")
                Exit Sub
            Else
                Dim cc1 As String = ""
                Dim msg As String = ""
                For m As Integer = 0 To 15
                    Dim aa As DataColumn
                    aa = dv.Table.Columns(m)
                    cc1 = "/" & aa.ColumnName.ToString.ToUpper & "/"
                    If a.ToString.ToUpper.IndexOf(cc1) >= 0 Then
                    Else
                        msg = msg & IIf(msg <> "", ",", "") & aa.ColumnName
                    End If
                Next
                If msg <> "" Then
                    Response.Write("<script language='javascript'>alert('Please check column Names : \n" & msg & "');</script>")
                    Exit Sub
                End If
                Dim i As Integer
                Dim dr As DataRow
                Dim EPMRecCount As Integer = 0
                Dim ErrCount As Integer = 0
                For i = Constants.EPM_Excel_Row_Start To dv.Count - 1
                    dr = myDataset.Tables(0).Rows(i)
                    objete = New ETEPMData
                    If Trim(myDataset.Tables(0).Rows(i).Item("Customer Site Code").ToString) <> "" Then
                        objete.SiteId = myDataset.Tables(0).Rows(i).Item("Customer Site Code").ToString.Replace(":", "")
                        objete.SiteName = dr.Item("Site Name").ToString.Replace("'", " ")
                        objete.Region = dr.Item("Project Region").ToString
                        objete.Zone = IIf(dr.Item("Project Zone").ToString <> "", dr.Item(3).ToString, Constants.EmptyZone) 'Zone have in 2 col Merge
                        objete.PackageType = dr.Item("Package Type").ToString
                        objete.WorkPackageId = dr.Item("Package ID").ToString
                        objete.PackageName = dr.Item("Package Name").ToString
                        objete.PackageStatus = dr.Item("Package Status").ToString
                        objete.PhaseTI = dr.Item("Phase").ToString
                        If dr.Item("Customer PO Received Date").ToString <> "" And Trim(dr.Item("Customer PO Received Date").ToString).Length >= 10 Then
                            objete.CustPORecDt = cst.formatdateDDMMYYYY(Format(CDate(dr.Item("Customer PO Received Date").ToString), "dd/MM/yyyy"))
                        End If
                        objete.CustPONo = dr.Item("Customer PO Number").ToString
                        objete.SubconTI = dr.Item("Subcontractor").ToString
                        If dr.Item("Contract Task Completed").ToString <> "" Then
                            CTaskCompleted = cst.formatdateDDMMYYYY(Format(CDate(dr.Item("Contract Task Completed").ToString), "dd/MM/yyyy"))
                        End If
                        If dr.Item("Contract Plan BAUT").ToString <> "" Then
                            CPlanBAUT = cst.formatdateDDMMYYYY(Format(CDate(dr.Item("Contract Plan BAUT").ToString), "dd/MM/yyyy"))
                        End If
                        If dr.Item("Contract Plan BAST").ToString <> "" Then
                            CPlanBAST = cst.formatdateDDMMYYYY(Format(CDate(dr.Item("Contract Plan BAST").ToString), "dd/MM/yyyy"))
                        End If
                        Dim mainq As String = ""
                        Dim valueq As String = ""
                        Dim updateQ As String = ""
                        Dim uq As String = ""
                        For m As Integer = 16 To dv.Table.Columns.Count - 1
                            Dim aa As DataColumn
                            aa = dv.Table.Columns(m)
                            Dim aa1 As String = ""
                            Dim aa2 As String = ""
                            If dv.Table.Rows(i).Item(m).ToString <> "" Then
                                aa1 = cst.formatdateDDMMYYYY(Format(CDate(dv.Table.Rows(i).Item(m)), "dd/MM/yyyy"))
                                aa2 = "'" & cst.formatdateDDMMYYYY(Format(CDate(dv.Table.Rows(i).Item(m)), "dd/MM/yyyy")) & "'"
                            Else
                                aa1 = "''"
                                aa2 = "''"
                            End If
                            Dim sh1 As String = aa.ColumnName
                            If sh1 = "ACT_9350" And aa1 <> "''" Then
                                objete.SiteIntegration = aa1
                            End If
                            uq = sh1 & "=" & aa2
                            updateQ = IIf(updateQ <> "", updateQ & uq & ",", uq & ",")
                            mainq = IIf(mainq <> "", mainq & sh1 & ",", sh1 & ",")
                            valueq = IIf(valueq <> "", valueq & aa2 & ",", aa2 & ",")
                        Next
                        mainq = mainq.Substring(0, Len(mainq) - 1)
                        valueq = valueq.Substring(0, Len(valueq) - 1)
                        updateQ = updateQ.Substring(0, Len(updateQ) - 1)
                        '@@@@@@@@@@@@@@@@@@@@@ preparing insert statement based on columns@@@@@@@@@@@@@@@@@@@@@@@
                        Dim strst As String = ""
                        Dim st As Integer = 0
                        st = objdb.ExeQueryScalar("SELECT EPMID FROM EPMData where SiteID='" & objete.SiteId.TrimEnd().TrimStart() & "' and WorkPackageId='" & objete.WorkPackageId.TrimEnd().TrimStart() & "'")
                        If st = 0 Then
                            strst = " INSERT INTO EPMDATA(SiteId,SiteName,Region,Zone,Address,City,PhaseTI,TISubcon,WorkPackageId,SiteIntegration," & _
                               "SiteAcpOnAir,SiteAcpOnBAST,Pstatus,RStatus,LMBY,LMDT,PackageType,PackageName,PackageStatus,CustPONo,CustPORecDt,Equipment_Arrived, Equipment_On_Site, ATP,CTaskComp,CPlanBAUT,CPlanBAST," & mainq & " )" & _
                               " VALUES ('" & objete.SiteId & "','" & objete.SiteName & "','" & objete.Region & "','" & objete.Zone & "','" & objete.Address & "','" & objete.City & "'," & _
                               "'" & objete.PhaseTI & "','" & objete.SubconTI & "','" & objete.WorkPackageId & "','" & objete.SiteIntegration & "','" & objete.SiteAcpOnAir & "','" & objete.SiteAcpOnBAST & "'," & _
                               "'" & objete.pstatus & "','" & objete.AT.RStatus & "','" & objete.AT.LMBY & "',GetDate(),'" & objete.PackageType & "','" & objete.PackageName & "'," & _
                               "'" & objete.PackageStatus & "','" & objete.CustPONo & "','" & objete.CustPORecDt & "','" & objete.Equipment_Arrived & "','" & objete.Equipment_On_Site & "','" & objete.ATP & "','" & CTaskCompleted & "','" & CPlanBAUT & "','" & CPlanBAST & "'," & valueq & ")"
                            If objete.SubconTI <> "" Then
                                'objdb.ExeNonQuery("Exec uspSubConIUfromEPM '" & objete.SubconTI & "','" & objete.AT.LMBY & "'")
                            End If
                        Else
                            If objete.SubconTI <> "" Then
                                'objdb.ExeNonQuery("Exec uspSubConIUfromEPM '" & objete.SubconTI & "','" & objete.AT.LMBY & "'")
                            End If
                            strst = " Update EPMData Set CustPONo = '" & objete.CustPONo & "',SiteIntegration='" & objete.SiteIntegration & "', SiteAcpOnAir='" & objete.SiteAcpOnAir & "',SiteAcpOnBAST = '" & objete.SiteAcpOnBAST & _
                            "',Equipment_Arrived='" & objete.Equipment_Arrived & "',Equipment_On_Site='" & objete.Equipment_On_Site & "',CTaskComp='" & CTaskCompleted & "',CPlanBAUT='" & CPlanBAST & "',CPlanBAST='" & CPlanBAST & _
                            "',ATP='" & objete.ATP & "',PhaseTI='" & objete.PhaseTI & "',PackageStatus='" & objete.PackageStatus & "'," & updateQ & "  where EPMId = '" & st & "'"
                        End If
                        'bugfix101006
                        objdb.ExeNonQuery(strst) 'removing this execution from uspEPMDInsert
                        '@@@@@@@@@@@@@@@@@@@@@@
                        objete.AT.RStatus = Constants.STATUS_ACTIVE
                        objete.AT.LMBY = Session("User_Name")
                        Dim kkt As Boolean = False
                        Dim sh As String = ""
                        Try
                            Dim strq As String = "exec uspEPMDInsert '" & objete.SiteId & "','" & objete.SiteName & "','" & objete.Region & "','" & objete.Zone & "','" & objete.WorkPackageId & "','" & objete.AT.LMBY & "','" & objete.PackageName & "','" & objete.PackageStatus.ToString.ToLower & "','" & objete.CustPONo & "'"
                            objdb.ExeNonQuery(strq)
                        Catch ex As Exception
                            objdb.ExeNonQuery("exec uspErrLog '', '" & objete.SiteId & "' ,'" & ex.Message.ToString & "','EPMuploading'")
                            ErrCount = ErrCount + 1
                        End Try
                        EPMRecCount = EPMRecCount + 1
                    End If
                    objete = Nothing
                Next
                'bugfix101129 updating the package status
                objdb.ExeNonQuery("update epmdata set packagestatus='Planned' where packagestatus in ('Work In Progress','Created')")
                'Updates IntegrationDate,Site Accept onAir, Site Accept on BAST
                objdae.uspEPMDataUpdate(Constants._EmptyDate, Session("User_Name"))
                'bugfix110111 new requirement to update the remapping site
                objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='IN Active', REMAPPEDFROM='EPM1' where workpkgid in (select workpackageid from epmdata where phaseti like '%Remapping%')")
                'updating the po
                objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='Active', REMAPPEDFROM='EPM1' where workpkgid not in (select workpackageid from epmdata where PackageStatus in ('Cancelled'))")
                objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='In Active', REMAPPEDFROM='EPM1' where workpkgid in (select workpackageid from epmdata where PackageStatus not in ('Planned','Completed'))")
                objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='IN ACTIVE', REMAPPEDFROM='EPM1' where workpkgid not in (select workpackageid from epmdata) and status='Active'")
                '--------------------------- New Code for phase as in-active status based 09032012 -----------------------------------------------
                objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='In Active', REMAPPEDFROM='EPM1' where workpkgid in (select workpackageid from epmdata where PhaseTI like '%cancelled%')")
                '---------------------------------------------------------------------------------------------------------------------------------
                'bugfix100427 Cleaning TIMilestone
                objdb.ExeQuery("delete from timilestone where milestone not in (select column_name from table_field_name where table_name='epmdata')")
                'Updating with NULL 
                Dim dtmile As New DataTable
                Dim p As Integer
                dtmile = objdb.ExeQueryDT("select Milestone from TIMilestone", "dtmile1")
                For p = 0 To dtmile.Rows.Count - 1
                    Try
                        qry = "Update EPMData Set " & dtmile.Rows(p).Item(0).ToString & " = NULL where Convert(varchar(8)," & dtmile.Rows(p).Item(0).ToString & " ,112) = " & Constants._EmptyDate & ""
                        objdb.ExeNonQuery(qry)
                    Catch ex As Exception
                        objdb.ExeNonQuery("exec uspErrLog '', 'Updating with NULL' ,'" & ex.Message.ToString & qry & "','Updating with NULL'")
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Warning.. Updating with NULL Error " & dtmile.Rows(p).Item(0).ToString & "');", True)
                        Exit For
                    End Try
                Next
                uploadViewLog(strFileName, EPMRecCount)
                EPMcountnew.InnerHtml = "<font color='green'>Successful EPM Transactions : " & EPMRecCount & "</font>"
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('No file was Selected');", True)
        End If
    End Sub

    Protected Sub btnproceed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnproceed.Click
        Dim dtquerys As DataTable
        dtquerys = Session("dtquerys")
        Dim k As Integer
        For k = 0 To dtquerys.Rows.Count - 1
            'bugfix101203 for the new epm v2.0 requirement			
            'objdb.ExeNonQuery(dtquerys.Rows(k).Item(0).ToString.Replace("Work In Progress", "Planned").Replace("Created", "Planned"))
			'bugfix101203 for the new epm v2.0 requirement
            Try
                objdb.ExeNonQuery(dtquerys.Rows(k).Item(0).ToString.Replace("Work In Progress", "Planned").Replace("Created", "Planned"))
            Catch ex As Exception
                'hcptcontrol.ErrorLogInsert(100, "btnProceed_click:frmPOUpload.aspx", ex.Message.ToString(), "NON-SP")
				hcptcontrol.ErrorLogInsert(100, "btnProceed_click:frmPOUpload.aspx", ex.Message.ToString() & ": " & dtquerys.Rows(k).Item(0).ToString, "NON-SP")
            End Try
        Next
        'update records
        Dim dtuquerys As DataTable
        dtuquerys = Session("dtuquerys")
        Dim m As Integer
        For m = 0 To dtuquerys.Rows.Count - 1
            objdb.ExeNonQuery(dtuquerys.Rows(m).Item(0).ToString)
        Next
        Dim qry As String = ""
        'Updates IntegrationDate,Site Accept onAir, Site Accept on BAST
        objdae.uspEPMDataUpdate(Constants._EmptyDate, Session("User_Name"))
        'bugfix110111 new requirement to update the remapping site
        objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='IN Active', REMAPPEDFROM='EPM1' where workpkgid in (select workpackageid from epmdata where phaseti like '%Remapping%')")
        'updating the po
        objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='Active', REMAPPEDFROM='EPM1' where workpkgid not in (select workpackageid from epmdata where PackageStatus in ('Cancelled'))")
        objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='In Active', REMAPPEDFROM='EPM1' where workpkgid in (select workpackageid from epmdata where PackageStatus not in ('Planned','Completed'))")
        objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='IN ACTIVE', REMAPPEDFROM='EPM1' where workpkgid not in (select workpackageid from epmdata) and status='Active'")
        '--------------------------- New Code for phase as in-active status based 09032012 -----------------------------------------------
        objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='In Active', REMAPPEDFROM='EPM1' where workpkgid in (select workpackageid from epmdata where PhaseTI like '%cancelled%')")
        '---------------------------------------------------------------------------------------------------------------------------------
        'bugfix100427 Cleaning TIMilestone
        objdb.ExeQuery("delete from timilestone where milestone not in (select column_name from table_field_name where table_name='epmdata')")
        'Updating with NULL 
        Dim dtmile As New DataTable
        Dim p As Integer
        dtmile = objdb.ExeQueryDT("select Milestone from TIMilestone", "dtmile1")
        For p = 0 To dtmile.Rows.Count - 1
            Try
                qry = "Update EPMData Set " & dtmile.Rows(p).Item(0).ToString & " = NULL where Convert(varchar(8)," & dtmile.Rows(p).Item(0).ToString & " ,112) = " & Constants._EmptyDate & ""
                objdb.ExeNonQuery(qry)
            Catch ex As Exception
                objdb.ExeNonQuery("exec uspErrLog '', 'Updating with NULL' ,'" & ex.Message.ToString & qry & "','Updating with NULL'")
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Warning.. Updating with NULL Error " & dtmile.Rows(p).Item(0).ToString & "');", True)
                Exit For
            End Try
        Next
        objdb.ExeNonQuery("truncate table EPMDATA_NEW")
        noissuescount.InnerHtml = "No Issues" & 0
        btnproceed.Visible = False
    End Sub

    Protected Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        UltraWebGridExcelExporter1.Export(EPMAfterValidating, "EPMvalidatedata")
    End Sub
End Class
