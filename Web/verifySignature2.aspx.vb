Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Common
Imports Microsoft.VisualBasic
Imports Entities
Imports BusinessLogic
Imports verifySignature
Partial Class verifySignature2
    Inherits System.Web.UI.Page
    Dim objetsitedoc As New ETSiteDoc
    Dim objbositedoc As New BOSiteDocs
    Dim objBOM As New BOMailReport
    Dim objET As New ETAuditTrail
    Dim objdb As New DBUtil
    Dim dtn, dt, dt1, dt2, dtr As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim userid, username, docpath, url, sdate As String
        Dim records As Integer
        If Request.QueryString("rdx") = "-1" Then
            Response.Write("Signature Verification Completed")
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "CloseWindow();", True)
        Else
            Response.Write(Request.QueryString("rdx") & "<br><br>")
        End If
        If Request.QueryString("rdx") = "0" Then
            'objdb.ExeQuery("exec uspAutoInsertMissingWFTransaction") 'auto insert missing wftransaction
            objdb.ExeQuery("delete from ReminderUsers") 'clearing
            objdb.ExeQuery("insert into ReminderUsers(userid, username, docpath, taskid) exec uspGetUsersDocuments")
            records = objdb.ExeQueryScalar("select count(*) from ReminderUsers")
            dt1 = objdb.ExeQueryDT("select top 1 * from ReminderUsers order by userid", "maildt")
            If dt1.Rows.Count() > 0 Then
                userid = dt1.Rows(0).Item("userid").ToString
                username = dt1.Rows(0).Item("username").ToString.Replace(" ", "^").Replace("\", "|")
                docpath = dt1.Rows(0).Item("docpath").ToString.Replace(" ", "^").Replace("\", "|")
                url = "window.location='verifySignature1.aspx?rdx=" & userid & "&name=" & username & "&docpath=" & docpath & "&records=" & records.ToString
                Response.Write("<script language='JavaScript'>" & url & "';</script>")
            Else
                Response.Write("No Records")
            End If
        End If
        If Request.QueryString("rdx") <> "" And Request.QueryString("rdx") <> "-1" Then
            '--process to verifying the signatures
            If Request.QueryString("rdx") <> "0" Then
                userid = Request.QueryString("rdx").Replace("^", " ").Replace("|", "\")
                username = Request.QueryString("name").Replace("^", " ").Replace("|", "\")
                docpath = ConfigurationManager.AppSettings("FPath") & Request.QueryString("docpath").Replace("^", " ").Replace("|", "\")
                records = objdb.ExeQueryScalar("select count(*) from ReminderUsers")
                Response.Write("No of Rows: " & Request.QueryString("records") & " counting " & records & "<br>")
                Response.Write("Workflow Transaction Sno: " & userid & "<br>")
                Response.Write("Subject Name: " & username & "<br>")
                Response.Write("Document Path: " & docpath & "<br>")
                Try
                    If File.Exists(docpath) Then
                        Dim fs As New FileStream(docpath, FileMode.Open, FileAccess.Read)
                        Dim ds As New digisign("")
                        ds.setPdfPathIO(fs)
                        sdate = ds.getSignatureShortDate2(username)
                        objdb.ExeQuery("exec uspSetSignatureDate " & Request.QueryString("rdx") & ",'" & sdate & "'")
                        fs.Close()
                    End If
                Catch ex As Exception
                    Response.Write("Error: " & ex.Message & "<br>")
                End Try
                Response.Write("Signed Date: " & sdate & "<br>")
            End If
            '--
            objdb.ExeQuery("delete from ReminderUsers where userid=" & Request.QueryString("rdx"))
            dt1 = objdb.ExeQueryDT("select top 1 * from ReminderUsers order by userid", "userdt")
            If dt1.Rows.Count > 0 Then
                userid = dt1.Rows(0).Item("userid").ToString
                username = dt1.Rows(0).Item("username").ToString.Replace(" ", "^").Replace("\", "|")
                docpath = dt1.Rows(0).Item("docpath").ToString.Replace(" ", "^").Replace("\", "|")
                url = "window.location='verifySignature1.aspx?rdx=" & userid & "&name=" & username & "&docpath=" & docpath & "&records=" & Request.QueryString("records")
                Response.Write("<script language='JavaScript'>" & url & "';</script>")
            Else
                url = "window.location='verifySignature1.aspx?rdx=-1"
                Response.Write("<script language='JavaScript'>" & url & "';</script>")
            End If
        End If
    End Sub
End Class
