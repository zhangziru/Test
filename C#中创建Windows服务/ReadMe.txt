Windows���񴴽���ѧϰ
�ο������ӣ�https://www.cnblogs.com/wyt007/p/8675957.html
�Լ�д�Ĳο����룺���ӣ�https://pan.baidu.com/s/1xseFVr8HUEwYQHcSPUjvtw 
��ȡ�룺qxer 
һ����������
1���ļ�-���½�-����Ŀ-��windows����-��windows����
	1�����ﲻ�������Խ�WindowsService1��ȷ����
2����Ŀ�ṹ��
	1��Program.cs�ļ�����ڡ�
	2��Service1.cs�Ƿ����ļ���Service1.cs���������֣�
		��1��һ������Designer����������������Ӹ����������ѡ���ļ�>�Ҽ�>�鿴�������
		��2��һ�����Ǻ�̨�ļ����������дһЩ�߼���Ĭ�ϰ���3�����������캯����OnStart��OnStop�����������OnPause��OnContinue��������ѡ���ļ�>�Ҽ�>�鿴���룩
3��˫��Service1.cs�ļ����������ҳ�棬���ſհ״��Ҽ�-����Ӱ�װ����
4���ڰ�װ������ƽ��棬�������������
	1�����serviceProcessInstaller1�������½ǵ��������У���Account�޸�ΪLocalSystem��
5��ѡ��ServiceInstaller1��
	1�����½ǵ����Կ��У���ServiceName�޸ĳɴ�����ָ����ServiceName  Ϊ ��CyrusTest����
	2����������ѡ��
		��1��DelayedAutoStart��ʾ�������Ƿ��ӳ�������
		��2��Description��ʾ�����������
		��3��DisplayName��ʾ������ʾ���ơ�
		��4��ServicesDependedOn��ʾ�����ķ����
		��5��StartType��ʾ�������ͣ���Ϊ�Զ��������ֶ������ͽ��á�
������װ����
1��ѡ����Ŀ�Ҽ�ѡ�����ɡ�������exe�ļ���
2��Ȼ�󽫴�C:\Windows\Microsoft.NET\Framework\v4.0.30319�п���installutil.exe�ļ�������Ŀ¼��bin/Debug���¡�Ŀ��:ʹinstallutil.exe��dp0WindowsService1.exe��ͬһ��Ŀ¼��
3���ڸ�Ŀ¼�½�����װ.bat���ļ���ʹ�ü��±��򿪣������������
%~dp0InstallUtil.exe %~dp0WindowsService1.exe
pause
	1��ע�⣺
		��1��ÿ������ǰҪ��һ��%~dp0����ʾ��Ŀ¼����Ϊ��ǰĿ¼���������ӣ����ܻ����
		��2��pause һ��Ҫ���У����򱨴�
	2�����˫����װ.bat�ļ�������ɷ���ע���ˡ�
	3�����ҵĵ������Ҽ�ѡ�񡰹������򿪡������Ӧ�ó����µġ����񡱣����ܿ�������ע��ķ����ˡ�
����ж�ط���
1���ڸ�Ŀ¼�½���ж��.bat���ļ���ʹ�ü��±��򿪣������������
%~dp0InstallUtil /u %~dp0WindowsService1.exe
pause
	1��ͬ��pauseҲҪ���С�
2����������������������������⣬�뽫������Ŀ����EVERYONEȨ�ޡ�
	eg��
		Windows �޷�����MQWindowsService����λ�� ���ؼ�����ϣ���
		����5���ܾ����ʡ�
3��һ��Ҫ�Թ���Ա�������Ӵ�����ص㡿
	1��Ҫ��Ȼ��װʱ�ᱨ��Windows����װ�쳣��System.Security.SecurityException: δ�ҵ�Դ����δ������ĳЩ��ȫ���¼�����
�ġ�����
1����VS��ѡ�񡰵��ԡ�-�����ӵ����̡���
2��ѡ�����Ǵ����ķ���WindowsService1.exe���Ϳ����ˡ�
3��ע�⣺һ��Ҫȷ��Windows�����ǿ����ġ�
�塢cmd���������ļ���*.bat�ļ���*.cmd�ļ���
1���ο����ӣ�https://jingyan.baidu.com/article/fdffd1f890e590f3e88ca167.html