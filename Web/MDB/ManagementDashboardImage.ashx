<%@ WebHandler Language="VB" Class="ManagementDashboardImage" %>

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

Public Class ManagementDashboardImage : Implements IHttpHandler
   
    Dim objUtil As New DBUtil
    Private myImage As Bitmap
    Private g As Graphics
    Private p() As String = {"TI", "CME", "SITAC", "SIS"}
    Private towns() As String = {"TI", "CME", "SITAC", "SIS"}
    Private myBrushes(4) As Brush
    Dim dt As New DataTable()
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "image/jpeg"
        'Dim dtRegion As DataTable = objBO.usp_Dashboard(0, 1)
        'Dim objBitmap As New Bitmap(200, 500)
        'objBitmap = CreatePieChart(200, dtRegion)
        'objBitmap.Save(context.Response.OutputStream, ImageFormat.Jpeg)
        'objBitmap.Dispose()
        'CreateColumnToRow()
        dt = objUtil.ExeQueryDT("exec uspManagementDashboardImageCreation", "DATAbase")

        Try
            ' Create an in-memory bitmap where you will draw the image. 
            ' The Bitmap is 300 pixels wide and 200 pixels high.
            myImage = New Bitmap(500, 375, PixelFormat.Format32bppRgb)

            ' Get the graphics context for the bitmap.
            g = Graphics.FromImage(myImage)

            '   Create the brushes for drawing
            createBrushes()
        Catch ex As Exception
            Throw ex
        End Try
        Try
            ' Set the background color and rendering quality.
            g.Clear(Color.WhiteSmoke)

            '   draws the barchart
            drawBarchart(g)

            ' Render the image to the HTML output stream.
            myImage.Save(context.Response.OutputStream, ImageFormat.Jpeg)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    Private Sub createBrushes()
        Try

            myBrushes(0) = New SolidBrush(Color.DarkGoldenrod)

            myBrushes(1) = New SolidBrush(Color.DarkViolet)
            myBrushes(2) = New SolidBrush(Color.Yellow)
            myBrushes(3) = New SolidBrush(Color.DarkOliveGreen)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub drawBarchart(ByVal g As Graphics)
        Try
            '   Variables declaration
            Dim i As Integer
            Dim xInterval As Integer = 75
            Dim width As Integer = 50
            Dim height As Integer
            Dim blackBrush As New SolidBrush(Color.Black)
            Dim string_format As New StringFormat
            string_format.Alignment = StringAlignment.Center
            string_format.LineAlignment = StringAlignment.Center
            string_format.FormatFlags = StringFormatFlags.DirectionVertical Or StringFormatFlags.DirectionRightToLeft
            Dim f As New Font("Verdana", 8, FontStyle.Bold)

            For i = 0 To dt.Rows.Count - 1
                height = ((dt.Rows(i)("total") * 10) \ 3) + 0

                '   divide by 10000 to adjust barchart to height of Bitmap

                '   Draws the bar chart using specific colours
                g.FillRectangle(myBrushes(i), xInterval * i + 50, 280 - height, width, height)

                '   label the barcharts
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit
                ' If (dt.Rows(i)("total") = 1) Then
                g.DrawString(dt.Rows(i)("DisplayName"), f, Brushes.Black, xInterval * i + 60 + (width / 3), 150, string_format)
                '   Draw the scale
                g.DrawString(dt.Rows(i)("total"), New Font("Verdana", 8, FontStyle.Bold), Brushes.Black, 0, 280 - height)

                '   Draw the axes
                g.DrawLine(Pens.Brown, 40, 10, 40, 290)         '   y-axis
                g.DrawLine(Pens.Brown, 20, 280, 490, 280)       '   x-axis
            Next
            For i = 0 To 3
                g.FillRectangle(myBrushes(Convert.ToInt32(i)), 25, 375 - (i * 20) - 20, 10, 10)
                g.DrawString(p(i).ToString() & " - " + Convert.ToString(dt.Rows(i)("total")), New Font("Verdana", 8, FontStyle.Bold), Brushes.Black, 50, 375 - (i * 20) - 20)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class