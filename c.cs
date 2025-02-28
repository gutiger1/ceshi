using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000008 RID: 8
[CompilerGenerated]
[DebuggerDisplay("\\{ path = {path}, item = {item} }", Type = "<Anonymous Type>")]
internal sealed class c<a, b>
{
	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000021 RID: 33 RVA: 0x0000238B File Offset: 0x0000058B
	public a a
	{
		get
		{
			return this.a;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000022 RID: 34 RVA: 0x00002393 File Offset: 0x00000593
	public b b
	{
		get
		{
			return this.b;
		}
	}

	// Token: 0x06000023 RID: 35 RVA: 0x0000239B File Offset: 0x0000059B
	public c(a A_0, b A_1)
	{
		this.a = A_0;
		this.b = A_1;
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000101BC File Offset: 0x0000E3BC
	public override bool Equals(object value)
	{
		c<a, b> c = value as c<a, b>;
		return this == c || (c != null && EqualityComparer<a>.Default.Equals(this.a, c.a) && EqualityComparer<b>.Default.Equals(this.b, c.b));
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000023B2 File Offset: 0x000005B2
	public override int GetHashCode()
	{
		return (-442717358 + EqualityComparer<a>.Default.GetHashCode(this.a)) * -1521134295 + EqualityComparer<b>.Default.GetHashCode(this.b);
	}

	// Token: 0x06000026 RID: 38 RVA: 0x0001020C File Offset: 0x0000E40C
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ path = {0}, item = {1} }}";
		object[] array = new object[2];
		int num = 0;
		a a = this.a;
		array[num] = ((a != null) ? a.ToString() : null);
		int num2 = 1;
		b b = this.b;
		array[num2] = ((b != null) ? b.ToString() : null);
		return string.Format(formatProvider, text, array);
	}

	// Token: 0x04000019 RID: 25
	private readonly a a;

	// Token: 0x0400001A RID: 26
	private readonly b b;
}
