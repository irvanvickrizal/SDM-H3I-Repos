<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTransactionType.aspx.vb" Inherits="Deem_Approval_frmTransactionType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transaction Type</title>
    <style type="text/css">
        .ltrLabel {
            font-family: verdana;
            font-size: 8pt;
            color: #000;
        }

        .lblField {
            font-family: verdana;
            font-size: 8pt;
            font-weight: bolder;
            color: #000;
        }

        .lblFieldHeader {
            font-family: verdana;
            font-size: 10pt;
            font-weight: bolder;
            color: #000;
        }

        .HeaderGrid {
            font-family: Arial Unicode MS;
            font-size: 8pt;
            font-weight: bold;
            color: white;
            background-color: Orange;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            vertical-align: middle;
        }

        .oddGrid {
            font-family: Arial Unicode MS;
            font-size: 7.5pt;
            background-color: White;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }

        .evenGrid {
            font-family: Arial Unicode MS;
            font-size: 7.5pt;
            background-color: #cfcfcf;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }

        .btnSave {
            height: 27px;
            width: 60.5px;
            background-image: url(../Images/button/BtnSave_0.gif);
        }

            .btnSave:hover {
                height: 27px;
                width: 60.5px;
                background-image: url(../Images/button/BtnSave_1.gif);
            }

            .btnSave:click {
                height: 27px;
                width: 60.5px;
                background-image: url(../Images/button/BtnSave_2.gif);
            }
    </style>

    <script type="text/javascript">
        function ConfirmationBox(desc) {
            var result = confirm('Are you sure you want to delete ' + desc + ' ?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div style="background-color: #c3c3c3; padding: 8px;">
            <span class="lblFieldHeader">Master Transaction Type</span>
        </div>
        <div style="margin-top: 10px;">
            <asp:UpdatePanel ID="UpGvTransactionType" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GvTransactionType" runat="server" AutoGenerateColumns="false" DataKeyNames="Trans_Id"
                        Width="100%" OnRowCancelingEdit="GvTransactionType_RowCancelingEdit" EmptyDataText="No Record Found"
                        CellSpacing="2" CellPadding="2" OnRowDeleting="GvTransactionType_RowDeleting" OnRowEditing="GvTransactionType_RowEditing"
                        HeaderStyle-CssClass="HeaderGrid" OnRowUpdating="GvTransactionType_RowUpdating">
                        <RowStyle CssClass="oddGrid" />
                        <AlternatingRowStyle CssClass="evenGrid" />
                        <EmptyDataRowStyle CssClass="evenGrid" />
                        <Columns>
                            <asp:TemplateField ShowHeader="false" HeaderStyle-BackColor="white" ItemStyle-Width="50px"
                                ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/gridview/update.jpg"
                                        ToolTip="Update" Height="16px" Width="16px" OnClientClick="return confirm('Are you sure you want to Update?');" />
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/gridview/Cancel.jpg"
                                        ToolTip="Cancel" Height="16px" Width="16px" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/gridview/Edit.jpg"
                                        ToolTip="Edit" Height="16px" Width="16px" />
                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Images/trash.png"
                                        ToolTip="Delete" Height="16px" Width="16px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="Transaction Type">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtTransactionTypeEdit" Width="250px" runat="server" Text='<%#Eval("Transaction_Type") %>' CssClass="ltrLabel"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblTransactionType" runat="server" Text='<%#Eval("Transaction_Type")%>' CssClass="ltrLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtTransactionDescEdit" Width="250px" runat="server" Text='<%#Eval("Trans_Description") %>' CssClass="ltrLabel"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblTransactionDesc" runat="server" Text='<%#Eval("Trans_Description")%>' CssClass="ltrLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="Trans Table">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtTransactionTableEdit" Width="200px" runat="server" Text='<%#Eval("Trans_Table") %>' CssClass="ltrLabel"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="TxtTransactionTable" runat="server" Text='<%#Eval("Trans_Table")%>' CssClass="ltrLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="Master Doc Table" ItemStyle-Width="150px">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtCODDocTableEdit" Width="150px" runat="server" Text='<%#Eval("CODDoc_Table")%>' CssClass="ltrLabel"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="TxtCODDocTable" runat="server" Text='<%#Eval("CODDoc_Table")%>' CssClass="ltrLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="Parent Doc Id">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DdlParentDocsGvEdit" runat="server" CssClass="ltrLabel">
                                        <asp:ListItem Text="--select--" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="BAST" Value="1032"></asp:ListItem>
                                        <asp:ListItem Text="SOAC" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="WCC" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="DdlParentDocsGv" runat="server" CssClass="ltrLabel">
                                        <asp:ListItem Text="--select--" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="BAST" Value="1032"></asp:ListItem>
                                        <asp:ListItem Text="SOAC" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="WCC" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="lmby" HeaderText="Modified User" ReadOnly="true" />
                            <asp:BoundField DataField="LMDT" HeaderText="Modified Date" ReadOnly="true" />
                        </Columns>
                    </asp:GridView>
                    <div style="margin-top: 5px;">
                        <asp:Label ID="LblErrMessageGv" runat="server" CssClass="lblField"></asp:Label>
                    </div>
                    <div style="width: 100%;">
                        <hr />
                    </div>
                    <div style="margin-top: 15px; border-style: solid; border-color: Gray; border-width: 2px; padding: 3px; width: 350px;">
                        <table>
                            <tr>
                                <td>
                                    <span class="ltrlabel">Transaction Type</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlTransactionType" runat="server" CssClass="ltrLabel">
                                        <asp:ListItem Text="--select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="General" Value="General"></asp:ListItem>
                                        <asp:ListItem Text="Change Request(CR)" Value="Change Request"></asp:ListItem>
                                        <asp:ListItem Text="Change Order(CO)" Value="Change Order"></asp:ListItem>
                                        <asp:ListItem Text="SOAC" Value="SOAC"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <span class="ltrlabel">Description</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtTransDescription" TextMode="MultiLine" runat="server" CssClass="ltrLabel" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="ltrlabel">Transaction Table</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtTransTable" runat="server" CssClass="lblText" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="ltrlabel">Master Doc Table</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtMasterDocTable" runat="server" CssClass="ltrLabel" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="ltrlabel">Parent Doc</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlParentDocs" runat="server" CssClass="ltrLabel">
                                        <asp:ListItem Text="--select--" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="BAST" Value="1032"></asp:ListItem>
                                        <asp:ListItem Text="SOAC" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="WCC" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <div style="margin-top: 5px; width: 91%; text-align: right; margin-right: 20px;">
                            <asp:LinkButton ID="LbtSave" runat="server" OnClientClick="return confirm('are you sure you want to save this data?');"
                                ValidationGroup="injection" Width="60.5px" Style="text-decoration: none; cursor: pointer;">
                                    <div class="btnSave"></div>
                            </asp:LinkButton>
                        </div>
                        <div style="margin-top: 5px;">
                            <asp:Label ID="LblErrorMessage" runat="server" ForeColor="Red" Font-Names="verdana" Font-Size="11px"></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GvTransactionType" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

    </form>
</body>
</html>
