using System;
using System.Runtime.CompilerServices;

namespace Agiso.Object
{
	// Token: 0x0200068B RID: 1675
	public class MailSenderAccount
	{
		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06001FEA RID: 8170 RVA: 0x0000E344 File Offset: 0x0000C544
		// (set) Token: 0x06001FEB RID: 8171 RVA: 0x0000E34C File Offset: 0x0000C54C
		public string SmtpHost { get; set; }

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06001FEC RID: 8172 RVA: 0x0000E355 File Offset: 0x0000C555
		// (set) Token: 0x06001FED RID: 8173 RVA: 0x0000E35D File Offset: 0x0000C55D
		public int SmtpPort { get; set; }

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06001FEE RID: 8174 RVA: 0x0000E366 File Offset: 0x0000C566
		// (set) Token: 0x06001FEF RID: 8175 RVA: 0x0000E36E File Offset: 0x0000C56E
		public string Account { get; set; }

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x0000E377 File Offset: 0x0000C577
		// (set) Token: 0x06001FF1 RID: 8177 RVA: 0x0000E37F File Offset: 0x0000C57F
		public string Password { get; set; }

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x0000E388 File Offset: 0x0000C588
		// (set) Token: 0x06001FF3 RID: 8179 RVA: 0x0000E390 File Offset: 0x0000C590
		public string MailFrom { get; set; }

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x0000E399 File Offset: 0x0000C599
		// (set) Token: 0x06001FF5 RID: 8181 RVA: 0x0000E3A1 File Offset: 0x0000C5A1
		public string Nick { get; set; }

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x0000E3AA File Offset: 0x0000C5AA
		// (set) Token: 0x06001FF7 RID: 8183 RVA: 0x0000E3B2 File Offset: 0x0000C5B2
		public bool EnableSsl { get; set; }

		// Token: 0x06001FF8 RID: 8184 RVA: 0x00052EC4 File Offset: 0x000510C4
		public bool ValidateAccount()
		{
			return !string.IsNullOrEmpty(this.SmtpHost) && !string.IsNullOrEmpty(this.Account) && !string.IsNullOrEmpty(this.Password) && !string.IsNullOrEmpty(this.MailFrom) && (this.SmtpPort >= 1 && this.SmtpPort <= 65535);
		}

		// Token: 0x04001220 RID: 4640
		[CompilerGenerated]
		private string a;

		// Token: 0x04001221 RID: 4641
		[CompilerGenerated]
		private int b;

		// Token: 0x04001222 RID: 4642
		[CompilerGenerated]
		private string c;

		// Token: 0x04001223 RID: 4643
		[CompilerGenerated]
		private string d;

		// Token: 0x04001224 RID: 4644
		[CompilerGenerated]
		private string e;

		// Token: 0x04001225 RID: 4645
		[CompilerGenerated]
		private string f;

		// Token: 0x04001226 RID: 4646
		[CompilerGenerated]
		private bool g;
	}
}
