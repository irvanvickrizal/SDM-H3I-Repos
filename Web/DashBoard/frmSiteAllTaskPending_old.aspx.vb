Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Imports NSNCustomizeConfiguration


Partial Class DashBoard_frmSiteAllTaskPending
    Inherits System.Web.UI.Page
    Dim dtTaskPendings As New DataTable
    Dim dtTaskPendingsRFT As New DataTable
    Dim dtTaskPendingsQC As New DataTable
    Dim dtTaskPendingsAppr As New DataTable
    Dim dtTaskPendingsSOAC As New DataTable
    Dim objBo As New BODashBoard
    Dim objdb As New DBUtil
    Dim delegatecontroller As New DelegationController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            LinkDisabled()
            HyperLinkDisabled()
            BindData()
            BindRFTReadyCreation()
            BindDataApprovedLast30Days()
        End If
    End Sub

    Private Sub LbtTI2GClick(ByVal sender As Object, ByVal e As EventArgs) Handles LbtTI2GLink.Click
        Response.Redirect("frmSiteDocCount.aspx")
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        dtTaskPendings = objdb.ExeQueryDT("exec HCPT_uspGetAllTaskPendings " & CommonSite.UserId(), "taskPendings")
        If dtTaskPendings.Rows.Count > 0 Then
            For intCount As Integer = 0 To dtTaskPendings.Rows.Count - 1
                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "TI2G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblTI2GLinkDisabled.Visible = True
                    Else
                        LbtTI2GLink.Visible = True
                        LbtTI2GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "TI3G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblTI3GLinkDisabled.Visible = True
                    Else
                        HpTI3GLink.Visible = True
                        HpTI3GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpTI3GLink.Target = "_top"
                        HpTI3GLink.NavigateUrl = ConfigurationManager.AppSettings("3GTIURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "SITAC2G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblSITAC2GLinkDisabled.Visible = True
                    Else
                        HpSITAC2GLink.Visible = True
                        HpSITAC2GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpSITAC2GLink.Target = "_top"
                        HpSITAC2GLink.NavigateUrl = ConfigurationManager.AppSettings("SITacURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=2G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "SITAC3G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblSITAC3GLinkDisabled.Visible = True
                    Else
                        HpSITAC3GLink.Visible = True
                        HpSITAC3GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpSITAC3GLink.Target = "_top"
                        HpSITAC3GLink.NavigateUrl = ConfigurationManager.AppSettings("3GSITacURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "CME2G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblCME2GLinkDisabled.Visible = True
                    Else
                        HpCME2GLink.Visible = True
                        HpCME2GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpCME2GLink.Target = "_top"
                        HpCME2GLink.NavigateUrl = ConfigurationManager.AppSettings("CMEURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=2G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "CME3G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblCME3GLinkDisabled.Visible = True
                    Else
                        HpCME3GLink.Visible = True
                        HpCME3GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpCME3GLink.Target = "_top"
                        HpCME3GLink.NavigateUrl = ConfigurationManager.AppSettings("3GCMEURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "SIS2G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblSIS2GLinkDisabled.Visible = True
                    Else
                        HpSIS2GLink.Visible = True
                        HpSIS2GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpSIS2GLink.Target = "_top"
                        HpSIS2GLink.NavigateUrl = ConfigurationManager.AppSettings("SISURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=2G&tskid=1"
                    End If
                End If

                If (dtTaskPendings.Rows(intCount)("scope").ToString().ToUpper() = "SIS3G") Then
                    If dtTaskPendings.Rows(intCount)("TotalTaskPending") = 0 Then
                        LblSIS3GLinkDisabled.Visible = True
                    Else
                        HpSIS3GLink.Visible = True
                        HpSIS3GLink.Text = Convert.ToString(dtTaskPendings.Rows(intCount)("TotalTaskPending"))
                        HpSIS3GLink.Target = "_top"
                        HpSIS3GLink.NavigateUrl = ConfigurationManager.AppSettings("3GSISURL") & "?S=" & Session("User_Login") & "&P=" & Session("User_Pwd") & "&R=" & Session("Res") & "&PT=3G&tskid=1"
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub BindRFTReadyCreation()
        dtTaskPendingsRFT = objdb.ExeQueryDT("exec HCPT_uspRFT_GetReadyCreation_Count " & CommonSite.UserId() & ", " & ConfigurationManager.AppSettings("BAUTID"), "taskPendings")
        If dtTaskPendingsRFT.Rows.Count > 0 Then
            For intCount As Integer = 0 To dtTaskPendingsRFT.Rows.Count - 1
                If (dtTaskPendingsRFT.Rows(intCount)("scope").ToString().ToUpper() = "TI2G") Then
                    If dtTaskPendingsRFT.Rows(intCount)("Totaldoc") = 0 Then
                        LblTI2GRFTLink.Visible = True
                    Else
                        LbtTI2GRFTLink.Visible = True
                        LblLinkRFT2G.Text = Convert.ToString(dtTaskPendingsRFT.Rows(intCount)("Totaldoc"))
                    End If
                End If
                LblTI3GRFTLink.Visible = True
                LblCME2GRFTLink.Visible = True
                LblCME3GRFTLink.Visible = True
                LblSIS2GRFTLink.Visible = True
                LblSIS3GRFTLink.Visible = True
                LblSITAC2GRFTLink.Visible = True
                LblSITAC3GRFTLink.Visible = True
            Next
        End If
    End Sub

    Private Sub BindDataApprovedLast30Days()
        dtTaskPendingsAppr = objdb.ExeQueryDT("exec HCPT_uspTrans_GetDocApprovedByUser30Days_Count " & CommonSite.UserId(), "taskPendings")
        If dtTaskPendingsAppr.Rows.Count > 0 Then
            For intCount As Integer = 0 To dtTaskPendingsAppr.Rows.Count - 1
                If (dtTaskPendingsAppr.Rows(intCount)("scope").ToString().ToUpper() = "TI2G") Then
                    If dtTaskPendingsAppr.Rows(intCount)("Totaldoc") = 0 Then
                        LblTI2GAPPLink.Visible = True
                    Else
                        LbtTI2GAPPLink.Visible = True
                        LblLinkAPP2G.Text = Convert.ToString(dtTaskPendingsAppr.Rows(intCount)("Totaldoc"))
                    End If
                End If
                LblTI3GAPPLink.Visible = True
                LblCME2GAPPLink.Visible = True
                LblCME3GAPPLink.Visible = True
                LblSIS2GAPPLink.Visible = True
                LblSIS3GAPPLink.Visible = True
                LblSITAC2GAPPLink.Visible = True
                LblSITAC3GAPPLink.Visible = True
            Next
        End If
    End Sub

    Private Sub LinkDisabled()
        LblTI2GLinkDisabled.Visible = False
        LblSIS2GLinkDisabled.Visible = False
        LblSITAC2GLinkDisabled.Visible = False
        LblCME2GLinkDisabled.Visible = False
        LblTI3GLinkDisabled.Visible = False
        LblSIS3GLinkDisabled.Visible = False
        LblSITAC3GLinkDisabled.Visible = False
        LblCME3GLinkDisabled.Visible = False

        LblTI2GRFTLink.Visible = False
        LblTI3GRFTLink.Visible = False

        LblSIS2GAPPLink.Visible = False
        LblSITAC2GAPPLink.Visible = False
        LblCME2GAPPLink.Visible = False
        LblTI2GAPPLink.Visible = False

        LblSIS3GAPPLink.Visible = False
        LblSITAC3GAPPLink.Visible = False
        LblCME3GAPPLink.Visible = False
        LblTI3GAPPLink.Visible = False
        
    End Sub

    Private Sub HyperLinkDisabled()
        LbtTI2GLink.Visible = False
        HpTI3GLink.Visible = False
        HpSIS2GLink.Visible = False
        HpSIS3GLink.Visible = False
        HpSITAC2GLink.Visible = False
        HpSITAC3GLink.Visible = False
        HpCME2GLink.Visible = False
        HpCME3GLink.Visible = False

        LbtTI2GRFTLink.Visible = False
        HpSIS2GRFTLink.Visible = False
        HpSITAC2GRFTLink.Visible = False
        HpCME2GRFTLink.Visible = False

        HpTI3GRFTLink.Visible = False
        HpSIS3GRFTLink.Visible = False
        HpSITAC3GRFTLink.Visible = False
        HpCME3GRFTLink.Visible = False

        LbtTI2GAPPLink.Visible = False
        HpCME2GAPPLink.Visible = False
        HpSITAC2GAPPLink.Visible = False
        HpSIS2GAPPLink.Visible = False
        HpTI3GAPPLink.Visible = False
        HpCME3GAPPLink.Visible = False
        HpSITAC3GAPPLink.Visible = False
        HpSIS3GAPPLink.Visible = False

    End Sub

#End Region

End Class
