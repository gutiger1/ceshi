using System;
using System.Runtime.CompilerServices;
using Agiso.Utils;

namespace AliwwClient.Server
{
	// Token: 0x02000093 RID: 147
	public class AjaxResult
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x000035E9 File Offset: 0x000017E9
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x000035F1 File Offset: 0x000017F1
		public bool IsSuccess { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x000035FA File Offset: 0x000017FA
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00003602 File Offset: 0x00001802
		public string AlertMsg { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000360B File Offset: 0x0000180B
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x00003613 File Offset: 0x00001813
		public string PromptMsg { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000361C File Offset: 0x0000181C
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00003624 File Offset: 0x00001824
		public object ExtendObj { get; set; }

		// Token: 0x06000402 RID: 1026 RVA: 0x0003CA3C File Offset: 0x0003AC3C
		public void SetTotal(int pageSize)
		{
			if (this.records > 0L && pageSize > 0)
			{
				this.total = Math.Ceiling(this.records / pageSize);
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0003CA84 File Offset: 0x0003AC84
		public AjaxResult CreateErrArro(string alertMsg)
		{
			this.IsSuccess = false;
			this.AlertMsg = alertMsg;
			return this;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0003CAA4 File Offset: 0x0003ACA4
		public AjaxResult CreateSuccArro(string promptMsg)
		{
			this.IsSuccess = true;
			this.PromptMsg = promptMsg;
			return this;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0003CAC4 File Offset: 0x0003ACC4
		public string Encode()
		{
			return JSON.Encode(this);
		}

		// Token: 0x04000361 RID: 865
		[CompilerGenerated]
		private bool a;

		// Token: 0x04000362 RID: 866
		[CompilerGenerated]
		private string b;

		// Token: 0x04000363 RID: 867
		[CompilerGenerated]
		private string c;

		// Token: 0x04000364 RID: 868
		[CompilerGenerated]
		private object d;

		// Token: 0x04000365 RID: 869
		public long records;

		// Token: 0x04000366 RID: 870
		public int page;

		// Token: 0x04000367 RID: 871
		public decimal total;

		// Token: 0x04000368 RID: 872
		public object rows;
	}
}
