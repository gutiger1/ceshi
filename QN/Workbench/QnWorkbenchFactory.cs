using System;

namespace AliwwClient.QN.Workbench
{
	// Token: 0x02000098 RID: 152
	public class QnWorkbenchFactory
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x0003CF5C File Offset: 0x0003B15C
		public static IQnWorkbench CreateQnWorkbench(string userNick, string version)
		{
			IQnWorkbench qnWorkbench = new QnWorkbenchCommon(userNick);
			qnWorkbench.Start();
			return qnWorkbench;
		}
	}
}
