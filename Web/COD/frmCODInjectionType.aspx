<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODInjectionType.aspx.vb"
    Inherits="COD_frmCODInjectionType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>COD Injection</title>
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
    <style type="text/css">
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
            font-size: 8pt;
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
            font-size: 9pt;
            text-align: center;
            height: 30px;
            color: white;
        }
        .GridOddRows
        {
            font-family: verdana;
            font-size: 7.5pt;
        }
        .GridEvenRows
        {
            background-color: #cfcfcf;
            font-family: verdana;
            font-size: 7.5pt;
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
    <div>
        <asp:UpdatePanel ID="Up1" runat="server">
            <ContentTemplate>
                <div class="row">
	                <div class="col-xs-12">
		                <div class="box box-info">
			                <div class="box-header with-border">
				                <h3 class="box-title">Injection Type List</h3>
			                </div>
			                <div class="box-body">
				                <div class="table table-responsive">
					                <div class="col-md-6">
                                        <asp:GridView ID="GvInjectionType" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" HeaderStyle-CssClass="GridHeader" AllowPaging="true" PageSize="5"
                                            CssClass="table table-bordered table-condensed">
                                            <RowStyle CssClass="GridOddRows" />
                                            <AlternatingRowStyle CssClass="GridEvenRows" />
                                            <EmptyDataRowStyle CssClass="emptyRowStyle" />
                                            <PagerSettings Position="TopAndBottom" />
                                            <PagerStyle CssClass="customPagination" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblInjectionId" runat="server" Text='<%#Eval("InjectionId") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LblParentDocId" runat="server" Text='<%#Eval("ParentDocId") %>' Visible="false"></asp:Label>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Injection">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblInjectionName" runat="server" Text='<%#Eval("InjectionName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblInjectionDesc" runat="server" Text='<%#Eval("InjectionDesc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="LMDT" HeaderText="Last Modified Date" HtmlEncode="false"
                                                    DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                                                <asp:BoundField DataField="CDT" HeaderText="Created Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                                    ConvertEmptyStringToNull="true" />
                                                <asp:BoundField DataField="ModifiedUser" HeaderText="Modified User" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/images/gridview/Cancel.jpg"
                                                            AlternateText="deleteicon" CommandName="deleteinjection" CommandArgument='<%#Eval("injectionid") %>'
                                                            OnClientClick="return confirm('are you sure you want to delete this injector?');"
                                                            Width="20px" Height="20px" />
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
                <div class="row">
	                <div class="col-xs-12">
		                <div class="box box-info">
			                <div class="box-header with-border">
				                <h3 class="box-title">Add New Injection Type</h3>
			                </div>
			                <div class="box-body">
				                <div class="table table-responsive">
					                <div class="col-md-6">
						                <div class="form-horizontal">
							                <div class="form-group">
								                <label class="col-sm-3 control-label">Injection Name</label>
								                <div class="col-sm-6">
                                                    <asp:TextBox ID="TxtInjectionName" runat="server" CssClass="form-control" />
								                </div>
							                </div>
							                <div class="form-group">
								                <label class="col-sm-3 control-label">Injection Description</label>
								                <div class="col-sm-6">
                                                    <asp:TextBox ID="TxtInjectionDesc" runat="server" CssClass="form-control" TextMode="MultiLine" />
								                </div>
							                </div>
							                <div class="form-group">
								                <label class="col-sm-3 control-label">Parent Documents</label>
								                <div class="col-sm-6">
                                                    <asp:DropDownList ID="DdlParentDoc" runat="server" CssClass="form-control" ValidationGroup="injection">
                                                        <asp:ListItem Text="--Select Doc--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="BAUT" Value="1031"></asp:ListItem>
                                                        <asp:ListItem Text="BAST" Value="1032"></asp:ListItem>
                                                    </asp:DropDownList>
								                </div>
							                </div>
							                <div class="form-group">
								                <label class="col-sm-3 control-label"></label>
								                <div class="col-sm-6">
                                                    <asp:RequiredFieldValidator ID="rfvDocType" runat="server" ControlToValidate="DdlParentDoc"
                                                        InitialValue="0" ErrorMessage="Please choose Doc Type" ValidationGroup="injection"
                                                        Display="None"></asp:RequiredFieldValidator>
                                                    <asp:LinkButton ID="LbtSave" Text="Save" runat="server" OnClientClick="return confirm('are you sure you want to save this injector?');"
                                                        ValidationGroup="injection" CssClass="btn btn-block btn-primary"/>
								                </div>
							                </div>
						                </div>
					                </div>
				                </div>
			                </div>
		                </div>
	                </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="LbtSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
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
        <asp:UpdatePanel ID="Up1" runat="server">
            <ContentTemplate>
                <div>
                    <asp:GridView ID="GvInjectionType" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                        HeaderStyle-CssClass="GridHeader" Width="99%">
                        <RowStyle CssClass="GridOddRows" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <EmptyDataRowStyle CssClass="emptyRowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <asp:Label ID="LblInjectionId" runat="server" Text='<%#Eval("InjectionId") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="LblParentDocId" runat="server" Text='<%#Eval("ParentDocId") %>' Visible="false"></asp:Label>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Injection">
                                <ItemTemplate>
                                    <asp:Label ID="LblInjectionName" runat="server" Text='<%#Eval("InjectionName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="LblInjectionDesc" runat="server" Text='<%#Eval("InjectionDesc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="LMDT" HeaderText="Last Modified Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                            <asp:BoundField DataField="CDT" HeaderText="Created Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                ConvertEmptyStringToNull="true" />
                            <asp:BoundField DataField="ModifiedUser" HeaderText="Modified User" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/images/gridview/Cancel.jpg"
                                        AlternateText="deleteicon" CommandName="deleteinjection" CommandArgument='<%#Eval("injectionid") %>'
                                        OnClientClick="return confirm('are you sure you want to delete this injector?');"
                                        Width="20px" Height="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div style="margin-top: 15px; border-style: solid; border-color: Gray; border-width: 2px;
                    padding: 3px; width: 400px;">
                    <table>
                        <tr>
                            <td>
                                <span class="lblBoldText">Injection Name</span>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtInjectionName" runat="server" CssClass="lblText" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="lblBoldText">Injection Description</span>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtInjectionDesc" runat="server" CssClass="lblText" TextMode="MultiLine"
                                    Height="40px" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="lblBoldText">Parent Document</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlParentDoc" runat="server" CssClass="lblText" ValidationGroup="injection">
                                    <asp:ListItem Text="--Select Doc--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="BAUT" Value="1031"></asp:ListItem>
                                    <asp:ListItem Text="BAST" Value="1032"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <div style="margin-top: 5px; width: 100%; text-align: right;">
                        <asp:RequiredFieldValidator ID="rfvDocType" runat="server" ControlToValidate="DdlParentDoc"
                            InitialValue="0" ErrorMessage="Please choose Doc Type" ValidationGroup="injection"
                            Display="None"></asp:RequiredFieldValidator>
                        <asp:LinkButton ID="LbtSave" runat="server" OnClientClick="return confirm('are you sure you want to save this injector?');"
                            ValidationGroup="injection" Width="60.5px" Style="text-decoration: none; cursor: pointer;">
                                    <div class="btnSave"></div>
                        </asp:LinkButton>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="LbtSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>--%>
</body>
</html>
