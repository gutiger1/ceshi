using System;
using System.Data;
using Agiso.DBAccess;
using Agiso.Object;

namespace Agiso.DbManager
{
	// Token: 0x020006A6 RID: 1702
	public class LogQnAliresRecodeManager
	{
		// Token: 0x060020FB RID: 8443 RVA: 0x00056C54 File Offset: 0x00054E54
		public static int Insert(LogQnAliresRecode log)
		{
			string text = string.Concat(new string[]
			{
				DbUtil.ToSqlString(log.FileName),
				"\r\n, ",
				DbUtil.ToSqlString(log.FileModifyTime.ToString("yyyy-MM-dd HH:mm:ss")),
				"\r\n, ",
				DbUtil.ToSqlString(log.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")),
				"\r\n, ",
				DbUtil.ToSqlString(log.ModifyVersion),
				"\r\n, ",
				DbUtil.ToSqlString(log.ModifyJsUrl),
				"\r\n, ",
				DbUtil.ToSqlString(log.ModifyJsUrl2)
			});
			string text2 = "INSERT INTO LogQnAliresRecode (FileName, FileModifyTime, CreateTime, ModifyVersion, ModifyJsUrl, ModifyJsUrl2) VALUES (" + text + ");";
			return LogQnAliresRecodeManager.a.ExecuteNonQuery(text2);
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x00056D34 File Offset: 0x00054F34
		public static int Update(string fileName, DateTime fileModifyTime, int modifyVersion, string modifyJsUrl, string ModifyJsUrl2)
		{
			string text = string.Concat(new string[]
			{
				"\r\nUPDATE LogQnAliresRecode\r\n   SET FileModifyTime = ",
				DbUtil.ToSqlString(fileModifyTime.ToString("yyyy-MM-dd HH:mm:ss")),
				"\r\n, ModifyVersion = ",
				DbUtil.ToSqlString(modifyVersion),
				"\r\n, ModifyJsUrl = ",
				DbUtil.ToSqlString(modifyJsUrl),
				"\r\n, ModifyJsUrl2 = ",
				DbUtil.ToSqlString(ModifyJsUrl2),
				"\r\n WHERE FileName = ",
				DbUtil.ToSqlString(fileName),
				"\r\n"
			});
			return LogQnAliresRecodeManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x00056DD4 File Offset: 0x00054FD4
		public static LogQnAliresRecode GetLog(string fileName)
		{
			string text = "SELECT FileModifyTime, ModifyVersion, ModifyJsUrl, ModifyJsUrl2 FROM LogQnAliresRecode WHERE FileName = " + DbUtil.ToSqlString(fileName) + ";";
			DataRow dataRow = LogQnAliresRecodeManager.a.ExecuteRow(text);
			LogQnAliresRecode logQnAliresRecode;
			if (dataRow != null)
			{
				logQnAliresRecode = new LogQnAliresRecode
				{
					FileModifyTime = DbUtil.TrimDateNull(dataRow["FileModifyTime"]),
					ModifyVersion = DbUtil.TrimIntNull(dataRow["ModifyVersion"]),
					ModifyJsUrl = DbUtil.TrimNull(dataRow["ModifyJsUrl"]),
					ModifyJsUrl2 = DbUtil.TrimNull(dataRow["ModifyJsUrl2"])
				};
			}
			else
			{
				logQnAliresRecode = null;
			}
			return logQnAliresRecode;
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x00056E78 File Offset: 0x00055078
		public static int Delete(string fileName)
		{
			string text = "DELETE FROM LogQnAliresRecode WHERE FileName = " + DbUtil.ToSqlString(fileName) + ";";
			return LogQnAliresRecodeManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x04001288 RID: 4744
		private static IDbAccess a = DbAccessDAL.CreateDbAccess();
	}
}
