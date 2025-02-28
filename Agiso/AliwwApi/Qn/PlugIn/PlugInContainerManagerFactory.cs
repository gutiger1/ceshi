using System;
using Agiso.AliwwApi.Object;

namespace Agiso.AliwwApi.Qn.PlugIn
{
	// Token: 0x02000763 RID: 1891
	public static class PlugInContainerManagerFactory
	{
		// Token: 0x06002608 RID: 9736 RVA: 0x0006F51C File Offset: 0x0006D71C
		public static IPlugInContainerManager Get(AliwwTalkWindowQn aliwwTalkWin, string userNick)
		{
			AliwwVersion aliwwVersion = aliwwTalkWin.AliwwVersion;
			AliwwVersion aliwwVersion2 = aliwwVersion;
			IPlugInContainerManager plugInContainerManager;
			if (aliwwVersion2 != AliwwVersion.QianNiu5)
			{
				if (aliwwVersion2 != AliwwVersion.QianNiu9)
				{
					throw new Exception("未知的千牛类型");
				}
				plugInContainerManager = new PlugInContainerManager91500(aliwwTalkWin, userNick);
			}
			else
			{
				plugInContainerManager = new PlugInContainerManager(aliwwTalkWin, userNick);
			}
			return plugInContainerManager;
		}
	}
}
