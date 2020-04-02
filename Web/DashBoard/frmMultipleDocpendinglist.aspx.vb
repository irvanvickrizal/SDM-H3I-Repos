Imports BusinessLogic
Imports Entities
Imports Common
Imports AXmsCtrl
Imports System.Data
Partial Class frmMultipleDocpendinglist
    Inherits System.Web.UI.Page
    Dim objED As New ETDelete
    Dim objBD As New BODelete
    Dim objBo As New BODashBoard
    Dim dt As New DataTable
    Dim ObjUtil As New DBUtil
    Dim strsql As String
    Dim objDDL As New BODDLs
    Private strExpr As String = ViewState("SortExpression")
    Dim objsms As New SMSNew
    Dim objmail As New TakeMail
    'for service
    Dim it, docid, xval, yval, pageno, siteid, version, wftsno, intHeight, intWidth, dguserid, roleid As Integer
    Dim pono, docpath, dgsusername, strs As String
    Dim np As String = "12345678"
    Dim dtr As DataTable
    Dim dtsign As New DataTable
    Dim DigitalSign_Result As String = "success"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        txtUserName.Text = Session("User_Login")
        If Not Page.IsPostBack Then
            BindSite()
        End If
    End Sub
    Sub BindSite()
        strsql = "EXEC MultipleDocpendinglist  " & Session("User_Id")
        dt = ObjUtil.ExeQueryDT(strsql, "MultipleDocpendinglist")
        grdSite.DataSource = dt
        grdSite.DataBind()
    End Sub
    Protected Function SortDataTable(ByVal dataTable As DataTable, ByVal isPageIndexChanging As Boolean) As DataView
        If dataTable IsNot Nothing Then
            Dim dataView As New DataView(dataTable)
            If GridViewSortExpression <> String.Empty Then
                If isPageIndexChanging Then
                    dataView.Sort = String.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection)
                Else
                    dataView.Sort = String.Format("{0} {1}", GridViewSortExpression, GetSortDirection())
                End If
            End If
            Return dataView
        Else
            Return New DataView()
        End If
    End Function
    Private Property GridViewSortExpression() As String
        Get
            Return IIf(TryCast(strExpr, String) Is Nothing, String.Empty, TryCast(strExpr, String))
        End Get

        Set(ByVal value As String)
            strExpr = value
        End Set
    End Property
    Private Property GridViewSortDirection() As String
        Get
            Return IIf(TryCast(ViewState("SortDirection"), String) Is Nothing, "ASC", TryCast(ViewState("SortDirection"), String))
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property
    Private Function GetSortDirection() As String
        Select Case GridViewSortDirection
            Case "ASC"
                GridViewSortDirection = "DESC"
                Exit Select
            Case "DESC"
                GridViewSortDirection = "ASC"
                Exit Select
        End Select
        Return GridViewSortDirection
    End Function
    Sub checkall(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim mainchk As New CheckBox
        mainchk = grdSite.HeaderRow.Cells(0).FindControl("chkall")
        Dim i As Integer
        For i = 0 To grdSite.Rows.Count - 1
            Dim chk As New HtmlInputCheckBox
            chk = grdSite.Rows(i).Cells(0).FindControl("CheckBox1")
            If mainchk.Checked = True Then
                chk.Checked = True
            Else
                chk.Checked = False
            End If
        Next
    End Sub
    Protected Sub grdSite_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSite.PageIndexChanging
        BindSite()
        grdSite.DataSource = SortDataTable(TryCast(grdSite.DataSource, DataTable), True)
        grdSite.PageIndex = e.NewPageIndex
        grdSite.DataBind()
    End Sub
    Protected Sub grdSite_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdSite.Sorting
        GridViewSortExpression = e.SortExpression
        Dim pageIndex As Integer = grdSite.PageIndex
        BindSite()
        grdSite.DataSource = SortDataTable(TryCast(grdSite.DataSource, DataTable), False)
        grdSite.DataBind()
    End Sub
    Protected Sub lnkrequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkrequest.Click
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        Dim npwd As String = ""
        Dim msgdata As New StringBuilder
        If txtUserName.Text <> "" Then
            objsms.requestSMS(Session("User_Name"), Session("User_Login"), Request.QueryString("siteno"), Request.QueryString("pono"), Request.QueryString("docname"))
            loadingdiv.Style("display") = "none"
            Response.Write("<script>alert('Please check for your password in your phone');</script>")
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('Please keyin the user name and click request password');", True)
        End If
    End Sub
    Protected Sub grdSite_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSite.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(13).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(13).Visible = False
        End If
    End Sub
    Protected Sub btnProceed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProceed.Click
        Dim j As Integer
        Dim sno As String = ""
        Dim checkedSites As String = ""
        Dim objdb As New DBUtil
        j = objdb.ExeQueryScalar("select count(*) from dgpassword where username='" & txtUserName.Text & "' and password='" & txtPassword.Text & "'")
        If j > 0 Then
            For j = 0 To grdSite.Rows.Count - 1
                Dim chk As New HtmlInputCheckBox
                chk = grdSite.Rows(j).Cells(0).FindControl("CheckBox1")
                If chk.Checked = True Then
                    strsql = "EXEC uspMultipleDocpendingIU '" & grdSite.Rows(j).Cells(6).Text & "'," & grdSite.Rows(j).Cells(3).Text & ",'" & grdSite.Rows(j).Cells(4).Text & "'," & grdSite.Rows(j).Cells(7).Text & ",'" & grdSite.Rows(j).Cells(8).Text & "'," & grdSite.Rows(j).Cells(9).Text & "," & Session("User_Id") & "," & Session("Role_Id") & ",'" & txtUserName.Text & "'," & grdSite.Rows(j).Cells(10).Text & "," & grdSite.Rows(j).Cells(11).Text & "," & grdSite.Rows(j).Cells(12).Text & ",2,'" & Session("User_Name") & "'"
                    ObjUtil.ExeQueryScalar(strsql)
                End If
            Next
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "alert('invalid  userid or password');", True)
        End If
        BindSite()
    End Sub
    Protected Sub grdSite_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSite.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbl As Label = CType(e.Row.FindControl("lblNo"), Label)
                lbl.Text = (grdSite.PageIndex * grdSite.PageSize) + (e.Row.RowIndex + 1) & ".&nbsp;"
        End Select
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim url As String = "../PO/frmViewDocument.aspx?id=" & e.Row.Cells(13).Text
            e.Row.Cells(5).Text = "<a href='#' onclick=""window.open('" & url & "','mywindow','status=yes,menubar=no,scrollbars=yes,resizable=yes,width=700px,height=600px')"">" & e.Row.Cells(5).Text & "</a>"
        End If
    End Sub
End Class

