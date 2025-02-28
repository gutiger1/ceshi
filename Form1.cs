using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Windows.Forms;
using Agiso;
using Agiso.AliwwApi;
using Agiso.AliwwApi.Object;
using Agiso.AliwwApi.Qn;
using Agiso.AliwwApi.Wangwang;
using Agiso.DBAccess;
using Agiso.DbManager;
using Agiso.Exceptions;
using Agiso.Extensions;
using Agiso.Handler;
using Agiso.Object;
using Agiso.Utils;
using Agiso.Windows;
using Agiso.WwService.Sdk;
using Agiso.WwService.Sdk.Domain;
using Agiso.WwService.Sdk.Response;
using Agiso.WwWebSocket.Model;
using AliwwClient.Cache;
using AliwwClient.Enums;
using AliwwClient.Manager;
using AliwwClient.Object;
using AliwwClient.Properties;
using AliwwClient.QN.Workbench;
using AliwwClient.Server;
using AliwwClient.WebSocketServer;
using AliwwClient.WebSocketServer.Extensions;
using CsvHelper;

namespace AliwwClient
{
	// Token: 0x0200003F RID: 63
	public partial class Form1 : BaseForm
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00002ACD File Offset: 0x00000CCD
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00002AD5 File Offset: 0x00000CD5
		private global::System.Timers.Timer TimerBeat { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00002ADE File Offset: 0x00000CDE
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00002AE6 File Offset: 0x00000CE6
		private global::System.Timers.Timer TimerMonitorSendThread { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00002AEF File Offset: 0x00000CEF
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00002AF7 File Offset: 0x00000CF7
		private global::System.Timers.Timer TimerAdjustLongOpenThread { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00002B00 File Offset: 0x00000D00
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00002B08 File Offset: 0x00000D08
		private global::System.Timers.Timer TimerReliabilityMonitor { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00002B11 File Offset: 0x00000D11
		public static AliwwMessageInfo AmiThreadProcessing
		{
			get
			{
				return Form1.l;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00002B1A File Offset: 0x00000D1A
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00002B22 File Offset: 0x00000D22
		private DateTime StopGetAgentWwMsgDateTime { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00002B2B File Offset: 0x00000D2B
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00002B33 File Offset: 0x00000D33
		private DateTime _lastSendMailTimeForDequeueTooLazy { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00002B3C File Offset: 0x00000D3C
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00002B44 File Offset: 0x00000D44
		public DateTime? CloseOptionTimeDisableCloseWindowWhenAutoReply { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00002B4D File Offset: 0x00000D4D
		private FormAliwwMsgQueueCount FormAliwwMsgQueueCountInstance
		{
			get
			{
				return FormAliwwMsgQueueCount.FormAliwwMsgQueueCountInstance;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00002B54 File Offset: 0x00000D54
		private FormErrorLog FormErrorLogInstance
		{
			get
			{
				return FormErrorLog.FormErrorLogInstance;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00002B5B File Offset: 0x00000D5B
		private FormAgentSettings FormAgentSettionInstance
		{
			get
			{
				return FormAgentSettings.FormAgentSettionInstance;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00002B62 File Offset: 0x00000D62
		private FormLogAutoReply FormLogAutoReplyInstance
		{
			get
			{
				return FormLogAutoReply.FormLogAutoReplyInstance;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00002B69 File Offset: 0x00000D69
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00002B71 File Offset: 0x00000D71
		public Dictionary<string, DateTime> DictKeepAliveForSomeTime { get; private set; } = new Dictionary<string, DateTime>();

		// Token: 0x0600019C RID: 412 RVA: 0x00021E4C File Offset: 0x0002004C
		private void a(GetAgentAliwwMessageResponse A_0)
		{
			if (A_0.DsrTask != null)
			{
				try
				{
					List<QnAgentInfo> list = AppConfig.GetLongOpenUsers().Where(new Func<QnAgentInfo, bool>(Form1.<>c.<>9.d)).ToList<QnAgentInfo>();
					if (list.Count > 0)
					{
						string text = "";
						foreach (QnAgentInfo qnAgentInfo in list)
						{
							if (!Form1.ao.Contains(qnAgentInfo.SellerNick))
							{
								text = qnAgentInfo.QnNick;
							}
						}
						if (text == "")
						{
							Form1.ao.Clear();
							text = list[0].QnNick;
						}
						string masterNick = Util.GetMasterNick(text);
						Form1.ao.Add(masterNick);
						if (AliwwTalkWindowQn.Get(text) == null && AliwwWorkBenchQn.Get(text) == null)
						{
							AliwwMessageInfo aliwwMessageInfo = new AliwwMessageInfo();
							aliwwMessageInfo.MsgId = AppConfig.GetRandomMsgId();
							aliwwMessageInfo.SellerNick = masterNick;
							aliwwMessageInfo.BuyerNick = AppConfig.RobotUserNick;
							aliwwMessageInfo.BuyerOpenUid = AppConfig.RobotOpenUid;
							aliwwMessageInfo.MessageBody = "123";
							aliwwMessageInfo.AliwwMessageSourceType = EnumAliwwMessageSource.FromTestSend;
							aliwwMessageInfo.EnqueueTime = DateTime.Now;
							aliwwMessageInfo.SendSoftware = MsgSendSoftware.QN;
							AppConfig.AliwwMsgQueueFirst.Enqueue(aliwwMessageInfo);
						}
						else
						{
							Hashtable hashtable = this.a(A_0.DsrTask);
							if (string.IsNullOrEmpty(A_0.DsrTask.DsrUrl))
							{
								if (A_0.DsrTask.DsrNumIid > 0L)
								{
									if (A_0.DsrTask.SellerType == null)
									{
										A_0.DsrTask.SellerType = "C";
									}
									string text2 = A_0.DsrTask.SellerType.ToUpper();
									string text3 = text2;
									string text4;
									if (!(text3 == "B"))
									{
										if (text3 == "C")
										{
										}
										text4 = string.Format("https://item.taobao.com/item.htm?id={0}&ns=1&abbucket=16#detail", A_0.DsrTask.DsrNumIid);
									}
									else
									{
										text4 = string.Format("https://detail.tmall.com/item.htm?id={0}&cm_id=140105335569ed55e27b&abbucket=16", A_0.DsrTask.DsrNumIid);
									}
									AppConfig.GetUserCacheOrCreate(text).Session.OpenWebHtml(text4, OpenUrlType.DsrItemUrl, hashtable);
								}
							}
							else if (A_0.DsrTask.DsrUrl.Contains("https"))
							{
								AppConfig.GetUserCacheOrCreate(text).Session.OpenWebHtml(A_0.DsrTask.DsrUrl, OpenUrlType.DsrRateUrl, hashtable);
							}
							else
							{
								string[] array = A_0.DsrTask.DsrUrl.Split(new char[] { ';' });
								foreach (string text5 in array)
								{
									Form1.aa aa = new Form1.aa();
									aa.a = AppConfig.GetUserCacheOrCreate(text);
									aa.a.QueryDsrComplete = false;
									aa.a.QueryDsrErrMsg = "";
									string text6 = "https://rate.taobao.com/user-rate-" + text5 + ".htm";
									aa.a.Session.OpenWebHtml(text6, OpenUrlType.DsrRateUrl, hashtable);
									if (array.Length == 1)
									{
										break;
									}
									if (Util.CheckWait(5000, new Func<bool>(aa.b), 200) && string.IsNullOrEmpty(aa.a.QueryDsrErrMsg))
									{
										break;
									}
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					Form1.e(ex.ToString());
				}
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00022204 File Offset: 0x00020404
		private Hashtable a(AgisoDsrTask A_0)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["dsrUrl"] = A_0.DsrUrl;
			hashtable["dsrSellerNick"] = A_0.SellerNick;
			hashtable["title"] = A_0.DsrTitle;
			hashtable["numIid"] = A_0.DsrNumIid;
			hashtable["sellerType"] = A_0.SellerType;
			hashtable["shopId"] = A_0.ShopId;
			return hashtable;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00002B7A File Offset: 0x00000D7A
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00002B82 File Offset: 0x00000D82
		private DataRow CurrentRowMsgDgvSelect { get; set; }

		// Token: 0x060001A0 RID: 416 RVA: 0x00022290 File Offset: 0x00020490
		public Form1()
		{
			this.k();
			if (AppConfig.AllowAutoLogin)
			{
				base.StartPosition = FormStartPosition.Manual;
				base.Location = new global::System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - base.Width, Screen.PrimaryScreen.Bounds.Height - base.Height - 40);
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0002249C File Offset: 0x0002069C
		public Form1(string[] args)
		{
			this.k();
			this.at = args;
			if (args != null && args.Length != 0)
			{
				int i = 0;
				while (i < args.Length)
				{
					string text = args[i];
					string text2 = text.ToLower();
					int num = text2.IndexOf("=");
					string text3 = ((num < 0) ? text2 : text2.Substring(0, num));
					string text4 = ((num < 0) ? "" : text2.Substring(num + 1));
					string text5 = text3;
					string text6 = text5;
					uint num2 = global::p.a(text6);
					if (num2 <= 1483009432U)
					{
						if (num2 <= 863312304U)
						{
							if (num2 != 296208398U)
							{
								if (num2 != 863312304U)
								{
									goto IL_03A7;
								}
								if (!(text6 == "shield"))
								{
									goto IL_03A7;
								}
								goto IL_03B1;
							}
							else
							{
								if (!(text6 == "wordshieldlevel"))
								{
									goto IL_03A7;
								}
								goto IL_03B1;
							}
						}
						else if (num2 != 1433271620U)
						{
							if (num2 != 1483009432U)
							{
								goto IL_03A7;
							}
							if (!(text6 == "debug"))
							{
								goto IL_03A7;
							}
						}
						else
						{
							if (!(text6 == "pwd"))
							{
								goto IL_03A7;
							}
							this.ag = text4;
						}
					}
					else if (num2 <= 3457429727U)
					{
						if (num2 != 1587795294U)
						{
							if (num2 != 3457429727U)
							{
								goto IL_03A7;
							}
							if (!(text6 == "robot"))
							{
								goto IL_03A7;
							}
							AppConfig.AliwwClientMode = AliwwClientMode.机器人模式;
						}
						else
						{
							if (!(text6 == "workbanchmod"))
							{
								goto IL_03A7;
							}
							this.ad = RobotModType.WorkbanchMod;
						}
					}
					else if (num2 != 3683249910U)
					{
						if (num2 != 3751703171U)
						{
							if (num2 != 4054936549U)
							{
								goto IL_03A7;
							}
							if (!(text6 == "robotmodtype"))
							{
								goto IL_03A7;
							}
							if (text4.Equals("workbanchmod"))
							{
								this.ad = RobotModType.WorkbanchMod;
							}
						}
						else
						{
							if (!(text6 == "mod"))
							{
								goto IL_03A7;
							}
							if (text4.Equals("robot"))
							{
								AppConfig.AliwwClientMode = AliwwClientMode.机器人模式;
							}
						}
					}
					else
					{
						if (!(text6 == "shieldlevel"))
						{
							goto IL_03A7;
						}
						goto IL_03B1;
					}
					IL_03CC:
					i++;
					continue;
					IL_03A7:
					this.ag = text3;
					goto IL_03CC;
					IL_03B1:
					int num3 = Util.ToInt(text4);
					if (num3 >= 0)
					{
						this.ac = num3;
						goto IL_03CC;
					}
					goto IL_03CC;
				}
			}
			if (AppConfig.AllowAutoLogin)
			{
				base.StartPosition = FormStartPosition.Manual;
				base.Location = new global::System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - base.Width, Screen.PrimaryScreen.Bounds.Height - base.Height - 40);
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000228D4 File Offset: 0x00020AD4
		public void SetFormControlValues(long tid, string buyerNick, DateTime? msgDate, int status, string sellerNick)
		{
			Form1.ab ab = new Form1.ab();
			ab.a = this;
			ab.c = buyerNick;
			ab.d = msgDate;
			ab.e = status;
			ab.f = sellerNick;
			ab.b = ((tid > 0L) ? tid.ToString() : "");
			if (base.InvokeRequired)
			{
				this.by.Invoke(new Action(ab.k));
				this.bw.Invoke(new Action(ab.j));
				if (ab.d != null)
				{
					this.bu.Invoke(new Action(ab.i));
				}
				this.bt.Invoke(new Action(ab.h));
				this.c3.Invoke(new Action(ab.g));
			}
			else
			{
				this.by.Text = ab.b;
				this.bw.Text = ab.c;
				if (ab.d != null)
				{
					this.bu.Value = ab.d.Value;
				}
				this.bt.SelectedIndex = ab.e;
				this.c3.Text = ab.f;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00002B8B File Offset: 0x00000D8B
		private void an(object sender, EventArgs e)
		{
			new FormPrivateKeyLogin().ShowDialog();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00022A28 File Offset: 0x00020C28
		private void am(object sender, EventArgs e)
		{
			if (this.bl.SelectedRows.Count == 0 && this.bl.SelectedCells.Count == 0)
			{
				MessageBox.Show("请选中要删除的行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				DialogResult dialogResult = MessageBox.Show("是否确认删除！", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
				if (dialogResult != DialogResult.No)
				{
					foreach (object obj in this.bl.SelectedCells)
					{
						DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
						this.bl.Rows[dataGridViewCell.RowIndex].Selected = true;
					}
					foreach (object obj2 in this.bl.SelectedRows)
					{
						DataGridViewRow dataGridViewRow = (DataGridViewRow)obj2;
						if (dataGridViewRow.Cells["AccountUserNick"].Value != null)
						{
							string text = dataGridViewRow.Cells["AccountUserNick"].Value.ToString();
							if (!string.IsNullOrEmpty(text))
							{
								this.DeleteAldsAccount(text);
							}
						}
					}
				}
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00022BA4 File Offset: 0x00020DA4
		public void DeleteAldsAccount(string userNick)
		{
			Form1.ac ac = new Form1.ac();
			ac.a = userNick;
			this.d(ac.a);
			if (AppConfig.UserDict.ContainsKey(ac.a))
			{
				AldsAccountManager.Delete(ac.a);
				base.Invoke(new Action(ac.b));
				AldsAccountInfo aldsAccountInfo;
				AppConfig.UserDict.TryRemove(ac.a, out aldsAccountInfo);
			}
			WebSocketClient wwWebSocketClient = AppConfig.WwWebSocketClient;
			if (wwWebSocketClient != null)
			{
				wwWebSocketClient.RemoveOnlineUser(ac.a);
			}
			AppConfig.WwServiceClient.RemoveUser(ac.a);
			AldsBehavior aldsSession = AppConfig.GetUserCacheOrCreate(ac.a).AldsSession;
			if (aldsSession != null)
			{
				aldsSession.ChangeLoginState(false);
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00022C50 File Offset: 0x00020E50
		private void al(object sender, EventArgs e)
		{
			AliwwClientMode aliwwClientMode = AppConfig.AliwwClientMode;
			AliwwClientMode aliwwClientMode2 = aliwwClientMode;
			if (aliwwClientMode2 != AliwwClientMode.自挂模式)
			{
				if (aliwwClientMode2 == AliwwClientMode.代挂模式)
				{
					this.ad(null, null);
				}
			}
			else
			{
				this.ah(null, null);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00022C84 File Offset: 0x00020E84
		private void ak(object sender, EventArgs e)
		{
			AliwwClientMode aliwwClientMode = AppConfig.AliwwClientMode;
			AliwwClientMode aliwwClientMode2 = aliwwClientMode;
			if (aliwwClientMode2 != AliwwClientMode.自挂模式)
			{
				if (aliwwClientMode2 == AliwwClientMode.代挂模式)
				{
					this.cv.Enabled = !this.cv.Enabled;
					if (this.cv.Enabled)
					{
						this.cj.Text = "已启用";
						this.StopGetAgentWwMsgDateTime = DateTime.Now;
						this.cy.Enabled = false;
					}
					else
					{
						this.cj.Text = "已暂停";
						this.StopGetAgentWwMsgDateTime = DateTime.Now.AddMinutes(1.0);
						this.cy.Enabled = true;
					}
				}
			}
			else
			{
				if (this.bd.Enabled)
				{
					DialogResult dialogResult = MessageBox.Show("暂停开关后，无法获取新的消息。是否要暂停开关？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
					if (dialogResult == DialogResult.No)
					{
						return;
					}
				}
				this.bd.Enabled = !this.bd.Enabled;
				if (this.bd.Enabled)
				{
					Form1.e("请求消息的开关已开启");
					this.cj.Text = "已启用";
					this.OpenWwMsgWebSocket();
				}
				else
				{
					Form1.e("请求消息的开关已关闭");
					this.cj.Text = "已暂停";
					this.CloseWwMsgWebSocket();
				}
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00022DD8 File Offset: 0x00020FD8
		private void aj(object sender, EventArgs e)
		{
			if (!(this.StopGetAgentWwMsgDateTime > DateTime.Now))
			{
				this.cv.Enabled = true;
				this.cj.Text = "已启用";
				this.cy.Enabled = false;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00022E20 File Offset: 0x00021020
		private void f(object A_0)
		{
			SellerCache sellerCache = (SellerCache)A_0;
			if (!sellerCache.IsDoingSendTick)
			{
				sellerCache.IsDoingSendTick = true;
				try
				{
					while (sellerCache.AliwwMsgQueue.Count > 0)
					{
						try
						{
							AliwwMessageInfo aliwwMessageInfo = null;
							if (!this.h.ContainsKey(Thread.CurrentThread.ManagedThreadId) || !this.h[Thread.CurrentThread.ManagedThreadId])
							{
								Form1.e(string.Format("当前发货线程{0}已失效1", Thread.CurrentThread.ManagedThreadId));
								break;
							}
							if (!sellerCache.AliwwMsgQueue.TryDequeue(out aliwwMessageInfo))
							{
								break;
							}
							aliwwMessageInfo.DequeueTime = DateTime.Now;
							Form1.l = (AliwwMessageInfo)aliwwMessageInfo.Clone();
							if (AppConfig.AllowAutoLogin)
							{
								ErrCodeType errCodeType;
								this.a(aliwwMessageInfo, out errCodeType);
							}
							else
							{
								string text;
								this.a(aliwwMessageInfo, out text);
							}
						}
						catch (Exception ex)
						{
							Form1.e(ex.ToString());
						}
						finally
						{
							if (Form1.l != null)
							{
								Form1.l.IsComplete = true;
								Form1.l = null;
							}
						}
					}
				}
				catch (Exception ex2)
				{
					Form1.e(ex2.ToString());
				}
				finally
				{
					sellerCache.IsDoingSendTick = false;
				}
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00022FC0 File Offset: 0x000211C0
		private void al()
		{
			List<string> list = new List<string>();
			foreach (AldsAccountInfo aldsAccountInfo in AppConfig.UserList)
			{
				string masterNick = Util.GetMasterNick(aldsAccountInfo.UserNick);
				if (!list.Contains(masterNick))
				{
					list.Add(masterNick);
				}
			}
			if (AppConfig.AllowAutoLogin)
			{
				foreach (string text in AppConfig.AgentUserDict.Keys)
				{
					list.Add(text);
				}
			}
			foreach (string text2 in list)
			{
				SellerCache sellerExecuteCache = AppConfig.GetSellerExecuteCache(text2);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.f), sellerExecuteCache);
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000230D4 File Offset: 0x000212D4
		private void a(AliwwMessageInfo A_0, out ErrCodeType A_1)
		{
			DateTime now = DateTime.Now;
			string buyerNick = A_0.BuyerNick;
			string text = ((buyerNick != null) ? buyerNick.Trim() : null);
			AldsAccountInfo agentAccountInfo = AppConfig.GetAgentAccountInfo(A_0.SellerNick);
			ErrCodeInfo errCodeInfo = new ErrCodeInfo();
			if (agentAccountInfo == null)
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.AccountIsNull);
				A_1 = ErrCodeType.AccountIsNull;
			}
			else
			{
				UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(agentAccountInfo.UserNick);
				IIM iim = null;
				try
				{
					Form1.l.UserNick = agentAccountInfo.UserNick;
					if (A_0.AliwwMessageSourceType == EnumAliwwMessageSource.FromCallUserOnly)
					{
						if (A_0.MessageTitle == "【转接前回复消息】" && (userCacheOrCreate.DictRecentGetNewMsgLastRecvTime.ContainsKey(A_0.BuyerOpenUid) && A_0.CreateTime <= userCacheOrCreate.DictRecentGetNewMsgLastRecvTime[A_0.BuyerOpenUid]))
						{
							errCodeInfo = new ErrCodeInfo(ErrCodeType.CallSuccQn);
							A_1 = ErrCodeType.CallSuccQn;
							return;
						}
						MsgSendSoftware sendSoftware = A_0.SendSoftware;
						MsgSendSoftware msgSendSoftware = sendSoftware;
						if (msgSendSoftware - MsgSendSoftware.QN > 1)
						{
							if (msgSendSoftware != MsgSendSoftware.AliwwBuyer9)
							{
								iim = IMManager.GetCurrentIMByLastPid(agentAccountInfo);
							}
							else
							{
								iim = new AliwwBuyer9(agentAccountInfo);
							}
						}
						else
						{
							iim = new AliwwQn(agentAccountInfo);
						}
						iim.Option.IsOnlyCall = true;
						iim.Option.IsNeedScreenshot = A_0.IsNeedScreenshot;
						iim.Option.ScreenshotFileName = A_0.FileName;
						this.WriteLine(agentAccountInfo.UserNick + "-" + A_0.BuyerNick + "，只呼叫不发送！");
					}
					else if (this.aw.ContainsKey(agentAccountInfo.UserNick))
					{
						if ((DateTime.Now - this.aw[agentAccountInfo.UserNick]).TotalMinutes < 16.0)
						{
							iim = new AliwwQn(agentAccountInfo);
						}
						else
						{
							this.aw.Remove(agentAccountInfo.UserNick);
							iim = IMManager.GetCurrentIM(agentAccountInfo);
						}
					}
					else
					{
						iim = IMManager.GetCurrentIMByLastPid(agentAccountInfo);
					}
					if (!this.a(A_0, ref iim, out errCodeInfo))
					{
						A_1 = errCodeInfo.ErrCode;
					}
					else
					{
						userCacheOrCreate.LastSendMsgTime = new DateTime?(DateTime.Now);
						IIM iim2 = iim;
						string text2 = text;
						string buyerOpenUid = A_0.BuyerOpenUid;
						string messageBody = A_0.MessageBody;
						errCodeInfo = iim2.SendMsg(text2, buyerOpenUid, (messageBody != null) ? messageBody.Trim() : null, "cntaobao");
						if (A_0.AliwwMessageSourceType != EnumAliwwMessageSource.FromTestSend && A_0.AliwwMessageSourceType != EnumAliwwMessageSource.FromCallUserOnly && A_0.AliwwMessageSourceType != EnumAliwwMessageSource.FromTransferMsg)
						{
							this.a(A_0, agentAccountInfo, ref errCodeInfo, ref iim);
						}
						if (A_0.AliwwMessageSourceType == EnumAliwwMessageSource.FromTransferMsg)
						{
							string transferCallDutyManualNick = AppConfig.GetTransferCallDutyManualNick(agentAccountInfo, A_0.BuyerNick, A_0.BuyerOpenUid);
							BehaviorBase session = userCacheOrCreate.GetSession(A_0.BuyerOpenUid);
							if (session != null)
							{
								session.TransferContact(A_0.BuyerNick, "cntaobao", A_0.BuyerOpenUid, transferCallDutyManualNick, "cntaobao");
							}
						}
						Process[] processesByName = Process.GetProcessesByName("arphaCrashReport");
						if (!Util.IsEmptyList<Process>(processesByName))
						{
							foreach (Process process in processesByName)
							{
								Win32Extend.KillProcess(process);
							}
						}
						if (AppConfig.AgentSettings.AllowAutoExitQn && !agentAccountInfo.LongOpen)
						{
							if ((AppConfig.GetSellerExecuteCache(A_0.SellerNick).AliwwMsgQueue.Count <= 0 && !this.DictKeepAliveForSomeTime.ContainsKey(agentAccountInfo.UserNick)) || (errCodeInfo.ErrCode >= ErrCodeType.CallTalkWinTimeOutInTalkWin && errCodeInfo.ErrCode <= (ErrCodeType)(-3000)))
							{
								Thread.Sleep(500);
								iim.KillProcess();
							}
						}
						else
						{
							if (A_0.AliwwMessageSourceType != EnumAliwwMessageSource.FromCallUserOnly)
							{
								iim.CloseCurrentChat();
							}
							IIM currentIM = IMManager.GetCurrentIM(agentAccountInfo);
							if (currentIM.SendSoftware != iim.SendSoftware && !this.aw.ContainsKey(agentAccountInfo.UserNick) && AppConfig.GetSellerExecuteCache(A_0.SellerNick).AliwwMsgQueue.Count <= 0)
							{
								Thread.Sleep(500);
								iim.KillProcess();
							}
						}
						MsgSendSoftware sendSoftware2 = iim.SendSoftware;
						MsgSendSoftware msgSendSoftware2 = sendSoftware2;
						if (msgSendSoftware2 - MsgSendSoftware.QN > 1)
						{
							if (msgSendSoftware2 == MsgSendSoftware.AliwwBuyer9)
							{
								ErrCodeType errCode = errCodeInfo.ErrCode;
								ErrCodeType errCodeType = errCode;
								if (errCodeType == ErrCodeType.SendFailWwLoginWinNotFoundWhileAutoLogin || errCodeType == ErrCodeType.StartWwTimeout)
								{
									this.aj();
									this.ClearAllQnProc(true, true, false, false, "因为旺旺出现启动超时，仅重启explorer");
								}
							}
						}
						else
						{
							ErrCodeType errCode2 = errCodeInfo.ErrCode;
							ErrCodeType errCodeType2 = errCode2;
							if (errCodeType2 - ErrCodeType.QnLoginWinBeClosed <= 1 || errCodeType2 - ErrCodeType.StartQnTimeout <= 1 || errCodeType2 == ErrCodeType.SendFailQn5LoginWinNotFoundWhileAutoLogin)
							{
								this.aj();
								this.ClearAllQnProc(true, true, true, false, "因为" + Util.GetEnumDescription(errCodeInfo.ErrCode) + "，杀掉所有千牛");
							}
						}
						A_1 = errCodeInfo.ErrCode;
					}
				}
				catch (GetChormeJsonException ex)
				{
					errCodeInfo = new ErrCodeInfo(ErrCodeType.SendFailQn5GetChormeJsonException);
					A_1 = errCodeInfo.ErrCode;
					Form1.e(ex.ToString());
				}
				catch (ExternalException ex2)
				{
					errCodeInfo = new ErrCodeInfo(ErrCodeType.const_121);
					A_1 = errCodeInfo.ErrCode;
					iim.KillProcess();
					if (ex2.Message.Contains("GDI+ 中发生一般性错误"))
					{
						Form1.e("助手异常，重启助手，错误原因：" + ex2.ToString());
						this.a(null);
					}
					else
					{
						Form1.e("助手异常，错误原因：" + ex2.ToString());
					}
				}
				catch (Exception ex3)
				{
					AggregateException ex4 = ex3 as AggregateException;
					if (ex4 != null)
					{
						if (ex4.InnerExceptions.Any(new Func<Exception, bool>(Form1.<>c.<>9.a)))
						{
							Form1.e("助手异常，重启助手，错误原因：" + ex4.ToString());
							this.a(null);
						}
					}
					if (iim != null)
					{
						MsgSendSoftware sendSoftware3 = iim.SendSoftware;
						MsgSendSoftware msgSendSoftware3 = sendSoftware3;
						if (msgSendSoftware3 - MsgSendSoftware.QN > 1)
						{
							if (msgSendSoftware3 == MsgSendSoftware.AliwwBuyer9)
							{
								int num = this.au + 1;
								this.au = num;
								if (num >= 2)
								{
									this.ClearAllQnProc(true, true, false, true, string.Format("因为旺旺出现{0}次异常，故杀死旺旺进程（不忽略时间）", this.au));
									this.au = 0;
								}
							}
						}
						else
						{
							int num = this.av + 1;
							this.av = num;
							if (num >= 2)
							{
								this.ClearAllQnProc(true, false, true, false, string.Format("因为千牛出现{0}次异常，故杀死千牛进程（不忽略时间）", this.av));
								this.av = 0;
							}
						}
					}
					iim.KillProcess();
					errCodeInfo = new ErrCodeInfo(ErrCodeType.const_121);
					Form1.e("SendFailQn5Exception: " + ex3.ToString());
					A_1 = errCodeInfo.ErrCode;
				}
				finally
				{
					List<WinOutOfMemory> list = WinOutOfMemory.GetList();
					if (!Util.IsEmptyList<WinOutOfMemory>(list))
					{
						list.ForEach(new Action<WinOutOfMemory>(Form1.<>c.<>9.a));
						this.ClearAllQnProc(true, true, true, true, "Windows系统提示内存不足，杀掉所有千牛");
					}
					DateTime dequeueTime = A_0.DequeueTime;
					int num2 = (int)(DateTime.Now - dequeueTime).TotalSeconds;
					if ((agentAccountInfo != null && !agentAccountInfo.LongOpen) || !AppConfig.AgentSettings.AllowAutoExitQn)
					{
						AppConfig.UsedTimeQueue.Enqueue((double)num2);
					}
					if (!string.IsNullOrEmpty(A_0.FileName))
					{
						try
						{
							this.@as.DictTaskInfo[new Guid(A_0.FileName)] = errCodeInfo;
						}
						catch
						{
						}
					}
					string text3 = string.Format("耗时{0}s,{1}", num2, errCodeInfo.ToString());
					string text4 = ((agentAccountInfo == null) ? "<空的帐号>" : agentAccountInfo.UserNick);
					string text5 = string.Format("{0}\t{1}\t{2}", text4, text, text3);
					this.WriteLine(text5);
					if (A_0.AliwwMessageSourceType != EnumAliwwMessageSource.FromTestSend && A_0.AliwwMessageSourceType != EnumAliwwMessageSource.FromTransferMsg)
					{
						LogSendResultManager.Insert(A_0.MsgId, text4, errCodeInfo.ErrCode.GetHashCode(), text3, (iim != null) ? iim.SendSoftware : MsgSendSoftware.Undefined);
					}
					if (A_0.AliwwMessageSourceType == EnumAliwwMessageSource.FromWwmsgService || A_0.AliwwMessageSourceType == EnumAliwwMessageSource.FromWwSocketService)
					{
						if (A_0.MessageBody.IsActivateMsg())
						{
							if (errCodeInfo.ErrCode != ErrCodeType.SendSucc)
							{
								int? num3 = null;
								ErrCodeType errCode3 = errCodeInfo.ErrCode;
								ErrCodeType errCodeType3 = errCode3;
								string text6;
								if (errCodeType3 <= ErrCodeType.BindingIncorrectMobile)
								{
									if (errCodeType3 == ErrCodeType.AldsPlugNotFound)
									{
										text6 = "您好，您还未添加自动发货插件，请添加自动发货插件后，在重新激活！";
										goto IL_0924;
									}
									if (errCodeType3 == ErrCodeType.LoginLimitNeedMasterAccountSmsCode)
									{
										text6 = "您好，您的代挂子账号需要主账号手机短信验证，请手动登录子账号验证通过后，在重新激活。";
										goto IL_0924;
									}
									if (errCodeType3 == ErrCodeType.BindingIncorrectMobile)
									{
										IEnumerable<string> enumerable = AppConfig.AgentPhones.Take(2);
										text6 = "子账号绑定的手机号不是指定的" + string.Join("或", enumerable) + "。如果确定已绑定，则请尝试5分钟后再试一次。";
										num3 = new int?(-3066);
										goto IL_0924;
									}
								}
								else if (errCodeType3 <= ErrCodeType.SendFailQn5LoginAuthInterceptNeedToScanQr)
								{
									if (errCodeType3 == ErrCodeType.PasswordError)
									{
										text6 = "密码错误，请重新设置代挂子账号密码";
										goto IL_0924;
									}
									if (errCodeType3 == ErrCodeType.SendFailQn5LoginAuthInterceptNeedToScanQr)
									{
										text6 = "还未进行实名认证（实名认证时，手机需要登录这个子号，这时需要验证码，因此你需要先把子号绑定回你自己的手机号，待实名认证后，再改回我们指定的手机号）";
										goto IL_0924;
									}
								}
								else
								{
									if (errCodeType3 == ErrCodeType.SendFailQn5LoginFailNeedToSelectSellerType)
									{
										text6 = "帐号首次登录时需要做一些验证，请5分钟后再尝试一次激活，一般可以正常。";
										goto IL_0924;
									}
									if (errCodeType3 == ErrCodeType.SendFailAccountIsBanned)
									{
										text6 = "子帐号已被禁言，请尝试更换子帐号再试。";
										goto IL_0924;
									}
								}
								text6 = Util.GetEnumDescription(errCodeInfo.ErrCode) ?? "";
								IL_0924:
								AppConfig.QnAgentServiceClient.UpdateAgentActivateCache(A_0.SellerNick, text6, num3);
							}
							else
							{
								AgentActivateValidateResponse agentActivateValidateResponse = AppConfig.QnAgentServiceClient.AgentActivateValidate(A_0.MessageBody, agentAccountInfo.UserNick);
								if (agentActivateValidateResponse.IsError)
								{
									this.WriteLine(agentAccountInfo.UserNick + "，激活失败，" + agentActivateValidateResponse.ErrMsg);
								}
								else
								{
									this.WriteLine(agentAccountInfo.UserNick + "，激活成功");
								}
							}
						}
						else if (errCodeInfo.ErrCode != ErrCodeType.SendSucc)
						{
							ErrCodeType errCode4 = errCodeInfo.ErrCode;
							ErrCodeType errCodeType4 = errCode4;
							if (errCodeType4 <= ErrCodeType.SendFailOnlyIdleBuyerNotTbBuyer)
							{
								if (errCodeType4 <= ErrCodeType.SendFailTbIntercept)
								{
									if (errCodeType4 == ErrCodeType.SendFailContainsUnsafeLink)
									{
										AppConfig.FailAliwwMessage(A_0, 1000);
										AppConfig.QnAgentServiceClient.AutoLoginError(agentAccountInfo.UserNick, null, null, 268435455L, string.Format("您的订单{0}发送失败，被千牛拦截，拦截原因：您发送的消息中包含不安全的链接，消息发送失败。", A_0.Tid), "");
										goto IL_0BCC;
									}
									if (errCodeType4 == ErrCodeType.SendSameMsgTooManySoInterceptByTb)
									{
										AppConfig.FailAliwwMessage(A_0, 800);
										goto IL_0BCC;
									}
									if (errCodeType4 == ErrCodeType.SendFailTbIntercept)
									{
										AppConfig.FailAliwwMessage(A_0, 700);
										goto IL_0BCC;
									}
								}
								else
								{
									if (errCodeType4 == ErrCodeType.SendFailBuyerRejectMessage)
									{
										AppConfig.FailAliwwMessage(A_0, 500);
										goto IL_0BCC;
									}
									if (errCodeType4 == ErrCodeType.SendFailTeamForbid)
									{
										AppConfig.FailAliwwMessage(A_0, 200);
										AppConfig.QnAgentServiceClient.AutoLoginError(agentAccountInfo.UserNick, null, null, 268435455L, string.Format("您的订单{0}发送失败，被千牛拦截，拦截原因：消息包含了团队管理禁用词。", A_0.Tid), "");
										goto IL_0BCC;
									}
									if (errCodeType4 == ErrCodeType.SendFailOnlyIdleBuyerNotTbBuyer)
									{
										AppConfig.FailAliwwMessage(A_0, 600);
										goto IL_0BCC;
									}
								}
							}
							else if (errCodeType4 <= ErrCodeType.SendFail30DaysNotContact)
							{
								if (errCodeType4 == ErrCodeType.SendMsgTimeOutInTalkWinOfTooManyMsg)
								{
									AppConfig.FailAliwwMessage(A_0, 1200);
									goto IL_0BCC;
								}
								if (errCodeType4 == ErrCodeType.SendFailIllegalKeywords)
								{
									AppConfig.FailAliwwMessage(A_0, 100);
									AppConfig.QnAgentServiceClient.AutoLoginError(agentAccountInfo.UserNick, null, null, 268435455L, string.Format("您的订单{0}发送失败，被千牛拦截，拦截原因：消息包含了恶意网址，违规广告及其他类关键词。", A_0.Tid), "");
									goto IL_0BCC;
								}
								if (errCodeType4 == ErrCodeType.SendFail30DaysNotContact)
								{
									AppConfig.FailAliwwMessage(A_0, 1100);
									goto IL_0BCC;
								}
							}
							else if (errCodeType4 <= ErrCodeType.SendFailNotFindRead)
							{
								if (errCodeType4 == ErrCodeType.SendFailLittleRedDot)
								{
									AppConfig.FailAliwwMessage(A_0, 900);
									goto IL_0BCC;
								}
								if (errCodeType4 == ErrCodeType.SendFailNotFindRead)
								{
									goto IL_0BCC;
								}
							}
							else
							{
								if (errCodeType4 == ErrCodeType.CallFailTargetNickReceiveFriendOnly)
								{
									AppConfig.FailAliwwMessage(A_0, 400);
									goto IL_0BCC;
								}
								if (errCodeType4 == ErrCodeType.CallFailTargetNickInBlackListOrNotExists)
								{
									AppConfig.FailAliwwMessage(A_0, 300);
									goto IL_0BCC;
								}
							}
							AppConfig.FailAliwwMessage(A_0, 0);
						}
					}
					IL_0BCC:;
				}
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00023D2C File Offset: 0x00021F2C
		private void a(AliwwMessageInfo A_0, AldsAccountInfo A_1, ref ErrCodeInfo A_2, ref IIM A_3)
		{
			string text = DbUtil.TrimNull(A_0.BuyerNick);
			string text2 = DbUtil.TrimNull(A_0.MessageBody);
			AliwwOptionQn5 option = A_3.Option;
			if (A_3.SendSoftware != MsgSendSoftware.AliwwBuyer9)
			{
				ErrCodeType errCode = A_2.ErrCode;
				ErrCodeType errCodeType = errCode;
				if (errCodeType <= ErrCodeType.LoginQnNotResponsing)
				{
					if (errCodeType <= ErrCodeType.SendFailChatWinNotFoundAndBenchWinHasFound)
					{
						if (errCodeType != ErrCodeType.QnNoResponse && errCodeType != ErrCodeType.FindTalkWindowButRecentIsNull)
						{
							if (errCodeType != ErrCodeType.SendFailChatWinNotFoundAndBenchWinHasFound)
							{
								return;
							}
							A_3.KillProcess();
							return;
						}
					}
					else
					{
						if (errCodeType == ErrCodeType.SendMsgTimeOutInTalkWinOfTooManyMsg)
						{
							goto IL_01C3;
						}
						if (errCodeType != ErrCodeType.SendMsgTimeOutInTalkWin && errCodeType != ErrCodeType.LoginQnNotResponsing)
						{
							return;
						}
					}
				}
				else
				{
					if (errCodeType <= ErrCodeType.SendFailQn5LoginWinHasBeenCloseByManual)
					{
						switch (errCodeType)
						{
						case ErrCodeType.LoginQnErrorNeedToRestart:
						case ErrCodeType.SecurityCheckError:
							goto IL_00F1;
						case ErrCodeType.StartQnTimeout:
						case ErrCodeType.LoginQnTimeout:
							break;
						case ErrCodeType.StillOnLoginWinArea:
							return;
						default:
							if (errCodeType != ErrCodeType.SendFailQn5LoginFailNeedToSlide && errCodeType != ErrCodeType.SendFailQn5LoginWinHasBeenCloseByManual)
							{
								return;
							}
							goto IL_00F1;
						}
					}
					else if (errCodeType != ErrCodeType.SendFailQn5LoginWinNotFoundWhileAutoLogin)
					{
						if (errCodeType - ErrCodeType.SendFailLittleRedDot <= 1 || errCodeType - ErrCodeType.SendFailPiece <= 1)
						{
							goto IL_01C3;
						}
						return;
					}
					this.ClearAllQnProc(true, true, true, false, "因为" + Util.GetEnumDescription(A_2.ErrCode) + "，杀掉所有千牛");
					if (AppConfig.AgentSettings.LoginOnQn)
					{
						this.WriteLine(string.Format("{0}\t{1}\t{2}", A_1.UserNick, text, A_2));
						LogSendResultManager.Insert(A_0.MsgId, (A_1 != null) ? A_1.UserNick : null, A_2.ErrCode.GetHashCode(), string.Format("千牛发送消息时，{0}。用千牛再发一次。", A_2), A_3.SendSoftware);
						A_3 = IMManager.smethod_0(AppConfig.AgentSettings.QnVersion, A_1);
						A_3.Option = option;
						A_2 = A_3.SendMsg(text, A_0.BuyerOpenUid, text2, "cntaobao");
						return;
					}
					return;
				}
				IL_00F1:
				A_3.KillProcess();
				if (AppConfig.AgentSettings.LoginOnQn)
				{
					this.WriteLine(string.Format("{0}\t{1}\t{2}", A_1.UserNick, text, A_2));
					LogSendResultManager.Insert(A_0.MsgId, (A_1 != null) ? A_1.UserNick : null, A_2.ErrCode.GetHashCode(), string.Format("千牛发送消息时，{0}。用千牛再发一次。", A_2), A_3.SendSoftware);
					A_3 = IMManager.smethod_0(AppConfig.AgentSettings.QnVersion, A_1);
					A_3.Option = option;
					A_2 = A_3.SendMsg(text, A_0.BuyerOpenUid, text2, "cntaobao");
					return;
				}
				return;
				IL_01C3:
				if (AppConfig.AgentSettings.LoginOnQn)
				{
					this.WriteLine(string.Format("{0}\t{1}\t{2}", A_1.UserNick, text, A_2));
					LogSendResultManager.Insert(A_0.MsgId, (A_1 != null) ? A_1.UserNick : null, A_2.ErrCode.GetHashCode(), string.Format("千牛发送消息时，{0}。用千牛再发一次。", A_2), A_3.SendSoftware);
					A_3 = IMManager.smethod_0(AppConfig.AgentSettings.QnVersion, A_1);
					A_3.Option = option;
					A_2 = A_3.SendMsg(text, A_0.BuyerOpenUid, text2, "cntaobao");
				}
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00024070 File Offset: 0x00022270
		private bool a(AliwwMessageInfo A_0, ref IIM A_1, out ErrCodeInfo A_2)
		{
			Form1.b b = new Form1.b();
			A_2 = null;
			bool flag;
			if (!AppConfig.AllowAutoLogin)
			{
				flag = true;
			}
			else if (A_0.MessageBody.IsActivateMsg())
			{
				flag = true;
			}
			else
			{
				Regex regex = new Regex("\\{\\$[a-z0-9_]{1,4}\\$\\}");
				MatchCollection matchCollection = regex.Matches(A_0.MessageBody);
				if (matchCollection == null || matchCollection.Count <= 0)
				{
					flag = true;
				}
				else
				{
					b.a = new List<string>();
					foreach (object obj in matchCollection)
					{
						Match match = (Match)obj;
						if (!b.a.Contains(match.Groups[0].Value))
						{
							b.a.Add(match.Groups[0].Value);
						}
					}
					AldsAccountInfo agentAccountInfo = AppConfig.GetAgentAccountInfo(A_0.SellerNick);
					IEmotionManager emotionManager = EmotionManagerFactory.Get(agentAccountInfo.UserNick);
					if (!emotionManager.HasUserId())
					{
						AliwwQn aliwwQn = new AliwwQn(agentAccountInfo);
						aliwwQn.Option.IsOnlyCall = true;
						aliwwQn.Option.IsNeedGetUserInfo = true;
						AliwwQn aliwwQn2 = aliwwQn;
						string buyerNick = A_0.BuyerNick;
						string text = ((buyerNick != null) ? buyerNick.Trim() : null);
						string buyerOpenUid = A_0.BuyerOpenUid;
						string messageBody = A_0.MessageBody;
						A_2 = aliwwQn2.SendMsg(text, buyerOpenUid, (messageBody != null) ? messageBody.Trim() : null, "cntaobao");
						if (A_2.ErrCode != ErrCodeType.CallSuccQn)
						{
							A_1 = aliwwQn;
							A_2.SetErrorMsg("获取userId出错了");
							return false;
						}
						aliwwQn.KillProcess();
					}
					DateTime dateTime = DateTime.MinValue;
					LogSyncEmotion logSyncEmotion = LogSyncEmotionManager.Get(A_0.SellerNick);
					if (logSyncEmotion != null)
					{
						dateTime = logSyncEmotion.LastSyncTime;
						if (logSyncEmotion.LastUserNick != agentAccountInfo.UserNick)
						{
							dateTime = DateTime.MinValue;
						}
					}
					List<EmotionItem> list = emotionManager.Get();
					Form1.b b2 = b;
					List<string> list2;
					if (list != null)
					{
						if ((list2 = list.Where(new Func<EmotionItem, bool>(b.c)).Select(new Func<EmotionItem, string>(Form1.<>c.<>9.a)).ToList<string>()) != null)
						{
							goto IL_0233;
						}
					}
					list2 = new List<string>();
					IL_0233:
					b2.b = list2;
					bool flag2 = false;
					if (Util.IsEmptyList<string>(b.b))
					{
						dateTime = DateTime.MinValue;
						flag2 = true;
					}
					else if (b.b.Count != b.a.Count)
					{
						FileInfo fileInfo = emotionManager.GetFileInfo();
						if (fileInfo.LastWriteTime < dateTime)
						{
							dateTime = fileInfo.LastWriteTime;
						}
						flag2 = true;
					}
					if (flag2)
					{
						DateTime now = DateTime.Now;
						GetEmotionsResponse emotions = AppConfig.QnAgentServiceClient.GetEmotions(A_0.SellerNick, dateTime);
						if (emotions.IsError)
						{
							A_2 = new ErrCodeInfo(ErrCodeType.GetAgentEmotionsFail, emotions.ErrMsg);
							return false;
						}
						bool flag3 = false;
						if (emotions.Emotions != null && emotions.Emotions.Count > 0)
						{
							b.b.AddRange(emotions.Emotions.Where(new Func<Emotion, bool>(b.c)).Select(new Func<Emotion, string>(Form1.<>c.<>9.a)).ToList<string>());
							bool flag4;
							flag3 = emotionManager.Append(emotions.Emotions, out flag4);
							if (flag4)
							{
								ImKillManager.Kill(agentAccountInfo.UserNick, "表情包文件变更", true);
							}
						}
						if (flag3)
						{
							LogSyncEmotionManager.UpdateOrInsert(A_0.SellerNick, agentAccountInfo.UserNick, now);
						}
					}
					foreach (string text2 in b.b)
					{
						A_0.MessageBody = A_0.MessageBody.Replace(text2, "{$旺旺分段符}" + text2);
					}
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0002447C File Offset: 0x0002267C
		private void a(AliwwMessageInfo A_0, out string A_1)
		{
			string sellerNick = A_0.SellerNick;
			string text = Util.Trim(A_0.BuyerNick);
			if (string.IsNullOrEmpty(text))
			{
				this.WriteLine(string.Format("{0}\t{1}\t买家旺旺为空！", sellerNick, text));
				A_1 = "买家旺旺为空，无法发送！";
				LogSendResultManager.Insert(A_0.MsgId, null, -2, "买家旺旺为空，无法发送！", MsgSendSoftware.Undefined);
				if (!AppConfig.AllowAutoLogin && A_0.MsgId > 0L)
				{
					AppConfig.WwServiceClient.LogSendResult(A_0.MsgId, null, -2, "买家旺旺为空，无法发送！");
				}
				AppConfig.FailAliwwMessage(A_0, 0);
			}
			else
			{
				List<AldsAccountInfo> userList = AppConfig.GetUserList(sellerNick);
				if (Util.IsEmptyList<AldsAccountInfo>(userList))
				{
					this.WriteLine(string.Format("{0}\t卖家旺旺被删除无法发送！", sellerNick));
					A_1 = "卖家旺旺已从旺旺助手删除，无法发送！";
					LogSendResultManager.Insert(A_0.MsgId, null, -1, "卖家旺旺已从旺旺助手删除，无法发送！", MsgSendSoftware.Undefined);
					if (!AppConfig.AllowAutoLogin && A_0.MsgId > 0L)
					{
						AppConfig.WwServiceClient.LogSendResult(A_0.MsgId, null, -1, "卖家旺旺已从旺旺助手删除，无法发送！");
					}
					AppConfig.FailAliwwMessage(A_0, 0);
				}
				else
				{
					Util.Trim(A_0.MessageTitle);
					string text2 = Util.Trim(A_0.MessageBody);
					ErrCodeInfo errCodeInfo = null;
					string text3 = "";
					ErrCodeType errCodeType = ErrCodeType.Undefined;
					string text4 = "";
					try
					{
						SellerCache sellerExecuteCache = AppConfig.GetSellerExecuteCache(sellerNick);
						if (!string.IsNullOrEmpty(sellerExecuteCache.LastSuccessSendUserNick))
						{
							text4 = sellerExecuteCache.LastSuccessSendUserNick;
							AldsAccountInfo aldsAccountInfo = (AppConfig.UserDict.ContainsKey(text4) ? AppConfig.UserDict[text4] : null);
							if (aldsAccountInfo != null && aldsAccountInfo.AutoSendOnOff)
							{
								Form1.l.UserNick = text4;
								errCodeInfo = this.Send(text4, text, A_0.BuyerOpenUid, text2);
								text3 = errCodeInfo.ToString();
								errCodeType = errCodeInfo.ErrCode;
								if (errCodeType != ErrCodeType.SendSucc)
								{
									sellerExecuteCache.LastSuccessSendUserNick = "";
									if (errCodeInfo.ErrCode == ErrCodeType.CallFailTargetNickReceiveFriendOnly)
									{
										this.WriteLine(string.Format("{0}\t{1}\t{2}！", sellerNick, text, text3));
										A_1 = ((errCodeInfo.ErrCode >= ErrCodeType.Undefined) ? "" : text3);
										LogSendResultManager.Insert(A_0.MsgId, text4, (int)errCodeInfo.ErrCode, text3, MsgSendSoftware.Undefined);
										if (!AppConfig.AllowAutoLogin && A_0.MsgId > 0L)
										{
											AppConfig.WwServiceClient.LogSendResult(A_0.MsgId, text4, (int)errCodeInfo.ErrCode, text3);
										}
										AppConfig.FailAliwwMessage(A_0, 400);
										AppConfig.LogMsgResultAndNotice(text4, false);
										return;
									}
								}
							}
						}
						if (errCodeType != ErrCodeType.SendSucc)
						{
							Form1.c c = new Form1.c();
							c.a = new List<AldsAccountInfo>();
							List<AldsAccountInfo> list = userList.Where(new Func<AldsAccountInfo, bool>(c.b)).ToList<AldsAccountInfo>();
							if (c.a.Count > 0)
							{
								list.AddRange(c.a);
							}
							if (list.Count <= 0)
							{
								this.WriteLine(string.Format("{0}\t{1}\t发送开关未开启！", sellerNick, text));
								A_1 = "发送开关未开启，无法发送！";
								LogSendResultManager.Insert(A_0.MsgId, null, -3, "发送开关未开启，无法发送！", MsgSendSoftware.Undefined);
								if (!AppConfig.AllowAutoLogin && A_0.MsgId > 0L)
								{
									AppConfig.WwServiceClient.LogSendResult(A_0.MsgId, null, -3, "发送开关未开启，无法发送！");
								}
								AppConfig.FailAliwwMessage(A_0, 0);
								return;
							}
							foreach (AldsAccountInfo aldsAccountInfo2 in list)
							{
								text4 = aldsAccountInfo2.UserNick;
								Form1.l.UserNick = text4;
								errCodeInfo = this.Send(aldsAccountInfo2.UserNick, text, A_0.BuyerOpenUid, text2);
								text3 = errCodeInfo.ToString();
								errCodeType = errCodeInfo.ErrCode;
								if (errCodeInfo.ErrCode != ErrCodeType.CallFailTargetNickReceiveFriendOnly)
								{
									if (errCodeType != ErrCodeType.SendSucc)
									{
										continue;
									}
									AppConfig.GetSellerExecuteCache(sellerNick).LastSuccessSendUserNick = aldsAccountInfo2.UserNick;
								}
								break;
							}
							if (errCodeInfo != null)
							{
								if (errCodeInfo.ErrCode >= ErrCodeType.Undefined)
								{
									AppConfig.LogMsgResultAndNotice(text4, true);
								}
								else
								{
									AppConfig.LogMsgResultAndNotice(text4, false);
								}
								if (errCodeInfo.ErrCode == ErrCodeType.MainWindowNotFound || errCodeInfo.ErrCode == ErrCodeType.CallFailChatWindowNotFound)
								{
									Random random = new Random();
									int num = random.Next(0, 10);
									this.b(num);
								}
							}
						}
					}
					catch (GetChormeJsonException)
					{
						this.WriteLine("您好，旺旺发送助手与千牛“" + text4 + "”通信失败，如果连续出现，请尝试操作助手下方的“通信修复”");
					}
					catch (Exception ex)
					{
						text3 += "<>失败，异常！";
						Form1.e(ex.ToString());
					}
					string text5 = ((errCodeType == ErrCodeType.SendSucc) ? text4 : string.Format("所有“{0}”下的主号或子号共{1}个", sellerNick, userList.Count));
					string text6 = string.Format("{0}\t{1}\t{2}", text5, text, text3);
					this.WriteLine(text6);
					A_1 = ((errCodeType >= ErrCodeType.Undefined) ? "" : text3);
					LogSendResultManager.Insert(A_0.MsgId, text4, (int)errCodeType, text3, MsgSendSoftware.Undefined);
					if (!AppConfig.AllowAutoLogin && A_0.MsgId > 0L)
					{
						AppConfig.WwServiceClient.LogSendResult(A_0.MsgId, text4, (int)errCodeType, text3);
					}
					if (errCodeType != ErrCodeType.SendSucc && A_0.Tid > 0L && (A_0.AliwwMessageSourceType == EnumAliwwMessageSource.FromWwmsgService || A_0.AliwwMessageSourceType == EnumAliwwMessageSource.FromWwSocketService))
					{
						UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(text4);
						ErrCodeType errCodeType2 = errCodeType;
						ErrCodeType errCodeType3 = errCodeType2;
						if (errCodeType3 <= ErrCodeType.SendFailTeamForbid)
						{
							if (errCodeType3 <= ErrCodeType.SendSameMsgTooManySoInterceptByTb)
							{
								if (errCodeType3 == ErrCodeType.SendFailContainsUnsafeLink)
								{
									AppConfig.FailAliwwMessage(A_0, 1000);
									return;
								}
								if (errCodeType3 == ErrCodeType.SendSameMsgTooManySoInterceptByTb)
								{
									AppConfig.FailAliwwMessage(A_0, 800);
									return;
								}
							}
							else
							{
								if (errCodeType3 == ErrCodeType.SendFailTbIntercept)
								{
									AppConfig.FailAliwwMessage(A_0, 700);
									return;
								}
								if (errCodeType3 == ErrCodeType.SendFailBuyerRejectMessage)
								{
									AppConfig.FailAliwwMessage(A_0, 500);
									return;
								}
								if (errCodeType3 == ErrCodeType.SendFailTeamForbid)
								{
									if (userCacheOrCreate.LastTeamForbidTime == null || (DateTime.Now - userCacheOrCreate.LastTeamForbidTime.Value).Hours >= 1)
									{
										userCacheOrCreate.LastTeamForbidTime = new DateTime?(DateTime.Now);
										AppConfig.WwServiceClient.NotifyUser(text4, A_0.Tid, 2);
									}
									AppConfig.FailAliwwMessage(A_0, 200);
									return;
								}
							}
						}
						else if (errCodeType3 <= ErrCodeType.SendFailIllegalKeywords)
						{
							if (errCodeType3 == ErrCodeType.SendFailOnlyIdleBuyerNotTbBuyer)
							{
								AppConfig.FailAliwwMessage(A_0, 600);
								return;
							}
							if (errCodeType3 == ErrCodeType.SendFailIllegalKeywords)
							{
								if (userCacheOrCreate.LastIllegalKeywordsTime == null || (DateTime.Now - userCacheOrCreate.LastIllegalKeywordsTime.Value).Hours >= 1)
								{
									userCacheOrCreate.LastIllegalKeywordsTime = new DateTime?(DateTime.Now);
									AppConfig.WwServiceClient.NotifyUser(text4, A_0.Tid, 4);
								}
								AppConfig.FailAliwwMessage(A_0, 100);
								return;
							}
						}
						else
						{
							if (errCodeType3 == ErrCodeType.SendFailAccountIsBanned)
							{
								if (userCacheOrCreate.LastAccountIsBannedTime == null || (DateTime.Now - userCacheOrCreate.LastAccountIsBannedTime.Value).Hours >= 1)
								{
									userCacheOrCreate.LastAccountIsBannedTime = new DateTime?(DateTime.Now);
									AppConfig.WwServiceClient.NotifyUser(text4, A_0.Tid, 1);
								}
								AppConfig.FailAliwwMessage(A_0, 0);
								return;
							}
							if (errCodeType3 == ErrCodeType.SendFail30DaysNotContact)
							{
								AppConfig.FailAliwwMessage(A_0, 1100);
								return;
							}
							if (errCodeType3 == ErrCodeType.CallFailTargetNickReceiveFriendOnly)
							{
								AppConfig.FailAliwwMessage(A_0, 400);
								return;
							}
						}
						AppConfig.FailAliwwMessage(A_0, 0);
					}
				}
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00024CC0 File Offset: 0x00022EC0
		private int ak()
		{
			int num = 0;
			try
			{
				if (AppConfig.AliwwMsgIdListOrderByEnqueueTime.Count == 0 && AppConfig.AliwwMsgQueueFirst.Count == 0)
				{
					this.e(2000);
					return 0;
				}
				this.e(10000);
				for (;;)
				{
					IL_01C8:
					if (AppConfig.AliwwMsgQueueFirst.Count > 0)
					{
						if (!this.h.ContainsKey(Thread.CurrentThread.ManagedThreadId) || !this.h[Thread.CurrentThread.ManagedThreadId])
						{
							goto Block_16;
						}
						do
						{
							AliwwMessageInfo aliwwMessageInfo;
							if (AppConfig.AliwwMsgQueueFirst.TryDequeue(out aliwwMessageInfo))
							{
								aliwwMessageInfo.DequeueTime = DateTime.Now;
								bool flag;
								this.a(aliwwMessageInfo, out flag);
							}
						}
						while (AppConfig.AliwwMsgQueueFirst.Count > 0);
					}
					while (AppConfig.AliwwMsgIdListOrderByEnqueueTime.Count > 0)
					{
						long num2 = AppConfig.AliwwMsgIdListOrderByEnqueueTime[0];
						string text = (AppConfig.AliwwMsgDictOrderByEnqueueTime.ContainsKey(num2) ? AppConfig.AliwwMsgDictOrderByEnqueueTime[num2] : "");
						if (string.IsNullOrEmpty(text))
						{
							break;
						}
						int num3 = 0;
						int num4 = (AppConfig.AllowAutoLogin ? (AppConfig.AgentSettings.SwitchNickAfterFiveMsg ? 5 : 10) : 15);
						AgisoQueue aliwwMsgQueue = AppConfig.GetSellerExecuteCache(text).AliwwMsgQueue;
						while (aliwwMsgQueue.Count > 0 && num3++ <= num4)
						{
							if (!this.h.ContainsKey(Thread.CurrentThread.ManagedThreadId) || !this.h[Thread.CurrentThread.ManagedThreadId])
							{
								goto IL_0202;
							}
							if (AppConfig.AliwwMsgQueueFirst.Count > 0)
							{
								goto IL_01C8;
							}
							AliwwMessageInfo aliwwMessageInfo2;
							if (!aliwwMsgQueue.TryDequeue(out aliwwMessageInfo2))
							{
								break;
							}
							aliwwMessageInfo2.DequeueTime = DateTime.Now;
							num++;
							bool flag2;
							this.a(aliwwMessageInfo2, out flag2);
						}
					}
					break;
				}
				goto IL_0225;
				Block_16:
				Form1.e(string.Format("当前发货线程{0}已失效2", Thread.CurrentThread.ManagedThreadId));
				return 0;
				IL_0202:
				Form1.e(string.Format("当前发货线程{0}已失效3", Thread.CurrentThread.ManagedThreadId));
				return 0;
				IL_0225:;
			}
			catch (Exception ex)
			{
				Form1.e(ex.ToString());
			}
			return num;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00024F24 File Offset: 0x00023124
		private void a(AliwwMessageInfo A_0, out bool A_1)
		{
			A_1 = false;
			string text = "";
			try
			{
				Form1.l = (AliwwMessageInfo)A_0.Clone();
				int num = 6;
				if (AppConfig.AllowAutoLogin && (DateTime.Now - A_0.EnqueueTime).TotalMinutes > (double)num && (DateTime.Now - this._lastSendMailTimeForDequeueTooLazy).TotalMinutes > (double)num)
				{
					this._lastSendMailTimeForDequeueTooLazy = DateTime.Now;
					AldsAccountInfo agentAccountInfo = AppConfig.GetAgentAccountInfo(A_0.SellerNick);
					if (agentAccountInfo != null)
					{
						AppConfig.QnAgentServiceClient.AutoLoginError(agentAccountInfo.UserNick, null, string.Format("消息出队时间大于{0}分钟", num), 1L, "", "");
					}
				}
				if (!AppConfig.AllowAutoLogin)
				{
					this.a(A_0, out text);
				}
				else
				{
					ErrCodeType errCodeType;
					this.a(A_0, out errCodeType);
					text = ((errCodeType < ErrCodeType.Undefined) ? Util.GetEnumDescription(errCodeType) : "");
					if (errCodeType <= (ErrCodeType)(-3000) && errCodeType >= ErrCodeType.CallTalkWinTimeOutInTalkWin)
					{
						A_1 = true;
					}
				}
			}
			catch (Exception ex)
			{
				Form1.e(ex.ToString());
			}
			finally
			{
				if (Form1.l != null)
				{
					Form1.l.IsComplete = true;
					Form1.l = null;
				}
				if (A_0.MsgId == AppConfig.TestSendMsgId)
				{
					AppConfig.HasTestSendAliwwMessageInfo = false;
					AppConfig.TestSendErrorMsg = text;
				}
				if (AppConfig.AllowAutoLogin && (DateTime.Now - this.ax).TotalMinutes >= 10.0)
				{
					Form1.d d = new Form1.d();
					d.b = this;
					this.ax = DateTime.Now;
					d.a = DateTime.Now;
					Task.Run(new Action(d.c));
				}
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00025130 File Offset: 0x00023330
		private void e(int A_0)
		{
			Form1.e e = new Form1.e();
			e.a = this;
			e.b = A_0;
			if (AppConfig.AllowAutoLogin && this.cv.Enabled && this.cv.Interval != e.b)
			{
				if (base.InvokeRequired)
				{
					base.Invoke(new Action(e.c));
				}
				else
				{
					this.cv.Interval = e.b;
				}
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000251B0 File Offset: 0x000233B0
		private void aj()
		{
			try
			{
				Process[] processesByName = Process.GetProcessesByName("AliWorkbench");
				foreach (Process process in processesByName)
				{
					try
					{
						if (process.MainWindowHandle == IntPtr.Zero)
						{
							Win32Extend.KillProcessById(process.Id, null);
						}
						process.Dispose();
					}
					catch
					{
					}
				}
				List<long> list = new List<long>();
				if (AppConfig.AgentSettings.LoginOnAliwwBuyer)
				{
					List<QnAgentInfo> longOpenUsers = AppConfig.GetLongOpenUsers();
					foreach (QnAgentInfo qnAgentInfo in longOpenUsers)
					{
						UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(qnAgentInfo.QnNick);
						if (userCacheOrCreate.LastSendProcessId > 0)
						{
							list.Add((long)userCacheOrCreate.LastSendProcessId);
						}
					}
				}
				foreach (KeyValuePair<string, DateTime> keyValuePair in this.DictKeepAliveForSomeTime)
				{
					UserCache userCacheOrCreate2 = AppConfig.GetUserCacheOrCreate(keyValuePair.Key);
					if (userCacheOrCreate2.LastSendProcessId > 0)
					{
						list.Add((long)userCacheOrCreate2.LastSendProcessId);
					}
				}
				Process[] processesByName2 = Process.GetProcessesByName("AliIM");
				foreach (Process process2 in processesByName2)
				{
					try
					{
						if (process2.MainWindowHandle == IntPtr.Zero && !list.Contains((long)process2.Id))
						{
							Win32Extend.KillProcessById(process2.Id, null);
						}
						process2.Dispose();
					}
					catch
					{
					}
				}
			}
			catch (Exception ex)
			{
				Form1.e(ex.ToString());
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000253D4 File Offset: 0x000235D4
		private void e(object A_0)
		{
			this.h[Thread.CurrentThread.ManagedThreadId] = true;
			Form1.l = null;
			while (this.h.ContainsKey(Thread.CurrentThread.ManagedThreadId) && this.h[Thread.CurrentThread.ManagedThreadId])
			{
				Thread.Sleep(1);
				if (this.ak)
				{
					return;
				}
				try
				{
					if (AppConfig.AllowAutoLogin && AppConfig.AgentSettings == null)
					{
						Thread.Sleep(500);
						continue;
					}
					Form1.i = new DateTime?(DateTime.Now);
					if ((DateTime.Now - this.n).TotalSeconds > 5.0)
					{
						this.n = DateTime.Now;
						try
						{
							this.ai(null, null);
						}
						catch (Exception ex)
						{
							LogWriter.WriteLog("触发答复开始异常\r\n" + ex.ToString(), 1);
							this.WriteLine("触发答复开始异常");
						}
						this.ai();
					}
					if (AppConfig.AllowAutoLogin && AppConfig.SyncEmotionsUserNicks.Count > 0)
					{
						List<string> list = AppConfig.SyncEmotionsUserNicks.Select(new Func<string, string>(Form1.<>c.<>9.f)).ToList<string>();
						foreach (string text in list)
						{
							ImKillManager.Kill(text, "表情包同步", true);
							AppConfig.SyncEmotionsUserNicks.Remove(text);
						}
					}
					if (this.y.Count > 0)
					{
						int count = this.y.Count;
						for (int i = count - 1; i >= 0; i--)
						{
							int num = this.y[i];
							Win32Extend.KillProcessById(num, null);
							this.y.Remove(num);
						}
					}
					Form1.i = null;
					Form1.j = 0;
					try
					{
						if (AppConfig.IsAllowSendTickByMultiThread && !AppConfig.CurrentSystemSettingInfo.AllowSendExpression)
						{
							this.al();
						}
						else
						{
							int num2;
							do
							{
								num2 = this.ak();
							}
							while (num2 > 0);
						}
					}
					catch (Exception ex2)
					{
						LogWriter.WriteLog("触发发货开始异常\r\n" + ex2.ToString(), 1);
						this.WriteLine("触发发货开始异常！");
						if (!AppConfig.AllowAutoLogin)
						{
							this.b(true);
						}
					}
				}
				catch (Exception ex3)
				{
					Form1.e(ex3.ToString());
				}
				Thread.Sleep(100);
			}
			Form1.e(string.Format("当前发货线程{0}已失效4", Thread.CurrentThread.ManagedThreadId));
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000256FC File Offset: 0x000238FC
		private void ai()
		{
			if (AppConfig.AllowAutoLogin && !Util.IsEmptyList<KeyValuePair<string, QnAgentInfo>>(AppConfig.AgentUserDict) && AppConfig.AgentSettings.LoginOnQn)
			{
				if ((DateTime.Now - this.w).TotalMinutes >= 5.0)
				{
					this.w = DateTime.Now;
					foreach (KeyValuePair<string, QnAgentInfo> keyValuePair in AppConfig.AgentUserDict)
					{
						AliwwTalkWindowQn aliwwTalkWindowQn = AliwwTalkWindowQn.Get(keyValuePair.Value.QnNick);
						if (aliwwTalkWindowQn != null && aliwwTalkWindowQn.HWnd != IntPtr.Zero)
						{
							AliwwTalkWindow aliwwTalkWindow = aliwwTalkWindowQn.Convert<AliwwTalkWindow>();
							aliwwTalkWindow.CloseCurrentChat();
						}
						UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(keyValuePair.Value.QnNick);
						if (userCacheOrCreate.LastSendMsgTime != null && (DateTime.Now - userCacheOrCreate.LastSendMsgTime.Value).TotalHours >= 1.0)
						{
							ImKillManager.Kill(keyValuePair.Value.QnNick, "一个小时内无消息", true);
							userCacheOrCreate.LastSendMsgTime = null;
						}
					}
				}
				DateTime now = DateTime.Now;
				if (this.az.Hour != now.Hour && now.Minute >= 4)
				{
					this.az = now;
					try
					{
						this.ClearUnLongOpenQn(null);
					}
					catch (Exception ex)
					{
						Form1.e(ex.ToString());
					}
					SysTrayWnd.CloseInActiveTrayWnd();
				}
				if (this.DictKeepAliveForSomeTime.Count > 0)
				{
					List<KeyValuePair<string, DateTime>> list = this.DictKeepAliveForSomeTime.Where(new Func<KeyValuePair<string, DateTime>, bool>(Form1.<>c.<>9.a)).ToList<KeyValuePair<string, DateTime>>();
					foreach (KeyValuePair<string, DateTime> keyValuePair2 in list)
					{
						QnAgentInfo agentInfo = AppConfig.GetAgentInfo(keyValuePair2.Key);
						if (agentInfo == null || (AppConfig.AgentSettings.AllowAutoExitQn && !agentInfo.LongOpen))
						{
							ImKillManager.Kill(keyValuePair2.Key, "关闭通过使用默认联系人联系打开的窗口", false);
						}
						this.DictKeepAliveForSomeTime.Remove(keyValuePair2.Key);
					}
				}
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000259A8 File Offset: 0x00023BA8
		private void d(object A_0)
		{
			while (!this.ak)
			{
				try
				{
					this.ah();
				}
				catch (Exception ex)
				{
					Form1.e(ex.ToString());
				}
				Thread.Sleep(1000);
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000259F4 File Offset: 0x00023BF4
		private void ah()
		{
			string[] array = this.TxtRoboter.Text.Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			if (array != null && array.Length != 0)
			{
				string[] array2 = array;
				int i = 0;
				while (i < array2.Length)
				{
					string text = array2[i];
					if (!this.ay.ContainsKey(text))
					{
						this.ay[text] = false;
					}
					Aliww aliww = new Aliww(text);
					AliwwTalkWindow aliwwTalkWindow = aliww.GetCustomerBenchWindow(false);
					if (aliwwTalkWindow != null)
					{
						goto IL_0180;
					}
					this.ay[text] = false;
					AppConfig.WriteLog("robot alitw is null", LogType.LogForRobot, 1);
					string text2 = Convert.ToString(ConfigurationManager.AppSettings[text]);
					if (!string.IsNullOrEmpty(text2))
					{
						ErrCodeInfo errCodeInfo = new AliwwQn(new AldsAccountInfo
						{
							UserNick = text,
							QnAccountPwd = text2
						})
						{
							Option = 
							{
								IsOnlyCall = true
							}
						}.SendMsg("agiso", "", "", "cntaobao");
						if (errCodeInfo.ErrCode != ErrCodeType.CallSuccQn)
						{
							this.WriteLine("call aliww fail, " + Util.GetEnumDescription(errCodeInfo.ErrCode));
							AppConfig.WriteLog("call aliww fail, " + Util.GetEnumDescription(errCodeInfo.ErrCode), LogType.LogForRobot, 1);
						}
						else
						{
							aliwwTalkWindow = aliww.GetCustomerBenchWindow(false);
							if (aliwwTalkWindow != null)
							{
								goto IL_0180;
							}
						}
					}
					IL_037B:
					i++;
					continue;
					IL_0180:
					this.ay[text] = true;
					UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(text);
					int num = 0;
					int num2 = 10;
					if (userCacheOrCreate.RecvMsgQueue.Count > 0)
					{
						while (num++ < num2)
						{
							RecvMsgResponse recvMsgResponse;
							userCacheOrCreate.RecvMsgQueue.TryDequeue(out recvMsgResponse);
							if (recvMsgResponse != null)
							{
								string fromNick = recvMsgResponse.FromNick;
								string text3 = recvMsgResponse.FromUid.Replace(recvMsgResponse.FromNick, "");
								try
								{
									if (aliwwTalkWindow.Disabled)
									{
										aliwwTalkWindow.CloseOwnedWindow();
									}
									this.ab = DateTime.Now;
									string text4 = Util.Trim(recvMsgResponse.Msg);
									if (text.Equals(recvMsgResponse.FromNick) || (text3 + text).Equals(recvMsgResponse.FromUid) || recvMsgResponse.FromNick.StartsWith(text + ":") || ("cntaobao" + text).Equals(recvMsgResponse.FromUid) || ("cnalichn" + text).Equals(recvMsgResponse.FromUid))
									{
										AppConfig.WriteLog("robot 当前消息为自己发的，不处理", LogType.LogForRobot, 1);
										continue;
									}
									if (string.IsNullOrEmpty(text4))
									{
										continue;
									}
									if (text4.IsActivateMsg())
									{
										AgentActivateValidateResponse agentActivateValidateResponse = Form1.aa.AgentActivateValidate(text4, fromNick);
										string text5;
										if (agentActivateValidateResponse.AgentHostId > 0L)
										{
											text5 = string.Format("代挂已激活，服务器ID：{0}", agentActivateValidateResponse.AgentHostId);
										}
										else
										{
											text5 = "代挂激活失败，" + agentActivateValidateResponse.ErrMsg;
										}
										string text6 = string.Format("{0}\t{1}\t{2}", text, fromNick, text5);
										this.WriteLine(text6);
										continue;
									}
									continue;
								}
								finally
								{
									BehaviorBase session = userCacheOrCreate.GetSession(recvMsgResponse.SecurityUID);
									if (session != null)
									{
										session.OpenChat(fromNick, recvMsgResponse.SecurityUID, "cntaobao");
									}
									Thread.Sleep(300);
									aliwwTalkWindow.CloseCurrentChat();
								}
								break;
							}
							break;
						}
						goto IL_037B;
					}
					goto IL_037B;
				}
				string text7 = array.FirstOrDefault(new Func<string, bool>(Form1.<>c.<>9.e));
				if (string.IsNullOrEmpty(text7))
				{
					foreach (KeyValuePair<string, bool> keyValuePair in this.ay)
					{
						if (keyValuePair.Value)
						{
							this.h(keyValuePair.Key);
						}
					}
				}
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00025E48 File Offset: 0x00024048
		private void h(string A_0)
		{
			string[] array = this.TxtRoboter.Text.Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			if (array != null && array.Length != 0)
			{
				string text = "";
				int num = 0;
				Aliww aliww = new Aliww(A_0);
				AliwwTalkWindow customerBenchWindow = aliww.GetCustomerBenchWindow(false);
				if (customerBenchWindow != null)
				{
					for (int i = 0; i < 1000; i++)
					{
						try
						{
							if (customerBenchWindow.Disabled)
							{
								customerBenchWindow.CloseOwnedWindow();
							}
							AliwwMsgElement aliwwMsgElement = customerBenchWindow.ChatView.GetLastReceiveMessage(A_0);
							if (aliwwMsgElement == null || string.IsNullOrEmpty(aliwwMsgElement.SenderNick))
							{
								Thread.Sleep(200);
								aliwwMsgElement = customerBenchWindow.ChatView.GetLastReceiveMessage(A_0);
								if (aliwwMsgElement == null || string.IsNullOrEmpty(aliwwMsgElement.SenderNick))
								{
									break;
								}
							}
							string contentText = aliwwMsgElement.ContentText;
							string senderNick = aliwwMsgElement.SenderNick;
							string senderSite = aliwwMsgElement.SenderSite;
							if (text.Equals(senderNick))
							{
								if (num > 5)
								{
									AppConfig.WriteLog("robot lastTargetCheckTimes > 5", LogType.LogForRobot, 1);
									num = 0;
									break;
								}
								num++;
								Thread.Sleep(200);
								AppConfig.WriteLog("robot lastTargetNick.Equals(buyerNick)", LogType.LogForRobot, 1);
							}
							text = senderNick;
							if (aliwwMsgElement.MsgSendId == null)
							{
								aliwwMsgElement.MsgSendId = "";
							}
							if (customerBenchWindow.UserNick.Equals(aliwwMsgElement.SenderNick) || ("cntaobao" + customerBenchWindow.UserNick).Equals(aliwwMsgElement.MsgSendId) || ("cnalichn" + customerBenchWindow.UserNick).Equals(aliwwMsgElement.MsgSendId) || ("cntaobao" + AppConfig.RobotUserNick).Equals(aliwwMsgElement.MsgSendId) || aliwwMsgElement.MsgSendId.StartsWith("cntaobao" + AppConfig.RobotUserNick + ":"))
							{
								AppConfig.WriteLog("robot 最后一条是自己发的，关闭", LogType.LogForRobot, 1);
							}
							else if (aliwwMsgElement.IsSysMsg)
							{
								AppConfig.WriteLog("robot 系统提示，关闭", LogType.LogForRobot, 1);
							}
							else if (!string.IsNullOrEmpty(contentText) && (contentText.IndexOf("系统提示") >= 0 || contentText.IndexOf("系统提醒") >= 0 || contentText.IndexOf("系统消息") >= 0 || contentText.IndexOf("对方的离线消息数已经达到上限") >= 0 || contentText.IndexOf("您还不是对方的好友") >= 0 || contentText.IndexOf("[自动回复]") >= 0 || "给您发送了一个震屏".Equals(contentText) || "给你发送了一个振屏".Equals(contentText) || "撤回了一条消息".Equals(contentText)))
							{
								AppConfig.WriteLog("robot 系统提示，关闭", LogType.LogForRobot, 1);
							}
							else
							{
								this.ab = DateTime.Now;
								if (contentText.IsActivateMsg())
								{
									AgentActivateValidateResponse agentActivateValidateResponse = Form1.aa.AgentActivateValidate(contentText, senderNick);
									string text2;
									if (agentActivateValidateResponse.AgentHostId > 0L)
									{
										text2 = string.Format("代挂已激活，服务器ID：{0}", agentActivateValidateResponse.AgentHostId);
									}
									else
									{
										text2 = "代挂激活失败，服务器ID：0。" + agentActivateValidateResponse.ErrMsg;
									}
									string text3 = string.Format("{0}\t{1}\t{2}", A_0, senderNick, text2);
									this.WriteLine(text3);
								}
							}
						}
						finally
						{
							customerBenchWindow.CloseCurrentChat();
							Thread.Sleep(200);
						}
					}
				}
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000261F8 File Offset: 0x000243F8
		private void ai(object sender, EventArgs e)
		{
			if (!AppConfig.AllowAutoLogin)
			{
				try
				{
					if (!Util.IsEmptyList<AutoReplyInfo>(AppConfig.AutoReplyList))
					{
						foreach (AldsAccountInfo aldsAccountInfo in AppConfig.UserList)
						{
							if (aldsAccountInfo.AutoReply)
							{
								string text = aldsAccountInfo.UserNick.Trim();
								Aliww aliww = new Aliww(text);
								AliwwTalkWindow customerBenchWindow = aliww.GetCustomerBenchWindow(false);
								if (customerBenchWindow != null)
								{
									bool flag;
									this.a(customerBenchWindow, aldsAccountInfo, out flag);
								}
								else
								{
									List<AliwwTalkWindow> list = aliww.EnumAliwwChatWindow();
									if (list != null && list.Count != 0)
									{
										foreach (AliwwTalkWindow aliwwTalkWindow in list)
										{
											string text2;
											bool flag2;
											string text3;
											aliwwTalkWindow.AutoReply(aldsAccountInfo, out text2, out flag2, out text3);
											if (!string.IsNullOrEmpty(text2))
											{
												this.WriteLine(string.Format("{0}\t{1}\t{2}", aliwwTalkWindow.UserNick, text3, text2));
											}
											if (!AppConfig.CurrentSystemSettingInfo.DisableCloseWindowWhenAutoReply && !flag2)
											{
												aliwwTalkWindow.CloseCurrentChat();
											}
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
					string text4 = ex.Message.ToLower();
					if ((!text4.Contains("http") || !text4.Contains("500")) && !text4.Contains("连接千牛失败"))
					{
						this.WriteLine("智能答复时异常");
						Form1.e("智能答复时异常: " + ex.Message);
					}
				}
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00026400 File Offset: 0x00024600
		private void ah(object sender, EventArgs e)
		{
			Form1.f f = new Form1.f();
			f.b = this;
			f.a = AppConfig.GetValidAndSwitchOnSellerNicks();
			if (!Util.IsEmptyList<string>(f.a))
			{
				Task.Run(new Action(f.c));
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00002B98 File Offset: 0x00000D98
		private void ag(object sender, EventArgs e)
		{
			Task.Run(new Action(this.j));
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00026444 File Offset: 0x00024644
		private void af(object sender, EventArgs e)
		{
			Form1.h h = new Form1.h();
			h.a = this;
			h.b = AppConfig.CurrentSystemSettingInfo.DisableCloseWindowWhenAutoReply;
			this.p();
			if (this.aj.Count > 50)
			{
				h.b = false;
			}
			string[] array = this.TxtRoboter.Text.Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			if (array != null && array.Length != 0)
			{
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					Form1.i i2 = new Form1.i();
					i2.b = h;
					i2.a = array2[i];
					Form1.j j = new Form1.j();
					j.b = i2;
					Aliww aliww = new Aliww(j.b.a);
					List<AliwwTalkWindow> list = aliww.EnumAliwwChatWindow();
					if (list != null && list.Count != 0)
					{
						this.ab = DateTime.Now;
						j.a = this.af;
						foreach (AliwwTalkWindow aliwwTalkWindow in list)
						{
							Form1.k k = new Form1.k();
							k.d = j;
							aliwwTalkWindow.AliVersion = AliwwVersion.AliwwBuyer2014;
							aliwwTalkWindow.chatWindowType = ChatWindowType.ChatWindow;
							string text = aliwwTalkWindow.GetNewMessage();
							if (text != null && text.IndexOf(AppConfig.RobotUserNick + " (") < 0 && text.IndexOf(k.d.b.a + " (") < 0 && text != null)
							{
								text = text.Trim();
								if (!string.IsNullOrEmpty(text) && (text.IndexOf("系统提示") >= 0 || text.IndexOf("系统提醒") >= 0 || text.IndexOf("系统消息") >= 0 || text.IndexOf("对方的离线消息数已经达到上限") >= 0 || text.IndexOf("您还不是对方的好友") >= 0 || text.IndexOf("[自动回复]") >= 0))
								{
									if (!k.d.b.b.b)
									{
										aliwwTalkWindow.CloseCurrentChat();
									}
								}
								else
								{
									AliwwMsgElement currentLastReceiveMessage = aliwwTalkWindow.GetCurrentLastReceiveMessage();
									k.a = currentLastReceiveMessage.SenderNick;
									k.c = currentLastReceiveMessage.SenderSite;
									if (text.IndexOf(AppConfig.DefaultTestSendMsgBody.Substring(0, 15)) >= 0)
									{
										string text2;
										int num = this.a(aliwwTalkWindow, k.d.b.a, k.a, k.c, "机器人已收到您测试发送的消息！", out text2);
										if (num == 201)
										{
											text2 = "测试发送答复成功";
										}
										string text3 = string.Format("{0}\t{1}\t{2}", k.d.b.a, k.a, text2);
										this.WriteLine(text3);
										if (!k.d.b.b.b)
										{
											aliwwTalkWindow.CloseCurrentChat();
										}
									}
									else
									{
										if (this.aj.ContainsKey(k.a))
										{
											if (this.aj[k.a].DeadLine < DateTime.Now)
											{
												continue;
											}
											this.aj.Remove(k.a);
										}
										this.aj.Add(k.a, new RobotAlitwScanInfo
										{
											Alitw = aliwwTalkWindow,
											ScanTime = DateTime.Now,
											DeadLine = DateTime.Now.AddMinutes(2.0)
										});
										k.b = Util.GetTidsFromString(text);
										Task.Run(new Action(k.e));
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00026870 File Offset: 0x00024A70
		private void ae(object sender, EventArgs e)
		{
			string applicationUuid = AppConfig.ApplicationUuid;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (AldsAccountInfo aldsAccountInfo in AppConfig.UserList)
			{
				if (aldsAccountInfo.IsValid && aldsAccountInfo.AutoSendOnOff && !aldsAccountInfo.IsRemoveByServer)
				{
					WebSocketClient wwWebSocketClient = AppConfig.WwWebSocketClient;
					if ((wwWebSocketClient != null && !wwWebSocketClient.IsOpened) || aldsAccountInfo.WebSocketStatus != "√")
					{
						dictionary[aldsAccountInfo.UserNick] = aldsAccountInfo.Password;
					}
				}
			}
			if (dictionary.Count > 0)
			{
				AppConfig.WwWebSocketClient.BatchLogin(dictionary, applicationUuid);
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00026938 File Offset: 0x00024B38
		private void ag()
		{
			if (!this.cu.Enabled)
			{
				if (base.InvokeRequired)
				{
					base.Invoke(new Action(this.i));
				}
				else
				{
					this.cu.Start();
				}
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00026980 File Offset: 0x00024B80
		private void af()
		{
			if (this.cu.Enabled)
			{
				if (base.InvokeRequired)
				{
					base.Invoke(new Action(this.h));
				}
				else
				{
					this.cu.Stop();
				}
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000269C4 File Offset: 0x00024BC4
		public void OpenWwMsgWebSocket()
		{
			if (AppConfig.IsSellerLoginOnOwnComputer && this.bd.Enabled && AppConfig.CurrentSystemSettingInfo.AllowGetMsgByWebSocket)
			{
				this.ag();
				this.ae(null, null);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00026A08 File Offset: 0x00024C08
		public void CloseWwMsgWebSocket()
		{
			this.af();
			WebSocketClient wwWebSocketClient = AppConfig.WwWebSocketClient;
			if (wwWebSocketClient != null)
			{
				wwWebSocketClient.Close();
			}
			foreach (AldsAccountInfo aldsAccountInfo in AppConfig.UserList)
			{
				aldsAccountInfo.WebSocketStatus = "-";
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00026A70 File Offset: 0x00024C70
		public static void BatchLogin()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			if (!Util.IsEmptyList<KeyValuePair<string, AldsAccountInfo>>(AppConfig.UserDict))
			{
				foreach (AldsAccountInfo aldsAccountInfo in AppConfig.UserDict.Values)
				{
					dictionary2[aldsAccountInfo.UserNick] = aldsAccountInfo.Password;
				}
				try
				{
					BatchLoginResponse batchLoginResponse = AppConfig.WwServiceClient.BatchLogin(dictionary2);
					if (batchLoginResponse.IsError)
					{
						LogWriter.WriteLog(batchLoginResponse.ErrMsg, 1);
						MessageBox.Show("登录时网络错误！\r\n如果问题仍然存在，请联系旺旺agiso！\r\n\r\n异常描述：" + batchLoginResponse.ErrMsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					if (batchLoginResponse.Accounts != null)
					{
						using (List<AliwwClientAccount>.Enumerator enumerator2 = batchLoginResponse.Accounts.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								AliwwClientAccount aliwwClientAccount = enumerator2.Current;
								AldsAccountInfo aldsAccountInfo2 = AppConfig.UserDict[aliwwClientAccount.UserNick];
								aldsAccountInfo2.Map(aliwwClientAccount);
								AppConfig.UserDict[aliwwClientAccount.UserNick] = aldsAccountInfo2;
								if (aldsAccountInfo2.IsValid && aldsAccountInfo2.AutoSendOnOff)
								{
									dictionary[aldsAccountInfo2.UserNick] = aldsAccountInfo2.Password;
								}
							}
							goto IL_017F;
						}
					}
					foreach (AldsAccountInfo aldsAccountInfo3 in AppConfig.UserDict.Values)
					{
						aldsAccountInfo3.VerifyResult = "登录时网络错误";
					}
					IL_017F:;
				}
				catch (Exception ex)
				{
					foreach (AldsAccountInfo aldsAccountInfo4 in AppConfig.UserDict.Values)
					{
						aldsAccountInfo4.VerifyResult = "登录时网络错误";
					}
					LogWriter.WriteLog(ex.ToString(), 1);
					MessageBox.Show("登录时网络错误！\r\n如果问题仍然存在，请联系旺旺agiso！\r\n\r\n异常描述：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				if (AppConfig.CurrentSystemSettingInfo.AllowGetMsgByWebSocket && dictionary.Count > 0 && AppConfig.WwWebSocketClient != null)
				{
					AppConfig.WwWebSocketClient.BatchLogin(dictionary, AppConfig.ApplicationUuid);
				}
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00026D28 File Offset: 0x00024F28
		private void ad(object sender, EventArgs e)
		{
			Form1.o o = new Form1.o();
			o.b = this;
			int num;
			int msgQueueAllSellerTotalCount = AppConfig.GetMsgQueueAllSellerTotalCount(out o.a, out num);
			if (msgQueueAllSellerTotalCount > 20)
			{
				string text = string.Format("消息队列还有{0}未处理，暂停获取消息", msgQueueAllSellerTotalCount);
				this.WriteLine(text);
				Form1.e(text + string.Format("，ThreadState：{0}，IsAlive：{1}", (int)this.g.ThreadState, this.g.IsAlive));
				Task.Run(new Action(this.ae));
			}
			else
			{
				Task.Run(new Action(o.c));
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00026DD0 File Offset: 0x00024FD0
		private void ae()
		{
			if (Monitor.TryEnter(Form1.a1, TimeSpan.FromMilliseconds(100.0)))
			{
				try
				{
					if (AppConfig.AgentSettings.AllowAutoExitQn)
					{
						if (AppConfig.AgentSettings.AutoDispatch)
						{
							if (AppConfig.AgentProxyInfo != null)
							{
								List<QnAgentInfo> longOpenUsers = AppConfig.GetLongOpenUsers();
								List<string> list = AppConfig.AliwwMsgDictOrderByEnqueueTime.Select(new Func<KeyValuePair<long, string>, string>(Form1.<>c.<>9.a)).ToList<string>();
								string text = "";
								if (Form1.l != null)
								{
									text = Form1.l.SellerNick;
									list.Add(text);
								}
								list = ((list != null) ? list.Distinct<string>().ToList<string>() : null);
								if (!Util.IsEmptyList<string>(list))
								{
									List<string> list2 = longOpenUsers.Select(new Func<QnAgentInfo, string>(Form1.<>c.<>9.c)).ToList<string>();
									list = list.Except(list2).ToList<string>();
									int num = AppConfig.AgentSettings.AgentDispatchLimit;
									if (num <= 0)
									{
										num = 3;
									}
									if (list.Count >= num)
									{
										if (!string.IsNullOrEmpty(text) && !list2.Contains(text))
										{
											list.Remove(text);
											num--;
										}
										List<string> list3 = list.Skip(num).Take(list.Count - num).ToList<string>();
										List<string> list4 = list3.Select(new Func<string, string>(Form1.<>c.<>9.c)).Where(new Func<string, bool>(Form1.<>c.<>9.b)).ToList<string>();
										if (!Util.IsEmptyList<string>(list4))
										{
											this.WriteLine("开始申请调度，" + string.Join("；", list4));
											AgentDispatchResponse agentDispatchResponse = AppConfig.QnAgentServiceClient.AgentDispatch(list4);
											if (agentDispatchResponse.IsError)
											{
												this.WriteLine("申请调度失败，" + agentDispatchResponse.ErrMsg);
											}
											else
											{
												if (Util.IsEmptyList<string>(agentDispatchResponse.SuccUserNickList))
												{
													this.WriteLine(string.Join(",", list4) + "，全部申请调度失败");
												}
												List<string> list5 = agentDispatchResponse.SuccUserNickList.Select(new Func<string, string>(Form1.<>c.<>9.a)).ToList<string>();
												foreach (string text2 in list5)
												{
													AppConfig.GetSellerExecuteCache(text2).RoolbackMsgs(true);
												}
												List<string> list6 = ((list4.Count == agentDispatchResponse.SuccUserNickList.Count) ? new List<string>() : list4.Except(agentDispatchResponse.SuccUserNickList).ToList<string>());
												this.WriteLine(string.Join(",", agentDispatchResponse.SuccUserNickList) + "，申请调度成功" + (Util.IsEmptyList<string>(list6) ? "" : ("\t" + string.Join(",", list6) + "，申请调度失败")));
											}
										}
									}
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					string text3 = "申请调度过程出现异常，";
					Exception ex2 = ex;
					LogWriter.WriteLog(text3 + ((ex2 != null) ? ex2.ToString() : null), 1);
				}
				finally
				{
					Monitor.Exit(Form1.a1);
				}
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000271A0 File Offset: 0x000253A0
		private void g(string A_0)
		{
			if (AppConfig.AliwwWebScoketServer.IsListening)
			{
				UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(A_0);
				if (userCacheOrCreate.LastAldsBeatTime != null && (DateTime.Now - userCacheOrCreate.LastAldsBeatTime.Value).TotalSeconds > 30.0 && !userCacheOrCreate.IsAldsSessionNull)
				{
					userCacheOrCreate.AldsSession.CloseSession();
					userCacheOrCreate.ClearAldsSession();
				}
				if (userCacheOrCreate.LastRecentBeatTime != null)
				{
					if ((DateTime.Now - userCacheOrCreate.LastRecentBeatTime.Value).TotalSeconds > 60.0)
					{
						this.b(A_0, "超过60s无心跳");
					}
					else if ((DateTime.Now - userCacheOrCreate.LastRecentBeatTime.Value).TotalSeconds > 30.0 && !userCacheOrCreate.IsRecentSessionNull)
					{
						userCacheOrCreate.RecentSession.CloseSession();
						userCacheOrCreate.ClearRecentSession();
					}
				}
				if (userCacheOrCreate.IsRecentSessionNull && userCacheOrCreate.LastRecentBeatTime == null)
				{
					this.b(A_0, "session不存在");
				}
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000272E8 File Offset: 0x000254E8
		private void a(AliwwTalkWindow A_0, AldsAccountInfo A_1, out bool A_2)
		{
			A_2 = false;
			int num = 5;
			UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(A_1.UserNick);
			int num2 = 0;
			RecvMsgResponse recvMsgResponse;
			while (num2 < num && (AppConfig.CurrentSystemSettingInfo.RecvMsgReplyInterval <= 0 || (userCacheOrCreate.RecvMsgQueue.TryPeek(out recvMsgResponse) && (DateTime.Now - recvMsgResponse.SentTime).TotalSeconds >= (double)AppConfig.CurrentSystemSettingInfo.RecvMsgReplyInterval)) && userCacheOrCreate.RecvMsgQueue.TryDequeue(out recvMsgResponse))
			{
				if ((recvMsgResponse.MsgFrom == MsgFrom.FromRecentRegMsg || (recvMsgResponse.MsgFrom == MsgFrom.FromAldsQn && !userCacheOrCreate.IsRecentSessionNull)) && string.IsNullOrEmpty(recvMsgResponse.Msg))
				{
					if (!userCacheOrCreate.DictRecentGetNewMsgLastRecvTime.ContainsKey(recvMsgResponse.SecurityUID) || !(recvMsgResponse.SentTime <= userCacheOrCreate.DictRecentGetNewMsgLastRecvTime[recvMsgResponse.SecurityUID]))
					{
						num2++;
						BehaviorBase session = userCacheOrCreate.GetSession(recvMsgResponse.SecurityUID);
						if (session != null)
						{
							session.OpenChat(recvMsgResponse.FromNick, recvMsgResponse.SecurityUID, "cntaobao");
						}
					}
				}
				else
				{
					num2++;
					string text;
					bool flag;
					int num3 = A_0.AutoReply(A_1, recvMsgResponse.FromUid, recvMsgResponse.SecurityUID, recvMsgResponse.Msg, recvMsgResponse.SentTime, recvMsgResponse.MsgFrom, out text, out flag, out A_2);
					if (!string.IsNullOrEmpty(text))
					{
						this.WriteLine(string.Format("{0}\t{1}\t{2}", A_0.UserNick, recvMsgResponse.FromNick, text));
					}
					if (!AppConfig.CurrentSystemSettingInfo.DisableCloseWindowWhenAutoReply && !flag)
					{
						if (num3 < 0)
						{
							BehaviorBase session2 = userCacheOrCreate.GetSession(recvMsgResponse.SecurityUID);
							if (session2 != null)
							{
								session2.OpenChat(recvMsgResponse.FromNick, recvMsgResponse.SecurityUID, "cntaobao");
							}
							Thread.Sleep(300);
						}
						A_0.CloseCurrentChat();
					}
				}
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000274C4 File Offset: 0x000256C4
		private void ad()
		{
			if (AppConfig.IsSellerLoginOnOwnComputer)
			{
				AppConfig.WwWebSocketClient = new WebSocketClient();
				AppConfig.WwWebSocketClient.AliwwWsMsgReceived += this.a;
				AppConfig.WwWebSocketClient.OnlineUserChanged += this.a;
				AppConfig.WwWebSocketClient.OnNotifyMsged += this.a;
				AppConfig.WwWebSocketClient.OnOpened += this.c;
				AppConfig.WwWebSocketClient.OnClosed += this.b;
				AppConfig.WwWebSocketClient.OnError += this.a;
				AppConfig.WwWebSocketClient.CanInitWs = new Func<bool>(this.g);
				AppConfig.WwWebSocketClient.GetMsgCount += this.ac;
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00027598 File Offset: 0x00025798
		private int ac()
		{
			List<string> list;
			int num;
			return AppConfig.GetMsgQueueAllSellerTotalCount(out list, out num);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000275B0 File Offset: 0x000257B0
		private void a(NotifyMsg A_0)
		{
			Form1.p p = new Form1.p();
			p.a = this;
			p.b = A_0;
			base.Invoke(new EventHandler(p.c));
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000275E4 File Offset: 0x000257E4
		private void a(CurrOnlineUsers A_0)
		{
			List<string> users = A_0.Users;
			if (Util.IsEmptyList<string>(users))
			{
				foreach (AldsAccountInfo aldsAccountInfo in AppConfig.UserList)
				{
					if (aldsAccountInfo.WebSocketStatus == "√")
					{
						aldsAccountInfo.WebSocketStatus = "-";
						if (A_0.HasNewConnection.GetValueOrDefault())
						{
							aldsAccountInfo.IsRemoveByServer = true;
							this.WriteLine(aldsAccountInfo.UserNick + "：实时获取消息被踢下线了，检测下是否在其他地方登录？");
						}
					}
				}
				WebSocketClient wwWebSocketClient = AppConfig.WwWebSocketClient;
				if (wwWebSocketClient != null)
				{
					wwWebSocketClient.Close();
				}
				LogWriter.WriteLog("没有在线用户，关闭获取消息长连接", 1);
			}
			else
			{
				foreach (AldsAccountInfo aldsAccountInfo2 in AppConfig.UserList)
				{
					if (aldsAccountInfo2.WebSocketStatus == "√" && !users.Contains(aldsAccountInfo2.UserNick))
					{
						aldsAccountInfo2.WebSocketStatus = "-";
						if (A_0.HasNewConnection.GetValueOrDefault())
						{
							aldsAccountInfo2.IsRemoveByServer = true;
							this.WriteLine(aldsAccountInfo2.UserNick + "：实时获取消息被踢下线了，检测下是否在其他地方登录？");
						}
					}
				}
				foreach (string text in users)
				{
					AppConfig.UserDict[text].WebSocketStatus = "√";
					AppConfig.UserDict[text].IsRemoveByServer = false;
				}
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000277B4 File Offset: 0x000259B4
		private void a(List<AliwwWsMsg> A_0)
		{
			AliwwMessageManager.Insert(A_0);
			foreach (AliwwWsMsg aliwwWsMsg in A_0)
			{
				string text = DbUtil.TrimNull(aliwwWsMsg.SellerNick);
				AliwwMessageInfo aliwwMessageInfo = new AliwwMessageInfo();
				aliwwMessageInfo.MsgId = aliwwWsMsg.IdNo;
				aliwwMessageInfo.SellerNick = aliwwWsMsg.SellerNick;
				aliwwMessageInfo.BuyerNick = aliwwWsMsg.BuyerNick;
				aliwwMessageInfo.BuyerOpenUid = aliwwWsMsg.BuyerOpenUid;
				aliwwMessageInfo.Tid = Util.ToLong(aliwwWsMsg.Tid);
				aliwwMessageInfo.MessageBody = aliwwWsMsg.Message;
				aliwwMessageInfo.CreateTime = aliwwWsMsg.CreateTime;
				aliwwMessageInfo.AliwwMessageSourceType = EnumAliwwMessageSource.FromWwSocketService;
				aliwwMessageInfo.EnqueueTime = DateTime.Now;
				AppConfig.GetSellerExecuteCache(text).AliwwMsgQueue.Enqueue(aliwwMessageInfo);
				try
				{
					AppConfig.WwWebSocketClient.MsgSendSuccess(aliwwWsMsg.IdNo);
				}
				catch (Exception ex)
				{
					Form1.e(ex.ToString());
				}
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000278C8 File Offset: 0x00025AC8
		private void c(object A_0)
		{
			foreach (AldsAccountInfo aldsAccountInfo in AppConfig.UserList)
			{
				aldsAccountInfo.WebSocketStatus = "√";
			}
			LogWriter.WriteLog("获取消息长连接已开启", 1);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00027928 File Offset: 0x00025B28
		private void b(object A_0)
		{
			foreach (AldsAccountInfo aldsAccountInfo in AppConfig.UserList)
			{
				aldsAccountInfo.WebSocketStatus = "-";
			}
			LogWriter.WriteLog("获取消息长连接已关闭", 1);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00002BAC File Offset: 0x00000DAC
		private void a(Exception A_0)
		{
			LogWriter.WriteLog(string.Format("获取消息长连接异常，{0}", A_0), 1);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00027988 File Offset: 0x00025B88
		private void ab()
		{
			this.@as = new AgentRemoteHttpListener();
			this.@as.Start();
			this.@as.OnRestart += this.a;
			this.@as.OnUpgrade += this.AliwwClient_OnUpgrade;
			this.@as.OnGetHostVersion += this.v;
			this.@as.OnSendAgain += this.a;
			this.@as.OnStopAgentWwMsg += this.d;
			this.@as.OnScreenshot += this.a;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00027A38 File Offset: 0x00025C38
		private void ac(object sender, EventArgs e)
		{
			if (!AppConfig.AllowAutoLogin || !this.n())
			{
				try
				{
					AppConfig.DpiX = base.CreateGraphics().DpiX;
				}
				catch (Exception ex)
				{
					Form1.e(ex.ToString());
				}
				DbHelper.InitDb();
				SystemSettingsManager.Init();
				AppConfig.InitAutoReplyData();
				DbHelper.UpdateDb();
				if (AppConfig.CurrentSystemSettingInfo.PeriodOfTime > 0)
				{
					int num = AppConfig.CurrentSystemSettingInfo.PeriodOfTime * 60;
					if (SystemSettingsManager.UpdateTransferInterval(AppConfig.CurrentSystemSettingInfo.IdNo, num))
					{
						AppConfig.CurrentSystemSettingInfo.TransferInterval = num;
						AppConfig.CurrentSystemSettingInfo.PeriodOfTime = 0;
					}
				}
				this.WriteLine("欢迎您使用Agiso自动发货助手");
				Form1.e("欢迎您使用Agiso自动发货助手");
				RepairQnAliresManager.RepairAlires();
				ServicePointManager.DefaultConnectionLimit = 512;
				ServicePointManager.Expect100Continue = false;
				this.Text += AppConfig.ApplicationUuid;
				this.bd.Interval = this.al;
				this.be.Interval = this.am;
				this.bl.AutoGenerateColumns = false;
				this.cc.AutoGenerateColumns = false;
				this.u();
				this.t();
				this.x();
				if (!AppConfig.AllowAutoLogin)
				{
					if (!this.bd.Enabled)
					{
						this.bd.Start();
					}
					this.ad();
					Form1.BatchLogin();
					this.cu.Start();
					if (AppConfig.CurrentSystemSettingInfo.AutoSendBeforeMsg)
					{
						AppConfig.GetAliwwMsgStartTime = DateTime.Now.AddHours(-5.0);
					}
				}
				if (AppConfig.UserList != null && AppConfig.UserList.Count > 0)
				{
					int num2 = this.q();
					if (num2 == 0)
					{
						this.a7.SelectedIndex = 1;
					}
				}
				this.bl.DataSource = AppConfig.UserList;
				AppConfig.UserList.ListChanged += this.a;
				this.a(AppConfig.AutoReplyList);
				this.s();
				AliwwClientMode aliwwClientMode = AppConfig.AliwwClientMode;
				AliwwClientMode aliwwClientMode2 = aliwwClientMode;
				if (aliwwClientMode2 != AliwwClientMode.机器人模式)
				{
					if (aliwwClientMode2 != AliwwClientMode.自挂模式)
					{
						if (aliwwClientMode2 == AliwwClientMode.代挂模式)
						{
							AppConfig.CurrentSystemSettingInfo.AliwwMessageLengthMax = 800;
							this.a7.SelectedIndex = 1;
							this.cv.Enabled = true;
							this.cv.Interval = new Random().Next(9000, 11000);
							this.cw.Visible = true;
							this.cz.Visible = true;
							this.c1.Visible = true;
							this.de.Visible = true;
							this.ab();
							GetAgentAllUserResponse allUser = AppConfig.QnAgentServiceClient.GetAllUser();
							if (allUser.AgentUser != null)
							{
								AppConfig.AgentUserDict.Clear();
								foreach (QnAgentInfo qnAgentInfo in allUser.AgentUser)
								{
									AppConfig.AgentUserDict[qnAgentInfo.SellerNick] = qnAgentInfo;
									AppConfig.GetUserCacheOrCreate(qnAgentInfo.QnNick).CurrentWorksheet = (qnAgentInfo.CustomerServiceNewVersion ? qnAgentInfo.CustomerServiceWorksheetInfo : new List<CustomerServiceWorksheet>());
								}
							}
							int num3 = 3;
							for (int num4 = 1; num4 <= num3; num4++)
							{
								try
								{
									GetAgentHostInfoResponse agentHostInfo = AppConfig.QnAgentServiceClient.GetAgentHostInfo();
									if (agentHostInfo.IsError)
									{
										if (num4 == num3)
										{
											if (MessageBox.Show(agentHostInfo.ErrMsg, "出错了", MessageBoxButtons.OK, MessageBoxIcon.Hand) == DialogResult.OK)
											{
												this.p(null, null);
											}
											return;
										}
									}
									else
									{
										if (agentHostInfo.info != null)
										{
											this.Text = this.Text + "【" + agentHostInfo.info.Name + "】";
											AppConfig.AllowKeepAliveNum = agentHostInfo.info.AllowKeepAliveNum;
										}
										if (agentHostInfo.Setting != null)
										{
											AppConfig.AgentSettings = agentHostInfo.Setting;
											AppConfig.AgentProxyInfo = agentHostInfo.AgentProxyInfo;
											ProxyXmlManager.Handle();
											break;
										}
										if (MessageBox.Show("获取到的代挂配置为空", "出错了", MessageBoxButtons.OK, MessageBoxIcon.Hand) == DialogResult.OK)
										{
											this.p(null, null);
										}
										return;
									}
								}
								catch (Exception ex2)
								{
									if (num4 == num3)
									{
										if (MessageBox.Show(ex2.Message, "出错了", MessageBoxButtons.OK, MessageBoxIcon.Hand) == DialogResult.OK)
										{
											this.p(null, null);
										}
										return;
									}
								}
							}
							this.ck.Items.Add("用默认联系人启动千牛7.12+聊天", null, new EventHandler(this.l));
							this.ck.Items.Add("用默认联系人启动千牛6.04+聊天", null, new EventHandler(this.k));
							this.ck.Items.Add("用默认联系人启动旺旺聊天", null, new EventHandler(this.j));
							DataTable notExistsSendLogResult = AliwwMessageManager.GetNotExistsSendLogResult(DateTime.Now.AddMinutes(-15.0), DateTime.Now);
							if (notExistsSendLogResult != null && notExistsSendLogResult.Rows.Count > 0)
							{
								foreach (object obj in notExistsSendLogResult.Rows)
								{
									DataRow dataRow = (DataRow)obj;
									this.a(dataRow);
								}
							}
							SystemSettingsInfo currentSystemSettingInfo = AppConfig.CurrentSystemSettingInfo;
							currentSystemSettingInfo.DisableCloseWindowWhenAutoReplyWhileTrueFunc = (Action)Delegate.Combine(currentSystemSettingInfo.DisableCloseWindowWhenAutoReplyWhileTrueFunc, new Action(this.e));
							Form1.AgentHttpListenerInstance = new AgentHttpListener();
							Form1.AgentHttpListenerInstance.Start();
							this.y();
						}
					}
					else
					{
						this.be.Start();
						this.cw.Visible = false;
						this.cz.Visible = false;
						this.c1.Visible = false;
						QnUserDbManager.DisableUseNativeMessageList(null);
						if (AppConfig.UserDict != null)
						{
							foreach (KeyValuePair<string, AldsAccountInfo> keyValuePair in AppConfig.UserDict)
							{
								AppConfig.GetUserCacheOrCreate(keyValuePair.Value.UserNick).CurrentWorksheet = CustomerServiceWorksheetManager.GetList(keyValuePair.Value.DefaultMouldId.Value, Util.GetMasterNick(keyValuePair.Value.UserNick));
							}
						}
						this.ag(null, null);
					}
				}
				else
				{
					this.TxtRoboter.Text = AppConfig.RobotUserNick ?? "";
					Form1.aa = new RobotClient(this.ag, AppConfig.PathPrefix);
					this.a7.SelectedIndex = 1;
					this.cg.Visible = true;
					this.c7.Visible = true;
					this.TxtRoboter.Visible = true;
					if (this.ad == RobotModType.ChatMod)
					{
						this.bf.Interval = this.ae;
						this.bf.Start();
					}
					else
					{
						try
						{
							this.k = new Thread(new ParameterizedThreadStart(this.d));
							this.k.SetApartmentState(ApartmentState.STA);
							if (!this.k.IsAlive)
							{
								this.k.Start();
							}
						}
						catch (Exception ex3)
						{
							Form1.e(ex3.ToString());
						}
					}
					this.ab();
				}
				try
				{
					AppConfig.AliwwWebScoketServer = new WebSocketServerIns(null);
					AppConfig.AliwwWebScoketServer.Start();
				}
				catch (Exception ex4)
				{
					Form1.e(ex4.ToString());
					if (AppConfig.AllowAutoLogin)
					{
						this.WriteLine("启动ws服务端异常！");
						Form1.e("启动ws服务端失败，失败：" + ex4.Message);
					}
					else
					{
						DialogResult dialogResult = MessageBox.Show("您好Agiso旺旺发送助手启动失败，请按以下方式重试：\r\n1：重启助手\r\n2：重启电脑\r\n3：以上方式都试过，可联系在线客服" + AppConfig.GetContactWay().Wangwang, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						if (dialogResult == DialogResult.OK)
						{
							this.p(null, null);
						}
					}
				}
				finally
				{
					this.aa();
				}
				try
				{
					this.r();
				}
				catch (Exception ex5)
				{
					Form1.e(ex5.ToString());
				}
				finally
				{
					this.z();
				}
				AppConfig.GetContactWay();
				string text;
				string text2;
				if (AppConfig.IsSellerLoginOnOwnComputer && this.IsNeedUpgrade(out text, out text2))
				{
					AppConfig.GetCurrentApplicationVersion();
					new FormUpgrade("检测到新版本，是否进行升级？\r\n\r\n" + text + " => " + text2).Show(this);
				}
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000282E0 File Offset: 0x000264E0
		private void b(string A_0, string A_1)
		{
			WinChromeContainerQn winChromeContainerQn = AliwwTalkWindowQn.Get(A_0);
			if (winChromeContainerQn == null)
			{
				AppConfig.WriteLog(string.Concat(new string[] { "【", A_1, "】UserNick: ", A_0, ", 未找到聊天窗口" }), LogType.LogSendMsgByWebSocket, 1);
			}
			else
			{
				UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(A_0);
				if (userCacheOrCreate.IsRecentSessionNull)
				{
					AppConfig.WriteLog(string.Concat(new string[] { "【", A_1, "】UserNick: ", A_0, ", 开启session" }), LogType.LogSendMsgByWebSocket, 1);
					try
					{
						if (winChromeContainerQn.CanConnect(A_0))
						{
							winChromeContainerQn.ImplantedJsForWsClient(A_0);
						}
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000283A8 File Offset: 0x000265A8
		private void aa()
		{
			this.TimerBeat = new global::System.Timers.Timer();
			this.TimerBeat.Interval = 10000.0;
			this.TimerBeat.Elapsed += this.d;
			this.TimerBeat.Start();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000283F8 File Offset: 0x000265F8
		private void d(object sender, ElapsedEventArgs e)
		{
			try
			{
				if (!AppConfig.AliwwWebScoketServer.IsListening)
				{
					Form1.e("wsserv down? unbelievable!");
					try
					{
						AppConfig.AliwwWebScoketServer.Start();
					}
					catch (Exception ex)
					{
						Form1.e(string.Format("启动ws服务端异常，{0}", ex));
					}
				}
				else if (AppConfig.AllowAutoLogin)
				{
					if (AppConfig.AgentSettings.LoginOnQn)
					{
						List<QnAgentInfo> longOpenUsers = AppConfig.GetLongOpenUsers();
						foreach (QnAgentInfo qnAgentInfo in longOpenUsers)
						{
							this.g(qnAgentInfo.QnNick);
						}
					}
				}
				else
				{
					foreach (KeyValuePair<string, AldsAccountInfo> keyValuePair in AppConfig.UserDict)
					{
						string key = keyValuePair.Key;
						AldsAccountInfo value = keyValuePair.Value;
						this.g(key);
					}
				}
			}
			catch (Exception ex2)
			{
				Form1.e(ex2.ToString());
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0002852C File Offset: 0x0002672C
		private void z()
		{
			this.TimerMonitorSendThread = new global::System.Timers.Timer();
			this.TimerMonitorSendThread.Interval = 10000.0;
			this.TimerMonitorSendThread.Elapsed += this.c;
			this.TimerMonitorSendThread.Start();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0002857C File Offset: 0x0002677C
		private void y()
		{
			this.TimerAdjustLongOpenThread = new global::System.Timers.Timer();
			this.TimerAdjustLongOpenThread.Interval = 1800000.0;
			this.TimerAdjustLongOpenThread.Elapsed += this.b;
			this.TimerAdjustLongOpenThread.Start();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000285CC File Offset: 0x000267CC
		private void x()
		{
			this.TimerReliabilityMonitor = new global::System.Timers.Timer();
			this.TimerReliabilityMonitor.Interval = 2000.0;
			this.TimerReliabilityMonitor.Elapsed += this.a;
			this.TimerReliabilityMonitor.Start();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0002861C File Offset: 0x0002681C
		private void w()
		{
			if (AppConfig.AllowKeepAliveNum > 0 && AppConfig.AgentSettings.AllowAutoExitQn && !Util.IsEmptyList<KeyValuePair<string, QnAgentInfo>>(AppConfig.AgentUserDict))
			{
				List<SellerSendMsgInfo> list = AliwwMessageManager.StatisticsSendMsgCount(DateTime.Now.AddMinutes(-120.0), new DateTime?(DateTime.Now.AddMinutes(-60.0)));
				List<SellerSendMsgInfo> list2 = AliwwMessageManager.StatisticsSendMsgCount(DateTime.Now.AddMinutes(-60.0), null);
				if (list != null || list2 != null)
				{
					List<SellerSendMsgWeight> list3;
					if (list == null)
					{
						list3 = list2.Select(new Func<SellerSendMsgInfo, SellerSendMsgWeight>(Form1.<>c.<>9.b)).OrderByDescending(new Func<SellerSendMsgWeight, double>(Form1.<>c.<>9.c)).ToList<SellerSendMsgWeight>();
					}
					else if (list2 == null)
					{
						list3 = list.Select(new Func<SellerSendMsgInfo, SellerSendMsgWeight>(Form1.<>c.<>9.a)).OrderByDescending(new Func<SellerSendMsgWeight, double>(Form1.<>c.<>9.b)).ToList<SellerSendMsgWeight>();
					}
					else
					{
						list3 = new List<SellerSendMsgWeight>();
						using (Dictionary<string, QnAgentInfo>.Enumerator enumerator = AppConfig.AgentUserDict.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Form1.q q = new Form1.q();
								q.a = enumerator.Current;
								double num = (double)list.FirstOrDefault(new Func<SellerSendMsgInfo, bool>(q.c)).MsgCount * 0.4;
								double num2 = (double)list2.FirstOrDefault(new Func<SellerSendMsgInfo, bool>(q.b)).MsgCount * 0.6;
								list3.Add(new SellerSendMsgWeight
								{
									SellerNick = q.a.Value.SellerNick,
									Weight = num + num2
								});
							}
						}
						list3 = list3.OrderByDescending(new Func<SellerSendMsgWeight, double>(Form1.<>c.<>9.a)).ToList<SellerSendMsgWeight>();
					}
					int num3 = 0;
					int num4 = AppConfig.AllowKeepAliveNum;
					List<string> list4 = new List<string>();
					List<string> list5 = new List<string>();
					List<string> list6 = new List<string>();
					foreach (KeyValuePair<string, QnAgentInfo> keyValuePair in AppConfig.AgentUserDict)
					{
						if (keyValuePair.Value.NotAllowAutoKeepAlive && keyValuePair.Value.LongOpen)
						{
							list4.Add(keyValuePair.Value.QnNick);
							num4--;
						}
						else
						{
							if (keyValuePair.Value.LongOpen)
							{
								list5.Add(keyValuePair.Value.QnNick);
							}
							keyValuePair.Value.LongOpen = false;
						}
					}
					if (num4 > 0)
					{
						foreach (SellerSendMsgWeight sellerSendMsgWeight in list3)
						{
							if (num3 >= num4)
							{
								break;
							}
							QnAgentInfo qnAgentInfo = AppConfig.AgentUserDict[sellerSendMsgWeight.SellerNick];
							if (!qnAgentInfo.NotAllowAutoKeepAlive && !list4.Contains(qnAgentInfo.QnNick))
							{
								list6.Add(qnAgentInfo.QnNick);
								qnAgentInfo.LongOpen = true;
								num3++;
								list4.Add(qnAgentInfo.QnNick);
							}
						}
						LogWriter.WriteLog("当前常挂用户：" + string.Join("，", list4), 1);
						List<string> list7 = list5.Except(list6).ToList<string>();
						List<string> list8 = list6.Except(list5).ToList<string>();
						if (!Util.IsEmptyList<string>(list8) || !Util.IsEmptyList<string>(list7))
						{
							AgentAdjustLongOpenResponse agentAdjustLongOpenResponse = AppConfig.QnAgentServiceClient.AdjustLongOpen(list8, list7);
							if (agentAdjustLongOpenResponse.IsError)
							{
								LogWriter.WriteLog("上传常挂信息失败，" + agentAdjustLongOpenResponse.ErrMsg, 1);
							}
						}
					}
					List<AliwwTalkWindowQn> list9 = AliwwTalkWindowQn.GetList(0);
					if (!Util.IsEmptyList<AliwwTalkWindowQn>(list9))
					{
						foreach (AliwwTalkWindowQn aliwwTalkWindowQn in list9)
						{
							string userNick = aliwwTalkWindowQn.UserNick;
							if (!string.IsNullOrEmpty(userNick) && !list4.Contains(userNick))
							{
								ImKillManager.Kill(userNick, "原先的常挂账号变更为非常挂账号", false);
							}
						}
					}
				}
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00028AF0 File Offset: 0x00026CF0
		private void a(long A_0)
		{
			DataRow dataRow = AliwwMessageManager.Select(A_0);
			this.a(dataRow);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00028B0C File Offset: 0x00026D0C
		private string v()
		{
			return AppConfig.GetCurrentApplicationVersion();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00028B20 File Offset: 0x00026D20
		public void AliwwClient_OnUpgrade(bool failhostallmsg = false)
		{
			Form1.r r = new Form1.r();
			r.a = this;
			r.b = failhostallmsg;
			new Thread(new ThreadStart(r.c))
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00028B60 File Offset: 0x00026D60
		private void a(object A_0)
		{
			new Thread(new ThreadStart(this.c))
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00028B8C File Offset: 0x00026D8C
		private string d(int A_0)
		{
			string text = "";
			string text2;
			if (this.StopGetAgentWwMsgDateTime > DateTime.Now)
			{
				text = "助手已停止获取消息，将在" + this.StopGetAgentWwMsgDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "后重新获取消息";
				text2 = text;
			}
			else
			{
				base.Invoke(new Action(this.b));
				this.StopGetAgentWwMsgDateTime = DateTime.Now.AddMinutes((double)A_0);
				text2 = text;
			}
			return text2;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00028C08 File Offset: 0x00026E08
		private ErrCodeInfo a(long A_0, string A_1, string A_2, MsgSendSoftware A_3, bool A_4, string A_5)
		{
			return this.SendToHimOnAgent(A_0, A_1, A_3, A_4, A_5);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00028C28 File Offset: 0x00026E28
		private void a(object sender, HelpEventArgs e)
		{
			if (this.a7.SelectedIndex == 3)
			{
				Util.OpenLink("https://www.yuque.com/agiso/aldstb/wn7qdv");
			}
			else
			{
				Util.OpenLink("https://www.yuque.com/agiso/aldstb/iocvxt");
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00028C60 File Offset: 0x00026E60
		private void a(object sender, FormClosingEventArgs e)
		{
			if (!this.ak)
			{
				e.Cancel = true;
				base.WindowState = FormWindowState.Minimized;
				base.Visible = false;
				this.a4.Visible = true;
				this.a4.ShowBalloonTip(600000, "提示", "程序仍在后台执行，如果继续退出，将无法自动发送旺旺消息！", ToolTipIcon.Info);
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00028CB8 File Offset: 0x00026EB8
		private void e(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				DataGridView dataGridView = (DataGridView)sender;
				DataGridViewRow dataGridViewRow = dataGridView.Rows[e.RowIndex];
				string text = DbUtil.TrimNull(dataGridViewRow.Cells["AccountUserNick"].Value);
				if (!AppConfig.UserDict.ContainsKey(text))
				{
					MessageBox.Show("帐号不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					AldsAccountInfo aldsAccountInfo = AppConfig.UserDict[text];
					FormAldsAccountEdit formAldsAccountEdit = new FormAldsAccountEdit(aldsAccountInfo);
					if (formAldsAccountEdit.ShowDialog(this) == DialogResult.OK)
					{
						AldsAccountManager.Update(aldsAccountInfo);
						if (aldsAccountInfo.AutoSendOnOff && aldsAccountInfo.IsValid)
						{
							AppConfig.WwWebSocketClient.AddOnlineUser(aldsAccountInfo.UserNick, aldsAccountInfo.Password, AppConfig.ApplicationUuid);
							this.m();
						}
						else
						{
							WebSocketClient wwWebSocketClient = AppConfig.WwWebSocketClient;
							if (wwWebSocketClient != null)
							{
								wwWebSocketClient.RemoveOnlineUser(aldsAccountInfo.UserNick);
							}
							this.d(aldsAccountInfo.UserNick);
						}
					}
					if (aldsAccountInfo.IsCustomerServiceNewVersion)
					{
						AppConfig.GetUserCacheOrCreate(aldsAccountInfo.UserNick).CurrentWorksheet = CustomerServiceWorksheetManager.GetList(aldsAccountInfo.DefaultMouldId.Value, Util.GetMasterNick(aldsAccountInfo.UserNick));
					}
					else
					{
						AppConfig.GetUserCacheOrCreate(aldsAccountInfo.UserNick).CurrentWorksheet = new List<CustomerServiceWorksheet>();
					}
				}
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00028E00 File Offset: 0x00027000
		private void d(object sender, DataGridViewCellEventArgs e)
		{
			Form1.s s = new Form1.s();
			s.a = this;
			if (e.RowIndex >= 0)
			{
				DataGridView dataGridView = (DataGridView)sender;
				DataGridViewRow dataGridViewRow = dataGridView.Rows[e.RowIndex];
				string text = DbUtil.TrimNull(dataGridViewRow.Cells["AccountUserNick"].Value);
				if (!AppConfig.UserDict.ContainsKey(text))
				{
					MessageBox.Show("开关失败，帐号不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					s.b = AppConfig.UserDict[text];
					string name = dataGridView.Columns[e.ColumnIndex].Name;
					string text2 = name;
					string text3 = text2;
					if (!(text3 == "AccountVerifyResult"))
					{
						if (!(text3 == "AccountAutoReply"))
						{
							if (!(text3 == "AccountAutoSendOnOff"))
							{
								if (text3 == "Account_Edit")
								{
									this.e(sender, e);
								}
							}
							else
							{
								if (s.b.AutoSendOnOff)
								{
									DialogResult dialogResult = MessageBox.Show("关闭后，该帐号将不参与发送旺旺消息工作。是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
									if (dialogResult != DialogResult.Yes)
									{
										return;
									}
								}
								if (!s.b.IsValid && !s.b.AutoSendOnOff)
								{
									MessageBox.Show("未购买自动发货或自动发货已过期，不能开启发送开关。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								}
								else
								{
									s.b.AutoSendOnOff = !s.b.AutoSendOnOff;
									if (s.b.AutoSendOnOff && s.b.IsValid)
									{
										AppConfig.WwWebSocketClient.AddOnlineUser(s.b.UserNick, s.b.Password, AppConfig.ApplicationUuid);
										this.m();
									}
									else
									{
										WebSocketClient wwWebSocketClient = AppConfig.WwWebSocketClient;
										if (wwWebSocketClient != null)
										{
											wwWebSocketClient.RemoveOnlineUser(s.b.UserNick);
										}
										this.d(s.b.UserNick);
									}
									AldsAccountManager.Update(s.b);
								}
							}
						}
						else if (s.b.VersionNo < 3)
						{
							s.b.EnableAutoReply = false;
							MessageBox.Show("是否未登录成功？建议尝试退出助手重新打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						else if (s.b.EnableAutoReply)
						{
							s.b.AutoReplyOnOff = !s.b.AutoReplyOnOff;
							if (s.b.AutoReply)
							{
								Task.Run(new Action(s.c));
							}
							else
							{
								this.WriteLine(s.b.UserNick + "\t取消了智能答复");
							}
							AldsAccountManager.Update(s.b);
						}
					}
					else if (!s.b.IsValid && !s.b.EnableAutoReply)
					{
						MessageBox.Show(s.b.VerifyResult, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000290FC File Offset: 0x000272FC
		private void a(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				DataGridView dataGridView = (DataGridView)sender;
				string name = dataGridView.Columns[e.ColumnIndex].Name;
				string text = name;
				string text2 = text;
				if (!(text2 == "AccountVerifyResult"))
				{
					if (text2 == "DeadLineStr")
					{
						string text3 = e.Value.ToString();
						DateTime dateTime;
						if (!string.IsNullOrEmpty(text3) && DateTime.TryParse(text3, out dateTime))
						{
							if (dateTime <= DateTime.Now)
							{
								dataGridView.Rows[e.RowIndex].Cells[name].Style.ForeColor = Color.Red;
							}
							else
							{
								dataGridView.Rows[e.RowIndex].Cells[name].Style.ForeColor = Color.Black;
							}
						}
					}
				}
				else
				{
					object value = e.Value;
					string text4 = ((value != null) ? value.ToString() : null);
					if (!string.IsNullOrEmpty(text4) && !text4.Contains("成功"))
					{
						dataGridView.Rows[e.RowIndex].Cells[name].Style.ForeColor = Color.Red;
					}
					else
					{
						dataGridView.Rows[e.RowIndex].Cells[name].Style.ForeColor = Color.Black;
					}
				}
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00029280 File Offset: 0x00027480
		private void a(object sender, TabControlEventArgs e)
		{
			if (AppConfig.IsSellerLoginOnOwnComputer)
			{
				if (this.a7.SelectedIndex != 0 && (AppConfig.UserList == null || AppConfig.UserList.Count == 0))
				{
					MessageBox.Show("还没有设置帐户！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					this.m();
				}
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000292DC File Offset: 0x000274DC
		private void b(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				base.Visible = true;
				base.WindowState = FormWindowState.Normal;
				this.a4.Visible = true;
				WindowsAPI.SetForegroundWindow(base.Handle);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00002BC1 File Offset: 0x00000DC1
		private void a(object sender, MouseEventArgs e)
		{
			this.b(sender, e);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00029320 File Offset: 0x00027520
		private void g(object sender, LinkLabelLinkClickedEventArgs e)
		{
			List<VerificationCodeSMS> smsValidCode = AppConfig.GetSmsValidCode(false, "***手动点击获取***", null);
			if (smsValidCode != null && smsValidCode.Count > 0)
			{
				List<string> list = new List<string>();
				this.WriteLine("获取到以下验证码：");
				using (List<VerificationCodeSMS>.Enumerator enumerator = smsValidCode.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						VerificationCodeSMS verificationCodeSMS = enumerator.Current;
						string text = verificationCodeSMS.ReceiveTime.ToString("yyyyMMddHHmmss") + verificationCodeSMS.VerificationCode;
						if (!list.Contains(text))
						{
							list.Add(text);
							this.WriteLine(verificationCodeSMS.ReceiveTime.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + verificationCodeSMS.VerificationCode, false);
						}
					}
					return;
				}
			}
			this.WriteLine("没有获取到验证码");
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00029414 File Offset: 0x00027614
		private void f(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (AppConfig.AllowAutoLogin)
			{
				DialogResult dialogResult = MessageBox.Show("将结束所有AliApp进程，是否继续？", "确认清理", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
				if (dialogResult != DialogResult.No)
				{
					this.b(false);
				}
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00029454 File Offset: 0x00027654
		private void b(bool A_0 = false)
		{
			if (A_0)
			{
				Win32Extend.KillProcessByNameWithCmd("AliApp");
				Win32Extend.KillProcessByNameWithCmd("AliRender");
			}
			else
			{
				List<WindowInfo> allDesktopWindows = Win32Extend.GetAllDesktopWindows();
				List<WindowInfo> list = allDesktopWindows.Where(new Func<WindowInfo, bool>(Form1.<>c.<>9.e)).ToList<WindowInfo>();
				IEnumerable<int> enumerable = list.Select(new Func<WindowInfo, int>(Form1.<>c.<>9.d)).Distinct<int>();
				List<int> list2 = new List<int>();
				if (!Util.IsEmptyList<int>(enumerable))
				{
					foreach (int num in enumerable)
					{
						list2.AddRange(AppConfig.GetChildPidList((long)num, "aliApp.exe", 2).Distinct<int>());
					}
				}
				List<int> list3 = Process.GetProcessesByName("AliApp").Select(new Func<Process, int>(Form1.<>c.<>9.b)).ToList<int>();
				list3.AddRange(Process.GetProcessesByName("AliRender").Select(new Func<Process, int>(Form1.<>c.<>9.a)).ToList<int>());
				IEnumerable<int> enumerable2 = list3.Distinct<int>().Except(list2);
				if (!Util.IsEmptyList<int>(enumerable2))
				{
					foreach (int num2 in enumerable2)
					{
						Win32Extend.KillProcessById(num2, null);
					}
				}
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00029614 File Offset: 0x00027814
		public void ClearUnLongOpenQn(string excepUserNick = null)
		{
			try
			{
				List<QnAgentInfo> longOpenUsers = AppConfig.GetLongOpenUsers();
				if (longOpenUsers.Count > 0)
				{
					Form1.t t = new Form1.t();
					List<WindowInfo> allDesktopWindows = Win32Extend.GetAllDesktopWindows();
					t.a = new List<string>();
					List<WindowInfo> list = allDesktopWindows.Where(new Func<WindowInfo, bool>(t.b)).ToList<WindowInfo>();
					Dictionary<string, int> dictionary = list.ToDictionary(new Func<WindowInfo, string>(Form1.<>c.<>9.c), new Func<WindowInfo, int>(Form1.<>c.<>9.b));
					IEnumerable<string> enumerable = longOpenUsers.Select(new Func<QnAgentInfo, string>(Form1.<>c.<>9.b));
					IEnumerable<string> enumerable2 = dictionary.Keys.Except(enumerable).Where(new Func<string, bool>(this.a));
					if (!Util.IsEmptyList<string>(enumerable2))
					{
						LogWriter.WriteLog("清理非常挂用户", 1);
						foreach (string text in enumerable2)
						{
							if (string.IsNullOrEmpty(excepUserNick) || !(excepUserNick == text))
							{
								Win32Extend.KillProcessById(dictionary[text], null);
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00029798 File Offset: 0x00027998
		private void e(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (AppConfig.AllowAutoLogin)
			{
				DialogResult dialogResult = MessageBox.Show("将结束所有千牛、explorer进程，是否继续？", "确认清理", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
				if (dialogResult != DialogResult.No)
				{
					this.ClearAllQnProc(true, true, true, true, "手动结束所有千牛、explorer进程（忽略时间）");
				}
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000297E0 File Offset: 0x000279E0
		public void ClearAllQnProc(bool killExplorer = true, bool ignoreTimeCheck = false, bool allowKillQn = true, bool allowKillAliww = true, string reason = "")
		{
			bool flag;
			if (!ignoreTimeCheck)
			{
				if ((AppConfig.AgentSettings.SelectWeekDays ?? "").Contains(((int)DateTime.Now.DayOfWeek).ToString()))
				{
					int hour = DateTime.Now.Hour;
					int? num = AppConfig.AgentSettings.AllowAutoKillQnTimeFrom;
					if ((hour >= num.GetValueOrDefault()) & (num != null))
					{
						int hour2 = DateTime.Now.Hour;
						num = AppConfig.AgentSettings.AllowAutoKillQnTimeTo;
						flag = (hour2 <= num.GetValueOrDefault()) & (num != null);
						goto IL_0095;
					}
				}
				flag = false;
			}
			else
			{
				flag = true;
			}
			IL_0095:
			if (flag)
			{
				if (allowKillQn)
				{
					Win32Extend.KillProcessByName("AliWorkbench");
					Win32Extend.KillProcessByName("AliApp");
					Win32Extend.KillProcessByName("AliRender");
				}
				if (allowKillAliww)
				{
					Win32Extend.KillProcessByNameWithCmd("AliIM");
					Win32Extend.KillProcessByNameWithCmd("AliExternal");
				}
				if (killExplorer)
				{
					AppConfig.RestartExplorer();
				}
				Win32Extend.KillProcessByNameWithCmd("ChsIME");
				if (!string.IsNullOrEmpty(reason))
				{
					Form1.e(reason);
				}
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000298F4 File Offset: 0x00027AF4
		private void a(bool A_0 = true, bool A_1 = true, string A_2 = "")
		{
			try
			{
				if (AppConfig.AgentSettings.AllowAutoExitQn)
				{
					List<string> list = new List<string>();
					if (A_0)
					{
						list.Add("AliApp");
						list.Add("AliRender");
						list.Add("AliWorkbench");
					}
					if (A_1)
					{
						list.Add("AliIM");
						list.Add("AliExternal");
					}
					Dictionary<int, float> cpuUse = Win32Extend.GetCpuUse(list.ToArray());
					int num = 0;
					float num2 = 0f;
					foreach (KeyValuePair<int, float> keyValuePair in cpuUse)
					{
						if (num2 < keyValuePair.Value)
						{
							num2 = keyValuePair.Value;
							num = keyValuePair.Key;
						}
					}
					if (num > 0 && num2 > 20f)
					{
						Win32Extend.KillProcessById(num, null);
					}
					this.ClearAllQnProc(true, true, false, false, A_2);
				}
			}
			catch (Exception ex)
			{
				Form1.e(ex.ToString());
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00029A0C File Offset: 0x00027C0C
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (((msg.Msg == 256) | (msg.Msg == 260)) && keyData != Keys.Escape)
			{
				switch (keyData)
				{
				case Keys.F10:
					if (this.FormAliwwMsgQueueCountInstance.Visible)
					{
						this.FormAliwwMsgQueueCountInstance.Visible = false;
					}
					this.FormAliwwMsgQueueCountInstance.ShowDialog(this);
					break;
				case Keys.F11:
					if (AppConfig.AllowAutoLogin)
					{
						if (this.FormAgentSettionInstance.Visible)
						{
							this.FormAgentSettionInstance.Visible = false;
						}
						this.FormAgentSettionInstance.ShowDialog(this);
					}
					break;
				case Keys.F12:
					if (this.FormErrorLogInstance.Visible)
					{
						this.FormErrorLogInstance.Visible = false;
					}
					this.FormErrorLogInstance.Show(this);
					break;
				default:
					return base.ProcessCmdKey(ref msg, keyData);
				}
			}
			return false;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00029AE8 File Offset: 0x00027CE8
		private void ab(object sender, EventArgs e)
		{
			long num = Util.ToLong(this.by.Text);
			string text = this.bw.Text.Trim();
			DateTime value = this.bu.Value;
			int selectedIndex = this.bt.SelectedIndex;
			string masterNick = Util.GetMasterNick(this.c3.Text.Trim());
			DataTable dataTable = AliwwMessageManager.Select(num, masterNick, text, null, new DateTime?(value), selectedIndex);
			this.b(dataTable);
			if (dataTable != null)
			{
				this.cx.Text = string.Format("总数:{0}", dataTable.Rows.Count);
			}
			else
			{
				this.cx.Text = "总数:0";
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00029BA4 File Offset: 0x00027DA4
		private void aa(object sender, EventArgs e)
		{
			Form1.u u = new Form1.u();
			u.a = this;
			if (this.ap > DateTime.Today)
			{
				MessageBox.Show("只有在超过1小时没登录旺旺助手才需要同步，不信你点击右上角的“查询”看看！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				u.c = AppConfig.GetValidAndSwitchOnSellerNicks();
				if (Util.IsEmptyList<string>(u.c))
				{
					MessageBox.Show("账号列表为空或者发送开关未打开，无需同步！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					u.b = this.bu.Value.Date;
					Task.Run(new Action(u.d));
				}
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00029C44 File Offset: 0x00027E44
		private void z(object sender, EventArgs e)
		{
			Form1.v v = new Form1.v();
			v.a = this;
			if (this.aq > DateTime.Today)
			{
				MessageBox.Show("只有在超过1小时没登录旺旺助手才需要同步，不信你点击右上角的“查询”看看！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				v.c = AppConfig.GetValidAndSwitchOnSellerNicks();
				if (Util.IsEmptyList<string>(v.c))
				{
					MessageBox.Show("账号列表为空或者发送开关未打开，无需同步！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					v.b = this.bu.Value.Date;
					Task.Run(new Action(v.d));
				}
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00029CE4 File Offset: 0x00027EE4
		private void a(DateTime A_0, string A_1, bool A_2, List<string> A_3)
		{
			try
			{
				SyncAliwwMessageResponse syncAliwwMessageResponse = AppConfig.WwServiceClient.SyncAliwwMessage(A_0, A_1, new bool?(A_2), A_3);
				if (syncAliwwMessageResponse.IsError)
				{
					string text = (A_2 ? "同步未发旺旺消息时失败" : "同步全部旺旺消息时失败") + "，失败原因：" + syncAliwwMessageResponse.ErrMsg;
					MessageBox.Show(text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else if (syncAliwwMessageResponse.Message == null || syncAliwwMessageResponse.Message.Count <= 0)
				{
					MessageBox.Show(string.Format("{0}无消息可同步！", A_0.ToString("yyyy-MM-dd")), "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					if (A_2)
					{
						this.aq = DateTime.Now;
					}
					else
					{
						this.ap = DateTime.Now;
					}
					if (syncAliwwMessageResponse.Message != null)
					{
						AliwwMessageManager.Insert(syncAliwwMessageResponse.Message);
					}
					base.Invoke(new EventHandler(this.c));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("同步消息异常了，请稍后重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Form1.e(ex.ToString());
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00002BCB File Offset: 0x00000DCB
		private void y(object sender, EventArgs e)
		{
			this.a(this.CurrentRowMsgDgvSelect);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00029E0C File Offset: 0x0002800C
		private void x(object sender, EventArgs e)
		{
			List<long> list = new List<long>();
			foreach (object obj in this.ce.SelectedItems)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				long num = DbUtil.TrimLongNull(listViewItem.SubItems["MsgId"].Text);
				if (!list.Contains(num))
				{
					list.Add(num);
					if (num != 0L)
					{
						DataRow dataRow = AliwwMessageManager.Select(num);
						if (dataRow == null)
						{
							long num2 = DbUtil.TrimLongNull(listViewItem.SubItems["Tid"].Text);
							this.WriteLine(string.Format("手动补发送时失败，订单号{0}，未找到消息内容。", num2));
							return;
						}
						this.a(dataRow);
					}
				}
			}
			this.ce.Select();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00029F00 File Offset: 0x00028100
		private void a(DataRow A_0)
		{
			if (A_0 != null)
			{
				AliwwMessageInfo aliwwMessageInfo = AliwwMessageInfo.Map(A_0);
				aliwwMessageInfo.AliwwMessageSourceType = EnumAliwwMessageSource.FromDbForSendOnceMore;
				aliwwMessageInfo.EnqueueTime = DateTime.Now;
				AppConfig.GetSellerExecuteCache(aliwwMessageInfo.SellerNick).AliwwMsgQueue.Enqueue(aliwwMessageInfo);
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00029F44 File Offset: 0x00028144
		private void b(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (e.IsSelected)
			{
				long num = Util.ToLong(e.Item.SubItems["MsgId"].Text);
				if (num == 0L)
				{
					this.cs.Text = "";
					this.ct.Items.Clear();
				}
				else
				{
					this.CurrentRowMsgDgvSelect = AliwwMessageManager.Select(num);
					if (this.CurrentRowMsgDgvSelect != null)
					{
						string text = DbUtil.TrimNull(this.CurrentRowMsgDgvSelect["MessageBody"]);
						string text2 = DbUtil.TrimNull(this.CurrentRowMsgDgvSelect["BuyerNick"]);
						text = text.Replace("\r\n", "\n").Replace("\n", "\r\n");
						this.cs.Text = text;
						this.cq.Text = text2;
					}
					DataTable dataTable = LogSendResultManager.Select(num);
					this.a(dataTable);
				}
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0002A04C File Offset: 0x0002824C
		private void u()
		{
			this.ce.Columns.Add("消息ID", 70, HorizontalAlignment.Left);
			this.ce.Columns.Add("订单号", 120, HorizontalAlignment.Center);
			this.ce.Columns.Add("卖家", 120, HorizontalAlignment.Left);
			this.ce.Columns.Add("买家", 120, HorizontalAlignment.Left);
			this.ce.Columns.Add("消息时间", 70, HorizontalAlignment.Center);
			this.ce.Columns.Add("记录时间", 70, HorizontalAlignment.Center);
			this.ce.Columns.Add("结果", 60, HorizontalAlignment.Center);
			this.ce.Columns.Add("BuyerOpenUid", 0, HorizontalAlignment.Center);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0002A128 File Offset: 0x00028328
		private void b(DataTable A_0)
		{
			this.ce.Items.Clear();
			if (A_0 != null && A_0.Rows.Count != 0)
			{
				for (int i = 0; i < A_0.Rows.Count; i++)
				{
					DataRow dataRow = A_0.Rows[i];
					ListViewItem listViewItem = new ListViewItem(new ListViewItem.ListViewSubItem[]
					{
						new ListViewItem.ListViewSubItem
						{
							Name = "MsgId",
							Text = dataRow["MsgId"].ToString()
						},
						new ListViewItem.ListViewSubItem
						{
							Name = "Tid",
							Text = dataRow["Tid"].ToString()
						},
						new ListViewItem.ListViewSubItem
						{
							Name = "SellerNick",
							Text = dataRow["SellerNick"].ToString()
						},
						new ListViewItem.ListViewSubItem
						{
							Name = "BuyerNick",
							Text = dataRow["BuyerNick"].ToString()
						},
						new ListViewItem.ListViewSubItem
						{
							Name = "CreateTime",
							Text = DbUtil.TrimDateNull(dataRow["CreateTime"]).ToString("HH:mm:ss")
						},
						new ListViewItem.ListViewSubItem
						{
							Name = "CreateTimeLocal",
							Text = DbUtil.TrimDateNull(dataRow["CreateTimeLocal"]).ToString("HH:mm:ss")
						},
						new ListViewItem.ListViewSubItem
						{
							Name = "ExistsSucc",
							Text = ((DbUtil.TrimIntNull(dataRow["ExistsSucc"]) > 0) ? "成功" : " ")
						},
						new ListViewItem.ListViewSubItem
						{
							Name = "BuyerOpenUid",
							Text = DbUtil.TrimNull(dataRow["BuyerOpenUid"])
						}
					}, -1);
					this.ce.Items.Add(listViewItem);
				}
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0002A330 File Offset: 0x00028530
		private void a(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (e.IsSelected)
			{
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0002A348 File Offset: 0x00028548
		private void w(object sender, EventArgs e)
		{
			if (this.ct.SelectedItems.Count != 0)
			{
				string text = this.ct.SelectedItems[0].SubItems["SendResultCode"].Text;
				if (string.IsNullOrEmpty(text) || !"成功".Equals(text))
				{
					string text2 = DbUtil.TrimNull(this.ct.SelectedItems[0].SubItems["SendResultMsg"].Text);
					MessageBox.Show(text2);
				}
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0002A3E0 File Offset: 0x000285E0
		private void t()
		{
			this.ct.Columns.Add("记录时间", 100, HorizontalAlignment.Center);
			this.ct.Columns.Add("结果", 60, HorizontalAlignment.Center);
			this.ct.Columns.Add("描述", 450, HorizontalAlignment.Left);
			this.ct.Columns.Add("发送者", 60, HorizontalAlignment.Left);
			if (AppConfig.AllowAutoLogin)
			{
				this.ct.Columns.Add("发送软件", 70, HorizontalAlignment.Center);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0002A47C File Offset: 0x0002867C
		private void a(DataTable A_0)
		{
			this.ct.Items.Clear();
			if (A_0 != null && A_0.Rows.Count != 0)
			{
				bool flag = A_0.Columns.Contains("UserNick");
				for (int i = 0; i < A_0.Rows.Count; i++)
				{
					DataRow dataRow = A_0.Rows[i];
					string text = (flag ? DbUtil.TrimNull(dataRow["UserNick"]) : "");
					ListViewItem listViewItem = new ListViewItem(new ListViewItem.ListViewSubItem[]
					{
						new ListViewItem.ListViewSubItem
						{
							Name = "CreateTimeLocal",
							Text = DbUtil.TrimDateNull(dataRow["CreateTimeLocal"]).ToString("MM-dd HH:mm:ss")
						},
						new ListViewItem.ListViewSubItem
						{
							Name = "SendResultCode",
							Text = ((DbUtil.TrimIntNull(dataRow["SendResultCode"]) > 0) ? "成功" : "")
						},
						new ListViewItem.ListViewSubItem
						{
							Name = "SendResultMsg",
							Text = DbUtil.TrimNull(dataRow["SendResultMsg"])
						},
						new ListViewItem.ListViewSubItem
						{
							Name = "UserNick",
							Text = text
						}
					}, -1);
					if (AppConfig.AllowAutoLogin)
					{
						MsgSendSoftware msgSendSoftware = (MsgSendSoftware)DbUtil.TrimIntNull(dataRow["SendSoftware"]);
						listViewItem.SubItems.Add(Util.GetEnumDescription(msgSendSoftware));
					}
					this.ct.Items.Add(listViewItem);
				}
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0002A61C File Offset: 0x0002881C
		private void s()
		{
			this.cd.Items.Clear();
			this.cd.Items.Add("【全部】");
			this.cd.Items.Add("【适用所有已添加的旺旺】");
			foreach (AldsAccountInfo aldsAccountInfo in AppConfig.UserList)
			{
				if (aldsAccountInfo.EnableAutoReply)
				{
					if (AppConfig.CurrentSystemSettingInfo.AutoReplyBySellerNick)
					{
						string masterNick = Util.GetMasterNick(aldsAccountInfo.UserNick);
						if (!this.cd.Items.Contains(masterNick))
						{
							this.cd.Items.Add(masterNick);
						}
					}
					else
					{
						this.cd.Items.Add(aldsAccountInfo.UserNick);
					}
				}
			}
			this.cd.SelectedIndex = 0;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0002A710 File Offset: 0x00028910
		private void v(object sender, EventArgs e)
		{
			string text = this.b5.Text.Trim();
			string text2 = this.cd.SelectedItem.ToString();
			if ((string.IsNullOrEmpty(text2) || text2.Equals("【全部】")) && string.IsNullOrEmpty(text))
			{
				this.a(AppConfig.AutoReplyList);
			}
			else
			{
				List<AutoReplyInfo> list = AutoReplyManager.Select(text, text2);
				this.a(list);
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0002A77C File Offset: 0x0002897C
		private void u(object sender, EventArgs e)
		{
			if (this.cc.SelectedCells != null && this.cc.SelectedCells.Count > 0)
			{
				int rowIndex = this.cc.SelectedCells[0].RowIndex;
				DataGridViewRow dataGridViewRow = this.cc.Rows[rowIndex];
				long num = DbUtil.TrimLongNull(dataGridViewRow.Cells["ArIdNo"].Value);
				if (num <= 0L)
				{
					this.b9.Text = "";
				}
				else
				{
					AutoReplyInfo autoReplyInfo = AutoReplyManager.Select(num);
					this.a(autoReplyInfo, false);
				}
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0002A828 File Offset: 0x00028A28
		private void t(object sender, EventArgs e)
		{
			if (AppConfig.AutoReplyList.Count >= 1000)
			{
				MessageBox.Show(string.Format("自动应答设置的关键词太多了，不能超过{0}个。", 1000), "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				AutoReplyInfo autoReplyInfo = new AutoReplyInfo();
				this.a(autoReplyInfo, true);
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0002A880 File Offset: 0x00028A80
		private void s(object sender, EventArgs e)
		{
			if (this.cc.SelectedRows.Count == 0 && this.cc.SelectedCells.Count == 0)
			{
				MessageBox.Show("请选中要删除的行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				DialogResult dialogResult = MessageBox.Show("是否确认删除！", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
				if (dialogResult != DialogResult.No)
				{
					foreach (object obj in this.cc.SelectedCells)
					{
						DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
						this.cc.Rows[dataGridViewCell.RowIndex].Selected = true;
					}
					foreach (object obj2 in this.cc.SelectedRows)
					{
						DataGridViewRow dataGridViewRow = (DataGridViewRow)obj2;
						AutoReplyManager.Delete(Util.ToLong(dataGridViewRow.Cells["ArIdNo"].Value));
					}
					AppConfig.InitAutoReplyList();
					this.v(null, null);
				}
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0002A9E0 File Offset: 0x00028BE0
		private void r(object sender, EventArgs e)
		{
			if (this.FormLogAutoReplyInstance.Visible)
			{
				this.FormLogAutoReplyInstance.Visible = false;
			}
			this.FormLogAutoReplyInstance.Show(this);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0002AA14 File Offset: 0x00028C14
		private void c(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				DataGridView dataGridView = (DataGridView)sender;
				DataGridViewRow dataGridViewRow = dataGridView.Rows[e.RowIndex];
				long num = DbUtil.TrimLongNull(dataGridViewRow.Cells["ArIdNo"].Value);
				if (num <= 0L)
				{
					this.b9.Text = "";
				}
				else
				{
					AutoReplyInfo autoReplyInfo = AutoReplyManager.Select(num);
					if (autoReplyInfo != null)
					{
						this.b9.Text = autoReplyInfo.ReplyWord;
						this.b9.Text = this.b9.Text.Replace("\r\n", "\n").Replace("\n", "\r\n");
					}
				}
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0002AAE0 File Offset: 0x00028CE0
		private void b(object sender, DataGridViewCellEventArgs e)
		{
			if (e != null && e.RowIndex >= 0)
			{
				DataGridView dataGridView = (DataGridView)sender;
				DataGridViewRow dataGridViewRow = dataGridView.Rows[e.RowIndex];
				long num = DbUtil.TrimLongNull(dataGridViewRow.Cells["ArIdNo"].Value);
				if (num <= 0L)
				{
					this.b9.Text = "";
				}
				else
				{
					AutoReplyInfo autoReplyInfo = AutoReplyManager.Select(num);
					this.a(autoReplyInfo, false);
				}
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0002AB68 File Offset: 0x00028D68
		private void a(AutoReplyInfo A_0, bool A_1 = false)
		{
			if (A_0 != null)
			{
				FormAutoReplyEdit formAutoReplyEdit = new FormAutoReplyEdit(A_0, A_1);
				DialogResult dialogResult = formAutoReplyEdit.ShowDialog(this);
				if (dialogResult == DialogResult.OK)
				{
					int num = AutoReplyManager.InserOrUpdate(A_0);
					if (num < 1)
					{
						MessageBox.Show("保存失败，可能已存在【相同关键词-相同答复语】的项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						this.a(A_0, A_1);
					}
					else
					{
						AppConfig.InitAutoReplyList();
						if (A_0.IdNo > 0L)
						{
							this.l();
						}
						else
						{
							this.v(null, null);
						}
					}
				}
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0002ABE8 File Offset: 0x00028DE8
		private void r()
		{
			this.g = new Thread(new ParameterizedThreadStart(this.e));
			this.g.Priority = ThreadPriority.Highest;
			this.g.SetApartmentState(ApartmentState.STA);
			this.g.IsBackground = true;
			this.g.Name = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Guid.NewGuid().ToString();
			if (!this.g.IsAlive)
			{
				this.g.Start();
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0002AC84 File Offset: 0x00028E84
		private int q()
		{
			int num = 0;
			using (IEnumerator<AldsAccountInfo> enumerator = AppConfig.UserList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Form1.w w = new Form1.w();
					w.b = this;
					w.a = enumerator.Current;
					if (!w.a.IsValid)
					{
						num++;
					}
					else if (w.a.AutoReply)
					{
						Task.Run(new Action(w.c));
					}
				}
			}
			return num;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0002AD18 File Offset: 0x00028F18
		private string f(string A_0)
		{
			if (this.ac >= 1)
			{
				A_0 = A_0.Replace("扣扣", "某q").Replace("好评", "hao ping");
			}
			if (this.ac >= 2)
			{
				A_0 = A_0.Replace("http://", "").Replace("确认收货", "收货");
			}
			return A_0;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0002AD90 File Offset: 0x00028F90
		private void p()
		{
			try
			{
				foreach (KeyValuePair<string, RobotAlitwScanInfo> keyValuePair in this.aj)
				{
					if (keyValuePair.Value.DeadLine > DateTime.Now)
					{
						this.aj.Remove(keyValuePair.Key);
					}
				}
			}
			catch (Exception ex)
			{
				Form1.e(ex.ToString());
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0002AE28 File Offset: 0x00029028
		private string c(int A_0)
		{
			Random random = new Random();
			string text;
			if (A_0 != 1)
			{
				if (A_0 != 2)
				{
					text = "";
				}
				else
				{
					int num = random.Next(0, this.ai.Length);
					text = this.ai[num];
				}
			}
			else
			{
				int num = random.Next(0, this.ah.Length);
				text = this.ah[num];
			}
			return text;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0002AE8C File Offset: 0x0002908C
		private int a(AliwwTalkWindow A_0, string A_1, string A_2, string A_3, string A_4, out string A_5)
		{
			int num;
			if (string.IsNullOrEmpty(A_1) || string.IsNullOrEmpty(A_2) || string.IsNullOrEmpty(A_4))
			{
				A_5 = "该填写的还是要填写的！";
				num = -1;
			}
			else
			{
				ErrCodeInfo errCodeInfo = A_0.SendToTalkWindowWholeMsg(A_2, A_4, A_3, "cntaobao");
				A_5 = errCodeInfo.ToString();
				num = errCodeInfo.ErrCode.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0002AEF4 File Offset: 0x000290F4
		public ErrCodeInfo Send(string userNick, string buyerNick, string buyerOpenUid, string msg)
		{
			ErrCodeInfo errCodeInfo;
			if (string.IsNullOrEmpty(userNick) || string.IsNullOrEmpty(buyerNick) || string.IsNullOrEmpty(msg))
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.UserNickOrMsgBodyIsNull);
			}
			else
			{
				userNick = userNick.Trim();
				buyerNick = buyerNick.Trim();
				msg = msg.Trim();
				Aliww aliww = new Aliww(userNick);
				aliww.CloseCustomerBenchWindowBeforeSend = false;
				aliww.OnSendToUserSuccess += Form1.<>c.<>9.a;
				ErrCodeInfo errCodeInfo2 = aliww.SendToUser(buyerNick, buyerOpenUid, msg, "cntaobao");
				errCodeInfo = errCodeInfo2;
			}
			return errCodeInfo;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0002AF88 File Offset: 0x00029188
		public void RestartSendAndAutoReplyThread()
		{
			try
			{
				Form1.l = null;
				try
				{
					Form1.e("开始中止发消息队列线程。");
					Task.Run(new Action(this.g.Abort));
					if (!Util.CheckWait(3000, new Func<bool>(this.a), 100))
					{
						if (AppConfig.AllowAutoLogin)
						{
							AppConfig.QnAgentServiceClient.AutoLoginError("agiso", null, "终止发货线程超时，请注意服务器", 268435455L, "", "");
						}
						Form1.e("终止发货线程超时");
					}
				}
				catch (Exception ex)
				{
					Form1.e("中止线程异常，" + ex.ToString());
					if (Form1.a2++ >= 3)
					{
						Form1.a2 = 0;
						this.WriteLine("重启发货线程异常，如发货异常，请尝试重启千牛。多次出现，可联系在线客服寻求帮助。");
					}
				}
				finally
				{
					Form1.e("重启发消息队列线程。");
					this.r();
				}
			}
			catch (Exception ex2)
			{
				Form1.e(ex2.ToString());
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00002BD9 File Offset: 0x00000DD9
		public void WriteLine(string txtLine)
		{
			this.WriteLine(txtLine, true);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0002B0AC File Offset: 0x000292AC
		private static void e(string A_0)
		{
			LogWriter.WriteLog(A_0.Trim(), 1);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00002BE3 File Offset: 0x00000DE3
		public void ShowPermitMessageBox()
		{
			MessageBox.Show("保存失败，你的Window系统对权限级别设置较高。请手动按以下步骤设置权限：\r\n\r\n1、进入安装目录 --》\r\n2、右键空白处 --》\r\n3、属性 --》\r\n4、选择“安全”选项卡 --》\r\n5、点击“编辑”后弹出权限修改窗口 --》\r\n6、点击“添加” --》\r\n7、输入“everyone”后点确定，回到权限修改窗口 --》\r\n8、勾选“完全控制” --》\r\n9、再一路确定就可以了。\r\n\r\n最后如果还不行，请退出程序后：\r\nShift + 右键桌面图标--》以管理员身份运行", "失败提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0002B0C8 File Offset: 0x000292C8
		private void d(string A_0)
		{
			string masterNick = Util.GetMasterNick(A_0);
			SellerCache sellerExecuteCache = AppConfig.GetSellerExecuteCache(masterNick);
			if (!string.IsNullOrEmpty(sellerExecuteCache.LastSuccessSendUserNick) && sellerExecuteCache.LastSuccessSendUserNick.Equals(A_0))
			{
				sellerExecuteCache.LastSuccessSendUserNick = "";
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0002B10C File Offset: 0x0002930C
		private void a(string A_0, string A_1, string A_2)
		{
			string text = A_0.ToLower();
			string text2 = text;
			if (!(text2 == "text"))
			{
				if (!(text2 == "html"))
				{
					if (text2 == "url")
					{
						new Form2().ShowInfo(A_1, A_2.Trim(), InfoBoxContentType.Url);
					}
				}
				else
				{
					new Form2().ShowInfo(A_1, A_2.Trim(), InfoBoxContentType.Html);
				}
			}
			else
			{
				new Form2().ShowInfo(A_1, A_2.Trim(), InfoBoxContentType.Text);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0002B188 File Offset: 0x00029388
		public void WriteLine(string str, bool addTime)
		{
			Form1.x x = new Form1.x();
			x.a = this;
			x.b = str;
			if (x.b == null)
			{
				x.b = "";
			}
			if (addTime)
			{
				x.b = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + x.b + "\r\n";
			}
			else
			{
				x.b += "\r\n";
			}
			Action<string> action = new Action<string>(x.c);
			if (base.InvokeRequired)
			{
				base.Invoke(action, new object[] { x.b });
			}
			else
			{
				action(x.b);
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0002B248 File Offset: 0x00029448
		private void b(int A_0)
		{
			Action<int> action = new Action<int>(this.a);
			if (base.InvokeRequired)
			{
				base.Invoke(action, new object[] { A_0 });
			}
			else
			{
				action(A_0);
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00002BFA File Offset: 0x00000DFA
		private void q(object sender, EventArgs e)
		{
			this.bc.Clear();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0002B28C File Offset: 0x0002948C
		private void p(object sender, EventArgs e)
		{
			this.o();
			this.RoolbackHostAllMsg();
			AgentRemoteHttpListener agentRemoteHttpListener = this.@as;
			if (agentRemoteHttpListener != null)
			{
				agentRemoteHttpListener.Stop();
			}
			AgentHttpListener agentHttpListenerInstance = Form1.AgentHttpListenerInstance;
			if (agentHttpListenerInstance != null)
			{
				agentHttpListenerInstance.Stop();
			}
			WebSocketServerIns aliwwWebScoketServer = AppConfig.AliwwWebScoketServer;
			if (aliwwWebScoketServer != null)
			{
				aliwwWebScoketServer.Stop();
			}
			Thread.Sleep(500);
			this.ak = true;
			Application.Exit();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00002C07 File Offset: 0x00000E07
		private void o(object sender, EventArgs e)
		{
			this.am(sender, e);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00002C11 File Offset: 0x00000E11
		private void n(object sender, EventArgs e)
		{
			this.b(null, null);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0002B2EC File Offset: 0x000294EC
		private void a(MsgSendSoftware A_0 = MsgSendSoftware.Undefined)
		{
			if (this.ce.SelectedItems.Count <= 0)
			{
				MessageBox.Show("请选中要联系的对象！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else if (this.ce.SelectedItems.Count <= 1 || MessageBox.Show("选中了多个对象，这样将会同时打开多个聊天窗口，确定要这样做？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
			{
				string text = "";
				using (IEnumerator enumerator = this.ce.SelectedItems.GetEnumerator())
				{
					IL_028D:
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						ListViewItem listViewItem = (ListViewItem)obj;
						long num = Util.ToLong(listViewItem.SubItems["MsgId"].Text);
						string text2 = listViewItem.SubItems["SellerNick"].Text;
						string text3 = listViewItem.SubItems["BuyerNick"].Text;
						string text4 = listViewItem.SubItems["BuyerOpenUid"].Text;
						if (AppConfig.AllowAutoLogin)
						{
							string masterNick = Util.GetMasterNick(text2);
							QnAgentInfo agentInfo = AppConfig.GetAgentInfo(masterNick);
							if (agentInfo == null)
							{
								text = text + text2 + "：代挂账号不存在了" + Environment.NewLine;
							}
							else
							{
								DataTable dataTable = LogSendResultManager.Select(num);
								if (dataTable != null && dataTable.Rows != null && dataTable.Rows.Count > 0)
								{
									string text5 = "";
									int i = dataTable.Rows.Count - 1;
									while (i >= 0)
									{
										DataRow dataRow = dataTable.Rows[i];
										if (DbUtil.TrimIntNull(dataRow["SendResultCode"]) != 201)
										{
											i--;
										}
										else
										{
											text5 = DbUtil.TrimNull(dataRow["UserNick"]);
											IL_01CE:
											if (!string.IsNullOrEmpty(text5) && agentInfo.QnNick != text5)
											{
												text = string.Concat(new string[]
												{
													text,
													"发送记录对应的“",
													text2,
													"”已不存在，当前的代挂账号：",
													agentInfo.QnNick,
													Environment.NewLine
												});
												goto IL_028D;
											}
											goto IL_0225;
										}
									}
									goto IL_01CE;
								}
								IL_0225:
								this.SendToHimOnAgent(num, agentInfo.QnNick, A_0, false, "");
							}
						}
						else
						{
							string masterNick2 = Util.GetMasterNick(text2);
							string text6 = "";
							if (string.IsNullOrEmpty(text6))
							{
								AldsAccountInfo accountInfo = AppConfig.GetAccountInfo(masterNick2);
								if (accountInfo == null)
								{
									continue;
								}
								text6 = accountInfo.UserNick;
							}
							if (!string.IsNullOrEmpty(text6))
							{
								AppConfig.ProcessStartQn(text6, text3, text4, "cntaobao");
							}
						}
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					MessageBox.Show(text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0002B5E0 File Offset: 0x000297E0
		public ErrCodeInfo SendToHimOnAgent(long msgId, string userNick, MsgSendSoftware appointSendSoftware, bool isNeedScreenshot = false, string fileName = "")
		{
			this.DictKeepAliveForSomeTime[userNick] = DateTime.Now.AddMinutes(2.0);
			DataRow dataRow = AliwwMessageManager.Select(msgId);
			ErrCodeInfo errCodeInfo;
			if (dataRow == null)
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.Undefined);
			}
			else
			{
				AliwwMessageInfo aliwwMessageInfo = AliwwMessageInfo.Map(dataRow);
				aliwwMessageInfo.IsNeedScreenshot = isNeedScreenshot;
				aliwwMessageInfo.SendSoftware = appointSendSoftware;
				aliwwMessageInfo.FileName = fileName;
				aliwwMessageInfo.AliwwMessageSourceType = EnumAliwwMessageSource.FromCallUserOnly;
				aliwwMessageInfo.EnqueueTime = DateTime.Now;
				AppConfig.GetSellerExecuteCache(Util.GetMasterNick(userNick)).AliwwMsgQueue.Enqueue(aliwwMessageInfo);
				errCodeInfo = new ErrCodeInfo(ErrCodeType.Undefined);
			}
			return errCodeInfo;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00002C1B File Offset: 0x00000E1B
		private void m(object sender, EventArgs e)
		{
			this.a(MsgSendSoftware.Undefined);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00002C24 File Offset: 0x00000E24
		private void l(object sender, EventArgs e)
		{
			this.a(MsgSendSoftware.QN);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00002C2D File Offset: 0x00000E2D
		private void k(object sender, EventArgs e)
		{
			this.a(MsgSendSoftware.QN604);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00002C36 File Offset: 0x00000E36
		private void j(object sender, EventArgs e)
		{
			this.a(MsgSendSoftware.AliwwBuyer9);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0002B678 File Offset: 0x00029878
		private void i(object sender, EventArgs e)
		{
			if (this.ce.SelectedItems.Count <= 0)
			{
				MessageBox.Show("请选中要联系的对象！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else if (this.ce.SelectedItems.Count <= 1 || MessageBox.Show("选中了多个对象，这样将会同时打开多个聊天窗口，确定要这样做？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
			{
				foreach (object obj in this.ce.SelectedItems)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					string text = listViewItem.SubItems["SellerNick"].Text;
					string masterNick = Util.GetMasterNick(text);
					AppConfig.AliwwMsgQueueFirst.Enqueue(new AliwwMessageInfo
					{
						MsgId = AppConfig.GetRandomMsgId(),
						SellerNick = masterNick,
						BuyerNick = AppConfig.RobotUserNick,
						BuyerOpenUid = AppConfig.RobotOpenUid,
						MessageBody = AppConfig.DefaultTestSendMsgBody,
						CreateTime = DateTime.Now,
						CreateTimeLocal = DateTime.Now,
						AliwwMessageSourceType = EnumAliwwMessageSource.FromTestSend,
						EnqueueTime = DateTime.Now
					});
				}
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00002C43 File Offset: 0x00000E43
		private void d(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Util.OpenLink("https://fuwu.taobao.com/ser/detail.html?service_code=ts-11973");
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0002B7D0 File Offset: 0x000299D0
		public static void MessageBoxShowErrCode(ErrCodeInfo resultCode)
		{
			if (resultCode.ErrCode.GetHashCode() <= 0)
			{
				string text = resultCode.ToString();
				List<string> list = Util.FindUrlsFrom(text);
				if (list != null && list.Count > 0)
				{
					DialogResult dialogResult = MessageBox.Show(text + "\r\n\r\n是否立即打开相应网址？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
					if (dialogResult != DialogResult.Yes)
					{
						return;
					}
					using (List<string>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text2 = enumerator.Current;
							Util.OpenLink(text2);
						}
						return;
					}
				}
				MessageBox.Show(text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00002C50 File Offset: 0x00000E50
		private void h(object sender, EventArgs e)
		{
			new FormSystemSetting().ShowDialog();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0002B890 File Offset: 0x00029A90
		private void g(object sender, EventArgs e)
		{
			if (!AppConfig.AllowAutoLogin)
			{
				string text = null;
				if (this.bl.SelectedCells.Count > 0 || this.bl.SelectedRows.Count > 0)
				{
					foreach (object obj in this.bl.SelectedCells)
					{
						DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
						this.bl.Rows[dataGridViewCell.RowIndex].Selected = true;
					}
					if (this.bl.SelectedRows.Count > 0)
					{
						DataGridViewCell dataGridViewCell2 = this.bl.SelectedRows[0].Cells["AccountUserNick"];
						if (dataGridViewCell2 != null)
						{
							text = dataGridViewCell2.Value.ToString();
						}
					}
				}
				new FormTestSend(text).ShowDialog();
			}
			else
			{
				new FormTestSend(null).ShowDialog();
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00002C5D File Offset: 0x00000E5D
		private void c(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.AliwwClient_OnUpgrade(false);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00002C66 File Offset: 0x00000E66
		private void b(object sender, LinkLabelLinkClickedEventArgs e)
		{
			RepairQnAliresManager.RepairAlires();
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0002B9AC File Offset: 0x00029BAC
		private void f(object sender, EventArgs e)
		{
			if (AppConfig.AutoReplyList == null)
			{
				MessageBox.Show("智能答复为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Filter = "Csv文件(*.csv)|*.csv";
				saveFileDialog.Title = "导出智能答复";
				saveFileDialog.FileName = "旺旺发送助手智能答复";
				saveFileDialog.AddExtension = true;
				saveFileDialog.RestoreDirectory = true;
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					string fileName = saveFileDialog.FileName;
					try
					{
						using (StreamWriter streamWriter = new StreamWriter(fileName))
						{
							using (CsvWriter csvWriter = new CsvWriter(streamWriter))
							{
								csvWriter.WriteField("类型");
								csvWriter.WriteField("关键词");
								csvWriter.WriteField("关联号");
								csvWriter.WriteField("回复语");
								csvWriter.WriteField("答复时间起始");
								csvWriter.WriteField("答复时间结束");
								csvWriter.WriteField("优先级");
								csvWriter.NextRecord();
								string text = DateTime.Now.Date.ToString("HH:mm");
								string text2 = DateTime.Now.Date.AddSeconds(-1.0).ToString("HH:mm");
								foreach (AutoReplyInfo autoReplyInfo in AppConfig.AutoReplyList)
								{
									csvWriter.WriteField(autoReplyInfo.TypeText);
									csvWriter.WriteField(autoReplyInfo.KeyWord);
									csvWriter.WriteField(autoReplyInfo.SellerNick);
									csvWriter.WriteField(autoReplyInfo.ReplyWord);
									CsvWriter csvWriter2 = csvWriter;
									DateTime? dateTime = autoReplyInfo.ArStartTime;
									DateTime dateTime2 = DateTime.MinValue;
									string text3;
									if (!(dateTime == dateTime2))
									{
										dateTime = autoReplyInfo.ArStartTime;
										text3 = ((dateTime != null) ? dateTime.GetValueOrDefault().ToString("HH:mm") : null);
									}
									else
									{
										text3 = text;
									}
									csvWriter2.WriteField(text3);
									CsvWriter csvWriter3 = csvWriter;
									dateTime = autoReplyInfo.ArEndTime;
									dateTime2 = DateTime.MinValue;
									string text4;
									if (!(dateTime == dateTime2))
									{
										dateTime = autoReplyInfo.ArEndTime;
										text4 = ((dateTime != null) ? dateTime.GetValueOrDefault().ToString("HH:mm") : null);
									}
									else
									{
										text4 = text2;
									}
									csvWriter3.WriteField(text4);
									csvWriter.WriteField<long>(autoReplyInfo.Grade);
									csvWriter.NextRecord();
								}
							}
						}
					}
					catch (Exception ex)
					{
						Form1.e(ex.ToString());
						MessageBox.Show("导出出错，" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
					MessageBox.Show("导出成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
				}
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0002BCFC File Offset: 0x00029EFC
		private void e(object sender, EventArgs e)
		{
			if (AppConfig.AutoReplyList != null && AppConfig.AutoReplyList.Count >= 1000)
			{
				MessageBox.Show(string.Format("智能答复已达上限-{0}，无法导入", 1000));
			}
			else if (MessageBox.Show("开始导入数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) != DialogResult.Cancel)
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "Csv文件(*.csv)|*.csv";
				openFileDialog.Title = "导入智能答复";
				openFileDialog.RestoreDirectory = true;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						int num = ((AppConfig.AutoReplyList == null) ? 0 : AppConfig.AutoReplyList.Count);
						List<AutoReplyInfo> list = new List<AutoReplyInfo>();
						Encoding type = EncodingType.GetType(openFileDialog.FileName);
						using (StreamReader streamReader = new StreamReader(openFileDialog.FileName, type))
						{
							using (CsvReader csvReader = new CsvReader(streamReader))
							{
								csvReader.Configuration.TrimHeaders = true;
								csvReader.Configuration.TrimFields = true;
								csvReader.Configuration.HasHeaderRecord = true;
								while (csvReader.Read())
								{
									if (!csvReader.FieldHeaders.Contains("类型") || !csvReader.FieldHeaders.Contains("关键词") || !csvReader.FieldHeaders.Contains("回复语") || !csvReader.FieldHeaders.Contains("关联号"))
									{
										MessageBox.Show("导入失败，导入的数据格式有误，头部必须包含列“类型”“关键词”“回复语”“关联号”", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
										return;
									}
									string field = csvReader.GetField("类型");
									if (!string.IsNullOrEmpty(field))
									{
										EnumArType enumArType = this.c(field);
										if (enumArType != EnumArType.Undefine)
										{
											string text = csvReader.GetField("关键词");
											if (enumArType == EnumArType.FirstReply)
											{
												text = "【Agiso首次智能答复Agiso】";
											}
											if (enumArType == EnumArType.FirstReply || enumArType == EnumArType.NoMatching || !string.IsNullOrEmpty(text))
											{
												string field2 = csvReader.GetField("回复语");
												if (!string.IsNullOrEmpty(field2))
												{
													string field3 = csvReader.GetField("关联号");
													if (!string.IsNullOrEmpty(field3))
													{
														AutoReplyInfo autoReplyInfo = new AutoReplyInfo
														{
															Type = enumArType,
															KeyWord = text,
															ReplyWord = field2,
															SellerNick = field3
														};
														if (csvReader.FieldHeaders.Contains("优先级"))
														{
															string field4 = csvReader.GetField("优先级");
															if (string.IsNullOrWhiteSpace(field4))
															{
																autoReplyInfo.Grade = 100L;
															}
															else
															{
																autoReplyInfo.Grade = Convert.ToInt64(field4);
															}
														}
														else
														{
															autoReplyInfo.Grade = 100L;
														}
														if (csvReader.FieldHeaders.Contains("答复时间起始"))
														{
															string field5 = csvReader.GetField("答复时间起始");
															autoReplyInfo.ArStartTime = new DateTime?(Convert.ToDateTime(field5));
														}
														else
														{
															autoReplyInfo.ArStartTime = new DateTime?(DateTime.Now.Date);
														}
														if (csvReader.FieldHeaders.Contains("答复时间结束"))
														{
															string field6 = csvReader.GetField("答复时间结束");
															autoReplyInfo.ArEndTime = new DateTime?(Convert.ToDateTime(field6));
														}
														else
														{
															autoReplyInfo.ArEndTime = new DateTime?(DateTime.Now.Date);
														}
														list.Add(autoReplyInfo);
														if (list.Count + num > 1000)
														{
															MessageBox.Show(string.Format("导入失败，智能答复总数不能超过{0}，请减少导入文件中的智能答复，或者删除已存在的无用智能答复", 1000));
															return;
														}
													}
												}
											}
										}
									}
								}
							}
						}
						if (list.Count <= 0)
						{
							MessageBox.Show("导入失败，导入的数据为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						else
						{
							int count = list.Count;
							int num2 = 0;
							foreach (AutoReplyInfo autoReplyInfo2 in list)
							{
								num2 += AutoReplyManager.InsertOrReplace(autoReplyInfo2);
							}
							if (num2 > 0)
							{
								AppConfig.InitAutoReplyList();
								this.v(null, null);
							}
							MessageBox.Show(string.Format("导入完成，总数：{0}，成功：{1}", count, num2), "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show("导入出错，" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0002C1E4 File Offset: 0x0002A3E4
		private EnumArType c(string A_0)
		{
			EnumArType enumArType = EnumArType.Undefine;
			if (!(A_0 == "首次答复"))
			{
				if (!(A_0 == "等于"))
				{
					if (!(A_0 == "包含"))
					{
						if (!(A_0 == "公式"))
						{
							if (A_0 == "无匹配")
							{
								enumArType = EnumArType.NoMatching;
							}
						}
						else
						{
							enumArType = EnumArType.Expression;
						}
					}
					else
					{
						enumArType = EnumArType.Contains;
					}
				}
				else
				{
					enumArType = EnumArType.EqualsWith;
				}
			}
			else
			{
				enumArType = EnumArType.FirstReply;
			}
			return enumArType;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00002C6D File Offset: 0x00000E6D
		private void a(object sender, LinkLabelLinkClickedEventArgs e)
		{
			AppConfig.RestartExplorer();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0002C264 File Offset: 0x0002A464
		private void o()
		{
			AppConfig.WriteLog("LogForAutoUpdateFail：StopWork Disable BtnRequestMsgOnOff start...", LogType.LogForAutoUpdateFail, 1);
			if (base.InvokeRequired)
			{
				base.Invoke(new EventHandler(this.b));
			}
			else
			{
				this.bj.Enabled = false;
			}
			AppConfig.WriteLog("LogForAutoUpdateFail：StopWork Stop timers start...", LogType.LogForAutoUpdateFail, 1);
			this.cv.Stop();
			this.cy.Stop();
			global::System.Timers.Timer timerBeat = this.TimerBeat;
			if (timerBeat != null)
			{
				timerBeat.Stop();
			}
			this.cu.Stop();
			this.bd.Stop();
			this.be.Stop();
			this.bf.Stop();
			HttpServerBase localHttpServer = FormErrorLog.LocalHttpServer;
			if (localHttpServer != null)
			{
				localHttpServer.Stop();
			}
			WebSocketClient wwWebSocketClient = AppConfig.WwWebSocketClient;
			if (wwWebSocketClient != null)
			{
				wwWebSocketClient.Disponse();
			}
			AppConfig.WriteLog("LogForAutoUpdateFail：StopWork over!", LogType.LogForAutoUpdateFail, 1);
			WindowInfo windowInfo = Win32Extend.FindWindowByClassAndName("#32770", "Agiso信息提示");
			if (windowInfo != null)
			{
				windowInfo.Close(true);
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0002C358 File Offset: 0x0002A558
		private bool a(ref string A_0)
		{
			bool flag = false;
			UpgradeInfo upgradeInfo = null;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag2;
			if (!this.a(ref upgradeInfo))
			{
				if (AppConfig.IsSellerLoginOnOwnComputer)
				{
					MessageBox.Show("获取更新文件出错了");
				}
				else
				{
					Form1.e("获取更新文件出错了");
				}
				flag2 = false;
			}
			else if (upgradeInfo.Updater == null || (AppConfig.IsSellerLoginOnOwnComputer ? (upgradeInfo.AliwwClient == null) : (upgradeInfo.AliwwClientAgent == null)))
			{
				if (AppConfig.IsSellerLoginOnOwnComputer)
				{
					MessageBox.Show("更新文件配置有误");
				}
				else
				{
					Form1.e("更新文件配置有误");
				}
				flag2 = false;
			}
			else
			{
				if (string.IsNullOrEmpty(upgradeInfo.Updater.UpgradeFolderPath))
				{
					upgradeInfo.Updater.UpgradeFolderPath = AppDomain.CurrentDomain.BaseDirectory;
				}
				if (string.IsNullOrEmpty(upgradeInfo.Updater.ProgramName))
				{
					if (AppConfig.IsSellerLoginOnOwnComputer)
					{
						MessageBox.Show("升级程序名有误");
					}
					else
					{
						Form1.e("升级程序名有误");
					}
					flag2 = false;
				}
				else
				{
					string text = upgradeInfo.Updater.UpgradeFolderPath + upgradeInfo.Updater.ProgramName + ".exe";
					if (this.a(text, upgradeInfo.Updater.Version) && !this.a(upgradeInfo.Updater))
					{
						if (AppConfig.IsSellerLoginOnOwnComputer)
						{
							MessageBox.Show("下载升级程序出错");
						}
						else
						{
							Form1.e("下载升级程序出错");
						}
						flag2 = false;
					}
					else
					{
						UpgradeItem upgradeItem = (AppConfig.IsSellerLoginOnOwnComputer ? upgradeInfo.AliwwClient : upgradeInfo.AliwwClientAgent);
						if (string.IsNullOrEmpty(upgradeItem.UpgradeFolderPath))
						{
							upgradeItem.UpgradeFolderPath = AppDomain.CurrentDomain.BaseDirectory;
						}
						if (Util.VersionCompare(AppConfig.GetCurrentApplicationVersion(), upgradeItem.Version) < 0)
						{
							bool flag3 = this.a(upgradeItem, "下载文件", "下载助手更新文件", "下载新版本助手失败");
							flag = flag || flag3;
							if (flag3)
							{
								string text2 = string.Format("{0}={1}={2}={3}={4}", new object[]
								{
									upgradeItem.ProgramName,
									upgradeItem.UpgradeFolderPath,
									upgradeItem.IsNeedToRestart,
									Util.IsEmptyList<string>(this.at) ? "" : string.Join("{$$}", this.at),
									upgradeItem.DisableKill
								});
								stringBuilder.Append(HttpUtility.UrlEncode(text2, Encoding.UTF8)).Append(" ");
							}
						}
						if (upgradeInfo.UpgradeOtherPrograms != null && upgradeInfo.UpgradeOtherPrograms.Count > 0)
						{
							foreach (UpgradeItem upgradeItem2 in upgradeInfo.UpgradeOtherPrograms)
							{
								text = upgradeItem2.UpgradeFolderPath + upgradeItem2.ProgramName + ".exe";
								if (this.a(text, upgradeItem2.Version))
								{
									bool flag4 = this.a(upgradeItem2, "下载文件", "下载" + upgradeItem2.ProgramName + "更新文件", "下载新版本" + upgradeItem2.ProgramName + "失败");
									flag = flag || flag4;
									if (flag4)
									{
										string text3 = string.Format("{0}={1}={2}=={3}", new object[] { upgradeItem2.ProgramName, upgradeItem2.UpgradeFolderPath, upgradeItem2.IsNeedToRestart, upgradeItem2.DisableKill });
										stringBuilder.Append(HttpUtility.UrlEncode(text3)).Append(" ");
									}
								}
							}
						}
						if (!flag)
						{
							if (AppConfig.IsSellerLoginOnOwnComputer)
							{
								MessageBox.Show("当前是最新版本");
							}
							else
							{
								Form1.e("当前是最新版本");
							}
						}
						A_0 = stringBuilder.ToString();
						flag2 = flag;
					}
				}
			}
			return flag2;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0002C734 File Offset: 0x0002A934
		public bool IsNeedUpgrade(out string currVersion, out string newVersion)
		{
			currVersion = "";
			newVersion = "";
			UpgradeInfo upgradeInfo = null;
			bool flag;
			if (!this.a(ref upgradeInfo))
			{
				if (AppConfig.IsSellerLoginOnOwnComputer)
				{
					MessageBox.Show("获取更新文件出错了");
				}
				else
				{
					Form1.e("获取更新文件出错了");
				}
				flag = false;
			}
			else if (upgradeInfo.Updater == null || (AppConfig.IsSellerLoginOnOwnComputer ? (upgradeInfo.AliwwClient == null) : (upgradeInfo.AliwwClientAgent == null)))
			{
				if (AppConfig.IsSellerLoginOnOwnComputer)
				{
					MessageBox.Show("更新文件配置有误");
				}
				else
				{
					Form1.e("更新文件配置有误");
				}
				flag = false;
			}
			else
			{
				if (string.IsNullOrEmpty(upgradeInfo.Updater.UpgradeFolderPath))
				{
					upgradeInfo.Updater.UpgradeFolderPath = AppDomain.CurrentDomain.BaseDirectory;
				}
				if (string.IsNullOrEmpty(upgradeInfo.Updater.ProgramName))
				{
					if (AppConfig.IsSellerLoginOnOwnComputer)
					{
						MessageBox.Show("升级程序名有误");
					}
					else
					{
						Form1.e("升级程序名有误");
					}
					flag = false;
				}
				else
				{
					UpgradeItem upgradeItem = (AppConfig.IsSellerLoginOnOwnComputer ? upgradeInfo.AliwwClient : upgradeInfo.AliwwClientAgent);
					if (string.IsNullOrEmpty(upgradeItem.UpgradeFolderPath))
					{
						upgradeItem.UpgradeFolderPath = AppDomain.CurrentDomain.BaseDirectory;
					}
					currVersion = AppConfig.GetCurrentApplicationVersion();
					newVersion = upgradeItem.Version;
					flag = Util.VersionCompare(AppConfig.GetCurrentApplicationVersion(), upgradeItem.Version) < 0;
				}
			}
			return flag;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0002C890 File Offset: 0x0002AA90
		private bool a(ref UpgradeInfo A_0)
		{
			bool flag;
			try
			{
				string text = string.Format("{0}?_t={1}", AppConfig.IsSellerLoginOnOwnComputer ? "http://download.agiso.com/AliwwClient/update.json" : AppConfig.AgentUpdateJsonUrl, DateTime.Now.Ticks);
				WebRequest webRequest = WebRequest.Create(text);
				WebResponse response = webRequest.GetResponse();
				using (Stream responseStream = response.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
					{
						string text2 = "";
						char[] array = new char[256];
						for (int i = streamReader.Read(array, 0, 256); i != 0; i = streamReader.Read(array, 0, 256))
						{
							string text3 = new string(array, 0, i);
							text2 += text3;
						}
						A_0 = JSON.Decode<UpgradeInfo>(text2);
						flag = true;
					}
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0002C9B4 File Offset: 0x0002ABB4
		private bool a(UpgradeItem A_0)
		{
			string text = Path.Combine(A_0.UpgradeFolderPath, "d2.tmp");
			string text2 = ((Environment.Version.Major >= 4) ? A_0.UpgradeFile.DotNet45 : A_0.UpgradeFile.DotNet2);
			if (text2.Contains("$ver"))
			{
				text2 = text2.Replace("$ver", A_0.Version);
			}
			text2 = text2 + "?_t=" + DateTime.Now.Ticks.ToString();
			DownFile downFile = new DownFile("下载文件", "下载升级程序更新文件", text2, text);
			bool flag;
			if (downFile.ShowDialog() != DialogResult.OK)
			{
				if (!AppConfig.AllowAutoLogin)
				{
					MessageBox.Show("下载新版本升级程序失败");
				}
				flag = false;
			}
			else
			{
				Util.UnZipFiles(text, A_0.UpgradeFolderPath, null);
				File.Delete(text);
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0002CA94 File Offset: 0x0002AC94
		private bool a(UpgradeItem A_0, string A_1, string A_2, string A_3)
		{
			string text = Path.Combine(A_0.UpgradeFolderPath, "d1.tmp");
			string text2 = ((Environment.Version.Major >= 4) ? A_0.UpgradeFile.DotNet45 : A_0.UpgradeFile.DotNet2);
			if (text2.Contains("$ver"))
			{
				text2 = text2.Replace("$ver", A_0.Version);
			}
			DownFile downFile = new DownFile(A_1, A_2, text2 + "?_t=" + DateTime.Now.Ticks.ToString(), text);
			bool flag;
			if (downFile.ShowDialog() != DialogResult.OK)
			{
				if (!AppConfig.AllowAutoLogin)
				{
					MessageBox.Show(A_3);
				}
				flag = false;
			}
			else
			{
				Thread.Sleep(1000);
				string text3 = Path.Combine(A_0.UpgradeFolderPath, "new");
				if (Directory.Exists(text3))
				{
					Directory.Delete(text3, true);
				}
				Directory.CreateDirectory(text3);
				Util.UnZipFiles(text, text3 + "/", null);
				File.Create(Path.Combine(text3, "download.ok"));
				File.Delete(text);
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0002CBB0 File Offset: 0x0002ADB0
		private bool a(string A_0, string A_1)
		{
			if (File.Exists(A_0))
			{
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(A_0);
				string text = string.Format("{0}.{1}.{2}", versionInfo.ProductMajorPart, versionInfo.ProductMinorPart, versionInfo.ProductBuildPart);
				if (Util.VersionCompare(text, A_1) >= 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0002CC14 File Offset: 0x0002AE14
		private void a(bool A_0)
		{
			try
			{
				string text = "";
				if (this.a(ref text))
				{
					this.a(A_0, text);
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 2);
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0002CC5C File Offset: 0x0002AE5C
		private bool n()
		{
			string text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hostStart.flag");
			bool flag;
			if (!File.Exists(text))
			{
				this.a(false);
				File.Create(text);
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0002CCA0 File Offset: 0x0002AEA0
		private void a(bool A_0, string A_1 = "")
		{
			string text = AppDomain.CurrentDomain.BaseDirectory + "UpdateAliwwClient.exe";
			AppConfig.WriteLog("LogForAutoUpdateFail：StopWork start...", LogType.LogForAutoUpdateFail, 1);
			this.o();
			if (AppConfig.AllowAutoLogin && A_0)
			{
				this.RoolbackHostAllMsg();
			}
			AppConfig.WriteLog("LogForAutoUpdateFail：Waiting for current queue work start...", LogType.LogForAutoUpdateFail, 1);
			if (AppConfig.DictSellerExecuteCache != null && AppConfig.DictSellerExecuteCache.Count > 0)
			{
				foreach (KeyValuePair<string, SellerCache> keyValuePair in AppConfig.DictSellerExecuteCache)
				{
					while (keyValuePair.Value.AliwwMsgQueue.Count > 0)
					{
						Thread.Sleep(3000);
					}
				}
				AliwwMessageInfo aliwwMessageInfo = Form1.l;
				while (aliwwMessageInfo != null && !aliwwMessageInfo.IsComplete && (DateTime.Now - aliwwMessageInfo.DequeueTime).TotalMinutes <= 2.0)
				{
					Thread.Sleep(500);
				}
			}
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(new WindowStruct("#32770", "Agiso信息提示"), 0, false);
			if (windowListByClassAndName != null)
			{
				windowListByClassAndName.ForEach(new Action<WindowInfo>(Form1.<>c.<>9.a));
			}
			Thread.Sleep(1000);
			this.ak = true;
			AppConfig.WriteLog("LogForAutoUpdateFail：Start UpdateAliwwClient.exe...", LogType.LogForAutoUpdateFail, 1);
			Process.Start(text, A_1);
			AppConfig.WriteLog("LogForAutoUpdateFail：Waiting for threadSendAndAr abort start...", LogType.LogForAutoUpdateFail, 1);
			int num = 60;
			while (num-- >= 0 && this.g != null && this.g.IsAlive)
			{
				Thread.Sleep(1000);
			}
			if (num < 0)
			{
				this.g.Abort();
			}
			AppConfig.WriteLog("LogForAutoUpdateFail：Closeing Form1 start...", LogType.LogForAutoUpdateFail, 1);
			base.Invoke(new EventHandler(this.a));
			AppConfig.WriteLog("LogForAutoUpdateFail：Restart complete!", LogType.LogForAutoUpdateFail, 1);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0002CEB4 File Offset: 0x0002B0B4
		private void d(object sender, EventArgs e)
		{
			FormAutoReplySetting formAutoReplySetting = new FormAutoReplySetting();
			formAutoReplySetting.ShowDialog();
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0002CED0 File Offset: 0x0002B0D0
		private void m()
		{
			if (!this.bd.Enabled)
			{
				DialogResult dialogResult = MessageBox.Show("您好，请求待发消息的开关处于暂停状态，无法获取新消息！\r\n是否启用开关？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
				if (dialogResult == DialogResult.Yes)
				{
					this.ak(null, null);
				}
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0002CF10 File Offset: 0x0002B110
		private void a(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex >= 0 && this.cc.Columns[e.ColumnIndex].Name == "ArValid")
			{
				if (Util.ToBoolean(this.cc.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
				{
					if (MessageBox.Show("是否要停用该答复", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
					{
						long num = Util.ToLong(this.cc.Rows[e.RowIndex].Cells["ArIdNo"].Value);
						AutoReplyManager.InvalidReply(num);
						AppConfig.InitAutoReplyList();
						this.l();
					}
				}
				else if (MessageBox.Show("是否要启用该答复", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
				{
					long num2 = Util.ToLong(this.cc.Rows[e.RowIndex].Cells["ArIdNo"].Value);
					AutoReplyManager.ValidReply(num2);
					AppConfig.InitAutoReplyList();
					this.l();
				}
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0002D04C File Offset: 0x0002B24C
		private void l()
		{
			int firstDisplayedScrollingRowIndex = this.cc.FirstDisplayedScrollingRowIndex;
			int firstDisplayedScrollingColumnIndex = this.cc.FirstDisplayedScrollingColumnIndex;
			int rowIndex = this.cc.CurrentCell.RowIndex;
			int columnIndex = this.cc.CurrentCell.ColumnIndex;
			this.v(null, null);
			this.cc.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
			this.cc.FirstDisplayedScrollingColumnIndex = firstDisplayedScrollingColumnIndex;
			this.cc.CurrentCell = this.cc.Rows[rowIndex].Cells[columnIndex];
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0002D0DC File Offset: 0x0002B2DC
		private void a(object sender, DataGridViewCellMouseEventArgs e)
		{
			DataGridView dataGridView = (DataGridView)sender;
			string name = dataGridView.Columns[e.ColumnIndex].Name;
			string text = name;
			if (text == "ModifyTime" || text == "CreateTime")
			{
				this.b(dataGridView.Columns[e.ColumnIndex].Name);
				this.a(dataGridView.DataSource as List<AutoReplyInfo>);
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0002D154 File Offset: 0x0002B354
		private void b(string A_0)
		{
			if (A_0 == Form1.a)
			{
				string text = Form1.b;
				string text2 = text;
				if (!(text2 == "DESC"))
				{
					if (text2 == "ASC")
					{
						Form1.b = "DESC";
					}
				}
				else
				{
					Form1.b = "ASC";
				}
			}
			else
			{
				Form1.a = A_0;
				Form1.b = "DESC";
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0002D1C0 File Offset: 0x0002B3C0
		private void a(List<AutoReplyInfo> A_0)
		{
			foreach (object obj in this.cc.Columns)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj;
				if (dataGridViewColumn.Visible)
				{
					dataGridViewColumn.HeaderText = dataGridViewColumn.HeaderText.TrimEnd(new char[] { '↓', '↑' });
				}
			}
			DataGridViewColumn dataGridViewColumn2 = this.cc.Columns[Form1.a];
			DataGridViewColumn dataGridViewColumn3 = dataGridViewColumn2;
			dataGridViewColumn3.HeaderText += ((Form1.b == "DESC") ? "↓" : "↑");
			this.cc.DataSource = Util.Sort<AutoReplyInfo>(A_0, Form1.a, Form1.b);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0002D2B0 File Offset: 0x0002B4B0
		public void RoolbackHostAllMsg()
		{
			List<Task> list = new List<Task>();
			using (IEnumerator<KeyValuePair<string, SellerCache>> enumerator = AppConfig.DictSellerExecuteCache.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Form1.y y = new Form1.y();
					y.a = enumerator.Current;
					if (y.a.Value.AliwwMsgQueue.Count > 0)
					{
						list.Add(Task.Run(new Action(y.b)));
					}
				}
			}
			AliwwMessageInfo aliwwMessageInfo = Form1.l;
			if (!string.IsNullOrEmpty((aliwwMessageInfo != null) ? aliwwMessageInfo.SellerNick : null))
			{
				try
				{
					Form1.l.Stop = true;
				}
				catch
				{
				}
			}
			Task.WhenAll(list).Wait(10000);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0002D388 File Offset: 0x0002B588
		public string GetSendLog()
		{
			return this.bc.Text;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0002D3A4 File Offset: 0x0002B5A4
		public void CloseOneLongOpenQn(string log)
		{
			Form1.z z = new Form1.z();
			List<QnAgentInfo> longOpenUsers = AppConfig.GetLongOpenUsers();
			List<QnAgentInfo> list = longOpenUsers.Where(new Func<QnAgentInfo, bool>(Form1.<>c.<>9.a)).ToList<QnAgentInfo>();
			if (!Util.IsEmptyList<QnAgentInfo>(list))
			{
				z.a = Form1.l;
				QnAgentInfo qnAgentInfo = ((z.a != null) ? list.FirstOrDefault(new Func<QnAgentInfo, bool>(z.b)) : list.First<QnAgentInfo>());
				if (qnAgentInfo != null)
				{
					ImKillManager.Kill(qnAgentInfo.QnNick, log, true);
				}
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0002D464 File Offset: 0x0002B664
		private void k()
		{
			this.a3 = new Container();
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
			this.a4 = new NotifyIcon(this.a3);
			this.a5 = new ContextMenuStrip(this.a3);
			this.a6 = new ToolStripMenuItem();
			this.a7 = new TabControl();
			this.a9 = new TabPage();
			this.bk = new Panel();
			this.bl = new DataGridView();
			this.dh = new DataGridViewTextBoxColumn();
			this.di = new DataGridViewTextBoxColumn();
			this.dj = new DataGridViewTextBoxColumn();
			this.dk = new DataGridViewCheckBoxColumn();
			this.dl = new DataGridViewCheckBoxColumn();
			this.dm = new DataGridViewTextBoxColumn();
			this.dn = new DataGridViewLinkColumn();
			this.dp = new DataGridViewTextBoxColumn();
			this.dq = new DataGridViewTextBoxColumn();
			this.ch = new ContextMenuStrip(this.a3);
			this.co = new ToolStripMenuItem();
			this.ci = new ToolStripMenuItem();
			this.c4 = new Panel();
			this.d3 = new Label();
			this.TxtRoboter = new TextBox();
			this.c7 = new Label();
			this.c8 = new Button();
			this.c5 = new Label();
			this.c6 = new Button();
			this.bb = new Button();
			this.ba = new Button();
			this.a8 = new TabPage();
			this.bn = new Panel();
			this.bc = new TextBox();
			this.bg = new ContextMenuStrip(this.a3);
			this.bh = new ToolStripMenuItem();
			this.bm = new Panel();
			this.cj = new Label();
			this.bi = new Button();
			this.bj = new Button();
			this.bo = new TabPage();
			this.bp = new SplitContainer();
			this.ce = new ListView();
			this.ck = new ContextMenuStrip(this.a3);
			this.cl = new ToolStripMenuItem();
			this.c0 = new ToolStripMenuItem();
			this.cp = new SplitContainer();
			this.cq = new TextBox();
			this.cr = new Button();
			this.cs = new TextBox();
			this.ct = new ListView();
			this.bq = new Panel();
			this.c9 = new Button();
			this.c3 = new TextBox();
			this.c2 = new Label();
			this.cx = new Label();
			this.cf = new Button();
			this.br = new Button();
			this.bs = new Button();
			this.bt = new ComboBox();
			this.bu = new DateTimePicker();
			this.bv = new Label();
			this.bw = new TextBox();
			this.bx = new Label();
			this.by = new TextBox();
			this.bz = new Label();
			this.b0 = new TabPage();
			this.b7 = new SplitContainer();
			this.cc = new DataGridView();
			this.dr = new DataGridViewTextBoxColumn();
			this.ds = new DataGridViewTextBoxColumn();
			this.dt = new DataGridViewTextBoxColumn();
			this.du = new DataGridViewTextBoxColumn();
			this.dv = new DataGridViewTextBoxColumn();
			this.dw = new DataGridViewTextBoxColumn();
			this.dx = new DataGridViewCheckBoxColumn();
			this.dy = new DataGridViewTextBoxColumn();
			this.dz = new DataGridViewTextBoxColumn();
			this.d0 = new DataGridViewTextBoxColumn();
			this.d1 = new DataGridViewTextBoxColumn();
			this.d2 = new DataGridViewTextBoxColumn();
			this.b8 = new Label();
			this.b9 = new TextBox();
			this.b1 = new Panel();
			this.df = new Button();
			this.cg = new Button();
			this.dd = new Button();
			this.dc = new Button();
			this.cd = new ComboBox();
			this.b3 = new Button();
			this.b2 = new Button();
			this.ca = new Button();
			this.b4 = new Label();
			this.b5 = new TextBox();
			this.cb = new Button();
			this.b6 = new Label();
			this.bd = new global::System.Windows.Forms.Timer(this.a3);
			this.be = new global::System.Windows.Forms.Timer(this.a3);
			this.bf = new global::System.Windows.Forms.Timer(this.a3);
			this.cm = new Panel();
			this.de = new LinkLabel();
			this.db = new LinkLabel();
			this.da = new LinkLabel();
			this.c1 = new LinkLabel();
			this.cz = new LinkLabel();
			this.cw = new LinkLabel();
			this.cn = new LinkLabel();
			this.cu = new global::System.Windows.Forms.Timer(this.a3);
			this.cv = new global::System.Windows.Forms.Timer(this.a3);
			this.cy = new global::System.Windows.Forms.Timer(this.a3);
			this.a5.SuspendLayout();
			this.a7.SuspendLayout();
			this.a9.SuspendLayout();
			this.bk.SuspendLayout();
			((ISupportInitialize)this.bl).BeginInit();
			this.ch.SuspendLayout();
			this.c4.SuspendLayout();
			this.a8.SuspendLayout();
			this.bn.SuspendLayout();
			this.bg.SuspendLayout();
			this.bm.SuspendLayout();
			this.bo.SuspendLayout();
			((ISupportInitialize)this.bp).BeginInit();
			this.bp.Panel1.SuspendLayout();
			this.bp.Panel2.SuspendLayout();
			this.bp.SuspendLayout();
			this.ck.SuspendLayout();
			((ISupportInitialize)this.cp).BeginInit();
			this.cp.Panel1.SuspendLayout();
			this.cp.Panel2.SuspendLayout();
			this.cp.SuspendLayout();
			this.bq.SuspendLayout();
			this.b0.SuspendLayout();
			((ISupportInitialize)this.b7).BeginInit();
			this.b7.Panel1.SuspendLayout();
			this.b7.Panel2.SuspendLayout();
			this.b7.SuspendLayout();
			((ISupportInitialize)this.cc).BeginInit();
			this.b1.SuspendLayout();
			this.cm.SuspendLayout();
			base.SuspendLayout();
			this.a4.BalloonTipIcon = ToolTipIcon.Info;
			this.a4.BalloonTipText = "Agiso旺旺发送助手";
			this.a4.BalloonTipTitle = "Agiso";
			this.a4.ContextMenuStrip = this.a5;
			this.a4.Icon = Resources.Icon1;
			this.a4.Tag = "Agiso旺旺发送助手";
			this.a4.Text = "Agiso旺旺发送助手";
			this.a4.Visible = true;
			this.a4.MouseClick += this.b;
			this.a4.MouseDoubleClick += this.a;
			this.a5.Items.AddRange(new ToolStripItem[] { this.a6 });
			this.a5.Name = "contextMenuStrip1";
			this.a5.Size = new Size(117, 26);
			this.a5.Text = "菜单";
			this.a6.Name = "toolStripMenuItem1";
			this.a6.Size = new Size(116, 22);
			this.a6.Text = "退出(&X)";
			this.a6.Click += this.p;
			this.a7.Controls.Add(this.a9);
			this.a7.Controls.Add(this.a8);
			this.a7.Controls.Add(this.bo);
			this.a7.Controls.Add(this.b0);
			this.a7.Dock = DockStyle.Fill;
			this.a7.Location = new global::System.Drawing.Point(0, 0);
			this.a7.Name = "TabForm";
			this.a7.SelectedIndex = 0;
			this.a7.Size = new Size(736, 453);
			this.a7.TabIndex = 17;
			this.a7.Selected += this.a;
			this.a9.Controls.Add(this.bk);
			this.a9.Controls.Add(this.c4);
			this.a9.Location = new global::System.Drawing.Point(4, 22);
			this.a9.Name = "tabPage3";
			this.a9.Padding = new Padding(3);
			this.a9.Size = new Size(728, 427);
			this.a9.TabIndex = 2;
			this.a9.Text = "帐号管理";
			this.a9.UseVisualStyleBackColor = true;
			this.bk.BorderStyle = BorderStyle.Fixed3D;
			this.bk.Controls.Add(this.bl);
			this.bk.Dock = DockStyle.Fill;
			this.bk.Location = new global::System.Drawing.Point(3, 3);
			this.bk.Name = "panel4";
			this.bk.Padding = new Padding(3);
			this.bk.Size = new Size(722, 319);
			this.bk.TabIndex = 223;
			this.bl.AllowUserToAddRows = false;
			this.bl.AllowUserToDeleteRows = false;
			this.bl.AllowUserToResizeRows = false;
			this.bl.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = SystemColors.Control;
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.bl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.bl.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.bl.Columns.AddRange(new DataGridViewColumn[] { this.dh, this.di, this.dj, this.dk, this.dl, this.dm, this.dn, this.dp, this.dq });
			this.bl.ContextMenuStrip = this.ch;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			this.bl.DefaultCellStyle = dataGridViewCellStyle2;
			this.bl.Dock = DockStyle.Fill;
			this.bl.Location = new global::System.Drawing.Point(3, 3);
			this.bl.MultiSelect = false;
			this.bl.Name = "DgvAldsAccount";
			this.bl.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = SystemColors.Control;
			dataGridViewCellStyle3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
			this.bl.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.bl.RowHeadersVisible = false;
			this.bl.RowTemplate.Height = 23;
			this.bl.Size = new Size(712, 309);
			this.bl.TabIndex = 224;
			this.bl.CellClick += this.d;
			this.bl.CellDoubleClick += this.e;
			this.bl.CellPainting += this.a;
			this.dh.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dh.DataPropertyName = "UserNick";
			this.dh.FillWeight = 135f;
			this.dh.HeaderText = "旺旺号";
			this.dh.MaxInputLength = 32;
			this.dh.MinimumWidth = 180;
			this.dh.Name = "AccountUserNick";
			this.dh.ReadOnly = true;
			this.dh.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.di.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.di.DataPropertyName = "VerifyResult";
			this.di.HeaderText = "登录结果";
			this.di.MinimumWidth = 130;
			this.di.Name = "AccountVerifyResult";
			this.di.ReadOnly = true;
			this.di.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dj.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dj.DataPropertyName = "WebSocketStatus";
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dj.DefaultCellStyle = dataGridViewCellStyle4;
			this.dj.FillWeight = 40f;
			this.dj.HeaderText = "状态";
			this.dj.MinimumWidth = 50;
			this.dj.Name = "AccountWebSocketStatus";
			this.dj.ReadOnly = true;
			this.dj.Resizable = DataGridViewTriState.True;
			this.dj.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dk.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dk.DataPropertyName = "AutoSendOnOff";
			this.dk.FillWeight = 50f;
			this.dk.HeaderText = "发送开关";
			this.dk.MinimumWidth = 62;
			this.dk.Name = "AccountAutoSendOnOff";
			this.dk.ReadOnly = true;
			this.dl.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dl.DataPropertyName = "AutoReply";
			this.dl.FillWeight = 50f;
			this.dl.HeaderText = "智能答复";
			this.dl.MinimumWidth = 62;
			this.dl.Name = "AccountAutoReply";
			this.dl.ReadOnly = true;
			this.dm.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dm.DataPropertyName = "ManualNick";
			this.dm.FillWeight = 60f;
			this.dm.HeaderText = "人工客服";
			this.dm.MinimumWidth = 95;
			this.dm.Name = "AccountManualNick";
			this.dm.ReadOnly = true;
			this.dm.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dm.Visible = false;
			this.dn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dn.DataPropertyName = "_Edit";
			this.dn.FillWeight = 45f;
			this.dn.HeaderText = "[设置]";
			this.dn.MinimumWidth = 60;
			this.dn.Name = "Account_Edit";
			this.dn.ReadOnly = true;
			this.dn.Text = "[设置]";
			this.dn.UseColumnTextForLinkValue = true;
			this.dp.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dp.DataPropertyName = "QnConnectionStatus";
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dp.DefaultCellStyle = dataGridViewCellStyle5;
			this.dp.FillWeight = 40f;
			this.dp.HeaderText = "千牛";
			this.dp.MinimumWidth = 54;
			this.dp.Name = "QnConnectionStatus";
			this.dp.ReadOnly = true;
			this.dq.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dq.DataPropertyName = "DeadLineStr";
			this.dq.FillWeight = 60f;
			this.dq.HeaderText = "到期时间";
			this.dq.MinimumWidth = 80;
			this.dq.Name = "DeadLineStr";
			this.dq.ReadOnly = true;
			this.ch.Items.AddRange(new ToolStripItem[] { this.co, this.ci });
			this.ch.Name = "contextMenuStrip3";
			this.ch.Size = new Size(164, 48);
			this.co.Name = "DToolStripMenuItem_EditAccount";
			this.co.Size = new Size(163, 22);
			this.co.Text = "编辑帐号设置(&E)";
			this.co.Visible = false;
			this.co.Click += this.n;
			this.ci.Name = "DToolStripMenuItem_DeleteAccount";
			this.ci.Size = new Size(163, 22);
			this.ci.Text = "删除帐号(&D)";
			this.ci.Click += this.o;
			this.c4.BorderStyle = BorderStyle.FixedSingle;
			this.c4.Controls.Add(this.d3);
			this.c4.Controls.Add(this.TxtRoboter);
			this.c4.Controls.Add(this.c7);
			this.c4.Controls.Add(this.c8);
			this.c4.Controls.Add(this.c5);
			this.c4.Controls.Add(this.c6);
			this.c4.Controls.Add(this.bb);
			this.c4.Controls.Add(this.ba);
			this.c4.Dock = DockStyle.Bottom;
			this.c4.Location = new global::System.Drawing.Point(3, 322);
			this.c4.Name = "panel1";
			this.c4.Size = new Size(722, 102);
			this.c4.TabIndex = 225;
			this.d3.AutoSize = true;
			this.d3.ForeColor = Color.FromArgb(255, 77, 79);
			this.d3.Location = new global::System.Drawing.Point(5, 80);
			this.d3.Name = "label3";
			this.d3.Size = new Size(239, 12);
			this.d3.TabIndex = 242;
			this.d3.Text = "4、千牛工作台需使用“单账号”的模式登录";
			this.TxtRoboter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.TxtRoboter.Location = new global::System.Drawing.Point(300, 60);
			this.TxtRoboter.MaxLength = 32;
			this.TxtRoboter.Name = "TxtRoboter";
			this.TxtRoboter.Size = new Size(221, 21);
			this.TxtRoboter.TabIndex = 240;
			this.TxtRoboter.Visible = false;
			this.c7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.c7.AutoSize = true;
			this.c7.Location = new global::System.Drawing.Point(524, 67);
			this.c7.Name = "label1";
			this.c7.Size = new Size(53, 12);
			this.c7.TabIndex = 239;
			this.c7.Text = "机器客服";
			this.c7.Visible = false;
			this.c8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.c8.Location = new global::System.Drawing.Point(622, 39);
			this.c8.Name = "BtnSystemSetting";
			this.c8.Size = new Size(80, 25);
			this.c8.TabIndex = 241;
			this.c8.Text = "发送选项(&O)";
			this.c8.UseVisualStyleBackColor = true;
			this.c8.Click += this.h;
			this.c5.AutoSize = true;
			this.c5.Location = new global::System.Drawing.Point(5, 10);
			this.c5.Name = "label15";
			this.c5.Size = new Size(515, 60);
			this.c5.TabIndex = 8;
			this.c5.Text = "1、可以添加多个店铺的账号，不同店铺需要分别购买软件。\r\n\r\n2、同一店铺可添加多个帐号，如果其中一个号发送失败，会自动切换其他帐号发送，不会重发。\r\n\r\n3、每个账号都有专属私钥，可登录自动发货后台查看。";
			this.c6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.c6.Location = new global::System.Drawing.Point(527, 39);
			this.c6.Name = "BtnTestSendFormShow";
			this.c6.Size = new Size(80, 25);
			this.c6.TabIndex = 207;
			this.c6.Text = "测试发送(&T)";
			this.c6.UseVisualStyleBackColor = true;
			this.c6.Click += this.g;
			this.bb.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.bb.Location = new global::System.Drawing.Point(527, 8);
			this.bb.Name = "BtnLoginAdd";
			this.bb.Size = new Size(80, 25);
			this.bb.TabIndex = 203;
			this.bb.Text = "添加账号(&A)";
			this.bb.UseVisualStyleBackColor = true;
			this.bb.Click += this.an;
			this.ba.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.ba.Location = new global::System.Drawing.Point(622, 8);
			this.ba.Name = "BtnLoginDel";
			this.ba.Size = new Size(80, 25);
			this.ba.TabIndex = 204;
			this.ba.Text = "删除账号(&D)";
			this.ba.UseVisualStyleBackColor = true;
			this.ba.Click += this.am;
			this.a8.Controls.Add(this.bn);
			this.a8.Controls.Add(this.bm);
			this.a8.Location = new global::System.Drawing.Point(4, 22);
			this.a8.Name = "tabPage2";
			this.a8.Padding = new Padding(3);
			this.a8.Size = new Size(728, 427);
			this.a8.TabIndex = 1;
			this.a8.Text = "发送日志";
			this.a8.UseVisualStyleBackColor = true;
			this.bn.Controls.Add(this.bc);
			this.bn.Dock = DockStyle.Fill;
			this.bn.Location = new global::System.Drawing.Point(3, 3);
			this.bn.Name = "panel6";
			this.bn.Padding = new Padding(3);
			this.bn.Size = new Size(722, 361);
			this.bn.TabIndex = 305;
			this.bc.ContextMenuStrip = this.bg;
			this.bc.Dock = DockStyle.Fill;
			this.bc.Location = new global::System.Drawing.Point(3, 3);
			this.bc.Multiline = true;
			this.bc.Name = "TxtLog";
			this.bc.ReadOnly = true;
			this.bc.ScrollBars = ScrollBars.Vertical;
			this.bc.Size = new Size(716, 355);
			this.bc.TabIndex = 0;
			this.bg.Items.AddRange(new ToolStripItem[] { this.bh });
			this.bg.Name = "contextMenuStrip2";
			this.bg.Size = new Size(141, 26);
			this.bh.Name = "ClearToolStripMenuItem";
			this.bh.Size = new Size(140, 22);
			this.bh.Text = "清空记录(&C)";
			this.bh.Click += this.q;
			this.bm.Controls.Add(this.cj);
			this.bm.Controls.Add(this.bi);
			this.bm.Controls.Add(this.bj);
			this.bm.Dock = DockStyle.Bottom;
			this.bm.Location = new global::System.Drawing.Point(3, 364);
			this.bm.Name = "panel5";
			this.bm.Size = new Size(722, 60);
			this.bm.TabIndex = 304;
			this.cj.AutoSize = true;
			this.cj.Location = new global::System.Drawing.Point(206, 24);
			this.cj.Name = "label19";
			this.cj.Size = new Size(41, 12);
			this.cj.TabIndex = 304;
			this.cj.Text = "已启用";
			this.bi.Dock = DockStyle.Right;
			this.bi.Location = new global::System.Drawing.Point(522, 0);
			this.bi.Name = "BtnRequestMsgNow";
			this.bi.Size = new Size(200, 60);
			this.bi.TabIndex = 303;
			this.bi.Text = "立即发送1次";
			this.bi.UseVisualStyleBackColor = true;
			this.bi.Click += this.al;
			this.bj.Dock = DockStyle.Left;
			this.bj.Location = new global::System.Drawing.Point(0, 0);
			this.bj.Name = "BtnRequestMsgOnOff";
			this.bj.Size = new Size(200, 60);
			this.bj.TabIndex = 302;
			this.bj.Text = "暂停/启用请求待发消息(&T)";
			this.bj.UseVisualStyleBackColor = true;
			this.bj.Click += this.ak;
			this.bo.Controls.Add(this.bp);
			this.bo.Controls.Add(this.bq);
			this.bo.Location = new global::System.Drawing.Point(4, 22);
			this.bo.Name = "tabPage4";
			this.bo.Padding = new Padding(3);
			this.bo.Size = new Size(728, 427);
			this.bo.TabIndex = 4;
			this.bo.Text = "发送记录明细";
			this.bo.UseVisualStyleBackColor = true;
			this.bp.Dock = DockStyle.Fill;
			this.bp.Location = new global::System.Drawing.Point(3, 58);
			this.bp.Name = "splitContainer1";
			this.bp.Orientation = Orientation.Horizontal;
			this.bp.Panel1.Controls.Add(this.ce);
			this.bp.Panel2.Controls.Add(this.cp);
			this.bp.Size = new Size(722, 366);
			this.bp.SplitterDistance = 240;
			this.bp.TabIndex = 2;
			this.ce.ContextMenuStrip = this.ck;
			this.ce.Dock = DockStyle.Fill;
			this.ce.FullRowSelect = true;
			this.ce.HideSelection = false;
			this.ce.Location = new global::System.Drawing.Point(0, 0);
			this.ce.Name = "LvAliwwMessage";
			this.ce.Size = new Size(722, 240);
			this.ce.TabIndex = 6;
			this.ce.UseCompatibleStateImageBehavior = false;
			this.ce.View = View.Details;
			this.ce.ItemSelectionChanged += this.b;
			this.ck.Items.AddRange(new ToolStripItem[] { this.cl, this.c0 });
			this.ck.Name = "contextMenuContact";
			this.ck.Size = new Size(213, 48);
			this.cl.Name = "SendToHimToolStripMenuItem";
			this.cl.Size = new Size(212, 22);
			this.cl.Text = "用默认联系人找他聊天(&C)";
			this.cl.Click += this.m;
			this.c0.Name = "TestToRobotToolStripMenuItem";
			this.c0.Size = new Size(212, 22);
			this.c0.Text = "给机器人发送一条消息(&T)";
			this.c0.Click += this.i;
			this.cp.Dock = DockStyle.Fill;
			this.cp.Location = new global::System.Drawing.Point(0, 0);
			this.cp.Name = "splitContainer3";
			this.cp.Panel1.Controls.Add(this.cq);
			this.cp.Panel1.Controls.Add(this.cr);
			this.cp.Panel1.Controls.Add(this.cs);
			this.cp.Panel2.Controls.Add(this.ct);
			this.cp.Size = new Size(722, 122);
			this.cp.SplitterDistance = 372;
			this.cp.TabIndex = 7;
			this.cq.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.cq.BackColor = SystemColors.ButtonHighlight;
			this.cq.Location = new global::System.Drawing.Point(5, 98);
			this.cq.Name = "TxtBuyerNickToSend";
			this.cq.ReadOnly = true;
			this.cq.Size = new Size(255, 21);
			this.cq.TabIndex = 9;
			this.cr.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.cr.Location = new global::System.Drawing.Point(267, 97);
			this.cr.Name = "BtnMsgSendOnceMore";
			this.cr.Size = new Size(103, 24);
			this.cr.TabIndex = 8;
			this.cr.Text = "再发送一次(M)";
			this.cr.UseVisualStyleBackColor = true;
			this.cr.Click += this.y;
			this.cs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.cs.BackColor = SystemColors.ButtonHighlight;
			this.cs.Location = new global::System.Drawing.Point(5, 3);
			this.cs.Multiline = true;
			this.cs.Name = "TxtMessageBody";
			this.cs.ReadOnly = true;
			this.cs.ScrollBars = ScrollBars.Vertical;
			this.cs.Size = new Size(364, 89);
			this.cs.TabIndex = 7;
			this.ct.Dock = DockStyle.Fill;
			this.ct.FullRowSelect = true;
			this.ct.HideSelection = false;
			this.ct.Location = new global::System.Drawing.Point(0, 0);
			this.ct.MultiSelect = false;
			this.ct.Name = "LvLogSendResult";
			this.ct.Size = new Size(346, 122);
			this.ct.TabIndex = 8;
			this.ct.UseCompatibleStateImageBehavior = false;
			this.ct.View = View.Details;
			this.ct.ItemSelectionChanged += this.a;
			this.ct.DoubleClick += this.w;
			this.bq.Controls.Add(this.c9);
			this.bq.Controls.Add(this.c3);
			this.bq.Controls.Add(this.c2);
			this.bq.Controls.Add(this.cx);
			this.bq.Controls.Add(this.cf);
			this.bq.Controls.Add(this.br);
			this.bq.Controls.Add(this.bs);
			this.bq.Controls.Add(this.bt);
			this.bq.Controls.Add(this.bu);
			this.bq.Controls.Add(this.bv);
			this.bq.Controls.Add(this.bw);
			this.bq.Controls.Add(this.bx);
			this.bq.Controls.Add(this.by);
			this.bq.Controls.Add(this.bz);
			this.bq.Dock = DockStyle.Top;
			this.bq.Location = new global::System.Drawing.Point(3, 3);
			this.bq.Name = "panel10";
			this.bq.Size = new Size(722, 55);
			this.bq.TabIndex = 0;
			this.c9.Location = new global::System.Drawing.Point(156, 29);
			this.c9.Name = "BtnSyncNotSendAliwwMsg";
			this.c9.Size = new Size(200, 23);
			this.c9.TabIndex = 18;
			this.c9.Text = "同步所选日期未发送的旺旺消息";
			this.c9.UseVisualStyleBackColor = true;
			this.c9.Click += this.z;
			this.c3.Location = new global::System.Drawing.Point(602, 29);
			this.c3.Name = "TxtSellerNick";
			this.c3.Size = new Size(106, 21);
			this.c3.TabIndex = 21;
			this.c2.AutoSize = true;
			this.c2.Location = new global::System.Drawing.Point(549, 34);
			this.c2.Name = "label2";
			this.c2.Size = new Size(53, 12);
			this.c2.TabIndex = 20;
			this.c2.Text = "卖家旺旺";
			this.cx.AutoSize = true;
			this.cx.Location = new global::System.Drawing.Point(650, 9);
			this.cx.Name = "LblMsgCount";
			this.cx.Size = new Size(0, 12);
			this.cx.TabIndex = 19;
			this.cf.Location = new global::System.Drawing.Point(360, 30);
			this.cf.Name = "BtnSyncAliwwMsg";
			this.cf.Size = new Size(183, 23);
			this.cf.TabIndex = 20;
			this.cf.Text = "同步所选日期所有的旺旺消息";
			this.cf.UseVisualStyleBackColor = true;
			this.cf.Click += this.aa;
			this.br.Location = new global::System.Drawing.Point(5, 28);
			this.br.Name = "BtnMsgsSendOnceMore";
			this.br.Size = new Size(143, 23);
			this.br.TabIndex = 17;
			this.br.Text = "再发送一次选中行";
			this.br.UseVisualStyleBackColor = true;
			this.br.Click += this.x;
			this.bs.Location = new global::System.Drawing.Point(574, 1);
			this.bs.Name = "BtnSearch";
			this.bs.Size = new Size(68, 23);
			this.bs.TabIndex = 16;
			this.bs.Text = "查询(&S)";
			this.bs.UseVisualStyleBackColor = true;
			this.bs.Click += this.ab;
			this.bt.DropDownStyle = ComboBoxStyle.DropDownList;
			this.bt.Items.AddRange(new object[] { "全部", "发送成功", "发送失败" });
			this.bt.Location = new global::System.Drawing.Point(488, 3);
			this.bt.Name = "ComboSendStatus";
			this.bt.Size = new Size(80, 20);
			this.bt.TabIndex = 15;
			this.bu.Checked = false;
			this.bu.CustomFormat = "yyyy-MM-dd";
			this.bu.Format = DateTimePickerFormat.Custom;
			this.bu.Location = new global::System.Drawing.Point(387, 3);
			this.bu.Name = "DtpCreateTime";
			this.bu.Size = new Size(95, 21);
			this.bu.TabIndex = 14;
			this.bv.AutoSize = true;
			this.bv.Location = new global::System.Drawing.Point(311, 6);
			this.bv.Name = "label7";
			this.bv.Size = new Size(77, 12);
			this.bv.TabIndex = 13;
			this.bv.Text = "消息生成日期";
			this.bw.Location = new global::System.Drawing.Point(205, 3);
			this.bw.Name = "TxtBuyerNick";
			this.bw.Size = new Size(100, 21);
			this.bw.TabIndex = 12;
			this.bx.AutoSize = true;
			this.bx.Location = new global::System.Drawing.Point(154, 6);
			this.bx.Name = "label6";
			this.bx.Size = new Size(53, 12);
			this.bx.TabIndex = 11;
			this.bx.Text = "买家旺旺";
			this.by.Location = new global::System.Drawing.Point(42, 3);
			this.by.Name = "TxtTid";
			this.by.Size = new Size(106, 21);
			this.by.TabIndex = 10;
			this.bz.AutoSize = true;
			this.bz.Location = new global::System.Drawing.Point(3, 6);
			this.bz.Name = "label5";
			this.bz.Size = new Size(41, 12);
			this.bz.TabIndex = 9;
			this.bz.Text = "订单号";
			this.b0.Controls.Add(this.b7);
			this.b0.Controls.Add(this.b1);
			this.b0.Location = new global::System.Drawing.Point(4, 22);
			this.b0.Name = "tabPage5";
			this.b0.Padding = new Padding(3);
			this.b0.Size = new Size(728, 427);
			this.b0.TabIndex = 5;
			this.b0.Text = "智能答复设置";
			this.b0.UseVisualStyleBackColor = true;
			this.b7.Dock = DockStyle.Fill;
			this.b7.Location = new global::System.Drawing.Point(3, 60);
			this.b7.Name = "splitContainer2";
			this.b7.Orientation = Orientation.Horizontal;
			this.b7.Panel1.Controls.Add(this.cc);
			this.b7.Panel2.Controls.Add(this.b8);
			this.b7.Panel2.Controls.Add(this.b9);
			this.b7.Size = new Size(722, 364);
			this.b7.SplitterDistance = 248;
			this.b7.TabIndex = 1;
			this.cc.AllowUserToAddRows = false;
			this.cc.AllowUserToResizeRows = false;
			this.cc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = SystemColors.Control;
			dataGridViewCellStyle6.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
			this.cc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.cc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.cc.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dr, this.ds, this.dt, this.du, this.dv, this.dw, this.dx, this.dy, this.dz, this.d0,
				this.d1, this.d2
			});
			dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = SystemColors.Window;
			dataGridViewCellStyle7.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle7.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
			this.cc.DefaultCellStyle = dataGridViewCellStyle7;
			this.cc.Dock = DockStyle.Fill;
			this.cc.Location = new global::System.Drawing.Point(0, 0);
			this.cc.Name = "DgvAutoReply";
			this.cc.ReadOnly = true;
			dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = SystemColors.Control;
			dataGridViewCellStyle8.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle8.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
			this.cc.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
			this.cc.RowTemplate.Height = 23;
			this.cc.Size = new Size(722, 248);
			this.cc.TabIndex = 0;
			this.cc.CellClick += this.a;
			this.cc.CellDoubleClick += this.b;
			this.cc.CellEnter += this.c;
			this.cc.ColumnHeaderMouseClick += this.a;
			this.dr.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dr.DataPropertyName = "IdNo";
			this.dr.HeaderText = "ArIdNo";
			this.dr.Name = "ArIdNo";
			this.dr.ReadOnly = true;
			this.dr.Visible = false;
			this.ds.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.ds.DataPropertyName = "Enabled";
			this.ds.HeaderText = "ArEnabled";
			this.ds.MinimumWidth = 50;
			this.ds.Name = "ArEnabled";
			this.ds.ReadOnly = true;
			this.ds.Visible = false;
			this.dt.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dt.DataPropertyName = "TypeText";
			this.dt.FillWeight = 60f;
			this.dt.HeaderText = "类型";
			this.dt.MinimumWidth = 60;
			this.dt.Name = "ArTypeText";
			this.dt.ReadOnly = true;
			this.dt.Resizable = DataGridViewTriState.True;
			this.du.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.du.DataPropertyName = "KeyWord";
			this.du.FillWeight = 450f;
			this.du.HeaderText = "关键词";
			this.du.MinimumWidth = 170;
			this.du.Name = "ArKeyWord";
			this.du.ReadOnly = true;
			this.dv.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dv.DataPropertyName = "SellerNick";
			this.dv.FillWeight = 160f;
			this.dv.HeaderText = "店铺旺旺";
			this.dv.MinimumWidth = 150;
			this.dv.Name = "ArSellerNick";
			this.dv.ReadOnly = true;
			this.dw.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dw.DataPropertyName = "ModifyTimeStr";
			this.dw.FillWeight = 60f;
			this.dw.HeaderText = "修改时间";
			this.dw.MinimumWidth = 120;
			this.dw.Name = "ModifyTime";
			this.dw.ReadOnly = true;
			this.dx.DataPropertyName = "Valid";
			this.dx.FillWeight = 40f;
			this.dx.HeaderText = "启用";
			this.dx.MinimumWidth = 40;
			this.dx.Name = "ArValid";
			this.dx.ReadOnly = true;
			this.dx.Resizable = DataGridViewTriState.True;
			this.dy.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dy.DataPropertyName = "CreateTimeStr";
			this.dy.FillWeight = 60f;
			this.dy.HeaderText = "创建时间";
			this.dy.MinimumWidth = 120;
			this.dy.Name = "CreateTime";
			this.dy.ReadOnly = true;
			this.dz.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dz.DataPropertyName = "Type";
			this.dz.HeaderText = "ArType";
			this.dz.Name = "ArType";
			this.dz.ReadOnly = true;
			this.dz.Visible = false;
			this.d0.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.d0.DataPropertyName = "Idx";
			this.d0.HeaderText = "ArIdx";
			this.d0.Name = "ArIdx";
			this.d0.ReadOnly = true;
			this.d0.Visible = false;
			this.d1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.d1.DataPropertyName = "ReplyWord";
			this.d1.HeaderText = "ArReplyWord";
			this.d1.Name = "ArReplyWord";
			this.d1.ReadOnly = true;
			this.d1.Visible = false;
			this.d2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.d2.DataPropertyName = "Option1";
			this.d2.HeaderText = "ArOption1";
			this.d2.Name = "ArOption1";
			this.d2.ReadOnly = true;
			this.d2.Visible = false;
			this.b8.AutoSize = true;
			this.b8.Location = new global::System.Drawing.Point(5, 8);
			this.b8.Name = "label17";
			this.b8.Size = new Size(41, 12);
			this.b8.TabIndex = 4;
			this.b8.Text = "回复语";
			this.b9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.b9.BackColor = SystemColors.ButtonHighlight;
			this.b9.Location = new global::System.Drawing.Point(52, 6);
			this.b9.Multiline = true;
			this.b9.Name = "TxtArReplyWord";
			this.b9.ReadOnly = true;
			this.b9.ScrollBars = ScrollBars.Vertical;
			this.b9.Size = new Size(665, 102);
			this.b9.TabIndex = 3;
			this.b1.Controls.Add(this.df);
			this.b1.Controls.Add(this.cg);
			this.b1.Controls.Add(this.dd);
			this.b1.Controls.Add(this.dc);
			this.b1.Controls.Add(this.cd);
			this.b1.Controls.Add(this.b3);
			this.b1.Controls.Add(this.b2);
			this.b1.Controls.Add(this.ca);
			this.b1.Controls.Add(this.b4);
			this.b1.Controls.Add(this.b5);
			this.b1.Controls.Add(this.cb);
			this.b1.Controls.Add(this.b6);
			this.b1.Dock = DockStyle.Top;
			this.b1.Location = new global::System.Drawing.Point(3, 3);
			this.b1.Name = "panel7";
			this.b1.Size = new Size(722, 57);
			this.b1.TabIndex = 0;
			this.df.Location = new global::System.Drawing.Point(424, 31);
			this.df.Name = "btnAutoReplySetting";
			this.df.Size = new Size(85, 22);
			this.df.TabIndex = 20;
			this.df.Text = "答复设置(&C)";
			this.df.UseVisualStyleBackColor = true;
			this.df.Click += this.d;
			this.cg.Location = new global::System.Drawing.Point(640, 1);
			this.cg.Name = "BtnLogAutoReply";
			this.cg.Size = new Size(79, 23);
			this.cg.TabIndex = 27;
			this.cg.Text = "答复记录(&L)";
			this.cg.UseVisualStyleBackColor = true;
			this.cg.Click += this.r;
			this.dd.Location = new global::System.Drawing.Point(257, 30);
			this.dd.Name = "btnExport";
			this.dd.Size = new Size(77, 22);
			this.dd.TabIndex = 15;
			this.dd.Text = "导出(&O)";
			this.dd.UseVisualStyleBackColor = true;
			this.dd.Click += this.f;
			this.dc.Location = new global::System.Drawing.Point(340, 31);
			this.dc.Name = "btnImport";
			this.dc.Size = new Size(77, 22);
			this.dc.TabIndex = 16;
			this.dc.Text = "导入(&I)";
			this.dc.UseVisualStyleBackColor = true;
			this.dc.Click += this.e;
			this.cd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.cd.DisplayMember = "UserNick";
			this.cd.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cd.FormattingEnabled = true;
			this.cd.Location = new global::System.Drawing.Point(354, 3);
			this.cd.Name = "ComboArSellerNickF";
			this.cd.Size = new Size(187, 20);
			this.cd.TabIndex = 27;
			this.cd.ValueMember = "UserNick";
			this.b3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.b3.Location = new global::System.Drawing.Point(554, 1);
			this.b3.Name = "BtnArSearch";
			this.b3.Size = new Size(79, 23);
			this.b3.TabIndex = 25;
			this.b3.Text = "查询(&S)";
			this.b3.UseVisualStyleBackColor = true;
			this.b3.Click += this.v;
			this.b2.Location = new global::System.Drawing.Point(173, 29);
			this.b2.Name = "BtnArDelete";
			this.b2.Size = new Size(77, 23);
			this.b2.TabIndex = 8;
			this.b2.Text = "删除(&D)";
			this.b2.UseVisualStyleBackColor = true;
			this.b2.Click += this.s;
			this.ca.Location = new global::System.Drawing.Point(88, 29);
			this.ca.Name = "BtnArEdit";
			this.ca.Size = new Size(77, 23);
			this.ca.TabIndex = 7;
			this.ca.Text = "编辑(&E)";
			this.ca.UseVisualStyleBackColor = true;
			this.ca.Click += this.u;
			this.b4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.b4.AutoSize = true;
			this.b4.Location = new global::System.Drawing.Point(295, 6);
			this.b4.Name = "label10";
			this.b4.Size = new Size(53, 12);
			this.b4.TabIndex = 20;
			this.b4.Text = "店铺旺旺";
			this.b5.Location = new global::System.Drawing.Point(52, 3);
			this.b5.Name = "TxtArKeywordF";
			this.b5.Size = new Size(238, 21);
			this.b5.TabIndex = 19;
			this.cb.Location = new global::System.Drawing.Point(5, 28);
			this.cb.Name = "BtnArAdd";
			this.cb.Size = new Size(77, 23);
			this.cb.TabIndex = 6;
			this.cb.Text = "添加(&A)";
			this.cb.UseVisualStyleBackColor = true;
			this.cb.Click += this.t;
			this.b6.AutoSize = true;
			this.b6.Location = new global::System.Drawing.Point(5, 6);
			this.b6.Name = "label12";
			this.b6.Size = new Size(41, 12);
			this.b6.TabIndex = 18;
			this.b6.Text = "关键词";
			this.bd.Interval = 10000;
			this.bd.Tick += this.ah;
			this.be.Interval = 86400000;
			this.be.Tick += this.ag;
			this.bf.Tick += this.af;
			this.cm.Controls.Add(this.de);
			this.cm.Controls.Add(this.db);
			this.cm.Controls.Add(this.da);
			this.cm.Controls.Add(this.c1);
			this.cm.Controls.Add(this.cz);
			this.cm.Controls.Add(this.cw);
			this.cm.Controls.Add(this.cn);
			this.cm.Dock = DockStyle.Bottom;
			this.cm.Location = new global::System.Drawing.Point(0, 453);
			this.cm.Name = "panel8";
			this.cm.Size = new Size(736, 21);
			this.cm.TabIndex = 18;
			this.de.AutoSize = true;
			this.de.Location = new global::System.Drawing.Point(212, 4);
			this.de.Name = "linkLabelRestartExplorer";
			this.de.Size = new Size(77, 12);
			this.de.TabIndex = 5;
			this.de.TabStop = true;
			this.de.Text = "重启explorer";
			this.de.Visible = false;
			this.de.LinkClicked += this.a;
			this.db.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.db.AutoSize = true;
			this.db.Location = new global::System.Drawing.Point(463, 4);
			this.db.Name = "linkLabelRepair";
			this.db.Size = new Size(53, 12);
			this.db.TabIndex = 4;
			this.db.TabStop = true;
			this.db.Text = "通信修复";
			this.db.TextAlign = ContentAlignment.MiddleRight;
			this.db.LinkClicked += this.b;
			this.da.AutoSize = true;
			this.da.Location = new global::System.Drawing.Point(522, 4);
			this.da.Name = "linkLabelUpgrade";
			this.da.Size = new Size(77, 12);
			this.da.TabIndex = 3;
			this.da.TabStop = true;
			this.da.Text = "助手一键升级";
			this.da.LinkClicked += this.c;
			this.c1.AutoSize = true;
			this.c1.Location = new global::System.Drawing.Point(129, 4);
			this.c1.Name = "linkLabelClearAllQn";
			this.c1.Size = new Size(77, 12);
			this.c1.TabIndex = 2;
			this.c1.TabStop = true;
			this.c1.Text = "杀掉所有千牛";
			this.c1.LinkClicked += this.e;
			this.cz.AutoSize = true;
			this.cz.Location = new global::System.Drawing.Point(10, 4);
			this.cz.Name = "linkLabelClearAliAppProc";
			this.cz.Size = new Size(113, 12);
			this.cz.TabIndex = 1;
			this.cz.TabStop = true;
			this.cz.Text = "清理无头AliApp进程";
			this.cz.LinkClicked += this.f;
			this.cw.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.cw.AutoSize = true;
			this.cw.Location = new global::System.Drawing.Point(408, 4);
			this.cw.Name = "linkLabelGetSMS";
			this.cw.Size = new Size(53, 12);
			this.cw.TabIndex = 0;
			this.cw.TabStop = true;
			this.cw.Text = "获取短信";
			this.cw.TextAlign = ContentAlignment.MiddleRight;
			this.cw.LinkClicked += this.g;
			this.cn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.cn.AutoSize = true;
			this.cn.Location = new global::System.Drawing.Point(602, 4);
			this.cn.Name = "linkLabelGotoBuy";
			this.cn.Size = new Size(131, 12);
			this.cn.TabIndex = 0;
			this.cn.TabStop = true;
			this.cn.Text = "Agiso自动发货续费升级";
			this.cn.TextAlign = ContentAlignment.MiddleRight;
			this.cn.LinkClicked += this.d;
			this.cu.Interval = 10000;
			this.cu.Tick += this.ae;
			this.cv.Interval = 10000;
			this.cv.Tick += this.ad;
			this.cy.Interval = 10000;
			this.cy.Tick += this.aj;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(736, 474);
			base.Controls.Add(this.a7);
			base.Controls.Add(this.cm);
			base.Icon = Resources.Icon1;
			this.MinimumSize = new Size(685, 465);
			base.Name = "Form1";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Agiso旺旺发送助手";
			base.FormClosing += this.a;
			base.Load += this.ac;
			base.HelpRequested += this.a;
			this.a5.ResumeLayout(false);
			this.a7.ResumeLayout(false);
			this.a9.ResumeLayout(false);
			this.bk.ResumeLayout(false);
			((ISupportInitialize)this.bl).EndInit();
			this.ch.ResumeLayout(false);
			this.c4.ResumeLayout(false);
			this.c4.PerformLayout();
			this.a8.ResumeLayout(false);
			this.bn.ResumeLayout(false);
			this.bn.PerformLayout();
			this.bg.ResumeLayout(false);
			this.bm.ResumeLayout(false);
			this.bm.PerformLayout();
			this.bo.ResumeLayout(false);
			this.bp.Panel1.ResumeLayout(false);
			this.bp.Panel2.ResumeLayout(false);
			((ISupportInitialize)this.bp).EndInit();
			this.bp.ResumeLayout(false);
			this.ck.ResumeLayout(false);
			this.cp.Panel1.ResumeLayout(false);
			this.cp.Panel1.PerformLayout();
			this.cp.Panel2.ResumeLayout(false);
			((ISupportInitialize)this.cp).EndInit();
			this.cp.ResumeLayout(false);
			this.bq.ResumeLayout(false);
			this.bq.PerformLayout();
			this.b0.ResumeLayout(false);
			this.b7.Panel1.ResumeLayout(false);
			this.b7.Panel2.ResumeLayout(false);
			this.b7.Panel2.PerformLayout();
			((ISupportInitialize)this.b7).EndInit();
			this.b7.ResumeLayout(false);
			((ISupportInitialize)this.cc).EndInit();
			this.b1.ResumeLayout(false);
			this.b1.PerformLayout();
			this.cm.ResumeLayout(false);
			this.cm.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00031674 File Offset: 0x0002F874
		[CompilerGenerated]
		private void j()
		{
			Form1.g g = new Form1.g();
			g.b = this;
			g.a = null;
			try
			{
				g.a = AppConfig.WwServiceClient.GetNotifyMessage(AppConfig.GetCurrentApplicationVersion(), HardwareInfo.Uuid);
			}
			catch (Exception ex)
			{
				this.WriteLine("查询通知消息2时错误！");
				Form1.e(ex.ToString());
				return;
			}
			if (g.a.IsError)
			{
				string text = "查询通知消息2时错误:" + g.a.ErrMsg;
				this.WriteLine(text);
				Form1.e(text);
			}
			else
			{
				base.Invoke(new EventHandler(g.c));
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00002C74 File Offset: 0x00000E74
		[CompilerGenerated]
		private void i()
		{
			this.cu.Start();
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00002C81 File Offset: 0x00000E81
		[CompilerGenerated]
		private void h()
		{
			this.cu.Stop();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00031724 File Offset: 0x0002F924
		[CompilerGenerated]
		private bool g()
		{
			bool flag;
			if (!AppConfig.IsSellerLoginOnOwnComputer)
			{
				flag = false;
			}
			else
			{
				if (global::k.a().InvokeRequired)
				{
					if (!(bool)global::k.a().Invoke(new Func<bool>(this.f)))
					{
						return false;
					}
				}
				else if (!this.bd.Enabled)
				{
					return false;
				}
				flag = AppConfig.CurrentSystemSettingInfo.AllowGetMsgByWebSocket;
			}
			return flag;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00002C8E File Offset: 0x00000E8E
		[CompilerGenerated]
		private bool f()
		{
			return this.bd.Enabled;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00002C9B File Offset: 0x00000E9B
		[CompilerGenerated]
		private void a(object sender, ListChangedEventArgs e)
		{
			this.s();
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00002CA3 File Offset: 0x00000EA3
		[CompilerGenerated]
		private void e()
		{
			this.CloseOptionTimeDisableCloseWindowWhenAutoReply = new DateTime?(DateTime.Now);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0003179C File Offset: 0x0002F99C
		[CompilerGenerated]
		private void c(object sender, ElapsedEventArgs e)
		{
			if (AppConfig.AllowAutoLogin)
			{
				AppConfig.StartWhileNoExitsExplorer();
				AliwwQn.smethod_0();
				AliwwErrorReport.Close();
				uint memoryLoad = Win32Extend.GetMemoryLoad();
				if (memoryLoad >= 95U)
				{
					Task.Run(new Func<Task>(this.d));
				}
				AliwwMessageInfo aliwwMessageInfo = Form1.l;
				if (aliwwMessageInfo != null && !aliwwMessageInfo.IsComplete && aliwwMessageInfo.DequeueTime.AddMinutes(2.0) < DateTime.Now)
				{
					this.h[this.g.ManagedThreadId] = false;
					Form1.e("ami processing: " + JSON.Encode(aliwwMessageInfo));
					LogSendResultManager.Insert(aliwwMessageInfo.MsgId, aliwwMessageInfo.UserNick, -100, "发送失败，消息发送超过2分钟", MsgSendSoftware.Undefined);
					if (!AppConfig.AllowAutoLogin && aliwwMessageInfo.MsgId > 0L)
					{
						AppConfig.WwServiceClient.LogSendResult(aliwwMessageInfo.MsgId, aliwwMessageInfo.UserNick, -100, "发送失败，消息发送超过2分钟");
					}
					AppConfig.FailAliwwMessage(aliwwMessageInfo, 0);
					AldsAccountInfo agentAccountInfo = AppConfig.GetAgentAccountInfo(aliwwMessageInfo.SellerNick);
					if (agentAccountInfo != null)
					{
						AppConfig.QnAgentServiceClient.AutoLoginError(agentAccountInfo.UserNick, null, "消息发送大于2分钟，请注意服务器", 268435455L, "", "");
					}
					this.ClearAllQnProc(false, true, true, true, "消息发送大于2分钟，杀掉所有千牛和旺旺（忽略时间）");
					this.RestartSendAndAutoReplyThread();
					return;
				}
				if (Form1.i != null && Form1.i.Value.AddMinutes(1.0) < DateTime.Now)
				{
					this.h[this.g.ManagedThreadId] = false;
					LogSendResultManager.Insert(aliwwMessageInfo.MsgId, aliwwMessageInfo.UserNick, -100, "发送失败，发货线程执行发货前的操作超过1分钟", MsgSendSoftware.Undefined);
					if (!AppConfig.AllowAutoLogin && aliwwMessageInfo.MsgId > 0L)
					{
						AppConfig.WwServiceClient.LogSendResult(aliwwMessageInfo.MsgId, aliwwMessageInfo.UserNick, -100, "发送失败，发货线程执行发货前的操作超过1分钟");
					}
					AppConfig.FailAliwwMessage(aliwwMessageInfo, 0);
					Form1.e("发货线程执行发货前的操作超过1分钟，异常");
					if (AppConfig.AllowAutoLogin && ++Form1.j >= 2)
					{
						AppConfig.QnAgentServiceClient.AutoLoginError("agiso", null, "发货线程执行发货前的操作超过1分钟，异常，请注意服务器", 268435455L, "", "");
					}
					this.RestartSendAndAutoReplyThread();
					return;
				}
			}
			else
			{
				try
				{
					AliwwMessageInfo aliwwMessageInfo2 = Form1.l;
					if (aliwwMessageInfo2 != null && (aliwwMessageInfo2.AliwwMessageSourceType == EnumAliwwMessageSource.FromWwmsgService || aliwwMessageInfo2.AliwwMessageSourceType == EnumAliwwMessageSource.FromWwSocketService) && (DateTime.Now - aliwwMessageInfo2.DequeueTime).TotalSeconds > 60.0)
					{
						this.h[this.g.ManagedThreadId] = false;
						Form1.e("ami processing: " + JSON.Encode(aliwwMessageInfo2));
						LogSendResultManager.Insert(aliwwMessageInfo2.MsgId, aliwwMessageInfo2.UserNick, -100, "发送失败，消息发送超过60秒", MsgSendSoftware.Undefined);
						if (!AppConfig.AllowAutoLogin && aliwwMessageInfo2.MsgId > 0L)
						{
							AppConfig.WwServiceClient.LogSendResult(aliwwMessageInfo2.MsgId, aliwwMessageInfo2.UserNick, -100, "发送失败，消息发送超过60秒");
						}
						AppConfig.FailAliwwMessage(aliwwMessageInfo2, 0);
						this.RestartSendAndAutoReplyThread();
						return;
					}
				}
				catch (Exception ex)
				{
					Form1.e(ex.ToString());
				}
			}
			if (this.g == null || !this.g.IsAlive || (this.h.ContainsKey(this.g.ManagedThreadId) && !this.h[this.g.ManagedThreadId]))
			{
				Thread.Sleep(2000);
				if (this.g == null || !this.g.IsAlive || (this.h.ContainsKey(this.g.ManagedThreadId) && !this.h[this.g.ManagedThreadId]))
				{
					Form1.e("监测到发货线程没有正常运行，重新初始化发货线程");
					this.r();
				}
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00031BDC File Offset: 0x0002FDDC
		[CompilerGenerated]
		private Task d()
		{
			Form1.a a = new Form1.a();
			a.b = AsyncTaskMethodBuilder.Create();
			a.c = this;
			a.a = -1;
			a.b.Start<Form1.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00031C20 File Offset: 0x0002FE20
		[CompilerGenerated]
		private void b(object sender, ElapsedEventArgs e)
		{
			GetAgentAllUserResponse allUser = AppConfig.QnAgentServiceClient.GetAllUser();
			if (!allUser.IsError)
			{
				this.w();
			}
			if (allUser.AgentUser != null)
			{
				foreach (QnAgentInfo qnAgentInfo in allUser.AgentUser)
				{
					AppConfig.AgentUserDict[qnAgentInfo.SellerNick] = qnAgentInfo;
					AppConfig.GetUserCacheOrCreate(qnAgentInfo.QnNick).CurrentWorksheet = (qnAgentInfo.CustomerServiceNewVersion ? qnAgentInfo.CustomerServiceWorksheetInfo : new List<CustomerServiceWorksheet>());
				}
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00031CD0 File Offset: 0x0002FED0
		[CompilerGenerated]
		private void a(object sender, ElapsedEventArgs e)
		{
			try
			{
				AliwwMessageInfo aliwwMessageInfo = Form1.l;
				if (aliwwMessageInfo != null)
				{
					string userNick = aliwwMessageInfo.UserNick;
				}
				if ((DateTime.Now - this.p).TotalSeconds >= 5.0)
				{
					this.p = DateTime.Now;
					if (AppConfig.AllowAutoLogin)
					{
						Win32Extend.GetAllDesktopWindows().Where(new Func<WindowInfo, bool>(Form1.<>c.<>9.n)).ToList<WindowInfo>()
							.ForEach(new Action<WindowInfo>(Form1.<>c.<>9.m));
						List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName("StandardFrame", "UIMaskWindow", 0, true, true, false);
						if (windowListByClassAndName != null)
						{
							windowListByClassAndName.ForEach(new Action<WindowInfo>(Form1.<>c.<>9.l));
						}
						List<WindowInfo> windowListByClassAndName2 = Win32Extend.GetWindowListByClassAndName(new WindowStruct("#32770", "- AliWorkbench.exe"), 0, true);
						if (windowListByClassAndName2 != null)
						{
							windowListByClassAndName2.ForEach(new Action<WindowInfo>(Form1.<>c.<>9.k));
						}
						List<WindowInfo> windowListByClassAndName3 = Win32Extend.GetWindowListByClassAndName(new WindowStruct("", "- 系统错误"), 0, true);
						if (windowListByClassAndName3 != null)
						{
							windowListByClassAndName3.ForEach(new Action<WindowInfo>(Form1.<>c.<>9.j));
						}
						WindowInfo windowInfo = Win32Extend.FindWindowByClassAndName(null, "图形验证码识别器");
						if (windowInfo == null || windowInfo.HWnd == IntPtr.Zero)
						{
							Process.Start("C:\\Program Files (x86)\\Agiso\\QnVerificationCodeService\\QnVerificationCodeService.exe");
						}
					}
				}
				if ((DateTime.Now - this.q).TotalSeconds >= 10.0)
				{
					this.q = DateTime.Now;
					if (AppConfig.AllowAutoLogin)
					{
						List<WindowInfo> windowListByClassAndName4 = Win32Extend.GetWindowListByClassAndName(new WindowStruct("StandardFrame", " - 消息通知"), 0, true);
						if (windowListByClassAndName4 != null)
						{
							windowListByClassAndName4.ForEach(new Action<WindowInfo>(Form1.<>c.<>9.i));
						}
						List<WindowInfo> windowListByClassAndName5 = Win32Extend.GetWindowListByClassAndName(new WindowStruct("Qt5152QWindowIcon", "-消息通知"), 0, true);
						if (windowListByClassAndName5 != null)
						{
							windowListByClassAndName5.ForEach(new Action<WindowInfo>(Form1.<>c.<>9.h));
						}
						if (AppConfig.AgentSettings.AllowAutoExitQn)
						{
							IEnumerable<WinLoginQnBase> enumerable = WinLoginQnBase.GetList().Where(new Func<WinLoginQnBase, bool>(Form1.<>c.<>9.a));
							if (Util.IsEmptyList<WinLoginQnBase>(enumerable))
							{
								using (Process.Start(AppConfig.GetExeFullFileNameQn(AppConfig.AgentSettings.QnVersion)))
								{
								}
							}
						}
					}
					if (AppConfig.IsSellerLoginOnOwnComputer)
					{
						RepairQnAliresManager.RepairAlires();
						if (FormTipRestart.SendInstance.Visible)
						{
							if (AppConfig.UserDict.Values.All(new Func<AldsAccountInfo, bool>(Form1.<>c.<>9.a)))
							{
								FormTipRestart.SendInstance.Close();
							}
						}
						if (RepairQnAliresManager.NeedToRestart)
						{
							RepairQnAliresManager.NeedToRestart = false;
							if (!FormTipRestart.SendInstance.Visible)
							{
								Task.Run<DialogResult>(new Func<DialogResult>(Form1.<>c.<>9.a));
							}
						}
					}
				}
				if ((DateTime.Now - this.r).TotalSeconds >= 30.0)
				{
					this.r = DateTime.Now;
					if (AppConfig.AllowAutoLogin)
					{
						List<WindowInfo> windowListByClassAndName6 = Win32Extend.GetWindowListByClassAndName(new WindowStruct("Qt5152QWindowToolSaveBits", "-超时提醒"), 0, true);
						if (windowListByClassAndName6 != null)
						{
							windowListByClassAndName6.ForEach(new Action<WindowInfo>(Form1.<>c.<>9.g));
						}
						List<WindowInfo> windowListByClassAndName7 = Win32Extend.GetWindowListByClassAndName(new WindowStruct("Qt5152QWindowToolSaveBits", "消息提醒"), 0, true);
						if (windowListByClassAndName7 != null)
						{
							windowListByClassAndName7.ForEach(new Action<WindowInfo>(Form1.<>c.<>9.f));
						}
					}
				}
				if ((DateTime.Now - this.s).TotalSeconds >= 60.0)
				{
					this.s = DateTime.Now;
					if (AppConfig.CurrentSystemSettingInfo.AllowKillAliApp && (DateTime.Now - this.o).TotalMinutes >= 5.0)
					{
						this.o = DateTime.Now;
						if (AppConfig.AgentSettings.LoginOnQn || !AppConfig.AllowAutoLogin)
						{
							this.y.AddRange(Win32Extend.GetProcListWhileMemoryTooLargeByName("AliApp", 120));
							this.y.AddRange(Win32Extend.GetProcListWhileMemoryTooLargeByName("AliRender", 120));
						}
						if (AppConfig.AgentSettings.LoginOnAliwwBuyer || !AppConfig.AllowAutoLogin)
						{
							this.y.AddRange(Win32Extend.GetProcListWhileMemoryTooLargeByName("AliExternal", 150));
						}
					}
					AliwwClientMode aliwwClientMode = AppConfig.AliwwClientMode;
					AliwwClientMode aliwwClientMode2 = aliwwClientMode;
					if (aliwwClientMode2 != AliwwClientMode.机器人模式)
					{
						if (aliwwClientMode2 != AliwwClientMode.自挂模式)
						{
							if (aliwwClientMode2 == AliwwClientMode.代挂模式)
							{
								if (AppConfig.CurrentSystemSettingInfo.DisableCloseWindowWhenAutoReply && (this.CloseOptionTimeDisableCloseWindowWhenAutoReply == null || (DateTime.Now - this.CloseOptionTimeDisableCloseWindowWhenAutoReply.Value).TotalSeconds > 150.0))
								{
									AppConfig.CurrentSystemSettingInfo.DisableCloseWindowWhenAutoReply = false;
									try
									{
										AppConfig.SaveConfig();
										this.CloseOptionTimeDisableCloseWindowWhenAutoReply = null;
									}
									catch (Exception ex)
									{
										Form1.e(ex.ToString());
									}
								}
								Win32Extend.KillProcessByName("iexplore");
							}
						}
					}
					else
					{
						int num = AppConfig.LastRobotReplyMinutes;
						if (DateTime.Now.Hour <= 7)
						{
							num *= 3;
						}
						if ((DateTime.Now - this.ab).TotalMinutes > (double)num)
						{
							AppConfig.NoticeAdministrator(NoticeAdminType.RobotLastReplyTooLongAgo, string.Format("【急】{0}超过{1}分钟未收到消息了", AppConfig.RobotUserNick, num), string.Format("{0}超过{1}分钟未收到消息了，请检查是否掉线了。", AppConfig.RobotUserNick, num));
						}
					}
				}
				if ((DateTime.Now - this.t).TotalSeconds >= 120.0)
				{
					this.t = DateTime.Now;
					if (AppConfig.AllowAutoLogin && AppConfig.AgentSettings.LoginOnQn)
					{
						List<WinAdQn> list = WinAdQn.GetList();
						if (list != null)
						{
							foreach (WinAdQn winAdQn in list)
							{
								if (winAdQn != null)
								{
									winAdQn.Close(true);
								}
							}
						}
					}
				}
				if ((DateTime.Now - this.u).TotalMinutes >= 5.0)
				{
					this.u = DateTime.Now;
					if (AppConfig.AllowAutoLogin)
					{
						List<WindowInfo> windowListByClassAndName8 = Win32Extend.GetWindowListByClassAndName(new WindowStruct("#32770", "关闭事件跟踪程序"), 0, false);
						if (!Util.IsEmptyList<WindowInfo>(windowListByClassAndName8))
						{
							foreach (WindowInfo windowInfo2 in windowListByClassAndName8)
							{
								windowInfo2.Close(true);
							}
						}
						foreach (KeyValuePair<string, QnAgentInfo> keyValuePair in AppConfig.AgentUserDict)
						{
							AliwwWorkBenchQn aliwwWorkBenchQn = AliwwWorkBenchQn.Get(keyValuePair.Value.QnNick);
							if (aliwwWorkBenchQn != null)
							{
								aliwwWorkBenchQn.Close(true);
							}
						}
						List<WindowInfo> windowListByClassAndName9 = Win32Extend.GetWindowListByClassAndName(new WindowStruct("#32770", "千牛数据升级"), 0, false);
						if (!Util.IsEmptyList<WindowInfo>(windowListByClassAndName9))
						{
							foreach (WindowInfo windowInfo3 in windowListByClassAndName9)
							{
								windowInfo3.Close(true);
							}
						}
						if (AppConfig.AgentSettings.LoginOnQn)
						{
							List<WindowInfo> allDesktopWindows = Win32Extend.GetAllDesktopWindows();
							foreach (WindowInfo windowInfo4 in allDesktopWindows)
							{
								if (windowInfo4.Info.ClassName == "#32770" && windowInfo4.Info.WindowName == "消息提示")
								{
									windowInfo4.Close(true);
								}
								else if (windowInfo4.Info.ClassName == "#32770" && windowInfo4.Info.WindowName == "系统消息")
								{
									windowInfo4.Close(true);
								}
							}
						}
						Win32Extend.KillProcessByNameWithCmd("WerFault");
						if (AppConfig.AgentSettings.LoginOnAliwwBuyer)
						{
							Process[] processesByName = Process.GetProcessesByName("CrashDumper");
							if (processesByName != null && processesByName.Length != 0)
							{
								foreach (Process process2 in processesByName)
								{
									try
									{
										using (process2)
										{
											if (process2.MainModule.FileName.Contains("C:\\Program Files (x86)\\AliWangWang"))
											{
												Win32Extend.KillProcess(process2);
											}
										}
									}
									catch
									{
									}
								}
							}
						}
					}
				}
				if ((DateTime.Now - this.v).TotalHours >= 6.0)
				{
					this.v = DateTime.Now;
					if (AppConfig.AllowAutoLogin)
					{
						double hardDiskSpace = Util.GetHardDiskSpace("C");
						double hardDiskFreeSpace = Util.GetHardDiskFreeSpace("C");
						if ((hardDiskSpace - hardDiskFreeSpace) / hardDiskSpace >= 0.85)
						{
							try
							{
								Process.Start("C:\\Program Files (x86)\\Agiso\\ClearFile\\ClearFile.exe");
							}
							catch
							{
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
				LogWriter.WriteLog("ReliabilityMonitor：" + ex2.ToString(), 1);
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00002CB5 File Offset: 0x00000EB5
		[CompilerGenerated]
		private void c()
		{
			this.a(true, "");
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00002CC3 File Offset: 0x00000EC3
		[CompilerGenerated]
		private void b()
		{
			this.cv.Enabled = false;
			this.cj.Text = "已暂停";
			this.cy.Enabled = true;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00002CEE File Offset: 0x00000EEE
		[CompilerGenerated]
		private bool a(string A_0)
		{
			return !this.DictKeepAliveForSomeTime.ContainsKey(A_0);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00002CFF File Offset: 0x00000EFF
		[CompilerGenerated]
		private void c(object sender, EventArgs e)
		{
			base.InvokeOnClick(this.bs, null);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00002D0E File Offset: 0x00000F0E
		[CompilerGenerated]
		private bool a()
		{
			return this.g.ThreadState == global::System.Threading.ThreadState.Aborted;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00002D22 File Offset: 0x00000F22
		[CompilerGenerated]
		private void a(int A_0)
		{
			this.bd.Interval = 31000 + A_0 * 1000;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00002D3C File Offset: 0x00000F3C
		[CompilerGenerated]
		private void b(object sender, EventArgs e)
		{
			this.bj.Enabled = false;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00002D4A File Offset: 0x00000F4A
		[CompilerGenerated]
		private void a(object sender, EventArgs e)
		{
			AgentRemoteHttpListener agentRemoteHttpListener = this.@as;
			if (agentRemoteHttpListener != null)
			{
				agentRemoteHttpListener.Stop();
			}
			AgentHttpListener agentHttpListenerInstance = Form1.AgentHttpListenerInstance;
			if (agentHttpListenerInstance != null)
			{
				agentHttpListenerInstance.Stop();
			}
			WebSocketServerIns aliwwWebScoketServer = AppConfig.AliwwWebScoketServer;
			if (aliwwWebScoketServer != null)
			{
				aliwwWebScoketServer.Stop();
			}
			base.Close();
		}

		// Token: 0x040001A5 RID: 421
		private static string a = "ModifyTime";

		// Token: 0x040001A6 RID: 422
		private static string b = "DESC";

		// Token: 0x040001A7 RID: 423
		[CompilerGenerated]
		private global::System.Timers.Timer c;

		// Token: 0x040001A8 RID: 424
		[CompilerGenerated]
		private global::System.Timers.Timer d;

		// Token: 0x040001A9 RID: 425
		[CompilerGenerated]
		private global::System.Timers.Timer e;

		// Token: 0x040001AA RID: 426
		[CompilerGenerated]
		private global::System.Timers.Timer f;

		// Token: 0x040001AB RID: 427
		private Thread g;

		// Token: 0x040001AC RID: 428
		private Dictionary<int, bool> h = new Dictionary<int, bool>();

		// Token: 0x040001AD RID: 429
		private static DateTime? i;

		// Token: 0x040001AE RID: 430
		private static int j = 0;

		// Token: 0x040001AF RID: 431
		private Thread k;

		// Token: 0x040001B0 RID: 432
		private static volatile AliwwMessageInfo l = null;

		// Token: 0x040001B1 RID: 433
		[CompilerGenerated]
		private DateTime m;

		// Token: 0x040001B2 RID: 434
		private DateTime n = DateTime.Now;

		// Token: 0x040001B3 RID: 435
		private DateTime o = DateTime.Now;

		// Token: 0x040001B4 RID: 436
		private DateTime p = DateTime.Now;

		// Token: 0x040001B5 RID: 437
		private DateTime q = DateTime.Now;

		// Token: 0x040001B6 RID: 438
		private DateTime r = DateTime.Now;

		// Token: 0x040001B7 RID: 439
		private DateTime s = DateTime.Now;

		// Token: 0x040001B8 RID: 440
		private DateTime t = DateTime.Now;

		// Token: 0x040001B9 RID: 441
		private DateTime u = DateTime.Now;

		// Token: 0x040001BA RID: 442
		private DateTime v = DateTime.Now;

		// Token: 0x040001BB RID: 443
		private DateTime w = DateTime.Now;

		// Token: 0x040001BC RID: 444
		[CompilerGenerated]
		private DateTime x;

		// Token: 0x040001BD RID: 445
		private List<int> y = new List<int>();

		// Token: 0x040001BE RID: 446
		[CompilerGenerated]
		private DateTime? z;

		// Token: 0x040001BF RID: 447
		private static RobotClient aa;

		// Token: 0x040001C0 RID: 448
		private DateTime ab = DateTime.Now;

		// Token: 0x040001C1 RID: 449
		private int ac = 1;

		// Token: 0x040001C2 RID: 450
		private RobotModType ad = RobotModType.ChatMod;

		// Token: 0x040001C3 RID: 451
		private int ae = 10000;

		// Token: 0x040001C4 RID: 452
		private string af = "【该信息由“agiso自动发货”程序发出】";

		// Token: 0x040001C5 RID: 453
		private string ag = "";

		// Token: 0x040001C6 RID: 454
		private string[] ah = new string[] { "您好{0}，抱歉，没有找到您的订单，我只能帮您查到最近24小时的订单。\n或者您提供下您的订单号我再帮您找找？", "您好{0}，没有找到您的订单哦，我只能查到最近24小时的订单。\n或者您提供下您的订单号我再帮您找找？", "您好{0}，您提供了订单号了吗？如果不懂得订单号是什么？\n进入“我的淘宝-->已买到的宝贝”，Ctrl+F搜索“订单号”你就知道啦！", "您好{0}，我现在就只认得订单号哦。如果不懂得订单号是什么？\n进入“我的淘宝-->已买到的宝贝”，Ctrl+F搜索“订单号”你就知道啦！" };

		// Token: 0x040001C7 RID: 455
		private string[] ai = new string[] { "您好{0}，您提供的订单号“{1}”，没找到这个订单哦，是不是卖家设置错了，咱们先去问问卖家吧。", "您提供的订单号“{1}”，没找到这个订单哦，是不是卖家设置错了，咱们先去问问卖家吧。", "没找到订单号“{1}”的订单哦，找卖家问问看吧。", "没找到订单号“{1}”的订单哦，是不是卖家设置错了，咱们先去问问卖家吧。对了，用购买的旺旺联系了吗？", "没找到订单号“{1}”的订单哦，要记得用您购买的旺旺号联系我哦，否则查不到的！如果确定没错，就需要联系卖家了！" };

		// Token: 0x040001C8 RID: 456
		private Dictionary<string, RobotAlitwScanInfo> aj = new Dictionary<string, RobotAlitwScanInfo>();

		// Token: 0x040001C9 RID: 457
		private bool ak = false;

		// Token: 0x040001CA RID: 458
		private int al = 30000;

		// Token: 0x040001CB RID: 459
		private int am = 17280000;

		// Token: 0x040001CC RID: 460
		[CompilerGenerated]
		private Dictionary<string, DateTime> an;

		// Token: 0x040001CD RID: 461
		private static List<string> ao = new List<string>();

		// Token: 0x040001CE RID: 462
		private DateTime ap;

		// Token: 0x040001CF RID: 463
		private DateTime aq;

		// Token: 0x040001D0 RID: 464
		[CompilerGenerated]
		private DataRow ar;

		// Token: 0x040001D1 RID: 465
		private AgentRemoteHttpListener @as = null;

		// Token: 0x040001D2 RID: 466
		public static AgentHttpListener AgentHttpListenerInstance = null;

		// Token: 0x040001D3 RID: 467
		private string[] at;

		// Token: 0x040001D4 RID: 468
		private int au = 0;

		// Token: 0x040001D5 RID: 469
		private int av = 0;

		// Token: 0x040001D6 RID: 470
		private Dictionary<string, DateTime> aw = new Dictionary<string, DateTime>();

		// Token: 0x040001D7 RID: 471
		private DateTime ax = DateTime.Now;

		// Token: 0x040001D8 RID: 472
		private Dictionary<string, bool> ay = new Dictionary<string, bool>();

		// Token: 0x040001D9 RID: 473
		private DateTime az = DateTime.Now;

		// Token: 0x040001DA RID: 474
		private static int a0 = 0;

		// Token: 0x040001DB RID: 475
		private static object a1 = new object();

		// Token: 0x040001DC RID: 476
		private static int a2 = 0;

		// Token: 0x040001DE RID: 478
		private NotifyIcon a4;

		// Token: 0x040001DF RID: 479
		private ContextMenuStrip a5;

		// Token: 0x040001E0 RID: 480
		private ToolStripMenuItem a6;

		// Token: 0x040001E1 RID: 481
		private TabControl a7;

		// Token: 0x040001E2 RID: 482
		private TabPage a8;

		// Token: 0x040001E3 RID: 483
		private TabPage a9;

		// Token: 0x040001E4 RID: 484
		private Button ba;

		// Token: 0x040001E5 RID: 485
		private Button bb;

		// Token: 0x040001E6 RID: 486
		private TextBox bc;

		// Token: 0x040001E7 RID: 487
		private global::System.Windows.Forms.Timer bd;

		// Token: 0x040001E8 RID: 488
		private global::System.Windows.Forms.Timer be;

		// Token: 0x040001E9 RID: 489
		private global::System.Windows.Forms.Timer bf;

		// Token: 0x040001EA RID: 490
		private ContextMenuStrip bg;

		// Token: 0x040001EB RID: 491
		private ToolStripMenuItem bh;

		// Token: 0x040001EC RID: 492
		private Button bi;

		// Token: 0x040001ED RID: 493
		private Button bj;

		// Token: 0x040001EE RID: 494
		private Panel bk;

		// Token: 0x040001EF RID: 495
		private DataGridView bl;

		// Token: 0x040001F0 RID: 496
		private Panel bm;

		// Token: 0x040001F1 RID: 497
		private Panel bn;

		// Token: 0x040001F2 RID: 498
		private TabPage bo;

		// Token: 0x040001F3 RID: 499
		private SplitContainer bp;

		// Token: 0x040001F4 RID: 500
		private Panel bq;

		// Token: 0x040001F5 RID: 501
		private Button br;

		// Token: 0x040001F6 RID: 502
		private Button bs;

		// Token: 0x040001F7 RID: 503
		private ComboBox bt;

		// Token: 0x040001F8 RID: 504
		private DateTimePicker bu;

		// Token: 0x040001F9 RID: 505
		private Label bv;

		// Token: 0x040001FA RID: 506
		private TextBox bw;

		// Token: 0x040001FB RID: 507
		private Label bx;

		// Token: 0x040001FC RID: 508
		private TextBox by;

		// Token: 0x040001FD RID: 509
		private Label bz;

		// Token: 0x040001FE RID: 510
		private TabPage b0;

		// Token: 0x040001FF RID: 511
		private Panel b1;

		// Token: 0x04000200 RID: 512
		private Button b2;

		// Token: 0x04000201 RID: 513
		private Button b3;

		// Token: 0x04000202 RID: 514
		private Label b4;

		// Token: 0x04000203 RID: 515
		private TextBox b5;

		// Token: 0x04000204 RID: 516
		private Label b6;

		// Token: 0x04000205 RID: 517
		private SplitContainer b7;

		// Token: 0x04000206 RID: 518
		private Label b8;

		// Token: 0x04000207 RID: 519
		private TextBox b9;

		// Token: 0x04000208 RID: 520
		private Button ca;

		// Token: 0x04000209 RID: 521
		private Button cb;

		// Token: 0x0400020A RID: 522
		private DataGridView cc;

		// Token: 0x0400020B RID: 523
		private ComboBox cd;

		// Token: 0x0400020C RID: 524
		private ListView ce;

		// Token: 0x0400020D RID: 525
		private Button cf;

		// Token: 0x0400020E RID: 526
		private Button cg;

		// Token: 0x0400020F RID: 527
		private ContextMenuStrip ch;

		// Token: 0x04000210 RID: 528
		private ToolStripMenuItem ci;

		// Token: 0x04000211 RID: 529
		private Label cj;

		// Token: 0x04000212 RID: 530
		private ContextMenuStrip ck;

		// Token: 0x04000213 RID: 531
		private ToolStripMenuItem cl;

		// Token: 0x04000214 RID: 532
		private Panel cm;

		// Token: 0x04000215 RID: 533
		private LinkLabel cn;

		// Token: 0x04000216 RID: 534
		private ToolStripMenuItem co;

		// Token: 0x04000217 RID: 535
		private SplitContainer cp;

		// Token: 0x04000218 RID: 536
		private TextBox cq;

		// Token: 0x04000219 RID: 537
		private Button cr;

		// Token: 0x0400021A RID: 538
		private TextBox cs;

		// Token: 0x0400021B RID: 539
		private ListView ct;

		// Token: 0x0400021C RID: 540
		private global::System.Windows.Forms.Timer cu;

		// Token: 0x0400021D RID: 541
		private global::System.Windows.Forms.Timer cv;

		// Token: 0x0400021E RID: 542
		private LinkLabel cw;

		// Token: 0x0400021F RID: 543
		private Label cx;

		// Token: 0x04000220 RID: 544
		private global::System.Windows.Forms.Timer cy;

		// Token: 0x04000221 RID: 545
		private LinkLabel cz;

		// Token: 0x04000222 RID: 546
		private ToolStripMenuItem c0;

		// Token: 0x04000223 RID: 547
		private LinkLabel c1;

		// Token: 0x04000224 RID: 548
		private Label c2;

		// Token: 0x04000225 RID: 549
		private TextBox c3;

		// Token: 0x04000226 RID: 550
		private Panel c4;

		// Token: 0x04000227 RID: 551
		private Label c5;

		// Token: 0x04000228 RID: 552
		private Button c6;

		// Token: 0x04000229 RID: 553
		public TextBox TxtRoboter;

		// Token: 0x0400022A RID: 554
		private Label c7;

		// Token: 0x0400022B RID: 555
		private Button c8;

		// Token: 0x0400022C RID: 556
		private Button c9;

		// Token: 0x0400022D RID: 557
		private LinkLabel da;

		// Token: 0x0400022E RID: 558
		private LinkLabel db;

		// Token: 0x0400022F RID: 559
		private Button dc;

		// Token: 0x04000230 RID: 560
		private Button dd;

		// Token: 0x04000231 RID: 561
		private LinkLabel de;

		// Token: 0x04000232 RID: 562
		private Button df;

		// Token: 0x04000233 RID: 563
		private DataGridViewCheckBoxColumn dg;

		// Token: 0x04000234 RID: 564
		private DataGridViewTextBoxColumn dh;

		// Token: 0x04000235 RID: 565
		private DataGridViewTextBoxColumn di;

		// Token: 0x04000236 RID: 566
		private DataGridViewTextBoxColumn dj;

		// Token: 0x04000237 RID: 567
		private DataGridViewCheckBoxColumn dk;

		// Token: 0x04000238 RID: 568
		private DataGridViewCheckBoxColumn dl;

		// Token: 0x04000239 RID: 569
		private DataGridViewTextBoxColumn dm;

		// Token: 0x0400023A RID: 570
		private DataGridViewLinkColumn dn;

		// Token: 0x0400023B RID: 571
		private DataGridViewTextBoxColumn dp;

		// Token: 0x0400023C RID: 572
		private DataGridViewTextBoxColumn dq;

		// Token: 0x0400023D RID: 573
		private DataGridViewTextBoxColumn dr;

		// Token: 0x0400023E RID: 574
		private DataGridViewTextBoxColumn ds;

		// Token: 0x0400023F RID: 575
		private DataGridViewTextBoxColumn dt;

		// Token: 0x04000240 RID: 576
		private DataGridViewTextBoxColumn du;

		// Token: 0x04000241 RID: 577
		private DataGridViewTextBoxColumn dv;

		// Token: 0x04000242 RID: 578
		private DataGridViewTextBoxColumn dw;

		// Token: 0x04000243 RID: 579
		private DataGridViewCheckBoxColumn dx;

		// Token: 0x04000244 RID: 580
		private DataGridViewTextBoxColumn dy;

		// Token: 0x04000245 RID: 581
		private DataGridViewTextBoxColumn dz;

		// Token: 0x04000246 RID: 582
		private DataGridViewTextBoxColumn d0;

		// Token: 0x04000247 RID: 583
		private DataGridViewTextBoxColumn d1;

		// Token: 0x04000248 RID: 584
		private DataGridViewTextBoxColumn d2;

		// Token: 0x04000249 RID: 585
		private Label d3;

		// Token: 0x02000042 RID: 66
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x06000286 RID: 646 RVA: 0x00002EAE File Offset: 0x000010AE
			internal bool c(EmotionItem A_0)
			{
				return this.a.Contains(A_0.ShortCut);
			}

			// Token: 0x06000287 RID: 647 RVA: 0x00002EC1 File Offset: 0x000010C1
			internal bool c(Emotion A_0)
			{
				return this.a.Contains(A_0.QuickSymbol) && !this.b.Contains(A_0.QuickSymbol);
			}

			// Token: 0x0400027F RID: 639
			public List<string> a;

			// Token: 0x04000280 RID: 640
			public List<string> b;
		}

		// Token: 0x02000043 RID: 67
		[CompilerGenerated]
		private sealed class c
		{
			// Token: 0x06000289 RID: 649 RVA: 0x00032AE4 File Offset: 0x00030CE4
			internal bool b(AldsAccountInfo A_0)
			{
				bool flag;
				if (!A_0.AutoSendOnOff)
				{
					flag = false;
				}
				else
				{
					UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(A_0.UserNick);
					if (userCacheOrCreate.AccountIsBanned)
					{
						this.a.Add(A_0);
						flag = false;
					}
					else
					{
						flag = true;
					}
				}
				return flag;
			}

			// Token: 0x04000281 RID: 641
			public List<AldsAccountInfo> a;
		}

		// Token: 0x02000044 RID: 68
		[CompilerGenerated]
		private sealed class d
		{
			// Token: 0x0600028B RID: 651 RVA: 0x00032B28 File Offset: 0x00030D28
			internal void c()
			{
				this.b.aj();
				double totalSeconds = (DateTime.Now - this.a).TotalSeconds;
				if (totalSeconds >= 60.0)
				{
					Form1.e(string.Format("杀掉升级程序时异常：{0}", totalSeconds));
				}
			}

			// Token: 0x04000282 RID: 642
			public DateTime a;

			// Token: 0x04000283 RID: 643
			public Form1 b;
		}

		// Token: 0x02000045 RID: 69
		[CompilerGenerated]
		private sealed class e
		{
			// Token: 0x0600028D RID: 653 RVA: 0x00002EED File Offset: 0x000010ED
			internal void c()
			{
				this.a.cv.Interval = this.b;
			}

			// Token: 0x04000284 RID: 644
			public Form1 a;

			// Token: 0x04000285 RID: 645
			public int b;
		}

		// Token: 0x02000046 RID: 70
		[CompilerGenerated]
		private sealed class f
		{
			// Token: 0x0600028F RID: 655 RVA: 0x00032B80 File Offset: 0x00030D80
			internal void c()
			{
				try
				{
					GetAliwwMessageResponse aliwwMessage = AppConfig.WwServiceClient.GetAliwwMessage(AppConfig.ApplicationUuid, this.a, new DateTime?(AppConfig.GetAliwwMsgStartTime));
					if (aliwwMessage.IsError)
					{
						this.b.WriteLine(aliwwMessage.ErrMsg);
						Form1.e(aliwwMessage.ErrMsg);
					}
					else if (!Util.IsEmptyList<AliwwMessage>(aliwwMessage.Message))
					{
						AliwwMessageManager.Insert(aliwwMessage.Message);
						List<long> list = aliwwMessage.Message.Select(new Func<AliwwMessage, long>(Form1.<>c.<>9.c)).ToList<long>();
						AppConfig.WwServiceClient.MsgReceived(list, this.a);
						for (int i = 0; i < aliwwMessage.Message.Count; i++)
						{
							string text = DbUtil.TrimNull(aliwwMessage.Message[i].SellerNick);
							AliwwMessageInfo aliwwMessageInfo = new AliwwMessageInfo();
							aliwwMessageInfo.MsgId = DbUtil.TrimLongNull(aliwwMessage.Message[i].IdNo);
							aliwwMessageInfo.SellerNick = DbUtil.TrimNull(aliwwMessage.Message[i].SellerNick);
							aliwwMessageInfo.BuyerNick = DbUtil.TrimNull(aliwwMessage.Message[i].BuyerNick);
							aliwwMessageInfo.BuyerOpenUid = DbUtil.TrimNull(aliwwMessage.Message[i].BuyerOpenUid);
							aliwwMessageInfo.Tid = DbUtil.TrimLongNull(aliwwMessage.Message[i].Tid);
							aliwwMessageInfo.MessageTitle = DbUtil.TrimNull(aliwwMessage.Message[i].MessageTitle);
							aliwwMessageInfo.MessageBody = DbUtil.TrimNull(aliwwMessage.Message[i].MessageBody);
							aliwwMessageInfo.CreateTime = DbUtil.TrimDateNull(aliwwMessage.Message[i].CreateTime);
							aliwwMessageInfo.AliwwMessageSourceType = EnumAliwwMessageSource.FromWwmsgService;
							aliwwMessageInfo.EnqueueTime = DateTime.Now;
							AppConfig.GetSellerExecuteCache(text).AliwwMsgQueue.Enqueue(aliwwMessageInfo);
						}
					}
				}
				catch (Exception ex)
				{
					this.b.WriteLine("查询待发旺旺消息时失败！如无法解决请联系旺旺agiso！");
					Form1.e(ex.ToString());
				}
			}

			// Token: 0x04000286 RID: 646
			public List<string> a;

			// Token: 0x04000287 RID: 647
			public Form1 b;
		}

		// Token: 0x02000047 RID: 71
		[CompilerGenerated]
		private sealed class g
		{
			// Token: 0x06000291 RID: 657 RVA: 0x00032DE0 File Offset: 0x00030FE0
			internal void c(object sender, EventArgs e)
			{
				if (this.a.Message != null)
				{
					foreach (AliwwNotifyMessage aliwwNotifyMessage in this.a.Message)
					{
						this.b.a(aliwwNotifyMessage.Type, aliwwNotifyMessage.Title, aliwwNotifyMessage.Content);
					}
				}
			}

			// Token: 0x04000288 RID: 648
			public GetNotifyMessageResponse a;

			// Token: 0x04000289 RID: 649
			public Form1 b;
		}

		// Token: 0x02000048 RID: 72
		[CompilerGenerated]
		private sealed class h
		{
			// Token: 0x0400028A RID: 650
			public Form1 a;

			// Token: 0x0400028B RID: 651
			public bool b;
		}

		// Token: 0x02000049 RID: 73
		[CompilerGenerated]
		private sealed class i
		{
			// Token: 0x0400028C RID: 652
			public string a;

			// Token: 0x0400028D RID: 653
			public Form1.h b;
		}

		// Token: 0x0200004A RID: 74
		[CompilerGenerated]
		private sealed class j
		{
			// Token: 0x0400028E RID: 654
			public string a;

			// Token: 0x0400028F RID: 655
			public Form1.i b;
		}

		// Token: 0x0200004B RID: 75
		[CompilerGenerated]
		private sealed class k
		{
			// Token: 0x06000296 RID: 662 RVA: 0x00032E60 File Offset: 0x00031060
			internal void e()
			{
				Form1.l l = new Form1.l();
				l.c = this;
				if (this.d.b.b.a.aj.ContainsKey(this.a))
				{
					l.a = this.d.b.b.a.aj[this.a].Alitw;
					l.b = null;
					try
					{
						l.b = Form1.aa.GetAliwwRobotMessages(this.d.b.a, this.a, this.b);
					}
					catch (Exception ex)
					{
						Form1.m m = new Form1.m();
						m.b = l;
						Exception ex2 = ex;
						m.a = ex2;
						this.d.b.b.a.Invoke(new EventHandler(m.c));
					}
					this.d.b.b.a.Invoke(new EventHandler(l.d));
				}
			}

			// Token: 0x04000290 RID: 656
			public string a;

			// Token: 0x04000291 RID: 657
			public string b;

			// Token: 0x04000292 RID: 658
			public string c;

			// Token: 0x04000293 RID: 659
			public Form1.j d;
		}

		// Token: 0x0200004C RID: 76
		[CompilerGenerated]
		private sealed class l
		{
			// Token: 0x06000298 RID: 664 RVA: 0x00032F84 File Offset: 0x00031184
			internal void d(object sender, EventArgs e)
			{
				Form1.n n = new Form1.n();
				n.b = this;
				if (this.b.IsError)
				{
					string text;
					this.c.d.b.b.a.a(this.a, this.c.d.b.a, this.c.a, this.c.c, "抱歉，查询订单时出现错误，请重试！如果一直出现该提示，请联系旺旺号“agiso”！\r\n" + this.c.d.a, out text);
					LogWriter.WriteLog(this.b.ErrMsg, 2);
					string text2 = string.Format("{0}\t{1}\t【查单时异常】{2}", this.c.d.b.a, this.c.a, text);
					this.c.d.b.b.a.WriteLine(text2);
					if (!this.c.d.b.b.b)
					{
						this.a.CloseCurrentChat();
					}
					this.c.d.b.b.a.aj.Remove(this.c.a);
				}
				else if (this.b.Message == null || this.b.Message.Count == 0)
				{
					string text3 = ((this.c.a == null || !this.c.a.StartsWith("2016040101260705#")) ? this.c.a : "");
					string text4;
					this.c.d.b.b.a.a(this.a, this.c.d.b.a, this.c.a, this.c.c, string.Format(this.c.d.b.b.a.c(string.IsNullOrEmpty(this.c.b) ? 1 : 2), text3, this.c.b) + "\r\n" + this.c.d.a, out text4);
					string text5 = string.Format("{0}\t{1}\t{2}", this.c.d.b.a, this.c.a, text4);
					this.c.d.b.b.a.WriteLine(text5);
					if (!this.c.d.b.b.b)
					{
						this.a.CloseCurrentChat();
					}
					this.c.d.b.b.a.aj.Remove(this.c.a);
				}
				else
				{
					n.a = new List<long>();
					AliwwMessageManager.Insert(this.b.Message);
					foreach (AliwwMessage aliwwMessage in this.b.Message)
					{
						string messageTitle = aliwwMessage.MessageTitle;
						string text6 = aliwwMessage.MessageBody;
						text6 = this.c.d.b.b.a.f(text6);
						string text7 = text6 + "\r\n" + this.c.d.a;
						string text8 = "";
						int num = 0;
						int num2 = 0;
						try
						{
							while (num <= 0 && num2++ < 3)
							{
								num = this.c.d.b.b.a.a(this.a, this.c.d.b.a, this.c.a, this.c.c, text7, out text8);
							}
							goto IL_04EF;
						}
						catch (Exception ex)
						{
							text8 = "失败，异常！";
							Form1.e(ex.ToString());
							goto IL_04EF;
						}
						goto IL_0443;
						IL_0455:
						string text9 = string.Format("{0}\t{1}\t{2}\tIdNo:{3}", new object[]
						{
							this.c.d.b.a,
							this.c.a,
							text8,
							aliwwMessage.IdNo
						});
						this.c.d.b.b.a.WriteLine(text9);
						LogSendResultManager.Insert(aliwwMessage.IdNo, this.c.d.b.a, num, text8, MsgSendSoftware.Undefined);
						continue;
						IL_0443:
						n.a.Add(aliwwMessage.IdNo);
						goto IL_0455;
						IL_04EF:
						if (num <= 0)
						{
							goto IL_0443;
						}
						goto IL_0455;
					}
					if (n.a.Count > 0)
					{
						Task.Run(new Action(n.c));
					}
					this.c.d.b.b.a.aj.Remove(this.c.a);
					if (!this.c.d.b.b.b)
					{
						this.a.CloseCurrentChat();
					}
				}
			}

			// Token: 0x04000294 RID: 660
			public AliwwTalkWindow a;

			// Token: 0x04000295 RID: 661
			public GetAliwwRobotMessagesResponse b;

			// Token: 0x04000296 RID: 662
			public Form1.k c;
		}

		// Token: 0x0200004D RID: 77
		[CompilerGenerated]
		private sealed class m
		{
			// Token: 0x0600029A RID: 666 RVA: 0x00033554 File Offset: 0x00031754
			internal void c(object sender, EventArgs e)
			{
				string text;
				this.b.c.d.b.b.a.a(this.b.a, this.b.c.d.b.a, this.b.c.a, this.b.c.c, "抱歉，查询订单时出现错误，请重试！如果一直出现该提示，请联系旺旺号“agiso”！\r\n" + this.b.c.d.a, out text);
				LogWriter.WriteLog(this.a.ToString(), 2);
				string text2 = string.Format("{0}\t{1}\t【查单时异常】{2}", this.b.c.d.b.a, this.b.c.a, text);
				this.b.c.d.b.b.a.WriteLine(text2);
				if (!this.b.c.d.b.b.b)
				{
					this.b.a.CloseCurrentChat();
				}
				this.b.c.d.b.b.a.aj.Remove(this.b.c.a);
			}

			// Token: 0x04000297 RID: 663
			public Exception a;

			// Token: 0x04000298 RID: 664
			public Form1.l b;
		}

		// Token: 0x0200004E RID: 78
		[CompilerGenerated]
		private sealed class n
		{
			// Token: 0x0600029C RID: 668 RVA: 0x000336CC File Offset: 0x000318CC
			internal void c()
			{
				try
				{
					FailAliwwRobotMessagesResponse failAliwwRobotMessagesResponse = Form1.aa.FailAliwwRobotMessages(this.a);
					if (failAliwwRobotMessagesResponse.IsError)
					{
						LogWriter.WriteLog(failAliwwRobotMessagesResponse.ErrMsg, 1);
						this.b.c.d.b.b.a.WriteLine(string.Format("重置消息失败,{0}:{1}", failAliwwRobotMessagesResponse.ErrMsg, string.Join("、", this.a.ConvertAll<string>(new Converter<long, string>(Form1.<>c.<>9.b)).ToArray())));
					}
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog(ex.ToString(), 1);
					this.b.c.d.b.b.a.WriteLine("重置消息失败:" + string.Join("、", this.a.ConvertAll<string>(new Converter<long, string>(Form1.<>c.<>9.a)).ToArray()));
				}
			}

			// Token: 0x04000299 RID: 665
			public List<long> a;

			// Token: 0x0400029A RID: 666
			public Form1.l b;
		}

		// Token: 0x0200004F RID: 79
		[CompilerGenerated]
		private sealed class o
		{
			// Token: 0x0600029E RID: 670 RVA: 0x00033808 File Offset: 0x00031A08
			internal void c()
			{
				try
				{
					List<string> list = AppConfig.AliwwMsgDictOrderByEnqueueTime.Values.Select(new Func<string, string>(Form1.<>c.<>9.d)).ToList<string>();
					if (Form1.AmiThreadProcessing != null)
					{
						list.Add(Form1.AmiThreadProcessing.SellerNick);
					}
					int num = list.Distinct<string>().Count<string>();
					int num2 = AppConfig.AliwwMsgIdListOrderByEnqueueTime.Count + ((Form1.AmiThreadProcessing != null) ? 1 : 0);
					GetAgentAliwwMessageResponse aliwwMessage = AppConfig.QnAgentServiceClient.GetAliwwMessage(num, num2, this.a);
					if (aliwwMessage.IsError)
					{
						if (aliwwMessage.ErrMsg.Contains("操作超时") && ++Form1.a0 >= 2)
						{
							this.b.ClearAllQnProc(true, true, true, true, string.Format("连续{0}次网络超时，杀掉所有千牛", Form1.a0));
						}
						this.b.WriteLine("获取消息失败，" + aliwwMessage.ErrMsg);
						Form1.e("获取消息失败，" + aliwwMessage.ErrMsg);
					}
					else
					{
						Form1.a0 = 0;
						if (!Util.IsEmptyList<AliwwMessage>(aliwwMessage.Message))
						{
							AliwwMessageManager.Insert(aliwwMessage.Message);
							List<long> list2 = aliwwMessage.Message.Select(new Func<AliwwMessage, long>(Form1.<>c.<>9.b)).ToList<long>();
							AppConfig.QnAgentServiceClient.MsgReceived(list2);
							aliwwMessage.Message = aliwwMessage.Message.OrderBy(new Func<AliwwMessage, DateTime?>(Form1.<>c.<>9.a)).ToList<AliwwMessage>();
							for (int i = 0; i < aliwwMessage.Message.Count; i++)
							{
								AliwwMessageInfo aliwwMessageInfo = new AliwwMessageInfo();
								aliwwMessageInfo.MsgId = aliwwMessage.Message[i].IdNo;
								aliwwMessageInfo.SellerNick = aliwwMessage.Message[i].SellerNick;
								aliwwMessageInfo.BuyerNick = aliwwMessage.Message[i].BuyerNick;
								aliwwMessageInfo.BuyerOpenUid = aliwwMessage.Message[i].BuyerOpenUid;
								aliwwMessageInfo.Tid = aliwwMessage.Message[i].Tid.GetValueOrDefault();
								aliwwMessageInfo.MessageTitle = aliwwMessage.Message[i].MessageTitle;
								aliwwMessageInfo.MessageBody = aliwwMessage.Message[i].MessageBody;
								aliwwMessageInfo.CreateTime = aliwwMessage.Message[i].CreateTime ?? DateTime.Now;
								aliwwMessageInfo.AliwwMessageSourceType = EnumAliwwMessageSource.FromWwmsgService;
								aliwwMessageInfo.EnqueueTime = DateTime.Now;
								if (aliwwMessageInfo.MessageBody.IsActivateMsg())
								{
									if (string.IsNullOrEmpty(aliwwMessageInfo.BuyerOpenUid) && AppConfig.RobotUserNickOpenUid.ContainsKey(aliwwMessageInfo.BuyerNick))
									{
										aliwwMessageInfo.BuyerOpenUid = AppConfig.RobotUserNickOpenUid[aliwwMessageInfo.BuyerNick];
									}
									AppConfig.AliwwMsgQueueFirst.Enqueue(aliwwMessageInfo);
								}
								else
								{
									AppConfig.GetSellerExecuteCache(aliwwMessage.Message[i].SellerNick).AliwwMsgQueue.Enqueue(aliwwMessageInfo);
								}
							}
						}
						if (!Util.IsEmptyList<QnAgentInfo>(aliwwMessage.AgentUser))
						{
							foreach (QnAgentInfo qnAgentInfo in aliwwMessage.AgentUser)
							{
								AppConfig.AgentUserDict[qnAgentInfo.SellerNick] = qnAgentInfo;
								AppConfig.GetUserCacheOrCreate(qnAgentInfo.QnNick).CurrentWorksheet = (qnAgentInfo.CustomerServiceNewVersion ? qnAgentInfo.CustomerServiceWorksheetInfo : new List<CustomerServiceWorksheet>());
							}
						}
						this.b.ae();
						this.b.a(aliwwMessage);
					}
				}
				catch (Exception ex)
				{
					this.b.WriteLine("查询待发旺旺消息时失败！如无法解决请联系旺旺agiso！");
					Form1.e(ex.ToString());
				}
			}

			// Token: 0x0400029B RID: 667
			public List<string> a;

			// Token: 0x0400029C RID: 668
			public Form1 b;
		}

		// Token: 0x02000050 RID: 80
		[CompilerGenerated]
		private sealed class p
		{
			// Token: 0x060002A0 RID: 672 RVA: 0x00002F05 File Offset: 0x00001105
			internal void c(object sender, EventArgs e)
			{
				this.a.a(this.b.Type, this.b.Title, this.b.Content);
			}

			// Token: 0x0400029D RID: 669
			public Form1 a;

			// Token: 0x0400029E RID: 670
			public NotifyMsg b;
		}

		// Token: 0x02000051 RID: 81
		[CompilerGenerated]
		private sealed class q
		{
			// Token: 0x060002A2 RID: 674 RVA: 0x00002F33 File Offset: 0x00001133
			internal bool c(SellerSendMsgInfo A_0)
			{
				return A_0.SellerNick == this.a.Value.SellerNick;
			}

			// Token: 0x060002A3 RID: 675 RVA: 0x00002F33 File Offset: 0x00001133
			internal bool b(SellerSendMsgInfo A_0)
			{
				return A_0.SellerNick == this.a.Value.SellerNick;
			}

			// Token: 0x0400029F RID: 671
			public KeyValuePair<string, QnAgentInfo> a;
		}

		// Token: 0x02000052 RID: 82
		[CompilerGenerated]
		private sealed class r
		{
			// Token: 0x060002A5 RID: 677 RVA: 0x00002F51 File Offset: 0x00001151
			internal void c()
			{
				this.a.a(this.b);
			}

			// Token: 0x040002A0 RID: 672
			public Form1 a;

			// Token: 0x040002A1 RID: 673
			public bool b;
		}

		// Token: 0x02000053 RID: 83
		[CompilerGenerated]
		private sealed class s
		{
			// Token: 0x060002A7 RID: 679 RVA: 0x00002F64 File Offset: 0x00001164
			internal void c()
			{
				this.a.b(this.b.UserNick, "开启智能答复");
			}

			// Token: 0x040002A2 RID: 674
			public Form1 a;

			// Token: 0x040002A3 RID: 675
			public AldsAccountInfo b;
		}

		// Token: 0x02000054 RID: 84
		[CompilerGenerated]
		private sealed class t
		{
			// Token: 0x060002A9 RID: 681 RVA: 0x00033C50 File Offset: 0x00031E50
			internal bool b(WindowInfo A_0)
			{
				bool flag;
				if (!A_0.Info.WindowName.EndsWith(" - 工作台") && !A_0.Info.WindowName.EndsWith("-千牛工作台") && !A_0.Info.WindowName.EndsWith(" - 接待中心") && !A_0.Info.WindowName.EndsWith("-接待中心"))
				{
					flag = false;
				}
				else
				{
					string text = Util.StrConvSimple(A_0.Info.WindowName.Replace(" - 工作台", "").Replace("-千牛工作台", "").Replace(" - 接待中心", "")
						.Replace("-接待中心", ""));
					if (this.a.Contains(text))
					{
						flag = false;
					}
					else
					{
						this.a.Add(text);
						flag = true;
					}
				}
				return flag;
			}

			// Token: 0x040002A4 RID: 676
			public List<string> a;
		}

		// Token: 0x02000055 RID: 85
		[CompilerGenerated]
		private sealed class u
		{
			// Token: 0x060002AB RID: 683 RVA: 0x00002F82 File Offset: 0x00001182
			internal void d()
			{
				this.a.a(this.b, AppConfig.ApplicationUuid, false, this.c);
			}

			// Token: 0x040002A5 RID: 677
			public Form1 a;

			// Token: 0x040002A6 RID: 678
			public DateTime b;

			// Token: 0x040002A7 RID: 679
			public List<string> c;
		}

		// Token: 0x02000056 RID: 86
		[CompilerGenerated]
		private sealed class v
		{
			// Token: 0x060002AD RID: 685 RVA: 0x00002FA1 File Offset: 0x000011A1
			internal void d()
			{
				this.a.a(this.b, AppConfig.ApplicationUuid, true, this.c);
			}

			// Token: 0x040002A8 RID: 680
			public Form1 a;

			// Token: 0x040002A9 RID: 681
			public DateTime b;

			// Token: 0x040002AA RID: 682
			public List<string> c;
		}

		// Token: 0x02000057 RID: 87
		[CompilerGenerated]
		private sealed class w
		{
			// Token: 0x060002AF RID: 687 RVA: 0x00002FC0 File Offset: 0x000011C0
			internal void c()
			{
				this.b.b(this.a.UserNick, "账号登录");
			}

			// Token: 0x040002AB RID: 683
			public AldsAccountInfo a;

			// Token: 0x040002AC RID: 684
			public Form1 b;
		}

		// Token: 0x02000058 RID: 88
		[CompilerGenerated]
		private sealed class x
		{
			// Token: 0x060002B1 RID: 689 RVA: 0x00033D3C File Offset: 0x00031F3C
			internal void c(string A_0)
			{
				try
				{
					if (!this.a.bc.IsDisposed)
					{
						this.a.bc.AppendText(this.b);
					}
				}
				catch
				{
				}
			}

			// Token: 0x040002AD RID: 685
			public Form1 a;

			// Token: 0x040002AE RID: 686
			public string b;
		}

		// Token: 0x02000059 RID: 89
		[CompilerGenerated]
		private sealed class y
		{
			// Token: 0x060002B3 RID: 691 RVA: 0x00002FDE File Offset: 0x000011DE
			internal void b()
			{
				this.a.Value.RoolbackMsgs(false);
			}

			// Token: 0x040002AF RID: 687
			public KeyValuePair<string, SellerCache> a;
		}

		// Token: 0x0200005A RID: 90
		[CompilerGenerated]
		private sealed class z
		{
			// Token: 0x060002B5 RID: 693 RVA: 0x00002FF1 File Offset: 0x000011F1
			internal bool b(QnAgentInfo A_0)
			{
				return A_0.SellerNick != this.a.SellerNick;
			}

			// Token: 0x040002B0 RID: 688
			public AliwwMessageInfo a;
		}

		// Token: 0x0200005B RID: 91
		[CompilerGenerated]
		private sealed class aa
		{
			// Token: 0x060002B7 RID: 695 RVA: 0x00033D8C File Offset: 0x00031F8C
			internal bool b()
			{
				return this.a.QueryDsrComplete;
			}

			// Token: 0x040002B1 RID: 689
			public UserCache a;
		}

		// Token: 0x0200005C RID: 92
		[CompilerGenerated]
		private sealed class ab
		{
			// Token: 0x060002B9 RID: 697 RVA: 0x00003009 File Offset: 0x00001209
			internal void k()
			{
				this.a.by.Text = this.b;
			}

			// Token: 0x060002BA RID: 698 RVA: 0x00003021 File Offset: 0x00001221
			internal void j()
			{
				this.a.bw.Text = this.c;
			}

			// Token: 0x060002BB RID: 699 RVA: 0x00003039 File Offset: 0x00001239
			internal void i()
			{
				this.a.bu.Value = this.d.Value;
			}

			// Token: 0x060002BC RID: 700 RVA: 0x00003056 File Offset: 0x00001256
			internal void h()
			{
				this.a.bt.SelectedIndex = this.e;
			}

			// Token: 0x060002BD RID: 701 RVA: 0x0000306E File Offset: 0x0000126E
			internal void g()
			{
				this.a.c3.Text = this.f;
			}

			// Token: 0x040002B2 RID: 690
			public Form1 a;

			// Token: 0x040002B3 RID: 691
			public string b;

			// Token: 0x040002B4 RID: 692
			public string c;

			// Token: 0x040002B5 RID: 693
			public DateTime? d;

			// Token: 0x040002B6 RID: 694
			public int e;

			// Token: 0x040002B7 RID: 695
			public string f;
		}

		// Token: 0x0200005D RID: 93
		[CompilerGenerated]
		private sealed class ac
		{
			// Token: 0x060002BF RID: 703 RVA: 0x00003086 File Offset: 0x00001286
			internal void b()
			{
				AppConfig.UserList.Remove(AppConfig.UserDict[this.a]);
			}

			// Token: 0x040002B8 RID: 696
			public string a;
		}
	}
}
