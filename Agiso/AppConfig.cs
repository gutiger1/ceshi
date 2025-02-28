using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Agiso.DBAccess;
using Agiso.DbManager;
using Agiso.Handler;
using Agiso.MessageSender;
using Agiso.Object;
using Agiso.Utils;
using Agiso.Windows;
using Agiso.WwService.Sdk;
using Agiso.WwService.Sdk.Domain;
using Agiso.WwService.Sdk.Request;
using Agiso.WwService.Sdk.Response;
using Agiso.WwWebSocket.Model;
using AliwwClient.Cache;
using AliwwClient.Enums;
using AliwwClient.Object;
using AliwwClient.WebSocketServer;

namespace Agiso
{
	// Token: 0x020000C1 RID: 193
	public static class AppConfig
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00003F59 File Offset: 0x00002159
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x00003F60 File Offset: 0x00002160
		public static WebSocketServerIns AliwwWebScoketServer { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00042B38 File Offset: 0x00040D38
		public static string AliwwSocketSecret
		{
			get
			{
				if (AppConfig.k == null)
				{
					AppConfig.k = AppConfig.a("AliwwSocketSecret", "ABDFDLKJJHWEREWJ");
				}
				return AppConfig.k;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00003F68 File Offset: 0x00002168
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x00003F6F File Offset: 0x0000216F
		public static DateTime GetAliwwMsgStartTime { get; set; } = DateTime.Now.AddMinutes(-10.0);

		// Token: 0x0600059E RID: 1438 RVA: 0x00042B6C File Offset: 0x00040D6C
		public static string GetIniFullFileNameAliww()
		{
			return "C:\\Program Files (x86)\\AliWangWang\\Aliim.ini";
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00042B84 File Offset: 0x00040D84
		public static string GetExeFullFileNameAliww()
		{
			return "C:\\Program Files (x86)\\AliWangWang\\AliIM.exe";
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00042B9C File Offset: 0x00040D9C
		public static string GetExeFullFileNameAliwwNew()
		{
			return "C:\\Program Files (x86)\\AliWangWang\\new\\new_AliIM.exe";
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00042BB4 File Offset: 0x00040DB4
		public static string GetIniFullFileNameQn(QnVersionType ver)
		{
			string text;
			if (ver != 1)
			{
				if (ver != 2)
				{
					text = "";
				}
				else
				{
					text = "C:\\Program Files (x86)\\AliWorkbench2\\AliWorkbench.ini";
				}
			}
			else
			{
				text = "C:\\Program Files (x86)\\AliWorkbench\\AliWorkbench.ini";
			}
			return text;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00042BE8 File Offset: 0x00040DE8
		public static string GetExeFullFileNameQn(QnVersionType ver)
		{
			string text;
			if (ver != 1)
			{
				if (ver != 2)
				{
					text = "";
				}
				else
				{
					text = "C:\\Program Files (x86)\\AliWorkbench2\\AliWorkbench.exe";
				}
			}
			else
			{
				text = "C:\\Program Files (x86)\\AliWorkbench\\AliWorkbench.exe";
			}
			return text;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00042C1C File Offset: 0x00040E1C
		public static string GetExeFullFileNameQnNew(QnVersionType ver)
		{
			string text;
			if (ver != 1)
			{
				if (ver != 2)
				{
					text = "";
				}
				else
				{
					text = "C:\\Program Files (x86)\\AliWorkbench2\\new\\new_AliWorkbench.exe";
				}
			}
			else
			{
				text = "C:\\Program Files (x86)\\AliWorkbench\\new\\new_AliWorkbench.exe";
			}
			return text;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00042C50 File Offset: 0x00040E50
		public static string AgentQnSetupDir
		{
			get
			{
				QnVersionType qnVersion = AppConfig.AgentSettings.QnVersion;
				QnVersionType qnVersionType = qnVersion;
				if (qnVersionType != 1)
				{
					if (qnVersionType == 2)
					{
						return "C:\\Program Files (x86)\\AliWorkbench2\\";
					}
				}
				return "C:\\Program Files (x86)\\AliWorkbench\\";
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00042C88 File Offset: 0x00040E88
		public static string AgentQnSetupIniFileName
		{
			get
			{
				return AppConfig.GetIniFullFileNameQn(AppConfig.AgentSettings.QnVersion);
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00042CA8 File Offset: 0x00040EA8
		public static string GetHttpServerUrlPrefix(int port)
		{
			return string.Concat(new string[]
			{
				"https",
				"://",
				"localhost",
				":",
				port.ToString()
			});
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00003F77 File Offset: 0x00002177
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x00003F7E File Offset: 0x0000217E
		public static float DpiX { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00042CF0 File Offset: 0x00040EF0
		public static string RobotUserNick
		{
			get
			{
				if (string.IsNullOrEmpty(AppConfig.o))
				{
					try
					{
						TestSendNickResponse testSendNickResponse = AppConfig.WwServiceClient.TestSendNick();
						if (testSendNickResponse.IsError)
						{
							LogWriter.WriteLog("获取机器人昵称出错：" + testSendNickResponse.ErrMsg, 1);
						}
						else
						{
							AppConfig.o = testSendNickResponse.Nick;
						}
					}
					catch (Exception ex)
					{
						LogWriter.WriteLog("获取机器人昵称异常：" + ex.ToString(), 1);
					}
					if (string.IsNullOrEmpty(AppConfig.o))
					{
						AppConfig.o = "淘潮汇";
					}
				}
				return AppConfig.o;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00003F86 File Offset: 0x00002186
		public static Dictionary<string, string> RobotUserNickOpenUid { get; } = new Dictionary<string, string>
		{
			{ "淘潮汇", "AAGm_gqxAAShiml5geqTDtb2" },
			{ "agiso机器人", "AAEx_gqxAAShiml5gerEbdVy" }
		};

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00042D90 File Offset: 0x00040F90
		public static string RobotOpenUid
		{
			get
			{
				string text;
				if (AppConfig.p.ContainsKey(AppConfig.RobotUserNick))
				{
					text = AppConfig.p[AppConfig.RobotUserNick];
				}
				else
				{
					text = "";
				}
				return text;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00042DC8 File Offset: 0x00040FC8
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x00003F8D File Offset: 0x0000218D
		public static AgentHostSetting AgentSettings
		{
			get
			{
				return AppConfig.q;
			}
			set
			{
				AppConfig.q = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00003F95 File Offset: 0x00002195
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x00003F9C File Offset: 0x0000219C
		public static AgentProxyInfo AgentProxyInfo { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00003FA4 File Offset: 0x000021A4
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x00003FAB File Offset: 0x000021AB
		public static int AllowKeepAliveNum { get; set; } = 3;

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00003FB3 File Offset: 0x000021B3
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x00003FBA File Offset: 0x000021BA
		public static List<string> AgentPhones { get; set; } = new List<string> { "15260424202", "15659866121", "13394028085" };

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00003FC2 File Offset: 0x000021C2
		public static Dictionary<string, QnAgentInfo> AgentUserDict { get; } = new Dictionary<string, QnAgentInfo>();

		// Token: 0x060005B5 RID: 1461 RVA: 0x00042DDC File Offset: 0x00040FDC
		public static List<VerificationCodeSMS> GetSmsValidCode(bool addToHistory, string userNick, string receiverWithMosaic)
		{
			List<VerificationCodeSMS> list = new List<VerificationCodeSMS>();
			GetVerificationCodeSMSResponse verificationCodeSMS = AppConfig.QnAgentServiceClient.GetVerificationCodeSMS(userNick, receiverWithMosaic);
			if (!verificationCodeSMS.IsError && verificationCodeSMS.SMSes != null && verificationCodeSMS.SMSes.Count > 0)
			{
				foreach (VerificationCodeSMS verificationCodeSMS2 in verificationCodeSMS.SMSes)
				{
					string text = verificationCodeSMS2.ReceiveTime.ToString("yyyyMMddHHmmss") + verificationCodeSMS2.VerificationCode;
					if (!AppConfig.w.Contains(text))
					{
						if (addToHistory)
						{
							AppConfig.w.Add(text);
						}
						list.Add(verificationCodeSMS2);
					}
				}
			}
			return list;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00042EB4 File Offset: 0x000410B4
		public static void InitAutoReplyList()
		{
			AppConfig.u = AutoReplyManager.Select(null, null);
			if (AppConfig.u != null)
			{
				AppConfig.a();
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00042EDC File Offset: 0x000410DC
		public static List<AutoReplyInfo> AutoReplyList
		{
			get
			{
				if (AppConfig.u == null)
				{
					AppConfig.InitAutoReplyList();
				}
				return AppConfig.u;
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00042F00 File Offset: 0x00041100
		private static void a()
		{
			foreach (AutoReplyInfo autoReplyInfo in AppConfig.u)
			{
				if (!autoReplyInfo.Valid)
				{
					autoReplyInfo.Enabled = false;
				}
				else
				{
					string sellerNick = autoReplyInfo.SellerNick;
					if (sellerNick.Equals("【适用所有已添加的旺旺】"))
					{
						autoReplyInfo.Enabled = true;
					}
					else
					{
						AldsAccountInfo aldsAccountInfo = null;
						if (sellerNick.IndexOf(":") > 0 || !AppConfig.CurrentSystemSettingInfo.AutoReplyBySellerNick)
						{
							if (AppConfig.UserDict.ContainsKey(sellerNick))
							{
								aldsAccountInfo = AppConfig.UserDict[sellerNick];
							}
						}
						else
						{
							string text = sellerNick;
							aldsAccountInfo = AppConfig.GetAccountInfo(text);
						}
						if (aldsAccountInfo == null)
						{
							autoReplyInfo.Enabled = false;
						}
						else if (!aldsAccountInfo.EnableAutoReply)
						{
							autoReplyInfo.Enabled = false;
						}
						else if (aldsAccountInfo.VersionNo < 3)
						{
							autoReplyInfo.Enabled = false;
						}
						else
						{
							autoReplyInfo.Enabled = true;
						}
					}
				}
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00043014 File Offset: 0x00041214
		public static SystemSettingsInfo CurrentSystemSettingInfo
		{
			get
			{
				if (AppConfig.x == null)
				{
					AppConfig.x = SystemSettingsManager.SelectOne();
				}
				return AppConfig.x;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00003FC9 File Offset: 0x000021C9
		public static string DefaultTestSendMsgBody
		{
			get
			{
				return "1、该内容仅为测试旺旺版本是否支持，别无他用。\r\n\r\n2、实际发送内容，请在系统后台网页设置。\r\n\r\n3、要设置自动发货，需要到帐号管理添加旺旺帐号。添加的帐号必须是购买“agiso自动发货”的旺旺号（可以是该旺旺号的主号或子号）。\r\n\r\n4、发送结果可看发送记录。";
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00003FD1 File Offset: 0x000021D1
		public static ConcurrentDictionary<string, SellerCache> DictSellerExecuteCache { get; } = new ConcurrentDictionary<string, SellerCache>();

		// Token: 0x060005BC RID: 1468 RVA: 0x0004303C File Offset: 0x0004123C
		public static SellerCache GetSellerExecuteCache(string sellerNick)
		{
			string text = Util.StrConvSimple(sellerNick);
			return AppConfig.y.GetOrAdd(text, new Func<string, SellerCache>(AppConfig.<>c.<>9.b));
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0004307C File Offset: 0x0004127C
		public static int GetMsgQueueAllSellerTotalCount(out List<string> sellerNicksNotInclude, out int hasMsgSellerCount)
		{
			int num = 0;
			hasMsgSellerCount = 0;
			sellerNicksNotInclude = new List<string>();
			if (AppConfig.y != null)
			{
				foreach (SellerCache sellerCache in AppConfig.y.Values)
				{
					if (sellerCache != null && sellerCache.AliwwMsgQueue != null)
					{
						num += sellerCache.AliwwMsgQueue.Count;
						if (sellerCache.AliwwMsgQueue.Count >= 10)
						{
							sellerNicksNotInclude.Add(sellerCache.SellerNick);
						}
						if (sellerCache.AliwwMsgQueue.Count > 0)
						{
							hasMsgSellerCount++;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0004313C File Offset: 0x0004133C
		public static List<KeyValuePair<string, int>> GetMsgQueueCount()
		{
			List<KeyValuePair<string, int>> list2;
			if (AppConfig.y != null && AppConfig.y.Count > 0)
			{
				List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
				foreach (KeyValuePair<string, SellerCache> keyValuePair in AppConfig.y)
				{
					int count = keyValuePair.Value.AliwwMsgQueue.Count;
					if (count != 0)
					{
						list.Add(new KeyValuePair<string, int>(keyValuePair.Key, count));
					}
				}
				list2 = list;
			}
			else
			{
				list2 = null;
			}
			return list2;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00003FD8 File Offset: 0x000021D8
		public static ConcurrentDictionary<string, UserCache> DictUserExecuteCache { get; } = new ConcurrentDictionary<string, UserCache>();

		// Token: 0x060005C0 RID: 1472 RVA: 0x000431DC File Offset: 0x000413DC
		public static UserCache GetUserCacheOrCreate(string userNick)
		{
			string text = Util.StrConvSimple(userNick).ToLower();
			return AppConfig.z.GetOrAdd(text, new Func<string, UserCache>(AppConfig.<>c.<>9.a));
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00043224 File Offset: 0x00041424
		public static UserCache AddOrUpdateUserCache(string userNick, UserCache userCache)
		{
			AppConfig.h h = new AppConfig.h();
			h.a = userCache;
			string text = Util.StrConvSimple(userNick).ToLower();
			return AppConfig.z.AddOrUpdate(text, h.a, new Func<string, UserCache, UserCache>(h.b));
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0004326C File Offset: 0x0004146C
		public static bool UserListExitUserNickIgnoreTraditional(string userNick)
		{
			AppConfig.a a = new AppConfig.a();
			a.a = userNick;
			bool flag;
			if (!(flag = AppConfig.UserList.Any(new Func<AldsAccountInfo, bool>(a.b))))
			{
				AppConfig.b b = new AppConfig.b();
				b.a = Util.StrConvSimple(a.a);
				flag = AppConfig.UserList.Any(new Func<AldsAccountInfo, bool>(b.b));
			}
			return flag;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x000432D4 File Offset: 0x000414D4
		public static BindingList<AldsAccountInfo> UserList
		{
			get
			{
				if (AppConfig.ab == null)
				{
					object obj = AppConfig.aa;
					lock (obj)
					{
						if (AppConfig.ab == null)
						{
							AppConfig.ab = new BindingList<AldsAccountInfo>();
							if (!Util.IsEmptyList<KeyValuePair<string, AldsAccountInfo>>(AppConfig.UserDict))
							{
								foreach (AldsAccountInfo aldsAccountInfo in AppConfig.UserDict.Values)
								{
									AppConfig.ab.Add(aldsAccountInfo);
								}
							}
						}
					}
				}
				return AppConfig.ab;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00043390 File Offset: 0x00041590
		public static ConcurrentDictionary<string, AldsAccountInfo> UserDict
		{
			get
			{
				if (AppConfig.ac == null)
				{
					AppConfig.ac = AldsAccountManager.SelectDict();
				}
				return AppConfig.ac;
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000433B8 File Offset: 0x000415B8
		public static List<string> GetValidAndSwitchOnSellerNicks()
		{
			List<string> list = new List<string>();
			try
			{
				if (Util.IsEmptyList<AldsAccountInfo>(AppConfig.UserList))
				{
					return list;
				}
				foreach (AldsAccountInfo aldsAccountInfo in AppConfig.UserList)
				{
					if (aldsAccountInfo.IsValid && aldsAccountInfo.AutoSendOnOff)
					{
						string masterNick = Util.GetMasterNick(aldsAccountInfo.UserNick);
						if (!list.Contains(masterNick))
						{
							list.Add(masterNick);
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
			}
			return list;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00043474 File Offset: 0x00041674
		public static AldsAccountInfo GetAccountInfo(string sellerNick)
		{
			AldsAccountInfo aldsAccountInfo;
			if (string.IsNullOrEmpty(sellerNick))
			{
				aldsAccountInfo = null;
			}
			else
			{
				foreach (AldsAccountInfo aldsAccountInfo2 in AppConfig.UserList)
				{
					string masterNick = Util.GetMasterNick(aldsAccountInfo2.UserNick);
					if (masterNick.Equals(sellerNick, StringComparison.OrdinalIgnoreCase))
					{
						return aldsAccountInfo2;
					}
				}
				aldsAccountInfo = null;
			}
			return aldsAccountInfo;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000434E8 File Offset: 0x000416E8
		public static AldsAccountInfo GetAgentAccountInfo(string sellerNick)
		{
			AldsAccountInfo aldsAccountInfo;
			if (string.IsNullOrEmpty(sellerNick))
			{
				aldsAccountInfo = null;
			}
			else
			{
				QnAgentInfo qnAgentInfo = AppConfig.GetAgentInfo(sellerNick);
				if (qnAgentInfo != null)
				{
					AldsAccountInfo aldsAccountInfo2 = new AldsAccountInfo
					{
						UserNick = qnAgentInfo.QnNick,
						DisplayUserNick = ((string.IsNullOrEmpty(qnAgentInfo.DisplayNick) || qnAgentInfo.SellerNick == qnAgentInfo.DisplayNick) ? qnAgentInfo.QnNick : (qnAgentInfo.DisplayNick + qnAgentInfo.QnNick.Substring(qnAgentInfo.QnNick.IndexOf(":")))),
						QnAccountPwd = qnAgentInfo.QnPassword,
						LongOpen = qnAgentInfo.LongOpen,
						ManualNick = qnAgentInfo.TransferNick,
						IsCustomerServiceNewVersion = qnAgentInfo.CustomerServiceNewVersion,
						TransferMessage = qnAgentInfo.TransferMessage,
						DisableTransfer = qnAgentInfo.DisableTransfer,
						SendIM = qnAgentInfo.SendIM
					};
					aldsAccountInfo = aldsAccountInfo2;
				}
				else
				{
					GetAgentAllUserResponse allUser = AppConfig.QnAgentServiceClient.GetAllUser();
					if (allUser.AgentUser != null)
					{
						foreach (QnAgentInfo qnAgentInfo2 in allUser.AgentUser)
						{
							AppConfig.v[qnAgentInfo2.SellerNick] = qnAgentInfo2;
							AppConfig.GetUserCacheOrCreate(qnAgentInfo2.QnNick).CurrentWorksheet = (qnAgentInfo2.CustomerServiceNewVersion ? qnAgentInfo2.CustomerServiceWorksheetInfo : new List<CustomerServiceWorksheet>());
						}
					}
					qnAgentInfo = AppConfig.GetAgentInfo(sellerNick);
					if (qnAgentInfo != null)
					{
						AldsAccountInfo aldsAccountInfo3 = new AldsAccountInfo
						{
							UserNick = qnAgentInfo.QnNick,
							QnAccountPwd = qnAgentInfo.QnPassword,
							LongOpen = qnAgentInfo.LongOpen,
							ManualNick = qnAgentInfo.TransferNick,
							IsCustomerServiceNewVersion = qnAgentInfo.CustomerServiceNewVersion,
							DisableTransfer = qnAgentInfo.DisableTransfer,
							SendIM = qnAgentInfo.SendIM
						};
						aldsAccountInfo = aldsAccountInfo3;
					}
					else
					{
						aldsAccountInfo = null;
					}
				}
			}
			return aldsAccountInfo;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000436E0 File Offset: 0x000418E0
		public static QnAgentInfo GetAgentInfo(string sellerNick)
		{
			AppConfig.c c = new AppConfig.c();
			c.a = sellerNick;
			QnAgentInfo qnAgentInfo;
			if (AppConfig.v.ContainsKey(c.a))
			{
				qnAgentInfo = AppConfig.v[c.a];
			}
			else
			{
				qnAgentInfo = AppConfig.v.FirstOrDefault(new Func<KeyValuePair<string, QnAgentInfo>, bool>(c.b)).Value;
			}
			return qnAgentInfo;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00043740 File Offset: 0x00041940
		public static List<AldsAccountInfo> GetUserList(string sellerNick)
		{
			List<AldsAccountInfo> list;
			if (string.IsNullOrEmpty(sellerNick))
			{
				list = null;
			}
			else
			{
				List<AldsAccountInfo> list2 = new List<AldsAccountInfo>();
				foreach (AldsAccountInfo aldsAccountInfo in AppConfig.UserList)
				{
					string masterNick = Util.GetMasterNick(aldsAccountInfo.UserNick);
					if (masterNick.Equals(sellerNick, StringComparison.OrdinalIgnoreCase))
					{
						list2.Add(aldsAccountInfo);
					}
				}
				list = list2;
			}
			return list;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000437C0 File Offset: 0x000419C0
		public static bool SaveConfig()
		{
			bool flag;
			if (AppConfig.CurrentSystemSettingInfo == null)
			{
				flag = false;
			}
			else
			{
				int num = SystemSettingsManager.Update(AppConfig.CurrentSystemSettingInfo);
				if (num > 0)
				{
					flag = true;
				}
				else
				{
					AppConfig.x = null;
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000437FC File Offset: 0x000419FC
		public static string GetCurrentApplicationVersion()
		{
			if (AppConfig.ad == null)
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
				AppConfig.ad = string.Format("{0}.{1}.{2}", versionInfo.ProductMajorPart, versionInfo.ProductMinorPart, versionInfo.ProductBuildPart);
			}
			return AppConfig.ad;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00003FDF File Offset: 0x000021DF
		public static string ApplicationUuid
		{
			get
			{
				return AppConfig.GetCurrentApplicationVersion() + "," + HardwareInfo.Uuid.Substring(16);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00043860 File Offset: 0x00041A60
		public static int LastRobotReplyMinutes
		{
			get
			{
				if (AppConfig.ae == -1)
				{
					AppConfig.ae = Util.ToInt(AppConfig.a("LastRobotReplyMinutes", "10"));
					if (AppConfig.ae < 10)
					{
						AppConfig.ae = 20;
					}
					if (AppConfig.ae > 60)
					{
						AppConfig.ae = 60;
					}
				}
				return AppConfig.ae;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x000438C0 File Offset: 0x00041AC0
		public static int AliwwMessageLengthMax
		{
			get
			{
				if (AppConfig.af == -1)
				{
					AppConfig.af = Util.ToInt(AppConfig.a("AliwwMessageLengthMax", "600"));
					if (AppConfig.af < 400)
					{
						AppConfig.af = 400;
					}
					else if (AppConfig.af > 2000)
					{
						AppConfig.af = 2000;
					}
				}
				return AppConfig.af;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0004392C File Offset: 0x00041B2C
		public static string RecoveryCustomerPath
		{
			get
			{
				if (AppConfig.ag == null)
				{
					AppConfig.ag = AppConfig.a("RecoveryCustomerPath", "");
				}
				return AppConfig.ag;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00043960 File Offset: 0x00041B60
		public static bool AllowGetMsgByWebSocket
		{
			get
			{
				if (AppConfig.ah == null)
				{
					try
					{
						AppConfig.ah = new bool?(bool.Parse(AppConfig.a("AllowGetMsgByWebSocket", "true")));
					}
					catch
					{
						AppConfig.ah = new bool?(true);
					}
				}
				return AppConfig.ah.Value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x000439C8 File Offset: 0x00041BC8
		public static bool IsAutoReplyBySellerNick
		{
			get
			{
				if (AppConfig.ai == null)
				{
					try
					{
						AppConfig.ai = new bool?(bool.Parse(AppConfig.a("IsAutoReplyBySellerNick", "true")));
					}
					catch
					{
						AppConfig.ai = new bool?(true);
					}
				}
				return AppConfig.ai.Value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00003FFD File Offset: 0x000021FD
		public static bool AllowAutoLogin
		{
			get
			{
				return AppConfig.AliwwClientMode == AliwwClientMode.代挂模式;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00004008 File Offset: 0x00002208
		public static bool IsSellerLoginOnOwnComputer
		{
			get
			{
				return AppConfig.AliwwClientMode == AliwwClientMode.自挂模式;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00043A30 File Offset: 0x00041C30
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x00004013 File Offset: 0x00002213
		public static AliwwClientMode AliwwClientMode
		{
			get
			{
				if (AppConfig.aj == (AliwwClientMode)0)
				{
					AppConfig.aj = (bool.Parse(AppConfig.a("AllowAutoLoginQn", "false")) ? AliwwClientMode.代挂模式 : AliwwClientMode.自挂模式);
				}
				return AppConfig.aj;
			}
			set
			{
				AppConfig.aj = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00043A74 File Offset: 0x00041C74
		public static bool AllowAutoKillQnForFreeMemory
		{
			get
			{
				if (AppConfig.ak == null)
				{
					try
					{
						AppConfig.ak = new bool?(bool.Parse(AppConfig.a("AllowAutoKillQnForFreeMemory", "true")));
					}
					catch
					{
						AppConfig.ak = new bool?(true);
					}
				}
				return AppConfig.ak.Value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00043ADC File Offset: 0x00041CDC
		public static bool IsAllowSendTickByMultiThread
		{
			get
			{
				if (AppConfig.al == null)
				{
					try
					{
						AppConfig.al = new bool?(bool.Parse(AppConfig.a("IsAllowSendTickByMultiThread", "false")));
					}
					catch
					{
						AppConfig.al = new bool?(false);
					}
				}
				return AppConfig.al.Value;
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00043B44 File Offset: 0x00041D44
		public static bool InitAutoReplyData()
		{
			return AutoReplyManager.HasData() || DbAccessDAL.InitDb(DbHelper.GetInitAutoReplyData());
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00043B68 File Offset: 0x00041D68
		public static void SetAllTextBoxSelectAllSupport(Control control)
		{
			Type type = new TextBox().GetType();
			foreach (object obj in control.Controls)
			{
				Control control2 = (Control)obj;
				if (control2.GetType() != type)
				{
					AppConfig.SetAllTextBoxSelectAllSupport(control2);
				}
				else
				{
					control2.KeyDown += AppConfig.<>c.<>9.a;
				}
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00043C04 File Offset: 0x00041E04
		public static MailSenderAccount DefaultMailSender
		{
			get
			{
				if (AppConfig.am == null)
				{
					AppConfig.am = new MailSenderAccount();
					AppConfig.am.Account = ConfigurationManager.AppSettings["DefaultMailSender-Account"];
					AppConfig.am.MailFrom = ConfigurationManager.AppSettings["DefaultMailSender-MailFrom"];
					AppConfig.am.Password = ConfigurationManager.AppSettings["DefaultMailSender-Password"];
					AppConfig.am.SmtpPort = int.Parse(ConfigurationManager.AppSettings["DefaultMailSender-Port"]);
					AppConfig.am.SmtpHost = ConfigurationManager.AppSettings["DefaultMailSender-SmtpHost"];
					AppConfig.am.Nick = ConfigurationManager.AppSettings["DefaultMailSender-DisplayName"];
					AppConfig.am.EnableSsl = false;
				}
				return AppConfig.am;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00043CDC File Offset: 0x00041EDC
		public static MailSenderAccount DefaultMailSender2
		{
			get
			{
				if (AppConfig.an == null)
				{
					AppConfig.an = new MailSenderAccount();
					AppConfig.an.Account = ConfigurationManager.AppSettings["DefaultMailSender-Account2"];
					AppConfig.an.MailFrom = ConfigurationManager.AppSettings["DefaultMailSender-MailFrom2"];
					AppConfig.an.Password = ConfigurationManager.AppSettings["DefaultMailSender-Password2"];
					AppConfig.an.SmtpPort = int.Parse(ConfigurationManager.AppSettings["DefaultMailSender-Port2"]);
					AppConfig.an.SmtpHost = ConfigurationManager.AppSettings["DefaultMailSender-SmtpHost2"];
					AppConfig.an.Nick = ConfigurationManager.AppSettings["DefaultMailSender-DisplayName2"];
					AppConfig.an.EnableSsl = false;
				}
				return AppConfig.an;
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00043DB4 File Offset: 0x00041FB4
		public static void SendMail(string mailTo, string subject, string body)
		{
			try
			{
				MailSender.SendMail(AppConfig.DefaultMailSender, mailTo, subject, body);
			}
			catch
			{
				try
				{
					MailSender.SendMail(AppConfig.DefaultMailSender2, mailTo, subject, body);
				}
				catch
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine("========================================");
					stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
					stringBuilder.AppendLine("管理邮件发送失败 - " + subject);
					stringBuilder.AppendLine("----------------------------------------");
					stringBuilder.AppendLine(body);
					stringBuilder.AppendLine("========================================");
					stringBuilder.AppendLine("");
					LogWriter.WriteLog(stringBuilder.ToString(), 2);
				}
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00043E84 File Offset: 0x00042084
		public static void NoticeAdministrator(NoticeAdminType type, string subject, string body)
		{
			if (!AppConfig.ap.ContainsKey(type) || !(AppConfig.ap[type].AddMinutes(10.0) > DateTime.Now))
			{
				AppConfig.ap[type] = DateTime.Now;
				if (AppConfig.ao == null)
				{
					try
					{
						AppConfig.ao = ConfigurationManager.AppSettings["AdminMail"];
					}
					catch
					{
					}
					if (AppConfig.ao == null)
					{
						AppConfig.ao = "";
					}
				}
				if (!string.IsNullOrEmpty(AppConfig.ao))
				{
					AppConfig.SendMail(AppConfig.ao, subject, body);
				}
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00043F38 File Offset: 0x00042138
		public static string PathPrefix
		{
			get
			{
				if (string.IsNullOrEmpty(AppConfig.aq))
				{
					AppConfig.aq = ConfigurationManager.AppSettings["PathPrefix"];
				}
				return AppConfig.aq;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00043F70 File Offset: 0x00042170
		public static string AgentPathPrefix
		{
			get
			{
				if (string.IsNullOrEmpty(AppConfig.ar))
				{
					AppConfig.ar = ConfigurationManager.AppSettings["AgentPathPrefix"];
				}
				return AppConfig.ar;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00043FA8 File Offset: 0x000421A8
		public static string AgentSecret
		{
			get
			{
				if (string.IsNullOrEmpty(AppConfig.@as))
				{
					AppConfig.@as = ConfigurationManager.AppSettings["AgentSecret"];
				}
				return AppConfig.@as;
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00043FE0 File Offset: 0x000421E0
		private static string a(string A_0, string A_1 = null)
		{
			string text;
			if (string.IsNullOrEmpty(A_0))
			{
				text = "";
			}
			else
			{
				try
				{
					if (!AppConfig.n.ContainsKey(A_0))
					{
						AppConfig.n[A_0] = ConfigurationManager.AppSettings[A_0] ?? A_1;
					}
				}
				catch
				{
					AppConfig.n[A_0] = A_1;
				}
				text = AppConfig.n[A_0];
			}
			return text;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00044058 File Offset: 0x00042258
		public static string GetPicValidCode(string picPath)
		{
			string text2;
			try
			{
				string text = Util.RequestHtmlByUrl("http://localhost:30001/?method=query&url=" + Util.UrlEncode(picPath, null));
				if (string.IsNullOrEmpty(text))
				{
					text2 = null;
				}
				else
				{
					PicCodeInfo picCodeInfo = JSON.Decode<PicCodeInfo>(text);
					if (picCodeInfo == null)
					{
						text2 = null;
					}
					else
					{
						text2 = picCodeInfo.Code;
					}
				}
			}
			catch (WebException ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
				WindowInfo windowInfo = Win32Extend.FindWindowByClassAndName(null, "图形验证码识别器");
				if (windowInfo == null || windowInfo.HWnd == IntPtr.Zero)
				{
					Process.Start("C:\\Program Files (x86)\\Agiso\\QnVerificationCodeService\\QnVerificationCodeService.exe");
				}
				text2 = "";
			}
			catch (Exception ex2)
			{
				LogWriter.WriteLog(ex2.ToString(), 1);
				text2 = "";
			}
			return text2;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00044128 File Offset: 0x00042328
		public static string GetPicValidCode(byte[] data, string type = "B942")
		{
			string text = Convert.ToBase64String(data);
			return AppConfig.GetPicValidCodeByBase64(text, type);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00044148 File Offset: 0x00042348
		public static string GetPicValidCodeByBase64(string string_0, string type = "B942")
		{
			string text2;
			try
			{
				string text = Util.RequestHtmlByUrl("http://localhost:30001/?method=query&type=" + type + "&data=" + Util.UrlEncode(string_0, null));
				if (string.IsNullOrEmpty(text))
				{
					text2 = null;
				}
				else
				{
					PicCodeInfo picCodeInfo = JSON.Decode<PicCodeInfo>(text);
					if (picCodeInfo == null)
					{
						text2 = null;
					}
					else
					{
						text2 = picCodeInfo.Code;
					}
				}
			}
			catch (WebException ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
				WindowInfo windowInfo = Win32Extend.FindWindowByClassAndName(null, "图形验证码识别器");
				if (windowInfo == null || windowInfo.HWnd == IntPtr.Zero)
				{
					Process.Start("C:\\Program Files (x86)\\Agiso\\QnVerificationCodeService\\QnVerificationCodeService.exe");
				}
				text2 = "";
			}
			catch (Exception ex2)
			{
				LogWriter.WriteLog(ex2.ToString(), 1);
				text2 = "";
			}
			return text2;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00044220 File Offset: 0x00042420
		public static void WriteLog(string logTxt, LogType type, int level = 1)
		{
			if ((AppConfig.EnabledLogOption & (long)type) > 0L)
			{
				LogWriter.WriteLog(logTxt, level);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0000401B File Offset: 0x0000221B
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x00004022 File Offset: 0x00002222
		public static long EnabledLogOption { get; set; }

		// Token: 0x060005E8 RID: 1512 RVA: 0x0004424C File Offset: 0x0004244C
		public static bool IsEnableLog(LogType type)
		{
			return (AppConfig.at & (long)type) > 0L;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0004426C File Offset: 0x0004246C
		public static List<QnAgentInfo> GetLongOpenUsers()
		{
			List<QnAgentInfo> list;
			if (AppConfig.AgentSettings.AllowAutoExitQn)
			{
				list = AppConfig.v.Values.Where(new Func<QnAgentInfo, bool>(AppConfig.<>c.<>9.b)).ToList<QnAgentInfo>();
			}
			else
			{
				list = AppConfig.v.Values.ToList<QnAgentInfo>();
			}
			return list;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000442CC File Offset: 0x000424CC
		public static List<QnAgentInfo> GetNotLognOpenUsers()
		{
			List<QnAgentInfo> list;
			if (AppConfig.AgentSettings.AllowAutoExitQn)
			{
				list = AppConfig.v.Values.Where(new Func<QnAgentInfo, bool>(AppConfig.<>c.<>9.a)).ToList<QnAgentInfo>();
			}
			else
			{
				list = new List<QnAgentInfo>();
			}
			return list;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00044324 File Offset: 0x00042524
		public static string GetNextDutyManualNick(AldsAccountInfo account)
		{
			UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(account.UserNick);
			if (!account.IsCustomerServiceNewVersion)
			{
				if (string.IsNullOrEmpty(account.ManualNick))
				{
					return "";
				}
				string[] array = account.ManualNick.Trim().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length == 0)
				{
					userCacheOrCreate.DutyManualNick = "";
					return "";
				}
				UserCache userCache = userCacheOrCreate;
				int num = userCache.ManualNickIdx;
				userCache.ManualNickIdx = num + 1;
				int num2 = num % array.Length;
				userCacheOrCreate.DutyManualNick = array[num2].Trim();
			}
			else
			{
				AppConfig.d d = new AppConfig.d();
				d.a = DateTime.Now;
				List<CustomerServiceWorksheet> list = (AppConfig.GetUserCacheOrCreate(account.UserNick).CurrentWorksheet ?? new List<CustomerServiceWorksheet>()).Where(new Func<CustomerServiceWorksheet, bool>(d.c)).ToList<CustomerServiceWorksheet>();
				if (list != null && list.Count > 0)
				{
					UserCache userCache2 = userCacheOrCreate;
					int num = userCache2.ManualNickIdx;
					userCache2.ManualNickIdx = num + 1;
					int num3 = num % list.Count;
					userCacheOrCreate.DutyManualNick = list[num3].UserNick;
				}
				else
				{
					userCacheOrCreate.DutyManualNick = null;
				}
			}
			return userCacheOrCreate.DutyManualNick;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00044468 File Offset: 0x00042668
		public static string GetDutyManualNick(AldsAccountInfo account)
		{
			UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(account.UserNick);
			if (string.IsNullOrEmpty(userCacheOrCreate.DutyManualNick))
			{
				userCacheOrCreate.ManualNickIdx = 0;
				if (!account.IsCustomerServiceNewVersion)
				{
					if (!string.IsNullOrEmpty(account.ManualNick))
					{
						string[] array = account.ManualNick.Trim().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
						if (array.Length != 0)
						{
							userCacheOrCreate.DutyManualNick = array[0].Trim();
						}
					}
				}
				else if (account.DefaultMouldId != null && account.DefaultMouldId.Value > 0L)
				{
					AppConfig.e e = new AppConfig.e();
					e.a = DateTime.Now;
					List<CustomerServiceWorksheet> list = (AppConfig.GetUserCacheOrCreate(account.UserNick).CurrentWorksheet ?? new List<CustomerServiceWorksheet>()).Where(new Func<CustomerServiceWorksheet, bool>(e.b)).ToList<CustomerServiceWorksheet>();
					if (list != null && list.Count > 0)
					{
						userCacheOrCreate.DutyManualNick = list[0].UserNick;
					}
				}
			}
			return userCacheOrCreate.DutyManualNick;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00044598 File Offset: 0x00042798
		public static string GetTransferCallDutyManualNick(AldsAccountInfo account, string buyerNick, string buyerOpenUid)
		{
			AppConfig.f f = new AppConfig.f();
			string masterNick = Util.GetMasterNick(account.UserNick);
			string text;
			if (!account.IsCustomerServiceNewVersion && string.IsNullOrEmpty(account.ManualNick) && account.UserNick.Contains(":"))
			{
				text = "";
			}
			else
			{
				Dictionary<string, string> dictBuyerTransferCallToManualNickCache = AppConfig.GetSellerExecuteCache(masterNick).DictBuyerTransferCallToManualNickCache;
				f.a = "";
				string text2 = ((string.IsNullOrEmpty(buyerNick) || buyerNick.Contains("**")) ? buyerOpenUid : buyerNick);
				if (!string.IsNullOrEmpty(text2) && dictBuyerTransferCallToManualNickCache.ContainsKey(text2))
				{
					f.a = dictBuyerTransferCallToManualNickCache[text2];
					if (!account.IsCustomerServiceNewVersion)
					{
						string manualNick = account.ManualNick;
						string[] array = ((manualNick != null) ? manualNick.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries) : null);
						if (Util.IsEmptyList<string>(array) || !array.Contains(f.a, StringComparer.OrdinalIgnoreCase))
						{
							f.a = "";
						}
					}
					else
					{
						AppConfig.g g = new AppConfig.g();
						g.b = f;
						g.a = DateTime.Now;
						CustomerServiceWorksheet customerServiceWorksheet = (AppConfig.GetUserCacheOrCreate(account.UserNick).CurrentWorksheet ?? new List<CustomerServiceWorksheet>()).FirstOrDefault(new Func<CustomerServiceWorksheet, bool>(g.d));
						if (customerServiceWorksheet == null)
						{
							g.b.a = "";
						}
					}
				}
				if (string.IsNullOrEmpty(f.a))
				{
					f.a = AppConfig.GetNextDutyManualNick(account);
					if (!string.IsNullOrEmpty(f.a) && !string.IsNullOrEmpty(text2))
					{
						dictBuyerTransferCallToManualNickCache[text2] = f.a;
					}
				}
				if (string.IsNullOrEmpty(f.a))
				{
					if (AppConfig.AllowAutoLogin)
					{
						f.a = masterNick;
					}
					else if (account.IsCustomerServiceNewVersion)
					{
						f.a = account.TransferNickIfNotDuty;
					}
				}
				text = f.a;
			}
			return text;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00044788 File Offset: 0x00042988
		public static int CurrentAliwwClientVersion
		{
			get
			{
				if (AppConfig.au == null)
				{
					string[] array = AppConfig.GetCurrentApplicationVersion().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
					AppConfig.au = new int?(Util.ToInt(array[0] + Util.ToInt(array[1]).ToString("D2") + Util.ToInt(array[2]).ToString("D2")));
				}
				return AppConfig.au.Value;
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00044810 File Offset: 0x00042A10
		public static void ProcessStartQn(string userNick, string buyerNick, string buyerOpenUid, string siteId = "cntaobao")
		{
			try
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				string text = ((!userNick.StartsWith(siteId)) ? (siteId + userNick) : userNick);
				string text2 = ((!buyerNick.StartsWith(siteId)) ? (siteId + buyerNick) : buyerNick);
				string text3 = "";
				string text4 = "";
				if (!string.IsNullOrEmpty(buyerOpenUid))
				{
					if (Util.IsNum(buyerOpenUid))
					{
						text3 = buyerOpenUid;
					}
					else
					{
						text4 = "12292026";
					}
				}
				Process.Start(string.Concat(new string[]
				{
					"aliim:sendmsg?uid=", text, "&touid=", text2, "&siteid=", siteId, "&status=2&portalId=&gid=&itemsId=&scene=&toRole=&source=light&client=false&encryptUID=", text3, "&bizDomain=taobao&openAppkey=", text4,
					"&openuid=", buyerOpenUid, "&sceneParam={\"toRole\":\"buyer\"，\"bizRef\": \"\"}"
				}));
				stopwatch.Stop();
				if (stopwatch.ElapsedMilliseconds >= 4000L)
				{
					LogWriter.WriteLog("ProcessStartQn is too long", 1);
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog("ProcessStartQn：" + ex.ToString(), 1);
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00044944 File Offset: 0x00042B44
		public static string GetAlicdnLocalHostIp()
		{
			string text = "C:\\Windows\\System32\\drivers\\etc\\hosts";
			string text4;
			try
			{
				using (FileStream fileStream = new FileStream(text, FileMode.OpenOrCreate, FileAccess.ReadWrite))
				{
					using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
					{
						string text2 = streamReader.ReadToEnd();
						Regex regex = new Regex("(\\r\\n)?(\\s*\\#?[\\d\\.]+?)\\s+?g\\.alicdn\\.com\\s*?(\\r\\n)?");
						MatchCollection matchCollection = regex.Matches(text2);
						if (matchCollection.Count > 0)
						{
							Match match = matchCollection[matchCollection.Count - 1];
							if (match.Success)
							{
								string text3 = match.Groups[2].Value;
								if (text3.StartsWith("#"))
								{
									text3 = "";
								}
								return text3;
							}
						}
						text4 = "";
					}
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
				text4 = "";
			}
			return text4;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00044A44 File Offset: 0x00042C44
		public static string[] AgentControlServerIPs
		{
			get
			{
				if (AppConfig.av == null)
				{
					string text = AppConfig.a("AgentControlServerIPs", "");
					AppConfig.av = text.Split(new char[] { ',' });
				}
				return AppConfig.av;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00044A8C File Offset: 0x00042C8C
		public static string AgentControlSecret
		{
			get
			{
				if (AppConfig.aw == null)
				{
					AppConfig.aw = AppConfig.a("AgentControlSecret", "");
				}
				return AppConfig.aw;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00044AC0 File Offset: 0x00042CC0
		public static string AgentUpdateJsonUrl
		{
			get
			{
				if (AppConfig.ax == null)
				{
					AppConfig.ax = AppConfig.a("AgentUpdateJsonUrl", "");
				}
				return AppConfig.ax;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00044AF4 File Offset: 0x00042CF4
		public static MsgSendSoftware MsgSendSoftware
		{
			get
			{
				if (AppConfig.ay == null)
				{
					string text = AppConfig.a("MsgSendSoftware", "1");
					int num = Util.ToInt(text);
					AppConfig.ay = new MsgSendSoftware?((MsgSendSoftware)num);
				}
				return AppConfig.ay.Value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00044B40 File Offset: 0x00042D40
		public static int AgentAllowAutoKillProcessEveryTwoHoursBetweenHourFrom
		{
			get
			{
				if (AppConfig.az == null)
				{
					string text = AppConfig.a("AgentAllowAutoKillProcessEveryTwoHoursBetweenHourFrom", "0");
					AppConfig.az = new int?(Util.ToInt(text));
				}
				return AppConfig.az.Value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00044B8C File Offset: 0x00042D8C
		public static int AgentAllowAutoKillProcessEveryTwoHoursBetweenHourTo
		{
			get
			{
				if (AppConfig.a0 == null)
				{
					string text = AppConfig.a("AgentAllowAutoKillProcessEveryTwoHoursBetweenHourTo", "24");
					AppConfig.a0 = new int?(Util.ToInt(text));
				}
				return AppConfig.a0.Value;
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00044BD8 File Offset: 0x00042DD8
		public static GetContactWayResponse GetContactWay()
		{
			if (AppConfig.a1 == null)
			{
				try
				{
					AppConfig.a1 = AppConfig.WwServiceClient.GetContactWay();
				}
				catch
				{
				}
				if (AppConfig.a1 == null)
				{
					AppConfig.a1 = new GetContactWayResponse
					{
						Wangwang = "agiso",
						WechatGZH = "阿奇索"
					};
				}
			}
			return AppConfig.a1;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0000402A File Offset: 0x0000222A
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x00004031 File Offset: 0x00002231
		public static List<string> SyncEmotionsUserNicks { get; private set; } = new List<string>();

		// Token: 0x060005FA RID: 1530 RVA: 0x00044C48 File Offset: 0x00042E48
		public static void GetMatchReplyInfo(AldsAccountInfo account, string msg, string contactNick, string contactOpenUid, DateTime sendTime, out string errorMsg, out AutoReplyInfo firstReplyInfo, out AutoReplyInfo replyInfo, out bool errMsgBlockSendArMsg)
		{
			errorMsg = "";
			firstReplyInfo = null;
			replyInfo = null;
			errMsgBlockSendArMsg = true;
			if (!(account.UserNick == contactNick) && !(Util.GetMasterNick(account.UserNick) == Util.GetMasterNick(contactNick)) && !(contactNick == AppConfig.RobotUserNick) && !contactNick.StartsWith(AppConfig.RobotUserNick + ":") && !(contactOpenUid == AppConfig.RobotOpenUid) && msg.IndexOf("系统提示") < 0 && msg.IndexOf("系统提醒") < 0 && msg.IndexOf("系统消息") < 0 && msg.IndexOf("对方的离线消息数已经达到上限") < 0 && msg.IndexOf("您还不是对方的好友") < 0 && msg.IndexOf("[自动回复]") < 0)
			{
				int num = 15;
				if ((DateTime.Now - sendTime).TotalMinutes > (double)15f)
				{
					errorMsg = string.Format("{0}->{1}：咨询时间已超过{2}分钟，不答复了。", contactNick, account.UserNick, num);
				}
				else
				{
					AutoReplyCollection autoReplyCollection = AutoReplyCollection.Load(AppConfig.AutoReplyList, account.UserNick, AppConfig.CurrentSystemSettingInfo.AutoReplyBySellerNick, "【适用所有已添加的旺旺】");
					if (LogFirstReplyManager.NotExistsFirstTimeReplyRecent(account.UserNick, contactNick, contactOpenUid, AppConfig.CurrentSystemSettingInfo.FirstReplyInterval) && LogAutoReplyManager.NotExistsFirstTimeReplyRecent(account.UserNick, contactNick, contactOpenUid, AppConfig.CurrentSystemSettingInfo.FirstReplyInterval))
					{
						List<AutoReplyInfo> list = autoReplyCollection.Where(new Func<AutoReplyInfo, bool>(AppConfig.<>c.<>9.a)).ToList<AutoReplyInfo>();
						if (list.Count > 0)
						{
							Random random = new Random();
							int num2 = random.Next(0, list.Count);
							firstReplyInfo = list[num2];
							if (firstReplyInfo != null && string.IsNullOrEmpty(firstReplyInfo.ReplyWord))
							{
								firstReplyInfo = null;
							}
						}
					}
					replyInfo = autoReplyCollection.Match(msg);
					if (replyInfo != null)
					{
						if (firstReplyInfo != null)
						{
							errMsgBlockSendArMsg = false;
							if (replyInfo.Type == EnumArType.NoMatching)
							{
								if (!AppConfig.CurrentSystemSettingInfo.FirstReplyContinueNoMatch)
								{
									replyInfo = null;
								}
								else if (AppConfig.CurrentSystemSettingInfo.NoMatchReplyInterval > 0)
								{
									LogAutoReply recent = LogAutoReplyManager.GetRecent(account.UserNick, contactNick, contactOpenUid, EnumArType.NoMatching);
									if (recent != null && (DateTime.Now - recent.CreateTime).TotalSeconds < (double)AppConfig.CurrentSystemSettingInfo.NoMatchReplyInterval)
									{
										errorMsg = string.Format("无匹配答复后，{0}秒内不再进行无匹配答复", AppConfig.CurrentSystemSettingInfo.NoMatchReplyInterval);
										replyInfo = null;
									}
								}
							}
							else if (!AppConfig.CurrentSystemSettingInfo.FirstReplyContinueMatch)
							{
								replyInfo = null;
							}
						}
						else if (replyInfo.Type == EnumArType.NoMatching && AppConfig.CurrentSystemSettingInfo.NoMatchReplyInterval > 0)
						{
							LogAutoReply recent2 = LogAutoReplyManager.GetRecent(account.UserNick, contactNick, contactOpenUid, EnumArType.NoMatching);
							if (recent2 != null && (DateTime.Now - recent2.CreateTime).TotalSeconds < (double)AppConfig.CurrentSystemSettingInfo.NoMatchReplyInterval)
							{
								errorMsg = string.Format("无匹配答复后，{0}秒内不再进行无匹配答复", AppConfig.CurrentSystemSettingInfo.NoMatchReplyInterval);
								replyInfo = null;
							}
						}
					}
				}
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00044FAC File Offset: 0x000431AC
		public static void HandleSameQuery(AldsAccountInfo account, string queryMsg, string fromNick, string replyMsg, out string errorMsg)
		{
			errorMsg = "";
			if (AppConfig.CurrentSystemSettingInfo.SameQueryReplyInterval > 0)
			{
				if ((DateTime.Now - AppConfig.a5).TotalHours >= 1.0)
				{
					object obj = AppConfig.a6;
					lock (obj)
					{
						if ((DateTime.Now - AppConfig.a5).TotalHours >= 1.0)
						{
							AppConfig.a5 = DateTime.Now;
							foreach (KeyValuePair<string, DateTime> keyValuePair in AppConfig.a3)
							{
								if ((DateTime.Now - keyValuePair.Value).TotalSeconds > (double)AppConfig.CurrentSystemSettingInfo.SameQueryReplyInterval)
								{
									DateTime dateTime;
									AppConfig.a3.TryRemove(keyValuePair.Key, out dateTime);
								}
							}
							foreach (KeyValuePair<string, DateTime> keyValuePair2 in AppConfig.a4)
							{
								if ((DateTime.Now - keyValuePair2.Value).TotalSeconds > (double)AppConfig.CurrentSystemSettingInfo.SameQueryReplyInterval)
								{
									DateTime dateTime;
									AppConfig.a4.TryRemove(keyValuePair2.Key, out dateTime);
								}
							}
						}
					}
				}
				if (!string.IsNullOrEmpty(queryMsg))
				{
					string text = string.Concat(new string[]
					{
						account.UserNick,
						":",
						fromNick,
						":",
						Util.ComputeHashMd5(queryMsg)
					});
					DateTime dateTime2;
					if (AppConfig.a3.TryGetValue(text, out dateTime2) && (DateTime.Now - dateTime2).TotalSeconds <= (double)AppConfig.CurrentSystemSettingInfo.SameQueryReplyInterval)
					{
						errorMsg = string.Format("{0}秒内相同的咨询，不重复答复", AppConfig.CurrentSystemSettingInfo.SameQueryReplyInterval);
						return;
					}
					AppConfig.a3[text] = DateTime.Now;
				}
				if (!string.IsNullOrEmpty(replyMsg))
				{
					string text2 = string.Concat(new string[]
					{
						account.UserNick,
						":",
						fromNick,
						":",
						Util.ComputeHashMd5(replyMsg)
					});
					DateTime dateTime3;
					if (AppConfig.a4.TryGetValue(text2, out dateTime3) && (DateTime.Now - dateTime3).TotalSeconds <= (double)AppConfig.CurrentSystemSettingInfo.SameQueryReplyInterval)
					{
						errorMsg = string.Format("{0}秒内相同的答复，不重复答复", AppConfig.CurrentSystemSettingInfo.SameQueryReplyInterval);
					}
					else
					{
						AppConfig.a4[text2] = DateTime.Now;
					}
				}
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000452CC File Offset: 0x000434CC
		public static void FailAliwwMessage(AliwwMessageInfo ami, SendLevelType sendLevel = 0)
		{
			if (ami.MsgId > 0L && (ami.AliwwMessageSourceType == EnumAliwwMessageSource.FromWwmsgService || ami.AliwwMessageSourceType == EnumAliwwMessageSource.FromWwSocketService))
			{
				List<long> list = new List<long> { ami.MsgId };
				string sellerNick = ami.SellerNick;
				AppConfig.FailAliwwMessage(list, sellerNick, sendLevel);
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00045324 File Offset: 0x00043524
		public static void FailAliwwMessage(List<long> msgIds, string sellerNick, SendLevelType sendLevel = 0)
		{
			if (AppConfig.AllowAutoLogin)
			{
				QnAgentInfo agentInfo = AppConfig.GetAgentInfo(sellerNick);
				string text = ((agentInfo != null) ? agentInfo.QnNick : null);
				if (string.IsNullOrEmpty(text))
				{
					LogWriter.WriteLog("重置消息失败，查无代挂账号信息，" + text, 1);
				}
				else
				{
					FailAgentAliwwMessagesResponse failAgentAliwwMessagesResponse = AppConfig.QnAgentServiceClient.FailAliwwMessages(msgIds, sellerNick, text, sendLevel);
					if (failAgentAliwwMessagesResponse.IsError)
					{
						LogWriter.WriteLog(string.Format("重置消息失败,{0}:{1}", failAgentAliwwMessagesResponse.ErrMsg, string.Join("、", msgIds.ConvertAll<string>(new Converter<long, string>(AppConfig.<>c.<>9.d)).ToArray())), 1);
					}
				}
			}
			else
			{
				FailAliwwMessagesResponse failAliwwMessagesResponse = AppConfig.WwServiceClient.FailAliwwMessages(msgIds, sendLevel);
				if (failAliwwMessagesResponse.IsError)
				{
					LogWriter.WriteLog(string.Format("重置消息失败,{0}:{1}", failAliwwMessagesResponse.ErrMsg, string.Join("、", msgIds.ConvertAll<string>(new Converter<long, string>(AppConfig.<>c.<>9.c)).ToArray())), 1);
				}
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0004543C File Offset: 0x0004363C
		public static void RoolbackMsgs(List<long> msgIds, string sellerNick)
		{
			if (AppConfig.AllowAutoLogin)
			{
				QnAgentInfo agentInfo = AppConfig.GetAgentInfo(sellerNick);
				string text = ((agentInfo != null) ? agentInfo.QnNick : null);
				if (string.IsNullOrEmpty(text))
				{
					LogWriter.WriteLog("回滚消息失败，查无代挂账号信息，" + text, 1);
				}
				else
				{
					AgentRollbackMsgsResponse agentRollbackMsgsResponse = AppConfig.QnAgentServiceClient.RollbackMsgs(msgIds, text);
					if (agentRollbackMsgsResponse.IsError)
					{
						LogWriter.WriteLog(string.Format("回滚消息失败,{0}:{1}", agentRollbackMsgsResponse.ErrMsg, string.Join("、", msgIds.ConvertAll<string>(new Converter<long, string>(AppConfig.<>c.<>9.b)).ToArray())), 1);
					}
				}
			}
			else
			{
				RollbackMsgsResponse rollbackMsgsResponse = AppConfig.WwServiceClient.RollbackMsgs(msgIds, sellerNick);
				if (rollbackMsgsResponse.IsError)
				{
					LogWriter.WriteLog(string.Format("回滚消息失败,{0}:{1}", rollbackMsgsResponse.ErrMsg, string.Join("、", msgIds.ConvertAll<string>(new Converter<long, string>(AppConfig.<>c.<>9.a)).ToArray())), 1);
				}
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00004039 File Offset: 0x00002239
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x00004040 File Offset: 0x00002240
		public static string TestSendErrorMsg { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00004048 File Offset: 0x00002248
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x0000404F File Offset: 0x0000224F
		public static bool HasTestSendAliwwMessageInfo { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00004057 File Offset: 0x00002257
		public static long TestSendMsgId { get; } = long.MinValue;

		// Token: 0x06000604 RID: 1540 RVA: 0x00045554 File Offset: 0x00043754
		public static long GetRandomMsgId()
		{
			return (long)Interlocked.Decrement(ref AppConfig.ba);
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0000405E File Offset: 0x0000225E
		public static BuyerInfoCache BuyerInfoCache { get; } = new BuyerInfoCache();

		// Token: 0x06000606 RID: 1542 RVA: 0x00045570 File Offset: 0x00043770
		public static bool CheckUserNickEqual(string sourceNick, string targetNick)
		{
			sourceNick = ((sourceNick != null) ? sourceNick.Trim() : null);
			targetNick = ((targetNick != null) ? targetNick.Trim() : null);
			bool flag;
			if (string.IsNullOrEmpty(sourceNick) || string.IsNullOrEmpty(targetNick))
			{
				flag = false;
			}
			else if (sourceNick.Contains("**") || targetNick.Contains("**"))
			{
				flag = false;
			}
			else
			{
				sourceNick = sourceNick.Replace("cntaobao", "");
				targetNick = targetNick.Replace("cntaobao", "");
				flag = sourceNick.Equals(targetNick, StringComparison.OrdinalIgnoreCase) || Util.StrConvSimple(sourceNick).Equals(Util.StrConvSimple(targetNick));
			}
			return flag;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x00004065 File Offset: 0x00002265
		// (set) Token: 0x06000608 RID: 1544 RVA: 0x0000406C File Offset: 0x0000226C
		public static int BuyerNickMixCount { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00004074 File Offset: 0x00002274
		public static List<string> UserNickLoginOnQnList { get; } = new List<string>();

		// Token: 0x0600060A RID: 1546 RVA: 0x0000407B File Offset: 0x0000227B
		public static void RestartExplorer()
		{
			Win32Extend.KillProcessByName("explorer");
			Task.Run(new Action(AppConfig.<>c.<>9.a));
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0004561C File Offset: 0x0004381C
		public static void StartWhileNoExitsExplorer()
		{
			if (Util.IsEmptyList<Process>(Process.GetProcessesByName("explorer")))
			{
				Process.Start("C:\\Windows\\explorer.exe");
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x000040AE File Offset: 0x000022AE
		public static AgisoQueue AliwwMsgQueueFirst { get; } = new AgisoQueue();

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x000040B5 File Offset: 0x000022B5
		public static List<long> AliwwMsgIdListOrderByEnqueueTime { get; } = new List<long>();

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x000040BC File Offset: 0x000022BC
		public static Dictionary<long, string> AliwwMsgDictOrderByEnqueueTime { get; } = new Dictionary<long, string>();

		// Token: 0x0600060F RID: 1551 RVA: 0x00045648 File Offset: 0x00043848
		public static void ClearMemorySilent(string reason)
		{
			if (Monitor.TryEnter(AppConfig.bh))
			{
				try
				{
					GC.Collect();
					GC.WaitForPendingFinalizers();
					foreach (Process process in Process.GetProcesses())
					{
						using (process)
						{
							if (!AppConfig.bi.Contains(process.ProcessName, StringComparer.OrdinalIgnoreCase))
							{
								try
								{
									WindowsAPI.EmptyWorkingSet(process.Handle);
								}
								catch
								{
								}
							}
						}
					}
					LogWriter.WriteLog(reason, 1);
				}
				finally
				{
					Monitor.Exit(AppConfig.bh);
				}
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00045700 File Offset: 0x00043900
		private static string a(int A_0)
		{
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Process WHERE ProcessId = " + A_0.ToString()))
			{
				using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							ManagementObject managementObject = (ManagementObject)enumerator.Current;
							using (managementObject)
							{
								try
								{
									string[] array = new string[2];
									ManagementObject managementObject3 = managementObject;
									string text = "GetOwner";
									object[] array2 = array;
									managementObject3.InvokeMethod(text, array2);
									return array[0];
								}
								catch
								{
									return string.Empty;
								}
							}
						}
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x000457F4 File Offset: 0x000439F4
		public static List<int> GetChildPidList(long parentId, string name = "", int recursion = 2)
		{
			List<int> list = new List<int>();
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(string.Format("SELECT ProcessID FROM Win32_Process WHERE ParentProcessID = {0} {1}", parentId, string.IsNullOrEmpty(name) ? "" : ("AND Name = \"" + name + "\""))))
			{
				using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
				{
					recursion--;
					foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						int num = Util.ToInt(managementObject["ProcessID"]);
						list.Add(Util.ToInt(managementObject["ProcessID"]));
						if (recursion > 0)
						{
							list.AddRange(AppConfig.GetChildPidList((long)num, name, recursion));
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00045900 File Offset: 0x00043B00
		public static void LogMsgResultAndNotice(string userNick, bool success)
		{
			string masterNick = Util.GetMasterNick(userNick);
			int num2;
			if (success)
			{
				int num;
				AppConfig.bj.TryRemove(masterNick, out num);
				AppConfig.bk.TryRemove(masterNick, out num);
			}
			else if (AppConfig.bj.TryGetValue(masterNick, out num2))
			{
				if (num2 >= 10 && !AppConfig.bk.ContainsKey(masterNick))
				{
					AppConfig.WwServiceClient.MsgFailNotice(userNick);
					AppConfig.bk.AddOrUpdate(masterNick, 1, new Func<string, int, int>(AppConfig.<>c.<>9.c));
				}
				else
				{
					AppConfig.bj.AddOrUpdate(masterNick, 1, new Func<string, int, int>(AppConfig.<>c.<>9.b));
				}
			}
			else
			{
				AppConfig.bj.AddOrUpdate(masterNick, 1, new Func<string, int, int>(AppConfig.<>c.<>9.a));
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x000459F8 File Offset: 0x00043BF8
		public static bool IsQnSystemMsg(string message)
		{
			return !string.IsNullOrWhiteSpace(message) && (message.Contains("号被禁言") || message.Contains("账号禁言") || message.Contains("您发送的消息中可能包含了恶意网址、违规广告及其他类关键词，请停止发送类似的消息") || message.Contains("由于对方将您添加至黑名单，您的消息已被拒收") || message.Contains("将您添加至黑名单，您的消息已被拒收") || message.Contains("您还不是对方的好友，对方设置不接收陌生人消息，无法收到您发的消息，请相互加为好友") || message.Contains("对方设置不接收陌生人消息，需要对方和您沟通后才能收到您发的消息") || message.Contains("买家已开启消息拒收，无法再发送消息，买家回复或下单后可继续发送") || message.Contains("系统识别到接待过程中存在敷衍行为，敷衍消息已拦截下发，请重新组织有效话术，每一个消费者都是潜在的客户") || message.Contains("亲，由于您短时间内多次发送相同内容，容易给消费者造成困扰，最新的消息已被系统拦截，请重新组织话术") || message.Contains("您发送的消息中包含不安全的链接，消息发送失败") || message.Contains("买家30天内主动和您沟通或下单后，您才能给买家发消息") || message.Contains("请勿使用阿里旺旺、千牛以外的其它聊天工具，以确保沟通、交易的安全") || message.Contains("谨防“订单卡单退款”类电信诈骗，请勿透露银行卡密码、验证码等信息") || message.Contains("消息可能包含存在未知风险的链接，请谨慎访问") || message.Contains("请勿虚假交易、通过不正当方式提高账户信用积分或商品销量，谨防被骗，以免资金损失") || message.Contains("请不要用阿里旺旺以外的其它聊天工具，谨防被骗，网站只视阿里旺旺聊天凭证为有效证据") || message.Contains("注意辨别邮件真伪，切勿向邮件中的陌生账号直接汇款") || message.Contains("在阿里集团旗下网站交易过程中不存在卡单现象，谨防被骗，如有疑问请致电网站客服") || message.Contains("阿里巴巴不会主动通过阿里旺旺要求缴纳消保、买保等资金，如有疑问请联系网站客服，谨防受骗") || message.Contains("请仔细查看是否为阿里集团旗下网站的网址，防止被钓鱼") || message.Contains("请勿轻易相信他人发送来的中奖信息，如有疑问请联系网站客服，谨防被骗") || message.Contains("请勿通过银行卡或者支付宝直接转账、汇款进行交易，谨防诈骗，以免资金损失") || message.Contains("支付宝不存在卡单现象，如果你已经支付，请马上修改密码，有疑问联系客服") || message.Contains("您发送的消息中可能包含了存在交易风险的外部网站或移动互联网应用信息，请不要继续发送此类消息。如您继续发布，则可能造成您的账户无法登录或使用阿里旺旺及其子账号") || message.Contains("红包无法作为交易凭证，如需购物交易，请拍下订单付款，不要用红包付款") || message.Contains("请注意保护您的个人信息，以免造成不必要的损失") || (message.Contains("由 ") && message.Contains(" 转交给")) || message.Contains("在聊天过程中，请勿以骚扰、诱导、好评返现等方式引导用户进行不真实评价，平台会对评价、回复内容逐条审核，若出现干扰用户评价真实性、客观性、自主性的行为及内容，平台将依照相关规则进行违规处理") || message.Contains("请不要轻信网络兼职，不要相信任何兼职形式的佣金返现，谨防被骗！") || message.Contains("您发送的消息中可能包含了存在交易风险的外部网站或移动互联网应用信息，请勿使用阿里旺旺、千牛以外的其它聊天工具，以确保买卖方沟通、交易安全") || message.Contains("为了确保交易安全，请务必在平台内完成所有交易。若脱离平台交易，平台无法为您的正当权益提供安全保障，同时将依据规则对您进行相应的处罚。") || message.Contains("安全提醒: 请勿发送外部非法链接，避免被平台处罚扣分甚至冻结店铺！") || message == "撤回了一条消息" || message.Contains("请不要用阿里集团外的聊天工具，谨防被骗，网站只视阿里旺旺或千牛聊天凭证为有效证据") || message.Contains("在聊天过程中，请勿以骚扰、诱导、好评返现等方式引导用户进行不真实评价，若出现干扰用户评价真实性、客观性、自主性的行为及内容，平台将依照相关规则进行违规处理。 推荐使用官方「评价有礼」工具，鼓励用户真实评价。详细规则和使用说明见：千牛后台-评价管理-评价有礼") || message.Contains("诈骗案件频发，谨防假客服欺诈！任何以不能下单或店铺有问题诱导卖家点击链接或扫码跳转到微信、QQ、支付宝或其它非官方平台的行为都是欺诈") || message.Contains("根据国家有关法律法规及平台规则，购买网游类商品需年满18周岁。平台严禁商家向未成年人提供相关商品，禁止未成年人购买") || AppConfig.i.IsMatch(message));
		}

		// Token: 0x04000414 RID: 1044
		public const string STARTUP_PATH_ALIWW = "C:\\Program Files (x86)\\AliWangWang\\";

		// Token: 0x04000415 RID: 1045
		public const string FIRST_REPLY_KEY_WORD = "【Agiso首次智能答复Agiso】";

		// Token: 0x04000416 RID: 1046
		public static int SocketServerPort = 0;

		// Token: 0x04000417 RID: 1047
		public const int MAX_AUTO_REPLY_COUNT = 1000;

		// Token: 0x04000418 RID: 1048
		public static string AliWorkbenchDataPath = "C:\\Users\\Public\\Documents\\AliWorkbench\\AliWorkbenchData";

		// Token: 0x04000419 RID: 1049
		public static bool LocalPc = Util.ToBoolean(ConfigurationManager.AppSettings["LocalPc"]);

		// Token: 0x0400041A RID: 1050
		public const string TransferMessageTitle = "【转接前回复消息】";

		// Token: 0x0400041B RID: 1051
		private static Regex i = new Regex("你还可以向该买家发送\\d+条消息");

		// Token: 0x0400041C RID: 1052
		[CompilerGenerated]
		private static WebSocketServerIns j;

		// Token: 0x0400041D RID: 1053
		private static string k;

		// Token: 0x0400041E RID: 1054
		[CompilerGenerated]
		private static DateTime l;

		// Token: 0x0400041F RID: 1055
		public const string HTTP_SERVER_PROTOCOL = "https";

		// Token: 0x04000420 RID: 1056
		public const string CERTIFICATE_DOMAIN = "localhost";

		// Token: 0x04000421 RID: 1057
		public const string CERTIFICATE_PASSWORD = "1523866676111";

		// Token: 0x04000422 RID: 1058
		public const string REMOTE_JS_URL = "http://wwmsg.agiso.com/qn20180420.js";

		// Token: 0x04000423 RID: 1059
		[CompilerGenerated]
		private static float m;

		// Token: 0x04000424 RID: 1060
		private static Dictionary<string, string> n = new Dictionary<string, string>();

		// Token: 0x04000425 RID: 1061
		private static string o;

		// Token: 0x04000426 RID: 1062
		[CompilerGenerated]
		private static readonly Dictionary<string, string> p;

		// Token: 0x04000427 RID: 1063
		private static AgentHostSetting q = AgentHostSetting.CreateDefault(1L);

		// Token: 0x04000428 RID: 1064
		[CompilerGenerated]
		private static AgentProxyInfo r;

		// Token: 0x04000429 RID: 1065
		[CompilerGenerated]
		private static int s;

		// Token: 0x0400042A RID: 1066
		[CompilerGenerated]
		private static List<string> t;

		// Token: 0x0400042B RID: 1067
		private static List<AutoReplyInfo> u;

		// Token: 0x0400042C RID: 1068
		public static DefaultClient WwServiceClient = new DefaultClient(AppConfig.GetCurrentApplicationVersion());

		// Token: 0x0400042D RID: 1069
		public static WebSocketClient WwWebSocketClient;

		// Token: 0x0400042E RID: 1070
		public static QnAgentClient QnAgentServiceClient = new QnAgentClient(AppConfig.AgentSecret, AppConfig.AgentPathPrefix, AppConfig.GetCurrentApplicationVersion())
		{
			GzipAccept = true
		};

		// Token: 0x0400042F RID: 1071
		[CompilerGenerated]
		private static readonly Dictionary<string, QnAgentInfo> v;

		// Token: 0x04000430 RID: 1072
		private static List<string> w = new List<string>();

		// Token: 0x04000431 RID: 1073
		private static SystemSettingsInfo x;

		// Token: 0x04000432 RID: 1074
		[CompilerGenerated]
		private static readonly ConcurrentDictionary<string, SellerCache> y;

		// Token: 0x04000433 RID: 1075
		[CompilerGenerated]
		private static readonly ConcurrentDictionary<string, UserCache> z;

		// Token: 0x04000434 RID: 1076
		public const string AUTO_REPLY_ALL_SELLER_FIT = "【适用所有已添加的旺旺】";

		// Token: 0x04000435 RID: 1077
		private static object aa = new object();

		// Token: 0x04000436 RID: 1078
		private static BindingList<AldsAccountInfo> ab;

		// Token: 0x04000437 RID: 1079
		private static ConcurrentDictionary<string, AldsAccountInfo> ac;

		// Token: 0x04000438 RID: 1080
		private static string ad;

		// Token: 0x04000439 RID: 1081
		private static int ae = -1;

		// Token: 0x0400043A RID: 1082
		private static int af = -1;

		// Token: 0x0400043B RID: 1083
		private static string ag;

		// Token: 0x0400043C RID: 1084
		private static bool? ah;

		// Token: 0x0400043D RID: 1085
		private static bool? ai;

		// Token: 0x0400043E RID: 1086
		private static AliwwClientMode aj;

		// Token: 0x0400043F RID: 1087
		private static bool? ak;

		// Token: 0x04000440 RID: 1088
		private static bool? al;

		// Token: 0x04000441 RID: 1089
		private static MailSenderAccount am;

		// Token: 0x04000442 RID: 1090
		private static MailSenderAccount an;

		// Token: 0x04000443 RID: 1091
		private static string ao = null;

		// Token: 0x04000444 RID: 1092
		private static Dictionary<NoticeAdminType, DateTime> ap = new Dictionary<NoticeAdminType, DateTime>();

		// Token: 0x04000445 RID: 1093
		private static string aq;

		// Token: 0x04000446 RID: 1094
		private static string ar;

		// Token: 0x04000447 RID: 1095
		private static string @as;

		// Token: 0x04000448 RID: 1096
		public static bool AllowReconnectionWwMsgWebSocket = true;

		// Token: 0x04000449 RID: 1097
		[CompilerGenerated]
		private static long at;

		// Token: 0x0400044A RID: 1098
		private static int? au;

		// Token: 0x0400044B RID: 1099
		public static string QnIniTxtFormat = "[Common]\r\nVersion={0}\r\n";

		// Token: 0x0400044C RID: 1100
		public static FixedLengthQueue UsedTimeQueue = new FixedLengthQueue(20U);

		// Token: 0x0400044D RID: 1101
		private static string[] av;

		// Token: 0x0400044E RID: 1102
		private static string aw;

		// Token: 0x0400044F RID: 1103
		private static string ax;

		// Token: 0x04000450 RID: 1104
		private static MsgSendSoftware? ay;

		// Token: 0x04000451 RID: 1105
		private static int? az;

		// Token: 0x04000452 RID: 1106
		private static int? a0;

		// Token: 0x04000453 RID: 1107
		private static GetContactWayResponse a1;

		// Token: 0x04000454 RID: 1108
		[CompilerGenerated]
		private static List<string> a2;

		// Token: 0x04000455 RID: 1109
		private static ConcurrentDictionary<string, DateTime> a3 = new ConcurrentDictionary<string, DateTime>();

		// Token: 0x04000456 RID: 1110
		private static ConcurrentDictionary<string, DateTime> a4 = new ConcurrentDictionary<string, DateTime>();

		// Token: 0x04000457 RID: 1111
		private static DateTime a5 = DateTime.Now;

		// Token: 0x04000458 RID: 1112
		private static object a6 = new object();

		// Token: 0x04000459 RID: 1113
		[CompilerGenerated]
		private static string a7;

		// Token: 0x0400045A RID: 1114
		[CompilerGenerated]
		private static bool a8;

		// Token: 0x0400045B RID: 1115
		[CompilerGenerated]
		private static readonly long a9;

		// Token: 0x0400045C RID: 1116
		private static int ba = 0;

		// Token: 0x0400045D RID: 1117
		[CompilerGenerated]
		private static readonly BuyerInfoCache bb;

		// Token: 0x0400045E RID: 1118
		[CompilerGenerated]
		private static int bc;

		// Token: 0x0400045F RID: 1119
		[CompilerGenerated]
		private static readonly List<string> bd;

		// Token: 0x04000460 RID: 1120
		[CompilerGenerated]
		private static readonly AgisoQueue be;

		// Token: 0x04000461 RID: 1121
		[CompilerGenerated]
		private static readonly List<long> bf;

		// Token: 0x04000462 RID: 1122
		[CompilerGenerated]
		private static readonly Dictionary<long, string> bg;

		// Token: 0x04000463 RID: 1123
		private static object bh = new object();

		// Token: 0x04000464 RID: 1124
		private static List<string> bi = new List<string> { "explorer", "winlogon" };

		// Token: 0x04000465 RID: 1125
		public static Regex NewLineRegex = new Regex("^\r\n$");

		// Token: 0x04000466 RID: 1126
		private static readonly ConcurrentDictionary<string, int> bj = new ConcurrentDictionary<string, int>();

		// Token: 0x04000467 RID: 1127
		private static readonly ConcurrentDictionary<string, int> bk = new ConcurrentDictionary<string, int>();

		// Token: 0x020000C3 RID: 195
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x06000626 RID: 1574 RVA: 0x00004113 File Offset: 0x00002313
			internal bool b(AldsAccountInfo A_0)
			{
				return A_0.UserNick == this.a;
			}

			// Token: 0x04000477 RID: 1143
			public string a;
		}

		// Token: 0x020000C4 RID: 196
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x06000628 RID: 1576 RVA: 0x00004126 File Offset: 0x00002326
			internal bool b(AldsAccountInfo A_0)
			{
				return string.Equals(Util.StrConvSimple(A_0.UserNick), this.a, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x04000478 RID: 1144
			public string a;
		}

		// Token: 0x020000C5 RID: 197
		[CompilerGenerated]
		private sealed class c
		{
			// Token: 0x0600062A RID: 1578 RVA: 0x0000413F File Offset: 0x0000233F
			internal bool b(KeyValuePair<string, QnAgentInfo> A_0)
			{
				return Util.StrConvSimple(A_0.Key) == Util.StrConvSimple(this.a);
			}

			// Token: 0x04000479 RID: 1145
			public string a;
		}

		// Token: 0x020000C6 RID: 198
		[CompilerGenerated]
		private sealed class d
		{
			// Token: 0x0600062C RID: 1580 RVA: 0x00045F74 File Offset: 0x00044174
			internal bool c(CustomerServiceWorksheet A_0)
			{
				bool flag;
				if (A_0.DetailWorksheet == null)
				{
					flag = false;
				}
				else if (!A_0.DetailWorksheet.ContainsKey(this.a.DayOfWeek))
				{
					flag = false;
				}
				else
				{
					IEnumerable<WorkTimeInfo> enumerable = A_0.DetailWorksheet[this.a.DayOfWeek];
					Func<WorkTimeInfo, bool> func;
					if ((func = this.b) == null)
					{
						func = (this.b = new Func<WorkTimeInfo, bool>(this.c));
					}
					flag = enumerable.Any(func);
				}
				return flag;
			}

			// Token: 0x0600062D RID: 1581 RVA: 0x00045FEC File Offset: 0x000441EC
			internal bool c(WorkTimeInfo A_0)
			{
				return A_0.HourFrom * 60 + A_0.MinuteFrom <= this.a.Hour * 60 + this.a.Minute && A_0.HourTo * 60 + A_0.MinuteTo > this.a.Hour * 60 + this.a.Minute;
			}

			// Token: 0x0400047A RID: 1146
			public DateTime a;

			// Token: 0x0400047B RID: 1147
			public Func<WorkTimeInfo, bool> b;
		}

		// Token: 0x020000C7 RID: 199
		[CompilerGenerated]
		private sealed class e
		{
			// Token: 0x0600062F RID: 1583 RVA: 0x00046054 File Offset: 0x00044254
			internal bool b(CustomerServiceWorksheet A_0)
			{
				bool flag;
				if (A_0.DetailWorksheet == null)
				{
					flag = false;
				}
				else if (!A_0.DetailWorksheet.ContainsKey(this.a.DayOfWeek))
				{
					flag = false;
				}
				else
				{
					List<WorkTimeInfo> list = A_0.DetailWorksheet[this.a.DayOfWeek];
					foreach (WorkTimeInfo workTimeInfo in list)
					{
						if (workTimeInfo.HourFrom * 60 + workTimeInfo.MinuteFrom <= this.a.Hour * 60 + this.a.Minute && workTimeInfo.HourTo * 60 + workTimeInfo.MinuteTo > this.a.Hour * 60 + this.a.Minute)
						{
							return true;
						}
					}
					flag = false;
				}
				return flag;
			}

			// Token: 0x0400047C RID: 1148
			public DateTime a;
		}

		// Token: 0x020000C8 RID: 200
		[CompilerGenerated]
		private sealed class f
		{
			// Token: 0x0400047D RID: 1149
			public string a;
		}

		// Token: 0x020000C9 RID: 201
		[CompilerGenerated]
		private sealed class g
		{
			// Token: 0x06000632 RID: 1586 RVA: 0x00046154 File Offset: 0x00044354
			internal bool d(CustomerServiceWorksheet A_0)
			{
				bool flag;
				if (A_0.UserNick != this.b.a || A_0.DetailWorksheet == null || !A_0.DetailWorksheet.ContainsKey(this.a.DayOfWeek))
				{
					flag = false;
				}
				else
				{
					IEnumerable<WorkTimeInfo> enumerable = A_0.DetailWorksheet[this.a.DayOfWeek];
					Func<WorkTimeInfo, bool> func;
					if ((func = this.c) == null)
					{
						func = (this.c = new Func<WorkTimeInfo, bool>(this.d));
					}
					flag = enumerable.Any(func);
				}
				return flag;
			}

			// Token: 0x06000633 RID: 1587 RVA: 0x000461E0 File Offset: 0x000443E0
			internal bool d(WorkTimeInfo A_0)
			{
				return A_0.HourFrom * 60 + A_0.MinuteFrom <= this.a.Hour * 60 + this.a.Minute && A_0.HourTo * 60 + A_0.MinuteTo > this.a.Hour * 60 + this.a.Minute;
			}

			// Token: 0x0400047E RID: 1150
			public DateTime a;

			// Token: 0x0400047F RID: 1151
			public AppConfig.f b;

			// Token: 0x04000480 RID: 1152
			public Func<WorkTimeInfo, bool> c;
		}

		// Token: 0x020000CA RID: 202
		[CompilerGenerated]
		private sealed class h
		{
			// Token: 0x06000635 RID: 1589 RVA: 0x0000415D File Offset: 0x0000235D
			internal UserCache b(string A_0, UserCache A_1)
			{
				return this.a;
			}

			// Token: 0x04000481 RID: 1153
			public UserCache a;
		}
	}
}
