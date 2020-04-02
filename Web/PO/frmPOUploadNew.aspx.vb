Imports System.Data
Imports System.Data.OleDb
Imports Businesslogic
Imports Entities
Imports Common
Imports System.IO
Partial Class frmPOUploadNew
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
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
        If EPMUpload.PostedFile.ContentLength <> 0 Then
            Dim myFile As HttpPostedFile = EPMUpload.PostedFile
            If System.IO.Path.GetExtension(myFile.FileName.ToLower) <> ".xls" Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Only Excel files Allowed');", True)
                Exit Sub
            End If
            Dim myDataset As New DataSet()
            Dim strFileName As String = EPMUpload.PostedFile.FileName
            strFileName = System.IO.Path.GetFileName(strFileName)
            Dim serverpath As String = Server.MapPath("EPMRAWDATA")
            EPMUpload.PostedFile.SaveAs(serverpath + "\" + strFileName)
            Dim filepathserver As String = serverpath & "\" & strFileName
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
                Catch ex As Exception
                    Response.Write("<script language='javascript'> alert('Please check column Name : [Customer Site Code]');</script>")
                    Exit Sub
                End Try
            Catch ex As Exception
                If ex.Message.IndexOf("Sheet1$") = 1 Then
                    Response.Write("<script language='javascript'> alert('Please check sheet name in selected Excel file,it should be Sheet1');</script>")
                End If
                Exit Sub
            End Try
            Dim a As String = "/Customer Site Code/Site Name/Project Region/Project Zone/Package Type/Package ID/Package Name/Package Status/Phase/Customer PO Received Date/Customer PO Number/TSEL Project ID/Subcon"
            Dim bc As String = "Customer Site Code/Site Name/Project Region/Project Zone/Package Type/Package ID/Package Name/Package Status/Phase/Customer PO Received Date/Customer PO Number/TSEL Project ID/Subcon"
            Dim cd() As String = bc.Split("/")
            Dim xy As Boolean
            Dim strmsg As String = ""
            For k As Integer = 0 To cd.Length - 1
                xy = False
                For k1 As Integer = 0 To 13
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
                For m As Integer = 0 To 10
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
                Dim i As Integer
                'Dim k As Integer = myDataset.Tables(0).Rows.Count
                Dim dr As DataRow
                Dim EPMRecCount As Integer = 0
                Dim ErrCount As Integer = 0
                '@@@@@@@@@@@@@@@@@@@@@@@Cheking the new column and adding new column@@@@@@@@@@@@@@@@@@@@@
                For r As Integer = 13 To dv.Table.Columns.Count - 1
                    Dim aa As DataColumn
                    aa = dv.Table.Columns(r) 'myDataset.Tables(0).Columns(m)
                    Dim sh As String = UCase(Right(aa.ColumnName, 3)) & "_" & Left(aa.ColumnName, 4)
                    Dim k As Integer = objdb.ExeUpdate("Exec uspTIMilestoneInsert  '" & sh & "'")
                    If k = 1 Then
                        viewflag = True
                        objdb.ExeNonQuery("Alter table EPMDATA add " & sh & " Datetime")
                    End If
                Next
                If viewflag = True Then
                    '@@@@@@@@@Creating view@@@@@@@@@@@@@@@

                    'FOR TI

                    strTI1 = "SELECT 'TI' AS Process, E.SiteId, E.SiteName, E.Region, E.Zone, E.PhaseTI AS Phase, E.WorkPackageId, E.PackageType, E.PackageName, E.PackageStatus," + _
                                         "E.CustPONo, E.CustPORecDt, ISNULL(P.POType, '') AS POType, ISNULL(P.POName, '') AS POName, "
                    dtTI = objdb.ExeQueryDT(" select milestone from ( select 'E.'+ MileStone  as MileStone  from TIMilestone  Union select 'Null AS ' + Milestone as MileStone from CMEMilestone Union select 'Null AS ' + Milestone as MileStone from SISMilestone Union select 'Null AS ' + Milestone  as MileStone from SitacMilestone ) as TT order by RIGHT(RTRIM(milestone),4) ", "dtt1")
                    For t = 0 To dtTI.Rows.Count - 1
                        strTI2 = IIf(strTI2 = "", dtTI.Rows(t).Item(0).ToString & ",", strTI2 & dtTI.Rows(t).Item(0).ToString & ",")
                    Next
                    strTI3 = "E.RStatus,  E.LMBY, E.LMDT FROM dbo.EPMDATA AS E INNER JOIN dbo.PODetails AS P ON E.WorkPackageId = P.WorkPkgId AND E.CustPONo = P.PoNo AND E.SiteId = P.SiteNo"
                    strTIFinal = strTI1 & strTI2 & strTI3
                    'FOR CME

                    strCME1 = "SELECT 'CME' AS Process, E.SiteId, E.SiteName, E.Region, E.Zone, E.PhaseTI AS Phase, E.WorkPackageId, E.PackageType, E.PackageName, E.PackageStatus," + _
                                         "E.CustPONo, E.CustPORecDt, ISNULL(P.POType, '') AS POType, ISNULL(P.POName, '') AS POName, "
                    dtCME = objdb.ExeQueryDT("select milestone from ( select  'Null AS ' + Milestone as MileStone from TIMilestone  Union select 'E.'+ MileStone  as MileStone  from CMEMilestone Union select 'Null AS ' + Milestone as MileStone from SISMilestone Union select 'Null AS ' + Milestone  as MileStone from SitacMilestone) as TT order by RIGHT(RTRIM(milestone),4) ", "dtt2")
                    For t = 0 To dtCME.Rows.Count - 1
                        strCME2 = IIf(strCME2 = "", dtCME.Rows(t).Item(0).ToString & ",", strCME2 & dtCME.Rows(t).Item(0).ToString & ",")
                    Next
                    strCME3 = "E.RStatus,  E.LMBY, E.LMDT FROM dbo.EPMDATACME AS E INNER JOIN dbo.PODetails AS P ON E.WorkPackageId = P.WorkPkgId AND E.CustPONo = P.PoNo AND E.SiteId = P.SiteNo"
                    strCMEFinal = strCME1 & strCME2 & strCME3
                    'for SIS

                    strSIS1 = "SELECT 'SIS' AS Process, E.SiteId, E.SiteName, E.Region, E.Zone, E.PhaseTI AS Phase, E.WorkPackageId, E.PackageType, E.PackageName, E.PackageStatus," + _
                                         "E.CustPONo, E.CustPORecDt, ISNULL(P.POType, '') AS POType, ISNULL(P.POName, '') AS POName, "
                    dtSIS = objdb.ExeQueryDT(" select milestone from ( select 'Null AS ' + Milestone  as MileStone  from TIMilestone  Union select 'Null AS ' + Milestone as MileStone from CMEMilestone Union select 'E.'+ MileStone  as MileStone from SISMilestone Union select 'Null AS ' + Milestone  as MileStone from SitacMilestone) as TT order by RIGHT(RTRIM(milestone),4) ", "dtt3")
                    For t = 0 To dtSIS.Rows.Count - 1
                        strSIS2 = IIf(strSIS2 = "", dtSIS.Rows(t).Item(0).ToString & ",", strSIS2 & dtSIS.Rows(t).Item(0).ToString & ",")
                    Next
                    strSIS3 = "E.RStatus,  E.LMBY, E.LMDT FROM dbo.EPMDATA AS E INNER JOIN dbo.PODetails AS P ON E.WorkPackageId = P.WorkPkgId AND E.CustPONo = P.PoNo AND E.SiteId = P.SiteNo"
                    strSISFinal = strSIS1 & strSIS2 & strSIS3
                    'for SITAC

                    strSitac1 = "SELECT 'Sitac' AS Process, E.SiteId, E.SiteName, E.Region, E.Zone, E.PhaseTI AS Phase, E.WorkPackageId, E.PackageType, E.PackageName, E.PackageStatus," + _
                                         "E.CustPONo, E.CustPORecDt, ISNULL(P.POType, '') AS POType, ISNULL(P.POName, '') AS POName, "
                    dtSitac = objdb.ExeQueryDT(" select milestone from ( select  'Null AS ' + Milestone as MileStone  from TIMilestone  Union select 'Null AS ' + Milestone as MileStone from CMEMilestone Union select 'Null AS ' + Milestone as MileStone from SISMilestone Union select 'E.'+ MileStone  as MileStone from SitacMilestone) as TT order by RIGHT(RTRIM(milestone),4) ", "dtt4")
                    For t = 0 To dtSitac.Rows.Count - 1
                        strSitac2 = IIf(strSitac2 = "", dtSitac.Rows(t).Item(0).ToString & ",", strSitac2 & dtSitac.Rows(t).Item(0).ToString & ",")
                    Next
                    strSitac3 = "E.RStatus,  E.LMBY, E.LMDT FROM dbo.EPMDATA AS E INNER JOIN dbo.PODetails AS P ON E.WorkPackageId = P.WorkPkgId AND E.CustPONo = P.PoNo AND E.SiteId = P.SiteNo"
                    strSitacFinal = strSitac1 & strSitac2 & strSitac3

                    strfinal = strTIFinal & " UNION " & strCMEFinal '& " UNION " & strSISFinal & " UNION " & strSitacFinal
                    objdb.ExeNonQuery("Alter View EBAST_Milestone_View_new as  " & strfinal)
                End If

                '@@@@@@@@@@@@@@@@@@@@@@@@@
                For i = Constants.EPM_Excel_Row_Start To dv.Count - 1 'myDataset.Tables(0).Rows.Count - 1
                    dr = myDataset.Tables(0).Rows(i)
                    objete = New ETEPMData
                    If Trim(myDataset.Tables(0).Rows(i).Item("Customer Site Code").ToString) <> "" Then
                        objete.SiteId = myDataset.Tables(0).Rows(i).Item("Customer Site Code").ToString
                        objete.SiteName = dr.Item("Site Name").ToString.Replace("'", "''")
                        objete.Region = dr.Item("Project Region").ToString.Replace("'", "''") 'Region have in 2 col Merge
                        objete.Zone = IIf(dr.Item("Project Zone").ToString <> "", dr.Item(3).ToString, Constants.EmptyZone) 'Zone have in 2 col Merge
                        objete.PackageType = dr.Item("Package Type").ToString.Replace("'", "''")
                        objete.WorkPackageId = dr.Item("Package ID").ToString.Replace("'", "''")
                        objete.PackageName = dr.Item("Package Name").ToString.Replace("'", "''")
                        objete.PackageStatus = dr.Item("Package Status").ToString.Replace("'", "''")
                        objete.PhaseTI = dr.Item("Phase").ToString.Replace("'", "''")
                        If dr.Item("Customer PO Received Date").ToString <> "" And Trim(dr.Item("Customer PO Received Date").ToString).Length >= 10 Then
                            objete.CustPORecDt = cst.formatdateDDMMYYYY(Format(CDate(dr.Item("Customer PO Received Date").ToString), "dd/MM/yyyy"))
                        End If
                        objete.CustPONo = dr.Item("Customer PO Number").ToString.Replace("'", "''")
                        objete.ProjectID = dr.Item("TSEL Project ID").ToString.Replace("'", "''")
                        objete.SubconTI = dr.Item("Subcon").ToString.Replace("'", "''")

                        Dim mainq As String = ""
                        Dim valueq As String = ""
                        For m As Integer = 13 To dv.Table.Columns.Count - 1
                            Dim aa As DataColumn
                            aa = dv.Table.Columns(m) 'myDataset.Tables(0).Columns(m)
                            Dim aa1 As String = ""
                            Dim aa2 As String = ""
                            If dv.Table.Rows(i).Item(m).ToString <> "" Then
                                aa1 = cst.formatdateDDMMYYYY(Format(CDate(dv.Table.Rows(i).Item(m)), "dd/MM/yyyy"))
                                aa2 = "''" & cst.formatdateDDMMYYYY(Format(CDate(dv.Table.Rows(i).Item(m)), "dd/MM/yyyy")) & "''"
                            Else
                                aa1 = "''''"
                                aa2 = "''''"

                            End If
                            'NO NEED NOW
                            'FillData(aa, aa1)
                            Dim sh1 As String = UCase(Right(aa.ColumnName, 3)) & "_" & Left(aa.ColumnName, 4)
                            mainq = IIf(mainq <> "", mainq & sh1 & ",", sh1 & ",")
                            valueq = IIf(valueq <> "", valueq & aa2 & ",", aa2 & ",")
                        Next
                        mainq = mainq.Substring(0, Len(mainq) - 1)
                        valueq = valueq.Substring(0, Len(valueq) - 1)

                        '@@@@@@@@@@@@@@@@@@@@@ preparing insert statement based on columns@@@@@@@@@@@@@@@@@@@@@@@
                        Dim strst As String = ""
                        strst = " INSERT INTO EPMDATA(SiteId,SiteName,Region,Zone,Address,City,PhaseTI,TISubcon,WorkPackageId,SiteIntegration," + _
    "SiteAcpOnAir,SiteAcpOnBAST,Pstatus,RStatus,LMBY,LMDT,PackageType,PackageName,PackageStatus,CustPONo,CustPORecDt,Equipment_Arrived, Equipment_On_Site, ATP,ProjectID," & mainq & " )" + _
    " VALUES (''" & objete.SiteId & "'',''" & objete.SiteName & "'',''" & objete.Region & "'',''" & objete.Zone & "'',''" & objete.Address & "'',''" & objete.City & "''," + _
    "''" & objete.PhaseTI & "'',''" & objete.SubconTI & "'',''" & objete.WorkPackageId & "'',''" & objete.SiteIntegration & "'',''" & objete.SiteAcpOnAir & "'',''" & objete.SiteAcpOnBAST & "''," + _
    "''" & objete.pstatus & "'',''" & objete.AT.RStatus & "'',''" & objete.AT.LMBY & "'',GetDate(),''" & objete.PackageType & "'',''" & objete.PackageName & "''," + _
    "''" & objete.PackageStatus & "'',''" & objete.CustPONo & "'',''" & objete.CustPORecDt & "'',''" & objete.Equipment_Arrived & "'',''" & objete.Equipment_On_Site & "'',''" & objete.ATP & "'',''" & objete.ProjectID & "''," & valueq & ")"
                        '@@@@@@@@@@@@@@@@@@@@@@
                        objete.AT.RStatus = Constants.STATUS_ACTIVE
                        objete.AT.LMBY = Session("User_Name")
                        Dim kkt As Boolean = False
                        Dim sh As String = ""
                        Try
                            'Dim epmid As Integer'''comeneted this function and added new by passing the created insert statement to new sp.
                            'epmid = objdae.insertepmrawdata(objete)
                            '@@@@@@@@@@@
                            Dim strq As String = "exec uspEPMDInsert '" & objete.SiteId & "','" & objete.SiteName & "','" & objete.Region & "','" & objete.Zone & "','" & objete.WorkPackageId & "','" & objete.AT.LMBY & "','" & objete.PackageName & "','" & strst & "' "
                            objdb.ExeNonQuery(strq)
                        Catch ex As Exception
                            objdb.ExeNonQuery("exec uspErrLog '', 'EPMUpload','" & ex.Message.ToString.Replace("'", "''") & "','EPMuploading'")
                            ErrCount = ErrCount + 1
                        End Try
                        EPMRecCount = EPMRecCount + 1
                    End If
                    objete = Nothing
                Next
                'EPMcount.InnerHtml = "<font color='green'>Success Full EPM Transactions : " & myDataset.Tables(0).Rows.Count - Constants.EPM_Excel_Row_Start & "</font>"
                objdae.uspEPMDataUpdate(Constants._EmptyDate, Session("User_Name")) 'Updates IntegrationDate,Site Accept onAir, Site Accept on BAST
                'Updating with NULL 
                Dim dtmile As New DataTable
                Dim p As Integer
                dtmile = objdb.ExeQueryDT("select Milestone from TIMilestone", "dtmile1")
                For p = 0 To dtmile.Rows.Count - 1
                    objdb.ExeNonQuery("Update EPMData Set " & dtmile.Rows(p).Item(0).ToString & " = NULL where Convert(varchar(8)," & dtmile.Rows(p).Item(0).ToString & " ,112) = " & Constants._EmptyDate & "")
                Next
                uploadViewLog(strFileName, EPMRecCount)
                EPMcount.InnerHtml = "<font color='green'>Successful EPM Transactions : " & EPMRecCount & "</font>"
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
End Class
