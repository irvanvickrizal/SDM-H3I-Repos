Imports System.Data
Imports Common
Imports System.Collections.Generic


Partial Class COD_CODWCCDocActivityGrouping
    Inherits System.Web.UI.Page

    Dim dbutils As New DBUtil
    Dim controller As New CODActivityController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub


    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        Dim isValid As Boolean = True
        Dim strRemarks As String = String.Empty
        If DdlParentDocType.SelectedValue.Equals("0") Then
            isValid = False
            strRemarks = "Please Choose Parent Document Type First"
        End If

        If isValid = True And (DdlDocType.SelectedIndex < 1 Or DdlDocType.Items.Count = 0) Then
            isValid = False
            strRemarks = "Please Choose Doc Type First"
        End If

        If isValid = True And (DdlActivities.SelectedIndex < 1 Or DdlActivities.Items.Count = 0) Then
            isValid = False
            strRemarks = "Please Choose Subcon Type First"
        End If

        If isValid = True Then
            WarningMessage(False, strRemarks)
            AddDocument(DdlParentDocType.SelectedValue, Integer.Parse(DdlDocType.SelectedValue), Integer.Parse(DdlActivities.SelectedValue), CommonSite.UserId)
        Else
            WarningMessage(True, strRemarks)
        End If

    End Sub


    Protected Sub BtnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        ClearForm()
    End Sub

    Protected Sub DdlParentDocType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlParentDocType.SelectedIndexChanged
        If DdlParentDocType.SelectedValue <> "0" Then
            BindDocs(DdlParentDocType.SelectedValue)
        Else
            DdlDocType.Items.Clear()
            DdlDocType.Items.Insert(0, "No Data Found")
        End If
    End Sub


    Protected Sub DdlDocType_selectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlDocType.SelectedIndexChanged
        If DdlDocType.SelectedIndex > 0 Then
            BindActivities(DdlParentDocType.SelectedValue, Integer.Parse(DdlDocType.SelectedValue))
        Else
            DdlActivities.Items.Clear()
            DdlActivities.Items.Insert(0, "No Data Found")
        End If
    End Sub


#Region "Custom Methods"
    Private Sub BindData()
        'MvCorePanel.SetActiveView(vwListDocs)
        GvDocGrouping.DataSource = controller.GetGroupingWCCDocActivity()
        GvDocGrouping.DataBind()
    End Sub

    Private Sub BindDocs(ByVal parenttype As String)
        Dim dtDocuments As New DataTable
        If parenttype.Equals("BAUT") Then
            dtDocuments = dbutils.ExeQueryDT("select doc_id, docname from coddoc where parent_id =" & ConfigurationManager.AppSettings("BAUTID") & _
            "  and rstatus = 2", "dt")
        Else
            dtDocuments = dbutils.ExeQueryDT("select WCCDocument_id 'doc_id', docname from WCC_CODDocument where isDeleted=0 ", "dt")
        End If
        DdlDocType.DataSource = dtDocuments
        DdlDocType.DataTextField = "docname"
        DdlDocType.DataValueField = "doc_id"
        DdlDocType.DataBind()
        DdlDocType.Items.Insert(0, "-- Document Type --")
    End Sub

    Private Sub BindActivities(ByVal parenttype As String, ByVal docid As Integer)
        Dim list As List(Of CODActivityInfo) = controller.GetDocActivityNotUsedBaseOnDocId(docid, parenttype)
        DdlActivities.Items.Clear()
        If list.Count > 0 Then
            DdlActivities.DataSource = list
            DdlActivities.DataTextField = "ActivityName"
            DdlActivities.DataValueField = "ActivityId"
            DdlActivities.DataBind()
            DdlActivities.Items.Insert(0, "--Select User Activity Type--")
        Else
            DdlActivities.Items.Insert(0, "No Data Found")
        End If
    End Sub

    Private Sub ClearForm()
        DdlParentDocType.ClearSelection()
        DdlDocType.Items.Clear()
        DdlActivities.Items.Clear()
    End Sub

    Private Sub AddDocument(ByVal parentdoctype As String, ByVal docid As Integer, ByVal activityid As Integer, ByVal userid As Integer)
        Dim info As New CODDocActivityGroupingInfo
        info.LMBY = userid
        info.ActivityId = activityid
        info.DocId = docid
        info.ParentDocType = parentdoctype
        Dim isSucceed As Boolean = controller.GroupingWCCDocActivity_I(info)
        If isSucceed = True Then
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "SucceedSave();", True)
            BindData()
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "ErrorSave();", True)
        End If
        ClearForm()
    End Sub

    Private Sub WarningMessage(ByVal isVisible As Boolean, ByVal remarks As String)
        'panelWarningMessage.Visible = isVisible
        LblWarningMessage.Text = remarks
    End Sub

    Private Sub GvDocGrouping_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvDocGrouping.PageIndexChanging
        GvDocGrouping.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub GvDocGrouping_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvDocGrouping.RowCommand
        If (e.CommandName.Equals("DeleteGrouping")) Then
            Dim value As Integer = Convert.ToInt32(e.CommandArgument)
            If value > -1 Then
                If controller.GroupingWCCDocActivity_D(value) Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Successfully deleted WCC Activity Document Grouping');", True)
                    BindData()
                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Failed to delete WCC Activity Document Grouping');", True)
                End If
            End If
        End If
    End Sub

#End Region

End Class
