<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmViewLog.aspx.vb" Inherits="PO_frmviewlog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />    
    <link rel="stylesheet" href="~/bootstrap/css/bootstrap.min.css" />
    <link type="text/css" rel="Stylesheet" href="../CSS/css/smoothness/jquery-ui-1.8.17.custom.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="~/dist/css/AdminLTE.min.css" />
    <link rel="stylesheet" href="~/dist/css/skins/_all-skins.min.css" />
    <link rel="stylesheet" href="~/dist/css/font-awesome-4.7.0/css/font-awesome.min.css" /> 
    <title>View Log</title>
    <style type="text/css">
        .GridHeaderStyle {
            padding: 5px;
            color: #000;
            background-color: #ffd800;
            font-family: verdana;
            font-size: 12px;
            font-weight: bolder;
            border-color:black;
            border-width:1px;
            border-style:solid;
            height:23px;
        }
         .headerpanel {
            background-color: gray;
            padding: 4px;
            color: white;
            font-family: Trebuchet MS;
            font-size: 16px;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        function invalidExportToExcel() {
            alert('Data is empty, please try another date!');
            return false;
        }
    </script>
</head>
<body>
    <%--Modified by Fauzan 13 Nov 2018, Redesign View Log--%>
    <div class="row">
        <form id="form1" runat="server">
            <asp:Panel ID="PnlPrint" runat="server">
                <div class="col-xs-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Document View Log</h3>
                        </div>
                        <div class="box-body">
                            <table class="table table-bordered table-condensed table-responsive">
                                <tr class="lblText">
                                    <td style="width: 20%">Po No</td>
                                    <td style="width: 1%">:
                                    </td>
                                    <td colspan="2" id="tdpono" runat="server"></td>
                                </tr>
                                <tr class="lblText">
                                    <td>Site No</td>
                                    <td>:</td>
                                    <td colspan="2" id="tdsiteno" runat="server"></td>
                                </tr>
                                <tr class="lblText">
                                    <td>Site Name</td>
                                    <td>:</td>
                                    <td colspan="2" id="tdsitename" runat="server"></td>
                                </tr>
                                <tr class="lblText">
                                    <td>Scope</td>
                                    <td>:</td>
                                    <td runat="server" id="tdscope" colspan="2"></td>
                                </tr>
                                <tr class="lblText">
                                    <td>Work Package ID</td>
                                    <td>:</td>
                                    <td colspan="2" id="tdwpid" runat="server"></td>
                                </tr>
                                <tr class="lblText">
                                    <td>Work Package Name</td>
                                    <td>:</td>
                                    <td colspan="2" id="tdwpname" runat="server"></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="LblDocument" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <asp:MultiView ID="mvPanelViewLog" runat="server">
                                <asp:View ID="vwCommonLog" runat="server">
                                    <div style="margin-top: 10px; min-height: 200px; max-height: 650px; overflow-y: scroll;">
                                        <asp:GridView ID="gvSearch" runat="server" BackColor="White" BorderColor="Black"
                                            BorderWidth="1px" GridLines="Both" AutoGenerateColumns="false"
                                            EmptyDataText="No Records Found" CssClass="table table-bordered table-condensed table-responsive">
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle CssClass="GridHeaderStyle" BorderColor="Black" ForeColor="White" />
                                            <RowStyle CssClass="GridOddRows" BorderColor="White" />
                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                            <Columns>
                                                <asp:TemplateField HeaderText=" No. " ItemStyle-BorderWidth="1" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black">
                                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblno" runat="Server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Event" DataField="task"
                                                    ItemStyle-BorderWidth="1" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black" />
                                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Name" DataField="User"
                                                    ItemStyle-BorderWidth="1" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black" />
                                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Type" DataField="UserType"
                                                    ItemStyle-BorderWidth="1" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black" />
                                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Role" DataField="UserRole"
                                                    ItemStyle-BorderWidth="1" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black" />
                                                <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Start Date Time" DataField="EventStartTime"
                                                    ItemStyle-BorderWidth="1" HtmlEncode="false" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black"
                                                    DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                                                <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="End Date Time" DataField="EventEndTime"
                                                    ItemStyle-BorderWidth="1" HtmlEncode="false" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black"
                                                    DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                                                <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Execute Date Time"
                                                    DataField="EventEnd" ItemStyle-BorderWidth="1" HtmlEncode="false" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black"
                                                    DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Remarks" DataField="Remarks"
                                                    ItemStyle-BorderWidth="1" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black" />
                                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Categories" DataField="Categories"
                                                    ItemStyle-BorderWidth="1" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black"/>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </asp:View>
                                <asp:View ID="vwATPLog" runat="server">
                                    <div style="width: 100%">
                                        <div style="text-align: right;">
                                            <asp:ImageButton ID="BtnPrintVIewLog" runat="server" ImageUrl="~/images/print-icon.png" OnClientClick="return confirm('Do you want to print view log ?');"
                                                ToolTip="Print View Log" Width="23px" Height="20px" />
                                            <asp:ImageButton ID="BtnSaveViewLog" runat="server" ImageUrl="~/images/save-icon.jpg"
                                                ToolTip="Save view log" Width="23px" Height="20px" />
                                        </div>
                                        <div style="margin-top: 10px; min-height: 200px; max-height: 650px; overflow-y: scroll;">
                                            <asp:GridView ID="gvATPLog" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                                                CssClass="GridOddRows" BorderWidth="1px" ForeColor="Black" GridLines="Vertical"
                                                Width="100%">
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle CssClass="GridHeaderStyle" ForeColor="#ffffff" />
                                                <RowStyle CssClass="GridOddRows" />
                                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Taskname" HeaderText="Task" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="Username" HeaderText="User" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="Usertitle" HeaderText="User Title" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="Eventstarttime" HeaderText="Start Time" HeaderStyle-HorizontalAlign="Center"
                                                        DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Eventendtime" HeaderText="End Time" HeaderStyle-HorizontalAlign="Center"
                                                        DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Leadtime" HeaderText="Delays(day)" HeaderStyle-HorizontalAlign="Center" Visible="false"
                                                        ItemStyle-HorizontalAlign="Center" />
                                                    <asp:TemplateField HeaderText="LeadTime">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblTaskId" runat="server" Text='<%#Eval("Task") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="LblDelays" runat="server"></asp:Label>
                                                            <asp:Label ID="LblStartTime" runat="server" Text='<%#Eval("Eventstarttime") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="LblEndTime" runat="server" Text='<%#Eval("Eventendtime") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="Category" HeaderText="Category" HeaderStyle-HorizontalAlign="Center" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                        <div class="box-footer">
                            <div class="col-xs-2">
                                <asp:Button ID="BtnExportToExcel" runat="server" Text="Export to Excel" CssClass="btn btn-block btn-primary pull-left" Width="150px" />
                            </div>
                            <div class="col-xs-2">
                                <input type="button" runat="server" id="btnClose" class="btn btn-block btn-danger pull-left" style="width:150px" value="Close" onclick="javascript: window.close()" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </form>
    </div>
    <%--OLD--%>
    <%--    <form id="form1" runat="server">
            <asp:Panel ID="PnlPrint" runat="server">
                <div class="headerpanel">
                    Document View Log
                </div>
                <div style="margin-top:8px;">
                    <table cellpadding="1" border="0" cellspacing="1" width="100%">
                        <tr class="lblText">
                            <td style="width: 20%">Po No</td>
                            <td style="width: 1%">:
                            </td>
                            <td colspan="2" id="tdpono" runat="server"></td>
                        </tr>
                        <tr class="lblText">
                            <td>Site No</td>
                            <td>:</td>
                            <td colspan="2" id="tdsiteno" runat="server"></td>
                        </tr>
                        <tr class="lblText">
                            <td>Site Name</td>
                            <td>:</td>
                            <td colspan="2" id="tdsitename" runat="server"></td>
                        </tr>
                        <tr class="lblText">
                            <td>Scope</td>
                            <td>:</td>
                            <td runat="server" id="tdscope" colspan="2"></td>
                        </tr>
                        <tr class="lblText">
                            <td>Work Package ID</td>
                            <td>:</td>
                            <td colspan="2" id="tdwpid" runat="server"></td>
                        </tr>
                        <tr class="lblText">
                            <td>Work Package Name</td>
                            <td>:</td>
                            <td colspan="2" id="tdwpname" runat="server"></td>
                        </tr>
                         <tr align="left">
                            <td colspan="4" align="left">
                                <asp:MultiView ID="mvPanelViewLog" runat="server">
                                    <asp:View ID="vwCommonLog" runat="server">
                                        <div style="padding:5px; background-color:#cfcfcf; font-family:Trebuchet MS;font-size:14px; margin-top:5px; font-weight:bolder;">
                                            <asp:Label ID="LblDocument" runat="server"></asp:Label>
                                        </div>
                                        <div style="overflow: auto; height: 250px; margin-top:10px;">
                                            <asp:GridView ID="gvSearch" runat="server" BackColor="White" BorderColor="Black"
                                                BorderWidth="1px" GridLines="Both" AutoGenerateColumns="false"
                                                EmptyDataText="No Records Found">
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle CssClass="GridHeaderStyle" BorderColor="Black" ForeColor="White" />
                                                <RowStyle CssClass="GridOddRows" BorderColor="White" />
                                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText=" No. " ItemStyle-BorderWidth="1" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black">
                                                        <ItemStyle HorizontalAlign="Right" Width="2%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblno" runat="Server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Event" DataField="task"
                                                        ItemStyle-BorderWidth="1" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Name" DataField="User"
                                                        ItemStyle-BorderWidth="1" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Type" DataField="UserType"
                                                        ItemStyle-BorderWidth="1" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Role" DataField="UserRole"
                                                        ItemStyle-BorderWidth="1" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Start Date Time" DataField="EventStartTime"
                                                        ItemStyle-BorderWidth="1" HtmlEncode="false" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black"
                                                        DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="End Date Time" DataField="EventEndTime"
                                                        ItemStyle-BorderWidth="1" HtmlEncode="false" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black"
                                                        DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Execute Date Time"
                                                        DataField="EventEnd" ItemStyle-BorderWidth="1" HtmlEncode="false" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black"
                                                        DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Remarks" DataField="Remarks"
                                                        ItemStyle-BorderWidth="1" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Categories" DataField="Categories"
                                                        ItemStyle-BorderWidth="1" HeaderStyle-BorderColor="Black" HeaderStyle-ForeColor="Black"/>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vwATPLog" runat="server">
                                        <div style="width: 100%">
                                            <div style="text-align: right;">
                                                <asp:ImageButton ID="BtnPrintVIewLog" runat="server" ImageUrl="~/images/print-icon.png" OnClientClick="return confirm('Do you want to print view log ?');"
                                                    ToolTip="Print View Log" Width="23px" Height="20px" />
                                                <asp:ImageButton ID="BtnSaveViewLog" runat="server" ImageUrl="~/images/save-icon.jpg"
                                                    ToolTip="Save view log" Width="23px" Height="20px" />
                                            </div>
                                            <div style="margin-left: 2px; margin-right: 2px;">
                                                <asp:GridView ID="gvATPLog" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                                                    CssClass="GridOddRows" BorderWidth="1px" ForeColor="Black" GridLines="Vertical"
                                                    Width="100%">
                                                    <FooterStyle BackColor="#CCCCCC" />
                                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle CssClass="GridHeaderStyle" ForeColor="#ffffff" />
                                                    <RowStyle CssClass="GridOddRows" />
                                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="No." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Taskname" HeaderText="Task" HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Username" HeaderText="User" HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Usertitle" HeaderText="User Title" HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Eventstarttime" HeaderText="Start Time" HeaderStyle-HorizontalAlign="Center"
                                                            DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" />
                                                        <asp:BoundField DataField="Eventendtime" HeaderText="End Time" HeaderStyle-HorizontalAlign="Center"
                                                            DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" />
                                                        <asp:BoundField DataField="Leadtime" HeaderText="Delays(day)" HeaderStyle-HorizontalAlign="Center" Visible="false"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <asp:TemplateField HeaderText="LeadTime">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblTaskId" runat="server" Text='<%#Eval("Task") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="LblDelays" runat="server"></asp:Label>
                                                                <asp:Label ID="LblStartTime" runat="server" Text='<%#Eval("Eventstarttime") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="LblEndTime" runat="server" Text='<%#Eval("Eventendtime") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Category" HeaderText="Category" HeaderStyle-HorizontalAlign="Center" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </asp:View>
                                </asp:MultiView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                <asp:Button ID="BtnExportToExcel" runat="server" Text="Export to Excel" CssClass="buttonStyle" Width="120px" />
                                <input type="button" runat="server" id="btnClose" class="buttonStyle" value="Close"
                                    onclick="javascript: window.close()" /></td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </form>--%>
    <script type="text/javascript" src="../js/jquery-1.9.min.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../dist/js/adminlte.min.js"></script>
</body>
</html>
