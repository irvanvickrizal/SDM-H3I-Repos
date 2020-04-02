<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Welcome to Nokia SDM</title>    
    <link href="CSS/Styles.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="Images/nsn-logo.ico" />
    
    <script type="text/javascript">
        function go() {
            window.showModalDialog('frmAllMessages.aspx');
        }

        function getRes() {
            document.getElementById('hdnRes').value = screen.width;
            //alert(document.getElementById('hdnRes').value);
        }

        function checkIsEmpty() {
            var msg = "";
            var a = document.getElementById("ddlType");
            var strUser = a.options[a.selectedIndex].value;
            if (strUser == 0) {
                msg = msg + "Process should be select \n";
            }
            if (msg != "") {
                alert("Mandatory field information : \n\n" + msg);
                return false;
            }
            else {
                return true;
            }
        }

        function showmsg() {
            alert('your password going to expire in 1 weeks time, please change your password');
            window.location = 'Default.aspx?pchecked=yes';
        }
    </script>

    <style type="text/css">
        body {
            letter-spacing: 0;
            color: #434343;
            padding: 20px 0;
            position: relative;
            text-shadow: 0 1px 0 rgba(255,255,255,.8);
            -webkit-font-smoothing: subpixel-antialiased;
        }

        #panelLeft {
            float: Left;
            width: 50%;
            padding-top: 5px;
            padding-left: 20px;
        }

        #panelRight {
            float: right;
            padding-top: 0px;
            width: 45%;
        }

        .txtFieldStyle {
            font-family: verdana;
            font-size: 12px;
            width: 250px;
            border-style: solid;
            border-color: gray;
            border-width: 1px;
            height: 20px;
            padding: 3px;
        }
    </style>

    <style type="text/css" media="Screen">
        .slides_container {
            width: 550px;
            height: 250px;
            display: none;
        }

            .slides_container div {
                width: 550px;
                height: 250px;
                display: block;
            }

        .pagination {
            list-style: none;
            margin: 0;
            padding: 0;
        }

        #frame {
            position: absolute;
            z-index: 0;
            width: 690px;
            height: 300px;
            top: 175px;
            left: 90px;
        }

        .pagination {
            margin: 26px auto 0;
            width: 100px;
            display: none;
        }

            .pagination li {
                float: left;
                margin: 0 1px;
                list-style: none;
            }

                .pagination li a {
                    display: block;
                    width: 12px;
                    height: 0;
                    padding-top: 12px;
                    background-image: url(Images/pagination.png);
                    background-position: 0 0;
                    float: left;
                    overflow: hidden;
                }

                .pagination li.current a {
                    background-position: 0 -12px;
                }

        .labelLogin {
            font-family: Verdana;
            font-size: 8.5pt;
            font-weight: bold;
            color: Maroon;
        }

        .ddlProcess {
            font-family: Verdana;
            font-size: 8.5pt;
            width: 140px;
        }
    </style>

    <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-1.5.1.js"></script>
    <script type="text/javascript" src="Scripts/jquery.corner.js"></script>
    <script type="text/javascript" src="Scripts/slides.min.jquery.js"></script>
    <script type="text/javascript" src="Scripts/BlockUI.js"></script>
    <script type="text/javascript" src="Scripts/jquery.watermark.js"></script>

    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../dist/css/AdminLTE.min.css" />
    <link rel="stylesheet" href="../dist/css/skins/_all-skins.min.css" />
    <link rel="stylesheet" href="../plugins/iCheck/flat/blue.css" />
    <link rel="stylesheet" href="../plugins/morris/morris.css" />
    <link rel="stylesheet" href="../plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" />
    <link rel="stylesheet" href="../dist/css/font-awesome-4.7.0/css/font-awesome.min.css" />

    

</head>
<body onload="getRes();">
    <form id="form1" runat="server">
        <div style="width: 100%; margin: 0 25%; margin-top: 30px;">
            <div style="background-image: url('Images/map.png'); background-repeat: no-repeat; height: 500px; opacity: 0.9; filter: alpha(opacity=90);">
                <div style="width: 100%;">
                    <div id="panelLeft">
                        <div style="height: 20px;"></div>
                    </div>
                    <div id="rightpanel" style="text-align: left;">
                        <div style="padding-top:50px; padding-left: 580px; width: 450px;">
                            
                            <table class="table-condensed">
                                <tr>
                                    <td>
                                        <h2><span style="color:white;font-family:Verdana;">Login Form</span></h2>
                                        <div style="height:3px; background-color:white; width:100%;"></div>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="text" class="form-control" id="txtUserName" placeholder="Fullname" style="color: black;width:250px;" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="password" class="form-control" id="txtPassword" placeholder="Password" runat="server" style="width: 250px; color:black;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <%--101228 user request--%>
                                        <%--<a id="A1" href="USR/frmForgotPwd.aspx" class="lblTitleU" runat="server">Forgot Password</a>--%>
                                        <%-- <a id="A1" onclick="javascript:alert('Please contact administrator or email to helpdesk..')" class="lblTitleU">Forgot Password</a>--%>
                                        <asp:LinkButton ID="LbtResetPassword" runat="server" Text="Forgot your password?"
                                            ForeColor="Maroon" Font-Names="verdana" Font-Size="8.5pt" Font-Bold="true"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div style="clear: both; width: 100%;">
                        <div style="width: 100%; text-align: left; font-size: 15px; font-weight: bolder; margin-left: 20px; padding-top: 155px;">
                            Copyright&copy;2014 Nokia Solutions and Networks. All Rights Reserved.
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <input type="hidden" runat="server" id="hdnRes" />
    </form>
</body>
</html>
