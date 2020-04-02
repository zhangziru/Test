
学习参考的链接：
T4模板之基础篇：https://www.cnblogs.com/guohu/p/9308811.html（文档已持久化到本地）
EF-CodeFirst编码工具插件（POCO Code First Generator）的使用，链接：https://www.cnblogs.com/godbell/p/7450547.html（文档已持久化到本地）
VS T4开发需要安装的插件：
通过VS ->工具->扩展更新->联机搜索-以下的安装包，依次安装。
1、 Devart T4 Editor：为VS提供智能提示功能。
2、T4 Toolbox：批量生成文件。原文链接:https://blog.csdn.net/CsethCRM/article/details/72457284?locationNum=3&fps=1

3、自己整理的源码链接：https://pan.baidu.com/s/1UUanDOha3UpwVcKkLpnXUA 密码：xcju
该项目有两个T4模板

Database1.tt  链接数据库的T4模板

TextTemplate1.tt文本T4模板

------------------------------------配置笔记----------------------------------------------
链接数据库的批次模板（T4模板），数据库中表的命名为TB_表名，视图名为V_视图名。或者重新定义修改T4模板中的表的过滤TableFilterInclude的设置。
如果自己配置，大概需要配置7处：
1、数据库连接字符串
2、生成文件的命名空间
3、生成文件的类型（默认为：实体、实体配置、）
4、数据库上下文的配置
5、设置实体配置文件的后缀名，一般默认的为configuration
6、配置表的过滤，配置视图的过滤
7、表名重命名
8、动态的引入命名空间（这个不在配置的T4文件中，而是在EF.Reverse.POCO.ttinclude的文件中进行配置）【重点】

EF.Reverse.POCO.ttinclude该文件被配置为【输出文件】。



附加的数据库：User Id=sa;Password=sasa;