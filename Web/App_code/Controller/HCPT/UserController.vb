Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class UserController
    Inherits BaseController

    Public Function GetUserLD(ByVal userid As Integer) As UserProfileInfo
        Dim info As New UserProfileInfo
        Dim strErrMessage As String = String.Empty
        Command = New SqlCommand("HCPT_uspGeneral_GetUser", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmUserID As New SqlParameter("@usrid", SqlDbType.Int)
        prmUserID.Value = userid
        Command.Parameters.Add(prmUserID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.UserId = Integer.Parse(DataReader.Item("usr_id"))
                    info.Fullname = Convert.ToString(DataReader.Item("Name"))
                    info.Email = Convert.ToString(DataReader.Item("Email"))
                    info.PhoneNo = Convert.ToString(DataReader.Item("PhoneNo"))
                    info.SignTitle = Convert.ToString(DataReader.Item("SignTitle"))
                End While
            Else
                info = Nothing
            End If
        Catch ex As Exception
            strErrMessage = ex.Message.ToString()
        Finally
            Connection.Close()
        End Try

        Return info
    End Function
End Class
