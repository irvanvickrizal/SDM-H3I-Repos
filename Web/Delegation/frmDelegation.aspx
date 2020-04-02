<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDelegation.aspx.vb" Inherits="Delegation_frmDelegation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delegation Form</title>
    <style type="text/css">
        .headerpanel
        {
            width:98%;
            padding:8px;
            font-family:verdana;
            font-size:13px;
            color:black;
            border-width:1px;
            border-color:gray;
            border-style:solid;
            background-color:#cfcfcf;
            margin-bottom:15px;
        }
        .lblText
        {
            font-family:verdana;
            font-size:11px;
        }
        .lblBoldText
        {
            font-family:verdana;
            font-size:11px;
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
           font-size:10pt;
           text-align:center;
           height:30px;
           color:white;
        }
        .GridOddRows
        {
           font-family:verdana;
           font-size:9pt;
           padding:3px;
        }
        .GridEvenRows
        {
           background-color:#cfcfcf;
           font-family:verdana;
           font-size:9pt;
           padding:3px;
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
         #PleaseWait
        {
            z-index: 200;
            position: absolute;
            top: 0pt;
            left: 0pt;
            text-align:center;
            height : 100px;
            width:100px;
            background-image: url(../Images/animation_processing.gif);
            background-repeat: no-repeat;
            margin: 0 10%; margin-top: 10px;
        }
        #blur
        {
            width: 100%;
            background-color:#ffffff;
            moz-opacity: 0.7;
            khtml-opacity: .7;
            opacity: .7;
            filter: alpha(opacity=70);
            z-index: 1;
            height: 100%;
            position:fixed;
            top: 0;
            left: 0;
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

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(
        function () {
                if (document.getElementById) {
                    var progress = document.getElementById('PleaseWait');
                    var blur = document.getElementById('blur');
                    progress.style.width = '100%';
                    progress.style.height = '100%';
                }
            }
        )
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div style="width: 100%;">
            <div class="headerpanel">
                <span>User Delegation Form</span>
            </div>
        </div>
        <div>
            <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UP1">
                <ProgressTemplate>
                    <div id="blur">
                        <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                            <img src="../Images/animation_processing.gif" style="vertical-align: middle" width="150"
                                height="150" alt="Processing" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UP1" runat="server">
                <ContentTemplate>
                    <div>
                        <asp:GridView ID="GvDelegations" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" AllowPaging="true" PageSize="10"
                            Width="99%">
                            <HeaderStyle CssClass="GridHeader" />
                            <RowStyle CssClass="GridOddRows" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <EmptyDataRowStyle CssClass="emptyRowStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemStyle Width="35px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="userdelegationname" HeaderText="Delegated User" />
                                <asp:BoundField DataField="CDT" HeaderText="Start Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                    NullDisplayText="Not Yet" ConvertEmptyStringToNull="true" />
                                <asp:BoundField DataField="LMDT" HeaderText="Finish Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                    NullDisplayText="Not Yet" ConvertEmptyStringToNull="true" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="border-style:solid; border-color:Gray; border-width:2px; padding:3px; margin-top:10px; width:350px;">
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        <span class="lblBoldText">Delegate User</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlDelegateUsers" runat="server" CssClass="lblText">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-top: 10px; text-align:right; margin-right:10px;">
                            <div>
                                <asp:Label ID="LblWarningMessage" runat="server" CssClass="lblText" ForeColor="green" Font-Italic="true" Font-Bold="true"></asp:Label>
                                <asp:Button ID="BtnAdd" runat="server" Text="Add" CssClass="btnstyle" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="BtnAdd" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
