<%@ Page Language="VB" AutoEventWireup="false" CodeFile="B4DashboardS.aspx.vb" Inherits="B4DashboardS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html  xmlns="http://www.w3.org/1999/xhtml" >
     <head>    
     <title></title>    
     <script type="text/javascript">    
         //the details page you want to display... 
         //bugfix110328 disable the dashboard and redirect tor the document upload screen
         var url = 'USR/frmPartnerSetup.aspx'; 
     </script>    
     <style type="text/css">    
         .loading-indicator {    
             font-size:8pt;    
             background-image: url(progress-bar.gif);  
             background-repeat: no-repeat;      
             background-position:top left;    
             padding-left:20px;    
             height:18px;    
             text-align:left;    
         }    
         #loading{    
             position:absolute;    
             left:45%;    
             top:40%;    
             border:1px solid #0e1b42;    
             background-image: url(progress-bar.gif); 
             background-repeat: no-repeat;
             padding:10px;    
             font:bold 14px verdana,tahoma,helvetica;    
             color:#003366;    
             width:500px;    
             text-align:center;    
         }    
     </style>    
     <div id="loading" >    
         <div>    
             Dashboard Loading.Please wait ... 
         </div>    
     </div>    
     </head>    
     <body onload="location.href = url;" style="overflow:hidden;overflow-y:hidden">    
     </body>    
     <script type="text/javascript">    
        if(document.layers) {    
             document.write('<Layer src="' + url + '" visibility="hide"></Layer>');    
         }    
        else if(document.all || document.getElementById) {    
             document.write('<iframe src="' + url + '" style="visibility:hidden;"></iframe>');    
         }    
        else {    
             location.href = url;    
         }    
     </script>    
</html>
