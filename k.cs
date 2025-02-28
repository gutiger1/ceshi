using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Agiso;
using Agiso.Handler;
using Agiso.Windows;
using AliwwClient;

// Token: 0x02000013 RID: 19
internal static class k
{
	// Token: 0x06000066 RID: 102 RVA: 0x000025E7 File Offset: 0x000007E7
	[CompilerGenerated]
	public static Form1 a()
	{
		return k.a;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x000025EE File Offset: 0x000007EE
	[CompilerGenerated]
	private static void a(Form1 A_0)
	{
		k.a = A_0;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00011064 File Offset: 0x0000F264
	[STAThread]
	internal static void a(string[] args)
	{
		bool flag;
		Mutex mutex = new Mutex(true, "AliwwClient", out flag);
		if (flag)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			k.a(new Form1(args));
			Application.Run(k.a);
			mutex.ReleaseMutex();
		}
		else
		{
			List<string> list = new List<string>(args);
			if (!list.Contains("-m") && !AppConfig.AllowAutoLogin)
			{
				MessageBox.Show("已有该程序运行在后台，如果需要多店同时使用，可以帐户管理里添加其他店的旺旺！", "Agiso信息提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			List<WindowInfo> allDesktopWindows = Win32Extend.GetAllDesktopWindows();
			if (allDesktopWindows != null)
			{
				foreach (WindowInfo windowInfo in allDesktopWindows)
				{
					if (windowInfo.Info.WindowName.Contains("Agiso旺旺发送助手"))
					{
						WindowsAPI.ShowWindow(windowInfo.HWnd, 1);
						windowInfo.SetForegroundWindow();
						break;
					}
				}
			}
		}
	}

	// Token: 0x04000043 RID: 67
	[CompilerGenerated]
	private static Form1 a;
}
