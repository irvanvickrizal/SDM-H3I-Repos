<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CR-Digital-signature.aspx.vb"
    Inherits="Digital_Sign_CR_Digital_signature" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document Approval/Reviewal</title>
    <link href="../CSS/CheckList.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
            
            function WindowsCloseCRApprover(){
                alert('Signed Sucessfully');
                var siteno = getQueryVariable('siteno');
                var wpid = getQueryVariable('wpid');
                window.opener.location.href = '../dashboard/frmDocCRApproved.aspx?wpid=' + wpid + '&TId=0';
                if (window.opener.progressWindow) {
                    window.opener.progressWindow.close();
                }
                window.close();
            }
            
            function WindowsCloseCRReviewer(){
                alert('Reviewed Sucessfully.');
                var siteno = getQueryVariable('siteno');
                var wpid = getQueryVariable('wpid');
                window.opener.location.href = '../dashboard/frmDocCRApproved.aspx?wpid=' + wpid + '&TId=0';
                if (window.opener.progressWindow) {
                    window.opener.progressWindow.close();
                }
                window.close();
            }
            
            function WindowRejectCloseCR(){
                var siteno = getQueryVariable('siteno');
                var wpid = getQueryVariable('wpid');
                window.opener.location.href = '../dashboard/frmDocCRApproved.aspx?wpid=' + wpid + '&TId=0&doctype=qcol';
                if (window.opener.progressWindow) {
                    window.opener.progressWindow.close();
                }
                window.close();
            }
            
            function getQueryVariable(variable){
                var query = window.location.search.substring(1);
                var vars = query.split("&");
                for (var i = 0; i < vars.length; i++) {
                    var pair = vars[i].split("=");
                    if (pair[0] == variable) {
                        return pair[1];
                    }
                }
            }
            
            function waitPreloadPage(){ //DOM
                document.getElementById('prepage').style.display = '';
                if (document.getElementById) {
                    document.getElementById('prepage').style.visibility = 'hidden';
                }
                else {
                    if (document.layers) { //NS4
                        document.prepage.visibility = 'hidden';
                    }
                    else { //IE4
                        document.all.prepage.style.visibility = 'hidden';
                    }
                }
            }
            var atLeast = 1
            function CheckItem()
            {
                var chk = ValidateCheckList();
                var remrks = ValidateRemarks();
                if (chk == false)
                {
                    alert("Please tick at least one Reason");
                    return false;
                }
                else
                {
                    if(remrks == false)
                    {
                        alert("Please describe in detail your reason of rejection");
                        return false;
                    }
                    else
                    {
                        var answer = confirm("Are you sure want to reject this document?");
                        if (answer){
		                    return true;
	                    }
	                    else{
		                    return false;
	                    }
                    }
                }
                return true;
            }
            
            function ValidateRemarks()
            {
                var remarks = document.getElementById("<%=TxtRemarksReject.ClientID %>");
                if (remarks.value.length < 10)
                {
                    return false;
                }
                return true;
            }
            
            function ValidateCheckList(){     
                var CHK = document.getElementById("<%=CbList.ClientID%>"); 
                var checkbox = CHK.getElementsByTagName("input");
                var counter=0;
                for (var i=0;i<checkbox.length;i++)
                {
                    if (checkbox[i].checked)
                    {
                        counter++;
                    }
                }
                if(atLeast>counter)
                {
                    return false;
                }
                return true;
            }
    </script>

    <script type="text/javascript">
        var atLeastSignReject = 1
            function CheckItemSignReject()
            {
                var remarksSignReject = ValidateRemarksSignReject();
                var chkSignReject = ValidateCheckListSignReject();
                
                if (chkSignReject == false)
                {
                    alert("Please tick at least one Reason");
                    return false;
                }
                
                else if (remarksSignReject == false){
                       alert("Please describe in detail your reason of rejection");
                        return false;
                }
                else
                {
                       var answer = confirm("Are you sure want to reject this document?");
                        if (answer){
		                    return true;
	                    }
	                    else{
		                    return false;
	                    }
                }
            }
            
            function ValidateRemarksSignReject()
            {
                var remarks = document.getElementById("<%=TxtRemarks_SignRejectPanel.ClientID %>");
                if (remarks.value.length < 10)
                {
                    return false;
                }
                return true;
            }
            
            function ValidateCheckListSignReject(){     
                var CHK = document.getElementById("<%=CbReasonLists.ClientID%>"); 
                var checkbox = CHK.getElementsByTagName("input");
                var counter=0;
                for (var i=0;i<checkbox.length;i++)
                {
                    if (checkbox[i].checked)
                    {
                        counter++;
                    }
                }
                if(atLeastSignReject>counter)
                {
                    return false;
                }
                return true;
            }
    </script>

    <style type="text/css">
        .lblBRText
        {
            font-family: Arial Unicode MS;
            font-size: 9pt;
            background-color:#cfcfcf;
        }
    </style>
    <style type="text/css">
        #blur
        {
            width: 100%;
            background-color: #ffffff;
            moz-opacity: 0.5;
            khtml-opacity: .5;
            opacity: .5;
            filter: alpha(opacity=50);
            z-index: 120;
            height: 100%;
            position: absolute;
            top: 0;
            left: 0;
        }
        #progress
        {
            z-index: 200;
            background-color: White;
            position: absolute;
            top: 0pt;
            left: 0pt;
            border: solid 1px black;
            padding: 5px 5px 5px 5px;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdncrid" runat="server" />
        <asp:HiddenField ID="hdnwfid" runat="server" />
        <asp:HiddenField ID="hdnTaskid" runat="server" />
        <asp:HiddenField ID="hdnwpid" runat="server" />
        <asp:HiddenField ID="hdnDocPath" runat="server" />
        <asp:HiddenField ID="hdnx" runat="server" />
        <asp:HiddenField ID="hdny" runat="server" />
        <asp:HiddenField ID="hdnpageNo" runat="server" />
        <asp:HiddenField ID="hdnPono" runat="server" />
        <asp:HiddenField ID="hdnRoleId" runat="server" />
        <asp:ScriptManager ID="SMCR" runat="server">
        </asp:ScriptManager>
        <script language="javascript" type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(
                function () {
                    if (document.getElementById) {
                        var progress = document.getElementById('progress');
                        var blur = document.getElementById('blur');
                        progress.style.width = '300px';
                        progress.style.height = '30px';
                        blur.style.height = document.documentElement.scrollHeight;
                        progress.style.top = document.documentElement.scrollHeight/3 - progress.style.height.replace('px','')/2 + 'px';
                        progress.style.left = document.body.offsetWidth/2 - progress.style.width.replace('px','')/2 + 'px';

                    }
                }
            )
        </script>
        <div>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%"
                id="TABLE1">
                <tr>
                    <td align="center">
                        <iframe runat="server" id="PDFViwer" width="99%" height="750px" scrolling="auto"></iframe>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="lblBRText" colspan="4">
                        <div id="divReviewer" runat="server" style="width: 100%; text-align: left; margin-left: 5px;">
                        </div>
                    </td>
                </tr>
                <tr id="listdocuments" runat="server">
                    <td class="hgap" style="width: 972px">
                        <asp:GridView ID="grddocuments" runat="server" Width="100%" AutoGenerateColumns="False"
                            EmptyDataText="No Records Found" DataKeyNames="CRSiteDocId">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total ">
                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:Label ID="LblSubDocid" runat="server" Text='<%#Eval("CRSiteDocId") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DocName" HeaderText="Document">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField ControlStyle-Width="180px">
                                    <ItemTemplate>
                                        <asp:RadioButtonList ID="rdbstatus" runat="server" AutoPostBack="false" RepeatDirection="Horizontal"
                                            RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0">
                                                    Approve
                                            </asp:ListItem>
                                            <asp:ListItem Value="1">
                                                    Reject
                                            </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtremarks" runat="server" Columns="40" CssClass="textFieldStyle"
                                            Rows="5" TextMode="MultiLine" Visible="false">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="hgap" style="width: 972px">
                        <asp:GridView ID="grddocuments2" runat="server" Width="100%" AutoGenerateColumns="False"
                            EmptyDataText="No Records Found" DataKeyNames="CRSiteDocId" OnRowDataBound="grddocuments2_RowDataBound">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:Label ID="LblSubDocid" runat="server" Text='<%#Eval("CRSiteDocId") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DocName" HeaderText="Document">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButtonList ID="rdbstatus" runat="server" AutoPostBack="false"
                                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0">
                                                    Approve
                                            </asp:ListItem>
                                            <asp:ListItem Value="1">
                                                    Reject
                                            </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtremarks" runat="server" Columns="40" CssClass="textFieldStyle"
                                            Rows="5" TextMode="MultiLine" Visible="false">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 972px" id="dgrow" runat="server">
                        <div style="margin-right: 5px;">
                            <div style="border-style: solid; border-color: Gray; border-width: 1px; margin-left: 2px;
                                margin-right: 2px; width: 450px; text-align: left;">
                                <div style="font-family: Verdana; font-size: 12px; font-weight: bold; background-color: #c3c3c3;
                                    padding: 3px; margin-bottom: 10px; text-align: center;">
                                    Digital Signature Login
                                </div>
                                <div>
                                    <div style="text-align: left; width: 450px;">
                                        <asp:UpdateProgress ID="UpgPanelSign" runat="server" DisplayAfter="0">
                                            <ProgressTemplate>
                                                <div id="blur">
                                                    <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                                                        <img src="../Images/animation_processing.gif" style="vertical-align: middle" width="150"
                                                            height="150" alt="Processing" />
                                                    </div>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:UpdatePanel ID="UpPanelSign" runat="server">
                                            <ContentTemplate>
                                                <asp:MultiView ID="MvPanelSign" runat="server">
                                                    <asp:View ID="defaultSign" runat="server">
                                                        <asp:Label ID="LblDocPath" runat="server"></asp:Label>
                                                        <asp:Label ID="LblPageNo" runat="server"></asp:Label>
                                                        <table cellpadding="1" cellspacing="0" width="100%">
                                                            <tr valign="top">
                                                                <td style="width: 15%">
                                                                    <span style="font-family: Verdana; font-size: 8.5pt;">Username</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="txtField" Width="95%">
                                                                    </asp:TextBox><span style="color: Red;">*</span>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                                                        ErrorMessage="Enter User Name" ValidationGroup="vgSign">
                                                                    </asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr valign="top">
                                                                <td style="width: 25%">
                                                                    <span style="font-family: Verdana; font-size: 8.5pt;">Password</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="txtField"
                                                                        Width="95%">
                                                                    </asp:TextBox><span style="color: Red;">*</span>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                                                        ErrorMessage="Enter Password" ValidationGroup="vgSign">
                                                                    </asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr valign="top">
                                                                <td colspan="2" style="width: 98%; text-align: right;">
                                                                    <asp:Button ID="BtnSign" runat="server" Text="Sign" ValidationGroup="vgSign" Width="80px"
                                                                        CssClass="buttonStyle" OnClientClick="return confirm('Are you sure you want to Approve this document?')" />&nbsp;<asp:Button
                                                                            ID="btnReject" runat="server" CssClass="buttonStyle" Text="Reject" Visible="False" /><asp:ValidationSummary
                                                                                ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                                ShowSummary="False" ValidationGroup="vgSign" />
                                                                    <asp:Button ID="BtnSignReject" runat="server" Text="Reject" CssClass="buttonStyle" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:View>
                                                    <asp:View ID="defaultSignReject" runat="server">
                                                        <div>
                                                            <table>
                                                                <tr valign="top">
                                                                    <td>
                                                                        <div style="padding-top: 5px;">
                                                                            <span class="lblText">Reason </span>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBoxList ID="CbReasonLists" runat="server" CssClass="lblText">
                                                                        </asp:CheckBoxList>
                                                                    </td>
                                                                </tr>
                                                                <tr valign="top">
                                                                    <td>
                                                                        <span class="lblText">Remarks </span>
                                                                    </td>
                                                                    <td>
                                                                        <div style="padding-left: 8px;">
                                                                            <asp:TextBox ID="TxtRemarks_SignRejectPanel" runat="server" TextMode="MultiLine"
                                                                                Height="80px" CssClass="textFieldStyle" Width="380px"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div style="text-align: right; width: 440px;">
                                                            <asp:Button ID="BtnSubmitSignReject" runat="server" Text="Submit" CssClass="buttonStyle"
                                                                OnClientClick="return CheckItemSignReject();" CausesValidation="false" />
                                                            <asp:Label ID="LblErrorSignReject" runat="server" Visible="false"></asp:Label>
                                                            <asp:Button ID="BtnCancelSignReject" runat="server" Text="Cancel" CssClass="buttonStyle"
                                                                CausesValidation="false" />
                                                        </div>
                                                    </asp:View>
                                                </asp:MultiView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="BtnSignReject" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnCancelSignReject" />
                                                
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div>
                                            <div style="width: 325px; margin-top: 10px; text-align: left;">
                                                <asp:LinkButton ID="lnkrequest" OnClientClick="this.style.display = 'none'; loadingdiv.style.display = '';"
                                                    runat="server">
                                                                    <div style="border-style:none; padding:2px; width:160px; vertical-align:middle;">
                                                                        <img src="../Images/reqPassword.png" alt="reqPassicon" style="border-style:none;" width="18" height="18" /> 
                                                                        <span style="font-family:Verdana; font-size:9pt; font-weight:bold; text-decoration:underline; color:Maroon;">
                                                                            Request Password
                                                                        </span>
                                                                    </div>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <table border="0" cellpadding="1" cellspacing="2" style="width: 100%;">
                                    <tr>
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr id="rerow" runat="server">
                    <td>
                        <asp:UpdateProgress ID="upgATPReport" AssociatedUpdatePanelID="upPanelReview" runat="server" DisplayAfter="0">
                            <ProgressTemplate>
                                <div id="blur">
                                    <div id="progress">
                                        <img src="../Images/animation_processing.gif" style="vertical-align: middle" width="150"
                                            height="150" alt="Processing" />
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="upPanelReview" runat="server">
                            <ContentTemplate>
                                <asp:MultiView ID="MvApprovalButtonPanel" runat="server">
                                    <asp:View ID="vwDefaultPanel" runat="server">
                                        <asp:Button ID="btnreview" runat="server" Text="Review" ValidationGroup="vgSign"
                                            Width="120px" CssClass="buttonStyle" OnClientClick="return confirm('Are you sure you want to Approve this document?')" />
                                        <asp:Button ID="BtnRejectReviewNew" runat="server" Text="Reject" CausesValidation="false"
                                            Width="120px" CssClass="buttonStyle" />
                                        <asp:Button ID="btnrejectreview" Visible="false" runat="server" CssClass="buttonStyle"
                                            Text="Reject" />
                                    </asp:View>
                                    <asp:View ID="vwRejectPanel" runat="server">
                                        <div>
                                            <table>
                                                <tr valign="top">
                                                    <td>
                                                        <div style="padding-top: 5px;">
                                                            <span class="lblText">Reason </span>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBoxList ID="CbList" runat="server" CssClass="lblText">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                                <tr valign="top">
                                                    <td>
                                                        <span class="lblText">Remarks </span>
                                                    </td>
                                                    <td>
                                                        <div style="padding-left: 8px;">
                                                            <asp:TextBox ID="TxtRemarksReject" runat="server" TextMode="MultiLine" Height="80px"
                                                                CssClass="textFieldStyle" Width="400px"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="text-align: right; width: 460px;">
                                            <asp:Button ID="BtnSubmitReject" runat="server" Text="Submit" CssClass="buttonStyle"
                                                OnClientClick="return CheckItem()" CausesValidation="false" />
                                            <asp:Label ID="LblWPID" runat="server" Visible="false"></asp:Label>
                                            <asp:Button ID="BtnCancelSubmit" runat="server" Text="Cancel" CssClass="buttonStyle"
                                                CausesValidation="false" />
                                        </div>
                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnRejectReviewNew" />
                                <asp:AsyncPostBackTrigger ControlID="BtnCancelSubmit" />
                                <asp:AsyncPostBackTrigger ControlID="BtnSubmitReject" />
                                <asp:AsyncPostBackTrigger ControlID="BtnCancelSubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div id="loadingdiv" runat="server" style="display: none; position: absolute; width: 95%;
            text-align: right; top: 91%; left: 18px;">
            <img src="../sendsms.GIF" runat="server" id="loading" alt="sendSMSIcon" />
        </div>
    </form>
</body>
</html>
