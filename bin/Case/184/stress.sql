--insert into run.dbo.testorder(serverid, orderdate, ordersno, market, stkcode, --orderqty, orderprice) values (1, 20161216, 10, '0', '600446', 100, 12.0)
into run.dbo.testorder(serverid, orderdate, ordersno, market, stkcode) values (1, 20170101, 19, @market, @stkcode)
exec run.dbo.test_testorder;