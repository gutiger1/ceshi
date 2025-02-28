using System;
using System.Configuration;
using Agiso.Utils;

namespace Agiso.DBAccess
{
	// Token: 0x020000FA RID: 250
	public abstract class DbAccessDAL
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0004F680 File Offset: 0x0004D880
		public static ConnectionStringSettings ConnStringSettings
		{
			get
			{
				if (DbAccessDAL.a == null || string.IsNullOrEmpty(DbAccessDAL.a.ConnectionString))
				{
					DbAccessDAL.a = ConfigurationManager.ConnectionStrings["DataConnectionString"];
					if (DbAccessDAL.a == null || string.IsNullOrEmpty(DbAccessDAL.a.ConnectionString))
					{
						DbAccessDAL.a = new ConnectionStringSettings("DataConnectionString", "Data Source=|DataDirectory|\\data.db;Version=3;New=True;", "System.Data.SQLite");
					}
				}
				return DbAccessDAL.a;
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0004F6FC File Offset: 0x0004D8FC
		public static IDbAccess CreateDbAccess()
		{
			IDbAccess dbAccess = null;
			string providerName = DbAccessDAL.ConnStringSettings.ProviderName;
			string text = providerName;
			if (!(text == "Microsoft.SqlServerCe.Client") && !(text == "Microsoft.SqlServerCe.Client.3.5"))
			{
				if (text == "System.Data.SQLite")
				{
				}
				dbAccess = new SqliteAccess();
			}
			dbAccess.SetConnString(DbAccessDAL.ConnStringSettings.ConnectionString);
			return dbAccess;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0004F75C File Offset: 0x0004D95C
		public static bool InitDb(string[] sqlArr)
		{
			bool flag = true;
			IDbAccess dbAccess = DbAccessDAL.CreateDbAccess();
			foreach (string text in sqlArr)
			{
				if (!string.IsNullOrEmpty(text))
				{
					try
					{
						dbAccess.ExecuteNonQuery(text);
					}
					catch (Exception ex)
					{
						LogWriter.WriteLog(ex.ToString(), 1);
						flag = false;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0004F7C0 File Offset: 0x0004D9C0
		public static string SqlSafeFormat(string strInput)
		{
			string text;
			if (string.IsNullOrEmpty(strInput))
			{
				text = "";
			}
			else
			{
				strInput = strInput.Trim().Replace("'", "''");
				if (DbAccessDAL.ConnStringSettings != null && !string.IsNullOrEmpty(DbAccessDAL.ConnStringSettings.ProviderName))
				{
					string providerName = DbAccessDAL.ConnStringSettings.ProviderName;
					string text2 = providerName;
					if (!(text2 == "System.Data.SqlClient") && !(text2 == "Microsoft.SqlServerCe.Client") && !(text2 == "Microsoft.SqlServerCe.Client.3.5") && text2 == "MySql.Data.MySqlClient")
					{
						strInput = strInput.Replace("\\", "\\\\");
					}
				}
				text = strInput;
			}
			return text;
		}

		// Token: 0x040004EF RID: 1263
		private static ConnectionStringSettings a;
	}
}
