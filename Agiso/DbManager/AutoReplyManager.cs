using System;
using System.Collections.Generic;
using System.Data;
using Agiso.DBAccess;
using Agiso.Object;
using Agiso.Utils;

namespace Agiso.DbManager
{
	// Token: 0x0200069E RID: 1694
	public class AutoReplyManager
	{
		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060020BC RID: 8380 RVA: 0x000548D8 File Offset: 0x00052AD8
		public static List<ArTypeObj> ArTypeList
		{
			get
			{
				if (AutoReplyManager.a == null)
				{
					AutoReplyManager.a = new List<ArTypeObj>
					{
						new ArTypeObj(EnumArType.EqualsWith, "等于"),
						new ArTypeObj(EnumArType.Contains, "包含"),
						new ArTypeObj(EnumArType.Expression, "公式"),
						new ArTypeObj(EnumArType.NoMatching, "无匹配"),
						new ArTypeObj(EnumArType.FirstReply, "首次答复")
					};
				}
				return AutoReplyManager.a;
			}
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x00054970 File Offset: 0x00052B70
		public static string GetArTypeListCaseWhen()
		{
			string text = "CASE ";
			foreach (ArTypeObj arTypeObj in AutoReplyManager.ArTypeList)
			{
				text += string.Format(" WHEN Type={0} THEN '{1}' ", arTypeObj.ArTypeValue, arTypeObj.ArTypeText);
			}
			text += " ELSE '[未知]' END";
			return text;
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x000549F8 File Offset: 0x00052BF8
		public static AutoReplyInfo Map(DataRow row)
		{
			AutoReplyInfo autoReplyInfo;
			if (row == null)
			{
				autoReplyInfo = null;
			}
			else
			{
				AutoReplyInfo autoReplyInfo2 = new AutoReplyInfo();
				autoReplyInfo2.IdNo = DbUtil.TrimLongNull(row["IdNo"]);
				autoReplyInfo2.Idx = DbUtil.TrimLongNull(row["Idx"]);
				autoReplyInfo2.Type = (EnumArType)DbUtil.TrimIntNull(row["Type"]);
				if (row.Table.Columns.Contains("TypeText"))
				{
					autoReplyInfo2.TypeText = DbUtil.TrimNull(row["TypeText"]);
				}
				if (row.Table.Columns.Contains("Valid"))
				{
					autoReplyInfo2.Valid = DbUtil.TrimBoolNull(row["Valid"]).GetValueOrDefault();
				}
				autoReplyInfo2.KeyWord = DbUtil.TrimNull(row["KeyWord"]);
				autoReplyInfo2.ReplyWord = DbUtil.TrimNull(row["ReplyWord"]);
				autoReplyInfo2.SellerNick = DbUtil.TrimNull(row["SellerNick"]);
				autoReplyInfo2.Grade = DbUtil.TrimLongNull(row["Grade"]);
				autoReplyInfo2.ArStartTime = new DateTime?(DbUtil.TrimDateNull(row["ArStartTime"]));
				autoReplyInfo2.ArEndTime = new DateTime?(DbUtil.TrimDateNull(row["ArEndTime"]));
				autoReplyInfo2.Option1 = DbUtil.TrimLongNull(row["Option1"]);
				autoReplyInfo2.ModifyTime = DbUtil.TrimDateNull(row["ModifyTime"]);
				autoReplyInfo2.CreateTime = DbUtil.TrimDateNull(row["CreateTime"]);
				autoReplyInfo = autoReplyInfo2;
			}
			return autoReplyInfo;
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x00054BA0 File Offset: 0x00052DA0
		public static AutoReplyInfo Select(long idno)
		{
			string text = string.Format("SELECT * FROM AutoReply WHERE IdNo={0}", DbUtil.ToSqlString(idno));
			DataRow dataRow = AutoReplyManager.b.ExecuteRow(text);
			return AutoReplyManager.Map(dataRow);
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x00054BD8 File Offset: 0x00052DD8
		public static List<AutoReplyInfo> Select(string keyWord = null, string sellerNick = null)
		{
			string text = "1=1";
			if (!string.IsNullOrEmpty(sellerNick) && !sellerNick.Equals("【全部】"))
			{
				text += string.Format(" AND SellerNick={0}", DbUtil.ToSqlString(sellerNick));
			}
			if (!string.IsNullOrEmpty(keyWord))
			{
				text += string.Format(" AND (KeyWord LIKE {0} OR ReplyWord LIKE {0})", DbUtil.ToSqlLikeString(keyWord));
			}
			string text2 = "AutoReply";
			string text3 = string.Format("IdNo,Idx,Enabled,Type,case when Type={1} Then '' ELSE KeyWord END AS KeyWord,ReplyWord,SellerNick,Grade,ArStartTime,ArEndTime,Option1,ModifyTime,CreateTime,{0} AS TypeText, Valid", AutoReplyManager.GetArTypeListCaseWhen(), 500);
			DataTable dataTable = DbAccessDAL.CreateDbAccess().ExecuteTable(text3, text2, text, "DATETIME( ModifyTime ) DESC", 1L, 99999L);
			List<AutoReplyInfo> list = new List<AutoReplyInfo>();
			List<AutoReplyInfo> list2;
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					list.Add(AutoReplyManager.Map(dataRow));
				}
				list2 = list;
			}
			else
			{
				list2 = list;
			}
			return list2;
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x00054D10 File Offset: 0x00052F10
		private static int c(AutoReplyInfo A_0)
		{
			string text = "\r\nUPDATE AutoReply\r\n   SET Type = {1}\r\n    , SellerNick = {2}\r\n    , KeyWord = {3}\r\n    , ReplyWord = {4}\r\n    , ModifyTime = {5}\r\n    , Grade = {6}\r\n    , ArStartTime = {7}\r\n    , ArEndTime = {8}\r\n WHERE IdNo = {0}\r\n";
			string text2 = string.Format(text, new object[]
			{
				DbUtil.ToSqlString(A_0.IdNo),
				DbUtil.ToSqlString((int)A_0.Type),
				DbUtil.ToSqlString(A_0.SellerNick),
				DbUtil.ToSqlString(A_0.KeyWord),
				DbUtil.ToSqlString(A_0.ReplyWord),
				DbUtil.ToSqlString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
				DbUtil.ToSqlString(A_0.Grade),
				DbUtil.ToSqlString(A_0.ArStartTime),
				DbUtil.ToSqlString(A_0.ArEndTime)
			});
			int num;
			try
			{
				num = AutoReplyManager.b.ExecuteNonQuery(text2);
			}
			catch (Exception ex)
			{
				string text3 = text2;
				string text4 = "\r\n";
				Exception ex2 = ex;
				LogWriter.WriteLog(text3 + text4 + ((ex2 != null) ? ex2.ToString() : null), 1);
				num = 0;
			}
			return num;
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x00054E1C File Offset: 0x0005301C
		public static int InserOrUpdate(AutoReplyInfo autoReply)
		{
			int num;
			if (autoReply == null)
			{
				num = 0;
			}
			else if (AutoReplyManager.a(autoReply))
			{
				num = 0;
			}
			else if (autoReply.IdNo == 0L)
			{
				num = AutoReplyManager.b(autoReply);
			}
			else
			{
				num = AutoReplyManager.c(autoReply);
			}
			return num;
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x00054E64 File Offset: 0x00053064
		private static int b(AutoReplyInfo A_0)
		{
			string text = "\r\nINSERT OR IGNORE INTO AutoReply (\r\n  Idx, Type, SellerNick, KeyWord, ReplyWord\r\n  , ModifyTime, CreateTime, Grade, ArStartTime, ArEndTime\r\n)\r\nVALUES ({0},{1},{2},{3},{4}\r\n  ,{5},{6},{7},{8},{9}\r\n)\r\n";
			string text2 = text;
			object[] array = new object[10];
			array[0] = DbUtil.ToSqlString(1);
			array[1] = DbUtil.ToSqlString((int)A_0.Type);
			array[2] = DbUtil.ToSqlString(A_0.SellerNick);
			array[3] = DbUtil.ToSqlString(A_0.KeyWord);
			array[4] = DbUtil.ToSqlString(A_0.ReplyWord);
			array[5] = DbUtil.ToSqlString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			array[6] = DbUtil.ToSqlString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			array[7] = DbUtil.ToSqlString(A_0.Grade);
			int num = 8;
			DateTime? dateTime = A_0.ArStartTime;
			array[num] = DbUtil.ToSqlString((dateTime != null) ? dateTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm") : null);
			int num2 = 9;
			dateTime = A_0.ArEndTime;
			array[num2] = DbUtil.ToSqlString((dateTime != null) ? dateTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm") : null);
			string text3 = string.Format(text2, array);
			int num3;
			try
			{
				num3 = AutoReplyManager.b.ExecuteNonQuery(text3);
			}
			catch (Exception ex)
			{
				string text4 = text3;
				string text5 = "\r\n";
				Exception ex2 = ex;
				LogWriter.WriteLog(text4 + text5 + ((ex2 != null) ? ex2.ToString() : null), 1);
				num3 = 0;
			}
			return num3;
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x00054FC4 File Offset: 0x000531C4
		private static bool a(AutoReplyInfo A_0)
		{
			string text = "SELECT 1 FROM AutoReply WHERE Type={0} AND SellerNick={1} AND KeyWord={2} AND ReplyWord={3} AND IdNo <>{4}";
			string text2 = string.Format(text, new object[]
			{
				DbUtil.ToSqlString((int)A_0.Type),
				DbUtil.ToSqlString(A_0.SellerNick),
				DbUtil.ToSqlString(A_0.KeyWord),
				DbUtil.ToSqlString(A_0.ReplyWord),
				DbUtil.ToSqlString(A_0.IdNo)
			});
			bool flag;
			try
			{
				flag = Convert.ToInt32(AutoReplyManager.b.ExecuteScalar(text2)) > 0;
			}
			catch (Exception ex)
			{
				string text3 = text2;
				string text4 = "\r\n";
				Exception ex2 = ex;
				LogWriter.WriteLog(text3 + text4 + ((ex2 != null) ? ex2.ToString() : null), 1);
				flag = true;
			}
			return flag;
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x00055084 File Offset: 0x00053284
		public static int InsertOrReplace(AutoReplyInfo autoReply)
		{
			int num;
			if (AutoReplyManager.a(autoReply))
			{
				num = 0;
			}
			else
			{
				string[] array = new string[21];
				array[0] = "\r\nINSERT OR REPLACE INTO AutoReply (\r\n  Idx, Type, SellerNick, KeyWord, ReplyWord\r\n  , ModifyTime, CreateTime, Grade, ArStartTime, ArEndTime\r\n)\r\nVALUES (";
				array[1] = DbUtil.ToSqlString(1);
				array[2] = ",";
				array[3] = DbUtil.ToSqlString((int)autoReply.Type);
				array[4] = ",";
				array[5] = DbUtil.ToSqlString(autoReply.SellerNick);
				array[6] = ",";
				array[7] = DbUtil.ToSqlString(autoReply.KeyWord);
				array[8] = ",";
				array[9] = DbUtil.ToSqlString(autoReply.ReplyWord);
				array[10] = "\r\n  ,";
				array[11] = DbUtil.ToSqlString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				array[12] = ",";
				array[13] = DbUtil.ToSqlString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				array[14] = "\r\n  ,";
				array[15] = DbUtil.ToSqlString(autoReply.Grade);
				array[16] = ",";
				int num2 = 17;
				DateTime? dateTime = autoReply.ArStartTime;
				array[num2] = DbUtil.ToSqlString((dateTime != null) ? dateTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm") : null);
				array[18] = ",";
				int num3 = 19;
				dateTime = autoReply.ArEndTime;
				array[num3] = DbUtil.ToSqlString((dateTime != null) ? dateTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm") : null);
				array[20] = "\r\n)\r\n";
				string text = string.Concat(array);
				try
				{
					num = AutoReplyManager.b.ExecuteNonQuery(text);
				}
				catch (Exception ex)
				{
					string text2 = text;
					string text3 = "\r\n";
					Exception ex2 = ex;
					LogWriter.WriteLog(text2 + text3 + ((ex2 != null) ? ex2.ToString() : null), 1);
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x00055258 File Offset: 0x00053458
		public static int Delete(long idno)
		{
			string text = "DELETE FROM AutoReply WHERE IdNo=" + DbUtil.ToSqlString(idno);
			return AutoReplyManager.b.ExecuteNonQuery(text);
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x0005528C File Offset: 0x0005348C
		public static bool HasData()
		{
			return AutoReplyManager.b.HasRow("'main'.'AutoReply'", "1=1");
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000552B0 File Offset: 0x000534B0
		public static int InvalidReply(long idNo)
		{
			string text = "Update AutoReply SET Valid = 0 WHERE IdNo = " + DbUtil.ToSqlString(idNo);
			return AutoReplyManager.b.ExecuteNonQuery(text);
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x000552E4 File Offset: 0x000534E4
		public static int ValidReply(long idNo)
		{
			string text = "Update AutoReply SET Valid = 1 WHERE IdNo = " + DbUtil.ToSqlString(idNo);
			return AutoReplyManager.b.ExecuteNonQuery(text);
		}

		// Token: 0x0400127D RID: 4733
		private static List<ArTypeObj> a;

		// Token: 0x0400127E RID: 4734
		private static IDbAccess b = DbAccessDAL.CreateDbAccess();
	}
}
