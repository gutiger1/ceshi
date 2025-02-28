using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x02000592 RID: 1426
	[Command("Debugger.enablePromiseTracker")]
	[SupportedBy("Chrome")]
	public class EnablePromiseTrackerCommand
	{
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x0000C28B File Offset: 0x0000A48B
		// (set) Token: 0x06001B1A RID: 6938 RVA: 0x0000C293 File Offset: 0x0000A493
		public bool CaptureStacks { get; set; }

		// Token: 0x04000F53 RID: 3923
		[CompilerGenerated]
		private bool a;
	}
}
