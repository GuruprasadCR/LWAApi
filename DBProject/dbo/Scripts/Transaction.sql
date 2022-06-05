

go

create procedure Spupdatename
as
begin
  begin Try
    begin Transaction
        update Players set Playername='Yuvraj'
        where Playerid=4
        commit transaction 
        print 'Transaction Committed'
        End try

        begin catch
        Rollback transaction
        print 'Transaction Rolledback'
        end catch
   end

   execute Spupdatename