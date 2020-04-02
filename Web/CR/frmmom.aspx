<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmmom.aspx.vb" Inherits="CR_frmmom"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Change Request</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>
    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>
    <script language="javascript" type="text/javascript">
        function checkIsEmpty()
        {
            var msg="";
             if (IsEmptyCheck(document.getElementById("txtMomDate").value)== false)
            {
                msg = msg + "MOM Date should not be Empty\n"
            }       
            if (IsEmptyCheck(document.getElementById("txtMOMWriter").value) == false)
            {
                msg = msg + "MOM Writer should not be Empty\n"
            }
            if (IsEmptyCheck(document.getElementById("txtLocation").value) == false)
            {
                msg = msg + "Location should not be Empty\n"
            }
            if (IsEmptyCheck(document.getElementById("txtSubject").value) == false)
            {
                msg = msg + "Subject should not be Empty\n"
            }
            if (IsEmptyCheck(document.getElementById("hdnUsersId").value) == false)
            {
                msg = msg + "Please Select Users\n"
            } 
                           
            if (msg != "")
            {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
            else
            {
                return true; //accessConfirm();
            }          
        }
        function checkOtherEmpty()
        {
            var msg=""
            if(IsEmptyCheck(document.getElementById("txtRequest").value) ==  false)
            {
                msg = msg+ "Please enter Other Request\n"
            }
            if (msg != "")
            {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
        }
        function CheckMOMDetails()
        {
            var msg=""
            if (IsEmptyCheck(document.getElementById("txtMomDate").value)==false) 
            {
                msg = msg+ "MOM Date should not be Empty.\n"
            }
            if(dateVal()==false)
            {
                msg = msg+ "MOM Date should not be Current date or Before current date.\n"
            }
            if (IsEmptyCheck(document.getElementById("txtLocation").value)==false) 
            {
                msg = msg+ "Location should not be Empty.\n"
            }
            if (IsEmptyCheck(document.getElementById("txtModerator").value)==false) 
            {
                msg = msg+ "Moderator should not be Empty.\n"
            }
            if (IsEmptyCheck(document.getElementById("txtMOMWriter").value)==false) 
            {
                msg = msg+ "MOM Writer should not be Empty.\n"
            }
             if (IsEmptyCheck(document.getElementById("txtArea").value)==false) 
            {
                msg = msg+ "Subject should not be Empty.\n"
            }
            var data = document.getElementById('grdUsers'); 
            if(data==null)
            {
                msg = msg + "MOM Attendees should not empty\n"
            }
            else
            {
                if(data.rows.length == 1)
                {
                    msg = msg + "MOM Attendees should not empty\n"
                }
            }
            var data1 = document.getElementById('GrdPo'); 
            if(data1==null)
            {
                msg = msg + "Change order should not empty.\n"
            }
            else
            {
                if(data1.rows.length ==1)
                {
                    msg = msg + "Change order should not empty.\n"
                }
            }
            
            if (msg != "")
            {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
            else
            {
                return true; //accessConfirm();
            }

        }
        function dateVal()
        {            
            var date = new Date()
            var mdate = document.getElementById("txtMomDate").value;
            var sdate = mdate.split("/");
            var boolcheck=0
            var currentYear=date.getFullYear();
            var currentmonth=date.getMonth();
            currentmonth=currentmonth+1;
            var currentdate=date.getDate();
            if(sdate[2]<=currentYear)
            {
                if(sdate[1]<=currentmonth)
                {
                    if(sdate[0]<=currentdate)
                    {
                        boolcheck=1
                    }
                    else
                    {
                        boolcheck=0
                    }
                }
                else
                {
                    boolcheck=0
                }
            }
            else
            {
                boolcheck=0
            }
            if(boolcheck == 1)
            {
               
                return true; 
            } 
            else
            {
                return false;
            }         

        }
        function CheckADDSites()
        {
            var msg=""
            var radio = document.getElementsByName("rblType");
            var aa=0; 
            var abc = 0;                   
            for (var ii = 0; ii < radio.length; ii++)
            {
                if (radio[ii].checked)    
                {   
                    abc = radio[ii].value;     
                    aa=1;                      
                 }                            
            }
            
            if (aa == 0)
            {                    
                msg = msg + "Change order type should be select\n"
            }
           
           
            var a = document.getElementById("ddlPONo"); 
            var strUser = a.options[a.selectedIndex].value;
            if (strUser == 0)
            {
                msg = msg + "PONo should be select\n"
            }
            var a = document.getElementById("ddlSite"); 
            var strUser = a.options[a.selectedIndex].value;
            if (strUser == 0)
            {
                msg = msg + "Site No should be select\n"
            }
            else
            {
           
                if(abc==2)
                {
                    if(a.options[a.selectedIndex].text==document.getElementById("txtSiteNo").value+'-'+document.getElementById("txtFldType").value)
                    {
                        msg = msg + "Please enter the site id different.\n"
                    }
                    
                }
                else if(abc==1)
                {
                    if(a.options[a.selectedIndex].text!=document.getElementById("txtSiteNo").value+'-'+document.getElementById("txtFldType").value)
                    {
                        msg = msg + "Site id should not be change.\n"
                    }
                }
            }
             if (IsEmptyCheck(document.getElementById("txtSubject").value)==false) 
            {
                msg = msg+ "Subject should not be Empty.\n"
            }
             if (IsEmptyCheck(document.getElementById("txtRemarks").value) == false)
            {
                msg = msg+ "Remarks Should not be Empty\n"
            }
            if (isNaN(document.getElementById("txtPurchase900").value)) 
            {
                msg = msg+ "Please enter Purchase 900 Number.\n"
            }
            if (isNaN(document.getElementById("txtPurchase1800").value)) 
            {
                msg = msg+ "Please enter Purchase 1800 Number.\n"
            }
            if (isNaN(document.getElementById("txtQty").value)) 
            {
                msg = msg+ "Please enter Quantity Number.\n"
            }
            if (isNaN(document.getElementById("txtAntennaQty").value)) 
            {
                msg = msg+ "Please enter  Antenna Quantity Number.\n"
            }
           
            if (msg != "")
            {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
            else
            {
                return true; //accessConfirm();
            }
        } 
        function OnChangedSites()
       {
            var msg=""
            var radio = document.getElementsByName("rblType");
            var aa=0; 
            var abc = 0;   
             for (var ii = 0; ii < radio.length; ii++)
            {
                if (radio[ii].checked)    
                {   
                    abc = radio[ii].value;     
                    aa=1;                      
                 }                            
            }
            
            if (aa == 0)
            {                    
                msg = msg + "Change order type should be select\n"
            }
           
           
            var a = document.getElementById("ddlPONo"); 
            var strUser = a.options[a.selectedIndex].value;
            if (strUser == 0)
            {
                msg = msg + "PONo should be select\n"
            }
           
            if (msg != "")
            {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
            else
            {
                return true; //accessConfirm();
            }
       }      
       function OnChangedBandType()
       {
            var a = document.getElementById("ddlbandtype"); 
            var strUser = a.options[a.selectedIndex].text;
            if(strUser=='Others')
            {     
                document.getElementById("txtBandType1").style.display =""; 
            }
            else
            {
                document.getElementById("txtBandType1").style.display ="none"; 

            }
            return false;
       }      
       function OnChangedBand()
       {
            var a = document.getElementById("ddlband"); 
            var strUser = a.options[a.selectedIndex].text;
            if(strUser=='Others')
            {     
                document.getElementById("txtBand").style.display =""; 
            }
            else
            {
                document.getElementById("txtBand").style.display ="none"; 

            }
            return false;
       }       
       function OnChangedConfig()
       {
            var a = document.getElementById("ddlconfig"); 
            var strUser = a.options[a.selectedIndex].text;
            if(strUser=='Others')
            {     
                document.getElementById("txtconfig").style.display =""; 
            }
            else
            {
                document.getElementById("txtconfig").style.display ="none"; 

            }
            return false;
       }        
        function bandValid()
        {
            var msg =""
            var strBand = document.getElementById('txtBand').value;  
            strBand = strBand.toUpperCase();                        
//            alert(strBand);
            if ((strBand=='DCS1800') || (strBand=='GSM900') || (strBand == 'DUAL BAND') ||(strBand == 'DUALBAND') )                             
            {
                var str900 = document.getElementById('txtPurchase900').value;
                var str1800 = document.getElementById('txtPurchase1800').value;                
                if ((strBand == 'DCS1800') && (str900 > 0))
                {                    
                    document.getElementById('txtPurchase1800').value = str900;
                    document.getElementById('txtPurchase900').value = 0;
                }
                else if ((strBand =='GSM900') && (str1800 > 0))
                {
                    document.getElementById('txtPurchase900').value = str1800;
                    document.getElementById('txtPurchase1800').value = 0;
                }           
                else if((strBand =='DUAL BAND') || (strBand =='DUALBAND')) 
                {
                    var config = document.getElementById('txtConfig').value;
                    //alert(config);
                    config = config.split("+");
                    if (config.length > 0)
                    {
                        document.getElementById('txtPurchase900').value = ConfigTot(config[0]);
                    }
                    if (config.length > 1)
                    {
                        document.getElementById('txtPurchase1800').value = ConfigTot(config[1]);
                    }
                    document.getElementById('txtQty').value = 2
                }
            }
           
        }
        function ConfigTot(config)
        {
            var tot = config;
            var res = 0;
            if (tot.length > 0)
            {
                for (i=0;i < tot.length; i++)
                {
                    res = res + int(tot[i]);
                }
            } 
            return res;
        }
        function viewCode(objType)
        {
            if (objType == 'S')
            {
                window.open('../CR/frmUsrhelp.aspx?SelMode=true',0,"width=400,height=400,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes");
            }
            else if(objType =='N')
            {
                window.open('../CR/frmAUser.aspx?SelMode=true',0,"width=400,height=200,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes");
            }
        }
         function MOMModerator()
        {
            var mom;
            mom = window.showModalDialog('../CR/frmWriterList.aspx?SelMode=true','','width=400,height=400,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');          
            if(typeof mom != 'undefined')
            {    
                document.getElementById('hdnSSId1').value = mom
                var bb = mom.split('####')        
                document.getElementById('hdnSupId1').value = bb[0];
                document.getElementById('txtModerator').value = bb[1];
                 document.getElementById('hdnSSName1').value = bb[1];
            }
        }
     
        function MOMWriter()
        {
            var mom;
            mom = window.showModalDialog('../CR/frmWriterList.aspx?SelMode=true','','width=400,height=400,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');          
            if(typeof mom != 'undefined')
            {    
                document.getElementById('hdnSSId').value = mom
                var bb = mom.split('####')        
                document.getElementById('hdnSupId').value = bb[0];
                document.getElementById('txtMOMWriter').value = bb[1];
                 document.getElementById('hdnSSName').value = bb[1];
            }
        }
     
   
    </script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="6000">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="1" cellspacing="2" style="width: 100%">
                    <tr>
                        <td class="pageTitle">
                            Change Request (MOM)</td>
                    </tr>
                    <tr>
                        <td class="hgap">
                            <input type="hidden" id="hdnMOMID" runat="server" />&nbsp;
                            <input type="hidden" id="hdnSort" runat="server" />
                            <input type="hidden" runat="server" id="hdnSSId" />
                            <input type="hidden" runat="server" id="hdnSupId" value="0" />
                            <input type="hidden" id="hdnSSName" runat="Server" />
                            <input type="hidden" runat="server" id="hdnSSId1" />
                            <input type="hidden" runat="server" id="hdnSupId1" value="0" />
                            <input type="hidden" id="hdnSSName1" runat="Server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="1" cellspacing="2" style="width: 100%">
                                <tr>
                                    <td class="lblTitle" style="width: 40%">
                                        MOM Date
                                    </td>
                                    <td style="width: 1%">
                                        :
                                    </td>
                                    <td style="width: 59%">
                                        <input id="txtMomDate" runat="server" class="textFieldStyle" maxlength="50" readonly="readonly"
                                            size="30" type="text" />
                                        <asp:ImageButton ID="btnSiteCreateFDate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                            Width="18px" /></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle" style="width: 40%">
                                        Time</td>
                                    <td style="width: 1%">
                                        :</td>
                                    <td style="width: 59%">
                                        <input id="txtTime" runat="server" class="textFieldStyle" maxlength="50" size="30"
                                            type="text" onclick="dateVal();" /></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle" style="width: 40%">
                                        MOM Location
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <input id="txtLocation" runat="server" class="textFieldStyle" maxlength="50" size="30"
                                            type="text" /></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle" style="width: 40%">
                                        Moderator</td>
                                    <td>
                                        :</td>
                                    <td>
                                        <input id="txtModerator" runat="server" class="textFieldStyle" maxlength="50" size="30"
                                            type="text" />
                                        <a id="Moderator" runat="server" class="ASmall" href="#" onclick="MOMModerator();"><span
                                            style="color: #0000ff;">Select Moderator</span></a></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle" style="width: 40%">
                                        Writer
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <input id="txtMOMWriter" runat="server" class="textFieldStyle" maxlength="50" size="30"
                                            type="text" />
                                        <a id="A3" runat="server" class="ASmall" href="#" onclick="MOMWriter();"><span style="color: #0000ff">
                                            Select Writer</span></a></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle" style="width: 40%">
                                        Subject</td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <textarea id="txtArea" runat="server" cols="50" rows="6" class="textFieldStyle"></textarea></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle" style="width: 40%">
                                        Additional Attendees
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <a id="A2" runat="server" href="#" onclick="viewCode('S')" class="ASmall">Add Attendees</a>
                                        <input type="hidden" runat="server" id="hdnUsersId" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="grdUsers" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                BorderColor="Black" EmptyDataText="No Records Found" Width="50%" OnPageIndexChanging="grdUsers_PageIndexChanging1"
                                OnRowDataBound="grdUsers_RowDataBound" PageSize="5" OnRowDeleting="grdUsers_RowDeleting">
                                <PagerSettings Position="TopAndBottom" />
                                <PagerStyle CssClass="PagerTitle " />
                                <HeaderStyle CssClass="GridHeader" />
                                <AlternatingRowStyle CssClass="GridEvenRows" />
                                <Columns>
                                    <asp:TemplateField HeaderText=" Total ">
                                        <ItemStyle HorizontalAlign="Right" Width="1%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblno" runat="Server"></asp:Label>
                                            <asp:HiddenField ID="hdSno" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"sno") %>' />
                                            <asp:HiddenField ID="hduserId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Usr_Id") %>' />
                                            <asp:HiddenField ID="hdUserName" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"UsrName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="UsrName" HeaderText="User Name" SortExpression="UsrName" />
                                    <asp:BoundField DataField="UsrType" HeaderText="User Type" SortExpression="UsrType" />
                                    <asp:BoundField DataField="UsrEmail" HeaderText="Email" SortExpression="UsrEmail" />
                                    <asp:ButtonField CommandName="Delete" Text="Remove" />
                                </Columns>
                                <RowStyle CssClass="GridOddRows" HorizontalAlign="Left" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="hgap">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="1" cellspacing="2" style="width: 100%">
                                <tr>
                                    <td class="lblTitle" style="width: 40%">
                                        Type</td>
                                    <td style="width: 1%">
                                        :</td>
                                    <td style="width: 59%">
                                        <asp:RadioButtonList ID="rblType" runat="server" CssClass="textFieldStyle" RepeatDirection="Horizontal"
                                            RepeatLayout="flow">
                                        </asp:RadioButtonList></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle" style="width: 40%">
                                        Pono
                                    </td>
                                    <td style="width: 1%">
                                        :
                                    </td>
                                    <td style="width: 59%">
                                        <asp:DropDownList ID="ddlPONo" runat="server" AutoPostBack="True" CssClass="textFieldStyle"
                                            OnSelectedIndexChanged="ddlPONo_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle" style="width: 40%">
                                        Site No</td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="True" CssClass="textFieldStyle"
                                            OnSelectedIndexChanged="ddlSite_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblTitle" style="width: 40%">
                                        Subject</td>
                                    <td>
                                        :</td>
                                    <td>
                                        <input id="txtSubject" runat="server" class="textFieldStyle" maxlength="300" size="40"
                                            type="text" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td runat="server" id="TdConfig">
                            <table id="tblSiteDetails" runat="server" width="100%">
                                <tr>
                                    <td class="SubPageTitle">
                                        Site Details</td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 200px" valign="top">
                                        <asp:Panel ID="Panel1" runat="server" GroupingText="Description">
                                            <table id="Table3" runat="server">
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        Po Name</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        Po Type</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        Vendor</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        Site Id</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        Site Name</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        WorkPackage ID</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        WorkPackage Name</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        Scope</td>
                                                </tr>
                                                <tr style="border-top-style: solid; border-right-style: solid; border-left-style: solid;
                                                    height: 20px; border-bottom-style: solid">
                                                    <td class="lblTitleMOM">
                                                        Description</td>
                                                </tr>
                                                <tr style="border-top-style: solid; border-right-style: solid; border-left-style: solid;
                                                    height: 20px; border-bottom-style: solid">
                                                    <td class="lblTitleMOM">
                                                        Band Type</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        Band</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        Configuration</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        Purchase 900</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        Purchase1800</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        Hard Ware</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        HardWare Code</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        Quantity</td>
                                                </tr>
                                                <tr>
                                                    <td class="lblTitleMOM">
                                                        Antenna Name</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        Antenna Quantity</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        Feeder Length</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        Feeder Type</td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td class="lblTitleMOM">
                                                        Feeder Quantity</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" Width="100%">
                                            <ItemTemplate>
                                                <asp:Panel ID="Panel2" runat="server" GroupingText='<%#Container.DataItem("sta")%>'
                                                    Width="100%">
                                                    <table id="Table2" runat="server">
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("PoName")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("PoType")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("Vendor")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("SiteNo")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("SiteName")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("WorkPkgId")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("workPKGName")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("Scope")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("Description")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("Band_Type")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("Band")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("Config")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("Purchase900")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("Purchase1800")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("BSSHW")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("BSSCode")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("Qty")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("AntennaName")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("AntennaQty")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("FeederLen")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("FeederType")%>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 20px">
                                                            <td>
                                                                :
                                                                <%#Container.DataItem("FeederQty")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:Panel ID="pnlchangereq" runat="server" GroupingText="Change Request" HorizontalAlign="left"
                                            Width="100%">
                                            <table id="Table1" runat="server">
                                                <tr style="height: 20px">
                                                    <td>
                                                        <input id="txtPoName" runat="server" class="textFieldStyle" maxlength="50" type="text" /></td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <input id="txtPoType" runat="server" class="textFieldStyle" maxlength="50" type="text" /></td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <input id="txtVendor" runat="server" class="textFieldStyle" maxlength="50" type="text" /></td>
                                                </tr>
                                                <tr id="trSiteNo" runat="server" style="height: 20px">
                                                    <td>
                                                        <input id="txtSiteNo" runat="server" class="textFieldStyle" maxlength="50" size="50"
                                                            type="text" /></td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <input id="txtSiteName" runat="server" class="textFieldStyle" maxlength="50" size="50"
                                                            type="text" /></td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <input id="txtWorkPkgID" runat="server" class="textFieldStyle" maxlength="50" type="text" /></td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <input id="txtWorkPkgName" runat="server" class="textFieldStyle" maxlength="50" type="text" /></td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <input id="txtFldType" runat="server" class="textFieldStyle" maxlength="50" readonly="readonly"
                                                            type="text" /></td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <input id="txtDesc" runat="server" class="textFieldStyle" maxlength="50" size="50"
                                                            type="text" /></td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <asp:DropDownList ID="ddlbandtype" runat="server" AutoPostBack="true" CssClass="selectFieldStyle">
                                                        </asp:DropDownList>
                                                        <input id="txtBandType1" runat="server" class="textFieldStyle" maxlength="50" size="20"
                                                            type="text" style="display: none" />
                                                    </td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <asp:DropDownList ID="ddlband" runat="server" AutoPostBack="true" CssClass="selectFieldStyle">
                                                        </asp:DropDownList>
                                                        <input id="txtBand" runat="server" class="textFieldStyle" maxlength="50" size="20"
                                                            type="text" style="display: none" />
                                                    </td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <asp:DropDownList ID="ddlconfig" runat="server" AutoPostBack="true" CssClass="selectFieldStyle">
                                                        </asp:DropDownList>
                                                        <input id="txtconfig" runat="server" class="textFieldStyle" maxlength="50" size="20"
                                                            type="text" style="display: none" />
                                                    </td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <input id="txtPurchase900" runat="server" class="textFieldStyle" maxlength="4" readonly="readonly"
                                                            type="text" /></td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <input id="txtPurchase1800" runat="server" class="textFieldStyle" maxlength="4" readonly="readonly"
                                                            type="text" /></td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <input id="txtBSSHW" runat="server" class="textFieldStyle" maxlength="50" size="50"
                                                            type="text" /></td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <input id="txtBSSCode" runat="server" class="textFieldStyle" maxlength="50" type="text" /></td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <input id="txtQty" runat="server" class="textFieldStyle" maxlength="50" type="text" /></td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <asp:DropDownList ID="ddlAntennaName" runat="server" AutoPostBack="true" CssClass="selectFieldStyle">
                                                        </asp:DropDownList>
                                                        <input id="txtAntName" runat="server" class="textFieldStyle" maxlength="50" size="20"
                                                            type="text" visible="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input id="txtAntennaQty" runat="server" class="textFieldStyle" maxlength="50" type="text" /></td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <asp:DropDownList ID="ddlFeederLength" runat="server" AutoPostBack="true" CssClass="selectFieldStyle">
                                                        </asp:DropDownList>
                                                        <input id="txtFeederLength" runat="server" class="textFieldStyle" maxlength="50"
                                                            size="20" type="text" visible="false" />
                                                    </td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <asp:DropDownList ID="ddlFeederType" runat="server" AutoPostBack="true" CssClass="selectFieldStyle">
                                                        </asp:DropDownList>
                                                        <input id="txtFeederType" runat="server" class="textFieldStyle" maxlength="50" size="20"
                                                            type="text" visible="false" />
                                                    </td>
                                                </tr>
                                                <tr style="height: 20px">
                                                    <td>
                                                        <input id="txtFeederQty" runat="server" class="textFieldStyle" maxlength="50" type="text" /></td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="3">
                                        <table cellpadding="1" cellspacing="2" border="0" width="100%">
                                            <tr>
                                                <td class="lblTitle" style="width: 40%">
                                                    Remarks</td>
                                                <td style="width: 1%">
                                                    :</td>
                                                <td style="width: 59%">
                                                    <textarea id="txtRemarks" cols="50" rows="6" runat="server" class="textFieldStyle"></textarea></td>
                                            </tr>
                                            <tr>
                                                <td class="lblTitle" style="width: 40%">
                                                    Cost Impact</td>
                                                <td style="width: 1%">
                                                    :</td>
                                                <td style="width: 59%">
                                                    <input id="chkCost" type="checkbox" runat="server" class="textFieldStyle" /></td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="3">
                                                    <asp:GridView ID="grdOthers" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        BorderColor="Black" EmptyDataText="No Records Found" Width="50%" OnPageIndexChanging="grdOthers_PageIndexChanging"
                                                        OnRowDataBound="grdOthers_RowDataBound" PageSize="5" OnRowDeleting="grdOthers_RowDeleting"
                                                        OnSelectedIndexChanging="grdOthers_SelectedIndexChanging">
                                                        <PagerSettings Position="TopAndBottom" />
                                                        <RowStyle CssClass="GridOddRows" HorizontalAlign="Left" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText=" Total ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblno" runat="Server"></asp:Label>
                                                                    <asp:HiddenField ID="hdSno" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"sno") %>' />
                                                                    <asp:HiddenField ID="hdmomid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"mom_Id") %>' />
                                                                    <asp:HiddenField ID="hdPono" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"pono") %>' />
                                                                    <asp:HiddenField ID="hdSite" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"siteid") %>' />
                                                                    <asp:HiddenField ID="hdScope" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"scope") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Content">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtcontent" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "contents") %>'
                                                                        Height="65px" TextMode="MultiLine" Width="245px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:ButtonField CommandName="select" Text="add" />
                                                            <asp:ButtonField CommandName="Delete" Text="Remove" />
                                                        </Columns>
                                                        <PagerStyle CssClass="PagerTitle " />
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <AlternatingRowStyle CssClass="GridEvenRows" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3">
                                        <asp:Button ID="btnAddSites" runat="server" OnClick="btnAddSites_Click" Text="Add Sites" />
                                        <asp:Button ID="btn1Cancel" runat="server" OnClick="btn1Cancel_Click" Text="Cancel Sites" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="hgap">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GrdPo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                BorderColor="Black" EmptyDataText="No Records Found" PageSize="5" Width="100%"
                                OnPageIndexChanging="GrdPo_PageIndexChanging" OnRowDataBound="GrdPo_RowDataBound"
                                OnRowDeleting="GrdPo_RowDeleting" OnSelectedIndexChanging="GrdPo_SelectedIndexChanging">
                                <PagerSettings Position="TopAndBottom" />
                                <RowStyle CssClass="GridOddRows" />
                                <Columns>
                                    <asp:TemplateField HeaderText=" Total ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblno" runat="Server"></asp:Label>
                                            <asp:HiddenField ID="hdSno" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"sno") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdMomd_Id" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Momd_Id") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdvendor" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"vendor") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdPoName" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"PoName") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdPotype" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"poType") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdfeederqty" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"feederqty") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdfeedertype_Other" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"feedertype_Other") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdfeedertype" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"feedertype") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdfeederlen_Other" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"feederlen_Other") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdfeederlen" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"feederlen") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdantennaqty" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"antennaqty") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdantennaname_Other" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"antennaname_Other") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdantennaname" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"antennaname") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdqty" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"qty") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdbsscode" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"bsscode") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdbsshw" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"bsshw") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdpurchase1800" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"purchase1800") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdpurchase900" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"purchase900") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdconfig_Other" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"config_Other") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdconfig" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"config") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdband_Other" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"band_Other") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdband_type_Other" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"band_type_Other") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdband" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"band") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdband_type" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"band_type") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hddescription" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"description") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdfldtype" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"fldtype") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdworkpackagename" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"workpackagename") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdsitename" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"sitename") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdScope" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"scope") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdRemarks" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Remarks") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdIsCostImp" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"IsCostImp") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdOldSiteNo" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"OldSiteNo") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdLKPDesc" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"LKPDesc") %>'>
                                            </asp:HiddenField>
                                            <asp:HiddenField ID="hdSubject" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"subject") %>'>
                                            </asp:HiddenField>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="1%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SiteNo" HeaderText="Site No" SortExpression="SiteNo" />
                                    <asp:BoundField DataField="PONo" HeaderText="PONo" />
                                    <asp:BoundField DataField="WorkPkgId" HeaderText="WorkPackage Id" />
                                    <asp:BoundField DataField="LKPDescNEW" HeaderText="Type" />
                                    <asp:ButtonField CommandName="Select" Text="Edit" />
                                    <asp:ButtonField CommandName="Delete" Text="Remove" />
                                </Columns>
                                <PagerStyle CssClass="PagerTitle " />
                                <HeaderStyle CssClass="GridHeader" />
                                <AlternatingRowStyle CssClass="GridEvenRows" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="hgap">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" id="tdGenerate" runat="server">
                            <asp:Button ID="BtnSubmit" runat="server" OnClick="BtnSubmit_Click" Text="Save MOM" />
                            <asp:Button ID="BtnCancel" runat="server" OnClick="BtnCancel_Click" Text="Cancel" /></td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
