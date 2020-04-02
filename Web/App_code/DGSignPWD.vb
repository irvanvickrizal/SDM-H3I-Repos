Imports System
Imports System.Collections.Generic
Imports System.Text
Imports SAPIUMCOMLib
Imports System.IO
Public Class DGSignPWD
    'Static Constructor 
    Shared Sub New()
        Dim mySAPIUM As SAPIUM = Nothing
        Try
            mySAPIUM = New SAPIUMClass()
        Catch ex As Exception
            Throw New Exception(("SAPI COM component is not registered!" & vbLf & "Make sure you have CoSign Client software installed!" & vbLf & vbLf & "Windows Error:" & vbLf) + ex.Message)
        End Try

        Dim rc As Integer = mySAPIUM.Init()
        If rc <> 0 Then
            Throw New Exception("Failed in SAPIUMInit() (" & rc.ToString("X") & ")")
        End If
    End Sub


    Public Structure UserInfo
        Public LoginName As String
        Public CommonName As String
        Public Email As String
        Public Password As String
        Public ImagesFolder As String


        Public Sub New(ByVal LoginName As String, ByVal Password As String, ByVal CN As String, ByVal Email As String, ByVal ImagesFolder As String)
            Me.New(LoginName, Password, CN, Email)
            Me.ImagesFolder = ImagesFolder
        End Sub

        Public Sub New(ByVal LoginName As String, ByVal Password As String, ByVal CN As String, ByVal Email As String)
            Me.LoginName = LoginName
            Me.CommonName = CN
            Me.Password = Password
            Me.Email = Email
            Me.ImagesFolder = Nothing
        End Sub
    End Structure

    Public Shared Sub CreateUser(ByVal AdminUsername As String, ByVal AdminPassword As String, ByVal Domain As String, ByVal ui As UserInfo)

        Dim mySAPIUM As SAPIUM = New SAPIUMClass()
        Dim hSes As UMSESHandle = Nothing

        Dim rc As Integer = mySAPIUM.Init()
        If rc <> 0 Then
            Throw New Exception("Failed in SAPIUMInit() (" & rc.ToString("X") & ")")
        End If

        rc = mySAPIUM.HandleAcquire(hSes)
        If rc <> 0 Then
            Throw New Exception("Failed in SAPIUMHandleAcquire() (" & rc.ToString("X") & ")")
        End If


        'Login 
        rc = mySAPIUM.Logon(hSes, AdminUsername, Domain, AdminPassword)
        If rc <> 0 Then
            mySAPIUM.HandleRelease(hSes)
            Throw New Exception("Failed in SAPIUMLogon() (" & rc.ToString("X") & ")")
        End If

        'Create User 
        rc = mySAPIUM.UserAdd(hSes, ui.LoginName, ui.CommonName, ui.Email, ui.Password, &H1)
        If rc <> 0 Then
            mySAPIUM.Logoff(hSes)
            mySAPIUM.HandleRelease(hSes)
            Throw New Exception("Failed in SAPIUMUserAdd() (" & rc.ToString("X") & ")")
        End If

        mySAPIUM.Logoff(hSes)

        mySAPIUM.HandleRelease(hSes)
    End Sub



    'Overwrites the existing password with the new one 
    Public Shared Sub SetPassword(ByVal AdminUsername As String, ByVal AdminPassword As String, ByVal Domain As String, ByVal ui As UserInfo)

        Dim mySAPIUM As SAPIUM = New SAPIUMClass()
        Dim hSes As UMSESHandle = Nothing

        Dim rc As Integer = mySAPIUM.HandleAcquire(hSes)
        If rc <> 0 Then
            Throw New Exception("Failed in SAPIUMHandleAcquire() (" & rc.ToString("X") & ")")
        End If


        'Login 
        rc = mySAPIUM.Logon(hSes, AdminUsername, Domain, AdminPassword)
        If rc <> 0 Then
            mySAPIUM.HandleRelease(hSes)
            Throw New Exception("Failed in SAPIUMLogon() (" & rc.ToString("X") & ")")
        End If

        'Reset Password 
        rc = mySAPIUM.CredentialSet(hSes, ui.LoginName, ui.Password)
        If rc <> 0 Then
            mySAPIUM.Logoff(hSes)
            mySAPIUM.HandleRelease(hSes)
            Throw New Exception("Failed in SAPIUMCredentialSet() (" & rc.ToString("X") & ")")
        End If

        mySAPIUM.Logoff(hSes)

        mySAPIUM.HandleRelease(hSes)
    End Sub


    Public Shared Sub DeleteUser(ByVal AdminUsername As String, ByVal Domain As String, ByVal AdminPassword As String, ByVal ui As UserInfo)

        Dim mySAPIUM As SAPIUM = New SAPIUMClass()
        Dim hSes As UMSESHandle = Nothing

        Dim rc As Integer = mySAPIUM.HandleAcquire(hSes)
        If rc <> 0 Then
            Throw New Exception("Failed in SAPIUMHandleAcquire() (" & rc.ToString("X") & ")")
        End If


        'Login 
        rc = mySAPIUM.Logon(hSes, AdminUsername, Domain, AdminPassword)
        If rc <> 0 Then
            mySAPIUM.HandleRelease(hSes)
            Throw New Exception("Failed in SAPIUMLogon() (" & rc.ToString("X") & ")")
        End If

        'Delete User 
        rc = mySAPIUM.UserDelete(hSes, ui.LoginName)
        If rc <> 0 Then
            mySAPIUM.Logoff(hSes)
            mySAPIUM.HandleRelease(hSes)
            Throw New Exception("Failed in SAPIUMUserDelete() (" & rc.ToString("X") & ")")
        End If
        mySAPIUM.Logoff(hSes)
        mySAPIUM.HandleRelease(hSes)
    End Sub
End Class