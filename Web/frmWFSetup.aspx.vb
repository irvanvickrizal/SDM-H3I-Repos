Imports Common
Imports BusinessLogic
Imports Entities
Imports System.Data
Partial Class frmWFSetup
    Inherits System.Web.UI.Page
    Dim objutil As New DBUtil
    Dim objDo As New ETTWorkFlow
    Dim objbo As New BOTWorkFlow
    Dim objbo1 As New BOUserType
    Dim objDl As New BODDLs
    Dim objbo2 As New BORole
    Dim dt As New DataTable
    Public dt1 As New DataTable
    Public dt2 As New DataTable
    Dim i As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return ConfirmDelete();")
        If Page.IsPostBack = False Then
            ' Session("User_Name") = "Seeta"
            'dt = objutil.ExeQueryDT("Exec uspDDLTUserType", "UserType")
            'dt = objbo1.uspDDLTUserType()
            'lstGroups.DataSource = dt.DefaultView
            'lstGroups.DataTextField = "txt"
            'lstGroups.DataValueField = "val"
            'lstGroups.DataBind()
            'lstGroups.SelectedIndex = 0
            'lstGroups_SelectedIndexChanged(Nothing, Nothing)
            dt = objbo2.uspGetTRole()
            lstSelected.DataSource = dt
            lstSelected.DataTextField = "txt"
            lstSelected.DataValueField = "val"
            lstSelected.DataBind()
            objDl.fillDDL(ddlGrp, "TDstGroup", True, Constants._DDL_Default_None)
            If Not Request.QueryString("id") Is Nothing Then
                'Disable(True)
                'grdWf.Enabled = False
                btnDelete.Visible = True
                btnSave.Text = "Save"
                txtWFCode.Disabled = True
                bindData()
                'btnSave.Enabled = False
                dt1 = objbo2.uspDDLTTask()
                If Request.QueryString("Mode") Is Nothing Then
                    FillGrid()
                Else
                    If grdWf.Rows.Count > 1 Then
                        Dim ddl As DropDownList
                        ddl = CType(grdWf.Rows(grdWf.Rows.Count - 1).Cells(2).FindControl("ddlTask"), DropDownList)
                        ddl.SelectedIndex = ddl.Items.Count - 1
                    End If
                    btnSave.Text = "Proceed"
                End If

            End If
            CallButtonDisp()
            If Not Request.QueryString("id") Is Nothing Then
                Dim stCount As Integer
                stCount = objutil.ExeQueryScalar("Select Count(*) from codwfdoc where wfid=" & Request.QueryString("id"))
                If stCount = 0 Then
                    If objutil.ExeQueryScalar("select count(*) from WFSiteDoc where sdrstatus=2 and  workflowid=" & Request.QueryString("id") & " and enddatetime is null") = 0 Then btnUpdate.Disabled = False
                Else
                    If objutil.ExeQueryScalar("select count(*) from WFSiteDoc where  sdrstatus=2 and workflowid=" & Request.QueryString("id") & " and enddatetime is null") = 0 Then btnUpdate.Disabled = False

                End If
                If btnUpdate.Disabled = False And Request.QueryString("Mode") = "E" Then
                    btnUp.Disabled = False
                    btnDown.Disabled = False
                    btnAdd.Disabled = False
                    btnRemove.Disabled = False
                    btnAddAll.Disabled = False
                    btnRemoveAll.Disabled = False
                Else
                    btnUp.Disabled = True
                    btnDown.Disabled = True
                    btnAdd.Disabled = True
                    btnRemove.Disabled = True
                    btnAddAll.Disabled = True
                    btnRemoveAll.Disabled = True
                End If
            End If
            'If btnSave.Text = "Save" Then btnDelete.Visible = True
        End If
    End Sub
    Sub Disable(ByVal bln As Boolean)
        btnAdd.Disabled = bln
        btnAddAll.Disabled = bln
        btnRemove.Disabled = bln
        btnRemoveAll.Disabled = bln
        btnUp.Disabled = bln
        btnDown.Disabled = bln
        btnEdit.Visible = bln
    End Sub
    Sub FillGrid()
        grdWf.Columns(3).Visible = True
        grdWf.Columns(7).Visible = True
        grdWf.Columns(8).Visible = True
        dt = objbo.uspTWFDefinitionDetails(Request.QueryString("id"))
        grdWf.DataSource = dt
        grdWf.DataBind()
        grdWf.Columns(3).Visible = False
        If grdWf.Rows.Count > 2 Then
            For i As Integer = 2 To grdWf.Rows.Count - 1
                dt2 = New DataTable
                If dt2.Rows.Count = 0 Then
                    dt2 = objbo2.uspGetTRole(grdWf.Rows(i).Cells(6).Text.ToString)
                End If
                Dim ddl As DropDownList
                ddl = CType(grdWf.Rows(i).Cells(2).FindControl("ddlTask"), DropDownList)
                ddl.Items.RemoveAt(0)
                Dim ddlrol As DropDownList
                ddlrol = CType(grdWf.Rows(i).Cells(4).FindControl("ddlRole"), DropDownList)
                ddlrol.Visible = True
                ddlrol.DataSource = dt2
                ddlrol.DataBind()
                ddlrol.SelectedValue = dt.Rows(i).Item("escroleid")
                Dim txt As HtmlInputText
                txt = CType(grdWf.Rows(i).Cells(5).FindControl("textesc"), HtmlInputText)
                txt.Visible = True
                txt.Value = dt.Rows(i).Item("esctime")
            Next
        End If
        grdWf.Columns(7).Visible = False
        grdWf.Columns(8).Visible = False
    End Sub
    Sub bindData()
        dt = objbo.uspTWFDefinitionDetails(Request.QueryString("id"))
        'Dim a() As String
        If dt.Rows.Count > 0 Then
            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                Dim strItem As String = dt.Rows(i).Item("Route").ToString
                lstUnSelected.Items.Insert(IIf(lstUnSelected.Items.Count > 0, lstUnSelected.Items.Count, 0), strItem) 'lstSelected.SelectedItem.Text)
                lstUnSelected.Items(lstUnSelected.Items.Count - 1).Value = dt.Rows(i).Item("roleId").ToString  'lstSelected.SelectedValue
                Dim strItem1 As String
                strItem1 = IIf(dt.Rows(i).Item("GrpCode").ToString.ToUpper = "C", "Customer", IIf(dt.Rows(i).Item("GrpCode").ToString.ToUpper = "S", "SubCon", IIf(dt.Rows(i).Item("GrpCode").ToString.ToUpper = "N", "NSN", IIf(dt.Rows(i).Item("GrpCode").ToString.ToUpper = "H", "Huawei", ""))))
                strItem1 = strItem1 & " - " & dt.Rows(i).Item("RoleDesc").ToString
                lstSelected.ClearSelection()
                lstSelected.Items.FindByText(strItem1).Selected = True
                lstSelected.Items.RemoveAt(lstSelected.SelectedIndex)
            Next
            'lstGroups.SelectedValue = dt.Rows(i - 1).Item("GrpId").ToString
            txtWFCode.Value = dt.Rows(i - 1).Item("WFCode").ToString
            txtWFName.Value = dt.Rows(i - 1).Item("WFDesc").ToString
            txtTime.Value = dt.Rows(i - 1).Item("KPITime").ToString
            

            'ddlGrp.SelectedValue = dt.Rows(i - 1).Item("DS_Id").ToString
            'lstGroups_SelectedIndexChanged(Nothing, Nothing)
        End If
    End Sub
    Sub CallButtonDisp()
        'If lstGroups.Items.Count > 0 Then
        '    btnAdd.Disabled = False
        '    btnAddAll.Disabled = False
        'Else
        '    btnAdd.Disabled = True
        '    btnAddAll.Disabled = True
        'End If
        If lstUnSelected.Items.Count > 0 Then
            btnRemove.Disabled = False
            btnRemoveAll.Disabled = False
        Else
            btnRemove.Disabled = True
            btnRemoveAll.Disabled = True
        End If
        If lstUnSelected.Items.Count > 1 And Request.QueryString("id") Is Nothing Then
            btnUp.Disabled = False
            btnDown.Disabled = False
        Else
            btnUp.Disabled = True
            btnDown.Disabled = True
        End If
        If btnUpdate.Disabled = False And Request.QueryString("Mode") = "E" Then
            btnUp.Disabled = False
            btnDown.Disabled = False
            btnAdd.Disabled = False
            btnRemove.Disabled = False
            btnAddAll.Disabled = False
            btnRemoveAll.Disabled = False
        End If
    End Sub
    Protected Sub btnAdd_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.ServerClick
        If lstSelected.SelectedIndex <> -1 Then
            Dim strItem As String = lstSelected.SelectedItem.Text 'Left(lstGroups.SelectedItem.Text, 1) & " - " & lstSelected.SelectedItem.Text
            lstUnSelected.Items.Insert(IIf(lstUnSelected.Items.Count > 0, lstUnSelected.Items.Count, 0), strItem) 'lstSelected.SelectedItem.Text)
            lstUnSelected.Items(lstUnSelected.Items.Count - 1).Value = lstSelected.SelectedValue
            lstSelected.Items.RemoveAt(lstSelected.SelectedIndex)
            'lstUnSelected.DataBind()
            'lstSelected.DataBind()
        End If
        CallButtonDisp()
    End Sub

    Protected Sub btnAddAll_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddAll.ServerClick
        'Dim i As Integer
        'For i = 0 To lstSelected.Items.Count - 1
        '    lstUnSelected.Items.Insert(IIf(lstUnSelected.Items.Count > 0, lstUnSelected.Items.Count, 0), lstSelected.Items(0).Text)
        '    lstSelected.Items.RemoveAt(0)
        'Next
        dt = objbo2.uspGetTRole()
        lstUnSelected.DataSource = dt
        lstUnSelected.DataTextField = "txt"
        lstUnSelected.DataValueField = "val"
        lstUnSelected.DataBind()
        lstSelected.Items.Clear()
        CallButtonDisp()
    End Sub

    Protected Sub btnRemove_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.ServerClick
        If lstUnSelected.SelectedIndex <> -1 Then
            lstSelected.Items.Insert(IIf(lstSelected.Items.Count > 0, lstSelected.Items.Count, 0), lstUnSelected.SelectedItem.Text)
            lstSelected.Items(lstSelected.Items.Count - 1).Value = lstUnSelected.SelectedValue
            lstUnSelected.Items.RemoveAt(lstUnSelected.SelectedIndex)
            'lstUnSelected.DataBind()
            'lstSelected.DataBind()
        End If
        CallButtonDisp()
    End Sub

    Protected Sub btnRemoveAll_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemoveAll.ServerClick
        'Dim i As Integer
        'For i = 0 To lstUnSelected.Items.Count - 1
        '    lstSelected.Items.Insert(IIf(lstSelected.Items.Count > 0, lstSelected.Items.Count, 0), lstUnSelected.Items(0).Text)
        '    lstUnSelected.Items.RemoveAt(0)
        'Next
        dt = objbo2.uspGetTRole()
        lstSelected.DataSource = dt
        lstSelected.DataTextField = "txt"
        lstSelected.DataValueField = "val"
        lstSelected.DataBind()
        lstUnSelected.Items.Clear()
        CallButtonDisp()
    End Sub

    Protected Sub btnUp_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.ServerClick
        Dim iIndex As Integer = 0
        Dim iValue As Integer = 0
        Dim iText As String = ""
        If lstUnSelected.SelectedIndex <> -1 Then
            If lstUnSelected.SelectedIndex = 0 Then
                iIndex = lstUnSelected.SelectedIndex
                iValue = lstUnSelected.SelectedValue
                iText = lstUnSelected.SelectedItem.Text
                lstUnSelected.Items.RemoveAt(lstUnSelected.SelectedIndex)
                'Modified by Fauzan, 18 Dec 2018
                Dim listItem As ListItem = New ListItem(iText, iValue)
                'lstUnSelected.Items.Insert(lstUnSelected.Items.Count - 1, iText)
                'lstUnSelected.Items(lstUnSelected.Items.Count - 1).Value = iValue
                lstUnSelected.Items.Insert(lstUnSelected.Items.Count - 1, listItem)
                'END
                'lstUnSelected.SelectedIndex = lstUnSelected.Items.Count - 1
            Else
                iIndex = lstUnSelected.SelectedIndex
                iValue = lstUnSelected.SelectedValue
                iText = lstUnSelected.SelectedItem.Text
                lstUnSelected.Items.RemoveAt(lstUnSelected.SelectedIndex)
                lstUnSelected.Items.Insert(iIndex - 1, iText)
                lstUnSelected.Items(iIndex - 1).Value = iValue
                'lstUnSelected.SelectedIndex = lstUnSelected.SelectedIndex - 1
            End If
        End If
        lstUnSelected.DataBind()
        lstUnSelected.SelectedIndex = iIndex - 1
    End Sub

    Protected Sub btnDown_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.ServerClick
        'If lstUnSelected.SelectedIndex <> -1 Then
        '    If lstUnSelected.SelectedIndex = lstUnSelected.Items.Count - 1 Then
        '        lstUnSelected.SelectedIndex = 0
        '    Else
        '        'lstUnSelected.SelectedIndex = lstUnSelected.SelectedIndex + 1
        '    End If
        'End If
        'lstUnSelected.DataBind()
        Dim iIndex As Integer = 0
        Dim iValue As Integer = 0
        Dim iText As String = ""
        If lstUnSelected.SelectedIndex <> -1 Then
            If lstUnSelected.SelectedIndex = 0 Then
                iIndex = lstUnSelected.SelectedIndex
                iValue = lstUnSelected.SelectedValue
                iText = lstUnSelected.SelectedItem.Text
                lstUnSelected.Items.RemoveAt(lstUnSelected.SelectedIndex)
                lstUnSelected.Items.Insert(1, iText)
                lstUnSelected.Items(1).Value = iValue
                'lstUnSelected.SelectedIndex = lstUnSelected.Items.Count - 1
            Else
                iIndex = lstUnSelected.SelectedIndex
                iValue = lstUnSelected.SelectedValue
                iText = lstUnSelected.SelectedItem.Text
                If iIndex = lstUnSelected.Items.Count - 1 Then
                    lstUnSelected.Items.RemoveAt(lstUnSelected.SelectedIndex)
                    lstUnSelected.Items.Insert(0, iText)
                    lstUnSelected.Items(0).Value = iValue
                Else
                    lstUnSelected.Items.RemoveAt(lstUnSelected.SelectedIndex)
                    lstUnSelected.Items.Insert(iIndex + 1, iText)
                    lstUnSelected.Items(iIndex + 1).Value = iValue
                End If

                'lstUnSelected.SelectedIndex = lstUnSelected.SelectedIndex - 1
            End If
        End If
        lstUnSelected.DataBind()
        'Modified by Fauzan, 18 Dec
        Dim objIndex As Integer
        If iIndex = lstUnSelected.Items.Count - 1 Then
            objIndex = iIndex
        Else
            objIndex = iIndex + 1
        End If
        'lstUnSelected.SelectedIndex = iIndex + 1
        lstUnSelected.SelectedIndex = objIndex
        'END
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim strIds As String = ""
        saveData()
        lstUnSelected.DataBind()
        If Not Request.QueryString("id") Is Nothing Then objDo.WFID = Request.QueryString("id")
        Dim strWFId As Integer = objbo.uspTWorkFlowIU(objDo)
        Dim strSql As String = ""
        If strWFId <> 0 Then
            If strWFId <> -1 Then
                Dim strTask As Integer = 1
                Dim isvalid As Boolean = True
                Dim strErrTsk As String = String.Empty
                For i As Integer = 0 To lstUnSelected.Items.Count - 1
                    Dim strEscRole As Integer = 0
                    Dim strEscTime As Integer = 0
                    Dim tskid As Integer = 0
                    If grdWf.Rows.Count > 0 Then
                        Dim ddl As New DropDownList
                        Dim ddlRole As New DropDownList
                        Dim txtesc As New HtmlInputText
                        ddl = CType(grdWf.Rows(i).Cells(2).FindControl("ddlTask"), DropDownList)
                        ddlRole = CType(grdWf.Rows(i).Cells(2).FindControl("ddlRole"), DropDownList)
                        txtesc = CType(grdWf.Rows(i).Cells(2).FindControl("textesc"), HtmlInputText)
                        strTask = ddl.SelectedValue
                        If ddlRole.Visible = True Then
                            strEscRole = ddlRole.SelectedValue
                            strEscTime = IIf(txtesc.Value = "", 0, Val(txtesc.Value))
                        End If
                        tskid = Integer.Parse(ddl.SelectedValue)
                        strErrTsk = ddl.SelectedItem.Text
                    End If
                    strSql = strSql & IIf(strSql = "", "", " Union ") & "Select " & strWFId & "," & i + 1 & ",GrpId,RoleID," & Constants.STATUS_ACTIVE & ",''" & Session("User_Name") & "'',GetDate()," & strTask & "," & strEscRole & "," & strEscTime & " from TRole where RoleId=" & lstUnSelected.Items(i).Value
                    'If (New WFController().CheckTaskIsUniqueBaseWorkflow(objDo.WFID, tskid) = False) Then
                    '    isvalid = False
                    '    Exit For
                    'Else
                    '    strErrTsk = String.Empty
                    'End If
                Next
                If isvalid = False Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Task " & strErrTsk & " already Exists. Please choose other different task!');", True)
                Else
                    If strSql <> "" Then
                        If btnSave.Text = "Proceed" Then
                            objbo.UspTWFDefinitionIU(strSql, strWFId)
                            Response.Redirect("frmWFSetup.aspx?id=" & strWFId)
                            'BOcommon.result(objbo.UspTWFDefinitionIU(strSql, strWFId), True, "frmWFSetup.aspx?id=" & strWFId, "", Constants._INSERT)
                        Else
                            If validateesctime() Then

                            Else
                                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Escalted time greater than total SLA.');", True)
                                Exit Sub

                            End If
                            objbo.UspTWFDefinitionIU(strSql, strWFId)
                            BOcommon.result(objbo.UspTWFDefinitionIU(strSql, strWFId), True, "frmWFList.aspx", "", Constants._INSERT)
                            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Data Saved Successfully.');", True)
                            Response.Redirect("frmWFList.aspx")
                        End If

                    End If
                End If
                
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Process flow Code already Exists.');", True)
            End If
        End If


    End Sub
    Sub saveData()
        objDo.DS_Id = ddlGrp.SelectedValue
        objDo.KPITime = IIf(txtTime.Value <> "", txtTime.Value, Constants.WFDefaultTime)
        objDo.WFCode = Trim(txtWFCode.Value.Replace("'", "''"))
        objDo.WFDesc = Trim(txtWFName.Value.Replace("'", "''"))
        objDo.at.RStatus = Constants.STATUS_ACTIVE
        objDo.at.LMBY = Session("User_Name")
    End Sub
    Protected Sub btnCancel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.ServerClick
        Response.Redirect("frmWFList.aspx")
    End Sub

    Protected Sub grdWf_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdWf.RowCreated
        If e.Row.RowType = DataControlRowType.Header Or e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(6).Visible = False
        End If
    End Sub

    Public Sub dodis(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddl As DropDownList = CType(sender, DropDownList)
        Dim drow As GridViewRow = CType(ddl.NamingContainer, GridViewRow)
        Dim i As Integer
        i = drow.RowIndex
        Dim ddltask As New DropDownList
        Dim ddlrole As New DropDownList
        Dim txt As New HtmlInputText
        ddltask = grdWf.Rows(i).Cells(2).FindControl("ddlTask")
        ddlrole = grdWf.Rows(i).Cells(4).FindControl("ddlRole")
        txt = grdWf.Rows(i).Cells(5).FindControl("textesc")
        If (ddltask.SelectedValue > 1) Then
            dt2 = New DataTable
            If dt2.Rows.Count = 0 Then
                dt2 = objbo2.uspGetTRole(grdWf.Rows(i).Cells(6).Text.ToString)
            End If
            ddlrole.Visible = True
            ddlrole.DataSource = dt2
            ddlrole.DataBind()
            txt.Visible = True
        Else
            ddlrole.Visible = False
            txt.Visible = False
        End If
    End Sub
    Public Function validateesctime() As Boolean
        Dim j As Integer
        Dim tot As Integer = 0
        For j = 0 To grdWf.Rows.Count - 1
            Dim txt As HtmlInputText
            txt = CType(grdWf.Rows(j).Cells(5).FindControl("textesc"), HtmlInputText)
            tot = tot + Val(txt.Value)
        Next
        If tot > txtTime.Value Then
            Return False
        Else
            Return True
        End If

    End Function

    Protected Sub grdWf_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdWf.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddl As DropDownList
            ddl = CType(e.Row.Cells(2).FindControl("ddlTask"), DropDownList)
            ddl.SelectedValue = e.Row.Cells(3).Text
            If i = 1 Then
                Dim drow As GridViewRow = CType(ddl.NamingContainer, GridViewRow)
                i = drow.RowIndex
                Dim ddltask As New DropDownList
                Dim ddlrole As New DropDownList
                Dim txt As New HtmlInputText
                ddltask = e.Row.Cells(2).FindControl("ddlTask")
                ddlrole = e.Row.Cells(4).FindControl("ddlRole")
                txt = e.Row.Cells(5).FindControl("textesc")
                If (ddltask.SelectedValue > 1) Then
                    dt2 = New DataTable
                    If dt2.Rows.Count = 0 Then
                        dt2 = objbo2.uspGetTRole(e.Row.Cells(6).Text.ToString)
                    End If
                    ddlrole.Visible = True
                    ddlrole.DataSource = dt2
                    ddlrole.DataBind()
                    ddlrole.SelectedValue = e.Row.Cells(7).Text.ToString
                    txt.Visible = True
                    txt.Value = e.Row.Cells(8).Text.ToString
                Else
                    ddlrole.Visible = False
                    txt.Visible = False
                End If
            End If
            i += 1


        End If

    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim i As Integer
        If Not Request.QueryString("id") Is Nothing Then
            i = objbo.uspWFDelete(Request.QueryString("id"))
            If i = 1 Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Data Deleted Successfully.');", True)
                Response.Redirect("frmWFList.aspx")
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "Script", "alert('Process Flow already have Transaction Data...');", True)
            End If
        End If
    End Sub

    Protected Sub btnUpdate_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("frmWFSetup.aspx?id=" & Request.QueryString("id") & "&Mode=E")
    End Sub

    Private Sub grdWf_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdWf.PageIndexChanging
        grdWf.PageIndex = e.NewPageIndex
        dt1 = objbo2.uspDDLTTask()
        FillGrid()
    End Sub
End Class
