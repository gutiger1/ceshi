using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace AliwwClient.Object
{
	// Token: 0x0200009E RID: 158
	public class UpgradeItem
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x000037CB File Offset: 0x000019CB
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x000037D3 File Offset: 0x000019D3
		[JsonProperty("Ver")]
		[Obsolete("6.03.01之后弃用，不再维护3.5版本的")]
		public string Ver { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x000037DC File Offset: 0x000019DC
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x000037E4 File Offset: 0x000019E4
		[JsonProperty("Version")]
		public string Version { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x000037ED File Offset: 0x000019ED
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x000037F5 File Offset: 0x000019F5
		[JsonProperty("ProgramName")]
		public string ProgramName { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x000037FE File Offset: 0x000019FE
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x00003806 File Offset: 0x00001A06
		[JsonProperty("UpgradeFile")]
		public UpgradeFile UpgradeFile { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000380F File Offset: 0x00001A0F
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x00003817 File Offset: 0x00001A17
		[JsonProperty("UpgradeDate")]
		public DateTime UpgradeDate { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00003820 File Offset: 0x00001A20
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00003828 File Offset: 0x00001A28
		[JsonProperty("UpgradeFolderPath")]
		public string UpgradeFolderPath { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00003831 File Offset: 0x00001A31
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x00003839 File Offset: 0x00001A39
		[JsonProperty("IsNeedToRestart")]
		public bool IsNeedToRestart { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00003842 File Offset: 0x00001A42
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x0000384A File Offset: 0x00001A4A
		[JsonProperty("DisableKill")]
		public bool DisableKill { get; set; }

		// Token: 0x04000383 RID: 899
		[CompilerGenerated]
		private string a;

		// Token: 0x04000384 RID: 900
		[CompilerGenerated]
		private string b;

		// Token: 0x04000385 RID: 901
		[CompilerGenerated]
		private string c;

		// Token: 0x04000386 RID: 902
		[CompilerGenerated]
		private UpgradeFile d;

		// Token: 0x04000387 RID: 903
		[CompilerGenerated]
		private DateTime e;

		// Token: 0x04000388 RID: 904
		[CompilerGenerated]
		private string f;

		// Token: 0x04000389 RID: 905
		[CompilerGenerated]
		private bool g;

		// Token: 0x0400038A RID: 906
		[CompilerGenerated]
		private bool h;
	}
}
