using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace AliwwClient.Object
{
	// Token: 0x0200009C RID: 156
	public class UpgradeInfo
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00003765 File Offset: 0x00001965
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0000376D File Offset: 0x0000196D
		[JsonProperty("AliwwClient")]
		public UpgradeItem AliwwClient { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00003776 File Offset: 0x00001976
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x0000377E File Offset: 0x0000197E
		[JsonProperty("Updater")]
		public UpgradeItem Updater { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00003787 File Offset: 0x00001987
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0000378F File Offset: 0x0000198F
		[JsonProperty("AliwwClientAgent")]
		public UpgradeItem AliwwClientAgent { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00003798 File Offset: 0x00001998
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x000037A0 File Offset: 0x000019A0
		[JsonProperty("UpgradeOtherPrograms")]
		public List<UpgradeItem> UpgradeOtherPrograms { get; set; }

		// Token: 0x0400037D RID: 893
		[CompilerGenerated]
		private UpgradeItem a;

		// Token: 0x0400037E RID: 894
		[CompilerGenerated]
		private UpgradeItem b;

		// Token: 0x0400037F RID: 895
		[CompilerGenerated]
		private UpgradeItem c;

		// Token: 0x04000380 RID: 896
		[CompilerGenerated]
		private List<UpgradeItem> d;
	}
}
