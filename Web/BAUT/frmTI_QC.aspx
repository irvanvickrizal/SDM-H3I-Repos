<%@ Page Language="VB" AutoEventWireup="true" CodeFile="frmTI_QC.aspx.vb" Inherits="BAUT_frmTI_QC"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>QC</title>
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>

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
                alert('This KPI NY Registered in Document on Parallel BAUT');
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

        .lblBText {
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
            text-align: left;
            font-weight: bold;
        }

        .lblRef {
            font-family: verdana;
            font-size: 10pt;
            color: #000000;
            text-align: left;
            font-weight: bold;
        }

        .lblBold {
            font-family: verdana;
            font-size: 20px;
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

        .GridHeader {
            color: #0e1b42;
            background-color: Orange;
            font-size: 9pt;
            font-family: Verdana;
            text-align: Left;
            vertical-align: bottom;
            font-weight: bold;
        }

        .GridEvenRows {
            background-color: #e3e3e3;
            vertical-align: top;
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
        }

        .GridOddRows {
            background-color: white;
            vertical-align: top;
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
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

        .GrdDocPanelRows {
            font-family: verdana;
            font-size: 8pt;
        }

        a:link {
            font-family: verdana;
            font-size: 8pt;
        }

        a:visited {
            font-family: verdana;
            font-size: 8pt;
        }

        a:active {
            font-family: verdana;
            font-size: 8pt;
        }
    </style>

    <style type="text/css">
        .lblTextSmall {
            font-family: verdana;
            font-size: 10px;
            color: #000;
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
            height:30px;
        }

        .headerform {
            margin-top: 15px;
            height: 60px;
        }
        .siteATTPanel{
            margin-top:10px;  
            height:120px;
        }

        .HeaderGrid {
            font-family: Verdana;
            font-size: 12px;
            font-weight: bold;
            color: white;
            background-color: Orange;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            vertical-align: middle;
            padding-top: 5px;
            padding-bottom: 5px;
        }

        .oddGrid {
            font-family: Verdana;
            font-size: 9px;
            background-color: White;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }

        .evenGrid {
            font-family: Verdana;
            font-size: 9px;
            background-color: #cfcfcf;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }

        .emptyrow {
            font-family: Verdana;
            font-size: 10px;
            color: Red;
            font-style: italic;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            padding: 5px;
        }

        .footerGrid {
            font-family: Verdana;
            font-size: 9px;
            color: white;
            font-style: italic;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            padding: 5px;
        }

        .HeaderGrid2 {
            font-family: Verdana;
            font-size: 11px;
            font-weight: bold;
            color: #000;
            background-color: #ffffff;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            vertical-align: middle;
            padding-top: 5px;
            padding-bottom: 5px;
        }

        .oddGrid2 {
            font-family: Verdana;
            font-size: 10px;
            background-color: White;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }

        .evenGrid2 {
            font-family: Verdana;
            font-size: 10px;
            background-color: White;
            border-color: black;
            border-style: solid;
            border-width: 1px;
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
        .footerPanel
        {
            margin-top: 10px; height: 350px;text-align:left;
        }
		.pnlremarks
		{
			margin-top: 5px; 
			font-family:verdana; 
			font-size:7.5pt;
		}
    </style>

    <script type="text/javascript">
        function CheckEmptyMandatory() {
            var onAirDate = document.getElementById("TxtOnAirDate");
            var integrationDate = document.getElementById("TxtIntDate");
            var sitename = document.getElementById("lblSiteName");
            var typeofwork = document.getElementById("txtTWork");
            var bandtype = document.getElementById("lblBand");
            var config = document.getElementById("TxtExtConfig");
            var onConfig = document.getElementById("txtOnAirCon");

            if (integrationDate.value.length < 1) {
                alert("Please define integration date first!");
                return false;
            }

            if (onAirDate.value.length < 1) {
                alert("Please define on air date first!");
                return false;
            }

            if (sitename.value.length < 1) {
                alert("Please insert sitename first!");
                return false;
            }

            if (typeofwork.value.length < 1) {
                alert("Please input type of work first!");
                return false;
            }

            if (bandtype.value.length < 1) {
                alert("Please input band first!");
                return false;
            }

            if (config.value.length < 1) {
                alert("Please input existing config first!");
                return false;
            }

            if (onConfig.value.length < 1) {
                alert("Please input on air config first!");
                return false;
            }

        }
        function ConfirmationBox(desc) {
            var result = confirm('Are you sure you want to delete Sector ' + desc + ' ?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

</head>
<body class="MainCSS">
    <form id="form1" runat="server">
        <input type="hidden" runat="server" id="hdnRole" />
        <input type="hidden" runat="server" id="HDDgSignTotal" />
        <input type="hidden" runat="server" id="hdnDGBox" />
        <input type="hidden" runat="server" id="hdnready4baut" />
        <input type="hidden" runat="server" id="hdnKeyVal" />
        <input type="hidden" runat="server" id="hdnScope" />
        <input runat="Server" id="hdndocid" type="hidden" />
        <input runat="Server" id="hdnWfId" type="hidden" />
        <input runat="Server" id="hdnversion" type="hidden" />
        <input runat="Server" id="hdnsiteid" type="hidden" />
        <input runat="Server" id="hdnAdminRole" type="hidden" />
        <input runat="Server" id="hdnAdmin" type="hidden" />
        <input runat="Server" id="hdnSiteno" type="hidden" />
        <input id="hdSno" runat="server" type="hidden" value="0" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="dvPrint" runat="server" style="width: 800px; height: 800px;">
            <div class="headerform" style="margin-top: 15px;">
                <table cellpadding="0" cellspacing="0" width="97%" border="0">
                    <tr>
                        <td align="left" valign="top" style="width: 20%">
                            <img src="https://sdmthree.nsnebast.com/Images/NOKIA.png" height="46px" width="104px"
                                alt="nsnLogo" />
                        </td>
                        <td colspan="4" align="center" class="lblBold" valign="top" style="width: 60%">4G <asp:label runat="server" ID="lblTitle"/> KPI REPORT<br />
                            ACCEPTANCE
                        </td>
                        <td align="right" valign="top" style="width: 20%">
                            <%--Modified by Fauzan, 7 Nov 2018. Add margin to move 3 logo position--%>
                            <img src="https://sdmthree.nsnebast.com/Images/three-logo.png" height="46px" width="60px" alt="hcptlogo" style="margin-right:20px" /> 
                        </td>
                    </tr>
                </table>
            </div>
            <div class="siteATTPanel">
                <table cellpadding="0" cellspacing="0">
                    <tr class="lblText">
                        <td style="width: 100px;">Date of Creation
                        </td>
                        <td>:</td>
                        <td>&nbsp;<asp:Label ID="LblDateofCreation" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>

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
                        <td>BTS Type</td>
                        <td>:</td>
                        <td>&nbsp;<asp:Label ID="LblBTSType" runat="server" CssClass="lblText"></asp:Label></td>
                    </tr>
                    <tr class="lblText">
                        <td>Region</td>
                        <td>:</td>
                        <td>&nbsp;<asp:Label ID="LblRegion" runat="server" CssClass="lblText"></asp:Label></td>
                    </tr>
                    <tr class="lblText">
                        <td>PO Number
                        </td>
                        <td>:</td>
                        <td>&nbsp;<asp:Label ID="LblPono" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="sitedescription">
                <div class="lblText">
                    <asp:label runat="server" ID="lblSiteDesc"/> <br/> The required Documentation is attached with this acceptance.
                </div>
            </div>
            
            <div class="SiteDetailInfoPanel">
				<div style="display:none">
                <asp:MultiView ID="MvDetailCore" runat="server">
                    <asp:View ID="VwList" runat="server">
                        <div id="SiteDetailInfoTables" runat="server"></div>
                        <div style="width: 100%; margin-top: 3px; text-align: center;">
                            <asp:Button ID="BtnEdit" runat="server" Text="Edit" CssClass="btnstyle" />
                        </div>
                    </asp:View>
                    <asp:View ID="VwEdit" runat="server">
                        <div>
                            <asp:GridView ID="GvSiteDetails" DataKeyNames="Detail_Id" runat="server" AutoGenerateColumns="false"
                                HeaderStyle-CssClass="HeaderGrid" ShowFooter="true" HeaderStyle-Font-Bold="true"
                                OnRowCancelingEdit="GvSiteDetails_RowCancelingEdit" OnRowDeleting="GvSiteDetails_RowDeleting"
                                OnRowEditing="GvSiteDetails_RowEditing" OnRowUpdating="GvSiteDetails_RowUpdating"
                                OnRowCommand="GvSiteDetails_RowCommand">
                                <RowStyle CssClass="oddGrid" />
                                <AlternatingRowStyle CssClass="evenGrid" />
                                <FooterStyle CssClass="footerGrid" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="false" HeaderStyle-BackColor="white" ItemStyle-VerticalAlign="Top"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/gridview/update.jpg"
                                                ToolTip="Update" Height="16px" Width="16px" />
                                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/gridview/Cancel.jpg"
                                                ToolTip="Cancel" Height="16px" Width="16px" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/Edit-icon.png"
                                                ToolTip="Edit" Height="16px" Width="16px" />
                                            <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Images/trash.png"
                                                ToolTip="Delete" Height="16px" Width="16px" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div style="height: 40px; vertical-align: top; text-align: center;">
                                                <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/gridview/AddNewitem.jpg"
                                                    CommandName="AddNew" Width="25px" Height="25px" ToolTip="Add new User" ValidationGroup="validation2" />
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sector" HeaderStyle-ForeColor="black" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Right">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSector" runat="server" Text='<%#Eval("Sector") %>' CssClass="lblTextSmall"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSector" runat="server" Text='<%#Eval("Sector") %>' CssClass="lblFieldSmall" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div>
                                                <asp:TextBox ID="txtftSector" runat="server" Width="80px" CssClass="lblTextSmall" Height="14px" />
                                                <asp:RequiredFieldValidator ID="rfvftSector" runat="server" ControlToValidate="txtftSector"
                                                    Text="*" ValidationGroup="validation2" />
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cell ID" HeaderStyle-ForeColor="black" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Right">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCellID" runat="server" Text='<%#Eval("Cell_ID")%>'
                                                CssClass="lblTextSmall" Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCellID" runat="server" Text='<%#Eval("Cell_ID")%>' CssClass="lblFieldSmall" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div>
                                                <asp:TextBox ID="txtftCellID" runat="server" Width="80px" CssClass="lblTextSmall" Height="14px" />
                                                <asp:RequiredFieldValidator ID="rfvftCellID" runat="server" ControlToValidate="txtftCellID"
                                                    Text="*" ValidationGroup="validation2" />
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Antenna Type" HeaderStyle-ForeColor="black" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtAntennaType" runat="server" Text='<%#Eval("Antenna_Type")%>'
                                                CssClass="lblTextSmall" Width="100px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAntennaType" runat="server" Text='<%#Eval("Antenna_Type")%>' CssClass="lblFieldSmall" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div>
                                                <asp:TextBox ID="txtftAntennaType" runat="server" Width="100px" CssClass="lblTextSmall" Height="14px" />
                                                <asp:RequiredFieldValidator ID="rfvftAntennaType" runat="server" ControlToValidate="txtftAntennaType"
                                                    Text="*" ValidationGroup="validation2" />
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Antenna Height(M)" HeaderStyle-ForeColor="black" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtAntennaHeight" runat="server" Text='<%#Eval("Antenna_Height")%>'
                                                CssClass="lblTextSmall" Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAntennaHeight" runat="server" Text='<%#Eval("Antenna_Height")%>' CssClass="lblFieldSmall" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div>
                                                <asp:TextBox ID="txtftAntennaHeight" runat="server" Width="50px" CssClass="lblTextSmall" Height="14px" />
                                                <asp:RequiredFieldValidator ID="rfvftAntennaHeight" runat="server" ControlToValidate="txtftAntennaHeight"
                                                    Text="*" ValidationGroup="validation2" />
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Azimuth" HeaderStyle-ForeColor="black" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtAzimuth" runat="server" Text='<%#Eval("Azimuth")%>'
                                                CssClass="lblTextSmall" Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAzimuth" runat="server" Text='<%#Eval("Azimuth")%>' CssClass="lblFieldSmall" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div>
                                                <asp:TextBox ID="txtftAzimuth" runat="server" Width="50px" CssClass="lblTextSmall" Height="14px" />
                                                <asp:RequiredFieldValidator ID="rfvftAzimuth" runat="server" ControlToValidate="txtftAzimuth"
                                                    Text="*" ValidationGroup="validation2" />
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mech Tilt" HeaderStyle-ForeColor="black" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtMechTilt" runat="server" Text='<%#Eval("Mech_Tilt")%>'
                                                CssClass="lblTextSmall" Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMechTilt" runat="server" Text='<%#Eval("Mech_Tilt")%>' CssClass="lblFieldSmall" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div>
                                                <asp:TextBox ID="txtftMechTilt" runat="server" Width="50px" CssClass="lblTextSmall" Height="14px" />
                                                <asp:RequiredFieldValidator ID="rfvftMechTilt" runat="server" ControlToValidate="txtftMechTilt"
                                                    Text="*" ValidationGroup="validation2" />
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Elec Tilt" HeaderStyle-ForeColor="black" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtElecTilt" runat="server" Text='<%#Eval("Elec_Tilt")%>'
                                                CssClass="lblTextSmall" Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblElecTilt" runat="server" Text='<%#Eval("Elec_Tilt")%>' CssClass="lblFieldSmall" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div>
                                                <asp:TextBox ID="txtftElecTilt" runat="server" Width="50px" CssClass="lblTextSmall" Height="14px" />
                                                <asp:RequiredFieldValidator ID="rfvftElecTilt" runat="server" ControlToValidate="txtftElecTilt"
                                                    Text="*" ValidationGroup="validation2" />
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        <span class="lblText">Longitude</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtLongitude" runat="server" CssClass="lblText"></asp:TextBox>
                                    </td>
                                    <td>
                                        <span class="lblText">Latitude</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtLatitude" runat="server" CssClass="lblText"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnClose" runat="server" Text="Save" CssClass="buttonStyle" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:View>
                </asp:MultiView>
			</div>
            </div>
            <div class="pnlremarks">
                
            </div>
            <div class="footerPanel">
                <div id="signaturePanel_Top" style="width: 100%; margin-top: 10px;">
                    <asp:DataList ID="DLDigitalSign_NSNOnly" runat="server" Width="100%" RepeatColumns="2">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" width="250px" border="0">
                                <tr>
                                    <td class="lblBText" style="text-align: left;">For and behalf of<br />
                                        <%#Eval("CompanyName")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="DgSign" runat="server" style="height: 100px; text-align: left;">
                                        <asp:ImageButton ID="ImgPostion" runat="server" ImageUrl="https://sdmthree.nsnebast.com/Images/dgsign.JPG" />
                                        <asp:HiddenField runat="Server" ID="hdXCoordinate" />
                                        <asp:HiddenField runat="Server" ID="hdYCoordinate" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblBText" style="border-width: 1px; border-color: Black; border-collapse: collapse; border-style: solid; vertical-align: top; text-align: center;">
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
                <div id="SignaturePanel_Bottom" style="width: 100%; margin-top: 10px;">
                    <asp:DataList ID="DdlDigitalSignature_Customer" runat="server" Width="100%" RepeatColumns="2">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" width="250px" border="0">
                                <tr>
                                    <td class="lblBText">For and behalf of<br />
                                        <%#Eval("CompanyName")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="DgSign" runat="server" style="height: 100px; text-align: left;">
                                        <asp:ImageButton ID="ImgPostion" runat="server" ImageUrl="https://sdmthree.nsnebast.com/Images/dgsign.JPG" />
                                        <asp:HiddenField runat="Server" ID="hdXCoordinate" />
                                        <asp:HiddenField runat="Server" ID="hdYCoordinate" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblBText" style="border-width: 1px; border-color: Black; border-collapse: collapse; border-style: solid; vertical-align: top; text-align: center;">
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
            <div style="display: none;">
                <table cellspacing="0" cellpadding="0" width="100%" border="1">
                    <tr class="lblText">
                        <td style="width: 24%">Company
                        </td>
                        <td style="width: 1%">:
                        </td>
                        <td style="width: 25%">Nokia Siemens Networks
                        </td>
                        <td rowspan="3" style="width: 25%">Site Quality Acceptance Certificate
                        </td>
                        <td style="width: 25%" colspan="4">Quality Certificate Form
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>Prepare By
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:Label ID="lblPreBy" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                        <td style="width: 12%">Last Update
                        </td>
                        <td style="width: 1%">:
                        </td>
                        <td colspan="2">
                            <input runat="server" id="txtLUpdate" class="textFieldStyle" readonly="readonly" />&nbsp;<asp:ImageButton
                                ID="btnDate" runat="server" Width="18px" ImageUrl="~/Images/calendar_icon.jpg"
                                Height="16px"></asp:ImageButton>
                            <b>
                                <asp:Label ID="lblLUpdate" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>Author
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:Label ID="lblAuthor" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                        <td>Page : 1/1
                        </td>
                        <td colspan="2">&nbsp;Version :
                        </td>
                        <td>
                            <asp:TextBox ID="txtVer" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                            <b>
                                <asp:Label ID="lblVer" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                    </tr>
                </table>
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
