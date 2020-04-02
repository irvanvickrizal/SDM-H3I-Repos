<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMasterBusinessDate.aspx.vb" Inherits="Deem_Approval_frmMasterBusinessDate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Master Business Day</title>
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
            <span class="lblFieldHeader">Master Business Day</span>
        </div>
        <div style="margin-top: 10px;">
            <asp:UpdatePanel ID="UpGvTransactionType" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GvBusinessDay" runat="server" AutoGenerateColumns="false" DataKeyNames="BD_Id"
                        Width="100%" OnRowCancelingEdit="GvBusinessDay_RowCancelingEdit" EmptyDataText="No Record Found"
                        CellSpacing="2" CellPadding="2" OnRowDeleting="GvBusinessDay_RowDeleting" OnRowEditing="GvBusinessDay_RowEditing"
                        HeaderStyle-CssClass="HeaderGrid" OnRowUpdating="GvBusinessDay_RowUpdating">
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
                                    <asp:TextBox ID="TxtOffDayEdit" Width="250px" runat="server" Text='<%#String.Format("{0:dd-MMMM-yyyy}", Eval("Off_Date"))%>' CssClass="ltrLabel"></asp:TextBox>
                                    <asp:ImageButton ID="ImgOffDateEdit" runat="server" ImageUrl="~/images/calendar_icon.jpg" Width="20px" Height="20px" />
                                    <cc1:CalendarExtender ID="ceStartTimeEdit" runat="server" Format="dd-MMMM-yyyy" PopupButtonID="ImgOffDateEdit"
                                        TargetControlID="TxtOffDayEdit">
                                    </cc1:CalendarExtender>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblOffDay" runat="server" Text='<%#String.Format("{0:dd-MMMM-yyyy}",Eval("Off_Date"))%>' CssClass="ltrLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtDescEdit" Width="250px" runat="server" Text='<%#Eval("BD_Description") %>' CssClass="ltrLabel"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblDesc" runat="server" Text='<%#Eval("BD_Description")%>' CssClass="ltrLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="lmby" HeaderText="Modified User" ReadOnly="true" />
                            <asp:BoundField DataField="LMDT" HeaderText="Modified Date" ReadOnly="true" HtmlEncode="false" DataFormatString="{0:dd-MMMM-yyyy}" ConvertEmptyStringToNull="true" />
                        </Columns>
                    </asp:GridView>
                    <div style="margin-top: 5px;">
                        <asp:Label ID="LblErrMessageGv" runat="server" CssClass="lblField"></asp:Label>
                    </div>
                    <div style="width: 100%;">
                        <hr />
                    </div>
                    <div style="margin-top: 15px; border-style: solid; border-color: Gray; border-width: 2px; padding: 3px; width: 400px;">
                        <table>
                            <tr>
                                <td>
                                    <span class="ltrLabel">Off Date</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtOffDate" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="ImgOffDate" runat="server" ImageUrl="~/images/calendar_icon.jpg" Width="20px" Height="20px" />
                                    <cc1:CalendarExtender ID="ceStartTime" runat="server" Format="dd-MMMM-yyyy" PopupButtonID="ImgOffDate"
                                        TargetControlID="TxtOffDate">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <span class="ltrLabel">Description</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtDescription" runat="server" Width="300px" Height="40px" TextMode="MultiLine"></asp:TextBox>
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
                    <asp:AsyncPostBackTrigger ControlID="GvBusinessDay" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
