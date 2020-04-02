Imports SAPILib
Imports System.Threading
Imports common
Public Class SAPIWrapper
    Public Shared SAPI_ENUM_DRAWING_ELEMENT_GRAPHICAL_IMAGE As Integer = &H1
    Public Shared SAPI_ENUM_DRAWING_ELEMENT_SIGNED_BY As Integer = &H2
    Public Shared SAPI_ENUM_DRAWING_ELEMENT_REASON As Integer = &H4
    Public Shared SAPI_ENUM_DRAWING_ELEMENT_TIME As Integer = &H8
    Public Shared AR_PDF_FLAG_FIELD_NAME_SET As Integer = &H80
    Public Shared objdb As New dbutil
	Public Shared paccontrol As New PACController
    'Checks if SAPI is initialized or not - for Internal Use only
    Private Shared Function isSAPIInitialized(ByVal SAPI As SAPICrypt) As Boolean

        Dim hSes As SESHandle = Nothing
        Dim rc As Integer

        rc = SAPI.HandleAcquire(hSes)
        If rc <> 0 Then Return False

        SAPI.HandleRelease(hSes)
        Return True
    End Function


    'Thread-Safe SAPIInit()
    Private Shared Sub SAPIInit()
        Dim SAPI As SAPICrypt
        Try
            SAPI = New SAPICryptClass()
        Catch ex As Exception
            Throw New Exception("The CoSign Client is not installed")
        End Try

        'If SAPI is initialized already - do nothing
        If isSAPIInitialized(SAPI) Then Return

        'Lock Mutex
        Dim SAPIInitMutex As New Mutex(False, "D54C494C-1AFB-4c00-924C-55EBCE2672CA")

        'Whait for the Mutex to be released
        SAPIInitMutex.WaitOne()

        ' if SAPIInit was called while waiting for mutex to be released - exit the function
        If (isSAPIInitialized(SAPI)) Then Return

        ' Init SAPI
        Dim rc As Integer = SAPI.Init()
        If rc <> 0 Then
            SAPIInitMutex.ReleaseMutex()    'Release Mutex
            Throw New Exception("Failed in SAPIInit (" + rc.ToString("X") + ")")
        End If

        'Release Mutex and Exit - OK
        SAPIInitMutex.ReleaseMutex()
    End Sub

    'Sign an existing field in the file
    Public Shared Function SAPI_sign_file(ByVal FileName As String, ByVal FieldName As String, ByVal User As String, ByVal Password As String, ByVal Reason As String) As String

        Return SAPI_sign_file(FileName, FieldName, User, Password, 0, 0, 0, 0, 0, False, Reason, 0, String.Empty)

    End Function

    
    Private Shared sapiSync As Object = New Object
    Public Shared Function SAPI_sign_file(ByVal FileName As String, ByVal FieldName As String, ByVal User As String, ByVal Password As String, _
        ByVal page As Integer, ByVal x As Integer, ByVal y As Integer, ByVal height As Integer, ByVal width As Integer, _
        ByVal Invisible As Boolean, ByVal Reason As String, ByVal AppearanceMask As Integer, ByVal NewFieldName As String) As String
        Dim strQ As String = ""
        strQ = FileName & "@@" & FieldName & "@@" & User & "@@" & Password & "@@" & page & "@@" & x & "@@" & y & "@@" & height & "@@" & width & "@@" & Invisible & "@@" & Reason & "@@" & AppearanceMask & "@@" & NewFieldName
        Dim isAcceptanceDateOnly As Boolean = IIf(User.ToLower().Equals("manager_npo"), True, False)


        If isAcceptanceDateOnly = False Then
            height = 50
            width = 150
        End If
        'height = 50
        'width = 150
        Try

            objdb.ExeNonQuery("exec uspErrLog '', '" & User & "','" & strQ & "','cosignallpara'")
        Catch ex As Exception
            objdb.ExeNonQuery("exec uspErrLog '', '" & ex.Message & "','" & User & "','cosignuserpwd'")
			paccontrol.ErrorLogInsert(101, "DigiSign", ex.Message.ToString(), "NON-SP")
        End Try
        SyncLock sapiSync
            Dim boolSession As Boolean = False
            Dim boolLogon As Boolean = False
            Dim boolSigField As Boolean = False

            Dim StrReturn As String = ""
            Dim rc As Integer = 0
            Dim SesHandle As New SESHandle
            Dim sf As SigFieldHandle = Nothing



            'Instantiate SAPI COM interface
            Dim SAPI As New SAPICryptClass
            Try

                'Call internal thread-safe SAPIInit
                SAPIInit()

                rc = SAPI.HandleAcquire(SesHandle)
                If rc <> 0 Then
                    StrReturn = "Failed in SAPIHandleAcquire() with rc = " + rc.ToString("X")
                    GoTo GoToLable
                End If
                boolSession = True

                'Login to CoSign
                'MICHAEL: In non-Active-Directory mode the Domain should be blank space rather than NULL (Nothing)
                rc = SAPI.Logon(SesHandle, User, " ", Password)
                If (rc <> 0) Then
                    Dim i As Integer = rc.ToString("X").IndexOf("e")
                    If i = -1 Then
                        'StrReturn = "User name or Password is wrong " + rc.ToString("X")
                        StrReturn = "Network Issue - Please Try Again" + rc.ToString("X")
                    Else
                        StrReturn = "Cosign userID or Password wrong " + rc.ToString("X")
                    End If

                    GoTo GoToLable
                End If
                boolLogon = True

                'Create new signature field
                If FieldName = Nothing Then

                    Dim SFS As New SigFieldSettingsClass
                    Dim TF As New TimeFormatClass
                    Dim Flags As Integer = 0

                    'Define name of the new signature field
                    If (NewFieldName.Length > 0) Then
                        SFS.Name = NewFieldName
                        Flags = Flags Or AR_PDF_FLAG_FIELD_NAME_SET
                    End If

                    Invisible = False
                    If Invisible Then
                        SFS.Invisible = 1
                        SFS.Page = -1
                    Else
                        'VISIBLE:
                        SFS.Invisible = 0
                        'location:
                        SFS.Page = page
                        SFS.X = x
                        SFS.Y = y
                        SFS.Height = height
                        SFS.Width = width
                        'appearance:
                        'SFS.AppearanceMask = AppearanceMask
                        'SFS.LabelsMask = AppearanceMask
                        'SFS.DependencyMode = SAPI_ENUM_DEPENDENCY_MODE.SAPI_ENUM_DEPENDENCY_MODE_INDEPENDENT
                        'SFS.SignatureType = SAPI_ENUM_SIGNATURE_TYPE.SAPI_ENUM_SIGNATURE_DIGITAL
                        'SFS.Flags = 0
                        ''time:
                        'TF.DateFormat = "dd MMM yyyy"
                        'TF.TimeFormat = "hh:mm:ss"
                        'TF.ExtTimeFormat = SAPI_ENUM_EXTENDED_TIME_FORMAT.SAPI_ENUM_EXTENDED_TIME_FORMAT_GMT
                        'SFS.TimeFormat = TF
                        If isAcceptanceDateOnly = True Then
                            SFS.Name = ""
                            'appearance:
                            SFS.AppearanceMask = SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME
                            SFS.LabelsMask = 0
                            SFS.SignatureType = SAPI_ENUM_SIGNATURE_TYPE.SAPI_ENUM_SIGNATURE_NONE
                            'time:
                            TF.DateFormat = "dd-MMMM-yyyy"
                            TF.ExtTimeFormat = SAPI_ENUM_EXTENDED_TIME_FORMAT.SAPI_ENUM_EXTENDED_TIME_FORMAT_NONE
                        Else
                            'appearance:
                            SFS.AppearanceMask = AppearanceMask
                            SFS.LabelsMask = AppearanceMask
                            SFS.SignatureType = SAPI_ENUM_SIGNATURE_TYPE.SAPI_ENUM_SIGNATURE_DIGITAL
                            'time:
                            TF.DateFormat = "dd MMM yyyy"
                            TF.TimeFormat = "hh:mm:ss"
                            TF.ExtTimeFormat = SAPI_ENUM_EXTENDED_TIME_FORMAT.SAPI_ENUM_EXTENDED_TIME_FORMAT_GMT
                        End If
                        'appearance:
                        'SFS.AppearanceMask = AppearanceMask
                        'SFS.LabelsMask = AppearanceMask
                        'SFS.DependencyMode = SAPI_ENUM_DEPENDENCY_MODE.SAPI_ENUM_DEPENDENCY_MODE_INDEPENDENT
                        'SFS.SignatureType = SAPI_ENUM_SIGNATURE_TYPE.SAPI_ENUM_SIGNATURE_DIGITAL
                        'SFS.Flags = 0
                        ''time:
                        'TF.DateFormat = "dd MMM yyyy"
                        'TF.TimeFormat = "hh:mm:ss"
                        'TF.ExtTimeFormat = SAPI_ENUM_EXTENDED_TIME_FORMAT.SAPI_ENUM_EXTENDED_TIME_FORMAT_GMT
                        'SFS.TimeFormat = TF
                        SFS.DependencyMode = SAPI_ENUM_DEPENDENCY_MODE.SAPI_ENUM_DEPENDENCY_MODE_INDEPENDENT
                        SFS.Flags = 0
                        SFS.TimeFormat = TF
                    End If

                    rc = SAPI.SignatureFieldCreate(SesHandle, SAPI_ENUM_FILE_TYPE.SAPI_ENUM_FILE_ADOBE, _
                        FileName, SFS, Flags, sf)
                    If rc <> 0 Then
                        StrReturn = "Failed to create new signature field with rc = " + rc.ToString("X")
                        GoTo GoToLable
                    End If
                    boolSigField = True
                Else
                    'Find existing signature field by name
                    Dim ctxField As New SAPIContext
                    Dim NumOfFields As Integer = 0

                    rc = SAPI.SignatureFieldEnumInit(SesHandle, ctxField, SAPI_ENUM_FILE_TYPE.SAPI_ENUM_FILE_ADOBE, _
                        FileName, 0, NumOfFields)
                    If rc <> 0 Then
                        StrReturn = "Failed to start signature field enumeration with rc = " + rc.ToString("X")
                        GoTo GoToLable
                    End If


                    Dim isFound As Boolean = False
                    Dim i As Integer

                    'MICHAEL:
                    'In VB.NET the enumeration continues until i = NumOfFields,
                    'While in C# it works until i < NumOfFields
                    For i = 0 To NumOfFields - 1

                        'Get Next field's handle
                        rc = SAPI.SignatureFieldEnumCont(SesHandle, ctxField, sf)
                        If rc <> 0 Then
                            SAPI.ContextRelease(ctxField)
                            StrReturn = "Failed in signature fields enumeration with rc = " + rc.ToString("X")
                            GoTo GoToLable
                        End If

                        'Retrieve Signature Field's info
                        Dim sfs As New SigFieldSettings
                        Dim sfi As New SigFieldInfo

                        rc = SAPI.SignatureFieldInfoGet(SesHandle, sf, sfs, sfi)
                        If rc <> 0 Then
                            SAPI.HandleRelease(sf)
                            SAPI.ContextRelease(ctxField)
                            StrReturn = "Failed to retrieve signature field details with rc = " + rc.ToString("X")
                            GoTo GoToLable

                        End If


                        'MICHAEL:
                        'The check if field is signed should be performed for the 
                        'relevant field only

                        'Check if the relevant field was found
                        If sfs.Name = FieldName Then
                            'Check if the field already signed
                            If sfi.IsSigned <> 0 Then
                                SAPI.HandleRelease(sf)
                                SAPI.ContextRelease(ctxField)
                                StrReturn = "The Signature field: " + FieldName + " is signed already"
                                GoTo GoToLable
                            Else
                                SAPI.ContextRelease(ctxField)
                                boolSigField = True
                                isFound = True
                                Exit For
                            End If
                        End If

                        'Release handle of irrelevant signature field
                        SAPI.HandleRelease(sf)

                    Next

                    If Not isFound Then
                        SAPI.ContextRelease(ctxField)
                        StrReturn = "The file doesn't contain any signature field named: " + FieldName
                        GoTo GoToLable
                    End If


                End If

                'Define the Reason
                rc = SAPI.ConfigurationValueSet(SesHandle, SAPI_ENUM_CONF_ID.SAPI_ENUM_CONF_ID_REASON, _
                    SAPI_ENUM_DATA_TYPE.SAPI_ENUM_DATA_TYPE_STR, Reason, 1)
                If rc <> 0 Then
                    StrReturn = "Failed in SAPIConfigurationValueSet with rc = " + rc.ToString("X")
                    GoTo GoToLable
                End If

                rc = SAPI.SignatureFieldSign(SesHandle, sf, 0)
                If rc <> 0 Then
                    StrReturn = "Failed to Sign signature field with rc = " + rc.ToString("X")
                    GoTo GoToLable
                Else
                    StrReturn = "success"
                End If


            Catch ex As Exception
                StrReturn = "The SAPI COM is not registered. Please reinstall your CoSign Client software" + Environment.NewLine + "Details: " + ex.Message
                GoTo GoToLable

            End Try
GoToLable:
            'MICHAEL:
            'Here is an additional problem
            'The session handle should be released after logging-off and not before

            If (boolSigField) Then SAPI.HandleRelease(sf)
            If (boolLogon) Then SAPI.Logoff(SesHandle)
            If (boolSession) Then SAPI.HandleRelease(SesHandle)


            Return StrReturn

        End SyncLock
    End Function

    Public Shared Function SAPI_sign_file_CR(ByVal FileName As String, ByVal FieldName As String, ByVal User As String, ByVal Password As String, ByVal Reason As String) As String
        Return SAPI_sign_file_CR(FileName, FieldName, User, Password, 0, 0, 0, 0, 0, False, Reason, 0, String.Empty)
    End Function

    Public Shared Function SAPI_sign_file_CR(ByVal FileName As String, ByVal FieldName As String, ByVal User As String, ByVal Password As String, _
        ByVal page As Integer, ByVal x As Integer, ByVal y As Integer, ByVal height As Integer, ByVal width As Integer, _
        ByVal Invisible As Boolean, ByVal Reason As String, ByVal AppearanceMask As Integer, ByVal NewFieldName As String) As String
        Dim strQ As String = ""
        strQ = FileName & "@@" & FieldName & "@@" & User & "@@" & Password & "@@" & page & "@@" & x & "@@" & y & "@@" & height & "@@" & width & "@@" & Invisible & "@@" & Reason & "@@" & AppearanceMask & "@@" & NewFieldName
        Dim isAcceptanceDateOnly As Boolean = IIf(User.ToLower().Equals("manager_npo"), True, False)

        If isAcceptanceDateOnly = False Then
            height = 40
            width = 100
        End If


        Try
            objdb.ExeNonQuery("exec uspErrLog '', '" & User & "','" & strQ & "','cosignallpara'")
        Catch ex As Exception
            objdb.ExeNonQuery("exec uspErrLog '', '" & ex.Message & "','" & User & "','cosignuserpwd'")
        End Try
        SyncLock sapiSync
            Dim boolSession As Boolean = False
            Dim boolLogon As Boolean = False
            Dim boolSigField As Boolean = False

            Dim StrReturn As String = ""
            Dim rc As Integer = 0
            Dim SesHandle As New SESHandle
            Dim sf As SigFieldHandle = Nothing
            'Instantiate SAPI COM interface
            Dim SAPI As New SAPICryptClass
            Try

                'Call internal thread-safe SAPIInit
                SAPIInit()

                rc = SAPI.HandleAcquire(SesHandle)
                If rc <> 0 Then
                    StrReturn = "Failed in SAPIHandleAcquire() with rc = " + rc.ToString("X")
                    GoTo GoToLable
                End If
                boolSession = True

                'Login to CoSign
                'MICHAEL: In non-Active-Directory mode the Domain should be blank space rather than NULL (Nothing)
                rc = SAPI.Logon(SesHandle, User, " ", Password)
                If (rc <> 0) Then
                    Dim i As Integer = rc.ToString("X").IndexOf("e")
                    If i = -1 Then
                        'StrReturn = "User name or Password is wrong " + rc.ToString("X")
                        StrReturn = "Network Issue - Please Try Again" + rc.ToString("X")
                    Else
                        StrReturn = "Cosign userID or Password wrong " + rc.ToString("X")
                    End If

                    GoTo GoToLable
                End If
                boolLogon = True

                'Create new signature field
                If FieldName = Nothing Then

                    Dim SFS As New SigFieldSettingsClass
                    Dim TF As New TimeFormatClass
                    Dim Flags As Integer = 0

                    'Define name of the new signature field
                    If (NewFieldName.Length > 0) Then
                        SFS.Name = NewFieldName
                        Flags = Flags Or AR_PDF_FLAG_FIELD_NAME_SET
                    End If

                    Invisible = False
                    If Invisible Then
                        SFS.Invisible = 1
                        SFS.Page = -1
                    Else
                        'VISIBLE:
                        SFS.Invisible = 0
                        'location:
                        SFS.Page = page
                        SFS.X = x
                        SFS.Y = y
                        SFS.Height = height
                        SFS.Width = width

                        If isAcceptanceDateOnly = True Then
                            SFS.Name = ""
                            'appearance:
                            SFS.AppearanceMask = SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME
                            SFS.LabelsMask = 0
                            SFS.SignatureType = SAPI_ENUM_SIGNATURE_TYPE.SAPI_ENUM_SIGNATURE_NONE
                            'time:
                            TF.DateFormat = "dd-MMMM-yyyy"
                            TF.ExtTimeFormat = SAPI_ENUM_EXTENDED_TIME_FORMAT.SAPI_ENUM_EXTENDED_TIME_FORMAT_NONE
                        Else
                            'appearance:
                            SFS.AppearanceMask = AppearanceMask
                            SFS.LabelsMask = AppearanceMask
                            SFS.SignatureType = SAPI_ENUM_SIGNATURE_TYPE.SAPI_ENUM_SIGNATURE_DIGITAL
                            'time:
                            TF.DateFormat = "dd MMM yyyy"
                            TF.TimeFormat = "hh:mm:ss"
                            TF.ExtTimeFormat = SAPI_ENUM_EXTENDED_TIME_FORMAT.SAPI_ENUM_EXTENDED_TIME_FORMAT_GMT
                        End If

                        SFS.DependencyMode = SAPI_ENUM_DEPENDENCY_MODE.SAPI_ENUM_DEPENDENCY_MODE_INDEPENDENT
                        SFS.Flags = 0
                        SFS.TimeFormat = TF

                    End If

                    rc = SAPI.SignatureFieldCreate(SesHandle, SAPI_ENUM_FILE_TYPE.SAPI_ENUM_FILE_ADOBE, _
                        FileName, SFS, Flags, sf)
                    If rc <> 0 Then
                        StrReturn = "Failed to create new signature field with rc = " + rc.ToString("X")
                        GoTo GoToLable
                    End If
                    boolSigField = True
                Else
                    'Find existing signature field by name
                    Dim ctxField As New SAPIContext
                    Dim NumOfFields As Integer = 0

                    rc = SAPI.SignatureFieldEnumInit(SesHandle, ctxField, SAPI_ENUM_FILE_TYPE.SAPI_ENUM_FILE_ADOBE, _
                        FileName, 0, NumOfFields)
                    If rc <> 0 Then
                        StrReturn = "Failed to start signature field enumeration with rc = " + rc.ToString("X")
                        GoTo GoToLable
                    End If


                    Dim isFound As Boolean = False
                    Dim i As Integer

                    'MICHAEL:
                    'In VB.NET the enumeration continues until i = NumOfFields,
                    'While in C# it works until i < NumOfFields
                    For i = 0 To NumOfFields - 1

                        'Get Next field's handle
                        rc = SAPI.SignatureFieldEnumCont(SesHandle, ctxField, sf)
                        If rc <> 0 Then
                            SAPI.ContextRelease(ctxField)
                            StrReturn = "Failed in signature fields enumeration with rc = " + rc.ToString("X")
                            GoTo GoToLable
                        End If

                        'Retrieve Signature Field's info
                        Dim sfs As New SigFieldSettings
                        Dim sfi As New SigFieldInfo

                        rc = SAPI.SignatureFieldInfoGet(SesHandle, sf, sfs, sfi)
                        If rc <> 0 Then
                            SAPI.HandleRelease(sf)
                            SAPI.ContextRelease(ctxField)
                            StrReturn = "Failed to retrieve signature field details with rc = " + rc.ToString("X")
                            GoTo GoToLable

                        End If


                        'MICHAEL:
                        'The check if field is signed should be performed for the 
                        'relevant field only

                        'Check if the relevant field was found
                        If sfs.Name = FieldName Then
                            'Check if the field already signed
                            If sfi.IsSigned <> 0 Then
                                SAPI.HandleRelease(sf)
                                SAPI.ContextRelease(ctxField)
                                StrReturn = "The Signature field: " + FieldName + " is signed already"
                                GoTo GoToLable
                            Else
                                SAPI.ContextRelease(ctxField)
                                boolSigField = True
                                isFound = True
                                Exit For
                            End If
                        End If

                        'Release handle of irrelevant signature field
                        SAPI.HandleRelease(sf)

                    Next

                    If Not isFound Then
                        SAPI.ContextRelease(ctxField)
                        StrReturn = "The file doesn't contain any signature field named: " + FieldName
                        GoTo GoToLable
                    End If


                End If

                'Define the Reason
                rc = SAPI.ConfigurationValueSet(SesHandle, SAPI_ENUM_CONF_ID.SAPI_ENUM_CONF_ID_REASON, _
                    SAPI_ENUM_DATA_TYPE.SAPI_ENUM_DATA_TYPE_STR, Reason, 1)
                If rc <> 0 Then
                    StrReturn = "Failed in SAPIConfigurationValueSet with rc = " + rc.ToString("X")
                    GoTo GoToLable
                End If

                rc = SAPI.SignatureFieldSign(SesHandle, sf, 0)
                If rc <> 0 Then
                    StrReturn = "Failed to Sign signature field with rc = " + rc.ToString("X")
                    GoTo GoToLable
                Else
                    StrReturn = "success"
                End If


            Catch ex As Exception
                StrReturn = "The SAPI COM is not registered. Please reinstall your CoSign Client software" + Environment.NewLine + "Details: " + ex.Message
                GoTo GoToLable

            End Try
GoToLable:
            'MICHAEL:
            'Here is an additional problem
            'The session handle should be released after logging-off and not before

            If (boolSigField) Then SAPI.HandleRelease(sf)
            If (boolLogon) Then SAPI.Logoff(SesHandle)
            If (boolSession) Then SAPI.HandleRelease(SesHandle)


            Return StrReturn

        End SyncLock
    End Function
End Class
