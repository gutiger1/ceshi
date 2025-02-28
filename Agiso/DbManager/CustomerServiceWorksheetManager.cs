using System;
using System.Collections.Generic;
using System.Data;
using Agiso.DBAccess;
using Agiso.WwWebSocket.Model;

namespace Agiso.DbManager
{
	// Token: 0x020006A3 RID: 1699
	public class CustomerServiceWorksheetManager
	{
		// Token: 0x060020EA RID: 8426 RVA: 0x00056460 File Offset: 0x00054660
		public static List<CustomerServiceWorksheet> GetList(long mouldId, string sellerNick)
		{
			List<CustomerServiceWorksheet> list;
			if (mouldId <= 0L)
			{
				list = null;
			}
			else
			{
				string text = "SELECT * FROM CustomerServiceWorksheet WHERE MouldId = " + DbUtil.ToSqlString(mouldId) + " AND SellerNick = " + DbUtil.ToSqlString(sellerNick);
				DataTable dataTable = CustomerServiceWorksheetManager.a.ExecuteTable(text);
				if (DbUtil.HasMoreRow(dataTable))
				{
					List<CustomerServiceWorksheet> list2 = new List<CustomerServiceWorksheet>();
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						list2.Add(CustomerServiceWorksheetManager.a(dataRow));
					}
					list = list2;
				}
				else
				{
					list = null;
				}
			}
			return list;
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x00056528 File Offset: 0x00054728
		public static List<CustomerServiceWorksheet> GetList()
		{
			string text = "SELECT * FROM CustomerServiceWorksheet";
			DataTable dataTable = CustomerServiceWorksheetManager.a.ExecuteTable(text);
			List<CustomerServiceWorksheet> list2;
			if (DbUtil.HasMoreRow(dataTable))
			{
				List<CustomerServiceWorksheet> list = new List<CustomerServiceWorksheet>();
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					list.Add(CustomerServiceWorksheetManager.a(dataRow));
				}
				list2 = list;
			}
			else
			{
				list2 = null;
			}
			return list2;
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000565BC File Offset: 0x000547BC
		public static int Insert(CustomerServiceWorksheet worksheet)
		{
			string text = string.Concat(new string[]
			{
				"INSERT INTO CustomerServiceWorksheet (MouldId, SellerNick, UserNick, WorkTimeJson, CreateTime)\r\n VALUES (",
				DbUtil.ToSqlString(worksheet.MouldId),
				", ",
				DbUtil.ToSqlString(worksheet.SellerNick),
				", ",
				DbUtil.ToSqlString(worksheet.UserNick),
				"\r\n, ",
				DbUtil.ToSqlString(worksheet.WorkTimeJson),
				", ",
				DbUtil.ToSqlString(DateTime.Now),
				")"
			});
			return CustomerServiceWorksheetManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x0005666C File Offset: 0x0005486C
		public static int Update(CustomerServiceWorksheet worksheet)
		{
			string text = string.Concat(new string[]
			{
				"UPDATE CustomerServiceWorksheet SET UserNick = ",
				DbUtil.ToSqlString(worksheet.UserNick),
				"\r\n, WorkTimeJson = ",
				DbUtil.ToSqlString(worksheet.WorkTimeJson),
				"\r\nWHERE IdNo = ",
				DbUtil.ToSqlString(worksheet.IdNo)
			});
			return CustomerServiceWorksheetManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x000566DC File Offset: 0x000548DC
		public static int Delete(long idNo)
		{
			string text = "DELETE FROM CustomerServiceWorksheet WHERE IdNo = " + DbUtil.ToSqlString(idNo);
			return CustomerServiceWorksheetManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x00056710 File Offset: 0x00054910
		public static int DeleteByMouldId(long mouldId)
		{
			string text = "DELETE FROM CustomerServiceWorksheet WHERE MouldId = " + DbUtil.ToSqlString(mouldId);
			return CustomerServiceWorksheetManager.a.ExecuteNonQuery(text);
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x00056744 File Offset: 0x00054944
		private static CustomerServiceWorksheet a(DataRow A_0)
		{
			CustomerServiceWorksheet customerServiceWorksheet;
			if (A_0 == null)
			{
				customerServiceWorksheet = null;
			}
			else
			{
				CustomerServiceWorksheet customerServiceWorksheet2 = new CustomerServiceWorksheet();
				if (A_0.Table.Columns.Contains("IdNo"))
				{
					customerServiceWorksheet2.IdNo = DbUtil.TrimLongNull(A_0["IdNo"]);
				}
				if (A_0.Table.Columns.Contains("MouldId"))
				{
					customerServiceWorksheet2.MouldId = DbUtil.TrimLongNull(A_0["MouldId"]);
				}
				if (A_0.Table.Columns.Contains("SellerNick"))
				{
					customerServiceWorksheet2.SellerNick = DbUtil.TrimNull(A_0["SellerNick"]);
				}
				if (A_0.Table.Columns.Contains("UserNick"))
				{
					customerServiceWorksheet2.UserNick = DbUtil.TrimNull(A_0["UserNick"]);
				}
				if (A_0.Table.Columns.Contains("ModifyTime"))
				{
					customerServiceWorksheet2.ModifyTime = DbUtil.TrimDateNull(A_0["ModifyTime"]);
				}
				if (A_0.Table.Columns.Contains("CreateTime"))
				{
					customerServiceWorksheet2.CreateTime = DbUtil.TrimDateNull(A_0["CreateTime"]);
				}
				if (A_0.Table.Columns.Contains("WorkTimeJson"))
				{
					customerServiceWorksheet2.WorkTimeJson = DbUtil.TrimNull(A_0["WorkTimeJson"]);
				}
				customerServiceWorksheet = customerServiceWorksheet2;
			}
			return customerServiceWorksheet;
		}

		// Token: 0x04001285 RID: 4741
		private static IDbAccess a = DbAccessDAL.CreateDbAccess();
	}
}
