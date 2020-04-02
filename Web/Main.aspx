<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Main.aspx.vb" Inherits="Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Welcome to E-ACCEPTANCE NOKIA</title>
	<!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
            <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
            <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
        <![endif]-->
</head>
<frameset border="0" framespacing="0" rows="30px,*" frameborder="NO" cols="*">
<frame name="topFrame" src="banner.aspx" noresize="noresize" scrolling="no" />
     <frameset border="0" rows="*" cols="15%,1%,84%" >
        <frame name="menuframe" src="frmMenuDisplay.aspx" />
         <frame name="frmSpace" src="Space.aspx" scrolling="no" noresize="noresize"/>
         <frameset border="0" rows="5,*">
          <frame name="frmSpace" src="Space.aspx" scrolling="no" noresize="noresize"/>
         <frame name="mainframe" src="welcome.aspx" />
           </frameset> 
     </frameset>         
 </frameset>
</html>
