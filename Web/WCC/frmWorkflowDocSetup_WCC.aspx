<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWorkflowDocSetup_WCC.aspx.vb"
    Inherits="WCC_frmWorkflowDocSetup_WCC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCC Workflow Grouping</title>
    <link href="../CSS/MasterFormManagement.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="background-color: #c3c3c3; height: 25px;padding:3px;">
                <div style="padding:3px; margin-top:3px; margin-left:3px;">
                    <span class="lblField">WCC Workflow Registration</span>
                </div>
            </div>
            <div style="margin-top: 10px;">
                <asp:GridView ID="GvWCCWorkflow" runat="server" AutoGenerateColumns="false" CellPadding="2"
                    Width="99%" EmptyDataText="No Record Found" CellSpacing="2">
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="GridEvenRows" />
                    <RowStyle CssClass="GridOddRows" />
                    <EmptyDataRowStyle CssClass="emptyRowStyle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnDelete" CommandName="deleteworkflow" runat="server" CommandArgument='<%#Eval("FlowGroupingId") %>'
                                    ImageUrl="~/Images/gridview/delete.jpg" ToolTip="Delete" Height="16px" Width="16px"
                                    OnClientClick="return confirm('Are you sure you want to delete this data ?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FlowName" HeaderText="WorkFlow" />
                        <asp:BoundField DataField="LMBY" HeaderText="Last Modified" />
                        <asp:BoundField DataField="LMDT" HeaderText="Last Modified Date" HtmlEncode="false"
                            DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <hr />
        <div style="margin-top: 10px;">
            <asp:GridView ID="GvWorkflows" runat="server" AutoGenerateColumns="false" CellPadding="2"
                AllowPaging="true" PageSize="5" CellSpacing="2">
                <HeaderStyle CssClass="GridHeader2" />
                <AlternatingRowStyle CssClass="GridEvenRows" />
                <RowStyle CssClass="GridOddRows" />
                <EmptyDataRowStyle CssClass="emptyRowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnAdd" CommandName="addflow" runat="server" CommandArgument='<%#Eval("WFID") %>'
                                ImageUrl="~/Images/gridview/AddNewItem.jpg" ToolTip="Add Workflow" Height="16px"
                                Width="16px" OnClientClick="return confirm('Are you sure you want to add this workflow as group of WCC Flow ?')" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FlowName" HeaderText="Workflow" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
