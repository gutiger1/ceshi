using System;
using System.Collections.Generic;
using System.Data;
using Agiso.DBAccess;
using Agiso.Object;
using Agiso.Utils;
using Agiso.WwService.Sdk.Domain;
using Agiso.WwWebSocket.Model;

namespace Agiso.DbManager
{
	// Token: 0x0200069F RID: 1695
	public class AliwwMessageManager
	{
		// Token: 0x060020CC RID: 8396 RVA: 0x00055318 File Offset: 0x00053518
		public static int Insert(List<AliwwWsMsg> aliwwWsMsgs)
		{
			List<AliwwMessage> list = new List<AliwwMessage>();
			foreach (AliwwWsMsg aliwwWsMsg in aliwwWsMsgs)
			{
				if (aliwwWsMsg.IdNo != 0L)
				{
					AliwwMessage aliwwMessage = new AliwwMessage
					{
						BuyerNick = aliwwWsMsg.BuyerNick,
						BuyerOpenUid = aliwwWsMsg.BuyerOpenUid,
						CreateTime = new DateTime?(aliwwWsMsg.CreateTime),
						IdNo = aliwwWsMsg.IdNo,
						SellerNick = aliwwWsMsg.SellerNick,
						MessageBody = aliwwWsMsg.Message,
						Tid = new long?(Util.ToLong(aliwwWsMsg.Tid)),
						MessageTitle = ""
					};
					list.Add(aliwwMessage);
				}
			}
			return AliwwMessageManager.Insert(list);
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x00055408 File Offset: 0x00053608
		public static int Insert(List<AliwwMessage> messages)
		{
			string text = "\r\nINSERT OR IGNORE INTO AliwwMessage (\r\n  MsgId, Tid, SellerNick, BuyerNick, BuyerOpenUid, MessageTitle\r\n  , MessageBody, ModifyTime, CreateTime, CreateTimeLocal)\r\nSELECT {0},{1},{2},{3},{4}\r\n  ,{5},{6},{7},{8},{9}\r\nWHERE NOT EXISTS ( SELECT 1 FROM AliwwMessage WHERE MsgId={0} );\r\n";
			int num = 0;
			foreach (AliwwMessage aliwwMessage in messages)
			{
				if (aliwwMessage.IdNo > 0L)
				{
					string text2 = string.Format(text, new object[]
					{
						DbUtil.ToSqlString(aliwwMessage.IdNo),
						DbUtil.ToSqlString(aliwwMessage.Tid),
						DbUtil.ToSqlString(aliwwMessage.SellerNick),
						DbUtil.ToSqlString(aliwwMessage.BuyerNick),
						DbUtil.ToSqlString(aliwwMessage.BuyerOpenUid),
						DbUtil.ToSqlString(aliwwMessage.MessageTitle),
						DbUtil.ToSqlString(aliwwMessage.MessageBody),
						DbUtil.ToSqlString((aliwwMessage.ModifyTime != null) ? aliwwMessage.ModifyTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""),
						DbUtil.ToSqlString((aliwwMessage.CreateTime != null) ? aliwwMessage.CreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""),
						DbUtil.ToSqlString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
					});
					try
					{
						num += AliwwMessageManager.a.ExecuteNonQuery(text2);
					}
					catch (Exception ex)
					{
						LogWriter.WriteLog(text2 + "\r\n" + ex.ToString(), 1);
					}
				}
			}
			return num;
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x000555E8 File Offset: 0x000537E8
		public static List<string> SelectMsg(string sellerNick, string buyerNick, string buyerOpenUid, string tids)
		{
			List<string> list = new List<string>();
			DataTable dataTable = AliwwMessageManager.Select(sellerNick, buyerNick, buyerOpenUid, tids);
			if (DbUtil.HasMoreRow(dataTable))
			{
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					list.Add(DbUtil.TrimNull(dataRow["MessageBody"]));
				}
			}
			return list;
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x00055674 File Offset: 0x00053874
		public static DataTable Select(string sellerNick, string buyerNick, string buyerOpenUid, string tids)
		{
			string text = "SellerNick = " + DbUtil.ToSqlString(sellerNick);
			if (!string.IsNullOrEmpty(buyerOpenUid))
			{
				text = text + " AND BuyerOpenUid = " + DbUtil.ToSqlString(buyerOpenUid);
			}
			else
			{
				if (string.IsNullOrEmpty(buyerNick) || buyerNick.Contains("**"))
				{
					return null;
				}
				text = text + " AND BuyerNick = " + DbUtil.ToSqlString(buyerNick);
			}
			if (!string.IsNullOrEmpty(tids))
			{
				text = text + " AND Tid IN (" + tids + ")";
			}
			else
			{
				text = text + " AND CreateTime > " + DbUtil.ToSqlString(DateTime.Now.AddDays(-1.0));
			}
			string text2 = "SELECT * FROM AliwwMessage WHERE " + text;
			return AliwwMessageManager.a.ExecuteTable(text2);
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x00055758 File Offset: 0x00053958
		public static DataRow Select(long msgId)
		{
			string text = "SELECT * FROM AliwwMessage WHERE MsgId={0}";
			text = string.Format(text, DbUtil.ToSqlString(msgId));
			return AliwwMessageManager.a.ExecuteRow(text);
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x0005578C File Offset: 0x0005398C
		public static DataTable Select(long tid, string sellerNick, string buyerNick, string buyerOpenUid, DateTime? msgDateFrom, int sendStatus)
		{
			long num;
			return AliwwMessageManager.Select(out num, tid, sellerNick, buyerNick, buyerOpenUid, msgDateFrom, sendStatus, null, null, null);
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x000557C8 File Offset: 0x000539C8
		public static DataTable Select(out long count, long tid, string sellerNick, string buyerNick, string buyerOpenUid, DateTime? msgDateFrom, int sendStatus, DateTime? msgDateEnd = null, int? pageNo = null, int? pageSize = null)
		{
			string text = string.Format("EXISTS (SELECT 1 FROM LogSendResult t2 WHERE t2.MsgId=AliwwMessage.MsgId AND SendResultCode = {0})", DbUtil.ToSqlString(201));
			string text2 = "1=1";
			if (tid > 0L)
			{
				text2 += string.Format(" AND Tid={0}", DbUtil.ToSqlString(tid));
			}
			else
			{
				if (!string.IsNullOrEmpty(sellerNick))
				{
					text2 = text2 + " AND SellerNick = " + DbUtil.ToSqlString(sellerNick);
				}
				if (!string.IsNullOrEmpty(buyerOpenUid))
				{
					text2 = text2 + " AND BuyerOpenUid=" + DbUtil.ToSqlString(buyerOpenUid);
				}
				else if (!string.IsNullOrEmpty(buyerNick) && !buyerNick.Contains("**"))
				{
					text2 = text2 + " AND BuyerNick=" + DbUtil.ToSqlString(buyerNick);
				}
				else
				{
					if (msgDateFrom == null)
					{
						msgDateFrom = new DateTime?(DateTime.Today);
					}
					string text3 = ((msgDateEnd != null) ? DbUtil.ToSqlString(msgDateFrom.Value.ToString("yyyy-MM-dd HH:mm:ss")) : DbUtil.ToSqlString(msgDateFrom.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")));
					string text4 = ((msgDateEnd != null) ? DbUtil.ToSqlString(msgDateEnd.Value.ToString("yyyy-MM-dd HH:mm:ss")) : DbUtil.ToSqlString(msgDateFrom.Value.Date.AddDays(1.0).ToString("yyyy-MM-dd HH:mm:ss")));
					text2 += string.Format(" AND CreateTime>={0} AND CreateTime<{1}", text3, text4);
				}
				if (sendStatus >= 0)
				{
					switch (sendStatus)
					{
					case 1:
						text2 = text2 + " AND " + text;
						break;
					case 2:
						text2 = text2 + " AND NOT " + text;
						break;
					}
				}
			}
			string text5 = "AliwwMessage";
			string text6 = "*," + text + " AS ExistsSucc";
			DataTable dataTable;
			if (pageSize.GetValueOrDefault() > 0 && pageNo.GetValueOrDefault() > 0)
			{
				count = (long)AliwwMessageManager.a.ExecuteCount(text5, text2);
				dataTable = ((count > 0L) ? AliwwMessageManager.a.ExecuteTable(text6, text5, text2, "IdNo DESC", (long)pageNo.Value, (long)pageSize.Value) : null);
			}
			else
			{
				DataTable dataTable2 = AliwwMessageManager.a.ExecuteTable(text6, text5, text2);
				count = (long)((dataTable2 != null) ? dataTable2.Rows.Count : 0);
				dataTable = dataTable2;
			}
			return dataTable;
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x00055A60 File Offset: 0x00053C60
		public static List<long> GetFailMsgIdList(out int failMsgCount, DateTime? timeStart, DateTime? timeEnd, bool includeAccountIsBanned)
		{
			failMsgCount = 0;
			if (timeStart == null)
			{
				timeStart = new DateTime?(DateTime.Today);
			}
			if (timeEnd == null)
			{
				timeEnd = new DateTime?(timeStart.Value.Date.AddDays(1.0));
			}
			List<long> list = new List<long>();
			string text = "";
			if (!includeAccountIsBanned)
			{
				text += string.Format(" AND SendResultCode <> {0}", -1050);
			}
			text += string.Format(" AND SendResultCode <> {0}", -1060);
			text += string.Format(" AND SendResultCode <> {0}", -4000);
			text += string.Format(" AND SendResultCode <> {0}", -161);
			text += string.Format(" AND SendResultCode <> {0}", -4010);
			text += string.Format(" AND SendResultCode <> {0}", -4020);
			text += string.Format(" AND SendResultCode <> {0}", -4030);
			text += string.Format(" AND SendResultCode <> {0}", -4040);
			text += string.Format(" AND SendResultCode <> {0}", -155);
			text += string.Format(" AND SendResultCode <> {0}", -3530);
			text += string.Format(" AND SendResultCode <> {0}", -209);
			text += string.Format(" AND SendResultCode <> {0}", -210);
			text += string.Format(" AND SendResultCode <> {0}", -225);
			string text2 = string.Format("SELECT DISTINCT MsgId FROM LogSendResult\r\nWHERE CreateTimeLocal >= {0}\r\nAND CreateTimeLocal < {1}\r\nAND SendResultCode < 0\r\nAND MsgId > 0\r\nAND NOT EXISTS (SELECT 1 FROM LogSendResult t2 WHERE t2.MsgId = LogSendResult.MsgId \r\n                    AND (t2.SendResultCode = {2}\r\n                    {3}\r\n                    OR t2.SendResultCode = {4}\r\n                    OR t2.SendResultCode = {5}\r\n                    OR t2.SendResultCode = {6}\r\n                    OR t2.SendResultCode = {7}\r\n                    OR t2.SendResultCode = {8}\r\n                    OR t2.SendResultCode = {9}\r\n                    OR t2.SendResultCode = {10}\r\n                    OR t2.SendResultCode = {11}\r\n                    OR t2.SendResultCode = {12}\r\n                    OR t2.SendResultCode = {13}\r\n                    OR t2.SendResultCode = {14}\r\n                    OR t2.SendResultCode = {15}))\r\n{16}\r\n", new object[]
			{
				DbUtil.ToSqlString(timeStart.Value.ToString("yyyy-MM-dd HH:mm:ss")),
				DbUtil.ToSqlString(timeEnd.Value.ToString("yyyy-MM-dd HH:mm:ss")),
				201,
				(!includeAccountIsBanned) ? string.Format(" OR t2.SendResultCode = {0}", -1050) : "",
				-1060,
				-4000,
				-161,
				-4010,
				-4020,
				-4030,
				-4040,
				-155,
				-3530,
				-209,
				-210,
				-225,
				text
			});
			DataTable dataTable = AliwwMessageManager.a.ExecuteTable(text2);
			if (((dataTable != null) ? dataTable.Rows : null) != null)
			{
				failMsgCount += dataTable.Rows.Count;
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					long num = DbUtil.TrimLongNull(dataRow["MsgId"]);
					list.Add(num);
				}
			}
			string text3 = string.Format("SELECT MsgId FROM AliwwMessage t1\r\nWHERE t1.CreateTime >= {0}\r\nAND t1.CreateTime < {1}\r\nAND t1.MsgId > 0\r\nAND t1.CreateTimeLocal < {2}\r\nAND NOT EXISTS(SELECT 1 FROM LogSendResult t2 WHERE t2.MsgId = t1.MsgId AND t2.SendResultCode <> {3} AND t2.SendResultCode <> {4})", new object[]
			{
				DbUtil.ToSqlString(timeStart.Value.ToString("yyyy-MM-dd HH:mm:ss")),
				DbUtil.ToSqlString(timeEnd.Value.ToString("yyyy-MM-dd HH:mm:ss")),
				DbUtil.ToSqlString(DateTime.Now.AddMinutes(-2.0).ToString("yyyy-MM-dd HH:mm:ss")),
				151,
				101
			});
			DataTable dataTable2 = AliwwMessageManager.a.ExecuteTable(text3);
			if (((dataTable2 != null) ? dataTable2.Rows : null) != null)
			{
				failMsgCount += dataTable2.Rows.Count;
				foreach (object obj2 in dataTable2.Rows)
				{
					DataRow dataRow2 = (DataRow)obj2;
					long num2 = DbUtil.TrimLongNull(dataRow2["MsgId"]);
					list.Add(num2);
				}
			}
			return list;
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x00055F2C File Offset: 0x0005412C
		public static DataTable GetNotExistsSendLogResult(DateTime msgDateFrom, DateTime msgDateTo)
		{
			string text = string.Concat(new string[]
			{
				"SELECT * FROM AliwwMessage WHERE CreateTime >= ",
				DbUtil.ToSqlString(msgDateFrom.ToString("yyyy-MM-dd HH:mm:ss")),
				"\r\nAND CreateTime < ",
				DbUtil.ToSqlString(msgDateTo.ToString("yyyy-MM-dd HH:mm:ss")),
				"\r\nAND NOT EXISTS (SELECT 1 FROM LogSendResult WHERE LogSendResult.MsgId = AliwwMessage.MsgId) "
			});
			return AliwwMessageManager.a.ExecuteTable(text);
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x00055F98 File Offset: 0x00054198
		public static int Delete(DateTime endTime)
		{
			string text = "DELETE FROM AliwwMessage WHERE CreateTime <= " + DbUtil.ToSqlString(endTime.ToString("yyyy-MM-dd HH:mm:ss"));
			return AliwwMessageManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x00055FD0 File Offset: 0x000541D0
		public static List<SellerSendMsgInfo> StatisticsSendMsgCount(DateTime start, DateTime? end = null)
		{
			string text = string.Concat(new string[]
			{
				"SELECT SellerNick, Count(1) MsgCount FROM AliwwMessage WHERE CreateTime >= ",
				DbUtil.ToSqlString(start),
				" ",
				(end != null) ? ("AND CreateTime < " + DbUtil.ToSqlString(end)) : "",
				" GROUP BY SellerNick"
			});
			DataTable dataTable = AliwwMessageManager.a.ExecuteTable(text);
			int? num;
			if (dataTable == null)
			{
				num = null;
			}
			else
			{
				DataRowCollection rows = dataTable.Rows;
				num = ((rows != null) ? new int?(rows.Count) : null);
			}
			int? num2 = num;
			List<SellerSendMsgInfo> list2;
			if (num2.GetValueOrDefault() > 0)
			{
				List<SellerSendMsgInfo> list = new List<SellerSendMsgInfo>();
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					list.Add(new SellerSendMsgInfo
					{
						SellerNick = dataRow["SellerNick"].ToString(),
						MsgCount = DbUtil.TrimIntNull(dataRow["MsgCount"])
					});
				}
				list2 = list;
			}
			else
			{
				list2 = null;
			}
			return list2;
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x00056128 File Offset: 0x00054328
		public static bool HasMsgBefore(DateTime beforeTime)
		{
			object obj = AliwwMessageManager.a.ExecuteScalar("SELECT 1 FROM AliwwMessage WHERE CreateTime < " + DbUtil.ToSqlString(beforeTime));
			return DbUtil.TrimIntNull(obj) > 0;
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x00056160 File Offset: 0x00054360
		public static AliwwMessageInfo GetLastMsg(string sellerNick)
		{
			string text = "select * from AliwwMessage where SellerNick = " + DbUtil.ToSqlString(sellerNick) + " order by CreateTime Desc limit 1";
			DataRow dataRow = AliwwMessageManager.a.ExecuteRow(text);
			return AliwwMessageInfo.Map(dataRow);
		}

		// Token: 0x0400127F RID: 4735
		private static IDbAccess a = DbAccessDAL.CreateDbAccess();
	}
}
