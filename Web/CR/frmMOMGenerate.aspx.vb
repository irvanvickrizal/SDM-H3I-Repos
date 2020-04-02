Imports Entities
Imports BusinessLogic
Imports Common
Imports System.Data
Imports System.IO
Partial Class CR_frmMOMGenerate
    Inherits System.Web.UI.Page
    Dim objBo As New BOMOM
    Dim objbom As New BOMailReport
    Dim dt As New DataTable
    Dim dtn As New DataTable
    Dim objdb As New DBUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            If Request.QueryString("id") <> Nothing Then
                BindDetails(Request.QueryString("id"))
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScriptdd", "getControlPosition();", True)
            End If
            ' BindDetails(10)
        End If
        BtnGenerate.Attributes.Add("onclick", "return getControlPosition();")
    End Sub
    Sub BindDetails(ByVal MOMId As Integer)
        Dim dtHead As DataTable = objBo.uspBindMomHead(MOMId)
        If dtHead.Rows.Count >= 1 Then
            For intCount As Integer = 0 To dtHead.Rows.Count - 1
                'tdMoMid.InnerHtml = "<font class=lblBBindText>Organizer Tel. : </font><font class=lblText>" & dtHead.Rows(0)("MOMRefNO") & "</font>"
                tdModerator.InnerHtml = "<font class=lblBBindText>Moderator : </font><font class=lblText>" & dtHead.Rows(0)("moderator") & "</font>"
                tdMOMWriter.InnerHtml = "<font class=lblBBindText>MOM Writer : </font><font class=lblText>" & dtHead.Rows(0)("MOMWriter") & "</font>"
                tdTime.InnerHtml = "<font class=lblBBindText>Form - To (Hrs) : </font><font class=lblText>" & dtHead.Rows(0)("times") & "</font>"
                tdDate.InnerHtml = "<font class=lblBBindText>Date : </font><font class=lblText>" & dtHead.Rows(0)("momdate") & "</font>"
                tdLocation.InnerHtml = "<font class=lblBBindText>Location :</font><font class=lblText>" & dtHead.Rows(0)("Location") & "</font>"
                tdSubject.InnerHtml = "<div class=lblBBindText>Subject</div><div class=lblText>" & dtHead.Rows(0)("subject") & "</div>"
            Next
        End If
        Dim dtUser As New DataTable
        dtuser = objBo.uspBindAttendes(MOMId)
        If dtUser.Rows.Count >= 1 Then
            HDDgSignTotal.Value = dtUser.Rows.Count
            dtList.DataSource = dtUser
            dtList.DataBind()
        End If
        Dim dtMOMDetails As DataTable = objBo.uspBindMOMAllDetails(MOMId)
        If dtMOMDetails.Rows.Count >= 1 Then
            Dim ihtmlMain As New HtmlTable
            ihtmlMain.Width = "100%"
            For intCount As Integer = 0 To dtMOMDetails.Rows.Count - 1
                Dim ihtmlRow1 As New HtmlTableRow
                Dim ihtmlcel1 As New HtmlTableCell
                ihtmlcel1.Attributes.Add("class", "lblBBindText")
                Dim sno As Integer = intCount
                ihtmlcel1.InnerHtml = (sno + 1).ToString & ". " & dtMOMDetails.Rows(intCount)("subject").ToString()
                ihtmlRow1.Cells.Add(ihtmlcel1)
                ihtmlMain.Rows.Add(ihtmlRow1)
                Dim ihtmlRow As New HtmlTableRow
                Dim ihtmlcel As New HtmlTableCell
                ihtmlcel.Width = "100%"
                ihtmlcel.Align = "left"
                ihtmlcel.Controls.Add(BindMOMDetails(MOMId, dtMOMDetails.Rows(intCount)("pono"), dtMOMDetails.Rows(intCount)("oldsiteno"), dtMOMDetails.Rows(intCount)("fldtype")))
                ihtmlRow.Cells.Add(ihtmlcel)
                ihtmlMain.Rows.Add(ihtmlRow)
                Dim dtMOMOther As DataTable = objBo.uspMOMOtherGenerate(MOMId, dtMOMDetails.Rows(intCount)("pono"), dtMOMDetails.Rows(intCount)("oldsiteno"), dtMOMDetails.Rows(intCount)("fldtype"))

                If (dtMOMOther.Rows.Count > 0) Then
                    Dim intCount1 As Integer = 0
                    'Dim iMainRowT1 As New HtmlTableRow
                    Dim iMainHeadingOther As New HtmlTable
                    iMainHeadingOther.Align = "left"
                    ' Bind heading
                    Dim iHeadingPOther As New Panel
                    iHeadingPOther.GroupingText = "Others Changes"
                    'iHeadingP1.Height = "250"
                    For intCount1 = 0 To dtMOMOther.Rows.Count - 1

                        Dim iHeadingROther As New HtmlTableRow
                        Dim iHeadingCOther As New HtmlTableCell
                        iHeadingCOther.InnerHtml = dtMOMOther.Rows(intCount1)("Input").ToString
                        iHeadingCOther.Attributes.Add("class", "lblBindText")
                        iHeadingROther.Cells.Add(iHeadingCOther)
                        iMainHeadingOther.Rows.Add(iHeadingROther)


                    Next

                    iHeadingPOther.Controls.Add(iMainHeadingOther)
                    Dim iHeadingROther1 As New HtmlTableRow
                    Dim iHeadingCOther1 As New HtmlTableCell
                    iHeadingCOther1.Controls.Add(iHeadingPOther)
                    iHeadingROther1.Cells.Add(iHeadingCOther1)
                    ihtmlMain.Rows.Add(iHeadingROther1)

                End If
            Next
            tdBindMomDetails.Controls.Add(ihtmlMain)
        End If

    End Sub
    Function BindMOMDetails(ByVal MOMId As Integer, ByVal PoNo As String, ByVal Siteid As String, ByVal Scope As String) As HtmlTable
        Dim iMainTable As New HtmlTable
        iMainTable.Width = "100%"
        iMainTable.CellPadding = 0
        iMainTable.Border = 0
        iMainTable.CellSpacing = 0
        Dim iMainRow As New HtmlTableRow

        Dim iMainRowT As New HtmlTableRow
        Dim iMainHeading As New HtmlTableCell
        iMainHeading.Align = "left"
        ' Bind heading
        Dim iHeadingP As New Panel
        'iHeadingP.GroupingText = "Site Details"
        iMainHeading.Width = "20%"

        Dim iHeadingT As New HtmlTable
        Dim iHeadingR1 As New HtmlTableRow
        Dim iHeadingC1 As New HtmlTableCell
        iHeadingC1.InnerHtml = "<br>Po"
        iHeadingC1.Attributes.Add("class", "lblBBindText")
        iHeadingR1.Cells.Add(iHeadingC1)
        iHeadingT.Rows.Add(iHeadingR1)

        Dim iHeadingR20 As New HtmlTableRow
        Dim iHeadingC20 As New HtmlTableCell
        iHeadingC20.InnerHtml = "PoName"
        iHeadingC20.Attributes.Add("class", "lblBBindText")
        iHeadingR20.Cells.Add(iHeadingC20)
        iHeadingT.Rows.Add(iHeadingR20)

        Dim iHeadingR21 As New HtmlTableRow
        Dim iHeadingC21 As New HtmlTableCell
        iHeadingC21.InnerHtml = "Po Type"
        iHeadingC21.Attributes.Add("class", "lblBBindText")
        iHeadingR21.Cells.Add(iHeadingC21)
        iHeadingT.Rows.Add(iHeadingR21)

        Dim iHeadingR22 As New HtmlTableRow
        Dim iHeadingC22 As New HtmlTableCell
        iHeadingC22.InnerHtml = "Vendor"
        iHeadingC22.Attributes.Add("class", "lblBBindText")
        iHeadingR22.Cells.Add(iHeadingC22)

        Dim iHeadingR2 As New HtmlTableRow
        Dim iHeadingC2 As New HtmlTableCell
        iHeadingC2.InnerHtml = "Site Id"
        iHeadingC2.Attributes.Add("class", "lblBBindText")
        iHeadingR2.Cells.Add(iHeadingC2)
        iHeadingT.Rows.Add(iHeadingR2)

        Dim iHeadingR3 As New HtmlTableRow
        Dim iHeadingC3 As New HtmlTableCell
        iHeadingC3.InnerHtml = "Site Name"
        iHeadingC3.Attributes.Add("class", "lblBBindText")
        iHeadingR3.Cells.Add(iHeadingC3)
        iHeadingT.Rows.Add(iHeadingR3)

        Dim iHeadingR4 As New HtmlTableRow
        Dim iHeadingC4 As New HtmlTableCell
        iHeadingC4.InnerHtml = "WorkPackage ID"
        iHeadingC4.Attributes.Add("class", "lblBBindText")
        iHeadingR4.Cells.Add(iHeadingC4)
        iHeadingT.Rows.Add(iHeadingR4)

        Dim iHeadingR5 As New HtmlTableRow
        Dim iHeadingC5 As New HtmlTableCell
        iHeadingC5.InnerHtml = "WorkPackage Name"
        iHeadingC5.Attributes.Add("class", "lblBBindText")
        iHeadingR5.Cells.Add(iHeadingC5)
        iHeadingT.Rows.Add(iHeadingR5)

        Dim iHeadingR6 As New HtmlTableRow
        Dim iHeadingC6 As New HtmlTableCell
        iHeadingC6.InnerHtml = "Scope"
        iHeadingC6.Attributes.Add("class", "lblBBindText")
        iHeadingR6.Cells.Add(iHeadingC6)
        iHeadingT.Rows.Add(iHeadingR6)

        Dim iHeadingR7 As New HtmlTableRow
        Dim iHeadingC7 As New HtmlTableCell
        iHeadingC7.InnerHtml = "Description"
        iHeadingC7.Attributes.Add("class", "lblBBindText")
        iHeadingR7.Cells.Add(iHeadingC7)
        iHeadingT.Rows.Add(iHeadingR7)

        Dim iHeadingR8 As New HtmlTableRow
        Dim iHeadingC8 As New HtmlTableCell
        iHeadingC8.InnerHtml = "Band Type"
        iHeadingC8.Attributes.Add("class", "lblBBindText")
        iHeadingR8.Cells.Add(iHeadingC8)
        iHeadingT.Rows.Add(iHeadingR8)

        Dim iHeadingR9 As New HtmlTableRow
        Dim iHeadingC9 As New HtmlTableCell
        iHeadingC9.InnerHtml = "Band"
        iHeadingC9.Attributes.Add("class", "lblBBindText")
        iHeadingR9.Cells.Add(iHeadingC9)
        iHeadingT.Rows.Add(iHeadingR9)

        Dim iHeadingR10 As New HtmlTableRow
        Dim iHeadingC10 As New HtmlTableCell
        iHeadingC10.InnerHtml = "Configuration"
        iHeadingC10.Attributes.Add("class", "lblBBindText")
        iHeadingR10.Cells.Add(iHeadingC10)
        iHeadingT.Rows.Add(iHeadingR10)

        Dim iHeadingR11 As New HtmlTableRow
        Dim iHeadingC11 As New HtmlTableCell
        iHeadingC11.InnerHtml = "Purchase 900"
        iHeadingC11.Attributes.Add("class", "lblBBindText")
        iHeadingR11.Cells.Add(iHeadingC11)
        iHeadingT.Rows.Add(iHeadingR11)

        Dim iHeadingR12 As New HtmlTableRow
        Dim iHeadingC12 As New HtmlTableCell
        iHeadingC12.InnerHtml = "Purchase 1800"
        iHeadingC12.Attributes.Add("class", "lblBBindText")
        iHeadingR12.Cells.Add(iHeadingC12)
        iHeadingT.Rows.Add(iHeadingR12)

        Dim iHeadingR13 As New HtmlTableRow
        Dim iHeadingC13 As New HtmlTableCell
        iHeadingC13.InnerHtml = "Hard Ware"
        iHeadingC13.Attributes.Add("class", "lblBBindText")
        iHeadingR13.Cells.Add(iHeadingC13)
        iHeadingT.Rows.Add(iHeadingR13)

        Dim iHeadingR14 As New HtmlTableRow
        Dim iHeadingC14 As New HtmlTableCell
        iHeadingC14.InnerHtml = "HardWare Code"
        iHeadingC14.Attributes.Add("class", "lblBBindText")
        iHeadingR14.Cells.Add(iHeadingC14)
        iHeadingT.Rows.Add(iHeadingR14)

        Dim iHeadingR15 As New HtmlTableRow
        Dim iHeadingC15 As New HtmlTableCell
        iHeadingC15.InnerHtml = "Quantity"
        iHeadingC15.Attributes.Add("class", "lblBBindText")
        iHeadingR15.Cells.Add(iHeadingC15)
        iHeadingT.Rows.Add(iHeadingR15)

        Dim iHeadingR16 As New HtmlTableRow
        Dim iHeadingC16 As New HtmlTableCell
        iHeadingC16.InnerHtml = "Antenna Name"
        iHeadingC16.Attributes.Add("class", "lblBBindText")
        iHeadingR16.Cells.Add(iHeadingC16)
        iHeadingT.Rows.Add(iHeadingR16)

        Dim iHeadingR17 As New HtmlTableRow
        Dim iHeadingC17 As New HtmlTableCell
        iHeadingC17.InnerHtml = "Antenna Quantity"
        iHeadingC17.Attributes.Add("class", "lblBBindText")
        iHeadingR17.Cells.Add(iHeadingC17)
        iHeadingT.Rows.Add(iHeadingR17)

        Dim iHeadingR24 As New HtmlTableRow
        Dim iHeadingC24 As New HtmlTableCell
        iHeadingC24.InnerHtml = "Feeder Length"
        iHeadingC24.Attributes.Add("class", "lblBBindText")
        iHeadingR24.Cells.Add(iHeadingC24)
        iHeadingT.Rows.Add(iHeadingR24)

        Dim iHeadingR18 As New HtmlTableRow
        Dim iHeadingC18 As New HtmlTableCell
        iHeadingC18.InnerHtml = "Feeder Type"
        iHeadingC18.Attributes.Add("class", "lblBBindText")
        iHeadingR18.Cells.Add(iHeadingC18)
        iHeadingT.Rows.Add(iHeadingR18)

        Dim iHeadingR19 As New HtmlTableRow
        Dim iHeadingC19 As New HtmlTableCell
        iHeadingC19.InnerHtml = "Feeder Quantity"
        iHeadingC19.Attributes.Add("class", "lblBBindText")
        iHeadingR19.Cells.Add(iHeadingC19)
        iHeadingT.Rows.Add(iHeadingR19)


        iHeadingT.Rows.Add(iHeadingR22)

        Dim iHeadingR23 As New HtmlTableRow
        Dim iHeadingC23 As New HtmlTableCell
        iHeadingC23.InnerHtml = "Remarks"
        iHeadingC23.Attributes.Add("class", "lblBBindText")
        iHeadingR23.Cells.Add(iHeadingC23)
        iHeadingT.Rows.Add(iHeadingR23)

        iHeadingP.Controls.Add(iHeadingT)

        iMainHeading.Controls.Add(iHeadingP)
        iMainRowT.Cells.Add(iMainHeading)
        iMainTable.Rows.Add(iMainRowT)


        Dim dtMOM As DataTable = objBo.uspBindMOMDetails(MOMId, PoNo, Siteid, Scope)

        If (dtMOM.Rows.Count > 0) Then

            Dim intCount As Integer = 0

            For intCount = 0 To dtMOM.Rows.Count - 1

                'Dim iMainRowT1 As New HtmlTableRow
                Dim iMainHeading1 As New HtmlTableCell
                iMainHeading1.Align = "left"
                ' Bind heading
                Dim iHeadingP1 As New Panel
                iHeadingP1.GroupingText = dtMOM.Rows(intCount)("sta").ToString
                'iHeadingP1.Height = "250"
                Dim iHeadingT01 As New HtmlTable
                Dim iHeadingR01 As New HtmlTableRow
                Dim iHeadingC01 As New HtmlTableCell
                iHeadingC01.InnerHtml = dtMOM.Rows(intCount)("pono").ToString
                iHeadingC01.Attributes.Add("class", "lblBindText")
                iHeadingR01.Cells.Add(iHeadingC01)
                iHeadingT01.Rows.Add(iHeadingR01)

                Dim iHeadingR020 As New HtmlTableRow
                Dim iHeadingC020 As New HtmlTableCell
                iHeadingC020.InnerHtml = dtMOM.Rows(intCount)("poname").ToString
                iHeadingC020.Attributes.Add("class", "lblBindText")
                iHeadingR020.Cells.Add(iHeadingC020)
                iHeadingT01.Rows.Add(iHeadingR020)

                Dim iHeadingR021 As New HtmlTableRow
                Dim iHeadingC021 As New HtmlTableCell
                iHeadingC021.InnerHtml = dtMOM.Rows(intCount)("potype").ToString
                iHeadingC021.Attributes.Add("class", "lblBindText")
                iHeadingR021.Cells.Add(iHeadingC021)
                iHeadingT01.Rows.Add(iHeadingR021)

                Dim iHeadingR022 As New HtmlTableRow
                Dim iHeadingC022 As New HtmlTableCell
                iHeadingC022.InnerHtml = dtMOM.Rows(intCount)("vendor").ToString
                iHeadingC022.Attributes.Add("class", "lblBindText")
                iHeadingR022.Cells.Add(iHeadingC022)
                iHeadingT01.Rows.Add(iHeadingR022)

                Dim iHeadingR02 As New HtmlTableRow
                Dim iHeadingC02 As New HtmlTableCell
                iHeadingC02.InnerHtml = dtMOM.Rows(intCount)("siteno").ToString
                iHeadingC02.Attributes.Add("class", "lblBindText")
                iHeadingR02.Cells.Add(iHeadingC02)
                iHeadingT01.Rows.Add(iHeadingR02)

                Dim iHeadingR03 As New HtmlTableRow
                Dim iHeadingC03 As New HtmlTableCell
                iHeadingC03.InnerHtml = dtMOM.Rows(intCount)("sitename").ToString
                iHeadingC03.Attributes.Add("class", "lblBindText")
                iHeadingR03.Cells.Add(iHeadingC03)
                iHeadingT01.Rows.Add(iHeadingR03)

                Dim iHeadingR04 As New HtmlTableRow
                Dim iHeadingC04 As New HtmlTableCell
                iHeadingC04.InnerHtml = dtMOM.Rows(intCount)("WorkPkgId").ToString
                iHeadingC04.Attributes.Add("class", "lblBindText")
                iHeadingR04.Cells.Add(iHeadingC04)
                iHeadingT01.Rows.Add(iHeadingR04)

                Dim iHeadingR05 As New HtmlTableRow
                Dim iHeadingC05 As New HtmlTableCell
                iHeadingC05.InnerHtml = dtMOM.Rows(intCount)("workPKGName").ToString
                iHeadingC05.Attributes.Add("class", "lblBindText")
                iHeadingR05.Cells.Add(iHeadingC05)
                iHeadingT01.Rows.Add(iHeadingR05)

                Dim iHeadingR06 As New HtmlTableRow
                Dim iHeadingC06 As New HtmlTableCell
                iHeadingC06.InnerHtml = dtMOM.Rows(intCount)("scope").ToString
                iHeadingC06.Attributes.Add("class", "lblBindText")
                iHeadingR06.Cells.Add(iHeadingC06)
                iHeadingT01.Rows.Add(iHeadingR06)

                Dim iHeadingR07 As New HtmlTableRow
                Dim iHeadingC07 As New HtmlTableCell
                iHeadingC07.InnerHtml = dtMOM.Rows(intCount)("Description").ToString
                iHeadingC07.Attributes.Add("class", "lblBindText")
                iHeadingR07.Cells.Add(iHeadingC07)
                iHeadingT01.Rows.Add(iHeadingR07)

                Dim iHeadingR08 As New HtmlTableRow
                Dim iHeadingC08 As New HtmlTableCell
                iHeadingC08.InnerHtml = dtMOM.Rows(intCount)("Band_Type").ToString
                iHeadingC08.Attributes.Add("class", "lblBindText")
                iHeadingR08.Cells.Add(iHeadingC08)
                iHeadingT01.Rows.Add(iHeadingR08)

                Dim iHeadingR09 As New HtmlTableRow
                Dim iHeadingC09 As New HtmlTableCell
                iHeadingC09.InnerHtml = dtMOM.Rows(intCount)("Band").ToString
                iHeadingC09.Attributes.Add("class", "lblBindText")
                iHeadingR09.Cells.Add(iHeadingC09)
                iHeadingT01.Rows.Add(iHeadingR09)

                Dim iHeadingR010 As New HtmlTableRow
                Dim iHeadingC010 As New HtmlTableCell
                iHeadingC010.InnerHtml = dtMOM.Rows(intCount)("Config").ToString
                iHeadingC010.Attributes.Add("class", "lblBindText")
                iHeadingR010.Cells.Add(iHeadingC010)
                iHeadingT01.Rows.Add(iHeadingR010)

                Dim iHeadingR011 As New HtmlTableRow
                Dim iHeadingC011 As New HtmlTableCell
                iHeadingC011.InnerHtml = dtMOM.Rows(intCount)("Purchase900").ToString
                iHeadingC011.Attributes.Add("class", "lblBindText")
                iHeadingR011.Cells.Add(iHeadingC011)
                iHeadingT01.Rows.Add(iHeadingR011)

                Dim iHeadingR012 As New HtmlTableRow
                Dim iHeadingC012 As New HtmlTableCell
                iHeadingC012.InnerHtml = dtMOM.Rows(intCount)("Purchase1800").ToString
                iHeadingC012.Attributes.Add("class", "lblBindText")
                iHeadingR012.Cells.Add(iHeadingC012)
                iHeadingT01.Rows.Add(iHeadingR012)

                Dim iHeadingR013 As New HtmlTableRow
                Dim iHeadingC013 As New HtmlTableCell
                iHeadingC013.InnerHtml = dtMOM.Rows(intCount)("BSSHW").ToString
                iHeadingC013.Attributes.Add("class", "lblBindText")
                iHeadingR013.Cells.Add(iHeadingC013)
                iHeadingT01.Rows.Add(iHeadingR013)

                Dim iHeadingR014 As New HtmlTableRow
                Dim iHeadingC014 As New HtmlTableCell
                iHeadingC014.InnerHtml = dtMOM.Rows(intCount)("BSSCode").ToString
                iHeadingC014.Attributes.Add("class", "lblBindText")
                iHeadingR014.Cells.Add(iHeadingC014)
                iHeadingT01.Rows.Add(iHeadingR014)

                Dim iHeadingR015 As New HtmlTableRow
                Dim iHeadingC015 As New HtmlTableCell
                iHeadingC015.InnerHtml = dtMOM.Rows(intCount)("qty").ToString
                iHeadingC015.Attributes.Add("class", "lblBindText")
                iHeadingR015.Cells.Add(iHeadingC015)
                iHeadingT01.Rows.Add(iHeadingR015)

                Dim iHeadingR016 As New HtmlTableRow
                Dim iHeadingC016 As New HtmlTableCell
                iHeadingC016.InnerHtml = dtMOM.Rows(intCount)("AntennaName").ToString
                iHeadingC016.Attributes.Add("class", "lblBindText")
                iHeadingR016.Cells.Add(iHeadingC016)
                iHeadingT01.Rows.Add(iHeadingR016)

                Dim iHeadingR017 As New HtmlTableRow
                Dim iHeadingC017 As New HtmlTableCell
                iHeadingC017.InnerHtml = dtMOM.Rows(intCount)("AntennaQty").ToString
                iHeadingC017.Attributes.Add("class", "lblBindText")
                iHeadingR017.Cells.Add(iHeadingC017)
                iHeadingT01.Rows.Add(iHeadingR017)

                Dim iHeadingR024 As New HtmlTableRow
                Dim iHeadingC024 As New HtmlTableCell
                iHeadingC024.InnerHtml = dtMOM.Rows(intCount)("FeederLen").ToString
                iHeadingC024.Attributes.Add("class", "lblBindText")
                iHeadingR024.Cells.Add(iHeadingC024)
                iHeadingT01.Rows.Add(iHeadingR024)

                Dim iHeadingR018 As New HtmlTableRow
                Dim iHeadingC018 As New HtmlTableCell
                iHeadingC018.InnerHtml = dtMOM.Rows(intCount)("FeederType").ToString
                iHeadingC018.Attributes.Add("class", "lblBindText")
                iHeadingR018.Cells.Add(iHeadingC018)
                iHeadingT01.Rows.Add(iHeadingR018)

                Dim iHeadingR019 As New HtmlTableRow
                Dim iHeadingC019 As New HtmlTableCell
                iHeadingC019.InnerHtml = dtMOM.Rows(intCount)("FeederQty").ToString
                iHeadingC019.Attributes.Add("class", "lblBindText")
                iHeadingR019.Cells.Add(iHeadingC019)
                iHeadingT01.Rows.Add(iHeadingR019)



                Dim iHeadingR023 As New HtmlTableRow
                Dim iHeadingC023 As New HtmlTableCell
                iHeadingC023.InnerHtml = dtMOM.Rows(intCount)("remarks").ToString
                iHeadingC023.Attributes.Add("class", "lblBindText")
                iHeadingR023.Cells.Add(iHeadingC023)
                iHeadingT01.Rows.Add(iHeadingR023)

                iHeadingP1.Controls.Add(iHeadingT01)

                iMainHeading1.Controls.Add(iHeadingP1)
                iMainRowT.Cells.Add(iMainHeading1)
                iMainTable.Rows.Add(iMainRowT)
            Next
        End If
        Return iMainTable
    End Function

    Protected Sub BtnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGenerate.Click


        For intCount As Integer = 0 To dtList.Items.Count - 1
            Dim ihdUserid As HiddenField = CType(dtList.Items(intCount).FindControl("HdUserid"), HiddenField)
            Dim iHDX As HiddenField = CType(dtList.Items(intCount).FindControl("hdXCoordinate"), HiddenField)
            Dim iHDY As HiddenField = CType(dtList.Items(intCount).FindControl("hdYCoordinate"), HiddenField)
            Dim iname As HiddenField = CType(dtList.Items(intCount).FindControl("hdnusrname"), HiddenField)
            Dim iemail As HiddenField = CType(dtList.Items(intCount).FindControl("hdnemail"), HiddenField)
            Dim XCo As Integer, YCo As Integer, PageNO As Integer
            XCo = (iHDX.Value - 166)

            YCo = (791 - iHDY.Value) + (5 + (intCount * 25))

            PageNO = (Math.Ceiling(iHDY.Value / 791))
            If PageNO = 0 Then PageNO = 1
            objBo.uspMomParticipantsUNew(Request.QueryString("id"), ihdUserid.Value, XCo, YCo, PageNO)
            sendmommail(iname.Value, iemail.Value)
        Next
        Dim Path As String = ConfigurationManager.AppSettings("Fpath") & ConfigurationManager.AppSettings("MOMPath")
        Dim dtUser As Integer = objdb.ExeQueryScalar("Exec uspGenerateMOM " & Request.QueryString("id") & ",'" & ConfigurationManager.AppSettings("MOMPath") & CreatePDFFile(Path) & "','" & CommonSite.UserName() & "'")

        'Dim dtUser As Integer = objBo.uspBindMOMGenerate(Request.QueryString("id"), ConfigurationManager.AppSettings("MOMPath") & CreatePDFFile(Path), CommonSite.UserName())

        ' Dim dtUser As Integer = objBo.uspBindMOMGenerate(Request.QueryString("id"), "MOM\ddd.pdf")
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('MOM has been generated');", True)
        Response.Redirect("frmMOMList.aspx")
    End Sub
    Function CreatePDFFile(ByVal StrPath As String) As String
        StrPath = StrPath.Replace(" \", "\").ToString 'bugfix100816 causing error when the path containing space before \
        BindDetails(Request.QueryString("id"))
        Dim dtba As New DataTable
        Dim filenameorg As String
        Dim ReFileName As String

        filenameorg = "MOM-" & Format(CDate(DateTime.Now), "ddMMyyyyHHss")
        ReFileName = filenameorg & ".htm"
        'path = ConfigurationManager.AppSettings("Fpath") & sitename & ft & secpath
        If (System.IO.File.Exists(StrPath & ReFileName)) Then
            System.IO.File.Delete(StrPath & ReFileName)
        End If
        If Not System.IO.Directory.Exists(StrPath) Then
            System.IO.Directory.CreateDirectory(StrPath)
        End If
        Dim sw As HtmlTextWriter = New HtmlTextWriter(New StreamWriter(StrPath & ReFileName, False, System.Text.UnicodeEncoding.UTF8))
        'sw.WriteLine("<html><head><link href=""http://localhost:2821/E-Bast/css/Styles.css"" rel=""stylesheet"" type=""text/css"" /></head>")
        sw.WriteLine("<html><head><style type=""text/css"">")
        sw.WriteLine("tr { padding: 3px; }")
        sw.WriteLine(".MainCSS { margin-bottom:0px;margin-left:20px;margin-right:20px;margin-top:0px;width: 850px; height: 700px; text-align: center; }")
        sw.WriteLine(".lblText { font-family: verdana; font-size: 8pt; color: #000000; text-align: left; }")
        sw.WriteLine(".lblBText { font-family: verdana; font-size: 8pt; color: #000000; text-align: left; font-weight: bold; }")
        sw.WriteLine(".lblBBindText { font-family: verdana;font-size: 8pt;color: #000000;text-align: left;font-weight: bold;height:20px;}")
        sw.WriteLine(".lblBindText { font-family: verdana;font-size: 8pt;color: #000000;text-align: left;height:20px;}")
        sw.WriteLine(".lblBold { font-family: verdana; font-size: 12pt; color: #000000; font-weight: bold; }")
        sw.WriteLine(".textFieldStyle { background-color: white; border: 1px solid; color: black; font-family: verdana; font-size: 9pt; }")
        sw.WriteLine(".GridHeader { color: #0e1b42; background-color: Orange; font-size: 9pt; font-family: Verdana; text-align: Left; vertical-align: bottom; font-weight: bold; }")
        sw.WriteLine(".GridEvenRows { background-color: #e3e3e3; vertical-align: top; font-family: verdana; font-size: 8pt; color: #000000; }")
        sw.WriteLine(".GridOddRows { background-color: white; vertical-align: top; font-family: verdana; font-size: 8pt; color: #000000; }")
        sw.WriteLine(".PagerTitle { font-size: 8pt; background-color: #cddbbf; text-align: Right; vertical-align: middle; color: #25375b; font-weight: bold; }")
        sw.WriteLine(".Hcap { height: 10px; }")
        sw.WriteLine(".VCap { width: 10px; }")
        sw.WriteLine(".title1 { font-family: Arial; font-weight: bold; font-size: 10pt; }")
        sw.WriteLine(".newStyle1 { font-family: Arial; font-weight: bold; font-size: small; color: #0000FF; font-style: italic; }")
        sw.WriteLine("</style></head>")
        sw.WriteLine("<body calss=""MainCSS"">")
        dvPrint.RenderControl(sw)
        '  ihtmlMain.RenderControl(sw)
        sw.WriteLine("</body>")
        sw.WriteLine("</html>")
        sw.Close()
        sw.Dispose()
        Return Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(StrPath & ReFileName, StrPath, filenameorg)
    End Function
    Protected Sub btnChangeRequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangeRequest.Click
        Response.Redirect("frmMOMList.aspx")
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub
    Sub sendmommail(ByVal pname As String, ByVal email As String)
        dt = objBOM.uspMailReportLD(2, )  ''this is fro document upload time sending mail
        Dim Remail As String = "'"
        Dim name As String = ""
        Remail = email
        name = pname
        Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
        Dim receiverAdd As String = Remail
        Dim mySMTPClient As New System.Net.Mail.SmtpClient
        Dim myEmail As New System.Net.Mail.MailMessage
        myEmail.BodyEncoding() = System.Text.Encoding.UTF8
        myEmail.SubjectEncoding = System.Text.Encoding.UTF8
        myEmail.To.Add(receiverAdd)
        myEmail.Subject = dt.Rows(0).Item(3).ToString  ''subject from table
        myEmail.Body = dt.Rows(0).Item(2).ToString & name & ",<br> <br>" ''salutatation from table
        myEmail.Body = myEmail.Body & " Process - " & ConfigurationManager.AppSettings("Type").Replace("\", "") & " <br> <br> "
        myEmail.Body = myEmail.Body & dt.Rows(0).Item(4).ToString & " <br> <br> "
        myEmail.Body = myEmail.Body & "<br/>" & dt.Rows(0).Item(5).ToString   ''closing
        myEmail.IsBodyHtml = True
        myEmail.From = New System.Net.Mail.MailAddress(senderAdd, "NSN")
        mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
        Try
            mySMTPClient.Send(myEmail)
        Catch ex As Exception
            objdb.ExeNonQuery("exec uspErrLog '', 'mommail','" & ex.Message.ToString() & "','mommailsending'")
        End Try

    End Sub

    Protected Sub dtList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dtList.ItemDataBound

    End Sub
End Class
