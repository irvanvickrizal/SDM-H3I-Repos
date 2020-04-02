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
        Dim dtRegion As DataTable = objBO.ReportsForSite(context.Request.QueryString.Get("pono"))
        Dim objBitmap As New Bitmap(200, 500)
        objBitmap = CreatePieChart(500, dtRegion)
        objBitmap.Save(context.Response.OutputStream, ImageFormat.Jpeg)
        objBitmap.Dispose()
       
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
   
    Public Function CreateColumnToRow(ByVal ds As DataTable) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Total", GetType(Integer))
        dt.Columns.Add("DisplayName", GetType(String))

        Dim dr1 As DataRow

        dr1 = dt.NewRow()
        If ds.Rows(0)("TotalSiteIntegration") > ds.Rows(0)("TotalActive") Then
            dr1(0) = (ds.Rows(0)("TotalSiteIntegration") - ds.Rows(0)("TotalActive"))

        Else
            dr1(0) = (ds.Rows(0)("TotalActive") - ds.Rows(0)("TotalSiteIntegration"))

        End If
        dr1(1) = "Total Site Documents Not Started"
        dt.Rows.Add(dr1)

        Dim dr As DataRow
        dr = dt.NewRow()
        dr(0) = ds.Rows(0)("TotalSiteDocNotStarted")
        dr(1) = "Total Site Documents in Progress"
        dt.Rows.Add(dr)


        Dim dr2 As DataRow

        dr2 = dt.NewRow()
        dr2(0) = ds.Rows(0)("TotalSiteCompleteDocuments")
        dr2(1) = "Total Site Completed Document"
        dt.Rows.Add(dr2)

       
        Return dt

    End Function
    Private Function CreatePieChart(ByVal width As Integer, ByVal Ds As DataTable) As Bitmap


        Dim title As String
        Dim total As Single
        Dim dt As DataTable = CreateColumnToRow(Ds)
        title = " Total Require Document : " + total.ToString
        Dim currentDegree As Single = 0.0F

        ' we need to create fonts for our legend and title 
        Dim fontLegend As New Font("Verdana", 7), fontTitle As New Font("Verdana", 8, FontStyle.Bold)



        Const bufferSpace As Integer = 10
        Dim legendHeight As Integer = fontLegend.Height * (dt.Rows.Count + 5) + bufferSpace
        Dim titleHeight As Integer = fontTitle.Height + bufferSpace
        Dim height As Integer = (width - 300) + legendHeight + titleHeight + bufferSpace
        Dim pieHeight As Integer = width - 300

        ' Create a rectange for drawing our pie 



        ' Create a Bitmap instance 
        Dim objBitmap As New Bitmap(width, height)
        Dim objGraphics As Graphics = Graphics.FromImage(objBitmap)

        Dim blackBrush As New SolidBrush(Color.Black)

        ' Put a white background in 
        objGraphics.FillRectangle(New SolidBrush(Color.White), 0, 0, width, height)
        ' find the total of the numeric data 



        ' Create our pie chart, start by creating an ArrayList of colors 
        Dim colors As New ArrayList()
        Dim rnd As New Random()
        colors.Add(New SolidBrush(Color.BurlyWood))
        colors.Add(New SolidBrush(Color.Green))
        colors.Add(New SolidBrush(Color.BlueViolet))





        For iLoop As Integer = 0 To dt.Rows.Count - 1
            total += dt.Rows(iLoop)("Total")
        Next


        objGraphics.SmoothingMode = SmoothingMode.HighSpeed
        Dim PieRect As New ArrayList()
        PieRect.Add(New Rectangle(0, titleHeight, width - 300, pieHeight))
        PieRect.Add(New Rectangle(0, titleHeight, width - 300, pieHeight))
        PieRect.Add(New Rectangle(0, titleHeight, width - 300, pieHeight))
        ' maintain a one-to-one ratio 
        For iLoop As Integer = 0 To dt.Rows.Count - 1

            objGraphics.FillPie(DirectCast(colors(iLoop), SolidBrush), DirectCast(PieRect(iLoop), Rectangle), currentDegree, Convert.ToSingle(dt.Rows(iLoop)("Total")) / total * 360)
            ' increment the currentDegree 
            currentDegree += Convert.ToSingle((dt.Rows(iLoop)("Total")) / total * 360)

        Next


        objGraphics.DrawRectangle(New Pen(Color.Black, 2), 230, (height - legendHeight - 150), width - 230, legendHeight - 50)
        For iLoop As Integer = 0 To dt.Rows.Count - 1
            objGraphics.FillRectangle(DirectCast(colors(iLoop), SolidBrush), 250, (height - 150 - legendHeight + fontLegend.Height * iLoop + 5), 10, 10)


            objGraphics.DrawString((DirectCast(dt.Rows(iLoop)("DisplayName"), String) & " - ") + Convert.ToString(dt.Rows(iLoop)("Total")), fontLegend, blackBrush, 270, (height - legendHeight + fontLegend.Height * iLoop + 3 - 150))

        Next



       
        objGraphics.Dispose()
        Return objBitmap

    End Function
End Class