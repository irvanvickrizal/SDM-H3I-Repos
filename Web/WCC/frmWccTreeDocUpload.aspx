<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWccTreeDocUpload.aspx.vb" Inherits="WCC_frmWccTreeDocUpload" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" /> 
    <script language="javascript" type="text/javascript">
    function Showmain(type) 
   { 
      if (type=='Int')
         {
          alert('Integration date not vailable');
         }
        
       if (type=='IntD')
       {
         
        alert('The document cannot be uploaded before the integration date');
        
       } 
            
       if (type=='2sta')
       
       {
         alert('This Document already processed for second stage so cannot upload again ');
       }
        if (type=='Lnk')
       
       {
         alert('Not Allowed to upload this Document Now.');
       }
       
        if (type=='nop')
       
       {
         alert('No permission to upload this Document ');
       }
       
       
        
              window.location = 'frmSiteDocUploadTree.aspx'
            

    }  
     
        function checkIsEmpty()
        {
            var msg="";
            var e = document.getElementById("ddlsite"); 
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0)
            {
                msg = msg + "Site should be select\n"
            }
            e = document.getElementById("ddlsection"); 
            strUser = e.options[e.selectedIndex].value;
            if (strUser == 0)
            {
                msg = msg + "Section should be select\n"
            }
            else
            {
                /*e = document.getElementById("ddlsubsection"); 
                strUser = e.options[e.selectedIndex].value;
                if (strUser == 0)
                {
                    msg = msg + "Sub-Section should be select\n"
                }
                else
                {*/
                    e = document.getElementById("ddldocument"); 
                    strUser = e.options[e.selectedIndex].value;
                    if (strUser == 0)
                    {
                        msg = msg + "Document should be select\n"
                    }
                /*}*/
            }    
            if (msg != "")
            {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
            else
            {
                //return accessConfirm();
                return true;
                
            } 
           
                     
        }
        function accessConfirm()
        {
            var r = confirm("Are you sure you want to save the details?");
            if (r == true)
            {
                return true;
            }
            else
            {return false;}
        }
        
        function myPostBack()
        {    
          var o = window.event.srcElement;
          if (o.tagName == "INPUT" && o.type == "checkbox")
              {__doPostBack("","");
              } 
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <table border="0" cellpadding="1" cellspacing="1" style="width: 100%" >
                    <tr class="pageTitle">
                        <td colspan="3"> Wcc Site Document Upload Tree</td>
                    </tr>
                    <tr style="height:5"><td colspan="3"></td></tr>
                    <tr>
                        <td class="lblTitle" style="height: 18px; width: 47px;">
                            Po No</td>
                        <td style="width: 1px; height: 18px">
                            :</td>
                        <td style="width: 81px; height: 18px">
                            <asp:DropDownList ID="ddlPO" runat="server" Width="120px" CssClass="selectFieldStyle" AutoPostBack="True" OnSelectedIndexChanged="ddlPO_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="lblpo" runat="server" Text="Label" Width="461px" CssClass="lblText"></asp:Label></td>
                    </tr>
                                <tr>
                        <td class="lblTitle" style="height: 18px; width: 47px;">
                            Site</td>
                        <td style="width: 1px; height: 18px;">:</td>
                        <td style="height: 18px; width: 81px;">
                            <asp:DropDownList ID="ddlsite" runat="server" Width="307px" CssClass="selectFieldStyle">
                            
                            </asp:DropDownList>
                            <asp:Label ID="lblsite" runat="server" Text="Label" Width="459px" CssClass="lblText"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="lblTitle" style="width: 47px">
                            Document</td>
                        <td valign="top">
                            :</td>
                        <td style="width: 81px">
                            <asp:DropDownList ID="ddldocument" runat="server" Width="305px" CssClass="selectFieldStyle" DataTextField="--Select--" DataValueField="0" OnSelectedIndexChanged="ddldocument_SelectedIndexChanged" AutoPostBack="True">
                                                        </asp:DropDownList>
                            <asp:Label ID="lbldoc" runat="server" Text="Label" Width="467px" CssClass="lblText"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="lblTitle" style="width: 47px">
                        </td>
                        <td valign="top">
                        </td>
                        <td style="width: 81px">
                        </td>
                    </tr>
                    <tr style="height:5"><td colspan="3">
                        <asp:Button ID="btnback" runat="server" Text="Back to Listing" Width="107px" /></td></tr>
                </table>
               
              <asp:panel id="panel1"  runat="server" Visible="False" >
                    
               <table style="width: 582px" >
        <tr>
        <td style="height: 17px">
        
        <asp:FileUpload ID="fileUpload" runat="server" EnableTheming="True" Width="456px"/>
            <asp:Button ID="btnupload" runat="server" Text="Upload" CssClass="buttonStyle"/><br />
           <br />
        <asp:label ID="lblStatus" runat="server"></asp:label></td>
        </tr>
     </table> 
     </asp:panel>  &nbsp;
        <asp:HiddenField ID="hdnkeyval" runat="server" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:HiddenField ID="hdnversion" runat="server" />
      <asp:HiddenField ID="hdnpo" runat="server" />
        &nbsp; &nbsp; &nbsp;
        <asp:HiddenField ID="hdnpoid" runat="server" />
                       <asp:HiddenField ID="hdnsiteno" runat="server" /><asp:HiddenField ID="hdnsiteid" runat="server" />
                       <asp:HiddenField ID="hdndocname" runat="server" />
                       <asp:HiddenField ID="hdnapprequired" runat="server" />
                         <asp:HiddenField ID="hdnready4baut" runat="server" />
                  <input type="hidden" runat="server" id="hdnDGBox" />
     
     </form>
</body>
</html>
