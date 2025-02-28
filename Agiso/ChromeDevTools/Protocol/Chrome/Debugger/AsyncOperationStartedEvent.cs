using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x02000582 RID: 1410
	[SupportedBy("Chrome")]
	[Event("Debugger.asyncOperationStarted")]
	public class AsyncOperationStartedEvent
	{
		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001ADF RID: 6879 RVA: 0x0000C126 File Offset: 0x0000A326
		// (set) Token: 0x06001AE0 RID: 6880 RVA: 0x0000C12E File Offset: 0x0000A32E
		public AsyncOperation Operation { get; set; }

		// Token: 0x04000F3E RID: 3902
		[CompilerGenerated]
		private AsyncOperation a;
	}
}
