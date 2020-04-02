Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class MasterListController
    Inherits BaseController



#Region "Master List"
    Public Function GetParentLIst(ByVal isDeleted As Boolean) As List(Of ScopeInfo)
        Dim list As New List(Of ScopeInfo)

        Command = New SqlCommand("uspScope_GetScopes", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)
        prmIsDeleted.Value = isDeleted
        Command.Parameters.Add(prmIsDeleted)
        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If (DataReader.HasRows) Then
                While (DataReader.Read())
                    Dim info As New ScopeInfo
                    info.ScopeId = DataReader.Item("GScope_Id")
                    info.ScopeName = DataReader.Item("Scope_Name")
                    info.ScopeDesc = Convert.ToString(DataReader.Item("Description"))
                    info.ScopeLMBY = Convert.ToString(DataReader.Item("LMBY"))
                    info.ScopeLMDT = Convert.ToDateTime(DataReader.Item("LMDT"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function
    Public Function GetAllDetailScopes(ByVal isDeleted As Boolean, ByVal scopeid As Integer) As List(Of DetailScopeInfo)
        Dim list As New List(Of DetailScopeInfo)
        Command = New SqlCommand("uspScope_GetAllDetailScopes", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)
        Dim prmScopeId As New SqlParameter("@gscopeid", SqlDbType.Int)
        prmScopeId.Value = scopeid
        prmIsDeleted.Value = isDeleted
        Command.Parameters.Add(prmIsDeleted)
        Command.Parameters.Add(prmScopeId)


        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New DetailScopeInfo
                    info.DScopeId = Integer.Parse(DataReader.Item("DScope_Id").ToString())
                    info.DScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.DScopeDesc = Convert.ToString(DataReader.Item("DScope_Description"))
                    info.DScopeLMBY = Convert.ToString(DataReader.Item("DScope_LMBY"))
                    info.DScopeLMDT = Convert.ToDateTime(DataReader.Item("DScope_LMDT"))
                    info.ScopeId = Integer.Parse(DataReader.Item("GScope_Id").ToString())
                    info.ScopeName = Convert.ToString(DataReader.Item("Scope_Name"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetAllMasterList(ByVal isDeleted As Boolean, ByVal scopeid As Integer) As DataSet
        Dim ds As New DataSet
        Command = New SqlCommand("uspScope_GetAllDetailScopes", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)
        Dim prmScopeId As New SqlParameter("@gscopeid", SqlDbType.Int)
        prmIsDeleted.Value = isDeleted
        prmScopeId.Value = scopeid
        Command.Parameters.Add(prmIsDeleted)
        Command.Parameters.Add(prmScopeId)

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

    Public Sub MasterListIU(ByVal info As MasterListInfo)
        Command = New SqlCommand("uspMasterList_IU_New", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmListId As New SqlParameter("@ListId", SqlDbType.Int)
        Dim prmSN As New SqlParameter("@SN", SqlDbType.VarChar, 10)
        Dim prmDocname As New SqlParameter("@Docname", SqlDbType.VarChar, 250)
        Dim prmSerialOrder As New SqlParameter("@SerialOrder", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@LMBY", SqlDbType.VarChar, 250)
        Dim prmParentId As New SqlParameter("@ParentId", SqlDbType.Int)

        prmListId.Value = info.listId
        prmSN.Value = info.SN
        prmDocname.Value = info.DOCName
        prmSerialOrder.Value = info.SerialOrder
        prmLMBY.Value = info.LIstLMBY
        prmParentId.Value = info.ParentId

        Command.Parameters.Add(prmListId)
        Command.Parameters.Add(prmSN)
        Command.Parameters.Add(prmDocname)
        Command.Parameters.Add(prmSerialOrder)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmParentId)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try


    End Sub

    Public Sub DeleteMasterLIstTemp(ByVal ListId As Integer, ByVal lmby As String)
        Command = New SqlCommand("uspMasterLIst_Delete_Temp", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmListId As New SqlParameter("@ListId", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@LMBY", SqlDbType.VarChar, 250)

        prmListId.Value = ListId
        prmLMBY.Value = lmby

        Command.Parameters.Add(prmListId)
        Command.Parameters.Add(prmLMBY)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
    End Sub
#End Region

#Region "Master BAUT SIT"
    Public Function GetListDetailScopesBaseBAUTSIT(ByVal isDeleted As Boolean, ByVal scopeid As Integer) As List(Of DetailScopeInfo)
        Dim list As New List(Of DetailScopeInfo)
        Command = New SqlCommand("uspScope_GetListDetailScopeBaseBAUTSIT", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)
        Dim prmScopeId As New SqlParameter("@gscopeid", SqlDbType.Int)
        prmScopeId.Value = scopeid
        prmIsDeleted.Value = isDeleted
        Command.Parameters.Add(prmIsDeleted)
        Command.Parameters.Add(prmScopeId)


        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New DetailScopeInfo
                    info.DScopeId = Integer.Parse(DataReader.Item("DScope_Id").ToString())
                    info.DScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.DScopeDesc = Convert.ToString(DataReader.Item("DScope_Description"))
                    info.DScopeLMBY = Convert.ToString(DataReader.Item("DScope_LMBY"))
                    info.DScopeLMDT = Convert.ToDateTime(DataReader.Item("DScope_LMDT"))
                    info.ScopeId = Integer.Parse(DataReader.Item("GScope_Id").ToString())
                    info.ScopeName = Convert.ToString(DataReader.Item("Scope_Name"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetMasterBAUTSIT_DS(ByVal dscopeid As Integer) As DataSet
        Dim ds As New DataSet
        Command = New SqlCommand("uspScope_GetBAUTSIT", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDScopeId As New SqlParameter("@dscopeid", SqlDbType.Int)
        prmDScopeId.Value = dscopeid
        Command.Parameters.Add(prmDScopeId)

        Try
            Connection.Open()
            Dim adapter As New SqlDataAdapter(Command)
            adapter.Fill(ds)
        Catch ex As Exception
            ds = Nothing
        Finally
            Connection.Close()
        End Try
        Return ds
    End Function

    Public Function MasterBAUTSIT_IU(ByVal info As DetailScopeInfo) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspScope_SITBAUT_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmMSITID As New SqlParameter("@msitid", SqlDbType.Int)
        Dim prmApproveDocId As New SqlParameter("@approvedocid", SqlDbType.Int)
        Dim prmDScopeId As New SqlParameter("@dscopeid", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 300)

        prmMSITID.Value = info.ScopeBAUTSITInfo.MSIT
        prmApproveDocId.Value = info.ScopeBAUTSITInfo.DocDependencyInfo.DocId
        prmDScopeId.Value = info.DScopeId
        prmLMBY.Value = info.ScopeBAUTSITInfo.CMAInfo.LMBY


        Command.Parameters.Add(prmMSITID)
        Command.Parameters.Add(prmApproveDocId)
        Command.Parameters.Add(prmDScopeId)
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

    Public Sub MasterBAUTSIT_D(ByVal msitid As Integer)
        Command = New SqlCommand("uspScope_SITBAUT_D", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmMSITID As New SqlParameter("@msitid", SqlDbType.Int)
        prmMSITID.Value = msitid
        Command.Parameters.Add(prmMSITID)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try
    End Sub
#End Region

#Region "Master LT Partner"
    Public Function GetListDetailScopesBaseLTPartner(ByVal isDeleted As Boolean, ByVal scopeid As Integer, ByVal activityid As Integer) As List(Of DetailScopeInfo)
        Dim list As New List(Of DetailScopeInfo)
        Command = New SqlCommand("uspScope_GetListDetailScopeBaseLTPartner", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)
        Dim prmScopeId As New SqlParameter("@gscopeid", SqlDbType.Int)
        Dim prmActivityId As New SqlParameter("@activityid", SqlDbType.Int)
        prmScopeId.Value = scopeid
        prmIsDeleted.Value = isDeleted
        prmActivityId.Value = activityid
        Command.Parameters.Add(prmIsDeleted)
        Command.Parameters.Add(prmScopeId)
        Command.Parameters.Add(prmActivityId)

        Try
            Connection.Open()
            DataReader = Command.ExecuteReader()
            If DataReader.HasRows Then
                While DataReader.Read()
                    Dim info As New DetailScopeInfo
                    info.DScopeId = Integer.Parse(DataReader.Item("DScope_Id").ToString())
                    info.DScopeName = Convert.ToString(DataReader.Item("DScope_Name"))
                    info.DScopeDesc = Convert.ToString(DataReader.Item("DScope_Description"))
                    info.DScopeLMBY = Convert.ToString(DataReader.Item("DScope_LMBY"))
                    info.DScopeLMDT = Convert.ToDateTime(DataReader.Item("DScope_LMDT"))
                    info.ScopeId = Integer.Parse(DataReader.Item("GScope_Id").ToString())
                    info.ScopeName = Convert.ToString(DataReader.Item("Scope_Name"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return list
    End Function

    Public Function GetLTPartner_DS(ByVal dscopeid As Integer) As DataSet
        Dim ds As New DataSet
        Command = New SqlCommand("uspScope_GetLTPartners", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDScopeId As New SqlParameter("@dscopeid", SqlDbType.Int)
        prmDScopeId.Value = dscopeid
        Command.Parameters.Add(prmDScopeId)

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

    Public Function LTPartner_IU(ByVal info As DetailScopeInfo) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspScope_LTPartner_IU", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmLTID As New SqlParameter("@ltid", SqlDbType.Int)
        Dim prmLTValue As New SqlParameter("@ltvalue", SqlDbType.Int)
        Dim prmDScopeId As New SqlParameter("@dscopeid", SqlDbType.Int)
        Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 300)
        Dim prmActivityId As New SqlParameter("@activityid", SqlDbType.Int)

        prmLTID.Value = info.ScopeLTPartnerInfo.LTID
        prmLTValue.Value = info.ScopeLTPartnerInfo.LTValue
        prmDScopeId.Value = info.DScopeId
        prmLMBY.Value = info.ScopeLTPartnerInfo.CMAInfo.ModifiedUser
        prmActivityId.Value = info.ScopeLTPartnerInfo.ActivityInfo.ActivityId

        Command.Parameters.Add(prmLTID)
        Command.Parameters.Add(prmLTValue)
        Command.Parameters.Add(prmDScopeId)
        Command.Parameters.Add(prmLMBY)
        Command.Parameters.Add(prmActivityId)

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

    Public Function LTPartner_D(ByVal ltid As Integer) As Boolean
        Dim isSucceed As Boolean = True
        Command = New SqlCommand("uspScope_LTPartner_D", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmLTID As New SqlParameter("@ltid", SqlDbType.Int)
        prmLTID.Value = ltid
        Command.Parameters.Add(prmLTID)

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

    Public Function AvailableCheckingBaseOnScopeActivity(ByVal dscopeid As Integer, ByVal activityid As Integer) As Boolean
        Dim isAvailable As Boolean = True
        Command = New SqlCommand("uspScope_LTPartner_CheckingScopeActivity_Available", Connection)
        Command.CommandType = CommandType.StoredProcedure

        Dim prmDScopeId As New SqlParameter("@dscopeid", SqlDbType.Int)
        Dim prmActivityId As New SqlParameter("@activityid", SqlDbType.Int)
        Dim prmRowCount As New SqlParameter("@rowcount", SqlDbType.Int)

        prmDScopeId.Value = dscopeid
        prmActivityId.Value = activityid
        prmRowCount.Direction = ParameterDirection.Output

        Command.Parameters.Add(prmDScopeId)
        Command.Parameters.Add(prmActivityId)
        Command.Parameters.Add(prmRowCount)

        Try
            Connection.Open()
            Command.ExecuteNonQuery()
            Dim rowcount As Integer = Integer.Parse(prmRowCount.Value.ToString())
            If rowcount > 0 Then
                isAvailable = False
            End If
        Catch ex As Exception
        Finally
            Connection.Close()
        End Try

        Return isAvailable
    End Function

#End Region
End Class
