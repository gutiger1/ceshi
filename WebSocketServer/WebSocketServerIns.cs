using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Threading;
using Agiso;
using Agiso.Handler;
using Agiso.Utils;
using Agiso.Windows;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace AliwwClient.WebSocketServer
{
	// Token: 0x02000078 RID: 120
	public class WebSocketServerIns
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000337F File Offset: 0x0000157F
		// (set) Token: 0x06000370 RID: 880 RVA: 0x00003387 File Offset: 0x00001587
		public List<AgisoWebSocketServer> ServerList { get; set; } = new List<AgisoWebSocketServer>();

		// Token: 0x06000371 RID: 881 RVA: 0x00037CFC File Offset: 0x00035EFC
		public WebSocketServerIns(List<int> ports = null)
		{
			if (ports == null || ports.Count <= 0)
			{
				this.b = new List<int> { 8967, 32000 };
			}
			else
			{
				this.b = ports;
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00037D5C File Offset: 0x00035F5C
		private void a()
		{
			if (AppConfig.AllowAutoLogin)
			{
				List<int> processIdByPorts = Util.GetProcessIdByPorts(this.b.ToArray());
				int id = Process.GetCurrentProcess().Id;
				if (processIdByPorts.Remove(id))
				{
					LogWriter.WriteLog("端口被当前应用占用了", 1);
				}
				if (!Util.IsEmptyList<int>(processIdByPorts))
				{
					LogWriter.WriteLog("获取到端口占用进程列表，" + string.Join<int>(",", processIdByPorts), 1);
					foreach (int num in processIdByPorts)
					{
						try
						{
							IntPtr mainWindowHandle = Process.GetProcessById(num).MainWindowHandle;
							if (mainWindowHandle != IntPtr.Zero)
							{
								WindowInfo windowInfo = new WindowInfo(mainWindowHandle);
								LogWriter.WriteLog(string.Format("进程{0}，windowName：{1}，className：{2}", num, windowInfo.Info.WindowName, windowInfo.Info.ClassName), 1);
							}
							goto IL_01B1;
						}
						catch (Exception ex)
						{
							LogWriter.WriteLog(string.Format("进程{0}，获取窗口信息异常，{1}", num, ex), 1);
							goto IL_01B1;
						}
						ManagementObjectSearcher managementObjectSearcher;
						try
						{
							IL_0103:
							using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
							{
								foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
								{
									ManagementObject managementObject = (ManagementObject)managementBaseObject;
									int num2 = Util.ToInt(managementObject["ProcessID"].ToString());
									if (num2 > 0)
									{
										LogWriter.WriteLog(string.Format("获取到端口占用子进程，{0}", num2), 1);
										Win32Extend.KillProcessById(num2, null);
									}
								}
							}
						}
						finally
						{
							if (managementObjectSearcher != null)
							{
								((IDisposable)managementObjectSearcher).Dispose();
							}
						}
						Win32Extend.KillProcessById(num, null);
						Thread.Sleep(1000);
						continue;
						IL_01B1:
						managementObjectSearcher = new ManagementObjectSearcher(string.Format("SELECT ProcessID FROM Win32_Process WHERE ParentProcessId={0}", num));
						goto IL_0103;
					}
				}
			}
			foreach (int num3 in this.b)
			{
				AgisoWebSocketServer agisoWebSocketServer = new AgisoWebSocketServer(num3);
				agisoWebSocketServer.AddWebSocketService<RecentBehavior>("/Aliww");
				agisoWebSocketServer.AddWebSocketService<AldsBehavior>("/Alds");
				if (AppConfig.AllowAutoLogin)
				{
					agisoWebSocketServer.AddWebSocketService<LoginBehavior>("/Login");
				}
				try
				{
					agisoWebSocketServer.Start();
				}
				catch (Exception ex2)
				{
					LogWriter.WriteLog(string.Format("ws serv start on port:{0} fail! {1}", num3, ex2), 1);
					agisoWebSocketServer.RemoveWebSocketService("/Aliww");
					agisoWebSocketServer.RemoveWebSocketService("/Alds");
					if (AppConfig.AllowAutoLogin)
					{
						agisoWebSocketServer.RemoveWebSocketService("/Login");
					}
					continue;
				}
				this.a(agisoWebSocketServer, num3);
			}
			if (this.ServerList.Count <= 0)
			{
				if (AppConfig.AllowAutoLogin)
				{
					AppConfig.QnAgentServiceClient.AutoLoginError("websocket服务端", null, "websocket服务端启动失败", 268435455L, "", "");
				}
				throw new Exception("无端口可用！");
			}
			AppConfig.SocketServerPort = this.ServerList[0].Port;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00038130 File Offset: 0x00036330
		private void a(AgisoWebSocketServer A_0, int A_1)
		{
			WebSocketServerIns.a a = new WebSocketServerIns.a();
			a.a = null;
			for (int i = 0; i < 10; i++)
			{
				try
				{
					using (WebSocket webSocket = new WebSocket(string.Format("ws://127.0.0.1:{0}{1}", A_1, "/Aliww"), new string[0]))
					{
						WebSocket webSocket2 = webSocket;
						EventHandler<ErrorEventArgs> eventHandler;
						if ((eventHandler = a.b) == null)
						{
							eventHandler = (a.b = new EventHandler<ErrorEventArgs>(a.f));
						}
						webSocket2.OnError += eventHandler;
						WebSocket webSocket3 = webSocket;
						EventHandler<CloseEventArgs> eventHandler2;
						if ((eventHandler2 = a.c) == null)
						{
							eventHandler2 = (a.c = new EventHandler<CloseEventArgs>(a.f));
						}
						webSocket3.OnClose += eventHandler2;
						WebSocket webSocket4 = webSocket;
						EventHandler eventHandler3;
						if ((eventHandler3 = a.d) == null)
						{
							eventHandler3 = (a.d = new EventHandler(a.f));
						}
						webSocket4.OnOpen += eventHandler3;
						webSocket.Connect();
						int num = 1000;
						Func<bool> func;
						if ((func = a.e) == null)
						{
							func = (a.e = new Func<bool>(a.f));
						}
						Util.CheckWait(num, func, 100);
						if (a.a != null && a.a.Value)
						{
							this.ServerList.Add(A_0);
							return;
						}
					}
				}
				catch (MissingMethodException ex)
				{
					this.ServerList.Add(A_0);
					LogWriter.WriteLog(ex.ToString(), 1);
					return;
				}
				catch (Exception ex2)
				{
					LogWriter.WriteLog(ex2.ToString(), 1);
				}
			}
			A_0.RemoveWebSocketService("/Aliww");
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00003390 File Offset: 0x00001590
		// (set) Token: 0x06000375 RID: 885 RVA: 0x00003398 File Offset: 0x00001598
		public LoginBehavior LoginSession { get; set; }

		// Token: 0x06000376 RID: 886 RVA: 0x000382F4 File Offset: 0x000364F4
		public LoginBehavior GetLogin(DateTime from)
		{
			List<LoginBehavior> list = new List<LoginBehavior>();
			foreach (AgisoWebSocketServer agisoWebSocketServer in this.ServerList)
			{
				foreach (IWebSocketSession webSocketSession in agisoWebSocketServer.LoginSessionManager.Sessions)
				{
					LoginBehavior loginBehavior = webSocketSession as LoginBehavior;
					if (loginBehavior != null && loginBehavior.StartTime > from && loginBehavior.ConnectionState == 1)
					{
						list.Add(loginBehavior);
						break;
					}
				}
			}
			return list.OrderByDescending(new Func<LoginBehavior, DateTime>(WebSocketServerIns.<>c.<>9.a)).FirstOrDefault<LoginBehavior>();
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000383F0 File Offset: 0x000365F0
		public void Stop()
		{
			foreach (AgisoWebSocketServer agisoWebSocketServer in this.ServerList)
			{
				agisoWebSocketServer.Stop();
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00038444 File Offset: 0x00036644
		public bool IsListening
		{
			get
			{
				foreach (AgisoWebSocketServer agisoWebSocketServer in this.ServerList)
				{
					if (agisoWebSocketServer.IsListening)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000384A0 File Offset: 0x000366A0
		public void Start()
		{
			if (!Util.IsEmptyList<AgisoWebSocketServer>(this.ServerList))
			{
				using (List<AgisoWebSocketServer>.Enumerator enumerator = this.ServerList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AgisoWebSocketServer agisoWebSocketServer = enumerator.Current;
						if (!agisoWebSocketServer.IsListening)
						{
							agisoWebSocketServer.Start();
						}
					}
					return;
				}
			}
			this.a();
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00038514 File Offset: 0x00036714
		public List<int> PostList
		{
			get
			{
				List<int> list = new List<int>();
				foreach (AgisoWebSocketServer agisoWebSocketServer in this.ServerList)
				{
					list.Add(agisoWebSocketServer.Port);
				}
				return list;
			}
		}

		// Token: 0x04000311 RID: 785
		[CompilerGenerated]
		private List<AgisoWebSocketServer> a;

		// Token: 0x04000312 RID: 786
		private List<int> b;

		// Token: 0x04000313 RID: 787
		[CompilerGenerated]
		private LoginBehavior c;

		// Token: 0x0200007A RID: 122
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x0600037F RID: 895 RVA: 0x000033B6 File Offset: 0x000015B6
			internal void f(object sender, ErrorEventArgs e)
			{
				this.a = new bool?(false);
			}

			// Token: 0x06000380 RID: 896 RVA: 0x000033B6 File Offset: 0x000015B6
			internal void f(object sender, CloseEventArgs e)
			{
				this.a = new bool?(false);
			}

			// Token: 0x06000381 RID: 897 RVA: 0x000033C4 File Offset: 0x000015C4
			internal void f(object sender, EventArgs e)
			{
				this.a = new bool?(true);
			}

			// Token: 0x06000382 RID: 898 RVA: 0x000033D2 File Offset: 0x000015D2
			internal bool f()
			{
				return this.a != null;
			}

			// Token: 0x04000316 RID: 790
			public bool? a;

			// Token: 0x04000317 RID: 791
			public EventHandler<ErrorEventArgs> b;

			// Token: 0x04000318 RID: 792
			public EventHandler<CloseEventArgs> c;

			// Token: 0x04000319 RID: 793
			public EventHandler d;

			// Token: 0x0400031A RID: 794
			public Func<bool> e;
		}
	}
}
