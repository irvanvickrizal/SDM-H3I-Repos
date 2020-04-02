<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCheckList.aspx.vb" Inherits="frmCheckList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Check List</title>
    <link href="../CSS/CheckList.css" rel="stylesheet" type="text/css" />
  
    <script language="javascript" type="text/javascript">
    function DoPrint()
    {
        var str='';
        var disp_setting="toolbar=no,location=no,status=no,directories=yes,menubar=yes,"; 
      disp_setting+="scrollbars=yes,width=750, height=600, left=100, top=25"; 
        str+='<HTML>\n<head>\n'
        str+='<link href="../CSS/CheckList.css" rel="stylesheet" type="text/css" />\n</HEAD>\n<body>\n'
        str+='<center>\n'+document.getElementById("dvPrint").innerHTML+'\n</center></body>\n</HTML>'
        var windowUrl = 'about:blank';
         var uniqueName = new Date();
         var windowName = 'Print' + uniqueName.getTime();
        var printWindow = window.open(windowUrl,windowName,disp_setting);
        printWindow.document.write(str);
        printWindow.document.close();
        printWindow.focus();
        printWindow.print();
        printWindow.close();
       
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0">
        <tr>
            <td>
            <table border="0" cellpadding="2" cellspacing="3" style="width: 98%">
            <tr>
                <td style="width:18%" class="lblTitle">Select PoNo</td>
                     <td style="width:1%">:</td>
                <td>
                    <asp:DropDownList ID="DDPoDetails" runat="server" AutoPostBack="True">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                  <td class="lblTitle">Select Site</td>
                 <td style="width:1%">:</td>
                <td>
                    <asp:DropDownList ID="DDSite" runat="server" AutoPostBack="True">
                    </asp:DropDownList> 
                  </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
            </td>
        </tr>
          <tr>
            <td>
              <div id="dvPrint" style="height:450px;overflow-y:scroll;" runat="server">
    <table cellpadding="0" border="0" cellspacing="0" width="98%" >
        <tr>
            <td>
              <table border="0" cellpadding="1" cellspacing="2" class="ContentText" width="100%">
                 
              <tr>
                <td align="left"><img src="../Images/nsn-logo.gif" border="0" />
                </td>
                 <td>
                </td>
                 <td align="right"><img src="../Images/logo_tsel.png" border="0" />
                </td>
              </tr>
            <tr>
                <td colspan="3" class="pageTitle">TI SUPPORTING CHECKLIST</td>
            </tr>
            <tr>
                <td style="width:15%">Site Name</td>
                <td style="width:1%">:</td>
                <td id="lblSiteName" runat="server" class="lblText"></td>
            </tr>
            <tr>
                <td>Site ID</td>
                <td>:</td>
                <td id="lblSiteNo" runat="server" class="lblText"></td>
            </tr>
            <tr>
                <td>Area</td>
                <td>:</td>
                <td id="lblArea" runat="server" class="lblText"></td>
            </tr>
            <tr>
                <td>Type Of Work</td>
                <td>:</td>
                <td id="lblWork" runat="server" class="lblText">Type Of Work</td>
            </tr>
            <tr>
                <td>Purchase Order No</td>
                <td>:</td>
                <td id="lblPONo" runat="server" class="lblText"></td>
            </tr>        
            </table>
            </td>
        </tr>
         <tr>
            <td>
            <table width="100%" border="0" cellpadding="1" cellspacing="2">  
            <tr>
                <td colspan="2" class="hgap">
                </td>
            </tr>
            <tr>
                <td colspan="2" runat="server" id="CheckList">
                </td>
            </tr>
            <tr>
                <td colspan="2" class="hgap">
                </td>
            </tr>
            </table>
            </td>
        </tr>
         <tr>
            <td>
              <table width="98%">
            <tr>
                <td style="width:18%">Successful Checking List</td><td><asp:CheckBox runat="server" ID="schkYes" Text="Yes" />&nbsp;&nbsp<asp:CheckBox runat="server" ID="sChkNo" Text="No" /></td>
            </tr>
            <tr>
                <td>Deficiencis Found</td><td><asp:CheckBox runat="server" ID="dchkYes" Text="Yes" />&nbsp;&nbsp<asp:CheckBox runat="server" ID="dchkNo" Text="No" /></td>
                
            </tr>
            <tr>
                <td>Checked By</td>
                <td></td>
            </tr>
            <tr>
                <td class="hgap"></td>
                <td></td>
            </tr>
            <tr>
                <td align="center">(Telkomsel)</td>
                <td align="center">(Nokia Siemens Network )</td>
            </tr>
        </table>
            </td>
        </tr>
    </table>
      
            </div>
            </td>
        </tr>
          <tr>
            <td align="right" style="clip: rect(auto 100px auto auto)"><input id="Button1" type="button" value="   Print   " onclick="DoPrint()" />
            </td>
        </tr>
    </table>    
    </form>
</body>
</html>
