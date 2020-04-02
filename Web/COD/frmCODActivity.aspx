<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODActivity.aspx.vb"
    Inherits="COD_frmCODActivity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>COD Activity</title>
    <style type="text/css">
        .lblText
        {
            font-family:verdana;
            font-size:12px;
        }
        .lblBoldText
        {
            font-family:verdana;
            font-size:12px;
            font-weight:bolder;
        }
         .emptyRowStyle
        {
            font-family:verdana;
            font-size:8pt;
            font-weight:bolder;
            color:maroon;
            border-style:solid;
            padding:3px;
            border-width:1px;
            border-color:gray;
        }
        .GridHeader
        {
           background-color:#ffc90e;
           font-family:verdana;
           font-weight:bold;
           font-size:9pt;
           text-align:center;
           height:30px;
           color:white;
        }
        .GridOddRows
        {
           font-family:verdana;
           font-size:7.5pt;
        }
        .GridEvenRows
        {
           background-color:#cfcfcf;
           font-family:verdana;
           font-size:7.5pt;
        }
        .btnstyle
        {
            font-family:verdana;
            font-size:11px;
            font-weight:bolder;
            background-color:#c3c3c3;
            color:#fff;
            padding:8px;
            cursor:pointer;
            border-width:1px;
            border-color:white;
            border-style:solid;
            
        }
       
    </style>

    <script type="text/javascript">
        function ErrorSave()  
        {
            alert("Error While Saving the Data, Please Try Again!");
            return false;
        }
        function SucceedSave()  
        {
            alert("Data Successful saved");
            return true;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:MultiView ID="MvCorePanel" runat="server">
                <asp:View ID="VwListActivities" runat="server">
                    <div style="border-bottom-color: Gray; border-bottom-width: 1px; border-bottom-style: solid;
                        padding-bottom: 20px; width: 99%;">
                        <asp:GridView ID="GvActivities" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                            Width="100%">
                            <HeaderStyle CssClass="GridHeader" />
                            <RowStyle CssClass="GridOddRows" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/images/gridview/cancel.jpg"
                                            CommandName="DeleteActivity" Width="20px" Height="20px" CommandArgument='<%#Eval("ActivityId") %>'
                                            OnClientClick="return confirm('Are you sure you want to delete this Activity ?')" />
                                        <asp:ImageButton ID="ImgEdit" runat="server" ImageUrl="~/images/gridview/edit.jpg"
                                            CommandName="EditActivity" Width="20px" Height="20px" CommandArgument='<%#Eval("ActivityId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ActivityName" HeaderText="ActivityName" />
                                <asp:BoundField DataField="ActivityDesc" HeaderText="Description" />
                                <asp:BoundField DataField="LMDT" HeaderText="Last Modified Date" HtmlEncode="false"
                                    DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                                <asp:BoundField DataField="ModifiedUser" HeaderText="Last Modified By" />
                                <asp:BoundField DataField="CDT" HeaderText="Create Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                    ConvertEmptyStringToNull="true" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="VwModifiedActivity" runat="server">
                    <div style="width: 400px;">
                        <div style="background-color: #c3c3c3; border-style: solid; border-width: 1px; border-color: Gray;
                            padding: 10px; width: 97%;">
                            <span class="lblBoldText">Create / Update Master Activity </span>
                        </div>
                        <div>
                            <asp:HiddenField ID="HdnActivityId" runat="server" />
                            <table>
                                <tr>
                                    <td>
                                        <span class="lblBoldText">Activity Name</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtActivityName" runat="server" CssClass="lblText" Width="270px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvActivityName" runat="server" ControlToValidate="TxtActivityName"
                                            SetFocusOnError="true" ValidationGroup="form" Display="None" ErrorMessage="Activity Name is Required!"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="lblBoldText">Description</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtDescription" runat="server" TextMode="MultiLine" Height="30px"
                                            Width="265px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width: 100%; margin-top: 10px; text-align: right; border-top-color: Gray;
                            border-top-width: 1px; border-top-style: solid; padding-top: 10px;">
                            <asp:Button ID="BtnSave" runat="server" Text="Save" ValidationGroup="form" />
                            <asp:Button ID="BtnUpdate" runat="server" Text="Update" OnClientClick="return confirm('Are you sure you want to update this Activity ?')"
                                CausesValidation="false" />
                            <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CausesValidation="false" />
                            <asp:ValidationSummary ID="VsForm" runat="server" DisplayMode="List" HeaderText="Warning Message" ValidationGroup="form"
                                ToolTip="Warning Message" ShowMessageBox="true" ShowSummary="false" />
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
        <div style="margin-top: 10px; text-align: right; width: 99%;">
            <asp:Button ID="BtnAdd" runat="server" Text="Add" OnClick="BtnAdd_Click" CssClass="btnstyle"
                CausesValidation="false" Width="50px" />
        </div>
    </form>
</body>
</html>
