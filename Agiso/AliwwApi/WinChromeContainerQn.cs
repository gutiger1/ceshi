using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Agiso.ChromeDevTools;
using Agiso.Utils;
using Agiso.WwService.Sdk;
using AliwwClient.Cache;
using AliwwClient.Object;
using AliwwClient.Properties;
using AliwwClient.WebSocketServer;
using AliwwClient.WebSocketServer.Extensions;
using Newtonsoft.Json;

namespace Agiso.AliwwApi
{
	// Token: 0x0200070C RID: 1804
	public abstract class WinChromeContainerQn : WinChromeContainer
	{
		// Token: 0x0600239F RID: 9119 RVA: 0x0005D9AC File Offset: 0x0005BBAC
		public ExecuteJsResultInfo ExecuteJsByRecent(string js, out RemoteSessionsResponse rsRsp)
		{
			ExecuteJsResultInfo executeJsResultInfo = base.Execute("/recent.html", "type=1&", js, out rsRsp);
			if (rsRsp == null)
			{
				executeJsResultInfo = base.Execute("/recent606.html", "type=1&", js, out rsRsp);
			}
			return executeJsResultInfo;
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x0005D9F0 File Offset: 0x0005BBF0
		public bool SetMsgByQnApiAuto(string userNick, string contactNick, string contactSiteId, string contactOpenUid, string msgContent, int type = 0)
		{
			UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(userNick);
			bool flag;
			if (!userCacheOrCreate.IsSessionNull)
			{
				BehaviorBase session = userCacheOrCreate.GetSession(contactOpenUid);
				flag = session != null && session.smethod_0(contactNick, contactOpenUid, msgContent, type, contactSiteId);
			}
			else
			{
				flag = this.a(contactNick, contactSiteId, contactOpenUid, msgContent, type);
			}
			return flag;
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x0005DA40 File Offset: 0x0005BC40
		public bool CallContactNickByQnapiAuto(string userNick, string contactNick, string contactOpenUid, string contactSiteId, int type = 0)
		{
			UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(userNick);
			bool flag;
			if (!userCacheOrCreate.IsSessionNull)
			{
				BehaviorBase session = userCacheOrCreate.GetSession(contactOpenUid);
				flag = session != null && session.OpenChat(contactNick, contactOpenUid, contactSiteId);
			}
			else
			{
				flag = this.a(contactNick, contactSiteId, contactOpenUid, " ", type);
			}
			return flag;
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x0005DA90 File Offset: 0x0005BC90
		private bool a(string A_0, string A_1, string A_2, string A_3, int A_4 = 0)
		{
			bool flag;
			if (AppConfig.AllowAutoLogin)
			{
				for (int i = 0; i < 5; i++)
				{
					if (this.a(A_1 + A_0, A_2, A_3, A_4))
					{
						return true;
					}
					Thread.Sleep(200);
				}
				flag = false;
			}
			else
			{
				flag = this.a(A_1 + A_0, A_2, A_3, A_4);
			}
			return flag;
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x0005DAF4 File Offset: 0x0005BCF4
		private bool a(string A_0, string A_1, string A_2, string A_3, string A_4, int A_5 = 0)
		{
			bool flag;
			try
			{
				ExecuteJsResultInfo executeJsResultInfo = base.Execute(A_0, "", QnApiConsts.GetJsForSetMsg(A_1, A_2, A_3, A_4, A_5));
				flag = executeJsResultInfo != null;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x0005DB3C File Offset: 0x0005BD3C
		private bool a(string A_0, string A_1, string A_2, int A_3 = 0)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["uid"] = A_0;
			hashtable["text"] = A_2;
			hashtable["type"] = A_3;
			hashtable["securityUID"] = A_1;
			hashtable["bizDomain"] = "taobao";
			string text = "window.insertText2Inputbox(" + JSON.Encode(hashtable) + ");";
			string text2 = Resources.MyQN + text;
			bool flag;
			try
			{
				RemoteSessionsResponse remoteSessionsResponse = null;
				ExecuteJsResultInfo executeJsResultInfo = this.ExecuteJsByRecent(text2, out remoteSessionsResponse);
				if (remoteSessionsResponse == null)
				{
					flag = false;
				}
				else if (executeJsResultInfo != null)
				{
					if (string.IsNullOrEmpty(executeJsResultInfo.result.result.value))
					{
						flag = true;
					}
					else
					{
						bool flag2;
						bool.TryParse(executeJsResultInfo.result.result.value, out flag2);
						flag = flag2;
					}
				}
				else
				{
					flag = false;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x0005DC38 File Offset: 0x0005BE38
		public bool TransferContactByQnApiAuto(string userNick, string contactNick, string contactSiteId, string contactOpenUid, string targetNick, string targetSiteId)
		{
			UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(userNick);
			bool flag;
			if (!userCacheOrCreate.IsSessionNull)
			{
				BehaviorBase session = userCacheOrCreate.GetSession(contactOpenUid);
				flag = session != null && session.TransferContact(contactNick, contactSiteId, contactOpenUid, targetNick, targetSiteId);
			}
			else
			{
				flag = this.b(contactNick, contactSiteId, contactOpenUid, targetNick, targetSiteId);
			}
			return flag;
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x0005DC88 File Offset: 0x0005BE88
		private bool b(string A_0, string A_1, string A_2, string A_3, string A_4)
		{
			return this.a(A_0, A_1, A_2, A_3, A_4) || this.b("appkey=21600714&", A_0, A_1, A_3, A_4) || this.b("appkey=21600715&", A_0, A_1, A_3, A_4) || this.b("appkey=21790264&", A_0, A_1, A_3, A_4) || this.b("//aldsqn.agiso.com/", A_0, A_1, A_3, A_4) || this.b("//alds.agiso.com/", A_0, A_1, A_3, A_4);
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x0005DD28 File Offset: 0x0005BF28
		private bool a(string A_0, string A_1, string A_2, string A_3, string A_4, string A_5)
		{
			RemoteSessionsResponse rsRsp = base.GetRsRsp(A_0, null, false);
			bool flag;
			if (rsRsp == null)
			{
				flag = false;
			}
			else
			{
				try
				{
					ExecuteJsResultInfo executeJsResultInfo = base.Execute(A_0, "", QnApiConsts.GetJsForTransferContact(A_1, A_2, A_4, A_5));
					flag = executeJsResultInfo != null;
				}
				catch
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x0005DD80 File Offset: 0x0005BF80
		private bool a(string A_0, string A_1, string A_2, string A_3, string A_4)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["contactID"] = A_1 + A_0;
			hashtable["targetID"] = A_4 + A_3;
			hashtable["contactSecurityUID"] = A_2 ?? "";
			hashtable["contactBizDomain"] = "taobao";
			string text = string.Concat(new string[]
			{
				"window.transferContact(",
				JSON.Encode(hashtable),
				", function() {window.__agi_transfer_obj = {};}, function(e){window.__agi_transfer_obj = {contactID:\"",
				A_0,
				"\", targetID: \"",
				A_3,
				"\"};$.extend(window.__agi_transfer_obj, e);})"
			});
			string text2 = Resources.MyQN + text;
			bool flag;
			try
			{
				RemoteSessionsResponse remoteSessionsResponse = null;
				ExecuteJsResultInfo executeJsResultInfo = this.ExecuteJsByRecent(text2, out remoteSessionsResponse);
				if (remoteSessionsResponse == null)
				{
					flag = false;
				}
				else
				{
					flag = executeJsResultInfo != null;
				}
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x0005DE6C File Offset: 0x0005C06C
		public virtual bool ImplantedJsForWsClient(string usernick)
		{
			long timeStamp = Utils.GetTimeStamp();
			string text = Utils.SignRequest(new Dictionary<string, string>
			{
				{ "usernick", usernick },
				{
					"timestamp",
					timeStamp.ToString()
				}
			}, AppConfig.AliwwSocketSecret);
			string text2 = Resources.NewRecent.Replace("{prot}", AppConfig.SocketServerPort.ToString()).Replace("{path}", "/Aliww").Replace("{usernick}", usernick)
				.Replace("{timestamp}", timeStamp.ToString())
				.Replace("{sign}", text);
			RemoteSessionsResponse remoteSessionsResponse;
			ExecuteJsResultInfo executeJsResultInfo = this.ExecuteJsByRecent(Resources.MyQN + text2, out remoteSessionsResponse);
			return executeJsResultInfo != null;
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x0005DF30 File Offset: 0x0005C130
		public ActiveUserInfo GetCurrentTargetUserNickByQnApi(string guid)
		{
			string text = this.a(guid);
			ActiveUserInfo activeUserInfo;
			if (string.IsNullOrEmpty(text))
			{
				activeUserInfo = null;
			}
			else
			{
				activeUserInfo = JsonConvert.DeserializeObject<ActiveUserInfo>(text);
			}
			return activeUserInfo;
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x0005DF5C File Offset: 0x0005C15C
		private string a(string A_0)
		{
			string text = string.Concat(new string[] { "\r\nvar temp_", A_0, " = '';\r\nwindow.getActiveUser(\r\n    function(rsp, cmd, param) {\r\n        temp_", A_0, " = JSON.stringify(rsp)\r\n    },\r\n    function(msg, cmd, param) {\r\n        temp_", A_0, " = ''\r\n    }\r\n)" });
			string text2 = Resources.MyQN + text;
			string text3;
			try
			{
				RemoteSessionsResponse remoteSessionsResponse = null;
				this.ExecuteJsByRecent(text2, out remoteSessionsResponse);
				if (remoteSessionsResponse == null)
				{
					text3 = null;
				}
				else
				{
					text3 = base.ExecuteJsAndReturn(remoteSessionsResponse, "temp_" + A_0, 1000);
				}
			}
			catch (Exception)
			{
				text3 = null;
			}
			return text3;
		}
	}
}
