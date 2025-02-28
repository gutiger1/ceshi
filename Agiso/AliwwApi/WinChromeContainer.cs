using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using Agiso.ChromeDevTools;
using Agiso.Handler;
using Agiso.Utils;
using Agiso.Windows;
using AliwwClient.Cache;
using AliwwClient.WebSocketServer.Extensions;

namespace Agiso.AliwwApi
{
	// Token: 0x0200070D RID: 1805
	public abstract class WinChromeContainer : WindowInfo
	{
		// Token: 0x060023AD RID: 9133 RVA: 0x0005DFFC File Offset: 0x0005C1FC
		private string a(string A_0, string A_1)
		{
			return A_0 + "#|=|#" + A_1;
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x0000EC4D File Offset: 0x0000CE4D
		public void RemoveDictRsRsp(string urlContain, string urlNotContain)
		{
			this.a.Remove(this.a(urlContain, urlNotContain));
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x0005E018 File Offset: 0x0005C218
		private int b()
		{
			int processId = base.ProcessId;
			int num;
			if (WinChromeContainer.b.TryGetValue(processId, out num))
			{
				if (ChromeRemote.ValidatePort(num))
				{
					return num;
				}
				int statusCode;
				WinChromeContainer.b.TryRemove(processId, out statusCode);
			}
			List<int> port = Win32Extend.GetPort(base.HWnd);
			if (port != null && port.Count > 0)
			{
				string text = string.Join(",", port.Select(new Func<int, string>(WinChromeContainer.<>c.<>9.a)).ToArray<string>());
				AppConfig.WriteLog(string.Concat(new string[]
				{
					"hWnd: ",
					base.HWnd.ToString("x"),
					", PortList: ",
					text,
					", Name: ",
					base.Info.WindowName
				}), LogType.LogForChormeDebug, 1);
				foreach (int num2 in port)
				{
					try
					{
						if (ChromeRemote.ValidatePort(num2))
						{
							WinChromeContainer.b.TryAdd(processId, num2);
							AppConfig.WriteLog(string.Format("hWnd: {0}, PortList: {1}, Name: {2}, ValidPort: {3}", new object[]
							{
								base.HWnd.ToString("x"),
								text,
								base.Info.WindowName,
								num2
							}), LogType.LogForChormeDebug, 1);
							return num2;
						}
					}
					catch (Exception ex)
					{
						WebException ex2 = ex as WebException;
						string text2 = string.Format("An exception was throw while matching port:{0}, portList:{1}!", num2, text);
						if (ex2 != null)
						{
							string text3 = text2;
							string text4 = "状态：";
							string text5;
							if (ex2.Response == null)
							{
								text5 = "";
							}
							else
							{
								int statusCode = (int)((HttpWebResponse)ex2.Response).StatusCode;
								text5 = statusCode.ToString();
							}
							text2 = text3 + text4 + text5 + ex2.ToString();
						}
						else
						{
							text2 += ex.ToString();
						}
						LogWriter.WriteLog(text2, 1);
					}
				}
			}
			return 0;
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x0005E26C File Offset: 0x0005C46C
		public RemoteSessionsResponse GetRsRsp(string urlContain, string urlNotContain, bool ignoreCache = false)
		{
			string text = this.a(urlContain, urlNotContain);
			RemoteSessionsResponse remoteSessionsResponse;
			if (!ignoreCache && this.a.ContainsKey(text))
			{
				remoteSessionsResponse = this.a[text];
			}
			else
			{
				int num = this.b();
				if (num <= 0)
				{
					remoteSessionsResponse = null;
				}
				else
				{
					RemoteSessionsResponse remoteSessionsResponse2 = null;
					try
					{
						remoteSessionsResponse2 = ChromeRemote.GetLocalAvailableSessions(num, urlContain, urlNotContain);
						if (remoteSessionsResponse2 != null)
						{
							this.a[text] = remoteSessionsResponse2;
						}
					}
					catch (Exception ex)
					{
						if (ex is WebException)
						{
							this.a.Clear();
							AppConfig.WriteLog(string.Format("hWnd: {0}, port: {1}, Name: {2}, urlContain: {3}, port request timeout!", new object[]
							{
								base.HWnd.ToString("x"),
								num,
								base.Info.WindowName,
								urlContain
							}), LogType.LogForChormeDebug, 1);
						}
						else
						{
							LogWriter.WriteLog(string.Format("hWnd: {0}, port: {1}, Name: {2}, urlContain: {3}, {4}!", new object[]
							{
								base.HWnd.ToString("x"),
								num,
								base.Info.WindowName,
								urlContain,
								ex.ToString()
							}), 1);
						}
					}
					remoteSessionsResponse = remoteSessionsResponse2;
				}
			}
			return remoteSessionsResponse;
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x0005E3B8 File Offset: 0x0005C5B8
		public virtual string GetHtml(RemoteSessionsResponse rsRsp)
		{
			string text;
			if (rsRsp == null)
			{
				text = null;
			}
			else
			{
				string text2 = "";
				try
				{
					using (ChromeSession chromeSession = new ChromeSession(rsRsp.webSocketDebuggerUrl))
					{
						try
						{
							text2 = chromeSession.GetHtml();
						}
						catch (Exception ex)
						{
							LogWriter.WriteLog("GetHtml时异常: " + ex.ToString(), 1);
						}
						finally
						{
							chromeSession.Close();
						}
					}
				}
				catch
				{
				}
				text = text2;
			}
			return text;
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x0005E45C File Offset: 0x0005C65C
		public virtual string GetHtml(string urlContain, string urlNotContain)
		{
			string text;
			try
			{
				RemoteSessionsResponse rsRsp = this.GetRsRsp(urlContain, urlNotContain, false);
				string html = this.GetHtml(rsRsp);
				text = html;
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(string.Concat(new string[]
				{
					"GetHtml时异常：urlContain，",
					urlContain,
					"；",
					ex.ToString(),
					" "
				}), 1);
				text = null;
			}
			return text;
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x0005E4D0 File Offset: 0x0005C6D0
		public virtual string GetHtmlAuto(UserCache userCache, string urlContain, string urlNotContain)
		{
			string text;
			if (!userCache.IsRecentSessionNull)
			{
				text = userCache.RecentSession.GetHtml();
			}
			else
			{
				text = this.GetHtml(urlContain, urlNotContain);
			}
			return text;
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x0005E500 File Offset: 0x0005C700
		public ExecuteJsResultInfo Execute(RemoteSessionsResponse rsRsp, string js)
		{
			ExecuteJsResultInfo executeJsResultInfo2;
			try
			{
				ExecuteJsResultInfo executeJsResultInfo = null;
				using (ChromeSession chromeSession = new ChromeSession(rsRsp.webSocketDebuggerUrl))
				{
					try
					{
						executeJsResultInfo = chromeSession.CompileScript(js);
					}
					catch (Exception ex)
					{
						LogWriter.WriteLog("ExecJs时异常: " + ex.ToString() + "，详情：" + js, 1);
					}
					finally
					{
						chromeSession.Close();
					}
				}
				executeJsResultInfo2 = executeJsResultInfo;
			}
			catch (Exception)
			{
				executeJsResultInfo2 = null;
			}
			return executeJsResultInfo2;
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x0005E59C File Offset: 0x0005C79C
		public ExecuteJsResultInfo Execute(string urlContain, string urlNotContain, string js, out RemoteSessionsResponse rsRsp)
		{
			rsRsp = this.GetRsRsp(urlContain, urlNotContain, false);
			ExecuteJsResultInfo executeJsResultInfo;
			if (rsRsp == null)
			{
				executeJsResultInfo = null;
			}
			else
			{
				ExecuteJsResultInfo executeJsResultInfo2 = this.Execute(rsRsp, js);
				if (executeJsResultInfo2 == null)
				{
					this.a.Clear();
				}
				executeJsResultInfo = executeJsResultInfo2;
			}
			return executeJsResultInfo;
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x0005E5E0 File Offset: 0x0005C7E0
		public ExecuteJsResultInfo Execute(string urlContain, string urlNotContain, string js)
		{
			RemoteSessionsResponse remoteSessionsResponse;
			return this.Execute(urlContain, urlNotContain, js, out remoteSessionsResponse);
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x0005E5FC File Offset: 0x0005C7FC
		public WindowInfo FindAefWin()
		{
			WindowInfo windowInfo = new WindowInfo(base.HWnd);
			WindowInfo windowInfo2 = windowInfo.FindWindowInDescendant("Aef_RenderWidgetHostHWND", null, false, new bool?(false));
			if (windowInfo2 == null)
			{
				windowInfo2 = windowInfo.FindWindowInDescendant("AEF_RenderWidgetHostHWND", null, false, new bool?(false));
			}
			return windowInfo2;
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x0005E648 File Offset: 0x0005C848
		public string ExecuteJsAndReturn(string js, string returnJS, string urlContain, string urlNotContain = null, int timeOutMilliseconds = 1000)
		{
			RemoteSessionsResponse remoteSessionsResponse = null;
			this.Execute(urlContain, urlNotContain, js, out remoteSessionsResponse);
			string text;
			if (remoteSessionsResponse == null)
			{
				text = null;
			}
			else
			{
				text = this.ExecuteJsAndReturn(remoteSessionsResponse, returnJS, timeOutMilliseconds);
			}
			return text;
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x0005E67C File Offset: 0x0005C87C
		public string ExecuteJsAndReturn(RemoteSessionsResponse rsRsp, string returnJS, int timeOutMilliseconds = 1000)
		{
			WinChromeContainer.a a = new WinChromeContainer.a();
			a.a = this;
			a.b = rsRsp;
			a.c = returnJS;
			string text;
			try
			{
				WinChromeContainer.b b = new WinChromeContainer.b();
				b.b = a;
				b.a = null;
				if (Util.CheckWait(timeOutMilliseconds, new Func<bool>(b.c), 100))
				{
					text = b.a.result.result.value;
				}
				else
				{
					text = null;
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog("ExecuteJsAndReturn时异常: " + ex.ToString(), 1);
				text = null;
			}
			return text;
		}

		// Token: 0x17000AF1 RID: 2801
		// (set) Token: 0x060023BA RID: 9146 RVA: 0x0000EC63 File Offset: 0x0000CE63
		private static Dictionary<string, bool> DictProcessCanConnect
		{
			[CompilerGenerated]
			set
			{
				WinChromeContainer.c = value;
			}
		} = new Dictionary<string, bool>();

		// Token: 0x060023BB RID: 9147 RVA: 0x0005E720 File Offset: 0x0005C920
		public bool CanConnect(string userNick)
		{
			string text = base.ProcessId.ToString() + userNick;
			if (!WinChromeContainer.c.ContainsKey(text))
			{
				int i = 0;
				while (i < 5)
				{
					RemoteSessionsResponse rsRsp = this.GetRsRsp(null, null, false);
					if (rsRsp == null)
					{
						Thread.Sleep(200);
						i++;
					}
					else
					{
						WinChromeContainer.c[text] = true;
						IL_005D:
						if (!WinChromeContainer.c.ContainsKey(text))
						{
							WinChromeContainer.c[text] = false;
							goto IL_0079;
						}
						goto IL_0079;
					}
				}
				goto IL_005D;
			}
			IL_0079:
			return WinChromeContainer.c[text];
		}

		// Token: 0x04001DC7 RID: 7623
		private Dictionary<string, RemoteSessionsResponse> a = new Dictionary<string, RemoteSessionsResponse>();

		// Token: 0x04001DC8 RID: 7624
		private static ConcurrentDictionary<int, int> b = new ConcurrentDictionary<int, int>();

		// Token: 0x04001DC9 RID: 7625
		[CompilerGenerated]
		private static Dictionary<string, bool> c;

		// Token: 0x0200070F RID: 1807
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x04001DCC RID: 7628
			public WinChromeContainer a;

			// Token: 0x04001DCD RID: 7629
			public RemoteSessionsResponse b;

			// Token: 0x04001DCE RID: 7630
			public string c;
		}

		// Token: 0x02000710 RID: 1808
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x060023C3 RID: 9155 RVA: 0x0005E7B4 File Offset: 0x0005C9B4
			internal bool c()
			{
				this.a = this.b.a.Execute(this.b.b, this.b.c);
				return this.a != null && this.a.result != null && this.a.result.result != null && !string.IsNullOrEmpty(this.a.result.result.value);
			}

			// Token: 0x04001DCF RID: 7631
			public ExecuteJsResultInfo a;

			// Token: 0x04001DD0 RID: 7632
			public WinChromeContainer.a b;
		}
	}
}
