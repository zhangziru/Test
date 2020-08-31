#                  [     SQL Server merge用法        ](https://www.cnblogs.com/Vincent-yuan/p/11521229.html)             

有两个表名：source 表和 target 表，并且要根据 source 表中匹配的值更新 target 表。

有三种情况：

- source 表有一些 target 表不存在的行。在这种情况下，需要将 source 表中的行插入到 target 中。
- target 表有一些 source表不存在的行。这种情况下，需要从 target 表中删除行。
- source 表的某些行具有与 target 表中的行相同的键。但是，这些行在非键列中具有不同的值。这种情况下，需要使用来自 source 表中的值更新 target 表中的行。

下图，说明了 source 表和 target 表 的一些操作：插入，更新，删除：

![img](https://img2018.cnblogs.com/blog/1182288/201909/1182288-20190915094054990-498735614.png)

如果单独使用 INSERT, UPDATE和DELETE语句，则需要三个单独的语句，来使 source 表中的匹配行的数据更新到 target表。

但是，使用 merge 可以同时执行三个操作。下面是 merge语句的语法：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```mssql
MERGE target_table USING source_table
ON merge_condition
WHEN MATCHED
    THEN update_statement
WHEN NOT MATCHED
    THEN insert_statement
WHEN NOT MATCHED BY SOURCE
    THEN DELETE;
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

使用示例：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```SQL
CREATE TABLE category (
    category_id INT PRIMARY KEY,
    category_name VARCHAR(255) NOT NULL,
    amount DECIMAL(10 , 2 )
);

INSERT INTO category(category_id, category_name, amount)
VALUES(1,'Children Bicycles',15000),
    (2,'Comfort Bicycles',25000),
    (3,'Cruisers Bicycles',13000),
    (4,'Cyclocross Bicycles',10000);


CREATE TABLE category_staging (
    category_id INT PRIMARY KEY,
    category_name VARCHAR(255) NOT NULL,
    amount DECIMAL(10 , 2 )
);


INSERT INTO category_staging(category_id, category_name, amount)
VALUES(1,'Children Bicycles',15000),
    (3,'Cruisers Bicycles',13000),
    (4,'Cyclocross Bicycles',20000),
    (5,'Electric Bikes',10000),
    (6,'Mountain Bikes',10000);
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

要使用 sales.category_staging（源表）中的值将数据更新到 sales.category（目标表），要使用 merge:

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```SQL
MERGE category t 
    USING category_staging s
ON (s.category_id = t.category_id)
WHEN MATCHED
    THEN UPDATE SET 
        t.category_name = s.category_name,
        t.amount = s.amount
WHEN NOT MATCHED BY TARGET 
    THEN INSERT (category_id, category_name, amount)
         VALUES (s.category_id, s.category_name, s.amount)
WHEN NOT MATCHED BY SOURCE 
    THEN DELETE;
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

执行过程如下图：

![img](https://img2018.cnblogs.com/blog/1182288/201909/1182288-20190915094935065-13998296.png)

 