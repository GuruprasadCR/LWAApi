--insert dbo.Players select 1, 'Sachin',2
--Where not exists (select 1 from dbo.Players where Playerid=1)

--insert dbo.Players select 2, 'Sehwag',1
--Where not exists (select 2 from dbo.Players where Playerid=2)

--insert dbo.Players select 3, 'Dravid',3
--Where not exists (select 3 from dbo.Players where Playerid=3)

--insert dbo.Players select 4, 'Dravid',2
--Where not exists (select 4 from dbo.Players where Playerid=4)