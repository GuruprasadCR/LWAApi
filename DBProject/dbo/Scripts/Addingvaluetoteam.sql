insert dbo.Teams select 1, 'KKR'
Where not exists (select 1 from dbo.Teams where Teamid=1)

insert dbo.Teams select 2, 'PBKS'
Where not exists (select 2 from dbo.Teams where Teamid=2)

insert dbo.Teams select 3, 'SRH'
Where not exists (select 3 from dbo.Teams where Teamid=3)