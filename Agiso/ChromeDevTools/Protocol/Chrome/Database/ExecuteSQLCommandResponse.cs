using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Database
{
	// Token: 0x020005E9 RID: 1513
	[SupportedBy("Chrome")]
	[CommandResponse("Database.executeSQL")]
	public class ExecuteSQLCommandResponse
	{
		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x0000CC1B File Offset: 0x0000AE1B
		// (set) Token: 0x06001C91 RID: 7313 RVA: 0x0000CC23 File Offset: 0x0000AE23
		public string[] ColumnNames { get; set; }

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06001C92 RID: 7314 RVA: 0x0000CC2C File Offset: 0x0000AE2C
		// (set) Token: 0x06001C93 RID: 7315 RVA: 0x0000CC34 File Offset: 0x0000AE34
		public object[] Values { get; set; }

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x0000CC3D File Offset: 0x0000AE3D
		// (set) Token: 0x06001C95 RID: 7317 RVA: 0x0000CC45 File Offset: 0x0000AE45
		public Error SqlError { get; set; }

		// Token: 0x04000FE3 RID: 4067
		[CompilerGenerated]
		private string[] a;

		// Token: 0x04000FE4 RID: 4068
		[CompilerGenerated]
		private object[] b;

		// Token: 0x04000FE5 RID: 4069
		[CompilerGenerated]
		private Error c;
	}
}
