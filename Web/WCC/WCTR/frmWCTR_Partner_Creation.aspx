<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWCTR_Partner_Creation.aspx.vb"
    Inherits="WCC_WCTR_frmWCTR_Partner_Creation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCTR Partner</title>
    <style type="text/css">
        .headerTitle
        {
            font-family:Verdana;
            font-size:18px;
            font-weight:bolder;
        }
        .headerPanel
        {
            padding:3px;
            border-bottom-color:#000;
            border-bottom-width:2px;
            border-bottom-style:Solid;
            width:100%;
        }
        .labelText
        {
            font-family:verdana;
            font-size:12px;
            text-align:left;
        }
        .labelFieldText
        {
            font-family:verdana;
            font-size:12px;
            font-weight:bolder;
            text-align:left;
        }
        .formPanel
        {
            width:960px;
            text-align:center;
            margin-top:10px;
        }
        .formPanel2
        {
            width:960px; 
            margin-top: 10px; 
            border-bottom-color: #000; 
            border-bottom-style: dashed;
            border-bottom-width: 1px; 
            padding-bottom: 5px;
        }
        .panelDetail
        {
            margin-top:20px;
            padding: 30px; 
            background: #FFF;
            border-radius: 5px; -moz-border-radius: 5px; -webkit-border-radius: 5px;
            box-shadow: 0px 0px 4px rgba(0,0,0,0.7); -webkit-box-shadow: 0 0 4px rgba(0,0,0,0.7); -moz-box-shadow: 0 0px 4px rgba(0,0,0,0.7);
            z-index:13000;
            width:960px;
        }
        .fancybox-custom .fancybox-skin
        {
            box-shadow: 0 0 50px #222;
        }
        .fancybox-title-inside {
            text-align: center;
            font-family:verdana;
            font-size:18px;
        }
        .gridHeader
        {
	        font-family:Verdana;
	        font-size:11px;
	        background-color:maroon;
	        font-weight:bolder;
	        text-align:center;
	        padding:5px;
	        color:white;
	        border-style:solid;
	        border-width:2px;
	        border-color:gray;
        }
        .gridHeader_2
        {
	        font-family:Verdana;
	        font-size:11px;
	        background-color:#ffc727;
	        font-weight:bolder;
	        text-align:center;
	        padding:5px;
	        color:white;
	        border-style:solid;
	        border-width:2px;
	        border-color:gray;
        }
        .gridOdd
        {
            font-family:Verdana;
	        font-size:11px;
	        padding:5px;
        }
        .gridEven
        {
            font-family:Verdana;
	        font-size:11px;
	        background-color:#cfcfcf;
	        padding:5px;
        }
        
        .dnnFormMessage {
            -moz-border-radius: 3px;
            border-radius: 3px;
            padding: 10px 10px 10px 40px;
            line-height: 1.4;
            margin: 0.5em 1em;
        }
        .dnnFormMessage span {
            float: none;
            padding: 0;
            width: 100%;
            text-align: left;
            text-shadow: 0px 1px 1px #fff;
        }
        .dnnFormError {
            color: #fff !important;
            background: url(../images/errorbg.gif) no-repeat left center;
            text-shadow: 0px 1px 1px #000;
            padding: 5px 20px;
            z-index:13000;
            margin-left:-10px;
        }
        .divCenter
        {   
            margin-top:5px;
            text-align:center;
        }
        .lblRemarks
        {
            font-family: Verdana; 
            font-size: 11px; 
            font-weight: bolder; 
            text-align: center;
        }
        .tableinfo
        {
            margin:0 auto;
            width:70%;
            border-style:solid;
            border-color:black;
            border-width:2px;
            padding:2px; 
        }
        #PleaseWait
                {
                    z-index: 200;
                    position: fixed;
                    top: 0pt;
                    left: 0pt;
                    text-align:center;
                    height : 100px;
                    width:100px;
                    background-image: url(../Images/animation_processing.gif);
                    background-repeat: no-repeat;
                    margin: 0 50%; margin-top: 10px;
                }
                #blur
                {
                    width: 100%;
                    background-color:#ffffff;
                    moz-opacity: 0.7;
                    khtml-opacity: .7;
                    opacity: .7;
                    filter: alpha(opacity=70);
                    z-index: 1;
                    height: 100%;
                    position:fixed;
                    top: 0;
                    left: 0;
                }
    .btnApprove
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnApprove_0.gif);
    }
    .btnApprove:hover
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnApprove_1.gif);
    }
    .btnApprove:click
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnApprove_2.gif);
    }
    .btnReject
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnReject_0.gif);
    }
    .btnReject:hover
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnReject_1.gif);
    }
    .btnReject:click
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnReject_2.gif);
    }
    .btnSubmit
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnSubmit_0.gif);
    }
    .btnSubmit:hover
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnSubmit_1.gif);
    }
    .btnSubmit:click
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnSubmit_2.gif);
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="headerPanel">
            <table width="100%;">
                <tr>
                    <td style="width: 30%;">
                        <img src="http://localhost/images/CRCOSubHeader/nsn-logo.gif" id="Img1" alt="NSNLogo" />
                    </td>
                    <td style="width: 69%;">
                        <span class="headerTitle">WORK COMPLETION TIME REPORT (WCTR)</span>
                    </td>
                </tr>
            </table>
        </div>
        <div class="panelDetail">
            <table>
                <tr>
                    <td>
                        <span class="labelFieldText">Name of Partner</span>
                    </td>
                    <td>
                        <asp:Label ID="LblNameofPartner" CssClass="labelText" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <span class="labelFieldText">Project (HOT/HOG)</span>
                    </td>
                    <td>
                        <asp:Label ID="LblPoNo" CssClass="labelText" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <span class="labelFieldText">Site ID / Site Name</span>
                    </td>
                    <td>
                        <asp:Label ID="LblSiteAtt" CssClass="labelText" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <span class="labelFieldText">PO Number / Scope</span>
                    </td>
                    <td>
                        <asp:Label ID="LblPONO_Scope" CssClass="labelText" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <span class="labelFieldText">Region</span>
                    </td>
                    <td>
                        <asp:Label ID="LblRegion" CssClass="labelText" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:TextBox ID="TxtTest" runat="server"></asp:TextBox>
        </div>
    </form>
</body>
</html>
