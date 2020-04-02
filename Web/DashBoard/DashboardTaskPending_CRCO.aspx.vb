Imports System.Data
Imports Common

Partial Class DashBoard_DashboardTaskPending_CRCO
    Inherits System.Web.UI.Page
    Dim objdb As New DBUtil
    Dim dtTaskPendingsCR As New DataTable
    Dim dtTaskReview As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Page.IsPostBack = False) Then
            LinkDisabled()
            BindData()
            BindStaffReview()
        End If
    End Sub

    Protected Sub LbtTI2GCR_Link(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtTI2GCRLink.Click
        Response.Redirect("frmSiteDocCount_CR.aspx")
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        dtTaskPendingsCR = objdb.ExeQueryDT("exec uspGetAllTaskPendings_CRCO " & CommonSite.UserId(), "taskPendings")
        If dtTaskPendingsCR.Rows.Count > 0 Then
            For intCount As Integer = 0 To dtTaskPendingsCR.Rows.Count - 1
                If (dtTaskPendingsCR.Rows(intCount)("scope").ToString().ToUpper() = "TI2G") Then
                    If dtTaskPendingsCR.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblTI2GCRLinkDisabled.Visible = True
                    Else
                        LbtTI2GCRLink.Visible = True
                        LbtTI2GCRLink.Text = Convert.ToString(dtTaskPendingsCR.Rows(intCount)("TotalTaskPending"))
                    End If
                End If

                If (dtTaskPendingsCR.Rows(intCount)("scope").ToString().ToUpper() = "TI3G") Then
                    If dtTaskPendingsCR.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblTI3GCRLinkDisabled.Visible = True
                    Else
                        HpTI3GCRLink.Visible = True
                        HpTI3GCRLink.Text = Convert.ToString(dtTaskPendingsCR.Rows(intCount)("TotalTaskPending"))
                        HpTI3GCRLink.Target = "_top"
                        HpTI3GCRLink.NavigateUrl = ConfigurationManager.AppSettings("3GTIURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=4"
                    End If
                End If

                If (dtTaskPendingsCR.Rows(intCount)("scope").ToString().ToUpper() = "CME2G") Then
                    If dtTaskPendingsCR.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblCME2GCRLinkDisabled.Visible = True
                    Else
                        HpCME2GCRLink.Visible = True
                        HpCME2GCRLink.Text = Convert.ToString(dtTaskPendingsCR.Rows(intCount)("TotalTaskPending"))
                        HpCME2GCRLink.Target = "_top"
                        HpCME2GCRLink.NavigateUrl = ConfigurationManager.AppSettings("CMEURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=4"
                    End If
                End If

                If (dtTaskPendingsCR.Rows(intCount)("scope").ToString().ToUpper() = "CME3G") Then
                    If dtTaskPendingsCR.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblCME3GCRLinkDisabled.Visible = True
                    Else
                        HpCME3GCRLink.Visible = True
                        HpCME3GCRLink.Text = Convert.ToString(dtTaskPendingsCR.Rows(intCount)("TotalTaskPending"))
                        HpCME3GCRLink.Target = "_top"
                        HpCME3GCRLink.NavigateUrl = ConfigurationManager.AppSettings("3GCMEURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=4"
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub BindStaffReview()
        dtTaskReview = objdb.ExeQueryDT("exec uspCO_GetTaskPendingReviewer " & CommonSite.UserId(), "taskreviewpendings")
        If dtTaskReview.Rows.Count > 0 Then
            For intCount As Integer = 0 To dtTaskReview.Rows.Count - 1
                If (dtTaskReview.Rows(intCount)("Scope").ToString().ToUpper() = "TI2G") Then
                    If dtTaskPendingsCR.Rows(intCount)("TotalTaskPending") = 0 Then
                        lblTI2GStaffLinkDisabled.Visible = True
                    Else
                        lblTI2GStaffLinkDisabled.Visible = False
                        aTI2GLinkCount.Visible = True
                        LtrTI2GStaffCount.Text = Convert.ToString(dtTaskPendingsCR.Rows(intCount)("TotalTaskPending"))
                    End If
                ElseIf (dtTaskReview.Rows(intCount)("Scope").ToString().ToUpper() = "TI3G") Then
                    If dtTaskPendingsCR.Rows(intCount)("TotalTaskPending") = 0 Then
                        lblTI3GStaffLinkDisabled.Visible = True
                    Else
                        lblTI3GStaffLinkDisabled.Visible = False
                        aTI3GLinkCount.Visible = True
                        LtrTI3GStaffCount.Text = Convert.ToString(dtTaskPendingsCR.Rows(intCount)("TotalTaskPending"))
                    End If
                ElseIf (dtTaskReview.Rows(intCount)("Scope").ToString().ToUpper() = "CME2G") Then
                    If dtTaskPendingsCR.Rows(intCount)("TotalTaskPending") = 0 Then
                        lblCME2GStaffLinkDisabled.Visible = True
                    Else
                        lblCME2GStaffLinkDisabled.Visible = False
                        aCME2GLinkCount.Visible = True
                        LtrCME2GStaffCount.Text = Convert.ToString(dtTaskPendingsCR.Rows(intCount)("TotalTaskPending"))
                    End If
                ElseIf (dtTaskReview.Rows(intCount)("Scope").ToString().ToUpper() = "CME3G") Then
                    If dtTaskPendingsCR.Rows(intCount)("TotalTaskPending") = 0 Then
                        lblCME3GStaffLinkDisabled.Visible = True
                    Else
                        lblCME3GStaffLinkDisabled.Visible = False
                        aCME3GLinkCount.Visible = True
                        LtrCME3GStaffCount.Text = Convert.ToString(dtTaskPendingsCR.Rows(intCount)("TotalTaskPending"))
                    End If
                End If
            Next
        End If
    End Sub


    Private Sub LinkDisabled()
        '------------ your task pending ---------
        LblSIS2GCRLinkDisabled.Visible = False
        LblSIS3GCRLinkDisabled.Visible = False
        LblSITAC2GCRLinkDisabled.Visible = False
        LblSITAC3GCRLinkDisabled.Visible = False
        LblCME2GCRLinkDisabled.Visible = False
        LblCME3GCRLinkDisabled.Visible = False
        LblTI2GCRLinkDisabled.Visible = False
        LblTI3GCRLinkDisabled.Visible = False

        LbtTI2GCRLink.Visible = False
        HpSIS2GCRLink.Visible = False
        HpSIS3GCRLink.Visible = False
        HpSITAC3GCRLink.Visible = False
        HpCME2GCRLink.Visible = False
        HpCME3GCRLink.Visible = False
        HpTI3GCRLink.Visible = False
        HpSITAC2GCRLink.Visible = False

        '---------- staff task pending ----------
        lblSIS2GStaffLinkDisabled.Visible = False
        lblSIS3GStaffLinkDisabled.Visible = False
        lblSITAC2GStaffLinkDisabled.Visible = False
        lblSITAC3GStaffLinkDisabled.Visible = False
        lblCME2GStaffLinkDisabled.Visible = False
        lblCME3GStaffLinkDisabled.Visible = False
        lblTI2GStaffLinkDisabled.Visible = False
        lblTI3GStaffLinkDisabled.Visible = False

        aSIS2GLinkCount.Visible = False
        aSITAC2GLinkCount.Visible = False
        aSITAC3GLinkCount.Visible = False
        aCME2GLinkCount.Visible = False
        aCME3GLinkCount.Visible = False
        aTI2GLinkCount.Visible = False
        aTI3GLinkCount.Visible = False
        aSIS3GLinkCount.Visible = False

        '----------------------------------------

    End Sub


#End Region

End Class
