using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Agiso;
using Agiso.AliwwApi.Wangwang;
using Agiso.DBAccess;
using Agiso.DbManager;
using Agiso.Handler;
using Agiso.Object;
using Agiso.Utils;
using Agiso.WwService.Sdk;
using Agiso.WwService.Sdk.Domain;
using Agiso.WwService.Sdk.Response;
using Agiso.WwWebSocket.Model;
using AliwwClient.Cache;
using AliwwClient.Manager;
using Newtonsoft.Json;

namespace AliwwClient.Server
{
	// Token: 0x0200008E RID: 142
	public class AgentRemoteHttpListener
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060003DD RID: 989 RVA: 0x0003AAE0 File Offset: 0x00038CE0
		// (remove) Token: 0x060003DE RID: 990 RVA: 0x0003AB18 File Offset: 0x00038D18
		public event Action<object> OnRestart
		{
			[CompilerGenerated]
			add
			{
				Action<object> action = this.e;
				Action<object> action2;
				do
				{
					action2 = action;
					Action<object> action3 = (Action<object>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<object>>(ref this.e, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<object> action = this.e;
				Action<object> action2;
				do
				{
					action2 = action;
					Action<object> action3 = (Action<object>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<object>>(ref this.e, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060003DF RID: 991 RVA: 0x0003AB50 File Offset: 0x00038D50
		// (remove) Token: 0x060003E0 RID: 992 RVA: 0x0003AB88 File Offset: 0x00038D88
		public event Action<bool> OnUpgrade
		{
			[CompilerGenerated]
			add
			{
				Action<bool> action = this.f;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> action3 = (Action<bool>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref this.f, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<bool> action = this.f;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> action3 = (Action<bool>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref this.f, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060003E1 RID: 993 RVA: 0x0003ABC0 File Offset: 0x00038DC0
		// (remove) Token: 0x060003E2 RID: 994 RVA: 0x0003ABF8 File Offset: 0x00038DF8
		public event Func<long, string, string, MsgSendSoftware, bool, string, ErrCodeInfo> OnScreenshot
		{
			[CompilerGenerated]
			add
			{
				Func<long, string, string, MsgSendSoftware, bool, string, ErrCodeInfo> func = this.g;
				Func<long, string, string, MsgSendSoftware, bool, string, ErrCodeInfo> func2;
				do
				{
					func2 = func;
					Func<long, string, string, MsgSendSoftware, bool, string, ErrCodeInfo> func3 = (Func<long, string, string, MsgSendSoftware, bool, string, ErrCodeInfo>)Delegate.Combine(func2, value);
					func = Interlocked.CompareExchange<Func<long, string, string, MsgSendSoftware, bool, string, ErrCodeInfo>>(ref this.g, func3, func2);
				}
				while (func != func2);
			}
			[CompilerGenerated]
			remove
			{
				Func<long, string, string, MsgSendSoftware, bool, string, ErrCodeInfo> func = this.g;
				Func<long, string, string, MsgSendSoftware, bool, string, ErrCodeInfo> func2;
				do
				{
					func2 = func;
					Func<long, string, string, MsgSendSoftware, bool, string, ErrCodeInfo> func3 = (Func<long, string, string, MsgSendSoftware, bool, string, ErrCodeInfo>)Delegate.Remove(func2, value);
					func = Interlocked.CompareExchange<Func<long, string, string, MsgSendSoftware, bool, string, ErrCodeInfo>>(ref this.g, func3, func2);
				}
				while (func != func2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060003E3 RID: 995 RVA: 0x0003AC30 File Offset: 0x00038E30
		// (remove) Token: 0x060003E4 RID: 996 RVA: 0x0003AC68 File Offset: 0x00038E68
		public event Func<string> OnGetHostVersion
		{
			[CompilerGenerated]
			add
			{
				Func<string> func = this.h;
				Func<string> func2;
				do
				{
					func2 = func;
					Func<string> func3 = (Func<string>)Delegate.Combine(func2, value);
					func = Interlocked.CompareExchange<Func<string>>(ref this.h, func3, func2);
				}
				while (func != func2);
			}
			[CompilerGenerated]
			remove
			{
				Func<string> func = this.h;
				Func<string> func2;
				do
				{
					func2 = func;
					Func<string> func3 = (Func<string>)Delegate.Remove(func2, value);
					func = Interlocked.CompareExchange<Func<string>>(ref this.h, func3, func2);
				}
				while (func != func2);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060003E5 RID: 997 RVA: 0x0003ACA0 File Offset: 0x00038EA0
		// (remove) Token: 0x060003E6 RID: 998 RVA: 0x0003ACD8 File Offset: 0x00038ED8
		public event Action<long> OnSendAgain
		{
			[CompilerGenerated]
			add
			{
				Action<long> action = this.i;
				Action<long> action2;
				do
				{
					action2 = action;
					Action<long> action3 = (Action<long>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<long>>(ref this.i, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<long> action = this.i;
				Action<long> action2;
				do
				{
					action2 = action;
					Action<long> action3 = (Action<long>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<long>>(ref this.i, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060003E7 RID: 999 RVA: 0x0003AD10 File Offset: 0x00038F10
		// (remove) Token: 0x060003E8 RID: 1000 RVA: 0x0003AD48 File Offset: 0x00038F48
		public event Func<int, string> OnStopAgentWwMsg
		{
			[CompilerGenerated]
			add
			{
				Func<int, string> func = this.j;
				Func<int, string> func2;
				do
				{
					func2 = func;
					Func<int, string> func3 = (Func<int, string>)Delegate.Combine(func2, value);
					func = Interlocked.CompareExchange<Func<int, string>>(ref this.j, func3, func2);
				}
				while (func != func2);
			}
			[CompilerGenerated]
			remove
			{
				Func<int, string> func = this.j;
				Func<int, string> func2;
				do
				{
					func2 = func;
					Func<int, string> func3 = (Func<int, string>)Delegate.Remove(func2, value);
					func = Interlocked.CompareExchange<Func<int, string>>(ref this.j, func3, func2);
				}
				while (func != func2);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0003AD80 File Offset: 0x00038F80
		public Dictionary<Guid, ErrCodeInfo> DictTaskInfo
		{
			get
			{
				return this.k;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0003AD98 File Offset: 0x00038F98
		public bool IsRuning
		{
			get
			{
				return this.b.IsAlive;
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0003ADB0 File Offset: 0x00038FB0
		public void Start()
		{
			this.a = new HttpListener();
			this.a.Prefixes.Add(string.Format("http://*:{0}/", 30002));
			this.a.Start();
			this.b = new Thread(new ThreadStart(this.b));
			this.b.Start();
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000358E File Offset: 0x0000178E
		public void Stop()
		{
			this.a.Close();
			this.b.Abort();
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0003AE1C File Offset: 0x0003901C
		private void b()
		{
			for (;;)
			{
				HttpListenerContext httpListenerContext = null;
				try
				{
					if (!this.a.IsListening)
					{
						break;
					}
					httpListenerContext = this.a.GetContext();
					AjaxResult ajaxResult = new AjaxResult();
					HttpListenerRequest request = httpListenerContext.Request;
					HttpListenerResponse response = httpListenerContext.Response;
					if (!request.IsLocal)
					{
						string text = request.RemoteEndPoint.Address.ToString();
						if (!AppConfig.AgentControlServerIPs.Contains(text))
						{
							LogWriter.WriteLog("无权访问IP：" + text, 1);
							response.WriteLine(ajaxResult.CreateErrArro("无权访问"));
							continue;
						}
					}
					string text2 = request.RawUrl;
					int num = text2.IndexOf('?');
					if (num >= 0)
					{
						text2 = text2.Substring(num + 1);
					}
					NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(text2);
					foreach (string text3 in nameValueCollection.AllKeys)
					{
						nameValueCollection[text3] = HttpUtility.UrlDecode(nameValueCollection[text3]);
					}
					StreamReader streamReader = new StreamReader(request.InputStream);
					string text4 = streamReader.ReadToEnd();
					if (text4.Length > 0)
					{
						NameValueCollection nameValueCollection2 = HttpUtility.ParseQueryString(text4);
						foreach (string text5 in nameValueCollection2.AllKeys)
						{
							nameValueCollection[text5] = HttpUtility.UrlDecode(nameValueCollection2[text5]);
						}
					}
					response.StatusCode = 200;
					response.AddHeader("Access-Control-Allow-Origin", "*");
					string text6 = nameValueCollection["method"];
					if (string.IsNullOrEmpty(text6))
					{
						response.WriteLine(ajaxResult.CreateErrArro("method为空"));
					}
					else
					{
						string text7 = nameValueCollection["timestamp"];
						if (!request.IsLocal)
						{
							if (Util.TimeSpanToDateTime(text7).AddMinutes(5.0) < DateTime.Now)
							{
								response.WriteLine(ajaxResult.CreateErrArro("超时"));
								continue;
							}
							Dictionary<string, string> dictionary = new Dictionary<string, string>();
							foreach (string text8 in nameValueCollection.AllKeys)
							{
								if (text8 != "sign" && text8[0] != '_')
								{
									dictionary.Add(text8, nameValueCollection[text8]);
								}
							}
							string text9 = Utils.SignRequest(dictionary, AppConfig.AgentControlSecret);
							if (!text9.Equals(nameValueCollection["sign"], StringComparison.CurrentCultureIgnoreCase))
							{
								response.WriteLine(ajaxResult.CreateErrArro("签名错误"));
								continue;
							}
						}
						string text10 = text6.ToLower();
						string text11 = text10;
						uint num2 = p.a(text11);
						if (num2 <= 2153434393U)
						{
							if (num2 <= 346818685U)
							{
								if (num2 <= 88245869U)
								{
									if (num2 != 40025559U)
									{
										if (num2 != 53498389U)
										{
											if (num2 == 88245869U)
											{
												if (text11 == "getsentlog")
												{
													long num3 = Util.ToLong(nameValueCollection["tid"]);
													string text12 = nameValueCollection["buyerNick"];
													DateTime? dateTime = Util.ToDateTime(nameValueCollection["msgDateFrom"]);
													DateTime? dateTime2 = Util.ToDateTime(nameValueCollection["msgDateEnd"]);
													int num4 = Util.ToInt(nameValueCollection["status"]);
													string masterNick = Util.GetMasterNick(nameValueCollection["sellerNick"]);
													int num5 = Util.ToInt(nameValueCollection["pageNo"]);
													int num6 = Util.ToInt(nameValueCollection["pageSize"]);
													long num7;
													DataTable dataTable = AliwwMessageManager.Select(out num7, num3, masterNick, text12, null, dateTime, num4, dateTime2, new int?(num5), new int?(num6));
													if (dataTable != null)
													{
														if (dataTable.Columns.Contains("MessageBody"))
														{
															dataTable.Columns.Remove("MessageBody");
														}
														if (dataTable.Columns.Contains("MessageTitle"))
														{
															dataTable.Columns.Remove("MessageTitle");
														}
													}
													ajaxResult.page = num5;
													ajaxResult.records = num7;
													ajaxResult.rows = dataTable;
													response.WriteLine(ajaxResult.CreateSuccArro(""));
													continue;
												}
											}
										}
										else if (text11 == "stopagentwwmsg")
										{
											if (this.j == null)
											{
												continue;
											}
											int num8 = Util.ToInt(nameValueCollection["minute"]);
											if (num8 <= 0)
											{
												response.WriteLine(ajaxResult.CreateErrArro("传递分钟数为0"));
												continue;
											}
											string text13 = this.j(num8);
											if (!string.IsNullOrEmpty(text13))
											{
												response.WriteLine(ajaxResult.CreateErrArro(text13));
												continue;
											}
											response.WriteLine(ajaxResult.CreateSuccArro("停止消息成功，助手将在" + DateTime.Now.AddMinutes((double)num8).ToString("yyyy-MM-dd HH:mm:ss") + "后重新获取消息"));
											continue;
										}
									}
									else if (text11 == "batchsendagain")
									{
										List<long> list = JsonConvert.DeserializeObject<List<long>>(nameValueCollection["msgIds"]);
										foreach (long num9 in list)
										{
											Action<long> action = this.i;
											if (action != null)
											{
												action(num9);
											}
										}
										response.WriteLine(ajaxResult.CreateSuccArro("再发一次的请求已发出"));
										continue;
									}
								}
								else if (num2 != 248531780U)
								{
									if (num2 != 337061389U)
									{
										if (num2 == 346818685U)
										{
											if (text11 == "getsendlog")
											{
												ajaxResult.rows = global::k.a().GetSendLog();
												response.WriteLine(ajaxResult.CreateSuccArro(""));
												continue;
											}
										}
									}
									else if (text11 == "aliwwscreenshot")
									{
										AgentRemoteHttpListener.a a = new AgentRemoteHttpListener.a();
										a.b = this;
										if (this.g == null)
										{
											response.WriteLine(ajaxResult.CreateErrArro("未定义截图方法"));
											continue;
										}
										a.a = Util.ToLong(nameValueCollection["msgId"]);
										DataRow dataRow = AliwwMessageManager.Select(a.a);
										if (dataRow == null)
										{
											continue;
										}
										AgentRemoteHttpListener.b b = new AgentRemoteHttpListener.b();
										b.e = a;
										b.a = AppConfig.GetAgentAccountInfo(DbUtil.TrimNull(dataRow["SellerNick"]));
										if (b.a == null)
										{
											response.WriteLine(ajaxResult.CreateErrArro(b.a.UserNick + "：已不存在该服务器上。"));
											continue;
										}
										b.c = MsgSendSoftware.Undefined;
										string text14 = "";
										DataTable dataTable2 = LogSendResultManager.Select(b.e.a);
										if (dataTable2 != null && dataTable2.Rows != null && dataTable2.Rows.Count > 0)
										{
											int l = dataTable2.Rows.Count - 1;
											while (l >= 0)
											{
												if (DbUtil.TrimIntNull(dataTable2.Rows[l]["SendResultCode"]) != 201)
												{
													l--;
												}
												else
												{
													text14 = DbUtil.TrimNull(dataTable2.Rows[l]["UserNick"]);
													b.c = (MsgSendSoftware)DbUtil.TrimIntNull(dataTable2.Rows[l]["SendSoftware"]);
													IL_07A2:
													if (!string.IsNullOrEmpty(text14) && text14 != b.a.UserNick)
													{
														response.WriteLine(ajaxResult.CreateErrArro(string.Concat(new string[]
														{
															"发送记录对应的“",
															text14,
															"”已不存在，当前代挂账号：",
															b.a.UserNick,
															"。"
														})));
														goto IL_19C1;
													}
													goto IL_080E;
												}
											}
											goto IL_07A2;
										}
										IL_080E:
										b.b = DbUtil.TrimNull(dataRow["BuyerNick"]);
										b.d = Guid.NewGuid();
										Task.Run(new Action(b.f));
										this.k[b.d] = null;
										ajaxResult.rows = b.d.ToString();
										response.WriteLine(ajaxResult.CreateSuccArro("等待截图结果"));
										continue;
									}
								}
								else if (text11 == "syncemotions")
								{
									string text15 = nameValueCollection["sellerNick"];
									AldsAccountInfo agentAccountInfo = AppConfig.GetAgentAccountInfo(text15);
									string text16 = nameValueCollection["quickSymbol"];
									DateTime? dateTime3 = Util.ToDateTime(nameValueCollection["modifyTime"]);
									if (agentAccountInfo == null)
									{
										response.WriteLine(ajaxResult.CreateErrArro("服务器查无代挂账号信息"));
										continue;
									}
									if (dateTime3 == null)
									{
										bool flag = false;
										try
										{
											new EmotionJsonManager(agentAccountInfo.UserNick).Delete(text16, out flag);
										}
										catch
										{
										}
										bool flag2;
										new EmotionXmlManager(agentAccountInfo.UserNick).Delete(text16, out flag2);
										if (!AppConfig.SyncEmotionsUserNicks.Contains(agentAccountInfo.UserNick) && (flag2 || flag))
										{
											AppConfig.SyncEmotionsUserNicks.Add(agentAccountInfo.UserNick);
										}
										response.WriteLine(ajaxResult.CreateSuccArro(""));
										continue;
									}
									IEmotionManager emotionManager = EmotionManagerFactory.Get(agentAccountInfo.UserNick);
									LogSyncEmotion logSyncEmotion = LogSyncEmotionManager.Get(text15);
									DateTime dateTime4 = ((logSyncEmotion != null) ? logSyncEmotion.LastSyncTime : DateTime.MinValue);
									if (dateTime3.Value < dateTime4)
									{
										response.WriteLine(ajaxResult.CreateSuccArro("表情已经同步过，无需重复同步"));
										continue;
									}
									if (!emotionManager.IsFileExists())
									{
										dateTime4 = DateTime.MinValue;
									}
									else
									{
										FileInfo fileInfo = emotionManager.GetFileInfo();
										if (dateTime4 > fileInfo.LastWriteTime)
										{
											dateTime4 = fileInfo.LastWriteTime;
										}
									}
									DateTime now = DateTime.Now;
									GetEmotionsResponse emotions = AppConfig.QnAgentServiceClient.GetEmotions(text15, dateTime4);
									if (emotions.IsError)
									{
										response.WriteLine(ajaxResult.CreateErrArro("获取表情失败"));
										continue;
									}
									if (emotions.Emotions == null || emotions.Emotions.Count <= 0)
									{
										response.WriteLine(ajaxResult.CreateSuccArro(""));
										continue;
									}
									bool flag3;
									if (!emotionManager.Append(emotions.Emotions, out flag3))
									{
										response.WriteLine(ajaxResult.CreateErrArro("同步失败"));
										continue;
									}
									if (!AppConfig.SyncEmotionsUserNicks.Contains(agentAccountInfo.UserNick) && flag3)
									{
										AppConfig.SyncEmotionsUserNicks.Add(agentAccountInfo.UserNick);
									}
									response.WriteLine(ajaxResult.CreateSuccArro(""));
									LogSyncEmotionManager.UpdateOrInsert(text15, agentAccountInfo.UserNick, now);
									continue;
								}
							}
							else if (num2 <= 1199077024U)
							{
								if (num2 != 353368967U)
								{
									if (num2 != 792548698U)
									{
										if (num2 == 1199077024U)
										{
											if (text11 == "getsentresult")
											{
												long num10 = Util.ToLong(nameValueCollection["msgId"]);
												DataTable dataTable3 = LogSendResultManager.Select(num10);
												ajaxResult.page = 1;
												ajaxResult.rows = dataTable3;
												ajaxResult.records = (long)((dataTable3 != null) ? dataTable3.Rows.Count : 0);
												response.WriteLine(ajaxResult.CreateSuccArro(""));
												continue;
											}
										}
									}
									else if (text11 == "getmanualnick")
									{
										string text17 = nameValueCollection["sellerNick"];
										string text18 = "";
										AldsAccountInfo agentAccountInfo2 = AppConfig.GetAgentAccountInfo(text17);
										if (agentAccountInfo2 != null)
										{
											text18 = agentAccountInfo2.ManualNick;
										}
										response.WriteLine("window.AgisoTransferTarget = \"" + text18 + "\";");
										continue;
									}
								}
								else if (text11 == "failhostallmsg")
								{
									global::k.a().RoolbackHostAllMsg();
									Task.Run(new Action(AgentRemoteHttpListener.<>c.<>9.a));
									response.WriteLine(ajaxResult.CreateSuccArro("回滚消息成功"));
									continue;
								}
							}
							else if (num2 <= 1329090924U)
							{
								if (num2 != 1287140103U)
								{
									if (num2 == 1329090924U)
									{
										if (text11 == "setqnversionfilereadaccess")
										{
											bool flag4;
											if (bool.TryParse(nameValueCollection["readOnly"], out flag4))
											{
												Util.SetFileReadAccess(AppConfig.AgentQnSetupIniFileName, flag4);
												response.WriteLine(ajaxResult.CreateSuccArro("Success"));
												continue;
											}
											response.WriteLine(ajaxResult.CreateErrArro("Fail"));
											continue;
										}
									}
								}
								else if (text11 == "geterrorlog")
								{
									DateTime? dateTime5 = Util.ToDateTime(nameValueCollection["date"]);
									int num11 = Util.ToInt(nameValueCollection["pageNo"]);
									int num12 = Util.ToInt(nameValueCollection["pageSize"]);
									if (dateTime5 == null)
									{
										response.WriteLine(ajaxResult.CreateErrArro("时间是必须的"));
										continue;
									}
									int num13;
									DataTable dataTable4 = ErrorLogManager.Get(dateTime5.Value, out num13, num11, num12);
									ajaxResult.rows = dataTable4;
									ajaxResult.page = num11;
									ajaxResult.records = (long)num13;
									response.WriteLine(ajaxResult.CreateSuccArro(""));
									continue;
								}
							}
							else if (num2 != 2130602914U)
							{
								if (num2 == 2153434393U)
								{
									if (text11 == "getversion")
									{
										if (this.h != null)
										{
											FileInfo fileInfo2 = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "AliwwClient.exe");
											string text19 = fileInfo2.LastWriteTime.ToString("MMddHHmm");
											uint memoryLoad = Win32Extend.GetMemoryLoad();
											double hardDiskSpace = Util.GetHardDiskSpace("C");
											double hardDiskFreeSpace = Util.GetHardDiskFreeSpace("C");
											int num14 = 0;
											Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
											if (AppConfig.DictSellerExecuteCache != null && AppConfig.DictSellerExecuteCache.Count > 0)
											{
												new List<KeyValuePair<string, int>>();
												foreach (KeyValuePair<string, SellerCache> keyValuePair in AppConfig.DictSellerExecuteCache)
												{
													num14 += keyValuePair.Value.AliwwMsgQueue.Count;
													if (keyValuePair.Value.AliwwMsgQueue.Count > 0)
													{
														dictionary2[keyValuePair.Key] = keyValuePair.Value.AliwwMsgQueue.Count;
													}
												}
											}
											string version = Util.GetVersion();
											string version2 = WinAliwwMainBuyer9.GetVersion();
											DateTime? dateTime6 = Util.ToDateTime(nameValueCollection["From"]);
											DateTime? dateTime7 = Util.ToDateTime(nameValueCollection["End"]);
											bool flag5 = Util.ToBoolean(nameValueCollection["includeFb"]);
											int num15;
											List<long> failMsgIdList = AliwwMessageManager.GetFailMsgIdList(out num15, dateTime6, dateTime7, flag5);
											d<string, uint, string, string, string, string, bool, bool, QnVersionType, int, int, Dictionary<string, int>, double, int, List<long>> d = new d<string, uint, string, string, string, string, bool, bool, QnVersionType, int, int, Dictionary<string, int>, double, int, List<long>>(text19, memoryLoad, hardDiskSpace.ToString("0.0"), hardDiskFreeSpace.ToString("0.0"), version, version2, AppConfig.AgentSettings.LoginOnQn, AppConfig.AgentSettings.LoginOnAliwwBuyer, AppConfig.AgentSettings.QnVersion, 0, num14, dictionary2, AppConfig.UsedTimeQueue.Avg, num15, failMsgIdList);
											ajaxResult.rows = d;
											response.WriteLine(ajaxResult.CreateSuccArro(""));
											continue;
										}
										continue;
									}
								}
							}
							else if (text11 == "changeqnversion")
							{
								string text20 = nameValueCollection["newQnVersion"];
								if (string.IsNullOrEmpty(text20))
								{
									response.WriteLine(ajaxResult.CreateErrArro("版本号为空"));
									continue;
								}
								text20 = text20.Trim();
								if (!text20.EndsWith("N"))
								{
									text20 += "N";
								}
								if (!Directory.Exists(AppConfig.AgentQnSetupDir + text20))
								{
									response.WriteLine(ajaxResult.CreateErrArro("不存在此版本的目录"));
									continue;
								}
								string text21 = string.Format(AppConfig.QnIniTxtFormat, text20);
								Util.ReWriteFile(AppConfig.AgentQnSetupIniFileName, text21);
								response.WriteLine(ajaxResult.CreateSuccArro("切换版本成功"));
								continue;
							}
						}
						else if (num2 <= 3656943972U)
						{
							if (num2 <= 3182110927U)
							{
								if (num2 != 2374336026U)
								{
									if (num2 != 2782038349U)
									{
										if (num2 == 3182110927U)
										{
											if (text11 == "testsend")
											{
												string text22 = nameValueCollection["buyerNick"];
												string text23 = ((text22 != null) ? text22.Trim() : null);
												string text24 = nameValueCollection["sellerNick"];
												string text25 = ((text24 != null) ? text24.Trim() : null);
												string text26 = nameValueCollection["sendText"];
												AppConfig.TestSendErrorMsg = "";
												AppConfig.AliwwMsgQueueFirst.Enqueue(new AliwwMessageInfo
												{
													MsgId = AppConfig.GetRandomMsgId(),
													AliwwMessageSourceType = EnumAliwwMessageSource.FromTestSend,
													BuyerNick = text23,
													MessageBody = text26,
													SellerNick = text25,
													CreateTime = DateTime.Now,
													CreateTimeLocal = DateTime.Now,
													EnqueueTime = DateTime.Now
												});
												response.WriteLine(ajaxResult.CreateSuccArro(""));
												continue;
											}
										}
									}
									else if (text11 == "sendagain")
									{
										long num16 = Util.ToLong(nameValueCollection["msgId"]);
										Action<long> action2 = this.i;
										if (action2 != null)
										{
											action2(num16);
										}
										response.WriteLine(ajaxResult.CreateSuccArro("再发一次的请求已发出"));
										continue;
									}
								}
								else if (text11 == "pushmsg")
								{
									List<AliwwMessage> list2 = JSON.Decode<List<AliwwMessage>>(nameValueCollection["msgList"]);
									if (Util.IsEmptyList<AliwwMessage>(list2))
									{
										response.WriteLine(ajaxResult.CreateErrArro("消息队列为空"));
										continue;
									}
									for (int m = 0; m < list2.Count; m++)
									{
										AliwwMessageInfo aliwwMessageInfo = new AliwwMessageInfo();
										aliwwMessageInfo.MsgId = list2[m].IdNo;
										aliwwMessageInfo.SellerNick = list2[m].SellerNick;
										aliwwMessageInfo.BuyerNick = list2[m].BuyerNick;
										aliwwMessageInfo.BuyerOpenUid = list2[m].BuyerOpenUid;
										aliwwMessageInfo.Tid = list2[m].Tid.GetValueOrDefault();
										aliwwMessageInfo.MessageTitle = list2[m].MessageTitle;
										aliwwMessageInfo.MessageBody = list2[m].MessageBody;
										aliwwMessageInfo.CreateTime = list2[m].CreateTime ?? DateTime.Now;
										aliwwMessageInfo.AliwwMessageSourceType = EnumAliwwMessageSource.FromWwmsgService;
										aliwwMessageInfo.EnqueueTime = DateTime.Now;
										QnAgentInfo agentInfo = AppConfig.GetAgentInfo(list2[m].SellerNick);
										if (agentInfo == null)
										{
											response.WriteLine(ajaxResult.CreateErrArro("查无代挂账号信息"));
											break;
										}
										AppConfig.GetSellerExecuteCache(list2[m].SellerNick).AliwwMsgQueue.Enqueue(aliwwMessageInfo);
										response.WriteLine(ajaxResult.CreateSuccArro("加入消息队列成功"));
									}
									continue;
								}
							}
							else if (num2 != 3219274948U)
							{
								if (num2 != 3652659876U)
								{
									if (num2 == 3656943972U)
									{
										if (text11 == "dokill")
										{
											Win32Extend.KillProcessByNameWithCmd("AliIM");
											Win32Extend.KillProcessByNameWithCmd("AliExternal");
											Win32Extend.KillProcessByNameWithCmd("AliWorkbench");
											Win32Extend.KillProcessByNameWithCmd("AliApp");
											Win32Extend.KillProcessByNameWithCmd("AliRender");
											Win32Extend.KillProcessByNameWithCmd("ChsIME");
											Win32Extend.KillProcessByNameWithCmd("WerFault");
											AppConfig.RestartExplorer();
											response.WriteLine(ajaxResult.CreateSuccArro("杀千牛成功"));
											continue;
										}
									}
								}
								else if (text11 == "exportmsg")
								{
									this.d = false;
									if (Thread.CurrentThread.GetApartmentState() > ApartmentState.STA)
									{
										Thread thread = new Thread(new ParameterizedThreadStart(this.a));
										thread.SetApartmentState(ApartmentState.STA);
										thread.IsBackground = true;
										thread.Start(ajaxResult);
									}
									else
									{
										this.a(ajaxResult);
									}
									if (Util.CheckWait(2000, new Func<bool>(this.a), 200))
									{
										response.WriteLine(ajaxResult.CreateSuccArro(""));
										continue;
									}
									response.WriteLine(ajaxResult.CreateErrArro(""));
									continue;
								}
							}
							else if (text11 == "changeagenthostsetting")
							{
								GetAgentHostInfoResponse agentHostInfo = AppConfig.QnAgentServiceClient.GetAgentHostInfo();
								if (agentHostInfo.IsError)
								{
									response.WriteLine(agentHostInfo.ErrMsg);
									continue;
								}
								if (agentHostInfo.Setting != null)
								{
									AppConfig.AgentSettings = agentHostInfo.Setting;
								}
								AppConfig.AgentProxyInfo = agentHostInfo.AgentProxyInfo;
								ProxyXmlManager.Handle();
								response.WriteLine(ajaxResult.CreateSuccArro("Save Success"));
								continue;
							}
						}
						else if (num2 <= 3704162098U)
						{
							if (num2 != 3683631926U)
							{
								if (num2 != 3700935799U)
								{
									if (num2 == 3704162098U)
									{
										if (text11 == "getscreenshotresult")
										{
											Guid guid = new Guid(nameValueCollection["guid"]);
											if (!this.k.ContainsKey(guid))
											{
												ajaxResult.ExtendObj = new f<bool>(true);
												response.WriteLine(ajaxResult.CreateErrArro("没有此任务"));
												continue;
											}
											if (this.k[guid] == null)
											{
												ajaxResult.ExtendObj = new f<bool>(false);
												response.WriteLine(ajaxResult.CreateSuccArro(""));
												continue;
											}
											if (this.k[guid].ErrCode == ErrCodeType.CallSuccWw || this.k[guid].ErrCode == ErrCodeType.CallSuccQn)
											{
												ajaxResult.CreateSuccArro("截图成功");
												ajaxResult.ExtendObj = new f<bool>(true);
												Bitmap bitmap = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\AldsScreenshot\\" + guid.ToString() + ".jpg");
												ajaxResult.rows = Convert.ToBase64String(Util.smethod_1(bitmap, ImageFormat.Jpeg));
											}
											else
											{
												ajaxResult.ExtendObj = new f<bool>(true);
												ajaxResult.CreateErrArro("截图失败，失败原因：" + Util.GetEnumDescription(this.k[guid].ErrCode));
											}
											response.WriteLine(ajaxResult);
											continue;
										}
									}
								}
								else if (text11 == "upgrade")
								{
									bool flag6 = Util.ToBoolean(nameValueCollection["failHostAllMsg"]);
									if (this.f != null)
									{
										this.f(flag6);
										response.WriteLine(ajaxResult.CreateSuccArro("升级成功"));
										continue;
									}
									continue;
								}
							}
							else if (text11 == "importmsgcondition")
							{
								long num17 = Util.ToLong(nameValueCollection["tid"]);
								string text27 = nameValueCollection["buyerNick"];
								DateTime? dateTime8 = Util.ToDateTime(nameValueCollection["msgDate"]);
								int num18 = Util.ToInt(nameValueCollection["status"]);
								string text28 = nameValueCollection["sellerNick"];
								global::k.a().SetFormControlValues(num17, text27, dateTime8, num18, text28);
								response.WriteLine(ajaxResult.CreateSuccArro("导入成功"));
								continue;
							}
						}
						else if (num2 <= 3759924213U)
						{
							if (num2 != 3727015018U)
							{
								if (num2 == 3759924213U)
								{
									if (text11 == "failallmsg")
									{
										AgentRemoteHttpListener.c c = new AgentRemoteHttpListener.c();
										c.a = nameValueCollection["sellerNick"];
										if (string.IsNullOrEmpty(c.a))
										{
											response.WriteLine(ajaxResult.CreateErrArro("卖家昵称不能为空"));
											break;
										}
										List<long> list3;
										AppConfig.GetSellerExecuteCache(c.a).RoolbackMsgs(out list3, true);
										AliwwMessageInfo amiThreadProcessing = Form1.AmiThreadProcessing;
										if (((amiThreadProcessing != null) ? amiThreadProcessing.SellerNick : null) == c.a)
										{
											Form1.AmiThreadProcessing.Stop = true;
										}
										ajaxResult.rows = list3;
										Task.Run(new Action(c.c));
										response.WriteLine(ajaxResult.CreateSuccArro("消息置为失败成功"));
										continue;
									}
								}
							}
							else if (text11 == "getagenthostsetting")
							{
								e<bool, bool, bool, int?, int?, string, QnVersionType, bool, bool, bool, string> e = new e<bool, bool, bool, int?, int?, string, QnVersionType, bool, bool, bool, string>(AppConfig.AgentSettings.AllowAutoExitQn, AppConfig.AgentSettings.LoginOnQn, AppConfig.AgentSettings.LoginOnAliwwBuyer, AppConfig.AgentSettings.AllowAutoKillQnTimeFrom, AppConfig.AgentSettings.AllowAutoKillQnTimeTo, AppConfig.AgentSettings.SelectWeekDays, AppConfig.AgentSettings.QnVersion, AppConfig.AgentSettings.AutoMinimizeTalkWindow, AppConfig.AgentSettings.SwitchNickAfterFiveMsg, AppConfig.AgentSettings.AutoKillAllAliWorkbenchAndAliApp, AppConfig.GetAlicdnLocalHostIp());
								ajaxResult.rows = e;
								response.WriteLine(ajaxResult.CreateSuccArro(""));
								continue;
							}
						}
						else if (num2 != 4060357925U)
						{
							if (num2 == 4271641068U)
							{
								if (text11 == "restart")
								{
									if (this.e != null)
									{
										this.e(null);
										response.WriteLine(ajaxResult.CreateSuccArro("重启成功"));
										continue;
									}
									continue;
								}
							}
						}
						else if (text11 == "getmessagecount")
						{
							ajaxResult.rows = AppConfig.GetMsgQueueCount();
							response.WriteLine(ajaxResult.CreateSuccArro(""));
							continue;
						}
						response.WriteLine(ajaxResult.CreateErrArro("Unkown Method"));
						IL_19C1:;
					}
				}
				catch (Exception ex)
				{
					try
					{
						LogWriter.WriteLog(ex.ToString(), 1);
						if (httpListenerContext != null)
						{
							httpListenerContext.Response.WriteLine(new AjaxResult().CreateErrArro("error"));
						}
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0003C8A8 File Offset: 0x0003AAA8
		private void a(object A_0)
		{
			AjaxResult ajaxResult = A_0 as AjaxResult;
			if (ajaxResult != null)
			{
				ajaxResult.rows = ClipboardProxy.GetText(10);
				this.d = true;
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000035C1 File Offset: 0x000017C1
		[CompilerGenerated]
		private bool a()
		{
			return this.d;
		}

		// Token: 0x0400034C RID: 844
		private HttpListener a;

		// Token: 0x0400034D RID: 845
		private Thread b;

		// Token: 0x0400034E RID: 846
		private bool d = false;

		// Token: 0x0400034F RID: 847
		[CompilerGenerated]
		private Action<object> e;

		// Token: 0x04000350 RID: 848
		[CompilerGenerated]
		private Action<bool> f;

		// Token: 0x04000351 RID: 849
		[CompilerGenerated]
		private Func<long, string, string, MsgSendSoftware, bool, string, ErrCodeInfo> g;

		// Token: 0x04000352 RID: 850
		[CompilerGenerated]
		private Func<string> h;

		// Token: 0x04000353 RID: 851
		[CompilerGenerated]
		private Action<long> i;

		// Token: 0x04000354 RID: 852
		[CompilerGenerated]
		private Func<int, string> j;

		// Token: 0x04000355 RID: 853
		private Dictionary<Guid, ErrCodeInfo> k = new Dictionary<Guid, ErrCodeInfo>();

		// Token: 0x02000090 RID: 144
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x04000358 RID: 856
			public long a;

			// Token: 0x04000359 RID: 857
			public AgentRemoteHttpListener b;
		}

		// Token: 0x02000091 RID: 145
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x060003F6 RID: 1014 RVA: 0x0003C980 File Offset: 0x0003AB80
			internal void f()
			{
				this.e.b.g(this.e.a, this.a.UserNick, this.b, this.c, true, this.d.ToString());
			}

			// Token: 0x0400035A RID: 858
			public AldsAccountInfo a;

			// Token: 0x0400035B RID: 859
			public string b;

			// Token: 0x0400035C RID: 860
			public MsgSendSoftware c;

			// Token: 0x0400035D RID: 861
			public Guid d;

			// Token: 0x0400035E RID: 862
			public AgentRemoteHttpListener.a e;
		}

		// Token: 0x02000092 RID: 146
		[CompilerGenerated]
		private sealed class c
		{
			// Token: 0x060003F8 RID: 1016 RVA: 0x0003C9D8 File Offset: 0x0003ABD8
			internal void c()
			{
				GetAgentAllUserResponse allUser = AppConfig.QnAgentServiceClient.GetAllUser();
				if (!allUser.IsError)
				{
					IEnumerable<QnAgentInfo> agentUser = allUser.AgentUser;
					Func<QnAgentInfo, bool> func;
					if ((func = this.b) == null)
					{
						func = (this.b = new Func<QnAgentInfo, bool>(this.c));
					}
					if (agentUser.FirstOrDefault(func) == null)
					{
						AppConfig.AgentUserDict.Remove(this.a);
					}
				}
			}

			// Token: 0x060003F9 RID: 1017 RVA: 0x000035D6 File Offset: 0x000017D6
			internal bool c(QnAgentInfo A_0)
			{
				return A_0.SellerNick == this.a;
			}

			// Token: 0x0400035F RID: 863
			public string a;

			// Token: 0x04000360 RID: 864
			public Func<QnAgentInfo, bool> b;
		}
	}
}
