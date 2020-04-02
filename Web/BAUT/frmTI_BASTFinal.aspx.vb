Imports System
Imports DAO
Imports Entities
Imports BusinessLogic
Imports System.Data
Imports Common
Imports System.IO
Imports Common_NSNFramework

Partial Class BAUT_frmTI_BASTFinal
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim dt2 As New DataTable
    Dim objET1 As New ETSiteDoc
    Dim objET As New ETAuditTrail
    Dim objdb As New DBUtil
    Dim objBo As New BOSiteDocs
    Dim objBOAT As New BoAuditTrail
    Dim docId As Integer = 0
    Dim i, j, k, l As Integer
    Dim strsql As String
    Dim objdo As New ETWFTransaction
    Dim objutil As New DBUtil
    Dim dt1 As New DataTable
    Dim grp As String
    Dim roleid As Integer
    Dim FileNamePath As String
    Dim FileNameOnly As String
    Dim ReFileName As String
    Dim objBOM As New BOMailReport
    Dim dtnr As New DataTable
    Dim objmail As New TakeMail
    Dim oddt As New DataTable
    Dim iCount As Integer = 0
    Dim dbutils_nsn As New DBUtils_NSN

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Page.IsPostBack = False Then
            btnFCOR.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtCORDated,'dd-mmm-yyyy');return false;")
            btnUTRAN.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtDated,'dd-mmm-yyyy');return false;")
            btnSAC.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtSACRD,'dd-mmm-yyyy');return false;")
            btnICR.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtICRD,'dd-mmm-yyyy');return false;")
            btnOAir.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtOAirD,'dd-mmm-yyyy');return false;")
            btnDate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtDate,'dd-mmm-yyyy');return false;")
            Getdata()
            filloddata()
            'generate bast ref
            'txtBastRefNo.Text = objdb.ExeQueryScalarString("select dbo.GenerateBAUTRefNo(" & Request.QueryString("id") & ",'BAST')")
            Dim strsql As String = "Exec uspSiteBASTDocListOnlineForm " & Request.QueryString("id")
            dt = objutil.ExeQueryDT(strsql, "sisbast")
            grddocuments.DataSource = dt
            grddocuments.DataBind()
            grddocuments.Columns(1).Visible = False
            grddocuments.Columns(2).Visible = False
            grddocuments.Columns(4).Visible = False
            Dim rw As Integer = 0
            If Not Request.QueryString("rw") = "" Then
                rw = Request.QueryString("rw")
            End If
            Session("webname") = ""
        End If
        If Request.QueryString("rwx") = "" Or Request.QueryString("rwx") = "0" Then
            btnAddRow.Enabled = True
            btnAddRowComplete.Text = "Complete"
        Else
            btnAddRow.Enabled = False
            btnAddRowComplete.Text = "Edit"
        End If
        tableHeader(False)
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
    End Sub
    Sub tableHeader(ByRef submit As Boolean)
        If Request.QueryString("rdoBAST") <> "" Then
            If Request.QueryString("rdoBAST") = "100percent" Then
                Session("rdoBAST1") = True
                Rb100percent.Checked = True
            End If
            If Request.QueryString("rdoBAST") = "5percent" Then
                Session("rdoBAST2") = True
                Rb5percent.Checked = True
            End If
        Else
            Session("rdoBAST1") = Rb100percent.Checked
            Session("rdoBAST2") = Rb5percent.Checked
        End If
        If Session("rdoBAST2") = True Then
            LblRemarks.Visible = True
        Else
            LblRemarks.Visible = False
        End If

        tdTable.Controls.Clear()
        Dim strsql As String = "select * from odbasttable where swid=" & Request.QueryString("id")
        dt = objutil.ExeQueryDT(strsql, "odbasttable")
        Dim iMainTable As New HtmlTable
        iMainTable.ID = "iMTbl"
        iMainTable.Width = "100%"
        iMainTable.Border = 1
        iMainTable.Align = "left"
        iMainTable.BorderColor = "#000000"
        iMainTable.CellPadding = 0
        iMainTable.CellSpacing = 0
        Dim iMainRows1 As New HtmlTableRow
        Dim iMainRows2 As New HtmlTableRow
        Dim iMainRows4 As New HtmlTableRow
        Dim iMianCell11 As New HtmlTableCell
        Dim iMianCell12 As New HtmlTableCell
        Dim iMianCell13 As New HtmlTableCell
        Dim iMianCell14 As New HtmlTableCell
        Dim iMianCell15 As New HtmlTableCell
        Dim iMianCell16 As New HtmlTableCell
        Dim iMianCell17 As New HtmlTableCell
        Dim iMianCell18 As New HtmlTableCell
        Dim iMianCell19 As New HtmlTableCell
        Dim iMianCell110 As New HtmlTableCell
        Dim iMianCell111 As New HtmlTableCell
        Dim iMianCell112 As New HtmlTableCell
        Dim iMianCell113 As New HtmlTableCell
        Dim iMianCell31 As New HtmlTableCell
        Dim iMianCell32 As New HtmlTableCell
        Dim iMianCell33 As New HtmlTableCell
        Dim iMianCell34 As New HtmlTableCell
        Dim iMianCell35 As New HtmlTableCell
        Dim iMianCell36 As New HtmlTableCell
        Dim iMianCell37 As New HtmlTableCell
        Dim iMianCell38 As New HtmlTableCell
        Dim iMianCell39 As New HtmlTableCell
        Dim iMianCell310 As New HtmlTableCell
        Dim iMianCell311 As New HtmlTableCell
        '------ Bast Value Column---------------
        Dim iMaincell_BastValueHeader As New HtmlTableCell
        Dim iMaincell_BastValueEuroHeader As New HtmlTableCell
        Dim iMaincell_BastValueUSDHeader As New HtmlTableCell
        Dim iMaincell_BastValueIDRHeader As New HtmlTableCell
        Dim iMaincell_BastvalueEuroTotal As New HtmlTableCell
        Dim iMaincell_BastvalueUSDTotal As New HtmlTableCell
        Dim iMaincell_BastvalueIDRTotal As New HtmlTableCell
        '---------------------------------------
        iMianCell11.InnerHtml = "PO Number"
        iMianCell11.Align = "center"
        iMianCell11.ColSpan = 2
        iMianCell11.RowSpan = 2
        iMianCell12.InnerHtml = "PO"
        iMianCell12.Align = "center"
        iMianCell12.ColSpan = 2
        iMianCell13.InnerHtml = "Implementation"
        iMianCell13.Align = "center"
        iMianCell13.ColSpan = 2
        If Session("rdoBAST2") = True Then
            iMaincell_BastValueHeader.InnerHtml = "Bast Value"
            iMaincell_BastValueHeader.ColSpan = 2
            iMaincell_BastValueHeader.Align = "center"
            iMaincell_BastValueEuroHeader.InnerHtml = "(EURO)"
            iMaincell_BastValueEuroHeader.Align = "center"
            iMaincell_BastValueEuroHeader.Visible = False
            iMaincell_BastValueUSDHeader.InnerHtml = "(USD)"
            iMaincell_BastValueUSDHeader.Align = "center"
            iMaincell_BastValueIDRHeader.InnerHtml = "(IDR)"
            iMaincell_BastValueIDRHeader.Align = "center"
        End If
        iMianCell14.InnerHtml = "Delta"
        iMianCell14.Align = "center"
        iMianCell14.ColSpan = 2
        iMianCell15.InnerHtml = "(EURO)"
        iMianCell15.Align = "center"
        iMianCell15.Visible = False
        iMianCell16.InnerHtml = "(USD)"
        iMianCell16.Align = "center"
        iMianCell17.InnerHtml = "(IDR)"
        iMianCell17.Align = "center"
        iMianCell18.InnerHtml = "(EURO)"
        iMianCell18.Align = "center"
        iMianCell18.Visible = False
        iMianCell19.InnerHtml = "(USD)"
        iMianCell19.Align = "center"
        iMianCell110.InnerHtml = "(IDR)"
        iMianCell110.Align = "center"
        iMianCell111.InnerHtml = "(EURO)"
        iMianCell111.Align = "center"
        iMianCell111.Visible = False
        iMianCell112.InnerHtml = "(USD)"
        iMianCell112.Align = "center"
        iMianCell113.InnerHtml = "(IDR)"
        iMianCell113.Align = "center"
        iMainRows1.Cells.Add(iMianCell11)
        iMainRows1.Cells.Add(iMianCell12)
        iMainRows1.Cells.Add(iMianCell13)
        If Session("rdoBAST2") = True Then
            iMainRows1.Cells.Add(iMaincell_BastValueHeader)
        End If
        iMainRows1.Cells.Add(iMianCell14)
        iMainRows2.Cells.Add(iMianCell15)
        iMainRows2.Cells.Add(iMianCell16)
        iMainRows2.Cells.Add(iMianCell17)
        iMainRows2.Cells.Add(iMianCell18)
        iMainRows2.Cells.Add(iMianCell19)
        iMainRows2.Cells.Add(iMianCell110)
        If Session("rdoBAST2") = True Then
            iMainRows2.Cells.Add(iMaincell_BastValueEuroHeader)
            iMainRows2.Cells.Add(iMaincell_BastValueUSDHeader)
            iMainRows2.Cells.Add(iMaincell_BastValueIDRHeader)
        End If
        iMainRows2.Cells.Add(iMianCell111)
        iMainRows2.Cells.Add(iMianCell112)
        iMainRows2.Cells.Add(iMianCell113)
        iMainTable.Rows.Add(iMainRows1)
        iMainTable.Rows.Add(iMainRows2)
        Dim total1 As Decimal
        Dim total2 As Decimal
        Dim total3 As Decimal
        Dim total4 As Decimal
        Dim total5 As Decimal
        Dim total6 As Decimal
        Dim total7 As Decimal
        Dim total8 As Decimal
        Dim total9 As Decimal

        Dim total_BstValEuro As Decimal
        Dim total_BstValUSD As Decimal
        Dim total_BstValIDR As Decimal

        'style configuration code behind -- irvan vickrizal
        Dim textcss1 As String = String.Empty
        Dim textcss2 As String = String.Empty
        Dim textcss1a As String = String.Empty
        Dim textcss2a As String = String.Empty
        Dim textcsspono As String = String.Empty
        Dim textcssservice As String = String.Empty
        Dim textcssusd As String = String.Empty
        Dim textcsstotalIDR As String = String.Empty
        Dim textcsstotalUSD As String = String.Empty
        Dim textcsslabeltotal As String = String.Empty

        textcss1 = "border-style:solid;border-color:white;border-width:1px;background-color:skyblue;text-align:center;font-size:8pt;"
        textcss2 = "border-style:solid;border-color:white;border-width:1px;background-color:skyblue;width:100px;text-align:center;font-size:8pt;"
        textcss1a = "border-style:solid;border-color:white;border-width:1px;background-color:white;text-align:center;font-size:8pt;"
        textcss2a = "border-style:solid;border-color:white;border-width:1px;background-color:white;width:100px;text-align:center;font-size:8pt;font-weight:bold;"
        textcsspono = textcss1
        textcssservice = textcss1
        textcssusd = textcss2
        textcsstotalIDR = textcss2a
        textcsstotalUSD = textcss2a
        textcsslabeltotal = textcss2a

        If Not String.IsNullOrEmpty(Request.QueryString("type")) Then
            If Request.QueryString("type").Equals("complete") Then
                textcss1 = "border-style:solid;border-color:white;border-width:1px;background-color:skyblue;text-align:center;font-size:5.5pt;"
                textcss2 = "border-style:solid;border-color:white;border-width:1px;background-color:skyblue;width:70px;text-align:center;font-size:5.5pt;"
                textcss1a = "border-style:solid;border-color:white;border-width:1px;background-color:white;text-align:center;width:70px;font-size:5.5pt;"
                textcss2a = "border-style:solid;border-color:white;border-width:1px;background-color:white;width:70px;text-align:center;font-size:5.5pt;"
                textcsspono = "border-style:solid;border-color:white;border-width:1px;background-color:white;text-align:center;font-size:5.5pt; width:65px;padding:0px;margin:0px;"
                textcssservice = "border-style:solid;border-color:white;border-width:1px;background-color:white;text-align:left;font-size:5.5pt;width:100%;padding:0px;margin:0px;"
                textcssusd = "border-style:solid;border-color:white;border-width:1px;background-color:white;text-align:center;font-size:5.5pt; width:55px;"
                textcsstotalIDR = "border-style:solid;border-color:white;border-width:1px;background-color:white;width:70px;text-align:center;font-size:6.5pt;font-weight:bold;"
                textcsstotalUSD = "border-style:solid;border-color:white;border-width:1px;background-color:white;width:60px;text-align:center;font-size:6.5pt;font-weight:bold;"
                Rb100percent.Visible = False
                Rb5percent.Visible = False
            Else
                Rb100percent.Visible = True
                Rb5percent.Visible = True
            End If
        End If

        'bugfix101004 fix on document load when there are data exist in the database
        iCount = Request.QueryString("rw")
        If Request.QueryString("rw") = Nothing Then
            If dt.Rows.Count > 0 Then
                iCount = dt.Rows.Count - 1
            End If
        End If
        If Session("rdoBAST1") = True Then
            btnAddRow.Attributes.Add("onclick", "transferValue(" & iCount + 1 & ",1);")
            btnAddRowComplete.Attributes.Add("onclick", "transferValue(" & iCount + 1 & ",1);")
            btnGenerate.Attributes.Add("onclick", "transferValue(" & iCount + 1 & ",1);")
        Else
            btnAddRow.Attributes.Add("onclick", "transferValue(" & iCount + 1 & ",2);")
            btnAddRowComplete.Attributes.Add("onclick", "transferValue(" & iCount + 1 & ",2);")
            btnGenerate.Attributes.Add("onclick", "transferValue(" & iCount + 1 & ",2);")
        End If
        '--
        For i = 1 To (iCount + 1)
            Dim iMianCell21 As New HtmlTableCell
            Dim iMianCell22 As New HtmlTableCell
            Dim iMianCell23 As New HtmlTableCell
            Dim iMianCell24 As New HtmlTableCell
            Dim iMianCell25 As New HtmlTableCell
            Dim iMianCell26 As New HtmlTableCell
            Dim iMianCell27 As New HtmlTableCell
            Dim iMianCell28 As New HtmlTableCell
            Dim iMianCell29 As New HtmlTableCell
            Dim iMianCell210 As New HtmlTableCell
            Dim iMianCell211 As New HtmlTableCell
            Dim iMaincell_BastValRowEuro As New HtmlTableCell
            Dim iMaincell_BastValRowUSD As New HtmlTableCell
            Dim iMaincell_BastValRowIDR As New HtmlTableCell
            
            Dim iMainRows3 As New HtmlTableRow
            Dim inhtmlmain As String = ""
            Dim inhtmlUSD As String = ""
            Dim inhtml As String = ""
            Dim inhtml1 As String = ""
            Dim inhtml2 As String = ""
            Dim inhtml3 As String = ""
            Dim inhtml4 As String = ""
            Dim inhtml5 As String = ""
            Dim inhtml6 As String = ""
            Dim inhtml7 As String = ""
            Dim inhtml8 As String = ""
            Dim inhtml9 As String = ""
            Dim inhtml10 As String = ""
            Dim inhtml11 As String = ""

            Dim inhtml_BSTValEuro As String = ""
            Dim inhtml_BSTValUSD As String = ""
            Dim inhtml_BSTValIDR As String = ""

            

            'Dim textcss1 As String = "border-style:solid;border-color:white;border-width:1px;background-color:skyblue;text-align:center;"
            'Dim textcss2 As String = "border-style:solid;border-color:white;border-width:1px;background-color:skyblue;width:100px;text-align:center;"
            'Dim textcss1a As String = "border-style:solid;border-color:white;border-width:1px;background-color:white;text-align:center;"
            'Dim textcss2a As String = "border-style:solid;border-color:white;border-width:1px;background-color:white;width:100px;text-align:center;"

            Dim rownumber As Integer = 1
            If dt.Rows.Count > 0 Then
                If i <= dt.Rows.Count Then
                    inhtml1 = String.Format("{0:#,##0}", dt.Rows(i - 1).Item("tblpono"))
                    inhtml2 = String.Format("{0:#,##0}", dt.Rows(i - 1).Item("tbldesc"))
                    inhtml3 = String.Format("{0:#,##0}", dt.Rows(i - 1).Item("tbleuro1"))
                    inhtml4 = String.Format("{0:###,##.#0}", dt.Rows(i - 1).Item("tblusd1"))
                    inhtml5 = String.Format("{0:#,##0}", dt.Rows(i - 1).Item("tblidr1"))
                    inhtml6 = String.Format("{0:#,##0}", dt.Rows(i - 1).Item("tbleuro2"))
                    inhtml7 = String.Format("{0:###,##.#0}", dt.Rows(i - 1).Item("tblusd2"))
                    inhtml8 = String.Format("{0:#,##0}", dt.Rows(i - 1).Item("tblidr2"))

                    inhtml_BSTValEuro = String.Format("{0:#,##0}", dt.Rows(i - 1).Item("tbbstvaleuro"))
                    inhtml_BSTValUSD = String.Format("{0:###,##.#0}", dt.Rows(i - 1).Item("tbbstvalusd"))
                    inhtml_BSTValIDR = String.Format("{0:#,##0}", dt.Rows(i - 1).Item("tbbstvalidr"))

                    'user request makes - values to appear positive (disabled)
                    inhtml9 = String.Format("{0:#,##0}", dt.Rows(i - 1).Item("tbleuro2") - dt.Rows(i - 1).Item("tbleuro1"))
                    inhtml10 = String.Format("{0:###,##.#0}", dt.Rows(i - 1).Item("tblusd2") - dt.Rows(i - 1).Item("tblusd1"))
                    inhtml11 = String.Format("{0:#,##0}", dt.Rows(i - 1).Item("tblidr2") - dt.Rows(i - 1).Item("tblidr1"))
                    'user request makes - values to appear positive
                    'If DT.Rows(i - 1).Item("tbleuro3") < 0 Then
                    '    inhtml9 = String.Format("{0:#,##0}", DT.Rows(i - 1).Item("tbleuro3") * -1)
                    'Else
                    '    inhtml9 = String.Format("{0:#,##0}", DT.Rows(i - 1).Item("tbleuro3"))
                    'End If
                    'If DT.Rows(i - 1).Item("tblusd3") < 0 Then
                    '    inhtml10 = String.Format("{0:###,##.#0}", DT.Rows(i - 1).Item("tblusd3") * -1)
                    'Else
                    '    inhtml10 = String.Format("{0:###,##.#0}", DT.Rows(i - 1).Item("tblusd3"))
                    'End If
                    'If DT.Rows(i - 1).Item("tblidr3") < 0 Then
                    '    inhtml11 = String.Format("{0:#,##0}", DT.Rows(i - 1).Item("tblidr3") * -1)
                    'Else
                    '    inhtml11 = String.Format("{0:#,##0}", DT.Rows(i - 1).Item("tblidr3"))
                    'End If
                    total1 += dt.Rows(i - 1).Item("tbleuro1")
                    total2 += dt.Rows(i - 1).Item("tblusd1")
                    total3 += dt.Rows(i - 1).Item("tblidr1")
                    total4 += dt.Rows(i - 1).Item("tbleuro2")
                    total5 += dt.Rows(i - 1).Item("tblusd2")
                    total6 += dt.Rows(i - 1).Item("tblidr2")
                    total7 += dt.Rows(i - 1).Item("tbleuro2") - dt.Rows(i - 1).Item("tbleuro1")
                    total8 += dt.Rows(i - 1).Item("tblusd2") - dt.Rows(i - 1).Item("tblusd1")
                    total9 += dt.Rows(i - 1).Item("tblidr2") - dt.Rows(i - 1).Item("tblidr1")
                    total_BstValEuro += dt.Rows(i - 1).Item("tbbstvaleuro")
                    total_BstValUSD += dt.Rows(i - 1).Item("tbbstvalusd")
                    total_BstValIDR += dt.Rows(i - 1).Item("tbbstvalidr")
                End If
                Dim textcssA As String = ""
                Dim textcssB As String = ""
                If btnAddRowComplete.Text = "Edit" Or submit Then
                    textcssA = textcss1a
                    textcssB = textcss2a
                Else
                    textcssA = textcss1
                    textcssB = textcss2
                End If
                inhtmlmain = "<input type='text' id='@id' runat='server'  value='@txt' style='@sty'>"
                inhtml = "<input type='text' id='@id' runat='server'  value='@txt' style='@sty' onblur = 'this.value=formatCurrency(this.value);'>"
                inhtmlUSD = "<input type='text' id='@id' runat='server'  value='@txt' style='@sty' onblur ='this.value=formatusdCurrency(this.value);'>"

                'If Session("rdoBAST2") = True Then
                '    iMianCell21.InnerHtml = inhtmlmain.Replace("@id", "input" & i & "a").Replace("@txt", inhtml1).Replace("@sty", textcsspono)
                '    iMianCell21.Align = "center"
                '    iMianCell22.InnerHtml = inhtmlmain.Replace("@id", "input" & i & "b").Replace("@txt", inhtml2).Replace("@sty", textcssservice)
                '    iMianCell22.Align = "center"
                'Else
                '    iMianCell21.InnerHtml = inhtmlmain.Replace("@id", "input" & i & "a").Replace("@txt", inhtml1).Replace("@sty", textcssA)
                '    iMianCell21.Align = "center"
                '    iMianCell22.InnerHtml = inhtmlmain.Replace("@id", "input" & i & "b").Replace("@txt", inhtml2).Replace("@sty", textcssA)
                '    iMianCell22.Align = "center"
                'End If
                iMianCell21.InnerHtml = inhtmlmain.Replace("@id", "input" & i & "a").Replace("@txt", inhtml1).Replace("@sty", textcsspono)
                iMianCell21.Align = "center"
                iMianCell22.InnerHtml = inhtmlmain.Replace("@id", "input" & i & "b").Replace("@txt", inhtml2).Replace("@sty", textcssservice)
                iMianCell22.Align = "center"

                '----------------- PO value configured-------------------------
                iMianCell23.InnerHtml = inhtml.Replace("@id", "input" & i & "c").Replace("@txt", inhtml3).Replace("@sty", textcssB)
                iMianCell23.Align = "center"
                iMianCell23.Visible = False

                'If Session("rdoBAST2") = True Then
                '    'iMianCell24.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "d").Replace("@txt", inhtml4).Replace("@sty", textcssusd)
                'Else
                '    iMianCell24.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "d").Replace("@txt", inhtml4).Replace("@sty", textcssB)
                'End If
                iMianCell24.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "d").Replace("@txt", inhtml4).Replace("@sty", textcssusd)
                iMianCell24.Align = "center"
                iMianCell25.InnerHtml = inhtml.Replace("@id", "input" & i & "e").Replace("@txt", inhtml5).Replace("@sty", textcssB)
                iMianCell25.Align = "center"
                '---------------------------------------------------------------
                '---------------- Implement value configured--------------------
                iMianCell26.InnerHtml = inhtml.Replace("@id", "input" & i & "f").Replace("@txt", inhtml6).Replace("@sty", textcssB)
                iMianCell26.Align = "center"
                iMianCell26.Visible = False
                'If Session("rdoBAST2") = True Then
                '    iMianCell27.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "g").Replace("@txt", inhtml7).Replace("@sty", textcssusd)
                'Else
                '    iMianCell27.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "g").Replace("@txt", inhtml7).Replace("@sty", textcssB)
                'End If
                iMianCell27.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "g").Replace("@txt", inhtml7).Replace("@sty", textcssusd)
                iMianCell27.Align = "center"
                iMianCell28.InnerHtml = inhtml.Replace("@id", "input" & i & "h").Replace("@txt", inhtml8).Replace("@sty", textcssB)
                iMianCell28.Align = "center"
                '---------------------------------------------------------------
                '-------------- BST value configured ---------------------------
                iMaincell_BastValRowEuro.InnerHtml = inhtml.Replace("@id", "input" & i & "l").Replace("@txt", "0").Replace("@sty", textcssB)
                iMaincell_BastValRowEuro.Align = "center"
                iMaincell_BastValRowEuro.Visible = False
                'If Session("rdoBAST2") = True Then
                '    iMaincell_BastValRowUSD.InnerHtml = inhtml.Replace("@id", "input" & i & "m").Replace("@txt", inhtml_BSTValUSD).Replace("@sty", textcssusd)
                '    iMaincell_BastValRowIDR.InnerHtml = inhtml.Replace("@id", "input" & i & "n").Replace("@txt", inhtml_BSTValIDR).Replace("@sty", textcssB)
                'Else
                '    iMaincell_BastValRowUSD.InnerHtml = inhtml.Replace("@id", "input" & i & "m").Replace("@txt", "0").Replace("@sty", textcssB)
                '    iMaincell_BastValRowIDR.InnerHtml = inhtml.Replace("@id", "input" & i & "n").Replace("@txt", "0").Replace("@sty", textcssB)
                'End If
                iMaincell_BastValRowUSD.InnerHtml = inhtml.Replace("@id", "input" & i & "m").Replace("@txt", inhtml_BSTValUSD).Replace("@sty", textcssusd)
                iMaincell_BastValRowIDR.InnerHtml = inhtml.Replace("@id", "input" & i & "n").Replace("@txt", inhtml_BSTValIDR).Replace("@sty", textcssB)
                iMaincell_BastValRowUSD.Align = "center"
                iMaincell_BastValRowIDR.Align = "center"
                '---------------------------------------------------------------

                '--------------- Delta value configured-------------------------
                iMianCell29.InnerHtml = inhtml.Replace("@id", "input" & i & "i").Replace("@txt", inhtml9).Replace("@sty", textcssB)
                iMianCell29.Align = "center"
                iMianCell29.Visible = False

                'If Session("rdoBAST2") = True Then
                '    iMianCell210.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "j").Replace("@txt", inhtml10).Replace("@sty", textcssusd)
                'Else
                '    iMianCell210.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "j").Replace("@txt", inhtml10).Replace("@sty", textcssB)
                'End If
                iMianCell210.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "j").Replace("@txt", inhtml10).Replace("@sty", textcssusd)
                iMianCell210.Align = "center"
                iMianCell211.InnerHtml = inhtml.Replace("@id", "input" & i & "k").Replace("@txt", inhtml11).Replace("@sty", textcssB)
                iMianCell211.Align = "center"
                '---------------------------------------------------------------

            Else
                inhtmlmain = "<input type='text' id='@id' runat='server'  value='@txt' style='@sty'>"
                inhtml = "<input type='text' id='@id' runat='server'  value='@txt' style='@sty' onblur = 'this.value=formatCurrency(this.value);'>"
                inhtmlUSD = "<input type='text' id='@id' runat='server'  value='@txt' style='@sty' onblur ='this.value=formatusdCurrency(this.value);'>"
                If Request.QueryString("rw") = "" Then
                    iMianCell21.InnerHtml = inhtmlmain.Replace("@id", "input" & i & "a").Replace("@txt", lblPONO.Text).Replace("@sty", textcsspono)
                Else
                    iMianCell21.InnerHtml = inhtmlmain.Replace("@id", "input" & i & "a").Replace("@txt", "").Replace("@sty", textcsspono)
                End If
                iMianCell22.InnerHtml = inhtmlmain.Replace("@id", "input" & i & "b").Replace("@txt", "").Replace("@sty", textcssservice)

                '---------- PO value configiration -------------
                iMianCell23.InnerHtml = inhtml.Replace("@id", "input" & i & "c").Replace("@txt", "0").Replace("@sty", textcss2)
                iMianCell23.Visible = False
                'If Session("rdoBAST2") = True Then
                '    iMianCell24.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "d").Replace("@txt", "0").Replace("@sty", textcssusd)
                'Else
                '    iMianCell24.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "d").Replace("@txt", "0").Replace("@sty", textcss2)
                'End If
                iMianCell24.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "d").Replace("@txt", "0").Replace("@sty", textcssusd)
                iMianCell24.Align = "center"
                iMianCell25.InnerHtml = inhtml.Replace("@id", "input" & i & "e").Replace("@txt", "0").Replace("@sty", textcss2)
                iMianCell25.Align = "center"
                '-----------------------------------------------

                '----------- Implementation value configuration---------
                iMianCell26.InnerHtml = inhtml.Replace("@id", "input" & i & "f").Replace("@txt", "0").Replace("@sty", textcss2)
                iMianCell26.Align = "center"
                iMianCell26.Visible = False
                'If Session("rdoBAST2") = True Then
                '    iMianCell27.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "g").Replace("@txt", "0").Replace("@sty", textcssusd)
                'Else
                '    iMianCell27.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "g").Replace("@txt", "0").Replace("@sty", textcss2)
                'End If
                iMianCell27.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "g").Replace("@txt", "0").Replace("@sty", textcssusd)
                iMianCell27.Align = "center"

                iMianCell28.InnerHtml = inhtml.Replace("@id", "input" & i & "h").Replace("@txt", "0").Replace("@sty", textcss2)
                iMianCell28.Align = "center"
                '-------------------------------------------------------
                '-------------- BST value configured ---------------------------
                iMaincell_BastValRowEuro.InnerHtml = inhtml.Replace("@id", "input" & i & "l").Replace("@txt", "0").Replace("@sty", textcss2)
                iMaincell_BastValRowEuro.Align = "center"
                iMaincell_BastValRowEuro.Visible = False
                'If Session("rdoBAST2") = True Then
                '    iMaincell_BastValRowUSD.InnerHtml = inhtml.Replace("@id", "input" & i & "m").Replace("@txt", "0").Replace("@sty", textcssusd)
                'Else
                '    iMaincell_BastValRowUSD.InnerHtml = inhtml.Replace("@id", "input" & i & "m").Replace("@txt", "0").Replace("@sty", textcss2)
                'End If
                iMaincell_BastValRowUSD.InnerHtml = inhtml.Replace("@id", "input" & i & "m").Replace("@txt", "0").Replace("@sty", textcssusd)
                iMaincell_BastValRowUSD.Align = "center"
                iMaincell_BastValRowIDR.InnerHtml = inhtml.Replace("@id", "input" & i & "n").Replace("@txt", "0").Replace("@sty", textcss2)
                iMaincell_BastValRowIDR.Align = "center"
                '---------------------------------------------------------------
                '--------------- Delta value configuration ---------------------
                iMianCell29.InnerHtml = inhtml.Replace("@id", "input" & i & "i").Replace("@txt", "0").Replace("@sty", textcss2)
                iMianCell29.Align = "center"
                iMianCell29.Visible = False
                'If Session("rdoBAST2") = True Then
                '    iMianCell210.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "j").Replace("@txt", "0").Replace("@sty", textcssusd)
                'Else
                '    iMianCell210.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "j").Replace("@txt", "0").Replace("@sty", textcss2)
                'End If
                iMianCell210.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "j").Replace("@txt", "0").Replace("@sty", textcssusd)
                iMianCell210.Align = "center"
                iMianCell211.InnerHtml = inhtml.Replace("@id", "input" & i & "k").Replace("@txt", "0").Replace("@sty", textcss2)
                iMianCell211.Align = "center"
                '---------------------------------------------------------------
            End If
            iMainRows3.Cells.Add(iMianCell21)
            iMainRows3.Cells.Add(iMianCell22)
            iMainRows3.Cells.Add(iMianCell23)
            iMainRows3.Cells.Add(iMianCell24)
            iMainRows3.Cells.Add(iMianCell25)
            iMainRows3.Cells.Add(iMianCell26)
            iMainRows3.Cells.Add(iMianCell27)
            iMainRows3.Cells.Add(iMianCell28)
            If Session("rdoBAST2") = True Then
                iMainRows3.Cells.Add(iMaincell_BastValRowEuro)
                iMainRows3.Cells.Add(iMaincell_BastValRowUSD)
                iMainRows3.Cells.Add(iMaincell_BastValRowIDR)
            End If
            iMainRows3.Cells.Add(iMianCell29)
            iMainRows3.Cells.Add(iMianCell210)
            iMainRows3.Cells.Add(iMianCell211)
            iMainTable.Rows.Add(iMainRows3)
        Next
        'user request makes - values to appear positive
        'If total1 < 0 Then total1 = total1 * -1
        'If total2 < 0 Then total2 = total2 * -1
        'If total3 < 0 Then total3 = total3 * -1
        'If total4 < 0 Then total4 = total4 * -1
        'If total5 < 0 Then total5 = total5 * -1
        'If total6 < 0 Then total6 = total6 * -1
        'If total7 < 0 Then total7 = total7 * -1
        'If total8 < 0 Then total8 = total8 * -1
        'If total9 < 0 Then total9 = total9 * -1
        Dim inhtmlmainTotal As String = "<input type='text' id='@id' runat='server'  value='@txt' style='@sty'>"
        Dim inhtmlTotal As String = "<input type='text' id='@id' runat='server'  value='@txt' style='@sty' onblur = 'this.value=formatCurrency(this.value);'>"
        Dim inhtmlUSDTotal As String = "<input type='text' id='@id' runat='server'  value='@txt' style='@sty' readonly='readonly' onblur ='this.value=formatusdCurrency(this.value);'>"

        'iMianCell31.InnerHtml = ""
        iMianCell32.InnerHtml = inhtmlmainTotal.Replace("@id", "inputLabelTotal").Replace("@txt", "Total").Replace("@sty", textcsslabeltotal)
        iMianCell32.ColSpan = 2
        iMianCell32.Align = "right"
        '------------------- Euro Value visible false--------------------------

        'PO value
        iMianCell33.InnerHtml = String.Format("{0:###,##.#0}", total1)
        iMianCell33.Align = "center"
        iMianCell33.Visible = False

        'Implementation value
        iMianCell36.InnerHtml = String.Format("{0:###,##.#0}", total4)
        iMianCell36.Align = "center"
        iMianCell36.Visible = False

        'BstVale
        iMaincell_BastvalueEuroTotal.InnerHtml = String.Format("{0:#,##0}", total_BstValEuro)
        iMaincell_BastvalueEuroTotal.Align = "center"
        iMaincell_BastvalueEuroTotal.Visible = False

        'Delta Value
        iMianCell39.InnerHtml = String.Format("{0:###,##.#0}", total7)
        iMianCell39.Align = "center"
        iMianCell39.Visible = False
        '----------------------------------------------------------------------
        'USD
        Dim USD1 As Double = total2
        Dim USD2 As Double = total5
        Dim USD3 As Double = total8
        Dim bstvalUSD As Double = total_BstValUSD

        'If Session("rdoBAST2") = True Then
        '    iMianCell34.InnerHtml = inhtmlUSDTotal.Replace("@id", "inputTotalPOUSD").Replace("@txt", String.Format("{0:###,##.#0}", USD1)).Replace("@sty", textcsstotalUSD)
        '    iMianCell35.InnerHtml = inhtmlTotal.Replace("@id", "inputTotalPOIDR").Replace("@txt", String.Format("{0:#,##0}", total3)).Replace("@sty", textcsstotalIDR)
        '    iMianCell37.InnerHtml = inhtmlUSDTotal.Replace("@id", "inputTotalImplementUSD").Replace("@txt", String.Format("{0:###,##.#0}", USD2)).Replace("@sty", textcsstotalUSD)
        '    iMianCell38.InnerHtml = inhtmlTotal.Replace("@id", "inputTotalImplementIDR").Replace("@txt", String.Format("{0:#,##0}", total6)).Replace("@sty", textcsstotalIDR)
        '    iMianCell310.InnerHtml = inhtmlUSDTotal.Replace("@id", "inputTotalDeltaUSD").Replace("@txt", String.Format("{0:###,##.#0}", USD3)).Replace("@sty", textcsstotalUSD)
        '    iMianCell311.InnerHtml = inhtmlTotal.Replace("@id", "inputTotalDeltaIDR").Replace("@txt", String.Format("{0:#,##0}", total9)).Replace("@sty", textcsstotalIDR)
        '    iMaincell_BastvalueUSDTotal.InnerHtml = inhtmlUSDTotal.Replace("@id", "inputTotalBastValUSD").Replace("@txt", String.Format("{0:###,##.#0}", bstvalUSD)).Replace("@sty", textcsstotalUSD)
        '    iMaincell_BastvalueIDRTotal.InnerHtml = inhtmlTotal.Replace("@id", "inputTotalBastValIDR").Replace("@txt", String.Format("{0:#,##0}", total_BstValIDR)).Replace("@sty", textcsstotalIDR)
        'Else
        '    iMianCell34.InnerHtml = String.Format("{0:###,##.#0}", USD1)
        '    iMianCell35.InnerHtml = String.Format("{0:#,##0}", total3)
        '    iMianCell37.InnerHtml = String.Format("{0:###,##.#0}", USD2)
        '    iMianCell38.InnerHtml = String.Format("{0:#,##0}", total6)
        '    iMianCell310.InnerHtml = String.Format("{0:###,##.#0}", USD3)
        '    iMianCell311.InnerHtml = String.Format("{0:#,##0}", total9)
        '    iMaincell_BastvalueUSDTotal.InnerHtml = String.Format("{0:###,##.#0}", bstvalUSD)
        '    iMaincell_BastvalueIDRTotal.InnerHtml = String.Format("{0:#,##0}", total_BstValIDR)
        'End If

        iMianCell34.InnerHtml = inhtmlUSDTotal.Replace("@id", "inputTotalPOUSD").Replace("@txt", String.Format("{0:###,##.#0}", USD1)).Replace("@sty", textcsstotalUSD)
        iMianCell35.InnerHtml = inhtmlTotal.Replace("@id", "inputTotalPOIDR").Replace("@txt", String.Format("{0:#,##0}", total3)).Replace("@sty", textcsstotalIDR)
        iMianCell37.InnerHtml = inhtmlUSDTotal.Replace("@id", "inputTotalImplementUSD").Replace("@txt", String.Format("{0:###,##.#0}", USD2)).Replace("@sty", textcsstotalUSD)
        iMianCell38.InnerHtml = inhtmlTotal.Replace("@id", "inputTotalImplementIDR").Replace("@txt", String.Format("{0:#,##0}", total6)).Replace("@sty", textcsstotalIDR)
        iMianCell310.InnerHtml = inhtmlUSDTotal.Replace("@id", "inputTotalDeltaUSD").Replace("@txt", String.Format("{0:###,##.#0}", USD3)).Replace("@sty", textcsstotalUSD)
        iMianCell311.InnerHtml = inhtmlTotal.Replace("@id", "inputTotalDeltaIDR").Replace("@txt", String.Format("{0:#,##0}", total9)).Replace("@sty", textcsstotalIDR)
        iMaincell_BastvalueUSDTotal.InnerHtml = inhtmlUSDTotal.Replace("@id", "inputTotalBastValUSD").Replace("@txt", String.Format("{0:###,##.#0}", bstvalUSD)).Replace("@sty", textcsstotalUSD)
        iMaincell_BastvalueIDRTotal.InnerHtml = inhtmlTotal.Replace("@id", "inputTotalBastValIDR").Replace("@txt", String.Format("{0:#,##0}", total_BstValIDR)).Replace("@sty", textcsstotalIDR)
        iMianCell34.Align = "center"
        iMianCell35.Align = "center"
        iMianCell37.Align = "center"
        iMianCell38.Align = "center"
        iMianCell310.Align = "center"
        iMianCell311.Align = "center"
        iMaincell_BastvalueUSDTotal.Align = "center"
        iMaincell_BastvalueIDRTotal.Align = "center"


        'iMainRows4.Cells.Add(iMianCell31)
        iMainRows4.Cells.Add(iMianCell32)
        iMainRows4.Cells.Add(iMianCell33)
        iMainRows4.Cells.Add(iMianCell34)
        iMainRows4.Cells.Add(iMianCell35)
        iMainRows4.Cells.Add(iMianCell36)
        iMainRows4.Cells.Add(iMianCell37)
        iMainRows4.Cells.Add(iMianCell38)
        iMainRows4.Cells.Add(iMianCell39)
        If Session("rdoBAST2") = True Then
            iMainRows4.Cells.Add(iMaincell_BastvalueUSDTotal)
            iMainRows4.Cells.Add(iMaincell_BastvalueIDRTotal)
            iMainRows4.Cells.Add(iMaincell_BastvalueEuroTotal)
        End If
        iMainRows4.Cells.Add(iMianCell310)
        iMainRows4.Cells.Add(iMianCell311)
        iMainTable.Rows.Add(iMainRows4)
        tdTable.Controls.Add(iMainTable)
        tdTable.Style.Add("lblText", " ")
        'If Session("rdoBAST2") = True Then
        'Else
        '    tdTable.Style.Add("lblText", " ")
        'End If
    End Sub
    'it will fill online form data if already exists for same site...
    Sub filloddata()
        Dim strsql As String = "Exec odbastDetail " & Request.QueryString("id")
        oddt = objutil.ExeQueryDT(strsql, "odbast")
        If oddt.Rows.Count > 0 Then
            'lblProj.Text = oddt.Rows(0).Item("Projectid").ToString
            txtDate.Value = IIf(oddt.Rows(0).Item("bastdate").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("bastdate").ToString, "")
            txtRef.Text = oddt.Rows(0).Item("AgreeRefno").ToString
            txtDated.Value = IIf(oddt.Rows(0).Item("AgreeDate").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("AgreeDate").ToString, "")
            txtCOR.Text = oddt.Rows(0).Item("FCOrefno").ToString
            txtCORDated.Value = IIf(oddt.Rows(0).Item("FCOdate").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("FCOdate").ToString, "")
            txtSACRNo.Text = oddt.Rows(0).Item("SOACRefno").ToString
            txtSACRD.Value = IIf(oddt.Rows(0).Item("SOACDate").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("SOACDate").ToString, "")
            txtICRNo.Text = oddt.Rows(0).Item("IntRefno").ToString
            txtICRD.Value = IIf(oddt.Rows(0).Item("IntDate").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("IntDate").ToString, "")
            txtOAir.Text = oddt.Rows(0).Item("onairrefno").ToString

            'Dim bautcontroller As New BAUTMasterController()
            'Dim bautinfo As BAUTMasterInfo = bautcontroller.GetDetailBAUT(Convert.ToInt32(hdnsiteid.Value), Integer.Parse(hdnversion.Value), 1031)
            'If bautinfo IsNot Nothing Then
            '    If bautinfo.OnAirDateBAUT.HasValue Then
            '        btnOAir.Visible = False
            '    Else
            '        btnOAir.Visible = True
            '    End If
            'End If
            txtOAirD.Value = IIf(oddt.Rows(0).Item("onairdate").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("onairdate").ToString, "")
            'txtPO.Value = oddt.Rows(0).Item("IDRPO").ToString
            'txtActual.Value = oddt.Rows(0).Item("IDRACT").ToString
            'txtDelta.Value = oddt.Rows(0).Item("IDRDelta").ToString
            'txtPOUSD.Value = oddt.Rows(0).Item("USDPO").ToString
            'txtActUSD.Value = oddt.Rows(0).Item("USDACT").ToString
            'txtDelUSD.Value = oddt.Rows(0).Item("USDDelta").ToString
        End If
        'bugfix100818 new request to link for the on air date from baut
        'strsql = "select convert(varchar,onairdated,103) onairD from odbaut where bautrefno='" & lblBR.Text & "'"

        'bugfix15102013 irvan new request to link for the on air date from baut
        strsql = "select convert(varchar,onairdated,103) onairD from odbaut where swid=(select top 1 sw_id from sitedoc where siteid=" & hdnsiteid.Value & " and version=" & hdnversion.Value & " and docid = 1031)"
        dt = objutil.ExeQueryDT(strsql, "odbaut")
        If dt.Rows.Count > 0 Then
            If (dt.Rows(0).Item("onairD").ToString.IndexOf("1900") = -1) Then
                txtOAirD.Value = dt.Rows(0).Item("onairD").ToString
                btnOAir.Visible = False
            Else
                txtOAirD.Value = ""
                btnOAir.Visible = True
            End If

            'txtOAirD.Value = IIf(dt.Rows(0).Item("onairD").ToString.IndexOf("1900") = -1, dt.Rows(0).Item("onairD").ToString, "")

        End If
    End Sub
    Sub Getdata()
        Dim str As String = ""
        dt = objBo.uspTIFBastOnLine(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            hdnsiteid.Value = dt.Rows(0).Item("SiteId").ToString
            hdnversion.Value = dt.Rows(0).Item("version").ToString
            hdnWfId.Value = dt.Rows(0).Item("WF_Id").ToString
            hdndocId.Value = dt.Rows(0).Item("docId").ToString
            hdnSiteno.Value = dt.Rows(0).Item("site_no").ToString
            hdnScope.Value = dt.Rows(0).Item("Scope").ToString
            hdnDGBox.Value = dt.Rows(0).Item("DGBox").ToString
            If (hdnWfId.Value = ConfigurationManager.AppSettings("WFBASTB3GID")) Then
                LblHeaderBAST.Text = "FINAL BERITA ACARA SERAH TERIMA BARANG"
                LblSubHeaderBAST.Text = "(""FINAL BASTB"")"
            End If
            'i = objBo.uspCheckIntegration(hdndocId.Value, hdnSiteno.Value)
            i = objdb.ExeQueryScalar("exec uspCheckIntegration  '" & hdndocId.Value & "' ,'" & hdnSiteno.Value & "'," & hdnversion.Value & "")
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
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('IntD');", True)
            End Select
            lblScope.Text = dt.Rows(0).Item("scope").ToString
            lblSite.Text = dt.Rows(0).Item("site_no") & "/" & dt.Rows(0).Item("site_name").ToString
            lblPORef.Text = dt.Rows(0).Item("pono").ToString
            lblPODated.Text = dt.Rows(0).Item("custporecdt").ToString
            lblPONO.Text = dt.Rows(0).Item("pono").ToString
            lblPONO2.Text = dt.Rows(0).Item("pono").ToString
            lblBR.Text = dt.Rows(0).Item("ReferenceNO").ToString
            lblBRD.Text = dt.Rows(0).Item("ctdt").ToString
            txtBastRefNo.Text = dt.Rows(0).Item("Ref").ToString
            lblProj.Text = dt.Rows(0).Item("projectid").ToString
            lblsiteidpo.Text = dt.Rows(0).Item("siteidpo").ToString
            lblsitenamepo.Text = dt.Rows(0).Item("sitenamepo").ToString
            str = "Exec [uspGetOnLineFormBind] " & dt.Rows(0).Item("WF_Id").ToString & "," & dt.Rows(0).Item("SiteId").ToString
            dt = objutil.ExeQueryDT(str, "SiteDoc1")
            'DLDigitalSign.DataSource = dt
            'DLDigitalSign.DataBind()
            '-------------New Code 05 Januari 2012 --------
            Dim dtSignPosition As DataView = dt.DefaultView
            dtSignPosition.Sort = "tsk_id desc"
            DLDigitalSign.DataSource = dtSignPosition
            DLDigitalSign.DataBind()
            '----------------------------------------------
            Dim dtv As DataView = dt.DefaultView
            dtv.Sort = "tsk_id desc"
            dtList.DataSource = dtv
            dtList.DataBind()
            HDDgSignTotal.Value = dt.Rows.Count
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
        Else
            hdnsiteid.Value = 0
            hdnversion.Value = 0
            hdnWfId.Value = 0
            hdndocId.Value = 0
            hdnSiteno.Value = ""
        End If
        'bugfix100818 new request to link for the date under telkomsel signatured
        str = "Exec [uspWCTROnLine] " & Request.QueryString("id")
        dt = objdb.ExeQueryDT(str, "SiteDoc1")
        If (dt.Rows.Count > 0) Then
            'dedy 091106
            str = "Exec [uspGetOnLineFormBindNew] " & dt.Rows(0).Item("SW_Id").ToString & "," & dt.Rows(0).Item("WF_Id").ToString & "," & dt.Rows(0).Item("SiteId").ToString & "," & ConfigurationManager.AppSettings("BAUTID")
            dt2 = objutil.ExeQueryDT(str, "SiteDoc1")
            For n As Integer = 0 To dt2.Rows.Count - 1
                If dt2.Rows(n).Item("tsk_id").ToString() = "4" Then
                    lblBRD.Text = dt2.Rows(n).Item("apptime").ToString
                End If
            Next
        End If
    End Sub
    Sub Dochecking()
        roleid = Session("Role_Id")
        grp = Session("User_Type")
        If objBo.uspApprRequired(hdnsiteid.Value, hdndocId.Value, hdnversion.Value) <> 0 Then
            If objBo.verifypermission(hdndocId.Value, roleid, grp) <> 0 Then
                Select Case objBo.DocUploadverify(hdnsiteid.Value, hdndocId.Value, hdnversion.Value).ToString
                    Case 1 'This document not attached to this site
                        hdnKeyVal.Value = 1
                        btnGenerate.Attributes.Clear()
                        'belowcase not going to happen we need to test the scenariao.
                        btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to upload?')")
                    Case 2 'means document was already uploaded for this version of site,do u want to overwrite
                        hdnKeyVal.Value = 2
                        btnGenerate.Attributes.Clear()
                        btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                    Case 3 'means document was not yet uploaded for thissite
                        hdnKeyVal.Value = 3
                        btnGenerate.Attributes.Clear()
                    Case 4 'means document already processed for sencod stage cannot upload
                        hdnKeyVal.Value = 0
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                        'makevisible()
                        Exit Sub
                End Select
            Else
                If Session("Role_Id") = 1 Then ''if role is sysadmin then no need to verify permission everthing permitted.                    
                    Select Case objBo.DocUploadverify(hdnsiteid.Value, hdndocId.Value, hdnversion.Value).ToString
                        Case 1 'This document not attached to this site
                            hdnKeyVal.Value = 1
                            btnGenerate.Attributes.Clear()
                            'belowcase not going to happen we need to test the scenariao.
                            btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to  upload?')")
                        Case 2 'means document was already uploaded for this version of site,do u want to overwrite
                            hdnKeyVal.Value = 2
                            btnGenerate.Attributes.Clear()
                            btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                        Case 3 'means document was not yet uploaded for thissite
                            hdnKeyVal.Value = 3
                            btnGenerate.Attributes.Clear()
                        Case 4 'means document already processed for sencod stage cannot upload
                            hdnKeyVal.Value = 0
                            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                            Exit Sub
                    End Select
                Else
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('nop');", True)
                End If
            End If
        Else 'Seeta 20081230 Appr Not Required
            Select Case objBo.DocUploadverify(hdnsiteid.Value, hdndocId.Value, hdnversion.Value).ToString
                Case 1 'This document not attached to this site
                    hdnKeyVal.Value = 1
                    btnGenerate.Attributes.Clear()
                    'belowcase not going to happen we need to test the scenariao.
                    btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document Not attached to this site,Do you want to  upload?')")
                Case 2 'means document was already uploaded for this version of site,do u want to overwrite
                    hdnKeyVal.Value = 2
                    btnGenerate.Attributes.Clear()
                    btnGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('This Document already uploaded Do you want to replace?')")
                Case 3 'means document was not yet uploaded for thissite
                    hdnKeyVal.Value = 3
                    btnGenerate.Attributes.Clear()
                Case 4 'means document already processed for sencod stage cannot upload
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                    Exit Sub
                    'Response.Redirect("frmSiteDocUploadTree.aspx")
            End Select
        End If
    End Sub

    Sub uploaddocument(ByVal vers As Integer, ByVal keyval As Integer)
        dt = objBo.getbautdocdetailsNEW(hdndocId.Value)
        Dim sec As String = dt.Rows(0).Item("sec_name").ToString
        Dim subsec As String = dt.Rows(0).Item("subsec_name").ToString
        Dim secpath As String = ""
        Dim ft As String = ""
        Dim path As String = ""
        Dim filenameorg As String
        FileNameOnly = "-" & Constants._Doc_BAST & "-"
        filenameorg = hdnSiteno.Value & FileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        ReFileName = filenameorg & ".htm"
        ft = ConfigurationManager.AppSettings("Type") & lblPONO.Text & "-" & hdnScope.Value & "\"
        path = ConfigurationManager.AppSettings("Fpath") & hdnSiteno.Value & ft
        Dim strResult As String = DOInsertTrans(hdnsiteid.Value, hdndocId.Value, vers, path)
        Dim DocPath As String = ""
        If strResult = "0" Then
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        ElseIf strResult = "1" Then
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        Else
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(Replace(path, "  \", "\"))
        End If
        With objET1
            .SiteID = hdnsiteid.Value
            .DocId = hdndocId.Value
            .IsUploded = 1
            .Version = vers
            .keyval = keyval
            .DocPath = DocPath
            .AT.RStatus = Constants.STATUS_ACTIVE
            .AT.LMBY = Session("User_Name")
            .orgDocPath = hdnSiteno.Value & ft & secpath & ReFileName
            .PONo = lblPONO.Text
        End With
        objBo.updatedocupload(objET1)
        Dim strsql As String = "Update bastmaster set Pstatus=1 where Site_ID = " & hdnsiteid.Value & " and SiteVersion =" & hdnversion.Value & " and PONO = '" & lblPONO.Text & "'"
        objutil.ExeUpdate(strsql)
        chek4alldoc() 'for messaage to previous screen ' and saving final docupload date in reporttable
        'for BAUT
        'objBo.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 1, lblPONO.Text, Session("User_Name"))
        objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 1 & ",'" & lblPONO.Text & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
        'for BAST1
        'objBo.check4BAUTBAST(hdnsiteid.Value, hdnversion.Value, 2, lblPONO.Text, Session("User_Name"))
        objdb.ExeQueryScalar("exec uspcheck4BAUTBAST " & hdnsiteid.Value & " ," & hdnversion.Value & "," & 2 & ",'" & lblPONO.Text & "','" & Session("User_Name") & "'," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & " ")
        'Fill Transaction table
        AuditTrail()
    End Sub

    Public Sub chek4alldoc()
        Dim i As Integer
        i = objBo.chek4alldoc(hdnsiteid.Value, hdnversion.Value)
        If i = 0 Then
            hdnready4baut.Value = 1
            objBo.uspRPTUpdate(lblPONO.Text, hdnSiteno.Value)
        Else
            hdnready4baut.Value = 0
        End If
    End Sub
    Sub AuditTrail()
        objET.PoNo = lblPONO.Text
        objET.SiteId = hdnsiteid.Value
        objET.DocId = hdndocId.Value
        objET.Task = "1"
        objET.Status = "1"
        objET.Userid = Session("User_Id")
        objET.Roleid = Session("Role_Id")
        objET.fldtype = hdnScope.Value
        objBOAT.uspAuditTrailI(objET)
    End Sub
    Function CreatePDFFile(ByVal StrPath As String) As String
        StrPath = StrPath.Replace(" \", "\").ToString 'bugfix100816 causing error when the path containing space before \
        Dim dtba As New DataTable
        Dim filenameorg As String
        Dim ReFileName As String
        filenameorg = hdnSiteno.Value & "-BAST-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
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
        sw.WriteLine(".MainCSS{margin-bottom: 0px;margin-left: 20px;margin-right: 20px;margin-top: 0px;width: 800px;height: 750px;text-align: center;}")
        sw.WriteLine(".lblText{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;}")
        sw.WriteLine(".lblBText{font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: bold;}")
        sw.WriteLine(".lblBold{font-family: verdana;font-size: 12pt;color: #000000;font-weight: bold;}")
        sw.WriteLine(".textFieldStyle{background-color: white;border: 1px solid;color: black;font-family: verdana;font-size: 9pt;}")
        sw.WriteLine(".GridHeader{color: #0e1b42;background-color: Orange;font-size: 9pt;font-family: verdana;text-align: Left;vertical-align: bottom;font-weight: bold;}")
        sw.WriteLine(".GridEvenRows{background-color: #e3e3e3;vertical-align: top;font-family: verdana;font-size: 8pt;color: #000000;}")
        sw.WriteLine(".GridOddRows{background-color: white;vertical-align: top;font-family: verdana;font-size: 8pt;color: #000000;}")
        sw.WriteLine(".PagerTitle{font-size: 8pt;background-color: #cddbbf;text-align: Right;vertical-align: middle;color: #25375b;font-weight: bold;}")
        sw.WriteLine(".Hcap{height: 5px;}")
        sw.WriteLine(".VCap{width: 10px;}")
        sw.WriteLine(".lblBBTextL{border-bottom: 1px #000 solid;border-left: 1px #000 solid;border-top: 1px #000 solid;font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: bold;text-decoration: none;}")
        sw.WriteLine(".lblBBTextM{border-bottom: 1px #000 solid;border-top: 1px #000 solid;font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: normal;text-decoration: none;width: 1%;}")
        sw.WriteLine(".lblBBTextR{border-right: 1px #000 solid;border-bottom: 1px #000 solid;border-top: 1px #000 solid;font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: normal;text-decoration: none;}")
        sw.WriteLine("#lblTotalA{font-weight: bold;}")
        sw.WriteLine("#lblJobDelay{font-weight: bold;}")
        sw.WriteLine("</style></head>")
        sw.WriteLine("<body class=""MainCSS"">")
        dvPrint.RenderControl(sw)
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        Return Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName, StrPath, filenameorg)
    End Function
    Function DOInsertTrans(ByVal siteid As String, ByVal docName As String, ByVal version As Integer, ByVal path As String) As String
        docId = hdndocId.Value
        Dim dtNew As DataTable
        If hdnWfId.Value <> 0 Then
            dtNew = objBo.doinserttrans(hdnWfId.Value, docId)
            If hdnDGBox.Value = True Then
                If dtNew.Rows.Count > 0 Then
                    objBo.DelWFTransaction(hdndocId.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
                    Dim bb As Boolean
                    Dim aa As Integer = 0
                    Dim status As Integer
                    For aa = 0 To dtNew.Rows.Count - 1
                        fillDetails()
                        If (dtNew.Rows(aa).Item(5).ToString <> "1") Then status = 1
                        objdo.Status = status
                        objdo.DocId = docId
                        objdo.UserType = dtNew.Rows(aa).Item(3).ToString
                        objdo.UsrRole = dtNew.Rows(aa).Item(4).ToString
                        objdo.WFId = dtNew.Rows(aa).Item(1).ToString
                        objdo.UGP_Id = dtNew.Rows(aa).Item("grpId").ToString
                        objdo.TSK_Id = dtNew.Rows(aa).Item(5).ToString
                        objdo.XVal = dtNew.Rows(aa).Item("X_Coordinate").ToString
                        objdo.YVal = dtNew.Rows(aa).Item("Y_Coordinate").ToString
                        objdo.PageNo = dtNew.Rows(aa).Item("PageNo").ToString
                        objBo.uspwftransactionIU(objdo)
                    Next
                End If
                Return "1"
            End If
            CreateXY()
        Else
            Dim status As Integer = 99
            objBo.uspwftransactionNOTWFI(docId, 0, hdnsiteid.Value, hdnversion.Value, 1, Session("User_Id"), status, 2, Session("User_Name"))
            Return "0"
        End If
    End Function
    Sub CreateXY()
        Dim dtNew As DataTable
        Dim strSql1 As String = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
        "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        " where tt.TSK_Id=1 and wfid=" & hdnWfId.Value & " order by wfdid"
        dtNew = objutil.ExeQueryDT(strSql1, "dd")
        If dtNew.Rows.Count > 0 Then
            objBo.DelWFTransaction(hdndocId.Value, dtNew.Rows(0).Item(1).ToString, hdnsiteid.Value, hdnversion.Value)
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
            objdo.Status = status
            objBo.uspwftransactionIU(objdo)
        Next
        strSql1 = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
        "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        " where tt.TSK_Id <> 1 and wfid=" & hdnWfId.Value & "   order by sorder"
        DtNewOne = objutil.ExeQueryDT(strSql1, "dd")
        dvNotIn = DtNewOne.DefaultView
        dvNotIn.RowFilter = "TSK_Id <>1"
        status = 1
        Dim bb As Boolean, intCount As Integer = 0, IncrMentY As Integer = 0
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
            objBo.uspwftransactionIU(objdo)
        Next
        Dim strsql2 As String
        Dim dtnew2 As DataTable
        Dim dvnotin2 As DataView
        Dim tcount As Integer = 0, IncrY As Integer = 0
        strsql2 = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
        "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
        " where tt.TSK_Id not in (1,5) and wfid=" & hdnWfId.Value & "   order by wfdid"
        dtnew2 = objutil.ExeQueryDT(strsql2, "dd2")
        dvnotin2 = dtnew2.DefaultView
        dvnotin2.RowFilter = "TSK_Id <>1"
        dvnotin2.Sort = "TSK_Id desc"
        status = 1
        For IncrMentX As Integer = 0 To dvnotin2.Count - 1
            Dim iHDX As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdXCoordinate"), HiddenField)
            Dim iHDY As HiddenField = CType(DLDigitalSign.Items(IncrMentX).FindControl("hdYCoordinate"), HiddenField)
            objdo.DocId = hdndocId.Value
            objdo.Site_Id = hdnsiteid.Value
            objdo.SiteVersion = hdnversion.Value
            objdo.Status = status
            objdo.UserType = dvnotin2(IncrMentX).Item(3).ToString
            objdo.UsrRole = dvnotin2(IncrMentX).Item(4).ToString
            objdo.WFId = dvnotin2(IncrMentX).Item(1).ToString
            objdo.TSK_Id = dvnotin2(IncrMentX).Item(5).ToString
            objdo.UGP_Id = dvnotin2(IncrMentX).Item("grpId").ToString
            Dim introw As Integer = 0
            If Request.QueryString("rw") IsNot Nothing Then
                introw = Request.QueryString("rw")
            End If
            Dim strversion = Request.Browser.Type
            'dedy 100202
            Dim xAdjustment1 As Integer = 0
            Dim xAdjustment2 As Integer = 0
            Dim yAdjustment As Integer = 0
            If InStr(strversion, "IE6") > 0 Then
                xAdjustment1 = -13
                xAdjustment2 = +73
                yAdjustment = -130
                If dvnotin2.Count = 2 Then
                    If IncrMentX = 0 Then
                        objdo.XVal = (iHDX.Value + 30) + xAdjustment1
                    Else
                        objdo.XVal = ((iHDX.Value / 2) + 43 + (intCount * 30)) + xAdjustment2
                    End If
                Else
                    objdo.XVal = ((iHDX.Value / 2) + (intCount * 30)) + xAdjustment1
                End If
                objdo.YVal = (185 + (iHDY.Value - 791) + (IncrY * 52) - (introw * 40)) + yAdjustment
            ElseIf InStr(strversion, "IE7") > 0 Then
                xAdjustment1 = -13
                xAdjustment2 = +73
                yAdjustment = -130
                If dvnotin2.Count = 2 Then
                    If IncrMentX = 0 Then
                        objdo.XVal = (iHDX.Value + 33) + xAdjustment1
                    Else
                        objdo.XVal = ((iHDX.Value / 2) + 44 + (intCount * 30)) + xAdjustment2
                    End If
                Else
                    objdo.XVal = ((iHDX.Value / 2) + (intCount * 30)) + xAdjustment1
                End If
                objdo.YVal = (195 + (791 - iHDY.Value) + (IncrY * 52) - (introw * 14)) + yAdjustment
            ElseIf InStr(strversion, "IE8") > 0 Then
                xAdjustment1 = -13
                xAdjustment2 = +73
                yAdjustment = -130
                If dvnotin2.Count = 2 Then
                    If IncrMentX = 0 Then
                        objdo.XVal = (iHDX.Value + 33) + xAdjustment1
                    Else
                        objdo.XVal = ((iHDX.Value / 2) + 44 + (intCount * 30)) + xAdjustment2
                    End If
                Else
                    objdo.XVal = ((iHDX.Value / 2) + (intCount * 30)) + xAdjustment1
                End If
                objdo.YVal = (195 + (791 - iHDY.Value) + (IncrY * 52) - (introw * 14)) + yAdjustment
            ElseIf InStr(strversion, "Fire") > 0 Then
                xAdjustment1 = -13
                xAdjustment2 = +73
                yAdjustment = -130
                If dvnotin2.Count = 2 Then
                    If IncrMentX = 0 Then
                        objdo.XVal = iHDX.Value + 30
                    Else
                        objdo.XVal = (iHDX.Value / 2) + 56 + (intCount * 30)
                    End If
                Else
                    objdo.XVal = (iHDX.Value / 2) + (intCount * 30)
                End If
                objdo.YVal = 200 + (791 - iHDY.Value) + (IncrY * 52) + (introw * 12)
            Else
                xAdjustment1 = -13
                xAdjustment2 = +73
                yAdjustment = -130
                If dvnotin2.Count = 2 Then
                    If IncrMentX = 0 Then
                        objdo.XVal = iHDX.Value + 30
                    Else
                        objdo.XVal = (iHDX.Value / 2) + 43 + (intCount * 30)
                    End If
                Else
                    objdo.XVal = (iHDX.Value / 2) + (intCount * 30)
                End If
                objdo.YVal = 185 + (iHDY.Value - 791) + (IncrY * 52) - (introw * 40)
            End If
            'end of dedy 100202
            Y = (Math.Ceiling(iHDY.Value / 791))
            If (IncrMentX > 0) Then
                If (IncrMentX Mod 2) = 0 Then
                    tcount = 0
                    IncrY = IncrY + 1
                Else
                    tcount = tcount + 1
                End If
            Else
                tcount = tcount + 1
            End If
            If Y = 0 Then Y = 1
            Y = 1
            objdo.PageNo = Y
            objdo.Site_Id = hdnsiteid.Value
            'bugfix101004: override y value to disable the auto calculation
            Dim strsql As String = "select * from odbasttable where swid=" & Request.QueryString("id")
            dt = objutil.ExeQueryDT(strsql, "odbasttable")
            'objdo.YVal = 290 - (dt.Rows.Count * 15)
            If Session("rdoBAST2") = True Then
                objdo.YVal = 85
            Else
                objdo.YVal = 85
            End If


            '--
            objdb.ExeNonQuery("update wftransaction set xval=" & (objdo.XVal - 15) & ",yval=" & objdo.YVal & ", pageno=" & objdo.PageNo & " where site_id= " & objdo.Site_Id & " and siteversion= " & objdo.SiteVersion & " and docid= " & objdo.DocId & " and tsk_id=" & objdo.TSK_Id & "")
        Next
    End Sub
    Sub fillDetails()
        objdo.DocId = hdndocId.Value
        objdo.Site_Id = hdnsiteid.Value
        objdo.SiteVersion = hdnversion.Value
        objdo.AT.RStatus = Constants.STATUS_ACTIVE
        objdo.AT.LMBY = Session("User_Name")
    End Sub
    Sub DOThis(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rad As RadioButtonList = CType(sender, RadioButtonList)
        Dim grdr As GridViewRow = CType(rad.NamingContainer, GridViewRow)
        Dim x As Integer = grdr.RowIndex
        Dim rdl As RadioButtonList = grddocuments.Rows(x).Cells(5).FindControl("rdbstatus")
        Dim txt1 As TextBox = grddocuments.Rows(x).Cells(6).FindControl("txtremarks")
        txt1.Text = "Please enter remarks here......"
        If rdl.SelectedValue = 0 Then  'approve
            txt1.Visible = False
        Else 'reject
            txt1.Visible = True
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub
    Function saveData() As String
        If Request.QueryString("rdoBAST") <> "" Then
            If Request.QueryString("rdoBAST") = "100percent" Then
                Session("rdoBAST1") = True
            End If
            If Request.QueryString("rdoBAST") = "5percent" Then
                Session("rdoBAST2") = True
            End If
        Else
            Session("rdoBAST1") = Rb100percent.Checked
            Session("rdoBAST2") = Rb5percent.Checked
        End If
        Dim strsql As String
        Dim s As String = csv.Value.ToString
        Dim parts1 As String() = s.Split(New Char() {"@"c})
        Dim parts2 As String()
        Dim item As String = "0"
        Dim items As String = ""
        Dim idx As Decimal = 0
        Dim j As Integer
        Dim rw As Integer = iCount
        If Not Request.QueryString("rw") = "" Then
            rw = Request.QueryString("rw")
        End If
        strsql = "delete from odbasttable where swid=" & Request.QueryString("id")
        objutil.ExeQuery(strsql)
        Dim strBasttype As String = String.Empty
        If Session("rdoBAST2") = True Then
            strBasttype = "5percent"
        Else
            strBasttype = "100percent"
        End If
        Dim result As Integer = 0
        While 1
            parts2 = parts1(idx).Split(New Char() {"!"c})
            items = ""
            j = 0
            For Each item In parts2
                If j = 0 Or j = 1 Then
                    If item = "" Then
                        items = items & "'N/A'@@"
                    Else
                        items = items & "'" & item & "'@@"
                    End If
                Else
                    If item = "" Or item = "NaN" Then
                        items = items.Replace(",", "") & "0@@"
                    Else
                        items = items.Replace(",", "") & "" & item.Replace(",", "") & "@@"
                    End If
                End If
                j += 1
            Next
            items = items.Replace("@@", ",")
            strsql = "Exec odbastinsert " & Request.QueryString("id") & " ,'" & lblPONO.Text & "','" & hdnsiteid.Value & "','" & hdnversion.Value & "'," & _
            "'" & lblProj.Text & "'," & IIf(txtDate.Value.ToString <> "", "'" & txtDate.Value & "'", "null") & ",'" & txtRef.Text & "'," & _
            "" & IIf(txtDated.Value.ToString <> "", "'" & txtDated.Value & "'", "null") & ",'" & txtCOR.Text & "'," & IIf(txtCORDated.Value.ToString <> "", "'" & txtCORDated.Value & "'", "null") & "," & _
            "'" & txtSACRNo.Text & "'," & IIf(txtSACRD.Value.ToString <> "", "'" & txtSACRD.Value & "'", "null") & ",'" & txtICRNo.Text & "', " & _
            "" & IIf(txtICRD.Value.ToString <> "", "'" & txtICRD.Value & "'", "null") & ",'" & txtOAir.Text & "'," & IIf(txtOAirD.Value.ToString <> "", "'" & txtOAirD.Value & "'", "null") & "," & _
            items & _
            "'" & strBasttype & "','" & Session("User_Name") & "'"
            result = objutil.ExeQueryScalar(strsql)
            If idx = rw Then
                Exit While
            End If
            idx += 1
        End While
        Return result
    End Function
    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
        If Request.Browser.Browser = "IE" Then
            'bugfix110111 disabled since there are cases where wctr is going though manually
            'Dim kn As Integer
            'kn = objutil.ExeQueryScalar("select count(*) from odwctrbast where swid=" & hdnswid.Value)
            'If kn = 0 Then
            '    Response.Write("<script language='javascript' type='text/javascript'>alert('Please generate WCTR first..');</script>")
            '    Exit Sub
            'End If
            If Not String.IsNullOrEmpty(Request.QueryString("type")) Then
                Dim result As Integer = 1
                result = saveData()
                btnAddRow.Visible = False
                btnAddRowComplete.Visible = False
                Rb100percent.Visible = False
                Rb5percent.Visible = False
                If result = 1 Then
                    tableHeader(True)
                    Dim ss As Boolean = True
                    For intCount As Integer = 0 To grddocuments.Rows.Count - 1
                        Dim rdl1 As RadioButtonList = CType(grddocuments.Rows(intCount).FindControl("rdbstatus"), RadioButtonList)
                        If rdl1.SelectedValue = 1 Then
                            ss = False
                        End If
                    Next
                    If (ss) Then
                        'Response.Write("<script>alert('No permission')</script>")
                    Else
                        Response.Write("<script>alert('No permissions to generate')</script>")
                        Exit Sub
                    End If
                    If (hdnKeyVal.Value = "") Then
                        hdnKeyVal.Value = 0
                    End If
                    If hdnDGBox.Value = True Then
                        strsql = "select count(*) from docSignPositon where doc_id=" & hdndocId.Value
                        If objutil.ExeQueryScalar(strsql) > 0 Then
                            btnDate.Visible = False
                            btnFCOR.Visible = False
                            btnUTRAN.Visible = False
                            btnSAC.Visible = False
                            btnICR.Visible = False
                            btnOAir.Visible = False
                            txtDate.Visible = False
                            txtRef.Visible = False
                            txtDated.Visible = False
                            txtCOR.Visible = False
                            txtCORDated.Visible = False
                            txtSACRNo.Visible = False
                            txtSACRD.Visible = False
                            txtICRNo.Visible = False
                            txtICRD.Visible = False
                            txtOAir.Visible = False
                            txtOAirD.Visible = False
                            lblDate.Text = txtDate.Value
                            If lblDate.Text = "" Then lblDate.Text = ""
                            If lblProj.Text = "" Then lblProj.Text = ""
                            lblRef.Text = txtRef.Text
                            If lblRef.Text = "" Then lblRef.Text = ""
                            lblDated.Text = txtDated.Value
                            If lblDated.Text = "" Then lblDated.Text = ""
                            lblCOR.Text = txtCOR.Text
                            If lblCOR.Text = "" Then lblCOR.Text = ""
                            lblCORDated.Text = txtCORDated.Value
                            If lblCORDated.Text = "" Then lblCORDated.Text = ""
                            lblSACR.Text = txtSACRNo.Text
                            If lblSACR.Text = "" Then lblSACR.Text = ""
                            lblSACRD.Text = txtSACRD.Value
                            If lblSACRD.Text = "" Then lblSACRD.Text = ""
                            lblICR.Text = txtICRNo.Text
                            If lblICR.Text = "" Then lblICR.Text = ""
                            lblICRD.Text = txtICRD.Value
                            If lblICRD.Text = "" Then lblICRD.Text = ""
                            lblOAir.Text = txtOAir.Text
                            If lblOAir.Text = "" Then lblOAir.Text = ""
                            lblOAirD.Text = txtOAirD.Value
                            If lblOAirD.Text = "" Then lblOAirD.Text = ""
                            btnGenerate.Visible = False
                            grddocuments.Visible = False
                            uploaddocument(hdnversion.Value, hdnKeyVal.Value)
                            If Request.QueryString("open") Is Nothing Then
                                dbutils_nsn.CheckGroupingDocBAST(hdnsiteid.Value, hdnversion.Value, ConfigurationManager.AppSettings("BASTID"), ConfigurationManager.AppSettings("WCTRBASTID"), ConfigurationManager.AppSettings("BOQDOCID"))
                                Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
                            Else
                                dbutils_nsn.CheckGroupingDocBAST(hdnsiteid.Value, hdnversion.Value, ConfigurationManager.AppSettings("BASTID"), ConfigurationManager.AppSettings("WCTRBASTID"), ConfigurationManager.AppSettings("BOQDOCID"))
                                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptwhh", "WindowsClose();", True)
                            End If
                        Else
                            Response.Write("<script>alert('please Set X,Y coordinates for Digital sign in document Template..')</script>")
                        End If
                    Else
                        btnDate.Visible = False
                        btnFCOR.Visible = False
                        btnUTRAN.Visible = False
                        btnSAC.Visible = False
                        btnICR.Visible = False
                        btnOAir.Visible = False
                        txtDate.Visible = False
                        txtRef.Visible = False
                        txtDated.Visible = False
                        txtCOR.Visible = False
                        txtCORDated.Visible = False
                        txtSACRNo.Visible = False
                        txtSACRD.Visible = False
                        txtICRNo.Visible = False
                        txtICRD.Visible = False
                        txtOAir.Visible = False
                        txtOAirD.Visible = False
                        lblDate.Text = txtDate.Value
                        If lblDate.Text = "" Then lblDate.Text = ""
                        If lblProj.Text = "" Then lblProj.Text = ""
                        lblRef.Text = txtRef.Text
                        If lblRef.Text = "" Then lblRef.Text = ""
                        lblDated.Text = txtDated.Value
                        If lblDated.Text = "" Then lblDated.Text = ""
                        lblCOR.Text = txtCOR.Text
                        If lblCOR.Text = "" Then lblCOR.Text = ""
                        lblCORDated.Text = txtCORDated.Value
                        If lblCORDated.Text = "" Then lblCORDated.Text = ""
                        lblSACR.Text = txtSACRNo.Text
                        If lblSACR.Text = "" Then lblSACR.Text = ""
                        lblSACRD.Text = txtSACRD.Value
                        If lblSACRD.Text = "" Then lblSACRD.Text = ""
                        lblICR.Text = txtICRNo.Text
                        If lblICR.Text = "" Then lblICR.Text = ""
                        lblICRD.Text = txtICRD.Value
                        If lblICRD.Text = "" Then lblICRD.Text = ""
                        lblOAir.Text = txtOAir.Text
                        If lblOAir.Text = "" Then lblOAir.Text = ""
                        lblOAirD.Text = txtOAirD.Value
                        If lblOAirD.Text = "" Then lblOAirD.Text = ""
                        btnGenerate.Visible = False
                        grddocuments.Visible = False
                        uploaddocument(hdnversion.Value, hdnKeyVal.Value)
                        If Request.QueryString("open") Is Nothing Then
                            dbutils_nsn.CheckGroupingDocBAST(hdnsiteid.Value, hdnversion.Value, ConfigurationManager.AppSettings("BASTID"), ConfigurationManager.AppSettings("WCTRBASTID"), ConfigurationManager.AppSettings("BOQDOCID"))
                            Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
                        Else
                            dbutils_nsn.CheckGroupingDocBAST(hdnsiteid.Value, hdnversion.Value, ConfigurationManager.AppSettings("BASTID"), ConfigurationManager.AppSettings("WCTRBASTID"), ConfigurationManager.AppSettings("BOQDOCID"))
                            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptwhh", "WindowsClose();", True)
                        End If
                    End If

                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while saving the Data');", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please Click Complete button First!');", True)
            End If
            
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please use Internet Explorer');", True)
        End If
    End Sub
    Protected Sub grddocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
            lbl.Text = (grddocuments.PageIndex * grddocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
            Dim url As String = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(4).Text
            If e.Row.Cells(2).Text <> "" And e.Row.Cells(2).Text <> "&nbsp;" Then
                If InStr(e.Row.Cells(3).Text, "WCTR BAST") > 0 Then
                    hdnswid.Value = e.Row.Cells(4).Text
                End If
                e.Row.Cells(3).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
            Else
                If InStr(e.Row.Cells(3).Text, "WCTR BAST") > 0 Then
                    url = "../baut/frmti_wctrbast.aspx?id=" & e.Row.Cells(4).Text & "&Open=0"
                    hdnswid.Value = e.Row.Cells(4).Text
                    e.Row.Cells(3).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
                    Dim rbllist As RadioButtonList = CType(e.Row.FindControl("rdbstatus"), RadioButtonList)
                    rbllist.Visible = False
                    e.Row.Cells(5).ForeColor = Drawing.Color.Red
                    e.Row.Cells(5).Text = "Please generate WCTR first"
                Else
                    e.Row.Cells(3).Text = e.Row.Cells(3).Text
                End If
            End If
        End If
    End Sub
    Protected Sub btnAddRow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRow.Click

        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
        If Rb100percent.Checked = True Or Rb5percent.Checked = True Then
            Session("txtBastRefNo") = txtBastRefNo.Text
            Session("txtDate") = txtDate.Value
            Session("txtRef") = txtRef.Text
            Session("txtDated") = txtDated.Value
            Session("txtCOR") = txtCOR.Text
            Session("txtCORDated") = txtCORDated.Value
            Session("txtSACRNo") = txtSACRNo.Text
            Session("txtSACRD") = txtSACRD.Value
            Session("txtICRNo") = txtICRNo.Text
            Session("txtICRD") = txtICRD.Value
            Session("txtOAir") = txtOAir.Text
            Session("txtOAirD") = txtOAirD.Value
            If Session("webname") = "" Then
                Session("webname") = Split(Request.UrlReferrer.ToString, "/")(2).ToString
            End If
            If Split(Session("webname"), ":").Length > 1 Then
                Session("webname") = Split(Session("webname"), ":")(0)
            End If
            Dim result As Integer = 0
            result = saveData()
            'bugfix101004
            Dim rw As Integer = iCount
            If Not Request.QueryString("rw") = "" Then
                rw = Request.QueryString("rw")
            End If
            'bugfix100927 remove the port no and use https instead
            Dim url As String = "https://" & Session("webname").ToString & Request.ServerVariables("URL") & "?id=" & Request.QueryString("id")
            If Request.QueryString("rw") = "" Then
                url &= "&rw=1" & "&rwx=" & Request.QueryString("rwx")
            Else
                url &= "&rw=" & (rw + 1) & "&rwx=" & Request.QueryString("rwx")
            End If
            'Response.Redirect(url)
            If Session("rdoBast1") = True Then
                Response.Redirect(url & "&rdoBAST=100percent")
            End If

            If Session("rdoBast2") = True Then
                Response.Redirect(url & "&rdoBAST=5percent")
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please choose type of form first!');", True)
        End If
        

    End Sub
    Protected Sub btnAddRowComplete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRowComplete.Click
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
        If Rb100percent.Checked = True Or Rb5percent.Checked = True Then
            If Session("webname") = "" Then
                Session("webname") = Split(Request.UrlReferrer.ToString, "/")(2).ToString
            End If
            If Split(Session("webname"), ":").Length > 1 Then
                Session("webname") = Split(Session("webname"), ":")(0)
            End If
            Dim result As Integer = 0
            If Request.QueryString("rwx") = "0" Or Request.QueryString("rwx") = "" Then
                result = saveData()
            End If
            'bugfix101004
            Dim rw As Integer = iCount
            If Not Request.QueryString("rw") = "" Then
                rw = Request.QueryString("rw")
            End If
            'bugfix100927 remove the port no and use https instead
            Dim url As String = "https://" & Session("webname").ToString & _
                Request.ServerVariables("URL") & "?id=" & Request.QueryString("id") & "&rw=" & rw
            If Request.QueryString("rwx") = "" Or Request.QueryString("rwx") = "0" Then
                url &= "&rwx=1"
            Else
                url &= "&rwx=0"
            End If
            'Response.Redirect(url)
            If Session("rdoBast1") = True Then
                If btnAddRowComplete.Text.ToLower().Equals("edit") Then
                    Response.Redirect(url & "&rdoBAST=100percent&type=edit")
                Else
                    Response.Redirect(url & "&rdoBAST=100percent&type=complete")
                End If

            End If
            If Session("rdoBast2") = True Then
                If btnAddRowComplete.Text.ToLower().Equals("edit") Then
                    Response.Redirect(url & "&rdoBAST=5percent&type=edit")
                Else
                    Response.Redirect(url & "&rdoBAST=5percent&type=complete")
                End If

            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please choose type of form first!');", True)
        End If
        

    End Sub
End Class
