using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x0200060A RID: 1546
	[SupportedBy("Chrome")]
	public class MediaQueryExpression
	{
		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x0000D0E3 File Offset: 0x0000B2E3
		// (set) Token: 0x06001D42 RID: 7490 RVA: 0x0000D0EB File Offset: 0x0000B2EB
		public double Value { get; set; }

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		// (set) Token: 0x06001D44 RID: 7492 RVA: 0x0000D0FC File Offset: 0x0000B2FC
		public string Unit { get; set; }

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x0000D105 File Offset: 0x0000B305
		// (set) Token: 0x06001D46 RID: 7494 RVA: 0x0000D10D File Offset: 0x0000B30D
		public string Feature { get; set; }

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x0000D116 File Offset: 0x0000B316
		// (set) Token: 0x06001D48 RID: 7496 RVA: 0x0000D11E File Offset: 0x0000B31E
		public SourceRange ValueRange { get; set; }

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x0000D127 File Offset: 0x0000B327
		// (set) Token: 0x06001D4A RID: 7498 RVA: 0x0000D12F File Offset: 0x0000B32F
		public double ComputedLength { get; set; }

		// Token: 0x0400102B RID: 4139
		[CompilerGenerated]
		private double a;

		// Token: 0x0400102C RID: 4140
		[CompilerGenerated]
		private string b;

		// Token: 0x0400102D RID: 4141
		[CompilerGenerated]
		private string c;

		// Token: 0x0400102E RID: 4142
		[CompilerGenerated]
		private SourceRange d;

		// Token: 0x0400102F RID: 4143
		[CompilerGenerated]
		private double e;
	}
}
