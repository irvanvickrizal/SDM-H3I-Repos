Imports System.Data
Imports System.Data.OleDb
Imports Businesslogic
Imports Entities
Imports Common
Imports System.IO
Partial Class MSD_frmNICData
    Inherits System.Web.UI.Page
    Dim objdo As New POErrLog
    Dim objdb As New DBUtil
    Dim viewflag As Boolean = True
    Dim objete As New ETEPMData
    Dim cst As New Constants
    Dim objdae As New BOEPMRawData
    Dim CTaskCompleted As String = ""
    Dim CPlanBAUT As String = ""
    Dim CPlanBAST As String = ""
    Dim SiteNamePO As String = ""
    Dim dtEpmData As New DataTable
    Dim dtMileStone As New DataTable
    Dim dt As New DataTable
    Dim strSQL As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            bindData()
            fillDataintoText()
        End If
        If Request.QueryString("SLNO") <> "" Then
            btnDelete.Enabled = True
            btnUpdateFields.Enabled = True
            btnSave.Enabled = False
        End If
        If grdNICDATA.Rows.Count = 0 Then
            btnUpdateFields.Enabled = False
            btnDelete.Enabled = False
            btnSave.Enabled = True
        End If
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim strDataType As String = ddlDataType.SelectedItem.Text
        Dim strFieldName As String = Nothing
        Dim strConfirmDataType As String = Nothing
        Dim strNICDataFields As String = Nothing
        Dim myStrSql As String = Nothing

        If txtFieldName.Text = "" Or strDataType = "--Select--" Then
            lblMSG.Text = "Insert the Field in TextBox or Select the Type"
            lblMSG.Visible = True
        Else
            Try
                strDataType = ddlDataType.SelectedItem.Text
                strFieldName = Trim(LTrim(RTrim(txtFieldName.Text)))
                If strDataType = "DateTime" Then
                    strConfirmDataType = "Alter table NICData add " & strFieldName & " Datetime"
                    strNICDataFields = "insert into NICDataFields (FieldName,DataType) values ('" & strFieldName & "','" & strDataType & "')"
                ElseIf strDataType = "Text" Then
                    strConfirmDataType = "Alter table NICData add " & strFieldName & " Varchar(100)"
                    strNICDataFields = "insert into NICDataFields (FieldName,DataType) values ('" & strFieldName & "','" & strDataType & "')"

                ElseIf strDataType = "Integer" Then
                    strConfirmDataType = "Alter table NICData add " & strFieldName & " Integer"
                    strNICDataFields = "insert into NICDataFields (FieldName,DataType) values ('" & strFieldName & "','" & strDataType & "')"
                End If
                objdb.ExeUpdate(strConfirmDataType)
                objdb.ExeUpdate(strNICDataFields)
                lblMSG.Visible = True
                lblMSG.Text = "The Field has been added in to NICDATA"
                txtFieldName.Text = ""
                ddlDataType.SelectedValue = 0
                btnDelete.Enabled = False
                btnUpdateFields.Enabled = False
                bindData()

                'fillMileStoneData()
            Catch ex As Exception
                lblMSG.Text = "The Column is already Exist in NICDATA"
                lblMSG.Visible = True
                txtFieldName.Text = ""
                ddlDataType.SelectedValue = 0
            End Try
        End If
        txtFieldName.Text = ""
        ddlDataType.SelectedValue = 0
    End Sub

    Sub bindData()

        strSQL = "Exec uspNICDataGetFields"
        dt = objdb.ExeQueryDT(strSQL, "NICDataTemplate")

        grdNICDATA.DataSource = dt
        grdNICDATA.DataBind()
    End Sub
    Sub fillDataintoText()
      
        strSQL = "exec uspGetNICFieldsData " & Request.QueryString("SLNo")
        dt = objdb.ExeQueryDT(strSQL, "NICDataFields")

        If dt.Rows.Count > 0 Then
            txtFieldName.Text = dt.Rows(0).Item("FieldName").ToString
            hdnFieldName.Value = dt.Rows(0).Item("FieldName").ToString
            ddlDataType.SelectedItem.Text = dt.Rows(0).Item("DataType").ToString
            hdnSLNO.Value = dt.Rows(0).Item("SLNO").ToString

        End If
    End Sub

    Protected Sub btnUpdateFields_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateFields.Click
        'EXEC sp_rename 'nicdata.OldField', 'NewField'
        If txtFieldName.Text = "" Then
            lblMSG.Text = "Plese select Atleast one filed to Update"
            lblMSG.Visible = True
        Else

            strSQL = "EXEC sp_rename  'NICDATA." & hdnFieldName.Value & "', '" & txtFieldName.Text & "'"
            objdb.ExeUpdate(strSQL)
            strSQL = "Update NICDataFields set FieldName= '" & txtFieldName.Text & "', DataType = '" & ddlDataType.SelectedItem.Text & "' where SLNO= " & hdnSLNO.Value & ""
            objdb.ExeQuery(strSQL)
            lblMSG.Text = "The Field has been Updated"
            txtFieldName.Text = ""
            ddlDataType.SelectedValue = 0
            lblMSG.Visible = True
            btnDelete.Enabled = False
            btnUpdateFields.Enabled = False
            btnSave.Enabled = True
        End If
        Response.Redirect("frmNICData.aspx")
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtFieldName.Text = "" Then
            lblMSG.Text = "Plese select Atleast one filed to Delete"
            lblMSG.Visible = True
        Else
            strSQL = "Exec uspNICFieldsDelete " & hdnFieldName.Value
            objdb.ExeUpdate(strSQL)
            strSQL = "Alter Table NICDATA drop column " & txtFieldName.Text
            objdb.ExeQuery(strSQL)
            lblMSG.Text = "The Field has been Deleted"
            lblMSG.Visible = True
            btnSave.Enabled = True
            btnUpdateFields.Enabled = False
            btnDelete.Enabled = False
            txtFieldName.Text = ""

        End If
        Response.Redirect("frmNICData.aspx")
    End Sub

    Protected Sub grdNICDATA_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdNICDATA.RowDataBound
        
    End Sub
End Class

