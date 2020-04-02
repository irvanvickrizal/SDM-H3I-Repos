Imports System
Imports DAO
Imports Entities
Imports BusinessLogic
Imports System.Data
Imports Common
Imports System.IO
Partial Class BAUT_frmTI_BAKPFinal
    Inherits System.Web.UI.Page
    Dim DT As New DataTable
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
    Dim PO_Row_Count As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            btnFCOR.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtCORDated,'dd-mmm-yyyy');return false;")
            btnUTRAN.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtDated,'dd-mmm-yyyy');return false;")
            btnSAC.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtSACRD,'dd-mmm-yyyy');return false;")
            btnICR.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtICRD,'dd-mmm-yyyy');return false;")
            btnOAir.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtOAirD,'dd-mmm-yyyy');return false;")
            btnDate.Attributes.Add("onclick", "javascript:popUpCalendar(this,txtDate,'dd-mmm-yyyy');return false;")
            Getdata()
            filloddata()
            'txtBastRefNo.Text = "--BAST RUNNING NUMBER--"
            ''generate bast ref
            If txtbakprefno.Text = "" Then
                txtbakprefno.Text = objdb.ExeQueryScalarString("select dbo.GenerateBAUTRefNo(" & Request.QueryString("id") & ",'BAKP')")
            End If
            Dim strsql As String = "Exec uspSiteBAKPDocListOnlineForm " & Request.QueryString("id")
            DT = objutil.ExeQueryDT(strsql, "sisbakp")
            grddocuments.DataSource = DT
            grddocuments.DataBind()
            grddocuments.Columns(1).Visible = False
            grddocuments.Columns(2).Visible = False
            grddocuments.Columns(4).Visible = False
            Dim rw As Integer = 0
            If Not Request.QueryString("rw") = "" Then
                rw = Request.QueryString("rw")
            End If
            btnAddRow.Attributes.Add("onclick", "transferValue(" & (rw + 1) & ");")
            btnAddRowComplete.Attributes.Add("onclick", "transferValue(" & (rw + 1) & ");")
            btnGenerate.Attributes.Add("onclick", "transferValue(" & (rw + 1) & ");")
        End If
        If Request.QueryString("rwx") = "" Or Request.QueryString("rwx") = "0" Then
            btnAddRow.Enabled = False
            'btnAddRow.Enabled = True
            btnAddRowComplete.Text = "Complete"
        Else
            btnAddRow.Enabled = False
            btnAddRowComplete.Text = "Edit"
        End If
        If Not Page.IsPostBack = True Then
            Dim Str As String = "Exec [Get_MilestoneID_BAKP] " & hdnsiteid.Value & ",'" & lblPORef.Text & "'," & Request.QueryString("id")
            DT = objutil.ExeQueryDT(Str, "Get_PO")
            ' Get_Mutilple_PONO()


            tableHeader(False)
        End If
        lblPORef.Visible = False
        lblPODated.Visible = False
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
    End Sub
    Sub Get_Mutilple_PONO()
        Dim Str As String = "select tblpono,siteno,tbldesc,ContractDate from ODbakpTable where swid = '" & Request.QueryString("id") & "'"
        DT = objutil.ExeQueryDT(Str, "odbakp")
        For i As Integer = 0 To DT.Rows.Count - 1
            If DT.Rows(i)("tbldesc") = "HARDWARE" Then
                txtPOHardware.Text += DT.Rows(i)("tblpono").ToString & " dated " & DT.Rows(i)("ContractDate")
            End If

            If DT.Rows(i)("tbldesc") = "SOFTWARE" Then
                txtPOSoftware.Text += DT.Rows(i)("tblpono").ToString & " dated " & DT.Rows(i)("ContractDate")
            End If

            If DT.Rows(i)("tbldesc") = "SERVICE" Then
                txtPOServices.Text += DT.Rows(i)("tblpono").ToString & " dated " & DT.Rows(i)("ContractDate")
            End If
        Next
    End Sub
    Sub tableHeader(ByRef submit As Boolean)
        tdTable.Controls.Clear()
        If Session("txtbakprefno") IsNot Nothing Then
            txtbakprefno.Text = Session("txtbakprefno")
        End If
        If Session("txtDate") IsNot Nothing Then
            txtDate.Value = Session("txtDate")
        End If
        If Session("txtRef") IsNot Nothing Then
            txtRef.Text = Session("txtRef")
        End If
        If Session("txtDated") IsNot Nothing Then
            txtDated.Value = Session("txtDated")
        End If
        If Session("txtCOR") IsNot Nothing Then
            txtCOR.Text = Session("txtCOR")
        End If
        If Session("txtCORDated") IsNot Nothing Then
            txtCORDated.Value = Session("txtCORDated")
        End If
        If Session("txtSACRNo") IsNot Nothing Then
            txtSACRNo.Text = Session("txtSACRNo")
        End If
        If Session("txtSACRD") IsNot Nothing Then
            txtSACRD.Value = Session("txtSACRD")
        End If
        If Session("txtICRNo") IsNot Nothing Then
            txtICRNo.Text = Session("txtICRNo")
        End If
        If Session("txtICRD") IsNot Nothing Then
            txtICRD.Value = Session("txtICRD")
        End If
        If Session("txtOAir") IsNot Nothing Then
            txtOAir.Text = Session("txtOAir")
        End If
        If Session("txtOAirD") IsNot Nothing Then
            txtOAirD.Value = Session("txtOAirD")
        End If

        Dim strsql As String = "select * from odbakptable where swid=" & Request.QueryString("id")
        DT = objutil.ExeQueryDT(strsql, "odbakptable")

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

        iMianCell11.InnerHtml = "PO Number"
        iMianCell11.Align = "center"
        iMianCell11.Width = "100px"
        iMianCell11.ColSpan = 2
        iMianCell11.RowSpan = 2
        iMianCell12.InnerHtml = "PO"
        iMianCell12.Align = "center"
        iMianCell12.ColSpan = 2
        iMianCell13.InnerHtml = "Implementation"
        iMianCell13.Align = "center"
        iMianCell13.ColSpan = 2
        iMianCell14.InnerHtml = "Delta"
        iMianCell14.Align = "center"
        iMianCell14.ColSpan = 2
        iMianCell14.Width = "200px"
        iMianCell15.InnerHtml = "(EURO)"
        iMianCell15.Align = "center"
        iMianCell15.Visible = False
        iMianCell16.InnerHtml = "(USD)"
        iMianCell16.Align = "right"
        iMianCell17.InnerHtml = "(IDR)"
        iMianCell17.Align = "right"
        iMianCell18.InnerHtml = "(EURO)"
        iMianCell18.Align = "right"
        iMianCell18.Visible = False
        iMianCell19.InnerHtml = "(USD)"
        iMianCell19.Align = "right"
        iMianCell110.InnerHtml = "(IDR)"
        iMianCell110.Align = "right"
        iMianCell111.InnerHtml = "(EURO)"
        iMianCell111.Align = "right"
        iMianCell111.Visible = False
        iMianCell112.InnerHtml = "(USD)"
        iMianCell112.Align = "right"
        iMianCell113.InnerHtml = "(IDR)"
        iMianCell113.Align = "right"

        iMainRows1.Cells.Add(iMianCell11)
        iMainRows1.Cells.Add(iMianCell12)
        iMainRows1.Cells.Add(iMianCell13)
        iMainRows1.Cells.Add(iMianCell14)
        iMainRows2.Cells.Add(iMianCell15)
        iMainRows2.Cells.Add(iMianCell16)
        iMainRows2.Cells.Add(iMianCell17)
        iMainRows2.Cells.Add(iMianCell18)
        iMainRows2.Cells.Add(iMianCell19)
        iMainRows2.Cells.Add(iMianCell110)
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
        Dim ii As Integer = DT.Rows.Count
        If DT.Rows.Count = 1 Then
            ii = ii - 1
        Else
            'ii = ii - 1
        End If

        For i = 1 To (ii + 1) ' suresh
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

            Dim textcss1 As String = "border-style:solid;border-color:white;border-width:1px;background-color:skyblue;text-align:right;"
            Dim textcss2 As String = "border-style:solid;border-color:white;border-width:1px;background-color:skyblue;width:90px;text-align:right;"
            Dim textcss1a As String = "border-style:solid;border-color:white;border-width:1px;background-color:white;text-align:right;"
            Dim textcss2a As String = "border-style:solid;border-color:white;border-width:1px;background-color:white;width:90px;text-align:right;"

            If DT.Rows.Count > 0 Then
                If i <= DT.Rows.Count Then
                    inhtml1 = String.Format("{0:#,##0}", DT.Rows(i - 1).Item("tblpono"))
                    inhtml2 = String.Format("{0:#,##0}", DT.Rows(i - 1).Item("tbldesc"))
                    inhtml3 = String.Format("{0:#,##0}", DT.Rows(i - 1).Item("tbleuro1"))
                    inhtml4 = String.Format("{0:###,##.#0}", DT.Rows(i - 1).Item("tblusd1"))
                    inhtml5 = String.Format("{0:#,##0}", DT.Rows(i - 1).Item("tblidr1"))
                    inhtml6 = String.Format("{0:#,##0}", DT.Rows(i - 1).Item("tbleuro2"))
                    inhtml7 = String.Format("{0:###,##.#0}", DT.Rows(i - 1).Item("tblusd2"))
                    inhtml8 = String.Format("{0:#,##0}", DT.Rows(i - 1).Item("tblidr2"))
                    inhtml9 = String.Format("{0:#,##0}", DT.Rows(i - 1).Item("tbleuro3"))
                    inhtml10 = String.Format("{0:###,##.#0}", DT.Rows(i - 1).Item("tblusd3"))
                    inhtml11 = String.Format("{0:#,##0}", DT.Rows(i - 1).Item("tblidr3"))

                    total1 += DT.Rows(i - 1).Item("tbleuro1")
                    total2 += DT.Rows(i - 1).Item("tblusd1")
                    total3 += DT.Rows(i - 1).Item("tblidr1")
                    total4 += DT.Rows(i - 1).Item("tbleuro2")
                    total5 += DT.Rows(i - 1).Item("tblusd2")
                    total6 += DT.Rows(i - 1).Item("tblidr2")
                    total7 += DT.Rows(i - 1).Item("tbleuro3")
                    total8 += DT.Rows(i - 1).Item("tblusd3")
                    total9 += DT.Rows(i - 1).Item("tblidr3")
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

                iMianCell21.InnerHtml = inhtmlmain.Replace("@id", "input" & i & "a").Replace("@txt", inhtml1).Replace("@sty", textcssA)
                iMianCell21.Align = "right"
                iMianCell22.InnerHtml = inhtmlmain.Replace("@id", "input" & i & "b").Replace("@txt", inhtml2).Replace("@sty", textcssA)
                iMianCell22.Align = "right"
                iMianCell23.InnerHtml = inhtml.Replace("@id", "input" & i & "c").Replace("@txt", inhtml3).Replace("@sty", textcssB)
                iMianCell23.Align = "right"
                iMianCell23.Visible = False
                iMianCell24.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "d").Replace("@txt", inhtml4).Replace("@sty", textcssB)
                iMianCell24.Align = "right"
                iMianCell25.InnerHtml = inhtml.Replace("@id", "input" & i & "e").Replace("@txt", inhtml5).Replace("@sty", textcssB)
                iMianCell25.Align = "right"
                iMianCell26.InnerHtml = inhtml.Replace("@id", "input" & i & "f").Replace("@txt", inhtml6).Replace("@sty", textcssB)
                iMianCell26.Align = "right"
                iMianCell26.Visible = False
                iMianCell27.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "g").Replace("@txt", inhtml7).Replace("@sty", textcssB)
                iMianCell27.Align = "right"
                iMianCell28.InnerHtml = inhtml.Replace("@id", "input" & i & "h").Replace("@txt", inhtml8).Replace("@sty", textcssB)
                iMianCell28.Align = "right"
                iMianCell29.InnerHtml = inhtml.Replace("@id", "input" & i & "i").Replace("@txt", inhtml9).Replace("@sty", textcssB)
                iMianCell29.Align = "right"
                iMianCell29.Visible = False
                iMianCell210.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "j").Replace("@txt", inhtml10).Replace("@sty", textcssB)
                iMianCell210.Align = "right"
                iMianCell211.InnerHtml = inhtml.Replace("@id", "input" & i & "k").Replace("@txt", inhtml11).Replace("@sty", textcssB)
                iMianCell211.Align = "right"
            Else
                inhtmlmain = "<input type='text' id='@id' runat='server'  value='@txt' style='@sty'>"
                inhtml = "<input type='text' id='@id' runat='server'  value='@txt' style='@sty' onblur = 'this.value=formatCurrency(this.value);'>"
                inhtmlUSD = "<input type='text' id='@id' runat='server'  value='@txt' style='@sty' onblur ='this.value=formatusdCurrency(this.value);'>"
                If Request.QueryString("rw") = "" Then
                    iMianCell21.InnerHtml = inhtmlmain.Replace("@id", "input" & i & "a").Replace("@txt", lblPONO.Text)
                Else
                    iMianCell21.InnerHtml = inhtmlmain.Replace("@id", "input" & i & "a").Replace("@txt", "")
                End If
                iMianCell22.InnerHtml = inhtmlmain.Replace("@id", "input" & i & "b").Replace("@txt", "0")
                iMianCell23.InnerHtml = inhtml.Replace("@id", "input" & i & "c").Replace("@txt", "0").Replace("@sty", textcss2)
                iMianCell23.Visible = False
                iMianCell24.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "d").Replace("@txt", "0").Replace("@sty", textcss2)
                iMianCell24.Align = "right"
                iMianCell25.InnerHtml = inhtml.Replace("@id", "input" & i & "e").Replace("@txt", "0").Replace("@sty", textcss2)
                iMianCell25.Align = "right"
                iMianCell26.InnerHtml = inhtml.Replace("@id", "input" & i & "f").Replace("@txt", "0").Replace("@sty", textcss2)
                iMianCell26.Align = "right"
                iMianCell26.Visible = False
                iMianCell27.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "g").Replace("@txt", "0").Replace("@sty", textcss2)
                iMianCell27.Align = "right"
                iMianCell28.InnerHtml = inhtml.Replace("@id", "input" & i & "h").Replace("@txt", "0").Replace("@sty", textcss2)
                iMianCell28.Align = "right"
                iMianCell29.InnerHtml = inhtml.Replace("@id", "input" & i & "i").Replace("@txt", "0").Replace("@sty", textcss2)
                iMianCell29.Align = "right"
                iMianCell29.Visible = False
                iMianCell210.InnerHtml = inhtmlUSD.Replace("@id", "input" & i & "j").Replace("@txt", "0").Replace("@sty", textcss2)
                iMianCell210.Align = "right"
                iMianCell211.InnerHtml = inhtml.Replace("@id", "input" & i & "k").Replace("@txt", "0").Replace("@sty", textcss2)
            End If

            iMainRows3.Cells.Add(iMianCell21)
            iMainRows3.Cells.Add(iMianCell22)
            iMainRows3.Cells.Add(iMianCell23)
            iMainRows3.Cells.Add(iMianCell24)
            iMainRows3.Cells.Add(iMianCell25)
            iMainRows3.Cells.Add(iMianCell26)
            iMainRows3.Cells.Add(iMianCell27)
            iMainRows3.Cells.Add(iMianCell28)
            iMainRows3.Cells.Add(iMianCell29)
            iMainRows3.Cells.Add(iMianCell210)
            iMainRows3.Cells.Add(iMianCell211)

            iMainTable.Rows.Add(iMainRows3)
        Next

        iMianCell31.InnerHtml = ""
        iMianCell32.InnerHtml = "<b>Total</b>"
        iMianCell32.Align = "right"
        iMianCell33.InnerHtml = String.Format("{0:###,##.#0}", total1)
        iMianCell33.Align = "right"
        iMianCell33.Visible = False
        'USD
        Dim USD1 As Double = total2
        iMianCell34.InnerHtml = String.Format("{0:###,##.#0}", USD1)
        iMianCell34.Align = "right"
        iMianCell35.InnerHtml = String.Format("{0:#,##0}", total3)
        iMianCell35.Align = "right"
        iMianCell36.InnerHtml = String.Format("{0:###,##.#0}", total4)
        iMianCell36.Align = "right"
        iMianCell36.Visible = False
        'USD
        Dim USD2 As Double = total5
        iMianCell37.InnerHtml = String.Format("{0:###,##.#0}", USD2)
        iMianCell37.Align = "right"
        iMianCell38.InnerHtml = String.Format("{0:#,##0}", total6)
        iMianCell38.Align = "right"
        iMianCell39.InnerHtml = String.Format("{0:###,##.#0}", total7)
        iMianCell39.Align = "right"
        iMianCell39.Visible = False
        'USD
        Dim USD3 As Double = total8
        iMianCell310.InnerHtml = String.Format("{0:###,##.#0}", USD3)
        iMianCell310.Align = "right"
        iMianCell311.InnerHtml = String.Format("{0:#,##0}", total9)
        iMianCell311.Align = "right"

        iMainRows4.Cells.Add(iMianCell31)
        iMainRows4.Cells.Add(iMianCell32)
        iMainRows4.Cells.Add(iMianCell33)
        iMainRows4.Cells.Add(iMianCell34)
        iMainRows4.Cells.Add(iMianCell35)
        iMainRows4.Cells.Add(iMianCell36)
        iMainRows4.Cells.Add(iMianCell37)
        iMainRows4.Cells.Add(iMianCell38)
        iMainRows4.Cells.Add(iMianCell39)
        iMainRows4.Cells.Add(iMianCell310)
        iMainRows4.Cells.Add(iMianCell311)

        iMainTable.Rows.Add(iMainRows4)

        tdTable.Controls.Add(iMainTable)
        tdTable.Style.Add("lblText", " ")
    End Sub

    ' it will fill online form data if already exists for same site...
    Sub filloddata()
        Dim strsql As String = "Exec odbakpDetail " & Request.QueryString("id")
        oddt = objutil.ExeQueryDT(strsql, "odbakp")
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
            txtOAirD.Value = IIf(oddt.Rows(0).Item("onairdate").ToString.IndexOf("1900") = -1, oddt.Rows(0).Item("onairdate").ToString, "")
            'txtPO.Value = oddt.Rows(0).Item("IDRPO").ToString
            'txtActual.Value = oddt.Rows(0).Item("IDRACT").ToString
            'txtDelta.Value = oddt.Rows(0).Item("IDRDelta").ToString
            'txtPOUSD.Value = oddt.Rows(0).Item("USDPO").ToString
            'txtActUSD.Value = oddt.Rows(0).Item("USDACT").ToString
            'txtDelUSD.Value = oddt.Rows(0).Item("USDDelta").ToString
        End If
    End Sub
    Sub Getdata()
        'DT = objBo.uspTIFBastOnLine(Request.QueryString("id"))
        DT = objutil.ExeQueryDT("Exec uspTIFBakpOnLine " & Request.QueryString("id") & "", "222")
        ' DT = objutil.ExeQueryDT("Exec uspTIFBakpOnLine " 40"", "222")
        If DT.Rows.Count > 0 Then
            hdnsiteid.Value = DT.Rows(0).Item("SiteId").ToString
            hdnversion.Value = DT.Rows(0).Item("version").ToString
            hdnWfId.Value = DT.Rows(0).Item("WF_Id").ToString
            hdndocId.Value = DT.Rows(0).Item("docId").ToString
            hdnSiteno.Value = DT.Rows(0).Item("site_no").ToString
            hdnScope.Value = DT.Rows(0).Item("Scope").ToString
            hdnDGBox.Value = DT.Rows(0).Item("DGBox").ToString
            'i = objBo.uspCheckIntegration(hdndocId.Value, hdnSiteno.Value)
            i = objdb.ExeQueryScalar("exec uspCheckIntegration  '" & hdndocId.Value & "' ,'" & hdnSiteno.Value & "'," & hdnversion.Value & "")
            Select Case i
                Case 1
                    Dochecking()
                Case 2
                    'temporary setting
                    hdnKeyVal.Value = 3

                    'original setting
                    'hdnKeyVal.Value = 0

                    'disabled temporary
                    'ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('Int');", True)
                Case 3
                    Dochecking()
                Case 4
                    hdnKeyVal.Value = 0
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('IntD');", True)
            End Select

            Dim St_no As String = ""
            St_no = DT.Rows(0).Item("site_no").ToString

            lblSite.Text = DT.Rows(0).Item("site_no") & "/" & DT.Rows(0).Item("site_name").ToString
            lblPORef.Text = DT.Rows(0).Item("pono").ToString
            lblPODated.Text = DT.Rows(0).Item("custporecdt").ToString
            lblPONO.Text = DT.Rows(0).Item("pono").ToString
            lblPONO2.Text = DT.Rows(0).Item("pono").ToString
            'lblBR.Text = DT.Rows(0).Item("ReferenceNO").ToString
            'lblBRD.Text = DT.Rows(0).Item("ctdt").ToString
            'txtbakprefno.Text = DT.Rows(0).Item("Ref").ToString
            lblProj.Text = DT.Rows(0).Item("projectid").ToString
            lblsiteidpo.Text = DT.Rows(0).Item("siteidpo").ToString
            lblsitenamepo.Text = DT.Rows(0).Item("sitenamepo").ToString
            Dim str As String = ""
            str = "Exec [uspGetOnLineFormBind] " & DT.Rows(0).Item("WF_Id").ToString & "," & DT.Rows(0).Item("SiteId").ToString
            DT = objutil.ExeQueryDT(str, "SiteDoc1")

            DLDigitalSign.DataSource = DT
            DLDigitalSign.DataBind()
            Dim dtv As DataView = DT.DefaultView
            dtv.Sort = "tsk_id desc"
            dtList.DataSource = dtv
            dtList.DataBind()
            HDDgSignTotal.Value = DT.Rows.Count

            str = "select bautrefno,convert(char,bautdate,103)as bautdate from odbaut where siteno = '" & St_no & "' AND PONO = '" & lblPORef.Text.ToString() & "'"
            DT = objutil.ExeQueryDT(str, "getpo")
            If DT.Rows.Count > 0 Then

                lblBR.Text = DT.Rows(0).Item("BautRefno").ToString
                lblBRD.Text = DT.Rows(0).Item("bautdate").ToString
            End If
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
        Else
            hdnsiteid.Value = 0
            hdnversion.Value = 0
            hdnWfId.Value = 0
            hdndocId.Value = 0
            hdnSiteno.Value = ""
        End If
    End Sub

#Region ""
    Sub Get_Sub_PO(ByVal SiteNo As String, ByVal PONO As String)

    End Sub
#End Region

    Sub Dochecking()
        roleid = Session("Role_Id")
        grp = Session("User_Type")
        If objBo.uspApprRequired(hdnsiteid.Value, hdndocId.Value, hdnversion.Value) <> 0 Then
            If objBo.verifypermission(hdndocId.Value, roleid, grp) <> 0 Then
                Select Case objBo.DocUploadverify(hdnsiteid.Value, hdndocId.Value, hdnversion.Value).ToString
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
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                        'makevisible()
                        Exit Sub
                End Select
            Else
                If Session("Role_Id") = 1 Then ''if role is sysadmin then no need to verify permission everthing permitted.                    
                    Select Case objBo.DocUploadverify(hdnsiteid.Value, hdndocId.Value, hdnversion.Value).ToString
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
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "Showmain('2sta');", True)
                    Exit Sub
                    'Response.Redirect("frmSiteDocUploadTree.aspx")
            End Select
        End If
    End Sub
    Sub uploaddocument(ByVal vers As Integer, ByVal keyval As Integer)
        DT = objBo.getbautdocdetailsNEW(hdndocId.Value) '(Constants._Doc_SSR)
        Dim sec As String = DT.Rows(0).Item("sec_name").ToString
        Dim subsec As String = DT.Rows(0).Item("subsec_name").ToString
        Dim secpath As String = ""
        Dim ft As String = ""
        Dim path As String = ""
        Dim filenameorg As String
        FileNameOnly = "-" & Constants._Doc_BAKP & "-"
        filenameorg = hdnSiteno.Value & FileNameOnly & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        ReFileName = filenameorg & ".htm"
        'secpath = IIf(subsec = "" Or subsec = "0", sec & "\", sec & "\" & subsec & "\")
        ft = ConfigurationManager.AppSettings("Type") & lblPONO.Text & "-" & hdnScope.Value & "\"
        path = ConfigurationManager.AppSettings("Fpath") & hdnSiteno.Value & ft
        Dim strResult As String = DOInsertTrans(hdnsiteid.Value, hdndocId.Value, vers, path)

        Dim DocPath As String = ""
        If strResult = "0" Then
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(path)
        ElseIf strResult = "1" Then
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(path)
        Else
            DocPath = hdnSiteno.Value & ft & secpath & CreatePDFFile(path)
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
        'Dim strsql As String = "Update bakpmaster set Pstatus=1 where Site_ID = " & hdnsiteid.Value & " and SiteVersion =" & hdnversion.Value & " and PONO = '" & lblPONO.Text & "'"
        'objutil.ExeUpdate(strsql)
        'sendmail2()
        chek4alldoc() ' for messaage to previous screen ' and saving final docupload date in reporttable
        'for BAUT
        'objBo.check4BAUTBAKP(hdnsiteid.Value, hdnversion.Value, 1, lblPONO.Text, Session("User_Name"))
        'for BAST1
        ' objBo.check4BAUTBAKP(hdnsiteid.Value, hdnversion.Value, 2, lblPONO.Text, Session("User_Name"))
        'Fill Transaction table
        AuditTrail()
        'If hdnready4baut.Value = 1 Then
        '    Response.Redirect("../PO/frmSiteDocUploadTree.aspx?ready=yes")
        'Else
        '    Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
        'End If
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

        filenameorg = hdnSiteno.Value & "-BAKP-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
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
                        If (dtNew.Rows(aa).Item(5).ToString <> "1") Then
                            If bb = False Then
                                'sendmailTrans(hdnsiteid.Value, dtNew.Rows(aa).Item(3).ToString, dtNew.Rows(aa).Item(4).ToString)
                                Try
                                    objmail.sendmailTrans(0, hdnsiteid.Value, dtNew.Rows(aa).Item(3).ToString, dtNew.Rows(aa).Item(4).ToString, ConfigurationManager.AppSettings("uploadmailconst"))
                                Catch ex As Exception
                                    objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailbaut'")
                                End Try
                                bb = True
                            End If
                        End If
                    Next
                End If
                Return "1"
            Else
                CreateXY()
                Return "1"
            End If
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
                     " where tt.TSK_Id=1 and wfid=" & hdnWfId.Value & "   order by wfdid"

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
            If bb = False Then
                Try
                    objmail.sendmailTrans(0, hdnsiteid.Value, dvNotIn(IncrMentX).Item(3).ToString, dvNotIn(IncrMentX).Item(4).ToString, ConfigurationManager.AppSettings("uploadmailconst"))
                Catch ex As Exception
                    objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailbaut'")
                End Try
                bb = True
            End If
        Next
        ' loop to update xy co-odinates
        ' loop to update xy co-odinates
        Dim strsql2 As String
        Dim dtnew2 As DataTable
        Dim dvnotin2 As DataView
        Dim tcount As Integer = 0, IncrY As Integer = 0

        strsql2 = "select wfdid,wfid,sorder,grpcode,roleid,a.tsk_id,a.grpId,tt.tskname,0 as   X_Coordinate,0 as  Y_Coordinate," & _
                              "0 as  PageNo,IsNull(a.Sorder,0)Sno from twfdefinition A inner join tusertype B on a.grpid=b.grpid inner join ttask as tt on tt.tsk_id=a.tsk_id " & _
                   " where tt.TSK_Id not in (1,5,6) and wfid=" & hdnWfId.Value & "   order by wfdid"
        dtnew2 = objutil.ExeQueryDT(strsql2, "dd2")

        dvnotin2 = dtnew2.DefaultView

        dvnotin2.RowFilter = "TSK_Id <>1"
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
                yAdjustment = -420
                'If PO_Row_Count = 0 Then
                '    yAdjustment = -400
                'End If

                'If PO_Row_Count = 1 Then
                '    yAdjustment = -430
                'End If

                'If PO_Row_Count = 2 Then
                '    yAdjustment = -550
                'End If

                'If PO_Row_Count = 3 Then
                '    yAdjustment = -630
                'End If

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


                'ElseIf InStr(strversion, "IE7") > 0 Then
                '    xAdjustment1 = 0
                '    xAdjustment2 = +80
                '    yAdjustment = 40
                '    If dvNotInNew.Count = 2 Then
                '        If IncrMentX = 0 Then
                '            objdo.XVal = ((iHDX.Value * 2) + 12) + xAdjustment1
                '        Else
                '            objdo.XVal = ((iHDX.Value / 2) + 84) + xAdjustment2
                '        End If
                '    Else
                '        objdo.XVal = ((iHDX.Value / 2) + 30 + (intCount * 30)) + xAdjustment1
                '    End If
                '    objdo.YVal = (182 + (791 - iHDY.Value) + (IncrMentY * 52)) + yAdjustment


            ElseIf InStr(strversion, "IE7") > 0 Then
                xAdjustment1 = +100 '-13
                xAdjustment2 = -13 '+73
                yAdjustment = 80
                'If PO_Row_Count = 0 Then
                '    yAdjustment = 80
                'End If

                'If PO_Row_Count = 1 Then
                '    yAdjustment = -430
                'End If


                'If PO_Row_Count = 2 Then
                '    yAdjustment = -10
                'End If

                'If PO_Row_Count = 3 Then
                '    yAdjustment = -80
                'End If


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

                'ElseIf InStr(strversion, "IE7") > 0 Then
                '    If dvnotin2.Count = 2 Then

                '        If IncrMentX = 0 Then
                '            objdo.XVal = iHDX.Value + 30
                '        Else
                '            objdo.XVal = (iHDX.Value / 2) + 43 + (intCount * 30)
                '        End If
                '    Else
                '        objdo.XVal = (iHDX.Value / 2) + (intCount * 30)
                '    End If

                '    objdo.YVal = 185 + (iHDY.Value - 791) + (IncrY * 52) - (introw * 40)

            ElseIf InStr(strversion, "IE8") > 0 Then
                xAdjustment1 = +350 '-13
                xAdjustment2 = -13
                yAdjustment = 10  '4 row added
              

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
                If (objdo.UserType = "C") Then
                    objdo.XVal = 30
                    objdo.YVal = 200
                End If

                If (objdo.UserType = "E") Then
                    objdo.XVal = 300
                    objdo.YVal = 200
                End If

            ElseIf InStr(strversion, "Fire") > 0 Then
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
            objdb.ExeNonQuery("update wftransaction set xval=" & objdo.XVal & ",yval=" & objdo.YVal & ", pageno=" & objdo.PageNo & " where site_id= " & objdo.Site_Id & " and siteversion= " & objdo.SiteVersion & " and docid= " & objdo.DocId & " and tsk_id=" & objdo.TSK_Id & "")
        Next
    End Sub

    'Sub sendmailTrans(ByVal siteid As Integer, ByVal usertype As String, ByVal usrrole As Integer)
    '    Dim dtn As New DataTable
    '    dtn = objBo.uspgetemail(siteid, usertype, usrrole)
    '    If dtn.Rows.Count > 0 Then
    '        If dtn.Rows(0).Item(3) <> "X" Then
    '            dt = objBOM.uspMailReportLD(Constants.docupload, )  ''this is fro document upload time sending mail
    '            Dim k As Integer
    '            Dim Remail As String = "'"
    '            Dim name As String = ""
    '            Dim doc As New StringBuilder
    '            Remail = dtn.Rows(0).Item(3).ToString
    '            name = dtn.Rows(0).Item(2).ToString
    '            Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
    '            Dim receiverAdd As String = Remail
    '            Dim mySMTPClient As New System.Net.Mail.SmtpClient
    '            Dim myEmail As New System.Net.Mail.MailMessage
    '            myEmail.BodyEncoding() = System.Text.Encoding.UTF8
    '            myEmail.SubjectEncoding = System.Text.Encoding.UTF8
    '            myEmail.To.Add(receiverAdd)
    '            myEmail.Subject = dt.Rows(0).Item(3).ToString  ''subject from table
    '            myEmail.Body = dt.Rows(0).Item(2).ToString & name & ",<br> <br>" ''salutatation from table
    '            myEmail.Body = myEmail.Body & dt.Rows(0).Item(4).ToString & " <br> <br> "
    '            myEmail.Body = myEmail.Body & " Process - " & ConfigurationManager.AppSettings("Type").Replace("\", "") & " <br> <br> "
    '            Dim sb As String = ""
    '            sb = "<table  border=1>"
    '            sb = sb & "<tr><td>Contract #</td><td>Site</td><td>Scope</td><td>Document</td><td>Date Submitted</td></tr>"
    '            For k = 0 To dtn.Rows.Count - 1
    '                sb = sb & "<tr><td> " & dtn.Rows(k).Item(6).ToString & "  </td><td>" & dtn.Rows(k).Item(4).ToString & "</td><td>" & dtn.Rows(k).Item(5).ToString & " </td><td>" & dtn.Rows(k).Item(0).ToString & "</td><td>" & dtn.Rows(k).Item(7).ToString & "</td></tr>"
    '            Next
    '            sb = sb & "</table>"
    '            myEmail.Body = myEmail.Body & sb & "<br/>"
    '            Dim ab As String
    '            ab = "<a href='http://203.153.105.232'>Click here</a>" & " to go to e-BAST"
    '            myEmail.Body = myEmail.Body & ab & "<br/>" & dt.Rows(0).Item(5).ToString   ''closing
    '            myEmail.IsBodyHtml = True
    '            myEmail.From = New System.Net.Mail.MailAddress(senderAdd, "NSN")
    '            mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
    '            Try
    '                mySMTPClient.Send(myEmail)
    '            Catch ex As Exception
    '                objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtrans'")
    '            End Try
    '        End If
    '    End If
    'End Sub
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
        Dim strsql As String
        Dim s As String = csv.Value.ToString
        Dim parts1 As String() = s.Split(New Char() {"@"c})
        Dim parts2 As String()
        Dim item As String = "0"
        Dim items As String = ""
        Dim idx As Decimal = 0
        Dim j As Integer
        Dim rw As Integer = 0
        If Not Request.QueryString("rw") = "" Then
            rw = Request.QueryString("rw")
        End If
        Dim result As Integer = 0
        While 1
            parts2 = parts1(idx).Split(New Char() {"!"c})
            items = ""
            j = 0
            For Each item In parts2
                If j = 0 Or j = 1 Then
                    If item = "" Then
                        items = items & "''@@"
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
            strsql = "Exec odbakpinsert " & Request.QueryString("id") & " ,'" & lblPONO.Text & "','" & hdnsiteid.Value & "','" & hdnversion.Value & "'," & _
            "'" & lblProj.Text & "'," & IIf(txtDate.Value.ToString <> "", "'" & txtDate.Value & "'", "null") & ",'" & txtRef.Text & "'," & _
            "" & IIf(txtDated.Value.ToString <> "", "'" & txtDated.Value & "'", "null") & ",'" & txtCOR.Text & "'," & IIf(txtCORDated.Value.ToString <> "", "'" & txtCORDated.Value & "'", "null") & "," & _
            "'" & txtSACRNo.Text & "'," & IIf(txtSACRD.Value.ToString <> "", "'" & txtSACRD.Value & "'", "null") & ",'" & txtICRNo.Text & "', " & _
            "" & IIf(txtICRD.Value.ToString <> "", "'" & txtICRD.Value & "'", "null") & ",'" & txtOAir.Text & "'," & IIf(txtOAirD.Value.ToString <> "", "'" & txtOAirD.Value & "'", "null") & "," & _
            items & _
            "'" & Session("User_Name") & "'"

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
            Dim result As Integer = 1
            'result = saveData()
            btnAddRow.Visible = False
            btnAddRowComplete.Visible = False
            If result = 1 Then
                tableHeader(False)
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
                        'txtPO.Visible = False
                        'txtActual.Visible = False
                        'txtDelta.Visible = False
                        'txtPOUSD.Visible = False
                        'txtActUSD.Visible = False
                        'txtDelUSD.Visible = False
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
                        'lblPO.Text = txtPO.Value
                        'If lblPO.Text = "" Then lblPO.Text = ""
                        'lblActual.Text = txtActual.Value
                        'If lblActual.Text = "" Then lblActual.Text = ""
                        'lblDelta.Text = txtDelta.Value
                        'If lblDelta.Text = "" Then lblDelta.Text = ""
                        'lblPOUSD.Text = txtPOUSD.Value
                        'If lblPOUSD.Text = "" Then lblPOUSD.Text = ""
                        'lblActUSD.Text = txtActUSD.Value
                        'If lblActUSD.Text = "" Then lblActUSD.Text = ""
                        'lblDelUSD.Text = txtDelUSD.Value
                        'If lblDelUSD.Text = "" Then lblDelUSD.Text = ""
                        btnGenerate.Visible = False
                        grddocuments.Visible = False
                        ' Response.Write("<script language='javascript' type='text/javascript'>alert('" + txtBastRefNo.Text + "');</script>")
                        uploaddocument(hdnversion.Value, hdnKeyVal.Value)
                        'Response.Write(Request.QueryString("open"))
                        If Request.QueryString("open") Is Nothing Then
                            Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
                        Else
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
                    'txtPO.Visible = False
                    'txtActual.Visible = False
                    'txtDelta.Visible = False
                    'txtPOUSD.Visible = False
                    'txtActUSD.Visible = False
                    'txtDelUSD.Visible = False

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
                    'lblPO.Text = txtPO.Value
                    'If lblPO.Text = "" Then lblPO.Text = ""
                    'lblActual.Text = txtActual.Value
                    'If lblActual.Text = "" Then lblActual.Text = ""
                    'lblDelta.Text = txtDelta.Value
                    'If lblDelta.Text = "" Then lblDelta.Text = ""
                    'lblPOUSD.Text = txtPOUSD.Value
                    'If lblPOUSD.Text = "" Then lblPOUSD.Text = ""
                    'lblActUSD.Text = txtActUSD.Value
                    'If lblActUSD.Text = "" Then lblActUSD.Text = ""
                    'lblDelUSD.Text = txtDelUSD.Value
                    'If lblDelUSD.Text = "" Then lblDelUSD.Text = ""
                    btnGenerate.Visible = False
                    grddocuments.Visible = False
                    'Response.Write("<script language='javascript' type='text/javascript'>alert('" + txtBastRefNo.Text + "');</script>")
                    uploaddocument(hdnversion.Value, hdnKeyVal.Value)
                    'Response.Write(Request.QueryString("open"))
                    'Response.Write("<script language='javascript' type='text/javascript'>alert('" + txtBastRefNo.Text + "');</script>")
                    If Request.QueryString("open") Is Nothing Then
                        Response.Redirect("../PO/frmSiteDocUploadTree.aspx")
                    Else
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptwhh", "WindowsClose();", True)
                        '  Response.Write("<script language='javascript' type='text/javascript'>WindowsClose();</script>")
                    End If
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while saving the Data');", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please use Internet Explorer');", True)
        End If
    End Sub
    Protected Sub grddocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
            lbl.Text = (grddocuments.PageIndex * grddocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
            'Dim url As String = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(4).Text
            'If e.Row.Cells(2).Text <> "" And e.Row.Cells(2).Text <> "&nbsp;" Then
            '    e.Row.Cells(3).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
            'Else
            '    e.Row.Cells(3).Text = e.Row.Cells(3).Text
            'End If
            Dim url As String = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(4).Text
            If e.Row.Cells(2).Text <> "" And e.Row.Cells(2).Text <> "&nbsp;" Then
                'hdnswid.Value = e.Row.Cells(4).Text
                If InStr(e.Row.Cells(3).Text, "WCTR BAKP") > 0 Then
                    hdnswid.Value = e.Row.Cells(4).Text
                End If
                e.Row.Cells(3).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(3).Text & "</a>"
            Else

                If InStr(e.Row.Cells(3).Text, "WCTR BAKP") > 0 Then
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
            'Dim rbl As RadioButtonList = CType(e.Row.FindControl("rdbstatus"), RadioButtonList)
            'If e.Row.Cells(7).Text = 2 Then
            '    rbl.Visible = True
            'Else
            '    rbl.Visible = False
            'End If
        End If
    End Sub
    Protected Sub btnAddRow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRow.Click
        PO_Row_Count = PO_Row_Count + 1
        Session("txtbakprefno") = txtbakprefno.Text
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

        Dim result As Integer = 0

        Dim rw1 As Integer = Request.QueryString("rw")
        ' result = saveData()
        Dim rw As Integer = 0
        If Not Request.QueryString("rw") = "" Then
            rw = Request.QueryString("rw")
        End If
        Dim url As String = "https://" & Request.ServerVariables("SERVER_NAME") & ":" & Request.ServerVariables("SERVER_PORT") & _
            Request.ServerVariables("URL") & "?id=" & Request.QueryString("id")
        If Request.QueryString("rw") = "" Then
            url &= "&rw=1" & "&rwx=" & Request.QueryString("rwx")
        Else
            url &= "&rw=" & (rw + 1) & "&rwx=" & Request.QueryString("rwx")
        End If
        Response.Redirect(url)
    End Sub

    Sub Adding_Dynamic_Rows()
        Dim ut As Integer = 2

        ' For i As Integer = 0 To ut - 1
        PO_Row_Count = PO_Row_Count + 1

        Session("txtbakprefno") = txtbakprefno.Text
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

        Dim result As Integer = 0
        result = saveData()
        Dim rw As Integer = 0
        If Not Request.QueryString("rw") = "" Then
            rw = Request.QueryString("rw")
        End If
        Dim url As String = "https://" & Request.ServerVariables("SERVER_NAME") & ":" & Request.ServerVariables("SERVER_PORT") & _
            Request.ServerVariables("URL") & "?id=" & Request.QueryString("id")
        If Request.QueryString("rw") = "" Then
            url &= "&rw=1" & "&rwx=" & Request.QueryString("rwx")
        Else
            url &= "&rw=" & (rw + 1) & "&rwx=" & Request.QueryString("rwx")
        End If
        Response.Redirect(url)
        'Next


    End Sub

    Protected Sub btnAddRowComplete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRowComplete.Click
        Dim result As Integer = 0
        If Request.QueryString("rwx") = "0" Or Request.QueryString("rwx") = "" Then
            If Not btnAddRowComplete.Text = "Edit" Then
                result = saveData()
            End If
        End If
        Dim rw As Integer = 0
        If Not Request.QueryString("rw") = "" Then
            rw = Request.QueryString("rw")
        End If
        Dim url As String = "https://" & Request.ServerVariables("SERVER_NAME") & ":" & Request.ServerVariables("SERVER_PORT") & _
            Request.ServerVariables("URL") & "?id=" & Request.QueryString("id") & "&rw=" & rw
        If Request.QueryString("rwx") = "" Or Request.QueryString("rwx") = "0" Then
            url &= "&rwx=1"
        Else
            url &= "&rwx=0"
        End If
        Response.Redirect(url)

    End Sub

    Protected Sub btnPO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPO.Click
        Dim Str As String = "Exec [Get_MilestoneID] " & hdnsiteid.Value & "," & lblPORef.Text & "," & Request.QueryString("id")
        DT = objutil.ExeQueryDT(Str, "Get_PO")
        Dim dt_Count As Integer = 4 'DT.Rows.Count
        Session("txtbakprefno") = txtbakprefno.Text
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

        Dim url As String

        Dim result As Integer = 0
        result = saveData()
        Dim rw As Integer = 0
        'If Not Request.QueryString("rw") = "" Then
        '    rw = Request.QueryString("rw")
        'End If
        rw = 2
        url = "https://" & Request.ServerVariables("SERVER_NAME") & ":" & Request.ServerVariables("SERVER_PORT") & _
            Request.ServerVariables("URL") & "?id=" & Request.QueryString("id")
        'If Request.QueryString("rw") = "" Then
        '    url &= "&rw=1" & "&rwx=" & Request.QueryString("rwx")
        'Else
        url &= "&rw=" & (rw + 1) & "&rwx=" & Request.QueryString("rwx")
        ' End If

        'If Request.QueryString("rwx") = "" Or Request.QueryString("rwx") = "0" Then
        '    btnAddRow.Enabled = True
        '    btnAddRowComplete.Text = "Complete"
        'Else
        '    btnAddRow.Enabled = False
        '    btnAddRowComplete.Text = "Edit"
        'End If

        'tableHeader(False)

        Response.Redirect(url)
    End Sub
End Class
