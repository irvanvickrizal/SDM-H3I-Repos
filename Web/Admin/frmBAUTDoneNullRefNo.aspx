<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBAUTDoneNullRefNo.aspx.vb"
    Inherits="Admin_frmBAUTDoneNullRefNo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BAUT Done With Null Reference No</title>
    
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
    <style type="text/css">
        .HeaderReport
        {
            background-color: #cfcfcf;
            font-family: verdana;
            font-size: 13px;
            font-weight: bold;
            margin-top: 15px;
            margin-bottom: 10px;
            padding: 3px;
        }
        .lblText
        {
            font-family: verdana;
            font-size: 12px;
        }
        .lblBoldText
        {
            font-family: verdana;
            font-size: 12px;
            font-weight: bolder;
        }
        .emptyRowStyle
        {
            font-family: verdana;
            font-size: 11px;
            font-weight: bolder;
            color: maroon;
            border-style: solid;
            padding: 3px;
            border-width: 1px;
            border-color: gray;
        }
        .GridHeader
        {
            background-color: #ffc90e;
            font-family: verdana;
            font-weight: bold;
            font-size: 12px;
            text-align: center;
            height: 30px;
            color: white;
        }
        .GridOddRows
        {
            font-family: verdana;
            font-size: 11px;
        }
        .GridEvenRows
        {
            background-color: #cfcfcf;
            font-family: verdana;
            font-size: 11px;
        }
        .btnstyle
        {
            font-family: verdana;
            font-size: 11px;
            font-weight: bolder;
            background-color: #c3c3c3;
            color: #fff;
            padding: 8px;
            cursor: pointer;
            border-width: 1px;
            border-color: white;
            border-style: solid;
        }
        .btnSave
        {
            height: 27px;
            width: 60.5px;
            background-image: url(../Images/button/BtnSave_0.gif);
        }
        .btnSave:hover
        {
            height: 27px;
            width: 60.5px;
            background-image: url(../Images/button/BtnSave_1.gif);
        }
        .btnSave:click
        {
            height: 27px;
            width: 60.5px;
            background-image: url(../Images/button/BtnSave_2.gif);
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
        .BtnExpt
        {
            border-style: solid;
            border-color: white;
            border-width: 1px;
            font-family: verdana;
            font-size: 11px;
            font-weight: bold;
            color: white;
            width: 120px;
            height: 25px;
            cursor: hand;
        }
    </style>
    
    <script type="text/javascript" src="../js/jquery-1.9.min.js"></script>
    <script type="text/javascript" src="../Scripts/SDMController.js"></script>
    <script type="text/javascript">
        function ErrorSave(errMessage)  
        {
            alert("Error While Saving the Data: " + errMessage + " Please Try Again!");
            return false;
        }
        function SucceedSave()  
        {
            alert("Data Successful saved");
            return true;
        }
        function SucceedDelete()  
        {
            alert("Data Successful Deleted");
            return true;
        }
        function ConfirmSave()
        {
            var answer = confirm("Are you sure want to save new injection?");
            if (answer){
                return true;
            }
            else{
                return false;
            }
        }
    </script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(
        function () {
                if (document.getElementById) {
                    var progress = document.getElementById('PleaseWait');
                    var blur = document.getElementById('blur');
                    progress.style.width = '300px';
                    progress.style.height = '30px';
                    blur.style.height = document.documentElement.clientHeight;
                    progress.style.top = document.documentElement.clientHeight/3 - progress.style.height.replace('px','')/2 + 'px';
                    progress.style.left = document.body.offsetWidth/2 - progress.style.width.replace('px','')/2 + 'px';
                }
            }
        )
        function invalidExportToExcel() {
            alert('Data is empty, please try another date!');
            return false;
        }

        function adjustSize() {
            adjustBlurScreen();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="Up1">
            <ProgressTemplate>
                <div id="blur">
                    <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                        <img src="../Images/preloader.gif" style="vertical-align: middle" alt="Processing" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="Up1" runat="server">
            <ContentTemplate>
                <div class="row">
	                <div class="col-xs-12">
		                <div class="box box-info">
			                <div class="box-header with-border">
				                <h3 class="box-title">BAUT Done With Null Reference No</h3>
			                </div>
			                <div class="box-body">
				                <div class="table table-responsive">
					                <div class="col-md-12">
						                <div class="form-horizontal">
							                <div class="form-group">
								                <label class="col-sm-1 control-label">Po No</label>
								                <div class="col-sm-2">
                                                    <asp:DropDownList ID="DdlPoNo" runat="server" CssClass="form-control" />
								                </div>
								                <label class="col-sm-1 control-label">Package Id</label>
								                <div class="col-sm-2">
                                                    <asp:TextBox ID="TxtPackageid" runat="server" CssClass="form-control" />
								                </div>
								                <div class="col-sm-1">
                                                    <asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="btn btn-block btn-info" />
								                </div>
                                                <div class="col-sm-3"></div>
								                <div class="col-sm-2">
                                                    <asp:Button ID="BtnExportExcel" runat="server" Text="Export To Excel" CssClass="btn btn-block btn-primary" />
								                </div>
							                </div>
						                </div>
                                        <div style="margin-top: 10px; min-height: 200px; max-height: 650px; overflow-y: scroll;">
                                            <asp:GridView ID="GvDocBAUT" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                                                HeaderStyle-CssClass="GridHeader" Width="99%" OnRowCancelingEdit="GvDocBAUT_RowCancelingEdit" OnRowDeleting="GvDocBAUT_RowDeleting" OnRowEditing="GvDocBAUT_RowEditing"
                                                OnRowUpdating="GvDocBAUT_RowUpdating">
                                                <RowStyle CssClass="GridOddRows" />
                                                <AlternatingRowStyle CssClass="GridEvenRows" />
                                                <EmptyDataRowStyle CssClass="emptyRowStyle" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="SiteNo" HeaderText="Site No" ReadOnly="true" />
                                                    <asp:BoundField DataField="PoNo" HeaderText="PO No" ReadOnly="true" />
                                                    <asp:BoundField DataField="WorkPackageId" HeaderText="Package Id" ReadOnly="true" />
                                                    <asp:BoundField DataField="TselApprover" HeaderText="Tsel Approver" ReadOnly="true" />
                                                    <asp:BoundField DataField="ApprovedDate" HeaderText="Approved Date" HtmlEncode="false"
                                                        DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" ReadOnly="true" />
                                                    <asp:TemplateField HeaderText="BAUT Status">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="LblBautStatusEdit" runat="server" Text='<%#Eval("BAUTStatus") %>'
                                                                Visible="false"></asp:Label>
                                                            <asp:DropDownList ID="DdlBAUTStatus" runat="server" CssClass="lblText">
                                                                <asp:ListItem Text="Not Yet" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Done" Value="1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBautStatus" runat="server" Text='<%#Eval("BAUTStatusText") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BAUT Ref No">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="LblSiteId" runat="server" Text='<%#Eval("Site_Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="LblVersion" runat="server" Text='<%#Eval("SiteVersion") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="LblPoNo" runat="server" CssClass="lblText" Text='<%#Eval("PONO") %>'
                                                                Visible="false"></asp:Label>
                                                            <asp:Label ID="LblPackageId" runat="server" CssClass="lblText" Text='<%#Eval("Workpackageid") %>'
                                                                Visible="false"></asp:Label>
                                                            <asp:TextBox ID="TxtRefNo" runat="server" CssClass="lblText" Text='<%#Eval("ReferenceNo") %>'
                                                                Width="98%"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblRefNo" runat="server" CssClass="lblText" Text='<%#Eval("ReferenceNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <EditItemTemplate>
                                                            <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/gridview/update.jpg"
                                                                ToolTip="Update" Height="16px" Width="16px" />
                                                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/gridview/Cancel.jpg"
                                                                ToolTip="Cancel" Height="16px" Width="16px" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <a href='../PO/frmViewDocument.aspx?id=<%#Eval("SW_Id") %>' target="_blank" style="text-decoration: none;
                                                                border-style: none;">
                                                                <img src="../Images/Pdf_Icon.png" alt="pdficon" id="pdfIcon" height="18" width="18"
                                                                    style="text-decoration: none; border-style: none;" />
                                                            </a>
                                                            <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/gridview/Edit.jpg"
                                                                ToolTip="Edit" Height="16px" Width="16px" />
                                                            <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Images/gridview/delete.jpg"
                                                                ToolTip="Delete" Height="16px" Width="16px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
					                </div>
				                </div>
			                </div>
		                </div>
	                </div>
                </div>
                <div style="display: none;">
                    <asp:GridView ID="GvDocBAUTPrint" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                        HeaderStyle-CssClass="GridHeader" Width="99%">
                        <RowStyle CssClass="GridOddRows" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <EmptyDataRowStyle CssClass="emptyRowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SiteNo" HeaderText="Site No" ReadOnly="true" />
                            <asp:BoundField DataField="SiteName" HeaderText="Site name" ReadOnly="true" />
                            <asp:BoundField DataField="PoNo" HeaderText="PO No" ReadOnly="true" />
                            <asp:BoundField DataField="WorkPackageId" HeaderText="Package Id" ReadOnly="true" />
                            <asp:BoundField DataField="TselApprover" HeaderText="Tsel Approver" ReadOnly="true" />
                            <asp:BoundField DataField="ApprovedDate" HeaderText="Approved Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" ReadOnly="true" />
                            <asp:BoundField DataField="BAUTStatusText" HeaderText="BAUT Status" ReadOnly="true" />
                            <asp:BoundField DataField="ReferenceNo" HeaderText="Ref No" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnSearch" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
    <asp:ScriptManager ID="SM1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="Up1">
            <ProgressTemplate>
                <div id="blur">
                    <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                        <img src="../Images/preloader.gif" style="vertical-align: middle" alt="Processing" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="HeaderReport">
            <table width="100%">
                <tr>
                    <td style="width: 80%">
                        BAUT Done With Null Reference No
                    </td>
                    <td style="width: 15%; text-align: right;">
                        <asp:Button ID="BtnExportExcel" runat="server" Text="Export To Excel" CssClass="BtnExpt"
                            BackColor="#7f7f7f" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="Up1" runat="server">
            <ContentTemplate>
                <div style="width: 100%; padding-bottom: 10px; margin-bottom: 10px; border-bottom-style: solid;
                    border-bottom-color: Gray; border-bottom-width: 1px;">
                    <table>
                        <tr>
                            <td>
                                <span class="lblBoldText">PoNo</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlPoNo" runat="server" CssClass="lblText">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <span class="lblBoldText">Package Id</span>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtPackageid" runat="server" CssClass="lblText"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="BtnSearch" runat="server" Text="Search" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:GridView ID="GvDocBAUT" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                    HeaderStyle-CssClass="GridHeader" Width="99%" OnRowCancelingEdit="GvDocBAUT_RowCancelingEdit"
                    AllowPaging="true" PageSize="20" OnRowDeleting="GvDocBAUT_RowDeleting" OnRowEditing="GvDocBAUT_RowEditing"
                    OnRowUpdating="GvDocBAUT_RowUpdating">
                    <RowStyle CssClass="GridOddRows" />
                    <AlternatingRowStyle CssClass="GridEvenRows" />
                    <EmptyDataRowStyle CssClass="emptyRowStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SiteNo" HeaderText="Site No" ReadOnly="true" />
                        <asp:BoundField DataField="PoNo" HeaderText="PO No" ReadOnly="true" />
                        <asp:BoundField DataField="WorkPackageId" HeaderText="Package Id" ReadOnly="true" />
                        <asp:BoundField DataField="TselApprover" HeaderText="Tsel Approver" ReadOnly="true" />
                        <asp:BoundField DataField="ApprovedDate" HeaderText="Approved Date" HtmlEncode="false"
                            DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" ReadOnly="true" />
                        <asp:TemplateField HeaderText="BAUT Status">
                            <EditItemTemplate>
                                <asp:Label ID="LblBautStatusEdit" runat="server" Text='<%#Eval("BAUTStatus") %>'
                                    Visible="false"></asp:Label>
                                <asp:DropDownList ID="DdlBAUTStatus" runat="server" CssClass="lblText">
                                    <asp:ListItem Text="Not Yet" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Done" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBautStatus" runat="server" Text='<%#Eval("BAUTStatusText") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BAUT Ref No">
                            <EditItemTemplate>
                                <asp:Label ID="LblSiteId" runat="server" Text='<%#Eval("Site_Id") %>' Visible="false"></asp:Label>
                                <asp:Label ID="LblVersion" runat="server" Text='<%#Eval("SiteVersion") %>' Visible="false"></asp:Label>
                                <asp:Label ID="LblPoNo" runat="server" CssClass="lblText" Text='<%#Eval("PONO") %>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="LblPackageId" runat="server" CssClass="lblText" Text='<%#Eval("Workpackageid") %>'
                                    Visible="false"></asp:Label>
                                <asp:TextBox ID="TxtRefNo" runat="server" CssClass="lblText" Text='<%#Eval("ReferenceNo") %>'
                                    Width="98%"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LblRefNo" runat="server" CssClass="lblText" Text='<%#Eval("ReferenceNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/gridview/update.jpg"
                                    ToolTip="Update" Height="16px" Width="16px" />
                                <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/gridview/Cancel.jpg"
                                    ToolTip="Cancel" Height="16px" Width="16px" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <a href='../PO/frmViewDocument.aspx?id=<%#Eval("SW_Id") %>' target="_blank" style="text-decoration: none;
                                    border-style: none;">
                                    <img src="../Images/Pdf_Icon.png" alt="pdficon" id="pdfIcon" height="18" width="18"
                                        style="text-decoration: none; border-style: none;" />
                                </a>
                                <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/gridview/Edit.jpg"
                                    ToolTip="Edit" Height="16px" Width="16px" />
                                <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Images/gridview/delete.jpg"
                                    ToolTip="Delete" Height="16px" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div style="display: none;">
                    <asp:GridView ID="GvDocBAUTPrint" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                        HeaderStyle-CssClass="GridHeader" Width="99%">
                        <RowStyle CssClass="GridOddRows" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <EmptyDataRowStyle CssClass="emptyRowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SiteNo" HeaderText="Site No" ReadOnly="true" />
                            <asp:BoundField DataField="SiteName" HeaderText="Site name" ReadOnly="true" />
                            <asp:BoundField DataField="PoNo" HeaderText="PO No" ReadOnly="true" />
                            <asp:BoundField DataField="WorkPackageId" HeaderText="Package Id" ReadOnly="true" />
                            <asp:BoundField DataField="TselApprover" HeaderText="Tsel Approver" ReadOnly="true" />
                            <asp:BoundField DataField="ApprovedDate" HeaderText="Approved Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" ReadOnly="true" />
                            <asp:BoundField DataField="BAUTStatusText" HeaderText="BAUT Status" ReadOnly="true" />
                            <asp:BoundField DataField="ReferenceNo" HeaderText="Ref No" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnSearch" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>--%>
</body>
</html>
