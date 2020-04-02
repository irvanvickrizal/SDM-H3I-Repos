<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmUserActivityLog.aspx.vb" Inherits="frmUserActivityLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Activity Log</title>
    <link type="text/css" rel="stylesheet" href="../CSS/Styles.css" />
    <style type="text/css">
        .headerpanel {
            background-color: gray;
            padding: 4px;
            padding-left: 10px;
            color: white;
            font-family: Trebuchet MS;
            font-size: 16px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 99%; height: 450px;">
            <div class="headerpanel">
                User Activity Log
            </div>
            <div style="margin-top: 5px; text-align: left;">
                <asp:GridView ID="grdActivityLog" runat="server" AutoGenerateColumns="False" Width="100%"
                    AllowPaging="True" PageSize="15">
                    <PagerSettings Position="Bottom" />
                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" Height="20px" />
                    <AlternatingRowStyle CssClass="GridEvenRows" />
                    <RowStyle CssClass="GridOddRows" />
                    <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                    <Columns>
                        <asp:TemplateField HeaderText=" No. ">
                            <ItemTemplate>
                                <%# container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="IPAddress" HeaderText="IP Address" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="ActivityDate" HeaderText="Execution Date" HtmlEncode="false"
                            DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </form>
</body>
</html>
