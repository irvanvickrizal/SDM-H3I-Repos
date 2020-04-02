Imports System.Data
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Collections
Imports System.Web.Security
Imports BusinessLogic
Imports Common
Imports Entities
Partial Class RPT_POReportsforsiite
    Inherits System.Web.UI.Page
    Dim objBO As New BODashBoard
    Dim objdl As New BODDLs

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            objdl.fillDDL(DDPoDetails, "pono", True, Constants._DDL_Default_Select)
        End If
    End Sub

    Protected Sub DDPoDetails_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDPoDetails.SelectedIndexChanged
        Dim pono As String = Me.DDPoDetails.SelectedItem.Value
        Dim grandtotal As Integer = 0
     

        If (Me.DDPoDetails.SelectedIndex > 0) Then

            Dim dt As DataTable = objBO.ReportsForSite(pono)

            If (dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    With Me
                        .lblTotalSite.Text = IIf(IsDBNull(dr("TotalActive")), 0, IIf(dr("TotalActive").ToString().Trim().Length = 0, 0, dr("TotalActive").ToString()))

                        .lblAvgSite.Text = "100"
                        .lblTotalSiteIntegrated.Text = IIf(IsDBNull(dr("TotalSiteIntegration")), 0, IIf(dr("TotalSiteIntegration").ToString().Trim().Length = 0, 0, dr("TotalSiteIntegration").ToString()))
                        .lblAvgSiteIntegrated.Text = Format(((dr("TotalSiteIntegration") / dr("TotalActive")) * 100), "0.00")
                        If (dr("TotalSiteIntegration") < dr("TotalActive")) Then
                            .lblTotalSiteDocumentStarted.Text = IIf(IsDBNull(dr("TotalSiteIntegration")), 0, IIf(dr("TotalSiteIntegration").ToString().Trim().Length = 0, 0, (dr("TotalActive") - dr("TotalSiteIntegration")).ToString()))
                            .lblAvgSiteDocumentStarted.Text = Format((((dr("TotalActive") - dr("TotalSiteIntegration")) / dr("TotalActive")) * 100), "0.00")

                        Else
                            .lblTotalSiteDocumentStarted.Text = IIf(IsDBNull(dr("TotalSiteIntegration")), 0, IIf(dr("TotalSiteIntegration").ToString().Trim().Length = 0, 0, (dr("TotalSiteIntegration") - dr("TotalActive")).ToString()))
                            .lblAvgSiteDocumentStarted.Text = Format((((dr("TotalSiteIntegration") - dr("TotalActive")) / dr("TotalActive")) * 100), "0.00")

                        End If
                        .lblTotalSiteDocumentsProgress.Text = IIf(IsDBNull(dr("TotalSiteDocNotStarted")), 0, IIf(dr("TotalSiteDocNotStarted").ToString().Trim().Length = 0, 0, dr("TotalSiteDocNotStarted").ToString()))
                        .lblAvgSiteDocumentsProgress.Text = Format(((dr("TotalSiteDocNotStarted") / dr("TotalActive")) * 100), "0.00") '("#0.00")

                        ' .lblAvgSiteDocumentsProgress.Text = IIf(IsDBNull(dr("AvgSiteDocProgress")), 0, IIf(dr("AvgSiteDocProgress").ToString().Trim().Length = 0, 0, dr("AvgSiteDocProgress").ToString()))
                        '  grandtotal += Convert.ToInt64(dr("TotalSiteDocOnProcess"))
                        .lblTotalSiteDocCompleted.Text = IIf(IsDBNull(dr("TotalSiteCompleteDocuments")), 0, IIf(dr("TotalSiteCompleteDocuments").ToString().Trim().Length = 0, 0, dr("TotalSiteCompleteDocuments").ToString("0.00")))
                        grandtotal += Convert.ToInt64(dr("TotalSiteCompleteDocuments"))

                        .lblAvgSiteDocCompleted.Text = Format(((dr("TotalSiteCompleteDocuments") / dr("TotalActive")) * 100), "0.00")

                        .lblTotalSitesBast.Text = IIf(IsDBNull(dr("TotalSiteBast")), 0, IIf(dr("TotalSiteBast").ToString().Trim().Length = 0, 0, dr("TotalSiteBast").ToString()))
                        .lblAvgSitesBast.Text = IIf(IsDBNull(dr("TotalSiteBast")), 0, IIf(dr("TotalSiteBast").ToString().Trim().Length = 0, 0, Format(((dr("TotalSiteBast") / dr("TotalActive")) * 100), "0.00")))


                        .lblIntegrationFirstUpload.Text = dr("AvgFirstDocUpload").ToString()
                        .lblLastDocApproved.Text = dr("AvgLastDocUpload").ToString()


                    End With
                Next
                Pichart.ImageUrl = "poreportsimage.ashx?pono=" & pono
            End If
        End If

    End Sub

    
End Class
