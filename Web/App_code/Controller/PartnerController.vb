Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic

Public Class PartnerController
    Private Shared mycon As SqlConnection = New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("conn").ToString())
    Private Shared reader As SqlDataReader

    ''Public Shared Function InsertUpdateSubcon(ByVal info As SubconInfo)
    'Dim isRegistered As Boolean = False

    'Dim command As New SqlCommand("uspSubcon_IU_New", mycon)
    '    command.CommandType = CommandType.StoredProcedure

    'Dim prmSubconId As New SqlParameter("@subconid", SqlDbType.Int)
    'Dim prmSubconName As New SqlParameter("@subconname", SqlDbType.VarChar, 250)
    'Dim prmDescription As New SqlParameter("@Description", SqlDbType.VarChar, 2000)
    'Dim prmLMBY As New SqlParameter("@lmby", SqlDbType.VarChar, 250)
    'Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)


    '    prmSubconId.Value = info.SubconId
    '    prmSubconName.Value = info.SubconName
    '    prmDescription.Value = info.SubconDesc
    '    prmLMBY.Value = info.LMBY
    '    prmIsDeleted.Value = info.IsDeleted

    '    command.Parameters.Add(prmSubconId)
    '    command.Parameters.Add(prmSubconName)
    '    command.Parameters.Add(prmDescription)
    '    command.Parameters.Add(prmLMBY)
    '    command.Parameters.Add(prmIsDeleted)

    '    Try
    '        mycon.Open()
    '        command.ExecuteNonQuery()
    '    Catch ex As Exception

    '    Finally
    '        mycon.Close()
    '    End Try


    '    Return isRegistered
    'End Function

    'Public Shared Sub DeleteSubconTemp(ByVal subconid As Integer)
    '    Dim command As New SqlCommand("uspSubcon_Delete_Temp", mycon)
    '    command.CommandType = CommandType.StoredProcedure

    '    Dim prmSubconId As New SqlParameter("@subconid", SqlDbType.Int)
    '    Dim prmIsDeleted As New SqlParameter("@isDeleted", SqlDbType.Bit)
    '    prmSubconId.Value = subconid
    '    prmIsDeleted.Value = True
    '    command.Parameters.Add(prmSubconId)
    '    command.Parameters.Add(prmIsDeleted)
    '    Try
    '        mycon.Open()
    '        command.ExecuteNonQuery()
    '    Catch ex As Exception

    '    Finally
    '        mycon.Close()
    '    End Try
    'End Sub

    Public Shared Function GetSubcons_DS(ByVal isDeleted As Boolean) As DataSet
        Dim ds As New DataSet

        Dim command As New SqlCommand("uspSubcon_GetAllSubcon_New", mycon)
        command.CommandType = CommandType.StoredProcedure

        Dim prmIsDeleted As New SqlParameter("@IsDeleted", SqlDbType.Bit)
        prmIsDeleted.Value = isDeleted
        command.Parameters.Add(prmIsDeleted)

        Try
            mycon.Open()
            Dim adapter As New SqlDataAdapter(command)
            adapter.Fill(ds)
        Catch ex As Exception
        Finally
            mycon.Close()
        End Try

        Return ds
    End Function

    Public Shared Function GetSubcons(ByVal isDeleted As Boolean) As List(Of SubconInfo)
        Dim list As New List(Of SubconInfo)

        'Dim command As New SqlCommand("uspSubcon_GetAllSubcon_New", mycon)
        'command.CommandType = CommandType.StoredProcedure

        Dim command As New SqlCommand("select * from subcon where rstatus =2", mycon)
        command.CommandType = CommandType.Text

        'Dim prmIsDeleted As New SqlParameter("@IsDeleted", SqlDbType.Bit)
        'prmIsDeleted.Value = isDeleted
        'command.Parameters.Add(prmIsDeleted)

        Try
            mycon.Open()
            reader = command.ExecuteReader()
            If reader.HasRows Then
                While (reader.Read())
                    Dim info As New SubconInfo
                    info.SubconId = Integer.Parse(reader.Item("scon_id").ToString())
                    info.SubconName = Convert.ToString(reader.Item("scon_name"))
                    list.Add(info)
                End While
            End If
        Catch ex As Exception
        Finally
            mycon.Close()
        End Try

        Return list
    End Function

    Public Shared Function GetSubconIdByUser(ByVal userid As Int32) As Integer
        Dim subconid As Integer = 0

        Dim command As New SqlCommand("uspSubcon_GetSubconCompanyByUserId", mycon)
        command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@userid", SqlDbType.BigInt)
        Dim prmSubconId As New SqlParameter("@subconid", SqlDbType.Int)
        prmUserId.Value = userid
        prmSubconId.Direction = ParameterDirection.Output
        command.Parameters.Add(prmUserId)
        command.Parameters.Add(prmSubconId)
        Try
            mycon.Open()
            command.ExecuteNonQuery()
            subconid = Integer.Parse(prmSubconId.Value.ToString())
        Catch ex As Exception

        Finally
            mycon.Close()
        End Try

        Return subconid
    End Function

    Public Shared Sub InsertUpdateUserSubcon(ByVal userid As Int32, ByVal subconid As Integer)
        Dim command As New SqlCommand("uspSubcon_UserSubcon", mycon)
        command.CommandType = CommandType.StoredProcedure

        Dim prmUserId As New SqlParameter("@userid", SqlDbType.BigInt)
        Dim prmSubconId As New SqlParameter("@subconid", SqlDbType.Int)

        prmUserId.Value = userid
        prmSubconId.Value = subconid

        command.Parameters.Add(prmUserId)
        command.Parameters.Add(prmSubconId)

        Try
            mycon.Open()
            command.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            mycon.Close()
        End Try

    End Sub

    Public Shared Function GetSubconNameBySubconId(ByVal subconid As Integer) As String
        Dim subconName As String = String.Empty
        Dim command As New SqlCommand("uspSubcon_GetSubcon_new", mycon)
        command.CommandType = CommandType.StoredProcedure

        Dim prmSubconName As New SqlParameter("@subconname", SqlDbType.VarChar, 250)
        Dim prmSubconId As New SqlParameter("@subconid", SqlDbType.Int)

        prmSubconName.Direction = ParameterDirection.Output
        prmSubconId.Value = subconid

        command.Parameters.Add(prmSubconName)
        command.Parameters.Add(prmSubconId)

        Try
            mycon.Open()
            command.ExecuteNonQuery()
            subconName = Convert.ToString(prmSubconName.Value)
        Catch ex As Exception
        Finally
            mycon.Close()
        End Try
        Return subconName
    End Function

End Class
