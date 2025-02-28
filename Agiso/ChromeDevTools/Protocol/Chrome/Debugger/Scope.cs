using System;
using System.Runtime.CompilerServices;
using Agiso.ChromeDevTools.Protocol.Chrome.Runtime;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005BD RID: 1469
	[SupportedBy("Chrome")]
	public class Scope
	{
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x0000C764 File Offset: 0x0000A964
		// (set) Token: 0x06001BD7 RID: 7127 RVA: 0x0000C76C File Offset: 0x0000A96C
		public string Type { get; set; }

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x0000C775 File Offset: 0x0000A975
		// (set) Token: 0x06001BD9 RID: 7129 RVA: 0x0000C77D File Offset: 0x0000A97D
		public RemoteObject Object { get; set; }

		// Token: 0x04000F9C RID: 3996
		[CompilerGenerated]
		private string a;

		// Token: 0x04000F9D RID: 3997
		[CompilerGenerated]
		private RemoteObject b;
	}
}
