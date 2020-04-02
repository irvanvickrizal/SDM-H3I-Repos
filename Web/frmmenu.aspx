<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmmenu.aspx.vb" Inherits="frmmenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Welcome</title>
     <link href="CSS/Styles.css" rel="stylesheet" type="text/css" />  
</head>
<body style="background-color:#528cd6" >
    <form id="form1" runat="server">
                            <asp:TreeView ID="TreeView1" runat="server" Height="29%" ImageSet="BulletedList4" ExpandDepth="0" ShowLines="True" style="vertical-align: top; text-align: left" Width="135px">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <Nodes>                         
                            <asp:TreeNode Text="Purchase Order" Value="Purchase Order">
                                <asp:TreeNode Text="PO Upload" Value="PO Upload" NavigateUrl="~/PO/frmPOUpload.aspx" Target="mainframe"></asp:TreeNode> 
                                <%--<asp:TreeNode Text="PO RawList" Value="PO RawList" NavigateUrl="~/PO/frmPORawList.aspx" Target="mainframe"></asp:TreeNode>--%>                                
                                <asp:TreeNode Text="PO List" Value="PO List" NavigateUrl="~/PO/frmPOListMain.aspx" Target="mainframe"></asp:TreeNode>
                                <asp:TreeNode Text="Document CheckList" Value="Document CheckList" NavigateUrl="~/PO/frmPOSiteList.aspx" Target="mainframe"></asp:TreeNode> 
                                <asp:TreeNode Text="Document Upload" Value="Document Upload" NavigateUrl="~/PO/frmSiteDocUpload.aspx" Target="mainframe"></asp:TreeNode>
                                <asp:TreeNode Text="Site Doc Status" Value="Site Doc Status" NavigateUrl="~/PO/frmSiteDocStatus.aspx" Target="mainframe"></asp:TreeNode> 
                                <asp:TreeNode Text="View SiteDocs" Value="View Site Doc" NavigateUrl="~/PO/frmDocView.aspx" Target="mainframe"></asp:TreeNode> 
                                <asp:TreeNode Text="Change Order(MOM)" Value="Change Order (MOM)" NavigateUrl="~/CR/frmMOMList.aspx" Target="mainframe"></asp:TreeNode>                                 
                                <asp:TreeNode Text="Generate CR" Value="Generate CR" NavigateUrl="~/CR/frmChangeRequest.aspx" Target="mainframe"></asp:TreeNode>                                                                 
                            </asp:TreeNode>
                            <asp:TreeNode Text="User Management" Value="User Management">
                                <asp:TreeNode Text="Ebast Users" Value="Ebast Users" NavigateUrl="~/USR/frmUserList.aspx" Target="mainframe"></asp:TreeNode>                                                                
                            </asp:TreeNode>
                            <asp:TreeNode Text="MasterData" Value="MasterData">
                                <asp:TreeNode Text="Location" Value="Location">                                    
                                <asp:TreeNode Text="Area" Value="Area" NavigateUrl="~/COD/frmCODArea.aspx" Target="mainframe"></asp:TreeNode>
                                <asp:TreeNode Text="Region" Value="Region" NavigateUrl="~/COD/frmCODRegion.aspx" Target="mainframe"></asp:TreeNode>
                                <asp:TreeNode Text="Zone" Value="Zone" NavigateUrl="~/COD/frmCODZone.aspx" Target="mainframe"></asp:TreeNode>
                                <asp:TreeNode Text="Site" Value="Site" NavigateUrl="~/COD/frmCODSite.aspx" Target="mainframe"></asp:TreeNode>
                                <%--<asp:TreeNode Text="Country" Value="Country" NavigateUrl="~/COD/frmCODCountry.aspx" Target="mainframe"></asp:TreeNode>
                                <asp:TreeNode Text="State" Value="State" NavigateUrl="~/COD/frmCODState.aspx" Target="mainframe"></asp:TreeNode>                                    
                                <asp:TreeNode Text="District" Value="District" NavigateUrl="~/COD/frmCODDistrict.aspx" Target="mainframe"></asp:TreeNode> --%>
                                <asp:TreeNode Text="Section" Value="Section" NavigateUrl="~/COD/frmCODSection.aspx" Target="mainframe"></asp:TreeNode> 
                                <asp:TreeNode Text="Sub Section" Value="Sub Section" NavigateUrl="~/COD/frmCODSubSection.aspx" Target="mainframe"></asp:TreeNode> 
                                <asp:TreeNode Text="Document" Value="Document" NavigateUrl="~/COD/frmCODDocument.aspx" Target="mainframe"></asp:TreeNode>                                
                                <asp:TreeNode Text="Sub Contractor" Value="Sub Contractor" NavigateUrl="~/USR/frmSubCon.aspx" Target="mainframe"></asp:TreeNode>                                
                                <asp:TreeNode Text="Mail Template" Value="Mail Template" NavigateUrl="~/frmMailReport.aspx" Target="mainframe"></asp:TreeNode>                                
                            </asp:TreeNode>
                            <asp:TreeNode Text="Others" Value="Others">
                                <asp:TreeNode Text="MessageBoard" Value="MessageBoard" Target="mainframe" NavigateUrl="~/frmMessageBoard.aspx"></asp:TreeNode>
                                <asp:TreeNode Text="Access Rights" Value="Acc Rights" NavigateUrl="~/frmSysMenu.aspx" Target="mainframe"></asp:TreeNode>
                             </asp:TreeNode>
                            
                            </asp:TreeNode>
                            <asp:TreeNode Text="WorkFlow" Value="WorkFlow">
                                <asp:TreeNode Text="User Type" Value="User Type" NavigateUrl="~/frmUserType.aspx" Target="mainframe"></asp:TreeNode>
                                <asp:TreeNode Text="Role Master" Value="Role Master" NavigateUrl="~/frmRole.aspx" Target="mainframe"></asp:TreeNode>
                                <asp:TreeNode Text="Customer" Value="Customer" NavigateUrl="~/USR/frmCustomer.aspx" Target="mainframe"></asp:TreeNode>
                                <asp:TreeNode Text="Distribution Group" Value="Distribution Group" NavigateUrl="~/USR/frmTDstGroup.aspx" Target="mainframe"></asp:TreeNode>
                                <asp:TreeNode Text="Distribution Users" Value="Distribution Users" NavigateUrl="~/USR/frmGrpUsers.aspx" Target="mainframe"></asp:TreeNode>
                                <asp:TreeNode Text="Process Flow" Value="Process Flow" NavigateUrl="~/frmWFList.aspx" Target="mainframe"></asp:TreeNode>
                                <asp:TreeNode Text="ProFlow Vs Document" Value="Process Flow Vs Document" NavigateUrl="~/frmWFDoc.aspx" Target="mainframe"></asp:TreeNode>
                                
                             </asp:TreeNode> 
                            
                        </Nodes><NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                    <asp:TreeView ID="TreeView2" runat="server" Height="29%" ImageSet="BulletedList4" ExpandDepth="0" ShowLines="True" style="vertical-align: top; text-align: left" Width="109px">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <Nodes>
                                  <asp:TreeNode Text="Document Upload" Value="Document Upload" NavigateUrl="~/PO/frmSiteDocUpload.aspx" Target="mainframe"></asp:TreeNode>
                                  <asp:TreeNode Text="Change Order (MOM)" Value="Change Order (MOM)" NavigateUrl="~/CR/frmMOMList.aspx" Target="mainframe"></asp:TreeNode>                                 
                                              
                        </Nodes>
                        <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                    <asp:TreeView ID="TreeView3" runat="server" Height="29%" ImageSet="BulletedList4" ExpandDepth="0" ShowLines="True" style="vertical-align: top; text-align: left" Width="135px">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <Nodes>
                                  <asp:TreeNode Text="User Management" Value="User Management">
                                <asp:TreeNode Text="Ebast Users" Value="Ebast Users" NavigateUrl="~/USR/frmUserList.aspx" Target="mainframe"></asp:TreeNode>                                                                
                            </asp:TreeNode>
                                              
                        </Nodes>
                        <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>     
                    <asp:TreeView ID="TreeView4" runat="server" Height="29%" ImageSet="BulletedList4" ExpandDepth="0" ShowLines="True" style="vertical-align: top; text-align: left" Width="135px">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <Nodes>                         
                            <asp:TreeNode Text="Purchase Order" Value="Purchase Order">
                                <asp:TreeNode Text="Document Upload" Value="Document Upload" NavigateUrl="~/PO/frmSiteDocUpload.aspx" Target="mainframe"></asp:TreeNode>
                                <asp:TreeNode Text="Site Doc Status" Value="Site Doc Status" NavigateUrl="~/PO/frmSiteDocStatus.aspx" Target="mainframe"></asp:TreeNode> 
                                <asp:TreeNode Text="View SiteDocs" Value="View Site Doc" NavigateUrl="~/PO/frmDocView.aspx" Target="mainframe"></asp:TreeNode>                                 
                            </asp:TreeNode>                            
                            <asp:TreeNode Text="Others" Value="Others">
                                <asp:TreeNode Text="MessageBoard" Value="MessageBoard" Target="mainframe" NavigateUrl="~/frmMessageBoardPost.aspx"></asp:TreeNode>                                
                            </asp:TreeNode>                          
                            
                        </Nodes><NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>               
  
    </form>
</body>
</html> 
