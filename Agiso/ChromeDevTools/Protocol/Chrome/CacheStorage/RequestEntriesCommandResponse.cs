using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CacheStorage
{
	// Token: 0x0200064F RID: 1615
	[CommandResponse("CacheStorage.requestEntries")]
	[SupportedBy("Chrome")]
	public class RequestEntriesCommandResponse
	{
		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x0000D9C9 File Offset: 0x0000BBC9
		// (set) Token: 0x06001E92 RID: 7826 RVA: 0x0000D9D1 File Offset: 0x0000BBD1
		public DataEntry[] CacheDataEntries { get; set; }

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x0000D9DA File Offset: 0x0000BBDA
		// (set) Token: 0x06001E94 RID: 7828 RVA: 0x0000D9E2 File Offset: 0x0000BBE2
		public bool HasMore { get; set; }

		// Token: 0x040010B6 RID: 4278
		[CompilerGenerated]
		private DataEntry[] a;

		// Token: 0x040010B7 RID: 4279
		[CompilerGenerated]
		private bool b;
	}
}
