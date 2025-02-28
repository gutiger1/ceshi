using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x02000641 RID: 1601
	[SupportedBy("Chrome")]
	public class ResourceStateDescriptor
	{
		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x0000D7FE File Offset: 0x0000B9FE
		// (set) Token: 0x06001E4E RID: 7758 RVA: 0x0000D806 File Offset: 0x0000BA06
		public string Name { get; set; }

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06001E4F RID: 7759 RVA: 0x0000D80F File Offset: 0x0000BA0F
		// (set) Token: 0x06001E50 RID: 7760 RVA: 0x0000D817 File Offset: 0x0000BA17
		public string EnumValueForName { get; set; }

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x0000D820 File Offset: 0x0000BA20
		// (set) Token: 0x06001E52 RID: 7762 RVA: 0x0000D828 File Offset: 0x0000BA28
		public CallArgument Value { get; set; }

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x0000D831 File Offset: 0x0000BA31
		// (set) Token: 0x06001E54 RID: 7764 RVA: 0x0000D839 File Offset: 0x0000BA39
		public ResourceStateDescriptor[] Values { get; set; }

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06001E55 RID: 7765 RVA: 0x0000D842 File Offset: 0x0000BA42
		// (set) Token: 0x06001E56 RID: 7766 RVA: 0x0000D84A File Offset: 0x0000BA4A
		public bool IsArray { get; set; }

		// Token: 0x0400109B RID: 4251
		[CompilerGenerated]
		private string a;

		// Token: 0x0400109C RID: 4252
		[CompilerGenerated]
		private string b;

		// Token: 0x0400109D RID: 4253
		[CompilerGenerated]
		private CallArgument c;

		// Token: 0x0400109E RID: 4254
		[CompilerGenerated]
		private ResourceStateDescriptor[] d;

		// Token: 0x0400109F RID: 4255
		[CompilerGenerated]
		private bool e;
	}
}
