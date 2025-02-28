using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Accessibility
{
	// Token: 0x02000671 RID: 1649
	[SupportedBy("Chrome")]
	public class AXNode
	{
		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x0000DDA3 File Offset: 0x0000BFA3
		// (set) Token: 0x06001F26 RID: 7974 RVA: 0x0000DDAB File Offset: 0x0000BFAB
		public string NodeId { get; set; }

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06001F27 RID: 7975 RVA: 0x0000DDB4 File Offset: 0x0000BFB4
		// (set) Token: 0x06001F28 RID: 7976 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		public AXValue Role { get; set; }

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x0000DDC5 File Offset: 0x0000BFC5
		// (set) Token: 0x06001F2A RID: 7978 RVA: 0x0000DDCD File Offset: 0x0000BFCD
		public AXValue Name { get; set; }

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x0000DDD6 File Offset: 0x0000BFD6
		// (set) Token: 0x06001F2C RID: 7980 RVA: 0x0000DDDE File Offset: 0x0000BFDE
		public AXValue Description { get; set; }

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x0000DDE7 File Offset: 0x0000BFE7
		// (set) Token: 0x06001F2E RID: 7982 RVA: 0x0000DDEF File Offset: 0x0000BFEF
		public AXValue Value { get; set; }

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x0000DDF8 File Offset: 0x0000BFF8
		// (set) Token: 0x06001F30 RID: 7984 RVA: 0x0000DE00 File Offset: 0x0000C000
		public AXValue Help { get; set; }

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x0000DE09 File Offset: 0x0000C009
		// (set) Token: 0x06001F32 RID: 7986 RVA: 0x0000DE11 File Offset: 0x0000C011
		public AXProperty[] Properties { get; set; }

		// Token: 0x040010FB RID: 4347
		[CompilerGenerated]
		private string a;

		// Token: 0x040010FC RID: 4348
		[CompilerGenerated]
		private AXValue b;

		// Token: 0x040010FD RID: 4349
		[CompilerGenerated]
		private AXValue c;

		// Token: 0x040010FE RID: 4350
		[CompilerGenerated]
		private AXValue d;

		// Token: 0x040010FF RID: 4351
		[CompilerGenerated]
		private AXValue e;

		// Token: 0x04001100 RID: 4352
		[CompilerGenerated]
		private AXValue f;

		// Token: 0x04001101 RID: 4353
		[CompilerGenerated]
		private AXProperty[] g;
	}
}
