<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCOTransactionDetail.aspx.vb"
    Inherits="CO_frmCOTransactionDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CO Transaction Detail</title>
    <style type="text/css">
        #HeaderPanel
        {
            width: 99%;
            background-repeat: repeat-x;
            background-color: #c5c3c3;
            font-family: verdana;
            font-weight: bolder;
            font-size: 10pt;
            color: white;
            padding-top: 8px;
            padding-bottom: 8px;
            -moz-border-radius: 3px;
            border-radius: 3px;
            text-shadow: 0px 1px 1px #000;
        }
        .gridHeader
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: #ffc727;
            font-weight: bolder;
            text-align: center;
            padding: 5px;
            color: white;
            border-style: solid;
            border-width: 1px;
            border-color: GrayText;
        }
        .gridOdd
        {
            font-family: Verdana;
            font-size: 11px;
            padding: 5px;
        }
        .gridEven
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: #cfcfcf;
            padding: 5px;
        }
        .fancybox-custom .fancybox-skin
        {
            box-shadow: 0 0 50px #222;
        }
        .fancybox-title-inside
        {
            text-align: center;
            font-family: verdana;
            font-size: 18px;
        }
        .lblText
        {
            font-family: Verdana;
            font-size: 11px;
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
        .btnstyle
        {
        	border-style:solid;
        	border-color:Gray;
        	border-width:2px;
        	background-color:#c3c3c3;
        	cursor:pointer;
        	color:White;
        	padding:5px;
        	font-family:Verdana;
        	font-size:11px;
        }
        
    </style>

    <script type="text/javascript" src="../js/jquery-1.8.2.min.js"></script>

    <script type="text/javascript" src="../js/jquery.mousewheel-3.0.6.pack.js"></script>

    <!-- Add fancyBox main JS and CSS files -->

    <script type="text/javascript" src="../fancybox/jquery.fancybox.js?v=2.1.3"></script>

    <link rel="stylesheet" type="text/css" href="../fancybox/jquery.fancybox.css?v=2.1.2"
        media="screen" />

    <script type="text/javascript">
    $(function() {
    			$('.fancybox').fancybox({
    			    width:500,
    			    height:800,   
    			    scrolling : 'No',     
    			    helpers: {
					    title : {
						    type : 'inside'
					    },
					    overlay : {
					        transitionOut :'elastic',
					        speedIn : 600,
						    speedOut : 300
					    }
				    }
    			});
    			
    			$('.fancyboxViewLog').fancybox({
    			    width:900,
    			    height:450,
    			    fitToView : false,
                    autoSize : false,
    			    helpers: {
					    title : {
						    type : 'inside'
					    },
					    overlay : {
					        transitionOut :'elastic',
					        speedIn : 600,
						    speedOut : 300
					    }
				    }
    			});
			});
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
        function OverlapExecutionDate()
        {
            alert("Date Time Execution have to same or greater than start date time");
            return false;
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
    <div id="HeaderPanel">
        CO Transaction Detail
    </div>
    <div style="margin-top: 10px;">
        <table>
            <tr class="lblText">
                <td style="width: 20%">
                    Po No
                </td>
                <td style="width: 1%">
                    :
                </td>
                <td colspan="2" id="tdpono" runat="server">
                </td>
            </tr>
            <tr class="lblText">
                <td>
                    PO Name
                </td>
                <td>
                    :
                </td>
                <td colspan="2" id="tdponame" runat="server">
                </td>
            </tr>
            <tr class="lblText">
                <td>
                    Site No
                </td>
                <td>
                    :
                </td>
                <td colspan="2" id="tdsiteno" runat="server">
                </td>
            </tr>
            <tr class="lblText">
                <td>
                    Site Name
                </td>
                <td>
                    :
                </td>
                <td colspan="2" id="tdsitename" runat="server">
                </td>
            </tr>
            <tr class="lblText">
                <td>
                    Scope
                </td>
                <td>
                    :
                </td>
                <td runat="server" id="tdscope" colspan="2">
                </td>
            </tr>
            <tr class="lblText">
                <td>
                    Work Package ID
                </td>
                <td>
                    :
                </td>
                <td colspan="2" id="tdwpid" runat="server">
                </td>
            </tr>
            <tr class="lblText">
                <td>
                    Project ID
                </td>
                <td>
                    :
                </td>
                <td colspan="2" id="tdprojectId" runat="server">
                </td>
            </tr>
        </table>
    </div>
    <div>
        <hr />
        <asp:UpdatePanel ID="Up1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GvCOTransaction" runat="server" AutoGenerateColumns="false" EmptyDataText="No record Found"
                    Width="99%" OnRowCancelingEdit="GvCOTransaction_RowCancelingEdit" OnRowEditing="GvCOTransaction_RowEditing"
                    OnRowUpdating="GvCOTransaction_RowUpdating">
                    <HeaderStyle CssClass="gridHeader" />
                    <RowStyle CssClass="gridOdd" />
                    <AlternatingRowStyle CssClass="gridEven" />
                    <Columns>
                        <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="gridHeader" ItemStyle-BorderColor="GrayText"
                            ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px">
                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>.
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Docname" HeaderText="Document" HeaderStyle-CssClass="gridHeader"
                            ReadOnly="true" ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="Solid"
                            ItemStyle-BorderWidth="1px" />
                        <asp:BoundField DataField="StartDateTime" ReadOnly="true" HeaderText="Start Date"
                            HeaderStyle-CssClass="gridHeader" ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="Solid"
                            ItemStyle-BorderWidth="1px" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                            ConvertEmptyStringToNull="true" />
                        <asp:TemplateField HeaderText="Submit Date" HeaderStyle-CssClass="gridHeader" ItemStyle-BorderColor="GrayText"
                            ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px">
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtEndDateTime" runat="server" Font-Size="11px" Font-Names="Verdana"></asp:TextBox>
                                <asp:ImageButton ID="btnEndDateTime" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                    Width="18px" />
                                <cc1:CalendarExtender ID="ceEndDateTime" runat="server" Format="dd-MMM-yyyy" PopupButtonID="btnEndDateTime"
                                    TargetControlID="TxtEndDateTime">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RfsEndDateTime" runat="server" ControlToValidate="TxtEndDateTime"
                                    ErrorMessage="Please choose Date Execution First" ValidationGroup="updatetrans"
                                    SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="LblErrorMessage" runat="server" Font-Size="11px" Font-Names="Verdana"
                                    ForeColor="Red"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LblStartDateTime" runat="server" Text='<%#Eval("StartDateTime", "{0:dd-MMM-yyyy}") %>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="LblEndDateTime" runat="server" Text='<%#Eval("EndDateTime","{0:dd-MMM-yyyy HH:mm:ss}") %>'
                                    HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Execution User" HeaderStyle-CssClass="gridHeader"
                            ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px">
                            <ItemTemplate>
                                <asp:Label ID="LblExecutionUser" runat="server" Text='<%#Eval("ExecutionUser") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="LblStartDateTimeTest" runat="server" Text='<%#Eval("StartDateTime", "{0:dd-MMM-yyyy}") %>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="LblTaskID" runat="server" Text='<%#Eval("Tsk_Id") %>' Visible="false"></asp:Label>
                                <asp:Label ID="LblSNO" runat="server" Text='<%#Eval("sno") %>' Visible="false"></asp:Label>
                                <asp:Label ID="LblWFID" runat="server" Text='<%#Eval("WFID") %>' Visible="false"></asp:Label>
                                <asp:Label ID="LblRoleID" runat="server" Text='<%#Eval("RoleId") %>' Visible="false"></asp:Label>
                                <asp:DropDownList ID="DdlUserRole" runat="server" CssClass="lblText">
                                </asp:DropDownList>
                                <asp:RangeValidator ID="RvDdlUserRole" runat="server" ControlToValidate="DdlUserRole"
                                    Display="None" ErrorMessage="Please choose user execution" ValidationGroup="updatetrans"
                                    SetFocusOnError="true" MinimumValue="1" MaximumValue="10000" Type="Integer"></asp:RangeValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridHeader" ItemStyle-BorderColor="GrayText"
                            ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px">
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/gridview/update.jpg"
                                    ToolTip="Update" Height="16px" Width="16px" ValidationGroup="updatetrans" />
                                <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/gridview/Cancel.jpg"
                                    ToolTip="Cancel" Height="16px" Width="16px" CausesValidation="false" />
                                <asp:ValidationSummary ID="VsUpdateTrans" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="updatetrans" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/gridview/Edit.jpg"
                                    ToolTip="Edit" Height="16px" Width="16px" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="width: 99%;">
        <hr />
        <table>
            <tr>
                <td>
                    <span class="lblText">Doc CO Online</span>
                </td>
                <td>
                    <a id="viewdoclink" runat="server" class="fancyboxViewLog fancybox.iframe" href="#"
                        style="border-style: none;">
                        <img src="../Images/ViewDoc.jpg" alt="viewdoc" height="24" width="24" style="border-style: none;" />
                    </a>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="lblText">Replacement CO Online</span>
                </td>
                <td>
                    <asp:Label ID="LblReplacementStatus" runat="server" ForeColor="Red" Font-Size="11px"
                        Font-Names="Verdana" Font-Italic="true" Text="NY Uploaded"></asp:Label>
                    <asp:FileUpload ID="FUCOUpload" runat="server" />
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btnstyle" OnClientClick="return confirm('Are you sure you want to upload this CO Document as replacement CO Online?');" />
                    <asp:Label ID="LblErrorMessage" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                    <a id="viewreplacementdoclink" runat="server" class="fancyboxViewLog fancybox.iframe"
                        href="#" style="border-style: none;">
                        <img src="../Images/ViewDoc.jpg" alt="viewdoc" height="24" width="24" style="border-style: none;" />
                    </a>
                    <asp:ImageButton ID="ImgDocDelete" runat="server" ImageUrl="~/images/action_delete.gif" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
