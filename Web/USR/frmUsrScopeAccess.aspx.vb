Imports Common
Imports System.Data
Imports Entities
Imports BusinessLogic

Partial Class USR_frmUsrScopeAccess
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim dtdoc As New DataTable
    Dim objBO As New BOUserType
    Dim objutil As New DBUtil
    Dim strQuery As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        If Page.IsPostBack = False Then
            If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
                BindData(Convert.ToInt32(Request.QueryString("id")))
            End If
        End If
    End Sub
    Protected Sub LbtSaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSave.Click
        If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
            UpdateScope(ChkTIScope.Checked, ChkSISScope.Checked, ChkSitacScope.Checked, ChkCMEScope.Checked, Integer.Parse(Request.QueryString("id")))
        End If

    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal userid As Int32)
        MvMainPanel.SetActiveView(VwScope)
        strQuery = "select TIScope, SISScope, CMEScope, SITACSCope from ebastusers_1 where usr_id=" & userid
        dt = objutil.ExeQueryDT(strQuery, "scopetable")
        If dt.Rows.Count > 0 Then
            Dim tiscope As Boolean = Convert.ToBoolean(dt.Rows(0).Item(0))
            Dim sisscope As Boolean = Convert.ToBoolean(dt.Rows(0).Item(1))
            Dim cmescope As Boolean = Convert.ToBoolean(dt.Rows(0).Item(2))
            Dim sitacscope As Boolean = Convert.ToBoolean(dt.Rows(0).Item(3))

            If tiscope = True Then
                ChkTIScope.Checked = True
            End If

            If sisscope = True Then
                ChkSISScope.Checked = True
            End If

            If cmescope = True Then
                ChkCMEScope.Checked = True
            End If

            If sitacscope = True Then
                ChkSitacScope.Checked = True
            End If

        End If
    End Sub

    Private Sub UpdateScope(ByVal tiscope As Boolean, ByVal sisscope As Boolean, ByVal sitacscope As Boolean, ByVal cmescope As Boolean, ByVal usrid As Integer)
        Dim strtiscope As String = IIf(tiscope = True, "true", "false")
        Dim strsisscope As String = IIf(sisscope = True, "true", "false")
        Dim strsitacscope As String = IIf(sitacscope = True, "true", "false")
        Dim strcmescope As String = IIf(cmescope = True, "true", "false")
        strQuery = "update ebastusers_1 set tiscope='" & strtiscope & "',SISScope='" & strsisscope & "',CMEScope='" & _
                    strcmescope & "',SitacScope='" & strsitacscope & "' where usr_id=" & usrid
        objutil.ExeQuery(strQuery)
        LblMessage.Text = "Data has been updated successfully"
        MvMainPanel.SetActiveView(VwMessage)
    End Sub

#End Region

End Class
