Imports Common
Imports CRFramework
Imports System.Data

Partial Class DashBoard_frmDashboardAgendaCRCO
    Inherits System.Web.UI.Page

    Dim objUtil As New DBUtil
    Dim strsql As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub LbtCRReadyCreation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtCRReadyCreation.Click
        Response.Redirect("CRDashboardAgenda.aspx?typedash=rc")
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        strsql = "exec [uspCRCO_Agenda] " & CommonSite.UserId & ", " & ConfigurationManager.AppSettings("CRDOCID") & ", " & ConfigurationManager.AppSettings("CODOCID")
        Dim dtCRAgenda As DataTable = objUtil.ExeQueryDT(strsql, "cragenda")
        If dtCRAgenda.Rows.Count > 0 Then
            For intCount As Integer = 0 To dtCRAgenda.Rows.Count - 1
                If (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "CODone") Then
                    If dtCRAgenda.Rows(intCount)("Total") = "0" Then
                        LblCODoneNull.Visible = True
                        LblCODone.Visible = False
                    Else
                        LblCODoneNull.Visible = False
                        LblCODone.Visible = True
                        LblCODone.Text = "CO Done(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                    End If
                ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "COTselSignature") Then
                    If dtCRAgenda.Rows(intCount)("Total") = "0" Then
                        LblCOTselUnderSignature.Visible = True
                        LblCOTselUnderSignature.Visible = False
                    Else
                        LblCOTselUnderSignatureNull.Visible = False
                        LblCOTselUnderSignature.Visible = True
                        LblCOTselUnderSignature.Text = "CO Tsel Under Signature(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                    End If
                ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "CONSNSignature") Then
                    If dtCRAgenda.Rows(intCount)("Total") = "0" Then
                        LblCONSNUnderSignatureNull.Visible = True
                        LblCONSNUnderSignature.Visible = False
                    Else
                        LblCONSNUnderSignatureNull.Visible = False
                        LblCONSNUnderSignature.Visible = True
                        LblCONSNUnderSignature.Text = "CO NSN Under Signature(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                    End If
                ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "CORejection") Then
                    If dtCRAgenda.Rows(intCount)("Total") = 0 Then
                        LblCORejectionNull.Visible = True
                        LbtCORejection.Visible = False
                    Else
                        LblCORejectionNull.Visible = False
                        LbtCORejection.Visible = True
                        LbtCORejection.Text = "CO Rejection(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                    End If
                ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "COReadyCreation") Then
                    If dtCRAgenda.Rows(intCount)("Total") = 0 Then
                        LblCOReadyCreationNull.Visible = True
                        LblCOReadyCreation.Visible = False
                    Else
                        LblCOReadyCreationNull.Visible = False
                        LblCOReadyCreation.Visible = True
                        LblCOReadyCreation.Text = "CO Ready Creation(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                    End If
                ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "CRFinalDone") Then
                    If dtCRAgenda.Rows(intCount)("Total") = "0" Then
                        LblCRFinalNull.Visible = True
                        LblCRFinal.Visible = False
                        LbtCRDashboardAgenda.Visible = False
                    Else
                        LblCRFinalNull.Visible = False
                        LblCRFinal.Visible = False
                        LbtCRDashboardAgenda.Visible = True
                        LblCRFinal.Text = "CR Final Done(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                        LbtCRDashboardAgenda.Text = "CR Final Done(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                    End If
                ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "TselSignature") Then
                    If dtCRAgenda.Rows(intCount)("Total") = "0" Then
                        LblCRTselUnderSignatureNull.Visible = True
                        LblCRTselUnderSignature.Visible = False
                    Else
                        LblCRTselUnderSignatureNull.Visible = False
                        LblCRTselUnderSignature.Visible = True
                        LblCRTselUnderSignature.Text = "CR Tsel Under Signature(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                    End If
                ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "NSNSignature") Then
                    If dtCRAgenda.Rows(intCount)("Total") = "0" Then
                        LblCRUnderNSNSignatureNull.Visible = True
                        LblCRUnderNSNSignature.Visible = False
                    Else
                        LblCRUnderNSNSignatureNull.Visible = False
                        LblCRUnderNSNSignature.Visible = True
                        LblCRUnderNSNSignature.Text = "CR NSN Under Signature(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                    End If
                ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "CRRejection") Then
                    If dtCRAgenda.Rows(intCount)("Total") = "0" Then
                        LblCRRejection.Visible = True
                        LbtCRRejection.Visible = False
                    Else
                        LblCRRejection.Visible = False
                        LbtCRRejection.Visible = True
                        LbtCRRejection.Text = "CR Rejection(" & dtCRAgenda.Rows(intCount)("Total") & ")"
                    End If
                ElseIf (dtCRAgenda.Rows(intCount)("TypeofPending").ToString() = "CRReadyCreation") Then
                    If dtCRAgenda.Rows(intCount)("Total") = "0" Then
                        LblCRReadyCreationNull.Visible = True
                        LbtCRReadyCreation.Visible = False
                    Else
                        LblCRReadyCreationNull.Visible = False
                        LbtCRReadyCreation.Visible = True
                        LbtCRReadyCreation.Text = "CR Ready Creation (" & dtCRAgenda.Rows(intCount)("Total") & ")"
                    End If
                End If
            Next
        End If
    End Sub
#End Region

End Class
