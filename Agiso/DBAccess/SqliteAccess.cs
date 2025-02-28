using System;
using System.Data;
using System.Data.SQLite;
using Agiso.Utils;

namespace Agiso.DBAccess
{
	// Token: 0x020000FD RID: 253
	public class SqliteAccess : IDbAccess
	{
		// Token: 0x060007E5 RID: 2021 RVA: 0x000045C3 File Offset: 0x000027C3
		public void SetConnString(string connstringString)
		{
			this.a = connstringString;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0004FDD8 File Offset: 0x0004DFD8
		public DataRow ExecuteRow(string commandText)
		{
			DataTable dataTable = this.ExecuteTable(commandText);
			DataRow dataRow;
			if (dataTable == null || dataTable.Rows.Count == 0)
			{
				dataRow = null;
			}
			else
			{
				dataRow = dataTable.Rows[0];
			}
			return dataRow;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0004FE18 File Offset: 0x0004E018
		public DataRow ExecuteRow(string fields, string tableName, string whereCondition)
		{
			string text = string.Format("SELECT TOP 1 {0} FROM {1} WHERE {2}", fields, tableName, whereCondition);
			return this.ExecuteRow(text);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0004FE40 File Offset: 0x0004E040
		public DataTable ExecuteTable(string commandText)
		{
			DataTable dataTable = new DataTable();
			SQLiteConnection sqliteConnection = null;
			try
			{
				sqliteConnection = new SQLiteConnection(this.a);
				sqliteConnection.Open();
				SQLiteDataAdapter sqliteDataAdapter = new SQLiteDataAdapter(commandText, sqliteConnection);
				sqliteDataAdapter.Fill(dataTable);
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 2);
				throw ex;
			}
			finally
			{
				sqliteConnection.Close();
				sqliteConnection.Dispose();
			}
			return dataTable;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0004FEBC File Offset: 0x0004E0BC
		public DataTable ExecuteTable(string fields, string tableName, string whereCondition)
		{
			string text = string.Concat(new string[] { "SELECT ", fields, " FROM ", tableName, " WHERE 1=1 AND ", whereCondition });
			return this.ExecuteTable(text);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0004FF08 File Offset: 0x0004E108
		public DataTable ExecuteTable(string fields, string tableName, string whereCondition, string sortExpression, long pageNumber, long pageSize)
		{
			DataTable dataTable;
			if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(fields))
			{
				dataTable = null;
			}
			else
			{
				if (string.IsNullOrEmpty(whereCondition))
				{
					whereCondition = "1=1";
				}
				string text;
				if (string.IsNullOrEmpty(sortExpression))
				{
					text = "\r\nSELECT {0}\r\n  FROM {1}\r\n WHERE {2}\r\n LIMIT {3}, {4}\r\n";
				}
				else
				{
					text = "\r\nSELECT {0}\r\n  FROM {1}\r\n WHERE {2}\r\n ORDER BY {5}\r\n LIMIT {3}, {4}\r\n";
				}
				text = string.Format(text, new object[]
				{
					fields,
					tableName,
					whereCondition,
					(pageNumber - 1L) * pageSize,
					pageSize,
					sortExpression
				});
				dataTable = this.ExecuteTable(text);
			}
			return dataTable;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0004FFA8 File Offset: 0x0004E1A8
		public int ExecuteCount(string tableName, string whereCondition)
		{
			string text = "SELECT COUNT(1) FROM " + tableName + " WHERE " + whereCondition;
			int num;
			using (SQLiteConnection sqliteConnection = new SQLiteConnection(this.a))
			{
				if (sqliteConnection.State != ConnectionState.Open)
				{
					sqliteConnection.Open();
				}
				object obj = new SQLiteCommand(sqliteConnection)
				{
					CommandText = text
				}.ExecuteScalar();
				num = DbUtil.TrimIntNull(obj);
			}
			return num;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00050024 File Offset: 0x0004E224
		public bool HasRow(string tableName, string whereCondition)
		{
			string text = string.Format("SELECT 1 WHERE EXISTS (SELECT 1 FROM {0} WHERE {1})", tableName, whereCondition);
			object obj = this.ExecuteScalar(text);
			return obj != null;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00050054 File Offset: 0x0004E254
		public DataSet ExecuteDataset(string commandText)
		{
			DataSet dataSet = new DataSet();
			SQLiteConnection sqliteConnection = null;
			try
			{
				sqliteConnection = new SQLiteConnection(this.a);
				sqliteConnection.Open();
				SQLiteDataAdapter sqliteDataAdapter = new SQLiteDataAdapter(commandText, sqliteConnection);
				sqliteDataAdapter.Fill(dataSet);
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 2);
				throw ex;
			}
			finally
			{
				sqliteConnection.Close();
				sqliteConnection.Dispose();
			}
			return dataSet;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000500D0 File Offset: 0x0004E2D0
		public int ExecuteNonQuery(string commandText)
		{
			SQLiteCommand sqliteCommand = new SQLiteCommand(commandText);
			return this.ExecuteNonQuery(sqliteCommand);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x000500F0 File Offset: 0x0004E2F0
		public int ExecuteNonQuery(IDbCommand cmd)
		{
			SQLiteConnection sqliteConnection = new SQLiteConnection(this.a);
			int num;
			try
			{
				cmd.Connection = sqliteConnection;
				sqliteConnection.Open();
				num = cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 2);
				throw ex;
			}
			finally
			{
				if (sqliteConnection.State == ConnectionState.Open)
				{
					sqliteConnection.Close();
				}
				sqliteConnection.Dispose();
			}
			return num;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0005016C File Offset: 0x0004E36C
		public int ExecuteUpdate(string tableName, string setExpression, string whereCondition)
		{
			int num;
			if (string.IsNullOrEmpty(setExpression) || string.IsNullOrEmpty(tableName))
			{
				num = 0;
			}
			else
			{
				if (string.IsNullOrEmpty(whereCondition))
				{
					whereCondition = "1=1";
				}
				string text = string.Concat(new string[] { "UPDATE ", tableName, " SET ", setExpression, " WHERE ", whereCondition });
				num = this.ExecuteNonQuery(text);
			}
			return num;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000501DC File Offset: 0x0004E3DC
		public int ExecuteInsert(string tableName, string fields, string values, string whereNotExistsCondition = null)
		{
			int num;
			if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(fields) || string.IsNullOrEmpty(values))
			{
				num = 0;
			}
			else
			{
				string text = string.Format("INSERT INTO {0} ({1}) SELECT {2}", tableName, fields, values);
				if (!string.IsNullOrEmpty(whereNotExistsCondition))
				{
					if (whereNotExistsCondition.Trim().StartsWith("SELECT ", StringComparison.OrdinalIgnoreCase))
					{
						text += string.Format(" WHERE NOT EXISTS({0})", whereNotExistsCondition);
					}
					else
					{
						text += string.Format(" WHERE NOT EXISTS(SELECT 1 FROM {0} WHERE {1})", tableName, whereNotExistsCondition);
					}
				}
				num = this.ExecuteNonQuery(text);
			}
			return num;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0005026C File Offset: 0x0004E46C
		public object ExecuteScalar(string commandText)
		{
			SQLiteCommand sqliteCommand = new SQLiteCommand(commandText);
			return this.ExecuteScalar(sqliteCommand);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0005028C File Offset: 0x0004E48C
		public object ExecuteScalar(IDbCommand cmd)
		{
			SQLiteConnection sqliteConnection = new SQLiteConnection(this.a);
			object obj = null;
			try
			{
				cmd.Connection = sqliteConnection;
				sqliteConnection.Open();
				obj = cmd.ExecuteScalar();
				cmd.Parameters.Clear();
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 2);
				throw ex;
			}
			finally
			{
				if (sqliteConnection.State == ConnectionState.Open)
				{
					sqliteConnection.Close();
				}
				sqliteConnection.Dispose();
			}
			return obj;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00050314 File Offset: 0x0004E514
		public IDataReader ExecuteReader(string commandText)
		{
			SQLiteCommand sqliteCommand = new SQLiteCommand(commandText);
			return this.ExecuteReader(sqliteCommand);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00050334 File Offset: 0x0004E534
		public IDataReader ExecuteReader(IDbCommand cmd)
		{
			SQLiteConnection sqliteConnection = new SQLiteConnection(this.a);
			SQLiteDataReader sqliteDataReader = null;
			try
			{
				cmd.Connection = sqliteConnection;
				sqliteConnection.Open();
				sqliteDataReader = ((SQLiteCommand)cmd).ExecuteReader(CommandBehavior.CloseConnection);
				cmd.Parameters.Clear();
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 2);
				throw ex;
			}
			return sqliteDataReader;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0005039C File Offset: 0x0004E59C
		public int InsertTable(DataTable table)
		{
			int num = 0;
			for (int i = 0; i < table.Rows.Count; i++)
			{
				string text = "INSERT INTO " + table.TableName + " ({0}) VALUES ({1})";
				string text2 = "";
				string text3 = "";
				for (int j = 0; j < table.Columns.Count; j++)
				{
					text2 = text2 + "," + table.Columns[j].ColumnName;
					text3 = text3 + "," + DbUtil.ToSqlString(table.Rows[i][j]);
				}
				text2 = text2.Substring(1);
				text3 = text3.Substring(1);
				text = string.Format(text, text2, text3);
				num += this.ExecuteNonQuery(text);
			}
			return num;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000045CC File Offset: 0x000027CC
		public int BulkInsert(DataTable table)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000045CC File Offset: 0x000027CC
		public void BatchUpdate(DataTable table)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x040004F1 RID: 1265
		private string a;
	}
}
