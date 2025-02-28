using System;
using System.Data;
using Agiso.DBAccess;
using Agiso.Object;

namespace Agiso.DbManager
{
	// Token: 0x020006A9 RID: 1705
	public class LogSyncEmotionManager
	{
		// Token: 0x0600210F RID: 8463 RVA: 0x0005797C File Offset: 0x00055B7C
		private static int a(string A_0, string A_1, DateTime A_2)
		{
			string text = string.Concat(new string[]
			{
				"INSERT OR IGNORE INTO LogSyncEmotion (SellerNick, LastUserNick, LastSyncTime)\r\nVALUES (",
				DbUtil.ToSqlString(A_0),
				"\r\n,",
				DbUtil.ToSqlString(A_1),
				"\r\n,",
				DbUtil.ToSqlString(A_2.ToString("yyyy-MM-dd HH:mm:ss")),
				")"
			});
			return LogSyncEmotionManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000579F0 File Offset: 0x00055BF0
		public static LogSyncEmotion Get(string sellerNick)
		{
			string text = "SELECT * FROM LogSyncEmotion WHERE SellerNick = " + DbUtil.ToSqlString(sellerNick);
			DataRow dataRow = LogSyncEmotionManager.a.ExecuteRow(text);
			return LogSyncEmotionManager.a(dataRow);
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x00057A24 File Offset: 0x00055C24
		public static int UpdateOrInsert(string sellerNick, string lastUserNick, DateTime lastSyncTime)
		{
			string text = string.Concat(new string[]
			{
				"UPDATE LogSyncEmotion\r\nSET LastUserNick = ",
				DbUtil.ToSqlString(lastUserNick),
				", LastSyncTime = ",
				DbUtil.ToSqlString(lastSyncTime.ToString("yyyy-MM-dd HH:mm:ss")),
				"\r\nWHERE SellerNick = ",
				DbUtil.ToSqlString(sellerNick)
			});
			int num = LogSyncEmotionManager.a.ExecuteNonQuery(text);
			int num2;
			if (num <= 0)
			{
				num2 = LogSyncEmotionManager.a(sellerNick, lastUserNick, lastSyncTime);
			}
			else
			{
				num2 = num;
			}
			return num2;
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x00057AA4 File Offset: 0x00055CA4
		private static LogSyncEmotion a(DataRow A_0)
		{
			LogSyncEmotion logSyncEmotion;
			if (A_0 == null)
			{
				logSyncEmotion = null;
			}
			else
			{
				LogSyncEmotion logSyncEmotion2 = new LogSyncEmotion();
				if (A_0.Table.Columns.Contains("SellerNick"))
				{
					logSyncEmotion2.SellerNick = DbUtil.TrimNull(A_0["SellerNick"]);
				}
				if (A_0.Table.Columns.Contains("LastUserNick"))
				{
					logSyncEmotion2.LastUserNick = DbUtil.TrimNull(A_0["LastUserNick"]);
				}
				if (A_0.Table.Columns.Contains("LastSyncTime"))
				{
					logSyncEmotion2.LastSyncTime = DbUtil.TrimDateNull(A_0["LastSyncTime"]);
				}
				logSyncEmotion = logSyncEmotion2;
			}
			return logSyncEmotion;
		}

		// Token: 0x0400128B RID: 4747
		private static IDbAccess a = DbAccessDAL.CreateDbAccess();
	}
}
