using System;
using WebSocketSharp.Server;

namespace AliwwClient.WebSocketServer
{
	// Token: 0x02000075 RID: 117
	public class AgisoWebSocketServer : WebSocketServer
	{
		// Token: 0x06000362 RID: 866 RVA: 0x0000334E File Offset: 0x0000154E
		public AgisoWebSocketServer(int port)
			: base(port)
		{
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00037940 File Offset: 0x00035B40
		public WebSocketSessionManager AliwwSessionManager
		{
			get
			{
				WebSocketServiceHost webSocketServiceHost = base.WebSocketServices["/Aliww"];
				return (webSocketServiceHost != null) ? webSocketServiceHost.Sessions : null;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0003796C File Offset: 0x00035B6C
		public WebSocketSessionManager AldsSessionManager
		{
			get
			{
				WebSocketServiceHost webSocketServiceHost = base.WebSocketServices["/Alds"];
				return (webSocketServiceHost != null) ? webSocketServiceHost.Sessions : null;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00037998 File Offset: 0x00035B98
		public WebSocketSessionManager LoginSessionManager
		{
			get
			{
				WebSocketServiceHost webSocketServiceHost = base.WebSocketServices["/Login"];
				return (webSocketServiceHost != null) ? webSocketServiceHost.Sessions : null;
			}
		}
	}
}
