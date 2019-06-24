select * from B_DictType

--truncate table B_DictType

--本地有两个库，代码结构一致，eg：bomonetemp 和 bomone
--实现 本地跨库操作
SET IDENTITY_INSERT [dbo].[B_DictType] ON 
GO
insert into bomonetemp.dbo.B_DictType(DT_Row, DT_Type, DT_TypeName, DT_TypeNameEn, DT_Order, Rflag, CreateACT_ID, CreateDate, UpdateACT_ID, 
                UpdateDate)  select *  from bomone.dbo.B_DictType
Go
set IDENTITY_INSERT   bomonetemp.dbo.B_DictType  OFF
GO

--远程数据库的地址：192.168.10.200，数据库的名称（bomone）
--远程 跨库操作

--创建链接服务器 
exec sp_addlinkedserver   'ITSV', ' ', 'SQLOLEDB ', '192.168.10.123' 
exec sp_addlinkedsrvlogin  'ITSV', 'false ',null, 'sa', '123456' 
--查询示例
select * from openrowset( 'SQLOLEDB ', '192.168.10.123'; 'sa'; '123456',bomone.dbo.S_organization_setting) 
select * from ITSV.OrganizeUser.dbo.S_organization_setting
--以后不再使用时删除链接服务器 
exec sp_dropserver  'ITSV', 'droplogins' 

--参考的原文链接：https://bbs.csdn.net/topics/390815669?page=1
