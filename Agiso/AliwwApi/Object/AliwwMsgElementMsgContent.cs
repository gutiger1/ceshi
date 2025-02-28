using System;
using System.Text;

namespace Agiso.AliwwApi.Object
{
	// Token: 0x0200072C RID: 1836
	public class AliwwMsgElementMsgContent
	{
		// Token: 0x06002476 RID: 9334 RVA: 0x0000EEF3 File Offset: 0x0000D0F3
		public void AppendContentText(string str)
		{
			this.a.AppendLine(str);
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x0000EF02 File Offset: 0x0000D102
		public void AppendContentHtml(string str)
		{
			this.b.AppendLine(str);
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002478 RID: 9336 RVA: 0x00062A9C File Offset: 0x00060C9C
		public string ContentText
		{
			get
			{
				return this.a.ToString().Trim();
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002479 RID: 9337 RVA: 0x00062ABC File Offset: 0x00060CBC
		public string ContentHtml
		{
			get
			{
				return this.b.ToString().Trim();
			}
		}

		// Token: 0x04001E31 RID: 7729
		private StringBuilder a = new StringBuilder();

		// Token: 0x04001E32 RID: 7730
		private StringBuilder b = new StringBuilder();
	}
}
