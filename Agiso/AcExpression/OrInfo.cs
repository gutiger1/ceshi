using System;

namespace Agiso.AcExpression
{
	// Token: 0x020000ED RID: 237
	public class OrInfo
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0004D2CC File Offset: 0x0004B4CC
		public AndInfo LastChild
		{
			get
			{
				return this.Child.LastAnd;
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0004D2E8 File Offset: 0x0004B4E8
		public bool Exec(string source)
		{
			bool flag;
			if (this.Child.Count > 0)
			{
				foreach (AndInfo andInfo in this.Child)
				{
					if (!andInfo.Exec(source))
					{
						return false;
					}
				}
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x040004E0 RID: 1248
		public AndCollection Child = new AndCollection();
	}
}
