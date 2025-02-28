using System;
using System.Threading;
using Agiso.Handler;
using Agiso.Object;
using AliwwClient.Cache;

namespace Agiso.AliwwApi.Qn.PlugIn
{
	// Token: 0x02000762 RID: 1890
	public class PlugInContainerManager91500 : IPlugInContainerManager
	{
		// Token: 0x06002606 RID: 9734 RVA: 0x0000F490 File Offset: 0x0000D690
		public PlugInContainerManager91500(AliwwTalkWindowQn aliwwTalkWin, string userNick)
		{
			this.a = aliwwTalkWin;
			this.b = userNick;
			this.c = AppConfig.GetUserCacheOrCreate(this.b);
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x0006F4C4 File Offset: 0x0006D6C4
		public ErrCodeInfo ClickAldsPlugIn()
		{
			AliwwTalkWindow aliwwTalkWindow = AliwwTalkWindow.ParseFromWindowInfo(this.a);
			for (int i = 0; i < 4; i++)
			{
				aliwwTalkWindow.FocusSendWin();
				SendKeysExtend.SendWait("^s");
				Thread.Sleep(200);
				if (!this.c.IsAldsSessionNull)
				{
					break;
				}
			}
			return null;
		}

		// Token: 0x04001F15 RID: 7957
		private AliwwTalkWindowQn a;

		// Token: 0x04001F16 RID: 7958
		private string b;

		// Token: 0x04001F17 RID: 7959
		private UserCache c;
	}
}
