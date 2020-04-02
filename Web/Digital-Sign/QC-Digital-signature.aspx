<%@ Page Language="VB" AutoEventWireup="false" CodeFile="QC-Digital-signature.aspx.vb"
    Inherits="Digital_Sign_QC_Digital_signature" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document Approval/Reviewal</title>
    <link href="../CSS/CheckList.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ltrLabel
        {
            font-family: verdana;
            font-size: 11px;
            color: #000;
        }
        .lblText
        {
            font-family: verdana;
            font-size: 11px;
            color: #000;
        }
        .lblTextSmall
        {
            font-family: verdana;
            font-size: 10px;
            color: #000;
        }
        .lblBold
        {
            font-family: verdana;
            font-size: 16px;
            color: #000;
            font-weight: bold;
        }
        .lblField
        {
            font-family: verdana;
            font-size: 11px;
            font-weight: bolder;
            color: #000;
        }
        .lblFieldSmall
        {
            font-family: verdana;
            font-size: 9px;
            font-weight: bolder;
            color: #000;
        }
        .lblFieldSmallRed
        {
            font-family: verdana;
            font-size: 10px;
            font-weight: bolder;
            color: White;
        }
        .HeaderPanel
        {
            background-color: #cfcfcf;
            font-family: verdana;
            font-size: 13px;
            font-weight: bold;
            margin-bottom: 10px;
            padding-top: 5px;
            padding-bottom: 5px;
            padding-left: 5px;
        }
        .HeaderGrid
        {
            font-family: Verdana;
            font-size: 11px;
            font-weight: bold;
            color: white;
            background-color: Orange;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            vertical-align: middle;
            padding-top: 5px;
            padding-bottom: 5px;
        }
        .oddGrid
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: White;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }
        .evenGrid
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: #cfcfcf;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }
        .emptyrow
        {
            font-family: Verdana;
            font-size: 11px;
            color: Red;
            font-style: italic;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            padding: 5px;
        }
        .footerGrid
        {
            font-family: Verdana;
            font-size: 11px;
            color: white;
            font-style: italic;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            padding: 5px;
        }
        .btnstyle
        {
            font-family: Verdana;
            font-size: 10px;
            font-weight: bolder;
            border-style: solid;
            background-color: Green;
            border-width: 2px;
            border-color: Black;
            color: White;
            cursor: pointer;
            padding: 5px;
        }
        .btnstylegray
        {
            font-family: Verdana;
            font-size: 10px;
            font-weight: bolder;
            border-style: solid;
            background-color: gray;
            border-width: 2px;
            border-color: Black;
            color: White;
            cursor: pointer;
            padding: 5px;
        }
        .btnSearch
        {
            height: 29px;
            width: 80px;
            background-image: url(../Images/button/BtSearch_0.gif);
        }
        .btnSearch:hover
        {
            height: 29px;
            width: 80px;
            background-image: url(../Images/button/BtSearch_1.gif);
        }
        .btnSearch:click
        {
            height: 29px;
            width: 80px;
            background-image: url(../Images/button/BtSearch_2.gif);
        }
        .warningMessage
        {
            font-family: Verdana;
            font-size: 11px;
            font-weight: bolder;
            color: Red;
            font-style: italic;
        }
        #PleaseWait
        {
            z-index: 200;
            position: absolute;
            top: 0pt;
            left: 0pt;
            text-align: center;
            height: 100px;
            width: 100px;
            background-image: url(../Images/preloader.gif);
            background-repeat: no-repeat;
            margin: 0 10%;
            margin-top: 10px;
        }
        #blur
        {
            width: 100%;
            background-color: #ffffff;
            moz-opacity: 0.7;
            khtml-opacity: .7;
            opacity: .7;
            filter: alpha(opacity=70);
            z-index: 120;
            height: 100%;
            position: absolute;
            top: 0;
            left: 0;
        }
    </style>

    <script language="javascript" type="text/javascript">
            
            function WindowsCloseQCApprover(){
                alert('Signed Sucessfully');
                var siteno = getQueryVariable('siteno');
                var wpid = getQueryVariable('wpid');
                window.opener.location.href = '../dashboard/frmDocQCApproved.aspx?id=' + siteno + '&wpid=' + wpid + '&TId=0&doctype=qcol';
                if (window.opener.progressWindow) {
                    window.opener.progressWindow.close();
                }
                window.close();
            }
            
            function WindowsCloseQCReviewer(){
                alert('Reviewed Sucessfully.');
                var siteno = getQueryVariable('siteno');
                var wpid = getQueryVariable('wpid');
                window.opener.location.href = '../dashboard/frmDocQCApproved.aspx?id=' + siteno + '&wpid=' + wpid + '&TId=0';
                if (window.opener.progressWindow) {
                    window.opener.progressWindow.close();
                }
                window.close();
            }
            
            function WindowRejectCloseQC(){
                var siteno = getQueryVariable('siteno');
                var wpid = getQueryVariable('wpid');
                window.opener.location.href = '../dashboard/frmDocQCApproved.aspx?id=' + siteno + '&wpid=' + wpid + '&TId=0&doctype=qcol';
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

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(
        function () {
                if (document.getElementById) {
                    var progress = document.getElementById('PleaseWait');
                    var blur = document.getElementById('blur');
                    progress.style.width = '100%';
                    progress.style.height = '100%';
                }
            }
        )
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdndocid" runat="server" />
        <asp:HiddenField ID="hdnParentDocid" runat="server" />
        <asp:HiddenField ID="hdnTaskid" runat="server" />
        <asp:HiddenField ID="hdnSiteid" runat="server" />
        <asp:HiddenField ID="hdnSiteVersion" runat="server" />
        <asp:ScriptManager ID="sm1" runat="server">
        </asp:ScriptManager>
        <div style="width: 100%">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%"
                id="TABLE1">
                <tr>
                    <td align="center">
                        <iframe runat="server" id="PDFViwer" width="99%" height="750px" scrolling="auto"></iframe>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="lblText" colspan="4">
                        <div id="divReviewer2" runat="server" style="width: 100%; text-align: left;">
                        </div>
                    </td>
                </tr>
                <tr id="listdocuments" runat="server">
                    <td class="hgap" style="width: 972px">
                        <asp:GridView ID="grddocuments" runat="server" Width="100%" AutoGenerateColumns="False"
                            EmptyDataText="No Records Found" DataKeyNames="Doc_Id">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total ">
                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblno" runat="Server">
                                        </asp:Label>
                                        <asp:Label Id="LblDocid" runat="server" Text='<%#Eval("doc_id") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="doc_id" HeaderText="doc" />
                                <asp:BoundField DataField="docpath" HeaderText="Path" />
                                <asp:BoundField DataField="DocName" HeaderText="Document">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SW_Id" />
                                <asp:BoundField DataField="site_id" />
                                <asp:BoundField DataField="pono" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButtonList ID="rdbstatus" runat="server" 
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
                    <td class="hgap" style="width: 972px">
                        <asp:GridView ID="grddocuments2" runat="server" Width="100%" AutoGenerateColumns="False"
                            EmptyDataText="No Records Found" DataKeyNames="Doc_Id" OnRowDataBound="grddocuments2_RowDataBound">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total ">
                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblnotwo" runat="Server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="doc_id" HeaderText="doc" />
                                <asp:BoundField DataField="docpath" HeaderText="Path" />
                                <asp:BoundField DataField="DocName" HeaderText="Document">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SW_Id" />
                                <asp:BoundField DataField="site_id" />
                                <asp:BoundField DataField="pono" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButtonList ID="rdbstatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DOThis"
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
                            </div>
                        </div>
                    </td>
                </tr>
                <tr id="rerow" runat="server">
                    <td>
                        <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0">
                            <ProgressTemplate>
                                <div id="blur">
                                    <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
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
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div id="loadingdiv" runat="server" style="display: none; position: absolute; width: 95%;
            text-align: right; top: 91%; left: 18px;">
            <img src="../sendsms.GIF" runat="server" id="loading"  alt="sendSMSIcon"/>
        </div>
    </form>
</body>
</html>
