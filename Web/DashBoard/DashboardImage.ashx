<%@ WebHandler Language="VB" Class="DashboardImage" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Collections
Imports System.Web.Security
Imports BusinessLogic
Imports Common
Imports Entities

Public Class DashboardImage : Implements IHttpHandler
    Dim objBOD As New BODDLs
    Dim objET As New ETEBASTUsers
    Dim objBO As New BODashBoard
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "image/jpeg"
        Dim dtRegion As DataTable = objBO.usp_Dashboard(0, 1)
        Dim objBitmap As New Bitmap(200, 500)
        objBitmap = CreatePieChart(200, dtRegion)
        objBitmap.Save(context.Response.OutputStream, ImageFormat.Jpeg)
        objBitmap.Dispose()
       
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    
    Private Function CreatePieChart(ByVal width As Integer, ByVal Ds As DataTable) As Bitmap

        Dim intRequre As Integer = 0
        Dim title As String
        Dim total As Single = 0.0F, tmp As Single = 0, nsn As Single, Customer As Single, UploadDocument As Single
        Dim dt As DataTable = CreateColumnToRow(Ds, total, UploadDocument, nsn, Customer)
        title = " Total Require Document : " + total.ToString
        Dim currentDegree As Single = 0.0F

        ' we need to create fonts for our legend and title 
        Dim fontLegend As New Font("Verdana", 7), fontTitle As New Font("Verdana", 8, FontStyle.Bold)



        Const bufferSpace As Integer = 10
        Dim legendHeight As Integer = fontLegend.Height * (dt.Rows.Count + 5) + bufferSpace
        Dim titleHeight As Integer = fontTitle.Height + bufferSpace
        Dim height As Integer = width + legendHeight + titleHeight + bufferSpace
        Dim pieHeight As Integer = width

        ' Create a rectange for drawing our pie 



        ' Create a Bitmap instance 
        Dim objBitmap As New Bitmap(width, height)
        Dim objGraphics As Graphics = Graphics.FromImage(objBitmap)

        Dim blackBrush As New SolidBrush(Color.Black)

        ' Put a white background in 
        objGraphics.FillRectangle(New SolidBrush(Color.White), 0, 0, width, height)
        ' find the total of the numeric data 

        Dim iLoop As Integer
        'For iLoop = 0 To dt.Rows.Count - 1
        '    tmp = Convert.ToSingle(dt.Rows(iLoop)("Total"))
        '    total += tmp
        'Next

        ' Create our pie chart, start by creating an ArrayList of colors 
        Dim colors As New ArrayList()
        Dim rnd As New Random()
        colors.Add(New SolidBrush(Color.BurlyWood))
        colors.Add(New SolidBrush(Color.Green))
        colors.Add(New SolidBrush(Color.BlueViolet))





        'For iLoop = 0 To dt.Rows.Count - 1
        '    colors.Add(New SolidBrush(Color.FromArgb(rnd.[Next](255), rnd.[Next](255), rnd.[Next](255))))
        'Next


        objGraphics.SmoothingMode = SmoothingMode.HighSpeed
        Dim PieRect As New ArrayList()
        PieRect.Add(New Rectangle(25, titleHeight, width - 25, pieHeight - 50))
        PieRect.Add(New Rectangle(25, titleHeight, width - 25, pieHeight - 50))
        PieRect.Add(New Rectangle(25, titleHeight, width - 25, pieHeight - 50))
        ' maintain a one-to-one ratio 
        For iLoop = 0 To dt.Rows.Count - 1
     
            objGraphics.FillPie(DirectCast(colors(iLoop), SolidBrush), DirectCast(PieRect(iLoop), Rectangle), currentDegree, Convert.ToSingle(dt.Rows(iLoop)("Total")) / total * 360)
            ' increment the currentDegree 
            currentDegree += Convert.ToSingle((dt.Rows(iLoop)("Total")) / total * 360)

        Next


        objGraphics.DrawRectangle(New Pen(Color.Black, 2), 0, height - legendHeight - 40, width, legendHeight)
        For iLoop = 0 To dt.Rows.Count - 1
            objGraphics.FillRectangle(DirectCast(colors(iLoop), SolidBrush), 5, height - 40 - legendHeight + fontLegend.Height * iLoop + 5, 10, 10)

            'If (iLoop = 2) Then
            '    objGraphics.DrawString(("Under Process Document - ") + Convert.ToString((dt.Rows(iLoop - 1)("Total") - dt.Rows(iLoop)("Total"))), fontLegend, blackBrush, 20, height - legendHeight + fontLegend.Height * iLoop + 3 - 40)
            'ElseIf (iLoop = 1) Then
            '    objGraphics.DrawString((DirectCast(dt.Rows(iLoop + 1)("DisplayName"), String) & " - ") + Convert.ToString(dt.Rows(iLoop + 1)("Total")), fontLegend, blackBrush, 20, height - legendHeight + fontLegend.Height * iLoop + 3 - 40)
            'Else

            objGraphics.DrawString((DirectCast(dt.Rows(iLoop)("DisplayName"), String) & " - ") + Convert.ToString(dt.Rows(iLoop)("Total")), fontLegend, blackBrush, 20, height - legendHeight + fontLegend.Height * iLoop + 3 - 40)
            'End If
        Next

        'display the total 
        objGraphics.DrawString("Document Approved by Customer : " & Convert.ToString(Customer), fontLegend, blackBrush, 5, height - fontLegend.Height - 5 - 40)
        objGraphics.DrawString("Document Approved by NSN : " & Convert.ToString(nsn), fontLegend, blackBrush, 5, height - fontLegend.Height - 20 - 40)
        objGraphics.DrawString("Total Upload Document : " & Convert.ToString(UploadDocument), fontLegend, blackBrush, 5, height - fontLegend.Height - 35 - 40)

        ' Create the title, centered 
        Dim stringFormat As New StringFormat()
        stringFormat.Alignment = StringAlignment.Far

        stringFormat.LineAlignment = StringAlignment.Center

        objGraphics.DrawString(title, fontTitle, blackBrush, New Rectangle(0, 0, width, titleHeight), stringFormat)

        ' Since we are outputting a Jpeg, set the 
        'ContentType appropriately 
        'context.Response.ContentType = "image/jpeg"

      
        'Dim oFp As System.Security.Permissions.FileIOPermission = New System.Security.Permissions.FileIOPermission(System.Security.Permissions.FileIOPermissionAccess.Write, Me.Server.MapPath("~/dashboard/"))
        'oFp.Assert()
        'Dim strImageName As String = "Pichart" + CommonSite.UserName() + DateTime.Now.Minute.ToString + ".jpg"
        'If (System.IO.File.Exists(Server.MapPath(".") + "/pichart/" + strImageName)) Then
        '    System.IO.File.Delete(Server.MapPath(".") + "/pichart/" + strImageName)
        '    objBitmap.Save(Server.MapPath(".") + "/pichart/" + strImageName, ImageFormat.Jpeg)
        'Else
        '    objBitmap.Save(Server.MapPath(".") + "/pichart/" + strImageName, ImageFormat.Jpeg)
        'End If
        ' PiChart.Src = "pichart.jpg"
        objGraphics.Dispose()
        Return objBitmap
    End Function
    Public Function CreateColumnToRow(ByVal ds As DataTable, ByRef Total As Single, ByRef intUploadDocument As Single, ByRef Nsn As Integer, ByRef Customer As Single) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Total", GetType(Integer))
        dt.Columns.Add("DisplayName", GetType(String))
        Dim intPendingUploadDocument As Integer = 0, intCompleteDocument As Integer = 0
        For intLoop As Integer = 0 To ds.Rows.Count - 1
            Total += Convert.ToInt32(ds.Rows(intLoop)("TotalDocument"))
            intUploadDocument += Convert.ToInt32(ds.Rows(intLoop)("TotalUploadDocument"))
            Nsn += Convert.ToInt32(ds.Rows(intLoop)("nsnApproved"))
            Customer += Convert.ToInt32(ds.Rows(intLoop)("CustomerApproved"))
            intPendingUploadDocument += Convert.ToInt32(ds.Rows(intLoop)("TotalDocument")) - Convert.ToInt32(ds.Rows(intLoop)("TotalUploadDocument"))
            intCompleteDocument += Convert.ToInt32(ds.Rows(intLoop)("completeDocument"))
        Next
        Dim dr1 As DataRow

        dr1 = dt.NewRow()
        dr1(0) = Total - intUploadDocument
        dr1(1) = "Pending For Upload Document"
        dt.Rows.Add(dr1)

        Dim dr As DataRow
        dr = dt.NewRow()
        If intUploadDocument < intCompleteDocument Then
            dr(0) = intCompleteDocument - intUploadDocument
        Else
            dr(0) = intUploadDocument - intCompleteDocument
        End If

        dr(1) = "Processing Document"
        dt.Rows.Add(dr)


        Dim dr2 As DataRow

        dr2 = dt.NewRow()
        dr2(0) = intCompleteDocument
        dr2(1) = "Complete Document"
        dt.Rows.Add(dr2)
        Return dt

    End Function

End Class