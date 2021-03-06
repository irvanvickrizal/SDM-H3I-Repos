USE [HCPT_Demo]
GO
/****** Object:  StoredProcedure [dbo].[uspValidatePasswordDateDirectCredential]    Script Date: 11/29/2018 8:02:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[uspValidatePasswordDateDirectCredential]
--Created by Fauzan
--Created on 27 Nov 2018
--Desc : Validate password date for direct login            
 @UsrId as int         
AS            
 Begin            
 if exists(select count(*) from EBastUsers_1  where RStatus = 2 and Acc_Status = 'A' and Approved = 'True'            
  And USR_ID = @UsrId)  
  begin  
     declare @count int  
     select @count= datediff(DD,getdate(),passwordexpdate) from ebastusers_1 where USR_ID = @UsrId  
     if @count < = 7 --warning message to change passord  
       begin   
             select 7 as status  
       end  
     else if @count=0 --expired  
       begin  
       update ebastusers_1 set usrpassword='FE4D5BC713A693B770B299547835837A' where USR_ID = @UsrId  
       select 0 as status   
       end  
     else  
      begin  
        select 2 as status  
      end   
  end  
 End