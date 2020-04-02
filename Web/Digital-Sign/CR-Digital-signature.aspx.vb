Imports System.Text
Imports System.Data
Imports Common
Imports CRFramework
Imports Common_NSNFramework
Imports System.Collections.Generic

Partial Class Digital_Sign_CR_Digital_signature
    Inherits System.Web.UI.Page
    Dim controller As New CRController
    Dim objutil As New DBUtil
    Dim objsms As New SMSNew
    Dim objmail As New TakeMail
    Dim dbutils_nsn As New DBUtils_NSN

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            lnkrequest.Attributes.Add("onclick", "waitPreloadPage();")
            ConfigureHiddenField(Convert.ToInt32(Request.QueryString("id")), Request.QueryString("wpid"))
            ConfigurePanel()
            BindData()
            BindDocReviewed()
        End If
    End Sub

    Protected Sub lnkrequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkrequest.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        Dim npwd As String = ""
        Dim msgdata As New StringBuilder
        If txtUserName.Text <> "" Then
            objsms.requestSMS(Session("User_Name"), Session("User_Login"), Request.QueryString("siteno"), Request.QueryString("pono"), Request.QueryString("docname"))
            loadingdiv.Style("display") = "none"
            Response.Write("<script>alert('Please check for your password in your phone');</script>")
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please keyin the user name and click request password');", True)
        End If
    End Sub

    Protected Sub BtnSubmitSignRejectClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmitSignReject.Click

        Dim categories As String = String.Empty
        Dim MyItem As ListItem
        Dim countThick As Integer = 0
        For Each MyItem In CbReasonLists.Items
            If MyItem.Selected = True Then
                If countThick = 1 Then
                    categories = categories & ", "
                End If
                categories = categories & MyItem.Text
                countThick = 1
            End If
        Next
        SubmitSignReject(TxtRemarks_SignRejectPanel.Text, categories)
    End Sub

    Protected Sub BtnSignRejectClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSignReject.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        BindSignRejectReasons()
    End Sub

    Protected Sub BtnCancelSignRejectClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancelSignReject.Click
        MvPanelSign.SetActiveView(defaultSign)
    End Sub

    Protected Sub BtnRejectReviewNewClick(ByVal Sender As Object, ByVal e As System.EventArgs) Handles BtnRejectReviewNew.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        BindReasons()
    End Sub

    Protected Sub BtnCancelSubmitClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancelSubmit.Click
        MvApprovalButtonPanel.SetActiveView(vwDefaultPanel)
        TxtRemarksReject.Text = ""
    End Sub

    Protected Sub BtnSubmitRejectClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmitReject.Click
        Dim categories As String = String.Empty
        Dim MyItem As ListItem
        Dim countThick As Integer = 0
        For Each MyItem In CbList.Items
            If MyItem.Selected = True Then
                If countThick = 1 Then
                    categories = categories & ", "
                End If
                categories = categories & MyItem.Text
                countThick = 1
            End If
        Next
        SubmitSignReject(TxtRemarksReject.Text, categories)
    End Sub

    Protected Sub BtnSign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSign.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        Dim npwd1 As String = ""
        npwd1 = "12345678"
        Dim j As Integer
        Dim objdb As New DBUtil
        j = objdb.ExeQueryScalar("select count(*) from dgpassword where username='" & txtUserName.Text & "' and password='" & txtPassword.Text & "'")
        'j = 1
        If j > 0 Then
            Try
                Dim Flags As Integer = 0
                Dim IntCount As Integer = 0
                LblDocPath.Text = hdnDocPath.Value
                LblPageNo.Text = hdnpageNo.Value
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_GRAPHICAL_IMAGE
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_SIGNED_BY
                If 1 = 0 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_REASON
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME
                Dim strResult As String = SAPIWrapper.SAPI_sign_file_CR(ConfigurationManager.AppSettings("FPath") + hdnDocPath.Value, Nothing, txtUserName.Text, _
                npwd1, hdnpageNo.Value, hdnx.Value, hdny.Value, 35, 100, False, "test", Flags, "")
                'Dim strResult = "success"
                If (strResult = Constants._DigitalSign_Result) Then
                    Dim i As Integer = -1
                    If controller.CRDocApproved(Convert.ToInt32(Request.QueryString("id")), Convert.ToInt32(hdncrid.Value), Integer.Parse(hdnwfid.Value), Integer.Parse(hdnTaskid.Value), Integer.Parse(hdnRoleId.Value), CommonSite.UserId) = True Then
                        i = 1
                    Else
                        i = 0
                    End If
                    If i = 0 Then
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
                    ElseIf i = 1 Then
                        If Integer.Parse(hdnTaskid.Value) > 0 Then
                            SendMailNextUser(controller.GetUserTaskPendingEmail(Convert.ToInt32(Request.QueryString("id")), "approval"), Convert.ToInt32(hdncrid.Value), String.Empty)
                        End If
                    End If
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseCRApprover();", True)
                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error on signning PDF:\n" + strResult + "');", True)
                End If
            Catch ex As Exception
                objdb.ExeNonQuery("exec uspErrLog '', 'BtnSign_Click','" & ex.Message.ToString.Replace("'", "''") & "','Error on end of Catch'")
            End Try
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('invalid  userid or password');", True)
        End If
    End Sub

    Protected Sub btnreject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReject.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        Dim rdbr As New RadioButtonList
        Dim str As String = ""
        Dim y As Integer = 0
        dim k as Integer = 0
        Dim count As Integer = 0
        Dim sw_id As Integer
        Dim remarks As String = ""
        Dim usrid As Integer = Session("User_Id")
        Dim roleid As Integer = Session("Role_Id")
        Dim usrname As String = Session("User_Name")
        Dim pono As String = ""
        Dim siteid As String = ""
        Dim docid As Integer
        For k = 0 To grddocuments.Rows.Count - 1
            rdbr = grddocuments.Rows(k).Cells(2).FindControl("rdbstatus")
            If rdbr.SelectedValue = 1 Then
                str = IIf(str <> "", str & "," & grddocuments.Rows(k).Cells(4).Text.ToString, grddocuments.Rows(k).Cells(4).Text.ToString)
                sw_id = grddocuments.Rows(k).Cells(4).Text.ToString
                Dim txt1 As TextBox = grddocuments.Rows(k).Cells(3).FindControl("txtremarks")
                remarks = txt1.Text
                'irvan vickrizal 4 august 2012
                objutil.ExeNonQuery("Exec uspBAUTDocReject " & sw_id & ",'" & remarks & "'," & usrid & "," & roleid & ",'" & usrname & "','" & pono & "','" & siteid & "'," & docid & "," & ConfigurationManager.AppSettings("WCTRBASTID") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID"))
                count = count + 1
            End If
        Next
        'reject the parent document as well
        If count > 0 Then
            For k = 0 To grddocuments2.Rows.Count - 1
                rdbr = grddocuments2.Rows(k).Cells(2).FindControl("rdbstatus")
                str = IIf(str <> "", str & "," & grddocuments2.Rows(k).Cells(4).Text.ToString, grddocuments2.Rows(k).Cells(4).Text.ToString)
                sw_id = grddocuments2.Rows(k).Cells(4).Text.ToString
                Dim txt1 As TextBox = grddocuments2.Rows(k).Cells(3).FindControl("txtremarks")
                remarks = txt1.Text
                'docid = grddocuments2.Rows(k).Cells(1).Text.ToString
                'siteid = grddocuments2.Rows(k).Cells(2).Text.ToString
                'pono = grddocuments2.Rows(k).Cells(6).Text.ToString
                'irvan vickrizal 4 august 2012
                objutil.ExeNonQuery("Exec uspBAUTDocReject " & sw_id & ",'" & remarks & "'," & usrid & "," & roleid & ",'" & usrname & "','" & pono & "','" & siteid & "'," & docid & "," & ConfigurationManager.AppSettings("WCTRBASTID") & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID"))
                count = count + 1
            Next
        End If
        Dim scripts As String = ""
        If count = 0 Then
            scripts = "alert('No document Selected for Reject');"
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "rejected", scripts, True)
        Else
            scripts = "alert('Document rejected successfully');WindowRejectClose();"
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "rejected", scripts, True)
        End If
    End Sub

    Protected Sub BtnReviewClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreview.Click
        Try
            Dim i As Integer = -1
            If controller.CRDocApproved(Convert.ToInt32(Request.QueryString("id")), Convert.ToInt32(hdncrid.Value), Integer.Parse(hdnwfid.Value), Integer.Parse(hdnTaskid.Value), Integer.Parse(hdnRoleId.Value), CommonSite.UserId) = True Then
                i = 1
            Else
                i = 0
            End If
            If i = 0 Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
            ElseIf i = 1 Then
                If Integer.Parse(hdnTaskid.Value) > 0 Then
                    'This is Email Sending next task pending
                    SendMailNextUser(controller.GetUserTaskPendingEmail(Convert.ToInt32(Request.QueryString("id")), "approval"), Convert.ToInt32(hdncrid.Value), String.Empty)
                End If
            End If
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowsCloseCRReviewer();", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error " + ex.Message.ToString() + "');", True)
        End Try
    End Sub

    Protected Sub grddocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grddocuments.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim url As String
            Dim lblSubdocid As Label = CType(e.Row.FindControl("LblSubDocid"), Label)
            url = "../PO/frmViewCRDocument.aspx?crid=" & hdncrid.Value & "&subdocid=" & lblSubdocid.Text
            e.Row.Cells(1).Text = "<a href='#' onclick=""window.open('" & url & "','mywindowopen','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(1).Text & "</a>"
        End If
    End Sub

    Protected Sub grddocuments2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim url As String
            Dim lblSubdocid As Label = CType(e.Row.FindControl("LblSubDocid"), Label)
            url = "../PO/frmViewCRDocument.aspx?crid=" & hdncrid.Value & "&subdocid=" & lblSubdocid.Text
            e.Row.Cells(1).Text = "<a href='#' onclick=""window.open('" & url & "','mywindowopen','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(1).Text & "</a>"
        End If
    End Sub

#Region "Custom Methods"
    Private Sub ConfigureHiddenField(ByVal sno As Int32, ByVal wpid As String)
        Dim info As CRWFTransactionInfo = controller.GetCRTransactionBySNO(sno)
        If Not info Is Nothing Then
            Dim CRDetail As CRInfo = GetCRDetail(info.CRID)
            hdncrid.Value = info.CRID
            hdnwfid.Value = info.WFID
            hdnTaskid.Value = info.TSKID
            hdnDocPath.Value = CRDetail.DocPath
            hdnPono.Value = CRDetail.PONO
            hdnx.Value = info.XVal
            hdny.Value = info.YVal
            hdnpageNo.Value = info.PageNo
            hdnRoleId.Value = info.RoleId
        End If
    End Sub

    Private Sub BindDocReviewed()
        Dim divRev As String = ""
        Dim reviewer As New ReviewerInfo

        For Each reviewer In controller.GetCRReviewed(Convert.ToInt32(Request.QueryString("id")))
            divRev += "Reviewed by " & reviewer.UserName & " As " & reviewer.SignTitle & " On " & String.Format("{0:dd MMM yyyy}", reviewer.ExecuteDate) & " " & "<br />"
        Next
        divReviewer.InnerHtml = divRev
    End Sub

    Private Sub ConfigurePanel()
        MvApprovalButtonPanel.SetActiveView(vwDefaultPanel)
        MvPanelSign.SetActiveView(defaultSign)
        If controller.GetTaskDescByTaskId(Integer.Parse(hdnTaskid.Value)).ToLower().Equals("approver") Then
            txtUserName.Text = objutil.ExeQueryScalarString("select usrlogin from ebastusers_1 where usrlogin='" & Session("User_Login") & "'")
            txtUserName.Enabled = False
            dgrow.Visible = True
            rerow.Visible = False
        Else
            dgrow.Visible = False
            rerow.Visible = True
        End If
    End Sub

    Private Sub BindData()
        BindPDFDocument(hdnDocPath.Value)
        BindAdditionalDocument(Convert.ToInt32(hdncrid.Value))
    End Sub

    Private Sub BindPDFDocument(ByVal strPath As String)
        PDFViwer.Attributes.Add("src", ConfigurationManager.AppSettings("Vpath") + strPath.Replace("\", "/"))
    End Sub

    Private Sub BindAdditionalDocument(ByVal crid As Int32)
        If Not String.IsNullOrEmpty(Request.QueryString("type")) Then
            If Request.QueryString("type").Equals("app") Then
                grddocuments.DataSource = controller.GetMOMCRDocumentList(crid)
                grddocuments.DataBind()
            Else
                grddocuments2.DataSource = controller.GetMOMCRDocumentList(crid)
                grddocuments2.DataBind()
            End If
        End If
    End Sub

    Protected Function GetCRDetail(ByVal crid As Int32) As CRInfo
        Return controller.GetCRDetail(crid)
    End Function

    Sub DOThis(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rad As RadioButtonList = CType(sender, RadioButtonList)
        Dim grdr As GridViewRow = CType(rad.NamingContainer, GridViewRow)
        Dim x As Integer = grdr.RowIndex
        Dim rdl As RadioButtonList = grddocuments.Rows(x).Cells(2).FindControl("rdbstatus")
        Dim txt1 As TextBox = grddocuments.Rows(x).Cells(3).FindControl("txtremarks")
        txt1.Text = "Please enter remarks here......"
        If rdl.SelectedValue = 0 Then 'when approve
            txt1.Visible = False
            txtUserName.Enabled = True
            txtUserName.Visible = True
            txtPassword.Enabled = True
            txtPassword.Visible = True
            lnkrequest.Enabled = True
            lnkrequest.Visible = True
            BtnSign.Enabled = True
            BtnSign.Visible = True
            btnReject.Enabled = False
            btnReject.Visible = False
            btnreview.Enabled = True
            btnreview.Visible = True
            btnrejectreview.Enabled = False
            btnrejectreview.Visible = False
        Else 'when reject
            txt1.Visible = True
            txtUserName.Enabled = False
            txtUserName.Visible = False
            txtPassword.Enabled = False
            txtPassword.Visible = False
            lnkrequest.Enabled = False
            lnkrequest.Visible = False
            BtnSign.Enabled = False
            BtnSign.Visible = False
            btnReject.Enabled = True
            btnReject.Visible = True
            btnreview.Enabled = False
            btnreview.Visible = False
            btnrejectreview.Enabled = True
            btnrejectreview.Visible = True
        End If
    End Sub

    Private Sub SubmitSignReject(ByVal remarks As String, ByVal catReason As String)
        Dim wpid As String = Request.QueryString("wpid")
        Dim site_id As Int32 = 0

        If (wpid IsNot Nothing) Then
            Dim GrpId As String = Request.QueryString("id")
            Dim i As Integer = -1
            If controller.CRDocRejected(Convert.ToInt32(Request.QueryString("id")), Convert.ToInt32(hdncrid.Value), Integer.Parse(hdnwfid.Value), Integer.Parse(hdnTaskid.Value), Integer.Parse(hdnRoleId.Value), CommonSite.UserId, remarks, catReason) = True Then
                i = 1
            Else
                i = 0
            End If
            If i = 0 Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Error while doing the transaction.');", True)
            ElseIf i = 1 Then
                Try
                    SendMailNextUser(controller.GetUserTaskPendingEmail(Convert.ToInt32(Request.QueryString("id")), "rejection"), Convert.ToInt32(hdncrid.Value), remarks)
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowRejectCloseCR();", True)
                Catch ex As Exception
                    'objdb.ExeNonQuery("exec uspErrLog 01, 'mailsendingreject','" & ex.Message.ToString.Replace("'", " ") & "','sendmailreject'")
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowRejectCloseCR();", True)
                End Try
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "WindowRejectCloseCR();", True)
            End If
        End If
    End Sub

    Private Sub SendMailNextUser(ByVal emails As List(Of EmailInfo), ByVal crid As Int32, ByVal remarks As String)
        Dim getEmailInfo As New EmailInfo
        Dim appruser As String = String.Empty
        Dim appvduser As String = String.Empty
        Dim appvddate As String = String.Empty
        Dim statusType As String = String.Empty
        Dim userlist As New List(Of UserInfo)
        For Each getEmailInfo In emails
            Dim info As New UserInfo
            appruser = getEmailInfo.Username
            appvduser = getEmailInfo.ExecuteBy
            appvddate = String.Format("{0:dd-MMMM-yyyy mm:hh:ss}", getEmailInfo.ExecuteDate)
            statusType = getEmailInfo.StatusType
            info.UserId = getEmailInfo.Userid
            info.Email = getEmailInfo.Email
            info.Handphone = getEmailInfo.PhoneNo
            info.UserType = ""
            userlist.Add(info)
        Next
        Dim sb As New StringBuilder
        If emails.Count > 1 Then
            sb.Append("Dear Sir / Madam,<br/>")
        Else
            sb.Append("Dear " & appruser & ", <br/>")
        End If
        Dim company As String = String.Empty
        If CommonSite.UserType = "N" Then
            company = "Nokia Siemens Networks"
        Else
            company = "Telkomsel"
        End If
        Dim crdetailinfo As CRInfo = GetCRDetail(crid)
        Dim emailsubject As String = String.Empty
        If statusType.Equals("Approved") Then
            sb.Append("Following detail of CR Document already approved by " & appvduser & " " & company & "<br/>")
            emailsubject = "CR Approved"
        ElseIf statusType.Equals("Rejected") Then
            sb.Append("Following detail of CR Document was rejected by " & appvduser & " " & company & "<br/>")
            emailsubject = "CR Rejected"
        Else
            sb.Append("Following detail CR Document is waiting your approval/review <br/>")
            emailsubject = "Document CR Waiting"
        End If
        sb.Append("CR No : " & crdetailinfo.CRNo & "<br/>")
        sb.Append("SiteNo :" & crdetailinfo.SiteNo & "<br/>")
        sb.Append("SiteName :" & crdetailinfo.SiteName & "<br/>")
        sb.Append("Scope :" & crdetailinfo.Scope & "<br/>")
        sb.Append("HOT/HOG :" & crdetailinfo.PONO & "<br/>")
        sb.Append("PO Name :" & crdetailinfo.EOName & "<br/>")
        sb.Append("Workpackageid :" & crdetailinfo.PackageId & "<br/>")
        If statusType.Equals("Rejected") Then
            sb.Append("Reason of Rejection : " & remarks & "<br/><br/>")
        End If
        sb.Append("<a href='https://www.telkomsel.nsnebast.com'>Click here</a>" & " to go to e-BAST<br/>")
        sb.Append("Powered By EBAST" & "<br/>")
        sb.Append("<img src='http://www.telkomsel.nsnebast.com/images/nsn-logo.gif' alt='companylogo' />")
        objmail.SendMailUserGroup(userlist, sb.ToString(), emailsubject)
    End Sub

    Private Sub BindSignRejectReasons()
        MvPanelSign.SetActiveView(defaultSignReject)
        CbReasonLists.DataSource = dbutils_nsn.GetReasons(ConfigurationManager.AppSettings("ATP"))
        CbReasonLists.DataTextField = "ReasonName"
        CbReasonLists.DataValueField = "ReasonId"
        CbReasonLists.DataBind()
    End Sub

    Private Sub BindReasons()
        MvApprovalButtonPanel.SetActiveView(vwRejectPanel)
        CbList.DataSource = dbutils_nsn.GetReasons(ConfigurationManager.AppSettings("ATP"))
        CbList.DataTextField = "ReasonName"
        CbList.DataValueField = "ReasonId"
        CbList.DataBind()
    End Sub

#End Region

End Class
