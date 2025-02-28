using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x020005F2 RID: 1522
	[SupportedBy("Chrome")]
	public class CSSProperty
	{
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x0000CD5E File Offset: 0x0000AF5E
		// (set) Token: 0x06001CC0 RID: 7360 RVA: 0x0000CD66 File Offset: 0x0000AF66
		public string Name { get; set; }

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x0000CD6F File Offset: 0x0000AF6F
		// (set) Token: 0x06001CC2 RID: 7362 RVA: 0x0000CD77 File Offset: 0x0000AF77
		public string Value { get; set; }

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06001CC3 RID: 7363 RVA: 0x0000CD80 File Offset: 0x0000AF80
		// (set) Token: 0x06001CC4 RID: 7364 RVA: 0x0000CD88 File Offset: 0x0000AF88
		public bool Important { get; set; }

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x0000CD91 File Offset: 0x0000AF91
		// (set) Token: 0x06001CC6 RID: 7366 RVA: 0x0000CD99 File Offset: 0x0000AF99
		public bool Implicit { get; set; }

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x0000CDA2 File Offset: 0x0000AFA2
		// (set) Token: 0x06001CC8 RID: 7368 RVA: 0x0000CDAA File Offset: 0x0000AFAA
		public string Text { get; set; }

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x0000CDB3 File Offset: 0x0000AFB3
		// (set) Token: 0x06001CCA RID: 7370 RVA: 0x0000CDBB File Offset: 0x0000AFBB
		public bool ParsedOk { get; set; }

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
		// (set) Token: 0x06001CCC RID: 7372 RVA: 0x0000CDCC File Offset: 0x0000AFCC
		public bool Disabled { get; set; }

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x0000CDD5 File Offset: 0x0000AFD5
		// (set) Token: 0x06001CCE RID: 7374 RVA: 0x0000CDDD File Offset: 0x0000AFDD
		public SourceRange Range { get; set; }

		// Token: 0x04000FF6 RID: 4086
		[CompilerGenerated]
		private string a;

		// Token: 0x04000FF7 RID: 4087
		[CompilerGenerated]
		private string b;

		// Token: 0x04000FF8 RID: 4088
		[CompilerGenerated]
		private bool c;

		// Token: 0x04000FF9 RID: 4089
		[CompilerGenerated]
		private bool d;

		// Token: 0x04000FFA RID: 4090
		[CompilerGenerated]
		private string e;

		// Token: 0x04000FFB RID: 4091
		[CompilerGenerated]
		private bool f;

		// Token: 0x04000FFC RID: 4092
		[CompilerGenerated]
		private bool g;

		// Token: 0x04000FFD RID: 4093
		[CompilerGenerated]
		private SourceRange h;
	}
}
