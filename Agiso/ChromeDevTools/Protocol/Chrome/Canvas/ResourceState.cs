using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x02000640 RID: 1600
	[SupportedBy("Chrome")]
	public class ResourceState
	{
		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x0000D7BA File Offset: 0x0000B9BA
		// (set) Token: 0x06001E45 RID: 7749 RVA: 0x0000D7C2 File Offset: 0x0000B9C2
		public string Id { get; set; }

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x0000D7CB File Offset: 0x0000B9CB
		// (set) Token: 0x06001E47 RID: 7751 RVA: 0x0000D7D3 File Offset: 0x0000B9D3
		public string TraceLogId { get; set; }

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x0000D7DC File Offset: 0x0000B9DC
		// (set) Token: 0x06001E49 RID: 7753 RVA: 0x0000D7E4 File Offset: 0x0000B9E4
		public ResourceStateDescriptor[] Descriptors { get; set; }

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x0000D7ED File Offset: 0x0000B9ED
		// (set) Token: 0x06001E4B RID: 7755 RVA: 0x0000D7F5 File Offset: 0x0000B9F5
		public string ImageURL { get; set; }

		// Token: 0x04001097 RID: 4247
		[CompilerGenerated]
		private string a;

		// Token: 0x04001098 RID: 4248
		[CompilerGenerated]
		private string b;

		// Token: 0x04001099 RID: 4249
		[CompilerGenerated]
		private ResourceStateDescriptor[] c;

		// Token: 0x0400109A RID: 4250
		[CompilerGenerated]
		private string d;
	}
}
