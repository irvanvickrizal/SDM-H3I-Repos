Imports System.Data
Partial Class DashBoard_frmDashboard_WCC
    Inherits System.Web.UI.Page
    Dim controller As New WCCController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If (Not Page.IsPostBack) Then
            BindAgenda(CommonSite.UserId)
        End If
    End Sub

    Protected Sub LbtSiteNeedWCC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSiteNeedWCC.Click
        Response.Redirect("../Dashboard_WCC/WCCDashboard_Prep.aspx")
    End Sub

    Protected Sub LbtWCCReadyCreation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtWCCReadyCreation.Click
        Response.Redirect("../Dashboard_WCC/WCCDashboard_ReadyCreation.aspx")
    End Sub

    Protected Sub LbtWCCRejection_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtWCCRejection.Click
        Response.Redirect("../Dashboard_WCC/WCCDashboard_Rejection_subcon.aspx")
    End Sub

#Region "Custom Methods"
    Private Sub BindAgenda(ByVal userid As Integer)
        Dim dtAgenda As DataTable = controller.WCCDashboardSubconAgenda(userid)
        If dtAgenda.Rows.Count > 0 Then
            For intCount As Integer = 0 To dtAgenda.Rows.Count - 1
                If (dtAgenda.Rows(intCount)("columnType").ToString() = "WCCPreparation") Then
                    If dtAgenda.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblSiteNeedWCCNull.Visible = True
                        LbtSiteNeedWCC.Visible = False
                    Else
                        LblSiteNeedWCCNull.Visible = False
                        LbtSiteNeedWCC.Visible = True
                        LbtSiteNeedWCC.Text = "WCC Preparation (" & Convert.ToString(dtAgenda.Rows(intCount)("TotalTaskPending")) & ")"
                    End If
                End If
                If (dtAgenda.Rows(intCount)("columnType").ToString() = "WCCReadyCreation") Then
                    If dtAgenda.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblWCCReadyCreationNull.Visible = True
                        LbtWCCReadyCreation.Visible = False
                    Else
                        LblWCCReadyCreationNull.Visible = False
                        LbtWCCReadyCreation.Visible = True
                        LbtWCCReadyCreation.Text = "WCC Ready Creation (" & Convert.ToString(dtAgenda.Rows(intCount)("TotalTaskPending")) & ")"
                    End If
                End If
                If (dtAgenda.Rows(intCount)("columnType").ToString() = "WCCOnTaskPending") Then
                    If dtAgenda.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblWCCinPipelineNull.Visible = True
                        LblOnTaskPending.Visible = False
                    Else
                        LblWCCinPipelineNull.Visible = False
                        LblOnTaskPending.Visible = True
                        LblOnTaskPending.Text = "WCC On Task Pending (" & Convert.ToString(dtAgenda.Rows(intCount)("TotalTaskPending")) & ")"
                    End If
                End If
                If (dtAgenda.Rows(intCount)("columnType").ToString() = "WCCDone") Then
                    If dtAgenda.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblWCCDoneNull.Visible = True
                        LblWCCDone.Visible = False
                    Else
                        LblWCCDoneNull.Visible = False
                        LblWCCDone.Visible = True
                        LblWCCDone.Text = "WCC Done (" & Convert.ToString(dtAgenda.Rows(intCount)("TotalTaskPending")) & ")"
                    End If
                End If
                If (dtAgenda.Rows(intCount)("columnType").ToString() = "WCCRejection") Then
                    If dtAgenda.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblWCCRejectionNull.Visible = True
                        LbtWCCRejection.Visible = False
                    Else
                        LblWCCRejectionNull.Visible = False
                        LbtWCCRejection.Visible = True
                        LbtWCCRejection.Text = "WCC Rejection (" & Convert.ToString(dtAgenda.Rows(intCount)("TotalTaskPending")) & ")"
                    End If
                End If
            Next
        End If
    End Sub
#End Region

End Class
