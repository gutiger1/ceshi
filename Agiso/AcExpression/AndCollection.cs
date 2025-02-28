using System;
using System.Collections.Generic;

namespace Agiso.AcExpression
{
	// Token: 0x020000EE RID: 238
	public class AndCollection : List<AndInfo>
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0004D360 File Offset: 0x0004B560
		public AndInfo LastAnd
		{
			get
			{
				if (base.Count == 0)
				{
					base.Add(new AndInfo());
				}
				return base[base.Count - 1];
			}
		}
	}
}
