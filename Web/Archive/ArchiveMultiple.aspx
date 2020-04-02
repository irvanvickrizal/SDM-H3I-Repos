<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ArchiveMultiple.aspx.vb" Inherits="ArchiveMultiple" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <title>Document Archeving</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table >
       <tr>
                <td class="pageTitle" colspan="3">
                    Multiple Site Documents Archive</td>
            </tr>   
      <tr>
   <td class="lblTitle" style="width: 221px">PO No</td>
    <td style="width:1%">:</td>
    <td>
         <asp:DropDownList ID="ddlpono" runat="server" AutoPostBack="True" CssClass="selectFieldStyle">
        </asp:DropDownList>
    
    </td>
    </tr>
    <tr>
   
    <td class="lblTitle" valign="top" style="width: 221px">Site No</td>
   <td style="width:1%"  valign="top">:</td>
    <td> 
        <asp:ListBox ID="lstsiteno" AutoPostBack="true" SelectionMode="Multiple" CssClass="selectFieldStyle" runat="server" ></asp:ListBox>
        </td>
     
       
    </tr>
    
     <tr>
    <td class="lblTitle" style="width: 221px">Target Location
    </td>
    <td style="width:1%">:</td>
    <td><asp:RadioButtonList ID="rbtnlist" runat="server" RepeatDirection="Horizontal" RepeatLayout="flow" CssClass="lblText"  AutoPostBack="True">
            <asp:ListItem Selected="True" Value="0">Zip File</asp:ListItem>
            <asp:ListItem Value="1">FTP Upload</asp:ListItem>
        </asp:RadioButtonList> </td>
   </tr>
    <tr id="rowUser" runat="server" visible="false">
    <td class="lblTitle"> User Id </td>
      
     <td style="width:1%">:</td>
       <td><asp:TextBox ID="txtuserid" runat="server" CssClass="textFieldStyle"></asp:TextBox> </td>
   </tr>
   
   <tr id="rowPwd" runat="server" visible="false">
    <td class="lblTitle">Pass Word</td>
     <td style="width:1%">:</td>
       <td> <asp:TextBox ID="txtpwd" runat="server" CssClass="textFieldStyle" ></asp:TextBox></td>
   </tr>
   
   <tr id="rowURL" runat="server" visible="false">
    <td class="lblTitle">FTP Url </td>
     <td style="width:1%">:</td>
       <td> <asp:TextBox ID="txtftpurl" runat="server" CssClass="textFieldStyle"></asp:TextBox> </td>
       </tr>
      
    
    <tr>
    <td style="height: 20px; width: 221px;">
           </td>
           <td style="width: 1%; height: 20px;">
           </td>
        <td style="height: 20px"><asp:Button ID="btnArchive" runat="server" Text="Archive" CssClass="buttonStyle" /> </td>
        
    </tr>
    
     <tr>
      
      <td class="lblTitle"  id="msg" runat="server" align="center" style="width: 221px"></td>
    
      </tr>
    </table>
       
    </div>
    </form>
</body>
</html>
