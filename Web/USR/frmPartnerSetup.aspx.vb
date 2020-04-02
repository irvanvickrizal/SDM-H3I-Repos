Imports Common

Partial Class USR_frmPartnerSetup
    Inherits System.Web.UI.Page
    Dim dbutils As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
            MvCompany.SetActiveView(VwOnly)
        End If
    End Sub

    Protected Sub LbtEditClicked(ByVal sender As Object, ByVal e As EventArgs) Handles LbtEdit.Click
        BindCompanies()
    End Sub

    Protected Sub LbtSaveCompanyClicked(ByVal sender As Object, ByVal e As EventArgs) Handles LbtSaveCompany.Click
        If DdlCompany.SelectedIndex > 0 Then
            SaveData(CommonSite.UserId, Integer.Parse(DdlCompany.SelectedValue))
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "NoCompanyDefined();", True)
            End If
        End If
    End Sub

    Protected Sub BtnGoToDashboardClicked(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGoToDashboard.Click
        Dim subconid As Integer = PartnerController.GetSubconIdByUser(CommonSite.UserId)
        If subconid = 0 Then
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "NoCompanyDefined();", True)
            End If
        Else
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "ConfirmationBox('" & PartnerController.GetSubconNameBySubconId(subconid) & "');", True)
            End If
        End If
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        LblFullname.Text = CommonSite.UserName
        Dim title As String = dbutils.ExeQueryScalarString("select signtitle from ebastusers_1 where usr_id=" & CommonSite.UserId)
        Dim email As String = dbutils.ExeQueryScalarString("select email from ebastusers_1 where usr_id=" & CommonSite.UserId)
        Dim phoneNo As String = dbutils.ExeQueryScalarString("select phoneNo from ebastusers_1 where usr_id=" & CommonSite.UserId)
        LblSignTitle.Text = title
        LblEmail.Text = email
        LblPhoneNo.Text = phoneNo
        Dim subconid As Integer = PartnerController.GetSubconIdByUser(CommonSite.UserId)
        If subconid = 0 Then
            LtrCompany.Text = "-"
        Else
            LtrCompany.Text = PartnerController.GetSubconNameBySubconId(subconid)
        End If
    End Sub
    Private Sub BindCompanies()
        DdlCompany.DataSource = PartnerController.GetSubcons(False)
        DdlCompany.DataTextField = "SubconName"
        DdlCompany.DataValueField = "SubconId"
        DdlCompany.DataBind()
        DdlCompany.Items.Insert(0, "--Please Choose Your Current Company--")
        Dim subconid As Integer = PartnerController.GetSubconIdByUser(CommonSite.UserId)
        If subconid > 0 Then
            DdlCompany.SelectedValue = Convert.ToString(subconid)
        End If
        MvCompany.SetActiveView(VwEditable)
    End Sub

    Private Sub SaveData(ByVal userid As Int32, ByVal subconid As Integer)
        LtrCompany.Text = DdlCompany.SelectedItem.Text
        PartnerController.InsertUpdateUserSubcon(userid, subconid)
        MvCompany.SetActiveView(VwOnly)
    End Sub
#End Region

End Class
