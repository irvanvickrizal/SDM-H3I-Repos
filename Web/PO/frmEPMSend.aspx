<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEPMSend.aspx.vb" Inherits="PO_frmEPMSend"  EnableEventValidation="false" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EPM Data Send</title>
    <link type="text/css" rel="stylesheet" href="../CSS/Styles.css" />
    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>
    <script language="javascript" type="text/javascript">
      function __doPostBack(eventTarget, eventArgument) 
      {  
          var theform = document.form1;  
          theform.__EVENTTARGET.value = eventTarget;  
          theform.__EVENTARGUMENT.value = eventArgument;  
          theform.submit();  
      }  
    </script>

</head>
<body>
    <form id="form1" runat="server" method="post" action="http://www.thetechfort.com:8051/api/processfile.aspx" enctype="multipart/form-data">
        <div>
          <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <%--<asp:UpdatePanel ID="updPanel" runat="server">
                <ContentTemplate>--%>
                    <table width="100%">
                        <tr class="pageTitle">
                            <td runat="server" id="rowTitle" colspan="3">EPM Data Upload</td>
                        </tr>
                         <tr><td class="lblText" colspan="3">From : <input type="text" id="txtFrom" runat="server" class="textFieldStyle"/>&nbsp;&nbsp;
                            <asp:ImageButton ID="btnFrom" runat="server"
                             Height="16px" ImageUrl="~/Images/calendar_icon.jpg" Width="18px"/>&nbsp;&nbsp;To&nbsp;&nbsp;
                             <input type="text" id="txtTo" runat="server" class="textFieldStyle"/> &nbsp;&nbsp;
                             <asp:ImageButton ID="btnTo" runat="server"
                             Height="16px" ImageUrl="~/Images/calendar_icon.jpg" Width="18px"/>
                             </td></tr>
                        <tr visible="false"><td class="lblTitle" style="width: 10%"></td>
                            <td width="1%"></td>
                            <td>
                                &nbsp; &nbsp;
                            <input type="hidden" name="__EVENTTARGET" value="" /> 
		                    <input type="hidden" name="__EVENTARGUMENT" value="" /> 
		                    <%--<input type="button" name="btnGenerate1" value="Generate Data" id="btnGenerate1" class="buttonStyle" style="width:100px"/>--%>
		                    <asp:Button ID="btnGenerate" runat="server" Text="Generate" cssclass="buttonStyle" style="width:100px" />
	                      <a id="Test" href="javascript:__doPostBack('btnGenerate','')">Send Data</a>
                            </td>
                        </tr>
                        <tr visible="false">
                            <td class="lblTitle" colspan="3" style="text-align: left">
                            <asp:Label runat="server" id="lbl1" CssClass="lblText" EnableViewState="False" Width="709px"></asp:Label>
                            </td>
                        </tr>
                       
                        <tr><td colspan="3">
                                <asp:GridView ID="grdExport" runat="server" AutoGenerateColumns="False" Width="100%" Visible="False">
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                    <AlternatingRowStyle CssClass="GridEvenRows" />
                                    <RowStyle CssClass="GridOddRows" />
                                    <Columns>
                                       
                                        <asp:BoundField DataField="SiteId" HeaderText="SITE ID" />
                                        <asp:BoundField DataField="WorkPackageId" HeaderText="WPID" >
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                                    
                                        <asp:BoundField DataField="ACT_9500" HeaderText="MS9500" />
                                        <asp:BoundField DataField="ACT_9750" HeaderText="MS9750" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
               <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
    </form>
</body>
</html>
