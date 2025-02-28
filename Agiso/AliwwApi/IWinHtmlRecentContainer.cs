using System;
using Agiso.AliwwApi.Object;

namespace Agiso.AliwwApi
{
	// Token: 0x02000705 RID: 1797
	public interface IWinHtmlRecentContainer : IWinHtmlContainer
	{
		// Token: 0x0600238A RID: 9098
		AliwwMsgElement GetLastReceiveMessage(string userNick);

		// Token: 0x0600238B RID: 9099
		string GetCurrentTargetUserNickByContent(string userNick);
	}
}
