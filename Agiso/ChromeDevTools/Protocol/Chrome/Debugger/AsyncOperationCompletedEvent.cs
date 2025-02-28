using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x02000581 RID: 1409
	[SupportedBy("Chrome")]
	[Event("Debugger.asyncOperationCompleted")]
	public class AsyncOperationCompletedEvent
	{
		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x0000C115 File Offset: 0x0000A315
		// (set) Token: 0x06001ADD RID: 6877 RVA: 0x0000C11D File Offset: 0x0000A31D
		public long Id { get; set; }

		// Token: 0x04000F3D RID: 3901
		[CompilerGenerated]
		private long a;
	}
}
