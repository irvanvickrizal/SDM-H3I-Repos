Imports System.Data
Imports Common
Partial Class Admin_frmATPPDFProcess_BP
    Inherits System.Web.UI.Page
    Dim dbutils As New DBUtil
    Dim controller As New ATPPipelineController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            BindData(GetSearchBy())
        End If
    End Sub

    Protected Sub DdlSearchBy_IndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        BindData(GetSearchBy())
    End Sub

    Protected Sub GvATPPipelineProcess_ItemCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvATPPipelineProcess.RowCommand
        If (e.CommandName.Equals("GeneratePDF")) Then
            GeneratePDF(Convert.ToInt32(e.CommandArgument.ToString()))
            BindData(GetSearchBy())
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal sortby As String)
        Dim dt As DataTable = dbutils.ExeQueryDT("exec uspATP_GetPipelineByStatus '" & sortby & "'", "dt")
        GvATPPipelineProcess.DataSource = dt
        GvATPPipelineProcess.DataBind()
    End Sub

    Private Sub GeneratePDF(ByVal taskpendingid As Int32)
        Dim info As ATPPipelineInfo = controller.GetATPPipelineProcessById(taskpendingid)
        Dim newDocPath As String = String.Empty
        Dim final As String = ""
        Dim yy() As String = info.PathFolder.Split("\")
        Dim ii As Integer
        For ii = 0 To yy.Length - 2
            final = IIf(final = "", yy(ii), final & "\" & yy(ii))
        Next
        final = final & "\"
        Try
            newDocPath = Common.TakeFileUpload.ConvertAnyFormatToPDFHtmlNew(info.OriginalPath, info.PathFolder, info.OriginalFilename)
            controller.UpdateATPQueing("SUCCESS", info.TaskPendingId)
            controller.UpdateATPPath(info.PackageId, final & newDocPath, Integer.Parse(ConfigurationManager.AppSettings("ATP")))
        Catch ex As Exception
            controller.UpdateATPQueing("FAILED", info.TaskPendingId)
        End Try
    End Sub

    Private Function GetSearchBy() As String
        If DdlSearchBy.SelectedValue = "0" Then
            Return "ALL"
        ElseIf DdlSearchBy.SelectedValue = "1" Then
            Return "SUCCESS"
        Else
            Return "FAILED"
        End If
    End Function

#End Region

End Class
