Imports BusinessLogic
Imports Entities
Imports Common
Imports System.Data
Partial Class MDApproval1
    Inherits System.Web.UI.Page
    Dim objmail As New TakeMail
    Dim ObjUtil As New DBUtil
    Dim it, docid, xval, yval, pageno, siteid, version, wftsno, intHeight, intWidth, dguserid, roleid As Integer
    Dim pono, docpath, dgsusername, strs As String
    Dim np As String = "12345678"
    Dim dtr As DataTable
    Dim dtsign As New DataTable
    Dim DigitalSign_Result As String = "success"
    Dim dt As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        dtsign = ObjUtil.ExeQueryDT("select top 1 * from wfMultipledocApprove where rstatus in (1,2) order by sno desc", "wfMultipledocApprove")
        If dtsign.Rows.Count > 0 Then
            siteid = dtsign.Rows(it).Item("siteid")
            version = dtsign.Rows(it).Item("version")
            docid = dtsign.Rows(it).Item("docid")
            docpath = dtsign.Rows(it).Item("docpath").ToString
            dguserid = dtsign.Rows(it).Item("dguserid")
            roleid = dtsign.Rows(it).Item("roleid")
            dgsusername = dtsign.Rows(it).Item("dgsusername")
            wftsno = dtsign.Rows(it).Item("wftsno")
            xval = dtsign.Rows(it).Item("xval")
            yval = dtsign.Rows(it).Item("yval")
            pageno = dtsign.Rows(it).Item("pageno")
            intHeight = 50
            intWidth = 150
            Dim err As Boolean = False
            'If docid = ConfigurationManager.AppSettings("BASTID") Then
            '    Dim bi As Integer = ObjUtil.ExeQueryScalar("exec BASTfinalSignValidation " & wftsno & "," & ConfigurationManager.AppSettings("BASTID") & "")
            '    If bi = 1 Then
            '        ObjUtil.ExeNonQuery("update wfMultipledocApprove set status='" & "Dependant Documents not yet finish Processflow cannot sign BAST" & "' , rstatus=1  where siteid=" & siteid & " and version=" & version & " and wftsno=" & wftsno & "")
            '        err = True
            '    End If
            'End If
            'If docid = ConfigurationManager.AppSettings("BAUTID") Then
            '    Dim bi As Integer = ObjUtil.ExeQueryScalar("exec BAUTfinalSignValidation " & wftsno & "," & ConfigurationManager.AppSettings("BAUTID") & "")
            '    If bi = 1 Then
            '        ObjUtil.ExeNonQuery("update wfMultipledocApprove set status='" & "Dependant Documents not yet finish Processflow cannot sign BAUT" & "' , rstatus=1  where siteid=" & siteid & " and version=" & version & " and wftsno=" & wftsno & "")
            '        err = True
            '    End If
            'End If
            If err = False Then
                Dim Flags As Integer = 0
                Dim IntCount As Integer = 0
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_GRAPHICAL_IMAGE
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_SIGNED_BY
                If 1 = 0 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_REASON
                If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME
                Dim strResult As String
                strResult = SAPIWrapper.SAPI_sign_file(ConfigurationManager.AppSettings("FPath") + docpath, Nothing, dgsusername, _
                np, pageno, xval, yval, intHeight, intWidth, False, "eBAST", Flags, "")
                If (strResult = DigitalSign_Result) Then
                    If (docid = CommonSite.BAUTID) Then
                        Dim strsql As String = "Exec uspGetBautXY " & wftsno & ",'" & pono & "'"
                        dt = ObjUtil.ExeQueryDT(strsql, "digilist")
                        If (dt.Rows.Count > 0) Then
                            If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_GRAPHICAL_IMAGE
                            If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_SIGNED_BY
                            If 1 = 0 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_REASON
                            If 1 = 1 Then Flags = Flags Or SAPIWrapper.SAPI_ENUM_DRAWING_ELEMENT_TIME
                            Dim strResult1 As String = SAPIWrapper.SAPI_sign_file(ConfigurationManager.AppSettings("FPath") + docpath, Nothing, dgsusername, _
                            np, dt.Rows(0)("pageno"), dt.Rows(0)("xposition"), dt.Rows(0)("yposition"), intHeight, intWidth, False, "BAUT sign" & wftsno, Flags, "BAUT sign" & wftsno)
                        End If
                    End If
                    Dim i As Integer = -1
                    i = ObjUtil.ExeQueryScalar("Exec [uspDocApproved] " & wftsno & "," & dguserid & ",'" & dgsusername & "'," & roleid & "," & ConfigurationManager.AppSettings("BAUTID") & "," & ConfigurationManager.AppSettings("BASTID") & "," & ConfigurationManager.AppSettings("BAST2ID"))
                    If i = 0 Then
                        ObjUtil.ExeNonQuery("update wfMultipledocApprove set status='" & "Error while doing Transaction" & "' , rstatus=1  where siteid=" & siteid & " and version=" & version & " and wftsno=" & wftsno & "")
                    ElseIf i = 1 Then
                        strs = "SELECT ISNULL(USERID,1)USERID,tsk_id,site_id,siteversion,(select usrtype from ebastusers_1 where usr_id=USERID) as utype,docid  FROM WFTRANSACTION WHERE docid='" & docid & "'" & _
                               " AND  SITE_ID=" & siteid & " AND SITEVERSION = " & version & " AND STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL AND RSTATUS=2"
                        dtr = ObjUtil.ExeQueryDT(strs, "ddd")
                        If dtr.Rows.Count > 0 Then
                            Dim nextid As Integer
                            If docid = ConfigurationManager.AppSettings("BAUTID") Then 'means document in BAUT
                                nextid = dtr.Rows(0).Item(0).ToString
                                'make under BAUT docs as status=2
                                ObjUtil.ExeNonQuery("update wftransaction set rstatus=2  where enddatetime is null and rstatus=4 and docid in (select doc_id from coddoc where parent_id ='" & ConfigurationManager.AppSettings("BAUTID") & "') and site_id='" & siteid & "' and siteversion='" & version & "'  ")
                            ElseIf docid = ConfigurationManager.AppSettings("BASTID") Then 'means document in BAST
                                nextid = dtr.Rows(0).Item(0).ToString
                                'added by satish on 26th octber2010 to stop BAST going to review for agus it should go along with other approve( 2 docs approve , 1 doc for revire to sgus)
                                ObjUtil.ExeNonQuery("update wftransaction set rstatus=4  where enddatetime is null and startdatetime is not null and docid ='" & ConfigurationManager.AppSettings("BASTID") & "' and site_id='" & siteid & "' and siteversion='" & version & "'  ")
                            End If
                        End If
                        If docid = ConfigurationManager.AppSettings("BASTID") Then
                            Dim t As Integer = 0
                            t = ObjUtil.ExeQueryScalar("select count(*) from wftransaction where STARTDATETIME IS NOT NULL AND ENDDATETIME IS NULL and site_id= " & siteid & " and siteversion=" & version & "  and docid=" & ConfigurationManager.AppSettings("BASTID")) '1032
                            If t <> 0 Then
                                Dim swid As Integer = 0
                                swid = ObjUtil.ExeQueryScalar("select top 1 sw_id from sitedoc where siteid= " & siteid & " and version=" & version & "  and docid=" & ConfigurationManager.AppSettings("WCTRBASTID"))
                                ObjUtil.ExeNonQuery("update odwctrbast set bastsubdate=getdate() where swid= " & swid)
                                ObjUtil.ExeNonQuery("update sitedoc set rstatus=2 where SITEID='" & siteid & "' AND VERSION = '" & version & "' and docid=" & ConfigurationManager.AppSettings("WCTRBASTID"))
                                ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript123", "gopage(" & swid & ");", True)
                            End If
                        End If
                        ObjUtil.ExeNonQuery("update wfMultipledocApprove set status='" & "Sign Success" & "' , rstatus=0  where siteid=" & siteid & " and version=" & version & " and wftsno=" & wftsno & "")
                    End If
                Else
                    ObjUtil.ExeNonQuery("update wfMultipledocApprove set status='" & strResult & "' , rstatus=1  where siteid=" & siteid & " and version=" & version & " and wftsno=" & wftsno & "")
                End If
            End If
        End If
        If Request.QueryString("rdx") = "-1" Then
            ObjUtil.ExeNonQuery("delete wfMultipledocApprove where rstatus=0")
            Response.Write("Multiple Approval Completed")
            ScriptManager.RegisterStartupScript(Page, GetType(UpdatePanel), "MyScript", "CloseWindow();", True)
        Else
            Dim url As String
            url = "window.location='MDApproval2.aspx?rdx=-1"
            Response.Write("<script language='JavaScript'>" & url & "';</script>")
        End If
    End Sub
End Class
