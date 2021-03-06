USE [HCPT_Demo]
GO
/****** Object:  StoredProcedure [dbo].[HCPT_uspTrans_DocApproved]    Script Date: 11/29/2018 8:00:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER procedure [dbo].[HCPT_uspTrans_DocApproved]
@sno as bigint,@wpid as varchar(50),@wfid as int, @TSKId as int, @RoleId as int,@userid as int,
@media nvarchar(10) --Added by Fauzan, 28 Nov 2018. To differentiate between email approval or web approval
as
declare @startdate as Datetime
declare @docid as int
declare @GetDate as DateTime
declare @nextsno as int
declare @pono as varchar(50), @siteid bigint, @version int
set @GetDate = GetDate()
begin
set nocount on;
select @startdate=StartDateTime,@docid = Doc_id from HCPT_WFTransaction where sno = @sno
select top 1 @pono=pono,@siteid=SiteId,@version=SiteVersion from poepmsitenew where workpackageid = @wpid
update HCPT_WFTransaction
set
enddatetime = @GetDate,
status = 0,
rstatus = 2,
lmby = @userid,
lmdt = @GetDate,
media = @media
where
sno = @sno

exec [HCPT_uspAuditTrail_I] @pono,@siteid,@docid,@version,@TSKId,1,@userid,@RoleId,@startdate,@GetDate,null,null,@wpid


-- Fix issue due to no change in next level
		if (select count(*) from HCPT_WFTransaction where WPID = @wpid and startdatetime is null and doc_id = @docid) > 0
			begin
			set @nextsno = (select top 1 wfc.sno from HCPT_WFTransaction wfc
							inner join twfdefinition twf on wfc.wf_id = twf.wfid and twf.tsk_id = wfc.tsk_id
								where WPID=@wpid and wfc.startdatetime is null and doc_id = @docid
							order by sorder asc)

			update HCPT_WFTransaction
			set startdatetime = @GetDate
			where
			sno = @nextsno
		end


end





