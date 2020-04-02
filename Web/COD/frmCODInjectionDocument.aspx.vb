
Partial Class COD_frmCODInjectionDocument
    Inherits System.Web.UI.Page

    Private controller As New CODInjectionController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_Name") Is Nothing Then Response.Redirect("~/SessionTimeOut.aspx")
        If Not Page.IsPostBack Then
            BindData()
            BindInjections(DdlInjectionType)
            BindDocuments(-1, DdlDocuments)
        End If
    End Sub

    Protected Sub LbtSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtSave.Click
        If DdlInjectionType.SelectedIndex > 0 And DdlDocuments.SelectedIndex > 0 And Not String.IsNullOrEmpty(TxtPackageId.Text) Then
            If controller.IsAvalaiblePackageId(TxtPackageId.Text) = True Then
                If controller.IsAvalaibleDocInjection(Integer.Parse(DdlDocuments.SelectedValue), TxtPackageId.Text) = True Then
                    Dim strError As String = "Document " & DdlDocuments.SelectedItem.Text & " with package id " & TxtPackageId.Text & " already exist"
                    If Not String.IsNullOrEmpty(strError) Then
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "NotSelected('" & strError & "');", True)
                    End If
                Else
                    LblErrorMessage.Visible = False
                    Dim info As New CODInjectionDocInfo
                    Dim ddlinjectionid() As String = DdlInjectionType.SelectedValue.Split("-")
                    info.InjectionId = Integer.Parse(ddlinjectionid(0))
                    info.PackageId = TxtPackageId.Text
                    info.Docid = Integer.Parse(DdlDocuments.SelectedValue)
                    info.LMBY = CommonSite.UserId
                    info.Remarks = TxtRemarks.Text
                    Dim strResult As String = SaveData(info)
                    If strResult.ToLower().Equals("succeed") Then
                        ResetForm()
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "SucceedSave();", True)
                        BindData()
                    Else
                        ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "NotSelected('" & strResult & "');", True)
                    End If
                End If
            Else
                Dim strError As String = "PackageId not found"
                If Not String.IsNullOrEmpty(strError) Then
                    ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "NotSelected('" & strError & "');", True)
                End If
            End If
           
        Else
            Dim strerror As String = String.Empty
            If DdlInjectionType.SelectedIndex = 0 Then
                strerror = "Please Select InjectionType"
            End If
            If DdlDocuments.SelectedIndex = 0 Then
                If Not String.IsNullOrEmpty(strerror) Then
                    strerror += ", document first"
                Else
                    strerror = "Please select document first"
                End If
            End If

            If String.IsNullOrEmpty(TxtPackageId.Text) Then
                If Not String.IsNullOrEmpty(strerror) Then
                    strerror += ", insert packageid first"
                Else
                    strerror = "Please packageid first"
                End If
            End If

            If Not String.IsNullOrEmpty(strerror) Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "NotSelected('" & strerror & "');", True)
            End If
        End If
       
    End Sub

    Protected Sub DdlInjectionType_IndexChanged(ByVal sender As Object, ByVal ae As System.EventArgs) Handles DdlInjectionType.SelectedIndexChanged
        If DdlInjectionType.SelectedIndex > 0 Then
            Dim ddlinjectionid() As String = DdlInjectionType.SelectedValue.Split("-")
            BindDocuments(Integer.Parse(ddlinjectionid(1)), DdlDocuments)
        Else
            BindDocuments(-1, DdlDocuments)
        End If
    End Sub

    Protected Sub GvInjectionDocs_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GvDocInjection.RowCommand
        If e.CommandName = "deleteinjection" Then
            Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim LblDocId As Label = DirectCast(row.Cells(0).FindControl("LblDocId"), Label)
            Dim LblPackageId As Label = DirectCast(row.Cells(0).FindControl("LblPackageId"), Label)
            If controller.InjectionDoc_D(Integer.Parse(LblDocId.Text), LblPackageId.Text) = True Then
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "SucceedDelete();", True)
                BindData()
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "ErrorDeleted();", True)
            End If
        End If
    End Sub

    Protected Sub LbtMassUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LbtMassUpdate.Click
        Response.Redirect("../Admin/frmInjectionDocMassUpdate.aspx")
    End Sub

#Region "Custom Methods"
    Private Sub BindData()
        GvDocInjection.DataSource = controller.GetInjectionDocs()
        GvDocInjection.DataBind()
    End Sub

    Private Sub BindInjections(ByVal ddl As DropDownList)
        ddl.DataSource = controller.GetInjections(False)
        ddl.DataTextField = "InjectionName"
        ddl.DataValueField = "DdlInjectionId"
        ddl.DataBind()

        ddl.Items.Insert(0, "-- Injection type --")
    End Sub

    Private Sub BindDocuments(ByVal parentid As Integer, ByVal ddl As DropDownList)
        ddl.DataSource = controller.GetDocumentsByParentId(parentid)
        ddl.DataTextField = "docname"
        ddl.DataValueField = "docid"
        ddl.DataBind()

        ddl.Items.Insert(0, "-- Document --")
    End Sub

    Private Function SaveData(ByVal info As CODInjectionDocInfo) As String
        Return controller.InjectionDoc_I(info)
    End Function

    Private Sub ResetForm()
        BindInjections(DdlInjectionType)
        BindDocuments(-1, DdlDocuments)
        TxtPackageId.Text = ""
    End Sub

    Private Sub GvDocInjection_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvDocInjection.PageIndexChanging
        GvDocInjection.PageIndex = e.NewPageIndex
        BindData()
    End Sub

#End Region

End Class
