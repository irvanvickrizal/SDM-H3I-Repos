'Created By : satish
Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data
Partial Class frmMileStoneTrack
    Inherits System.Web.UI.Page
    Dim objdo As New ETCODArea
    Dim objbo As New BOCODArea
    Dim objdl As New BODDLs
    Dim dt As New DataTable
    Dim objutil As New DBUtil
    Dim j, n, status As Integer
    Dim milest As String = ""
    Dim dupmilest As String = ""
    Dim str As String = ""
    Protected Sub btnNewGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
        tblmilestone.Visible = True
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Page_size") Is Nothing Then Response.Redirect("~/SessionTimeout.aspx")
        btnSave.Attributes.Add("onclick", "javascript:return checkIsEmpty();")
        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this record?');")
        If Page.IsPostBack = False Then
            objdl.fillDDL(ddlmilestone, "codmilestone", True, Constants._DDL_Default_Select)
            objdl.fillDDL(ddlgroup, "codlookup", 8, True, Constants._DDL_Default_Select)
            dt = objutil.ExeQueryDT("uspDDLcodmilestonenew", "ddd")
            lstmilestone.DataSource = dt
            lstmilestone.DataTextField = "txt"
            lstmilestone.DataValueField = "val"
            lstmilestone.DataBind()
            objdl.fillDDL(ddlpo, "PoNo", True, "--Please Select--")
            binddata()
            If Not Request.QueryString("pono") Is Nothing Then
                tdTitle.InnerText = ""
                addrow.InnerText = "Edit"
                btnSave.Text = "Update"
                btnNewGroup.Disabled = True
                Binddetails()

            End If
        End If
    End Sub
    Sub binddata()
        dt = objutil.ExeQueryDT("exec uspMilestoneTrackLD " & "0" & "," & "0" & "", "dd")
        grdmielstonetrack.DataSource = dt
        grdmielstonetrack.PageSize = Session("Page_size")
        grdmielstonetrack.DataBind()
    End Sub
    Sub Binddetails()
        Dim k, m As Integer
        dt = objutil.ExeQueryDT("exec uspMilestoneTrackLD '" & Request.QueryString("pono") & "','" & Request.QueryString("de") & "'", "dd")
        If dt.Rows.Count > 0 Then
            ddlpo.Items.FindByText(dt.Rows(0).Item("PONO").ToString).Selected = True
            ddlgroup.SelectedItem.Text = dt.Rows(0).Item("Description").ToString
            ddlgroup_SelectedIndexChanged(Nothing, Nothing)
            For k = 0 To dt.Rows.Count - 1
                For m = 0 To lstmilestone.Items.Count - 1
                    If lstmilestone.Items(m).Text.ToString = dt.Rows(k).Item(2).ToString Then
                        lstmilestone.Items(m).Selected = True
                    End If
                Next
            Next
            'ddlmilestone.Items.FindByText(dt.Rows(0).Item("milestone").ToString).Selected = True
            tblmilestone.Visible = True
            btnDelete.Visible = True
        End If
    End Sub

    Protected Sub grdmielstonetrack_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdmielstonetrack.PageIndexChanging
        grdmielstonetrack.PageIndex = e.NewPageIndex
        binddata()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Cheking for already exists records
        If Request.QueryString("pono") Is Nothing Then
            For j = 0 To lstmilestone.Items.Count - 1
                If lstmilestone.Items(j).Selected = True Then
                    n = objutil.ExeQueryScalar("select count(*) from milestonetrack where pono='" & ddlpo.SelectedItem.Text & "' and milestone='" & lstmilestone.Items(j).Text & "'")
                    If n <> 0 Then
                        dupmilest = IIf(dupmilest <> "", dupmilest & lstmilestone.Items(j).Text, lstmilestone.Items(j).Text)
                    End If
                End If
            Next
        End If

        'If dupmilest = "" Then
        For j = 0 To lstmilestone.Items.Count - 1
            If lstmilestone.Items(j).Selected = True Then
                milest = IIf(milest <> "", milest & "," & "''" & lstmilestone.Items(j).Text & "''", "''" & lstmilestone.Items(j).Text & "''")
            End If
        Next
        If milest <> "" Then
            If Request.QueryString("pono") Is Nothing Then
                status = objutil.ExeQueryScalar("exec uspMilestoneTrackIU " & 0 & ",'" & ddlpo.SelectedItem.Text & "','" & ddlgroup.SelectedItem.Text & "','" & milest & "','" & Session("User_Name") & "' ")
                BOcommon.result(status, True, "frmMileStoneTrack.aspx", "This combination ", Constants._INSERT)
            Else
                status = objutil.ExeQueryScalar("exec uspMilestoneTrackIU " & 1 & ",'" & ddlpo.SelectedItem.Text & "','" & ddlgroup.SelectedItem.Text & "','" & milest & "','" & Session("User_Name") & "' ")
                BOcommon.result(status, True, "frmMileStoneTrack.aspx", " This combination ", Constants._UPDATE)
            End If
        Else
            Response.Write("<script> alert('Please select the Milestone')</script>")
        End If
       
        'Else
        'str = dupmilest & " mapping already exists with PO " & ddlpo.SelectedItem.Text
        'Response.Write("<script> alert('" + str + "')</script>")

        'End If
    End Sub
    Protected Sub grdmielstonetrack_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdmielstonetrack.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdmielstonetrack.PageIndex * grdmielstonetrack.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"

        End Select
    End Sub
    Protected Sub grdmielstonetrack_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdmielstonetrack.Sorting
        If hdnSort.Value = "" Or hdnSort.Value = e.SortExpression & " Desc" Then
            hdnSort.Value = e.SortExpression & " Asc"
        Else
            hdnSort.Value = e.SortExpression & " Desc"
        End If
        binddata()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim status As Integer
        status = objutil.ExeQueryScalar("exec uspMilestoneTrackDelete '" & Request.QueryString("pono") & "','" & Request.QueryString("de") & "' ")
        BOcommon.result(status, True, "frmMileStoneTrack.aspx", " ", Constants._DELETE)
    End Sub
    Protected Sub btnCanel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanel.ServerClick
        Response.Redirect("frmMileStoneTrack.aspx")
    End Sub
    Protected Sub btnNewGroup_ServerClick1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewGroup.ServerClick
        tdTitle.InnerText = ""
        tblmilestone.Visible = True
    End Sub

    Protected Sub ddlgroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlgroup.SelectedIndexChanged
        lbldefault.Text = objutil.ExeQueryScalar(" select lkpcode from codlookup where lkpdesc ='" & ddlgroup.SelectedItem.Text & "' ")
    End Sub
End Class

