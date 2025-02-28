using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Agiso.AliwwApi.Object;
using Agiso.AliwwApi.Qn;
using Agiso.AliwwApi.Wangwang;
using Agiso.AliwwApi.WinAlert;
using Agiso.Extensions;
using Agiso.Handler;
using Agiso.Object;
using Agiso.Utils;
using Agiso.Windows;
using Agiso.WwService.Sdk;
using AliwwClient.Cache;
using AliwwClient.Manager;
using AliwwClient.WebSocketServer;
using AliwwClient.WebSocketServer.Extensions;

namespace Agiso.AliwwApi
{
	// Token: 0x020006FF RID: 1791
	public class Aliww
	{
		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x0600233A RID: 9018 RVA: 0x0000EB53 File Offset: 0x0000CD53
		// (set) Token: 0x0600233B RID: 9019 RVA: 0x0000EB5B File Offset: 0x0000CD5B
		public bool CloseCustomerBenchWindowBeforeSend { get; set; }

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600233C RID: 9020 RVA: 0x0003A434 File Offset: 0x00038634
		public bool AllowCheckTargetNick
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x0005A214 File Offset: 0x00058414
		public string UserNick
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x0600233E RID: 9022 RVA: 0x0005A22C File Offset: 0x0005842C
		public string UserSiteId
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x0000EB64 File Offset: 0x0000CD64
		public Aliww(string userNick)
		{
			this.b = userNick;
			this.c = Aliww.e;
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x0000EB7F File Offset: 0x0000CD7F
		public Aliww(string userNick, string userSiteId)
		{
			this.b = userNick;
			this.c = userSiteId;
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06002341 RID: 9025 RVA: 0x0005A244 File Offset: 0x00058444
		// (remove) Token: 0x06002342 RID: 9026 RVA: 0x0005A27C File Offset: 0x0005847C
		public event Action<AliwwTalkWindow> OnSendToUserSuccess
		{
			[CompilerGenerated]
			add
			{
				Action<AliwwTalkWindow> action = this.f;
				Action<AliwwTalkWindow> action2;
				do
				{
					action2 = action;
					Action<AliwwTalkWindow> action3 = (Action<AliwwTalkWindow>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<AliwwTalkWindow>>(ref this.f, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<AliwwTalkWindow> action = this.f;
				Action<AliwwTalkWindow> action2;
				do
				{
					action2 = action;
					Action<AliwwTalkWindow> action3 = (Action<AliwwTalkWindow>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<AliwwTalkWindow>>(ref this.f, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x0005A2B4 File Offset: 0x000584B4
		private int CurrProcessId
		{
			get
			{
				if (this.g <= 0 && this.MainWindow != null)
				{
					this.g = this.MainWindow.ProcessId;
				}
				return this.g;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x0005A2F4 File Offset: 0x000584F4
		protected UserCache CurrUserCache
		{
			get
			{
				if (this.h == null)
				{
					this.h = AppConfig.GetUserCacheOrCreate(this.b);
				}
				return this.h;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x0005A328 File Offset: 0x00058528
		public AliwwMainWindow MainWindow
		{
			get
			{
				if (this.i == null)
				{
					this.i = this.c("");
					if (this.i == null)
					{
						this.d();
						if (!string.IsNullOrEmpty(this.CurrUserCache.UserNickAs))
						{
							this.i = this.c(this.CurrUserCache.UserNickAs);
						}
					}
					if (this.i != null)
					{
						this.j = this.i.AliVersion;
					}
				}
				return this.i;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x0005A3B4 File Offset: 0x000585B4
		public virtual AliwwVersion AliwwVersion
		{
			get
			{
				if (this.j == AliwwVersion.Undefine && this.MainWindow != null)
				{
					this.j = this.MainWindow.AliVersion;
				}
				return this.j;
			}
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x0005A3F0 File Offset: 0x000585F0
		private ErrCodeInfo d(string A_0)
		{
			IntPtr intPtr = this.g();
			ErrCodeInfo errCodeInfo;
			if (intPtr == IntPtr.Zero)
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.CallFailFindFriendControlNotFound);
			}
			else
			{
				this.f();
				switch (this.MainWindow.AliVersion)
				{
				case AliwwVersion.AliwwSeller2012ReadView:
				case AliwwVersion.AliwwSeller2013:
				case AliwwVersion.AliwwBuyer2014:
				case AliwwVersion.AliwwAlbb2014:
				case AliwwVersion.TradeManager2014:
				case AliwwVersion.TradeManager2015:
				{
					string text = "****";
					for (int i = 0; i < 3; i++)
					{
						WindowsAPI.SendMessage(intPtr, 7, 0, 0);
						WindowsAPI.SendMessage(intPtr, 12, 0, text);
						if (Win32Extend.GetText(intPtr) == text)
						{
							break;
						}
						Thread.Sleep(100);
					}
					WindowsAPI.PostMessage(intPtr, 256U, 13U, 0U);
					WindowsAPI.PostMessage(intPtr, 258U, 13U, 0U);
					WindowsAPI.PostMessage(intPtr, 257U, 13U, 0U);
					IntPtr intPtr2 = IntPtr.Zero;
					for (int j = 0; j < 10; j++)
					{
						Thread.Sleep(200);
						WindowInfo windowInfo = Win32Extend.FindWindowByClassAndName(null, "指定用户发送");
						if (windowInfo != null)
						{
							intPtr2 = windowInfo.HWnd;
							if (intPtr2 != IntPtr.Zero)
							{
								break;
							}
						}
						if (this.b(A_0) != null)
						{
							this.f();
							return new ErrCodeInfo(ErrCodeType.CallSuccWw);
						}
					}
					if (intPtr2 == IntPtr.Zero)
					{
						return new ErrCodeInfo(ErrCodeType.CallFailSpecifySendWindowNotFound);
					}
					IntPtr intPtr3 = WindowsAPI.FindWindowEx(intPtr2, IntPtr.Zero, "SearchInput", null);
					IntPtr intPtr4 = WindowsAPI.FindWindowEx(intPtr3, IntPtr.Zero, "EditComponent", null);
					if (this.AliwwVersion == AliwwVersion.AliwwAlbb2014 || this.AliwwVersion == AliwwVersion.TradeManager2014 || this.AliwwVersion == AliwwVersion.TradeManager2015)
					{
						intPtr3 = WindowsAPI.FindWindowEx(intPtr2, IntPtr.Zero, "EditComponent", null);
					}
					for (int k = 0; k < 3; k++)
					{
						WindowsAPI.SendMessage(intPtr3, 12, 0, A_0);
						WindowsAPI.SendMessage(intPtr4, 12, 0, A_0);
						Application.DoEvents();
						if (Win32Extend.GetText(intPtr4) == A_0)
						{
							break;
						}
						Thread.Sleep(100);
					}
					if (Win32Extend.GetText(intPtr4) != A_0)
					{
						return new ErrCodeInfo(ErrCodeType.CallFailSpecifySendWindowSetTextFail);
					}
					IntPtr intPtr5 = WindowsAPI.FindWindowEx(intPtr2, IntPtr.Zero, "StandardButton", "发 送");
					WindowsAPI.PostMessage(intPtr5, 513U, 0U, 0U);
					WindowsAPI.PostMessage(intPtr5, 514U, 0U, 0U);
					WindowsAPI.PostMessage(intPtr5, 515U, 0U, 0U);
					WindowsAPI.PostMessage(intPtr5, 258U, 13U, 0U);
					WindowsAPI.PostMessage(intPtr3, 258U, 13U, 0U);
					WindowsAPI.PostMessage(intPtr4, 258U, 13U, 0U);
					this.i();
					for (int l = 0; l < 2; l++)
					{
						if (this.e())
						{
							return new ErrCodeInfo(ErrCodeType.CallFailTargetNickReceiveFriendOnly);
						}
						Thread.Sleep(100);
					}
					goto IL_03AB;
				}
				}
				for (int m = 0; m < 3; m++)
				{
					WindowsAPI.SendMessage(intPtr, 7, 0, 0);
					WindowsAPI.SendMessage(intPtr, 12, 0, A_0);
					if (Win32Extend.GetText(intPtr) == A_0)
					{
						break;
					}
					Thread.Sleep(100);
				}
				WindowsAPI.PostMessage(intPtr, 256U, 13U, 0U);
				WindowsAPI.PostMessage(intPtr, 258U, 13U, 0U);
				WindowsAPI.PostMessage(intPtr, 257U, 13U, 0U);
				int num = 0;
				while (num < 10 && !(Win32Extend.GetText(intPtr) != A_0))
				{
					Thread.Sleep(100);
					WindowsAPI.SendMessage(intPtr, 256, 13, 0);
					WindowsAPI.SendMessage(intPtr, 258, 13, 0);
					WindowsAPI.SendMessage(intPtr, 257, 13, 0);
					num++;
				}
				IL_03AB:
				WindowsAPI.SendMessage(intPtr, 8, 0, 0);
				this.f();
				errCodeInfo = new ErrCodeInfo(ErrCodeType.CallSuccWw);
			}
			return errCodeInfo;
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x0005A7C4 File Offset: 0x000589C4
		public ErrCodeInfo CallingUserNickQianNiu(string toUserNick, string siteId, string toOpenUid)
		{
			ErrCodeInfo errCodeInfo;
			if (string.IsNullOrEmpty(toUserNick))
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.CallFailTargetNickIsNull);
			}
			else
			{
				string text = this.b;
				BehaviorBase session = this.CurrUserCache.Session;
				if (!string.IsNullOrEmpty((session != null) ? session.UserNick : null))
				{
					BehaviorBase session2 = this.CurrUserCache.Session;
					text = ((session2 != null) ? session2.UserNick : null);
				}
				try
				{
					if (!this.CurrUserCache.IsSessionNull)
					{
						if (this.MainWindow != null)
						{
							AliwwTalkWindowQn aliwwTalkWindowQn = this.MainWindow.Convert<AliwwTalkWindowQn>();
							if (aliwwTalkWindowQn != null && !aliwwTalkWindowQn.CallContactNickByQnapiAuto(this.UserNick, toUserNick, toOpenUid, siteId, 0))
							{
								AppConfig.ProcessStartQn(text, toUserNick, toOpenUid, siteId);
								Thread.Sleep(200);
							}
						}
					}
					else
					{
						AppConfig.ProcessStartQn(text, toUserNick, toOpenUid, siteId);
						Thread.Sleep(200);
					}
					errCodeInfo = new ErrCodeInfo(ErrCodeType.CallSuccQn);
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog(ex.ToString(), 1);
					errCodeInfo = new ErrCodeInfo(ErrCodeType.CallFailWwcmdException);
				}
			}
			return errCodeInfo;
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x0005A8D4 File Offset: 0x00058AD4
		public ErrCodeInfo CallingUserNickProtocol(string toUserNick, string siteId, string toOpenUid)
		{
			ErrCodeInfo errCodeInfo;
			if (string.IsNullOrEmpty(toUserNick))
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.CallFailTargetNickIsNull);
			}
			else
			{
				string text = this.b;
				BehaviorBase session = this.CurrUserCache.Session;
				if (!string.IsNullOrEmpty((session != null) ? session.UserNick : null))
				{
					BehaviorBase session2 = this.CurrUserCache.Session;
					text = ((session2 != null) ? session2.UserNick : null);
				}
				AppConfig.ProcessStartQn(text, toUserNick, toOpenUid, siteId);
				Thread.Sleep(200);
				errCodeInfo = new ErrCodeInfo(ErrCodeType.CallSuccQn);
			}
			return errCodeInfo;
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x0005A958 File Offset: 0x00058B58
		public ErrCodeInfo CallUser(string toUserNick, string siteId, string toOpenUid, ref AliwwTalkWindow alitw)
		{
			ErrCodeInfo errCodeInfo;
			if (this.MainWindow == null)
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.MainWindowNotFound);
			}
			else
			{
				ErrCodeInfo errCodeInfo2 = new ErrCodeInfo();
				switch (this.MainWindow.AliVersion)
				{
				case AliwwVersion.QianNiu:
				case AliwwVersion.AliwwBuyer9:
				case AliwwVersion.QianNiu2:
				case AliwwVersion.QianNiu3:
				case AliwwVersion.QianNiu5:
				case AliwwVersion.QianNiu9:
					errCodeInfo2 = this.CallingUserNickQianNiu(toUserNick, siteId, toOpenUid);
					goto IL_018E;
				case AliwwVersion.AliwwSeller2012ReadView:
				case AliwwVersion.AliwwSeller2013:
				case AliwwVersion.AliwwBuyer2014:
				case AliwwVersion.AliwwAlbb2014:
				case AliwwVersion.TradeManager2014:
				case AliwwVersion.TradeManager2015:
					if (Aliww.d)
					{
						int i = 0;
						while (i < 2)
						{
							if (this.CallingUserNickQianNiu(toUserNick, siteId, toOpenUid).ErrCode >= ErrCodeType.Undefined)
							{
								int j = 0;
								while (j < 4)
								{
									Thread.Sleep(300);
									alitw = this.GetTalkWindow(toUserNick);
									if (alitw == null)
									{
										j++;
									}
									else
									{
										if (alitw.chatWindowType == ChatWindowType.CustomerBench)
										{
											Thread.Sleep(300);
											goto IL_0154;
										}
										goto IL_0154;
									}
								}
								if (this.AliwwVersion == AliwwVersion.AliwwAlbb2014 || this.AliwwVersion == AliwwVersion.TradeManager2014 || this.AliwwVersion == AliwwVersion.TradeManager2015)
								{
									if (Aliww.e.Equals("cntaobao"))
									{
										Aliww.e = "cnalichn";
										this.c = Aliww.e;
									}
									else
									{
										Aliww.e = "cntaobao";
										this.c = Aliww.e;
									}
								}
								i++;
								continue;
							}
							IL_0154:
							this.c = Aliww.e;
							goto IL_015F;
						}
						goto IL_0154;
					}
					IL_015F:
					if (alitw == null)
					{
						this.CloseAllAliwwFriendNeedToAddWindow();
						errCodeInfo2 = this.d(toUserNick);
						goto IL_018E;
					}
					goto IL_018E;
				}
				errCodeInfo2 = new ErrCodeInfo(ErrCodeType.CallFailMainWindowNotFoundOrVersionNotSupport);
				IL_018E:
				int num = 0;
				while (errCodeInfo2.ErrCode > ErrCodeType.Undefined && num < 5)
				{
					alitw = this.GetTalkWindow(toUserNick);
					if (alitw != null)
					{
						if (alitw.chatWindowType == ChatWindowType.CustomerBench && (!this.AllowCheckTargetNick && !this.CloseCustomerBenchWindowBeforeSend))
						{
							Thread.Sleep(1400);
						}
						return errCodeInfo2;
					}
					if (this.e())
					{
						return new ErrCodeInfo(ErrCodeType.CallFailTargetNickReceiveFriendOnly);
					}
					Thread.Sleep(200);
					num++;
				}
				if (alitw == null)
				{
					errCodeInfo2 = ((errCodeInfo2.ErrCode > ErrCodeType.Undefined) ? new ErrCodeInfo(ErrCodeType.CallFailChatWindowNotFound) : errCodeInfo2);
				}
				errCodeInfo = errCodeInfo2;
			}
			return errCodeInfo;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x0005AB9C File Offset: 0x00058D9C
		public ErrCodeInfo CallUser(string toUserNick, string toOpenUid, ref AliwwTalkWindow alitw)
		{
			return this.CallUser(toUserNick, "cntaobao", toOpenUid, ref alitw);
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x0005ABBC File Offset: 0x00058DBC
		public ErrCodeInfo SendToUser(string toUserNick, string toOpenUid, string msgBody, string siteId = "cntaobao")
		{
			ErrCodeInfo errCodeInfo = new ErrCodeInfo();
			this.CloseAllAliwwFriendNeedToAddWindow();
			this.h();
			this.a(0);
			this.CloseAllAliwwAccountNotExistsWindow();
			if (this.MainWindow == null)
			{
				AliwwMainWindow aliwwMainWindow = AliwwMainWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", "千牛卖家工作台"));
				if (aliwwMainWindow == null)
				{
					return new ErrCodeInfo(ErrCodeType.MainWindowNotFound);
				}
				this.CallingUserNickQianNiu(toUserNick, siteId, toOpenUid);
				if (!Util.CheckWait(3000, new Func<bool>(this.c), 200))
				{
					List<WinLoginBuyer9> list = WinLoginBuyer9.GetList();
					if (list != null)
					{
						foreach (WinLoginBuyer9 winLoginBuyer in list)
						{
							winLoginBuyer.Close(true);
						}
					}
					List<WinLoginQnBase> list2 = WinLoginQnBase.GetList();
					if (list2 != null)
					{
						foreach (WinLoginQnBase winLoginQnBase in list2)
						{
							winLoginQnBase.Close(true);
						}
					}
					if (this.e())
					{
						return new ErrCodeInfo(ErrCodeType.CallFailTargetNickReceiveFriendOnly);
					}
					if (this.CloseAllAliwwAccountNotExistsWindow())
					{
						return new ErrCodeInfo(ErrCodeType.CallFailTargetNickInBlackListOrNotExists);
					}
					return new ErrCodeInfo(ErrCodeType.MainWindowNotFound);
				}
			}
			if (this.CloseCustomerBenchWindowBeforeSend)
			{
				AliwwTalkWindow customerBenchWindow = this.GetCustomerBenchWindow(false);
				if (customerBenchWindow != null)
				{
					customerBenchWindow.Close(true);
					Application.DoEvents();
				}
			}
			AliwwTalkWindow aliwwTalkWindow = null;
			bool flag = false;
			int i = 0;
			while (i < 15)
			{
				if (this.AliwwVersion == AliwwVersion.QianNiu3 || this.AliwwVersion == AliwwVersion.QianNiu5)
				{
					goto IL_01A3;
				}
				if (this.AliwwVersion == AliwwVersion.QianNiu9)
				{
					goto IL_01A3;
				}
				bool flag2 = true;
				IL_01A8:
				if (flag2)
				{
					errCodeInfo = this.CallUser(toUserNick, toOpenUid, ref aliwwTalkWindow);
				}
				else if (this.AliwwVersion == AliwwVersion.QianNiu3 || this.AliwwVersion == AliwwVersion.QianNiu5 || this.AliwwVersion == AliwwVersion.QianNiu9)
				{
					aliwwTalkWindow = this.GetTalkWindow(toUserNick);
					if (aliwwTalkWindow != null && aliwwTalkWindow.HWnd != IntPtr.Zero)
					{
						errCodeInfo = new ErrCodeInfo(ErrCodeType.CallSuccQn);
					}
				}
				if (this.e())
				{
					return new ErrCodeInfo(ErrCodeType.CallFailTargetNickReceiveFriendOnly);
				}
				if (!this.CloseAllAliwwAccountNotExistsWindow())
				{
					if (aliwwTalkWindow != null && aliwwTalkWindow.HWnd != IntPtr.Zero)
					{
						if (aliwwTalkWindow.chatWindowType != ChatWindowType.ChatWindow)
						{
							if (this.AliwwVersion == AliwwVersion.QianNiu5)
							{
								goto IL_025A;
							}
							if (this.AliwwVersion == AliwwVersion.QianNiu9)
							{
								goto IL_025A;
							}
							bool flag3 = false;
							IL_0278:
							if (flag3)
							{
								return new ErrCodeInfo(ErrCodeType.SendFailSessionIsNull);
							}
							if (!this.AllowCheckTargetNick)
							{
								goto IL_0478;
							}
							if (this.CurrUserCache.IsSessionNull && !flag && (aliwwTalkWindow.AliVersion == AliwwVersion.QianNiu5 || aliwwTalkWindow.AliVersion == AliwwVersion.QianNiu9) && AppConfig.AliwwWebScoketServer.IsListening)
							{
								AliwwTalkWindowQn aliwwTalkWindowQn = aliwwTalkWindow.Convert<AliwwTalkWindowQn>();
								if (aliwwTalkWindowQn != null && this.CurrUserCache.IsSessionNull)
								{
									AppConfig.WriteLog("【发消息时】UserNick: " + this.UserNick + ", 开始会话", LogType.LogSendMsgByWebSocket, 1);
									flag = true;
									if (aliwwTalkWindowQn.CanConnect(this.UserNick) && aliwwTalkWindowQn.ImplantedJsForWsClient(this.UserNick))
									{
										Util.CheckWait(2000, new Func<bool>(this.a), 100);
									}
								}
							}
							if (!aliwwTalkWindow.CheckCallUserInfoEquals(toUserNick, toOpenUid, 1, "cntaobao"))
							{
								if (i < 1)
								{
									if (this.AliwwVersion == AliwwVersion.QianNiu9 || this.AliwwVersion == AliwwVersion.QianNiu5 || this.AliwwVersion == AliwwVersion.QianNiu3)
									{
										this.CallingUserNickQianNiu(toUserNick, siteId, toOpenUid);
									}
								}
								else if (i < 4)
								{
									this.d(toUserNick);
								}
								else if (i % 10 == 0)
								{
									if (this.AliwwVersion == AliwwVersion.QianNiu9 || this.AliwwVersion == AliwwVersion.QianNiu5 || this.AliwwVersion == AliwwVersion.QianNiu3)
									{
										BehaviorBase session = this.CurrUserCache.GetSession(toOpenUid);
										if (session != null)
										{
											session.smethod_0(toUserNick, toOpenUid, " ", 0, "cntaobao");
										}
									}
								}
								else if (i % 5 == 0 && (this.AliwwVersion == AliwwVersion.QianNiu9 || this.AliwwVersion == AliwwVersion.QianNiu5 || this.AliwwVersion == AliwwVersion.QianNiu3))
								{
									BehaviorBase session2 = this.CurrUserCache.GetSession(toOpenUid);
									if (session2 != null)
									{
										session2.OpenChat(toUserNick, toOpenUid, "cntaobao");
									}
								}
								errCodeInfo = new ErrCodeInfo(ErrCodeType.CallFailCheckTargetNickUnEquals);
								goto IL_0478;
							}
							break;
							IL_025A:
							flag3 = !Util.CheckWait(3000, new Func<bool>(this.b), 200);
							goto IL_0278;
						}
						break;
					}
					else if (i < 1 && (this.AliwwVersion == AliwwVersion.QianNiu9 || this.AliwwVersion == AliwwVersion.QianNiu5 || this.AliwwVersion == AliwwVersion.QianNiu3))
					{
						this.CallingUserNickQianNiu(toUserNick, siteId, toOpenUid);
					}
					IL_0478:
					Application.DoEvents();
					Thread.Sleep(200);
					i++;
					continue;
				}
				errCodeInfo = new ErrCodeInfo(ErrCodeType.CallFailTargetNickInBlackListOrNotExists);
				IL_04B6:
				if (errCodeInfo.ErrCode < ErrCodeType.Undefined)
				{
					this.f();
					this.CloseLoginWindow();
					return errCodeInfo;
				}
				if (aliwwTalkWindow != null)
				{
					errCodeInfo = aliwwTalkWindow.SendToTalkWindowWholeMsg(toUserNick, toOpenUid, msgBody, "cntaobao");
					errCodeInfo = this.CheckMsgIsSendSucc(aliwwTalkWindow, msgBody, toUserNick, errCodeInfo);
					if (errCodeInfo.ErrCode == ErrCodeType.SendFailAccountIsBanned)
					{
						this.CurrUserCache.AccountIsBanned = true;
					}
					else
					{
						this.CurrUserCache.AccountIsBanned = false;
					}
					if (errCodeInfo.ErrCode > ErrCodeType.Undefined)
					{
						Action<AliwwTalkWindow> action = this.f;
						if (action != null)
						{
							action(aliwwTalkWindow);
						}
					}
				}
				this.f();
				return errCodeInfo;
				IL_01A3:
				flag2 = i == 0;
				goto IL_01A8;
			}
			goto IL_04B6;
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x0005B13C File Offset: 0x0005933C
		protected void CloseLoginWindow()
		{
			List<WindowInfo> list = new List<WindowInfo>();
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(new WindowStruct("StandardFrame", "千牛 - 卖家工作台"), 0, false);
			if (windowListByClassAndName != null)
			{
				list.AddRange(windowListByClassAndName);
			}
			List<WindowInfo> windowListByClassAndName2 = Win32Extend.GetWindowListByClassAndName(new WindowStruct("StandardFrame", "阿里旺旺2013卖家版"), 0, false);
			if (windowListByClassAndName2 != null)
			{
				list.AddRange(windowListByClassAndName2);
			}
			List<WindowInfo> windowListByClassAndName3 = Win32Extend.GetWindowListByClassAndName(new WindowStruct("StandardFrame", "阿里旺旺"), 0, false);
			if (windowListByClassAndName3 != null)
			{
				foreach (WindowInfo windowInfo in windowListByClassAndName3)
				{
					List<WindowInfo> list2 = windowInfo.EnumChildWindowList();
					if (list2.Count != 1 || !"WebControl".Equals(list2[0].Info.ClassName))
					{
						List<IntPtr> childHandleListByClassTreePath = Win32Extend.GetChildHandleListByClassTreePath(windowInfo.HWnd, new string[] { "StandardWindow", "WebControl", "Aef_WidgetWin_0" });
						if (childHandleListByClassTreePath == null || childHandleListByClassTreePath.Count <= 3)
						{
							list.Add(windowInfo);
						}
					}
				}
			}
			List<WindowInfo> windowListByClassAndName4 = Win32Extend.GetWindowListByClassAndName(new WindowStruct("StandardFrame", "TradeManager2014"), 0, false);
			if (windowListByClassAndName4 != null)
			{
				list.AddRange(windowListByClassAndName4);
			}
			List<WindowInfo> windowListByClassAndName5 = Win32Extend.GetWindowListByClassAndName(new WindowStruct("StandardFrame", "TradeManager2015"), 0, false);
			if (windowListByClassAndName5 != null)
			{
				list.AddRange(windowListByClassAndName5);
			}
			if (list != null && list.Count > 0 && (this.AliwwVersion == AliwwVersion.QianNiu9 || this.AliwwVersion == AliwwVersion.QianNiu3 || this.AliwwVersion == AliwwVersion.QianNiu5 || list.Count >= 3))
			{
				foreach (WindowInfo windowInfo2 in list)
				{
					windowInfo2.Close(true);
				}
			}
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x0005B350 File Offset: 0x00059550
		private AliwwMainWindow c(string A_0 = "")
		{
			string text = ((!string.IsNullOrEmpty(A_0)) ? A_0 : this.b);
			AliwwTalkWindowQn aliwwTalkWindowQn = AliwwTalkWindowQn.Get(text.Trim());
			AliwwMainWindow aliwwMainWindow = AliwwMainWindow.ParseFromWindowInfo(aliwwTalkWindowQn);
			AliwwMainWindow aliwwMainWindow2;
			if (aliwwMainWindow != null)
			{
				aliwwMainWindow.AliVersion = aliwwTalkWindowQn.AliwwVersion;
				aliwwMainWindow2 = aliwwMainWindow;
			}
			else
			{
				AliwwWorkBenchQn aliwwWorkBenchQn = AliwwWorkBenchQn.Get(text.Trim());
				aliwwMainWindow = AliwwMainWindow.ParseFromWindowInfo(aliwwWorkBenchQn);
				if (aliwwMainWindow != null)
				{
					aliwwMainWindow.AliVersion = aliwwWorkBenchQn.AliwwVersion;
					aliwwMainWindow2 = aliwwMainWindow;
				}
				else
				{
					aliwwMainWindow = AliwwMainWindow.ParseFromWindowInfo(AliwwTalkWindowBuyer9.Get(text.Trim()));
					if (aliwwMainWindow != null)
					{
						aliwwMainWindow.AliVersion = AliwwVersion.AliwwBuyer9;
						aliwwMainWindow2 = aliwwMainWindow;
					}
					else
					{
						aliwwMainWindow = AliwwMainWindow.ParseFromWindowInfo(WinAliwwMainBuyer9.Get(text.Trim()));
						if (aliwwMainWindow != null)
						{
							aliwwMainWindow.AliVersion = AliwwVersion.AliwwBuyer9;
							aliwwMainWindow2 = aliwwMainWindow;
						}
						else
						{
							aliwwMainWindow = AliwwMainWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", this.b.Trim() + "-阿里旺旺 读屏版"));
							if (aliwwMainWindow != null)
							{
								aliwwMainWindow.AliVersion = AliwwVersion.AliwwSeller2012ReadView;
								aliwwMainWindow2 = aliwwMainWindow;
							}
							else
							{
								aliwwMainWindow = AliwwMainWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", this.b.Trim() + "-阿里旺旺2013卖家版"));
								if (aliwwMainWindow != null)
								{
									aliwwMainWindow.AliVersion = AliwwVersion.AliwwSeller2013;
									aliwwMainWindow2 = aliwwMainWindow;
								}
								else
								{
									aliwwMainWindow = AliwwMainWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", this.b.Trim() + "-阿里旺旺"));
									if (aliwwMainWindow != null)
									{
										aliwwMainWindow.AliVersion = AliwwVersion.AliwwBuyer2014;
										aliwwMainWindow2 = aliwwMainWindow;
									}
									else
									{
										aliwwMainWindow = AliwwMainWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardWindow", this.b.Trim() + " - 千牛"));
										if (aliwwMainWindow != null)
										{
											aliwwMainWindow.AliVersion = AliwwVersion.QianNiu;
											aliwwMainWindow2 = aliwwMainWindow;
										}
										else
										{
											aliwwMainWindow = AliwwMainWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", this.b.Trim() + "-卖家工作台"));
											if (aliwwMainWindow != null)
											{
												aliwwMainWindow.AliVersion = AliwwVersion.QianNiu2;
												aliwwMainWindow2 = aliwwMainWindow;
											}
											else
											{
												aliwwMainWindow = AliwwMainWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", this.b.Trim() + " - 千牛"));
												if (aliwwMainWindow != null)
												{
													aliwwMainWindow.AliVersion = AliwwVersion.QianNiu2;
													aliwwMainWindow2 = aliwwMainWindow;
												}
												else
												{
													aliwwMainWindow = AliwwMainWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", this.b.Trim() + "-阿里旺旺2014"));
													if (aliwwMainWindow != null)
													{
														aliwwMainWindow.AliVersion = AliwwVersion.AliwwAlbb2014;
														aliwwMainWindow2 = aliwwMainWindow;
													}
													else
													{
														aliwwMainWindow = AliwwMainWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", this.b.Trim() + "-TradeManager2014"));
														if (aliwwMainWindow != null)
														{
															aliwwMainWindow.AliVersion = AliwwVersion.TradeManager2014;
															aliwwMainWindow2 = aliwwMainWindow;
														}
														else
														{
															aliwwMainWindow = AliwwMainWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", this.b.Trim() + "-TradeManager2015"));
															if (aliwwMainWindow != null)
															{
																aliwwMainWindow.AliVersion = AliwwVersion.TradeManager2015;
																aliwwMainWindow2 = aliwwMainWindow;
															}
															else
															{
																aliwwMainWindow = AliwwMainWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", this.b.Trim() + "-TM 2015"));
																if (aliwwMainWindow != null)
																{
																	aliwwMainWindow.AliVersion = AliwwVersion.TradeManager2015;
																	aliwwMainWindow2 = aliwwMainWindow;
																}
																else
																{
																	aliwwMainWindow = AliwwMainWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", this.b.Trim() + " - 插件中心"));
																	if (aliwwMainWindow != null)
																	{
																		aliwwMainWindow.AliVersion = AliwwVersion.QianNiu3;
																		aliwwMainWindow2 = aliwwMainWindow;
																	}
																	else
																	{
																		aliwwMainWindow = AliwwMainWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", this.b.Trim() + " - 客服工作台"));
																		if (aliwwMainWindow != null)
																		{
																			aliwwMainWindow.AliVersion = AliwwVersion.QianNiu3;
																			aliwwMainWindow2 = aliwwMainWindow;
																		}
																		else
																		{
																			aliwwMainWindow2 = null;
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return aliwwMainWindow2;
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x0005B6E8 File Offset: 0x000598E8
		public AliwwTalkWindow GetCustomerBenchWindow(bool onlyQn = false)
		{
			AliwwTalkWindow aliwwTalkWindow = this.a(onlyQn);
			if (aliwwTalkWindow != null)
			{
				aliwwTalkWindow.CurrentAliww = this;
				aliwwTalkWindow.UserNick = this.b;
			}
			return aliwwTalkWindow;
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x0005B71C File Offset: 0x0005991C
		private AliwwTalkWindow a(bool A_0 = false)
		{
			switch (this.AliwwVersion)
			{
			case AliwwVersion.QianNiu:
			{
				AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardWindow", this.b.Trim() + " - 客服工作台"));
				if (aliwwTalkWindow != null)
				{
					aliwwTalkWindow.AliVersion = this.AliwwVersion;
					aliwwTalkWindow.chatWindowType = ChatWindowType.CustomerBench;
					aliwwTalkWindow.UserNick = this.b;
					return aliwwTalkWindow;
				}
				break;
			}
			case AliwwVersion.AliwwSeller2012ReadView:
			{
				if (A_0)
				{
					return null;
				}
				AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardWindow", this.b.Trim() + "--客服工作台"));
				if (aliwwTalkWindow != null)
				{
					aliwwTalkWindow.AliVersion = this.AliwwVersion;
					aliwwTalkWindow.chatWindowType = ChatWindowType.CustomerBench;
					aliwwTalkWindow.UserNick = this.b;
					return aliwwTalkWindow;
				}
				break;
			}
			case AliwwVersion.AliwwSeller2013:
			{
				if (A_0)
				{
					return null;
				}
				AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardWindow", this.b.Trim() + "--客服工作台"));
				if (aliwwTalkWindow != null)
				{
					aliwwTalkWindow.AliVersion = this.AliwwVersion;
					aliwwTalkWindow.chatWindowType = ChatWindowType.CustomerBench;
					aliwwTalkWindow.UserNick = this.b;
					return aliwwTalkWindow;
				}
				break;
			}
			case AliwwVersion.AliwwBuyer9:
			{
				if (A_0)
				{
					return null;
				}
				AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", "阿里旺旺 - " + this.b.Trim()));
				if (aliwwTalkWindow == null && !string.IsNullOrEmpty(this.CurrUserCache.UserNickAs))
				{
					aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", "阿里旺旺 - " + this.CurrUserCache.UserNickAs.Trim()));
				}
				if (aliwwTalkWindow != null)
				{
					aliwwTalkWindow.AliVersion = this.AliwwVersion;
					aliwwTalkWindow.chatWindowType = ChatWindowType.CustomerBench;
					aliwwTalkWindow.UserNick = this.b;
					return aliwwTalkWindow;
				}
				break;
			}
			case AliwwVersion.QianNiu2:
			{
				AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", this.b.Trim() + " - 客服工作台"));
				if (aliwwTalkWindow != null)
				{
					aliwwTalkWindow.AliVersion = this.AliwwVersion;
					aliwwTalkWindow.chatWindowType = ChatWindowType.CustomerBench;
					aliwwTalkWindow.UserNick = this.b;
					return aliwwTalkWindow;
				}
				break;
			}
			case AliwwVersion.QianNiu3:
			case AliwwVersion.QianNiu5:
			case AliwwVersion.QianNiu9:
			{
				AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(AliwwTalkWindowQn.Get(this.b.Trim()));
				if (aliwwTalkWindow == null)
				{
					this.d();
					if (!string.IsNullOrEmpty(this.CurrUserCache.UserNickAs))
					{
						aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(AliwwTalkWindowQn.Get(this.CurrUserCache.UserNickAs.Trim()));
					}
				}
				if (aliwwTalkWindow != null)
				{
					aliwwTalkWindow.AliVersion = this.AliwwVersion;
					aliwwTalkWindow.chatWindowType = ChatWindowType.CustomerBench;
					aliwwTalkWindow.UserNick = this.b;
					return aliwwTalkWindow;
				}
				break;
			}
			case AliwwVersion.AliwwAlbb2014:
			{
				if (A_0)
				{
					return null;
				}
				AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", this.b.Trim() + " - 卖家工作台"));
				if (aliwwTalkWindow != null)
				{
					aliwwTalkWindow.AliVersion = this.AliwwVersion;
					aliwwTalkWindow.chatWindowType = ChatWindowType.CustomerBench;
					aliwwTalkWindow.UserNick = this.b;
					return aliwwTalkWindow;
				}
				break;
			}
			case AliwwVersion.TradeManager2014:
			{
				if (A_0)
				{
					return null;
				}
				AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", this.b.Trim() + " - 卖家工作台"));
				if (aliwwTalkWindow != null)
				{
					aliwwTalkWindow.AliVersion = this.AliwwVersion;
					aliwwTalkWindow.chatWindowType = ChatWindowType.CustomerBench;
					aliwwTalkWindow.UserNick = this.b;
					return aliwwTalkWindow;
				}
				break;
			}
			case AliwwVersion.TradeManager2015:
			{
				if (A_0)
				{
					return null;
				}
				AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", this.b.Trim() + " - 卖家工作台"));
				if (aliwwTalkWindow != null)
				{
					aliwwTalkWindow.AliVersion = this.AliwwVersion;
					aliwwTalkWindow.chatWindowType = ChatWindowType.CustomerBench;
					aliwwTalkWindow.UserNick = this.b;
					return aliwwTalkWindow;
				}
				break;
			}
			}
			return null;
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x0005BB34 File Offset: 0x00059D34
		private AliwwTalkWindow b(string A_0)
		{
			AliwwTalkWindow aliwwTalkWindow = this.a(A_0);
			if (aliwwTalkWindow != null)
			{
				aliwwTalkWindow.CurrentAliww = this;
				aliwwTalkWindow.UserNick = this.b;
			}
			return aliwwTalkWindow;
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x0005BB68 File Offset: 0x00059D68
		private AliwwTalkWindow a(string A_0)
		{
			switch (this.AliwwVersion)
			{
			case AliwwVersion.AliwwSeller2012ReadView:
			{
				AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", A_0.Trim() + Aliww.GetWindowNameSuffix(this.b)));
				if (aliwwTalkWindow != null)
				{
					aliwwTalkWindow.AliVersion = AliwwVersion.AliwwSeller2012ReadView;
					aliwwTalkWindow.chatWindowType = ChatWindowType.ChatWindow;
					aliwwTalkWindow.UserNick = this.b;
					return aliwwTalkWindow;
				}
				break;
			}
			case AliwwVersion.AliwwSeller2013:
			{
				AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", A_0.Trim() + Aliww.GetWindowNameSuffix(this.b)));
				if (aliwwTalkWindow != null)
				{
					aliwwTalkWindow.AliVersion = AliwwVersion.AliwwSeller2013;
					aliwwTalkWindow.chatWindowType = ChatWindowType.ChatWindow;
					aliwwTalkWindow.UserNick = this.b;
					return aliwwTalkWindow;
				}
				break;
			}
			case AliwwVersion.AliwwBuyer2014:
			{
				AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", A_0.Trim() + Aliww.GetWindowNameSuffix(this.b)));
				if (aliwwTalkWindow != null)
				{
					aliwwTalkWindow.AliVersion = AliwwVersion.AliwwBuyer2014;
					aliwwTalkWindow.chatWindowType = ChatWindowType.ChatWindow;
					aliwwTalkWindow.UserNick = this.b;
					return aliwwTalkWindow;
				}
				break;
			}
			case AliwwVersion.AliwwAlbb2014:
			{
				AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", A_0.Trim() + Aliww.GetWindowNameSuffix(this.b)));
				if (aliwwTalkWindow != null)
				{
					aliwwTalkWindow.AliVersion = AliwwVersion.AliwwAlbb2014;
					aliwwTalkWindow.chatWindowType = ChatWindowType.ChatWindow;
					aliwwTalkWindow.UserNick = this.b;
					return aliwwTalkWindow;
				}
				break;
			}
			case AliwwVersion.TradeManager2014:
			{
				AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", A_0.Trim() + Aliww.GetWindowNameSuffix(this.b)));
				if (aliwwTalkWindow != null)
				{
					aliwwTalkWindow.AliVersion = AliwwVersion.TradeManager2014;
					aliwwTalkWindow.chatWindowType = ChatWindowType.ChatWindow;
					aliwwTalkWindow.UserNick = this.b;
					return aliwwTalkWindow;
				}
				break;
			}
			case AliwwVersion.TradeManager2015:
			{
				AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", A_0.Trim() + Aliww.GetWindowNameSuffix(this.b)));
				if (aliwwTalkWindow != null)
				{
					aliwwTalkWindow.AliVersion = AliwwVersion.TradeManager2015;
					aliwwTalkWindow.chatWindowType = ChatWindowType.ChatWindow;
					aliwwTalkWindow.UserNick = this.b;
					return aliwwTalkWindow;
				}
				break;
			}
			}
			return null;
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x0005BDA4 File Offset: 0x00059FA4
		public static string GetWindowNameSuffix(string userNick)
		{
			return " - " + userNick.Trim();
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x0005BDC4 File Offset: 0x00059FC4
		public List<AliwwTalkWindow> EnumAliwwChatWindow()
		{
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(new WindowStruct("StandardFrame", null), 0, false);
			List<AliwwTalkWindow> list;
			if (windowListByClassAndName == null || windowListByClassAndName.Count == 0)
			{
				list = null;
			}
			else
			{
				string windowNameSuffix = Aliww.GetWindowNameSuffix(this.b);
				List<AliwwTalkWindow> list2 = new List<AliwwTalkWindow>();
				foreach (WindowInfo windowInfo in windowListByClassAndName)
				{
					if (windowInfo.Info.WindowName.EndsWith(windowNameSuffix))
					{
						AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(windowInfo);
						aliwwTalkWindow.UserNick = this.b;
						aliwwTalkWindow.chatWindowType = ChatWindowType.ChatWindow;
						aliwwTalkWindow.CurrentAliww = this;
						aliwwTalkWindow.AliVersion = this.AliwwVersion;
						list2.Add(aliwwTalkWindow);
					}
				}
				list = list2;
			}
			return list;
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x0005BEA4 File Offset: 0x0005A0A4
		public AliwwTalkWindow GetTalkWindow(string contactNick)
		{
			AliwwTalkWindow aliwwTalkWindow = this.GetCustomerBenchWindow(false);
			if (aliwwTalkWindow == null)
			{
				aliwwTalkWindow = this.b(contactNick);
			}
			return aliwwTalkWindow;
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x0005BECC File Offset: 0x0005A0CC
		public WindowInfo GetUserInfoWindow(string buyerNick, List<WindowInfo> wiList)
		{
			WindowInfo windowInfo = null;
			foreach (WindowInfo windowInfo2 in wiList)
			{
				switch (this.AliwwVersion)
				{
				case AliwwVersion.AliwwSeller2012ReadView:
				case AliwwVersion.AliwwSeller2013:
				case AliwwVersion.AliwwBuyer9:
				case AliwwVersion.QianNiu2:
				case AliwwVersion.QianNiu3:
				case AliwwVersion.QianNiu5:
				case AliwwVersion.QianNiu9:
					if (windowInfo2.Info.WindowName.ToLower().Equals(buyerNick.Trim().ToLower() + "的资料"))
					{
						windowInfo = windowInfo2;
					}
					else
					{
						windowInfo2.Close(true);
					}
					break;
				}
			}
			return windowInfo;
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x0005BFA0 File Offset: 0x0005A1A0
		public List<WindowInfo> GetUserInfoWindowList()
		{
			List<WindowInfo> list = new List<WindowInfo>();
			List<WindowInfo> list2;
			switch (this.AliwwVersion)
			{
			case AliwwVersion.AliwwSeller2012ReadView:
			case AliwwVersion.AliwwSeller2013:
			{
				list2 = Win32Extend.GetWindowListByClass("StandardWindow");
				using (List<WindowInfo>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						WindowInfo windowInfo = enumerator.Current;
						if (windowInfo.Info.WindowName.EndsWith("的资料"))
						{
							list.Add(windowInfo);
						}
					}
					return list;
				}
				break;
			}
			case AliwwVersion.AliwwBuyer2014:
			case AliwwVersion.AliwwAlbb2014:
			case AliwwVersion.TradeManager2014:
			case AliwwVersion.TradeManager2015:
			case AliwwVersion.AliwwBuyer9_QianNiu5:
				return list;
			case AliwwVersion.AliwwBuyer9:
			case AliwwVersion.QianNiu2:
			case AliwwVersion.QianNiu3:
			case AliwwVersion.QianNiu5:
			case AliwwVersion.QianNiu9:
				break;
			default:
				return list;
			}
			list2 = Win32Extend.GetWindowListByClass("#32770");
			foreach (WindowInfo windowInfo2 in list2)
			{
				if (windowInfo2.Info.WindowName.EndsWith("的资料"))
				{
					list.Add(windowInfo2);
				}
			}
			return list;
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x0005C0D0 File Offset: 0x0005A2D0
		public List<WindowInfo> EnumUserInfoWindow()
		{
			List<WindowInfo> userInfoWindowList = this.GetUserInfoWindowList();
			List<WindowInfo> list;
			if (userInfoWindowList == null)
			{
				list = null;
			}
			else
			{
				list = userInfoWindowList;
			}
			return list;
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x0005C0F4 File Offset: 0x0005A2F4
		public List<WindowInfo> EnumSelectAlreadyLoginAccountWindows()
		{
			List<WindowInfo> list;
			if (this.AliwwVersion == AliwwVersion.TradeManager2015)
			{
				List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(new WindowStruct("#32770", "请选择已登录的TradeManager帐号"), 0, false);
				list = windowListByClassAndName;
			}
			else
			{
				list = null;
			}
			return list;
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x0005C130 File Offset: 0x0005A330
		public List<WindowInfo> EnumAliwwFriendNeedToAddWindow()
		{
			WindowStruct windowStruct;
			switch (this.AliwwVersion)
			{
			case AliwwVersion.QianNiu:
			case AliwwVersion.AliwwSeller2012ReadView:
			case AliwwVersion.AliwwSeller2013:
			case AliwwVersion.AliwwBuyer2014:
			case AliwwVersion.AliwwBuyer9:
			case AliwwVersion.QianNiu2:
			case AliwwVersion.QianNiu5:
			case AliwwVersion.QianNiu9:
				windowStruct = new WindowStruct("#32770", "添加好友");
				goto IL_0094;
			case AliwwVersion.AliwwAlbb2014:
			case AliwwVersion.TradeManager2014:
				windowStruct = new WindowStruct("StandardWindow", "添加好友信息");
				goto IL_0094;
			case AliwwVersion.TradeManager2015:
				windowStruct = new WindowStruct("StandardFrame", "添加好友信息");
				goto IL_0094;
			}
			windowStruct = new WindowStruct("", "添加好友");
			IL_0094:
			return Win32Extend.GetWindowListByClassAndName(windowStruct, this.CurrProcessId, true);
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x0005C1E0 File Offset: 0x0005A3E0
		public List<WindowInfo> EnumAliwwAccountNotExistsWindow()
		{
			if (this.AliwwVersion == AliwwVersion.QianNiu5)
			{
				List<WinFileNotAcceptAlert> list = WinBlackListOrNotAccountExistsAlert.GetList(this.CurrProcessId);
				if (!AppConfig.AllowAutoLogin && (list == null || list.Count <= 0))
				{
					list = WinBlackListOrNotAccountExistsAlert.GetListQn(this.CurrProcessId);
				}
				if (list != null)
				{
					return list.Select(new Func<WinFileNotAcceptAlert, WindowInfo>(Aliww.<>c.<>9.a)).ToList<WindowInfo>();
				}
			}
			return null;
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x0005C268 File Offset: 0x0005A468
		public List<WindowInfo> EnumAliwwAlertWindow()
		{
			AliwwVersion aliwwVersion = this.AliwwVersion;
			AliwwVersion aliwwVersion2 = aliwwVersion;
			if (aliwwVersion2 - AliwwVersion.QianNiu <= 5 || aliwwVersion2 - AliwwVersion.AliwwAlbb2014 <= 2)
			{
			}
			WindowStruct windowStruct = new WindowStruct("#32770", "阿里旺旺");
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(windowStruct, this.CurrProcessId, false);
			List<WindowInfo> list = new List<WindowInfo>();
			foreach (WindowInfo windowInfo in windowListByClassAndName)
			{
				IntPtr childHandleByClassTreePath = Win32Extend.GetChildHandleByClassTreePath(windowInfo.HWnd, new string[] { "WebControl", "AEF_WidgetWin_0", "AEF_RenderWidgetHostHWND" });
				if (!(childHandleByClassTreePath != IntPtr.Zero))
				{
					list.Add(windowInfo);
				}
			}
			return list;
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x0005C33C File Offset: 0x0005A53C
		private void a(int A_0)
		{
			List<WindowInfo> list = this.EnumUserInfoWindow();
			if (this.AllowCheckTargetNick && list != null && list.Count > 0 && A_0 < 3)
			{
				foreach (WindowInfo windowInfo in list)
				{
					windowInfo.Close(true);
					Application.DoEvents();
				}
				Thread.Sleep(300);
				A_0++;
				this.a(A_0);
			}
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x0005C3CC File Offset: 0x0005A5CC
		private void i()
		{
			List<WindowInfo> list = this.EnumSelectAlreadyLoginAccountWindows();
			if (list != null)
			{
				foreach (WindowInfo windowInfo in list)
				{
					windowInfo.Close(true);
					Application.DoEvents();
				}
			}
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x0005C430 File Offset: 0x0005A630
		private void h()
		{
			List<WindowInfo> list = this.EnumAliwwAlertWindow();
			if (list != null)
			{
				foreach (WindowInfo windowInfo in list)
				{
					windowInfo.Close(true);
					Application.DoEvents();
				}
			}
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x0005C494 File Offset: 0x0005A694
		public void CloseAllAliwwFriendNeedToAddWindow()
		{
			List<WindowInfo> list = this.EnumAliwwFriendNeedToAddWindow();
			if (list != null)
			{
				foreach (WindowInfo windowInfo in list)
				{
					windowInfo.Close(true);
					Application.DoEvents();
				}
			}
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x0005C4F8 File Offset: 0x0005A6F8
		public bool CloseAllAliwwAccountNotExistsWindow()
		{
			List<WindowInfo> list = this.EnumAliwwAccountNotExistsWindow();
			bool flag;
			if (list != null && list.Count > 0)
			{
				foreach (WindowInfo windowInfo in list)
				{
					windowInfo.Close(true);
					Application.DoEvents();
				}
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x0005C570 File Offset: 0x0005A770
		private IntPtr g()
		{
			IntPtr intPtr;
			if (this.MainWindow == null)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				IntPtr hwnd = this.MainWindow.HWnd;
				IntPtr intPtr2 = 0;
				IntPtr intPtr3 = IntPtr.Zero;
				IntPtr intPtr4 = IntPtr.Zero;
				switch (this.MainWindow.AliVersion)
				{
				case AliwwVersion.QianNiu:
					intPtr3 = WindowsAPI.FindWindowEx(hwnd, IntPtr.Zero, "StandardWindow", null);
					intPtr4 = WindowsAPI.FindWindowEx(hwnd, intPtr3, "StandardWindow", null);
					intPtr2 = WindowsAPI.FindWindowEx(intPtr4, IntPtr.Zero, "EditComponent", null);
					goto IL_02FD;
				case AliwwVersion.AliwwSeller2012ReadView:
				case AliwwVersion.AliwwSeller2013:
				case AliwwVersion.AliwwBuyer2014:
					intPtr2 = WindowsAPI.FindWindowEx(hwnd, IntPtr.Zero, "EditComponent", "查找好友和网站内容...");
					if (intPtr2 == IntPtr.Zero)
					{
						intPtr3 = WindowsAPI.FindWindowEx(hwnd, IntPtr.Zero, "StandardWindow", "WorkingPage");
						intPtr4 = WindowsAPI.FindWindowEx(intPtr3, IntPtr.Zero, "StandardWindow", null);
						intPtr4 = WindowsAPI.FindWindowEx(intPtr3, intPtr4, "StandardWindow", null);
						intPtr4 = WindowsAPI.FindWindowEx(intPtr3, intPtr4, "StandardWindow", null);
						intPtr2 = WindowsAPI.FindWindowEx(intPtr4, IntPtr.Zero, "EditComponent", null);
						goto IL_02FD;
					}
					goto IL_02FD;
				case AliwwVersion.AliwwBuyer9:
				case AliwwVersion.QianNiu3:
					goto IL_02FD;
				case AliwwVersion.QianNiu2:
					intPtr3 = WindowsAPI.FindWindowEx(hwnd, IntPtr.Zero, "StandardWindow", null);
					intPtr4 = WindowsAPI.FindWindowEx(hwnd, intPtr3, "StandardWindow", null);
					intPtr4 = WindowsAPI.FindWindowEx(intPtr4, IntPtr.Zero, "StandardWindow", null);
					intPtr2 = WindowsAPI.FindWindowEx(intPtr4, IntPtr.Zero, "EditComponent", null);
					goto IL_02FD;
				case AliwwVersion.QianNiu5:
				{
					AliwwTalkWindow customerBenchWindow = this.GetCustomerBenchWindow(false);
					if (customerBenchWindow == null)
					{
						goto IL_02FD;
					}
					List<IntPtr> childHandlesByClassTreePath = Win32Extend.GetChildHandlesByClassTreePath(customerBenchWindow.HWnd, new string[] { "StandardWindow", "EditComponent" });
					if (childHandlesByClassTreePath == null || childHandlesByClassTreePath.Count <= 0)
					{
						goto IL_02FD;
					}
					using (List<IntPtr>.Enumerator enumerator = childHandlesByClassTreePath.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							IntPtr intPtr5 = enumerator.Current;
							WindowInfo windowFromHandler = WindowInfo.GetWindowFromHandler(intPtr5);
							if (windowFromHandler != null)
							{
								string text = windowFromHandler.GetText();
								if ("好友/群/聊天记录".Equals(text) || text.Contains("好友") || text.Contains("聊天记录") || text.Contains("/群"))
								{
									intPtr2 = windowFromHandler.HWnd;
									IL_029D:
									goto IL_02FD;
								}
								WindowInfo nextWindow = windowFromHandler.GetNextWindow(null, null);
								if (nextWindow != null && "StandardButton".Equals(nextWindow.Info.ClassName))
								{
									intPtr2 = windowFromHandler.HWnd;
								}
							}
						}
						goto IL_029D;
					}
					break;
				}
				case AliwwVersion.AliwwAlbb2014:
				case AliwwVersion.TradeManager2014:
				case AliwwVersion.TradeManager2015:
					break;
				default:
					goto IL_02FD;
				}
				intPtr2 = WindowsAPI.FindWindowEx(hwnd, IntPtr.Zero, "EditComponent", "查找好友和网站内容...");
				if (intPtr2 == IntPtr.Zero)
				{
					intPtr3 = WindowsAPI.FindWindowEx(hwnd, IntPtr.Zero, "StandardWindow", "WorkingPage");
					intPtr2 = WindowsAPI.FindWindowEx(intPtr3, IntPtr.Zero, "EditComponent", null);
				}
				IL_02FD:
				intPtr = intPtr2;
			}
			return intPtr;
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x0005C890 File Offset: 0x0005AA90
		private void f()
		{
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(new WindowStruct(null, "指定用户发送"), 0, false);
			for (int i = 0; i < windowListByClassAndName.Count; i++)
			{
				windowListByClassAndName[i].Close(true);
				Application.DoEvents();
			}
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x0005C8D8 File Offset: 0x0005AAD8
		private bool e()
		{
			List<WindowInfo> list = new List<WindowInfo>();
			List<WindowInfo> list2 = this.EnumAliwwFriendNeedToAddWindow();
			if (list2 != null && list2.Count > 0)
			{
				foreach (WindowInfo windowInfo in list2)
				{
					list.Add(windowInfo);
				}
			}
			List<WindowInfo> list3 = this.EnumAliwwAlertWindow();
			if (list3 != null && list3.Count > 0)
			{
				foreach (WindowInfo windowInfo2 in list3)
				{
					list.Add(windowInfo2);
				}
			}
			bool flag;
			if (list.Count > 0)
			{
				LogWriter.WriteLog("存在需要加好友的窗口句柄：" + list[0].HWnd.ToString(), 3);
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x0005C9E0 File Offset: 0x0005ABE0
		public void RejectUser(string msgBody, ErrCodeInfo errCode, WindowInfo win = null, string noticeSellerMsgExtend = "")
		{
			if (AppConfig.AllowAutoLogin)
			{
				if (!msgBody.IsActivateMsg())
				{
					AppConfig.QnAgentServiceClient.RejectUser(this.UserNick, Util.GetEnumDescription(errCode.ErrCode));
					FileItem fileItem = null;
					if (win != null)
					{
						using (Bitmap bitmapFromDC = win.GetBitmapFromDC(0))
						{
							byte[] array = Util.smethod_1(bitmapFromDC, ImageFormat.Png);
							fileItem = new FileItem("screenshot.jpg", array, "image/jpeg");
						}
					}
					string noticeSellerMsg = this.GetNoticeSellerMsg(errCode.ErrCode, noticeSellerMsgExtend);
					ErrCodeType errCode2 = errCode.ErrCode;
					ErrCodeType errCodeType = errCode2;
					string text;
					if (errCodeType <= ErrCodeType.PasswordError)
					{
						if (errCodeType == ErrCodeType.BindingIncorrectMobile)
						{
							goto IL_00EA;
						}
						if (errCodeType - ErrCodeType.LimitLogin <= 1)
						{
							text = "https://www.yuque.com/agiso/aldstb/pqg1fe";
							goto IL_00F2;
						}
					}
					else
					{
						if (errCodeType == ErrCodeType.SendFailQn5LoginAuthInterceptNeedToScanQr)
						{
							goto IL_00EA;
						}
						if (errCodeType == ErrCodeType.SendFailQn5LoginFailGetSmsValidCodeNull)
						{
							text = "https://www.yuque.com/agiso/aldstb/gl0r3m3f6dq8tyf5";
							goto IL_00F2;
						}
						if (errCodeType == ErrCodeType.SendFailAccountIsBanned)
						{
							goto IL_00EA;
						}
					}
					text = "";
					goto IL_00F2;
					IL_00EA:
					text = "https://www.yuque.com/agiso/aldstb/pma44u";
					IL_00F2:
					AppConfig.QnAgentServiceClient.AutoLoginError(this.UserNick, fileItem, (fileItem == null) ? "" : Util.GetEnumDescription(errCode.ErrCode), 1L, noticeSellerMsg, text);
				}
				AppConfig.GetSellerExecuteCache(Util.GetMasterNick(this.UserNick)).RoolbackMsgs(true);
			}
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x0005CB40 File Offset: 0x0005AD40
		public string GetNoticeSellerMsg(ErrCodeType errCode, string msgExtend = "")
		{
			string text = "";
			if (errCode <= ErrCodeType.PasswordError)
			{
				if (errCode <= ErrCodeType.NeedIdVerification)
				{
					if (errCode != ErrCodeType.NeedSendSmsToValidate)
					{
						if (errCode == ErrCodeType.NeedIdVerification)
						{
							text = "您好，您的代挂子账号登录失败，提示需要身份证验证，可能是手机区号填写有误，请修改手机区号后，在重新激活";
						}
					}
					else
					{
						text = "您好，您的代挂子账号登录失败，需要短信验证，可以更换子号在试";
					}
				}
				else if (errCode != ErrCodeType.LoginLimitNeedMasterAccountSmsCode)
				{
					switch (errCode)
					{
					case ErrCodeType.BindingIncorrectMobile:
					{
						IEnumerable<string> enumerable = AppConfig.AgentPhones.Take(2);
						text = "您好，您的代挂子账号绑定的手机号不是指定的" + string.Join("或", enumerable) + "，请修改后到配置页面重新激活。可以尝试添加多个子号以备用。";
						break;
					}
					case ErrCodeType.LimitLogin:
						text = "您好，您的代挂子账号被限制登录，需先修改子帐号密码，再到配置页面将正确的密码填上后重新激活。可以尝试添加多个子号以备用。";
						break;
					case ErrCodeType.PasswordError:
						text = "您好，您的代挂子账号登陆时提示密码错误，需到配置页面将正确的密码填上后重新激活。可以尝试添加多个子号以备用。";
						break;
					}
				}
				else
				{
					text = "您好，您的代挂子账号需要主账号手机短信验证，请手动登录子账号验证通过后，在重新激活。";
				}
			}
			else if (errCode <= ErrCodeType.SendFailQn5LoginAuthInterceptNeedToScanQr)
			{
				if (errCode != ErrCodeType.SendFailQn5LoginFailNeedToConcatTb)
				{
					if (errCode == ErrCodeType.SendFailQn5LoginAuthInterceptNeedToScanQr)
					{
						text = "您好，您的代挂子账号登录时提示您的账号未进行实人认证，请按代挂服务配置页面的教程重新操作实人认证后，再重新自助激活！";
					}
				}
				else
				{
					text = "您好，您的代挂子账号被限制登录，需先修改子帐号密码，再到配置页面将正确的密码填上后重新激活。可以尝试添加多个子号以备用。";
				}
			}
			else if (errCode != ErrCodeType.SendFailQn5LoginFailGetSmsValidCodeNull)
			{
				if (errCode == ErrCodeType.SendFailAccountIsBanned)
				{
					text = string.Concat(new string[]
					{
						"您好，您的代挂子账号--",
						this.UserNick,
						"，消息发送失败，账号已被禁言了\r\n",
						string.IsNullOrEmpty(msgExtend) ? "" : ("，禁言至" + msgExtend),
						"。请更换新的代挂子账号，以免影响您的消息发送。"
					});
				}
			}
			else
			{
				text = "代挂账号绑定的手机号码已失效，请重新换绑激活";
			}
			return text;
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x0005CCB4 File Offset: 0x0005AEB4
		public ErrCodeInfo CheckMsgIsSendSucc(AliwwTalkWindow alitw, string msgBody, string toUserNick, ErrCodeInfo errCod)
		{
			ErrCodeInfo errCodeInfo;
			if (errCod == null)
			{
				errCodeInfo = null;
			}
			else
			{
				if (errCod.ErrCode == ErrCodeType.SendFailAll || errCod.ErrCode == ErrCodeType.SendFailPiece)
				{
					bool flag;
					if (errCod.InnerErrCodes != null)
					{
						flag = errCod.InnerErrCodes.Any(new Func<ErrCodeInfo, bool>(Aliww.<>c.<>9.a));
					}
					else
					{
						flag = false;
					}
					if (flag)
					{
						errCod = new ErrCodeInfo(ErrCodeType.SendFailTeamForbid);
						return errCod;
					}
				}
				if (errCod.ErrCode == ErrCodeType.SendSucc)
				{
					Aliww.a a = new Aliww.a();
					a.b = this;
					bool flag2;
					if (this.AliwwVersion != AliwwVersion.QianNiu5)
					{
						if (this.AliwwVersion != AliwwVersion.QianNiu9)
						{
							flag2 = false;
							goto IL_00C4;
						}
					}
					flag2 = !this.CurrUserCache.IsRecentSessionNull;
					IL_00C4:
					if (flag2 && (msgBody != null && !msgBody.IsActivateMsg()))
					{
						this.a(alitw, msgBody, toUserNick, ref errCod);
						if (errCod.ErrCode != ErrCodeType.SendSucc)
						{
							return errCod;
						}
						if (AppConfig.AllowAutoLogin)
						{
							this.a(alitw, ref errCod);
							if (errCod.ErrCode != ErrCodeType.SendSucc)
							{
								return errCod;
							}
						}
					}
					a.a = false;
					if (AppConfig.AllowAutoLogin && AppConfig.AgentSettings.QnVersion == 2 && errCod.ErrCode == ErrCodeType.SendSucc)
					{
						List<WinFuwuTip> list = WinFuwuTip.GetList(this.CurrProcessId);
						if (list != null)
						{
							list.ForEach(new Action<WinFuwuTip>(a.e));
						}
					}
					if (AppConfig.IsSellerLoginOnOwnComputer && errCod.ErrCode == ErrCodeType.SendSucc)
					{
						Util.CheckWait(500, new Func<bool>(a.d), 100);
					}
					if (a.a && !Util.IsEmptyList<WinFuwuTip>(WinFuwuTip.GetList(this.CurrProcessId)))
					{
						errCod = new ErrCodeInfo(ErrCodeType.SendFailHasFuwuTipWindow);
					}
				}
				errCodeInfo = errCod;
			}
			return errCodeInfo;
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x0005CEA4 File Offset: 0x0005B0A4
		private void d()
		{
			if (string.IsNullOrEmpty(this.CurrUserCache.UserNickAs) && !this.CurrUserCache.IsSessionNull)
			{
				UserAdaptiveManager userAdaptiveManager = new UserAdaptiveManager(this.CurrUserCache.Session);
				int aliWorkbenchProcessId = userAdaptiveManager.GetAliWorkbenchProcessId();
				if (aliWorkbenchProcessId > 0)
				{
					List<AliwwTalkWindowQn> list = AliwwTalkWindowQn.GetList(aliWorkbenchProcessId);
					if (!Util.IsEmptyList<AliwwTalkWindowQn>(list) && !string.IsNullOrEmpty(list[0].UserNick) && list[0].UserNick.Trim() != this.UserNick)
					{
						this.CurrUserCache.UserNickAs = list[0].UserNick.Trim();
						AppConfig.AddOrUpdateUserCache(this.CurrUserCache.UserNickAs, this.CurrUserCache);
					}
				}
			}
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x0005CF74 File Offset: 0x0005B174
		private void a(AliwwTalkWindow A_0, string A_1, string A_2, ref ErrCodeInfo A_3)
		{
			int i = 0;
			while (i < 5)
			{
				if (Util.IsEmptyList<WinFuwuTip>(WinFuwuTip.GetList(this.CurrProcessId)))
				{
					AliwwMsgElement realLastRcvMsg = A_0.GetRealLastRcvMsg(false);
					if (realLastRcvMsg == null || !realLastRcvMsg.IsSysMsg)
					{
						Thread.Sleep(100);
						i++;
						continue;
					}
					if (realLastRcvMsg.ContentText.Contains("号被禁言") || realLastRcvMsg.ContentText.Contains("账号禁言"))
					{
						A_3 = new ErrCodeInfo(ErrCodeType.SendFailAccountIsBanned);
						if (AppConfig.AllowAutoLogin)
						{
							Regex regex = new Regex("\\d{4}-\\d{1,2}-\\d{1,2} \\d{1,2}:\\d{1,2}:\\d{1,2}");
							Match match = regex.Match(realLastRcvMsg.ContentText);
							string text = "";
							if (match.Success)
							{
								text = match.Groups[0].Value;
							}
							this.RejectUser(A_1, A_3, null, text);
						}
					}
					else if (realLastRcvMsg.ContentText.Contains("您发送的消息中可能包含了恶意网址、违规广告及其他类关键词，请停止发送类似的消息"))
					{
						A_3 = new ErrCodeInfo(ErrCodeType.SendFailIllegalKeywords);
					}
					else if (realLastRcvMsg.ContentText.Contains("由于对方将您添加至黑名单，您的消息已被拒收") || realLastRcvMsg.ContentText.Contains("将您添加至黑名单，您的消息已被拒收"))
					{
						A_3 = new ErrCodeInfo(ErrCodeType.CallFailTargetNickInBlackListOrNotExists);
					}
					else if (realLastRcvMsg.ContentText.Contains("您还不是对方的好友，对方设置不接收陌生人消息，无法收到您发的消息，请相互加为好友") || realLastRcvMsg.ContentText.Contains("对方设置不接收陌生人消息，需要对方和您沟通后才能收到您发的消息"))
					{
						A_3 = new ErrCodeInfo(ErrCodeType.CallFailTargetNickReceiveFriendOnly);
					}
					else if (realLastRcvMsg.ContentText.Contains("买家已开启消息拒收，无法再发送消息，买家回复或下单后可继续发送"))
					{
						A_3 = new ErrCodeInfo(ErrCodeType.SendFailBuyerRejectMessage);
					}
					else if (realLastRcvMsg.ContentText.Contains("系统识别到接待过程中存在敷衍行为，敷衍消息已拦截下发，请重新组织有效话术，每一个消费者都是潜在的客户"))
					{
						A_3 = new ErrCodeInfo(ErrCodeType.SendFailTbIntercept);
					}
					else if (realLastRcvMsg.ContentText.Contains("亲，由于您短时间内多次发送相同内容，容易给消费者造成困扰，最新的消息已被系统拦截，请重新组织话术"))
					{
						A_3 = new ErrCodeInfo(ErrCodeType.SendSameMsgTooManySoInterceptByTb);
					}
					else if (realLastRcvMsg.ContentText.Contains("您发送的消息中包含不安全的链接，消息发送失败"))
					{
						A_3 = new ErrCodeInfo(ErrCodeType.SendFailContainsUnsafeLink);
					}
					else if (realLastRcvMsg.ContentText.Contains("买家30天内主动和您沟通或下单后，您才能给买家发消息"))
					{
						A_3 = new ErrCodeInfo(ErrCodeType.SendFail30DaysNotContact);
					}
					else if (AppConfig.AllowAutoLogin && !AppConfig.IsQnSystemMsg(realLastRcvMsg.ContentText))
					{
						LogWriter.WriteLog(string.Concat(new string[]
						{
							"系统提示",
							Environment.NewLine,
							realLastRcvMsg.ContentText,
							Environment.NewLine,
							"UserNick：",
							this.UserNick,
							Environment.NewLine,
							"买家旺旺：",
							A_2
						}), 1);
					}
				}
				return;
			}
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x0005D228 File Offset: 0x0005B428
		private void a(AliwwTalkWindow A_0, ref ErrCodeInfo A_1)
		{
			int i = 0;
			while (i < 10)
			{
				if (Util.IsEmptyList<WinFuwuTip>(WinFuwuTip.GetList(this.CurrProcessId)))
				{
					AliwwMsgElement realLastRcvMsg = A_0.GetRealLastRcvMsg(true);
					if (realLastRcvMsg != null)
					{
						if (realLastRcvMsg.MsgType == MsgType.未知)
						{
							if (i == 9)
							{
								A_1 = new ErrCodeInfo(ErrCodeType.SendFailNotFindRead);
								break;
							}
							Thread.Sleep(150);
						}
						else
						{
							if (realLastRcvMsg.MsgType == MsgType.已读 || realLastRcvMsg.MsgType == MsgType.未读)
							{
								break;
							}
							if (realLastRcvMsg.MsgType == MsgType.小红点)
							{
								A_1 = new ErrCodeInfo(ErrCodeType.SendFailLittleRedDot);
								break;
							}
						}
					}
					i++;
					continue;
				}
				return;
			}
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x0000EBAA File Offset: 0x0000CDAA
		[CompilerGenerated]
		private bool c()
		{
			return this.MainWindow != null;
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x0000EBB5 File Offset: 0x0000CDB5
		[CompilerGenerated]
		private bool b()
		{
			return !this.CurrUserCache.IsSessionNull;
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x0000EBB5 File Offset: 0x0000CDB5
		[CompilerGenerated]
		private bool a()
		{
			return !this.CurrUserCache.IsSessionNull;
		}

		// Token: 0x04001D9F RID: 7583
		[CompilerGenerated]
		private bool a;

		// Token: 0x04001DA0 RID: 7584
		private string b;

		// Token: 0x04001DA1 RID: 7585
		private string c;

		// Token: 0x04001DA2 RID: 7586
		private static bool d = true;

		// Token: 0x04001DA3 RID: 7587
		private static string e = "cntaobao";

		// Token: 0x04001DA4 RID: 7588
		[CompilerGenerated]
		private Action<AliwwTalkWindow> f;

		// Token: 0x04001DA5 RID: 7589
		private int g;

		// Token: 0x04001DA6 RID: 7590
		private UserCache h;

		// Token: 0x04001DA7 RID: 7591
		private AliwwMainWindow i;

		// Token: 0x04001DA8 RID: 7592
		private AliwwVersion j;

		// Token: 0x02000701 RID: 1793
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x06002374 RID: 9076 RVA: 0x0005D2C4 File Offset: 0x0005B4C4
			internal void e(WinFuwuTip A_0)
			{
				Agiso.Windows.Rectangle clientRect = A_0.GetClientRect();
				A_0.SimulateMouseClick(clientRect.GetWidth() - 60, clientRect.GetHeight() - 39, false, 1);
				LogWriter.WriteLog("服务态度提醒2", 1);
				this.a = true;
				Thread.Sleep(200);
			}

			// Token: 0x06002375 RID: 9077 RVA: 0x0005D314 File Offset: 0x0005B514
			internal bool d()
			{
				List<WinFuwuTip> list = WinFuwuTip.GetList(this.b.CurrProcessId);
				if (list != null)
				{
					List<WinFuwuTip> list2 = list;
					Action<WinFuwuTip> action;
					if ((action = this.c) == null)
					{
						action = (this.c = new Action<WinFuwuTip>(this.d));
					}
					list2.ForEach(action);
				}
				return !Util.IsEmptyList<WinFuwuTip>(list);
			}

			// Token: 0x06002376 RID: 9078 RVA: 0x0005D2C4 File Offset: 0x0005B4C4
			internal void d(WinFuwuTip A_0)
			{
				Agiso.Windows.Rectangle clientRect = A_0.GetClientRect();
				A_0.SimulateMouseClick(clientRect.GetWidth() - 60, clientRect.GetHeight() - 39, false, 1);
				LogWriter.WriteLog("服务态度提醒2", 1);
				this.a = true;
				Thread.Sleep(200);
			}

			// Token: 0x04001DAC RID: 7596
			public bool a;

			// Token: 0x04001DAD RID: 7597
			public Aliww b;

			// Token: 0x04001DAE RID: 7598
			public Action<WinFuwuTip> c;
		}
	}
}
