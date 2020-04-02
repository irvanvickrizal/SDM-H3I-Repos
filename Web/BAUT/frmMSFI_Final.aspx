<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMSFI_Final.aspx.vb" Inherits="BAUT_frmMSFI_Final" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MSFI</title>
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function getControlPosition() {
            var Total = document.getElementById('HDDgSignTotal').value;
            for (intCount1 = 0; intCount1 < Total; intCount1++) {
                var divctrl = document.getElementById('DLDigitalSign_ctl0' + intCount1 + '_ImgPostion');
                eval("document.getElementById('DLDigitalSign_ctl0" + intCount1 + "_hdXCoordinate')").value = findPosX(divctrl);
                eval("document.getElementById('DLDigitalSign_ctl0" + intCount1 + "_hdYCoordinate')").value = findPosY(divctrl);
                //alert(findPosX(divctrl) + " , " + findPosY(divctrl));
            }
        }

        function findPosX(imgElem) {
            xPos = eval(imgElem).offsetLeft;
            tempEl = eval(imgElem).offsetParent;
            while (tempEl != null) {
                xPos += tempEl.offsetLeft;
                tempEl = tempEl.offsetParent;
            }
            return xPos;
        }

        function findPosY(imgElem) {
            yPos = eval(imgElem).offsetTop;
            tempEl = eval(imgElem).offsetParent;
            while (tempEl != null) {
                yPos += tempEl.offsetTop;
                tempEl = tempEl.offsetParent;
            }
            return yPos;
        }

        function Showmain(type) {
            if (type == 'Int') {
                alert('Integration date not available');
            }
            if (type == 'IntD') {
                alert('The document cannot be uploaded before the integration date');
            }
            if (type == '2sta') {
                alert('This Document already processed for second stage so cannot upload again ');
            }
            if (type == 'nop') {
                alert('No permission to upload this Document ');
            }
            if (type == 'nyreg') {
                alert('This CAC NY Registered in Document on Parallel MSFI');
            }
            if (type == "successuploaded") {
                alert('Document Successfully Generated');
            }
            window.location = '../PO/frmSiteDocUploadTree.aspx'
        }
        function showErrRejection(type, docname) {
            if (type == 'rmkem') {
                alert('Please fill reason of first');
            }
        }
        function readOnlyCheckBox() {
            return false;
        }
    </script>

    <style type="text/css">
        tr {
            padding: 3px;
        }

        .MainCSS {
            margin-bottom: 0px;
            margin-left: 10px;
            margin-top: 0px;
            width: 800px;
        }

        .lblText {
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
            text-align: left;
        }

        .lblFieldHeader {
            font-family: verdana;
            font-size: 10px;
            color: #000000;
            text-align: center;
            font-weight: bold;
        }

        .lblFieldText {
            font-family: verdana;
            font-size: 9px;
            color: #000000;
            text-align: left;
        }

        .lblFieldBoldText {
            font-family: verdana;
            font-size: 9px;
            color: #000000;
            text-align: left;
            font-weight: bolder;
        }

        .lblTextItalic {
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
            text-align: left;
            font-style: italic;
        }

        .lblBText {
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
            text-align: left;
            font-weight: bold;
        }

        .lblRef {
            font-family: verdana;
            font-size: 11px;
            color: #000000;
            text-align: left;
            font-weight: bold;
        }

        .lblBold {
            font-family: verdana;
            font-size: 18px;
            color: #000000;
            font-weight: bold;
        }

        .textFieldStyle {
            background-color: white;
            border: 1px solid;
            color: black;
            font-family: verdana;
            font-size: 9pt;
        }

        .PagerTitle {
            font-size: 8pt;
            background-color: #cddbbf;
            text-align: Right;
            vertical-align: middle;
            color: #25375b;
            font-weight: bold;
        }

        .Hcap {
            height: 5px;
        }

        .VCap {
            width: 10px;
        }

        .checkDocumentPanel {
            margin-left: 40px;
        }
    </style>

    <style type="text/css">
        .lblTextSmall {
            font-family: verdana;
            font-size: 9px;
            color: #000;
        }

        .siteATTPanel {
            margin-top: 10px;
            height: 70px;
        }

        .SiteDetailInfoPanel {
            margin-top: 10px;
            width: 100%;
            text-align: left;
            height: 150px;
        }

        .sitedescription {
            margin-top: 10px;
            width: 800px;
            height: 60px;
        }

        .headerform {
            margin-top: 15px;
            height: 60px;
        }

        .pnlremarks {
            height: 60px;
        }

        .whitespace {
            height: 5px;
        }

        .pnlNote {
            height: 200px;
        }

        .footerPanel {
            margin-top: 10px;
            height: 250px;
        }

        .btnstyle {
            font-family: Verdana;
            font-size: 10px;
            font-weight: bolder;
            border-style: solid;
            background-color: Green;
            border-width: 2px;
            border-color: Black;
            color: White;
            cursor: pointer;
            padding: 5px;
        }

        .btnstyleEdit {
            font-family: Verdana;
            font-size: 10px;
            font-weight: bolder;
            border-style: solid;
            background-color: maroon;
            border-width: 2px;
            border-color: Black;
            color: White;
            cursor: pointer;
            padding: 5px;
        }

        #dvPrint {
            width: 800px;
            height: 950px;
            overflow: hidden;
        }
         #dvPrint2 {
            width: 800px;
            height: 950px;
            overflow: hidden;
        }

        .PageBreak {
            page-break-before: always;            
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnpono" runat="server" />
        <asp:HiddenField ID="hdnsiteid" runat="server" />
        <asp:HiddenField ID="hdnsiteno" runat="server" />
        <asp:HiddenField ID="hdnVersion" runat="server" />
        <asp:HiddenField ID="hdnwfid" runat="server" />
        <asp:HiddenField ID="hdnDGBox" runat="server" />
        <asp:HiddenField ID="hdndocid" runat="server" />
        <asp:HiddenField ID="hdnKeyVal" runat="server" />
        <asp:HiddenField ID="hdnScope" runat="server" />
        <asp:HiddenField ID="hdnPageNo" runat="server" />
        <div id="dvPrint" runat="server">
            <div class="headerform">
                <table cellpadding="0" cellspacing="0" width="97%" border="0">
                    <tr>
                        <td align="left" valign="top" style="width: 15%">
                            <img src="https://sdmthree.nsnebast.com/Images/NOKIA.png" height="46" width="104"
                                alt="nsnLogo" />
                        </td>
                        <td colspan="4" align="center" class="lblBold" valign="top" style="width: 70%">Master Site File Index<br />
                        </td>
                        <td align="right" valign="top" style="width: 15%">
                            <img src="https://sdmthree.nsnebast.com/Images/three-logo.png" height="46" width="60"
                                alt="hcptlogo" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="siteATTPanel">
                <table valign="top" style="width: 100%;">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr class="lblText">
                                    <td>Project Name</td>
                                    <td>:</td>
                                    <td>&nbsp;<asp:Label ID="lblProjectName" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                                <tr class="lblText">
                                    <td style="width: 100px;">Doc Ref No
                                    </td>
                                    <td>:</td>
                                    <td>&nbsp;<asp:Label ID="lblDocRefNo" runat="server" CssClass="lblText"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="lblText">
                                    <td>Service Type</td>
                                    <td>:</td>
                                    <td>&nbsp;<asp:Label ID="lblServiceType" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>


                            </table>
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr class="lblText">
                                    <td>Site ID
                                    </td>
                                    <td>:</td>
                                    <td>&nbsp;<asp:Label ID="lblSiteID" runat="server" CssClass="lblText"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="lblText">
                                    <td>Site Name
                                    </td>
                                    <td>:</td>
                                    <td>&nbsp;<asp:Label ID="lblSiteName" runat="server" CssClass="lblText"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="lblText">
                                    <td></td>
                                    <td></td>
                                    <td>&nbsp;<asp:Label ID="Label1" runat="server" CssClass="lblText"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </div>
            <div class="pnlNote">
                <asp:Literal ID="ltrMSFIList" runat="server">
                </asp:Literal>
                <asp:GridView ID="gvMSFIList" runat="server" AutoGenerateColumns="false" Width="100%" BorderColor="Black" BorderWidth="1px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="sn" HeaderText="SN" ItemStyle-Width="20px" ItemStyle-CssClass="lblTextSmall" ControlStyle-Font-Bold="false" />
                        <asp:BoundField DataField="Docname" HeaderText="Site Documentation Package" ItemStyle-CssClass="lblTextSmall" ItemStyle-Width="300px" />
                        <asp:TemplateField HeaderText="Category" ItemStyle-CssClass="lblTextSmall" ItemStyle-Width="180px">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCopy" runat="server" Text="Copy" ItemStyle-CssClass="lblTextSmall" />
                                <asp:CheckBox ID="chkOriginal" runat="server" Text="Original" ItemStyle-CssClass="lblTextSmall" />
                                <asp:CheckBox ID="chkNA" runat="server" Text="NA" ItemStyle-CssClass="lblTextSmall" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="lblText" Width="98%"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div id="dvPrint2" runat="server">
            <div class="headerform">
                <br />
                <table cellpadding="0" cellspacing="0" width="97%" border="0">
                    <tr>
                        <td align="left" valign="top" style="width: 15%">
                            <img src="https://sdmthree.nsnebast.com/Images/NOKIA.png" height="46" width="104"
                                alt="nsnLogo" />
                        </td>
                        <td colspan="4" align="center" class="lblBold" valign="top" style="width: 70%">Master Site File Index<br />
                        </td>
                        <td align="right" valign="top" style="width: 15%">
                            <img src="https://sdmthree.nsnebast.com/Images/three-logo.png" height="46" width="60"
                                alt="hcptlogo" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="siteATTPanel">
                <table valign="top" style="width: 100%;">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr class="lblText">
                                    <td>Project Name</td>
                                    <td>:</td>
                                    <td>&nbsp;<asp:Label ID="lblProjectName2" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                                <tr class="lblText">
                                    <td style="width: 100px;">Doc Ref No
                                    </td>
                                    <td>:</td>
                                    <td>&nbsp;<asp:Label ID="lblDocRefNo2" runat="server" CssClass="lblText"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="lblText">
                                    <td>Service Type</td>
                                    <td>:</td>
                                    <td>&nbsp;<asp:Label ID="lblSvcType2" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>


                            </table>
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr class="lblText">
                                    <td>Site ID
                                    </td>
                                    <td>:</td>
                                    <td>&nbsp;<asp:Label ID="lblSiteID2" runat="server" CssClass="lblText"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="lblText">
                                    <td>Site Name
                                    </td>
                                    <td>:</td>
                                    <td>&nbsp;<asp:Label ID="lblSitename2" runat="server" CssClass="lblText"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="lblText">
                                    <td></td>
                                    <td></td>
                                    <td>&nbsp;<asp:Label ID="Label7" runat="server" CssClass="lblText"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </div>
            <div class="pnlNote">
                <asp:Literal ID="ltrMSFIList2" runat="server">
                </asp:Literal>
                <asp:GridView ID="gvMSFIList2" runat="server" AutoGenerateColumns="false" Width="100%" BorderColor="Black" BorderWidth="1px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="sn" HeaderText="SN" ItemStyle-Width="20px" ItemStyle-CssClass="lblTextSmall" ControlStyle-Font-Bold="false" />
                        <asp:BoundField DataField="Docname" HeaderText="Site Documentation Package" ItemStyle-CssClass="lblTextSmall" ItemStyle-Width="300px" />
                        <asp:TemplateField HeaderText="Category" ItemStyle-CssClass="lblTextSmall" ItemStyle-Width="180px">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCopy" runat="server" Text="Copy" ItemStyle-CssClass="lblTextSmall" />
                                <asp:CheckBox ID="chkOriginal" runat="server" Text="Original" ItemStyle-CssClass="lblTextSmall" />
                                <asp:CheckBox ID="chkNA" runat="server" Text="NA" ItemStyle-CssClass="lblTextSmall" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="lblText" Width="98%"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div id="pnlButton" runat="server" style="width: 800px; text-align: center;">
            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnstyleEdit" />
            <asp:Button ID="btnGenerate" runat="server" Text="Submit" CssClass="btnstyle"
                Width="120px" OnClientClick="return confirm('Are you sure you want to Submit?')" />
        </div>
    </form>
</body>
</html>
