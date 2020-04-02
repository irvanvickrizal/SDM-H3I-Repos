Imports System.Data
Imports Common
Partial Class ReportManagement
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim objutil As New DBUtil
    Dim i As Integer
    Dim kk As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack = True Then
            binddata()
        End If

    End Sub
    Sub binddata()
        grdsummary.DataSource = objutil.ExeQueryDT("exec MANAGEMENTSUMMARY " & ConfigurationManager.AppSettings("BAUTID") & " ," & ConfigurationManager.AppSettings("BASTID") & " ", "grd1")
        grdaccprogress.DataSource = objutil.ExeQueryDT("exec MANAGEMENTACCPROGSTATUSSUMMARY " & ConfigurationManager.AppSettings("BAUTID") & " ," & ConfigurationManager.AppSettings("BASTID") & "", "grd2")
        grdregaccstatus.DataSource = objutil.ExeQueryDT("exec MANAGEMENTACCSTATUSREGIONAL " & ConfigurationManager.AppSettings("BAUTID") & "", "grd3")
        grdregaccprostatus.DataSource = objutil.ExeQueryDT("exec ManagementRegaccprostatus", "grd4")
        grdaccstatusfinal.DataSource = objutil.ExeQueryDT("exec MANAGEMENTACCSTATUSFINAL " & ConfigurationManager.AppSettings("BASTID") & "", "grd5")
        DataBind()
        doit(grdregaccstatus)
        doit(grdaccstatusfinal)


    End Sub
    Sub doit(ByVal gv As GridView)
        For i = 0 To gv.Rows.Count - 1
            If gv.Rows(i).Cells(0).Text = kk Then
                gv.Rows(i).Cells(0).Text = ""
            Else
                kk = gv.Rows(i).Cells(0).Text
            End If
        Next
    End Sub

End Class
