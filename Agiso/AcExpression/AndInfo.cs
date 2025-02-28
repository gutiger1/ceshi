using System;
using System.Text;

namespace Agiso.AcExpression
{
	// Token: 0x020000EC RID: 236
	public class AndInfo
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0004D200 File Offset: 0x0004B400
		public OrInfo LastChild
		{
			get
			{
				return this.Child.LastOr;
			}
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0004D21C File Offset: 0x0004B41C
		public bool Exec(string source)
		{
			bool flag;
			if (this.Child.Count > 0)
			{
				foreach (OrInfo orInfo in this.Child)
				{
					if (orInfo.Exec(source))
					{
						return true;
					}
				}
				flag = false;
			}
			else
			{
				string text = this.ContentB.ToString();
				if (string.IsNullOrEmpty(text))
				{
					flag = true;
				}
				else
				{
					text = text.Trim();
					flag = string.IsNullOrEmpty(text) || source.Contains(text);
				}
			}
			return flag;
		}

		// Token: 0x040004DE RID: 1246
		public OrCollection Child = new OrCollection();

		// Token: 0x040004DF RID: 1247
		public StringBuilder ContentB = new StringBuilder();
	}
}
