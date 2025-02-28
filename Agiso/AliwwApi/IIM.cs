using System;
using Agiso.Object;

namespace Agiso.AliwwApi
{
	// Token: 0x02000702 RID: 1794
	public interface IIM
	{
		// Token: 0x06002377 RID: 9079
		ErrCodeInfo SendMsg(string toUserNick, string toOpenUid, string msgBody, string siteId = "cntaobao");

		// Token: 0x06002378 RID: 9080
		void KillProcess();

		// Token: 0x06002379 RID: 9081
		void CloseCurrentChat();

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x0600237A RID: 9082
		// (set) Token: 0x0600237B RID: 9083
		AliwwOptionQn5 Option { get; set; }

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x0600237C RID: 9084
		MsgSendSoftware SendSoftware { get; }
	}
}
