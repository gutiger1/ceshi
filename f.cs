using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x0200000B RID: 11
[CompilerGenerated]
[DebuggerDisplay("\\{ IsComplete = {IsComplete} }", Type = "<Anonymous Type>")]
internal sealed class f<a>
{
	// Token: 0x06000049 RID: 73 RVA: 0x000024B1 File Offset: 0x000006B1
	public a b()
	{
		return this.a;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x000024B9 File Offset: 0x000006B9
	public f(a A_0)
	{
		this.a = A_0;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00010D3C File Offset: 0x0000EF3C
	public override bool Equals(object value)
	{
		f<a> f = value as f<a>;
		return this == f || (f != null && EqualityComparer<a>.Default.Equals(this.a, f.a));
	}

	// Token: 0x0600004C RID: 76 RVA: 0x000024C9 File Offset: 0x000006C9
	public override int GetHashCode()
	{
		return 91612381 + EqualityComparer<a>.Default.GetHashCode(this.a);
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00010D74 File Offset: 0x0000EF74
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ IsComplete = {0} }}";
		object[] array = new object[1];
		int num = 0;
		a a = this.a;
		array[num] = ((a != null) ? a.ToString() : null);
		return string.Format(formatProvider, text, array);
	}

	// Token: 0x04000035 RID: 53
	private readonly a a;
}
