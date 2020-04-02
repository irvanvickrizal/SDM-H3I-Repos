Imports System.Data
Imports System.Data.OleDb
Imports Businesslogic
Imports Entities
Imports Common
Imports System.IO
Partial Class MSD_frmNICUpload
    Inherits System.Web.UI.Page
    Dim objdo As New POErrLog
    Dim objdb As New DBUtil
    Dim viewflag As Boolean = True
    Dim objete As New ETEPMData
    Dim cst As New Constants
    Dim objdae As New BOEPMRawData
    Dim CTaskCompleted As String = ""
    Dim CPlanBAUT As String = ""
    Dim CPlanBAST As String = ""
    Dim SiteNamePO As String = ""
    Dim dtEpmData As New DataTable
    Dim dtMileStone As New DataTable
    Dim dt As New DataTable
    Dim strSQL As String
    Sub uploadViewLog(ByVal strFileName As String, ByVal recCount As Integer)
        Dim strOFile As String = strFileName
        strFileName = Year(Today) & "-" & MonthName(Month(Today)) & "-" & "NICData.txt"
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

    Protected Sub btnSaveData_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveData.Click
        Dim Poname As String = ""
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
             "Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
            Dim EPMSheet As String = "NIC"
            Dim dv As DataView
            Try
                Dim myData As New OleDbDataAdapter("SELECT * FROM [" & EPMSheet & "$] ", strConn)
                myData.TableMappings.Add("Table", "ExcelTest")
                myData.Fill(myDataset)
                Try
                    dv = myDataset.Tables(0).DefaultView
                    dv.Sort = "SiteID"
                    dv.RowFilter = "[SiteID] <> ''"
                Catch ex As Exception
                    Response.Write("<script language='javascript'> alert('Please check column Name : [Site ID]');</script>")
                    Exit Sub
                End Try
            Catch ex As Exception
                If ex.Message.IndexOf("Sheet1$") = 1 Then
                    Response.Write("<script language='javascript'> alert('Please check sheet name in selected Excel file,it should be Sheet1');</script>")
                Else
                    Response.Write(ex.Message)
                End If
                Exit Sub
            End Try
            Dim flds As String = ""
            For i As Integer = 0 To dv.Table.Columns.Count - 1
                flds = flds & dv.Table.Columns(i).ColumnName.ToString.ToUpper
                If i < dv.Table.Columns.Count - 1 Then
                    flds = flds & ","
                End If
            Next
            Dim vals As String = ""
            Dim sqls As String
            objdb.ExeQuery("DELETE FROM NICEPMFields")
            For j As Integer = 0 To dv.Table.Rows.Count - 1
                vals = ""
                For i As Integer = 0 To dv.Table.Columns.Count - 1
                    vals = vals & "'" & dv.Table.Rows(j).Item(i).ToString.Replace("'", "''") & "'"
                    If i < dv.Table.Columns.Count - 1 Then
                        vals = vals & ","
                    End If
                Next
                If vals <> "" Then
                    sqls = "INSERT INTO NICEPMFields (" & flds & ") VALUES (" & vals & ")"
                    objdb.ExeQuery(sqls)
                End If
            Next
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Upload Successful');", True)
            ''Dim a As String = "/SiteId/SiteName/Region/Zone/Address/City/WorkPackageId/SiteIntegration/SiteAcpOnAir/SiteAcpOnBAST/PackageType/PackageName/PackageStatus/CustPONo/POName/"
            ''Dim bc As String = "/SiteId/SiteName/Region/Zone/Address/City/WorkPackageId/SiteIntegration/SiteAcpOnAir/SiteAcpOnBAST/PackageType/PackageName/PackageStatus/CustPONo/POName/"
            ''Dim cd() As String = bc.Split("/")
            ''Dim xy As Boolean
            ''Dim strmsg As String = ""
            ''Dim str1 As String
            ''Dim str2 As String
            ''For k As Integer = 0 To cd.Length - 1
            ''    xy = False
            ''    For k1 As Integer = 0 To dv.Table.Columns.Count - 1
            ''        str1 = dv.Table.Columns(k1).ColumnName.ToString.ToUpper
            ''        str2 = cd(k).ToUpper()
            ''        If str1 = str2 Then
            ''            xy = True
            ''            Exit For
            ''        End If
            ''    Next
            ''    If xy = False Then
            ''        strmsg = strmsg & IIf(strmsg <> "", ",", "") & cd(k)
            ''    End If
            ''Next
            ''    If strmsg <> "" Then
            ''        Response.Write("<script language='javascript'>alert('Please check column Names : \n" & strmsg & "');</script>")
            ''        Exit Sub
            ''    Else
            ''        'Dim cc1 As String = ""
            ''        'Dim msg As String = ""
            ''        'For m As Integer = 0 To dv.Table.Columns.Count - 1
            ''        '    Dim aa As DataColumn
            ''        '    aa = dv.Table.Columns(m)
            ''        '    cc1 = "/" & aa.ColumnName.ToString.ToUpper & "/"
            ''        '    If a.ToString.ToUpper.IndexOf(cc1) >= 0 Then
            ''        '    Else
            ''        '        msg = msg & IIf(msg <> "", ",", "") & aa.ColumnName
            ''        '    End If
            ''        'Next
            ''        'If msg <> "" Then
            ''        '    Response.Write("<script language='javascript'>alert('Please check column Names : \n" & msg & "');</script>")
            ''        '    Exit Sub
            ''        'End If
            ''        Dim i As Integer
            ''        'Dim k As Integer = myDataset.Tables(0).Rows.Count
            ''        Dim dr As DataRow
            ''        Dim EPMRecCount As Integer = 0
            ''        Dim ErrCount As Integer = 0
            ''        '@@@@@@@@@@@@@@@@@@@@@@@Cheking the new column and adding new column@@@@@@@@@@@@@@@@@@@@@
            ''        For r As Integer = 15 To dv.Table.Columns.Count - 1
            ''            Dim aa As DataColumn
            ''            aa = dv.Table.Columns(r) 'myDataset.Tables(0).Columns(m)
            ''            Dim sh As String = UCase(Right(aa.ColumnName, 3)) & "_" & Left(aa.ColumnName, 4)
            ''            Dim k As Integer = objdb.ExeUpdate("Exec uspTIMilestoneDetailsInsert '" & sh & "'")
            ''            If k = 1 Then
            ''                viewflag = True
            ''                objdb.ExeUpdate("Alter table NICData add " & sh & " Datetime")
            ''            ElseIf k = 3 Then
            ''                viewflag = True
            ''            End If
            ''        Next
            ''        '@@@@
            ''        For i = 0 To myDataset.Tables(0).Rows.Count - 1
            ''            objdb.ExeNonQuery("Delete from NICData where CustPONo='" & myDataset.Tables(0).Rows(i).Item("CustPoNo") & "'")
            ''        Next
            ''        '@@@@@@@@@@@@@@@@@@@@@@@@@
            ''        For i = Constants.EPM_Excel_Row_Start To dv.Count - 1 'myDataset.Tables(0).Rows.Count - 1
            ''            dr = myDataset.Tables(0).Rows(i)
            ''            objete = New ETEPMData
            ''            If Trim(myDataset.Tables(0).Rows(i).Item("SiteId").ToString) <> "" Then
            ''                objete.SiteId = myDataset.Tables(0).Rows(i).Item("SiteId").ToString
            ''                objete.SiteName = dr.Item("SiteName").ToString.Replace("'", "''")
            ''                objete.Region = dr.Item("Region").ToString.Replace("'", "''") 'Region have in 2 col Merge
            ''                objete.Zone = IIf(dr.Item("Zone").ToString <> "", dr.Item(3).ToString, Constants.EmptyZone) 'Zone have in 2 col Merge
            ''                objete.Address = dr.Item("Address").ToString.Replace("'", "''")
            ''                objete.City = dr.Item("City").ToString.Replace("'", "''")
            ''                objete.WorkPackageId = dr.Item("WorkPackageId").ToString.Replace("'", "''")
            ''                objete.SiteIntegration = dr.Item("SiteIntegration").ToString.Replace("'", "''")
            ''                objete.SiteAcpOnAir = dr.Item("SiteAcpOnAir").ToString.Replace("'", "''")
            ''                objete.SiteAcpOnBAST = dr.Item("SiteAcpOnBAST").ToString.Replace("'", "''")
            ''                objete.PackageType = dr.Item("PackageType").ToString.Replace("'", "''")
            ''                objete.PackageName = dr.Item("PackageName").ToString.Replace("'", "''")
            ''                objete.PackageStatus = dr.Item("PackageStatus").ToString.Replace("'", "''")
            ''                objete.CustPONo = dr.Item("CustPONo").ToString.Replace("'", "''")

            ''                Dim mainq As String = ""
            ''                Dim valueq As String = ""
            ''                Dim updateQ As String = ""
            ''                Dim uq As String = ""
            ''                For m As Integer = 16 To dv.Table.Columns.Count - 1
            ''                    Dim aa As DataColumn
            ''                    aa = dv.Table.Columns(m)
            ''                    Dim aa1 As String = ""
            ''                    Dim aa2 As String = ""
            ''                    If dv.Table.Rows(i).Item(m).ToString <> "" Then
            ''                        aa1 = cst.formatdateDDMMYYYY(Format(CDate(dv.Table.Rows(i).Item(m)), "dd/MM/yyyy"))
            ''                        aa2 = "''" & cst.formatdateDDMMYYYY(Format(CDate(dv.Table.Rows(i).Item(m)), "dd/MM/yyyy")) & "''"
            ''                    Else
            ''                        aa1 = "''''"
            ''                        aa2 = "''''"
            ''                    End If
            ''                    Dim sh1 As String = UCase(Right(aa.ColumnName, 3)) & "_" & Left(aa.ColumnName, 4)
            ''                    If sh1 = "ACT_9350" And aa1 <> "''''" Then
            ''                        objete.SiteIntegration = aa1
            ''                    End If
            ''                    uq = sh1 & "=" & aa2
            ''                    updateQ = IIf(updateQ <> "", updateQ & uq & ",", uq & ",")
            ''                    mainq = IIf(mainq <> "", mainq & sh1 & ",", sh1 & ",")
            ''                    valueq = IIf(valueq <> "", valueq & aa2 & ",", aa2 & ",")

            ''                Next
            ''                mainq = mainq.Substring(0, Len(mainq) - 1)
            ''                valueq = valueq.Substring(0, Len(valueq) - 1)
            ''                updateQ = updateQ.Substring(0, Len(updateQ) - 1)
            ''                '@@@@@@@@@@@@@@@@@@@@@ preparing insert statement based on columns@@@@@@@@@@@@@@@@@@@@@@@
            ''                Dim strst As String = ""
            ''                Dim st As Integer = 0
            ''                st = objdb.ExeQueryScalar("SELECT MSID FROM NICData where SiteID='" & objete.SiteId & "' and WorkPackageId='" & objete.WorkPackageId & "'")
            ''                If st = 0 Then
            ''                    strst = " INSERT INTO NICData(SiteId,SiteName,Region,Zone,Address,City,WorkPackageId,SiteIntegration," + _
            ''                       "SiteAcpOnAir,SiteAcpOnBAST,RStatus,LMBY,LMDT,PackageType,PackageName,PackageStatus,CustPONo )" + _
            ''                       " VALUES ('" & objete.SiteId & "','" & objete.SiteName & "','" & objete.Region & "','" & objete.Zone & "','" & objete.Address & "','" & objete.City & "'," + _
            ''                       "'" & objete.WorkPackageId & "','" & objete.SiteIntegration & "','" & objete.SiteAcpOnAir & "','" & objete.SiteAcpOnBAST & "'," + _
            ''                       "'" & objete.AT.RStatus & "','" & objete.AT.LMBY & "',GetDate(),'" & objete.PackageType & "','" & objete.PackageName & "'," + _
            ''                       "'" & objete.PackageStatus & "','" & objete.CustPONo & "')"
            ''                    objdb.ExeNonQuery(strst)
            ''                Else
            ''                    strst = " Update NICData Set SiteIntegration= ''" & objete.SiteIntegration & "'', SiteAcpOnAir=''" & objete.SiteAcpOnAir & "'',SiteAcpOnBAST = ''" & objete.SiteAcpOnBAST & "''," + _
            ''                            " PackageStatus=''" & objete.PackageStatus & "''," & updateQ & "  where EPMId = ''" & st & "''"
            ''                End If
            ''                '@@@@@@@@@@@@@@@@@@@@@@
            ''                objete.AT.RStatus = Constants.STATUS_ACTIVE
            ''                objete.AT.LMBY = Session("User_Name")
            ''                Dim kkt As Boolean = False
            ''                Dim sh As String = ""
            ''                Try
            ''                    'Dim epmid As Integer'''comeneted this function and added new by passing the created insert statement to new sp.
            ''                    'epmid = objdae.insertepmrawdata(objete)
            ''                    '@@@@@@@@@@@
            ''                Catch ex As Exception
            ''                    objdb.ExeNonQuery("exec uspErrLog '', '" & objete.SiteId & "' ,'" & ex.Message.ToString.Replace("'", "''") & "','EPMuploading'")
            ''                    ErrCount = ErrCount + 1
            ''                End Try
            ''                EPMRecCount = EPMRecCount + 1
            ''            End If
            ''            objete = Nothing
            ''        Next
            ''        strSQL = "Exec uspMileStoneUpdate  '" & Constants._EmptyDate & "', '" & Session("User_Name") & "'"
            ''        objdb.ExeQueryDT(strSQL, "Table1")
            ''        objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='In Active',REMAPPEDFROM='EPM1' where workpkgid  in (select workpackageid from  NICData where PackageStatus not in ('Cancelled','planned'))")
            ''        objdb.ExeNonQuery("UPDATE PODETAILS SET STATUS='IN ACTIVE', REMAPPEDFROM='EPM1' WHERE WORKPKGID NOT IN(SELECT WORKPACKAGEID FROM NICData) AND STATUS='ACTIVE'")
            ''        'Updating with NULL 
            ''        Dim dtmile As New DataTable
            ''        uploadViewLog(strFileName, EPMRecCount)
            ''        EPMcount.InnerHtml = "<font color='green'>Successful EPM Transactions : " & EPMRecCount & "</font>"
            ''    End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('No file was Selected');", True)
        End If
    End Sub
End Class
