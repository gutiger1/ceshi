using System;
using System.Collections.Generic;
using System.Data;
using Agiso.DBAccess;
using Agiso.Utils;
using Agiso.WwWebSocket.Model;

namespace Agiso.DbManager
{
	// Token: 0x020006A2 RID: 1698
	public class CustomerServiceMouldManager
	{
		// Token: 0x060020E3 RID: 8419 RVA: 0x0005619C File Offset: 0x0005439C
		public static List<CustomerServiceMould> Get(string sellerNick)
		{
			string text = "SELECT * FROM CustomerServiceMould WHERE SellerNick = " + DbUtil.ToSqlString(sellerNick);
			DataTable dataTable = CustomerServiceMouldManager.a.ExecuteTable(text);
			List<CustomerServiceMould> list2;
			if (DbUtil.HasMoreRow(dataTable))
			{
				List<CustomerServiceMould> list = new List<CustomerServiceMould>();
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					list.Add(CustomerServiceMouldManager.a(dataRow));
				}
				list2 = list;
			}
			else
			{
				list2 = null;
			}
			return list2;
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x0005623C File Offset: 0x0005443C
		public static long Insert(string sellerNick, string title)
		{
			string text = string.Concat(new string[]
			{
				"INSERT INTO CustomerServiceMould (SellerNick, Title, CreateTime) VALUES (",
				DbUtil.ToSqlString(sellerNick),
				", ",
				DbUtil.ToSqlString(title),
				", ",
				DbUtil.ToSqlString(DateTime.Now),
				");select last_insert_rowid() newid"
			});
			return Util.ToLong(CustomerServiceMouldManager.a.ExecuteScalar(text));
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000562B0 File Offset: 0x000544B0
		public static int Update(long idno, string title)
		{
			string text = string.Concat(new string[]
			{
				"UPDATE CustomerServiceMould SET Title = ",
				DbUtil.ToSqlString(title),
				", ModifyTime = ",
				DbUtil.ToSqlString(DateTime.Now),
				" WHERE IdNo = ",
				DbUtil.ToSqlString(idno)
			});
			return CustomerServiceMouldManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x0005631C File Offset: 0x0005451C
		public static int Delete(long idNo)
		{
			string text = "DELETE FROM CustomerServiceMould WHERE IdNo = " + DbUtil.ToSqlString(idNo);
			return CustomerServiceMouldManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x00056350 File Offset: 0x00054550
		private static CustomerServiceMould a(DataRow A_0)
		{
			CustomerServiceMould customerServiceMould;
			if (A_0 == null)
			{
				customerServiceMould = null;
			}
			else
			{
				CustomerServiceMould customerServiceMould2 = new CustomerServiceMould();
				if (A_0.Table.Columns.Contains("IdNo"))
				{
					customerServiceMould2.IdNo = DbUtil.TrimLongNull(A_0["IdNo"]);
				}
				if (A_0.Table.Columns.Contains("SellerNick"))
				{
					customerServiceMould2.SellerNick = DbUtil.TrimNull(A_0["SellerNick"]);
				}
				if (A_0.Table.Columns.Contains("Title"))
				{
					customerServiceMould2.Title = DbUtil.TrimNull(A_0["Title"]);
				}
				if (A_0.Table.Columns.Contains("ModifyTime"))
				{
					customerServiceMould2.ModifyTime = DbUtil.TrimDateNull(A_0["ModifyTime"]);
				}
				if (A_0.Table.Columns.Contains("CreateTime"))
				{
					customerServiceMould2.CreateTime = DbUtil.TrimDateNull(A_0["CreateTime"]);
				}
				customerServiceMould = customerServiceMould2;
			}
			return customerServiceMould;
		}

		// Token: 0x04001284 RID: 4740
		private static IDbAccess a = DbAccessDAL.CreateDbAccess();
	}
}
