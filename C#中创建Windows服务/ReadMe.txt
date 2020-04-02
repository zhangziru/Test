Windows服务创建的学习
参考的链接：https://www.cnblogs.com/wyt007/p/8675957.html
自己写的参考代码：链接：https://pan.baidu.com/s/1xseFVr8HUEwYQHcSPUjvtw 
提取码：qxer 
一、创建服务
1、文件-》新建-》项目-》windows桌面-》windows服务。
	1）这里不改名，仍叫WindowsService1，确定。
2、项目结构：
	1）Program.cs文件是入口。
	2）Service1.cs是服务文件。Service1.cs包含两部分：
		（1）一部分是Designer。可以在这里面添加各种组件。（选中文件>右键>查看设计器）
		（2）一部分是后台文件。里面可以写一些逻辑，默认包含3个方法：构造函数、OnStart和OnStop，还可以添加OnPause和OnContinue方法。（选中文件>右键>查看代码）
3、双击Service1.cs文件，进入设计页面，对着空白处右键-》添加安装器。
4、在安装器的设计界面，出现两个组件。
	1）点击serviceProcessInstaller1，在右下角的属性栏中，将Account修改为LocalSystem。
5、选中ServiceInstaller1。
	1）右下角的属性框中，将ServiceName修改成代码中指定的ServiceName  为 “CyrusTest”。
	2）其他自行选择。
		（1）DelayedAutoStart表示开机后是否延迟启动。
		（2）Description表示服务的描述。
		（3）DisplayName表示服务显示名称。
		（4）ServicesDependedOn表示依赖的服务项。
		（5）StartType表示启动类型，分为自动启动，手动启动和禁用。
二、安装服务
1、选中项目右键选择“生成”，生成exe文件。
2、然后将从C:\Windows\Microsoft.NET\Framework\v4.0.30319中拷贝installutil.exe文件到生成目录（bin/Debug）下。目的:使installutil.exe和dp0WindowsService1.exe在同一级目录。
3、在该目录新建“安装.bat”文件，使用记事本打开，输入如下命令：
%~dp0InstallUtil.exe %~dp0WindowsService1.exe
pause
	1）注意：
		（1）每个命令前要加一个%~dp0，表示将目录更改为当前目录。倘若不加，可能会出错。
		（2）pause 一定要换行，否则报错。
	2）最后双击安装.bat文件，就完成服务注册了。
	3）在我的电脑上右键选择“管理”，打开“服务和应用程序”下的“服务”，就能看到我们注册的服务了。
三、卸载服务
1、在该目录新建“卸载.bat”文件，使用记事本打开，输入如下命令：
%~dp0InstallUtil /u %~dp0WindowsService1.exe
pause
	1）同样pause也要换行。
2、如果在启动过程中遇到如下问题，请将整个项目加上EVERYONE权限。
	eg：
		Windows 无法启动MQWindowsService服务（位于 本地计算机上）。
		错误5：拒绝访问。
3、一定要以管理员身份运行哟。【重点】
	1）要不然安装时会报“Windows服务安装异常：System.Security.SecurityException: 未找到源，但未能搜索某些或全部事件”。
四、调试
1、在VS中选择“调试”-“附加到进程”。
2、选择我们创建的服务（WindowsService1.exe）就可以了。
3、注意：一定要确保Windows服务是开启的。
五、cmd批量处理文件：*.bat文件和*.cmd文件。
1、参考链接：https://jingyan.baidu.com/article/fdffd1f890e590f3e88ca167.html