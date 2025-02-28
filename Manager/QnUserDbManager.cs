using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Agiso.DBAccess;
using Agiso.Utils;
using Newtonsoft.Json;

namespace AliwwClient.Manager
{
	// Token: 0x020000BC RID: 188
	public static class QnUserDbManager
	{
		// Token: 0x06000581 RID: 1409 RVA: 0x0004221C File Offset: 0x0004041C
		public static void SetConfig(string key, string value, long? userId = null)
		{
			IEnumerable<DirectoryInfo> enumerable = QnUserDbManager.a(userId);
			if (enumerable != null)
			{
				try
				{
					foreach (DirectoryInfo directoryInfo in enumerable)
					{
						string text = "Data Source=" + Path.Combine(directoryInfo.FullName, "user.db") + ";Version=3;";
						SqliteAccess sqliteAccess = new SqliteAccess();
						sqliteAccess.SetConnString(text);
						DataRow dataRow = sqliteAccess.ExecuteRow("SELECT * FROM key2value WHERE key = " + DbUtil.ToSqlString(key));
						if (dataRow == null)
						{
							sqliteAccess.ExecuteNonQuery(string.Concat(new string[]
							{
								"INSERT INTO key2value ( key, value) VALUES (",
								DbUtil.ToSqlString(key),
								",",
								DbUtil.ToSqlString(value),
								")"
							}));
						}
						else
						{
							string text2 = DbUtil.TrimNull(dataRow["value"]);
							if (text2 != value)
							{
								sqliteAccess.ExecuteUpdate("key2value", "value = " + DbUtil.ToSqlString(value), "key = " + DbUtil.ToSqlString(key));
							}
						}
					}
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog(string.Format("set config error，{0}", ex), 1);
				}
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00042398 File Offset: 0x00040598
		public static void DelConfig(string key, long? userId = null)
		{
			IEnumerable<DirectoryInfo> enumerable = QnUserDbManager.a(userId);
			if (enumerable != null)
			{
				try
				{
					foreach (DirectoryInfo directoryInfo in enumerable)
					{
						string text = "Data Source=" + Path.Combine(directoryInfo.FullName, "user.db") + ";Version=3;";
						SqliteAccess sqliteAccess = new SqliteAccess();
						sqliteAccess.SetConnString(text);
						sqliteAccess.ExecuteNonQuery("DELETE FROM key2value WHERE key = " + DbUtil.ToSqlString(key));
					}
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog(string.Format("del config error，{0}", ex), 1);
				}
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00042460 File Offset: 0x00040660
		public static string GetConfig(string key, long userId)
		{
			IEnumerable<DirectoryInfo> enumerable = QnUserDbManager.a(new long?(userId));
			DirectoryInfo directoryInfo = ((enumerable != null) ? enumerable.FirstOrDefault<DirectoryInfo>() : null);
			string text;
			if (directoryInfo == null)
			{
				text = null;
			}
			else
			{
				try
				{
					string text2 = "Data Source=" + Path.Combine(directoryInfo.FullName, "user.db") + ";Version=3;";
					SqliteAccess sqliteAccess = new SqliteAccess();
					sqliteAccess.SetConnString(text2);
					text = DbUtil.TrimNull(sqliteAccess.ExecuteScalar("SELECT value FROM key2value WHERE key = " + DbUtil.ToSqlString(key)));
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog(string.Format("get config error，{0}", ex), 1);
					text = null;
				}
			}
			return text;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00042514 File Offset: 0x00040714
		private static IEnumerable<DirectoryInfo> a(long? A_0)
		{
			QnUserDbManager.b b = new QnUserDbManager.b();
			b.a = A_0;
			IEnumerable<DirectoryInfo> enumerable;
			if (!Directory.Exists("C:\\Users\\Public\\Documents\\AliWorkbench"))
			{
				enumerable = null;
			}
			else
			{
				string text = "C:\\Users\\Public\\Documents\\AliWorkbench\\AliWorkbenchData";
				string text2 = Path.Combine("C:\\Users\\Public\\Documents\\AliWorkbench", "DataConfig.ini");
				if (File.Exists(text2))
				{
					string text3 = File.ReadAllText(text2, Encoding.Default);
					Match match = QnUserDbManager.f.Match(text3);
					if (match.Success)
					{
						text = match.Groups["path"].Value;
					}
				}
				string text4 = Path.Combine(text, "NewAppData");
				if (!Directory.Exists(text4))
				{
					enumerable = null;
				}
				else
				{
					string[] directories = Directory.GetDirectories(text4);
					IEnumerable<DirectoryInfo> enumerable2 = directories.Select(new Func<string, c<string, DirectoryInfo>>(QnUserDbManager.<>c.<>9.a)).Where(new Func<c<string, DirectoryInfo>, bool>(b.b)).Select(new Func<c<string, DirectoryInfo>, DirectoryInfo>(QnUserDbManager.<>c.<>9.a));
					enumerable = enumerable2;
				}
			}
			return enumerable;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00003EB6 File Offset: 0x000020B6
		public static void UseNativeMessageList(long? userId = null)
		{
			QnUserDbManager.DelConfig("usenativemessagelist", userId);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00003EC4 File Offset: 0x000020C4
		public static void DisableUseNativeMessageList(long? userId = null)
		{
			QnUserDbManager.SetConfig("usenativemessagelist", "false", userId);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00003ED8 File Offset: 0x000020D8
		public static void SetAldsPluginFirst(long? userId = null)
		{
			QnUserDbManager.SetConfig("chatextendsort", QnUserDbManager.e, userId);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0004262C File Offset: 0x0004082C
		public static bool IsAldsPluginFirst(long userId)
		{
			string config = QnUserDbManager.GetConfig("chatextendsort", userId);
			bool flag;
			if (string.IsNullOrEmpty(config))
			{
				flag = false;
			}
			else
			{
				Dictionary<string, Dictionary<string, QnUserDbManager.a>> dictionary;
				try
				{
					dictionary = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, QnUserDbManager.a>>>(config.ToString());
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog(string.Format("反序列化失败，{0}", ex), 1);
					return false;
				}
				if (!dictionary.ContainsKey("0"))
				{
					LogWriter.WriteLog("未检测到键值：0", 1);
					flag = false;
				}
				else
				{
					Dictionary<string, QnUserDbManager.a> dictionary2 = dictionary["0"];
					QnUserDbManager.a value = dictionary2.FirstOrDefault(new Func<KeyValuePair<string, QnUserDbManager.a>, bool>(QnUserDbManager.<>c.<>9.a)).Value;
					if (value == null)
					{
						flag = false;
					}
					else
					{
						QnUserDbManager.a a = null;
						foreach (KeyValuePair<string, QnUserDbManager.a> keyValuePair in dictionary2)
						{
							if (a == null || keyValuePair.Value.c() > a.c())
							{
								a = keyValuePair.Value;
							}
						}
						flag = a.d() == value.d();
					}
				}
			}
			return flag;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0004277C File Offset: 0x0004097C
		static QnUserDbManager()
		{
			Dictionary<string, Dictionary<string, QnUserDbManager.a>> dictionary = new Dictionary<string, Dictionary<string, QnUserDbManager.a>>();
			string text = "0";
			Dictionary<string, QnUserDbManager.a> dictionary2 = new Dictionary<string, QnUserDbManager.a>();
			string text2 = "ww_plugin_9124";
			QnUserDbManager.a a = new QnUserDbManager.a();
			a.c("ww_plugin_9124");
			a.c(10001);
			dictionary2.Add(text2, a);
			string text3 = "ww_plugin_9294";
			QnUserDbManager.a a2 = new QnUserDbManager.a();
			a2.c("ww_plugin_9294");
			a2.c(10002);
			dictionary2.Add(text3, a2);
			dictionary.Add(text, dictionary2);
			dictionary.Add("-1", new Dictionary<string, QnUserDbManager.a>());
			Dictionary<string, Dictionary<string, QnUserDbManager.a>> dictionary3 = dictionary;
			QnUserDbManager.e = JsonConvert.SerializeObject(dictionary3);
		}

		// Token: 0x04000407 RID: 1031
		private static Regex c = new Regex("^\\d+#3$");

		// Token: 0x04000408 RID: 1032
		private static readonly string e;

		// Token: 0x04000409 RID: 1033
		public const string UsenativemessagelistKey = "usenativemessagelist";

		// Token: 0x0400040A RID: 1034
		public const string ChatextendsortKey = "chatextendsort";

		// Token: 0x0400040B RID: 1035
		private static Regex f = new Regex("data_path_v2=(?<path>[^\r\n]+)");

		// Token: 0x020000BD RID: 189
		private class a
		{
			// Token: 0x0600058A RID: 1418 RVA: 0x00003EEB File Offset: 0x000020EB
			[CompilerGenerated]
			public string d()
			{
				return this.a;
			}

			// Token: 0x0600058B RID: 1419 RVA: 0x00003EF3 File Offset: 0x000020F3
			[CompilerGenerated]
			public void c(string A_0)
			{
				this.a = A_0;
			}

			// Token: 0x0600058C RID: 1420 RVA: 0x00003EFC File Offset: 0x000020FC
			[CompilerGenerated]
			public int c()
			{
				return this.b;
			}

			// Token: 0x0600058D RID: 1421 RVA: 0x00003F04 File Offset: 0x00002104
			[CompilerGenerated]
			public void c(int A_0)
			{
				this.b = A_0;
			}

			// Token: 0x0400040C RID: 1036
			[CompilerGenerated]
			private string a;

			// Token: 0x0400040D RID: 1037
			[CompilerGenerated]
			private int b;
		}

		// Token: 0x020000BF RID: 191
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x06000595 RID: 1429 RVA: 0x00042830 File Offset: 0x00040A30
			internal bool b(c<string, DirectoryInfo> A_0)
			{
				return (this.a == null || this.a.Value <= 0L) ? QnUserDbManager.c.IsMatch(A_0.b.Name) : (A_0.b.Name == string.Format("{0}#3", this.a));
			}

			// Token: 0x04000412 RID: 1042
			public long? a;
		}
	}
}
