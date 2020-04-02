Imports CRFramework
Imports System.Data
Imports Common

Partial Class COD_frmDocInitiator
    Inherits System.Web.UI.Page
    Dim ccontroller As New CommonController
    Dim objdb As New DBUtil
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub GvDocInitiator_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName.Equals("AddNew") Then
            Dim DdlRole As DropDownList = CType(GvDocInitiator.FooterRow.FindControl("DdlRole"), DropDownList)
            Dim DdlDocumentType As DropDownList = CType(GvDocInitiator.FooterRow.FindControl("DdlDocumentType"), DropDownList)
            AddNewInitiatorDocument(DdlDocumentType.SelectedValue, DdlRole.SelectedValue)
        End If
    End Sub

    Protected Sub GvDocInitiator_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim docinitiatorid As Int32 = Convert.ToInt32(GvDocInitiator.DataKeys(e.RowIndex).Values("InitiatorDoc_Id").ToString())
        DeleteInitiatorDocument(Convert.ToString(docinitiatorid))
    End Sub

    Protected Sub GvDocInitiator_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GvDocInitiator.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim ddlRole As DropDownList = CType(e.Row.FindControl("DdlRole"), DropDownList)
            Dim ddlDocumentType As DropDownList = CType(e.Row.FindControl("DdlDocumentType"), DropDownList)
            BindDocumentType(ddlDocumentType)
            If ddlDocumentType.SelectedIndex > 0 Then
                BindRole(ddlRole, Integer.Parse(ddlDocumentType.SelectedValue))
            Else
                BindRole(ddlRole, 0)
            End If
        ElseIf e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblType As Label = CType(e.Row.FindControl("LblType"), Label)
            Dim imgbtnDelete As ImageButton = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)

            If lblType.Text.ToLower().Equals("workflow") Then
                imgbtnDelete.Visible = False
            Else
                imgbtnDelete.Visible = True
            End If

        End If
    End Sub

    Protected Sub DdlDocumentTypeChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddlRole As DropDownList = GvDocInitiator.FooterRow.FindControl("DdlRole")
        Dim ddlDocType As DropDownList = GvDocInitiator.FooterRow.FindControl("DdlDocumentType")
        BindRole(ddlRole, Integer.Parse(ddlDocType.SelectedValue))
        BindData2()
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        Dim ds As DataSet = ccontroller.GetDocInitiatorList_DS()
        If (ds.Tables(0).Rows.Count > 0) Then
            GvDocInitiator.DataSource = ds
            GvDocInitiator.DataBind()
        Else
            ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
            GvDocInitiator.DataSource = ds
            GvDocInitiator.DataBind()
            Dim columncount As Integer = GvDocInitiator.Rows(0).Cells.Count
            GvDocInitiator.Rows(0).Cells.Clear()
            GvDocInitiator.Rows(0).Cells.Add(New TableCell())
            GvDocInitiator.Rows(0).Cells(0).ColumnSpan = columncount
            GvDocInitiator.Rows(0).Cells(0).Text = "No Records Found"
        End If
    End Sub

    Private Sub BindData2()
        Dim ds As DataSet = ccontroller.GetDocInitiatorList_DS()
        If (ds.Tables(0).Rows.Count = 0) Then
            Dim columncount As Integer = GvDocInitiator.Rows(0).Cells.Count
            GvDocInitiator.Rows(0).Cells.Clear()
            GvDocInitiator.Rows(0).Cells.Add(New TableCell())
            GvDocInitiator.Rows(0).Cells(0).ColumnSpan = columncount
            GvDocInitiator.Rows(0).Cells(0).Text = "No Records Found"
        End If
    End Sub
    Private Sub BindRole(ByVal ddl As DropDownList, ByVal docid As String)
        Dim strQuery As String = "exec uspGeneral_GetRoleByInitiatorDocument " & docid
        Dim dtRoles As DataTable = objdb.ExeQueryDT(strQuery, "roles")
        ddl.DataSource = dtRoles
        ddl.DataTextField = "roledesc"
        ddl.DataValueField = "roleid"
        ddl.DataBind()
        ddl.Items.Insert("0", "--Select Role--")
    End Sub

    Private Sub BindDocumentType(ByVal ddl As DropDownList)
        Dim strQuery As String = "select doc_id,docname from coddoc where rstatus = 2"
        Dim dtRoles As DataTable = objdb.ExeQueryDT(strQuery, "roles")
        ddl.DataSource = dtRoles
        ddl.DataTextField = "docname"
        ddl.DataValueField = "doc_id"
        ddl.DataBind()
        ddl.Items.Insert("0", "--Document--")
    End Sub

    Private Sub AddNewInitiatorDocument(ByVal docid As String, ByVal roleid As String)
        If docid > 0 And roleid > 0 Then
            Dim strQuery As String = "exec uspGeneral_DocumentInitiator_I " & docid & ", " & roleid & ", " & CommonSite.UserName
            objdb.ExeNonQuery(strQuery)
            BindData()
        End If
    End Sub

    Private Sub DeleteInitiatorDocument(ByVal id As String)
        Dim strQuery As String = "delete Initiator_Document_Authorization where InitiatorDoc_Id= " & id
        objdb.ExeNonQuery(strQuery)
        BindData()
    End Sub
#End Region

End Class
