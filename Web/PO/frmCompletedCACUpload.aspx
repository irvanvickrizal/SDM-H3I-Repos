<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCompletedCACUpload.aspx.vb" Inherits="PO_frmCompletedCACUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KPI Acceptance ready Creation</title>
    <style type="text/css">
        .HeaderReport {
            background-color: #cfcfcf;
            font-family: Trebuchet MS;
            font-size: 16px;
            font-weight: bold;
            margin-bottom: 10px;
            padding: 4px;
        }

        .GridHeader {
            background-color: #fff200;
            font-family: verdana;
            font-weight: bold;
            font-size: 12px;
            text-align: center;
            height: 20px;
            color: black;
        }

        .GridOddRows {
            font-family: verdana;
            font-size: 8pt;
        }

        .GridEvenRows {
            background-color: #cfcfcf;
            font-family: verdana;
            font-size: 8pt;
        }

        .BtnExpt {
            border-style: solid;
            border-color: white;
            border-width: 1px;
            font-family: verdana;
            font-size: 11px;
            font-weight: bold;
            color: white;
            width: 120px;
            height: 25px;
            cursor: pointer;
        }

        .btnSubmit {
            font-family: verdana;
            font-size: 10px;
            font-weight: bold;
            color: black;
            padding: 1px;
            cursor: pointer;
        }

        .btnRefresh {
            font-family: verdana;
            font-size: 10px;
            font-weight: bold;
            color: black;
            padding: 1px;
            cursor: pointer;
            height: 25px;
        }

        .AccordionTitle, .AccordionContent, .AccordionContainer {
            position: relative;
            width: 280px;
        }

        .AccordionTitle {
            height: 20px;
            overflow: hidden;
            cursor: pointer;
            font-family: Arial;
            font-size: 8pt;
            font-weight: bold;
            vertical-align: middle;
            text-align: center;
            background-repeat: repeat-x;
            display: table-cell;
            background-color: gray;
            -moz-user-select: none;
        }

        .AccordionContent {
            height: 0px;
            overflow: auto;
            display: none;
        }

        #PleaseWait {
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

        #blur {
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
    <script type="text/javascript">
        function invalidExportToExcel() {
            alert('Data is empty, please try another date!');
            return false;
        }
    </script>

    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
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
    <link href="../css/Pagination.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-xs-12">
	        <div class="row">
		        <div class="box box-info">
			        <div class="box-header with-border">
                        <h3 class="box-title">CAC Last Approval Pending List</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
                            <div class="col-md-4">
                                <div class="form-inline">
                                    <div class="form-group">
                                        <asp:Button ID="BtnExportExcel" runat="server" Text="Export To Excel" CssClass="btn btn-block btn-success" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <span style="display:block; height:50px;"></span>
                            </div>
                            <asp:GridView ID="GvCACReport" runat="server" AllowPaging="true" EmptyDataText="No QC Ready Creation"
                                CellPadding="1" CellSpacing="2" Width="100%" Font-Names="Verdana" Font-Size="11px" PageSize="10"
                                AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed">
                                <PagerSettings Position="TopAndBottom" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                <AlternatingRowStyle CssClass="GridEvenRows" />
                                <RowStyle CssClass="GridOddRows" />
                                <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemStyle Width="35px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Siteno" HeaderText="Site No" />
                                    <asp:BoundField DataField="Site_name" HeaderText="Site Name" />
                                    <asp:BoundField DataField="Pono" HeaderText="PO Number" />
                                    <asp:BoundField DataField="POName" HeaderText="PO Name" />
                                    <asp:BoundField DataField="workpackageid" HeaderText="Package ID" />
                                    <asp:BoundField DataField="ARA_Name" HeaderText="Area" />
                                    <asp:BoundField DataField="rgnname" HeaderText="Region" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href='frmCompletedCACUploadDetail.aspx?swid=<%#Eval("SW_Id") %>&wpid=<%#Eval("workpackageid") %>&from=RC' style="text-decoration: none; border-style: none;">
                                                <img src="../Images/doce.gif" alt="CAC Upload" id="pdfIcon" height="18" width="18"
                                                    style="text-decoration: none; border-style: none;" />
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
				        </div>
			        </div>
		        </div>
	        </div>
        </div>
    </form>
    <%--OLD--%>
<%--    <form id="form1" runat="server">
        <div style="width: 99%; height: 450px">
            <div class="HeaderReport">
                <table width="100%">
                    <tr>
                        <td style="width: 80%">CAC Last Approval Pending List
                        </td>
                        <td style="width: 15%; text-align: right;">
                            <asp:Button ID="BtnExportExcel" runat="server" Text="Export To Excel" CssClass="BtnExpt"
                                BackColor="#7f7f7f" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:GridView ID="GvCACReport" runat="server" AllowPaging="true" EmptyDataText="No QC Ready Creation"
                    CellPadding="1" CellSpacing="2" Width="100%" Font-Names="Verdana" Font-Size="11px" PageSize="15"
                    AllowSorting="True" AutoGenerateColumns="False">
                    <PagerSettings Position="TopAndBottom" />
                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                    <AlternatingRowStyle CssClass="GridEvenRows" />
                    <RowStyle CssClass="GridOddRows" />
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Siteno" HeaderText="Site No" />
                        <asp:BoundField DataField="Site_name" HeaderText="Site Name" />
                        <asp:BoundField DataField="Pono" HeaderText="PO Number" />
                        <asp:BoundField DataField="POName" HeaderText="PO Name" />
                        <asp:BoundField DataField="workpackageid" HeaderText="Package ID" />
                        <asp:BoundField DataField="ARA_Name" HeaderText="Area" />
                        <asp:BoundField DataField="rgnname" HeaderText="Region" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a href='frmCompletedCACUploadDetail.aspx?swid=<%#Eval("SW_Id") %>&wpid=<%#Eval("workpackageid") %>&from=RC' style="text-decoration: none; border-style: none;">
                                    <img src="../Images/doce.gif" alt="CAC Upload" id="pdfIcon" height="18" width="18"
                                        style="text-decoration: none; border-style: none;" />
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        Modified by Fauzan, 7 Nov 2018. Remove 3 Logo
        <div style="text-align: right; width: 99%; vertical-align: bottom;">
            <img src="~/images/three-logo.png" alt="threelogo" runat="server" id="threelogoid" height="70" width="50" />
        </div>
    </form>--%>
</body>
</html>
