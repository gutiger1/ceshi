using System;
using System.Runtime.CompilerServices;

namespace Agiso.Utils
{
	// Token: 0x020000EB RID: 235
	public class ProcessInfoForAliresRepair : ProcessInfo
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x00004448 File Offset: 0x00002648
		public ProcessInfoForAliresRepair(ProcessInfo p)
		{
			base.Id = p.Id;
			base.MainModuleFileName = p.MainModuleFileName;
			base.MainWindowHandle = p.MainWindowHandle;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00004475 File Offset: 0x00002675
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x0000447D File Offset: 0x0000267D
		public string RepairAliresResult { get; set; }

		// Token: 0x040004DD RID: 1245
		[CompilerGenerated]
		private string a;
	}
}
