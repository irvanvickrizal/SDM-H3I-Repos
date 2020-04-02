
Partial Class BrowserInfo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ltlBrowserName.Text = Request.Browser.Type & ", " & Request.Browser.Platform

        ltlAllData.Text = "Type = " & Request.Browser.Type & "<br>"
        ltlAllData.Text &= "Name = " & Request.Browser.Browser & "<br>"
        ltlAllData.Text &= "Version = " & Request.Browser.Version & "<br>"
        ltlAllData.Text &= "Major Version = " & Request.Browser.MajorVersion & "<br>"
        ltlAllData.Text &= "Minor Version = " & Request.Browser.MinorVersion & "<br>"
        ltlAllData.Text &= "Platform = " & Request.Browser.Platform & "<br>"
        ltlAllData.Text &= "Is Beta = " & Request.Browser.Beta & "<br>"
        ltlAllData.Text &= "Is Crawler = " & Request.Browser.Crawler & "<br>"
        ltlAllData.Text &= "Is AOL = " & Request.Browser.AOL & "<br>"
        ltlAllData.Text &= "Is Win16 = " & Request.Browser.Win16 & "<br>"
        ltlAllData.Text &= "Is Win32 = " & Request.Browser.Win32 & "<br>"
        ltlAllData.Text &= "Supports Frames = " & Request.Browser.Frames & "<br>"
        ltlAllData.Text &= "Supports Tables = " & Request.Browser.Tables & "<br>"
        ltlAllData.Text &= "Supports Cookies = " & Request.Browser.Cookies & "<br>"
        ltlAllData.Text &= "Supports VB Script = " & Request.Browser.VBScript & "<br>"
        ltlAllData.Text &= "Supports JavaScript = " & Request.Browser.JavaScript & "<br>"
        ltlAllData.Text &= "Supports Java Applets = " & Request.Browser.JavaApplets & "<br>"
        ltlAllData.Text &= "CDF = " & Request.Browser.CDF & "<br>"
    End Sub
End Class
