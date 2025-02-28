using System;
using System.Data;

namespace Agiso.DBAccess
{
	// Token: 0x020000FC RID: 252
	public interface IDbAccess
	{
		// Token: 0x060007D1 RID: 2001
		void SetConnString(string connstringString);

		// Token: 0x060007D2 RID: 2002
		DataRow ExecuteRow(string commandText);

		// Token: 0x060007D3 RID: 2003
		DataRow ExecuteRow(string fields, string tableName, string whereCondition);

		// Token: 0x060007D4 RID: 2004
		DataTable ExecuteTable(string commandText);

		// Token: 0x060007D5 RID: 2005
		DataTable ExecuteTable(string fields, string tableName, string whereCondition);

		// Token: 0x060007D6 RID: 2006
		DataTable ExecuteTable(string fields, string tableName, string whereCondition, string sortExpression, long pageNumber, long pageSize);

		// Token: 0x060007D7 RID: 2007
		int ExecuteCount(string tableName, string whereCondition);

		// Token: 0x060007D8 RID: 2008
		bool HasRow(string tableName, string whereCondition);

		// Token: 0x060007D9 RID: 2009
		DataSet ExecuteDataset(string commandText);

		// Token: 0x060007DA RID: 2010
		int ExecuteNonQuery(string commandText);

		// Token: 0x060007DB RID: 2011
		int ExecuteNonQuery(IDbCommand cmd);

		// Token: 0x060007DC RID: 2012
		int ExecuteUpdate(string tableName, string setExpression, string whereCondition);

		// Token: 0x060007DD RID: 2013
		int ExecuteInsert(string tableName, string fields, string values, string whereNotExistsCondition = null);

		// Token: 0x060007DE RID: 2014
		object ExecuteScalar(string commandText);

		// Token: 0x060007DF RID: 2015
		object ExecuteScalar(IDbCommand cmd);

		// Token: 0x060007E0 RID: 2016
		IDataReader ExecuteReader(string commandText);

		// Token: 0x060007E1 RID: 2017
		IDataReader ExecuteReader(IDbCommand cmd);

		// Token: 0x060007E2 RID: 2018
		int InsertTable(DataTable table);

		// Token: 0x060007E3 RID: 2019
		void BatchUpdate(DataTable table);

		// Token: 0x060007E4 RID: 2020
		int BulkInsert(DataTable table);
	}
}
