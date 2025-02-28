using System;
using System.Data;
using Agiso.DBAccess;
using Agiso.Object;

namespace Agiso.DbManager
{
	// Token: 0x0200069B RID: 1691
	public class AliwwUserInfoManager
	{
		// Token: 0x060020A8 RID: 8360 RVA: 0x00054054 File Offset: 0x00052254
		public static bool InserOrIgnore(string userNick, long userId)
		{
			string text = string.Concat(new string[]
			{
				"INSERT OR IGNORE INTO AliwwUserInfo (UserNick, UserId, CreateTime) VALUES (",
				DbUtil.ToSqlString(userNick),
				",",
				DbUtil.ToSqlString(userId),
				",",
				DbUtil.ToSqlString(DateTime.Now),
				")"
			});
			return AliwwUserInfoManager.b.ExecuteNonQuery(text) > 0;
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x000540C8 File Offset: 0x000522C8
		public static AliwwUserInfo Get(string userNick)
		{
			string text = "SELECT * FROM AliwwUserInfo WHERE UserNick = " + DbUtil.ToSqlString(userNick) + " OR UserNick = " + DbUtil.ToSqlString(userNick.ToLower());
			DataRow dataRow = AliwwUserInfoManager.b.ExecuteRow(text);
			AliwwUserInfo aliwwUserInfo2;
			if (dataRow != null)
			{
				AliwwUserInfo aliwwUserInfo = new AliwwUserInfo
				{
					IdNo = DbUtil.TrimLongNull(dataRow["IdNo"]),
					UserNick = DbUtil.TrimNull(dataRow["UserNick"]),
					UserId = DbUtil.TrimLongNull(dataRow["UserId"]),
					CreateTime = DbUtil.TrimDateNull(dataRow["CreateTime"])
				};
				aliwwUserInfo2 = aliwwUserInfo;
			}
			else
			{
				aliwwUserInfo2 = null;
			}
			return aliwwUserInfo2;
		}

		// Token: 0x0400127A RID: 4730
		private static IDbAccess b = DbAccessDAL.CreateDbAccess();
	}
}
