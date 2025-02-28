using System;
using System.Data;
using Agiso.DBAccess;
using Agiso.Object;

namespace Agiso.DbManager
{
	// Token: 0x020006AA RID: 1706
	public class SystemSettingsManager
	{
		// Token: 0x06002115 RID: 8469 RVA: 0x00057B54 File Offset: 0x00055D54
		public static DataRow SelectOneRow()
		{
			DataTable dataTable;
			for (;;)
			{
				dataTable = SystemSettingsManager.a.ExecuteTable("SELECT * FROM SystemSettings LIMIT 1");
				if (dataTable == null)
				{
					break;
				}
				if (!dataTable.Columns.Contains("Option1"))
				{
					SystemSettingsManager.a.ExecuteNonQuery("ALTER TABLE 'SystemSettings' ADD 'Option1' INTEGER;");
					SystemSettingsManager.a.ExecuteNonQuery("UPDATE SystemSettings SET Option1 = 3;");
					SystemSettingsManager.a.ExecuteNonQuery("ALTER TABLE 'AutoReply' ADD 'Option1' INTEGER;");
				}
				else if (!dataTable.Columns.Contains("MsgSendFirstTime"))
				{
					SystemSettingsManager.a.ExecuteNonQuery("ALTER TABLE 'SystemSettings' ADD 'MsgSendFirstTime' TEXT;");
				}
				else if (!dataTable.Columns.Contains("ManualNick"))
				{
					SystemSettingsManager.a.ExecuteNonQuery("ALTER TABLE 'SystemSettings' ADD 'ManualNick' TEXT;");
				}
				else
				{
					if (dataTable.Columns.Contains("SendInterval"))
					{
						goto IL_00EB;
					}
					SystemSettingsManager.a.ExecuteNonQuery("ALTER TABLE 'SystemSettings' ADD 'SendInterval' INTEGER;");
				}
			}
			return null;
			IL_00EB:
			DataRow dataRow;
			if (dataTable.Rows.Count == 0)
			{
				dataRow = dataTable.NewRow();
			}
			else
			{
				dataRow = dataTable.Rows[0];
			}
			return dataRow;
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x00057C74 File Offset: 0x00055E74
		public static SystemSettingsInfo SelectOne()
		{
			return SystemSettingsManager.Map(SystemSettingsManager.SelectOneRow());
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x00057C90 File Offset: 0x00055E90
		public static SystemSettingsInfo Map(DataRow row)
		{
			SystemSettingsInfo systemSettingsInfo;
			if (row == null)
			{
				systemSettingsInfo = null;
			}
			else
			{
				SystemSettingsInfo systemSettingsInfo2 = new SystemSettingsInfo();
				systemSettingsInfo2.AliwwMessageLengthMax = DbUtil.TrimIntNull(row["AliwwMessageLengthMax"]);
				systemSettingsInfo2.CloseWindowBeforeSend = DbUtil.TrimIntNull(row["CloseWindowBeforeSend"]);
				systemSettingsInfo2.CloseWindowWhenSended = DbUtil.TrimIntNull(row["CloseWindowWhenSended"]);
				systemSettingsInfo2.CreateTime = DbUtil.TrimDateNull(row["CreateTime"]);
				systemSettingsInfo2.IdNo = DbUtil.TrimLongNull(row["IdNo"]);
				systemSettingsInfo2.ManualNick = DbUtil.TrimNull(row["ManualNick"]);
				systemSettingsInfo2.ModifyTime = DbUtil.TrimDateNull(row["ModifyTime"]);
				systemSettingsInfo2.MsgSendFirstTime = DbUtil.TrimNull(row["MsgSendFirstTime"]);
				if (row.Table.Columns.Contains("InsertMsgSuccInterval"))
				{
					systemSettingsInfo2.InsertMsgSuccInterval = DbUtil.TrimIntNull(row["InsertMsgSuccInterval"]);
				}
				systemSettingsInfo2.Option1 = DbUtil.TrimLongNull(row["Option1"]);
				systemSettingsInfo2.SendInterval = DbUtil.TrimIntNull(row["SendInterval"]);
				if (systemSettingsInfo2.SendInterval < 0)
				{
					systemSettingsInfo2.SendInterval = 0;
				}
				if (systemSettingsInfo2.SendInterval <= 5)
				{
					systemSettingsInfo2.SendInterval *= 1000;
				}
				if (systemSettingsInfo2.SendInterval <= 500)
				{
					systemSettingsInfo2.SendInterval = 500;
				}
				if (systemSettingsInfo2.SendInterval > 5000)
				{
					systemSettingsInfo2.SendInterval = 5000;
				}
				string text = DbUtil.TrimNull(row["SendMessageHotKey"]);
				if (text.ToUpper().Equals("^{ENTER}"))
				{
					systemSettingsInfo2.SendMessageHotKey = "^{ENTER}";
				}
				else
				{
					systemSettingsInfo2.SendMessageHotKey = "{ENTER}";
				}
				systemSettingsInfo2.CurrentVersion = DbUtil.TrimIntNull(row["CurrentVersion"]);
				if (row.Table.Columns.Contains("PeriodOfTime"))
				{
					systemSettingsInfo2.PeriodOfTime = DbUtil.TrimIntNull(row["PeriodOfTime"]);
				}
				if (row.Table.Columns.Contains("SameQueryReplyInterval"))
				{
					systemSettingsInfo2.SameQueryReplyInterval = DbUtil.TrimIntNull(row["SameQueryReplyInterval"]);
				}
				if (row.Table.Columns.Contains("TransferInterval"))
				{
					systemSettingsInfo2.TransferInterval = DbUtil.TrimIntNull(row["TransferInterval"]);
				}
				if (row.Table.Columns.Contains("RecvMsgReplyInterval"))
				{
					systemSettingsInfo2.RecvMsgReplyInterval = DbUtil.TrimIntNull(row["RecvMsgReplyInterval"]);
				}
				if (row.Table.Columns.Contains("NoMatchReplyInterval"))
				{
					systemSettingsInfo2.NoMatchReplyInterval = DbUtil.TrimIntNull(row["NoMatchReplyInterval"]);
				}
				if (row.Table.Columns.Contains("FirstReplyInterval"))
				{
					systemSettingsInfo2.FirstReplyInterval = DbUtil.TrimIntNull(row["FirstReplyInterval"]);
				}
				systemSettingsInfo = systemSettingsInfo2;
			}
			return systemSettingsInfo;
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x00057FA4 File Offset: 0x000561A4
		public static int Update(SystemSettingsInfo info)
		{
			int num;
			if (info == null)
			{
				num = 0;
			}
			else
			{
				string text = string.Concat(new string[]
				{
					"\r\nUPDATE SystemSettings\r\nSET SendMessageHotKey = ",
					DbUtil.ToSqlString(info.SendMessageHotKey),
					"\r\n    , CloseWindowWhenSended = ",
					DbUtil.ToSqlString(info.CloseWindowWhenSended),
					"\r\n    , CloseWindowBeforeSend = ",
					DbUtil.ToSqlString(info.CloseWindowBeforeSend),
					"\r\n    , AliwwMessageLengthMax = ",
					DbUtil.ToSqlString(info.AliwwMessageLengthMax),
					"\r\n    , ModifyTime = ",
					DbUtil.ToSqlString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
					"\r\n    , Option1 = ",
					DbUtil.ToSqlString(info.Option1),
					"\r\n    , ManualNick = ",
					DbUtil.ToSqlString(info.ManualNick),
					"\r\n    , SendInterval = ",
					DbUtil.ToSqlString(info.SendInterval),
					"\r\n    , CurrentVersion = ",
					DbUtil.ToSqlString(info.CurrentVersion),
					"\r\n    , InsertMsgSuccInterval = ",
					DbUtil.ToSqlString(info.InsertMsgSuccInterval),
					"\r\n    , SameQueryReplyInterval = ",
					DbUtil.ToSqlString(info.SameQueryReplyInterval),
					"\r\n    , TransferInterval = ",
					DbUtil.ToSqlString(info.TransferInterval),
					"\r\n    , RecvMsgReplyInterval = ",
					DbUtil.ToSqlString(info.RecvMsgReplyInterval),
					"\r\n    , NoMatchReplyInterval = ",
					DbUtil.ToSqlString(info.NoMatchReplyInterval),
					"\r\n    , FirstReplyInterval = ",
					DbUtil.ToSqlString(info.FirstReplyInterval),
					"\r\nWHERE IdNo = ",
					DbUtil.ToSqlString(info.IdNo)
				});
				num = SystemSettingsManager.a.ExecuteNonQuery(text);
			}
			return num;
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x000581B0 File Offset: 0x000563B0
		public static bool UpdateTransferInterval(long idNo, int transferInterval)
		{
			string text = string.Concat(new string[]
			{
				"\r\nUpdate SystemSettings\r\nSET transferInterval = ",
				DbUtil.ToSqlString(transferInterval),
				", PeriodOfTime = 0\r\nWHERE IdNo = ",
				DbUtil.ToSqlString(idNo),
				"\r\n"
			});
			try
			{
				return SystemSettingsManager.a.ExecuteNonQuery(text) > 0;
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x00058228 File Offset: 0x00056428
		public static void Init()
		{
			try
			{
				SystemSettingsManager.a.ExecuteRow("SELECT CurrentVersion FROM SystemSettings");
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("CurrentVersion"))
				{
					SystemSettingsManager.a.ExecuteNonQuery("ALTER TABLE SystemSettings ADD COLUMN CurrentVersion INTEGER");
				}
			}
			string text = string.Concat(new string[]
			{
				"INSERT OR IGNORE INTO SystemSettings (IdNo, SendMessageHotKey, Option1, CloseWindowWhenSended, CloseWindowBeforeSend, AliwwMessageLengthMax, MsgSendFirstTime, ModifyTime, CreateTime, CurrentVersion)VALUES (1, '{ENTER}', 3843, 1, 0, 800, '回复 1、提货\r\n回复 2、了解提货方法\r\n回复 3、找店主',",
				DbUtil.ToSqlString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
				",",
				DbUtil.ToSqlString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
				",",
				DbUtil.ToSqlString(50607),
				")"
			});
			SystemSettingsManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x0400128C RID: 4748
		private static IDbAccess a = DbAccessDAL.CreateDbAccess();
	}
}
