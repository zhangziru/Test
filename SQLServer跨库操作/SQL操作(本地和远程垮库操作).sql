select * from B_DictType

--truncate table B_DictType

--�����������⣬����ṹһ�£�eg��bomonetemp �� bomone
--ʵ�� ���ؿ�����
SET IDENTITY_INSERT [dbo].[B_DictType] ON 
GO
insert into bomonetemp.dbo.B_DictType(DT_Row, DT_Type, DT_TypeName, DT_TypeNameEn, DT_Order, Rflag, CreateACT_ID, CreateDate, UpdateACT_ID, 
                UpdateDate)  select *  from bomone.dbo.B_DictType
Go
set IDENTITY_INSERT   bomonetemp.dbo.B_DictType  OFF
GO

--Զ�����ݿ�ĵ�ַ��192.168.10.200�����ݿ�����ƣ�bomone��
--Զ�� ������

--�������ӷ����� 
exec sp_addlinkedserver   'ITSV', ' ', 'SQLOLEDB ', '192.168.10.123' 
exec sp_addlinkedsrvlogin  'ITSV', 'false ',null, 'sa', '123456' 
--��ѯʾ��
select * from openrowset( 'SQLOLEDB ', '192.168.10.123'; 'sa'; '123456',bomone.dbo.S_organization_setting) 
select * from ITSV.OrganizeUser.dbo.S_organization_setting
--�Ժ���ʹ��ʱɾ�����ӷ����� 
exec sp_dropserver  'ITSV', 'droplogins' 

--�ο���ԭ�����ӣ�https://bbs.csdn.net/topics/390815669?page=1
