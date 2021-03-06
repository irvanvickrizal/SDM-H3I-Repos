USE [HCPT_Demo]
GO
/****** Object:  StoredProcedure [dbo].[HCPT_uspSiteDocListTaskNewSp]    Script Date: 11/29/2018 7:58:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[HCPT_uspSiteDocListTaskNewSp]
@usrid as int,@wpid as int,@tskdesc as varchar(10), 
@sno int = -1 --Added by Fauzan, 27 Nov 2018. For email approval
as
begin
	set nocount on;
	select distinct wf.sno, wf.Doc_id, wf.Tsk_Id,wf.Role_Id, sd.SW_Id, IsNUll(cd.DOCCode,'') as DocCode, cd.DOCName, sd.DocPath,
	po.pono, po.siteno, po.scope, sd.siteid, po.siteversion, po.workpkgid, sd.LMDT, sd.WF_Id 
	from HCPT_WFTransaction wf
		inner join podetails po on wf.wpid = po.workpkgid		
		inner join codsite cs on cs.site_no = po.siteno
		inner join SiteDoc sd on sd.siteid = cs.Site_Id and sd.Version = po.SiteVersion and sd.DocId = wf.Doc_id
		inner join CODDoc cd on cd.doc_id = wf.Doc_id
		inner join TaskGroup tg on tg.Tsk_id = wf.Tsk_Id and tg.TaskDesc = @tskdesc
	where 
	wf.Role_Id in(select usrRole from ebastusers_1 where usr_id = @usrid and ACC_Status = 'A') and 
	((cs.rgn_id in(select rgn_id from EBASTUserRole where usr_id = @usrid and LVLCode = 'R')) or
	 (cs.ara_id in(select ara_id from EBASTUserRole where usr_id = @usrid and LVLCode = 'A')) or
	  ((select count(*) from ebastusers_1 where usr_id = @usrid and LVLCode = 'N') > 0))
	and wf.StartDateTime is not null and wf.EndDateTime is null and wf.rstatus = 2 and wf.WPID = @wpid
	and (wf.sno = @sno or @sno = -1)
	order by sd.LMDt asc

end



 


 
