Imports BusinessLogic
Imports Common
Imports System.Data
Partial Class frmCheckList
    Inherits System.Web.UI.Page
    Dim objBO As New BODashBoard
    Dim objdl As New BODDLs
    Dim objbod As New BOSiteDocs

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            objdl.fillDDL(DDPoDetails, "pono", True, Constants._DDL_Default_Select)
        End If
        dvPrint.Style.Add("display", "none")
    End Sub
    Sub CreatecheckList()
        Dim iMain As New HtmlTable
        iMain.Width = "100%"
        iMain.Attributes.Add("class", "ContentText")
        iMain.CellPadding = 0
        iMain.CellSpacing = 0
        iMain.Border = 1
        iMain.BorderColor = "#00576a"
        'top row
        Dim iMainRow As New HtmlTableRow
        iMainRow.Attributes.Add("class", "SubPageTitle")
        Dim iMainCell1 As New HtmlTableCell
        Dim iMainCell2 As New HtmlTableCell
        Dim iMainCell3 As New HtmlTableCell
        iMainCell1.Width = "60%"
        iMainCell2.Width = "16%"
        iMainCell3.Width = "24%"
        iMainCell1.InnerHtml = "Nokia Siemens Networks Folder"
        iMainCell2.ColSpan = 2
        iMainCell2.InnerHtml = "Checked"
        iMainCell3.InnerHtml = "Note"
        iMainRow.Cells.Add(iMainCell1)
        iMainRow.Cells.Add(iMainCell2)
        iMainRow.Cells.Add(iMainCell3)
        iMain.Rows.Add(iMainRow)
        'second row
        Dim iMainRow41 As New HtmlTableRow
        iMainRow41.Attributes.Add("class", "SubPageTitle")
        Dim iMainCell141 As New HtmlTableCell
        Dim iMainCell142 As New HtmlTableCell
        Dim iMainCell143 As New HtmlTableCell
        Dim iMainCell144 As New HtmlTableCell
        iMainCell141.Width = "60%"
        iMainCell142.Width = "8%"
        iMainCell143.Width = "8%"
        iMainCell144.Width = "24%"
        iMainCell142.Align = "center"
        iMainCell143.Align = "center"
        iMainCell141.InnerHtml = "&nbsp;"
        iMainCell142.InnerHtml = "NSN&nbsp;&nbsp;"
        iMainCell143.InnerHtml = "Telkomsel&nbsp;&nbsp;"
        iMainCell144.InnerHtml = "&nbsp;"
        iMainRow41.Cells.Add(iMainCell141)
        iMainRow41.Cells.Add(iMainCell142)
        iMainRow41.Cells.Add(iMainCell143)
        iMainRow41.Cells.Add(iMainCell144)
        iMain.Rows.Add(iMainRow41)
        CheckList.Controls.Add(iMain)
        Dim dtCheck As New DataTable
        Dim stval() As String = DDSite.SelectedValue.Split("-")

        dtCheck = objBO.CheckListReport(stval(0))
        Dim intSecId, intSubSecId, IntCount, IntCountSub As New Integer
        Dim boolSec, boolSubSec As New Boolean
        Static intSubsecOldId, intsecOldId, intSecIdCount, intSubSecIdCount, intDocCount As New Integer
        intSecIdCount = 1
        intSubSecIdCount = 1
        intDocCount = 1
        For IntCount = 0 To dtCheck.Rows.Count - 1
            If ((IntCount + 1 Mod 15) = 0) Then

                Dim iMainRowBreak As New HtmlTableRow
                iMainRowBreak.Attributes.Add("class", "PageBreak")
                Dim iMainCell1Break1 As New HtmlTableCell
                Dim iMainCell1Break2 As New HtmlTableCell
                Dim iMainCell1Break3 As New HtmlTableCell
                Dim iMainCell1Break4 As New HtmlTableCell
                iMainCell1Break1.Width = "60%"
                iMainCell1Break2.Width = "8%"
                iMainCell1Break3.Width = "8%"
                iMainCell1Break4.Width = "24%"
                iMainCell1Break2.Align = "center"
                iMainCell1Break3.Align = "center"
                iMainRowBreak.Cells.Add(iMainCell1Break1)
                iMainRowBreak.Cells.Add(iMainCell1Break2)
                iMainRowBreak.Cells.Add(iMainCell1Break3)
                iMainRowBreak.Cells.Add(iMainCell1Break4)
                iMain.Rows.Add(iMainRowBreak)
            End If


            intSecId = dtCheck.Rows(IntCount)("Sec_id")
            intSubSecId = dtCheck.Rows(IntCount)("subSec_id")

            If (intSecId = dtCheck.Rows(IntCount)("Sec_id")) Then
                If (intSubSecId = 0) Then
                    If (boolSec) Then
                        Dim iMainRowr As New HtmlTableRow
                        Dim iMainCellr1 As New HtmlTableCell
                        iMainCellr1.Attributes.Add("class", "SubSecDocText")
                        Dim iMainCellr2 As New HtmlTableCell
                        Dim iMainCellr3 As New HtmlTableCell
                        Dim iMainCellr4 As New HtmlTableCell
                        iMainCellr1.Width = "60%"
                        iMainCellr2.Width = "8%"
                        iMainCellr2.Align = "center"
                        iMainCellr3.Width = "8%"
                        iMainCellr3.Align = "center"
                        iMainCellr4.Width = "24%"
                        intDocCount = intDocCount + 1
                        iMainCellr1.InnerHtml = (intSecIdCount - 1).ToString() + "." + intDocCount.ToString() + " " + dtCheck.Rows(IntCount)("Docname")
                        'iMainCell13.InnerHtml = "Telkomsel"
                        Dim iCheckNSN As New CheckBox
                        If (dtCheck.Rows(IntCount)("nsnapproved") = 0) Then
                            iCheckNSN.Checked = False
                        Else
                            iCheckNSN.Checked = True
                        End If
                        iCheckNSN.Enabled = False

                        Dim iCheckTELK As New CheckBox
                        If (dtCheck.Rows(IntCount)("customerapproved") = 0) Then
                            iCheckTELK.Checked = False
                            'iCheckTELK.Enabled = True
                        Else
                            iCheckTELK.Checked = True
                            'iCheckTELK.Enabled = True
                        End If
                        iCheckTELK.Enabled = False

                        iMainCellr2.Controls.Add(iCheckNSN)
                        iMainCellr3.Controls.Add(iCheckTELK)
                        iMainCellr4.InnerHtml = "&nbsp;"
                        iMainRowr.Cells.Add(iMainCellr1)
                        iMainRowr.Cells.Add(iMainCellr2)
                        iMainRowr.Cells.Add(iMainCellr3)
                        iMainRowr.Cells.Add(iMainCellr4)
                        iMain.Rows.Add(iMainRowr)

                    Else
                        Dim iMainRowr As New HtmlTableRow
                        Dim iMainCellr1 As New HtmlTableCell
                        iMainCellr1.Attributes.Add("class", "SectionText")
                        Dim iMainCellr2 As New HtmlTableCell
                        Dim iMainCellr3 As New HtmlTableCell
                        Dim iMainCellr4 As New HtmlTableCell
                        iMainCellr1.Width = "60%"
                        iMainCellr2.Width = "8%"
                        iMainCellr3.Width = "8%"
                        iMainCellr4.Width = "24%"
                        iMainCellr1.InnerHtml = intSecIdCount.ToString() + " " + dtCheck.Rows(IntCount)("Sec_name")
                        iMainCellr2.InnerHtml = "&nbsp;"
                        iMainCellr3.InnerHtml = "&nbsp;"
                        iMainCellr4.InnerHtml = "&nbsp;"
                        iMainRowr.Cells.Add(iMainCellr1)
                        iMainRowr.Cells.Add(iMainCellr2)
                        iMainRowr.Cells.Add(iMainCellr3)
                        iMainRowr.Cells.Add(iMainCellr4)
                        iMain.Rows.Add(iMainRowr)

                        Dim iMainRowr1 As New HtmlTableRow
                        Dim iMainCellr11 As New HtmlTableCell
                        iMainCellr11.Attributes.Add("class", "SubSecDocText")

                        Dim iMainCellr12 As New HtmlTableCell
                        Dim iMainCellr13 As New HtmlTableCell
                        Dim iMainCellr14 As New HtmlTableCell
                        iMainCellr11.Width = "60%"
                        iMainCellr12.Width = "8%"
                        iMainCellr12.Align = "center"
                        iMainCellr13.Width = "8%"
                        iMainCellr13.Align = "center"
                        iMainCellr14.Width = "24%"
                        intDocCount = 1
                        iMainCellr11.InnerHtml = intSecIdCount.ToString() + "." + intDocCount.ToString() + " " + dtCheck.Rows(IntCount)("Docname")
                        Dim iCheckNSN As New CheckBox
                        iCheckNSN.Checked = True
                        iCheckNSN.Enabled = False
                        Dim iCheckTELK As New CheckBox
                        iCheckTELK.Checked = True
                        iCheckTELK.Enabled = False
                        iMainCellr12.Controls.Add(iCheckNSN)
                        iMainCellr13.Controls.Add(iCheckTELK)

                        iMainCellr14.InnerHtml = "&nbsp;"
                        iMainRowr1.Cells.Add(iMainCellr11)
                        iMainRowr1.Cells.Add(iMainCellr12)
                        iMainRowr1.Cells.Add(iMainCellr13)
                        iMainRowr1.Cells.Add(iMainCellr14)
                        iMain.Rows.Add(iMainRowr1)
                        intSecIdCount = intSecIdCount + 1
                        boolSec = True
                    End If

                Else
                    If (intSubsecOldId = dtCheck.Rows(IntCount)("subSec_id")) Then
                        boolSubSec = True
                    Else
                        boolSubSec = False
                    End If
                    If (boolSubSec) Then
                        Dim iMainRowr As New HtmlTableRow
                        Dim iMainCellr1 As New HtmlTableCell
                        iMainCellr1.Attributes.Add("class", "DocText")
                        Dim iMainCellr2 As New HtmlTableCell
                        Dim iMainCellr3 As New HtmlTableCell
                        Dim iMainCellr4 As New HtmlTableCell
                        iMainCellr1.Width = "60%"
                        iMainCellr2.Width = "8%"
                        iMainCellr3.Width = "8%"
                        iMainCellr4.Width = "24%"
                        intDocCount = intDocCount + 1
                        iMainCellr2.Align = "center"
                        iMainCellr3.Align = "center"
                        Dim iCheckNSN As New CheckBox
                        If (dtCheck.Rows(IntCount)("nsnapproved") = 0) Then
                            iCheckNSN.Checked = False
                            'iCheckNSN.Enabled = True
                        Else
                            iCheckNSN.Checked = True
                            'iCheckNSN.Enabled = True
                        End If
                        iCheckNSN.Enabled = False
                        Dim iCheckTELK As New CheckBox
                        If (dtCheck.Rows(IntCount)("customerapproved") = 0) Then
                            iCheckTELK.Checked = False
                            'iCheckTELK.Enabled = True
                        Else
                            iCheckTELK.Checked = True
                            'iCheckTELK.Enabled = True
                        End If
                        iCheckTELK.Enabled = False

                        iMainCellr1.InnerHtml = (intSecIdCount - 1).ToString() + "." + (intSubSecIdCount - 1).ToString() + "." + intDocCount.ToString() + " " + dtCheck.Rows(IntCount)("Docname")
                        iMainCellr2.Controls.Add(iCheckNSN)
                        iMainCellr3.Controls.Add(iCheckTELK)
                        iMainCellr4.InnerHtml = "&nbsp;"
                        iMainRowr.Cells.Add(iMainCellr1)
                        iMainRowr.Cells.Add(iMainCellr2)
                        iMainRowr.Cells.Add(iMainCellr3)
                        iMainRowr.Cells.Add(iMainCellr4)
                        iMain.Rows.Add(iMainRowr)

                    Else
                        If (intsecOldId = dtCheck.Rows(IntCount)("Sec_id")) Then
                            boolSec = False
                        Else
                            boolSec = True
                        End If
                        If boolSec Then
                            Dim iMainRowr As New HtmlTableRow
                            Dim iMainCellr1 As New HtmlTableCell
                            iMainCellr1.Attributes.Add("class", "SectionText")
                            Dim iMainCellr2 As New HtmlTableCell
                            Dim iMainCellr3 As New HtmlTableCell
                            Dim iMainCellr4 As New HtmlTableCell
                            iMainCellr1.Width = "60%"
                            iMainCellr2.Width = "8%"
                            iMainCellr3.Width = "8%"
                            iMainCellr4.Width = "24%"
                            iMainCellr1.InnerHtml = intSecIdCount.ToString() + " " + dtCheck.Rows(IntCount)("Sec_name")
                            iMainCellr2.InnerHtml = "&nbsp;"
                            iMainCellr3.InnerHtml = "&nbsp;"
                            iMainCellr4.InnerHtml = "&nbsp;"
                            iMainRowr.Cells.Add(iMainCellr1)
                            iMainRowr.Cells.Add(iMainCellr2)
                            iMainRowr.Cells.Add(iMainCellr3)
                            iMainRowr.Cells.Add(iMainCellr4)
                            iMain.Rows.Add(iMainRowr)
                            intsecOldId = dtCheck.Rows(IntCount)("Sec_id")
                            intSecIdCount = intSecIdCount + 1
                            intSubSecIdCount = 1

                        End If


                        Dim iMainRowr2 As New HtmlTableRow
                        Dim iMainCellr21 As New HtmlTableCell
                        iMainCellr21.Attributes.Add("class", "SubSectionText")

                        Dim iMainCellr22 As New HtmlTableCell
                        Dim iMainCellr23 As New HtmlTableCell
                        Dim iMainCellr24 As New HtmlTableCell
                        iMainCellr21.InnerHtml = (intSecIdCount - 1).ToString() + "." + intSubSecIdCount.ToString() + " " + dtCheck.Rows(IntCount)("subSec_name")
                        iMainCellr22.InnerHtml = "&nbsp;"
                        iMainCellr23.InnerHtml = "&nbsp;"
                        iMainCellr24.InnerHtml = "&nbsp;"
                        iMainRowr2.Cells.Add(iMainCellr21)
                        iMainRowr2.Cells.Add(iMainCellr22)
                        iMainRowr2.Cells.Add(iMainCellr23)
                        iMainRowr2.Cells.Add(iMainCellr24)
                        iMain.Rows.Add(iMainRowr2)
                        intSubsecOldId = dtCheck.Rows(IntCount)("subSec_id")

                        intSubSecIdCount = intSubSecIdCount + 1
                        Dim iMainRowr1 As New HtmlTableRow
                        Dim iMainCellr11 As New HtmlTableCell
                        iMainCellr11.Attributes.Add("class", "DocText")

                        Dim iMainCellr12 As New HtmlTableCell
                        Dim iMainCellr13 As New HtmlTableCell
                        Dim iMainCellr14 As New HtmlTableCell
                        iMainCellr12.Align = "center"
                        iMainCellr13.Align = "center"
                        intDocCount = 1
                        Dim iCheckNSN As New CheckBox
                        If (dtCheck.Rows(IntCount)("nsnapproved") = 0) Then
                            iCheckNSN.Checked = False
                            'iCheckNSN.Enabled = True
                        Else
                            iCheckNSN.Checked = True
                            'iCheckNSN.Enabled = True
                        End If
                        iCheckNSN.Enabled = False
                        Dim iCheckTELK As New CheckBox
                        If (dtCheck.Rows(IntCount)("customerapproved") = 0) Then
                            iCheckTELK.Checked = False
                            'iCheckTELK.Enabled = True
                        Else
                            iCheckTELK.Checked = True
                            'iCheckTELK.Enabled = True
                        End If
                        iCheckTELK.Enabled = False
                        iMainCellr11.InnerHtml = (intSecIdCount - 1).ToString() + "." + (intSubSecIdCount - 1).ToString() + "." + intDocCount.ToString() + " " + dtCheck.Rows(IntCount)("Docname")
                        iMainCellr12.Controls.Add(iCheckNSN)
                        iMainCellr13.Controls.Add(iCheckTELK)
                        iMainCellr14.InnerHtml = "&nbsp;"
                        iMainRowr1.Cells.Add(iMainCellr11)
                        iMainRowr1.Cells.Add(iMainCellr12)
                        iMainRowr1.Cells.Add(iMainCellr13)
                        iMainRowr1.Cells.Add(iMainCellr14)
                        iMain.Rows.Add(iMainRowr1)
                        boolSubSec = True
                        'intDocCount = intDocCount + 1
                    End If

                End If


            Else

                Dim iMainRowr As New HtmlTableRow
                Dim iMainCellr1 As New HtmlTableCell
                iMainCellr1.Attributes.Add("class", "SectionText")
                Dim iMainCellr2 As New HtmlTableCell
                Dim iMainCellr3 As New HtmlTableCell
                Dim iMainCellr4 As New HtmlTableCell
                iMainCellr1.Width = "60%"
                iMainCellr2.Width = "8%"
                iMainCellr3.Width = "8%"
                iMainCellr4.Width = "24%"
                iMainCellr1.InnerHtml = intSecIdCount.ToString() + " " + dtCheck.Rows(IntCount)("Sec_name")
                iMainCellr2.InnerHtml = "&nbsp;"
                iMainCellr3.InnerHtml = "&nbsp;"
                iMainCellr4.InnerHtml = "&nbsp;"
                iMainRowr.Cells.Add(iMainCellr1)
                iMainRowr.Cells.Add(iMainCellr2)
                iMainRowr.Cells.Add(iMainCellr3)
                iMainRowr.Cells.Add(iMainCellr4)
                iMain.Rows.Add(iMainRowr)
                intSecIdCount = intSecIdCount + 1
            End If


        Next

        CheckList.Controls.Add(iMain)

    End Sub

    Protected Sub DDPoDetails_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDPoDetails.SelectedIndexChanged
        ' objdl.fillDDL(DDSite, "POSiteNoDoc", DDPoDetails.SelectedValue, True, Constants._DDL_Default_Select)
        Dim dt As New DataTable
        Dim objd As New DBUtil
        dt = objd.ExeQueryDT("SELECT DISTINCT convert(varchar(10),B.Site_ID)+ '-'+ convert(varchar(20),a.PO_Id) AS [VAL], A.SiteNo + '-'+ A.FldType AS [txt] FROM   PODetails  AS A   " & _
                     "INNER JOIN CODSite AS B ON A.SiteNo = B.Site_No Inner Join SiteDoc S on S.SiteId=B.Site_Id WHERE A.RStatus = 2 AND A.PoNo = '" & DDPoDetails.SelectedItem.Text & "'", "chklist")
        DDSite.DataSource = dt
        DDSite.DataTextField = "txt"
        DDSite.DataValueField = "val"
        DDSite.DataBind()
        DDSite.Items.Insert(0, "--Select--")
    End Sub

    Protected Sub DDSite_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDSite.SelectedIndexChanged
        If (DDSite.SelectedValue <> "0") Then
            BindSiteDetails()
            CreatecheckList()
            dvPrint.Style.Add("display", "")
        End If
      
    End Sub
    Sub BindSiteDetails()
        Dim dtCheck As New DataTable
        Dim stval() As String = DDSite.SelectedValue.Split("-")
        'dtCheck = objBO.CheckListSiteDetails(DDSite.SelectedValue)
        dtCheck = objBO.CheckListSiteDetails(stval(0))
        lblSiteName.InnerHtml = dtCheck.Rows(0)("site_name")
        lblSiteNo.InnerHtml = DDSite.SelectedItem.Text 'dtCheck.Rows(0)("site_no")
        lblPONo.InnerHtml = dtCheck.Rows(0)("pono")
        lblArea.InnerHtml = dtCheck.Rows(0)("ara_name")
    End Sub
End Class
