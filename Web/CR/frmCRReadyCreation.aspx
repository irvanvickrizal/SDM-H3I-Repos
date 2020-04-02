<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCRReadyCreation.aspx.vb"
    Inherits="CR_frmCRReadyCreation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CR Ready Creation</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .HeaderGrid
    {
        font-family: Verdana;
        font-size: 8pt;
        font-weight: bold;
        color: White;
        background-color: #ffc90E;
        border-color:white;
        vertical-align:middle;
    }
    .oddGrid
    {
        font-family: Verdana;
        font-size: 7.5pt;
        background-color: White;
    }
    .evenGrid
    {
        font-family: Verdana;
        font-size: 9pt;
        background-color:#cfcfcf;
    }
        .PnlHeader
        {
            Padding:3px;
            background-color:#7f7f7f;
            Color:#ffffff;
            font-family:Verdana;
            font-size:12px;
            font-weight:bolder;
        }
        
        .PnlHeader2
        {
            Padding:7px;
            background-color:#7f7f7f;
            Color:#ffffff;
            font-family:Verdana;
            font-size:12px;
            font-weight:bolder;
        }
        
        .BtnATPOnline
        {
            font-family:verdana;
            font-size:10px;
            font-weight:bolder;
            border-style:solid;
            border-width:1px;
            border-color:#ffffff;
            background-color:#cccccc;
            color:#3F48CC;
            height:20px;
            cursor:hand;
        }
        .BtnATPViewAll
        {
            font-family:verdana;
            font-size:10px;
            font-weight:bolder;
            border-style:solid;
            border-width:1px;
            border-color:black;
            background-color:#cccccc;
            color:#3F48CC;
            height:20px;
            cursor:hand;
            width:60px;
        }
        .HeaderPanel
        {
           width:99%;
           background-repeat: repeat-x;
           background-image: url(../Images/banner/BG_Banner.png);
           font-family:verdana;
           font-weight:bolder;
           font-size:10pt;
           color:white;
           padding-top:5px;
           padding-bottom:5px;
        }
        .GeneratePDFForm
        {
            height:26px;
            width:105px;
            background-image: url(../Images/button/BtnGenerateFinalCR_0.gif);
        }
        .GeneratePDFForm:hover
        {
            height:26px;
            width:105px;
            background-image: url(../Images/button/BtnGenerateFinalCR_1.gif);
        }
        .GeneratePDFForm:click
        {
            height:26px;
            width:105px;
            background-image: url(../Images/button/BtnGenerateFinalCR_2.gif);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:MultiView ID="MvCorePanel" runat="server">
                <asp:View ID="VwReadyCreation" runat="server">
                    <asp:GridView ID="GvCRReadyCreation" runat="server" AutoGenerateColumns="false" EmptyDataText="No CR Final Ready Creation">
                        <PagerSettings Position="TopAndBottom" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <RowStyle CssClass="GridOddRows" />
                        <EmptyDataRowStyle ForeColor="red" Font-Names="Verdana" Font-Size="10pt" Font-Bold="true"
                            Height="25px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <asp:TemplateField HeaderText=" No. " HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="24px">
                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SiteNo" HeaderText="Site No" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="80px" />
                            <asp:BoundField DataField="PoNo" HeaderText="PO Number" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="EOName" HeaderText="PO Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="250px"/>
                            <asp:BoundField DataField="SiteName" HeaderText="Site Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="250px" />
                            <asp:BoundField DataField="PackageId" HeaderText="Work Package ID" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="submitDate" HeaderText="Submit Date" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                ItemStyle-Width="160px" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LbtDocumentCount" runat="server" Text="Check Document"
                                        CommandName="opencrapproved" CommandArgument='<%#Eval("Packageid") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:View>
                <asp:View ID="vwCRFinal" runat="server">
                    <div class="HeaderPanel">
                        <div style="margin-left: 10px;">
                            CR Approved List
                        </div>
                    </div>
                    <div>
                        <table>
                            <tr class="lblText">
                                <td style="width: 20%">
                                    Po No</td>
                                <td style="width: 1%">
                                    :
                                </td>
                                <td colspan="2" id="tdpono" runat="server">
                                </td>
                            </tr>
                            <tr class="lblText">
                                <td>
                                    PO Name</td>
                                <td>
                                    :</td>
                                <td colspan="2" id="tdponame" runat="server">
                                </td>
                            </tr>
                            <tr class="lblText">
                                <td>
                                    Site No</td>
                                <td>
                                    :</td>
                                <td colspan="2" id="tdsiteno" runat="server">
                                </td>
                            </tr>
                            <tr class="lblText">
                                <td>
                                    Site Name</td>
                                <td>
                                    :</td>
                                <td colspan="2" id="tdsitename" runat="server">
                                </td>
                            </tr>
                            <tr class="lblText">
                                <td>
                                    Scope</td>
                                <td>
                                    :</td>
                                <td runat="server" id="tdscope" colspan="2">
                                </td>
                            </tr>
                            <tr class="lblText">
                                <td>
                                    Work Package ID</td>
                                <td>
                                    :</td>
                                <td colspan="2" id="tdwpid" runat="server">
                                </td>
                            </tr>
                            <tr class="lblText">
                                <td>
                                    Project ID</td>
                                <td>
                                    :</td>
                                <td colspan="2" id="tdprojectId" runat="server">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="margin-top: 10px;">
                        <asp:GridView ID="GvListCRApproved" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="HeaderGrid"
                            Width="99%" CellPadding="3" CellSpacing="2" EmptyDataText="No Record CR Found">
                            <AlternatingRowStyle CssClass="evenGrid2" />
                            <EmptyDataRowStyle CssClass="lblSubHeader" ForeColor="red" />
                            <Columns>
                                <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="HeaderGrid" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Font-Names="Verdana" ItemStyle-Font-Size="7.5pt">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:Label ID="LblCRID" runat="server" Text='<%#Eval("CR_ID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CRNo" HeaderText="CR No" ConvertEmptyStringToNull="true"
                                    NullDisplayText="Nothing" ItemStyle-Font-Size="7.5pt" HeaderStyle-CssClass="HeaderGrid"
                                    HeaderStyle-Height="25px" />
                                <asp:BoundField DataField="Description_of_Change" HeaderText="Description of Change"
                                    ConvertEmptyStringToNull="true" NullDisplayText="Nothing" ItemStyle-Font-Size="7.5pt"
                                    HeaderStyle-CssClass="HeaderGrid" HeaderStyle-Height="25px" />
                                <asp:BoundField DataField="Schedule_Impact" HeaderText="Schedule Impact" ConvertEmptyStringToNull="true"
                                    NullDisplayText="Nothing" ItemStyle-Font-Size="7.5pt" HeaderStyle-CssClass="HeaderGrid"
                                    HeaderStyle-Height="25px" />
                                <asp:BoundField DataField="Other_Impact" HeaderText="Other Impact" ConvertEmptyStringToNull="true"
                                    NullDisplayText="Nothing" ItemStyle-Font-Size="7.5pt" HeaderStyle-CssClass="HeaderGrid"
                                    HeaderStyle-Height="25px" />
                                <asp:BoundField DataField="IndicativePriceCost_USD" HeaderText="Indicative Price(USD)"
                                    ConvertEmptyStringToNull="true" NullDisplayText="Nothing" ItemStyle-Font-Size="7.5pt"
                                    HeaderStyle-CssClass="HeaderGrid" HeaderStyle-Height="25px" />
                                <asp:BoundField DataField="IndicativePriceCost_IDR" HeaderText="Indicative Price(IDR)"
                                    ConvertEmptyStringToNull="true" NullDisplayText="Nothing" ItemStyle-Font-Size="7.5pt"
                                    HeaderStyle-CssClass="HeaderGrid" HeaderStyle-Height="25px" />
                                <asp:BoundField DataField="PercentagePriceChange_USD" HeaderText="Percentage Price(USD)"
                                    ConvertEmptyStringToNull="true" NullDisplayText="Nothing" ItemStyle-Font-Size="7.5pt"
                                    ItemStyle-Width="60px" HeaderStyle-CssClass="HeaderGrid" HeaderStyle-Height="25px" />
                                <asp:BoundField DataField="PercentagePriceChange_IDR" HeaderText="Percentage Price(IDR)"
                                    ConvertEmptyStringToNull="true" NullDisplayText="Nothing" ItemStyle-Font-Size="7.5pt"
                                    ItemStyle-Width="60px" HeaderStyle-CssClass="HeaderGrid" HeaderStyle-Height="25px" />
                                <asp:TemplateField ItemStyle-Font-Names="verdana" ItemStyle-Font-Size="7.5pt" HeaderStyle-CssClass="HeaderGrid"
                                    HeaderStyle-Height="25px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbtGenerateFinalCR" Width="105px" runat="server" OnClientClick="return confirm('Do you want to create as Final CR?')"
                                            Style="text-decoration: none;" CommandName="pdfgenerate" CommandArgument='<%#Eval("CR_ID") %>'>
                                    <div class="GeneratePDFForm"></div>                    
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </form>
</body>
</html>
