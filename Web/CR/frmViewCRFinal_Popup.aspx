<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmViewCRFinal_Popup.aspx.vb" Inherits="CR_frmViewCRFinal_Popup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View CR Final</title>
    <style type="text/css">
        .HeaderReport
        {
            background-color:#a3a3a3;
            font-family:verdana;
            font-size:13px;
            font-weight:bold;
            padding:3px;
        }
        .BtnExpt
        {
           border-style:solid;
           border-color:white;
           border-width:1px;
           font-family:verdana;
           font-size:11px;
           font-weight:bold;
           color:white;
           width:120px;
           height:25px;
           cursor:hand;
        }
        .HeaderGrid
        {
            font-family: Arial Unicode MS;
            font-size: 8pt;
            font-weight: bold;
            color: White;
            background-color: #c3c3c3;
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
            color:black;
            font-size: 9pt;
            background-color:#ffffff;
        }
        .evenGrid3
        {
            font-family: Arial Unicode MS;
            color:black;
            font-size: 9pt;
            background-color: #e0e0e0;
        }
        .subHeader
        {
            font-family: Arial;
            font-size: 12pt;
            font-weight:bold;
        }
        .lblText
        {
            color:black;
            font-size:11pt;
        }
    </style>
    <style type="text/css">
        #accordion { list-style: none; margin: -15px 0; padding: 0; height: 500px; overflow: hidden; background: #cfcfcf;}   
        #accordion li { float: left; border-left: display: block; height: 500px; width: 30px; padding: 15px 0; overflow: hidden; color: #fff; text-decoration: none; font-size: 16px; line-height: 1.5em; border-left: 1px solid #fff;}   
        #accordion li img { border: none; border-right: 1px solid #fff; float: left; margin: -15px 15px 0 0; }   
        #accordion li.active { width: 97%; } 
        #docHeader,#logHeader {cursor:hand;}
    </style>

    <script type="text/javascript" src="../js/jquery-1.4.1.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function(){       
            activeItem = $("#accordion li:first");     
            $(activeItem).addClass('active');       
            $("#accordion li").click(function(){         
            $(activeItem).animate({width: "30px"}, 
            {duration:300, queue:false});         
            $(this).animate({width: "97%"}, 
            {duration:300, queue:false});         
            activeItem = this;     });   }); 
    </script>
    <script type="text/javascript">
        function invalidExportToExcel() {
            alert('Data is empty, please try again!');
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ul id="accordion">
                <li>
                    <img src="~/images/accordion/CRDocumentHeader.png" id="docHeader" runat="server"
                        alt="docHeader" />
                    <strong><span class="subHeader">Document View</span></strong>
                    <div style="width: 99%">
                        <table>
                            <tr valign="top">
                                <td style="width: 72%">
                                    <iframe runat="server" id="docView" width="99%" height="450px" scrolling="auto"></iframe>
                                </td>
                                <td style="width: 29%">
                                    <table>
                                        <tr class="lblText">
                                            <td style="width: 25%">
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
                                                WorkPackageID</td>
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
                                </td>
                            </tr>
                        </table>
                    </div>
                </li>
                <li>
                    <img src="~/images/accordion/ViewLogHeader.png" id="logHeader" runat="server" alt="logHeader" />
                    <div style="width: 95%; float: left; background-color: #cfcfcf;">
                        <div class="HeaderReport">
                            <table width="100%">
                                <tr>
                                    <td style="width: 80%">
                                        List CR Approved
                                    </td>
                                    <td style="width: 15%; text-align: right;">
                                        <asp:Button ID="BtnExportListCRApproved" runat="server" Text="Export To Excel" CssClass="BtnExpt"
                                            BackColor="#7f7f7f" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="background-color: #cfcfcf;height:180px;overflow:scroll; width:100%;">
                        <asp:GridView ID="GvListCR" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="HeaderGrid"
                            Width="100%" CellPadding="1" CellSpacing="1" EmptyDataText="No Record CR Found">
                            <RowStyle CssClass="evenGrid" />
                            <AlternatingRowStyle CssClass="evenGrid3" />
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
                                    NullDisplayText="Nothing" ItemStyle-Font-Size="9pt" HeaderStyle-CssClass="HeaderGrid"
                                     />
                                <asp:BoundField DataField="Description_of_Change" HeaderText="Desc of Change" ConvertEmptyStringToNull="true"
                                    NullDisplayText="Nothing" ItemStyle-Font-Size="9pt" HeaderStyle-CssClass="HeaderGrid"
                                     />
                                <asp:BoundField DataField="IndicativePriceCost_USD" HeaderText="Indicative Price(USD)"
                                    ConvertEmptyStringToNull="true" NullDisplayText="Nothing" ItemStyle-Font-Size="9pt"
                                    HeaderStyle-CssClass="HeaderGrid" />
                                <asp:BoundField DataField="IndicativePriceCost_IDR" HeaderText="Indicative Price(IDR)"
                                    ConvertEmptyStringToNull="true" NullDisplayText="Nothing" ItemStyle-Font-Size="9pt"
                                    HeaderStyle-CssClass="HeaderGrid" />
                                <asp:BoundField DataField="PercentagePriceChange_USD" HeaderText="Percentage Price(USD)"
                                    ConvertEmptyStringToNull="true" NullDisplayText="Nothing" ItemStyle-Font-Size="9pt"
                                    ItemStyle-Width="60px" HeaderStyle-CssClass="HeaderGrid" />
                                <asp:BoundField DataField="PercentagePriceChange_IDR" HeaderText="Percentage Price(IDR)"
                                    ConvertEmptyStringToNull="true" NullDisplayText="Nothing" ItemStyle-Font-Size="9pt"
                                    ItemStyle-Width="60px" HeaderStyle-CssClass="HeaderGrid" />
                                <asp:BoundField DataField="CRStatus" HeaderText="Status" ConvertEmptyStringToNull="true"
                                    NullDisplayText="Nothing" ItemStyle-Font-Size="9pt" HeaderStyle-CssClass="HeaderGrid"
                                     />
                                <asp:BoundField DataField="InitiatorName" HeaderText="InitiatorName" ConvertEmptyStringToNull="true"
                                    NullDisplayText="Nothing" ItemStyle-Font-Size="9pt" HeaderStyle-CssClass="HeaderGrid"
                                    />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="18px">
                                    <ItemTemplate>
                                        <br />
                                         <a href="#" style="text-decoration: none; border-width: 0px;" onclick="window.open('../po/frmViewCRDocument.aspx?crid=<%# DataBinder.Eval(Container.DataItem,"CR_ID") %>&wpid=<%# DataBinder.Eval(Container.DataItem,"Package_Id") %>','','Width=850,height=500');">
                                            <img src="../Images/Pdf_Icon.png" alt="pdficon" id="pdfIcon" height="18" width="18"
                                                style="text-decoration: none; border-width: 0px;" runat="server" />
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </div>
                    </div>
                    <div style="width: 95%; float: left; background-color: #cfcfcf;height:220px;">
                        <div class="HeaderReport">
                            <table width="100%">
                                <tr>
                                    <td style="width: 80%">
                                        Historical CR Log
                                    </td>
                                    <td style="width: 15%; text-align: right;">
                                        <asp:Button ID="BtnCRLogExport" runat="server" Text="Export To Excel" CssClass="BtnExpt"
                                            BackColor="#7f7f7f" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="background-color: #cfcfcf;height:220px;overflow:scroll; width:100%;">
                        <asp:GridView ID="GvSummaryViewLog" runat="server" CellPadding="1" CellSpacing="1" HeaderStyle-CssClass="HeaderGrid"
                            BorderWidth="1px" AutoGenerateColumns="false" Width="100%"
                            EmptyDataText="No Records Found">
                            <RowStyle CssClass="evenGrid" />
                            <AlternatingRowStyle CssClass="evenGrid3" />
                            <EmptyDataRowStyle CssClass="lblSubHeader" ForeColor="red" />
                            <Columns>
                                <asp:TemplateField HeaderText=" No. " ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="HeaderGrid">
                                    <ItemTemplate >
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="CRNo" DataField="CRNo"
                                     ItemStyle-Font-Size="9pt" HeaderStyle-CssClass="HeaderGrid"/>
                                    
                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Event" DataField="AuditInfo"
                                    ItemStyle-Font-Size="9pt" HeaderStyle-CssClass="HeaderGrid"/>
                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Name" DataField="User"
                                    ItemStyle-Font-Size="9pt" HeaderStyle-CssClass="HeaderGrid" />
                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Type" DataField="UserType"
                                    ItemStyle-Font-Size="9pt" HeaderStyle-CssClass="HeaderGrid" />
                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Role" DataField="SignTitle"
                                    ItemStyle-Font-Size="9pt" HeaderStyle-CssClass="HeaderGrid" />
                                <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Start Date Time" DataField="EventStartTime"
                                    DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Font-Size="9pt" HeaderStyle-CssClass="HeaderGrid" />
                                <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="End Date Time" DataField="EventEndTime"
                                    DataFormatString="{0:dd-MMM-yyyy}"  ItemStyle-Font-Size="9pt" HeaderStyle-CssClass="HeaderGrid" />
                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Remarks" DataField="Remarks"
                                    ItemStyle-Font-Size="9pt" HeaderStyle-CssClass="HeaderGrid" />
                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Categories" DataField="Categories"
                                    ItemStyle-Font-Size="9pt" HeaderStyle-CssClass="HeaderGrid" />
                            </Columns>
                        </asp:GridView>
                        </div>
                    </div>
                </li>
            </ul>
        </div>
    </form>
</body>
</html>
