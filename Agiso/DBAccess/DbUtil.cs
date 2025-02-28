using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Agiso.DBAccess
{
	// Token: 0x020000FB RID: 251
	public class DbUtil
	{
		// Token: 0x060007BF RID: 1983 RVA: 0x0004F878 File Offset: 0x0004DA78
		public static string SafeFormat(string strInput)
		{
			return strInput.Trim().Replace("'", "''");
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0004F8A0 File Offset: 0x0004DAA0
		public static string ToSqlString(object paramStr)
		{
			string text;
			if (paramStr == null || Convert.IsDBNull(paramStr))
			{
				text = "null";
			}
			else
			{
				Type type = paramStr.GetType();
				Type underlyingType = Nullable.GetUnderlyingType(type);
				if (underlyingType != null)
				{
					type = underlyingType;
				}
				if (type.IsValueType)
				{
					if (type == typeof(bool))
					{
						return ((bool)paramStr) ? "1" : "0";
					}
					if (type == typeof(DateTime))
					{
						return string.Format("'{0:yyyy-MM-dd HH:mm:ss}'", (DateTime)paramStr);
					}
					if (type.IsEnum)
					{
						return Convert.ChangeType(paramStr, Enum.GetUnderlyingType(type)).ToString();
					}
					string text2 = paramStr.ToString();
					if (DbUtil.a.IsMatch(text2))
					{
						return text2;
					}
				}
				text = "'" + DbAccessDAL.SqlSafeFormat(paramStr.ToString()) + "'";
			}
			return text;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0004F99C File Offset: 0x0004DB9C
		public static string ToSqlEndDate(string paramDate)
		{
			return DbUtil.ToSqlString(paramDate + " 23:59:59");
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0004F9BC File Offset: 0x0004DBBC
		public static string ToSqlLikeString(string paramStr)
		{
			return "'%" + DbAccessDAL.SqlSafeFormat(paramStr) + "%'";
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0004F9E4 File Offset: 0x0004DBE4
		public static string ToSqlLikeStringR(string paramStr)
		{
			return "'" + DbAccessDAL.SqlSafeFormat(paramStr) + "%'";
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0004FA0C File Offset: 0x0004DC0C
		public static string ToSqlInString(ICollection list)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in list)
			{
				stringBuilder.Append("," + DbUtil.ToSqlString(obj));
			}
			string text = stringBuilder.ToString();
			string text2;
			if (text.Length < 1)
			{
				text2 = "()";
			}
			else
			{
				text2 = "(" + stringBuilder.ToString().Substring(1) + ")";
			}
			return text2;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0004FAB4 File Offset: 0x0004DCB4
		public static string TrimNull(object obj)
		{
			string text;
			if (obj == null)
			{
				text = "";
			}
			else if (obj is DBNull)
			{
				text = "";
			}
			else
			{
				text = obj.ToString().Trim();
			}
			return text;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0004FAF0 File Offset: 0x0004DCF0
		public static int TrimIntNull(object obj)
		{
			int num;
			if (obj is DBNull)
			{
				num = 0;
			}
			else if (obj is bool)
			{
				num = (bool.Parse(obj.ToString()) ? 1 : 0);
			}
			else
			{
				try
				{
					num = int.Parse(obj.ToString());
				}
				catch
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0004FB50 File Offset: 0x0004DD50
		public static uint TrimUintNull(object obj)
		{
			uint num;
			if (obj is DBNull)
			{
				num = 0U;
			}
			else if (obj is bool)
			{
				num = (bool.Parse(obj.ToString()) ? 1U : 0U);
			}
			else
			{
				try
				{
					num = uint.Parse(obj.ToString());
				}
				catch
				{
					num = 0U;
				}
			}
			return num;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0004FBB0 File Offset: 0x0004DDB0
		public static long TrimLongNull(object obj)
		{
			long num;
			if (obj is DBNull)
			{
				num = 0L;
			}
			else
			{
				try
				{
					num = long.Parse(obj.ToString());
				}
				catch
				{
					num = 0L;
				}
			}
			return num;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0004FC04 File Offset: 0x0004DE04
		public static decimal TrimDecimalNull(object obj)
		{
			decimal num;
			if (obj is DBNull)
			{
				num = 0m;
			}
			else
			{
				try
				{
					num = decimal.Parse(obj.ToString());
				}
				catch
				{
					num = 0m;
				}
			}
			return num;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0004FC50 File Offset: 0x0004DE50
		public static DateTime TrimDateNull(object obj)
		{
			DateTime dateTime;
			if (obj is DBNull)
			{
				dateTime = DateTime.MinValue;
			}
			else
			{
				try
				{
					dateTime = DateTime.Parse(obj.ToString());
				}
				catch
				{
					dateTime = DateTime.MinValue;
				}
			}
			return dateTime;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0004FC9C File Offset: 0x0004DE9C
		public static double TrimDoubleNull(object obj)
		{
			double num;
			if (obj is DBNull)
			{
				num = 0.0;
			}
			else if (obj is bool)
			{
				num = (double)(bool.Parse(obj.ToString()) ? 1 : 0);
			}
			else
			{
				try
				{
					num = double.Parse(obj.ToString());
				}
				catch
				{
					num = 0.0;
				}
			}
			return num;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0004FD10 File Offset: 0x0004DF10
		public static bool HasMoreRow(DataSet ds)
		{
			return ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0004FD54 File Offset: 0x0004DF54
		public static bool HasMoreRow(DataTable dt)
		{
			return dt != null && dt.Rows.Count != 0;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0004FD80 File Offset: 0x0004DF80
		public static bool? TrimBoolNull(object obj)
		{
			bool? flag;
			if (obj is DBNull)
			{
				flag = null;
			}
			else
			{
				try
				{
					flag = new bool?(int.Parse(obj.ToString()) > 0);
				}
				catch
				{
					flag = new bool?(false);
				}
			}
			return flag;
		}

		// Token: 0x040004F0 RID: 1264
		private static Regex a = new Regex("^-?\\d+(\\.\\d+)?$");
	}
}
