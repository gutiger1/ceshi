using System;
using System.ComponentModel;

namespace Agiso.Object
{
	// Token: 0x0200068C RID: 1676
	[Flags]
	public enum MsgSendSoftware
	{
		// Token: 0x04001228 RID: 4648
		[Description("未指定聊天软件")]
		Undefined = 0,
		// Token: 0x04001229 RID: 4649
		[Description("千牛7.12+")]
		QN = 1,
		// Token: 0x0400122A RID: 4650
		[Description("千牛6.04+")]
		QN604 = 2,
		// Token: 0x0400122B RID: 4651
		[Description("买家旺旺版")]
		AliwwBuyer9 = 200,
		// Token: 0x0400122C RID: 4652
		[Description("旺旺国际版")]
		Tm2015 = 300
	}
}
