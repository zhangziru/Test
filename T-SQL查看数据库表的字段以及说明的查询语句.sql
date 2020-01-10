--�鿴��ṹ�ı�ֵ����
Create function [dbo].[GetTableInfo](@tableName varchar(100))
returns table
as 
return(
SELECT   
 (case when a.colorder=1 then ddd.value else '' end) as "���������ģ�",--���������ͬ�ͷ��ؿ�  
 (case when a.colorder=1 then d.name else '' end) as ����,--���������ͬ�ͷ��ؿ�  
 (case when a.colorder=1 then ddd.value else '' end) as ��˵��,--���������ͬ�ͷ��ؿ�  
     a.colorder as �ֶ����,  
     a.name as �ֶ���,  
     (case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '��'else '' end) as �Ƿ�������ʶ,  
     (case when (SELECT count(*) FROM sysobjects--��ѯ����  
                     WHERE (name in  
                             (SELECT name FROM sysindexes   
                               WHERE (id = a.id)  AND (indid in  
                                     (SELECT indid FROM sysindexkeys  
                                       WHERE (id = a.id) AND (colid in  
                                         (SELECT colid FROM syscolumns  
                                          WHERE (id = a.id) AND (name = a.name)
           )  
                                      )
                    )
        )
           )
      )
         AND (xtype = 'PK'))>0 then '��' else '' end) as ����,--��ѯ����END  
 b.name as ����,  
 a.length as ռ���ֽ���,  
 COLUMNPROPERTY(a.id,a.name,'PRECISION') as  ����,  
 isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0) as С��λ��,  
 (case when a.isnullable=1 then '��'else '' end) as �����,  
 isnull(e.text,'') as Ĭ��ֵ, 
 isnull(g.[value],'') AS �ֶ�˵��   
 
 FROM syscolumns a 
 
 left join systypes b
 
 
 on a.xtype=b.xusertype  
 inner join sysobjects d   
 on a.id=d.id and d.xtype='U' and d.name<>'dtproperties'  
 LEFT OUTER JOIN( SELECT major_id, value 
     FROM sys.extended_properties 
     WHERE name='MS_Description' AND minor_id = 0)
    as ddd  ON a.id = ddd.major_id
 left join syscomments e  
 on a.cdefault=e.id  
 left join sys.extended_properties g  
 on a.id=g.major_id AND a.colid = g.minor_id  where d.name like @tableName
 --order by a.id,a.colorder
)