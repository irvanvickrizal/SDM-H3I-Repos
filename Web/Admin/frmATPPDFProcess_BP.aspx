<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmATPPDFProcess_BP.aspx.vb"
    Inherits="Admin_frmATPPDFProcess_BP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ATP Background Process</title>
    <style type="text/css">
        .HeaderReport
        {
            background-color:#cfcfcf;
            font-family:verdana;
            font-size:13px;
            font-weight:bold;
            margin-top:15px;
            margin-bottom:10px;
            padding:3px;
        }
        .GridHeader
        {
           background-color:#ffc90e;
           font-family:verdana;
           font-weight:bold;
           font-size:10pt;
           text-align:center;
           height:30px;
           color:white;
        }
        .GridOddRows
        {
           font-family:verdana;
           font-size:8pt;
        }
        .GridEvenRows
        {
           background-color:#cfcfcf;
           font-family:verdana;
           font-size:8pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <span style="font-family: Verdana; font-size: 8pt;">Search By</span>
                <asp:DropDownList ID="DdlSearchBy" runat="server" AutoPostBack="true" CssClass="GridEvenRows" OnSelectedIndexChanged="DdlSearchBy_IndexChanged">
                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Success" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Failed" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="margin-top:20px">
                <asp:GridView ID="GvATPPipelineProcess" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="GridHeader"
                    AllowPaging="false" CellPadding="2" CellSpacing="2" EmptyDataText="No Record Found">
                    <RowStyle CssClass="GridOddRows" />
                    <AlternatingRowStyle CssClass="GridEvenRows" />
                    <EmptyDataRowStyle CssClass="GridEvenRows" />
                    <Columns>
                        <asp:BoundField DataField="Package_id" HeaderText="Package Id" />
                        <asp:BoundField DataField="Original_Filename" HeaderText="Original Filename" />
                        <asp:BoundField DataField="Original_Path" HeaderText="Path" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="LMDT" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                            ConvertEmptyStringToNull="true" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LbtGenerate" runat="server" Text="Generate PDF" CommandName="GeneratePDF"
                                    CommandArgument='<%#Eval("TaskPending_Id") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
