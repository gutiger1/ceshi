using System;
using System.IO;
using System.Runtime.CompilerServices;
using Agiso.Windows;

namespace Agiso.Utils
{
	// Token: 0x020000EA RID: 234
	public class ProcessInfo
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x00004415 File Offset: 0x00002615
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x0000441D File Offset: 0x0000261D
		public int Id { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x00004426 File Offset: 0x00002626
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x0000442E File Offset: 0x0000262E
		public string MainModuleFileName { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0004D134 File Offset: 0x0004B334
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0004D14C File Offset: 0x0004B34C
		public IntPtr MainWindowHandle
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
				WindowInfo windowInfo = new WindowInfo(this.c);
				if (this.c == IntPtr.Zero || windowInfo.Info == null)
				{
					this.d = "";
				}
				this.d = new WindowInfo(this.c).Info.WindowName;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0004D1B4 File Offset: 0x0004B3B4
		public string MainDirectoryName
		{
			get
			{
				string text;
				if (!string.IsNullOrEmpty(this.MainModuleFileName))
				{
					text = Path.GetDirectoryName(this.MainModuleFileName);
				}
				else
				{
					text = "";
				}
				return text;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0004D1E8 File Offset: 0x0004B3E8
		public string MainWindowTitle
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00004437 File Offset: 0x00002637
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x0000443F File Offset: 0x0000263F
		public string Version { get; set; }

		// Token: 0x040004D8 RID: 1240
		[CompilerGenerated]
		private int a;

		// Token: 0x040004D9 RID: 1241
		[CompilerGenerated]
		private string b;

		// Token: 0x040004DA RID: 1242
		private IntPtr c;

		// Token: 0x040004DB RID: 1243
		private string d;

		// Token: 0x040004DC RID: 1244
		[CompilerGenerated]
		private string e;
	}
}
