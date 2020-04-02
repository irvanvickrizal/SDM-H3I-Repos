Imports Common
Partial Class banner
    Inherits System.Web.UI.Page
    Dim objd As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        Response.Write("<script>var timeServer = new Date('" + DateTime.Now.ToString() + "');</script>")
        If Session("Res") Is Nothing Then Session("Res") = "1024"
        If Session("User_Type") = "N" Then
            Label1.Text = "Welcome   <b>" & Session("User_Name") & "</b> NSN  " & "  Date:" & Today.ToString("dd/MM/yyyy")
        ElseIf Session("User_Type") = "S" Then
            Dim str As String
            Label1.Text = "Welcome   <b>" & Session("User_Name") & "</b> Subcontractor <b/> Date:" & Today.ToString("dd/MM/yyyy")
        ElseIf Session("User_Type") = "C" Then
            Dim str1 As String = objd.ExeQueryScalarString("EXEC uspCustomer '" & Session("User_Id") & "'")
            Label1.Text = "Welcome   <b>" & Session("User_Name") & "</b> " & str1 & " <b/> " & "  Date:" & Today.ToString("dd/MM/yyyy")
        ElseIf Session("User_Type") = "H" Then
            Label1.Text = "Welcome   <b>" & Session("User_Name") & "</b> Huawei <b/> " & "  Date:" & Today.ToString("dd/MM/yyyy")
        End If
        'cmelink.Target = "_top"
        'sislink.Target = "_top"
        'sitaclink.Target = "_top"
        'HyperLink5.Target = "_top"
        'bugfix101207 included bypass parameter during maintenance
        'cmelink.NavigateUrl = ConfigurationManager.AppSettings("CMEURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=" & Session("PType")
        'sislink.NavigateUrl = ConfigurationManager.AppSettings("SISURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=" & Session("PType")
        'sitaclink.NavigateUrl = ConfigurationManager.AppSettings("SITacURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=" & Session("PType")
        'HyperLink5.NavigateUrl = ConfigurationManager.AppSettings("3GTIURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G"
    End Sub
End Class

