using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005D2 RID: 1490
	[Command("Debugger.setSkipAllPauses")]
	[SupportedBy("Chrome")]
	public class SetSkipAllPausesCommand
	{
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x0000CAC7 File Offset: 0x0000ACC7
		// (set) Token: 0x06001C52 RID: 7250 RVA: 0x0000CACF File Offset: 0x0000ACCF
		public bool Skipped { get; set; }

		// Token: 0x04000FCF RID: 4047
		[CompilerGenerated]
		private bool a;
	}
}
