'Created By : Radha
'Date : 09-10-2008
'Updated By : Dedy
'Date : 12-11-2008
Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data

Partial Class frmGrpUsers
    Inherits System.Web.UI.Page
    Dim objdl As New BODDLs
    'Dim objdlP As New BOPDDLs
    Dim objdo As New ETGrpUsers
    Dim objbo As New BOGrpUsers
    Dim dt As New DataTable
    Dim dt2 As New DataTable
    Dim objputil As New DBUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        tblExistingUsers.Visible = False
        tblMoreUsers.Visible = False
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        If Page.IsPostBack = False Then
            objdl.fillDDL(ddlDS_Id, "TDstGroup", True, Constants._DDL_Default_Select)

            binddata()
            getCeklist()
            If Not Request.QueryString("id") Is Nothing Then
                addrow.InnerText = "Group Users Edit"
                btnSave.Text = "Update"
            End If
        End If
    End Sub

    Sub binddata()
        dt = objbo.uspEBASTUsersList(, -1, )
        grdGrpUsers.DataSource = dt
        grdGrpUsers.DataBind()
        getCeklist()
    End Sub

    Sub bindExisting(ByVal name As String)
        dt = objbo.uspEBASTUsersList(, -1, )
        grdExistingUsers.DataSource = dt
        grdExistingUsers.DataBind()
    End Sub

    Sub getCeklist()
        'binddata()
        Dim j As Integer
        Dim sno As String = ""
        For j = 0 To grdGrpUsers.Rows.Count - 1
            Dim chk As HtmlInputCheckBox
            chk = CType(grdGrpUsers.Rows(j).Cells(1).FindControl("chkUsers"), HtmlInputCheckBox)
            If chk.Checked = True Then
                sno = IIf(sno = "", grdGrpUsers.Rows(j).Cells(2).Text, sno & "','" & grdGrpUsers.Rows(j).Cells(2).Text)
            End If
        Next
        'Dim WPID As String = sno
        If sno <> "" Then
            tblExistingUsers.Visible = True
            Dim strqur As String = ""
            strqur = " select USR_ID,[Name] from EBASTUsers_1 where [Name] in ('" & sno & "')"
            dt = objputil.ExeQueryDT(strqur, "strTable")
            grdExistingUsers.DataSource = dt
            grdExistingUsers.DataBind()
        End If

    End Sub


    Sub binddata2()
        Dim chk As HtmlInputCheckBox

        dt2 = objbo.uspEBASTUsersList(, ddlDS_Id.SelectedValue, ddlUSR_Id.SelectedValue)

        For i As Integer = 0 To grdGrpUsers.Rows.Count - 1

            chk = CType(grdGrpUsers.Rows(i).Cells(1).FindControl("chkUsers"), HtmlInputCheckBox)
            chk.Checked = False

            For j As Integer = 0 To dt2.Rows.Count - 1
                If chk.Value = dt2.Rows(j).Item("USR_ID") Then
                    chk.Checked = True
                End If
            Next
        Next
    End Sub

    Sub binddata3()
        dt = objbo.uspEBASTUsersList(ddlUSR_Id.SelectedValue, -1, )
        grdGrpUsers.DataSource = dt
        'grdGrpUsers.PageSize = 3
        grdGrpUsers.DataBind()
    End Sub

    Protected Sub grdGrpUsers_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdGrpUsers.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdGrpUsers.PageIndex * grdGrpUsers.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        fillDescription()

        Dim chk As HtmlInputCheckBox
        Dim strid As String = ""

        For i As Integer = 0 To grdGrpUsers.Rows.Count - 1
            chk = CType(grdGrpUsers.Rows(i).Cells(1).FindControl("chkUsers"), HtmlInputCheckBox)

            If chk.Checked = True Then
                strid = strid & IIf(strid = "", "", ",") & "'" & chk.Value & "'"
            End If
        Next
        If strid <> "" Then
            strid = strid.Replace("'", "''")
            objdo.USR_Id = strid
            Dim stsql As String

            stsql = "exec uspGrpUsersIU " & objdo.UD_Id & "," & objdo.DS_Id & ",'" & objdo.USR_Id & "'," & objdo.GRP_Id & "," & objdo.AT.RStatus & ",'" & objdo.AT.LMBY & "' "
            BOcommon.result(objputil.ExeQueryScalar(stsql), True, "frmGrpUsers.aspx", "DS_Name", Constants._INSERT)
        Else

        End If
    End Sub

    Sub fillDescription()
        objdo.AT.RStatus = Constants.STATUS_ACTIVE
        objdo.AT.LMBY = Session("User_Name")
        objdo.DS_Id = ddlDS_Id.SelectedValue
        objdo.GRP_Id = ddlUSR_Id.SelectedValue
    End Sub

    Protected Sub ddlDS_Id_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDS_Id.SelectedIndexChanged
        objdl.fillDDL(ddlUSR_Id, "TUserType1", True, Constants._DDL_Default_Select)
    End Sub

    Protected Sub ddlUSR_Id_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUSR_Id.SelectedIndexChanged
        binddata3()
        binddata2()
        getCeklist()
        space.Attributes.Add("class", "col-sm-3")   'Added by Fauzan, 19 Dec 2018. Set Space
    End Sub

    Protected Sub btnAddUS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddUS.Click
        'tblMoreUsers.Visible = True    'Commented by Fauzan 19 Dec 2018.
        getCeklist()
        'Added by Fauzan, 19 Dec 2018. Set Space
        space.Attributes.Remove("class")
        If grdGrpUsers.Rows.Count > 0 Then
            If ddlUSR_Id.SelectedValue <> "0" Then
                tblMoreUsers.Visible = True
            End If
            If tblExistingUsers.Visible = True Then
                space.Attributes.Add("class", "col-sm-1")
            Else
                space.Attributes.Add("class", "col-sm-3")
            End If
        End If
        'END
    End Sub
End Class
