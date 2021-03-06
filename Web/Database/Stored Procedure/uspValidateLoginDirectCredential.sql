USE [HCPT_Demo]
GO
/****** Object:  StoredProcedure [dbo].[uspValidateLoginDirectCredential]    Script Date: 11/29/2018 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[uspValidateLoginDirectCredential]  
--Created by Fauzan
--Created on 27 Nov 2018
--Desc : Validate login for direct login
@usr int
as  
begin   
declare @usrid as integer   
set @usrid=-1   
select @usrid=usr_id from ebastusers_1 where RStatus=2 and Acc_Status='A' and USR_ID = @usr
if @usrid<>-1   
begin  
  --inserting into login log audit trail    
  insert into audittrail (userid, eventstarttime, remarks)    
  select usr_id, getdate(), 'login' from ebastusers_1 where usr_id=@usrid
  --  
  select 'valid',eu.USR_Id USR_Id,eu.[Name] [Name],eu.USRType USRType,eu.SRCID SRCID,eu.UsrRole UsrRole,eu.acc_status,   
  isnull(er.Ara_Id,0) as ARA_Id,isnull(er.RGN_Id,0) as RGN_Id,isnull(er.ZN_Id,0)as ZN_Id,isnull(er.Site_Id,0)as Site_Id,  
  eu.FirstTime_Login FirstTime_Login,eu.lvlcode lvlcode,eu.phoneno,eu.usrlogin,eu.usrPassword  
  from EBastUsers_1 as eu   
  left outer join ebastuserrole as er on er.usr_id=eu.usr_id   
  where eu.RStatus=2 and eu.Acc_Status='A' and eu.usr_id=@usrid    
end  
else  
begin    
  select 'invalid', * from ebastusers_1 where RStatus=2 and usr_id=@usrid
end    
end 