<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODActivityWCCLocked.aspx.vb"
    Inherits="COD_frmCODActivityWCCLocked" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Activity WCC Locked</title>
   <style type="text/css">
        .lblText
        {
            font-family:verdana;
            font-size:12px;
        }
        .lblBoldText
        {
            font-family:verdana;
            font-size:12px;
            font-weight:bolder;
        }
         .emptyRowStyle
        {
            font-family:verdana;
            font-size:8pt;
            font-weight:bolder;
            color:maroon;
            border-style:solid;
            padding:3px;
            border-width:1px;
            border-color:gray;
        }
        .GridHeader
        {
           background-color:#ffc90e;
           font-family:verdana;
           font-weight:bold;
           font-size:9pt;
           text-align:center;
           height:30px;
           color:white;
        }
        .GridOddRows
        {
           font-family:verdana;
           font-size:7.5pt;
        }
        .GridEvenRows
        {
           background-color:#cfcfcf;
           font-family:verdana;
           font-size:7.5pt;
        }
        .btnstyle
        {
            font-family:verdana;
            font-size:11px;
            font-weight:bolder;
            background-color:#c3c3c3;
            color:#fff;
            padding:8px;
            cursor:pointer;
            border-width:1px;
            border-color:white;
            border-style:solid;
            
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <asp:GridView ID="GvActivities" runat="server" AutoGenerateColumns="false">
                    <Columns>
                    </Columns>
                </asp:GridView>
            </div>
            <div>
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="LblActivity" runat="server" Text="Activity" CssClass="lblBoldText"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlActivities" runat="server" CssClass="lblText">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblDocName" runat="server" Text="Document" CssClass="lblBoldText"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlDocuments" runat="server" CssClass="lblText">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Button ID="BtnAdd" runat="server" Text="Add" CssClass="btnstyle" />
                    <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btnstlye" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
