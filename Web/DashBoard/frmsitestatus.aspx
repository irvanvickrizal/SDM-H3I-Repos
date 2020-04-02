<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmsitestatus.aspx.vb" Inherits="frmsitestatus" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
        <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
            <ContentTemplate>        
                <div style="width:100%;">
                   <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">     
                        <tr>
                            <td  style="background-image: url(../Images/barbg.jpg) ;background-repeat: repeat-x;"></td>
                        </tr>       
                        <tr>
                            <td style="height:10px;">
                             
                            </td>
                        </tr>
                        <tr>
                            <td>   <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr  runat="server" id="tdArea">
                                        <td>
                                            Area</td>
                                        <td>
                                            <asp:DropDownList ID="DDArea" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDArea_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr runat="server" id="tdRegion">
                                        <td >
                                            Region</td>
                                        <td>
                                            <asp:DropDownList ID="DDRegion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDRegion_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr  runat="server" id="tdZone">
                                        <td>
                                            Zone</td>
                                        <td>
                                            <asp:DropDownList ID="DDZone" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDZone_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr runat="server" id="tdSite">
                                        <td > Site</td>
                                        <td>
                                            <asp:DropDownList ID="DDSite" runat="server">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <input id="BtnFind" type="submit" value="Find" runat="server" onserverclick="BtnFind_ServerClick"/></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height:10px;">
                            </td>
                        </tr>
                        <tr>
                            <td id="tdBast" runat="server">
                             <asp:GridView ID="grdsitestatus" runat="server" AllowSorting="True"
                                    AutoGenerateColumns="False" DataKeyNames="site_no" EmptyDataText="No Records Found"
                                    PageSize="5" Width="100%" AllowPaging="True">
                                    <PagerSettings Position="TopAndBottom" />
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                    <AlternatingRowStyle CssClass="GridEvenRows" />
                                    <RowStyle CssClass="GridOddRows" />
                                    <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <Columns>   
                                         <asp:TemplateField HeaderText="Site No">
                                            <ItemTemplate>
                                            <a href='../Po/frmSiteDocStatus.aspx?id=<%# DataBinder.Eval(Container.DataItem,"site_no") %>'><%# DataBinder.Eval(Container.DataItem,"site_no") %></a>
                                            </ItemTemplate>
                                         </asp:TemplateField>                         
                                         <asp:TemplateField HeaderText="Site Name">
                                            <ItemTemplate>
                                               <asp:Label ID="lblsiteName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"site_name") %>'></asp:Label>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Precentage">
                                            <ItemTemplate>
                                               <asp:Label ID="lblPrecentageName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Percentage") %>'></asp:Label>

                                            </ItemTemplate>
                                         </asp:TemplateField> 
                                       
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
           </ContentTemplate>
         </asp:UpdatePanel>
    </form>
</body>
</html>
