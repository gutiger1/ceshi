using System;
using System.Data;
using Agiso.DBAccess;
using Agiso.Object;
using Agiso.Utils;

namespace Agiso.DbManager
{
	// Token: 0x020006A7 RID: 1703
	public class LogSendResultManager
	{
		// Token: 0x06002101 RID: 8449 RVA: 0x00056EAC File Offset: 0x000550AC
		public static int Insert(long msgId, string userNick, int resultCode, string resultMsg, MsgSendSoftware sendSoftware = MsgSendSoftware.Undefined)
		{
			string text = string.Format("INSERT INTO LogSendResult (MsgId, UserNick, SendResultCode, SendResultMsg, CreateTimeLocal, SendSoftware) VALUES ({0},{1},{2},{3},{4},{5});", new object[]
			{
				DbUtil.ToSqlString(msgId),
				DbUtil.ToSqlString(userNick),
				DbUtil.ToSqlString(resultCode),
				DbUtil.ToSqlString(resultMsg),
				DbUtil.ToSqlString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
				DbUtil.ToSqlString((int)sendSoftware)
			});
			int num;
			try
			{
				num = LogSendResultManager.a.ExecuteNonQuery(text);
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("may not be NULL") || ex.Message.Contains("NOT NULL constraint failed"))
				{
					try
					{
						LogSendResultManager.a.ExecuteNonQuery("\r\nDROP TABLE IF EXISTS LogSendResult_bak;\r\nDROP TABLE IF EXISTS AliwwMessage_bak;\r\nALTER TABLE LogSendResult RENAME TO LogSendResult_bak;\r\nALTER TABLE AliwwMessage RENAME TO AliwwMessage_bak;\r\n\r\nCREATE TABLE IF NOT EXISTS 'LogSendResult' (\r\n'IdNo'  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\n'MsgId'  INTEGER NOT NULL,\r\n'UserNick'       TEXT(50),\r\n'SendResultCode' INTEGER,\r\n'SendResultMsg'  TEXT(200),\r\n'CreateTimeLocal'  TEXT\r\n);\r\n\r\nCREATE INDEX IF NOT EXISTS 'Idx_LogSR_CreateTimeLocal'\r\nON 'LogSendResult' ('CreateTimeLocal' ASC);\r\n\r\nCREATE INDEX IF NOT EXISTS 'Idx_LogSR_MsgId_SendResultCode'\r\nON 'LogSendResult' ('MsgId' ASC, 'SendResultCode' ASC);\r\n\r\nCREATE INDEX IF NOT EXISTS 'Idx_LogSR_SendResultCode_CreateTimeLocal'\r\nON 'LogSendResult' ('SendResultCode' ASC, 'CreateTimeLocal' ASC);\r\n\r\n\r\nCREATE TABLE IF NOT EXISTS 'AliwwMessage' (\r\n'IdNo'  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\n'MsgId'  INTEGER NOT NULL,\r\n'Tid'  INTEGER NOT NULL,\r\n'SellerNick'  TEXT(32) NOT NULL,\r\n'BuyerNick'  TEXT(32) NOT NULL,\r\n'MessageTitle'  TEXT(500),\r\n'MessageBody'  TEXT(5000),\r\n'ModifyTime'  TEXT,\r\n'CreateTime'  TEXT,\r\n'CreateTimeLocal'  TEXT\r\n);\r\n\r\nCREATE INDEX IF NOT EXISTS 'Idx_AM_Tid'\r\nON 'AliwwMessage' ('Tid' ASC);\r\n\r\nCREATE UNIQUE INDEX IF NOT EXISTS 'Idx_AM_MsgId'\r\nON 'AliwwMessage' ('MsgId' ASC);\r\n\r\nCREATE INDEX IF NOT EXISTS 'Idx_AM_SellerNick_CreateTime'\r\nON 'AliwwMessage' ('SellerNick' ASC, 'CreateTime' ASC);\r\n\r\nCREATE INDEX IF NOT EXISTS 'Idx_AM_CreateTime'\r\nON 'AliwwMessage' ('CreateTime' ASC);\r\n\r\n\r\nINSERT INTO AliwwMessage\r\nSELECT * FROM AliwwMessage_bak\r\n ORDER BY IdNo DESC\r\n LIMIT 1;\r\n\r\n");
						return LogSendResultManager.Insert(msgId, userNick, resultCode, resultMsg, sendSoftware);
					}
					catch
					{
						goto IL_0137;
					}
				}
				if (ex.Message.Contains("UserNick"))
				{
					try
					{
						LogSendResultManager.a.ExecuteNonQuery("ALTER TABLE 'LogSendResult' ADD 'UserNick' TEXT(50);");
						return LogSendResultManager.Insert(msgId, userNick, resultCode, resultMsg, sendSoftware);
					}
					catch
					{
						goto IL_0137;
					}
				}
				if (ex.Message.Contains("SendSoftware"))
				{
					try
					{
						LogSendResultManager.a.ExecuteNonQuery("ALTER TABLE LogSendResult ADD SendSoftware INTEGER DEFAULT 0;");
						return LogSendResultManager.Insert(msgId, userNick, resultCode, resultMsg, sendSoftware);
					}
					catch
					{
					}
				}
				IL_0137:
				LogWriter.WriteLog(text + "\r\n" + ex.ToString(), 1);
				num = 0;
			}
			return num;
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x00057044 File Offset: 0x00055244
		public static DataTable Select(long msgId)
		{
			string text = "SELECT * FROM LogSendResult WHERE MsgId=" + DbUtil.ToSqlString(msgId) + " ORDER BY CreateTimeLocal";
			return LogSendResultManager.a.ExecuteTable(text);
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x0005707C File Offset: 0x0005527C
		public static int Delete(DateTime endTime)
		{
			string text = "DELETE FROM LogSendResult WHERE CreateTimeLocal <= " + DbUtil.ToSqlString(endTime.ToString("yyyy-MM-dd HH:mm:ss"));
			return LogSendResultManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x04001289 RID: 4745
		private static IDbAccess a = DbAccessDAL.CreateDbAccess();
	}
}
