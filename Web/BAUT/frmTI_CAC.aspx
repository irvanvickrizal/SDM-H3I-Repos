<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTI_CAC.aspx.vb" Inherits="BAUT_frmTI_CAC" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Provision Acceptance Certificate</title>
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
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
            font-size: 10px;
            color: #000;
        }

        .siteATTPanel {
            margin-top: 10px;
            height: 140px;
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
            height: 80px;
        }

        .pnlNote2 {
            height: 20px;
        }

        .footerPanel {
            margin-top: 10px;
            height: 350px;
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

        #dvPrint {
            width: 800px;
            height: 950px;
        }
    </style>

    <script type="text/javascript">
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
            if (type == 'nscope') {
                alert('Master Scope not defined, please go to Management PO List to define Master Scope');
            }
            if (type == 'nwork') {
                alert('TypeofWork not defined, please go to Management PO List to define TypeofWork');
            }
            if (type == 'napodate') {
                alert('PO Date Not yet available');
            }
            if (type == 'naprojectid') {
                alert('Project ID is Empty, Please fill Project ID Field in PO Management');
            }
            //window.location = '../HCPT_Dashboard/frmRFTReadyCreation.aspx';
			window.location = '../PO/frmSiteDocUploadTree.aspx'
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnsiteid" runat="server" />
        <asp:HiddenField ID="hdnsiteno" runat="server" />
        <asp:HiddenField ID="hdnVersion" runat="server" />
        <asp:HiddenField ID="hdnwfid" runat="server" />
        <asp:HiddenField ID="hdnDGBox" runat="server" />
        <asp:HiddenField ID="hdndocid" runat="server" />
        <asp:HiddenField ID="hdnKeyVal" runat="server" />
        <asp:HiddenField ID="hdnScope" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="dvPrint" runat="server">
            <div class="headerform">
                <table cellpadding="0" cellspacing="0" width="97%" border="0">
                    <tr>
                        <td align="left" valign="top" style="width: 15%">
                            <img src="https://sdmthree.nsnebast.com/Images/NOKIA.png" height="46px" width="104px"
                                alt="nsnLogo" />
                        </td>
                        <td colspan="4" align="center" class="lblBold" valign="top" style="width: 70%">
                            CONDITIONAL ACCEPTANCE CERTIFICATE<br />
                            <asp:Label ID="LblRFTNo" runat="server" CssClass="lblRef"></asp:Label>
                        </td>
                        <td align="right" valign="top" style="width: 15%">
                            <img src="https://sdmthree.nsnebast.com/Images/three-logo.png" height="46px" width="60px"
                                alt="hcptlogo" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="siteATTPanel">
                <table cellpadding="0" cellspacing="0">
					<tr id="pnlPOType" runat="server" class="lblText">
                        <td>
                            <span class="lblText">PO Type</span>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="DdlPOType" runat="server" AutoPostBack="true" CssClass="lblText">
                                <asp:ListItem Text="EQP & Service" Value="eqpandsvc"></asp:ListItem>
                                <asp:ListItem Text="EQP Only" Value="eqp"></asp:ListItem>
                                <asp:ListItem Text="SVC Only" Value="svc"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>
                            Type of Works</td>
                        <td>
                            :</td>
                        <td>
                            &nbsp;<asp:Label ID="LblTypeofWork" runat="server" CssClass="lblText"></asp:Label></td>
                    </tr>
                    <tr class="lblText">
                        <td style="width: 100px;">
                            Date of Creation
                        </td>
                        <td>
                            :</td>
                        <td>
                            &nbsp;<asp:Label ID="LblDateofCreation" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>
                            Contractor Name</td>
                        <td>
                            :</td>
                        <td>
                            &nbsp;<asp:Label ID="LblContractorName" runat="server" CssClass="lblText"></asp:Label></td>
                    </tr>
                    <tr class="lblText">
                        <td>
                            Site ID
                        </td>
                        <td>
                            :</td>
                        <td>
                            &nbsp;<asp:Label ID="lblSiteID" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>
                            Site Name
                        </td>
                        <td>
                            :</td>
                        <td>
                            &nbsp;<asp:Label ID="lblSiteName" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>
                            PO Number
                        </td>
                        <td>
                            :</td>
                        <td>
                            &nbsp;<asp:Label ID="LblPono" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="sitedescription">
                <div class="lblText">
                    The contractor hereby certifies that relevant parts of the Works referred to in this Provisional Acceptance Certificate complies with the 2013 Network Expansion Procurement dated 02 December 2013 (the <i><b>2013 NEPA</b></i>), in particular
                    the specification and this has successfully passed the Acceptance Teas and Aceptance Criteria(as defined
                    in Annex E of 2013 NEPA) on the Site with the following remarks:
                </div>
            </div>
            <div class="pnlremarks">
                <table>
                    <tr class="lblText" valign="top">
                        <td>
                            <input type="checkbox" id="ChkPassed" runat="server" />
                        </td>
                        <td>
                            Passed Without any Punch List or outstanding Documentation
                        </td>
                    </tr>
                    <tr class="lblText" valign="top">
                        <td>
                            <input type="checkbox" id="ChkNoPassed" runat="server" />
                        </td>
                        <td>
                            Passed with a Punch List of Cosmetic and Minor Defect(s) whereby the defect(s) have
                            been classified in accordance with Annex E (Acceptance) of the 2013 NEPA"
                        </td>
                    </tr>
                </table>
                <hr />
            </div>
            <div class="whitespace">
            </div>
            <div class="pnlNote">
                <span class="lblTextItalic">Note:</span>
                <ul>
                    <li><span class="lblTextItalic">The Punch List shall be cleared and outstanding Documenttion shall be supplied to the Customer with the time period(s) stipulated in the Punch
                        List</span></li>
                    <li><span class="lblTextItalic">Conditional Acceptance Punch List attached</span></li>
                </ul>
            </div>
            <div class="whitespace" style="clear: both;">
            </div>
            <div class="pnlNote2">
                <span class="lblText">The Required Documentation is attached with this certificate and
                    a detailed check list of the bill of materials </span>
            </div>
            <div class="footerPanel">
                <div id="signaturePanel_Top" style="text-align: left; width: 100%; margin-top: 10px;">
                    <asp:DataList ID="DLDigitalSign_NSNOnly" runat="server" Width="100%" RepeatColumns="2">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" width="250px" border="0">
                                <tr>
                                    <td class="lblBText" style="text-align: left;">
                                        For and behalf of<br />
                                        <%#Eval("CompanyName")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="DgSign" runat="server" style="height: 100px; text-align: left;">
                                        <asp:ImageButton ID="ImgPostion" runat="server" ImageUrl="http://www.telkomsel.nsnebast.com/Images/dgsign.JPG" Visible="false" />
                                        <asp:HiddenField runat="Server" ID="hdXCoordinate" />
                                        <asp:HiddenField runat="Server" ID="hdYCoordinate" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblBText" style="border-width: 1px; border-color: Black; border-collapse: collapse;
                                        border-style: solid; vertical-align: top; text-align: center;">
                                        <%#Eval("Fullname")%>
                                        <br />
                                        <%#Eval("SignTitle")%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div style="margin-top: 5px; text-align: left;">
                    <span class="lblBText" style="text-decoration: underline;">Counter signatures</span>
                </div>
                <div id="SignaturePanel_Bottom" style="text-align: left; width: 100%; margin-top: 10px;">
                    <asp:DataList ID="DdlDigitalSignature_Customer" runat="server" Width="100%" RepeatColumns="2">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" width="250px" border="0">
                                <tr>
                                    <td class="lblBText">
                                        For and behalf of<br />
                                        <%#Eval("CompanyName")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="DgSign" runat="server" style="height: 100px; text-align: left;">
                                        <asp:ImageButton ID="ImgPostion" runat="server" ImageUrl="http://www.telkomsel.nsnebast.com/Images/dgsign.JPG" Visible="false" />
                                        <asp:HiddenField runat="Server" ID="hdXCoordinate" />
                                        <asp:HiddenField runat="Server" ID="hdYCoordinate" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblBText" style="border-width: 1px; border-color: Black; border-collapse: collapse;
                                        border-style: solid; vertical-align: top; text-align: center;">
                                        <%#Eval("Fullname")%>
                                        <br />
                                        <%#Eval("SignTitle")%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
        <div id="documentPanel" style="margin-top: 15px;">
            <table width="100%">
                <tr>
                    <td class="lblText">
                        <asp:GridView ID="grdDocuments" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnSelectedIndexChanged="DoThis" EmptyDataText="No Records Found" DataKeyNames="Doc_Id"
                            PageSize="2">
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total ">
                                    <ItemStyle HorizontalAlign="Right" Width="30px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblno" runat="Server"></asp:Label>
                                        <asp:Label ID="LblDocName" runat="server" Text='<%#Eval("DocName") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="LblSWId" runat="server" Text='<%#Eval("SW_Id") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="LblDocid" runat="server" Text='<%#Eval("doc_id") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="doc_id" HeaderText="doc" />
                                <asp:BoundField DataField="docpath" HeaderText="Path" />
                                <asp:BoundField DataField="DocName" HeaderText="Document" ControlStyle-Width="100px">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SW_Id" />
                                <asp:TemplateField ControlStyle-Width="180px">
                                    <ItemTemplate>
                                        <asp:RadioButtonList ID="rdbstatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DOThis"
                                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0">
                                                            Approve
                                            </asp:ListItem>
                                            <asp:ListItem Value="1">
                                                
                                                            Reject
                                                            
                                            </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="MultiLine" Rows="5" Columns="40" ID="txtremarks" runat="server"
                                            Visible="false" CssClass="textFieldStyle">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr class="lblText">
                    <td align="center">
                        <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="buttonStyle"
                            Width="120px" OnClientClick="return CheckEmptyMandatory();" />
                        <asp:Button ID="btnSubmitReject" runat="server" Text="Submit" CssClass="buttonStyle"
                            Width="120px" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
