using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Agiso;
using Agiso.Utils;
using AliwwClient.Object;
using Newtonsoft.Json;

namespace AliwwClient.WebSocketServer.Extensions
{
	// Token: 0x0200007B RID: 123
	public static class BehaviorBaseExtension
	{
		// Token: 0x06000383 RID: 899 RVA: 0x00038578 File Offset: 0x00036778
		private static string a(this BehaviorBase A_0, string A_1, out bool A_2)
		{
			A_2 = Util.IsNum(A_1);
			string text;
			if (A_0 == null)
			{
				text = "";
			}
			else if (string.IsNullOrEmpty(A_1))
			{
				text = "";
			}
			else
			{
				if (A_0 is RecentBehavior && !A_2)
				{
					string recentOpenUidByAldsOpenUid = AppConfig.BuyerInfoCache.GetRecentOpenUidByAldsOpenUid(A_1);
					if (!string.IsNullOrEmpty(recentOpenUidByAldsOpenUid))
					{
						A_2 = Util.IsNum(recentOpenUidByAldsOpenUid);
						return recentOpenUidByAldsOpenUid;
					}
				}
				else if ((A_0 is AldsBehavior) & A_2)
				{
					string aldsOpenUidByRecentOpenUid = AppConfig.BuyerInfoCache.GetAldsOpenUidByRecentOpenUid(A_1);
					if (!string.IsNullOrEmpty(aldsOpenUidByRecentOpenUid))
					{
						A_2 = Util.IsNum(aldsOpenUidByRecentOpenUid);
						return aldsOpenUidByRecentOpenUid;
					}
				}
				text = A_1;
			}
			return text;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00038620 File Offset: 0x00036820
		public static bool CheckCallUserInfoEqualsByQnApi(this BehaviorBase session, string contactNick, string contactOpenUid)
		{
			ActiveUserInfo activeUserInfo;
			return session.CheckCallUserInfoEqualsByQnApi(contactNick, contactOpenUid, out activeUserInfo);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00038638 File Offset: 0x00036838
		public static bool CheckCallUserInfoEqualsByQnApi(this BehaviorBase session, string contactNick, string contactOpenUid, out ActiveUserInfo activeUserInfo)
		{
			activeUserInfo = session.GetActiveUserInfo();
			bool flag;
			if (activeUserInfo == null)
			{
				flag = false;
			}
			else if (!string.IsNullOrEmpty(contactOpenUid) && !string.IsNullOrEmpty(activeUserInfo.SecurityUID) && contactOpenUid.Equals(activeUserInfo.SecurityUID))
			{
				flag = true;
			}
			else if (!string.IsNullOrEmpty(contactNick) && !contactNick.Contains("**") && !string.IsNullOrEmpty(activeUserInfo.Uid) && !activeUserInfo.Uid.Contains("**") && (("cntaobao" + contactNick).Equals(activeUserInfo.Uid) || ("cnalichn" + contactNick).Equals(activeUserInfo.Uid) || Util.GetMasterNick("cntaobao" + contactNick).Equals(activeUserInfo.Uid) || Util.GetMasterNick("cnalichn" + contactNick).Equals(activeUserInfo.Uid)))
			{
				flag = true;
			}
			else
			{
				if (AppConfig.BuyerNickMixCount < 5 && activeUserInfo != null && !string.IsNullOrEmpty(activeUserInfo.Uid) && activeUserInfo.Uid.Contains("**"))
				{
					AppConfig.BuyerNickMixCount++;
					AppConfig.QnAgentServiceClient.AutoLoginError(session.AccountUserNick, null, string.Concat(new string[] { "店铺", session.AccountUserNick, "，买家昵称", activeUserInfo.Uid, "，已经在灰度了" }), 2L, "", "");
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000387EC File Offset: 0x000369EC
		public static ActiveUserInfo GetActiveUserInfo(this BehaviorBase session)
		{
			BehaviorBaseExtension.c c = new BehaviorBaseExtension.c();
			c.a = session;
			ActiveUserInfo activeUserInfo;
			if (c.a == null)
			{
				activeUserInfo = null;
			}
			else
			{
				c.b = Guid.NewGuid().ToString();
				c.a.DictWebSocketObj[c.b] = null;
				if (!c.a.SendGetToUserNick(c.b))
				{
					activeUserInfo = null;
				}
				else
				{
					Util.CheckWait(1500, new Func<bool>(c.c), 20);
					ActiveUserInfo activeUserInfo2 = c.a.DictWebSocketObj[c.b] as ActiveUserInfo;
					object obj;
					c.a.DictWebSocketObj.TryRemove(c.b, out obj);
					activeUserInfo = activeUserInfo2;
				}
			}
			return activeUserInfo;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000388B4 File Offset: 0x00036AB4
		public static string GetDisplayNick(this BehaviorBase session, string contactUid, string contactOpenUid)
		{
			BehaviorBaseExtension.d d = new BehaviorBaseExtension.d();
			d.a = session;
			string text;
			if (d.a == null)
			{
				text = null;
			}
			else if (string.IsNullOrEmpty(contactOpenUid))
			{
				text = null;
			}
			else
			{
				bool flag;
				contactOpenUid = d.a.a(contactOpenUid, out flag);
				if (d.a is RecentBehavior && !flag)
				{
					text = null;
				}
				else if (d.a is AldsBehavior && flag)
				{
					text = null;
				}
				else
				{
					d.b = Guid.NewGuid().ToString();
					d.a.DictWebSocketString[d.b] = null;
					try
					{
						Hashtable hashtable = new Hashtable();
						hashtable["guid"] = d.b;
						hashtable["type"] = "displayNick";
						hashtable["uids"] = new string[] { contactUid.StartsWith("cntaobao") ? contactUid : ("cntaobao" + contactUid) };
						if (!string.IsNullOrEmpty(contactOpenUid))
						{
							hashtable["securityUIDs"] = new string[] { contactOpenUid };
						}
						if (!d.a.SendTo(JSON.Encode(hashtable)))
						{
							text = null;
						}
						else
						{
							if (Util.CheckWait(1500, new Func<bool>(d.c), 200) && !string.IsNullOrEmpty(d.a.DictWebSocketString[d.b]))
							{
								Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(d.a.DictWebSocketString[d.b]);
								if (dictionary.ContainsKey(contactOpenUid))
								{
									return dictionary[contactOpenUid];
								}
							}
							text = null;
						}
					}
					finally
					{
						string text2;
						d.a.DictWebSocketString.TryRemove(d.b, out text2);
					}
				}
			}
			return text;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00038AB0 File Offset: 0x00036CB0
		public static bool OpenChat(this BehaviorBase session, string contactNick, string contactOpenUid, string contactSideId = "cntaobao")
		{
			bool flag;
			if (session == null)
			{
				flag = false;
			}
			else
			{
				bool flag2;
				contactOpenUid = session.a(contactOpenUid, out flag2);
				if (!string.IsNullOrEmpty(contactOpenUid) && (string.IsNullOrEmpty(contactNick) || contactNick.Contains("**")))
				{
					if (session is RecentBehavior && !flag2)
					{
						return false;
					}
					if (session is AldsBehavior && flag2)
					{
						return false;
					}
				}
				Hashtable hashtable = new Hashtable
				{
					{ "type", "openChat" },
					{
						"nick",
						contactSideId + contactNick
					},
					{ "bizDomain", "taobao" },
					{ "sceneParam", "{\"toRole\":\"buyer\"}" }
				};
				if (!string.IsNullOrEmpty(contactOpenUid) && ((session is RecentBehavior && flag2) || (session is AldsBehavior && !flag2)))
				{
					hashtable.Add("securityUID", contactOpenUid);
				}
				flag = session.SendTo(JSON.Encode(hashtable));
			}
			return flag;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00038BBC File Offset: 0x00036DBC
		public static bool smethod_0(this BehaviorBase session, string contactNick, string contactOpenUid, string msgContent, int type = 0, string contactSideId = "cntaobao")
		{
			BehaviorBaseExtension.e e = new BehaviorBaseExtension.e();
			e.a = session;
			bool flag;
			if (e.a == null)
			{
				flag = false;
			}
			else
			{
				bool flag2;
				contactOpenUid = e.a.a(contactOpenUid, out flag2);
				if (!string.IsNullOrEmpty(contactOpenUid) && (string.IsNullOrEmpty(contactNick) || contactNick.Contains("**")))
				{
					if (e.a is RecentBehavior && !flag2)
					{
						return false;
					}
					if (e.a is AldsBehavior && flag2)
					{
						return false;
					}
				}
				e.b = Guid.NewGuid().ToString();
				e.a.DictWebSocketInt[e.b] = 0;
				Hashtable hashtable = new Hashtable();
				hashtable["uid"] = contactSideId + contactNick;
				hashtable["text"] = msgContent;
				hashtable["type"] = type;
				if (!string.IsNullOrEmpty(contactOpenUid) && ((e.a is RecentBehavior && flag2) || (e.a is AldsBehavior && !flag2)))
				{
					hashtable["securityUID"] = contactOpenUid;
				}
				hashtable["bizDomain"] = "taobao";
				hashtable["guid"] = e.b;
				if (!e.a.SendTo("{\"type\":\"setTextByApi\", \"msg\": " + JSON.Encode(hashtable) + "}"))
				{
					flag = false;
				}
				else
				{
					Util.CheckWait(2000, new Func<bool>(e.c), 100);
					flag = e.a.DictWebSocketInt[e.b] > 0;
				}
			}
			return flag;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00038D94 File Offset: 0x00036F94
		public static void OpenWebHtml(this BehaviorBase session, string openUrl, OpenUrlType urlType, Hashtable hsParams = null)
		{
			if (session != null)
			{
				if (hsParams == null)
				{
					hsParams = new Hashtable();
				}
				hsParams["url"] = openUrl;
				hsParams["openUrlType"] = (int)urlType;
				string text = "{\"type\":\"openDsrTaskHtml\", \"msg\": " + JSON.Encode(hsParams) + "}";
				session.SendTo(text);
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00038DF4 File Offset: 0x00036FF4
		public static string SendAndReceBackMsg(this BehaviorBase session, string msg)
		{
			BehaviorBaseExtension.f f = new BehaviorBaseExtension.f();
			f.a = session;
			string text;
			if (f.a == null)
			{
				text = "";
			}
			else
			{
				f.b = Guid.NewGuid().ToString();
				f.a.DictWebSocketString[f.b] = "";
				Hashtable hashtable = new Hashtable();
				hashtable["type"] = "sendAndReceBackMsg";
				hashtable["guid"] = f.b;
				hashtable["msg"] = msg;
				f.a.SendTo(JSON.Encode(hashtable));
				if (Util.CheckWait(3000, new Func<bool>(f.c), 200))
				{
					text = f.a.DictWebSocketString[f.b];
				}
				else
				{
					text = "";
				}
			}
			return text;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00038EE0 File Offset: 0x000370E0
		public static string GetHtml(this RecentBehavior session)
		{
			BehaviorBaseExtension.g g = new BehaviorBaseExtension.g();
			g.a = session;
			string text;
			if (g.a == null)
			{
				text = "";
			}
			else
			{
				g.b = Guid.NewGuid().ToString();
				g.a.DictWebSocketString[g.b] = null;
				g.a.SendGetHtml(g.b);
				g.c = "";
				Util.CheckWait(1500, new Func<bool>(g.d), 20);
				string text2;
				g.a.DictWebSocketString.TryRemove(g.b, out text2);
				text = g.c;
			}
			return text;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00038F98 File Offset: 0x00037198
		public static int GetHtmlLength(this RecentBehavior session)
		{
			BehaviorBaseExtension.a a = new BehaviorBaseExtension.a();
			a.a = session;
			int num;
			if (a.a == null)
			{
				num = 0;
			}
			else
			{
				a.b = Guid.NewGuid().ToString();
				a.a.DictWebSocketInt[a.b] = 0;
				string text = "{\"type\":\"getHtmlLength\", \"msg\": \"" + a.b + "\"}";
				a.a.SendTo(text);
				Util.CheckWait(500, new Func<bool>(a.c), 20);
				int num2 = a.a.DictWebSocketInt[a.b];
				int num3;
				a.a.DictWebSocketInt.TryRemove(a.b, out num3);
				num = num2;
			}
			return num;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0003906C File Offset: 0x0003726C
		public static bool TransferContact(this BehaviorBase session, string contactNick, string contactSiteId, string contactOpenUid, string transferNick, string transferSiteId)
		{
			BehaviorBaseExtension.b b = new BehaviorBaseExtension.b();
			b.a = session;
			if (!contactNick.Contains("**") && contactNick.Contains(":"))
			{
				contactOpenUid = "";
			}
			contactOpenUid = contactOpenUid ?? "";
			bool flag;
			if (string.IsNullOrEmpty(b.a.Version))
			{
				flag = b.a.SendTransfer(contactNick, contactSiteId, contactOpenUid, transferNick, transferSiteId, "");
			}
			else
			{
				b.b = Guid.NewGuid().ToString("N");
				b.a.DictWebSocketString[b.b] = null;
				if (!b.a.SendTransfer(contactNick, contactSiteId, contactOpenUid, transferNick, transferSiteId, b.b))
				{
					flag = false;
				}
				else
				{
					Util.CheckWait(1500, new Func<bool>(b.c), 100);
					string text = b.a.DictWebSocketString[b.b];
					string text2;
					b.a.DictWebSocketString.TryRemove(b.b, out text2);
					if (text == null)
					{
						k.a().WriteLine(string.Concat(new string[]
						{
							b.a.AccountUserNick,
							"\t",
							contactNick,
							"\t转接到",
							transferNick,
							"异常"
						}));
					}
					else if (text == "")
					{
						k.a().WriteLine(b.a.AccountUserNick + "\t" + contactNick + "\t转接成功");
					}
					else if (!string.IsNullOrEmpty(text))
					{
						if (text.Contains("被转交客服不在线"))
						{
							k.a().WriteLine(string.Concat(new string[]
							{
								b.a.AccountUserNick,
								"\t",
								contactNick,
								"\t转接到",
								transferNick,
								"失败，被转接客服不在线"
							}));
						}
						else if (text.Contains("不是团队成员"))
						{
							k.a().WriteLine(string.Concat(new string[]
							{
								b.a.AccountUserNick,
								"\t",
								contactNick,
								"\t转接到",
								transferNick,
								"失败，被转接客服不存在"
							}));
						}
						else
						{
							k.a().WriteLine(string.Concat(new string[]
							{
								b.a.AccountUserNick,
								"\t",
								contactNick,
								"\t转接到",
								transferNick,
								"失败，",
								text
							}));
							LogWriter.WriteLog(string.Concat(new string[]
							{
								"转接失败，账号",
								contactNick,
								"从",
								b.a.AccountUserNick,
								"转接到",
								transferNick,
								"失败"
							}), 1);
						}
					}
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0200007C RID: 124
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x06000390 RID: 912 RVA: 0x00039374 File Offset: 0x00037574
			internal bool c()
			{
				return this.a.DictWebSocketInt.ContainsKey(this.b) && this.a.DictWebSocketInt[this.b] > 0;
			}

			// Token: 0x0400031B RID: 795
			public RecentBehavior a;

			// Token: 0x0400031C RID: 796
			public string b;
		}

		// Token: 0x0200007D RID: 125
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x06000392 RID: 914 RVA: 0x000033DF File Offset: 0x000015DF
			internal bool c()
			{
				return this.a.DictWebSocketString[this.b] != null;
			}

			// Token: 0x0400031D RID: 797
			public BehaviorBase a;

			// Token: 0x0400031E RID: 798
			public string b;
		}

		// Token: 0x0200007E RID: 126
		[CompilerGenerated]
		private sealed class c
		{
			// Token: 0x06000394 RID: 916 RVA: 0x000033FA File Offset: 0x000015FA
			internal bool c()
			{
				return this.a.DictWebSocketObj[this.b] is ActiveUserInfo;
			}

			// Token: 0x0400031F RID: 799
			public BehaviorBase a;

			// Token: 0x04000320 RID: 800
			public string b;
		}

		// Token: 0x0200007F RID: 127
		[CompilerGenerated]
		private sealed class d
		{
			// Token: 0x06000396 RID: 918 RVA: 0x0000341A File Offset: 0x0000161A
			internal bool c()
			{
				return this.a.DictWebSocketString[this.b] != null;
			}

			// Token: 0x04000321 RID: 801
			public BehaviorBase a;

			// Token: 0x04000322 RID: 802
			public string b;
		}

		// Token: 0x02000080 RID: 128
		[CompilerGenerated]
		private sealed class e
		{
			// Token: 0x06000398 RID: 920 RVA: 0x00003435 File Offset: 0x00001635
			internal bool c()
			{
				return this.a.DictWebSocketInt[this.b] != 0;
			}

			// Token: 0x04000323 RID: 803
			public BehaviorBase a;

			// Token: 0x04000324 RID: 804
			public string b;
		}

		// Token: 0x02000081 RID: 129
		[CompilerGenerated]
		private sealed class f
		{
			// Token: 0x0600039A RID: 922 RVA: 0x000393C0 File Offset: 0x000375C0
			internal bool c()
			{
				return !string.IsNullOrEmpty(this.a.DictWebSocketString[this.b]);
			}

			// Token: 0x04000325 RID: 805
			public BehaviorBase a;

			// Token: 0x04000326 RID: 806
			public string b;
		}

		// Token: 0x02000082 RID: 130
		[CompilerGenerated]
		private sealed class g
		{
			// Token: 0x0600039C RID: 924 RVA: 0x000393EC File Offset: 0x000375EC
			internal bool d()
			{
				bool flag;
				if (this.a.DictWebSocketString[this.b] != null)
				{
					this.c = this.a.DictWebSocketString[this.b];
					flag = true;
				}
				else
				{
					flag = false;
				}
				return flag;
			}

			// Token: 0x04000327 RID: 807
			public RecentBehavior a;

			// Token: 0x04000328 RID: 808
			public string b;

			// Token: 0x04000329 RID: 809
			public string c;
		}
	}
}
