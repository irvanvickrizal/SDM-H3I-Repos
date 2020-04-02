Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Net.Mail

Public Class MailConfigController
    Inherits BaseController

    Public Function GetConfig(ByVal configname As String) As MailConfigInfo
        Dim strErrMessage As String = String.Empty
        Dim info As New MailConfigInfo
        Command = New SqlCommand("uspConfig_GetMailConfigurationBaseName", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmConfigName As New SqlParameter("@configname", SqlDbType.VarChar, 100)
        prmConfigName.Value = configname
        Command.Parameters.Add(prmConfigName)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While DataReader.Read
                    info.ConfigID = Integer.Parse(DataReader.Item("MailConfig_ID"))
                    info.ConfigName = Convert.ToString(DataReader.Item("MailConfig_Name"))
                    info.SMTP = Convert.ToString(DataReader.Item("SMTP"))
                    info.SMTPPort = Integer.Parse(DataReader.Item("SMTP_PORT"))
                    info.MailUsername = Convert.ToString(DataReader.Item("Mail_Username"))
                    info.MailPassword = Convert.ToString(DataReader.Item("Mail_Password"))
                End While
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        If Not String.IsNullOrEmpty(strErrMessage) Then
            ErrorLogInsert(10, "GetConfig", strErrMessage, "uspConfig_GetMailConfigurationBaseName")
        End If

        Return info
    End Function

    Public Function SmtpClientEBAST(ByVal myemail As MailMessage) As SmtpClient
        Dim smtpconfig As New SmtpClient
        Dim info As MailConfigInfo = GetConfig(BaseConfiguration.MAIL_PRIMARY_CONFIG)
        If info IsNot Nothing Then
            smtpconfig.Host = info.SMTP
            Dim credentials As New System.Net.NetworkCredential(info.MailUsername, info.MailPassword)
            smtpconfig.Credentials = credentials

            Try
                smtpconfig.Send(myemail)
            Catch ex As Exception
                ErrorLogInsert(10, "SmtpClientEBAST", ex.Message.ToString(), "NON-SP")
            End Try
        End If

        Return smtpconfig
    End Function

#Region "Error Log"
    Public Sub ErrorLogInsert(ByVal errcode As String, ByVal erroperation As String, ByVal errdesc As String, ByVal errsp As String)
        Command = New SqlCommand("uspErrLog", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Dim prmErrCode As New SqlParameter("@errcode", SqlDbType.Int)
        Dim prmErrOperation As New SqlParameter("@errOperation", SqlDbType.VarChar, 100)
        Dim prmErrDesc As New SqlParameter("@errdesc", SqlDbType.VarChar, 1000)
        Dim prmErrSP As New SqlParameter("@errsp", SqlDbType.VarChar, 50)

        prmErrCode.Value = errcode
        prmErrOperation.Value = erroperation
        prmErrDesc.Value = errdesc
        prmErrSP.Value = errsp

        Command.Parameters.Add(prmErrCode)
        Command.Parameters.Add(prmErrOperation)
        Command.Parameters.Add(prmErrDesc)
        Command.Parameters.Add(prmErrSP)


        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

    End Sub
#End Region

End Class
