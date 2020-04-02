<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BastReadyCreation.aspx.vb"
    Inherits="Dashboard_New_BastReadyCreation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BAST Ready Creation</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .HeaderReport
        {
            background-color:#cfcfcf;
            font-family:verdana;
            font-size:13px;
            font-weight:bold;
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
        .GridEvenRowsNew
        {
            font-family:verdana;
            font-size:9pt;
            background-color:#cfcfcf;
            
        }
        .GridOddRowsNew
        {
            font-family:verdana;
            font-size:9pt;
        }
        .BtnExpt
        {
           border-style:solid;
           border-color:white;
           border-width:1px;
           font-family:verdana;
           font-size:11px;
           font-weight:bold;
           color:white;
           width:120px;
           height:25px;
           cursor:pointer;
        }
    </style>

    <script type="text/javascript">
        function WindowsClose(){
                window.opener.location.href = window.opener.location.href;
                if (window.opener.progressWindow) {
                    window.opener.progressWindow.close();
                }
                window.close();
            }
            
            function Approved(siteno, version, pono){
                alert(siteno);
                window.open('../digital-sign/customer-Digital-signature.aspx?siteno=' + siteno + '&version=' + version, 'welcome3', 'width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');
            }
            
            function Approve(id, siteno, version){
                window.open('../BAUT/frmTI_BASTFinal.aspx?id=' + id + '&open=1', 'welcome5', 'width=950,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');
            }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="HeaderReport">
                <table width="100%">
                <tr>
                    <td style="width: 80%">
                        BAST Ready Creation
                    </td>
                    <td style="width: 15%; text-align: right;">
                        <asp:Button ID="BtnDashboard" runat="server" Text="Go to Dashboard" CssClass="BtnExpt"
                            BackColor="#7f7f7f" />
                    </td>
                </tr>
            </table>
            </div>
            <div>
                <asp:GridView ID="grdDocuments" runat="server" AllowPaging="False" EmptyDataText="All documents approved" Width="100%"
                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="siteno">
                    <PagerSettings Position="TopAndBottom" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="GridEvenRowsNew" />
                    <RowStyle CssClass="GridOddRowsNew" />
                    <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                    <Columns>
                        <asp:TemplateField HeaderText=" No ">
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Pono" HeaderText="PoNo" />
                        <asp:TemplateField HeaderText="Site NO">
                            <ItemTemplate>
                                <a href="#" onclick="Approved('<%# DataBinder.Eval(Container.DataItem,"siteno") %>',<%# DataBinder.Eval(Container.DataItem,"version") %>,'<%# DataBinder.Eval(Container.DataItem,"PONo") %>')">
                                    <%# DataBinder.Eval(Container.DataItem,"siteno") %>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Site NO">
                            <ItemTemplate>
                                <a href="#" onclick="Approve(<%# DataBinder.Eval(Container.DataItem,"sw_id") %>,'<%# DataBinder.Eval(Container.DataItem,"siteno") %>',<%# DataBinder.Eval(Container.DataItem,"version") %>)">
                                    <asp:Label ID="LblSiteNo" runat="server" Text='<%#Eval("siteno") %>' Visible='<%# Convert.ToBoolean(Integer.Parse(Eval("ReadyBAST").ToString())) %>'></asp:Label>
                                </a>
                                <asp:Label ID="LblSiteNoDisabled" runat="server" Text='<%#Eval("siteno") %>' Visible='<%#IIf((Convert.ToBoolean(Integer.Parse(Eval("ReadyBAST").ToString()))),"false","true") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="LblReadyBAST" Text='<%#Eval("ReadyBAST") %>' runat="server" Visible="false"></asp:Label>
                                <asp:Image ID="imgGreen" runat="server" ImageUrl="~/Images/ok.jpg" Height="12px"
                                    Width="12px" Visible='<%#Convert.ToBoolean(Convert.ToInt16(Eval("ReadyBAST"))) %>' />
                                <asp:Image ID="imgRed" runat="server" ImageUrl="~/Images/notok.jpg" Height="13px"
                                    Width="13px" Visible='<%#IIf((Convert.ToBoolean(Integer.Parse(Eval("ReadyBAST").ToString()))),"false","true") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="workpackageid" HeaderText="Work Package Id" />
                        <asp:BoundField DataField="Scope" HeaderText="Scope" />
                        <asp:BoundField DataField="subdate" HeaderText="Ready for BAST Date" />
                        <asp:BoundField DataField="nodays" HeaderText="Oldest Task" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
