using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Automation;
using Agiso.ChromeDevTools;
using Agiso.Handler;
using Agiso.Utils;
using Agiso.Windows;

namespace Agiso.AliwwApi.Wangwang
{
	// Token: 0x02000742 RID: 1858
	public class WinAliwwMainBuyer9 : WinChromeContainer, IWinValid
	{
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x0000F09E File Offset: 0x0000D29E
		// (set) Token: 0x060024EE RID: 9454 RVA: 0x0000F0A6 File Offset: 0x0000D2A6
		public bool IsUndefined { get; protected set; }

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x060024EF RID: 9455 RVA: 0x00067084 File Offset: 0x00065284
		public string Nick
		{
			get
			{
				if (string.IsNullOrEmpty(this._nick))
				{
					bool flag;
					this.a(out flag);
				}
				return this._nick;
			}
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000670B0 File Offset: 0x000652B0
		public static List<WinAliwwMainBuyer9> GetList(int processId = 0)
		{
			return WinFindableBase.GetList<WinAliwwMainBuyer9>(new WindowStruct("StandardFrame", "阿里旺旺"), processId, false);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000670D8 File Offset: 0x000652D8
		public static WinAliwwMainBuyer9 Get(string userNick)
		{
			List<WinAliwwMainBuyer9> list = WinAliwwMainBuyer9.GetList(0);
			foreach (WinAliwwMainBuyer9 winAliwwMainBuyer in list)
			{
				string nick = winAliwwMainBuyer.Nick;
				if (!string.IsNullOrEmpty(nick))
				{
					if (nick.Equals(userNick, StringComparison.OrdinalIgnoreCase))
					{
						return winAliwwMainBuyer;
					}
					if (Util.StrConvSimple(nick).Equals(Util.StrConvSimple(userNick), StringComparison.OrdinalIgnoreCase))
					{
						return winAliwwMainBuyer;
					}
				}
			}
			return null;
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x00067164 File Offset: 0x00065364
		public bool ClickExit()
		{
			WinAliwwMainBuyer9.a a = new WinAliwwMainBuyer9.a();
			a.c = this;
			base.SetForegroundWindow();
			Rectangle clientRect = base.GetClientRect();
			this._childChromeWin.Click(23, clientRect.GetHeight() - 15, true);
			a.a = null;
			Util.CheckWait(300, new Func<bool>(a.e), 10);
			bool flag;
			if (a.a == null)
			{
				flag = false;
			}
			else
			{
				Thread.Sleep(50);
				Rectangle clientRect2 = a.a.GetClientRect();
				a.a.Click(35, clientRect2.GetHeight() - 17, true);
				a.b = base.ProcessId;
				flag = Util.CheckWait(1000, new Func<bool>(a.d), 100);
			}
			return flag;
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x00067230 File Offset: 0x00065430
		private WindowInfo b()
		{
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(new WindowStruct
			{
				ClassName = "#32770",
				WindowName = "退出确认"
			}, base.ProcessId, false);
			foreach (WindowInfo windowInfo in windowListByClassAndName)
			{
				if (windowInfo.FindWindowsInDescendant("StandardButton", "确认", false, new bool?(false)).Count > 0)
				{
					return windowInfo;
				}
			}
			return null;
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x000672D0 File Offset: 0x000654D0
		public static string GetVersion()
		{
			string text = AppConfig.GetIniFullFileNameAliww();
			string text2;
			if (string.IsNullOrEmpty(text))
			{
				Process[] processesByName = Process.GetProcessesByName("AliIM");
				foreach (Process process in processesByName)
				{
					using (process)
					{
						if (process != null && process.MainWindowHandle != IntPtr.Zero)
						{
							text = process.MainModule.FileName.Replace("AliIM.exe", "Aliim.ini");
							return Util.GetVersionByIniFileName(text);
						}
					}
				}
				text2 = "";
			}
			else
			{
				text2 = Util.GetVersionByIniFileName(text);
			}
			return text2;
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x060024F5 RID: 9461 RVA: 0x0006738C File Offset: 0x0006558C
		private RemoteSessionsResponse RsRsp
		{
			get
			{
				if (this.b == null)
				{
					this.b = base.GetRsRsp("alires:///offlinepkg/ww/pages/panel.html", null, false);
				}
				return this.b;
			}
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000673C0 File Offset: 0x000655C0
		private void a(out bool A_0)
		{
			A_0 = false;
			if (this.RsRsp != null)
			{
				try
				{
					string html = this.GetHtml(this.b);
					if (html != null && html.Contains("旺旺主面板"))
					{
						A_0 = true;
						string text = "<span class=\"nick-(\\S*)\">(?<nick>[^<]*)</span>";
						new Regex(text);
						Match match = Regex.Match(html, text);
						if (match != null && match.Success)
						{
							Group group = match.Groups["nick"];
							if (group != null)
							{
								string value = group.Value;
								this._nick = ((value != null) ? value.Replace("&amp;", "&") : null);
							}
						}
						this.IsUndefined = false;
						if (html.Contains("undefined..."))
						{
							this.IsUndefined = true;
						}
					}
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog("校验WinAliwwMainBuyer9时出错了" + Environment.NewLine + ex.ToString(), 1);
				}
			}
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000674C0 File Offset: 0x000656C0
		public void OpenChat(string buyerNick)
		{
			if (this.RsRsp != null)
			{
				base.Execute(this.RsRsp, "\r\nif(!localStorage.getItem('WW_DEBUG')){\r\n    localStorage.setItem('WW_DEBUG', 1)\r\n}\r\nif(window.DRA){\r\n    window.DRA.invoke('contact.openChatDlg', {\r\n            cid: {\r\n                appkey: 'cntaobao',\r\n                nick: '" + buyerNick + "'\r\n            }\r\n        })\r\n    }\r\n");
			}
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x000674F8 File Offset: 0x000656F8
		public bool IsValid()
		{
			bool flag = false;
			this._childChromeWin = base.FindWindowInDescendant("Aef_RenderWidgetHostHWND", "Chrome Legacy Window", false, new bool?(false));
			if (this._childChromeWin != null)
			{
				this.a(out flag);
			}
			return flag;
		}

		// Token: 0x04001E97 RID: 7831
		protected const string URL_CONTAIN = "alires:///offlinepkg/ww/pages/panel.html";

		// Token: 0x04001E98 RID: 7832
		[CompilerGenerated]
		private bool a;

		// Token: 0x04001E99 RID: 7833
		protected string _nick;

		// Token: 0x04001E9A RID: 7834
		protected WindowInfo _childChromeWin;

		// Token: 0x04001E9B RID: 7835
		private RemoteSessionsResponse b;

		// Token: 0x02000743 RID: 1859
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x060024FB RID: 9467 RVA: 0x0006753C File Offset: 0x0006573C
			internal bool e()
			{
				this.a = Win32Extend.FindWindowByClassAndName("coolmenu", null);
				return this.a != null;
			}

			// Token: 0x060024FC RID: 9468 RVA: 0x00067564 File Offset: 0x00065764
			internal bool d()
			{
				try
				{
					Process.GetProcessById(this.b);
				}
				catch (ArgumentException)
				{
					return true;
				}
				WindowInfo windowInfo = this.c.b();
				if (windowInfo != null)
				{
					AutomationElement automationElement = AutomationElement.FromHandle(windowInfo.HWnd).FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "退出时， 总是弹出该窗口"));
					if (automationElement != null)
					{
						TogglePattern togglePattern = (TogglePattern)automationElement.GetCurrentPattern(TogglePattern.Pattern);
						TogglePattern.TogglePatternInformation togglePatternInformation = togglePattern.Current;
						if (togglePatternInformation.ToggleState == ToggleState.On)
						{
							togglePattern.Toggle();
						}
					}
					WindowInfo windowInfo2 = windowInfo.FindWindowInDescendant("StandardButton", "确认", false, new bool?(false));
					if (windowInfo2 != null)
					{
						windowInfo2.Click(true);
					}
				}
				return false;
			}

			// Token: 0x04001E9C RID: 7836
			public WindowInfo a;

			// Token: 0x04001E9D RID: 7837
			public int b;

			// Token: 0x04001E9E RID: 7838
			public WinAliwwMainBuyer9 c;
		}
	}
}
