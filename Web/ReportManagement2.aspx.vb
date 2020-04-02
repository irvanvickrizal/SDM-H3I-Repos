
Partial Class ReportManagement2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack = True Then
            LDsummary1.binddata("01")
            LDsummary2.binddata("02")
            LDsummary3.binddata("03")
            LDsummary4.binddata("04")
            LDsummary5.binddata("05")
            LDsummary6.binddata("06")
            LDsummary7.binddata("07")
            LDsummary8.binddata("08")
            LDsummary9.binddata("09")
            LDsummary10.binddata("10")
        End If
    End Sub
End Class
