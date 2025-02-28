using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000006 RID: 6
[CompilerGenerated]
[DebuggerDisplay("\\{ StartX = {StartX}, StartY = {StartY}, EndX = {EndX}, EndY = {EndY} }", Type = "<Anonymous Type>")]
internal sealed class a<a, b, c, d>
{
	// Token: 0x0600000F RID: 15 RVA: 0x000022DF File Offset: 0x000004DF
	public a h()
	{
		return this.a;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x000022E7 File Offset: 0x000004E7
	public b g()
	{
		return this.b;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000022EF File Offset: 0x000004EF
	public c f()
	{
		return this.c;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000022F7 File Offset: 0x000004F7
	public d e()
	{
		return this.d;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000022FF File Offset: 0x000004FF
	public a(a A_0, b A_1, c A_2, d A_3)
	{
		this.a = A_0;
		this.b = A_1;
		this.c = A_2;
		this.d = A_3;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x0000FDB0 File Offset: 0x0000DFB0
	public override bool Equals(object value)
	{
		global::a<a, b, c, d> a = value as global::a<a, b, c, d>;
		return this == a || (a != null && EqualityComparer<a>.Default.Equals(this.a, a.a) && EqualityComparer<b>.Default.Equals(this.b, a.b) && EqualityComparer<c>.Default.Equals(this.c, a.c) && EqualityComparer<d>.Default.Equals(this.d, a.d));
	}

	// Token: 0x06000015 RID: 21 RVA: 0x0000FE30 File Offset: 0x0000E030
	public override int GetHashCode()
	{
		return (((-414232046 + EqualityComparer<a>.Default.GetHashCode(this.a)) * -1521134295 + EqualityComparer<b>.Default.GetHashCode(this.b)) * -1521134295 + EqualityComparer<c>.Default.GetHashCode(this.c)) * -1521134295 + EqualityComparer<d>.Default.GetHashCode(this.d);
	}

	// Token: 0x06000016 RID: 22 RVA: 0x0000FE98 File Offset: 0x0000E098
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ StartX = {0}, StartY = {1}, EndX = {2}, EndY = {3} }}";
		object[] array = new object[4];
		int num = 0;
		a a = this.a;
		array[num] = ((a != null) ? a.ToString() : null);
		int num2 = 1;
		b b = this.b;
		array[num2] = ((b != null) ? b.ToString() : null);
		int num3 = 2;
		c c = this.c;
		array[num3] = ((c != null) ? c.ToString() : null);
		int num4 = 3;
		d d = this.d;
		array[num4] = ((d != null) ? d.ToString() : null);
		return string.Format(formatProvider, text, array);
	}

	// Token: 0x0400000F RID: 15
	private readonly a a;

	// Token: 0x04000010 RID: 16
	private readonly b b;

	// Token: 0x04000011 RID: 17
	private readonly c c;

	// Token: 0x04000012 RID: 18
	private readonly d d;
}
