using System;
using Agiso;
using Agiso.AliwwApi.Qn;
using Agiso.DbManager;
using Agiso.Object;
using Agiso.Utils;
using AliwwClient.Cache;
using AliwwClient.Manager;
using WebSocketSharp;

namespace AliwwClient.WebSocketServer
{
	// Token: 0x02000074 RID: 116
	public class RecentBehavior : BehaviorBase
	{
		// Token: 0x0600035C RID: 860 RVA: 0x00037590 File Offset: 0x00035790
		protected override void Beat()
		{
			UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(base.AccountUserNick);
			userCacheOrCreate.LastRecentBeatTime = new DateTime?(DateTime.Now);
			if (userCacheOrCreate.IsRecentSessionNull)
			{
				userCacheOrCreate.RecentSession = this;
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000375C8 File Offset: 0x000357C8
		protected override void OnOpen()
		{
			if (!string.IsNullOrEmpty(base.UserNick))
			{
				base.AccountUserNick = base.UserNick;
				if (AppConfig.IsSellerLoginOnOwnComputer && !AppConfig.UserListExitUserNickIgnoreTraditional(base.UserNick))
				{
					int aliWorkbenchProcessId = new UserAdaptiveManager(this).GetAliWorkbenchProcessId();
					foreach (AldsAccountInfo aldsAccountInfo in AppConfig.UserList)
					{
						AliwwTalkWindowQn aliwwTalkWindowQn = AliwwTalkWindowQn.Get(aldsAccountInfo.UserNick);
						if (aliwwTalkWindowQn != null && aliwwTalkWindowQn.ProcessId == aliWorkbenchProcessId)
						{
							base.AccountUserNick = aldsAccountInfo.UserNick;
							break;
						}
					}
				}
				UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(base.AccountUserNick);
				userCacheOrCreate.RecentSession = this;
				userCacheOrCreate.LastRecentBeatTime = new DateTime?(DateTime.Now);
				if (base.UserNick != base.AccountUserNick)
				{
					AppConfig.AddOrUpdateUserCache(base.UserNick, userCacheOrCreate);
				}
				if (!AppConfig.AllowAutoLogin)
				{
					LogWriter.WriteLog(base.UserNick + "，Recent Session opened!", 1);
				}
				else if (base.UserId > 0L)
				{
					AliwwUserInfoManager.InserOrIgnore(base.AccountUserNick, base.UserId);
				}
				AldsAccountInfo aldsAccountInfo2;
				if (AppConfig.UserDict.TryGetValue(base.AccountUserNick, out aldsAccountInfo2))
				{
					aldsAccountInfo2.QnConnectionStatus = "√";
				}
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00037734 File Offset: 0x00035934
		protected override void OnClose(CloseEventArgs e)
		{
			if (!string.IsNullOrEmpty(base.UserNick))
			{
				UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(base.AccountUserNick);
				if (!userCacheOrCreate.IsRecentSessionNull && userCacheOrCreate.RecentSession.ID == base.ID)
				{
					userCacheOrCreate.ClearRecentSession();
				}
				if (!AppConfig.AllowAutoLogin)
				{
					LogWriter.WriteLog(base.UserNick + "，Recent Session closed！原因：" + JSON.Encode(e), 1);
				}
				AldsAccountInfo aldsAccountInfo;
				if (AppConfig.UserDict.TryGetValue(base.AccountUserNick, out aldsAccountInfo))
				{
					aldsAccountInfo.QnConnectionStatus = (userCacheOrCreate.IsSessionNull ? "-" : "√");
				}
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000377E0 File Offset: 0x000359E0
		protected override void OnError(ErrorEventArgs e)
		{
			if (!string.IsNullOrEmpty(base.UserNick))
			{
				string text = base.UserNick + "，Recent Session exception";
				if (e != null)
				{
					text = text + "，" + e.Message;
					if (e.Exception != null)
					{
						text = text + "，" + e.Exception.ToString();
					}
				}
				if (!AppConfig.AllowAutoLogin)
				{
					LogWriter.WriteLog(text, 1);
				}
				UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(base.AccountUserNick);
				if (!userCacheOrCreate.IsRecentSessionNull && userCacheOrCreate.RecentSession.ID == base.ID)
				{
					userCacheOrCreate.ClearRecentSession();
				}
				AldsAccountInfo aldsAccountInfo;
				if (AppConfig.UserDict.TryGetValue(base.AccountUserNick, out aldsAccountInfo))
				{
					aldsAccountInfo.QnConnectionStatus = (userCacheOrCreate.IsSessionNull ? "-" : "√");
				}
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000378C4 File Offset: 0x00035AC4
		public override bool SendTo(string data)
		{
			bool flag;
			try
			{
				base.Send(data);
				flag = true;
			}
			catch (Exception ex)
			{
				UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(base.AccountUserNick);
				if (!userCacheOrCreate.IsRecentSessionNull && userCacheOrCreate.RecentSession.ID == base.ID)
				{
					userCacheOrCreate.ClearRecentSession();
				}
				LogWriter.WriteLog("发送消息异常：" + ex.ToString(), 1);
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000309 RID: 777
		public const string Path = "/Aliww";
	}
}
