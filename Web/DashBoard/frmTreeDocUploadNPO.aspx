 <%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTreeDocUploadNPO.aspx.vb" Inherits="DashBoard_frmTreeDocUploadNPO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Document</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function Showmain(type, docid = 0) {
            if (type == 'Int') {
                alert('Integration date not vailable');
            }

            if (type == 'IntD') {

                alert('The document cannot be uploaded before the integration date');

            }

            if (type == '2sta') {
                alert('This Document already processed for second stage so cannot upload again ');
            }
            if (type == 'Lnk') {
                alert('Not Allowed to upload this Document Now.');
            }

            if (type == 'nop') {
                alert('No permission to upload this Document ');
            }

            if (type == 'scs') {
                alert('Document Successfully Uploaded');
            }
            //Modified by Fauzan, 30 Nov 2018. Make it dynamic
            window.location = "frmSSVL0RC.aspx?id=" + docid + " "
            //window.location = 'frmSSVL0RC.aspx'
        }

        function checkIsEmpty() {
            var msg = "";
            var e = document.getElementById("ddlsite");
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0) {
                msg = msg + "Site should be select\n"
            }
            e = document.getElementById("ddlsection");
            strUser = e.options[e.selectedIndex].value;
            if (strUser == 0) {
                msg = msg + "Section should be select\n"
            }
            else {
                /*e = document.getElementById("ddlsubsection"); 
                strUser = e.options[e.selectedIndex].value;
                if (strUser == 0)
                {
                    msg = msg + "Sub-Section should be select\n"
                }
                else
                {*/
                e = document.getElementById("ddldocument");
                strUser = e.options[e.selectedIndex].value;
                if (strUser == 0) {
                    msg = msg + "Document should be select\n"
                }
                /*}*/
            }
            if (msg != "") {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
            else {
                //return accessConfirm();
                return true;

            }


        }
        function accessConfirm() {
            var r = confirm("Are you sure you want to save the details?");
            if (r == true) {
                return true;
            }
            else { return false; }
        }

        function myPostBack() {
            var o = window.event.srcElement;
            if (o.tagName == "INPUT" && o.type == "checkbox") {
                __doPostBack("", "");
            }
        }
        function viewSite() {
            window.open('frmServerFiles.aspx', '', 'width=600,height=600,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
        }
    </script>

    <link type="text/css" rel="Stylesheet" href="../CSS/css/smoothness/jquery-ui-1.8.17.custom.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="~/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/dist/css/AdminLTE.min.css" />
    <link rel="stylesheet" href="~/dist/css/skins/_all-skins.min.css" />
    <link rel="stylesheet" href="~/plugins/iCheck/flat/blue.css" />
    <link rel="stylesheet" href="~/plugins/morris/morris.css" />
    <link rel="stylesheet" href="~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" />
    <link rel="stylesheet" href="~/dist/css/font-awesome-4.7.0/css/font-awesome.min.css" />
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-md-6">
            <h3>Site Document Upload</h3>
            <div style="height: 1px; width: 100%; background-color: gray; margin-top: 5px; margin-bottom: 5px;"></div>
            <div class="col-md-3 bg-info">
                <asp:Label ID="siteno" runat="server" CssClass="text-black" Text="SiteNo"></asp:Label>
            </div>
            <div class="col-md-9 bg-primary">
                <asp:Label ID="lblSiteNo" runat="server" CssClass="text-bold" Text="Label"></asp:Label>
            </div>
            <div class="col-md-12">
                <div class="clearfix" style="height: 5px;"></div>
            </div>
            <div class="col-md-3 bg-info">
                <asp:Label ID="Label1" runat="server" CssClass="text-black" Text="Scope"></asp:Label>
            </div>
            <div class="col-md-9 bg-primary">
                <asp:Label ID="lblScope" runat="server" CssClass="text-bold" Text="Label"></asp:Label>
            </div>
            <div class="col-md-12">
                <div class="clearfix" style="height: 5px;"></div>
            </div>
            <div class="col-md-3 bg-info">
                <asp:Label ID="Label2" runat="server" CssClass="text-black" Text="WorkpackageID"></asp:Label>
            </div>
            <div class="col-md-9 bg-primary">
                <asp:Label ID="lblwpid" runat="server" CssClass="text-bold" Text="Label"></asp:Label>
            </div>
            <div class="col-md-12">
                <div class="clearfix" style="height: 5px;"></div>
            </div>
            <div class="col-md-3 bg-info">
                <asp:Label ID="Label3" runat="server" CssClass="text-black" Text="Cust.PONO"></asp:Label>
            </div>
            <div class="col-md-9 bg-primary">
                <asp:Label ID="lblPONO" runat="server" CssClass="text-bold" Text="Label"></asp:Label>
            </div>
            <div class="col-md-12">
                <div class="clearfix" style="height: 5px;"></div>
            </div>
            <div class="col-md-3 bg-info">
                <asp:Label ID="Label4" runat="server" CssClass="text-black" Text="Document"></asp:Label>
            </div>
            <div class="col-md-9 bg-primary">
                <asp:Label ID="lbldoc" runat="server" Text="Label" Width="467px" CssClass="text-bold"></asp:Label>
            </div>
            <div class="col-md-12">
                <div class="clearfix" style="height: 25px;"></div>
            </div>
            <div class="form-inline">
                <label class="file-upload">
                    <span><strong>Document Upload</strong></span>
                    <asp:FileUpload ID="fileUpload" runat="server" EnableTheming="True" />
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </label>
                <div class="form-group">
                    <div class="text-right">
                        <asp:Button ID="btnupload" runat="server" Text="Upload" CssClass="btn btn-primary" ValidationGroup="uploadgrup" />
                        <asp:Button ID="btnLastApproverSubmit" runat="server" Text="Submit to Last Approver" CssClass="btn btn-danger" />
                    </div>
                </div>
            </div>
            <div style="height: 3px;"></div>
            <div class="alert alert-info">
                <strong>Warning!</strong> Max. Upload Attachment File is 5MB.
            </div>
            <div class="col-md-12">
                <asp:RegularExpressionValidator ID="uplValidator" runat="server" ControlToValidate="fileUpload" Display="Dynamic"
                    ErrorMessage="PDF file only!" ValidationExpression="(.+\.([Pp][Dd][Ff]))" ValidationGroup="uploadgrup"></asp:RegularExpressionValidator>
            </div>
            <div class="clearfix"></div>
            <div class="text-right">
                <asp:Button ID="btnback" runat="server" Text="Back to Listing" CssClass="btn btn-default" />
            </div>
        </div>
        <div class="col-md-6">
            <h3>Site History</h3>
             <div style="height: 1px; width: 100%; background-color: gray; margin-top: 5px; margin-bottom: 5px;"></div>
            <div style="margin-top: 5px;">
                       <asp:GridView ID="GvSiteHistory" runat="server" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" color="" Width="100%" Style="font-family: Verdana;" EmptyDataText="No History Found">
                            <%--<HeaderStyle CssClass="gridHeader" />
                            <RowStyle CssClass="gridOdd" />
                            <AlternatingRowStyle CssClass="gridEven" />
                           <PagerStyle CssClass="PagerTitle" Font-Names="Verdana" Font-Size="4pt" Height="3px"
                            HorizontalAlign="Right" VerticalAlign="Middle" />--%>
                           <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" Height="20px" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle CssClass="PagerTitle" Font-Names="Verdana" Font-Size="8pt" Height="5px"
                             HorizontalAlign="Right" VerticalAlign="Middle" />
                           <EmptyDataRowStyle CssClass="panel-warning" BackColor="Red" />
                            <Columns>
                                <asp:TemplateField HeaderText="Remarks" HeaderStyle-ForeColor="black" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="60%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Approval Status" HeaderStyle-ForeColor="black" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblapprovl" runat="server" Text='<%# Eval("approval") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-ForeColor="black"  ItemStyle-VerticalAlign="Middle" ItemStyle-Width="88%">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldate" runat="server" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" Text='<%# Eval("lmdt") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Guid" visible="false" HeaderStyle-ForeColor="black" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="88%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGuid" runat="server" Text='<%# Eval("rGuid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" HeaderStyle-ForeColor="black" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="9%">
                                    <ItemTemplate>
                                        <a href='<%# "frmViewDocument_SiteHistory.aspx?guid=" & Eval("Rguid") %>' target="_blank">View Doc</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
            </div>
        </div>
        <asp:Panel ID="panel1" runat="server" Width="800px">
        </asp:Panel>
        &nbsp;
        <asp:HiddenField ID="hdnkeyval" runat="server" />
        <asp:HiddenField ID="hdnversion" runat="server" />
        <asp:HiddenField ID="hdnpo" runat="server" />
        <asp:HiddenField ID="hdnpoid" runat="server" />
        <asp:HiddenField ID="hdnsiteno" runat="server" />
        <asp:HiddenField ID="hdnsiteid" runat="server" />
        <asp:HiddenField ID="hdndocname" runat="server" />
        <asp:HiddenField ID="hdnapprequired" runat="server" />
        <asp:HiddenField ID="hdnready4baut" runat="server" />
        <input type="hidden" runat="server" id="hdnDGBox" />
        <div style="margin-top: 10px;">
        </div>
    </form>
</body>
</html>
