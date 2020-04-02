Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class USR_frmUserView
    Inherits System.Web.UI.Page
    Dim objBOD As New BODDLs
    Dim objBODP As New BODDLs
    Dim objET As New ETEBASTUsers
    Dim objBO As New BOUserLD
    Dim dt As New DataTable
    Dim objBOS As New BOUserSetup
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("Page_size") Is Nothing Then Response.Redirect("~\SessionTimeout.aspx")
        If Session("Page_size") Is Nothing Then
            Response.Write("<script language=""JavaScript"">window.close();</script>")
        End If
        If Not IsPostBack Then
            objBODP.fillDDL(ddlUsertype, "TUserType1", False, "")
            objBODP.fillDDL(ddlRole, "TRole1", ddlUsertype.SelectedValue, False, "")
            binddata()
        End If
    End Sub
    Sub binddata()
        dt = New DataTable
        dt = objBO.uspEBASTUserD(Request.QueryString("ID"))
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                ddlUsertype.SelectedValue = .Item("USRTYPE").ToString
                lblUser.InnerText = ddlUsertype.SelectedItem.Text
                objBODP.fillDDL(ddlRole, "TRole1", ddlUsertype.SelectedValue, False, "")
                ddlRole.SelectedValue = .Item("USRROLE")
                lblRole.InnerText = ddlRole.SelectedItem.Text
                lblName.InnerText = .Item("NAME").ToString
                lblLogin.InnerText = .Item("USRLOGIN").ToString
                lblEmail.InnerText = .Item("EMAIL").ToString
                lblPhone.InnerText = .Item("PHONENO").ToString
                lblArea.InnerText = .Item("AREA").ToString
                lblRegion.InnerText = .Item("REGION").ToString
                lblZone.InnerText = .Item("ZONE").ToString
                lblSite.InnerText = .Item("SITE").ToString
                rowVisible(dt.Rows(0))
            End With
        End If
    End Sub
    Sub rowVisible(ByVal dtrow As DataRow)
        If dtrow.Item("Site").ToString <> "" Then
            If dtrow.Item("Zone").ToString <> "" Then
                If dtrow.Item("Region").ToString <> "" Then
                    If dtrow.Item("Area").ToString <> "" Then
                    Else
                        trArea.Visible = False
                        trRegion.Visible = False
                        trZone.Visible = False
                        trSite.Visible = False
                    End If
                Else
                    trRegion.Visible = False
                    trZone.Visible = False
                    trSite.Visible = False
                End If
            Else
                trZone.Visible = False
                trSite.Visible = False
            End If
        Else
            trSite.Visible = False
        End If



    End Sub
End Class
