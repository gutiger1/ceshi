using System;

namespace Agiso.Object
{
	// Token: 0x02000687 RID: 1671
	[Flags]
	public enum EnumAppConfigOption1
	{
		// Token: 0x04001205 RID: 4613
		Undefine = 0,
		// Token: 0x04001206 RID: 4614
		AllowCheckTargetNick = 1,
		// Token: 0x04001207 RID: 4615
		DisableCloseWindowWhenAutoReply = 2,
		// Token: 0x04001208 RID: 4616
		AllowSendExpression = 4,
		// Token: 0x04001209 RID: 4617
		AllowSetMinimizeCustomerBenchWindowWhileSendSuccess = 8,
		// Token: 0x0400120A RID: 4618
		DisableAutoFitEnterOrCtrlEnter = 16,
		// Token: 0x0400120B RID: 4619
		AllowWriteLogAboutThreadAbort = 32,
		// Token: 0x0400120C RID: 4620
		AllowUseQnIndex = 64,
		// Token: 0x0400120D RID: 4621
		AllowKillAliApp = 128,
		// Token: 0x0400120E RID: 4622
		AutoReplyBySellerNick = 256,
		// Token: 0x0400120F RID: 4623
		AllowGetMsgByWebSocket = 512,
		// Token: 0x04001210 RID: 4624
		AllowSendMsgByWebSocket = 1024,
		// Token: 0x04001211 RID: 4625
		CloseWindowScanAutoReplay = 2048,
		// Token: 0x04001212 RID: 4626
		OnlyFirstReply = 4096,
		// Token: 0x04001213 RID: 4627
		AutoSendBeforeMsg = 8192,
		// Token: 0x04001214 RID: 4628
		[Obsolete]
		CloseSendMsgCheck = 16384,
		// Token: 0x04001215 RID: 4629
		EnableSendMsgCheck = 32768,
		// Token: 0x04001216 RID: 4630
		FirstReplyContinueNoMatch = 65536,
		// Token: 0x04001217 RID: 4631
		FirstReplyContinueMatch = 131072
	}
}
