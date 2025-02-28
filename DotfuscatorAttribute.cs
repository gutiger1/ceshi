using System;
using System.Runtime.InteropServices;

// Token: 0x02000004 RID: 4
[ComVisible(false)]
[AttributeUsage(AttributeTargets.Assembly)]
public sealed class DotfuscatorAttribute : Attribute
{
	// Token: 0x06000002 RID: 2 RVA: 0x00002295 File Offset: 0x00000495
	public DotfuscatorAttribute(string a, int c)
	{
		this.a = a;
		this.c = c;
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000003 RID: 3 RVA: 0x000022AC File Offset: 0x000004AC
	public string A
	{
		get
		{
			return this.a;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000004 RID: 4 RVA: 0x000022B4 File Offset: 0x000004B4
	public int C
	{
		get
		{
			return this.c;
		}
	}

	// Token: 0x04000001 RID: 1
	private string a;

	// Token: 0x04000002 RID: 2
	private int c;
}
