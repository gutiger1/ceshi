using System;
using System.Collections.Concurrent;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;
using Agiso;
using Agiso.DBAccess;
using Agiso.Utils;
using Newtonsoft.Json.Linq;

namespace AliwwClient.Manager
{
	// Token: 0x020000B9 RID: 185
	public class ProxyXmlManager
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00003E20 File Offset: 0x00002020
		public static string ProxyXmlPath
		{
			get
			{
				return Path.Combine(AppConfig.AliWorkbenchDataPath, "System\\proxy.xml") ?? "";
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00041B04 File Offset: 0x0003FD04
		public static ProxyInfo Get()
		{
			ProxyXmlManager.a a = new ProxyXmlManager.a();
			string proxyXmlPath = ProxyXmlManager.ProxyXmlPath;
			ProxyInfo proxyInfo;
			if (!File.Exists(proxyXmlPath))
			{
				proxyInfo = null;
			}
			else
			{
				FileInfo fileInfo = new FileInfo(proxyXmlPath);
				XmlDocument xmlDocument = new XmlDocument();
				try
				{
					xmlDocument.Load(proxyXmlPath);
				}
				catch (Exception ex)
				{
					string text = "加载代理文件失败，";
					Exception ex2 = ex;
					LogWriter.WriteLog(text + ((ex2 != null) ? ex2.ToString() : null), 1);
					File.Delete(proxyXmlPath);
					return null;
				}
				a.a = new ProxyInfo();
				XmlNode xmlNode = xmlDocument.SelectSingleNode("/xparam/bUse");
				if (xmlNode != null)
				{
					a.a.BUse = Util.ToInt(xmlNode.InnerText) > 0;
				}
				XmlNode xmlNode2 = xmlDocument.SelectSingleNode("/xparam/proxy");
				if (xmlNode2 != null)
				{
					XmlNode xmlNode3 = xmlNode2.SelectSingleNode("type");
					if (xmlNode3 != null)
					{
						a.a.Type = Util.ToInt(xmlNode3.InnerText);
					}
					XmlNode xmlNode4 = xmlNode2.SelectSingleNode("host");
					if (xmlNode4 != null)
					{
						a.a.Host = xmlNode4.InnerText;
					}
					XmlNode xmlNode5 = xmlNode2.SelectSingleNode("port");
					if (xmlNode5 != null)
					{
						a.a.Port = Util.ToInt(xmlNode5.InnerText);
					}
					XmlNode xmlNode6 = xmlNode2.SelectSingleNode("user");
					if (xmlNode6 != null)
					{
						a.a.User = xmlNode6.InnerText;
					}
					XmlNode xmlNode7 = xmlNode2.SelectSingleNode("pass");
					if (xmlNode7 != null)
					{
						a.a.Pass = xmlNode7.InnerText;
					}
				}
				ProxyXmlManager.a.AddOrUpdate(fileInfo.LastWriteTime, a.a, new Func<DateTime, ProxyInfo, ProxyInfo>(a.b));
				proxyInfo = a.a;
			}
			return proxyInfo;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00041CDC File Offset: 0x0003FEDC
		public static bool Save(string host, int port, out bool needToRestart)
		{
			needToRestart = false;
			bool flag;
			try
			{
				ProxyInfo proxyInfo = ProxyXmlManager.Get();
				if (proxyInfo != null && proxyInfo.Host == host && proxyInfo.Port == port && proxyInfo.BUse)
				{
					flag = true;
				}
				else
				{
					string text = Path.Combine(AppConfig.AliWorkbenchDataPath, "System") ?? "";
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					string proxyXmlPath = ProxyXmlManager.ProxyXmlPath;
					if (!File.Exists(proxyXmlPath))
					{
						using (File.Create(proxyXmlPath))
						{
							goto IL_00A5;
						}
					}
					File.Delete(proxyXmlPath);
					using (File.Create(proxyXmlPath))
					{
					}
					IL_00A5:
					XmlDocument xmlDocument = new XmlDocument();
					XmlElement xmlElement = xmlDocument.CreateElement("xparam");
					xmlDocument.AppendChild(xmlElement);
					xmlElement.AppendChild(ProxyXmlManager.a(xmlDocument, "bUse", "1"));
					XmlNode xmlNode = xmlElement.AppendChild(ProxyXmlManager.a(xmlDocument, "proxy", ""));
					xmlNode.AppendChild(ProxyXmlManager.a(xmlDocument, "type", "4"));
					xmlNode.AppendChild(ProxyXmlManager.a(xmlDocument, "host", host));
					xmlNode.AppendChild(ProxyXmlManager.a(xmlDocument, "port", port.ToString()));
					xmlNode.AppendChild(ProxyXmlManager.a(xmlDocument, "user", ""));
					xmlNode.AppendChild(ProxyXmlManager.a(xmlDocument, "pass", ""));
					xmlNode.AppendChild(ProxyXmlManager.a(xmlDocument, "domain", ""));
					xmlDocument.Save(proxyXmlPath);
					needToRestart = true;
					flag = true;
				}
			}
			catch (Exception ex)
			{
				string text2 = "修改代理失败，";
				Exception ex2 = ex;
				LogWriter.WriteLog(text2 + ((ex2 != null) ? ex2.ToString() : null), 1);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00041EF8 File Offset: 0x000400F8
		public static bool Delete()
		{
			string proxyXmlPath = ProxyXmlManager.ProxyXmlPath;
			bool flag;
			if (File.Exists(proxyXmlPath))
			{
				File.Delete(proxyXmlPath);
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00041F20 File Offset: 0x00040120
		private static XmlElement a(XmlDocument A_0, string A_1, string A_2)
		{
			XmlElement xmlElement = A_0.CreateElement(A_1);
			xmlElement.InnerText = A_2;
			return xmlElement;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00041F40 File Offset: 0x00040140
		public static void Handle()
		{
			if (AppConfig.AgentProxyInfo != null)
			{
				bool flag;
				ProxyXmlManager.a(AppConfig.AgentProxyInfo.ProxyIp, AppConfig.AgentProxyInfo.Port, out flag);
				bool flag2;
				ProxyXmlManager.Save(AppConfig.AgentProxyInfo.ProxyIp, AppConfig.AgentProxyInfo.Port, out flag2);
				ProxyInfo proxyInfo = ProxyXmlManager.Get();
				LogWriter.WriteLog((proxyInfo != null) ? ("代理信息：" + JSON.Encode(proxyInfo)) : "代理信息：null", 1);
				if (!flag && !flag2)
				{
					return;
				}
				k.a().ClearAllQnProc(false, true, true, true, "重新设置千牛代理，杀掉所有千牛");
				Thread.Sleep(20);
				string text = Path.Combine(AppConfig.AliWorkbenchDataPath, "AppData\\Global");
				if (!Directory.Exists(text))
				{
					return;
				}
				try
				{
					Directory.Delete(text, true);
					return;
				}
				catch
				{
					return;
				}
			}
			ProxyXmlManager.a();
			ProxyInfo proxyInfo2 = ProxyXmlManager.Get();
			if (proxyInfo2 != null && proxyInfo2.BUse && ProxyXmlManager.Delete())
			{
				k.a().ClearAllQnProc(false, true, true, true, "删除了千牛代理，杀掉所有千牛");
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00042050 File Offset: 0x00040250
		private static void a(string A_0, int A_1, out bool A_2)
		{
			A_2 = false;
			string text = Path.Combine(AppConfig.AliWorkbenchDataPath, "Global.db");
			if (File.Exists(text))
			{
				SqliteAccess sqliteAccess = new SqliteAccess();
				sqliteAccess.SetConnString("Data Source=" + Path.Combine(AppConfig.AliWorkbenchDataPath, "Global.db") + ";Version=3;New=True;");
				DataRow dataRow = sqliteAccess.ExecuteRow("select * from key2value where key = 'NetProxyConfig'");
				object obj = dataRow["value"];
				string text2 = ((obj != null) ? obj.ToString() : null);
				if (!string.IsNullOrEmpty(text2))
				{
					JObject jobject = JSON.Decode<JObject>(text2);
					int num = Util.ToInt(jobject.GetValue("type"));
					JToken value = jobject.GetValue("host");
					string text3 = ((value != null) ? value.ToString() : null);
					int num2 = Util.ToInt(jobject.GetValue("port"));
					if (num != 2 || text3 != A_0 || num2 != A_1)
					{
						b<int, string, string, string, string, string> b = new b<int, string, string, string, string, string>(2, A_0, A_1.ToString(), "", "", "");
						sqliteAccess.ExecuteNonQuery("update key2value set value = '" + JSON.Encode(b) + "' where key = 'NetProxyConfig'");
						A_2 = true;
					}
				}
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00042188 File Offset: 0x00040388
		private static void a()
		{
			string text = Path.Combine(AppConfig.AliWorkbenchDataPath, "Global.db");
			if (File.Exists(text))
			{
				SqliteAccess sqliteAccess = new SqliteAccess();
				sqliteAccess.SetConnString("Data Source=" + Path.Combine(AppConfig.AliWorkbenchDataPath, "Global.db") + ";Version=3;New=True;");
				b<int, string, string, string, string, string> b = new b<int, string, string, string, string, string>(0, "", "", "", "", "");
				sqliteAccess.ExecuteNonQuery("update key2value set value = '" + JSON.Encode(b) + "' where key = 'NetProxyConfig'");
			}
		}

		// Token: 0x040003FF RID: 1023
		private static ConcurrentDictionary<DateTime, ProxyInfo> a = new ConcurrentDictionary<DateTime, ProxyInfo>();

		// Token: 0x020000BA RID: 186
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x06000573 RID: 1395 RVA: 0x00003E48 File Offset: 0x00002048
			internal ProxyInfo b(DateTime A_0, ProxyInfo A_1)
			{
				return this.a;
			}

			// Token: 0x04000400 RID: 1024
			public ProxyInfo a;
		}
	}
}
