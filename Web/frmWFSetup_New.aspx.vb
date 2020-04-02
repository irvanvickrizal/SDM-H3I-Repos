Imports BusinessLogic
Imports Common
Partial Class frmWFSetup_New
    Inherits System.Web.UI.Page
    Dim objDl As New BODDLs
    Dim controller As New WFController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            objDl.fillDDL(ddlGrp, "TDstGroup", True, Constants._DDL_Default_None)
            If GetWFID() > 0 Then
                BindData(GetWFID())
                BindWorkflowDefinition(GetWFID())
            End If
        End If
    End Sub

    Protected Sub BtnCreateNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCreateNew.Click
        If FormChecking() = True Then
            Dim info As New WorkflowInfo
            info.WFID = 0
            info.WFCode = txtWFCode.Value
            info.WFName = txtWFName.Value
            info.SLATotal = txtTime.Value
            info.RStatus = 2
            If ddlGrp.SelectedIndex > 0 Then
                info.DscopeInfo.ScopeId = Integer.Parse(ddlGrp.SelectedValue)
            Else
                info.DscopeInfo.ScopeId = 0
            End If

            info.CMAInfo.ModifiedUser = CommonSite.UserName

            Dim getNewWFID As Integer = controller.Workflow_IU(info)
            If getNewWFID > 0 Then
                LblErrorMessage.Visible = True
                LblErrorMessage.ForeColor = Drawing.Color.Green
                LblErrorMessage.Font.Italic = True
                LblErrorMessage.Text = "New Workflow Successfully added"
                Response.Redirect("frmWFSetup_New.aspx?id=" & getNewWFID.ToString())
            Else
                LblErrorMessage.Visible = True
                LblErrorMessage.ForeColor = Drawing.Color.Green
                LblErrorMessage.Font.Italic = True
                LblErrorMessage.Text = "FLow Code Already Exist"
            End If

        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData(ByVal wfid As Integer)
        Dim info As WorkflowInfo = controller.Workflow_D(wfid, String.Empty, String.Empty, String.Empty)
        If info IsNot Nothing Then
            txtWFCode.Value = info.WFCode
            txtWFName.Value = info.WFName
            txtTime.Value = info.SLATotal.ToString()
        End If
    End Sub

    Private Sub BindWorkflowDefinition(ByVal wfid As Integer)

    End Sub

    Private Function GetWFID() As Integer
        If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
            Return Integer.Parse(Request.QueryString("id"))
        Else
            Return 0
        End If
    End Function

    Private Function FormChecking() As Boolean
        Dim isvalid As Boolean = True
        Dim strErrMessage As String = String.Empty
        If String.IsNullOrEmpty(txtWFCode.Value) And isvalid = True Then
            strErrMessage = "Please fill Process Flow Code!"
            isvalid = False
        End If
        If String.IsNullOrEmpty(txtWFName.Value) And isvalid = True Then
            strErrMessage = "Please fill Process Flow Name"
            isvalid = False
        End If
        If String.IsNullOrEmpty(txtTime.Value) And isvalid = True Then
            strErrMessage = "Please fill SLA Total"
            isvalid = False
        End If

        If isvalid = False Then
            LblErrorMessage.Text = strErrMessage
            LblErrorMessage.ForeColor = Drawing.Color.Red
            LblErrorMessage.Font.Italic = True
            LblErrorMessage.Visible = True
        End If

        Return isvalid
    End Function
#End Region
End Class
