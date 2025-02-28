using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005B1 RID: 1457
	[SupportedBy("Chrome")]
	[Event("Debugger.promiseUpdated")]
	public class PromiseUpdatedEvent
	{
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x0000C676 File Offset: 0x0000A876
		// (set) Token: 0x06001BAF RID: 7087 RVA: 0x0000C67E File Offset: 0x0000A87E
		public string EventType { get; set; }

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x0000C687 File Offset: 0x0000A887
		// (set) Token: 0x06001BB1 RID: 7089 RVA: 0x0000C68F File Offset: 0x0000A88F
		public PromiseDetails Promise { get; set; }

		// Token: 0x04000F8E RID: 3982
		[CompilerGenerated]
		private string a;

		// Token: 0x04000F8F RID: 3983
		[CompilerGenerated]
		private PromiseDetails b;
	}
}
