using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x0200000D RID: 13
[DebuggerDisplay("\\{ type = {type} }", Type = "<Anonymous Type>")]
[CompilerGenerated]
internal sealed class h<a>
{
	// Token: 0x06000054 RID: 84 RVA: 0x00002537 File Offset: 0x00000737
	public a b()
	{
		return this.a;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x0000253F File Offset: 0x0000073F
	public h(a A_0)
	{
		this.a = A_0;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00010E7C File Offset: 0x0000F07C
	public override bool Equals(object value)
	{
		h<a> h = value as h<a>;
		return this == h || (h != null && EqualityComparer<a>.Default.Equals(this.a, h.a));
	}

	// Token: 0x06000057 RID: 87 RVA: 0x0000254F File Offset: 0x0000074F
	public override int GetHashCode()
	{
		return -122111396 + EqualityComparer<a>.Default.GetHashCode(this.a);
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00010EB4 File Offset: 0x0000F0B4
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ type = {0} }}";
		object[] array = new object[1];
		int num = 0;
		a a = this.a;
		array[num] = ((a != null) ? a.ToString() : null);
		return string.Format(formatProvider, text, array);
	}

	// Token: 0x04000038 RID: 56
	private readonly a a;
}
