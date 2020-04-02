<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTI_MSFI.aspx.vb" Inherits="BAUT_frmTI_MSFI" EnableEventValidation="false" %>

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

        .lblTextright {
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
            text-align: right;
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
            window.location = '../HCPT_Dashboard/frmRFTReadyCreation.aspx';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnMSFIID" runat="server" />
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
            <div>
                <span class="lblBold">Master Site File Index Checklist</span>
            </div>
            <div style="margin-top: 5px;">
                <table cellpadding="0" cellspacing="0">
                    <tr class="lblText">
                        <td>Project Name
                        </td>
                        <td>:</td>
                        <td>&nbsp;<asp:Label ID="LblPono" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>DCC Ref. No.</td>
                        <td>:</td>
                        <td>&nbsp;<asp:TextBox ID="LblDccNo" runat="server" CssClass="lblText"></asp:TextBox></td>
                    </tr>
                    <tr id="pnlPOType" runat="server" class="lblText">
                        <td>
                            <span class="lblText">Service Type</span>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="DdlPOType" runat="server" AutoPostBack="true" CssClass="lblText">
                                <asp:ListItem Text="Upgrade Hardware" Value="eqpandsvc"></asp:ListItem>
                                <asp:ListItem Text="Upgrade Software" Value="eqp"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>Link Name
                        </td>
                        <td>:</td>
                        <td>&nbsp;<asp:Label ID="lblSiteName" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>Link ID
                        </td>
                        <td>:</td>
                        <td>&nbsp;<asp:Label ID="lblSiteID" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div style="overflow-y: auto; float: left; width: 70%;">
                <asp:MultiView ID="mvPanelList" runat="server">
                    <asp:View ID="vwMasterList" runat="server">
                        <asp:GridView ID="GvMasterList" runat="server" AllowPaging="false" AutoGenerateColumns="false" EmptyDataText="No documents to be reviewed" Width="100%">
                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle" />
                            <HeaderStyle CssClass="GridHeader" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <EmptyDataRowStyle Font-Bold="true" Font-Names="Verdana" Font-Size="10pt" ForeColor="red" Height="25px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText="SN" ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <div style="margin-left: 5px;">
                                            <asp:Literal ID="ltrSN" runat="server"></asp:Literal>
                                            <asp:Label ID="lblListID" runat="server" Visible="false" Text='<%#Eval("list_id") %>'></asp:Label>
                                            <asp:Label ID="lblCheckedCount" runat="server" Visible="false" Text='<%#Eval("ischecked")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Site Documentation Package">
                                    <ItemTemplate>
                                        <asp:Literal ID="ltrDocName" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="20px">
                                    <ItemTemplate>

                                        <div style="text-align: center; width: 100%;">
                                            <asp:CheckBox ID="chkChecked" runat="server"></asp:CheckBox>
                                            <asp:ImageButton ID="imgDelete" runat="server" CommandName="deletesitedoc" CommandArgument='<%#Eval("list_id") %>' ImageUrl="~/images/trash.png" Width="15px" Height="15px" OnClientClick="return confirm('Are you sure you want to unchecklist this Site Document?')" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="vwDefinedList" runat="server">
                        <asp:GridView ID="gvMSFIList" runat="server" AutoGenerateColumns="false" Width="100%" BorderColor="Black" BorderWidth="1px">
                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle" />
                            <HeaderStyle CssClass="GridHeader" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <EmptyDataRowStyle Font-Bold="true" Font-Names="Verdana" Font-Size="10pt" ForeColor="red" Height="25px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText="SN" ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <div style="margin-left: 5px;">
                                            <asp:Literal ID="ltrSN" runat="server"></asp:Literal>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Site Documentation Package" ItemStyle-Width="300px">
                                    <ItemTemplate>
                                        <asp:Literal ID="ltrDocName" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" ItemStyle-CssClass="lblTextSmall" ItemStyle-Width="180px">
                                    <ItemTemplate>
                                        <asp:Panel ID="pnlCategory" runat="server">
                                            <asp:CheckBox ID="chkCopy" runat="server" Text="Copy" ItemStyle-CssClass="lblTextSmall" Checked='<%#Eval("isCopy")%>' />
                                            <asp:CheckBox ID="chkOriginal" runat="server" Text="Original" ItemStyle-CssClass="lblTextSmall" Checked='<%#Eval("isOriginal")%>' />
                                            <asp:CheckBox ID="chkNA" runat="server" Text="NA" ItemStyle-CssClass="lblTextSmall" Checked='<%#Eval("isNa")%>' />
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="lblText" Width="98%" Text='<%#Eval("remarks") %>'></asp:TextBox>
                                        <asp:Label ID="lblMSFIDetID" runat="server" Visible="false" Text='<%#Eval("msfidet_id")%>'></asp:Label>
                                        <asp:Label ID="lblParentID" runat="server" Visible="false" Text='<%#Eval("parent_id")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:View>
                </asp:MultiView>

            </div>
            <div style="float: right; width: 29%;">
                <div>
                    <span style="color: #0094ff; font-family: Verdana; font-size: 13px;">Supporting Document</span>
                </div>
                <div style="height: 2px; width: 100%; background-color: #0094ff; margin-top: 5px;"></div>
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
                    </table>
                </div>
            </div>
            <div style="clear: both;"></div>
            <div style="height: 1px; width: 100%; background-color: #808080; margin-top: 5px;"></div>

        </div>
        <div class="footerPanel" style="text-align: right; margin-top: 5px;">
            <div id="SignaturePanel_Bottom" style="text-align: left; width: 100%; margin-top: 10px;">
                <asp:DataList ID="DdlDigitalSignature_Customer" runat="server" Width="100%" RepeatColumns="2">
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="250px" border="0">
                            <tr>
                                <td class="lblBText">For and behalf of<br />
                                    <%#Eval("CompanyName")%>
                                </td>
                            </tr>
                            <tr>
                                <td id="Td1" runat="server" style="height: 100px; text-align: left;">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="http://www.telkomsel.nsnebast.com/Images/dgsign.JPG" Visible="false" />
                                    <asp:HiddenField runat="Server" ID="HiddenField1" />
                                    <asp:HiddenField runat="Server" ID="HiddenField2" />
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
            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="btnstyle" />
            <asp:Button ID="btnEdit" runat="server" Text="Edit Checklist" CssClass="btnstyleEdit" />
            <asp:Button ID="btnGenerate" runat="server" Text="Create Checklist" CssClass="buttonStyle"
                Width="120px" OnClientClick="return CheckEmptyMandatory();" />
            <asp:Button ID="btnSubmitReject" runat="server" Text="Submit" CssClass="buttonStyle"
                Width="120px" />
        </div>



    </form>
</body>
</html>

