using System;
using System.Collections.Concurrent;
using System.Data;
using Agiso.DBAccess;
using Agiso.Extensions;
using Agiso.Object;

namespace Agiso.DbManager
{
	// Token: 0x0200069C RID: 1692
	public class AldsAccountManager
	{
		// Token: 0x060020AC RID: 8364 RVA: 0x00054178 File Offset: 0x00052378
		public static DataTable Select()
		{
			DataTable dataTable = AldsAccountManager.a.ExecuteTable("*", "AldsAccount", "1=1");
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					object obj2 = dataRow["Password"];
					string text = ((obj2 != null) ? obj2.ToString().smethod_1("782ki934", "8ht9zh90") : null);
					dataRow["Password"] = text;
				}
			}
			return dataTable;
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x00054244 File Offset: 0x00052444
		public static string SelectUsersString()
		{
			object obj = AldsAccountManager.a.ExecuteScalar("SELECT group_concat( (UserNick || ',' || Password), ';' ) FROM AldsAccount");
			return DbUtil.TrimNull(obj);
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x0005426C File Offset: 0x0005246C
		public static int Delete(string userNick)
		{
			string text = string.Format("DELETE FROM AldsAccount WHERE UserNick={0}", DbUtil.ToSqlString(userNick));
			return AldsAccountManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x00054298 File Offset: 0x00052498
		public static int Insert(AldsAccountInfo account)
		{
			account.NotDutyNickReplyMsg = "抱歉，我的主人没有告诉我人工客服是谁。暂时与我联系吧。";
			string password = account.Password;
			string text = ((password != null) ? password.smethod_0("782ki934", "8ht9zh90") : null);
			account.CreateTime = (account.ModifyTime = DateTime.Now);
			string text2 = string.Concat(new string[]
			{
				"INSERT OR REPLACE INTO AldsAccount (UserNick, Password, Option1, NotDutyNickReplyMsg, ModifyTime, CreateTime) \r\nVALUES(",
				DbUtil.ToSqlString(account.UserNick),
				",",
				DbUtil.ToSqlString(text),
				",",
				DbUtil.ToSqlString(account.Option1),
				"\r\n,",
				DbUtil.ToSqlString(account.NotDutyNickReplyMsg),
				",",
				DbUtil.ToSqlString(account.ModifyTime),
				",",
				DbUtil.ToSqlString(account.CreateTime),
				")"
			});
			return AldsAccountManager.a.ExecuteNonQuery(text2);
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000543A0 File Offset: 0x000525A0
		public static int Update(AldsAccountInfo account)
		{
			string text = string.Concat(new string[]
			{
				"UPDATE AldsAccount SET Option1=",
				DbUtil.ToSqlString(account.Option1),
				",ManualNick=",
				DbUtil.ToSqlString(account.ManualNick),
				"\r\n,ModifyTime=",
				DbUtil.ToSqlString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
				",QnAccountPwd=",
				DbUtil.ToSqlString(account.QnAccountPwd),
				"\r\n,DefaultMouldId=",
				DbUtil.ToSqlString(account.DefaultMouldId),
				",TransferNickIfNotDuty=",
				DbUtil.ToSqlString(account.TransferNickIfNotDuty),
				"\r\n,NotDutyNickReplyMsg=",
				DbUtil.ToSqlString(account.NotDutyNickReplyMsg),
				"\r\nWHERE UserNick=",
				DbUtil.ToSqlString(account.UserNick)
			});
			return AldsAccountManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x000544A0 File Offset: 0x000526A0
		public static int UpdatePassword(AldsAccountInfo account)
		{
			string password = account.Password;
			string text = ((password != null) ? password.smethod_0("782ki934", "8ht9zh90") : null);
			string text2 = "UPDATE AldsAccount SET Password=" + DbUtil.ToSqlString(text) + " WHERE UserNick=" + DbUtil.ToSqlString(account.UserNick);
			return AldsAccountManager.a.ExecuteNonQuery(text2);
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x000544FC File Offset: 0x000526FC
		public static ConcurrentDictionary<string, AldsAccountInfo> SelectDict()
		{
			ConcurrentDictionary<string, AldsAccountInfo> concurrentDictionary = new ConcurrentDictionary<string, AldsAccountInfo>();
			DataTable dataTable = AldsAccountManager.Select();
			ConcurrentDictionary<string, AldsAccountInfo> concurrentDictionary2;
			if (dataTable == null)
			{
				concurrentDictionary2 = concurrentDictionary;
			}
			else
			{
				bool flag = false;
				if (!dataTable.Columns.Contains("Option1"))
				{
					AldsAccountManager.a.ExecuteNonQuery("ALTER TABLE 'AldsAccount' ADD 'Option1' INTEGER DEFAULT 1;");
					flag = true;
				}
				if (!dataTable.Columns.Contains("ManualNick"))
				{
					AldsAccountManager.a.ExecuteNonQuery("ALTER TABLE 'AldsAccount' ADD 'ManualNick' TEXT;");
					flag = true;
				}
				if (!dataTable.Columns.Contains("QnAccountPwd"))
				{
					AldsAccountManager.a.ExecuteNonQuery("ALTER TABLE 'AldsAccount' ADD 'QnAccountPwd' TEXT;");
					flag = true;
				}
				if (flag)
				{
					concurrentDictionary2 = AldsAccountManager.SelectDict();
				}
				else
				{
					if (DbUtil.HasMoreRow(dataTable))
					{
						foreach (object obj in dataTable.Rows)
						{
							DataRow dataRow = (DataRow)obj;
							AldsAccountInfo aldsAccountInfo = AldsAccountManager.Map(dataRow);
							concurrentDictionary.TryAdd(aldsAccountInfo.UserNick, aldsAccountInfo);
						}
					}
					concurrentDictionary2 = concurrentDictionary;
				}
			}
			return concurrentDictionary2;
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x00054620 File Offset: 0x00052820
		public static AldsAccountInfo Map(DataRow row)
		{
			AldsAccountInfo aldsAccountInfo;
			if (row == null)
			{
				aldsAccountInfo = null;
			}
			else
			{
				AldsAccountInfo aldsAccountInfo2 = new AldsAccountInfo();
				aldsAccountInfo2.UserNick = DbUtil.TrimNull(row["UserNick"]);
				aldsAccountInfo2.Password = DbUtil.TrimNull(row["Password"]);
				aldsAccountInfo2.Idx = DbUtil.TrimLongNull(row["Idx"]);
				aldsAccountInfo2.Option1 = DbUtil.TrimLongNull(row["Option1"]);
				aldsAccountInfo2.ManualNick = DbUtil.TrimNull(row["ManualNick"]);
				aldsAccountInfo2.QnAccountPwd = DbUtil.TrimNull(row["QnAccountPwd"]);
				aldsAccountInfo2.ModifyTime = DbUtil.TrimDateNull(row["ModifyTime"]);
				aldsAccountInfo2.CreateTime = DbUtil.TrimDateNull(row["CreateTime"]);
				if (row.Table.Columns.Contains("DefaultMouldId"))
				{
					aldsAccountInfo2.DefaultMouldId = new long?((long)DbUtil.TrimIntNull(row["DefaultMouldId"]));
				}
				if (row.Table.Columns.Contains("NotDutyNickReplyMsg"))
				{
					aldsAccountInfo2.NotDutyNickReplyMsg = DbUtil.TrimNull(row["NotDutyNickReplyMsg"]);
				}
				aldsAccountInfo2.TransferNickIfNotDuty = DbUtil.TrimNull(row["TransferNickIfNotDuty"]);
				aldsAccountInfo = aldsAccountInfo2;
			}
			return aldsAccountInfo;
		}

		// Token: 0x0400127B RID: 4731
		private static IDbAccess a = DbAccessDAL.CreateDbAccess();
	}
}
