using System;
using System.Data;
using Agiso.DBAccess;
using Agiso.Object;

namespace Agiso.DbManager
{
	// Token: 0x0200069D RID: 1693
	public class AliwwTalkClickPointManager
	{
		// Token: 0x060020B6 RID: 8374 RVA: 0x00054778 File Offset: 0x00052978
		public static int Insert(string userNick, uint pointX, uint pointY)
		{
			string text = string.Format("INSERT OR IGNORE INTO AliwwTalkClickPoint(UserNick, PointX, PointY, CreateTime) VALUES ({0}, {1}, {2}, {3})", new object[]
			{
				DbUtil.ToSqlString(userNick),
				DbUtil.ToSqlString(pointX),
				DbUtil.ToSqlString(pointY),
				DbUtil.ToSqlString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
			});
			return AliwwTalkClickPointManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x000547E4 File Offset: 0x000529E4
		public static int Update(string userNick, uint pointX, uint pointY)
		{
			string text = string.Format("UPDATE AliwwTalkClickPoint SET PointX = {0}, PointY = {1} WHERE UserNick = {2}", DbUtil.ToSqlString(pointX), DbUtil.ToSqlString(pointY), DbUtil.ToSqlString(userNick));
			return AliwwTalkClickPointManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x00054828 File Offset: 0x00052A28
		public static AliwwTalkClickPointInfo Load(string userNick)
		{
			string text = "SELECT * FROM AliwwTalkClickPoint WHERE UserNick=" + DbUtil.ToSqlString(userNick);
			DataRow dataRow = AliwwTalkClickPointManager.a.ExecuteRow(text);
			return AliwwTalkClickPointManager.Map(dataRow);
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x0005485C File Offset: 0x00052A5C
		public static AliwwTalkClickPointInfo Map(DataRow row)
		{
			AliwwTalkClickPointInfo aliwwTalkClickPointInfo;
			if (row == null)
			{
				aliwwTalkClickPointInfo = null;
			}
			else
			{
				aliwwTalkClickPointInfo = new AliwwTalkClickPointInfo
				{
					UserNick = DbUtil.TrimNull(row["UserNick"]),
					PointX = DbUtil.TrimUintNull(row["PointX"]),
					PointY = DbUtil.TrimUintNull(row["PointY"]),
					CreateTime = DbUtil.TrimDateNull(row["CreateTime"])
				};
			}
			return aliwwTalkClickPointInfo;
		}

		// Token: 0x0400127C RID: 4732
		private static IDbAccess a = DbAccessDAL.CreateDbAccess();
	}
}
