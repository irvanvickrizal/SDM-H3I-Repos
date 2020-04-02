<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Demovideo.aspx.vb" Inherits="Demovideo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>welcome to e-BAST demo</title>
    <link href="CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
           <table id="tblvideo" runat="server" border="0" cellpadding="1" cellspacing="1" width="100%">        
        <tr>  
               <td align="left" colspan="2" valign="top">
                    <asp:Image ID="Image2" runat="server" ImageAlign="Left" ImageUrl="~/images/bar1.png"/></td>
             </tr>
         </table>
        <table>
            <tr class="pageTitle">
                <td align="left" colspan="2">welcome to e-BAST video demo
                </td>
            </tr>
        <tr class="subpageTitle" runat="server" id="lnkpassword">
                <td align="left" colspan="2">
                    Please enter password
                    <asp:TextBox ID="txtpassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:Button ID="btngo" runat="server" Text="GO" /></td>
            </tr>
              <tr class="subpageTitle" runat="server" id="lnkdownload" visible="false">
                <td align="left" colspan="2"><asp:LinkButton ID="LinkButton1" runat="Server" Text="Click here" OnClick="LinkButton1_Click"></asp:LinkButton>
         to download the file</td>
            </tr>
            <tr align=center runat="server" id="lnkwatch" visible="false">
                            <td>
               <%-- <object id="MediaPlayer" 
                 classid="CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95" 
                 codebase="http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,4,5,715" 
                 standby="Loading Microsoft Windows Media Player components..." 
                 type="application/x-oleobject" style="width: 800px; height: 475px">
                  <param name="FileName" value=".\demo.avi" >
                  <param name="AnimationatStart" value="true"> 
                  <param name="TransparentatStart" value="true"> 
                  <param name="AutoStart" value="True"> 
                  <param name="ShowControls" value="1">  
                 </object>--%>
                 <object classid="clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B" codebase="http://www.apple.com/qtactivex/qtplugin.cab" height="500" width="640">
<param name="src" value="./Demo.mp4">
<param name="autoplay" value="true">
<param name="controller" value="true">
<embed height="500" pluginspage="http://www.apple.com/quicktime/download/" src="./Demo.mp4" type="video/quicktime" width="640" controller="true" autoplay="true"> 
</object>
                </td>
            </tr>
        </table>
        
        
       
      
    
    </div>
    </form>
</body>
</html>
