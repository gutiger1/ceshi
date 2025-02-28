using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Agiso.DbManager;
using Agiso.Object;

namespace AliwwClient.Cache
{
	// Token: 0x020000A5 RID: 165
	public class LogQnAliresRecodeCache
	{
		// Token: 0x1700009A RID: 154
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x00003A49 File Offset: 0x00001C49
		private static Dictionary<string, LogQnAliresRecode> DictLogQnAlires
		{
			[CompilerGenerated]
			set
			{
				LogQnAliresRecodeCache.a = value;
			}
		} = new Dictionary<string, LogQnAliresRecode>();

		// Token: 0x060004A8 RID: 1192 RVA: 0x0003D5FC File Offset: 0x0003B7FC
		public static LogQnAliresRecode GetLog(string fileName)
		{
			LogQnAliresRecode logQnAliresRecode;
			if (LogQnAliresRecodeCache.a.ContainsKey(fileName))
			{
				logQnAliresRecode = LogQnAliresRecodeCache.a[fileName];
			}
			else
			{
				LogQnAliresRecode log = LogQnAliresRecodeManager.GetLog(fileName);
				if (log == null)
				{
					LogQnAliresRecodeCache.a[fileName] = log;
				}
				logQnAliresRecode = log;
			}
			return logQnAliresRecode;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0003D640 File Offset: 0x0003B840
		public static int Update(string fileName, DateTime fileModifyTime, int modifyVersion, string modifyJsUrl, string modifyJsUrl2)
		{
			int num = LogQnAliresRecodeManager.Update(fileName, fileModifyTime, modifyVersion, modifyJsUrl, modifyJsUrl2);
			if (num > 0 && LogQnAliresRecodeCache.a.ContainsKey(fileName))
			{
				LogQnAliresRecode logQnAliresRecode = LogQnAliresRecodeCache.a[fileName];
				logQnAliresRecode.FileModifyTime = fileModifyTime;
				logQnAliresRecode.ModifyVersion = modifyVersion;
				logQnAliresRecode.ModifyJsUrl = modifyJsUrl;
				logQnAliresRecode.ModifyJsUrl2 = modifyJsUrl2;
				LogQnAliresRecodeCache.a[fileName] = logQnAliresRecode;
			}
			return num;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0003D6A8 File Offset: 0x0003B8A8
		public static int Insert(string fileName, DateTime fileModifyTime, int modifyVersion, string modifyJsUrl, string modifyJsUrl2)
		{
			LogQnAliresRecode logQnAliresRecode = new LogQnAliresRecode();
			logQnAliresRecode.FileName = fileName;
			logQnAliresRecode.FileModifyTime = fileModifyTime;
			logQnAliresRecode.ModifyVersion = modifyVersion;
			logQnAliresRecode.ModifyJsUrl = modifyJsUrl;
			logQnAliresRecode.ModifyJsUrl2 = modifyJsUrl2;
			logQnAliresRecode.CreateTime = DateTime.Now;
			LogQnAliresRecodeCache.a[fileName] = logQnAliresRecode;
			int num;
			try
			{
				num = LogQnAliresRecodeManager.Insert(logQnAliresRecode);
			}
			catch
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x040003B2 RID: 946
		[CompilerGenerated]
		private static Dictionary<string, LogQnAliresRecode> a;
	}
}
