using System;
using Agiso;
using Agiso.DbManager;
using Agiso.Object;
using Agiso.Utils;
using AliwwClient.Cache;
using AliwwClient.Manager;
using WebSocketSharp;

namespace AliwwClient.WebSocketServer
{
	// Token: 0x0200006B RID: 107
	public class AldsBehavior : BehaviorBase
	{
		// Token: 0x0600031C RID: 796 RVA: 0x00034F5C File Offset: 0x0003315C
		protected override void Beat()
		{
			UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(base.AccountUserNick);
			userCacheOrCreate.LastAldsBeatTime = new DateTime?(DateTime.Now);
			if (userCacheOrCreate.IsAldsSessionNull)
			{
				userCacheOrCreate.AldsSession = this;
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00034F94 File Offset: 0x00033194
		protected override void OnOpen()
		{
			if (!string.IsNullOrEmpty(base.UserNick))
			{
				base.AccountUserNick = base.UserNick;
				UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(base.AccountUserNick);
				userCacheOrCreate.AldsSession = this;
				userCacheOrCreate.LastAldsBeatTime = new DateTime?(DateTime.Now);
				if (!AppConfig.AllowAutoLogin)
				{
					LogWriter.WriteLog(base.UserNick + "，Alds Session opened!", 1);
				}
				else if (base.UserId > 0L)
				{
					AliwwUserInfoManager.InserOrIgnore(base.AccountUserNick, base.UserId);
				}
				AldsAccountInfo aldsAccountInfo;
				if (AppConfig.UserDict.TryGetValue(base.AccountUserNick, out aldsAccountInfo))
				{
					aldsAccountInfo.QnConnectionStatus = "√";
					base.SendLoginState(aldsAccountInfo.IsValid);
				}
				QnUserDbManager.DisableUseNativeMessageList(new long?(base.UserId));
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00035068 File Offset: 0x00033268
		protected override void OnClose(CloseEventArgs e)
		{
			if (!string.IsNullOrEmpty(base.UserNick))
			{
				UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(base.AccountUserNick);
				if (!userCacheOrCreate.IsAldsSessionNull && userCacheOrCreate.AldsSession.ID == base.ID)
				{
					userCacheOrCreate.ClearAldsSession();
				}
				if (!AppConfig.AllowAutoLogin)
				{
					LogWriter.WriteLog(base.UserNick + "，Alds Session closed！原因：" + JSON.Encode(e), 1);
				}
				AldsAccountInfo aldsAccountInfo;
				if (AppConfig.UserDict.TryGetValue(base.AccountUserNick, out aldsAccountInfo))
				{
					aldsAccountInfo.QnConnectionStatus = (userCacheOrCreate.IsSessionNull ? "-" : "√");
				}
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00035114 File Offset: 0x00033314
		protected override void OnError(ErrorEventArgs e)
		{
			if (!string.IsNullOrEmpty(base.UserNick))
			{
				string text = base.UserNick + "，Alds Session exception";
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
				if (!userCacheOrCreate.IsAldsSessionNull && userCacheOrCreate.AldsSession.ID == base.ID)
				{
					userCacheOrCreate.ClearAldsSession();
				}
				AldsAccountInfo aldsAccountInfo;
				if (AppConfig.UserDict.TryGetValue(base.AccountUserNick, out aldsAccountInfo))
				{
					aldsAccountInfo.QnConnectionStatus = (userCacheOrCreate.IsSessionNull ? "-" : "√");
				}
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000351F8 File Offset: 0x000333F8
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
				if (!userCacheOrCreate.IsAldsSessionNull && userCacheOrCreate.AldsSession.ID == base.ID)
				{
					userCacheOrCreate.ClearAldsSession();
				}
				LogWriter.WriteLog(ex.ToString(), 1);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000031DC File Offset: 0x000013DC
		public void ChangeLoginState(bool isLogin)
		{
			base.SendLoginState(isLogin);
		}

		// Token: 0x040002EA RID: 746
		public const string Path = "/Alds";
	}
}
