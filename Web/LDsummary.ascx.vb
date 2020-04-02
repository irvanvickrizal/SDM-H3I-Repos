Imports Common
Imports System.Data
Partial Class LDsummary
    Inherits System.Web.UI.UserControl
    Dim objutil As New DBUtil
    Dim dt As New DataTable
    Public rgn As String
    Dim rgnc As String
    Dim rgna() As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack = True Then

        End If
    End Sub
    Sub binddata(ByVal rgn As String)
        rgnc = objutil.ExeQueryScalarString("select convert(varchar(20),rgn_id) +'@' + rgnname from codregion where rgncode='" & rgn & "'")
        rgna = rgnc.Split("@")
        lblregion.Text = rgna(1)
        dt = objutil.ExeQueryDT("exec uspApproachingLDSummary " & rgna(0) & "", "abc")
        grdDB.DataSource = dt
        grdDB.DataBind()
    End Sub
End Class
