using System;
using System.ComponentModel;

namespace Agiso
{
	// Token: 0x020000CB RID: 203
	[Flags]
	public enum LogType
	{
		// Token: 0x04000483 RID: 1155
		[Description("用于跟踪程序为何挂起")]
		LogForTraceHold = 1,
		// Token: 0x04000484 RID: 1156
		[Description("用于跟踪智能答复问题")]
		LogForAutoReply = 2,
		// Token: 0x04000485 RID: 1157
		[Description("用于跟踪ChormeDebug")]
		LogForChormeDebug = 4,
		// Token: 0x04000486 RID: 1158
		[Description("用于跟踪PasswordOrAccountIsNull")]
		LogForPasswordOrAccountIsNull = 8,
		// Token: 0x04000487 RID: 1159
		[Description("用于跟踪自动升级失败，无法升级成功的问题")]
		LogForAutoUpdateFail = 16,
		// Token: 0x04000488 RID: 1160
		[Description("用于记录收到的买家消息")]
		LogReceivedMsg = 32,
		// Token: 0x04000489 RID: 1161
		[Description("用于追踪极速发送消息")]
		LogSendMsgByWebSocket = 128,
		// Token: 0x0400048A RID: 1162
		[Description("用于追踪机器人不回复消息的情形")]
		LogForRobot = 256,
		// Token: 0x0400048B RID: 1163
		[Description("用于跟踪LoginWs的链接情况")]
		LogLoginWs = 512,
		// Token: 0x0400048C RID: 1164
		[Description("用于跟踪消息发送异常")]
		LogSendMsg = 1024
	}
}
