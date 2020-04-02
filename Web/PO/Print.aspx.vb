Imports System
Partial Class PO_Print
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ctrl As Control = CType(Session("ctrl"), Control)
        PrintHelper.PrintWebControl(ctrl)
    End Sub

End Class
