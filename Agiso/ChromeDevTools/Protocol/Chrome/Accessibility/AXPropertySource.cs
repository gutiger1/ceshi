using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Accessibility
{
	// Token: 0x02000673 RID: 1651
	[SupportedBy("Chrome")]
	public class AXPropertySource
	{
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x0000DE3C File Offset: 0x0000C03C
		// (set) Token: 0x06001F3A RID: 7994 RVA: 0x0000DE44 File Offset: 0x0000C044
		public string Name { get; set; }

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06001F3B RID: 7995 RVA: 0x0000DE4D File Offset: 0x0000C04D
		// (set) Token: 0x06001F3C RID: 7996 RVA: 0x0000DE55 File Offset: 0x0000C055
		public GEnum0 SourceType { get; set; }

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x0000DE5E File Offset: 0x0000C05E
		// (set) Token: 0x06001F3E RID: 7998 RVA: 0x0000DE66 File Offset: 0x0000C066
		public object Value { get; set; }

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06001F3F RID: 7999 RVA: 0x0000DE6F File Offset: 0x0000C06F
		// (set) Token: 0x06001F40 RID: 8000 RVA: 0x0000DE77 File Offset: 0x0000C077
		public AXValueType Type { get; set; }

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x0000DE80 File Offset: 0x0000C080
		// (set) Token: 0x06001F42 RID: 8002 RVA: 0x0000DE88 File Offset: 0x0000C088
		public bool Invalid { get; set; }

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06001F43 RID: 8003 RVA: 0x0000DE91 File Offset: 0x0000C091
		// (set) Token: 0x06001F44 RID: 8004 RVA: 0x0000DE99 File Offset: 0x0000C099
		public string InvalidReason { get; set; }

		// Token: 0x04001104 RID: 4356
		[CompilerGenerated]
		private string a;

		// Token: 0x04001105 RID: 4357
		[CompilerGenerated]
		private GEnum0 b;

		// Token: 0x04001106 RID: 4358
		[CompilerGenerated]
		private object c;

		// Token: 0x04001107 RID: 4359
		[CompilerGenerated]
		private AXValueType d;

		// Token: 0x04001108 RID: 4360
		[CompilerGenerated]
		private bool e;

		// Token: 0x04001109 RID: 4361
		[CompilerGenerated]
		private string f;
	}
}
