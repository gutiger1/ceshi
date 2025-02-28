using System;
using System.Collections;
using System.Collections.Generic;
using Agiso;
using Agiso.Utils;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace AliwwClient.WebSocketServer
{
	// Token: 0x02000077 RID: 119
	public class LoginBehavior : WebSocketBehavior
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000366 RID: 870 RVA: 0x000379C4 File Offset: 0x00035BC4
		public Dictionary<string, string> DicWebSocketString
		{
			get
			{
				if (this.a == null)
				{
					this.a = new Dictionary<string, string>();
				}
				return this.a;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000379F0 File Offset: 0x00035BF0
		public string CurrUrl
		{
			get
			{
				if (string.IsNullOrEmpty(this.b) && (DateTime.Now - this._getLastUrltime).TotalMilliseconds > 500.0)
				{
					this.GetCurrUrl();
				}
				return this.b;
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00003358 File Offset: 0x00001558
		protected override void OnOpen()
		{
			this.GetCurrUrl();
			AppConfig.AliwwWebScoketServer.LoginSession = this;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00037A40 File Offset: 0x00035C40
		protected override void OnClose(CloseEventArgs e)
		{
			if (AppConfig.AliwwWebScoketServer.LoginSession == this)
			{
				AppConfig.AliwwWebScoketServer.LoginSession = null;
			}
			AppConfig.WriteLog("loginWs连接被关闭了，" + JSON.Encode(e), LogType.LogLoginWs, 1);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00037A84 File Offset: 0x00035C84
		protected override void OnError(ErrorEventArgs e)
		{
			if (AppConfig.AliwwWebScoketServer.LoginSession == this)
			{
				AppConfig.AliwwWebScoketServer.LoginSession = null;
			}
			AppConfig.WriteLog("loginWs连接出错了，" + e.Message + "：：" + e.Exception.ToString(), LogType.LogLoginWs, 1);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00037AD8 File Offset: 0x00035CD8
		protected override void OnMessage(MessageEventArgs e)
		{
			Hashtable hashtable = null;
			try
			{
				hashtable = JSON.Decode(e.Data) as Hashtable;
			}
			catch
			{
			}
			if (hashtable == null)
			{
				LogWriter.WriteLog("ReceivedFromQnMsg：" + e.Data, 1);
			}
			else if (hashtable["type"] != null)
			{
				string text = Convert.ToString(hashtable["guid"]);
				if (string.IsNullOrEmpty(text))
				{
					LogWriter.WriteLog("guid为空", 1);
				}
				object obj = hashtable["type"];
				object obj2 = obj;
				string text2 = obj2 as string;
				if (text2 != null)
				{
					if (!(text2 == "getCurrUrlRsp"))
					{
						if ((text2 == "getHtmlRsp" || text2 == "getSideOffsetRsp" || text2 == "getImageCaptchaRsp") && this.DicWebSocketString.ContainsKey(text))
						{
							this.DicWebSocketString[text] = Convert.ToString(hashtable["result"]);
						}
					}
					else if (this.DicWebSocketString.ContainsKey(text))
					{
						this.b = Convert.ToString(hashtable["result"]);
						this.DicWebSocketString.Remove(text);
						AppConfig.WriteLog("loginWs已连接，" + this.b, LogType.LogLoginWs, 1);
					}
				}
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00037C48 File Offset: 0x00035E48
		public void GetCurrUrl()
		{
			string text = Guid.NewGuid().ToString().Replace("-", "");
			this.DicWebSocketString[text] = null;
			if (this.SendTo(JSON.Encode(new g<string, string>("getCurrUrl", text))))
			{
				this._getLastUrltime = DateTime.Now;
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00037CAC File Offset: 0x00035EAC
		public bool SendTo(string data)
		{
			bool flag;
			try
			{
				base.Send(data);
				flag = true;
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(this.CurrUrl + "：" + ex.ToString(), 1);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0400030D RID: 781
		public const string Path = "/Login";

		// Token: 0x0400030E RID: 782
		private Dictionary<string, string> a;

		// Token: 0x0400030F RID: 783
		public DateTime _getLastUrltime = DateTime.Now;

		// Token: 0x04000310 RID: 784
		private string b;
	}
}
