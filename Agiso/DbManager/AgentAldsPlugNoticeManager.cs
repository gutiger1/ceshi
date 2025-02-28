using System;
using System.Data;
using Agiso.DBAccess;
using Agiso.Object;

namespace Agiso.DbManager
{
	// Token: 0x0200069A RID: 1690
	public class AgentAldsPlugNoticeManager
	{
		// Token: 0x060020A2 RID: 8354 RVA: 0x00053F30 File Offset: 0x00052130
		public static int Insert(string userNick, DateTime noticeTime)
		{
			string text = string.Concat(new string[]
			{
				"INSERT OR IGNORE INTO agentaldsplugnotice (usernick, noticetime) VALUES (",
				DbUtil.ToSqlString(userNick),
				",",
				DbUtil.ToSqlString(noticeTime),
				")"
			});
			return AgentAldsPlugNoticeManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x00053F88 File Offset: 0x00052188
		public static AgentAldsPlugNotice Get(string userNick)
		{
			string text = "SELECT * FROM agentaldsplugnotice WHERE UserNick = " + DbUtil.ToSqlString(userNick);
			DataRow dataRow = AgentAldsPlugNoticeManager.a.ExecuteRow(text);
			return AgentAldsPlugNoticeManager.a(dataRow);
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x00053FBC File Offset: 0x000521BC
		public static int Update(string userNick, DateTime noticeTime)
		{
			string text = "Update agentaldsplugnotice SET NoticeTime = " + DbUtil.ToSqlString(noticeTime) + " WHERE UserNick = " + DbUtil.ToSqlString(userNick);
			return AgentAldsPlugNoticeManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x00053FFC File Offset: 0x000521FC
		private static AgentAldsPlugNotice a(DataRow A_0)
		{
			AgentAldsPlugNotice agentAldsPlugNotice;
			if (A_0 == null)
			{
				agentAldsPlugNotice = null;
			}
			else
			{
				AgentAldsPlugNotice agentAldsPlugNotice2 = new AgentAldsPlugNotice();
				AgentAldsPlugNotice agentAldsPlugNotice3 = agentAldsPlugNotice2;
				object obj = A_0["UserNick"];
				agentAldsPlugNotice3.UserNick = ((obj != null) ? obj.ToString() : null);
				agentAldsPlugNotice2.NoticeTime = DbUtil.TrimDateNull(A_0["NoticeTime"]);
				agentAldsPlugNotice = agentAldsPlugNotice2;
			}
			return agentAldsPlugNotice;
		}

		// Token: 0x04001279 RID: 4729
		private static IDbAccess a = DbAccessDAL.CreateDbAccess();
	}
}
