using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x020005FA RID: 1530
	[SupportedBy("Chrome")]
	[Command("CSS.forcePseudoState")]
	public class ForcePseudoStateCommand
	{
		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x0000CF5C File Offset: 0x0000B15C
		// (set) Token: 0x06001D04 RID: 7428 RVA: 0x0000CF64 File Offset: 0x0000B164
		public long NodeId { get; set; }

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x0000CF6D File Offset: 0x0000B16D
		// (set) Token: 0x06001D06 RID: 7430 RVA: 0x0000CF75 File Offset: 0x0000B175
		public string[] ForcedPseudoClasses { get; set; }

		// Token: 0x04001014 RID: 4116
		[CompilerGenerated]
		private long a;

		// Token: 0x04001015 RID: 4117
		[CompilerGenerated]
		private string[] b;
	}
}
