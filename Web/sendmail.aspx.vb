Imports Common
Imports System.Data
Imports System.IO
Partial Class sendmail
    Inherits System.Web.UI.Page
    Dim ti2gdt, ti3gdt, cme2gdt, cme3gdt, sis2gdt, sis3gdt, sitac2gdt, sitac3gdt As New DataTable
    Dim ti2gdtp, ti3gdtp, cme2gdtp, cme3gdtp, sis2gdtp, sis3gdtp, sitac2gdtp, sitac3gdtp As New DataTable
    Dim objutil As New DBUtil
    Dim m, y, i As Integer
    Dim kk, mname As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack = True Then
            m = DateTime.Now.Month
            y = DateTime.Now.Year
            mname = objutil.ExeQueryScalarString("select datename(mm,getdate())")

            lbl2g.Text = " Milestone Achievement Summary " & y & " PO's 2G"
            lbl3g.Text = " Milestone Achievement Summary " & y & " PO's 3G"

            'ti2g
            ti2gdt = objutil.ExeQueryDT("uspTI2Gemail " & m & "," & y & " ", "4scopes1")
            ti2g.DataSource = ti2gdt
            ti2g.DataBind()
            doit(ti2g)
            ti2g.Caption = "<b><font color=maroon>progressive - " & mname & " </font></b>"

            'ti2gp
            ti2gdtp = objutil.ExeQueryDT("uspTI2GemailP " & m & "," & y & " ", "4scopes1p")
            ti2gp.DataSource = ti2gdtp
            ti2gp.DataBind()
            doit(ti2gp)
            ti2gp.Caption = "<b><font color=maroon>cummulative upto - " & mname & " </font></b>"

            'cme2g
            cme2gdt = objutil.ExeQueryDT("uspCME2Gemail " & m & "," & y & " ", "4scopes2")
            cme2g.DataSource = cme2gdt
            cme2g.DataBind()
            doit(cme2g)
            cme2g.Caption = "<b><font color=maroon>progressive - " & mname & " </font></b>"

            'cme2gp
            cme2gdtp = objutil.ExeQueryDT("uspCME2Gemailp " & m & "," & y & " ", "4scopes2")
            cme2gp.DataSource = cme2gdtp
            cme2gp.DataBind()
            doit(cme2gp)
            cme2gp.Caption = "<b><font color=maroon>cummulative upto - " & mname & " </font></b>"




            'sis2g
            sis2gdt = objutil.ExeQueryDT("uspSIS2Gemail " & m & "," & y & " ", "4scopes3")
            sis2g.DataSource = sis2gdt
            sis2g.DataBind()
            doit(sis2g)
            sis2g.Caption = "<b><font color=maroon>progressive - " & mname & " </font></b>"


            'sis2gp
            sis2gdtp = objutil.ExeQueryDT("uspSIS2Gemailp " & m & "," & y & " ", "4scopes3p")
            sis2gp.DataSource = sis2gdtp
            sis2gp.DataBind()
            doit(sis2gp)
            sis2gp.Caption = "<b><font color=maroon>cummulative upto - " & mname & " </font></b>"


            'sitac2g
            sitac2gdt = objutil.ExeQueryDT("uspSITAC2Gemail " & m & "," & y & " ", "4scopes4")
            sitac2g.DataSource = sitac2gdt
            sitac2g.DataBind()
            doit(sitac2g)
            sitac2g.Caption = "<b><font color=maroon>progressive - " & mname & " </font></b>"

            'sitac2gp
            sitac2gdtp = objutil.ExeQueryDT("uspSITAC2Gemailp " & m & "," & y & " ", "4scopes4")
            sitac2gp.DataSource = sitac2gdtp
            sitac2gp.DataBind()
            doit(sitac2gp)
            sitac2gp.Caption = "<b><font color=maroon>cummulative upto - " & mname & " </font></b>"



            'ti3g
            ti3gdt = objutil.ExeQueryDT("uspTI3Gemail " & m & "," & y & " ", "4scopes5")
            ti3g.DataSource = ti3gdt
            ti3g.DataBind()
            doit(ti3g)
            ti3g.Caption = "<b><font color=maroon>progressive - " & mname & " </font></b>"

            'ti3gp
            ti3gdtp = objutil.ExeQueryDT("uspTI3Gemailp " & m & "," & y & " ", "4scopes5p")
            ti3gp.DataSource = ti3gdt
            ti3gp.DataBind()
            doit(ti3gp)
            ti3gp.Caption = "<b><font color=maroon>cummulative upto - " & mname & " </font></b>"


            'CME3G
            cme3gdt = objutil.ExeQueryDT("uspCME3Gemail " & m & "," & y & " ", "4scopes6")
            CME3G.DataSource = cme3gdt
            CME3G.DataBind()
            doit(CME3G)
            CME3G.Caption = "<b><font color=maroon>progressive - " & mname & " </font></b>"

            'CME3Gp
            cme3gdtp = objutil.ExeQueryDT("uspCME3Gemailp " & m & "," & y & " ", "4scopes6p")
            CME3Gp.DataSource = cme3gdtp
            CME3Gp.DataBind()
            doit(CME3Gp)
            CME3Gp.Caption = "<b><font color=maroon>cummulative upto - " & mname & " </font></b>"


            'SIS3G
            sis3gdt = objutil.ExeQueryDT("uspSIS3Gemail " & m & "," & y & " ", "4scopes7")
            SIS3G.DataSource = sis3gdt
            SIS3G.DataBind()
            doit(SIS3G)
            SIS3G.Caption = "<b><font color=maroon>progressive - " & mname & " </font></b>"


            'SIS3Gp
            sis3gdtp = objutil.ExeQueryDT("uspSIS3Gemailp " & m & "," & y & " ", "4scopes7p")
            SIS3Gp.DataSource = sis3gdtp
            SIS3Gp.DataBind()
            doit(SIS3Gp)
            SIS3Gp.Caption = "<b><font color=maroon>cummulative upto - " & mname & " </font></b>"




            'SITAC3G
            sitac3gdt = objutil.ExeQueryDT("uspSITAC3Gemail " & m & "," & y & " ", "4scopes8")
            SITAC3G.DataSource = sitac3gdt
            SITAC3G.DataBind()
            doit(SITAC3G)
            SITAC3G.Caption = "<b><font color=maroon>progressive - " & mname & " </font></b>"

            'SITAC3Gp
            sitac3gdtp = objutil.ExeQueryDT("uspSITAC3Gemailp " & m & "," & y & " ", "4scopes8p")
            SITAC3Gp.DataSource = sitac3gdtp
            SITAC3Gp.DataBind()
            doit(SITAC3Gp)
            SITAC3Gp.Caption = "<b><font color=maroon>cummulative upto - " & mname & " </font></b>"



        End If
    End Sub
    Sub doit(ByVal gv As GridView)
        For i = 0 To gv.Rows.Count - 1
            If gv.Rows(i).Cells(0).Text = kk Then
                gv.Rows(i).Cells(0).Text = ""
            Else
                kk = gv.Rows(i).Cells(0).Text
            End If
        Next
        kk = ""
    End Sub
End Class
