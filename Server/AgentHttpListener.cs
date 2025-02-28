using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Agiso;
using Agiso.Utils;

namespace AliwwClient.Server
{
	// Token: 0x02000087 RID: 135
	public class AgentHttpListener
	{
		// Token: 0x17000048 RID: 72
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000346C File Offset: 0x0000166C
		private static List<string> AliCdnIps
		{
			[CompilerGenerated]
			set
			{
				AgentHttpListener.c = value;
			}
		} = new List<string>();

		// Token: 0x17000049 RID: 73
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00003474 File Offset: 0x00001674
		private static HashSet<string> ValidateAliCdnIps
		{
			[CompilerGenerated]
			set
			{
				AgentHttpListener.d = value;
			}
		} = new HashSet<string>();

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000347C File Offset: 0x0000167C
		public static ConcurrentQueue<RequestItem> RequestItemQueue { get; } = new ConcurrentQueue<RequestItem>();

		// Token: 0x060003AF RID: 943 RVA: 0x0003987C File Offset: 0x00037A7C
		private string a(string A_0)
		{
			ContentItem contentItem;
			if (AgentHttpListener.f.TryGetValue(A_0, out contentItem))
			{
				if (contentItem.ExpireTime > DateTime.Now)
				{
					contentItem.ExpireTime = DateTime.Now.AddMinutes(10.0);
					return contentItem.Content;
				}
				ContentItem contentItem2;
				AgentHttpListener.f.TryRemove(A_0, out contentItem2);
			}
			return null;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000398E4 File Offset: 0x00037AE4
		private void b(string A_0, string A_1)
		{
			if (!string.IsNullOrWhiteSpace(A_1))
			{
				AgentHttpListener.f.TryAdd(A_0, new ContentItem
				{
					Content = A_1,
					ExpireTime = DateTime.Now.AddMinutes(10.0)
				});
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00039930 File Offset: 0x00037B30
		public bool IsRuning
		{
			get
			{
				return this.b != null && this.b.IsAlive;
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00039954 File Offset: 0x00037B54
		public void Start()
		{
			AgentHttpListener.ChangeAlicdnLocalHostIp();
			this.a = new HttpListener();
			this.a.Prefixes.Add("http://g.alicdn.com:80/");
			this.a.Prefixes.Add("https://g.alicdn.com:443/");
			this.a.Prefixes.Add(string.Format("http://*:{0}/", AgentHttpListener.i));
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

		// Token: 0x060003B3 RID: 947 RVA: 0x00003483 File Offset: 0x00001683
		public void Stop()
		{
			this.g.Dispose();
			this.h.Dispose();
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

		// Token: 0x060003B4 RID: 948 RVA: 0x00039A14 File Offset: 0x00037C14
		protected void HttpHandle()
		{
			for (;;)
			{
				HttpListenerContext httpListenerContext = null;
				try
				{
					AgentHttpListener.a a = new AgentHttpListener.a();
					a.b = this;
					if (!this.a.IsListening)
					{
						break;
					}
					httpListenerContext = this.a.GetContext();
					a.a = httpListenerContext;
					Task.Run(new Action(a.c));
				}
				catch (Exception ex)
				{
					try
					{
						LogWriter.WriteLog(ex.ToString(), 1);
						this.a(httpListenerContext, "", false);
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00039AAC File Offset: 0x00037CAC
		private static HttpClient WwMsgclient
		{
			get
			{
				if (AgentHttpListener.k == null)
				{
					AgentHttpListener.k = new HttpClient();
					AgentHttpListener.k.BaseAddress = new Uri("http://wwmsg.agiso.com");
					AgentHttpListener.k.Timeout = TimeSpan.FromSeconds(5.0);
					AgentHttpListener.k.DefaultRequestHeaders.Referrer = new Uri("alires:///WebUI/chatnewmsg/recent.html?debug=true");
				}
				return AgentHttpListener.k;
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00039B1C File Offset: 0x00037D1C
		private string a()
		{
			if (string.IsNullOrEmpty(AgentHttpListener.j))
			{
				for (int i = 0; i < 2; i++)
				{
					try
					{
						AgentHttpListener.j = AgentHttpListener.WwMsgclient.GetStringAsync("/in").Result;
						if (!AgentHttpListener.j.Contains("__agi_ports"))
						{
							AgentHttpListener.j = "";
						}
						break;
					}
					catch (Exception ex)
					{
						LogWriter.WriteLog("获取wwmsg.agiso.com/in错误，" + ex.ToString(), 1);
					}
				}
			}
			return AgentHttpListener.j;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00039BB4 File Offset: 0x00037DB4
		private string a(string A_0, string A_1)
		{
			string text = this.a(A_0);
			string text2;
			if (text != null)
			{
				text2 = text;
			}
			else
			{
				AgentHttpListener.ChangeAlicdnLocalHostIp();
				if (AgentHttpListener.c.Count <= 0)
				{
					AppConfig.QnAgentServiceClient.AutoLoginError("agiso", null, "未获取到AliCdn的真实Ip", 2L, "", "");
					text2 = "";
				}
				else
				{
					List<string> list = AgentHttpListener.d.Union(AgentHttpListener.c.Except(AgentHttpListener.d)).ToList<string>();
					foreach (string text3 in list)
					{
						string text4 = A_0.Replace("g.alicdn.com", text3);
						HttpWebRequest httpWebRequest;
						if (text4.StartsWith("https", StringComparison.OrdinalIgnoreCase))
						{
							ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AgentHttpListener.<>c.<>9.a);
							httpWebRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(text4));
						}
						else
						{
							httpWebRequest = (HttpWebRequest)WebRequest.Create(text4);
						}
						httpWebRequest.Method = "GET";
						httpWebRequest.Timeout = 10000;
						httpWebRequest.Host = "g.alicdn.com";
						try
						{
							using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
							{
								text = Util.GetResponseAsString(httpWebResponse, Encoding.UTF8);
								if (string.IsNullOrEmpty(text))
								{
									text = " ";
								}
								this.b(A_0, text);
								AgentHttpListener.d.Add(text3);
								return text;
							}
						}
						catch (Exception ex)
						{
							LogWriter.WriteLog(string.Format("reqUrl“{0}”请求异常，异常信息：{1}", text4, ex), 1);
						}
					}
					text2 = "";
				}
			}
			return text2;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00039DC4 File Offset: 0x00037FC4
		private void a(HttpListenerContext A_0, string A_1, bool A_2 = false)
		{
			if (A_0 != null)
			{
				try
				{
					A_0.Response.Headers.Add("Access-Control-Allow-Origin", "*");
					if (A_2)
					{
						A_0.Response.Headers.Add("Cache-Control", "max-age=2592000,s-maxage=3600");
					}
					using (StreamWriter streamWriter = new StreamWriter(A_0.Response.OutputStream, Encoding.UTF8))
					{
						streamWriter.WriteLine(A_1);
					}
				}
				catch (Exception ex)
				{
					if (!(ex is HttpListenerException) || (!ex.Message.Contains("企图在不存在的网络连接上进行操作") && !ex.Message.Contains("设备不识别此命令")))
					{
						LogWriter.WriteLog(ex.ToString(), 1);
					}
				}
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00039EA4 File Offset: 0x000380A4
		public static void ChangeAlicdnLocalHostIp()
		{
			if (!AppConfig.LocalPc && AgentHttpListener.c.Count <= 0)
			{
				object obj = AgentHttpListener.l;
				lock (obj)
				{
					if (AgentHttpListener.c.Count <= 0)
					{
						string alicdnLocalHostIp = AppConfig.GetAlicdnLocalHostIp();
						if (!string.IsNullOrEmpty(alicdnLocalHostIp))
						{
							LogWriter.WriteLog("开始还原hosts", 1);
							string text;
							AgentHttpListener.a("", out text);
							if (!string.IsNullOrEmpty(text))
							{
								return;
							}
							Thread.Sleep(1000);
						}
						AgentHttpListener.AliCdnIps = Dns.GetHostAddresses("g.alicdn.com").Where(new Func<IPAddress, bool>(AgentHttpListener.<>c.<>9.b)).Select(new Func<IPAddress, string>(AgentHttpListener.<>c.<>9.a))
							.ToList<string>();
						string text2;
						AgentHttpListener.a("127.0.0.1", out text2);
						if (!string.IsNullOrEmpty(text2))
						{
							AgentHttpListener.a("127.0.0.1", out text2);
						}
						if (!string.IsNullOrEmpty(text2))
						{
							LogWriter.WriteLog("修改hosts失败，" + text2, 1);
						}
						else
						{
							LogWriter.WriteLog("修改hosts完成", 1);
						}
					}
				}
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0003A01C File Offset: 0x0003821C
		private static bool a(string A_0, out string A_1)
		{
			string text = "C:\\Windows\\System32\\drivers\\etc\\hosts";
			string text2 = "C:\\Windows\\System32\\drivers\\etc\\hosts.back";
			A_1 = "";
			try
			{
				A_0 = ((A_0 != null) ? A_0.Trim() : null);
				if (!string.IsNullOrEmpty(A_0))
				{
					Regex regex = new Regex("\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}");
					Match match = regex.Match(A_0);
					if (!match.Success)
					{
						A_1 = "重定向aliCdn的IP格式有误";
						return false;
					}
				}
				File.Copy(text, text2, true);
				string text3 = "";
				string text4 = "";
				using (FileStream fileStream = new FileStream(text, FileMode.OpenOrCreate, FileAccess.ReadWrite))
				{
					using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
					{
						text4 = (text3 = streamReader.ReadToEnd());
						Regex regex2 = new Regex("(\\r\\n)+\\s*(?<ip>\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})\\s+g\\.alicdn\\.com\\s*");
						MatchCollection matchCollection = regex2.Matches(text4);
						if (matchCollection.Count > 0)
						{
							for (int i = 0; i < matchCollection.Count; i++)
							{
								Match match2 = matchCollection[i];
								if (matchCollection.Count == 1 && match2.Groups["ip"].Value == A_0)
								{
									return true;
								}
								string value = match2.Groups[0].Value;
								text3 = text3.Replace(value, "");
							}
							if (!string.IsNullOrEmpty(A_0))
							{
								text3 = text3 + "\r\n" + A_0 + " g.alicdn.com";
							}
						}
						else if (!string.IsNullOrEmpty(A_0))
						{
							text3 = text3 + "\r\n" + A_0 + " g.alicdn.com";
						}
					}
				}
				if (text3 != text4)
				{
					using (new FileStream(text, FileMode.Truncate, FileAccess.ReadWrite))
					{
					}
					using (FileStream fileStream3 = new FileStream(text, FileMode.Append, FileAccess.Write))
					{
						using (StreamWriter streamWriter = new StreamWriter(fileStream3, Encoding.Default))
						{
							streamWriter.Write(text3);
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog("重定向alicdn失败，" + ex.ToString(), 1);
				A_1 = "重定向alicdn失败";
				try
				{
					File.Copy(text2, text, true);
				}
				catch (Exception ex2)
				{
					LogWriter.WriteLog("重定向alicdn失败，" + ex2.ToString(), 1);
				}
				return false;
			}
			finally
			{
				File.Delete(text2);
			}
			return true;
		}

		// Token: 0x04000330 RID: 816
		private HttpListener a;

		// Token: 0x04000331 RID: 817
		private Thread b;

		// Token: 0x04000332 RID: 818
		[CompilerGenerated]
		private static List<string> c;

		// Token: 0x04000333 RID: 819
		[CompilerGenerated]
		private static HashSet<string> d;

		// Token: 0x04000334 RID: 820
		[CompilerGenerated]
		private static readonly ConcurrentQueue<RequestItem> e;

		// Token: 0x04000335 RID: 821
		[CompilerGenerated]
		private static readonly ConcurrentDictionary<string, ContentItem> f = new ConcurrentDictionary<string, ContentItem>();

		// Token: 0x04000336 RID: 822
		private Timer g = new Timer(new TimerCallback(AgentHttpListener.<>c.<>9.b), null, TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(30.0));

		// Token: 0x04000337 RID: 823
		private Timer h = new Timer(new TimerCallback(AgentHttpListener.<>c.<>9.a), null, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(10.0));

		// Token: 0x04000338 RID: 824
		[CompilerGenerated]
		private static readonly int i = 17532;

		// Token: 0x04000339 RID: 825
		private static string j = "";

		// Token: 0x0400033A RID: 826
		private static HttpClient k;

		// Token: 0x0400033B RID: 827
		private static object l = new object();

		// Token: 0x02000089 RID: 137
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x060003C5 RID: 965 RVA: 0x0003A51C File Offset: 0x0003871C
			internal void c()
			{
				/*
An exception occurred when decompiling this method (060003C5)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void AliwwClient.Server.AgentHttpListener/a::c()

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 50
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 403
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 278
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 117
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 88
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 92
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1683
*/;
			}

			// Token: 0x04000342 RID: 834
			public HttpListenerContext a;

			// Token: 0x04000343 RID: 835
			public AgentHttpListener b;
		}
	}
}
