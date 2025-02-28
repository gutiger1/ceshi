using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x02000599 RID: 1433
	[SupportedBy("Chrome")]
	public class FunctionDetails
	{
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x0000C3AC File Offset: 0x0000A5AC
		// (set) Token: 0x06001B43 RID: 6979 RVA: 0x0000C3B4 File Offset: 0x0000A5B4
		public Location Location { get; set; }

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x0000C3BD File Offset: 0x0000A5BD
		// (set) Token: 0x06001B45 RID: 6981 RVA: 0x0000C3C5 File Offset: 0x0000A5C5
		public string FunctionName { get; set; }

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x0000C3CE File Offset: 0x0000A5CE
		// (set) Token: 0x06001B47 RID: 6983 RVA: 0x0000C3D6 File Offset: 0x0000A5D6
		public bool IsGenerator { get; set; }

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x0000C3DF File Offset: 0x0000A5DF
		// (set) Token: 0x06001B49 RID: 6985 RVA: 0x0000C3E7 File Offset: 0x0000A5E7
		public Scope[] ScopeChain { get; set; }

		// Token: 0x04000F64 RID: 3940
		[CompilerGenerated]
		private Location a;

		// Token: 0x04000F65 RID: 3941
		[CompilerGenerated]
		private string b;

		// Token: 0x04000F66 RID: 3942
		[CompilerGenerated]
		private bool c;

		// Token: 0x04000F67 RID: 3943
		[CompilerGenerated]
		private Scope[] d;
	}
}
