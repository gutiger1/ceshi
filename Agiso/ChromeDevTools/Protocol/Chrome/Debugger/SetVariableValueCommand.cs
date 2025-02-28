using System;
using System.Runtime.CompilerServices;
using Agiso.ChromeDevTools.Protocol.Chrome.Runtime;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005D4 RID: 1492
	[SupportedBy("Chrome")]
	[Command("Debugger.setVariableValue")]
	public class SetVariableValueCommand
	{
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x0000CAD8 File Offset: 0x0000ACD8
		// (set) Token: 0x06001C56 RID: 7254 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
		public long ScopeNumber { get; set; }

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x0000CAE9 File Offset: 0x0000ACE9
		// (set) Token: 0x06001C58 RID: 7256 RVA: 0x0000CAF1 File Offset: 0x0000ACF1
		public string VariableName { get; set; }

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x0000CAFA File Offset: 0x0000ACFA
		// (set) Token: 0x06001C5A RID: 7258 RVA: 0x0000CB02 File Offset: 0x0000AD02
		public CallArgument NewValue { get; set; }

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x0000CB0B File Offset: 0x0000AD0B
		// (set) Token: 0x06001C5C RID: 7260 RVA: 0x0000CB13 File Offset: 0x0000AD13
		public string CallFrameId { get; set; }

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x0000CB1C File Offset: 0x0000AD1C
		// (set) Token: 0x06001C5E RID: 7262 RVA: 0x0000CB24 File Offset: 0x0000AD24
		public string FunctionObjectId { get; set; }

		// Token: 0x04000FD0 RID: 4048
		[CompilerGenerated]
		private long a;

		// Token: 0x04000FD1 RID: 4049
		[CompilerGenerated]
		private string b;

		// Token: 0x04000FD2 RID: 4050
		[CompilerGenerated]
		private CallArgument c;

		// Token: 0x04000FD3 RID: 4051
		[CompilerGenerated]
		private string d;

		// Token: 0x04000FD4 RID: 4052
		[CompilerGenerated]
		private string e;
	}
}
