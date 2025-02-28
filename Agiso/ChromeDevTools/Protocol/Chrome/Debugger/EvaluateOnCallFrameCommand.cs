using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x02000594 RID: 1428
	[Command("Debugger.evaluateOnCallFrame")]
	[SupportedBy("Chrome")]
	public class EvaluateOnCallFrameCommand
	{
		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0000C29C File Offset: 0x0000A49C
		// (set) Token: 0x06001B1E RID: 6942 RVA: 0x0000C2A4 File Offset: 0x0000A4A4
		public string CallFrameId { get; set; }

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x0000C2AD File Offset: 0x0000A4AD
		// (set) Token: 0x06001B20 RID: 6944 RVA: 0x0000C2B5 File Offset: 0x0000A4B5
		public string Expression { get; set; }

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001B21 RID: 6945 RVA: 0x0000C2BE File Offset: 0x0000A4BE
		// (set) Token: 0x06001B22 RID: 6946 RVA: 0x0000C2C6 File Offset: 0x0000A4C6
		public string ObjectGroup { get; set; }

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x0000C2CF File Offset: 0x0000A4CF
		// (set) Token: 0x06001B24 RID: 6948 RVA: 0x0000C2D7 File Offset: 0x0000A4D7
		public bool IncludeCommandLineAPI { get; set; }

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
		// (set) Token: 0x06001B26 RID: 6950 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		public bool DoNotPauseOnExceptionsAndMuteConsole { get; set; }

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x0000C2F1 File Offset: 0x0000A4F1
		// (set) Token: 0x06001B28 RID: 6952 RVA: 0x0000C2F9 File Offset: 0x0000A4F9
		public bool ReturnByValue { get; set; }

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0000C302 File Offset: 0x0000A502
		// (set) Token: 0x06001B2A RID: 6954 RVA: 0x0000C30A File Offset: 0x0000A50A
		public bool GeneratePreview { get; set; }

		// Token: 0x04000F54 RID: 3924
		[CompilerGenerated]
		private string a;

		// Token: 0x04000F55 RID: 3925
		[CompilerGenerated]
		private string b;

		// Token: 0x04000F56 RID: 3926
		[CompilerGenerated]
		private string c;

		// Token: 0x04000F57 RID: 3927
		[CompilerGenerated]
		private bool d;

		// Token: 0x04000F58 RID: 3928
		[CompilerGenerated]
		private bool e;

		// Token: 0x04000F59 RID: 3929
		[CompilerGenerated]
		private bool f;

		// Token: 0x04000F5A RID: 3930
		[CompilerGenerated]
		private bool g;
	}
}
