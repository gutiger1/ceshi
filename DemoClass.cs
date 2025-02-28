using System;
using System.Runtime.CompilerServices;
using Agiso.DBAccess;

// Token: 0x02000011 RID: 17
public class DemoClass : IDemoInterFace
{
	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000060 RID: 96 RVA: 0x000025BD File Offset: 0x000007BD
	// (set) Token: 0x06000061 RID: 97 RVA: 0x000025C5 File Offset: 0x000007C5
	public long DemoProt { get; set; }

	// Token: 0x06000062 RID: 98 RVA: 0x00010FBC File Offset: 0x0000F1BC
	public int DemoFunc(int demoParam)
	{
		string text = "INSERT INTO demo_table (IdNo, Val) VALUES ({0}, {1});";
		string.Format(text, DbUtil.ToSqlString(1), DbUtil.ToSqlString("value"));
		DbUtil.ToSqlString(1) + "," + DbUtil.ToSqlString("value");
		string text2 = "\r\nUPDATE demo_table\r\nSET Val = {0}\r\nWHERE IdNo = {1}\r\n";
		string.Format(text2, DbUtil.ToSqlString("value"), DbUtil.ToSqlString(1));
		return 0;
	}

	// Token: 0x04000041 RID: 65
	[CompilerGenerated]
	private long a;
}
