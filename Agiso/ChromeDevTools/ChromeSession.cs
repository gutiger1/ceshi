using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Agiso.Utils;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace Agiso.ChromeDevTools
{
	// Token: 0x02000103 RID: 259
	public class ChromeSession : IDisposable
	{
		// Token: 0x0600082D RID: 2093 RVA: 0x000521D0 File Offset: 0x000503D0
		public ChromeSession(string sessionEndpoint)
		{
			this.SetActiveSession(sessionEndpoint);
			this.e = new WebSocket(this.b, "", null, null, "", "", -1, null);
			this.e.Opened += this.b;
			this.e.MessageReceived += this.a;
			this.e.Error += this.a;
			this.e.Closed += this.a;
			try
			{
				this.e.Open();
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog("webSocket.Open err: " + ex.ToString(), 1);
			}
			this.d = null;
			this.f.WaitOne(1000);
			if (this.d != null)
			{
				throw this.d;
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x000522FC File Offset: 0x000504FC
		public void Close()
		{
			this.e.Close();
			this.d = null;
			this.g.WaitOne(1000);
			if (this.d != null)
			{
				throw this.d;
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00052340 File Offset: 0x00050540
		public string SendCommand(string cmd)
		{
			this.d = null;
			string text;
			if (this.e.State == 1)
			{
				this.f.Reset();
				this.e.Send(cmd);
				this.f.WaitOne(1000, true);
				if (this.d != null)
				{
					throw this.d;
				}
				text = this.h;
			}
			else
			{
				text = null;
			}
			return text;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0000465F File Offset: 0x0000285F
		public void SetActiveSession(string sessionWSEndpoint)
		{
			this.b = sessionWSEndpoint.Replace("ws://localhost", "ws://127.0.0.1");
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x000523AC File Offset: 0x000505AC
		public long GetCommandId()
		{
			Interlocked.Increment(ref this.c);
			return this.c;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00004679 File Offset: 0x00002879
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x000523D0 File Offset: 0x000505D0
		public ExecuteJsResultInfo CompileScript(string js)
		{
			long commandId = this.GetCommandId();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["expression"] = js;
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			dictionary2["params"] = dictionary;
			dictionary2["id"] = commandId;
			dictionary2["method"] = "Runtime.evaluate";
			string text = JSON.Encode(dictionary2);
			string text2 = this.SendCommand(text);
			ExecuteJsResultInfo executeJsResultInfo = JSON.Decode<ExecuteJsResultInfo>(text2);
			if (executeJsResultInfo == null || executeJsResultInfo.result == null || !executeJsResultInfo.result.wasThrown)
			{
				return executeJsResultInfo;
			}
			if (executeJsResultInfo.result.exceptionDetails != null)
			{
				throw new Exception(string.Format("text:{0}\r\nline:{1} column:{2}", executeJsResultInfo.result.exceptionDetails.text, executeJsResultInfo.result.exceptionDetails.line, executeJsResultInfo.result.exceptionDetails.column));
			}
			throw new Exception(text2);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000524D4 File Offset: 0x000506D4
		[CompilerGenerated]
		private void b(object sender, EventArgs e)
		{
			try
			{
				this.f.Set();
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog("webSocket.Opened err: " + ex.ToString(), 1);
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0005251C File Offset: 0x0005071C
		[CompilerGenerated]
		private void a(object sender, MessageReceivedEventArgs e)
		{
			try
			{
				this.h = e.Message;
				this.f.Set();
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog("webSocket.MessageReceived err: " + ex.ToString(), 1);
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00052570 File Offset: 0x00050770
		[CompilerGenerated]
		private void a(object sender, ErrorEventArgs e)
		{
			string.Format("chrome err={0},message={1},websocketState={2}", new object[]
			{
				e.Exception.Message,
				this.h,
				this.e.State.ToString()
			});
			try
			{
				this.d = e.Exception;
				this.f.Set();
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog("webSocket.Error err: " + ex.ToString(), 1);
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00052608 File Offset: 0x00050808
		[CompilerGenerated]
		private void a(object sender, EventArgs e)
		{
			try
			{
				this.g.Set();
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog("webSocket.Closed err: " + ex.ToString(), 1);
			}
		}

		// Token: 0x04000507 RID: 1287
		private string b;

		// Token: 0x04000508 RID: 1288
		private long c = 0L;

		// Token: 0x04000509 RID: 1289
		private Exception d = null;

		// Token: 0x0400050A RID: 1290
		private WebSocket e;

		// Token: 0x0400050B RID: 1291
		private ManualResetEvent f = new ManualResetEvent(false);

		// Token: 0x0400050C RID: 1292
		private ManualResetEvent g = new ManualResetEvent(false);

		// Token: 0x0400050D RID: 1293
		private string h;
	}
}
