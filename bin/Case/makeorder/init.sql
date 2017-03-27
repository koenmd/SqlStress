
use run
go


create table testorder
(
   serverid int,
   orderdate int,
   ordersno int,
   orderid  char(10),
   market  char(1),
   stkcode char(6),
   orderqty int,
   orderprice numeric(10,2),
   remark char(64),
   --constraint testorderrec_pk primary key nonclustered(ordersno, orderdate, serverid)
)
go

create procedure test_testorder
as
begin
insert into run.dbo.testorder(serverid, orderdate, ordersno, market, stkcode, orderqty, orderprice) values (1, 20161216, 10, '0', '600446', 100, 12.0)
end
go


create procedure test_runtestorder
as
begin
insert into run..testorder(serverid, orderdate, ordersno, market, stkcode, orderqty, orderprice) values (1, 20161216, 10, '0', '600446', 100, 12.0)
end
go

