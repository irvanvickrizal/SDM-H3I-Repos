Imports System.Data
Imports System.Data.OleDb
Imports Businesslogic
Imports Entities
Imports Common
Imports System.IO
Partial Class MSD_frmMileStoneDetails
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack = True Then
            fillMileStoneData()
            fillEPMData()
            fillUnSelected()
            fillNICEPMData()
            'fillNICEPMDataTemplate()
        End If
    End Sub

    Sub fillMileStoneData()
        strSQL = "Exec uspGetMileStoneDetailsColumns "
        dtMileStone = objdb.ExeQueryDT(strSQL, "Table")
        lstMileStoneDetails.DataSource = dtMileStone
        lstMileStoneDetails.DataTextField = "TColumn"
        lstMileStoneDetails.DataValueField = "Seq"
        lstMileStoneDetails.DataBind()

        lstNICDummyData.DataSource = dtMileStone
        lstNICDummyData.DataTextField = "TColumn"
        lstNICDummyData.DataValueField = "Seq"
        lstNICDummyData.DataBind()
        Session("NICData") = dtMileStone
    End Sub

    Sub fillEPMData()
        strSQL = "Exec uspGetEPMDataColumns " & ddlFilter.SelectedValue
        dtEpmData = objdb.ExeQueryDT(strSQL, "Table1")
        lstEPMData.DataSource = dtEpmData
        lstEPMData.DataTextField = "TColumn"
        lstEPMData.DataValueField = "Seq"
        lstEPMData.DataBind()
    End Sub

    Sub fillUnSelected()
        strSQL = "Exec uspGetMileStoneDetailsColumns2 "
        dtEpmData = objdb.ExeQueryDT(strSQL, "Table1")
        lstUnSelected.DataSource = dtEpmData
        lstUnSelected.DataTextField = "TColumn"
        lstUnSelected.DataValueField = "Seq"
        lstUnSelected.DataBind()
    End Sub

    Protected Sub btnAddAll_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddAll.ServerClick
        strSQL = "Exec uspGetEPMNICColumns "
        dtEpmData = objdb.ExeQueryDT(strSQL, "Table2")
        lstUnSelected.DataSource = dtEpmData
        lstUnSelected.DataTextField = "TColumn"
        lstUnSelected.DataValueField = "Seq"
        lstUnSelected.DataBind()
        lstEPMData.Items.Clear()
        lstMileStoneDetails.Items.Clear()
        CallButtonDisp()
    End Sub

    Protected Sub btnRemove_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.ServerClick
        Dim strText As String
        Dim strVal() As String
        Dim strVal1 As String
        Dim strVal2 As String
        If lstUnSelected.SelectedIndex <> -1 Then
            strText = lstUnSelected.SelectedItem.Text
            strVal = Split(strText, "-")
            strVal1 = Trim(LTrim(RTrim(strVal(0))))
            strVal2 = Trim(LTrim(RTrim(strVal(1))))
            If strVal1 = "EPM" Then
                lstEPMData.Items.Insert(IIf(lstEPMData.Items.Count > 0, lstEPMData.Items.Count, 0), strText)
                lstEPMData.Items(lstEPMData.Items.Count - 1).Value = lstUnSelected.SelectedValue
                lstUnSelected.Items.RemoveAt(lstUnSelected.SelectedIndex)
            ElseIf strVal1 = "NIC" Then
                lstMileStoneDetails.Items.Insert(IIf(lstMileStoneDetails.Items.Count > 0, lstMileStoneDetails.Items.Count, 0), strText)
                lstMileStoneDetails.Items(lstMileStoneDetails.Items.Count - 1).Value = lstUnSelected.SelectedValue
                lstUnSelected.Items.RemoveAt(lstUnSelected.SelectedIndex)
            End If
        End If
        CallButtonDisp()
    End Sub

    Protected Sub btnRemoveAll_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemoveAll.ServerClick
        Dim i As Integer
        Dim strVal() As String
        Dim strVal1 As String
        Dim strVal2 As String
        For i = 0 To lstUnSelected.Items.Count - 1
            strVal = Split(lstUnSelected.Items(0).Text, "-")
            strVal1 = Trim(LTrim(RTrim(strVal(0))))
            strVal2 = Trim(LTrim(RTrim(strVal(1))))
            If strVal1 = "EPM" Then
                lstEPMData.Items.Insert(IIf(lstEPMData.Items.Count > 0, lstEPMData.Items.Count, 0), strVal2) 'lstUnSelected.Items(0).Text)
                lstUnSelected.Items.RemoveAt(0)
            ElseIf strVal1 = "NIC" Then
                lstMileStoneDetails.Items.Insert(IIf(lstEPMData.Items.Count > 0, lstMileStoneDetails.Items.Count, 0), strVal2) 'lstUnSelected.Items(0).Text)
                lstUnSelected.Items.RemoveAt(0)
            End If
        Next
    End Sub

    Protected Sub btnAdd_ServerClick1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.ServerClick
        If lstEPMData.SelectedIndex <> -1 Then
            Dim strItem As String = lstEPMData.SelectedItem.Text  'Left(lstGroups.SelectedItem.Text, 1) & " - " & lstEpmData.SelectedItem.Text
            lstUnSelected.Items.Insert(IIf(lstUnSelected.Items.Count > 0, lstUnSelected.Items.Count, 0), strItem) 'lstEpmData.SelectedItem.Text)
            lstUnSelected.Items(lstUnSelected.Items.Count - 1).Value = lstEPMData.SelectedValue '& " - " & "EPM"
            lstEPMData.Items.RemoveAt(lstEPMData.SelectedIndex)
        End If
        If lstMileStoneDetails.SelectedIndex <> -1 Then
            Dim strItem As String = lstMileStoneDetails.SelectedItem.Text  'Left(lstGroups.SelectedItem.Text, 1) & " - " & lstEpmData.SelectedItem.Text
            lstUnSelected.Items.Insert(IIf(lstUnSelected.Items.Count > 0, lstUnSelected.Items.Count, 0), strItem) 'lstEpmData.SelectedItem.Text)
            lstUnSelected.Items(lstUnSelected.Items.Count - 1).Value = lstMileStoneDetails.SelectedValue
            lstMileStoneDetails.Items.RemoveAt(lstMileStoneDetails.SelectedIndex)
        End If
        CallButtonDisp()
    End Sub

    Sub CallButtonDisp()
        If lstUnSelected.Items.Count > 0 Then
            btnRemove.Disabled = False
            btnRemoveAll.Disabled = False
        Else
            btnRemove.Disabled = True
            btnRemoveAll.Disabled = True
        End If
        If lstUnSelected.Items.Count > 1 And Request.QueryString("id") Is Nothing Then
            btnUp.Disabled = False
            btnDown.Disabled = False
        Else
            btnUp.Disabled = True
            btnDown.Disabled = True
        End If
    End Sub

    Protected Sub btnUp_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.ServerClick
        Dim iIndex As Integer = 0
        Dim iValue As Integer = 0
        Dim iText As String = ""
        If lstUnSelected.SelectedIndex <> -1 Then
            If lstUnSelected.SelectedIndex = 0 Then
                iIndex = lstUnSelected.SelectedIndex
                iValue = lstUnSelected.SelectedValue
                iText = lstUnSelected.SelectedItem.Text
                lstUnSelected.Items.RemoveAt(lstUnSelected.SelectedIndex)
                lstUnSelected.Items.Insert(lstUnSelected.Items.Count - 1, iText)
                lstUnSelected.Items(lstUnSelected.Items.Count - 1).Value = iValue
            Else
                iIndex = lstUnSelected.SelectedIndex
                iValue = lstUnSelected.SelectedValue
                iText = lstUnSelected.SelectedItem.Text
                lstUnSelected.Items.RemoveAt(lstUnSelected.SelectedIndex)
                lstUnSelected.Items.Insert(iIndex - 1, iText)
                lstUnSelected.Items(iIndex - 1).Value = iValue
            End If
        End If
        lstUnSelected.DataBind()
        lstUnSelected.SelectedIndex = iIndex - 1
    End Sub

    Protected Sub btnDown_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.ServerClick
        Dim iIndex As Integer = 0
        Dim iValue As Integer = 0
        Dim iText As String = ""
        If lstUnSelected.SelectedIndex <> -1 Then
            If lstUnSelected.SelectedIndex = 0 Then
                iIndex = lstUnSelected.SelectedIndex
                iValue = lstUnSelected.SelectedValue
                iText = lstUnSelected.SelectedItem.Text
                lstUnSelected.Items.RemoveAt(lstUnSelected.SelectedIndex)
                lstUnSelected.Items.Insert(1, iText)
                lstUnSelected.Items(1).Value = iValue
            Else
                iIndex = lstUnSelected.SelectedIndex
                iValue = lstUnSelected.SelectedValue
                iText = lstUnSelected.SelectedItem.Text
                If iIndex = lstUnSelected.Items.Count - 1 Then
                    lstUnSelected.Items.RemoveAt(lstUnSelected.SelectedIndex)
                    lstUnSelected.Items.Insert(0, iText)
                    lstUnSelected.Items(0).Value = iValue
                Else
                    lstUnSelected.Items.RemoveAt(lstUnSelected.SelectedIndex)
                    lstUnSelected.Items.Insert(iIndex + 1, iText)
                    lstUnSelected.Items(iIndex + 1).Value = iValue
                End If
            End If
        End If
        lstUnSelected.DataBind()
        lstUnSelected.SelectedIndex = iIndex + 1
    End Sub

    Protected Sub btnDisply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisply.Click
        Dim i As Integer
        Dim myVal As String
        Dim strSQL As String
        Dim myCmd As String
        Dim myTableField As String
        Dim strField() As String
        Dim strField1 As String
        Dim strInsert As String
        If lstUnSelected.Items.Count > 0 Then
            For i = 0 To lstUnSelected.Items.Count - 1
                If i = 0 Then
                    myCmd = "DEL"
                    strSQL = "exec uspNICEPMFieldsInsert  '" & myCmd & "'"
                    objdb.ExeUpdate(strSQL)
                End If
                myVal = lstUnSelected.Items(i).Text
                strField = Split(myVal, "-")
                If strField.Length = 2 Then
                    strField1 = Trim(LTrim(RTrim(strField(1))))
                ElseIf strField.Length = 3 Then
                    strField1 = Trim(LTrim(RTrim(strField(2))))
                Else
                    strField1 = Trim(LTrim(RTrim(strField(0))))
                End If
                myTableField = "Alter table NICEPMFields add [" & strField1 & "] varchar(255)"
                objdb.ExeUpdate(myTableField)
            Next
            strInsert = " insert into NICEPMFields(SLNo) values(1)"
            objdb.ExeUpdate(strInsert)
            fillNICEPMData()
        End If
        'This is for NICDATA TEMPLATE
        If lstNICDummyData.Items.Count > 0 Then
            For i = 0 To lstNICDummyData.Items.Count - 1
                If i = 0 Then
                    myCmd = "DEL"
                    strSQL = "exec uspNICDataTemplate  '" & myCmd & "'"
                    objdb.ExeUpdate(strSQL)
                End If
                myVal = lstNICDummyData.Items(i).Text
                strField = Split(myVal, "-")
                strField1 = Trim(LTrim(RTrim(strField(1))))
                myTableField = "Alter table  NICDataTemplate add " & strField1 & " varchar(20)"
                objdb.ExeUpdate(myTableField)
            Next
            strInsert = " insert into NICDataTemplate(SLNo) values(1)"
            objdb.ExeUpdate(strInsert)
            fillNICEPMDataTemplate()
        End If
    End Sub

    Sub fillNICEPMData()
        Dim sqlStr As String
        sqlStr = "Exec uspNICEPMData 0"
        dt = objdb.ExeQueryDT(sqlStr, "NICEPMData")
        grdNICEPMData.DataSource = dt
        grdNICEPMData.DataBind()
    End Sub

    Sub fillNICEPMDataTemplate()
        Dim sqlStr As String
        sqlStr = "Exec uspNICGetDataTemplate 0"
        dt = objdb.ExeQueryDT(sqlStr, "NICDataTemplate")
        GvNICTemplate.DataSource = dt
        GvNICTemplate.DataBind()
    End Sub

    Protected Sub btnExportExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportExcel.Click
        ExportToExcel()
    End Sub

    Public Sub ExportToExcel()
        '------- dedy 091202
        Dim sqlStr As String
        sqlStr = "Exec uspNICGetDataTemplate 1"
        dt = objdb.ExeQueryDT(sqlStr, "NICDataTemplate")
        GvNICTemplate.DataSource = dt
        GvNICTemplate.DataBind()
        '-------
        Dim tw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()
        Response.ContentType = "application/vnd.ms-excel"
        'Response.ContentType = "text/csv"
        Response.AddHeader("content-disposition", "attachment;filename=Sheet1.xls")
        'Response.AddHeader("content-disposition", "attachment;filename=Sheet1.csv")
        Response.Charset = ""
        EnableViewState = False
        Controls.Add(frm)
        frm.Controls.Add(GvNICTemplate)
        frm.RenderControl(hw)
        Response.Write(tw.ToString())
        Response.End()
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ReportTemplate()
    End Sub

    Public Sub ReportTemplate()
       
        '------- dedy 091201
        Dim sqlStr As String
        sqlStr = "Exec uspNICEPMData 1"
        dt = objdb.ExeQueryDT(sqlStr, "NICEPMData")
        grdNICEPMData.DataSource = dt
        grdNICEPMData.DataBind()
        'Dim tw As New StringWriter()
        'Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        'Dim frm As HtmlForm = New HtmlForm()
        'Response.ContentType = "application/Excel 8.0"
        'Response.ContentType = "text/csv"
        'Response.AddHeader("content-disposition", "attachment;filename=Sheet1.xls")
        'Response.AddHeader("content-disposition", "attachment;filename=Sheet1.csv")
        'Response.Charset = ""
        'EnableViewState = False
        'Controls.Add(frm)
        'frm.Controls.Add(grdNICEPMData)
        'frm.RenderControl(hw)
        'Response.Write(tw.ToString())
        'Response.End()

        Response.Cache.SetExpires(DateTime.Now.AddSeconds(1))
        Response.ContentType = "application/vnd.ms-excel"
        Response.Charset = ""
        Me.EnableViewState = False
        Response.Write("<html xmlns:x=""urn:schemas-microsoft-com:office:excel"">")
        Response.Write(vbCr & vbLf)
        Response.Write("<style> .mystyle1 " & vbCr & vbLf & "{mso-style-parent:style0;mso-number-format:""" & "\@" & """" & ";} " & vbCr & vbLf & "</style>")
        Dim tw As New StringWriter()
        Dim hw As New HtmlTextWriter(tw)
        grdNICEPMData.RenderControl(hw)
        Response.AppendHeader("content-disposition", "attachment;filename=NIC.xls")
        Response.Write(tw.ToString())
        Response.End()

    End Sub

    Protected Sub ddlFilter_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFilter.SelectedIndexChanged
        fillEPMData()
    End Sub
    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub
End Class
