using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using Agiso;
using Agiso.Utils;

namespace AliwwClient.Server
{
	// Token: 0x0200008C RID: 140
	public class HttpServerBase
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x00003546 File Offset: 0x00001746
		public HttpServerBase(int httpPort)
		{
			this.c = httpPort;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0003A7C0 File Offset: 0x000389C0
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x00003561 File Offset: 0x00001761
		public int HttpPort
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0003A7D8 File Offset: 0x000389D8
		public bool IsRuning
		{
			get
			{
				return this.c > 0 && this.b != null && this.b.IsAlive;
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0003A804 File Offset: 0x00038A04
		public void Start()
		{
			this.a = new HttpListener();
			this.a.Prefixes.Add(string.Format("http://*:{0}/", this.HttpPort));
			try
			{
				this.a.Start();
				this.b = new Thread(new ThreadStart(this.HttpHandle));
				this.b.Start();
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000356A File Offset: 0x0000176A
		public void Stop()
		{
			HttpListener httpListener = this.a;
			if (httpListener != null)
			{
				httpListener.Close();
			}
			Thread thread = this.b;
			if (thread != null)
			{
				thread.Abort();
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0003A894 File Offset: 0x00038A94
		protected virtual void HttpHandle()
		{
			for (;;)
			{
				HttpListenerContext httpListenerContext = null;
				try
				{
					httpListenerContext = this.a.GetContext();
					AjaxResult ajaxResult = new AjaxResult();
					HttpListenerRequest request = httpListenerContext.Request;
					if (!request.IsLocal)
					{
						string text = request.RemoteEndPoint.Address.ToString();
						if (!AppConfig.AgentControlServerIPs.Contains(text))
						{
							using (StreamWriter streamWriter = new StreamWriter(httpListenerContext.Response.OutputStream, Encoding.UTF8))
							{
								streamWriter.WriteLine(ajaxResult.CreateErrArro("无权访问").Encode());
								continue;
							}
						}
					}
					string text2 = request.RawUrl;
					int num = text2.IndexOf('?');
					if (num >= 0)
					{
						text2 = text2.Substring(num + 1);
					}
					NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(text2);
					foreach (string text3 in nameValueCollection.AllKeys)
					{
						nameValueCollection[text3] = HttpUtility.UrlDecode(nameValueCollection[text3]);
					}
					httpListenerContext.Response.StatusCode = 200;
					httpListenerContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
					using (StreamWriter streamWriter2 = new StreamWriter(httpListenerContext.Response.OutputStream, Encoding.UTF8))
					{
						streamWriter2.WriteLine(new AjaxResult().CreateSuccArro("connected").Encode());
					}
				}
				catch (Exception ex)
				{
					try
					{
						LogWriter.WriteLog(ex.ToString(), 1);
						if (httpListenerContext != null)
						{
							using (StreamWriter streamWriter3 = new StreamWriter(httpListenerContext.Response.OutputStream, Encoding.UTF8))
							{
								streamWriter3.WriteLine(new AjaxResult().CreateErrArro("error").Encode());
							}
						}
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x04000349 RID: 841
		private HttpListener a;

		// Token: 0x0400034A RID: 842
		private Thread b;

		// Token: 0x0400034B RID: 843
		private int c = 32100;

		// Token: 0x0200008D RID: 141
		// (Invoke) Token: 0x060003DA RID: 986
		public delegate T Func1<out T>();
	}
}
