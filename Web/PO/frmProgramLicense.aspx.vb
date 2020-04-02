Imports BusinessLogic
Imports Common
Imports System.Data
Imports System.Web.UI
Imports Entities
Imports System.Net.Mail
Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.Security.Cryptography

Partial Class frmProgramLicense
    Inherits System.Web.UI.Page
    Dim objUtil As New DBUtil
    Dim sconid As String
    Dim licenseno As String
    Private key() As Byte = {}
    Private IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then
            Response.Redirect("~/SessionTimeout.aspx")
        End If
    End Sub

    Protected Sub btnLicense_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLicense.Click
        If Not Session("User_Login") = "" Then
            sconid = objUtil.ExeQueryScalarString("select scon_id from ti2g..subcon where scon_id=(select distinct srcid from ti2g..ebastusers_1 where usrlogin='" & Session("User_Login") & "')")
            If Not sconid = "" Then
                licenseno = objUtil.ExeQueryScalarString("select licenseno from ti2g..easypdflicense where sconid=" & sconid)
                If licenseno = "" Then
                    licenseno = objUtil.ExeQueryScalarString("select top(1) licenseno from ti2g..easypdflicense where sconid is null")
                    objUtil.ExeQuery("update ti2g..easypdflicense set sconid=" & sconid & " where licenseno='" & licenseno & "'")
                    tdLicense1.Visible = True
                    tdLicense2.Visible = True
                    tdLicense2.InnerHtml = ""
                    tdLicense2.InnerHtml = licenseno
                Else
                    tdLicense1.Visible = True
                    tdLicense2.Visible = True
                    tdLicense1.InnerHtml = ""
                    tdLicense1.InnerHtml = "You are entitled only for 1 license, for additional license please contact eBAST Administator"
                    tdLicense2.InnerHtml = ""
                    tdLicense2.InnerHtml = "You have downloaded the following license no : " & licenseno
                End If
            End If
        End If
    End Sub
End Class