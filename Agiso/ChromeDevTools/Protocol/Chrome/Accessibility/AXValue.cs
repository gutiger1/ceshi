using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Accessibility
{
	// Token: 0x02000677 RID: 1655
	[SupportedBy("Chrome")]
	public class AXValue
	{
		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
		// (set) Token: 0x06001F4C RID: 8012 RVA: 0x0000DECC File Offset: 0x0000C0CC
		public AXValueType Type { get; set; }

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x0000DED5 File Offset: 0x0000C0D5
		// (set) Token: 0x06001F4E RID: 8014 RVA: 0x0000DEDD File Offset: 0x0000C0DD
		public object Value { get; set; }

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x0000DEE6 File Offset: 0x0000C0E6
		// (set) Token: 0x06001F50 RID: 8016 RVA: 0x0000DEEE File Offset: 0x0000C0EE
		public AXRelatedNode RelatedNodeValue { get; set; }

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x0000DEF7 File Offset: 0x0000C0F7
		// (set) Token: 0x06001F52 RID: 8018 RVA: 0x0000DEFF File Offset: 0x0000C0FF
		public AXRelatedNode[] RelatedNodeArrayValue { get; set; }

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06001F53 RID: 8019 RVA: 0x0000DF08 File Offset: 0x0000C108
		// (set) Token: 0x06001F54 RID: 8020 RVA: 0x0000DF10 File Offset: 0x0000C110
		public AXPropertySource[] Sources { get; set; }

		// Token: 0x04001117 RID: 4375
		[CompilerGenerated]
		private AXValueType a;

		// Token: 0x04001118 RID: 4376
		[CompilerGenerated]
		private object b;

		// Token: 0x04001119 RID: 4377
		[CompilerGenerated]
		private AXRelatedNode c;

		// Token: 0x0400111A RID: 4378
		[CompilerGenerated]
		private AXRelatedNode[] d;

		// Token: 0x0400111B RID: 4379
		[CompilerGenerated]
		private AXPropertySource[] e;
	}
}
