USE [HCPT_Demo]
GO
/****** Object:  StoredProcedure [dbo].[HCPT_uspTrans_GetTaskPending]    Script Date: 11/29/2018 7:59:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER procedure [dbo].[HCPT_uspTrans_GetTaskPending]
@usrid int,
@sno int = -1 --Added by Fauzan, 27 Nov 2018. For email approval
as
declare @usrRole int
begin
set nocount on;
set @usrRole = (select usrRole from ebastusers_1 where usr_id = @usrid)
Select CountUsrType, WPID, submitdate, pono, poname,siteno, sitename, siteidpo,sitenamepo,rgnname,ara_name 'areaname', scope,tsk_id from(
select count(*) as CountUsrType,WPID,
(select top 1 lmdt from sitedoc sd1 where sd1.siteid=cs.Site_Id and sd1.version = po.SiteVersion and sd1.DocId = wfs.Doc_id) 'submitdate',
po.pono, po.poname, po.siteno, po.sitename, po.siteidpo,po.sitenamepo,cr.rgnname, ara_name,po.scope,
wfs.Tsk_Id
from HCPT_WFTransaction wfs
	inner join podetails po on po.workpkgid = wfs.WPID
	inner join codsite cs on cs.site_no = po.siteno
	inner join sitedoc sd on sd.siteid = cs.site_id and sd.version = po.siteversion and sd.docid = wfs.doc_Id
	inner join codregion cr on cr.rgn_id = cs.rgn_id
	inner join codarea ca on ca.ara_id = cs.ara_id
where wfs.startdatetime is not null and wfs.enddatetime is null and sd.isuploaded = 1
and wfs.rstatus = 2 and wfs.status = 1 and wfs.role_id in(@usrRole)
and ((cs.ara_id in(select ara_id from ebastuserrole where usr_id = @usrid and lvlcode = 'A')) or
(cs.rgn_id in(select rgn_id from ebastuserrole where usr_id = @usrid and lvlcode = 'R')))
and po.status in('active','complete')
and (wfs.sno = @sno or @sno = -1)
group by wpid,siteno,SiteName,po.pono,cs.site_id,SiteVersion, doc_Id, poname,siteidpo,sitenamepo,RGNName,ara_name,scope,Tsk_Id
)
as tb1 order by submitdate asc

end









