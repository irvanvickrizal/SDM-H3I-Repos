<%@ Page Language="VB" AutoEventWireup="false" CodeFile="fb_ViewHistoricalWCCDeletion.aspx.vb"
    Inherits="fancybox_Form_fb_ViewHistoricalWCCDeletion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCC Historical Deletion</title>
    <style type="text/css">
        #panelHeader
        {
            background-color: #cfcfcf;
            padding: 5px;
            font-family: verdana;
            font-size: 15px;
            font-weight: bolder;
            color: #ffffff;
            width: 99%;
            border-radius: 3px;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
        }
        .gridHeader_2
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: #ffc727;
            font-weight: bolder;
            text-align: center;
            padding: 5px;
            color: white;
            border-style: solid;
            border-width: 1px;
            border-color: gray;
        }
        .gridOdd
        {
            font-family: Verdana;
            font-size: 11px;
            padding: 5px;
            border-style: solid;
            border-width: 1px;
            border-color: gray;
        }
        .gridEven
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: #cfcfcf;
            font-weight: bolder;
            text-align: center;
            padding: 5px;
            border-style: solid;
            border-width: 1px;
            border-color: gray;
        }
        .lblText
        {
            font-family: Verdana;
            font-size: 11px;
        }
        .lblBoldText
        {
            font-family: Verdana;
            font-size: 11px;
            font-weight: bolder;
        }
        .btnSearch
        {
            height: 29px;
            width: 80px;
            background-image: url(../Images/button/BtSearch_0.gif);
        }
        .btnSearch:hover
        {
            height: 29px;
            width: 80px;
            background-image: url(../Images/button/BtSearch_1.gif);
        }
        .btnSearch:click
        {
            height: 29px;
            width: 80px;
            background-image: url(../Images/button/BtSearch_2.gif);
        }
        #PleaseWait
        {
            z-index: 200;
            position: absolute;
            top: 0pt;
            left: 0pt;
            text-align: center;
            height: 100px;
            width: 100px;
            background-image: url(../Images/preloader.gif);
            background-repeat: no-repeat;
            margin: 0 10%;
            margin-top: 10px;
        }
        #blur
        {
            width: 100%;
            background-color: #ffffff;
            moz-opacity: 0.7;
            khtml-opacity: .7;
            opacity: .7;
            filter: alpha(opacity=70);
            z-index: 120;
            height: 100%;
            position: absolute;
            top: 0;
            left: 0;
        }
    </style>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(
        function () {
                if (document.getElementById) {
                    var progress = document.getElementById('PleaseWait');
                    var blur = document.getElementById('blur');
                    progress.style.width = '300px';
                    progress.style.height = '30px';
                    blur.style.height = document.documentElement.clientHeight;
                    progress.style.top = document.documentElement.clientHeight/3 - progress.style.height.replace('px','')/2 + 'px';
                    progress.style.left = document.body.offsetWidth/2 - progress.style.width.replace('px','')/2 + 'px';
                }
            }
        )
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="SM1" runat="server">
    </asp:ScriptManager>
    <div id="panelHeader">
        WCC Historical of Deletion
    </div>
    <div style="height: 400px; overflow: auto; margin-top:10px;">
        <div style="border-bottom-style: solid; border-bottom-color: Gray; border-bottom-width: 2px;
            width: 100%;">
            <table>
                <tr>
                    <td>
                        <span class="lblBoldText">Package Id</span>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPackageId" runat="server" CssClass="lblText"></asp:TextBox>
                    </td>
                    <td>
                        <span class="lblBoldText">PO Subcontractor</span>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPOSubcontractor" runat="server" CssClass="lblText"></asp:TextBox>
                    </td>
                    <td>
                        <asp:LinkButton ID="LbtSearch" runat="server" Width="80px" ValidationGroup="wpidsearch"
                            Style="text-decoration: none; cursor: pointer;">
                        <div class="btnSearch"></div>  
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-top: 10px;">
            <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="Up1">
                <ProgressTemplate>
                    <div id="blur">
                        <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                            <img src="../Images/preloader.gif" style="vertical-align: middle" alt="Processing" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="Up1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GvWCCDeletion" runat="server" AutoGenerateColumns="false" EmptyDataText="No record Found" AllowPaging="true" PageSize="15">
                        <HeaderStyle CssClass="gridHeader_2" />
                        <RowStyle CssClass="gridOdd" />
                        <AlternatingRowStyle CssClass="gridEven" />
                        <Columns>
                            <asp:BoundField DataField="SiteNo" HeaderText="SiteNo" />
                            <asp:BoundField DataField="SiteName" HeaderText="SiteName" />
                            <asp:BoundField DataField="PONO" HeaderText="Po.No" />
                            <asp:BoundField DataField="PackageId" HeaderText="PackageId" />
                            <asp:BoundField DataField="POSubcontractor" HeaderText="PO Subcon" />
                            <asp:BoundField DataField="IssuanceDate" HeaderText="Issuance Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                            <asp:BoundField DataField="CertificateNumber" HeaderText="Certificate No." />
                            <asp:BoundField DataField="Scope" HeaderText="Scope" />
                            <asp:BoundField DataField="SubconName" HeaderText="Subcon" />
                            <asp:BoundField DataField="LMDT" HeaderText="Upload date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                ConvertEmptyStringToNull="true" />
                            <asp:BoundField DataField="WCCDeletedDate" HeaderText="Deleted Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                            <asp:BoundField DataField="WCCDeletionRemarks" HeaderText="Remarks of Deletion" />
                            <asp:BoundField DataField="UserDeletion" HeaderText="User Deletion" />
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="LbtSearch" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
