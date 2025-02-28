using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Timers;
using Agiso.Utils;
using Agiso.WwWebSocket.Model;
using Newtonsoft.Json;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace Agiso.WwService.Sdk
{
	// Token: 0x020000E2 RID: 226
	public class WebSocketClient
	{
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600068F RID: 1679 RVA: 0x00047CE0 File Offset: 0x00045EE0
		// (remove) Token: 0x06000690 RID: 1680 RVA: 0x00047D18 File Offset: 0x00045F18
		public event Action<List<AliwwWsMsg>> AliwwWsMsgReceived
		{
			[CompilerGenerated]
			add
			{
				Action<List<AliwwWsMsg>> action = this.f;
				Action<List<AliwwWsMsg>> action2;
				do
				{
					action2 = action;
					Action<List<AliwwWsMsg>> action3 = (Action<List<AliwwWsMsg>>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<List<AliwwWsMsg>>>(ref this.f, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<List<AliwwWsMsg>> action = this.f;
				Action<List<AliwwWsMsg>> action2;
				do
				{
					action2 = action;
					Action<List<AliwwWsMsg>> action3 = (Action<List<AliwwWsMsg>>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<List<AliwwWsMsg>>>(ref this.f, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000691 RID: 1681 RVA: 0x00047D50 File Offset: 0x00045F50
		// (remove) Token: 0x06000692 RID: 1682 RVA: 0x00047D88 File Offset: 0x00045F88
		public event Action<CurrOnlineUsers> OnlineUserChanged
		{
			[CompilerGenerated]
			add
			{
				Action<CurrOnlineUsers> action = this.g;
				Action<CurrOnlineUsers> action2;
				do
				{
					action2 = action;
					Action<CurrOnlineUsers> action3 = (Action<CurrOnlineUsers>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<CurrOnlineUsers>>(ref this.g, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<CurrOnlineUsers> action = this.g;
				Action<CurrOnlineUsers> action2;
				do
				{
					action2 = action;
					Action<CurrOnlineUsers> action3 = (Action<CurrOnlineUsers>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<CurrOnlineUsers>>(ref this.g, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000693 RID: 1683 RVA: 0x00047DC0 File Offset: 0x00045FC0
		// (remove) Token: 0x06000694 RID: 1684 RVA: 0x00047DF8 File Offset: 0x00045FF8
		public event Action<NotifyMsg> OnNotifyMsged
		{
			[CompilerGenerated]
			add
			{
				Action<NotifyMsg> action = this.h;
				Action<NotifyMsg> action2;
				do
				{
					action2 = action;
					Action<NotifyMsg> action3 = (Action<NotifyMsg>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<NotifyMsg>>(ref this.h, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<NotifyMsg> action = this.h;
				Action<NotifyMsg> action2;
				do
				{
					action2 = action;
					Action<NotifyMsg> action3 = (Action<NotifyMsg>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<NotifyMsg>>(ref this.h, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000695 RID: 1685 RVA: 0x00047E30 File Offset: 0x00046030
		// (remove) Token: 0x06000696 RID: 1686 RVA: 0x00047E68 File Offset: 0x00046068
		public event Action<object> OnOpened
		{
			[CompilerGenerated]
			add
			{
				Action<object> action = this.i;
				Action<object> action2;
				do
				{
					action2 = action;
					Action<object> action3 = (Action<object>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<object>>(ref this.i, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<object> action = this.i;
				Action<object> action2;
				do
				{
					action2 = action;
					Action<object> action3 = (Action<object>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<object>>(ref this.i, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000697 RID: 1687 RVA: 0x00047EA0 File Offset: 0x000460A0
		// (remove) Token: 0x06000698 RID: 1688 RVA: 0x00047ED8 File Offset: 0x000460D8
		public event Action<object> OnClosed
		{
			[CompilerGenerated]
			add
			{
				Action<object> action = this.j;
				Action<object> action2;
				do
				{
					action2 = action;
					Action<object> action3 = (Action<object>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<object>>(ref this.j, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<object> action = this.j;
				Action<object> action2;
				do
				{
					action2 = action;
					Action<object> action3 = (Action<object>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<object>>(ref this.j, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000699 RID: 1689 RVA: 0x00047F10 File Offset: 0x00046110
		// (remove) Token: 0x0600069A RID: 1690 RVA: 0x00047F48 File Offset: 0x00046148
		public event Action<Exception> OnError
		{
			[CompilerGenerated]
			add
			{
				Action<Exception> action = this.k;
				Action<Exception> action2;
				do
				{
					action2 = action;
					Action<Exception> action3 = (Action<Exception>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Exception>>(ref this.k, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Exception> action = this.k;
				Action<Exception> action2;
				do
				{
					action2 = action;
					Action<Exception> action3 = (Action<Exception>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Exception>>(ref this.k, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600069B RID: 1691 RVA: 0x00047F80 File Offset: 0x00046180
		// (remove) Token: 0x0600069C RID: 1692 RVA: 0x00047FB8 File Offset: 0x000461B8
		public event Func<int> GetMsgCount
		{
			[CompilerGenerated]
			add
			{
				Func<int> func = this.l;
				Func<int> func2;
				do
				{
					func2 = func;
					Func<int> func3 = (Func<int>)Delegate.Combine(func2, value);
					func = Interlocked.CompareExchange<Func<int>>(ref this.l, func3, func2);
				}
				while (func != func2);
			}
			[CompilerGenerated]
			remove
			{
				Func<int> func = this.l;
				Func<int> func2;
				do
				{
					func2 = func;
					Func<int> func3 = (Func<int>)Delegate.Remove(func2, value);
					func = Interlocked.CompareExchange<Func<int>>(ref this.l, func3, func2);
				}
				while (func != func2);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00004303 File Offset: 0x00002503
		public bool IsOpened
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00047FF0 File Offset: 0x000461F0
		public WebSocketState State
		{
			get
			{
				WebSocket webSocket = this.c;
				WebSocketState webSocketState;
				if (webSocket == null)
				{
					webSocketState = -1;
				}
				else
				{
					webSocketState = webSocket.State;
				}
				return webSocketState;
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00048018 File Offset: 0x00046218
		private void a(string A_0)
		{
			bool flag;
			if (this.a())
			{
				Func<bool> canInitWs = this.CanInitWs;
				flag = canInitWs != null && canInitWs();
			}
			else
			{
				flag = false;
			}
			if (flag)
			{
				object obj = WebSocketClient.e;
				lock (obj)
				{
					bool flag3;
					if (this.a())
					{
						Func<bool> canInitWs2 = this.CanInitWs;
						flag3 = canInitWs2 != null && canInitWs2();
					}
					else
					{
						flag3 = false;
					}
					if (flag3)
					{
						this.c = new WebSocket(this.a + A_0, "", null, null, "", "", -1, null);
						this.c.Closed += this.a;
						this.c.MessageReceived += this.a;
						this.c.Error += this.a;
						this.c.Opened += this.b;
						this.c.Open();
					}
				}
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00048128 File Offset: 0x00046328
		private void b(object sender, EventArgs e)
		{
			this.d = true;
			Action<object> action = this.i;
			if (action != null)
			{
				action(sender);
			}
			if (this.b == null && Interlocked.CompareExchange<global::System.Timers.Timer>(ref this.b, new global::System.Timers.Timer(30000.0), null) == null)
			{
				this.b.Elapsed += this.a;
				this.b.Start();
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0004819C File Offset: 0x0004639C
		private void a(object sender, ElapsedEventArgs e)
		{
			WsMsg wsMsg = new WsMsg();
			wsMsg.MsgType = "Beat";
			BeatInfo beatInfo = new BeatInfo();
			Func<int> func = this.l;
			beatInfo.MsgCount = ((func != null) ? func() : 0);
			wsMsg.Body = JSON.Encode(beatInfo);
			WsMsg wsMsg2 = wsMsg;
			this.TrySend(wsMsg2);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x000481EC File Offset: 0x000463EC
		private void a(object sender, ErrorEventArgs e)
		{
			this.d = false;
			if (this.k != null)
			{
				this.k(e.Exception);
			}
			this.c = null;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00048224 File Offset: 0x00046424
		private void a(object sender, MessageReceivedEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.Message))
			{
				WsMsg wsMsg = JsonConvert.DeserializeObject<WsMsg>(e.Message);
				string msgType = wsMsg.MsgType;
				string text = msgType;
				if (!(text == "AliwwWsMsg"))
				{
					if (!(text == "CurrOnlineUsers"))
					{
						if (!(text == "NotifyMsg"))
						{
							if (!(text == "BeatBack"))
							{
							}
						}
						else if (this.h != null)
						{
							NotifyMsg notifyMsg = JsonConvert.DeserializeObject<NotifyMsg>(wsMsg.Body);
							this.h(notifyMsg);
						}
					}
					else if (this.g != null)
					{
						CurrOnlineUsers currOnlineUsers = JsonConvert.DeserializeObject<CurrOnlineUsers>(wsMsg.Body);
						this.g(currOnlineUsers);
					}
				}
				else if (this.f != null)
				{
					List<AliwwWsMsg> list = JsonConvert.DeserializeObject<List<AliwwWsMsg>>(wsMsg.Body);
					this.f(list);
				}
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0004830C File Offset: 0x0004650C
		private void a(object sender, EventArgs e)
		{
			this.d = false;
			if (this.j != null)
			{
				this.j(sender);
			}
			this.c = null;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00048340 File Offset: 0x00046540
		public void AddOnlineUser(string nick, string password, string version)
		{
			if (this.a())
			{
				Func<bool> canInitWs = this.CanInitWs;
				if (canInitWs == null || canInitWs())
				{
					long timeStamp = Utils.GetTimeStamp();
					Dictionary<string, string> dictionary = new Dictionary<string, string>
					{
						{
							"timestamp",
							timeStamp.ToString()
						},
						{ "nick", nick },
						{ "version", version }
					};
					string text = Utils.SignRequest(dictionary, password);
					string text2 = string.Format("/{0}/{1}/{2}/{3}", new object[] { timeStamp, version, nick, text });
					this.a(text2);
				}
			}
			else
			{
				long timeStamp2 = Utils.GetTimeStamp();
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>
				{
					{
						"timestamp",
						timeStamp2.ToString()
					},
					{ "nick", nick }
				};
				string text3 = Utils.SignRequest(dictionary2, password);
				AddUserWsMsg addUserWsMsg = new AddUserWsMsg
				{
					Timestamp = timeStamp2,
					Nick = nick,
					Sign = text3
				};
				WsMsg wsMsg = new WsMsg
				{
					MsgType = "AddUserWsMsg",
					Body = JsonConvert.SerializeObject(addUserWsMsg)
				};
				this.TrySend(wsMsg);
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00048468 File Offset: 0x00046668
		public void RemoveOnlineUser(string nick)
		{
			WsMsg wsMsg = new WsMsg
			{
				MsgType = "RemoveUserWsMsg",
				Body = nick
			};
			this.TrySend(wsMsg);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00048498 File Offset: 0x00046698
		public void MsgSendSuccess(long idNo)
		{
			WsMsg wsMsg = new WsMsg
			{
				MsgType = "MsgSendSuccess",
				Body = idNo.ToString()
			};
			this.TrySend(wsMsg);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000484CC File Offset: 0x000466CC
		public void BatchLogin(Dictionary<string, string> users, string version)
		{
			if (this.a())
			{
				Func<bool> canInitWs = this.CanInitWs;
				if (canInitWs == null || canInitWs())
				{
					StringBuilder stringBuilder = new StringBuilder();
					long timeStamp = Utils.GetTimeStamp();
					stringBuilder.AppendFormat("/{0}/{1}", timeStamp, version);
					foreach (KeyValuePair<string, string> keyValuePair in users)
					{
						Dictionary<string, string> dictionary = new Dictionary<string, string>
						{
							{
								"timestamp",
								timeStamp.ToString()
							},
							{ "version", version },
							{ "nick", keyValuePair.Key }
						};
						string text = Utils.SignRequest(dictionary, keyValuePair.Value);
						stringBuilder.AppendFormat("/{0}/{1}", keyValuePair.Key, text);
					}
					string text2 = stringBuilder.ToString();
					this.a(text2);
				}
			}
			else
			{
				foreach (KeyValuePair<string, string> keyValuePair2 in users)
				{
					this.AddOnlineUser(keyValuePair2.Key, keyValuePair2.Value, version);
				}
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0004861C File Offset: 0x0004681C
		public void Close()
		{
			if (this.State == 1)
			{
				WebSocket webSocket = this.c;
				if (webSocket != null)
				{
					webSocket.Close();
				}
			}
			this.d = false;
			WebSocket webSocket2 = this.c;
			if (webSocket2 != null)
			{
				webSocket2.Dispose();
			}
			this.c = null;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0000430B File Offset: 0x0000250B
		public void Disponse()
		{
			this.Close();
			global::System.Timers.Timer timer = this.b;
			if (timer != null)
			{
				timer.Dispose();
			}
			this.b = null;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00048664 File Offset: 0x00046864
		private bool a()
		{
			WebSocket webSocket = this.c;
			return webSocket == null || webSocket.State == -1 || webSocket.State == 2 || webSocket.State == 3;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0004869C File Offset: 0x0004689C
		public bool TrySend(WsMsg wsMsg)
		{
			bool flag;
			try
			{
				WebSocket webSocket = this.c;
				if (webSocket == null)
				{
					flag = false;
				}
				else if (webSocket.State != 1)
				{
					flag = false;
				}
				else
				{
					webSocket.Send(JsonConvert.SerializeObject(wsMsg));
					flag = true;
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
				flag = false;
			}
			return flag;
		}

		// Token: 0x040004BA RID: 1210
		private string a = (WwMsgConfig.IsTest ? "ws://wwmsgwebsocket.test.agiso.com:30001" : "ws://wwmsgwebsocket.agiso.com:30001");

		// Token: 0x040004BB RID: 1211
		private global::System.Timers.Timer b;

		// Token: 0x040004BC RID: 1212
		private WebSocket c;

		// Token: 0x040004BD RID: 1213
		private bool d = false;

		// Token: 0x040004BE RID: 1214
		private static object e = new object();

		// Token: 0x040004BF RID: 1215
		public Func<bool> CanInitWs;

		// Token: 0x040004C0 RID: 1216
		[CompilerGenerated]
		private Action<List<AliwwWsMsg>> f;

		// Token: 0x040004C1 RID: 1217
		[CompilerGenerated]
		private Action<CurrOnlineUsers> g;

		// Token: 0x040004C2 RID: 1218
		[CompilerGenerated]
		private Action<NotifyMsg> h;

		// Token: 0x040004C3 RID: 1219
		[CompilerGenerated]
		private Action<object> i;

		// Token: 0x040004C4 RID: 1220
		[CompilerGenerated]
		private Action<object> j;

		// Token: 0x040004C5 RID: 1221
		[CompilerGenerated]
		private Action<Exception> k;

		// Token: 0x040004C6 RID: 1222
		[CompilerGenerated]
		private Func<int> l;
	}
}
