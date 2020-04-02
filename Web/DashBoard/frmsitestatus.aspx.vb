Imports BusinessLogic
Imports Common
Imports system.Data
Partial Class frmsitestatus
    Inherits System.Web.UI.Page
    Dim objBO As New BODashBoard
    Dim objBL As New BODDLs
    Dim objBoWF As New BOWTDoc

    Dim dt As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            BindTd()
            BindDD()
            BindData()
        End If
        'Response.Write(CommonSite.GetDashBoardLevel())
    End Sub
    Sub BindTd()
        tdArea.Style.Add("display", "none")
        tdRegion.Style.Add("display", "none")
        tdZone.Style.Add("display", "none")
        tdSite.Style.Add("display", "none")
    End Sub
    Sub BindDD()
        If CommonSite.GetDashBoardLevel() = 0 Then
            objBL.fillDDL(DDArea, "codArea", False, Constants._DDL_Default_Select)
            tdArea.Style.Add("display", "")
        ElseIf CommonSite.GetDashBoardLevel() = 1 Then
            objBL.fillDDL(DDArea, "codArea", False, Constants._DDL_Default_Select)
            tdArea.Style.Add("display", "")
        ElseIf CommonSite.GetDashBoardLevel() = 2 Then
            objBL.fillDDL(DDRegion, "codRegion", False, Constants._DDL_Default_Select)
            tdRegion.Style.Add("display", "")
        ElseIf CommonSite.GetDashBoardLevel() = 3 Then
            objBL.fillDDL(DDZone, "codZone", False, Constants._DDL_Default_Select)
            tdZone.Style.Add("display", "")
        ElseIf CommonSite.GetDashBoardLevel() = 4 Then
            objBL.fillDDL(DDSite, "codSite", False, Constants._DDL_Default_Select)
            tdSite.Style.Add("display", "")
        Else
            ' objBL.fillDDL(DDArea, "codArea", False, Constants._DDL_Default_Select)
        End If
    End Sub
    Sub BindData(Optional ByVal intSiteId As Integer = 0)
        dt = objBO.uspDashBoardSiteStatusLevel(CommonSite.GetDashBoardLevel(), CommonSite.GetDashBoardLevelId(), intSiteId)
        grdsitestatus.DataSource = dt
        grdsitestatus.PageSize = Convert.ToInt32(Session("Page_size"))
        grdsitestatus.DataBind()
    End Sub

    
    Protected Sub grdsitestatus_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdsitestatus.PageIndexChanging
        grdsitestatus.PageIndex = e.NewPageIndex
        If Not DDSite.SelectedValue = "" Then
            BindData(DDSite.SelectedItem.Value)
        Else
            BindData()
        End If

    End Sub

    Protected Sub DDArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        objBL.fillDDL(DDRegion, "codRegion", DDArea.SelectedItem.Value, False, Constants._DDL_Default_Select)
        tdRegion.Style.Add("display", "")
    End Sub

    Protected Sub DDZone_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        objBL.fillDDL(DDSite, "codsite", DDZone.SelectedItem.Value, False, Constants._DDL_Default_Select)
        tdSite.Style.Add("display", "")
    End Sub

    Protected Sub DDRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        objBL.fillDDL(DDZone, "codzone", DDRegion.SelectedItem.Value, False, Constants._DDL_Default_Select)
        tdZone.Style.Add("display", "")
    End Sub

    Protected Sub BtnFind_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        BindData(DDSite.SelectedItem.Value)
    End Sub
End Class
