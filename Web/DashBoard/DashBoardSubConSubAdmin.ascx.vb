Imports BusinessLogic
Imports Common
Imports Entities
Imports System.Data
Partial Class Include_DashBoardSubConSubAdmin
    Inherits System.Web.UI.UserControl
    Dim objBOD As New BODDLs
    'Dim objBODP As New BOPDDLs
    Dim objET As New ETEBASTUsers
    Dim objBO As New BODashBoard
    Dim dt, dtdg As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            ' lblsrcname.Visible = False
            If Session("Region_Id") <> "" Then
                BindData()
            Else
                BindData()
            End If

        End If
    End Sub
    Sub BindData()
        CreateDatatable()
        grdDashBoard.PageSize = 14
        grdDashBoard.DataSource = dtdg
        grdDashBoard.DataBind()
        '// BindData1()
    End Sub
    Sub CreateDatatable()
        ' Create columns        

        dtdg.Columns.Add("SEC_Id", Type.GetType("System.Int32"))
        dtdg.Columns.Add("RegionName", Type.GetType("System.String"))
        dtdg.Columns.Add("TotalSite", Type.GetType("System.String"))
        dtdg.Columns.Add("Sec_Name", Type.GetType("System.String"))
        dtdg.Columns.Add("DocCount", Type.GetType("System.Int32"))
        dtdg.Columns.Add("NsnApprove", Type.GetType("System.Int32"))
        dtdg.Columns.Add("CustomerApprove", Type.GetType("System.Int32"))
        dtdg.Columns.Add("Complete", Type.GetType("System.Int32"))
        dtdg.Columns.Add("Remaining", Type.GetType("System.String"))
        dtdg.Columns.Add("TotalPerc", Type.GetType("System.Decimal"))
        Dim dtRegion As New DataTable()
        ' dtRegion = objBO.uspDashBoardRegion(Session("Area_Id"))
        dtRegion = objBO.uspDashBoardPoNo()
        For Each drowRegion As DataRow In dtRegion.Rows
            dt = objBO.uspDashBoardGetPoNo(drowRegion.Item("pono"))
            Dim i As Integer
            i = 0
            For Each drow As DataRow In dt.Rows
                ' Declare row       
                Dim myrow As DataRow
                ' create new row        
                myrow = dtdg.NewRow
                If i = 0 Then
                    myrow("SEC_Id") = drow.Item("SEC_Id")
                    myrow("RegionName") = drowRegion.Item("pono")
                    myrow("TotalSite") = drow.Item("TotalSite")
                    myrow("Sec_Name") = drow.Item("Sec_name")
                    myrow("DocCount") = drow.Item("DocCount")
                    myrow("NsnApprove") = drow.Item("NSNDocCount")
                    myrow("CustomerApprove") = drow.Item("CustomerDocCount")
                    myrow("Complete") = drow.Item("CompleteDocCount")
                    myrow("Remaining") = Convert.ToInt32(drow.Item("RemainingDocCount"))
                    Dim decPerc As New Decimal
                    If drow.Item("DocCount") <> "0" Then
                        decPerc = Convert.ToDecimal((Convert.ToInt32(drow.Item("CompleteDocCount")) * 100) / (Convert.ToInt32(drow.Item("RemainingDocCount")) + Convert.ToInt32(drow.Item("DocCount"))))
                        If Convert.ToInt32(decPerc) = 100 Then
                            myrow("TotalPerc") = "99.99"
                        Else
                            myrow("TotalPerc") = Format(decPerc, "#.00")
                        End If

                    Else
                        myrow("TotalPerc") = 0
                    End If
                Else
                    myrow("SEC_Id") = drow.Item("SEC_Id")
                    myrow("RegionName") = ""
                    myrow("TotalSite") = ""
                    myrow("Sec_Name") = drow.Item("Sec_name")
                    myrow("DocCount") = drow.Item("DocCount")
                    myrow("NsnApprove") = drow.Item("NSNDocCount")
                    myrow("CustomerApprove") = drow.Item("CustomerDocCount")
                    myrow("Complete") = drow.Item("CompleteDocCount")
                    myrow("Remaining") = Convert.ToInt32(drow.Item("RemainingDocCount"))
                    Dim decPerc As New Decimal
                    If drow.Item("DocCount") <> "0" Then
                        decPerc = Convert.ToDecimal((Convert.ToInt32(drow.Item("CompleteDocCount")) * 100) / (Convert.ToInt32(drow.Item("RemainingDocCount")) + Convert.ToInt32(drow.Item("DocCount"))))
                        If Convert.ToInt32(decPerc) = 100 Then
                            myrow("TotalPerc") = "99.99"
                        Else
                            myrow("TotalPerc") = Format(decPerc, "#.00")
                        End If

                    Else
                        myrow("TotalPerc") = 0
                    End If

                End If
                i = i + 1
                dtdg.Rows.Add(myrow)
            Next
        Next
    End Sub


    Protected Sub grdDashBoard_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDashBoard.PageIndexChanging
        grdDashBoard.PageIndex = e.NewPageIndex
        BindData()
    End Sub
End Class
