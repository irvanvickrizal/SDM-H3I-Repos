Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic

Public Class COTransactionNAController
    Inherits BaseController

    Public Function GetCOTransactionNC(ByVal packageid As String, ByVal userid As Integer) As List(Of COTransactionNCInfo)
        Dim list As New List(Of COTransactionNCInfo)
        Command = New SqlCommand("uspCO_GetDocTransactionNotCompleted", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmUserId As New SqlParameter("@usrid", SqlDbType.Int)

        prmPackageId.Value = packageid
        prmUserId.Value = userid

        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmUserId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New COTransactionNCInfo
                    info.COID = Convert.ToInt32(DataReader.Item("co_id"))
                    info.SiteNo = Convert.ToString(DataReader.Item("SiteNo"))
                    info.SiteName = Convert.ToString(DataReader.Item("Sitename"))
                    info.PONO = Convert.ToString(DataReader.Item("PONO"))
                    info.PackageId = Convert.ToString(DataReader.Item("workpackageid"))
                    info.Scope = Convert.ToString(DataReader.Item("Scope"))
                    info.InitiatorUser = Convert.ToString(DataReader.Item("initiatoruser"))
                    info.WFDesc = Convert.ToString(DataReader.Item("WFDesc"))
                    info.LastExecutionDate = Convert.ToDateTime(DataReader.Item("LastExecutionDate"))
                    info.PendingRole = Convert.ToString(DataReader.Item("PendingRole"))
                    info.DocPathStatus = Convert.ToString(DataReader.Item("DocPathStatus"))
                    info.SWID = Convert.ToString(DataReader.Item("swid"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetCODocumentReplacement(ByVal coid As Int32) As CODocumentReplacementInfo
        Dim info As New CODocumentReplacementInfo
        Command = New SqlCommand("uspCO_GetDocReplacement", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmCOID As New SqlParameter("@COID", SqlDbType.BigInt)
        prmCOID.Value = coid
        Command.Parameters.Add(prmCOID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    info.CODocumentReplacementId = Convert.ToInt32(DataReader.Item("CODocRep_Id"))
                    info.COID = Convert.ToInt32(DataReader.Item("CO_ID"))
                    info.DocPath = Convert.ToString(DataReader.Item("DocPath"))
                    info.IsUploaded = Convert.ToBoolean(DataReader.Item("IsUploaded"))
                    info.LMBY = Convert.ToString(DataReader.Item("uploader"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return info
    End Function

    Public Function CheckIsReplacementCODocumentAlreadyUploaded(ByVal coid As Int32) As Boolean
        Dim isUploaded As Boolean = False
        Command = New SqlCommand("uspCO_IsDocReplacementAlreadyUploaded", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmCOID As New SqlParameter("@CO_ID", SqlDbType.BigInt)
        Dim prmIsUploaded As New SqlParameter("@isUploaded", SqlDbType.Bit)

        prmCOID.Value = coid
        prmIsUploaded.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmCOID)
        Command.Parameters.Add(prmIsUploaded)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            isUploaded = Convert.ToBoolean(prmIsUploaded.Value)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return isUploaded
    End Function

    Public Function CODocumentReplacement_IU(ByVal info As CODocumentReplacementInfo) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspCO_CODocumentReplacement_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmCOID As New SqlParameter("@coid", SqlDbType.BigInt)
        Dim prmDocPath As New SqlParameter("@docpath", SqlDbType.VarChar, 2000)
        Dim prmIsUploaded As New SqlParameter("@isUploaded", SqlDbType.Bit)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.Int)

        prmCOID.Value = info.COID
        prmDocPath.Value = info.DocPath
        prmIsUploaded.Value = info.IsUploaded
        prmLMBY.Value = info.LMBY

        Command.Parameters.Add(prmCOID)
        Command.Parameters.Add(prmDocPath)
        Command.Parameters.Add(prmIsUploaded)
        Command.Parameters.Add(prmLMBY)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
            isSucceed = False
        Finally
            Connection.Close()
        End Try

        Return isSucceed
    End Function

    Public Function GetCOTransaction(ByVal coid As Int32) As List(Of CRFramework.COWFTransactionInfo)
        Dim list As New List(Of CRFramework.COWFTransactionInfo)
        Command = New SqlCommand("uspCO_GetTransactionByCOID", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmCOID As New SqlParameter("@COID", SqlDbType.BigInt)
        prmCOID.Value = coid
        Command.Parameters.Add(prmCOID)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New CRFramework.COWFTransactionInfo
                    info.SNO = Convert.ToInt32(DataReader.Item("sno"))
                    info.DocId = Integer.Parse(DataReader.Item("doc_id").ToString())
                    info.WFID = Integer.Parse(DataReader.Item("WFID").ToString())
                    info.TSKID = Integer.Parse(DataReader.Item("Tsk_Id").ToString())
                    info.UGPID = Integer.Parse(DataReader.Item("UGP_Id").ToString())
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("StartDateTime"))) Then
                        info.StartTime = Convert.ToDateTime(DataReader.Item("StartDateTime"))
                    End If
                    If Not String.IsNullOrEmpty(Convert.ToString(DataReader.Item("EndDateTime"))) Then
                        info.EndTime = Convert.ToDateTime(DataReader.Item("EndDateTime"))
                    End If
                    info.RoleId = Integer.Parse(DataReader.Item("RoleID").ToString())
                    info.RStatus = Integer.Parse(DataReader.Item("RStatus").ToString())
                    info.Status = Integer.Parse(DataReader.Item("Status").ToString())
                    info.LMBY = Convert.ToString(DataReader.Item("executionuser"))
                    info.LMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetCOTransaction_DS(ByVal coid As Int32) As DataSet
        Dim ds As New DataSet
        Command = New SqlCommand("uspCO_GetTransactionByCOID", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmCOID As New SqlParameter("@COID", SqlDbType.BigInt)
        prmCOID.Value = coid
        Command.Parameters.Add(prmCOID)

        Try
            Connection.Open()
            Dim adapter As New SqlDataAdapter(Command)
            adapter.Fill(ds)
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return ds
    End Function

    Public Function GetUserByRoleID_Location(ByVal roleid As Integer, ByVal packageid As String) As List(Of UserProfile)
        Dim list As New List(Of UserProfile)
        Command = New SqlCommand("uspCO_GetUserByRole_Location", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmPackageId As New SqlParameter("@packageid", SqlDbType.VarChar, 50)
        Dim prmRoleId As New SqlParameter("@RoleID", SqlDbType.Int)

        prmPackageId.Value = packageid
        prmRoleId.Value = roleid

        Command.Parameters.Add(prmPackageId)
        Command.Parameters.Add(prmRoleId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read
                    Dim info As New UserProfile
                    info.UserId = Integer.Parse(DataReader.Item("usr_id").ToString())
                    info.Fullname = Convert.ToString(DataReader.Item("name"))
                    info.RoleId = roleid
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Sub UpdateCOTransactionManualProcess(ByVal sno As Int32, ByVal coid As Int32, ByVal wfid As Integer, ByVal tskid As Integer, ByVal roleid As Integer, ByVal userid As Integer, ByVal enddatetime As DateTime, ByVal lmby As Integer)
        Command = New SqlCommand("uspCO_DocApproved_ManualProcess", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmSNO As New SqlParameter("@sno", SqlDbType.BigInt)
        Dim prmCOID As New SqlParameter("@coid", SqlDbType.BigInt)
        Dim prmWFID As New SqlParameter("@wfid", SqlDbType.Int)
        Dim prmTSKID As New SqlParameter("@TSKID", SqlDbType.Int)
        Dim prmRoleId As New SqlParameter("@Roleid", SqlDbType.Int)
        Dim prmUserId As New SqlParameter("@userid", SqlDbType.Int)
        Dim prmEndDateTime As New SqlParameter("@enddatetime", SqlDbType.DateTime)
        Dim prmModifiedUserId As New SqlParameter("@modifieduserid", SqlDbType.Int)

        prmSNO.Value = sno
        prmCOID.Value = coid
        prmWFID.Value = wfid
        prmTSKID.Value = tskid
        prmRoleId.Value = roleid
        prmUserId.Value = userid
        prmEndDateTime.Value = enddatetime
        prmModifiedUserId.Value = lmby

        Command.Parameters.Add(prmSNO)
        Command.Parameters.Add(prmCOID)
        Command.Parameters.Add(prmWFID)
        Command.Parameters.Add(prmTSKID)
        Command.Parameters.Add(prmRoleId)
        Command.Parameters.Add(prmUserId)
        Command.Parameters.Add(prmEndDateTime)
        Command.Parameters.Add(prmModifiedUserId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

    End Sub

End Class
