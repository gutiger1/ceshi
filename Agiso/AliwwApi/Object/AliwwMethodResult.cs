using System;
using System.Runtime.CompilerServices;
using Agiso.Object;

namespace Agiso.AliwwApi.Object
{
	// Token: 0x02000728 RID: 1832
	public class AliwwMethodResult
	{
		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x0000EDF4 File Offset: 0x0000CFF4
		// (set) Token: 0x06002451 RID: 9297 RVA: 0x0000EDFC File Offset: 0x0000CFFC
		public string UserNick { get; set; }

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x0000EE05 File Offset: 0x0000D005
		// (set) Token: 0x06002453 RID: 9299 RVA: 0x0000EE0D File Offset: 0x0000D00D
		public string BuyerNick { get; set; }

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06002454 RID: 9300 RVA: 0x0000EE16 File Offset: 0x0000D016
		// (set) Token: 0x06002455 RID: 9301 RVA: 0x0000EE1E File Offset: 0x0000D01E
		public DateTime CurrentTime { get; set; }

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002456 RID: 9302 RVA: 0x0000EE27 File Offset: 0x0000D027
		// (set) Token: 0x06002457 RID: 9303 RVA: 0x0000EE2F File Offset: 0x0000D02F
		public ErrCodeInfo ResultCode { get; set; }

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002458 RID: 9304 RVA: 0x0000EE38 File Offset: 0x0000D038
		// (set) Token: 0x06002459 RID: 9305 RVA: 0x0000EE40 File Offset: 0x0000D040
		public string ResultMsg { get; set; }

		// Token: 0x04001E1D RID: 7709
		[CompilerGenerated]
		private string a;

		// Token: 0x04001E1E RID: 7710
		[CompilerGenerated]
		private string b;

		// Token: 0x04001E1F RID: 7711
		[CompilerGenerated]
		private DateTime c;

		// Token: 0x04001E20 RID: 7712
		[CompilerGenerated]
		private ErrCodeInfo d;

		// Token: 0x04001E21 RID: 7713
		[CompilerGenerated]
		private string e;
	}
}
