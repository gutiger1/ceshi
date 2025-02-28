using System;
using System.Runtime.CompilerServices;
using Agiso.AliwwApi;

namespace AliwwClient.Object
{
	// Token: 0x020000A2 RID: 162
	public class RobotAlitwScanInfo
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00003A16 File Offset: 0x00001C16
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x00003A1E File Offset: 0x00001C1E
		public AliwwTalkWindow Alitw { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00003A27 File Offset: 0x00001C27
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x00003A2F File Offset: 0x00001C2F
		public DateTime ScanTime { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00003A38 File Offset: 0x00001C38
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x00003A40 File Offset: 0x00001C40
		public DateTime DeadLine { get; set; }

		// Token: 0x040003A7 RID: 935
		[CompilerGenerated]
		private AliwwTalkWindow a;

		// Token: 0x040003A8 RID: 936
		[CompilerGenerated]
		private DateTime b;

		// Token: 0x040003A9 RID: 937
		[CompilerGenerated]
		private DateTime c;
	}
}
