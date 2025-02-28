using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Agiso.AliwwApi.Object;
using Agiso.AliwwApi.Qn;
using Agiso.AliwwApi.Wangwang;
using Agiso.DbManager;
using Agiso.Extensions;
using Agiso.Handler;
using Agiso.Object;
using Agiso.Utils;
using Agiso.Windows;
using AliwwClient.Cache;
using AliwwClient.Object;
using AliwwClient.WebSocketServer;
using AliwwClient.WebSocketServer.Extensions;
using HtmlAgilityPack;

namespace Agiso.AliwwApi
{
	// Token: 0x02000719 RID: 1817
	public class AliwwTalkWindow : WindowInfo
	{
		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060023F7 RID: 9207 RVA: 0x0000ED2D File Offset: 0x0000CF2D
		// (set) Token: 0x060023F8 RID: 9208 RVA: 0x0000ED35 File Offset: 0x0000CF35
		public string UserNick { get; set; }

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x0005F784 File Offset: 0x0005D984
		private string CurrentSellerNick
		{
			get
			{
				if (this.b == null)
				{
					this.b = Util.GetMasterNick(this.UserNick);
				}
				return this.b;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x0005F7B8 File Offset: 0x0005D9B8
		// (set) Token: 0x060023FB RID: 9211 RVA: 0x0000ED3E File Offset: 0x0000CF3E
		public Aliww CurrentAliww
		{
			get
			{
				if (this.c == null)
				{
					this.c = new Aliww(this.CurrentUserCache.UserNick);
				}
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x060023FC RID: 9212 RVA: 0x0005F7F0 File Offset: 0x0005D9F0
		public UserCache CurrentUserCache
		{
			get
			{
				if (this.d == null)
				{
					this.d = AppConfig.GetUserCacheOrCreate(this.UserNick);
				}
				return this.d;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x0000ED47 File Offset: 0x0000CF47
		// (set) Token: 0x060023FE RID: 9214 RVA: 0x0000ED4F File Offset: 0x0000CF4F
		public AliwwVersion AliVersion { get; set; }

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060023FF RID: 9215 RVA: 0x0005F824 File Offset: 0x0005DA24
		public WinRichEdit MsgBoxWin
		{
			get
			{
				if (this.f == null)
				{
					WindowInfo windowInfo = null;
					if (this.SplitterBar != null)
					{
						windowInfo = this.SplitterBar.FindWindowInDescendant("RichEditComponent", null, false, new bool?(false));
					}
					if (windowInfo == null)
					{
						IntPtr intPtr = this.f();
						if (intPtr != IntPtr.Zero)
						{
							windowInfo = new WindowInfo(intPtr);
						}
					}
					this.f = windowInfo.Convert<WinRichEdit>();
				}
				return this.f;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002400 RID: 9216 RVA: 0x0005F8A0 File Offset: 0x0005DAA0
		public WindowInfo TeamForbidWin
		{
			get
			{
				if (this.g == null && this.MsgBoxWin != null)
				{
					WindowInfo parentWin = this.MsgBoxWin.GetParentWin();
					if (parentWin != null)
					{
						IntPtr intPtr = WindowsAPI.FindWindowEx(parentWin.HWnd, this.MsgBoxWin.HWnd, "StandardWindow", "");
						if (intPtr != IntPtr.Zero)
						{
							this.g = new WindowInfo(intPtr);
						}
					}
				}
				return this.g;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x0005F91C File Offset: 0x0005DB1C
		public IWinHtmlRecentContainer ChatView
		{
			get
			{
				if (this.h == null)
				{
					for (int i = 0; i < 15; i++)
					{
						if (this.SplitterBar != null)
						{
							this.h = WinChromeRecent.Get(this.SplitterBar.HWnd);
							if (this.h != null)
							{
								break;
							}
						}
						Thread.Sleep(100);
					}
				}
				return this.h;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06002402 RID: 9218 RVA: 0x0005F980 File Offset: 0x0005DB80
		private WindowInfo SplitterBar
		{
			get
			{
				if (this.i == null)
				{
					this.i = base.FindWindowInDescendant("SplitterBar", null, false, new bool?(false));
				}
				return this.i;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06002403 RID: 9219 RVA: 0x0005F9BC File Offset: 0x0005DBBC
		public WindowInfo ButtonClose
		{
			get
			{
				if (this.j == null)
				{
					if (this.SplitterBar != null)
					{
						this.j = this.SplitterBar.FindWindowInDescendant("StandardButton", "关闭", false, new bool?(false));
					}
					if (this.j == null)
					{
						IntPtr childHandleByClassTreePath = Win32Extend.GetChildHandleByClassTreePath(base.HWnd, new string[] { "StandardWindow", "SplitterBar", "StandardWindow", "StandardButton" });
						if (childHandleByClassTreePath != IntPtr.Zero)
						{
							WindowInfo windowInfo = new WindowInfo(childHandleByClassTreePath);
							this.j = windowInfo;
						}
					}
				}
				return this.j;
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x0005FA70 File Offset: 0x0005DC70
		public WindowInfo ButtonSend
		{
			get
			{
				if (this.k == null && this.SplitterBar != null)
				{
					this.k = this.SplitterBar.FindWindowInDescendant("StandardButton", "发送", false, new bool?(false));
				}
				return this.k;
			}
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x0005FAC0 File Offset: 0x0005DCC0
		public static AliwwTalkWindow ParseFromWindowInfo(WindowInfo source)
		{
			AliwwTalkWindow aliwwTalkWindow;
			if (source == null)
			{
				aliwwTalkWindow = null;
			}
			else
			{
				aliwwTalkWindow = new AliwwTalkWindow
				{
					HWnd = source.HWnd,
					Info = 
					{
						ClassName = source.Info.ClassName,
						WindowName = source.Info.WindowName
					}
				};
			}
			return aliwwTalkWindow;
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x0005FB18 File Offset: 0x0005DD18
		private List<WindowInfo> h()
		{
			return Win32Extend.GetWindowListByClassAndName(new WindowStruct("#32770", "系统提示"), base.ProcessId, false);
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x0005FB44 File Offset: 0x0005DD44
		public void CloseCurrentChat()
		{
			if (this.AliVersion == AliwwVersion.QianNiu9)
			{
				this.b();
				Thread.Sleep(200);
				Win32Extend.CloseWindowListByClassAndName("Qt5152QWindowIcon", "-会话移除二次确认", 0, true, 200);
			}
			else
			{
				List<WinCloseTip> list = WinCloseTip.GetList(0);
				if (list != null)
				{
					list.ForEach(new Action<WinCloseTip>(AliwwTalkWindow.<>c.<>9.b));
				}
				if (this.ButtonClose != null)
				{
					this.ButtonClose.Click(true);
				}
				else
				{
					base.KeyPressEscape(true);
				}
				ChatWindowType chatWindowType = this.chatWindowType;
				ChatWindowType chatWindowType2 = chatWindowType;
				if (chatWindowType2 != ChatWindowType.CustomerBench)
				{
					if (chatWindowType2 == ChatWindowType.ChatWindow)
					{
						base.Close(true);
					}
				}
				else
				{
					Application.DoEvents();
					Thread.Sleep(200);
					List<WindowInfo> list2 = this.h();
					if (list2 != null)
					{
						list2.ForEach(new Action<WindowInfo>(AliwwTalkWindow.<>c.<>9.b));
					}
					List<WinCloseTip> list3 = WinCloseTip.GetList(0);
					if (list3 != null)
					{
						list3.ForEach(new Action<WinCloseTip>(AliwwTalkWindow.<>c.<>9.a));
					}
				}
			}
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x0005FC68 File Offset: 0x0005DE68
		public string GetCurrentTargetUserNick()
		{
			switch (this.AliVersion)
			{
			case AliwwVersion.AliwwBuyer2014:
			case AliwwVersion.AliwwAlbb2014:
			case AliwwVersion.TradeManager2014:
			case AliwwVersion.TradeManager2015:
				return base.Info.WindowName.Replace(Aliww.GetWindowNameSuffix(this.UserNick), "");
			}
			ChatWindowType chatWindowType = this.chatWindowType;
			ChatWindowType chatWindowType2 = chatWindowType;
			if (chatWindowType2 != ChatWindowType.CustomerBench)
			{
				if (chatWindowType2 == ChatWindowType.ChatWindow)
				{
					return base.Info.WindowName.Replace(Aliww.GetWindowNameSuffix(this.UserNick), "");
				}
			}
			ActiveUserInfo activeUserInfo = this.g();
			return (activeUserInfo != null) ? activeUserInfo.Uid.Replace("cntaobao", "") : null;
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x0005FD3C File Offset: 0x0005DF3C
		public AliwwMsgElement GetCurrentLastReceiveMessage()
		{
			AliwwMsgElement aliwwMsgElement;
			if (this.ChatView != null)
			{
				aliwwMsgElement = this.ChatView.GetLastReceiveMessage(this.UserNick);
			}
			else
			{
				aliwwMsgElement = null;
			}
			return aliwwMsgElement;
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x0005FD6C File Offset: 0x0005DF6C
		public AliwwMsgElement GetRealLastRcvMsg(bool getSellerLastMsg)
		{
			UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(this.UserNick);
			AliwwMsgElement aliwwMsgElement;
			if (!userCacheOrCreate.IsRecentSessionNull)
			{
				aliwwMsgElement = new WinChromeRecent
				{
					HWnd = base.HWnd
				}.GetRealLastRcvMsg(this.UserNick, getSellerLastMsg);
			}
			else
			{
				if (this.ChatView != null)
				{
					WinChromeRecent winChromeRecent = this.ChatView as WinChromeRecent;
					if (winChromeRecent != null)
					{
						return winChromeRecent.GetRealLastRcvMsg(this.UserNick, getSellerLastMsg);
					}
				}
				aliwwMsgElement = null;
			}
			return aliwwMsgElement;
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x0005FDE8 File Offset: 0x0005DFE8
		private ActiveUserInfo g()
		{
			ActiveUserInfo activeUserInfo;
			if (this.ChatView == null)
			{
				activeUserInfo = null;
			}
			else if (!this.CurrentUserCache.IsSessionNull)
			{
				activeUserInfo = this.CurrentUserCache.Session.GetActiveUserInfo();
			}
			else
			{
				activeUserInfo = this.a(5, "cntaobao");
			}
			return activeUserInfo;
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x0005FE38 File Offset: 0x0005E038
		public string GetNewMessage()
		{
			return this.ChatView.GetInnerText(this.UserNick);
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x0005FE58 File Offset: 0x0005E058
		private IntPtr f()
		{
			IntPtr intPtr = IntPtr.Zero;
			ChatWindowType chatWindowType = this.chatWindowType;
			ChatWindowType chatWindowType2 = chatWindowType;
			if (chatWindowType2 != ChatWindowType.CustomerBench)
			{
				if (chatWindowType2 == ChatWindowType.ChatWindow)
				{
					intPtr = this.e();
				}
			}
			else
			{
				intPtr = this.d();
			}
			return intPtr;
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x0005FE94 File Offset: 0x0005E094
		private IntPtr e()
		{
			IntPtr intPtr;
			if (this == null)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				IntPtr hwnd = base.HWnd;
				IntPtr intPtr2 = IntPtr.Zero;
				switch (this.AliVersion)
				{
				case AliwwVersion.AliwwSeller2012ReadView:
				case AliwwVersion.AliwwSeller2013:
				case AliwwVersion.AliwwBuyer2014:
				case AliwwVersion.AliwwBuyer9:
				case AliwwVersion.AliwwAlbb2014:
				case AliwwVersion.TradeManager2014:
				case AliwwVersion.TradeManager2015:
					intPtr2 = Win32Extend.GetChildHandleByClassTreePath(hwnd, new string[] { "SplitterBar", "StandardWindow", "RichEditComponent" });
					if (intPtr2 == IntPtr.Zero)
					{
						intPtr2 = Win32Extend.GetChildHandleByClassTreePath(hwnd, new string[] { "StandardWindow", "StandardWindow", "SplitterBar", "StandardWindow", "RichEditComponent" });
					}
					break;
				}
				intPtr = intPtr2;
			}
			return intPtr;
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x0005FF7C File Offset: 0x0005E17C
		private IntPtr d()
		{
			IntPtr intPtr;
			if (this == null)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				IntPtr hwnd = base.HWnd;
				IntPtr intPtr2 = IntPtr.Zero;
				IntPtr intPtr3 = IntPtr.Zero;
				IntPtr intPtr4 = IntPtr.Zero;
				switch (this.AliVersion)
				{
				case AliwwVersion.QianNiu:
				case AliwwVersion.QianNiu2:
				case AliwwVersion.QianNiu3:
				case AliwwVersion.QianNiu5:
				{
					List<IntPtr> childHandleList = Win32Extend.GetChildHandleList(hwnd, "StandardWindow", null);
					foreach (IntPtr intPtr5 in childHandleList)
					{
						List<IntPtr> childHandleList2 = Win32Extend.GetChildHandleList(intPtr5, "SplitterBar", null);
						foreach (IntPtr intPtr6 in childHandleList2)
						{
							List<IntPtr> childHandleList3 = Win32Extend.GetChildHandleList(intPtr6, "StandardWindow", null);
							foreach (IntPtr intPtr7 in childHandleList3)
							{
								List<IntPtr> childHandleList4 = Win32Extend.GetChildHandleList(intPtr7, "RichEditComponent", null);
								if (childHandleList4.Count > 0)
								{
									return childHandleList4[0];
								}
							}
						}
					}
					break;
				}
				case AliwwVersion.AliwwSeller2012ReadView:
				case AliwwVersion.AliwwSeller2013:
					intPtr2 = WindowsAPI.FindWindowEx(hwnd, IntPtr.Zero, "StandardWindow", null);
					intPtr2 = WindowsAPI.FindWindowEx(hwnd, intPtr2, "StandardWindow", null);
					intPtr2 = WindowsAPI.FindWindowEx(hwnd, intPtr2, "StandardWindow", null);
					intPtr2 = WindowsAPI.FindWindowEx(intPtr2, IntPtr.Zero, "SplitterBar", null);
					intPtr3 = WindowsAPI.FindWindowEx(intPtr2, IntPtr.Zero, "StandardWindow", null);
					intPtr3 = WindowsAPI.FindWindowEx(intPtr2, intPtr3, "StandardWindow", null);
					intPtr4 = WindowsAPI.FindWindowEx(intPtr3, IntPtr.Zero, "RichEditComponent", null);
					break;
				case AliwwVersion.AliwwBuyer9:
					intPtr4 = Win32Extend.GetChildHandleByClassTreePath(hwnd, new string[] { "SplitterBar", "StandardWindow", "RichEditComponent" });
					break;
				case AliwwVersion.AliwwAlbb2014:
				case AliwwVersion.TradeManager2014:
				case AliwwVersion.TradeManager2015:
					return Win32Extend.GetChildHandleByClassTreePath(hwnd, new string[] { "SplitterBar", "StandardWindow", "RichEditComponent" });
				}
				intPtr = intPtr4;
			}
			return intPtr;
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000601EC File Offset: 0x0005E3EC
		public ErrCodeInfo SendToTalkWindowWholeMsgForAutoReply(string buyerNick, string buyerOpenUid, string msgBody)
		{
			int num = 0;
			while (num < 3 && (this.chatWindowType == ChatWindowType.CustomerBench && !this.CheckCallUserInfoEquals(buyerNick, buyerOpenUid, 1, "cntaobao")))
			{
				AliwwTalkWindow aliwwTalkWindow = null;
				this.CurrentAliww.CallUser(buyerNick, buyerOpenUid, ref aliwwTalkWindow);
				num++;
			}
			return this.SendToTalkWindowWholeMsg(buyerNick, buyerOpenUid, msgBody, "cntaobao");
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x0006024C File Offset: 0x0005E44C
		public ErrCodeInfo SendToTalkWindowWholeMsg(string buyerNick, string buyerOpenUid, string msgBody, string siteId = "cntaobao")
		{
			Application.DoEvents();
			if (this.AliVersion != AliwwVersion.QianNiu9)
			{
				if (this.MsgBoxWin == null)
				{
					return new ErrCodeInfo(ErrCodeType.SendFailMsgInputControlNotFound);
				}
				this.MsgBoxWin.ClearText();
			}
			else if (this.CurrentUserCache.IsSessionNull)
			{
				return new ErrCodeInfo(ErrCodeType.SendFailSessionIsNull);
			}
			int num = 0;
			int num2 = 0;
			List<ErrCodeInfo> list = new List<ErrCodeInfo>();
			msgBody = msgBody.Replace("\\", "\\\\");
			string[] array = msgBody.Split(new string[] { "\r\t\r\n", "\n\t\n", "{$旺旺分段符}" }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				string text2 = text.Trim();
				if (!string.IsNullOrEmpty(text2))
				{
					List<ErrCodeInfo> list2 = this.a(buyerNick, buyerOpenUid, text, ref num, ref num2, siteId);
					list.AddRange(list2);
				}
			}
			if (this.AliVersion != AliwwVersion.QianNiu9)
			{
				this.MsgBoxWin.SetInputEnable();
			}
			ErrCodeInfo errCodeInfo;
			if (num2 == 0)
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.SendSucc);
			}
			else if (num == 0)
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.SendFailAll, list);
			}
			else
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.SendFailPiece, list);
			}
			return errCodeInfo;
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x00060398 File Offset: 0x0005E598
		private List<ErrCodeInfo> a(string A_0, string A_1, string A_2, ref int A_3, ref int A_4, string A_5)
		{
			List<ErrCodeInfo> list = new List<ErrCodeInfo>();
			do
			{
				int num;
				if (A_2.Length <= AppConfig.CurrentSystemSettingInfo.AliwwMessageLengthMax)
				{
					num = A_2.Length;
				}
				else
				{
					num = A_2.LastIndexOf("\n", AppConfig.CurrentSystemSettingInfo.AliwwMessageLengthMax);
					if (num <= 0)
					{
						num = AppConfig.CurrentSystemSettingInfo.AliwwMessageLengthMax;
					}
				}
				string text = A_2.Substring(0, num).Trim();
				ErrCodeInfo errCodeInfo = this.d(A_0, A_1, text, A_5);
				list.Add(errCodeInfo);
				if (errCodeInfo.ErrCode < ErrCodeType.Undefined)
				{
					goto IL_00E0;
				}
				A_3++;
				if (AppConfig.CurrentSystemSettingInfo.SendInterval < 300)
				{
					Thread.Sleep(300);
				}
				else
				{
					Thread.Sleep(AppConfig.CurrentSystemSettingInfo.SendInterval);
				}
				A_2 = A_2.Substring(num).Trim();
			}
			while (!string.IsNullOrEmpty(A_2));
			return list;
			IL_00E0:
			A_4++;
			return list;
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x00060494 File Offset: 0x0005E694
		private ErrCodeInfo d(string A_0, string A_1, string A_2, string A_3)
		{
			A_2 = ((A_2 != null) ? A_2.Trim() : null);
			ErrCodeInfo errCodeInfo;
			if (string.IsNullOrEmpty(A_2))
			{
				LogWriter.WriteLog("msg is null", 1);
				errCodeInfo = new ErrCodeInfo();
			}
			else if (this.AliVersion == AliwwVersion.QianNiu9)
			{
				errCodeInfo = this.c(A_0, A_1, A_2, A_3);
			}
			else
			{
				errCodeInfo = this.b(A_0, A_1, A_2, A_3);
			}
			return errCodeInfo;
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000604F4 File Offset: 0x0005E6F4
		private ErrCodeInfo c(string A_0, string A_1, string A_2, string A_3)
		{
			AliwwTalkWindow.a a = new AliwwTalkWindow.a();
			a.a = A_1;
			a.b = this;
			bool flag = false;
			Win32Extend.CloseWindowListByClassAndName("Qt5152QWindowToolSaveBits", "AppGuideView", base.ProcessId, false, 200);
			Win32Extend.CloseWindowListByClassAndName("Qt5152QWindowIcon", "错误", base.ProcessId, true, 200);
			Win32Extend.CloseWindowListByClassAndName("Qt5152QWindowIcon", "-会话移除二次确认", base.ProcessId, true, 200);
			Win32Extend.CloseWindowListByClassAndName("Qt5152QWindowIcon", "询问", base.ProcessId, true, 200);
			List<WinFuwuTip> list = WinFuwuTip.GetList(base.ProcessId);
			if (list != null)
			{
				list.ForEach(new Action<WinFuwuTip>(a.c));
			}
			ErrCodeInfo errCodeInfo;
			if (!Util.CheckWait(3000, new Func<bool>(a.c), 200))
			{
				string text = AppDomain.CurrentDomain.BaseDirectory + "\\ScreenshotAliTalkWinDisable\\";
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				List<WindowInfo> allDesktopWindows = Win32Extend.GetAllDesktopWindows();
				foreach (WindowInfo windowInfo in allDesktopWindows)
				{
					if (windowInfo.ProcessId == base.ProcessId && windowInfo.Visible && Win32Extend.GetOwnerWindow(windowInfo.HWnd) == base.HWnd)
					{
						using (Bitmap bitmapFromDC = windowInfo.GetBitmapFromDC(0))
						{
							string text2 = string.Format("{0}{1}{2}.png", text, DateTime.Now.ToString("yyyyMMddHHmmssfff"), windowInfo.HWnd.ToInt64());
							if (!File.Exists(text2))
							{
								bitmapFromDC.Save(text2, ImageFormat.Png);
							}
						}
					}
				}
				errCodeInfo = new ErrCodeInfo(ErrCodeType.SendFailAliTalkWinDisable);
			}
			else
			{
				AliwwTalkWindow.l = a.a;
				this.FocusSendWin();
				base.PressControlAndOtherKey(65);
				base.PressKey(8, 3);
				Thread.Sleep(200);
				for (int i = 0; i < 4; i++)
				{
					BehaviorBase session = this.CurrentUserCache.GetSession(a.a);
					if (flag = session != null && session.smethod_0(A_0, a.a, A_2, 0, "cntaobao"))
					{
						break;
					}
					Thread.Sleep(200);
				}
				if (!flag)
				{
					errCodeInfo = new ErrCodeInfo(ErrCodeType.SendFailSetTextIntoMsgInputFail);
				}
				else if (!this.CurrentUserCache.IsRecentSessionNull)
				{
					AliwwTalkWindow.b b = new AliwwTalkWindow.b();
					b.b = a;
					b.a = DateTime.Now;
					this.CurrentUserCache.LastSendMsgSuccDateTime = null;
					for (int j = 0; j < 4; j++)
					{
						this.c();
						int num = 500;
						Func<bool> func;
						if ((func = b.c) == null)
						{
							func = (b.c = new Func<bool>(b.d));
						}
						if (Util.CheckWait(num, func, 100))
						{
							return new ErrCodeInfo(ErrCodeType.SendSuccPiece);
						}
					}
					if (AppConfig.AllowAutoLogin && Util.IsEmptyList<WinFuwuTip>(WinFuwuTip.GetList(base.ProcessId)))
					{
						ClipboardProxy.Clear();
						Thread.Sleep(100);
						base.PressControlAndOtherKey(67);
						string text3 = ClipboardProxy.GetText(10);
						if (!string.IsNullOrEmpty((text3 != null) ? text3.Trim() : null))
						{
							return new ErrCodeInfo(ErrCodeType.SendFailTeamForbid);
						}
					}
					errCodeInfo = new ErrCodeInfo(ErrCodeType.SendFailPiece);
				}
				else
				{
					Thread.Sleep(300);
					for (int k = 0; k < 4; k++)
					{
						this.c();
						Thread.Sleep(200);
						Win32Extend.CloseWindowListByClassAndName("Qt5152QWindowIcon", "错误", base.ProcessId, true, 200);
						if (Util.IsEmptyList<WinFuwuTip>(WinFuwuTip.GetList(base.ProcessId)))
						{
							ClipboardProxy.Clear();
							this.FocusSendWin();
							if (!base.DoSendKeys("^a", true))
							{
								base.PressControlAndOtherKey(65);
							}
							base.PressControlAndOtherKey(67);
							Thread.Sleep(200);
							string text4 = ClipboardProxy.GetText(10);
							if (string.IsNullOrEmpty((text4 != null) ? text4.Trim() : null) && !ClipboardProxy.ContainsImage(10))
							{
								return new ErrCodeInfo(ErrCodeType.SendSuccPiece);
							}
						}
						Thread.Sleep(200);
					}
					errCodeInfo = new ErrCodeInfo(ErrCodeType.SendFailPiece);
				}
			}
			return errCodeInfo;
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x0006098C File Offset: 0x0005EB8C
		private ErrCodeInfo b(string A_0, string A_1, string A_2, string A_3)
		{
			AliwwTalkWindow.c c = new AliwwTalkWindow.c();
			c.a = this;
			ErrCodeInfo errCodeInfo;
			if (!this.MsgBoxWin.ClearText())
			{
				LogWriter.WriteLog("Clean up input box text failed", 1);
				errCodeInfo = new ErrCodeInfo();
			}
			else
			{
				bool flag = false;
				DateTime now = DateTime.Now;
				c.b = 0;
				int num = 0;
				if (!this.CurrentUserCache.IsSessionNull)
				{
					for (int i = 0; i < 4; i++)
					{
						if (!this.MsgBoxWin.ClearText())
						{
							Thread.Sleep(100);
						}
						else
						{
							BehaviorBase session = this.CurrentUserCache.GetSession(A_1);
							int num3;
							if (flag = session != null && session.smethod_0(A_0, A_1, A_2, 0, "cntaobao"))
							{
								int num2 = c.b;
								c.b = num2 + 1;
								num3 = 3000;
							}
							else
							{
								num++;
								num3 = 500;
							}
							AliwwTalkWindow.d d = new AliwwTalkWindow.d();
							d.b = c;
							d.a = new CancellationTokenSource();
							try
							{
								flag = Util.CheckWait(num3, new Func<bool>(d.c), d.a.Token, 100);
							}
							finally
							{
								if (d.a != null)
								{
									((IDisposable)d.a).Dispose();
								}
							}
							if (flag)
							{
								int textLength = this.MsgBoxWin.GetTextLength();
								if ((double)textLength < Math.Ceiling((double)A_2.Length * 1.5))
								{
									break;
								}
								flag = false;
							}
							Thread.Sleep(100);
						}
					}
				}
				if (!flag)
				{
					int textLength2 = this.MsgBoxWin.GetTextLength();
					string text = this.MsgBoxWin.GetText();
					bool flag2 = text == " " || !string.IsNullOrWhiteSpace(text);
					LogWriter.WriteLog(string.Format("接口填写消息失败，{0}，{1}，insertSuccCount：{2}，insertFailCount：{3}", new object[] { textLength2, flag2, c.b, num }), 1);
					try
					{
						for (int j = 0; j < 4; j++)
						{
							if (this.MsgBoxWin.ClearText())
							{
								if (AppConfig.CurrentSystemSettingInfo.AllowSendExpression)
								{
									try
									{
										this.MsgBoxWin.SetText(A_2, true, WinTxtInput.CheckSetTextMatchType.CheckByFirstChar, 1);
										goto IL_028F;
									}
									catch
									{
										this.MsgBoxWin.SetText(A_2, false, WinTxtInput.CheckSetTextMatchType.CheckByFirstChar, 1);
										goto IL_028F;
									}
									goto IL_0245;
								}
								goto IL_0245;
								IL_028F:
								string text2 = this.MsgBoxWin.GetText().Trim(new char[] { ' ' });
								if (string.IsNullOrEmpty(text2))
								{
									if (j == 0)
									{
										A_2 = "\r\n" + A_2;
										goto IL_0284;
									}
									goto IL_0284;
								}
								else
								{
									if (!this.b(text2, A_2))
									{
										goto IL_0284;
									}
									flag = true;
									break;
								}
								IL_0245:
								this.MsgBoxWin.SetInputDisable();
								this.MsgBoxWin.SetText(A_2, false, WinTxtInput.CheckSetTextMatchType.CheckByFirstChar, 1);
								goto IL_028F;
							}
							Thread.Sleep(100);
							IL_0284:;
						}
					}
					finally
					{
						this.MsgBoxWin.SetInputEnable();
					}
				}
				if (flag)
				{
					int k = 0;
					while (k < 5)
					{
						if (!this.CheckCallUserInfoEquals(A_0, A_1, 4, A_3))
						{
							return new ErrCodeInfo(ErrCodeType.SendFailCheckTargetNickFailAfterSetText);
						}
						int textLength3 = this.MsgBoxWin.GetTextLength();
						if ((double)textLength3 >= Math.Ceiling((double)A_2.Length * 1.5))
						{
							return new ErrCodeInfo(ErrCodeType.SendFailSetTextIntoMsgRepeat);
						}
						this.PressSendButtonToMessageBox();
						Thread.Sleep(100);
						string text3 = this.MsgBoxWin.GetText().Trim();
						if (!string.IsNullOrEmpty(text3) || textLength3 != 0)
						{
							if (this.AliVersion == AliwwVersion.QianNiu5)
							{
								List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(new WindowStruct("PopupWindow", ""), 0, false);
								if (windowListByClassAndName != null && windowListByClassAndName.Count > 0)
								{
									foreach (WindowInfo windowInfo in windowListByClassAndName)
									{
										if (windowInfo.ProcessId == base.ProcessId && windowInfo.Visible)
										{
											return new ErrCodeInfo(ErrCodeType.SendFailTeamForbid);
										}
									}
								}
								WindowInfo teamForbidWin = this.TeamForbidWin;
								if (((teamForbidWin != null) ? teamForbidWin.HWnd : IntPtr.Zero) != IntPtr.Zero && this.TeamForbidWin.Visible)
								{
									return new ErrCodeInfo(ErrCodeType.SendFailTeamForbid);
								}
							}
							k++;
						}
						else
						{
							IL_0441:
							string text4 = this.MsgBoxWin.GetText().TrimEnd(new char[0]);
							if (string.IsNullOrEmpty(text4))
							{
								return new ErrCodeInfo(ErrCodeType.SendSuccPiece);
							}
							return new ErrCodeInfo(ErrCodeType.SendFailWhileSendPiece);
						}
					}
					goto IL_0441;
				}
				errCodeInfo = new ErrCodeInfo(ErrCodeType.SendFailSetTextIntoMsgInputFail);
			}
			return errCodeInfo;
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x00060E68 File Offset: 0x0005F068
		public void FocusSendWin()
		{
			base.SetForegroundWindow();
			base.Click(36, 58, true);
			Thread.Sleep(100);
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName("Qt5152QWindowPopupSaveBits", "千牛工作台", base.ProcessId, true, true, false);
			if (!Util.IsEmptyList<WindowInfo>(windowListByClassAndName))
			{
				foreach (WindowInfo windowInfo in windowListByClassAndName)
				{
					windowInfo.Close(true);
					Thread.Sleep(200);
				}
			}
			List<WindowInfo> windowListByClassAndName2 = Win32Extend.GetWindowListByClassAndName("SoPY_Status", "", base.ProcessId, true, true, false);
			if (!Util.IsEmptyList<WindowInfo>(windowListByClassAndName2))
			{
				foreach (WindowInfo windowInfo2 in windowListByClassAndName2)
				{
					windowInfo2.Close(true);
					Thread.Sleep(200);
				}
			}
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x00060F74 File Offset: 0x0005F174
		private bool a(out Agiso.Windows.Rectangle A_0, out Agiso.Windows.Rectangle A_1, out Agiso.Windows.Rectangle A_2, out WindowInfo A_3, out WindowInfo A_4)
		{
			WindowsAPI.GetWindowRect(base.HWnd, out A_0);
			List<WindowInfo> list = Win32Extend.GetChildHandleList(base.HWnd, "Qt5152QWindowIcon", "千牛工作台").Select(new Func<IntPtr, WindowInfo>(AliwwTalkWindow.<>c.<>9.a)).ToList<WindowInfo>();
			List<WindowInfo> list2 = list.Where(new Func<WindowInfo, bool>(AliwwTalkWindow.<>c.<>9.a)).ToList<WindowInfo>();
			bool flag;
			if (list2.Count != 2)
			{
				if (!AppConfig.AllowAutoLogin)
				{
					LogWriter.WriteLog(string.Format("可见窗口数量不对，{0}", list2.Count), 1);
				}
				List<Agiso.Windows.Rectangle> list3 = new List<Agiso.Windows.Rectangle>();
				if (list.Count == 0)
				{
					A_1 = default(Agiso.Windows.Rectangle);
					A_2 = default(Agiso.Windows.Rectangle);
					A_3 = null;
					A_4 = null;
					flag = false;
				}
				else
				{
					foreach (WindowInfo windowInfo in list)
					{
						Agiso.Windows.Rectangle rectangle;
						WindowsAPI.GetWindowRect(windowInfo.HWnd, out rectangle);
						list3.Add(rectangle);
					}
					Agiso.Windows.Rectangle rectangle2 = list3[0];
					Agiso.Windows.Rectangle rectangle3 = list3[0];
					WindowInfo windowInfo2 = list[0];
					WindowInfo windowInfo3 = list[0];
					for (int i = 1; i < list3.Count; i++)
					{
						Agiso.Windows.Rectangle rectangle4 = list3[i];
						if (rectangle2.Left > rectangle4.Left)
						{
							rectangle2 = rectangle4;
							windowInfo2 = list[i];
						}
						else if (rectangle3.GetHeight() < rectangle4.GetHeight())
						{
							rectangle3 = rectangle4;
							windowInfo3 = list[i];
						}
					}
					A_1 = rectangle2;
					A_2 = rectangle3;
					A_3 = windowInfo2;
					A_4 = windowInfo3;
					flag = false;
				}
			}
			else
			{
				Agiso.Windows.Rectangle rectangle5;
				WindowsAPI.GetWindowRect(list2[0].HWnd, out rectangle5);
				Agiso.Windows.Rectangle rectangle6;
				WindowsAPI.GetWindowRect(list2[1].HWnd, out rectangle6);
				if (rectangle5.GetHeight() > rectangle6.GetHeight())
				{
					A_1 = rectangle6;
					A_2 = rectangle5;
					A_3 = list2[1];
					A_4 = list2[0];
				}
				else
				{
					A_1 = rectangle5;
					A_2 = rectangle6;
					A_3 = list2[0];
					A_4 = list2[1];
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000611EC File Offset: 0x0005F3EC
		private void c()
		{
			Win32Extend.CloseWindowListByClassAndName("Qt5152QWindowIcon", "错误", base.ProcessId, true, 200);
			List<WinFuwuTip> list = WinFuwuTip.GetList(base.ProcessId);
			if (!Util.IsEmptyList<WinFuwuTip>(list))
			{
				list.ForEach(new Action<WinFuwuTip>(AliwwTalkWindow.<>c.<>9.a));
				if (!Util.CheckWait(500, new Func<bool>(this.a), 100))
				{
					LogWriter.WriteLog("点击发送按钮时，检测到服务态度提醒窗口，停止点击", 1);
					return;
				}
			}
			this.FocusSendWin();
			base.KeyPressEnter(false);
			Agiso.Windows.Rectangle rectangle;
			Agiso.Windows.Rectangle rectangle2;
			Agiso.Windows.Rectangle rectangle3;
			WindowInfo windowInfo;
			WindowInfo windowInfo2;
			bool flag = this.a(out rectangle, out rectangle2, out rectangle3, out windowInfo, out windowInfo2);
			int width = rectangle.GetWidth();
			int height = rectangle.GetHeight();
			int num = (flag ? (rectangle3.GetWidth() + 65) : 545);
			base.SetForegroundWindow();
			base.Click(width - num, height - 25, true);
			num = (flag ? (rectangle3.GetWidth() + 83) : 545);
			base.Click(width - num, height - 30, true);
			Thread.Sleep(100);
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x0000ED58 File Offset: 0x0000CF58
		private void b()
		{
			base.KeyPressEscape(true);
			Thread.Sleep(100);
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x0006130C File Offset: 0x0005F50C
		private bool b(string A_0, string A_1)
		{
			bool flag;
			if (A_0 != null && AppConfig.NewLineRegex.IsMatch(A_0))
			{
				flag = true;
			}
			else if (string.IsNullOrWhiteSpace(A_0))
			{
				flag = false;
			}
			else if (A_0.StartsWith(A_1.Substring(0, 1)))
			{
				flag = true;
			}
			else if (A_0.Length > 2 && A_1.Length > 2 && A_0.Contains(A_1.Substring(1, 2)))
			{
				flag = true;
			}
			else
			{
				if (!AliwwTalkWindow.m.IsMatch(A_1))
				{
					LogWriter.WriteLog(string.Format("errCode: -206! 验证消息输入不通过! :{0}:{1}:", A_1.Substring(0, 1), (A_0.Length <= 10) ? A_0 : A_0.Substring(0, 10)), 1);
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000613C4 File Offset: 0x0005F5C4
		public void PressSendButtonToMessageBox()
		{
			WindowInfo buttonSend = this.ButtonSend;
			if (buttonSend != null)
			{
				buttonSend.Click(false);
			}
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x000613E4 File Offset: 0x0005F5E4
		public int AutoReply(AldsAccountInfo account, out string result, out bool hasTransfer, out string contactNick)
		{
			hasTransfer = false;
			contactNick = "";
			int num;
			if (this.ChatView == null)
			{
				result = "";
				num = -90000;
			}
			else
			{
				try
				{
					AliwwMsgElement lastReceiveMessage = this.ChatView.GetLastReceiveMessage(this.UserNick);
					if (lastReceiveMessage == null)
					{
						result = "";
						num = -90010;
					}
					else if (lastReceiveMessage.IsSysMsg)
					{
						result = "";
						num = -90015;
					}
					else
					{
						contactNick = this.GetCurrentTargetUserNick();
						string senderSite = lastReceiveMessage.SenderSite;
						if (string.IsNullOrEmpty(contactNick))
						{
							result = "咨询者未知";
							num = -90020;
						}
						else if (contactNick.Contains("-->") || contactNick.Contains("--&gt"))
						{
							result = "";
							num = -90021;
						}
						else
						{
							string text = lastReceiveMessage.ContentText;
							if (text == null)
							{
								if (lastReceiveMessage.ContentHtml == null)
								{
									result = "问的问题为空";
									return -90022;
								}
								if (lastReceiveMessage.ContentHtml.ToLower().Contains("img"))
								{
								}
							}
							text = text.Trim();
							if (text.Contains("一个震屏") || text.Contains("一个振屏"))
							{
								result = "震屏不答复";
								num = -90025;
							}
							else
							{
								if (text.EndsWith("（对方正在使用“淘宝客户端”收发消息）"))
								{
									text = text.Replace("（对方正在使用“淘宝客户端”收发消息）", "");
								}
								if (text.EndsWith("（对方正在使用“旺信”收发消息）"))
								{
									text = text.Replace("（对方正在使用“旺信”收发消息）", "");
								}
								if (this.UserNick.Equals(lastReceiveMessage.SenderNick))
								{
									result = "";
									num = -90026;
								}
								else
								{
									text = text.Replace("&nbsp;", " ");
									bool flag;
									num = this.a(account, text, contactNick, senderSite, "", lastReceiveMessage.SendTime, MsgFrom.FromWindow, out result, out hasTransfer, out flag);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					result = "智能答复时异常！";
					LogWriter.WriteLog(ex.ToString(), 1);
					num = -90999;
				}
			}
			return num;
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x00061620 File Offset: 0x0005F820
		public int AutoReply(AldsAccountInfo account, string contactUid, string contactOpenUid, string msg, DateTime sendtime, MsgFrom msgFrom, out string result, out bool hasTransfer, out bool hasReplyMsg)
		{
			hasTransfer = false;
			hasReplyMsg = false;
			int num;
			try
			{
				string text = contactUid.Replace("cntaobao", "").Replace("cnalichn", "");
				string text2 = contactUid.Replace(text, "");
				num = this.a(account, msg, text, text2, contactOpenUid, sendtime, msgFrom, out result, out hasTransfer, out hasReplyMsg);
			}
			catch (Exception ex)
			{
				result = "智能答复时异常！";
				LogWriter.WriteLog(ex.ToString(), 1);
				num = -90999;
			}
			return num;
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x000616B0 File Offset: 0x0005F8B0
		private int a(AldsAccountInfo A_0, string A_1, string A_2, string A_3, string A_4, DateTime A_5, MsgFrom A_6, out string A_7, out bool A_8, out bool A_9)
		{
			A_8 = false;
			A_9 = false;
			AutoReplyInfo autoReplyInfo;
			AutoReplyInfo autoReplyInfo2;
			bool flag;
			AppConfig.GetMatchReplyInfo(A_0, A_1, A_2, A_4, A_5, out A_7, out autoReplyInfo, out autoReplyInfo2, out flag);
			int num;
			if (!string.IsNullOrEmpty(A_7) && flag)
			{
				num = -90050;
			}
			else
			{
				SortedDictionary<ArActionType, List<object>> sortedDictionary = ((autoReplyInfo != null) ? autoReplyInfo.Explain(A_0) : null);
				SortedDictionary<ArActionType, List<object>> sortedDictionary2 = ((autoReplyInfo2 != null) ? autoReplyInfo2.Explain(A_0) : null);
				SortedDictionary<ArActionType, List<object>> sortedDictionary3 = Util.Merge(new SortedDictionary<ArActionType, List<object>>[] { sortedDictionary, sortedDictionary2 });
				if (Util.IsEmptyList<KeyValuePair<ArActionType, List<object>>>(sortedDictionary3))
				{
					A_7 = "未匹配到答复语";
					num = -90080;
				}
				else
				{
					AppConfig.HandleSameQuery(A_0, A_1, A_2, (autoReplyInfo2 != null) ? autoReplyInfo2.ReplyWord : null, out A_7);
					if (!string.IsNullOrEmpty(A_7))
					{
						num = -90100;
					}
					else
					{
						if (!Util.IsEmptyList<KeyValuePair<ArActionType, List<object>>>(sortedDictionary))
						{
							LogFirstReplyManager.Insert(A_0.UserNick, A_2, A_3 + A_2, A_4, A_1, A_5, autoReplyInfo.ReplyWord);
						}
						if (!Util.IsEmptyList<KeyValuePair<ArActionType, List<object>>>(sortedDictionary2))
						{
							LogAutoReplyManager.Insert(A_0.UserNick, A_2, A_3 + A_2, A_4, A_1, A_5, autoReplyInfo2, A_6, DateTime.Now);
						}
						ErrCodeInfo errCodeInfo = new ErrCodeInfo();
						StringBuilder stringBuilder = new StringBuilder();
						UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(this.UserNick);
						foreach (KeyValuePair<ArActionType, List<object>> keyValuePair in sortedDictionary3)
						{
							ArActionType key = keyValuePair.Key;
							ArActionType arActionType = key;
							if (arActionType <= ArActionType.Tiqu)
							{
								if (arActionType != ArActionType.ReplyMsg)
								{
									if (arActionType != ArActionType.Tiqu)
									{
										continue;
									}
									string tidsFromString = Util.GetTidsFromString(A_1);
									List<string> list = null;
									if (Util.IsNum(A_4))
									{
										string aldsOpenUidByRecentOpenUid = AppConfig.BuyerInfoCache.GetAldsOpenUidByRecentOpenUid(A_4);
										if (!string.IsNullOrEmpty(aldsOpenUidByRecentOpenUid))
										{
											list = AliwwMessageManager.SelectMsg(this.CurrentSellerNick, A_2, aldsOpenUidByRecentOpenUid, tidsFromString);
										}
									}
									else
									{
										list = AliwwMessageManager.SelectMsg(this.CurrentSellerNick, A_2, A_4, tidsFromString);
									}
									if (list == null)
									{
										list = new List<string>();
									}
									if (list.Count == 0)
									{
										list.Add("抱歉，没有发货记录。");
									}
									using (List<string>.Enumerator enumerator2 = list.GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											string text = enumerator2.Current;
											errCodeInfo = this.SendToTalkWindowWholeMsgForAutoReply(A_2, A_4, text);
											if (errCodeInfo.ErrCode != ErrCodeType.SendSucc)
											{
												stringBuilder.Append(string.Format("{0}", errCodeInfo));
											}
											A_9 = true;
										}
										continue;
									}
								}
								using (List<object>.Enumerator enumerator3 = keyValuePair.Value.GetEnumerator())
								{
									while (enumerator3.MoveNext())
									{
										object obj = enumerator3.Current;
										errCodeInfo = this.SendToTalkWindowWholeMsgForAutoReply(A_2, A_4, obj.ToString());
										if (errCodeInfo.ErrCode != ErrCodeType.SendSucc)
										{
											stringBuilder.Append(string.Format("{0}；", errCodeInfo));
										}
										A_9 = true;
									}
									continue;
								}
							}
							if (arActionType != ArActionType.AppointTransferCall)
							{
								if (arActionType == ArActionType.TransferCall)
								{
									if (A_2.StartsWith(this.CurrentSellerNick + ":") || A_2.Equals(this.CurrentSellerNick))
									{
										errCodeInfo = new ErrCodeInfo(ErrCodeType.TransferCallFailedBecauseOfSameAccount);
										stringBuilder.Append(errCodeInfo);
									}
									else
									{
										Dictionary<string, DateTime> dictBuyerNickTransferTime = userCacheOrCreate.DictBuyerNickTransferTime;
										if (AppConfig.CurrentSystemSettingInfo.TransferInterval > 0 && dictBuyerNickTransferTime.ContainsKey(A_2) && (DateTime.Now - dictBuyerNickTransferTime[A_2]).TotalSeconds <= (double)AppConfig.CurrentSystemSettingInfo.TransferInterval)
										{
											errCodeInfo = new ErrCodeInfo(ErrCodeType.TransferCallFinish);
											string text2 = AppConfig.CurrentSystemSettingInfo.TransferInterval.ToString() + "秒内不再重复转接";
											stringBuilder.Append(text2 + "；");
										}
										else
										{
											string transferCallDutyManualNick = AppConfig.GetTransferCallDutyManualNick(A_0, A_2, A_4);
											string text3;
											if (!string.IsNullOrEmpty(transferCallDutyManualNick))
											{
												text3 = "抱歉，转接失败，您可以手动加旺旺好友后联系，人工客服旺旺号“" + transferCallDutyManualNick + "”。";
											}
											else
											{
												text3 = A_0.NotDutyNickReplyMsg;
											}
											if (this.chatWindowType != ChatWindowType.CustomerBench)
											{
												errCodeInfo = new ErrCodeInfo(ErrCodeType.TransferCallFailedBecauseOfChatType);
												stringBuilder.Append(string.Format("{0}；", errCodeInfo));
												this.SendToTalkWindowWholeMsgForAutoReply(A_2, A_4, text3);
											}
											else
											{
												errCodeInfo = this.a(transferCallDutyManualNick, A_2, A_3, A_4);
												if (errCodeInfo.ErrCode != ErrCodeType.TransferCallFinish)
												{
													stringBuilder.Append(errCodeInfo);
													if (!string.IsNullOrEmpty(text3))
													{
														this.SendToTalkWindowWholeMsgForAutoReply(A_2, A_4, text3);
													}
												}
												else
												{
													userCacheOrCreate.DictBuyerNickTransferTime[A_2] = DateTime.Now;
													A_8 = true;
												}
											}
										}
									}
								}
							}
							else
							{
								Dictionary<string, DateTime> dictBuyerNickTransferTime2 = userCacheOrCreate.DictBuyerNickTransferTime;
								if (AppConfig.CurrentSystemSettingInfo.TransferInterval > 0 && dictBuyerNickTransferTime2.ContainsKey(A_2) && (DateTime.Now - dictBuyerNickTransferTime2[A_2]).TotalSeconds <= (double)AppConfig.CurrentSystemSettingInfo.TransferInterval)
								{
									errCodeInfo = new ErrCodeInfo(ErrCodeType.TransferCallFinish);
									string text4 = AppConfig.CurrentSystemSettingInfo.TransferInterval.ToString() + "秒内不再重复转接";
									stringBuilder.Append(text4 + "；");
								}
								else
								{
									string text5 = keyValuePair.Value[0].ToString();
									if (!A_0.UserNick.Equals(text5))
									{
										errCodeInfo = this.a(text5, A_2, A_3, A_4);
										if (errCodeInfo.ErrCode != ErrCodeType.TransferCallFinish)
										{
											stringBuilder.Append(errCodeInfo.ToString());
											this.SendToTalkWindowWholeMsgForAutoReply(A_2, A_4, "抱歉，转接失败，您可以手动加旺旺好友后联系，人工客服旺旺号“" + text5 + "”。");
										}
										else
										{
											userCacheOrCreate.DictBuyerNickTransferTime[A_2] = DateTime.Now;
											A_8 = true;
										}
									}
								}
							}
						}
						if (string.IsNullOrEmpty(stringBuilder.ToString()))
						{
							A_7 = (Util.IsEmptyList<KeyValuePair<ArActionType, List<object>>>(sortedDictionary2) ? "首次答复成功。" : "智能答复成功。");
							num = 90100;
						}
						else
						{
							A_7 = "智能答复失败！" + stringBuilder.ToString();
							num = -90100;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x00061D30 File Offset: 0x0005FF30
		private ErrCodeInfo a(string A_0, string A_1, string A_2, string A_3)
		{
			AliwwTalkWindowQn aliwwTalkWindowQn = this.Convert<AliwwTalkWindowQn>();
			ErrCodeInfo errCodeInfo;
			if (string.IsNullOrEmpty(A_0))
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.TransferCallManualNickIsNull);
			}
			else if (aliwwTalkWindowQn == null)
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.TransferCallFailed);
			}
			else
			{
				string text = A_0;
				if (text.StartsWith(this.CurrentSellerNick + "："))
				{
					text = text.Replace(this.CurrentSellerNick + "：", this.CurrentSellerNick + ":");
				}
				if (!text.Equals(this.CurrentSellerNick) && !text.Contains(":"))
				{
					text = this.CurrentSellerNick + ":" + text;
				}
				if (text.StartsWith(this.CurrentSellerNick + ":") || text.Equals(this.CurrentSellerNick))
				{
					if (string.IsNullOrEmpty(A_2))
					{
						A_2 = "cntaobao";
					}
					errCodeInfo = (aliwwTalkWindowQn.TransferContactByQnApiAuto(this.UserNick, A_1, A_2, A_3, text, "cntaobao") ? new ErrCodeInfo(ErrCodeType.TransferCallFinish) : new ErrCodeInfo(ErrCodeType.TransferCallFailed));
				}
				else
				{
					errCodeInfo = new ErrCodeInfo(ErrCodeType.TransferCallFailed);
				}
			}
			return errCodeInfo;
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x00061E74 File Offset: 0x00060074
		public bool CheckCallUserInfoEquals(string buyerNick, string buyerOpenUid, int retryTimes = 1, string siteId = "cntaobao")
		{
			for (int i = 0; i < retryTimes; i++)
			{
				bool? flag;
				if (this.a(buyerNick, buyerOpenUid, out flag, siteId))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x00061EA4 File Offset: 0x000600A4
		private bool a(string A_0, string A_1, out bool? A_2, string A_3 = "cntaobao")
		{
			A_2 = null;
			bool flag;
			if (this.AliVersion == AliwwVersion.AliwwBuyer9)
			{
				flag = this.a(A_0, A_3);
			}
			else
			{
				flag = this.AliVersion == AliwwVersion.TradeManager2015 || this.a(A_0) || this.b(A_0, A_1, A_3);
			}
			return flag;
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x00061EF8 File Offset: 0x000600F8
		private bool a(string A_0, string A_1 = "cntaobao")
		{
			AliwwTalkWindowBuyer9 aliwwTalkWindowBuyer = this.Convert<AliwwTalkWindowBuyer9>();
			string html = aliwwTalkWindowBuyer.GetHtml("alires:///offlinepkg/ww/leftbar/src/index.html", null);
			bool flag;
			if (string.IsNullOrEmpty(html))
			{
				flag = false;
			}
			else
			{
				try
				{
					HtmlDocument htmlDocument = new HtmlDocument();
					htmlDocument.LoadHtml(html);
					HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode("//li[contains(@class,'J_cItem') and contains(@class,'active')]");
					if (htmlNode == null || htmlNode.Attributes["data-uid"] == null)
					{
						flag = false;
					}
					else
					{
						string value = htmlNode.Attributes["data-uid"].Value;
						flag = value.Equals(A_1 + A_0) || value.Equals(A_1 + Util.GetMasterNick(A_0));
					}
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog(ex.ToString(), 1);
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x00061FD8 File Offset: 0x000601D8
		private bool a(string A_0)
		{
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(new WindowStruct("tooltips_class32", A_0), 0, false);
			bool flag;
			if (windowListByClassAndName.Count <= 0)
			{
				flag = false;
			}
			else
			{
				foreach (WindowInfo windowInfo in windowListByClassAndName)
				{
					if (windowInfo.GetParent() == base.HWnd)
					{
						return true;
					}
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x00062064 File Offset: 0x00060264
		private bool b(string A_0, string A_1, string A_2 = "cntaobao")
		{
			bool flag;
			if (!this.CurrentUserCache.IsSessionNull)
			{
				BehaviorBase session = this.CurrentUserCache.GetSession(A_1);
				flag = session != null && session.CheckCallUserInfoEqualsByQnApi(A_0, A_1);
			}
			else
			{
				flag = this.a(A_0, A_1, A_2);
			}
			return flag;
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x000620AC File Offset: 0x000602AC
		private bool a(string A_0, string A_1, string A_2 = "cntaobao")
		{
			AliwwTalkWindowQn aliwwTalkWindowQn = this.Convert<AliwwTalkWindowQn>();
			string text = Guid.NewGuid().ToString().Replace("-", "");
			ActiveUserInfo currentTargetUserNickByQnApi = aliwwTalkWindowQn.GetCurrentTargetUserNickByQnApi(text);
			return (A_2 + A_0).Equals((currentTargetUserNickByQnApi != null) ? currentTargetUserNickByQnApi.Uid : null);
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x00062104 File Offset: 0x00060304
		private ActiveUserInfo a(int A_0 = 5, string A_1 = "cntaobao")
		{
			AliwwTalkWindowQn aliwwTalkWindowQn = this.Convert<AliwwTalkWindowQn>();
			string text = Guid.NewGuid().ToString().Replace("-", "");
			ActiveUserInfo activeUserInfo = null;
			for (int i = 0; i < A_0; i++)
			{
				activeUserInfo = aliwwTalkWindowQn.GetCurrentTargetUserNickByQnApi(text);
				if (activeUserInfo != null)
				{
					return activeUserInfo;
				}
				Thread.Sleep(100);
			}
			return activeUserInfo;
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x0000ED85 File Offset: 0x0000CF85
		[CompilerGenerated]
		private bool a()
		{
			return Util.IsEmptyList<WinFuwuTip>(WinFuwuTip.GetList(base.ProcessId));
		}

		// Token: 0x04001DE3 RID: 7651
		[CompilerGenerated]
		private string a;

		// Token: 0x04001DE4 RID: 7652
		private string b;

		// Token: 0x04001DE5 RID: 7653
		private Aliww c;

		// Token: 0x04001DE6 RID: 7654
		private UserCache d;

		// Token: 0x04001DE7 RID: 7655
		[CompilerGenerated]
		private AliwwVersion e;

		// Token: 0x04001DE8 RID: 7656
		private WinRichEdit f;

		// Token: 0x04001DE9 RID: 7657
		private WindowInfo g;

		// Token: 0x04001DEA RID: 7658
		private IWinHtmlRecentContainer h;

		// Token: 0x04001DEB RID: 7659
		private WindowInfo i;

		// Token: 0x04001DEC RID: 7660
		private WindowInfo j;

		// Token: 0x04001DED RID: 7661
		private WindowInfo k;

		// Token: 0x04001DEE RID: 7662
		public ChatWindowType chatWindowType;

		// Token: 0x04001DEF RID: 7663
		private static string l = string.Empty;

		// Token: 0x04001DF0 RID: 7664
		private static Regex m = new Regex("^(\\{\\$[a-z0-9_]{1,4}\\$\\})(?<subMsg>.*)");

		// Token: 0x0200071B RID: 1819
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x06002433 RID: 9267 RVA: 0x00062200 File Offset: 0x00060400
			internal void c(WinFuwuTip A_0)
			{
				if (AliwwTalkWindow.l == this.a)
				{
					Agiso.Windows.Rectangle clientRect = A_0.GetClientRect();
					A_0.SimulateMouseClick(clientRect.GetWidth() - 60, clientRect.GetHeight() - 39, false, 1);
					LogWriter.WriteLog("服务态度提醒0", 1);
					Thread.Sleep(200);
				}
				else
				{
					LogWriter.WriteLog("检测到服务态度提醒窗口，进行关闭", 1);
					A_0.Close(true);
					Thread.Sleep(500);
					this.b.FocusSendWin();
					this.b.PressControlAndOtherKey(65);
					this.b.KeyPress(Keys.Back, false);
				}
			}

			// Token: 0x06002434 RID: 9268 RVA: 0x000622A0 File Offset: 0x000604A0
			internal bool c()
			{
				if (this.b.Disabled)
				{
					this.b.CloseOwnedWindow();
					Thread.Sleep(500);
				}
				return !this.b.Disabled;
			}

			// Token: 0x04001DF8 RID: 7672
			public string a;

			// Token: 0x04001DF9 RID: 7673
			public AliwwTalkWindow b;
		}

		// Token: 0x0200071C RID: 1820
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x06002436 RID: 9270 RVA: 0x000622E0 File Offset: 0x000604E0
			internal bool d()
			{
				return this.b.b.CurrentUserCache.LastSendMsgSuccDateTime != null && this.b.b.CurrentUserCache.LastSendMsgSuccDateTime.Value >= this.a;
			}

			// Token: 0x04001DFA RID: 7674
			public DateTime a;

			// Token: 0x04001DFB RID: 7675
			public AliwwTalkWindow.a b;

			// Token: 0x04001DFC RID: 7676
			public Func<bool> c;
		}

		// Token: 0x0200071D RID: 1821
		[CompilerGenerated]
		private sealed class c
		{
			// Token: 0x04001DFD RID: 7677
			public AliwwTalkWindow a;

			// Token: 0x04001DFE RID: 7678
			public int b;
		}

		// Token: 0x0200071E RID: 1822
		[CompilerGenerated]
		private sealed class d
		{
			// Token: 0x06002439 RID: 9273 RVA: 0x00062338 File Offset: 0x00060538
			internal bool c()
			{
				string text = this.b.a.MsgBoxWin.GetText();
				bool flag;
				if (this.b.b < 2 && text == " ")
				{
					this.a.Cancel();
					flag = false;
				}
				else
				{
					flag = (this.b.b >= 2 && text == " ") || !string.IsNullOrWhiteSpace(text);
				}
				return flag;
			}

			// Token: 0x04001DFF RID: 7679
			public CancellationTokenSource a;

			// Token: 0x04001E00 RID: 7680
			public AliwwTalkWindow.c b;
		}
	}
}
