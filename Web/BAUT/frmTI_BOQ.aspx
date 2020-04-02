<%@ Page Language="VB" Trace="false" AutoEventWireup="false" CodeFile="frmTI_BOQ.aspx.vb"
    Inherits="frmTI_BOQ" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BOQ</title>
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/Validation.js">
    </script>

    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js">
    </script>

    <script language="javascript" type="text/javascript">
            function WindowsClose(){
                window.opener.location.href = '../dashboard/DashboardpopupBast.aspx?time=' + (new Date()).getTime();
                if (window.opener.progressWindow) {
                    window.opener.progressWindow.close();
                }
                window.close();
            }
            
            function getControlPosition(){
                var Total = document.getElementById('HDDgSignTotal').value;
                for (intCount1 = 0; intCount1 < Total; intCount1++) {
                    var divctrl = document.getElementById('DLDigitalSign_ctl0' + intCount1 + '_ImgPostion');
                    eval("document.getElementById('DLDigitalSign_ctl0" + intCount1 + "_hdXCoordinate')").value = findPosX(divctrl);
                    eval("document.getElementById('DLDigitalSign_ctl0" + intCount1 + "_hdYCoordinate')").value = findPosY(divctrl);
                    //alert(findPosX(divctrl) + " , " + findPosY(divctrl));
                }
            }
            
            function findPosX(imgElem){
                xPos = eval(imgElem).offsetLeft;
                tempEl = eval(imgElem).offsetParent;
                while (tempEl != null) {
                    xPos += tempEl.offsetLeft;
                    tempEl = tempEl.offsetParent;
                }
                return xPos;
            }
            
            function findPosY(imgElem){
                yPos = eval(imgElem).offsetTop;
                tempEl = eval(imgElem).offsetParent;
                while (tempEl != null) {
                    yPos += tempEl.offsetTop;
                    tempEl = tempEl.offsetParent;
                }
                return yPos;
            }
            
            function Calc(){
                var ValA = document.getElementById('txtPO').value;
                var ValB = document.getElementById('txtActual').value;
                if (ValA == '' && ValB != '') {
                    document.getElementById('txtDelta').value = '';
                    alert("For Delta Value Please Fill The PO Value");
                }
                else 
                    if (ValA != '' && ValB == '') {
                        document.getElementById('txtDelta').value = document.getElementById('txtPO').value;
                        
                    }
                    else 
                        if (ValA != '' && ValB != '') {
                            var ValC = ValA - ValB;
                            if (ValC != 0) {
                                document.getElementById('txtDelta').value = ValC;
                            }
                            else {
                                document.getElementById('txtDelta').value = ValC;
                            }
                        }
                        else 
                            if (ValA == '' && ValB == '') {
                                document.getElementById('txtDelta').value = '';
                            }
            }
            
            function Calc1(){
                var ValA = document.getElementById('txtPOUSD').value;
                var ValB = document.getElementById('txtActUSD').value;
                if (ValA == '' && ValB != '') {
                    document.getElementById('txtDelUSD').value = '';
                    alert("For Delta Value Please Fill The PO Value");
                }
                else 
                    if (ValA != '' && ValB == '') {
                        document.getElementById('txtDelUSD').value = document.getElementById('txtPOUSD').value;
                    }
                    else 
                        if (ValA != '' && ValB != '') {
                            var ValC = ValA - ValB;
                            if (ValC != 0) {
                                document.getElementById('txtDelUSD').value = ValC;
                            }
                            else {
                                document.getElementById('txtDelUSD').value = ValC;
                            }
                        }
                        else 
                            if (ValA == '' && ValB == '') {
                                document.getElementById('txtDelUSD').value = '';
                            }
            }
            
            function Showmain(type){
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
                if (type == 'Lnk') {
                    alert('Not Allowed to upload this Document Now.');
                }
                window.location = '../PO/frmSiteDocUploadTree.aspx';
            }
            
            function transferValue(rw,type){
                var csv = "";
                for (var i = 1; i <= rw; i++) {
                    var valA = 0;
                    if (document.getElementById('input' + i + 'a').value != "") 
                        var valA = document.getElementById('input' + i + 'a').value;
                    var valB = 0;
                    if (document.getElementById('input' + i + 'b').value != "") 
                        var valB = document.getElementById('input' + i + 'b').value;
                    var valD = 0;
                    if (document.getElementById('input' + i + 'd').value != "") 
                        var valD = document.getElementById('input' + i + 'd').value;
                    var valE = 0;
                    if (document.getElementById('input' + i + 'e').value != "") 
                        var valE = document.getElementById('input' + i + 'e').value;
                    var valG = 0;
                    if (document.getElementById('input' + i + 'g').value != "") 
                        var valG = document.getElementById('input' + i + 'g').value;
                    var valH = 0;
                    if (document.getElementById('input' + i + 'h').value != "") 
                        var valH = document.getElementById('input' + i + 'h').value;
                    var valM = 0;
                    if (type == 2) {
                        if (document.getElementById('input' + i + 'm').value != ""){
                            var valM = document.getElementById('input' + i + 'm').value;    
                        }
                    }
                    var valN = 0;
                    if (type == 2) {
                        if (document.getElementById('input' + i + 'n').value != ""){
                            var valN = document.getElementById('input' + i + 'n').value;
                        }
                    }
                    
                    var valJ = valD - valG;
                    var valK = valE - valH;
                    if (i == 1) {
                        csv = valA + "!" + valB + "!" + "0!" + valD + "!" + valE + "!" + "0!" + valG + "!" + valH + "!" + "0!" + valJ + "!" + valK + "!" + "0!" + valM + "!" + valN;
                    }
                    else {
                        csv += "@" + valA + "!" + valB + "!" + "0!" + valD + "!" + valE + "!" + "0!" + valG + "!" + valH + "!" + "0!" + valJ + "!" + valK + "!" + "0!" + valM + "!" + valN;
                    }
                }
                document.getElementById('csv').value = csv;
            }
            
            function formatCurrency(num){
                num = num.toString().replace(/\$|\,/g, '');
                if (isNaN(num)) 
                    num = "0";
                sign = (num == (num = Math.abs(num)));
                num = Math.floor(num * 100 + 0.50000000001);
                num = Math.floor(num / 100).toString();
                for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++) 
                    num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
                return (num);
            }
            
            function formatusdCurrency(num){
                num = num.toString().replace(/\$|\,/g, '');
                if (isNaN(num)) 
                    num = "0";
                sign = (num == (num = Math.abs(num)));
                num = Math.floor(num * 100 + 0.50000000001);
                cents = num % 100;
                num = Math.floor(num / 100).toString();
                if (cents < 10) 
                    cents = "0" + cents;
                for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++) 
                    num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
                return (((sign) ? '' : '-') + num + '.' + cents);
            }
    </script>

    <style type="text/css">
            tr {
                padding: 3px;
            }
            
            .MainCSS {
                margin-bottom: 0px;
                margin-left: 20px;
                margin-right: 20px;
                margin-top: 0px;
                width: 800px;
                height: 700px;
                text-align: center;
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
            
            .lblBold {
                font-family: verdana;
                font-size: 12pt;
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
                font-family: verdana;
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
            
            .lblBBTextL {
                border-bottom: 1px #000 solid;
                border-left: 1px #000 solid;
                border-top: 1px #000 solid;
                font-family: verdana;
                font-size: 8pt;
                color: #000000;
                text-align: left;
                font-weight: bold;
                text-decoration: none;
            }
            
            .lblBBTextM {
                border-bottom: 1px #000 solid;
                border-top: 1px #000 solid;
                font-family: verdana;
                font-size: 8pt;
                color: #000000;
                text-align: left;
                font-weight: normal;
                text-decoration: none;
                width: 1%;
            }
            
            .lblBBTextR {
                border-right: 1px #000 solid;
                border-bottom: 1px #000 solid;
                border-top: 1px #000 solid;
                font-family: verdana;
                font-size: 8pt;
                color: #000000;
                text-align: left;
                font-weight: normal;
                text-decoration: none;
            }
            
            #lblTotalA {
                font-weight: bold;
            }
            
            #lblJobDelay {
                font-weight: bold;
            }
        </style>
</head>
<body class="MainCSS">
    <form id="form1" runat="server">
        <input type="hidden" runat="server" id="HDDgSignTotal" /><input runat="server" id="hdnready4baut"
            type="hidden" /><input runat="server" id="hdnKeyVal" type="hidden" /><input runat="server"
                id="hdnDGBox" type="hidden" /><input runat="server" id="hdnScope" type="hidden" /><input
                    runat="Server" id="hdndocId" type="hidden" /><input runat="Server" id="hdnWfId" type="hidden" /><input
                        runat="Server" id="hdnversion" type="hidden" /><input runat="Server" id="hdnsiteid"
                            type="hidden" /><input runat="Server" id="hdnAdminRole" type="hidden" /><input runat="Server"
                                id="hdnAdmin" type="hidden" /><input runat="Server" id="hdnSiteno" type="hidden" />
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>
        <div id="dvPrint" runat="server">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" valign="top" style="width: 20%">
                        <img src="http://www.telkomsel.nsnebast.com/Images/nsn-logo.gif" height="36px" width="104px"
                            alt="logonsn" />
                    </td>
                    <td colspan="4" align="center" class="lblBold" valign="top" style="width: 60%">
                        ATTACHMENT TO
                        <br />
                        BOQ &amp; Pricing Sheet
                        <br />
                        (TYPE OF WORK)
                    </td>
                    <td align="right" valign="top" style="width: 20%">
                        <img src="http://www.telkomsel.nsnebast.com/Images/logo_tsel.png" alt="logotsel" />
                    </td>
                </tr>
            </table>
            <div id="BOQForm">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="3" class="Hcap">
                            <br />
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td style="width: 25%">
                            Type of Works
                        </td>
                        <td style="width: 1%">
                            :
                        </td>
                        <td style="width: 74%">
                            Equipment
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>
                            Site ID/Site Name (NSN)
                        </td>
                        <td>
                            :
                        </td>
                        <td class="lblText" style="height: 19px">
                            <asp:Label ID="lblSite" runat="server">
                            </asp:Label>&nbsp; <strong>SiteID(PO): </strong>
                            <asp:Label ID="lblsiteidpo" runat="server">
                            </asp:Label>
                            <strong>Sitename(PO): </strong>
                            <asp:Label ID="lblsitenamepo" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>
                            Project ID
                        </td>
                        <td>
                            :
                        </td>
                        <td class="lblText">
                            <b>
                                <asp:Label ID="lblProj" runat="server" CssClass="lblText">
                                </asp:Label>
                            </b>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td colspan="3">
                            <hr />
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td class="Hcap" colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="Hcap" style="height: 5px">
                            <asp:Label ID="lblPONO" runat="server" CssClass="lblText" Visible="false">
                            </asp:Label>
                            <asp:Label ID="lblPONO2" runat="server" CssClass="lblText" Visible="false">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="BOQValue" style="height: 500px">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td id="tdTable" runat="server" align="center" class="lblText" colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="Hcap" style="height: 5px">
                            <asp:HiddenField ID="csv" runat="Server" />
                            <asp:RadioButton ID="Rb100Percent" runat="server" Text="BOQ 100%" AutoPostBack="true"
                                GroupName="rbBOQForm" />
                            <asp:RadioButton ID="Rb5Percent" runat="server" Text="BOQ 5%" AutoPostBack="true"
                                GroupName="rbBOQForm" />
                            <asp:Button ID="btnAddRow" runat="server" Text="Add Row" CssClass="buttonStyle" /><asp:Button
                                ID="btnAddRowComplete" runat="server" Text="Complete" CssClass="buttonStyle" />
                            <asp:Button ID="BtnAdditionalInformation" runat="server" Text="Add Info" CssClass="buttonStyle" />
                            <asp:HiddenField ID="hdfAdditionalInformation" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="Hcap" style="text-align: left;">
                            <asp:Label ID="LblRemarks" runat="server" Style="font-family: Verdana; font-size: 7.5pt;
                                text-align: left; margin-top: 5px;" Font-Bold="true" Text="Remarks : Additional value in delta will be claim in separate PO">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="additionalValue" style="text-align: left; margin-top: 10px;">
                    <asp:Panel ID="PnlAdditionalValue" runat="server">
                        <div id="AdditionalValueTop">
                            <table border="1" cellpadding="0" cellspacing="0" style="border-color: Black;">
                                <tr>
                                    <td class="lblText" style="text-align: center; border-color: Black;">
                                        REMARKS
                                    </td>
                                    <td class="lblText" style="text-align: center; border-color: Black;">
                                        Value (USD)
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblText" style="border-color: Black;">
                                        Value of Buy Back Hardware
                                    </td>
                                    <td>
                                        <input type="text" id="TxtBBHValue" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                            style="text-align: center; font-size: 9pt; font-family: verdana; border-style: solid;
                                            border-width: 1px; border-color: White;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblText" style="border-color: Black;">
                                        Value of Buy Back Software
                                    </td>
                                    <td>
                                        <input type="text" onblur="this.value=formatusdCurrency(this.value);" style="text-align: center;
                                            font-size: 9pt; font-family: verdana; border-style: solid; border-width: 1px;
                                            border-color: White;" id="txtBBSValue" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="AdditionalValueDown" style="margin-top: 5px; text-align: right; width: 330px;">
                            <asp:Button ID="BtnClearAdditionalValue" runat="server" Text="Delete Table" Style="width: 100pt;
                                background-color: Silver; color: black; font-family: Verdana; font-size: 8pt;
                                font-weight: bold; border-right: 1px double; border-top: 1px double; border-left: 1px double;
                                border-bottom: 1px double;" />
                            <asp:Button ID="BtnDeleteAdditionalValue" runat="server" Text="Delete Table" Style="width: 100pt;
                                background-color: Silver; color: black; font-family: Verdana; font-size: 8pt;
                                font-weight: bold; border-right: 1px double; border-top: 1px double; border-left: 1px double;
                                border-bottom: 1px double;" OnClientClick="return confirm('Are you sure you want to delete this modernization table?');" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div id="pnlSignature">
                <asp:DataList ID="DLDigitalSign" runat="server" Width="100%" RepeatColumns="3">
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="200px" border="0">
                            <tr>
                                <td class="lblBText" style="border-width: 1px; border-color: Black; border-collapse: collapse;
                                    border-style: solid; vertical-align: top; text-align: center;">
                                    <%#Container.DataItem("Description") %>
                                </td>
                            </tr>
                            <tr>
                                <td id="DgSign" runat="server" style="height: 100px;">
                                    <asp:ImageButton ID="ImgPostion" runat="server" ImageUrl="http://www.telkomsel.nsnebast.com/Images/dgsign.JPG" /><asp:HiddenField
                                        runat="Server" ID="hdXCoordinate" />
                                    <asp:HiddenField runat="Server" ID="hdYCoordinate" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblBText" style="border-width: 1px; border-color: Black; border-collapse: collapse;
                                    border-style: solid; vertical-align: top; text-align: center;">
                                    <%#Container.DataItem("name") %>
                                    <br />
                                    <%#Container.DataItem("roledesc") %>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div id="BOQAttachment">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="grddocuments" runat="server" Width="100%" AutoGenerateColumns="False"
                                        EmptyDataText="No Records Found" DataKeyNames="Doc_Id" PageSize="2">
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                        <AlternatingRowStyle CssClass="GridEvenRows" />
                                        <RowStyle CssClass="GridOddRows" />
                                        <Columns>
                                            <asp:TemplateField HeaderText=" Total ">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblno" runat="Server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="doc_id" HeaderText="doc" />
                                            <asp:BoundField DataField="docpath" HeaderText="Path" />
                                            <asp:BoundField DataField="DocName" HeaderText="Document">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SW_Id" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rdbstatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                                                        OnSelectedIndexChanged="DOThis" AutoPostBack="true">
                                                        <asp:ListItem Selected="True" Value="0">
                                                            Approve
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="1">
                                                            Reject
                                                        </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
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
                                    <asp:HiddenField ID="hdnswid" runat="server" />
                                </td>
                            </tr>
                            <tr align="center" class="lblText">
                                <td align="center">
                                    <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="buttonStyle" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
