using System;
using System.Runtime.CompilerServices;
using Agiso.ChromeDevTools.Protocol.Chrome.Runtime;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x02000587 RID: 1415
	[SupportedBy("Chrome")]
	public class CollectionEntry
	{
		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x0000C1E1 File Offset: 0x0000A3E1
		// (set) Token: 0x06001AFB RID: 6907 RVA: 0x0000C1E9 File Offset: 0x0000A3E9
		public RemoteObject Key { get; set; }

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x0000C1F2 File Offset: 0x0000A3F2
		// (set) Token: 0x06001AFD RID: 6909 RVA: 0x0000C1FA File Offset: 0x0000A3FA
		public RemoteObject Value { get; set; }

		// Token: 0x04000F49 RID: 3913
		[CompilerGenerated]
		private RemoteObject a;

		// Token: 0x04000F4A RID: 3914
		[CompilerGenerated]
		private RemoteObject b;
	}
}
