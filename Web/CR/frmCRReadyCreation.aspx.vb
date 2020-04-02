Imports CRFramework
Imports System.Data
Imports BusinessLogic
Imports Common
Partial Class CR_frmCRReadyCreation
    Inherits System.Web.UI.Page
    Dim controller As New CRController
    Dim objdb As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("wpid")) Then
                BindCRListApproved(Request.QueryString("wpid"))
            Else
                BindData()
            End If
        End If
    End Sub

    Protected Sub GvCRReadyCreationItemCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvCRReadyCreation.RowCommand
        If e.CommandName.Equals("opencrapproved") Then
            'BindCRListApproved(e.CommandArgument.ToString()) Go To CR Final Form
            Response.Redirect("../BAUT/frmTI_FinalCR.aspx?wpid=" & e.CommandArgument.ToString())
        End If
    End Sub

    Protected Sub GvListCRApprovedRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvListCRApproved.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim getCRID As Int32 = controller.GetCRIDInFinalCR(Request.QueryString("wpid"))
            'If getCRID > 0 Then
            '    Dim LbtGenerateFinalCR As LinkButton = CType(e.Row.FindControl("LbtGenerateFinalCR"), LinkButton)
            'End If
        End If
    End Sub

    Protected Sub GvListCRApprovedItemCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvListCRApproved.RowCommand
        If e.CommandName.Equals("pdfgenerate") Then
            GenerateFinalCR(Convert.ToInt32(e.CommandArgument.ToString()))
        ElseIf e.CommandName.Equals("changefcr") Then
            ChangeFinalCR(Convert.ToInt32(e.CommandArgument.ToString()))
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        MvCorePanel.SetActiveView(VwReadyCreation)
        GvCRReadyCreation.DataSource = controller.GetCRFinalReadyCreation(CommonSite.UserId)
        GvCRReadyCreation.DataBind()
    End Sub

    Private Sub BindCRListApproved(ByVal wpid As String)
        MvCorePanel.SetActiveView(vwCRFinal)
        BindSiteAtt(wpid)
        GvListCRApproved.DataSource = controller.GetCRApproved(wpid)
        GvListCRApproved.DataBind()
    End Sub

    Private Sub BindSiteAtt(ByVal packageid As String)
        Dim getSiteAttributeQuery As String = "select  TselProjectID,SiteNo, PoNo,POName, Workpackageid,Scope,sitename from poepmsitenew where workpackageid = '" & packageid & "'"
        Dim dtSiteAtt As DataTable = objdb.ExeQueryDT(getSiteAttributeQuery, "siteAtt")
        If dtSiteAtt.Rows.Count > 0 Then
            tdpono.InnerText = dtSiteAtt.Rows(0).Item(2)
            tdponame.InnerText = dtSiteAtt.Rows(0).Item(3)
            tdsiteno.InnerText = dtSiteAtt.Rows(0).Item(1)
            tdsitename.InnerText = dtSiteAtt.Rows(0).Item(6)
            tdscope.InnerText = dtSiteAtt.Rows(0).Item(5)
            tdwpid.InnerText = dtSiteAtt.Rows(0).Item(4)
            tdprojectId.InnerText = dtSiteAtt.Rows(0).Item(0)
        End If
    End Sub

    Private Sub GenerateFinalCR(ByVal crid As Int32)
        Response.Redirect("../Baut/frmTI_FinalCR.aspx?crid=" & crid)
    End Sub

    Private Sub ChangeFinalCR(ByVal crid As Int32)
        controller.UpdateCRAsFinal(Request.QueryString("wpid"), crid)
        'BindDataForm(HdnFinalCRID.Value)
    End Sub

#End Region

End Class
