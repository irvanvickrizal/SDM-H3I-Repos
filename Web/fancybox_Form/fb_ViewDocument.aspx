<%@ Page Language="VB" AutoEventWireup="false" CodeFile="fb_ViewDocument.aspx.vb"
    Inherits="fancybox_Form_fb_ViewDocument" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Document</title>
    <style type="text/css">
        #panelHeader
        {
            background-color:#cfcfcf;
            padding:5px;
            font-family:verdana;
            font-size:15px;
            font-weight:bolder;
            color:#ffffff;
            width:99%;
            border-radius: 3px; -moz-border-radius: 3px; -webkit-border-radius: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="panelHeader">
            <span id="spDocumentName" runat="server"></span>
        </div>
        <div style="height: 100%; width: 100%; margin-top:5px;">
            <iframe runat="server" id="docView" width="100%" height="575px" scrolling="auto"></iframe>
        </div>
    </form>
</body>
</html>
