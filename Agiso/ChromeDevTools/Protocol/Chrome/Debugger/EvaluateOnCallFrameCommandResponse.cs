using System;
using System.Runtime.CompilerServices;
using Agiso.ChromeDevTools.Protocol.Chrome.Runtime;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x02000595 RID: 1429
	[SupportedBy("Chrome")]
	[CommandResponse("Debugger.evaluateOnCallFrame")]
	public class EvaluateOnCallFrameCommandResponse
	{
		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x0000C313 File Offset: 0x0000A513
		// (set) Token: 0x06001B2D RID: 6957 RVA: 0x0000C31B File Offset: 0x0000A51B
		public RemoteObject Result { get; set; }

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x0000C324 File Offset: 0x0000A524
		// (set) Token: 0x06001B2F RID: 6959 RVA: 0x0000C32C File Offset: 0x0000A52C
		public bool WasThrown { get; set; }

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x0000C335 File Offset: 0x0000A535
		// (set) Token: 0x06001B31 RID: 6961 RVA: 0x0000C33D File Offset: 0x0000A53D
		public ExceptionDetails ExceptionDetails { get; set; }

		// Token: 0x04000F5B RID: 3931
		[CompilerGenerated]
		private RemoteObject a;

		// Token: 0x04000F5C RID: 3932
		[CompilerGenerated]
		private bool b;

		// Token: 0x04000F5D RID: 3933
		[CompilerGenerated]
		private ExceptionDetails c;
	}
}
