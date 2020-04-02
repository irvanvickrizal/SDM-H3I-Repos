Imports System
Imports System.IO
Imports System.Security.AccessControl
Imports System.Security.Permissions
Imports System.Data
Partial Class welcome
    Inherits System.Web.UI.Page

    Private dhcontroller As New DashboardController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Write(Session("User_Type"))

        'Added by Fauzan, 24 Nov 2018. For directly access without login. Called from DirectCredential.aspx.vb
        If Session("RoutingUrl") IsNot Nothing Then
            Dim routingUrl As String = Session("RoutingUrl")
            Session("RoutingUrl") = Nothing
            Response.Redirect(routingUrl)
        End If

        Select Case Session("User_Type")
            Case "N"
                DashboardConfiguration(CommonSite.RollId, "N")
            Case "S"
                DashboardConfiguration(CommonSite.RollId, "S")
            Case "C"
                Dim strTSKId As String = Convert.ToString(Session("tskid"))
                If Not String.IsNullOrEmpty(strTSKId) Then
                    If strTSKId.Equals("1") Then
                        Response.Redirect("B4Dashboard.aspx")
                    ElseIf strTSKId.Equals("2") Then
                        Response.Redirect("B4DashboardATP.aspx")
                    ElseIf strTSKId.Equals("3") Then
                        Response.Redirect("B4DashboardQC.aspx")
                    ElseIf strTSKId.Equals("10") Then
                        Response.Redirect("B4DashboardSOAC.aspx")
                    Else
                        Response.Redirect("B4DashboardC.aspx")
                    End If
                Else
                    Response.Redirect("B4DashboardC.aspx")
                End If
            Case "H"
                Response.Redirect("B4DashboardC.aspx")
        End Select
    End Sub
    Sub AddDirectorySecurity(ByVal FileName As String, ByVal Account As String, ByVal Rights As FileSystemRights, ByVal ControlType As AccessControlType)
        ' Create a new DirectoryInfoobject.
        Dim dInfo As New DirectoryInfo(FileName)
        ' Get a DirectorySecurity object that represents the 
        ' current security settings.
        Dim dSecurity As DirectorySecurity = dInfo.GetAccessControl()
        ' Add the FileSystemAccessRule to the security settings. 
        dSecurity.AddAccessRule(New FileSystemAccessRule(Account, Rights, ControlType))
        ' Set the new access settings.
        dInfo.SetAccessControl(dSecurity)
    End Sub
    ' Removes an ACL entry on the specified directory for the specified account.
    Sub RemoveDirectorySecurity(ByVal FileName As String, ByVal Account As String, ByVal Rights As FileSystemRights, ByVal ControlType As AccessControlType)
        ' Create a new DirectoryInfo object.
        Dim dInfo As New DirectoryInfo(FileName)
        ' Get a DirectorySecurity object that represents the 
        ' current security settings.
        Dim dSecurity As DirectorySecurity = dInfo.GetAccessControl()
        ' Add the FileSystemAccessRule to the security settings. 
        dSecurity.RemoveAccessRule(New FileSystemAccessRule(Account, Rights, ControlType))
        ' Set the new access settings.
        dInfo.SetAccessControl(dSecurity)
    End Sub

#Region "Custom Methods"
    Private Sub DashboardConfiguration(ByVal roleid As Integer, ByVal userType As String)
        'Dim isDashboardConfig As Integer = dbutils.ExeQueryScalar("select count(*) from DashboardUser where roleid=" & CommonSite.RollId)
        Dim dtDashboardConfigs As DataTable = dhcontroller.GetDashboardConfigByRole(roleid)
        If Not dtDashboardConfigs Is Nothing And dtDashboardConfigs.Rows.Count > 0 Then
            Response.Redirect(dtDashboardConfigs.Rows(0)("form_name").ToString())
        Else
            If userType.Equals("S") Then
                Response.Redirect("B4DashboardS.aspx")
            ElseIf userType.Equals("N") Then
                Response.Redirect("B4Dashboard.aspx")
            ElseIf userType.Equals("H") Then
                Response.Redirect("B4DashboardC.aspx")
            End If
        End If
    End Sub
#End Region
End Class
