<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDocInitiator.aspx.vb"
    Inherits="COD_frmDocInitiator" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document Initiator</title>
    <style type="text/css">
        .HeaderReport
        {
            background-color:#cfcfcf;
            font-family:verdana;
            font-size:13px;
            font-weight:bold;
            margin-bottom:10px;
            padding:5px;
        }
        .lblTitle
        {
            font-family:Arial Unicode MS;
            font-size:8pt;
        }
        .HeaderGrid
        {
            font-family: Arial Unicode MS;
            font-size: 8pt;
            font-weight: bold;
            color: White;
            background-color: #ffc90E;
            border-color:white;
            vertical-align:middle;
            height:25px;
        }
        .oddGrid
        {
            font-family: Arial Unicode MS;
            font-size: 8pt;
            background-color: White;
        }
        .evenGrid
        {
            font-family: Arial Unicode MS;
            font-size: 8pt;
            background-color:#cfcfcf;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
         <div class="HeaderReport">
            Initiator Document Customization
        </div>
        <div>
            <asp:UpdatePanel ID="UpMainPanel" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GvDocInitiator" runat="server" AutoGenerateColumns="false" DataKeyNames="InitiatorDoc_Id"
                        OnRowDeleting="GvDocInitiator_RowDeleting" OnRowCommand="GvDocInitiator_RowCommand" EmptyDataText="No Configuration Found"
                        ShowFooter="true">
                        <HeaderStyle CssClass="HeaderGrid" />
                        <RowStyle CssClass="oddGrid" />
                        <AlternatingRowStyle CssClass="evenGrid" />
                        <Columns>
                            <asp:TemplateField ShowHeader="false" HeaderStyle-BackColor="white">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Images/gridview/delete.jpg"
                                        ToolTip="Delete" Height="16px" Width="16px" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/gridview/AddNewitem.jpg"
                                        CommandName="AddNew" Width="20px" Height="20px" ToolTip="Add new User" ValidationGroup="validation" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Document">  
                                <ItemTemplate>
                                    <asp:Label ID="LblDocument" runat="server" CssClass="lblTitle" Text='<%#Eval("docname") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="DdlDocumentType" runat="server" CssClass="lblTitle" AutoPostBack="true" OnSelectedIndexChanged="DdlDocumentTypeChanged"></asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Role"> 
                                <ItemTemplate>
                                    <asp:Label ID="LblRole" runat="server" CssClass="lblTitle" Text='<%#Eval("roledesc") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="DdlRole" runat="server" CssClass="lblTitle"></asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type of Definition">
                                <ItemTemplate>
                                    <asp:Label ID="LblType" runat="server" CssClass="lblTitle" Text='<%#Eval("TypeofDefinition") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GvDocInitiator" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
