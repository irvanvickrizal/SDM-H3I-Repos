Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class DashBoard_mom_Approve
    Inherits System.Web.UI.Page
    Dim objDL As New BODDLs
    Dim dt As New DataTable
    Dim objBo As New BODashBoard
    Dim intPage, intX, intY, intHeight, intWidth As New Integer
    Dim objdb As New DBUtil
    Dim dtn As New DataTable
    Dim objBOM As New BOMailReport
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            binddata()
        End If
    End Sub
  
    Sub binddata()

        dt = objBo.uspPendingMOM(CommonSite.UserId())
        grdDocuments.DataSource = dt
        grdDocuments.DataBind()

    End Sub
    
    Protected Sub BtnSign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSign.Click
        Dim strResult As String, strError As String = ""


        Dim IntCount As Integer = 0



        intHeight = 50
        intWidth = 150
        For iLoop As Integer = 0 To grdDocuments.Rows.Count - 1
            Dim CheckBoxSno As HtmlInputCheckBox = CType(grdDocuments.Rows(iLoop).FindControl("EmpId"), HtmlInputCheckBox)
            Dim iHdTaskId As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("HdTaskid"), HiddenField)
            Dim iHdDocPath As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("HDDocPath"), HiddenField)
            Dim iHdXval As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("HDXVal"), HiddenField)
            Dim iHdYval As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("HDYVal"), HiddenField)
            Dim iHdPageNo As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("HDPage"), HiddenField)
            Dim iHdSno As HiddenField = CType(grdDocuments.Rows(iLoop).FindControl("HDSno"), HiddenField)

            If CheckBoxSno.Checked Then
                Dim Flags As Integer = 0
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_GRAPHICAL_IMAGE
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_SIGNED_BY
                If 1 = 0 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_REASON
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME
                Dim i As Integer = -1
                strResult = SAPIWrapper.SAPI_sign_file(ConfigurationManager.AppSettings("FPath") + iHdDocPath.Value, Nothing, txtUserName.Text, _
                txtPassword.Text, iHdPageNo.Value, iHdXval.Value, iHdYval.Value, intHeight, intWidth, False, "test", Flags, "")
                ' strResult = "success"
                If (strResult = "success") Then
                    i = objBo.uspMOMApproved(Convert.ToInt32(CheckBoxSno.Value))
                    If i = 0 Then
                        strError += iHdDocPath.Value.Remove(1, 4) + " - has been error while doing the transaction.  \n\n "
                    Else
                        ''added by satish on 11thApril
                        ''*************************** for mail functionality.***********************
                        ''to find next user
                        'Dim nextid As Integer
                        'Dim strs As String = "SELECT ISNULL(USERID,1) FROM WFTRANSACTION WHERE" & _
                        '" SITE_ID=(SELECT SITE_ID FROM WFTRANSACTION WHERE SNO = " & Convert.ToInt32(Request.QueryString("id")) & " ) " & _
                        '" AND SITEVERSION = (SELECT SITEVERSION FROM WFTRANSACTION WHERE SNO = " & iHdSno.Value & ")" & _
                        '" AND STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL"
                        'nextid = objdb.ExeQueryScalar(strs)
                        'If nextid <> 0 Then
                        '    sendmailTransNew(nextid)
                        'Else
                        '    sendmailTransNew(1)
                        'End If
                        ''**************************************************************************
                        strError += iHdDocPath.Value.Remove(1, 4) + " - has been Signed Sucessfully. \n\n "
                    End If
                Else
                    strError += iHdDocPath.Value.Remove(1, 4) + " - has been error on signning PDF: \n " + strResult + " \n\n "
                End If
            End If
        Next
        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('" + strError + "');", True)

        txtUserName.Text = ""
        txtPassword.Text = ""
        binddata()
    End Sub
    


    Protected Sub grdDocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDocuments.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblnoSec"), Label)
                lbl.Text = (grdDocuments.PageIndex * grdDocuments.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

 

    Protected Sub grdDocuments_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDocuments.PageIndexChanging
        grdDocuments.PageIndex = e.NewPageIndex
        binddata()
    End Sub
    Sub sendmailTransNew(ByVal userid As Integer)
        Try
            dtn = objdb.ExeQueryDT("exec uspgetemailNEW " & userid, "maildt")
            If dtn.Rows.Count > 0 And dtn.Rows(0).Item(3) <> "X" Then
                dt = objBOM.uspMailReportLD(9, )  ''this is for document upload time sending mail
                Dim k As Integer
                Dim Remail As String = "'"
                Dim name As String = ""
                Dim doc As New StringBuilder
                Remail = dtn.Rows(0).Item(3).ToString
                name = dtn.Rows(0).Item(2).ToString
                Dim senderAdd As String = ConfigurationManager.AppSettings("Smailid")
                Dim receiverAdd As String = Remail
                Dim mySMTPClient As New System.Net.Mail.SmtpClient
                Dim myEmail As New System.Net.Mail.MailMessage
                myEmail.BodyEncoding() = System.Text.Encoding.UTF8
                myEmail.SubjectEncoding = System.Text.Encoding.UTF8
                myEmail.To.Add(receiverAdd)
                myEmail.Subject = dt.Rows(0).Item(3).ToString  ''subject from table
                myEmail.Body = dt.Rows(0).Item(2).ToString & name & ",<br> <br>" ''salutatation from table
                myEmail.Body = myEmail.Body & dt.Rows(0).Item(4).ToString & " <br> <br> "
                myEmail.Body = myEmail.Body & " Process - " & ConfigurationManager.AppSettings("Type").Replace("\", "") & " <br> <br> "
                Dim sb As String = ""
                sb = "<table  border=1>"
                sb = sb & "<tr><td>Contract #</td><td>Site</td><td>Scope</td><td>Document</td><td>Date Requested</td></tr>"
                For k = 0 To dtn.Rows.Count - 1
                    sb = sb & "<tr><td> " & dtn.Rows(k).Item(6).ToString & "  </td><td>" & dtn.Rows(k).Item(4).ToString & "</td><td>" & dtn.Rows(k).Item(5).ToString & " </td><td>" & dtn.Rows(k).Item(0).ToString & "</td><td>" & dtn.Rows(k).Item(7).ToString & "</td></tr>"
                Next
                sb = sb & "</table>"
                myEmail.Body = myEmail.Body & sb
                myEmail.Body = myEmail.Body & "<br/>" & dt.Rows(0).Item(5).ToString   ''closing
                myEmail.IsBodyHtml = True
                myEmail.From = New System.Net.Mail.MailAddress(senderAdd, "NSN")
                mySMTPClient.Host = ConfigurationManager.AppSettings("smtp")
                Try
                    mySMTPClient.Send(myEmail)
                Catch ex As Exception
                    objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtrans'")
                End Try
            End If
        Catch ex As Exception
            objdb.ExeNonQuery("exec uspErrLog '', 'mailsending','" & ex.Message.ToString.Replace("'", "''") & "','sendmailtrans'")
        End Try
    End Sub
End Class
