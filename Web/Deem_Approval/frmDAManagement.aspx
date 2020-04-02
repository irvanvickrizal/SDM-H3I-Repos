<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDAManagement.aspx.vb" Inherits="Deem_Approval_frmDAManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deem Approval Management</title>
    <style type="text/css">
        .ltrLabel {
            font-family: verdana;
            font-size: 11px;
            color: #000;
        }

        .lblField {
            font-family: verdana;
            font-size: 11px;
            font-weight: bolder;
            color: #000;
        }

        .lblFieldHeader {
            font-family: verdana;
            font-size: 13px;
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
            var result = confirm('Are you sure you want to delete deem approval configuration for ' + desc + ' ?');
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
            <span class="lblFieldHeader">Deem Approval Configuration</span>
        </div>
        <div style="margin-top: 10px;">
            <asp:UpdatePanel ID="UpGvTransactionType" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GvDeemApproval" runat="server" AutoGenerateColumns="false" DataKeyNames="ODDA_Id"
                        Width="100%" OnRowCancelingEdit="GvDeemApproval_RowCancelingEdit" EmptyDataText="No Record Found"
                        CellSpacing="2" CellPadding="2" OnRowDeleting="GvDeemApproval_RowDeleting" OnRowEditing="GvDeemApproval_RowEditing"
                        HeaderStyle-CssClass="HeaderGrid" OnRowUpdating="GvDeemApproval_RowUpdating">
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
                                    <asp:Label ID="LblTransactionTypeEdit" runat="server" Text='<%#Eval("Transaction_Type")%>' CssClass="ltrLabel"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblTransactionType" runat="server" Text='<%#Eval("Transaction_Type")%>' CssClass="ltrLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="Document">
                                <EditItemTemplate>
                                    <asp:Label ID="LblDocumentEdit" runat="server" CssClass="ltrLabel"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblDocument" runat="server" CssClass="ltrLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="User Group">
                                <EditItemTemplate>
                                    <asp:Label ID="LblUserGroupEdit" runat="server" Text='<%#Eval("GrpDesc")%>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblUserGroup" runat="server" Text='<%#Eval("GrpDesc")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="Total Doc">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtTotalDocEdit" runat="server" Text='<%#Eval("Total_Doc")%>' Width="50px"></asp:TextBox>
                                    <span class="ltrLabel">Doc(s)</span>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblTotalDoc" runat="server" Text='<%#Eval("Total_Doc")%>'></asp:Label>
                                    <span class="ltrLabel">Doc(s)</span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="SLA">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtSLADocEdit" runat="server" Text='<%#Eval("SLA_Doc")%>' Width="50px"></asp:TextBox>
                                    <span class="ltrLabel">Day(s)</span>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblSLADoc" runat="server" Text='<%#Eval("SLA_Doc")%>'></asp:Label>
                                    <span class="ltrLabel">Day(s)</span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="Warning Day">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtSLAWarningEdit" runat="server" Text='<%#Eval("SLA_Notif_Warning_Day")%>' Width="50px"></asp:TextBox>
                                    <span class="ltrLabel">Day(s)</span>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblSLAWarning" runat="server" Text='<%#Eval("SLA_Notif_Warning_Day")%>'></asp:Label>
                                    <span class="ltrLabel">Day(s)</span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="Execute Day">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtSLAExecuteDayEdit" runat="server" Text='<%#Eval("Execute_Doc_Exceed_SLA")%>' Width="50px"></asp:TextBox>
                                    <span class="ltrLabel">Day(s)</span>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblSLAExecuteDay" runat="server" Text='<%#Eval("Execute_Doc_Exceed_SLA")%>'></asp:Label>
                                    <span class="ltrLabel">Day(s)</span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="HasDocGroup" HeaderText="Has Group" ReadOnly="true" ControlStyle-CssClass="ltrLabel" />
                            <asp:TemplateField ShowHeader="true" HeaderText="Doc Linked as Group">
                                <EditItemTemplate>
                                    <asp:Label ID="LblDocumentGroupEdit" runat="server" CssClass="ltrLabel"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblDocumentGroup" runat="server" CssClass="ltrLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="LMBY" HeaderText="Modified User" ReadOnly="true" ControlStyle-CssClass="ltrLabel" />
                            <asp:BoundField DataField="LMDT" HeaderText="Modified Date" ReadOnly="true" ControlStyle-CssClass="ltrLabel"
                                HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy hh:mm:ss}" ConvertEmptyStringToNull="true" />
                        </Columns>
                    </asp:GridView>
                    <div style="margin-top: 5px;">
                        <asp:Label ID="LblErrMessageGv" runat="server" CssClass="lblField"></asp:Label>
                    </div>
                    <div style="width: 100%;">
                        <hr />
                    </div>
                    <div style="margin-top: 15px; border-style: solid; border-color: Gray; border-width: 2px; padding: 3px; width: 550px;">
                        <table>
                            <tr>
                                <td>
                                    <span class="ltrLabel">Transaction Type</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlTransactionTypes" runat="server" CssClass="ltrLabel" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="ltrLabel">Document</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlDocuments" runat="server" CssClass="ltrLabel" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="panelconfirmdocgroup" runat="server">
                                <td>
                                    <span class="ltrLabel">Has Doc Group</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlHasDocGroup" runat="server" CssClass="ltrLabel" AutoPostBack="true">
                                        <asp:ListItem Text="--select--" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="paneldocgroup" runat="server">
                                <td>
                                    <span class="ltrLabel">Doc Linked as Group Related</span>
                                </td>
                                <td>
                                   <asp:DropDownList ID="DdlDocumentGroup" runat="server" CssClass="ltrLabel"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="ltrLabel">User Type</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlUserTypes" runat="server" CssClass="ltrLabel"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="ltrLabel">Task Role</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlTasks" runat="server" CssClass="ltrLabel">
                                        <asp:ListItem Text="--select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Reviewer" Value="reviewer"></asp:ListItem>
                                        <asp:ListItem Text="Approver" Value="approver"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="ltrLabel">Total Doc per Day</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtTotalDoc" runat="server" Width="30px" CssClass="ltrLabel"></asp:TextBox><span class="ltrLabel">Doc(s)</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="ltrLabel">SLA Day</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtSLADoc" runat="server" Width="30px" CssClass="ltrLabel"></asp:TextBox><span class="ltrLabel">Day(s)</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="ltrLabel">Warning Notification Before SLA</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtWarningNotif" runat="server" Width="30px" CssClass="ltrLabel"></asp:TextBox><span class="ltrLabel">Day(s)</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="ltrLabel">Execute after SLA</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtExecuteAfterSLA" runat="server" Width="30px" CssClass="ltrLabel"></asp:TextBox><span class="ltrLabel">Day(s)</span>
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
                    <asp:AsyncPostBackTrigger ControlID="LbtSave" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
