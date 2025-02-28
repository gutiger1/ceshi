using System;
using System.Collections.Generic;
using System.Data;
using Agiso.DBAccess;
using Agiso.Object;
using Agiso.Utils;

namespace Agiso.DbManager
{
	// Token: 0x020006A8 RID: 1704
	public class LogAutoReplyManager
	{
		// Token: 0x06002106 RID: 8454 RVA: 0x000570B4 File Offset: 0x000552B4
		public static int Insert(string userNick, string senderNick, string senderUid, string senderOpenUid, string msg, DateTime sendTime, AutoReplyInfo arMatch, MsgFrom msgFrom, DateTime createTime)
		{
			string text = "\r\nINSERT INTO LogAutoReply (\r\n  SellerNick, SenderNick, SenderId, SenderOpenUid, ConsultWord, ConsultTime\r\n  , MatchType, KeyWord, ReplyWord, FromType, CreateTime\r\n)\r\nVALUES ({0},{1},{2},{3},{4}\r\n  ,{5},{6},{7},{8},{9},{10}\r\n)\r\n";
			string text2 = string.Format(text, new object[]
			{
				DbUtil.ToSqlString(userNick),
				DbUtil.ToSqlString(senderNick),
				DbUtil.ToSqlString(senderUid),
				DbUtil.ToSqlString(senderOpenUid),
				DbUtil.ToSqlString(msg),
				DbUtil.ToSqlString(sendTime.ToString("yyyy-MM-dd HH:mm:ss")),
				DbUtil.ToSqlString((int)arMatch.Type),
				DbUtil.ToSqlString(arMatch.KeyWord),
				DbUtil.ToSqlString(arMatch.ReplyWord),
				DbUtil.ToSqlString((int)msgFrom),
				DbUtil.ToSqlString(createTime.ToString("yyyy-MM-dd HH:mm:ss"))
			});
			int num;
			try
			{
				num = LogAutoReplyManager.a.ExecuteNonQuery(text2);
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(text2 + "\r\n" + ex.ToString(), 1);
				num = 0;
			}
			return num;
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x000571B0 File Offset: 0x000553B0
		public static bool NotExistsFirstTimeReplyRecent(string userNick, string buyerNick, string buyerOpenUid, int interval)
		{
			bool flag;
			if (interval <= 0)
			{
				flag = true;
			}
			else
			{
				string text = "SellerNick = " + DbUtil.ToSqlString(userNick) + " AND CreateTime > " + DbUtil.ToSqlString(DateTime.Now.AddMinutes((double)(-(double)interval)).ToString("yyyy-MM-dd HH:mm:ss"));
				if (!string.IsNullOrEmpty(buyerNick) && !buyerNick.Contains("**"))
				{
					text = text + " AND SenderNick = " + DbUtil.ToSqlString(buyerNick);
				}
				else
				{
					if (string.IsNullOrEmpty(buyerOpenUid))
					{
						goto IL_0145;
					}
					string text2;
					if (Util.IsNum(buyerOpenUid))
					{
						text2 = AppConfig.BuyerInfoCache.GetAldsOpenUidByRecentOpenUid(buyerOpenUid);
					}
					else
					{
						text2 = AppConfig.BuyerInfoCache.GetRecentOpenUidByAldsOpenUid(buyerOpenUid);
					}
					if (string.IsNullOrEmpty(text2))
					{
						text = text + " AND SenderOpenUid = " + DbUtil.ToSqlString(buyerOpenUid);
					}
					else
					{
						text = string.Concat(new string[]
						{
							text,
							" AND SenderOpenUid IN (",
							DbUtil.ToSqlString(buyerOpenUid),
							", ",
							DbUtil.ToSqlString(text2),
							")"
						});
					}
				}
				try
				{
					return !LogAutoReplyManager.a.HasRow("LogAutoReply", text);
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog(text + "\r\n" + ex.ToString(), 1);
					return false;
				}
				IL_0145:
				flag = true;
			}
			return flag;
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x00057318 File Offset: 0x00055518
		public static DataTable GetMaxTimeTable(DateTime from, string filterSellerNick)
		{
			string text = string.Format("CreateTime BETWEEN {0} AND {1}", DbUtil.ToSqlString(from.Date.ToString("yyyy-MM-dd HH:mm:ss")), DbUtil.ToSqlString(from.Date.AddDays(1.0).AddSeconds(-1.0).ToString("yyyy-MM-dd HH:mm:ss")));
			if (!filterSellerNick.Equals("【全部】"))
			{
				text += string.Format(" AND SellerNick={0}", DbUtil.ToSqlString(filterSellerNick));
			}
			return LogAutoReplyManager.a.ExecuteTable(string.Format("\r\nSELECT SellerNick, SenderNick, MAX(CreateTime) MaxCreateTime\r\n  FROM LogAutoReply\r\n WHERE 1=1 AND {0}\r\n GROUP BY SellerNick, SenderNick\r\n ORDER BY MaxCreateTime DESC", text));
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x000573CC File Offset: 0x000555CC
		public static List<LogAutoReply> GetList(DateTime from, string sellerNick, string senderNick)
		{
			string text = string.Concat(new string[]
			{
				"CreateTime BETWEEN ",
				DbUtil.ToSqlString(from.Date),
				" AND ",
				DbUtil.ToSqlString(from.Date.AddSeconds(86399.0)),
				"\r\n                AND SellerNick=",
				DbUtil.ToSqlString(sellerNick),
				" \r\n                AND SenderNick = ",
				DbUtil.ToSqlString(senderNick)
			});
			string text2 = "SELECT * FROM LogAutoReply WHERE " + text + " ORDER BY CreateTime DESC";
			DataTable dataTable = LogAutoReplyManager.a.ExecuteTable(text2);
			return LogAutoReplyManager.Map(dataTable);
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x00057480 File Offset: 0x00055680
		public static List<LogAutoReply> Map(DataTable dt)
		{
			List<LogAutoReply> list = new List<LogAutoReply>();
			List<LogAutoReply> list2;
			if (dt == null)
			{
				list2 = list;
			}
			else
			{
				foreach (object obj in dt.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					list.Add(LogAutoReplyManager.Map(dataRow));
				}
				list2 = list;
			}
			return list2;
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x000574F8 File Offset: 0x000556F8
		public static LogAutoReply Map(DataRow row)
		{
			LogAutoReply logAutoReply;
			if (row == null)
			{
				logAutoReply = null;
			}
			else
			{
				logAutoReply = new LogAutoReply
				{
					IdNo = (row.Table.Columns.Contains("IdNo") ? DbUtil.TrimLongNull(row["IdNo"]) : 0L),
					SellerNick = (row.Table.Columns.Contains("SellerNick") ? DbUtil.TrimNull(row["SellerNick"]) : ""),
					SenderNick = (row.Table.Columns.Contains("SenderNick") ? DbUtil.TrimNull(row["SenderNick"]) : ""),
					SenderId = (row.Table.Columns.Contains("SenderId") ? DbUtil.TrimNull(row["SenderId"]) : ""),
					SenderOpenUid = (row.Table.Columns.Contains("SenderOpenUid") ? DbUtil.TrimNull(row["SenderOpenUid"]) : ""),
					ConsultWord = (row.Table.Columns.Contains("ConsultWord") ? DbUtil.TrimNull(row["ConsultWord"]) : ""),
					ConsultTime = (row.Table.Columns.Contains("ConsultTime") ? DbUtil.TrimNull(row["ConsultTime"]) : ""),
					MatchType = (EnumArType)(row.Table.Columns.Contains("MatchType") ? DbUtil.TrimIntNull(row["MatchType"]) : 0),
					KeyWord = (row.Table.Columns.Contains("KeyWord") ? DbUtil.TrimNull(row["KeyWord"]) : ""),
					ReplyWord = (row.Table.Columns.Contains("ReplyWord") ? DbUtil.TrimNull(row["ReplyWord"]) : ""),
					DutyManualNick = (row.Table.Columns.Contains("DutyManualNick") ? DbUtil.TrimNull(row["DutyManualNick"]) : ""),
					FromType = (row.Table.Columns.Contains("FromType") ? DbUtil.TrimIntNull(row["FromType"]) : 0),
					IsTransferFail = (row.Table.Columns.Contains("IsTransferFail") ? DbUtil.TrimIntNull(row["IsTransferFail"]) : 0),
					TransferFailMsg = (row.Table.Columns.Contains("TransferFailMsg") ? DbUtil.TrimNull(row["TransferFailMsg"]) : ""),
					CreateTime = (row.Table.Columns.Contains("CreateTime") ? DbUtil.TrimDateNull(row["CreateTime"]) : DateTime.MinValue)
				};
			}
			return logAutoReply;
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x00057840 File Offset: 0x00055A40
		public static LogAutoReply GetRecent(string userNick, string buyerNick, string buyerOpenUid, EnumArType type)
		{
			string text = string.Format("SellerNick = {0} AND MatchType = {1}", DbUtil.ToSqlString(userNick), (int)type);
			if (!string.IsNullOrEmpty(buyerNick) && !buyerNick.Contains("**"))
			{
				text = text + " AND SenderNick = " + DbUtil.ToSqlString(buyerNick);
			}
			else
			{
				if (string.IsNullOrEmpty(buyerOpenUid))
				{
					goto IL_011A;
				}
				string text2;
				if (Util.IsNum(buyerOpenUid))
				{
					text2 = AppConfig.BuyerInfoCache.GetAldsOpenUidByRecentOpenUid(buyerOpenUid);
				}
				else
				{
					text2 = AppConfig.BuyerInfoCache.GetRecentOpenUidByAldsOpenUid(buyerOpenUid);
				}
				if (string.IsNullOrEmpty(text2))
				{
					text = text + " AND SenderOpenUid = " + DbUtil.ToSqlString(buyerOpenUid);
				}
				else
				{
					text = string.Concat(new string[]
					{
						text,
						" AND SenderOpenUid IN (",
						DbUtil.ToSqlString(buyerOpenUid),
						", ",
						DbUtil.ToSqlString(text2),
						")"
					});
				}
			}
			string text3 = "SELECT *\r\nFROM LogAutoReply\r\nWHERE " + text + "\r\nORDER BY CreateTime DESC\r\nLIMIT 1";
			try
			{
				DataRow dataRow = LogAutoReplyManager.a.ExecuteRow(text3);
				return LogAutoReplyManager.Map(dataRow);
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
				return null;
			}
			IL_011A:
			return null;
		}

		// Token: 0x0400128A RID: 4746
		private static IDbAccess a = DbAccessDAL.CreateDbAccess();
	}
}
