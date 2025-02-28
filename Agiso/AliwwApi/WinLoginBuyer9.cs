using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Automation;
using Agiso.Extensions;
using Agiso.Handler;
using Agiso.Utils;
using Agiso.Windows;

namespace Agiso.AliwwApi
{
	// Token: 0x02000712 RID: 1810
	public class WinLoginBuyer9 : WindowInfo, IWinValid
	{
		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x060023CA RID: 9162 RVA: 0x0005ECD4 File Offset: 0x0005CED4
		public WindowInfo LoginAreaWin
		{
			get
			{
				if (this.a == null)
				{
					this.a = base.FindWindowInDescendant("StandardWindow", "登录窗口", false, new bool?(false));
				}
				return this.a;
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x060023CB RID: 9163 RVA: 0x0005ED14 File Offset: 0x0005CF14
		public WinTxtInput NickInput
		{
			get
			{
				if (this.b == null && this.LoginBtn != null)
				{
					WindowInfo windowInfo = new WindowInfo(this.LoginBtn.GetParent());
					WindowInfo windowInfo2 = windowInfo.FindWindowInDescendant("EditComponent", null, false, new bool?(false));
					this.b = windowInfo2.Convert<WinTxtInput>();
				}
				return this.b;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x060023CC RID: 9164 RVA: 0x0005ED74 File Offset: 0x0005CF74
		public WinTxtInput PwdInput
		{
			get
			{
				if (this.c == null && this.NickInput != null)
				{
					WindowInfo windowInfo = base.FindWindowInDescendant("AliEdit", null, false, new bool?(false));
					this.c = windowInfo.Convert<WinTxtInput>();
				}
				return this.c;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x060023CD RID: 9165 RVA: 0x0005EDC0 File Offset: 0x0005CFC0
		public WinTxtInput PwdInput1
		{
			get
			{
				if (this.d == null && this.NickInput != null)
				{
					WindowInfo nextWindow = this.NickInput.GetNextWindow("EditComponent", null);
					this.d = nextWindow.Convert<WinTxtInput>();
				}
				return this.d;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x0005EE0C File Offset: 0x0005D00C
		public WindowInfo LoginBtn
		{
			get
			{
				if (this.e == null)
				{
					this.e = base.FindWindowInDescendant("StandardButton", "登 录", false, new bool?(false));
				}
				return this.e;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x060023CF RID: 9167 RVA: 0x0005EE4C File Offset: 0x0005D04C
		public WindowInfo LoginLoadingWin
		{
			get
			{
				return base.FindWindowInDescendant("StandardWindow", "登录状态窗口", false, new bool?(false));
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x0005EE74 File Offset: 0x0005D074
		public AutomationElement AutoLoginCheckboxAE
		{
			get
			{
				if (this.f == null)
				{
					AutomationElement automationElement = AutomationElement.FromHandle(base.HWnd).FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "登录窗口"));
					if (automationElement != null)
					{
						this.f = automationElement.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "自动登录"));
					}
				}
				return this.f;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x060023D1 RID: 9169 RVA: 0x0005EEE0 File Offset: 0x0005D0E0
		public AutomationElement RememberPasswordCheckboxAE
		{
			get
			{
				if (this.g == null)
				{
					AutomationElement automationElement = AutomationElement.FromHandle(base.HWnd).FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "登录窗口"));
					if (automationElement != null)
					{
						this.g = automationElement.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "记住密码"));
					}
				}
				return this.g;
			}
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x0005EF4C File Offset: 0x0005D14C
		public void DoLogin(string userNick, string pwd, bool longOpen, int retryTimes)
		{
			base.SetForegroundWindow();
			if (!this.NickInput.SetText(userNick, true, WinTxtInput.CheckSetTextMatchType.CheckByFullText, 10))
			{
				this.a(userNick, 3);
			}
			if (retryTimes == 0)
			{
				WinLoginBuyer9.a a = new WinLoginBuyer9.a();
				this.NickInput.SetForegroundWindow();
				this.NickInput.SetFocus();
				WindowsAPI.keybd_event(32, 0, 0, 0);
				a.a = null;
				a.b = base.ProcessId;
				Util.CheckWait(1000, new Func<bool>(a.c), 100);
				if (a.a != null && a.a.WinUserList != null)
				{
					a.a.WinUserList.Click(50, 10, true);
					Thread.Sleep(200);
				}
			}
			string text = this.NickInput.GetText();
			if (!(userNick != text))
			{
				TogglePattern togglePattern = (TogglePattern)this.RememberPasswordCheckboxAE.GetCurrentPattern(TogglePattern.Pattern);
				TogglePattern.TogglePatternInformation togglePatternInformation = togglePattern.Current;
				if (togglePatternInformation.ToggleState != ToggleState.On)
				{
					togglePattern.Toggle();
				}
				togglePattern = (TogglePattern)this.AutoLoginCheckboxAE.GetCurrentPattern(TogglePattern.Pattern);
				togglePatternInformation = togglePattern.Current;
				if (togglePatternInformation.ToggleState == ToggleState.On)
				{
					togglePattern.Toggle();
				}
				if (retryTimes > 0)
				{
					this.PwdInput.SetFocus();
					AppConfig.WriteLog("WinLoginBuyer9.DoLogin SendKeys pwd Start", LogType.LogForTraceHold, 1);
					SendKeysExtend.SendWait("{END}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}{BKSP}" + pwd);
					AppConfig.WriteLog("WinLoginBuyer9.DoLogin SendKeys pwd End", LogType.LogForTraceHold, 1);
					Thread.Sleep(100);
					this.PwdInput.KillFocus();
				}
				this.LoginBtn.Click(115, 15, true);
			}
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x0005F0F4 File Offset: 0x0005D2F4
		private void a(string A_0, int A_1)
		{
			bool flag = false;
			int num = 0;
			while (num < A_1 && !(flag = this.NickInput.SetText(A_0, false, WinTxtInput.CheckSetTextMatchType.CheckByFullText, 10)))
			{
				Thread.Sleep(100);
				num++;
			}
			if (!flag)
			{
				LogWriter.WriteLog("旺旺登录，账号输入错误，登录账号为" + A_0 + "，误登" + this.NickInput.GetText(), 1);
			}
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x0005F158 File Offset: 0x0005D358
		public bool ResetLoginBtn()
		{
			this.e = null;
			this.b = null;
			this.c = null;
			this.f = null;
			this.g = null;
			return this.LoginBtn != null && this.NickInput != null && this.PwdInput != null && this.AutoLoginCheckboxAE != null && this.RememberPasswordCheckboxAE != null;
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x0005F1C0 File Offset: 0x0005D3C0
		public WindowInfo GetLoginErrorComponent()
		{
			if (this.LoginAreaWin != null)
			{
				List<WindowInfo> list = this.LoginAreaWin.FindWindowsInDescendant("StandardWindow", "", new FindWindowOption
				{
					IsOnlyFirst = false
				}, new bool?(false));
				if (list.Count > 1)
				{
					return list[1].Visible ? list[1] : null;
				}
			}
			return null;
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000532C8 File Offset: 0x000514C8
		public bool ResetSendVerificationCodeBtn()
		{
			return false;
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x0005F22C File Offset: 0x0005D42C
		public static List<WinLoginBuyer9> GetList()
		{
			return WinFindableBase.GetList<WinLoginBuyer9>(new WindowStruct("StandardFrame", "阿里旺旺"), 0, false);
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x0005F254 File Offset: 0x0005D454
		public bool IsValid()
		{
			return this.LoginAreaWin != null && this.LoginBtn != null && this.NickInput != null && this.PwdInput != null;
		}

		// Token: 0x04001DD1 RID: 7633
		private WindowInfo a;

		// Token: 0x04001DD2 RID: 7634
		private WinTxtInput b;

		// Token: 0x04001DD3 RID: 7635
		private WinTxtInput c;

		// Token: 0x04001DD4 RID: 7636
		private WinTxtInput d;

		// Token: 0x04001DD5 RID: 7637
		private WindowInfo e;

		// Token: 0x04001DD6 RID: 7638
		private AutomationElement f;

		// Token: 0x04001DD7 RID: 7639
		private AutomationElement g;

		// Token: 0x02000713 RID: 1811
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x060023DB RID: 9179 RVA: 0x0005F288 File Offset: 0x0005D488
			internal bool c()
			{
				this.a = WinLoginUserListBuyer9.Get(this.b);
				return this.a != null;
			}

			// Token: 0x04001DD8 RID: 7640
			public WinLoginUserListBuyer9 a;

			// Token: 0x04001DD9 RID: 7641
			public int b;
		}
	}
}
