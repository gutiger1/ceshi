using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Agiso;
using Agiso.AliwwApi;
using Agiso.AliwwApi.Qn;
using Agiso.DbManager;
using Agiso.Extensions;
using Agiso.Handler;
using Agiso.Object;
using Agiso.Utils;
using Agiso.Windows;
using Agiso.WwService.Sdk;
using Agiso.WwService.Sdk.Domain;
using Agiso.WwService.Sdk.Response;
using AliwwClient.Cache;
using AliwwClient.Enums;
using AliwwClient.Manager;
using AliwwClient.Object;
using AliwwClient.QN.Workbench;
using AliwwClient.WebSocketServer.Extensions;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace AliwwClient.WebSocketServer
{
	// Token: 0x0200006C RID: 108
	public abstract class BehaviorBase : WebSocketBehavior
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0003526C File Offset: 0x0003346C
		public string UserNick
		{
			get
			{
				if (this.a == null)
				{
					this.a = base.QueryString["userNick"];
				}
				return this.a;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000324 RID: 804 RVA: 0x000352A4 File Offset: 0x000334A4
		public long UserId
		{
			get
			{
				if (this.b <= 0L)
				{
					this.b = Util.ToLong(base.QueryString["userId"]);
				}
				return this.b;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000325 RID: 805 RVA: 0x000352EC File Offset: 0x000334EC
		public string Version
		{
			get
			{
				if (string.IsNullOrEmpty(this.c))
				{
					this.c = base.QueryString["version"];
				}
				return this.c;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00035328 File Offset: 0x00033528
		// (set) Token: 0x06000327 RID: 807 RVA: 0x000031EE File Offset: 0x000013EE
		public string QnVersion
		{
			get
			{
				if (string.IsNullOrEmpty(this.d))
				{
					this.d = base.QueryString["qnVersion"];
				}
				return this.d;
			}
			private set
			{
				this.d = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000328 RID: 808 RVA: 0x000031F7 File Offset: 0x000013F7
		// (set) Token: 0x06000329 RID: 809 RVA: 0x000031FF File Offset: 0x000013FF
		public string AccountUserNick { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00003208 File Offset: 0x00001408
		public ConcurrentDictionary<string, string> DictWebSocketString { get; } = new ConcurrentDictionary<string, string>();

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00003210 File Offset: 0x00001410
		public ConcurrentDictionary<string, object> DictWebSocketObj { get; } = new ConcurrentDictionary<string, object>();

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00003218 File Offset: 0x00001418
		public ConcurrentDictionary<string, int> DictWebSocketInt { get; } = new ConcurrentDictionary<string, int>();

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00035364 File Offset: 0x00033564
		private bool IsRecentBehavior
		{
			get
			{
				return base.Context.RequestUri.AbsolutePath.ToLower() == "/Aliww".ToLower();
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00035398 File Offset: 0x00033598
		protected override void OnMessage(MessageEventArgs e)
		{
			BehaviorBase.a a = new BehaviorBase.a();
			a.b = this;
			a.c = e;
			if (!string.IsNullOrEmpty(this.UserNick))
			{
				a.a = null;
				try
				{
					a.a = JSON.Decode(a.c.Data) as Hashtable;
				}
				catch
				{
				}
				if (a.a == null)
				{
					LogWriter.WriteLog("ReceivedFromQnMsg：" + a.c.Data, 1);
				}
				else if (a.a["type"] != null)
				{
					Task.Run(new Action(a.d));
				}
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00035454 File Offset: 0x00033654
		protected void ReceivedMsg(MessageEventArgs e, Hashtable hs)
		{
			BehaviorBase.b b = new BehaviorBase.b();
			b.b = this;
			string text = (this.IsRecentBehavior ? "recent" : "aldsqn");
			AppConfig.WriteLog(string.Concat(new string[] { "userNick：", this.UserNick, ", msg：", e.Data, ", msgRecvClient：", text }), LogType.LogReceivedMsg, 1);
			if (hs["msg"] != null)
			{
				Hashtable hashtable = hs["msg"] as Hashtable;
				RecvMsgResponse recvMsgResponse = new RecvMsgResponse();
				recvMsgResponse.Type = Util.ToInt(hashtable["type"]);
				object obj = hashtable["fromuid"];
				recvMsgResponse.FromUid = ((obj != null) ? obj.ToString() : null);
				object obj2 = hashtable["nick"];
				recvMsgResponse.FromNick = ((obj2 != null) ? obj2.ToString() : null);
				object obj3 = hashtable["securityUID"];
				recvMsgResponse.SecurityUID = ((obj3 != null) ? obj3.ToString() : null);
				object obj4 = hashtable["message"];
				recvMsgResponse.Msg = ((obj4 != null) ? obj4.ToString() : null);
				recvMsgResponse.TimeStamp = Util.ToLong(hashtable["time"]);
				recvMsgResponse.Offline = Util.ToBoolean(hashtable["offline"]);
				MsgFrom msgFrom;
				if (!this.IsRecentBehavior)
				{
					msgFrom = MsgFrom.FromAldsQn;
				}
				else
				{
					object obj5 = hashtable["from"];
					msgFrom = ((((obj5 != null) ? obj5.ToString() : null) == "GetNewMsg") ? MsgFrom.FromRecentGetNewMsg : MsgFrom.FromRecentRegMsg);
				}
				recvMsgResponse.MsgFrom = msgFrom;
				RecvMsgResponse recvMsgResponse2 = recvMsgResponse;
				if (!(Util.GetMasterNick(this.AccountUserNick) == Util.GetMasterNick(recvMsgResponse2.FromNick)))
				{
					UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(this.AccountUserNick);
					MsgFrom msgFrom2 = recvMsgResponse2.MsgFrom;
					MsgFrom msgFrom3 = msgFrom2;
					if (msgFrom3 - MsgFrom.FromRecentRegMsg > 1)
					{
						if (msgFrom3 == MsgFrom.FromAldsQn)
						{
							if (!string.IsNullOrEmpty(recvMsgResponse2.FromNick) && !recvMsgResponse2.FromNick.Contains("**"))
							{
								AppConfig.BuyerInfoCache.UpdateAldsOpenUid(recvMsgResponse2.FromNick, recvMsgResponse2.SecurityUID);
							}
							else
							{
								string displayNick = userCacheOrCreate.AldsSession.GetDisplayNick(recvMsgResponse2.FromUid, recvMsgResponse2.SecurityUID);
								AppConfig.BuyerInfoCache.UpdateAldsOpenUid(displayNick, recvMsgResponse2.SecurityUID);
							}
						}
					}
					else if (!string.IsNullOrEmpty(recvMsgResponse2.FromNick) && !recvMsgResponse2.FromNick.Contains("**"))
					{
						AppConfig.BuyerInfoCache.UpdateRecentOpenUid(recvMsgResponse2.FromNick, recvMsgResponse2.SecurityUID);
					}
					else
					{
						string displayNick2 = userCacheOrCreate.RecentSession.GetDisplayNick(recvMsgResponse2.FromUid, recvMsgResponse2.SecurityUID);
						AppConfig.BuyerInfoCache.UpdateRecentOpenUid(displayNick2, recvMsgResponse2.SecurityUID);
					}
					b.a = this.AccountUserNick + ((recvMsgResponse2.TimeStamp > 10000000000L) ? (recvMsgResponse2.TimeStamp / 1000L) : recvMsgResponse2.TimeStamp).ToString() + recvMsgResponse2.FromUid;
					bool flag;
					if (recvMsgResponse2.MsgFrom != MsgFrom.FromRecentRegMsg)
					{
						if (recvMsgResponse2.MsgFrom != MsgFrom.FromAldsQn)
						{
							flag = false;
							goto IL_0319;
						}
					}
					flag = string.IsNullOrEmpty(recvMsgResponse2.Msg);
					IL_0319:
					if (flag)
					{
						Thread.Sleep(500);
						if (BehaviorBase.i.ContainsKey(b.a) || !BehaviorBase.j.TryAdd(b.a, ""))
						{
							return;
						}
						Task.Run(new Func<Task>(b.d));
					}
					else
					{
						if (BehaviorBase.i.TryAdd(b.a, ""))
						{
							Task.Run(new Func<Task>(b.c));
						}
						if (recvMsgResponse2.MsgFrom == MsgFrom.FromRecentGetNewMsg)
						{
							userCacheOrCreate.DictRecentGetNewMsgLastRecvTime[recvMsgResponse2.SecurityUID] = DateTime.Now;
						}
					}
					if (string.IsNullOrEmpty(recvMsgResponse2.Msg) || !AppConfig.IsQnSystemMsg(recvMsgResponse2.Msg))
					{
						AliwwClientMode aliwwClientMode = AppConfig.AliwwClientMode;
						AliwwClientMode aliwwClientMode2 = aliwwClientMode;
						if (aliwwClientMode2 != AliwwClientMode.机器人模式)
						{
							if (aliwwClientMode2 != AliwwClientMode.自挂模式)
							{
								if (aliwwClientMode2 == AliwwClientMode.代挂模式)
								{
									if (recvMsgResponse2.MsgFrom != MsgFrom.FromAldsQn && (DateTime.Now - recvMsgResponse2.SentTime).TotalSeconds < 30.0)
									{
										string masterNick = Util.GetMasterNick(this.AccountUserNick);
										AldsAccountInfo agentAccountInfo = AppConfig.GetAgentAccountInfo(masterNick);
										if (agentAccountInfo != null)
										{
											if ((!string.IsNullOrEmpty(recvMsgResponse2.FromNick) && recvMsgResponse2.FromNick.Contains(":")) || (!string.IsNullOrEmpty(recvMsgResponse2.FromUid) && recvMsgResponse2.FromUid.Contains(":")))
											{
												k.a().WriteLine(string.Concat(new string[] { "不进行智能答复，", this.AccountUserNick, "，买家：", recvMsgResponse2.FromNick, "，商家子账号无法进行智能答复" }));
											}
											else if (agentAccountInfo.DisableTransfer)
											{
												AliwwMessageInfo amiThreadProcessing = Form1.AmiThreadProcessing;
												if (((amiThreadProcessing != null) ? amiThreadProcessing.UserNick : null) != this.AccountUserNick)
												{
													AliwwTalkWindowQn aliwwTalkWindowQn = AliwwTalkWindowQn.Get(this.AccountUserNick);
													if (aliwwTalkWindowQn != null)
													{
														AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(aliwwTalkWindowQn);
														aliwwTalkWindow.AliVersion = aliwwTalkWindowQn.AliwwVersion;
														aliwwTalkWindow.CloseCurrentChat();
													}
												}
												k.a().WriteLine("不进行智能答复，" + this.AccountUserNick + "，转接开关已关闭");
											}
											else if (string.IsNullOrWhiteSpace(agentAccountInfo.TransferMessage))
											{
												string transferCallDutyManualNick = AppConfig.GetTransferCallDutyManualNick(agentAccountInfo, recvMsgResponse2.FromNick, recvMsgResponse2.SecurityUID);
												AppConfig.WriteLog("userNick：" + this.AccountUserNick + ", transferNick：" + transferCallDutyManualNick, LogType.LogReceivedMsg, 1);
												this.TransferContact(recvMsgResponse2.FromNick, "cntaobao", recvMsgResponse2.SecurityUID, transferCallDutyManualNick, "cntaobao");
											}
											else
											{
												SellerCache sellerExecuteCache = AppConfig.GetSellerExecuteCache(masterNick);
												if (string.IsNullOrEmpty(recvMsgResponse2.Msg) && recvMsgResponse2.MsgFrom == MsgFrom.FromRecentRegMsg)
												{
													AliwwMessageInfo aliwwMessageInfo = new AliwwMessageInfo
													{
														MsgId = AppConfig.GetRandomMsgId(),
														BuyerNick = recvMsgResponse2.FromNick,
														BuyerOpenUid = recvMsgResponse2.SecurityUID,
														AliwwMessageSourceType = EnumAliwwMessageSource.FromCallUserOnly,
														MessageBody = "",
														MessageTitle = "【转接前回复消息】",
														SellerNick = masterNick,
														UserNick = this.AccountUserNick,
														EnqueueTime = DateTime.Now,
														CreateTime = DateTime.Now
													};
													sellerExecuteCache.AliwwMsgQueue.Enqueue(aliwwMessageInfo);
												}
												else
												{
													AliwwMessageInfo aliwwMessageInfo2 = new AliwwMessageInfo
													{
														MsgId = AppConfig.GetRandomMsgId(),
														BuyerNick = recvMsgResponse2.FromNick,
														BuyerOpenUid = recvMsgResponse2.SecurityUID,
														AliwwMessageSourceType = EnumAliwwMessageSource.FromTransferMsg,
														MessageBody = agentAccountInfo.TransferMessage,
														MessageTitle = "【转接前回复消息】",
														SellerNick = masterNick,
														UserNick = this.AccountUserNick,
														EnqueueTime = DateTime.Now,
														CreateTime = DateTime.Now
													};
													sellerExecuteCache.AliwwMsgQueue.Enqueue(aliwwMessageInfo2);
												}
											}
										}
									}
								}
							}
							else if (AppConfig.UserDict.ContainsKey(this.AccountUserNick) && AppConfig.UserDict[this.AccountUserNick].AutoReply)
							{
								if (string.IsNullOrEmpty(recvMsgResponse2.Msg) && recvMsgResponse2.MsgFrom == MsgFrom.FromAldsQn && userCacheOrCreate.RecentSession == null)
								{
									string config = QnUserDbManager.GetConfig("usenativemessagelist", this.UserId);
									if (config == "false")
									{
										AldsAccountInfo aldsAccountInfo = AppConfig.UserList.Where(new Func<AldsAccountInfo, bool>(b.c)).FirstOrDefault<AldsAccountInfo>();
										if (aldsAccountInfo != null)
										{
											AutoReplyCollection autoReplyCollection = AutoReplyCollection.Load(AppConfig.AutoReplyList, aldsAccountInfo.UserNick, AppConfig.CurrentSystemSettingInfo.AutoReplyBySellerNick, "【适用所有已添加的旺旺】");
											bool flag2;
											if (autoReplyCollection == null)
											{
												flag2 = false;
											}
											else
											{
												flag2 = autoReplyCollection.Any(new Func<AutoReplyInfo, bool>(BehaviorBase.<>c.<>9.a));
											}
											if (flag2)
											{
												if (!FormTipRestart.HasReplyInstanceShow && !FormTipRestart.ReplyInstance.Visible)
												{
													FormTipRestart.HasReplyInstanceShow = true;
													Task.Run<DialogResult>(new Func<DialogResult>(BehaviorBase.<>c.<>9.a));
												}
												k.a().WriteLine("智能答复匹配有误，请重启下千牛：" + this.UserNick + "！");
												return;
											}
										}
									}
								}
								this.a(recvMsgResponse2);
							}
						}
						else
						{
							AppConfig.GetUserCacheOrCreate(this.AccountUserNick).RecvMsgQueue.Enqueue(recvMsgResponse2);
						}
					}
				}
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00035CE0 File Offset: 0x00033EE0
		protected void GetToUserNick(Hashtable hs)
		{
			Hashtable hashtable = hs["msg"] as Hashtable;
			if (hashtable != null)
			{
				string text = Convert.ToString(hashtable["guid"]);
				if (text != null && this.DictWebSocketObj.ContainsKey(text))
				{
					this.DictWebSocketObj[text] = new ActiveUserInfo
					{
						Uid = Convert.ToString(hashtable["toUserNick"]),
						SecurityUID = Convert.ToString(hashtable["securityUID"]),
						BizDomain = Convert.ToString(hashtable["bizDomain"])
					};
				}
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00035D84 File Offset: 0x00033F84
		protected void GetHtmlLength(Hashtable hs)
		{
			Hashtable hashtable = hs["msg"] as Hashtable;
			if (hashtable != null)
			{
				string text = Convert.ToString(hashtable["guid"]);
				if (text != null && this.DictWebSocketInt.ContainsKey(text))
				{
					this.DictWebSocketInt[text] = Util.ToInt(hashtable["htmlLength"]);
				}
			}
		}

		// Token: 0x06000332 RID: 818
		protected abstract void Beat();

		// Token: 0x06000333 RID: 819 RVA: 0x00035DEC File Offset: 0x00033FEC
		protected void GetHtml(Hashtable hs)
		{
			Hashtable hashtable = hs["msg"] as Hashtable;
			if (hashtable != null)
			{
				string text = Convert.ToString(hashtable["guid"]);
				if (text != null && this.DictWebSocketString.ContainsKey(text))
				{
					this.DictWebSocketString[text] = Convert.ToString(hashtable["html"]);
				}
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00035E54 File Offset: 0x00034054
		protected void OpenDsrTaskHtml(Hashtable hs)
		{
			BehaviorBase.c c = new BehaviorBase.c();
			c.b = this;
			c.a = hs["msg"] as Hashtable;
			if (c.a != null)
			{
				Task.Run(new Action(c.c));
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00035EA4 File Offset: 0x000340A4
		protected void HandleDsrRateUrlHtml(Hashtable webHtmlObj, string userNick, out string errMsg)
		{
			errMsg = "";
			AliwwWorkBenchQn aliwwWorkBenchQn = null;
			string text = Util.Trim(webHtmlObj["dsrSellerNick"]);
			string text2 = Util.Trim(webHtmlObj["dsrUrl"]);
			long num = Util.ToLong(webHtmlObj["numIid"]);
			string text3 = Util.Trim(webHtmlObj["title"]);
			string text4 = Util.Trim(webHtmlObj["sellerType"]);
			long num2 = Util.ToLong(webHtmlObj["shopId"]);
			for (int i = 0; i < 10; i++)
			{
				WindowInfo windowInfo = Win32Extend.FindWindowByClassAndName("#32770", "设置打开方式");
				if (windowInfo != null && windowInfo.HWnd != IntPtr.Zero)
				{
					WindowInfo windowInfo2 = windowInfo.FindWindowInDescendant("StandardButton", "确定", false, new bool?(false));
					if (windowInfo2 != null && windowInfo2.HWnd != IntPtr.Zero)
					{
						windowInfo2.Click(true);
					}
				}
				aliwwWorkBenchQn = AliwwWorkBenchQn.Get(userNick);
				if (aliwwWorkBenchQn != null)
				{
					break;
				}
				Thread.Sleep(200);
			}
			if (aliwwWorkBenchQn == null)
			{
				errMsg = "打开评价网页失败，未找到" + userNick + "的千牛首页";
			}
			else
			{
				RateStatisticalDayReport rateStatisticalDayReport = new RateStatisticalDayReport
				{
					SellerNick = text,
					Days = DateTime.Today.AddDays(-1.0)
				};
				for (int j = 0; j < 10; j++)
				{
					WindowInfo windowInfo3 = Win32Extend.FindWindowByClassAndName("#32770", "提示");
					if (windowInfo3 != null && windowInfo3.HWnd != IntPtr.Zero)
					{
						WindowInfo windowInfo4 = windowInfo3.FindWindowInDescendant("StandardButton", "取消", false, new bool?(false));
						windowInfo4.Click(true);
					}
					string html = aliwwWorkBenchQn.GetHtml(text2, null);
					if (string.IsNullOrEmpty(html))
					{
						errMsg = text + "获取的评价html为空";
						Thread.Sleep(300);
					}
					else
					{
						DsrManager dsrManager = new DsrManager(text, num, text3, text2, text4, num2);
						if (dsrManager.HandlingRateHtml(html, out rateStatisticalDayReport, out errMsg))
						{
							break;
						}
						Thread.Sleep(300);
					}
				}
				try
				{
					if (!string.IsNullOrEmpty(errMsg))
					{
						LogWriter.WriteLog(errMsg, 1);
					}
					AppConfig.QnAgentServiceClient.DsrReport(rateStatisticalDayReport, text, text2, errMsg, num2);
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog(ex.ToString(), 1);
				}
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0003611C File Offset: 0x0003431C
		protected void HandleDsrItemUrlHtml(Hashtable webHtmlObj, string userNick, out string rateUrl, out long shopId, out string errMsg)
		{
			rateUrl = "";
			shopId = 0L;
			errMsg = "";
			AliwwWorkBenchQn aliwwWorkBenchQn = null;
			string text = Util.Trim(webHtmlObj["dsrSellerNick"]);
			string text2 = Util.Trim(webHtmlObj["dsrUrl"]);
			long num = Util.ToLong(webHtmlObj["numIid"]);
			string text3 = Util.Trim(webHtmlObj["title"]);
			string text4 = Util.Trim(webHtmlObj["sellerType"]);
			long num2 = Util.ToLong(webHtmlObj["shopId"]);
			for (int i = 0; i < 10; i++)
			{
				WindowInfo windowInfo = Win32Extend.FindWindowByClassAndName("#32770", "设置打开方式");
				if (windowInfo != null && windowInfo.HWnd != IntPtr.Zero)
				{
					WindowInfo windowInfo2 = windowInfo.FindWindowInDescendant("StandardButton", "确定", false, new bool?(false));
					if (windowInfo2 != null && windowInfo2.HWnd != IntPtr.Zero)
					{
						windowInfo2.Click(true);
					}
				}
				aliwwWorkBenchQn = AliwwWorkBenchQn.Get(userNick);
				if (aliwwWorkBenchQn != null)
				{
					break;
				}
				Thread.Sleep(200);
			}
			if (aliwwWorkBenchQn == null)
			{
				errMsg = "打开宝贝网页失败，未找到" + userNick + "的千牛首页";
			}
			else
			{
				DsrManager dsrManager = new DsrManager(text, num, text3, text2, text4, num2);
				for (int j = 0; j < 10; j++)
				{
					WindowInfo windowInfo3 = Win32Extend.FindWindowByClassAndName("#32770", "提示");
					if (windowInfo3 != null && windowInfo3.HWnd != IntPtr.Zero)
					{
						WindowInfo windowInfo4 = windowInfo3.FindWindowInDescendant("StandardButton", "取消", false, new bool?(false));
						windowInfo4.Click(true);
					}
					string html = aliwwWorkBenchQn.GetHtml(text2, null);
					if (string.IsNullOrEmpty(html))
					{
						errMsg = text + "获取的宝贝html为空";
						Thread.Sleep(300);
					}
					else if (!dsrManager.HandlingItemHtml(html, out errMsg))
					{
						Thread.Sleep(300);
					}
					else
					{
						if (!errMsg.Contains("已下架"))
						{
							rateUrl = dsrManager.DsrUrl;
							shopId = dsrManager.ShopId;
							break;
						}
						break;
					}
				}
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00036354 File Offset: 0x00034554
		protected void BackMsg(Hashtable hs)
		{
			string text = Util.Trim(hs["guid"]);
			string text2 = Util.Trim(hs["msg"]);
			this.DictWebSocketString[text] = text2;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00036394 File Offset: 0x00034594
		protected void AutoLogin(Hashtable hs)
		{
			string text = Convert.ToString(hs["userNick"]);
			if (string.IsNullOrEmpty(text))
			{
				text = this.AccountUserNick;
			}
			if (AppConfig.UserDict.ContainsKey(text))
			{
				AldsAccountInfo aldsAccountInfo = AppConfig.UserDict[text];
				if (aldsAccountInfo.IsValid)
				{
					this.SendLoginState(true);
					return;
				}
			}
			string text2 = Convert.ToString(hs["password"]);
			if (string.IsNullOrEmpty(text2))
			{
				this.SendAutoLoginResult("获取的私钥为空，请刷新页面再试！");
			}
			else
			{
				LoginResponse loginResponse = null;
				try
				{
					loginResponse = AppConfig.WwServiceClient.Login(text, text2);
				}
				catch (Exception ex)
				{
					this.SendAutoLoginResult(ex.Message);
					return;
				}
				if (loginResponse.IsError)
				{
					if (loginResponse.ErrMsg.Contains("密码错误"))
					{
						this.SendAutoLoginResult("登录失败，请刷新页面再试！");
					}
					else
					{
						this.SendAutoLoginResult(loginResponse.ErrMsg);
					}
				}
				else
				{
					AliwwClientAccount account = loginResponse.Account;
					if (!AppConfig.UserDict.ContainsKey(account.UserNick))
					{
						BehaviorBase.d d = new BehaviorBase.d();
						d.a = new AldsAccountInfo();
						d.a.Map(account);
						d.a.Password = text2;
						d.a.AutoSendOnOff = true;
						d.a.QnConnectionStatus = "√";
						if (AppConfig.UserDict.TryAdd(text, d.a))
						{
							AldsAccountManager.Insert(d.a);
							k.a().Invoke(new Action(d.b));
						}
					}
					else
					{
						AldsAccountInfo aldsAccountInfo2 = AppConfig.UserDict[account.UserNick];
						aldsAccountInfo2.Map(account);
						aldsAccountInfo2.Password = text2;
						aldsAccountInfo2.AutoSendOnOff = true;
						aldsAccountInfo2.QnConnectionStatus = "√";
						AldsAccountManager.UpdatePassword(aldsAccountInfo2);
					}
					WebSocketClient wwWebSocketClient = AppConfig.WwWebSocketClient;
					if (wwWebSocketClient != null)
					{
						wwWebSocketClient.AddOnlineUser(text, text2, AppConfig.ApplicationUuid);
					}
					this.SendLoginState(true);
				}
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00036594 File Offset: 0x00034794
		protected void CancelLogin()
		{
			try
			{
				k.a().DeleteAldsAccount(this.AccountUserNick);
				this.SendCancelLoginResult("");
			}
			catch (Exception ex)
			{
				this.SendCancelLoginResult(ex.Message);
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000365E0 File Offset: 0x000347E0
		protected void ClickEnter()
		{
			AliwwTalkWindowQn aliwwTalkWindowQn = AliwwTalkWindowQn.Get(this.AccountUserNick);
			if (aliwwTalkWindowQn != null)
			{
				AliwwTalkWindow aliwwTalkWindow = aliwwTalkWindowQn.Convert<AliwwTalkWindow>();
				aliwwTalkWindow.PressSendButtonToMessageBox();
			}
		}

		// Token: 0x0600033B RID: 827
		public abstract bool SendTo(string data);

		// Token: 0x0600033C RID: 828 RVA: 0x00003220 File Offset: 0x00001420
		public void SendAutoLoginResult(string msg)
		{
			this.SendTo("{\"type\": \"autoLoginResult\", \"msg\": \"" + msg + "\"}");
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0003660C File Offset: 0x0003480C
		public void SendLoginState(bool isLogin)
		{
			this.SendTo("{\"type\": \"loginState\", \"isLogin\": " + (isLogin ? 1 : 0).ToString() + "}");
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000323B File Offset: 0x0000143B
		public void SendCancelLoginResult(string msg)
		{
			this.SendTo("{\"type\": \"cancelLoginResult\", \"msg\": \"" + msg + "\"}");
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00036640 File Offset: 0x00034840
		public bool SendGetToUserNick(string guid)
		{
			return this.SendTo("{\"type\":\"getToUserNick\", \"msg\": \"" + guid + "\"}");
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00003256 File Offset: 0x00001456
		public void SendGetHtml(string guid)
		{
			this.SendTo("{\"type\":\"getHtml\", \"msg\": \"" + guid + "\"}");
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00036668 File Offset: 0x00034868
		public bool SendTransfer(string contactNick, string contactSiteId, string contactOpenUid, string transferNick, string transferNickSiteId, string guid = "")
		{
			bool flag;
			if (transferNick.Equals(this.AccountUserNick))
			{
				flag = false;
			}
			else
			{
				contactOpenUid = contactOpenUid ?? "";
				Hashtable hashtable = new Hashtable();
				hashtable["type"] = "transfer";
				hashtable["msg"] = new Hashtable
				{
					{
						"transferUid",
						transferNickSiteId + transferNick
					},
					{
						"buyerUid",
						contactSiteId + contactNick
					},
					{ "contactSecurityUID", contactOpenUid },
					{ "contactBizDomain", "taobao" },
					{ "guid", guid }
				};
				flag = this.SendTo(JSON.Encode(hashtable));
			}
			return flag;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00036724 File Offset: 0x00034924
		public void CloseSession()
		{
			try
			{
				base.Close();
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0003675C File Offset: 0x0003495C
		private void a(RecvMsgResponse A_0)
		{
			try
			{
				string fromNick = A_0.FromNick;
				string text = A_0.FromUid.Replace(A_0.FromNick, "");
				string msg = A_0.Msg;
				Util.GetMasterNick(this.AccountUserNick);
				AldsAccountInfo aldsAccountInfo = AppConfig.UserList.Where(new Func<AldsAccountInfo, bool>(this.a)).FirstOrDefault<AldsAccountInfo>();
				if (aldsAccountInfo != null)
				{
					SortedDictionary<ArActionType, List<object>> sortedDictionary = null;
					AutoReplyInfo autoReplyInfo = null;
					AutoReplyInfo autoReplyInfo2 = null;
					UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(this.AccountUserNick);
					bool flag;
					if (A_0.MsgFrom != MsgFrom.FromRecentRegMsg)
					{
						if (A_0.MsgFrom != MsgFrom.FromAldsQn)
						{
							flag = false;
							goto IL_0096;
						}
					}
					flag = string.IsNullOrEmpty(A_0.Msg);
					IL_0096:
					SortedDictionary<ArActionType, List<object>> sortedDictionary3;
					SortedDictionary<ArActionType, List<object>> sortedDictionary2;
					if (flag)
					{
						AutoReplyCollection autoReplyCollection = AutoReplyCollection.Load(AppConfig.AutoReplyList, aldsAccountInfo.UserNick, AppConfig.CurrentSystemSettingInfo.AutoReplyBySellerNick, "【适用所有已添加的旺旺】");
						if (Util.IsEmptyList<AutoReplyInfo>(autoReplyCollection) || autoReplyCollection.Count != 1 || autoReplyCollection[0].Type != EnumArType.NoMatching)
						{
							userCacheOrCreate.RecvMsgQueue.Enqueue(A_0);
							return;
						}
						autoReplyInfo2 = autoReplyCollection[0];
						sortedDictionary2 = (sortedDictionary3 = autoReplyInfo2.Explain(aldsAccountInfo));
					}
					else
					{
						string text2;
						bool flag2;
						AppConfig.GetMatchReplyInfo(aldsAccountInfo, msg, fromNick, A_0.SecurityUID, A_0.SentTime, out text2, out autoReplyInfo, out autoReplyInfo2, out flag2);
						if (!string.IsNullOrEmpty(text2))
						{
							k.a().WriteLine(string.Concat(new string[] { aldsAccountInfo.UserNick, "\t", A_0.FromNick, "\t", text2 }));
							if (flag2)
							{
								return;
							}
						}
						sortedDictionary = ((autoReplyInfo != null) ? autoReplyInfo.Explain(aldsAccountInfo) : null);
						sortedDictionary2 = ((autoReplyInfo2 != null) ? autoReplyInfo2.Explain(aldsAccountInfo) : null);
						sortedDictionary3 = Util.Merge(new SortedDictionary<ArActionType, List<object>>[] { sortedDictionary, sortedDictionary2 });
					}
					if (sortedDictionary3 == null)
					{
						k.a().WriteLine(aldsAccountInfo.UserNick + "\t" + A_0.FromNick + "\t未匹配到答复语");
					}
					else if (sortedDictionary3.Any(new Func<KeyValuePair<ArActionType, List<object>>, bool>(BehaviorBase.<>c.<>9.a)))
					{
						userCacheOrCreate.RecvMsgQueue.Enqueue(A_0);
					}
					else
					{
						string text3;
						AppConfig.HandleSameQuery(aldsAccountInfo, msg, fromNick, (autoReplyInfo2 != null) ? autoReplyInfo2.ReplyWord : null, out text3);
						if (!string.IsNullOrEmpty(text3))
						{
							k.a().WriteLine(string.Concat(new string[] { aldsAccountInfo.UserNick, "\t", A_0.FromNick, "\t", text3 }));
						}
						else
						{
							if (!Util.IsEmptyList<KeyValuePair<ArActionType, List<object>>>(sortedDictionary))
							{
								LogFirstReplyManager.Insert(aldsAccountInfo.UserNick, fromNick, text + fromNick, A_0.SecurityUID, msg, A_0.SentTime, autoReplyInfo.ReplyWord);
							}
							if (!Util.IsEmptyList<KeyValuePair<ArActionType, List<object>>>(sortedDictionary2))
							{
								LogAutoReplyManager.Insert(aldsAccountInfo.UserNick, fromNick, text + fromNick, A_0.SecurityUID, msg, A_0.SentTime, autoReplyInfo2, A_0.MsgFrom, DateTime.Now);
							}
							foreach (KeyValuePair<ArActionType, List<object>> keyValuePair in sortedDictionary3)
							{
								Dictionary<string, DateTime> dictBuyerNickTransferTime = userCacheOrCreate.DictBuyerNickTransferTime;
								if (AppConfig.CurrentSystemSettingInfo.TransferInterval > 0 && dictBuyerNickTransferTime.ContainsKey(fromNick) && (DateTime.Now - dictBuyerNickTransferTime[fromNick]).TotalSeconds <= (double)AppConfig.CurrentSystemSettingInfo.TransferInterval)
								{
									k.a().WriteLine(string.Format("{0}\t{1}\t转接失败，{2}时间内不再重复转接", this.AccountUserNick, A_0.FromNick, AppConfig.CurrentSystemSettingInfo.TransferInterval));
									break;
								}
								string text4 = ((keyValuePair.Key == ArActionType.AppointTransferCall) ? ((string)keyValuePair.Value[0]) : AppConfig.GetTransferCallDutyManualNick(aldsAccountInfo, fromNick, A_0.SecurityUID));
								if (string.IsNullOrEmpty(text4))
								{
									if (string.IsNullOrEmpty(aldsAccountInfo.NotDutyNickReplyMsg))
									{
										k.a().WriteLine(this.AccountUserNick + "\t" + A_0.FromNick + "\t转接失败，转接人为空");
									}
									else
									{
										userCacheOrCreate.RecvMsgQueue.Enqueue(A_0);
									}
									break;
								}
								AliwwMessageInfo amiThreadProcessing = Form1.AmiThreadProcessing;
								if (((amiThreadProcessing != null) ? amiThreadProcessing.BuyerNick : null) == A_0.FromNick)
								{
									userCacheOrCreate.RecvMsgQueue.Enqueue(A_0);
									break;
								}
								string text5 = A_0.FromUid.Replace(A_0.FromNick, "");
								if (this.TransferContact(A_0.FromNick, text5, A_0.SecurityUID, text4, text5))
								{
									userCacheOrCreate.DictBuyerNickTransferTime[fromNick] = DateTime.Now;
								}
								else if (!AppConfig.CurrentSystemSettingInfo.DisableCloseWindowWhenAutoReply)
								{
									AliwwTalkWindowQn aliwwTalkWindowQn = AliwwTalkWindowQn.Get(this.AccountUserNick);
									if (aliwwTalkWindowQn != null)
									{
										AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(aliwwTalkWindowQn);
										aliwwTalkWindow.AliVersion = aliwwTalkWindowQn.AliwwVersion;
										aliwwTalkWindow.CloseCurrentChat();
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000032B2 File Offset: 0x000014B2
		[CompilerGenerated]
		private bool a(AldsAccountInfo A_0)
		{
			return A_0.UserNick.Equals(this.AccountUserNick);
		}

		// Token: 0x040002EB RID: 747
		private string a;

		// Token: 0x040002EC RID: 748
		private long b;

		// Token: 0x040002ED RID: 749
		private string c;

		// Token: 0x040002EE RID: 750
		private string d;

		// Token: 0x040002EF RID: 751
		[CompilerGenerated]
		private string e;

		// Token: 0x040002F0 RID: 752
		[CompilerGenerated]
		private readonly ConcurrentDictionary<string, string> f;

		// Token: 0x040002F1 RID: 753
		[CompilerGenerated]
		private readonly ConcurrentDictionary<string, object> g;

		// Token: 0x040002F2 RID: 754
		[CompilerGenerated]
		private readonly ConcurrentDictionary<string, int> h;

		// Token: 0x040002F3 RID: 755
		[CompilerGenerated]
		private static readonly ConcurrentDictionary<string, string> i = new ConcurrentDictionary<string, string>();

		// Token: 0x040002F4 RID: 756
		[CompilerGenerated]
		private static readonly ConcurrentDictionary<string, string> j = new ConcurrentDictionary<string, string>();

		// Token: 0x0200006E RID: 110
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x0600034D RID: 845 RVA: 0x00036CB8 File Offset: 0x00034EB8
			internal void d()
			{
				try
				{
					string text = this.a["type"].ToString();
					string text2 = text;
					uint num = p.a(text2);
					if (num <= 2421315013U)
					{
						if (num <= 411440346U)
						{
							if (num <= 366443288U)
							{
								if (num != 126112952U)
								{
									if (num == 366443288U)
									{
										if (text2 == "cancelLogin")
										{
											this.b.CancelLogin();
										}
									}
								}
								else if (text2 == "setTextByApiResponse" && this.a["guid"] != null)
								{
									this.b.DictWebSocketInt[this.a["guid"].ToString()] = (Util.ToBoolean(this.a["isSuccess"]) ? 1 : (-1));
								}
							}
							else if (num != 408273166U)
							{
								if (num == 411440346U)
								{
									if (text2 == "qnVersion")
									{
										BehaviorBase behaviorBase = this.b;
										object obj = this.a["version"];
										behaviorBase.QnVersion = ((obj != null) ? obj.ToString() : null);
									}
								}
							}
							else if (text2 == "getHtmlLength")
							{
								this.b.GetHtmlLength(this.a);
							}
						}
						else if (num <= 657055850U)
						{
							if (num != 648559207U)
							{
								if (num == 657055850U)
								{
									if (text2 == "openDsrTaskHtml")
									{
										this.b.OpenDsrTaskHtml(this.a);
									}
								}
							}
							else if (text2 == "receivedMsg")
							{
								this.b.ReceivedMsg(this.c, this.a);
							}
						}
						else if (num != 2284091735U)
						{
							if (num == 2421315013U)
							{
								if (text2 == "beat")
								{
									this.b.Beat();
								}
							}
						}
						else if (text2 == "transferRsp")
						{
							object obj2 = this.a["errorMsg"];
							string text3 = ((obj2 != null) ? obj2.ToString() : null);
							this.b.DictWebSocketString[this.a["guid"].ToString()] = (string.IsNullOrEmpty(text3) ? "" : text3);
						}
					}
					else if (num <= 2836982291U)
					{
						if (num <= 2672852315U)
						{
							if (num != 2573467991U)
							{
								if (num == 2672852315U)
								{
									if (text2 == "clickEnter")
									{
										this.b.ClickEnter();
									}
								}
							}
							else if (text2 == "backMsg")
							{
								this.b.BackMsg(this.a);
							}
						}
						else if (num != 2781784282U)
						{
							if (num == 2836982291U)
							{
								if (text2 == "autoLogin")
								{
									this.b.AutoLogin(this.a);
								}
							}
						}
						else if (text2 == "sendMsgSucc")
						{
							UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(this.b.AccountUserNick);
							userCacheOrCreate.LastSendMsgSuccDateTime = new DateTime?(DateTime.Now);
						}
					}
					else if (num <= 3286248437U)
					{
						if (num != 3240340334U)
						{
							if (num == 3286248437U)
							{
								if (!(text2 == "backMsgToServer"))
								{
								}
							}
						}
						else if (text2 == "getToUserNick")
						{
							this.b.GetToUserNick(this.a);
						}
					}
					else if (num != 3478694047U)
					{
						if (num != 3750239498U)
						{
							if (num == 3895753178U)
							{
								if (text2 == "closeCurrentChat")
								{
									AliwwTalkWindowQn aliwwTalkWindowQn = AliwwTalkWindowQn.Get(this.b.AccountUserNick);
									if (aliwwTalkWindowQn != null)
									{
										AliwwTalkWindow aliwwTalkWindow = aliwwTalkWindowQn.Convert<AliwwTalkWindow>();
										aliwwTalkWindow.CloseCurrentChat();
									}
								}
							}
						}
						else if (text2 == "getHtml")
						{
							this.b.GetHtml(this.a);
						}
					}
					else if (text2 == "displayNickRsp")
					{
						ConcurrentDictionary<string, string> dictWebSocketString = this.b.DictWebSocketString;
						string text4 = this.a["guid"].ToString();
						object obj3 = this.a["dnickFromSecurityUIDs"];
						string text5;
						if (obj3 != null)
						{
							if ((text5 = obj3.ToString()) != null)
							{
								goto IL_048E;
							}
						}
						text5 = "";
						IL_048E:
						dictWebSocketString[text4] = text5;
					}
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog(ex.ToString(), 1);
				}
			}

			// Token: 0x040002F9 RID: 761
			public Hashtable a;

			// Token: 0x040002FA RID: 762
			public BehaviorBase b;

			// Token: 0x040002FB RID: 763
			public MessageEventArgs c;
		}

		// Token: 0x0200006F RID: 111
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x0600034F RID: 847 RVA: 0x00037188 File Offset: 0x00035388
			internal Task d()
			{
				BehaviorBase.b.a a = new BehaviorBase.b.a();
				a.b = AsyncTaskMethodBuilder.Create();
				a.c = this;
				a.a = -1;
				a.b.Start<BehaviorBase.b.a>(ref a);
				return a.b.Task;
			}

			// Token: 0x06000350 RID: 848 RVA: 0x000371CC File Offset: 0x000353CC
			internal Task c()
			{
				BehaviorBase.b.b b = new BehaviorBase.b.b();
				b.b = AsyncTaskMethodBuilder.Create();
				b.c = this;
				b.a = -1;
				b.b.Start<BehaviorBase.b.b>(ref b);
				return b.b.Task;
			}

			// Token: 0x06000351 RID: 849 RVA: 0x00003324 File Offset: 0x00001524
			internal bool c(AldsAccountInfo A_0)
			{
				return A_0.UserNick.Equals(this.b.AccountUserNick);
			}

			// Token: 0x040002FC RID: 764
			public string a;

			// Token: 0x040002FD RID: 765
			public BehaviorBase b;

			// Token: 0x02000070 RID: 112
			private sealed class a : IAsyncStateMachine
			{
				// Token: 0x06000353 RID: 851 RVA: 0x00037210 File Offset: 0x00035410
				void IAsyncStateMachine.d()
				{
					/*
An exception occurred when decompiling this method (06000353)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void AliwwClient.WebSocketServer.BehaviorBase/b/a::d()

 ---> System.ArgumentOutOfRangeException: length ('-1') must be a non-negative value. (Parameter 'length')
Actual value was -1.
   at System.ArgumentOutOfRangeException.ThrowNegative[T](T value, String paramName)
   at System.Array.CopyImpl(Array sourceArray, Int32 sourceIndex, Array destinationArray, Int32 destinationIndex, Int32 length, Boolean reliable)
   at System.Array.Copy(Array sourceArray, Array destinationArray, Int32 length)
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 51
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 403
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 278
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 117
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 88
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 92
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1683
*/;
				}

				// Token: 0x06000354 RID: 852 RVA: 0x000022DD File Offset: 0x000004DD
				void IAsyncStateMachine.d(IAsyncStateMachine A_0)
				{
				}

				// Token: 0x040002FE RID: 766
				public int a;

				// Token: 0x040002FF RID: 767
				public AsyncTaskMethodBuilder b;

				// Token: 0x04000300 RID: 768
				public BehaviorBase.b c;

				// Token: 0x04000301 RID: 769
				private TaskAwaiter d;
			}

			// Token: 0x02000071 RID: 113
			private sealed class b : IAsyncStateMachine
			{
				// Token: 0x06000356 RID: 854 RVA: 0x000372DC File Offset: 0x000354DC
				void IAsyncStateMachine.d()
				{
					/*
An exception occurred when decompiling this method (06000356)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void AliwwClient.WebSocketServer.BehaviorBase/b/b::d()

 ---> System.ArgumentOutOfRangeException: length ('-1') must be a non-negative value. (Parameter 'length')
Actual value was -1.
   at System.ArgumentOutOfRangeException.ThrowNegative[T](T value, String paramName)
   at System.Array.CopyImpl(Array sourceArray, Int32 sourceIndex, Array destinationArray, Int32 destinationIndex, Int32 length, Boolean reliable)
   at System.Array.Copy(Array sourceArray, Array destinationArray, Int32 length)
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 51
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 403
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 278
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 117
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 88
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 92
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1683
*/;
				}

				// Token: 0x06000357 RID: 855 RVA: 0x000022DD File Offset: 0x000004DD
				void IAsyncStateMachine.d(IAsyncStateMachine A_0)
				{
				}

				// Token: 0x04000302 RID: 770
				public int a;

				// Token: 0x04000303 RID: 771
				public AsyncTaskMethodBuilder b;

				// Token: 0x04000304 RID: 772
				public BehaviorBase.b c;

				// Token: 0x04000305 RID: 773
				private TaskAwaiter d;
			}
		}

		// Token: 0x02000072 RID: 114
		[CompilerGenerated]
		private sealed class c
		{
			// Token: 0x06000359 RID: 857 RVA: 0x000373A8 File Offset: 0x000355A8
			internal void c()
			{
				OpenUrlType openUrlType = (OpenUrlType)Util.ToInt(this.a["openUrlType"]);
				OpenUrlType openUrlType2 = openUrlType;
				OpenUrlType openUrlType3 = openUrlType2;
				if (openUrlType3 != OpenUrlType.DsrItemUrl)
				{
					if (openUrlType3 == OpenUrlType.DsrRateUrl)
					{
						string text;
						this.b.HandleDsrRateUrlHtml(this.a, this.b.AccountUserNick, out text);
						UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(this.b.AccountUserNick);
						userCacheOrCreate.QueryDsrComplete = true;
						userCacheOrCreate.QueryDsrErrMsg = text;
					}
				}
				else
				{
					string text2;
					long num;
					string text3;
					this.b.HandleDsrItemUrlHtml(this.a, this.b.AccountUserNick, out text2, out num, out text3);
					try
					{
						string text4 = Util.Trim(this.a["dsrSellerNick"]);
						if (!string.IsNullOrEmpty(text3))
						{
							LogWriter.WriteLog(text3, 1);
						}
						else if (string.IsNullOrEmpty(text3) && string.IsNullOrEmpty(text2))
						{
							RateStatisticalDayReport rateStatisticalDayReport = new RateStatisticalDayReport
							{
								SellerNick = text4,
								Days = DateTime.Today.AddDays(-1.0)
							};
							AppConfig.QnAgentServiceClient.DsrReport(rateStatisticalDayReport, text4, text2, text3, num);
						}
						else
						{
							this.a["url"] = text2;
							this.a["dsrUrl"] = text2;
							this.a["shopId"] = num;
							this.a["openUrlType"] = 2;
							string text5 = "{\"type\":\"openDsrTaskHtml\", \"msg\": " + JSON.Encode(this.a) + "}";
							this.b.Sessions.SendTo(text5, this.b.ID);
						}
					}
					catch (Exception ex)
					{
						LogWriter.WriteLog(ex.ToString(), 1);
					}
				}
			}

			// Token: 0x04000306 RID: 774
			public Hashtable a;

			// Token: 0x04000307 RID: 775
			public BehaviorBase b;
		}

		// Token: 0x02000073 RID: 115
		[CompilerGenerated]
		private sealed class d
		{
			// Token: 0x0600035B RID: 859 RVA: 0x0000333C File Offset: 0x0000153C
			internal void b()
			{
				AppConfig.UserList.Add(this.a);
			}

			// Token: 0x04000308 RID: 776
			public AldsAccountInfo a;
		}
	}
}
