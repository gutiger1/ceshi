using System;
using System.Runtime.CompilerServices;

namespace Agiso.Windows
{
	// Token: 0x020006AE RID: 1710
	public class WindowStruct
	{
		// Token: 0x06002168 RID: 8552 RVA: 0x000025CE File Offset: 0x000007CE
		public WindowStruct()
		{
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x0000EA76 File Offset: 0x0000CC76
		public WindowStruct(string szClassNameParam, string szWindowNameParam)
		{
			this.ClassName = szClassNameParam;
			this.WindowName = szWindowNameParam;
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x0000EA8D File Offset: 0x0000CC8D
		// (set) Token: 0x0600216B RID: 8555 RVA: 0x0000EA95 File Offset: 0x0000CC95
		public string ClassName { get; set; }

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x0000EA9E File Offset: 0x0000CC9E
		// (set) Token: 0x0600216D RID: 8557 RVA: 0x0000EAA6 File Offset: 0x0000CCA6
		public string WindowName { get; set; }

		// Token: 0x04001294 RID: 4756
		[CompilerGenerated]
		private string a;

		// Token: 0x04001295 RID: 4757
		[CompilerGenerated]
		private string b;
	}
}
