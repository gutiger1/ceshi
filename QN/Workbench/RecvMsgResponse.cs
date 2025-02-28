using System;
using System.Runtime.CompilerServices;
using Agiso.Object;
using Agiso.Utils;
using Newtonsoft.Json;

namespace AliwwClient.QN.Workbench
{
	// Token: 0x02000097 RID: 151
	public class RecvMsgResponse
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00003655 File Offset: 0x00001855
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x0000365D File Offset: 0x0000185D
		[JsonProperty("type")]
		public int Type { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000366F File Offset: 0x0000186F
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x00003666 File Offset: 0x00001866
		[JsonProperty("fromuid")]
		public string FromUid { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00003680 File Offset: 0x00001880
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x00003677 File Offset: 0x00001877
		[JsonProperty("nick")]
		public string FromNick { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00003688 File Offset: 0x00001888
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x00003690 File Offset: 0x00001890
		[JsonProperty("securityUID")]
		public string SecurityUID { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x000036A2 File Offset: 0x000018A2
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x00003699 File Offset: 0x00001899
		[JsonProperty("message")]
		public string Msg { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x000036AA File Offset: 0x000018AA
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x000036B2 File Offset: 0x000018B2
		[JsonProperty("time")]
		public long TimeStamp { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x000036BB File Offset: 0x000018BB
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x000036C3 File Offset: 0x000018C3
		[JsonProperty("offline")]
		public bool Offline { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x000036CC File Offset: 0x000018CC
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x000036D4 File Offset: 0x000018D4
		public MsgFrom MsgFrom { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0003CF40 File Offset: 0x0003B140
		public DateTime SentTime
		{
			get
			{
				return Util.TimeSpanToDateTime((double)this.TimeStamp);
			}
		}

		// Token: 0x0400036C RID: 876
		[CompilerGenerated]
		private int a;

		// Token: 0x0400036D RID: 877
		[CompilerGenerated]
		private string b;

		// Token: 0x0400036E RID: 878
		[CompilerGenerated]
		private string c;

		// Token: 0x0400036F RID: 879
		[CompilerGenerated]
		private string d;

		// Token: 0x04000370 RID: 880
		[CompilerGenerated]
		private string e;

		// Token: 0x04000371 RID: 881
		[CompilerGenerated]
		private long f;

		// Token: 0x04000372 RID: 882
		[CompilerGenerated]
		private bool g;

		// Token: 0x04000373 RID: 883
		[CompilerGenerated]
		private MsgFrom h;
	}
}
