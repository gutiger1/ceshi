using System;
using System.Collections.Generic;

namespace AliwwClient.QN.Workbench
{
	// Token: 0x02000095 RID: 149
	public interface IQnWorkbench
	{
		// Token: 0x06000409 RID: 1033
		string GetLastError();

		// Token: 0x0600040A RID: 1034
		bool Start();

		// Token: 0x0600040B RID: 1035
		bool IsImplantedJsWork();

		// Token: 0x0600040C RID: 1036
		List<RecvMsgResponse> GetMsg();

		// Token: 0x0600040D RID: 1037
		bool ImplantedJs();
	}
}
