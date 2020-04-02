<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDgtlCert.aspx.vb" Inherits="frmDgtlCert" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>User Details</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
      
    </head>
<body>
    <form id="form1" runat="server">
    <div>
   <table width="100%">
            <tr><td class="pageTitle">Steps to Install Digital Signature Client Certificate</td></tr>
            <tr><td><br /><a href="../Images/cosign.crt">Click Here</a><b><font class="lblText"> to Download Digital Signature Client Certificate</font></b> </td></tr>
            <tr><td class="lblText"><br />1. Right click on the cosign.crt then click "Install Certificate"</td></tr>
            <tr><td class="lblText">2. On "Certificate Import Wizard" then click Next</td></tr>
            <tr><td class="lblText">3. Select "Place all certificate in the following store"</td></tr>
            <tr><td class="lblText">4. Click "Browse"</td></tr>
            <tr><td class="lblText">5. Select "Trusted Root Certification Authorities" then click OK </td></tr>
            <tr><td class="lblText">6. Then click Next</td></tr>
            <tr><td class="lblText">7. Then click Finish</td></tr>
            <tr><td class="lblText">8. "The import was successful." click OK<br /><br /></td></tr>
            
            <tr><td class="pageTitleSub" style="height: 19px">Steps For Adobe Reader</td></tr>
            <tr><td class="lblText"><br />1. Open the Adobe Reader </td></tr>
            <tr><td class="lblText">2. Select Edit menu and select Preferences </td></tr>
            <tr><td class="lblText">3. Select on Security, Make sure “Verify Signatures when the document is opened” is checked and “View documents in preview document mode when signing” is unchecked.</td></tr>
            <tr><td class="lblText">4. Leave the "Appearance Empty"</td></tr>
            <tr><td class="lblText">5. Then click on the "Advanced Preferences"</td></tr>
            <tr><td class="lblText">6. Select the "Windows Integration" tab</td></tr>
            <tr><td class="lblText">7. Check for all the option available, then click OK</td></tr>
            <tr><td class="lblText">8. Then again OK, to close the "Preferences" window</td></tr>
   </table>
    </div>
        
    </form>
</body>
</html>
