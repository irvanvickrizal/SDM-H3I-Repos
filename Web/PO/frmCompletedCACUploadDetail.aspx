<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCompletedCACUploadDetail.aspx.vb" Inherits="PO_frmCompletedCACUploadDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Completed CAC Upload Detail</title>
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />
    <link rel="stylesheet" href="~/bootstrap/css/bootstrap.min.css" />
    <script type="text/javascript">
        function getControlPosition() {
            var Total = document.getElementById('HDDgSignTotal').value;
            for (intCount1 = 0; intCount1 < Total; intCount1++) {
                var divctrl = document.getElementById('DLDigitalSign_ctl0' + intCount1 + '_ImgPostion');
                eval("document.getElementById('DLDigitalSign_ctl0" + intCount1 + "_hdXCoordinate')").value = findPosX(divctrl);
                eval("document.getElementById('DLDigitalSign_ctl0" + intCount1 + "_hdYCoordinate')").value = findPosY(divctrl);
                //alert(findPosX(divctrl) + " , " + findPosY(divctrl));
            }
        }

        function findPosX(imgElem) {
            xPos = eval(imgElem).offsetLeft;
            tempEl = eval(imgElem).offsetParent;
            while (tempEl != null) {
                xPos += tempEl.offsetLeft;
                tempEl = tempEl.offsetParent;
            }
            return xPos;
        }

        function findPosY(imgElem) {
            yPos = eval(imgElem).offsetTop;
            tempEl = eval(imgElem).offsetParent;
            while (tempEl != null) {
                yPos += tempEl.offsetTop;
                tempEl = tempEl.offsetParent;
            }
            return yPos;
        }

        function Showmain(type) {
            if (type == 'Int') {
                alert('Integration date not available');
            }
            if (type == 'IntD') {
                alert('The document cannot be uploaded before the integration date');
            }
            if (type == '2sta') {
                alert('This Document already processed for second stage so cannot upload again ');
            }
            if (type == 'nop') {
                alert('No permission to upload this Document ');
            }
            if (type == 'nyreg') {
                alert('This CAC NY Registered in Document on Parallel MSFI');
            }
            if (type == "successuploaded") {
                alert('Document Successfully Generated');
            }
            window.location = '../PO/frmSiteDocUploadTree.aspx'
        }
        function showErrRejection(type, docname) {
            if (type == 'rmkem') {
                alert('Please fill reason of first');
            }
        }
        function readOnlyCheckBox() {
            return false;
        }
    </script>

    <style type="text/css">
        tr {
            padding: 3px;
        }

        .MainCSS {
            margin-bottom: 0px;
            margin-left: 10px;
            margin-top: 0px;
            width: 800px;
        }

        .lblText {
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
            text-align: left;
        }

        .lblFieldHeader {
            font-family: verdana;
            font-size: 10px;
            color: #000000;
            text-align: center;
            font-weight: bold;
        }

        .lblFieldText {
            font-family: verdana;
            font-size: 9px;
            color: #000000;
            text-align: left;
        }

        .lblFieldBoldText {
            font-family: verdana;
            font-size: 9px;
            color: #000000;
            text-align: left;
            font-weight: bolder;
        }

        .lblTextItalic {
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
            text-align: left;
            font-style: italic;
        }

        .lblBText {
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
            text-align: left;
            font-weight: bold;
        }

        .lblRef {
            font-family: verdana;
            font-size: 11px;
            color: #000000;
            text-align: left;
            font-weight: bold;
        }

        .lblBold {
            font-family: verdana;
            font-size: 18px;
            color: #000000;
            font-weight: bold;
        }

        .textFieldStyle {
            background-color: white;
            border: 1px solid;
            color: black;
            font-family: verdana;
            font-size: 9pt;
        }

        .PagerTitle {
            font-size: 8pt;
            background-color: #cddbbf;
            text-align: Right;
            vertical-align: middle;
            color: #25375b;
            font-weight: bold;
        }

        .Hcap {
            height: 5px;
        }

        .VCap {
            width: 10px;
        }

        .checkDocumentPanel {
            margin-left: 40px;
        }
    </style>

    <style type="text/css">
        .lblTextSmall {
            font-family: verdana;
            font-size: 9px;
            color: #000;
        }

        .siteATTPanel {
            margin-top: 10px;
            height: 70px;
        }

        .SiteDetailInfoPanel {
            margin-top: 10px;
            width: 100%;
            text-align: left;
            height: 150px;
        }

        .sitedescription {
            margin-top: 10px;
            width: 800px;
            height: 60px;
        }

        .headerform {
            margin-top: 15px;
            height: 60px;
        }

        .pnlremarks {
            height: 60px;
        }

        .whitespace {
            height: 5px;
        }

        .pnlNote {
            height: 200px;
        }

        .footerPanel {
            margin-top: 10px;
            height: 250px;
        }

        .btnstyle {
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

        .btnstyleEdit {
            font-family: Verdana;
            font-size: 10px;
            font-weight: bolder;
            border-style: solid;
            background-color: maroon;
            border-width: 2px;
            border-color: Black;
            color: White;
            cursor: pointer;
            padding: 5px;
        }

        #dvPrint {
            width: 800px;
            height: 950px;
            overflow: hidden;
        }

        .HeaderReport {
            background-color: #cfcfcf;
            font-family: Trebuchet MS;
            font-size: 16px;
            font-weight: bold;
            margin-bottom: 10px;
            padding: 4px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <asp:HiddenField ID="hdnpono" runat="server" />
        <asp:HiddenField ID="hdnsiteid" runat="server" />
        <asp:HiddenField ID="hdnsiteno" runat="server" />
        <asp:HiddenField ID="hdnVersion" runat="server" />
        <asp:HiddenField ID="hdnwfid" runat="server" />
        <asp:HiddenField ID="hdnDGBox" runat="server" />
        <asp:HiddenField ID="hdndocid" runat="server" />
        <asp:HiddenField ID="hdnKeyVal" runat="server" />
        <asp:HiddenField ID="hdnScope" runat="server" />
        <div class="panel panel-primary" style="width: 99%">
            <div class="panel-heading">
                <i class="fa fa-tasks fa-fw"></i>Upload Completed CAC
            </div>
            <div class="panel-body">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="lblPN">Project Name</label>
                        <div class="input-group">
                            <asp:Label ID="lblProjectName" runat="server" CssClass="text-primary"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="lblSvcType">Service Type</label>
                        <div class="input-group">
                            <asp:Label ID="lblServiceType" runat="server" CssClass="text-primary"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="lblSvcType">Site ID</label>
                        <div class="input-group">
                            <asp:Label ID="lblSiteID" runat="server" CssClass="text-primary"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="lblSvcType">Site Name</label>
                        <div class="input-group">
                            <asp:Label ID="lblSiteName" runat="server" CssClass="text-primary"></asp:Label>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <h4></h4>
                </div>
                <div class="col-md-8">
                    <div class="form-group">
                        <label for="CACUpload">Completed CAC Upload</label>
                        <div class="input-group">
                            <label class="btn btn-default">
                                <asp:FileUpload ID="fuCACUpload" runat="server" accept=".pdf" />
                            </label>
                            <asp:Button ID="btnUploadBOQ" runat="server" Text="Upload File" Font-Size="12px" CssClass="btn btn-primary" BackColor="Green" OnClientClick="return ValidateCACUpload();" />
                            <div class="clearfix"></div>
                            <span class="text text-danger">***Note: (Only <b>(*.pdf)</b> File is allowed to upload)</span>
                            <br />
                            <asp:Label ID="lblWarningMessageCACUpload" runat="server" CssClass="text-danger"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group" style="width: 40%;">
                        <label for="finalCACApprovedDate">Final CAC Approved Date</label>
                        <div class="input-group">
                            <asp:TextBox ID="TxtCACDate" runat="server" CssClass="form-control"></asp:TextBox>
                            <span class="input-group-addon">
                                <asp:ImageButton ID="ImgCACDate" runat="server" Height="18px" ImageUrl="~/Images/calendar_icon.jpg"
                                    Width="18px" />
                                <cc1:CalendarExtender ID="ceCACDate" runat="server" Format="dd-MMMM-yyyy" PopupButtonID="ImgCACDate"
                                    TargetControlID="TxtCACDate">
                                </cc1:CalendarExtender>
                            </span>
                        </div>
                    </div>

                    <div class="pull-right">
                        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="btn btn-primary" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" />
                    </div>
                </div>
            </div>
        </div>



    </form>
</body>
</html>
