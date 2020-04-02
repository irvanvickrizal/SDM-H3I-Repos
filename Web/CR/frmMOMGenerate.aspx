<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMOMGenerate.aspx.vb"
    Inherits="CR_frmMOMGenerate" EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MOM Generate</title>
    <style type="text/css">
        tr
        {
            padding: 3px;
        }
        .MainCSS
        {
           margin-bottom:0px;
	        margin-left:20px;
	        margin-right:20px;
	        margin-top:0px;
            width: 850px;
            height: 700px;
            text-align: center;
        }
        .lblText
        {
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
            text-align: left;
        }
        .lblUText
        {
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
            text-align: left;
            text-decoration: underline;
        }
        .lblTextNumber
        {
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
            text-align: center;
        }
        .lblBText
        {
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
            text-align: left;
            font-weight: bold;
        }
        .lblBBindText
        {
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
            text-align: left;
            font-weight: bold;
               height:20px;
        }
        .lblBindText
        {
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
            text-align: left;
               height:20px;
                       }
        .lblBold
        {
            font-family: verdana;
            font-size: 12pt;
            color: #000000;
            font-weight: bold;
        }
        .textFieldStyle
        {
            background-color: white;
            border: 1px solid;
            color: black;
            font-family: verdana;
            font-size: 9pt;
        }
        .GridHeader
        {
            color: #0e1b42;
            background-color: Orange;
            font-size: 9pt;
            font-family: Verdana;
            text-align: Left;
            vertical-align: bottom;
            font-weight: bold;
        }
        .GridEvenRows
        {
            background-color: #e3e3e3;
            vertical-align: top;
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
        }
        .GridOddRows
        {
            background-color: white;
            vertical-align: top;
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
        }
        .PagerTitle
        {
            font-size: 8pt;
            background-color: #cddbbf;
            text-align: Right;
            vertical-align: middle;
            color: #25375b;
            font-weight: bold;
        }
        .Hcap
        {
            height: 10px;
        }
        .VCap
        {
            width: 10px;
        }
    </style>
     <script language="javascript" type="text/javascript">
   function getControlPosition()
    {
        var Total=document.getElementById('HDDgSignTotal').value 
        for(intCount1=1;intCount1<=Total;intCount1++)
        {
            var divctrl = document.getElementById('dtList_ctl0'+ intCount1 +'_ImgPostion');   
            var divBounds=Sys.UI.DomElement.getBounds(divctrl);
            eval("document.getElementById('dtList_ctl0"+ intCount1 +"_hdXCoordinate')").value=divBounds.x;
            eval("document.getElementById('dtList_ctl0"+ intCount1 +"_hdYCoordinate')").value=divBounds.y;  
            //alert(divBounds.x);           
            
        }
       }
    </script>
</head>
<body class="MainCSS">
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
        <div id="dvPrint" runat="server" style="width:100%">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="left">
                                    <img src="http://www.telkomsel.nsnebast.com/images/nsn-logo.gif" />
                                </td>
                                <td>
                                    <table border="0" cellpadding="1" cellspacing="2" style="width: 100%">
                                        <tr>
                                            <td class="lblBold">
                                                Minutes of Meeting</td>
                                        </tr>
                                        <tr>
                                            <td class="lblBText">
                                                Organizer Tel.</td>
                                        </tr>
                                        <tr>
                                            <td id="tdModerator" runat="server">
                                                Moderator</td>
                                        </tr>
                                        <tr>
                                            <td id="tdMOMWriter" runat="server">
                                                Mom Writer</td>
                                        </tr>
                                        <tr>
                                            <td id="Td1" runat="server" class="Hcap">
                                            </td>
                                        </tr>
                                    </table>
                                    </td>
                                <td align="right">
                                    <img src="http://www.telkomsel.nsnebast.com/images/logo_tsel.png" />
                                </td>
                            </tr>
                            <tr>
                                <td id="tdDate" runat="server">
                                    Date :</td>
                                <td id="tdTime" runat="server">
                                    Form - To (hrs.)</td>
                                <td id="tdLocation" runat="server">
                                    Location</td>
                            </tr>
                            <tr>
                                <td id="Td2" runat="server" class="Hcap" colspan="3">
                                </td>
                            </tr>
                            <tr>
                                <td runat="server" colspan="3" id="tdSubject">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="Hcap">
                    </td>
                </tr>
                <tr><td class="lblBold"> Participants</td></tr>
                <tr>
                    <td>
                        <asp:DataList ID="dtList" Width="100%" runat="server">
                            <HeaderTemplate>
                                <table cellpadding="0" width="100%" cellspacing="0">
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:Label ID="lblTeam" Text="Name" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:Label ID="lblName" Text="Team" runat="server"></asp:Label>
                                        </td>
                                         <td style="width: 20%">
                                            <asp:Label ID="Label1" Text="Distribution" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 40%">
                                            <asp:Label ID="lblSign" Text="Signature & Date" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-color: Black;
                                    border-collapse: collapse; border-style: solid;">
                                    <tr>
                                        <td style="width: 20%" class="lblText">
                                            <%#Container.DataItem("UsrName")%>
                                        </td>
                                        <td style="width: 20%" class="lblText">
                                            <%#Container.DataItem("roledesc")%>
                                        </td>
                                         <td style="width: 20%" class="lblText">
                                            <%#Container.DataItem("Description")%>
                                        </td>
                                        <td style="width: 40%; height: 50px;" class="lblText">
                                            <asp:ImageButton ID="ImgPostion" runat="server" ImageUrl="http://www.telkomsel.nsnebast.com/sis/Images/dgsign.JPG" />
                                            <asp:HiddenField runat="Server" ID="hdXCoordinate" value="0" />
                                            <asp:HiddenField runat="Server" ID="hdYCoordinate" value="0" />
                                             <asp:HiddenField runat="Server" ID="HdUserid" Value='<%#Container.DataItem("usr_id")%>' />
                                             <asp:HiddenField runat="Server" ID="hdnusrname" Value='<%#Container.DataItem("usrname")%>' />
                                             <asp:HiddenField runat="Server" ID="hdnemail" Value='<%#Container.DataItem("email")%>' />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <HeaderStyle CssClass="GridHeader" BorderStyle="Solid" BorderWidth="1px" Font-Size="8pt" />
                        </asp:DataList>
                    </td>
                </tr>
                <tr>
                    <td class="Hcap">
                    </td>
                </tr>
                <tr>
                    <td runat="Server" id="tdBindMomDetails" align="center">
                    </td>
                </tr>
            </table>
        </div>
        <div><asp:Button ID="BtnGenerate" runat="server" Text="MOM Generate" /> <asp:Button ID="btnChangeRequest" runat="server" Text="Back to Change Request" />
            <input id="HDDgSignTotal" runat="server" type="hidden" /></div>
    </form>
</body>
</html>
