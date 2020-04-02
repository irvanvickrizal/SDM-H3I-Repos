<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListCR.aspx.vb" Inherits="CR_frmListCR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>List CR</title>
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>

    <style type="text/css">
     .HeaderGrid
    {
        font-family: Arial Unicode MS;
        font-size: 8pt;
        font-weight: bold;
        color: White;
        background-color: #ffc90E;
        border-color:white;
        vertical-align:middle;
    }
    .oddGrid
    {
        font-family: Arial Unicode MS;
        font-size: 7.5pt;
        background-color: White;
    }
    .evenGrid
    {
        font-family: Arial Unicode MS;
        font-size: 9pt;
        background-color:#cfcfcf;
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
        .lblSubHeader
        {
            font-family:Arial Unicode MS;
            font-size:8pt;
            font-weight:bolder;
        }
        .lblTitle
        {
            font-family:Arial Unicode MS;
            font-size:10pt;
            font-weight:bolder;
        }
         .lblBText
        {
            font-family: Arial Unicode MS;
            font-size: 7.5pt;
            color: #000000;
            text-align: left;
            font-weight: bold;
        }
        .evengrid2
        {
            background-color: #cfcfcf;
        }
        .btnAddCR
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnAddCR_0.gif);
        }
        .btnAddCR:hover
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnAddCR_1.gif);
        }
        .btnAddCR:click
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnAddCR_2.gif);
        }
        .btnCancel
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnCancel_0.gif);
        }
        .btnCancel:hover
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnCancel_1.gif);
        }
        .btnCancel:click
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnCancel_2.gif);
        }
        .btnGenerateCR
        {
            height:26px;
            width:105px;
            background-image: url(../Images/button/btnGenerateCRForm_0.gif);
        }
        .btnGenerateCR:hover
        {
            height:26px;
            width:105px;
            background-image: url(../Images/button/btnGenerateCRForm_1.gif);
        }
        .btnGenerateCR:click
        {
            height:26px;
            width:105px;
            background-image: url(../Images/button/btnGenerateCRForm_2.gif);
        }
        .btnViewCRFinal
        {
            height:26px;
            width:80px;
            background-image: url(../Images/button/BtnViewCRFinal_0.gif);
        }
        .btnViewCRFinal:hover
        {
            height:26px;
            width:80px;
            background-image: url(../Images/button/BtnViewCRFinal_1.gif);
        }
        .btnViewCRFinal:click
        {
            height:26px;
            width:80px;
            background-image: url(../Images/button/BtnViewCRFinal_2.gif);
        }
    </style>

    <script type="text/javascript">
        var ContentHeight = 100;
        var TimeToSlide = 250.0;

        var openAccordion = '';

        function runAccordion(index)
        {
          var nID = "Accordion" + index + "Content";
          if(openAccordion == nID)
            nID = '';
            
          setTimeout("animate(" + new Date().getTime() + "," + TimeToSlide + ",'" 
              + openAccordion + "','" + nID + "')", 33);
          
          openAccordion = nID;
        }
        function animate(lastTick, timeLeft, closingId, openingId)
        {  
          var curTick = new Date().getTime();
          var elapsedTicks = curTick - lastTick;
          
          var opening = (openingId == '') ? null : document.getElementById(openingId);
          var closing = (closingId == '') ? null : document.getElementById(closingId);
         
          if(timeLeft <= elapsedTicks)
          {
            if(opening != null)
              opening.style.height = ContentHeight + 'px';
            
            if(closing != null)
            {
              closing.style.display = 'none';
              closing.style.height = '0px';
            }
            return;
          }
         
          timeLeft -= elapsedTicks;
          var newClosedHeight = Math.round((timeLeft/TimeToSlide) * ContentHeight);

          if(opening != null)
          {
            if(opening.style.display != 'block')
              opening.style.display = 'block';
            opening.style.height = (ContentHeight - newClosedHeight) + 'px';
          }
          
          if(closing != null)
            closing.style.height = newClosedHeight + 'px';

          setTimeout("animate(" + curTick + "," + timeLeft + ",'" 
              + closingId + "','" + openingId + "')", 33);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="HeaderPanel">
            <asp:HiddenField ID="HdnCRIDAsFinal" runat="server" />
            <div style="margin-left: 10px;">
                Change Request Form List
            </div>
        </div>
        <div style="width: 99%; border-bottom-color: Black; border-bottom-style: solid; border-bottom-width: 1px;
            padding-bottom: 10px; margin-top: 10px;">
            <table>
                <tr>
                    <td>
                        <span class="lblText">WorkPackage ID</span>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPackageId" CssClass="lblText" runat="server" Width="180px"></asp:TextBox>
                        <asp:Button ID="BtnSearch" runat="server" CssClass="buttonStyle" Width="60px" Text="Search" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 99%;">
            <asp:MultiView ID="MvMainPanel" runat="server">
                <asp:View ID="vwCorePanel" runat="server">
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
                    <asp:MultiView ID="MvPanel" runat="server">
                        <asp:View ID="VwListCR" runat="server">
                            <div style="margin-top: 15px;">
                                <asp:GridView ID="GvListCR" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="HeaderGrid"
                                    Width="100%" CellPadding="3" CellSpacing="2" EmptyDataText="No Record CR Found">
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
                                        <asp:BoundField DataField="Description_of_Change" HeaderText="Desc of Change" ConvertEmptyStringToNull="true"
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
                                        <asp:BoundField DataField="CRStatus" HeaderText="Status" ConvertEmptyStringToNull="true"
                                            NullDisplayText="Nothing" ItemStyle-Font-Size="7.5pt" HeaderStyle-CssClass="HeaderGrid"
                                            HeaderStyle-Height="25px" />
                                        <asp:BoundField DataField="InitiatorName" HeaderText="InitiatorName" ConvertEmptyStringToNull="true"
                                            NullDisplayText="Nothing" ItemStyle-Font-Size="7.5pt" HeaderStyle-CssClass="HeaderGrid"
                                            HeaderStyle-Height="25px" />
                                        <asp:TemplateField ItemStyle-Font-Names="verdana" ItemStyle-Font-Size="7.5pt" HeaderStyle-CssClass="HeaderGrid"
                                            HeaderStyle-Height="25px" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgGenerateCRForm" runat="server" CommandArgument='<%#Eval("CR_ID") %>'
                                                    CommandName="generate" OnClientClick="return confirm('Are you sure you want to Generate this CR ?')"
                                                    ImageUrl="~/images/doce.gif" ToolTip="Generate CR Form" />
                                                <asp:ImageButton ID="ImgDelete" runat="server" CommandArgument='<%#Eval("CR_ID") %>'
                                                    CommandName="deletecr" OnClientClick="return confirm('Are you sure you want to delete this CR ?')"
                                                    ImageUrl="~/images/action_delete.gif" ToolTip="Delete CR Form" />
                                                <a href="#" style="text-decoration: none; border-width: 0px;" onclick="window.open('../po/frmCRViewLog.aspx?id=<%# DataBinder.Eval(Container.DataItem,"CR_ID") %>&wpid=<%# DataBinder.Eval(Container.DataItem,"Package_Id") %>','','Width=700,height=500');">
                                                    <img src="~/images/ViewLog.jpg" alt="ViewLog" height="18" width="18" title="View Log"
                                                        style="text-decoration: none; border-width: 0px;" runat="server" />
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <tr style="width: 100%;">
                                                    <td style="border-style: none; border-color: White; border-bottom: none;">
                                                    </td>
                                                    <td style="border-style: none; border-color: White; border-bottom: none;">
                                                    </td>
                                                    <td style="border-style: none; border-color: White; border-bottom: none;">
                                                    </td>
                                                    <td style="border-style: none; border-color: White; border-bottom: none;">
                                                    </td>
                                                    <td style="border-style: none; border-color: White; border-bottom: none;">
                                                    </td>
                                                    <td colspan="6" style="border-style: none; border-color: White; border-bottom: none;
                                                        text-align: right;">
                                                        <div style="width: 99%; text-align: right;">
                                                            <asp:GridView ID="GvListCRAdditionalDoc" runat="server" AutoGenerateColumns="false"
                                                                EmptyDataText="No Additional Doc Uploaded">
                                                                <Columns>
                                                                    <asp:BoundField DataField="DocName" HeaderText=" Doc Name" ItemStyle-Font-Size="7.5pt"
                                                                        HeaderStyle-CssClass="HeaderGrid" HeaderStyle-Height="25px" />
                                                                    <asp:BoundField DataField="LMDT" ItemStyle-Font-Size="7.5pt" HeaderText="Upload Date"
                                                                        HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true"
                                                                        HeaderStyle-CssClass="HeaderGrid" HeaderStyle-Height="25px" />
                                                                    <asp:BoundField DataField="InitiatorName" HeaderText="Initiator Name" ItemStyle-Font-Size="7.5pt"
                                                                        HeaderStyle-CssClass="HeaderGrid" HeaderStyle-Height="25px" />
                                                                    <asp:TemplateField ItemStyle-Font-Size="7.5pt" HeaderStyle-CssClass="HeaderGrid"
                                                                        HeaderStyle-Height="25px">
                                                                        <ItemTemplate>
                                                                            <a href='../PO/frmViewCRDocument.aspx?crid=<%#Eval("CRID") %>&subdocid=<%#Eval("CRSiteDocId") %>'
                                                                                target="_blank" style="text-decoration: none;">
                                                                                <img src="../Images/Pdf_Icon.png" alt="pdficon" id="pdfIcon" height="18" width="18"
                                                                                    style="text-decoration: none; border-width: 0px;" runat="server" />
                                                                            </a>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <div>
                                                            <asp:ImageButton ID="ImgAddMom" runat="server" CommandArgument='<%#Eval("CR_ID") %>'
                                                                CommandName="addmom" ImageUrl="~/images/add.gif" ToolTip="Add MOM Doc" />
                                                            <asp:Label ID="LblCRStatus" runat="server" Text='<%#Eval("CRStatus") %>' Visible="false"></asp:Label>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div style="width: 100%; text-align: right; margin-top: 5px;">
                                <asp:LinkButton ID="LbtAddCR" Width="60px" runat="server" Style="text-decoration: none;">
                                    <div class="btnAddCR"></div>                    
                                </asp:LinkButton>
                                <asp:LinkButton ID="LbtViewCRFinal" Width="80px" runat="server" Style="text-decoration: none;">
                                    <div class="btnViewCRFinal"></div>
                                </asp:LinkButton>
                            </div>
                        </asp:View>
                        <asp:View ID="VwUploadDocumentMOMCR" runat="server">
                            <div>
                                <asp:HiddenField ID="HdnCRID" runat="server" />
                                <table width="100%">
                                    <tr class="pageTitle">
                                        <td colspan="2">
                                            MOM Upload
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div style="height: 5px;">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="GvListDocMOM" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="HeaderGrid"
                                                Width="60%" CellPadding="1" CellSpacing="1" EmptyDataText="No MOM CR">
                                                <AlternatingRowStyle CssClass="evenGrid2" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="HeaderGrid" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Font-Names="Verdana" ItemStyle-Font-Size="7.5pt">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DocName" HeaderText=" Doc Name" ItemStyle-Font-Size="7.5pt"
                                                        HeaderStyle-CssClass="HeaderGrid" HeaderStyle-Height="25px" />
                                                    <asp:BoundField DataField="LMDT" ItemStyle-Font-Size="7.5pt" HeaderText="Upload Date"
                                                        HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true"
                                                        HeaderStyle-CssClass="HeaderGrid" HeaderStyle-Height="25px" />
                                                    <asp:BoundField DataField="InitiatorName" HeaderText="Initiator Name" ItemStyle-Font-Size="7.5pt"
                                                        HeaderStyle-CssClass="HeaderGrid" HeaderStyle-Height="25px" />
                                                    <asp:TemplateField ItemStyle-Font-Names="verdana" ItemStyle-Font-Size="7.5pt" HeaderStyle-CssClass="HeaderGrid"
                                                        HeaderStyle-Height="25px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" CommandArgument='<%#Eval("CRSiteDocId") %>'
                                                                CommandName="deletedoc" OnClientClick="return confirm('Are you sure you want to delete this MOM ?')"
                                                                ImageUrl="~/images/action_delete.gif" ToolTip="Delete Class" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <a href='../PO/frmViewCRDocument.aspx?crid=<%#Eval("CRID") %>&subdocid=<%#Eval("CRSiteDocId") %>'
                                                                target="_blank" style="text-decoration: none; border-style: none;">
                                                                <img src="../Images/Pdf_Icon.png" alt="pdficon" id="pdfIcon" height="18" width="18"
                                                                    style="text-decoration: none; border-style: none;" />
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:FileUpload ID="FUMOMUpload" runat="server" Width="518px" />&nbsp;<asp:Button
                                                ID="btnview" runat="server" CssClass="buttonStyle" Text="Upload" Width="71px" />(Max
                                            2 MB)
                                            <br />
                                            <asp:Label ID="lblStatusUpload" ForeColor="green" CssClass="lblText" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="width: 100%; text-align: right; margin-top: 5px;">
                                <asp:LinkButton ID="LbtGenerateCR" Width="105px" runat="server" OnClientClick="return confirm('Are you sure you want to Generate this CR Form ?')"
                                    Style="text-decoration: none;">
                                    <div class="btnGenerateCR"></div>                    
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtCancelUploadMOM" Width="60px" runat="server" Style="text-decoration: none;">
                                    <div class="btnCancel"></div>                    
                                </asp:LinkButton>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </asp:View>
                <asp:View ID="vwNoFound" runat="server">
                    <asp:Label ID="LblNoFound" CssClass="lblText" ForeColor="Red" runat="server" Text="WPID Not Found"></asp:Label>
                </asp:View>
                <asp:View ID="vwNoCRNeeded" runat="server">
                    <asp:Label ID="LblNoCRNeeded" CssClass="lblText" ForeColor="Red" runat="server" Text="CheckList Not Yet Ready, Please Create First!"></asp:Label>
                </asp:View>
            </asp:MultiView>
        </div>
    </form>
</body>
</html>
