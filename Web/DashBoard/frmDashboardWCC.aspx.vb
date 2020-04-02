Imports system.Data

Partial Class DashBoard_frmDashboardWCC
    Inherits System.Web.UI.Page

    Private controller As New WCCController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If (Not Page.IsPostBack) Then
            BindAgenda(CommonSite.UserId)
            TaskPendingCount(CommonSite.UserId)
        End If
    End Sub

    Protected Sub LbtWCCTaskPending_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtWCCTaskPending.Click
        Response.Redirect("frmSiteDocCount_WCC.aspx")
    End Sub

#Region "Custom Methods"
    Private Sub BindAgenda(ByVal userid As Integer)
        Dim dtAgenda As DataTable = controller.WCCDashboardAgenda(userid)
        If dtAgenda.Rows.Count > 0 Then
            For intCount As Integer = 0 To dtAgenda.Rows.Count - 1
                If (dtAgenda.Rows(intCount)("columnType").ToString() = "WCCPreparation") Then
                    If dtAgenda.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblSiteNeedWCCNull.Visible = True
                        LblWCCPreparation.Visible = False
                    Else
                        LblSiteNeedWCCNull.Visible = False
                        LblWCCPreparation.Visible = True
                        LblWCCPreparation.Text = "WCC Preparation (" & Convert.ToString(dtAgenda.Rows(intCount)("TotalTaskPending")) & ")"
                    End If
                End If
                If (dtAgenda.Rows(intCount)("columnType").ToString() = "WCCReadyCreation") Then
                    If dtAgenda.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblWCCReadyCreationNull.Visible = True
                        LblWCCReadyCreation.Visible = False
                    Else
                        LblWCCReadyCreationNull.Visible = False
                        LblWCCReadyCreation.Visible = True
                        LblWCCReadyCreation.Text = "WCC Ready Creation (" & Convert.ToString(dtAgenda.Rows(intCount)("TotalTaskPending")) & ")"
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
                        LblWCCRejection.Visible = False
                    Else
                        LblWCCRejectionNull.Visible = False
                        LblWCCRejection.Visible = True
                        LblWCCRejection.Text = "WCC Rejection (" & Convert.ToString(dtAgenda.Rows(intCount)("TotalTaskPending")) & ")"
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub TaskPendingCount(ByVal userid As Integer)
        Dim pendingCount As Integer = controller.GetWCCTaskPendingCount(userid)
        If pendingCount > 0 Then
            LblWCCTaskPending.Visible = False
            LbtWCCTaskPending.Text = "Your Task Pending (" & pendingCount.ToString() & ")"
        End If
    End Sub
#End Region

End Class
