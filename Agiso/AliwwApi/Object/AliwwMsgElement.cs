using System;
using System.Runtime.CompilerServices;

namespace Agiso.AliwwApi.Object
{
	// Token: 0x02000729 RID: 1833
	public class AliwwMsgElement
	{
		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x0600245B RID: 9307 RVA: 0x0000EE49 File Offset: 0x0000D049
		// (set) Token: 0x0600245C RID: 9308 RVA: 0x0000EE51 File Offset: 0x0000D051
		public long MsgId { get; set; }

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x0600245D RID: 9309 RVA: 0x0000EE5A File Offset: 0x0000D05A
		// (set) Token: 0x0600245E RID: 9310 RVA: 0x0000EE62 File Offset: 0x0000D062
		public string MsgSendId { get; set; }

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x0600245F RID: 9311 RVA: 0x0000EE6B File Offset: 0x0000D06B
		// (set) Token: 0x06002460 RID: 9312 RVA: 0x0000EE73 File Offset: 0x0000D073
		public bool IsHistory { get; set; }

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x0000EE7C File Offset: 0x0000D07C
		// (set) Token: 0x06002462 RID: 9314 RVA: 0x0000EE84 File Offset: 0x0000D084
		public AliwwMsgElementMsgHead MsgHead { get; set; }

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06002463 RID: 9315 RVA: 0x0000EE8D File Offset: 0x0000D08D
		// (set) Token: 0x06002464 RID: 9316 RVA: 0x0000EE95 File Offset: 0x0000D095
		public AliwwMsgElementMsgContent MsgContent { get; set; }

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06002465 RID: 9317 RVA: 0x000629AC File Offset: 0x00060BAC
		public string SenderSite
		{
			get
			{
				string text;
				if (this.MsgHead == null)
				{
					text = "";
				}
				else
				{
					text = this.MsgHead.SenderSite;
				}
				return text;
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x000629DC File Offset: 0x00060BDC
		public string SenderNick
		{
			get
			{
				string text;
				if (this.MsgHead == null)
				{
					text = "";
				}
				else
				{
					text = this.MsgHead.SenderName;
				}
				return text;
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06002467 RID: 9319 RVA: 0x00062A0C File Offset: 0x00060C0C
		public DateTime SendTime
		{
			get
			{
				DateTime dateTime;
				if (this.MsgHead == null)
				{
					dateTime = DateTime.MinValue;
				}
				else
				{
					dateTime = this.MsgHead.MsgTime;
				}
				return dateTime;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x00062A3C File Offset: 0x00060C3C
		public string ContentText
		{
			get
			{
				string text;
				if (this.MsgContent == null)
				{
					text = "";
				}
				else
				{
					text = this.MsgContent.ContentText;
				}
				return text;
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06002469 RID: 9321 RVA: 0x00062A6C File Offset: 0x00060C6C
		public string ContentHtml
		{
			get
			{
				string text;
				if (this.MsgContent == null)
				{
					text = "";
				}
				else
				{
					text = this.MsgContent.ContentHtml;
				}
				return text;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x0000EE9E File Offset: 0x0000D09E
		// (set) Token: 0x0600246B RID: 9323 RVA: 0x0000EEA6 File Offset: 0x0000D0A6
		public bool IsSysMsg { get; set; }

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x0000EEAF File Offset: 0x0000D0AF
		// (set) Token: 0x0600246D RID: 9325 RVA: 0x0000EEB7 File Offset: 0x0000D0B7
		public MsgType MsgType { get; set; }

		// Token: 0x04001E22 RID: 7714
		[CompilerGenerated]
		private long a;

		// Token: 0x04001E23 RID: 7715
		[CompilerGenerated]
		private string b;

		// Token: 0x04001E24 RID: 7716
		[CompilerGenerated]
		private bool c;

		// Token: 0x04001E25 RID: 7717
		[CompilerGenerated]
		private AliwwMsgElementMsgHead d;

		// Token: 0x04001E26 RID: 7718
		[CompilerGenerated]
		private AliwwMsgElementMsgContent e;

		// Token: 0x04001E27 RID: 7719
		[CompilerGenerated]
		private bool f;

		// Token: 0x04001E28 RID: 7720
		[CompilerGenerated]
		private MsgType g;
	}
}
