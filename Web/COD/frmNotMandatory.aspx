<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmNotMandatory.aspx.vb"
    Inherits="COD_frmNotMandatory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Injection Form</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:UpdatePanel ID="UpInjectionPanel" runat="server">
                <ContentTemplate>
                    <div>
                        <asp:GridView ID="GvIgnoreDocuments" runat="server">
                            <Columns>
                                
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
