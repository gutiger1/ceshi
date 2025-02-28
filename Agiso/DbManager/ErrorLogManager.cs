using System;
using System.Data;
using Agiso.DBAccess;

namespace Agiso.DbManager
{
	// Token: 0x020006A4 RID: 1700
	public class ErrorLogManager
	{
		// Token: 0x060020F3 RID: 8435 RVA: 0x000568B0 File Offset: 0x00054AB0
		public static DataTable Get(DateTime date, out int count, int pageNo = 0, int pageSize = 0)
		{
			string text = string.Format("CreateTime BETWEEN {0} AND {1}", DbUtil.ToSqlString(date.Date.ToString("yyyy-MM-dd HH:mm:ss")), DbUtil.ToSqlString(date.Date.AddSeconds(86399.0).ToString("yyyy-MM-dd HH:mm:ss")));
			DataTable dataTable;
			if (pageNo > 0)
			{
				count = ErrorLogManager.b.ExecuteCount("ErrorLog", text);
				dataTable = ((count > 0) ? ErrorLogManager.b.ExecuteTable("IdNo, LogContent, CreateTime", "ErrorLog", text, "CreateTime DESC", (long)pageNo, (long)pageSize) : null);
			}
			else
			{
				dataTable = ErrorLogManager.b.ExecuteTable("IdNo, LogContent, CreateTime", "ErrorLog", text, "CreateTime DESC", 1L, 999L);
				count = ((dataTable != null) ? dataTable.Rows.Count : 0);
			}
			return dataTable;
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x0005699C File Offset: 0x00054B9C
		public static int Delete(DateTime endTime)
		{
			string text = "DELETE FROM ErrorLog WHERE CreateTime <= " + DbUtil.ToSqlString(endTime.ToString("yyyy-MM-dd HH:mm:ss"));
			return ErrorLogManager.b.ExecuteNonQuery(text);
		}

		// Token: 0x04001286 RID: 4742
		private static IDbAccess b = DbAccessDAL.CreateDbAccess();
	}
}
