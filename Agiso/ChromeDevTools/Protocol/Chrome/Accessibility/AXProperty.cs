using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Accessibility
{
	// Token: 0x02000672 RID: 1650
	[SupportedBy("Chrome")]
	public class AXProperty
	{
		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x0000DE1A File Offset: 0x0000C01A
		// (set) Token: 0x06001F35 RID: 7989 RVA: 0x0000DE22 File Offset: 0x0000C022
		public string Name { get; set; }

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x0000DE2B File Offset: 0x0000C02B
		// (set) Token: 0x06001F37 RID: 7991 RVA: 0x0000DE33 File Offset: 0x0000C033
		public AXValue Value { get; set; }

		// Token: 0x04001102 RID: 4354
		[CompilerGenerated]
		private string a;

		// Token: 0x04001103 RID: 4355
		[CompilerGenerated]
		private AXValue b;
	}
}
