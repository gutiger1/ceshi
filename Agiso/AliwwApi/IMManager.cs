using System;
using Agiso.AliwwApi.Qn;
using Agiso.AliwwApi.Wangwang;
using Agiso.Handler;
using Agiso.Object;
using Agiso.WwWebSocket.Model;
using Agiso.WwWebSocket.Model.Enums;
using AliwwClient.Cache;

namespace Agiso.AliwwApi
{
	// Token: 0x02000703 RID: 1795
	public class IMManager
	{
		// Token: 0x0600237D RID: 9085 RVA: 0x0005D364 File Offset: 0x0005B564
		public static IIM smethod_0(QnVersionType qnver, AldsAccountInfo account)
		{
			if (!AppConfig.AgentSettings.LoginOnQn)
			{
				throw new Exception("千牛登录已被禁用");
			}
			if (qnver - 1 > 1)
			{
				throw new Exception("非法调用，无效的千牛版本");
			}
			return new AliwwQn(account);
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x0005D3AC File Offset: 0x0005B5AC
		public static IIM GetCurrentIM(AldsAccountInfo account)
		{
			AgentHostSetting agentSettings = AppConfig.AgentSettings;
			if (!agentSettings.LoginOnQn && !agentSettings.LoginOnAliwwBuyer)
			{
				throw new Exception("助手未设置登录软件");
			}
			IIM iim;
			if (agentSettings.LoginAutoSelect)
			{
				SendIMType sendIM = account.SendIM;
				SendIMType sendIMType = sendIM;
				if (sendIMType != 1)
				{
					if (sendIMType != 2)
					{
						if (AppConfig.UserNickLoginOnQnList.Contains(account.UserNick))
						{
							iim = IMManager.smethod_0(agentSettings.QnVersion, account);
						}
						else if (!account.LongOpen || account.DisableTransfer)
						{
							iim = new AliwwBuyer9(account);
						}
						else
						{
							iim = IMManager.smethod_0(agentSettings.QnVersion, account);
						}
					}
					else
					{
						iim = new AliwwBuyer9(account);
					}
				}
				else
				{
					iim = IMManager.smethod_0(agentSettings.QnVersion, account);
				}
			}
			else if (agentSettings.LoginOnQn)
			{
				iim = IMManager.smethod_0(agentSettings.QnVersion, account);
			}
			else
			{
				iim = new AliwwBuyer9(account);
			}
			return iim;
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x0005D488 File Offset: 0x0005B688
		public static IIM GetCurrentIMByLastPid(AldsAccountInfo account)
		{
			UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(account.UserNick);
			IIM iim;
			if (Win32Extend.CheckProcessIdIsRuing(userCacheOrCreate.LastSendProcessId))
			{
				MsgSendSoftware lastSendSoftware = userCacheOrCreate.LastSendSoftware;
				MsgSendSoftware msgSendSoftware = lastSendSoftware;
				if (msgSendSoftware - MsgSendSoftware.QN > 1)
				{
					if (msgSendSoftware != MsgSendSoftware.AliwwBuyer9)
					{
						throw new Exception("无效版本号");
					}
					iim = new AliwwBuyer9(account);
				}
				else
				{
					iim = new AliwwQn(account);
				}
			}
			else
			{
				if (userCacheOrCreate.LastSendProcessId > 0)
				{
					userCacheOrCreate.LastSendProcessId = 0;
					userCacheOrCreate.LastSendSoftware = MsgSendSoftware.Undefined;
				}
				iim = IMManager.GetCurrentIM(account);
			}
			return iim;
		}
	}
}
