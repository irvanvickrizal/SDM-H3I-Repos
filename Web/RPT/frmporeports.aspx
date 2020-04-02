<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmporeports.aspx.vb" Inherits="RPT_frmporeports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
   function DoPrint()
        {
            var str='';
            var disp_setting="toolbar=no,location=no,status=no,directories=yes,menubar=yes,"; 
          disp_setting+="scrollbars=yes,width=750, height=600, left=100, top=25"; 
            str+='<HTML>\n<head>\n'
            str+='<link href="../CSS/CheckList.css" rel="stylesheet" type="text/css" />\n</HEAD>\n<body>\n'
            str+='<center>\n'+document.getElementById("bindArea").innerHTML+'\n</center></body>\n</HTML>'
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
    function DisplayArea(id)
    {   
    var intCount;
    var varii=document.getElementById("AreaId"+id).style.display;
        for(intCount=0;intCount<document.getElementById("HDArea").value;intCount++)
        {
            document.getElementById("AreaId"+intCount).style.display="none"
        }
        if(varii=="none")
        {
        document.getElementById("AreaId"+id).style.display=""
        }
        else
        {
        document.getElementById("AreaId"+id).style.display="none"
        }
       
    }
     function DisplayRegion(araid,id,total)
    {
    var intCount;
//    alert(document.getElementById("RegionId"+araid+'-'+id).style.display)
     var varii=document.getElementById("RegionId"+araid+'-'+id).style.display;
    

        for(intCount=0;intCount<total-1;intCount++)
        {
            document.getElementById("RegionId"+araid+'-'+id).style.display="none"
        }
     
        if(varii=="none")
        {
     
        document.getElementById("RegionId"+araid+'-'+id).style.display=""
        }
        else
        {
        document.getElementById("RegionId"+araid+'-'+id).style.display="none"
        }      
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <%-- <div id="bindArea" style="height: 450px; overflow-y:scroll ;" runat="Server">--%>
    <div id="bindArea" runat="Server" style="width:100%">
        <asp:HiddenField ID="HDArea" runat="server" />
        <asp:HiddenField ID="HDRegion" runat="server" />
    <table cellpadding="4" cellspacing="4" width="100%">
        <tr class="pageTitle">
          <td colspan="2"  id="tdTitle" runat="server" align="center">Purchase Order Summary Report</td>
        </tr>                
        </table>                      
    </div> 
         <td align="left" style="clip: rect(auto 100px auto auto)">
          <input id="Button1" type="button" value="Print" onclick="DoPrint()"/></td>
     <%--<asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="buttonStyle" OnClick="DoPrint()"/></td>  --%>
    </form>
</body>
</html>
