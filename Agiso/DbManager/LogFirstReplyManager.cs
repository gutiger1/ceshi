using System;
using Agiso.DBAccess;
using Agiso.Utils;

namespace Agiso.DbManager
{
	// Token: 0x020006A5 RID: 1701
	public class LogFirstReplyManager
	{
		// Token: 0x060020F7 RID: 8439 RVA: 0x000569D4 File Offset: 0x00054BD4
		public static int Insert(string userNick, string senderNick, string senderUid, string senderOpenUid, string msg, DateTime sendTime, string replyMsg)
		{
			string text = string.Concat(new string[]
			{
				"\r\nINSERT INTO LogFirstReply (\r\n  SellerNick, SenderNick, SenderId, SenderOpenUid, ConsultWord, ConsultTime\r\n  , ReplyWord, CreateTime\r\n)\r\nVALUES (",
				DbUtil.ToSqlString(userNick),
				",\r\n",
				DbUtil.ToSqlString(senderNick),
				",\r\n",
				DbUtil.ToSqlString(senderUid),
				",\r\n",
				DbUtil.ToSqlString(senderOpenUid),
				",\r\n",
				DbUtil.ToSqlString(msg),
				",\r\n",
				DbUtil.ToSqlString(sendTime.ToString("yyyy-MM-dd HH:mm:ss")),
				",\r\n",
				DbUtil.ToSqlString(replyMsg),
				",\r\n",
				DbUtil.ToSqlString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
				"\r\n)\r\n"
			});
			int num;
			try
			{
				num = LogFirstReplyManager.a.ExecuteNonQuery(text);
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(text + "\r\n" + ex.ToString(), 1);
				num = 0;
			}
			return num;
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x00056AEC File Offset: 0x00054CEC
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
					return !LogFirstReplyManager.a.HasRow("LogFirstReply", text);
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

		// Token: 0x04001287 RID: 4743
		private static IDbAccess a = DbAccessDAL.CreateDbAccess();
	}
}
