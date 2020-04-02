Imports System.Data
Imports System.Data.OleDb
Imports Businesslogic
Imports Entities
Imports Common
Imports System.IO
Partial Class frmWCCUpload
    Inherits System.Web.UI.Page
    Dim objBo As New BOPoUpload   'DAPoupload
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
    End Sub
    Protected Sub btnview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnview.Click
        If POUpload.PostedFile.ContentLength <> 0 Then
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
            "Data Source=" & filepathserver & ";" & _
            "Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
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
            Dim i As Integer
            Dim k As Integer = dv.Count  'myDataset.Tables(0).Rows.Count
            Dim aa1 As Integer = 0
            Dim aa2 As Integer = 0
            Dim aa3 As Integer = 0
            Dim aa4 As Integer = 0
            Dim aa5 As Integer = 0
            Dim PCount As Integer = 0
            'Dim a As String = "/Total Sites/Customer PO/TSEL Project ID/WPId/Site ID/Reason/Scope/New Band Type/BAND/New Configuration/900 Purchased/1800 Purchased/Hardware (OUTDOOR/INDOOR)/ID Code/QTY/Planned Antenna/Antenna Qty/Feeder Length/" & _
            '                  "Feeder Type/Feeder Qty/Existing Configuration/Dual Band/PO Type/Value in USD/Value in IDR/Value 2 in IDR/"
            Dim a As String = "/Total Sites/PO Type/Customer PO/PO Name/TSEL Project ID/WPId/Site ID EPM/Site ID PO/Scope/Reason (1)/Reason (2)/Value In USD/Value In IDR/Value 2 in IDR/"
            Dim cc1 As String = ""
            Dim msg As String = ""
            For m As Integer = 0 To 13
                Dim aa As DataColumn
                aa = dv.Table.Columns(m) 'myDataset.Tables(0).Columns(m)
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
            ' commented by satish on 1st october  new template no more dynamic columns
            '@@@@@@@@@@@@@@@@@@@@@@@Cheking the new column and adding new column@@@@@@@@@@@@@@@@@@@@@
            'For r As Integer = 14 To dv.Table.Columns.Count - 1
            '    Dim aa As DataColumn
            '    aa = dv.Table.Columns(r) 'myDataset.Tables(0).Columns(m)
            '    Dim sh As String = Trim(aa.ColumnName.ToString) '"PLN" & "_" & aa.ColumnName.ToString.Substring(2, 4)
            '    objdb.ExeUpdate("uspAlterpotable '" & sh & "'")
            'Next
            '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            Dim ErrCount As Integer = 0
            Dim testflag As Boolean
            For i = Constants.PO_Excel_Row_Start To dv.Count - 1
                objET = New ETPoUpload
                testflag = True
                With objET
                    Try
                        Dim dr As DataRow
                        dr = dv.Item(i).Row
                        .PoNo = LTrim(RTrim(dr.Item("Customer PO").ToString)) 'dr.Item("Customer PO").ToString.Replace("'", "''") 
                        .WorkPkgId = IIf(dr.Item("WPId").ToString <> "", dr.Item("WPId").ToString.Replace("'", "''"), 0) 'IIf(dr.Item(2).ToString <> "", dr.Item(2).ToString.Replace("'", "''"), 0)
                        .SiteNo = dr.Item("Site ID EPM").ToString.Replace("'", "''") 'dr.Item(3).ToString.Replace("'", "''")
                        '.FldType = dr.Item("Reason").ToString.Replace("'", "''") 'dr.Item(4).ToString.Replace("'", "''")
                        .FldType = Trim(dr.Item("Reason (1)").ToString.Replace("'", "''").Replace("-", " ") & " " & dr.Item("Reason (2)").ToString.Replace("'", "''").Replace("-", " ")) 'dr.Item(4).ToString.Replace("'", "''")
                        '.Band_Type = dr.Item("Band Type").ToString.Replace("'", "''") 'dr.Item(5).ToString.Replace("'", "''")
                        '.Band = dr.Item("Band").ToString.Replace("'", "''") 'dr.Item(6).ToString.Replace("'", "''")
                        '.Config = dr.Item("New Configuration").ToString.Replace("'", "''") 'dr.Item(7).ToString.Replace("'", "''")
                        '.Purchase900 = IIf(dr.Item("900 Purchased").ToString <> "", dr.Item("900 Purchased").ToString, 0) 'IIf(dr.Item(8).ToString <> "", dr.Item(8).ToString, 0)
                        '.Purchase1800 = IIf(dr.Item("1800 Purchased").ToString <> "", dr.Item("1800 Purchased").ToString, 0) 'IIf(dr.Item(9).ToString <> "", dr.Item(9).ToString, 0)
                        '.CPurchase900 = .Purchase900.ToString
                        '.CPurchase1800 = .Purchase1800.ToString
                        '.BSSHW = dr.Item("Hardware (OUTDOOR/INDOOR)").ToString.Replace("'", "''") 'dr.Item(10).ToString.Replace("'", "''")
                        '.BSSCode = dr.Item("ID Code").ToString.Replace("'", "''") 'dr.Item(11).ToString.Replace("'", "''")
                        '.Qty = IIf(dr.Item("Qty").ToString <> "", dr.Item("Qty").ToString, 0) 'IIf(dr.Item(12).ToString <> "", dr.Item(12).ToString, 0)
                        '.CQty = .Qty
                        '.AntennaName = dr.Item("Planned Antenna").ToString.Replace("'", "''") 'dr.Item(13).ToString.Replace("'", "''")
                        .POName = dr.Item("PO Name").ToString.Replace("'", "''")
                        .POType = dr.Item("PO Type").ToString.Replace("'", "''")
                        'Dim AQ() As String
                        'AQ = dr.Item("Antenna Qty").ToString.Split("+")
                        'Dim tot As Integer = 0
                        'If AQ.Length > 1 Then
                        '    For aa As Integer = 0 To AQ.Length - 1
                        '        tot = tot + configTot(AQ(aa))
                        '    Next
                        '    .AntennaQty = tot
                        'Else
                        '    .AntennaQty = IIf(dr.Item("Antenna Qty").ToString <> "", dr.Item("Antenna Qty").ToString, 0) 'IIf(dr.Item(14).ToString <> "", dr.Item(14).ToString, 0)
                        'End If
                        '.Feederlen = IIf(dr.Item("Feeder Length").ToString <> "", dr.Item("Feeder Length").ToString, 0) 'IIf(dr.Item(15).ToString <> "", dr.Item(15).ToString, 0)
                        '.FeederType = dr.Item("Feeder Type").ToString.Replace("'", "''") 'dr.Item(16).ToString.Replace("'", "''")
                        '.FeederQty = IIf(dr.Item("Feeder Qty").ToString <> "", dr.Item("Feeder Qty").ToString.Replace("'", "''"), "") 'IIf(dr.Item(17).ToString <> "", dr.Item(17).ToString, 0)
                        .Value1 = IIf(dr.Item("Value in USD").ToString <> "", dr.Item("Value in USD").ToString, 0) 'IIf(dr.Item(18).ToString <> "", dr.Item(18).ToString, 0)
                        .Value2 = IIf(dr.Item("Value in IDR").ToString <> "", dr.Item("Value in IDR").ToString, 0) 'IIf(dr.Item(19).ToString <> "", dr.Item(19).ToString, 0)
                        .Pstatus = Constants.PO_PStatus_Pending
                        .AT.RStatus = Constants.STATUS_ACTIVE
                        .AT.LMBY = Session("User_Name")
                        '.ExistConfig = dr.Item("Existing Configuration").ToString.Replace("'", "''")
                        .Value2IDR = IIf(dr.Item("Value 2 in IDR").ToString <> "", dr.Item("Value 2 in IDR").ToString, 0) '
                        .scopegroup = dr.Item("scope").ToString.Replace("'", "''")
                        tselpid = dr.Item("TSEL Project ID").ToString.Replace("'", "''")
                        siteidPO = dr.Item("Site ID PO").ToString.Replace("'", "''")
                        'for case 1
                        If .WorkPkgId <> "" And .WorkPkgId = "0" Then
                            .ECase1 = 1
                            aa1 = aa1 + 1
                        End If
                        'for case 2
                        'Dim scope As String = .FldType
                        'scope = UCase(.FldType)
                        'If scope.IndexOf(Constants._FLDType_New) > -1 Then
                        '    'Response.Write(.FldType)
                        '    If .Band <> "Dual Band" And .Band <> "" Then
                        '    ElseIf .Band <> "" And .Band = "Dual Band" Then 'c by sat
                        '        If .CQty <> 2 Then
                        '            .ECase4 = 1
                        '            .CQty = 2
                        '            If .ECase1 = 0 And .Band <> "" Then aa4 = aa4 + 1 ''c by sat

                        '        End If
                        '    End If
                        '    If configTot(.Config) <> .Purchase900 + .Purchase1800 Then
                        '        .ECase5 = 1
                        '        If .ECase1 = 0 Then aa5 = aa5 + 1
                        '    End If

                        '    If .Band = "Dual Band" Then
                        '        Dim ab() As String
                        '        Dim st As String = .Config
                        '        ab = st.Split("+")
                        '        'Dim tot As Integer = 0
                        '        If ab.GetLength(0) = 2 Then
                        '            .CPurchase900 = configTot(ab(0))
                        '            .CPurchase1800 = configTot(ab(1))
                        '        End If
                        '    ElseIf .Band <> "" Then
                        '        'commneted by satish
                        '        'If Mid(.Band, 4, Len(.Band)) = 1800 Then
                        '        '    .CPurchase1800 = configTot(.Config)
                        '        'ElseIf Mid(.Band, 4, Len(.Band)) = 900 Then
                        '        '    .CPurchase900 = configTot(.Config)
                        '        'End If
                        '    End If
                        'End If
                    Catch ex As Exception
                        objdb.ExeNonQuery("exec uspErrLog '', 'poupload','" & ex.Message.ToString.Replace("'", "''") & "','pouploading'")
                        ErrCount = ErrCount + 1
                        testflag = False
                    End Try
                    If testflag = True Then ' to check datatype error mismatch or some expection
                        Try
                            'commented by satish on 1st october  new template no more dynamic columns
                            '%%%%%%%%%%%%%%%%%%%%
                            ' ''Dim updatequery1 As String = ""
                            ' ''Dim updatequery2 As String = ""
                            ' ''Dim cname As String = ""
                            ' ''Dim cvalue As String = ""
                            ' ''Dim cv As String = ""
                            '' ''create update query
                            ' ''For r As Integer = 14 To dv.Table.Columns.Count - 1
                            ' ''    Dim aa As DataColumn
                            ' ''    aa = dv.Table.Columns(r) 'myDataset.Tables(0).Columns(m)
                            ' ''    cname = Trim(aa.ColumnName.ToString)
                            ' ''    If dv.Table.Rows(i).Item(r).ToString <> "" Then
                            ' ''        cvalue = "''" & cst.formatdateDDMMYYYY(Format(CDate(dv.Table.Rows(i).Item(r)), "dd/MM/yyyy")) & "''"
                            ' ''    Else
                            ' ''        cvalue = "null"
                            ' ''    End If

                            ' ''    cv = IIf(cv <> "", cv & "," & cname & "=" & cvalue, cname & "=" & cvalue)
                            ' ''Next
                            ' ''updatequery1 = "update porawdata set " & cv
                            ' ''updatequery2 = "update podetails set " & cv
                            'objBo.uspPORawInsert(objET)
                            '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                            Dim Testing As String = "This is just for testing"
                            With objET
                                strsqlf = "Exec uspWCCPORawInsert '" & .PoNo & "','" & .SiteNo & "','" & .SiteName & "','" & .WorkPkgId & "','" & .FldType & "','" & .Description & "','" & _
                                .Band_Type & "','" & .Band & "','" & .Config & "'," & .Purchase900 & "," & .Purchase1800 & ",'" & .BSSHW & "','" & .BSSCode & "','" & _
                                .AntennaName & "'," & .AntennaQty & ",'" & .Feederlen & "','" & .FeederType & "','" & .FeederQty & "'," & .Pstatus & "," & .AT.RStatus & ",'" & _
                                .AT.LMBY & "'," & .Qty & "," & .Value1 & "," & .Value2 & "," & .ECase1 & "," & .ECase2 & "," & .ECase3 & "," & .ECase4 & "," & .ECase5 & "," & _
                                .CPurchase900 & "," & .CPurchase1800 & "," & .CQty & ",'" & .ExistConfig & "','" & .POType & "','" & .POName & "','" & .Vendor & "'," & .Value2IDR & "," & _
                                "'" & .scopegroup & "','" & tselpid & "','" & siteidPO & "','" & Testing & "' "
                            End With
                            objdb.ExeQueryScalar(strsqlf)
                            tselpid = ""
                            If .ECase1 = 0 Then PCount = PCount + 1
                        Catch ex As Exception
                            PCount = PCount + 1
                            objdb.ExeNonQuery("exec uspErrLog '', 'poupload','" & ex.Message.ToString.Replace("'", "''") & "','pouploading'")
                        End Try
                    End If
                End With


            Next
            '' ADDED BY SATISH TO UPDATE DEFAULT DATE
            'objdb.ExeNonQuery("EXEC uspPOUpdatenew  " & Constants._EmptyDate & "")

            '@@@@@@@@@@@@@@@@
            Session("RawPONo") = dv.Table.Rows(0).Item("Customer PO").ToString 'myDataset.Tables(0).Rows(i - 1).Item(1).ToString
            '    DupSites.InnerText = ""
            '    Dim stCount As Integer = 0
            '    stCount = objBo.uspSiteCount(Session("RawPONo"))
            '    stCount = stCount
            '    Dim s1 As String = ""
            '    Dim s2 As String = ""
            '    Dim s3 As String = ""
            '    Dim s4 As String = ""
            '    Dim s5 As String = ""
            '    Dim s6 As String = ""
            '    If aa1 > 0 Then
            '        A1.Visible = True
            '        A1.InnerText = "Missing Workpackage Id : " & aa1
            '        s1 = A1.InnerText
            '    Else
            '        A1.Visible = False
            '        s1 = ""
            '    End If
            '    'If aa2 > 0 Then
            '    '    A2.Visible = True
            '    '    A2.InnerText = "Configuration Error (Band1800 - Purchased Shows in 900) : " & aa2
            '    '    s2 = A2.InnerText
            '    'Else
            '    '    A2.Visible = False
            '    '    s2 = ""
            '    'End If
            '    'If aa3 > 0 Then
            '    '    A3.Visible = True
            '    '    A3.InnerText = "Configuration Error (Band900 - Purchased Shows in 1800) : " & aa3
            '    '    s3 = A3.InnerText
            '    'Else
            '    '    A3.Visible = False
            '    '    s3 = ""
            '    'End If
            '    'If aa4 > 0 Then
            '    '    A4.Visible = True
            '    '    A4.InnerText = "Dual Band - Qty MisMatch : " & aa4
            '    '    s4 = A4.InnerText
            '    'Else
            '    '    A4.Visible = False
            '    '    s4 = ""
            '    'End If
            '    'If aa5 > 0 Then
            '    '    A5.Visible = True
            '    '    A5.InnerText = "Configuration Error (config shows 333+444, But Quantity is not matching with Config Total) : " & aa5
            '    '    s5 = A5.InnerText
            '    'Else
            '    '    A5.Visible = False
            '    '    s5 = ""
            '    'End If
            '    If stCount > 0 Then
            '        DupSites.InnerText = "Updated Sites : " & stCount
            '        s6 = DupSites.InnerText
            '    Else
            '        s6 = ""
            '    End If
            '    If ErrCount > 0 Then
            '        errrow.InnerHtml = "<font color='orangered'> Datatype Mismatch count : " & ErrCount & "</font>"
            '    End If
            '    PrCount.InnerHtml = "<font color='green'>Successfull PO Transactions : " & PCount & "</font>"
            '    uploadPOViewLog(strFileName, PCount, s1, s2, s3, s4, s5, s6)
            '    objdo.UPLFileName = strFileName.Replace("'", "''")
            '    objdo.TrCount = PCount - stCount
            '    objdo.MisIdCount = aa1
            '    objdo.Pur1800 = aa2
            '    objdo.Pur900 = aa3
            '    objdo.DualQty = aa4
            '    objdo.ConFigErr = aa5
            '    objdo.MisSite = stCount
            '    objdo.AT.RStatus = Constants.STATUS_ACTIVE
            '    objdo.AT.LMBY = Session("User_Name")
            '    objBo.uspPOErrLogInsert(objdo)
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Uploaded Successfully');", True)
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
    'Protected Sub btnEview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEview.Click
    '    Dim Poname As String = ""
    '    If EPMUpload.PostedFile.ContentLength <> 0 Then
    '        Dim myFile As HttpPostedFile = EPMUpload.PostedFile
    '        If System.IO.Path.GetExtension(myFile.FileName.ToLower) <> ".xls" Then
    '            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Only Excel files Allowed');", True)
    '            Exit Sub
    '        End If
    '        Dim myDataset As New DataSet()
    '        Dim strFileName As String = EPMUpload.PostedFile.FileName
    '        strFileName = System.IO.Path.GetFileName(strFileName)
    '        Dim serverpath As String = Server.MapPath("EPMRAWDATA")
    '        EPMUpload.PostedFile.SaveAs(serverpath + "\" + strFileName)
    '        Dim filepathserver As String = serverpath & "\" & strFileName
    '        Dim strConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
    '        "Data Source=" & filepathserver & ";" & _
    '        "Extended Properties=""Excel 8.0;"""
    '        Dim EPMSheet As String = ConfigurationManager.AppSettings("EPMSheet").ToString
    '        Dim dv As DataView
    '        Try
    '            Dim myData As New OleDbDataAdapter("SELECT  * FROM [" & EPMSheet & "$] ", strConn)
    '            myData.TableMappings.Add("Table", "ExcelTest")
    '            myData.Fill(myDataset)
    '            Try
    '                dv = myDataset.Tables(0).DefaultView
    '                dv.Sort = "Customer Site Code"
    '                dv.RowFilter = "[Customer Site Code] <> ''"
    '            Catch ex As Exception
    '                Response.Write("<script language='javascript'> alert('Please check column Name : [Customer Site Code]');</script>")
    '                Exit Sub
    '            End Try
    '        Catch ex As Exception
    '            If ex.Message.IndexOf("Sheet1$") = 1 Then
    '                Response.Write("<script language='javascript'> alert('Please check sheet name in selected Excel file,it should be Sheet1');</script>")
    '            End If
    '            Exit Sub
    '        End Try
    '        Dim a As String = "/Customer Site Code/Site Name/Project Region/Project Zone/Package Type/Package ID/Package Name/Package Status/Phase/Customer PO Received Date/Customer PO Number/PO Name/Subcontractor/Contract Task Completed/Contract Plan BAUT/Contract Plan BAST/"
    '        Dim bc As String = "Customer Site Code/Site Name/Project Region/Project Zone/Package Type/Package ID/Package Name/Package Status/Phase/Customer PO Received Date/Customer PO Number/PO Name/Subcontractor/Contract Task Completed/Contract Plan BAUT/Contract Plan BAST/"
    '        Dim cd() As String = bc.Split("/")
    '        Dim xy As Boolean
    '        Dim strmsg As String = ""
    '        For k As Integer = 0 To cd.Length - 1
    '            xy = False
    '            For k1 As Integer = 0 To 15
    '                If dv.Table.Columns(k1).ColumnName.ToString.ToUpper = cd(k).ToUpper Then
    '                    xy = True
    '                    Exit For
    '                End If
    '            Next
    '            If xy = False Then
    '                strmsg = strmsg & IIf(strmsg <> "", ",", "") & cd(k)
    '            End If
    '        Next
    '        If strmsg <> "" Then
    '            Response.Write("<script language='javascript'>alert('Please check column Names : \n" & strmsg & "');</script>")
    '            Exit Sub
    '        Else
    '            Dim cc1 As String = ""
    '            Dim msg As String = ""
    '            For m As Integer = 0 To 15
    '                Dim aa As DataColumn
    '                aa = dv.Table.Columns(m) 'myDataset.Tables(0).Columns(m)
    '                cc1 = "/" & aa.ColumnName.ToString.ToUpper & "/"
    '                If a.ToString.ToUpper.IndexOf(cc1) >= 0 Then
    '                Else
    '                    msg = msg & IIf(msg <> "", ",", "") & aa.ColumnName
    '                End If
    '            Next
    '            If msg <> "" Then
    '                Response.Write("<script language='javascript'>alert('Please check column Names : \n" & msg & "');</script>")
    '                Exit Sub
    '            End If
    '            Dim i As Integer
    '            'Dim k As Integer = myDataset.Tables(0).Rows.Count
    '            Dim dr As DataRow
    '            Dim EPMRecCount As Integer = 0
    '            Dim ErrCount As Integer = 0
    '            '@@@@@@@@@@@@@@@@@@@@@@@Cheking the new column and adding new column@@@@@@@@@@@@@@@@@@@@@
    '            For r As Integer = 16 To dv.Table.Columns.Count - 1
    '                Dim aa As DataColumn
    '                aa = dv.Table.Columns(r) 'myDataset.Tables(0).Columns(m)
    '                Dim sh As String = UCase(Right(aa.ColumnName, 3)) & "_" & Left(aa.ColumnName, 4)
    '                Dim k As Integer = objdb.ExeUpdate("Exec uspTIMilestoneInsert  '" & sh & "'")
    '                If k = 1 Then
    '                    viewflag = True
    '                    objdb.ExeUpdate("Alter table EPMDATA add " & sh & " Datetime")
    '                ElseIf k = 3 Then
    '                    viewflag = True
    '                End If
    '            Next
    '            If viewflag = True Then
    '                '@@@@@@@@@Creating view@@@@@@@@@@@@@@@

    '                'FOR TI

    '                'strTI1 = "SELECT 'TI' AS Process, E.SiteId, E.SiteName, E.Region, E.Zone, E.PhaseTI AS Phase, E.WorkPackageId, E.PackageType, E.PackageName, E.PackageStatus," + _
    '                '                     "E.CustPONo, E.CustPORecDt, ISNULL(P.POType, '') AS POType, ISNULL(P.POName, '') AS POName, "
    '                'dtTI = objdb.ExeQueryDT(" select milestone from ( select 'E.'+ MileStone  as MileStone  from TIMilestone  Union select 'Null AS ' + Milestone as MileStone from NSNCMEKL..CMEMilestone Union select 'Null AS ' + Milestone as MileStone from NSNSISKL..SISMilestone Union select 'Null AS ' + Milestone  as MileStone from NSNSItacKL..SItacMilestone ) as TT order by RIGHT(RTRIM(milestone),4) ", "dtt1")
    '                'For t = 0 To dtTI.Rows.Count - 1
    '                '    strTI2 = IIf(strTI2 = "", dtTI.Rows(t).Item(0).ToString & ",", strTI2 & dtTI.Rows(t).Item(0).ToString & ",")
    '                'Next
    '                'strTI3 = "E.RStatus,  E.LMBY, E.LMDT FROM dbo.EPMDATA AS E INNER JOIN dbo.PODetails AS P ON E.WorkPackageId = P.WorkPkgId AND E.CustPONo = P.PoNo AND E.SiteId = P.SiteNo"
    '                'strTIFinal = strTI1 & strTI2 & strTI3
    '                ''FOR CME

    '                'strCME1 = "SELECT 'CME' AS Process, E.SiteId, E.SiteName, E.Region, E.Zone, E.PhaseTI AS Phase, E.WorkPackageId, E.PackageType, E.PackageName, E.PackageStatus," + _
    '                '                     "E.CustPONo, E.CustPORecDt, ISNULL(P.POType, '') AS POType, ISNULL(P.POName, '') AS POName, "
    '                'dtCME = objdb.ExeQueryDT("select milestone from ( select  'Null AS ' + Milestone as MileStone from TIMilestone  Union select 'E.'+ MileStone  as MileStone  from NSNCMEKL..CMEMilestone Union select 'Null AS ' + Milestone as MileStone from NSNSISKL..SISMilestone Union select 'Null AS ' + Milestone  as MileStone from NSNSItacKL..SItacMilestone) as TT order by RIGHT(RTRIM(milestone),4) ", "dtt2")
    '                'For t = 0 To dtCME.Rows.Count - 1
    '                '    strCME2 = IIf(strCME2 = "", dtCME.Rows(t).Item(0).ToString & ",", strCME2 & dtCME.Rows(t).Item(0).ToString & ",")
    '                'Next
    '                'strCME3 = "E.RStatus,  E.LMBY, E.LMDT FROM dbo.EPMDATACME AS E INNER JOIN dbo.PODetails AS P ON E.WorkPackageId = P.WorkPkgId AND E.CustPONo = P.PoNo AND E.SiteId = P.SiteNo"
    '                'strCMEFinal = strCME1 & strCME2 & strCME3
    '                ''for SIS

    '                'strSIS1 = "SELECT 'SIS' AS Process, E.SiteId, E.SiteName, E.Region, E.Zone, E.PhaseTI AS Phase, E.WorkPackageId, E.PackageType, E.PackageName, E.PackageStatus," + _
    '                '                     "E.CustPONo, E.CustPORecDt, ISNULL(P.POType, '') AS POType, ISNULL(P.POName, '') AS POName, "
    '                'dtSIS = objdb.ExeQueryDT(" select milestone from ( select 'Null AS ' + Milestone  as MileStone  from TIMilestone  Union select 'Null AS ' + Milestone as MileStone from NSNCMEKL..CMEMilestone Union select 'E.'+ MileStone  as MileStone from NSNSISKL..SISMilestone Union select 'Null AS ' + Milestone  as MileStone from NSNSItacKL..SItacMilestone) as TT order by RIGHT(RTRIM(milestone),4) ", "dtt3")
    '                'For t = 0 To dtSIS.Rows.Count - 1
    '                '    strSIS2 = IIf(strSIS2 = "", dtSIS.Rows(t).Item(0).ToString & ",", strSIS2 & dtSIS.Rows(t).Item(0).ToString & ",")
    '                'Next
    '                'strSIS3 = "E.RStatus,  E.LMBY, E.LMDT FROM dbo.EPMDATA AS E INNER JOIN dbo.PODetails AS P ON E.WorkPackageId = P.WorkPkgId AND E.CustPONo = P.PoNo AND E.SiteId = P.SiteNo"
    '                'strSISFinal = strSIS1 & strSIS2 & strSIS3
    '                ''for SITAC

    '                'strSitac1 = "SELECT 'Sitac' AS Process, E.SiteId, E.SiteName, E.Region, E.Zone, E.PhaseTI AS Phase, E.WorkPackageId, E.PackageType, E.PackageName, E.PackageStatus," + _
    '                '                     "E.CustPONo, E.CustPORecDt, ISNULL(P.POType, '') AS POType, ISNULL(P.POName, '') AS POName, "
    '                'dtSitac = objdb.ExeQueryDT(" select milestone from ( select  'Null AS ' + Milestone as MileStone  from TIMilestone  Union select 'Null AS ' + Milestone as MileStone from NSNCMEKL..CMEMilestone Union select 'Null AS ' + Milestone as MileStone from NSNSISKL..SISMilestone Union select 'E.'+ MileStone  as MileStone from NSNSItacKL..SItacMilestone) as TT order by RIGHT(RTRIM(milestone),4) ", "dtt4")
    '                'For t = 0 To dtSitac.Rows.Count - 1
    '                '    strSitac2 = IIf(strSitac2 = "", dtSitac.Rows(t).Item(0).ToString & ",", strSitac2 & dtSitac.Rows(t).Item(0).ToString & ",")
    '                'Next
    '                'strSitac3 = "E.RStatus,  E.LMBY, E.LMDT FROM dbo.EPMDATA AS E INNER JOIN dbo.PODetails AS P ON E.WorkPackageId = P.WorkPkgId AND E.CustPONo = P.PoNo AND E.SiteId = P.SiteNo"
    '                'strSitacFinal = strSitac1 & strSitac2 & strSitac3

    '                'strfinal = strTIFinal & " UNION " & strCMEFinal '& " UNION " & strSISFinal & " UNION " & strSitacFinal
    '                'objdb.ExeNonQuery("Alter View EBAST_Milestone_View_new as  " & strfinal)
    '            End If

    '            '@@@@@@@@@@@@@@@@@@@@@@@@@
    '            For i = Constants.EPM_Excel_Row_Start To dv.Count - 1 'myDataset.Tables(0).Rows.Count - 1
    '                dr = myDataset.Tables(0).Rows(i)
    '                objete = New ETEPMData
    '                If Trim(myDataset.Tables(0).Rows(i).Item("Customer Site Code").ToString) <> "" Then
    '                    objete.SiteId = myDataset.Tables(0).Rows(i).Item("Customer Site Code").ToString
    '                    objete.SiteName = dr.Item("Site Name").ToString.Replace("'", "''")
    '                    objete.Region = dr.Item("Project Region").ToString.Replace("'", "''") 'Region have in 2 col Merge
    '                    objete.Zone = IIf(dr.Item("Project Zone").ToString <> "", dr.Item(3).ToString, Constants.EmptyZone) 'Zone have in 2 col Merge
    '                    objete.PackageType = dr.Item("Package Type").ToString.Replace("'", "''")
    '                    objete.WorkPackageId = dr.Item("Package ID").ToString.Replace("'", "''")
    '                    objete.PackageName = dr.Item("Package Name").ToString.Replace("'", "''")
    '                    objete.PackageStatus = dr.Item("Package Status").ToString.Replace("'", "''")
    '                    objete.PhaseTI = dr.Item("Phase").ToString.Replace("'", "''")
    '                    If dr.Item("Customer PO Received Date").ToString <> "" And Trim(dr.Item("Customer PO Received Date").ToString).Length >= 10 Then
    '                        objete.CustPORecDt = cst.formatdateDDMMYYYY(Format(CDate(dr.Item("Customer PO Received Date").ToString), "dd/MM/yyyy"))
    '                    End If
    '                    objete.CustPONo = dr.Item("Customer PO Number").ToString.Replace("'", "''")
    '                    'objete.ProjectID = dr.Item("TSEL Project ID").ToString.Replace("'", "''")
    '                    objete.SubconTI = dr.Item("Subcontractor").ToString.Replace("'", "''")
    '                    'Poname = dr.Item("PO Name").ToString.Replace("'", "''")
    '                    If dr.Item("Contract Task Completed").ToString <> "" Then
    '                        CTaskCompleted = cst.formatdateDDMMYYYY(Format(CDate(dr.Item("Contract Task Completed").ToString.Replace("'", "''")), "dd/MM/yyyy"))
    '                    End If
    '                    If dr.Item("Contract Plan BAUT").ToString <> "" Then
    '                        CPlanBAUT = cst.formatdateDDMMYYYY(Format(CDate(dr.Item("Contract Plan BAUT").ToString.Replace("'", "''")), "dd/MM/yyyy"))
    '                    End If
    '                    If dr.Item("Contract Plan BAST").ToString <> "" Then
    '                        CPlanBAST = cst.formatdateDDMMYYYY(Format(CDate(dr.Item("Contract Plan BAST").ToString.Replace("'", "''")), "dd/MM/yyyy"))
    '                    End If

    '                    Dim mainq As String = ""
    '                    Dim valueq As String = ""
    '                    Dim updateQ As String = ""
    '                    Dim uq As String = ""
    '                    For m As Integer = 16 To dv.Table.Columns.Count - 1
    '                        Dim aa As DataColumn
    '                        aa = dv.Table.Columns(m) 'myDataset.Tables(0).Columns(m)
    '                        Dim aa1 As String = ""
    '                        Dim aa2 As String = ""
    '                        If dv.Table.Rows(i).Item(m).ToString <> "" Then
    '                            aa1 = cst.formatdateDDMMYYYY(Format(CDate(dv.Table.Rows(i).Item(m)), "dd/MM/yyyy"))
    '                            aa2 = "''" & cst.formatdateDDMMYYYY(Format(CDate(dv.Table.Rows(i).Item(m)), "dd/MM/yyyy")) & "''"
    '                        Else
    '                            aa1 = "''''"
    '                            aa2 = "''''"

    '                        End If
    '                        'NO NEED NOW
    '                        'FillData(aa, aa1)
    '                        Dim sh1 As String = UCase(Right(aa.ColumnName, 3)) & "_" & Left(aa.ColumnName, 4)
    '                        If sh1 = "ACT_9350" And aa1 <> "''''" Then
    '                            objete.SiteIntegration = aa1
    '                        End If
    '                        uq = sh1 & "=" & aa2
    '                        updateQ = IIf(updateQ <> "", updateQ & uq & ",", uq & ",")
    '                        mainq = IIf(mainq <> "", mainq & sh1 & ",", sh1 & ",")
    '                        valueq = IIf(valueq <> "", valueq & aa2 & ",", aa2 & ",")

    '                    Next
    '                    mainq = mainq.Substring(0, Len(mainq) - 1)
    '                    valueq = valueq.Substring(0, Len(valueq) - 1)
    '                    updateQ = updateQ.Substring(0, Len(updateQ) - 1)

    '                    '@@@@@@@@@@@@@@@@@@@@@ preparing insert statement based on columns@@@@@@@@@@@@@@@@@@@@@@@
    '                    Dim strst As String = ""
    '                    Dim st As Integer = 0
    '                    st = objdb.ExeQueryScalar("SELECT EPMID FROM EPMData where SiteID='" & objete.SiteId & "' and WorkPackageId='" & objete.WorkPackageId & "'")
    '                    If st = 0 Then
    '                        strst = " INSERT INTO EPMDATA(SiteId,SiteName,Region,Zone,Address,City,PhaseTI,TISubcon,WorkPackageId,SiteIntegration," + _
    '                           "SiteAcpOnAir,SiteAcpOnBAST,Pstatus,RStatus,LMBY,LMDT,PackageType,PackageName,PackageStatus,CustPONo,CustPORecDt,Equipment_Arrived, Equipment_On_Site, ATP,CTaskComp,CPlanBAUT,CPlanBAST," & mainq & " )" + _
    '                           " VALUES (''" & objete.SiteId & "'',''" & objete.SiteName & "'',''" & objete.Region & "'',''" & objete.Zone & "'',''" & objete.Address & "'',''" & objete.City & "''," + _
    '                           "''" & objete.PhaseTI & "'',''" & objete.SubconTI & "'',''" & objete.WorkPackageId & "'',''" & objete.SiteIntegration & "'',''" & objete.SiteAcpOnAir & "'',''" & objete.SiteAcpOnBAST & "''," + _
    '                           "''" & objete.pstatus & "'',''" & objete.AT.RStatus & "'',''" & objete.AT.LMBY & "'',GetDate(),''" & objete.PackageType & "'',''" & objete.PackageName & "''," + _
    '                           "''" & objete.PackageStatus & "'',''" & objete.CustPONo & "'',''" & objete.CustPORecDt & "'',''" & objete.Equipment_Arrived & "'',''" & objete.Equipment_On_Site & "'',''" & objete.ATP & "'',''" & CTaskCompleted & "'',''" & CPlanBAUT & "'',''" & CPlanBAST & "''," & valueq & ")"
    '                        If objete.SubconTI <> "" Then
    '                            objdb.ExeNonQuery("Exec uspSubConIUfromEPM '" & objete.SubconTI & "','" & objete.AT.LMBY & "'")
    '                        End If
    '                    Else
    '                        If objete.SubconTI <> "" Then
    '                            objdb.ExeNonQuery("Exec uspSubConIUfromEPM '" & objete.SubconTI & "','" & objete.AT.LMBY & "'")
    '                        End If
    '                        strst = " Update EPMData Set SiteIntegration= ''" & objete.SiteIntegration & "'', SiteAcpOnAir=''" & objete.SiteAcpOnAir & "'',SiteAcpOnBAST = ''" & objete.SiteAcpOnBAST & "''," + _
    '                                " Equipment_Arrived=''" & objete.Equipment_Arrived & "'',Equipment_On_Site=''" & objete.Equipment_On_Site & "'',CTaskComp = ''" & CTaskCompleted & "'' ,CPlanBAUT = ''" & CPlanBAST & "'',CPlanBAST=''" & CPlanBAST & "'' ,ATP=''" & objete.ATP & "'',PackageStatus=''" & objete.PackageStatus & "''," & updateQ & "  where EPMId = ''" & st & "''"
    '                    End If
    '                    '@@@@@@@@@@@@@@@@@@@@@@
    '                    objete.AT.RStatus = Constants.STATUS_ACTIVE
    '                    objete.AT.LMBY = Session("User_Name")
    '                    Dim kkt As Boolean = False
    '                    Dim sh As String = ""
    '                    Try
    '                        'Dim epmid As Integer'''comeneted this function and added new by passing the created insert statement to new sp.
    '                        'epmid = objdae.insertepmrawdata(objete)
    '                        '@@@@@@@@@@@

    '                        Dim strq As String = "exec uspEPMDInsert '" & objete.SiteId & "','" & objete.SiteName & "','" & objete.Region & "','" & objete.Zone & "','" & objete.WorkPackageId & "','" & objete.AT.LMBY & "','" & objete.PackageName & "','" & objete.PackageStatus.ToString.ToLower & "','" & objete.CustPONo & "','" & strst & "'"
    '                        objdb.ExeNonQuery(strq)
    '                    Catch ex As Exception
    '                        objdb.ExeNonQuery("exec uspErrLog '', '" & objete.SiteId & "' ,'" & ex.Message.ToString.Replace("'", "''") & "','EPMuploading'")
    '                        ErrCount = ErrCount + 1
    '                    End Try
    '                    EPMRecCount = EPMRecCount + 1
    '                End If
    '                objete = Nothing
    '            Next
    '            'EPMcount.InnerHtml = "<font color='green'>Success Full EPM Transactions : " & myDataset.Tables(0).Rows.Count - Constants.EPM_Excel_Row_Start & "</font>"
    '            objdae.uspEPMDataUpdate(Constants._EmptyDate, Session("User_Name")) 'Updates IntegrationDate,Site Accept onAir, Site Accept on BAST
    '            objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='In Active',REMAPPEDFROM='EPM1' where workpkgid  in (select workpackageid from  epmdata where PackageStatus not in ('Cancelled','planned'))")
    '            objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='IN ACTIVE', REMAPPEDFROM='EPM1' WHERE WORKPKGID NOT IN(SELECT WORKPACKAGEID FROM EPMDATA) AND STATUS='ACTIVE'")

    '            'Updating with NULL 
    '            Dim dtmile As New DataTable
    '            Dim p As Integer
    '            dtmile = objdb.ExeQueryDT("select Milestone from TIMilestone", "dtmile1")
    '            For p = 0 To dtmile.Rows.Count - 1
    '                objdb.ExeNonQuery("Update EPMData Set " & dtmile.Rows(p).Item(0).ToString & " = NULL where Convert(varchar(8)," & dtmile.Rows(p).Item(0).ToString & " ,112) = " & Constants._EmptyDate & "")
    '            Next
    '            uploadViewLog(strFileName, EPMRecCount)
    '            EPMcount.InnerHtml = "<font color='green'>Successful EPM Transactions : " & EPMRecCount & "</font>"
    '            'GenerateMileStoneView()
    '            'objdb.ExeUpdate("Exec USPMILESTONE")

    '        End If
    '    Else
    '        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('No file was Selected');", True)
    '    End If

    'End Sub








    'new function from seeta..

    'Sub GenerateMileStoneView()
    '    Dim strTI As String = "", strTINull As String = ""
    '    Dim strCME As String = "", strCMENull As String = ""
    '    Dim strSIS As String = "", strSISNull As String = ""
    '    Dim strSITAC As String = "", strSITACNull As String = ""

    '    Dim strTIMain As String = "SELECT 'TI' AS Process, E.SiteId, E.SiteName, E.Region, E.Zone, E.PhaseTI AS Phase, E.WorkPackageId, E.PackageType, " & _
    '    "E.PackageName, E.PackageStatus,E.CustPONo, E.CustPORecDt,ISNULL(P.POType, '') AS POType, ISNULL(P.POName, '') AS POName, "

    '    Dim strCMEMain As String = "SELECT 'CME' AS Process, E.SiteId, E.SiteName, E.Region, E.Zone, E.PhaseTI AS Phase, E.WorkPackageId, E.PackageType, " & _
    '    "E.PackageName, E.PackageStatus,E.CustPONo, E.CustPORecDt,ISNULL(P.POType, '') AS POType, ISNULL(P.POName, '') AS POName, "

    '    Dim strSISMain As String = "SELECT 'SIS' AS Process, E.SiteId, E.SiteName, E.Region, E.Zone, E.PhaseTI AS Phase, E.WorkPackageId, E.PackageType, " & _
    '    "E.PackageName, E.PackageStatus,E.CustPONo, E.CustPORecDt,ISNULL(P.POType, '') AS POType, ISNULL(P.POName, '') AS POName, "

    '    Dim strSITACMain As String = "SELECT 'SITac' AS Process, E.SiteId, E.SiteName, E.Region, E.Zone, E.PhaseTI AS Phase, E.WorkPackageId, E.PackageType, " & _
    '    "E.PackageName, E.PackageStatus,E.CustPONo, E.CustPORecDt,ISNULL(P.POType, '') AS POType, ISNULL(P.POName, '') AS POName, "

    '    Dim strTIEnd As String = ",E.RStatus,E.LMBY,E.LMDT FROM NSNTIPILOT..EPMDATA AS E INNER JOIN NSNTIPILOT..PODetails AS P ON E.WorkPackageId = P.WorkPkgId AND E.CustPONo = P.PoNo AND E.SiteId = P.SiteNo "
    '    Dim strCMEEnd As String = ",E.RStatus,E.LMBY, E.LMDT FROM NSNCMEPilot..EPMDATA AS E INNER JOIN NSNCMEPilot..PODetails AS P ON E.WorkPackageId = P.WorkPkgId AND E.CustPONo = P.PoNo AND E.SiteId = P.SiteNo"
    '    Dim strSISEnd As String = ",E.RStatus,E.LMBY, E.LMDT FROM NSNSISPilot..EPMDATA AS E INNER JOIN NSNSISPilot..PODetails AS P ON E.WorkPackageId = P.WorkPkgId AND E.CustPONo = P.PoNo AND E.SiteId = P.SiteNo"
    '    Dim strSITacEnd As String = ",E.RStatus,E.LMBY, E.LMDT FROM NSNSITACPilot..EPMDATA AS E INNER JOIN NSNSITACPilot..PODetails AS P ON E.WorkPackageId = P.WorkPkgId AND E.CustPONo = P.PoNo AND E.SiteId = P.SiteNo"

    '    Dim dtTI, dtCME, dtSIS, dtSITac As DataTable

    '    dtTI = objdb.ExeQueryDT("select c.Name from NSNTIPILOT..sysobjects A inner join NSNTIPILOT..syscolumns c on a.id= c.id inner join NSNTIPILOT..systypes T on c.xtype=T.xusertype where A.xtype='U'  and a.name='EPMDATA' and (c.name like 'ACT_%' or c.name like 'FOR_%')", "EPMData")
    '    dtCME = objdb.ExeQueryDT("select c.Name from NSNCMEPILOT..sysobjects A inner join NSNCMEPILOT..syscolumns c on a.id= c.id inner join NSNCMEPILOT..systypes T on c.xtype=T.xusertype where A.xtype='U'  and a.name='EPMDATA' and (c.name like 'ACT_%' or c.name like 'FOR_%')", "EPMData")
    '    dtSIS = objdb.ExeQueryDT("select c.Name from NSNSISPILOT..sysobjects A inner join NSNSISPILOT..syscolumns c on a.id= c.id inner join NSNSISPILOT..systypes T on c.xtype=T.xusertype where A.xtype='U'  and a.name='EPMDATA' and (c.name like 'ACT_%' or c.name like 'FOR_%')", "EPMData")
    '    dtSITac = objdb.ExeQueryDT("select c.Name from NSNSITACPILOT..sysobjects A inner join NSNSITACPILOT..syscolumns c on a.id= c.id inner join NSNSITACPILOT..systypes T on c.xtype=T.xusertype where A.xtype='U'  and a.name='EPMDATA' and (c.name like 'ACT_%' or c.name like 'FOR_%')", "EPMData")

    '    If dtTI.Rows.Count > 0 Then
    '        For i As Integer = 0 To dtTI.Rows.Count - 1
    '            strTI = strTI & IIf(strTI = "", "", ",") & "E." & dtTI.Rows(i).Item(0).ToString
    '            strTINull = strTINull & IIf(strTINull = "", "", ",") & "Null as " & dtTI.Rows(i).Item(0).ToString
    '        Next
    '    End If

    '    If dtCME.Rows.Count > 0 Then
    '        For j As Integer = 0 To dtCME.Rows.Count - 1
    '            strCME = strCME & IIf(strCME = "", "", ",") & "E." & dtCME.Rows(j).Item(0).ToString
    '            strCMENull = strCMENull & IIf(strCMENull = "", "", ",") & "Null as " & dtCME.Rows(j).Item(0).ToString
    '        Next
    '    End If

    '    If dtSIS.Rows.Count > 0 Then
    '        For k As Integer = 0 To dtSIS.Rows.Count - 1
    '            strSIS = strSIS & IIf(strSIS = "", "", ",") & "E." & dtSIS.Rows(k).Item(0).ToString
    '            strSISNull = strSISNull & IIf(strSISNull = "", "", ",") & "Null as " & dtSIS.Rows(k).Item(0).ToString
    '        Next
    '    End If

    '    If dtSITac.Rows.Count > 0 Then
    '        For m As Integer = 0 To dtSITac.Rows.Count - 1
    '            strSITAC = strSITAC & IIf(strSITAC = "", "", ",") & "E." & dtSITac.Rows(m).Item(0).ToString
    '            strSITACNull = strSITACNull & IIf(strSITACNull = "", "", ",") & "Null as " & dtSITac.Rows(m).Item(0).ToString
    '        Next
    '    End If

    '    Dim strView As String = ""

    '    strView = "Create view TI_View as " & strTIMain.Replace("'", "''") & strTI & "," & strCMENull & "," & strSISNull & "," & strSITACNull & strTIEnd
    '    objdb.ExeUpdate("Exec DropView 'TI_View','" & strView & "'")

    '    strView = "Create view CME_View as " & strCMEMain.Replace("'", "''") & strTINull & "," & strCME & "," & strSISNull & "," & strSITACNull & strCMEEnd
    '    objdb.ExeUpdate("Exec DropView 'CME_View','" & strView & "'")

    '    strView = "Create view SIS_View as " & strSISMain.Replace("'", "''") & strTINull & "," & strCMENull & "," & strSIS & "," & strSITACNull & strSISEnd
    '    objdb.ExeUpdate("Exec DropView 'SIS_View','" & strView & "'")

    '    strView = "Create view SITAC_View as " & strSITACMain.Replace("'", "''") & strTINull & "," & strCMENull & "," & strSISNull & "," & strSITAC & strSITacEnd
    '    objdb.ExeUpdate("Exec DropView 'SITAC_View','" & strView & "'")

    'End Sub

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
            'If tot = objET.Purchase1800 + objET.Purchase900 Then
            '    Return 0
            'Else
            '    Return 1
            'End If
        End If
    End Function
    Sub go()

    End Sub
End Class
