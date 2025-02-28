using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000007 RID: 7
[CompilerGenerated]
[DebuggerDisplay("\\{ type = {type}, host = {host}, port = {port}, domain = {domain}, user = {user}, password = {password} }", Type = "<Anonymous Type>")]
internal sealed class b<a, b, c, d, e, f>
{
	// Token: 0x06000017 RID: 23 RVA: 0x00002325 File Offset: 0x00000525
	public a l()
	{
		return this.a;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x0000232D File Offset: 0x0000052D
	public b k()
	{
		return this.b;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002335 File Offset: 0x00000535
	public c j()
	{
		return this.c;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000233D File Offset: 0x0000053D
	public d i()
	{
		return this.d;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002345 File Offset: 0x00000545
	public e h()
	{
		return this.e;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x0000234D File Offset: 0x0000054D
	public f g()
	{
		return this.f;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002355 File Offset: 0x00000555
	public b(a A_0, b A_1, c A_2, d A_3, e A_4, f A_5)
	{
		this.a = A_0;
		this.b = A_1;
		this.c = A_2;
		this.d = A_3;
		this.e = A_4;
		this.f = A_5;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x0000FF58 File Offset: 0x0000E158
	public override bool Equals(object value)
	{
		global::b<a, b, c, d, e, f> b = value as global::b<a, b, c, d, e, f>;
		return this == b || (b != null && EqualityComparer<a>.Default.Equals(this.a, b.a) && EqualityComparer<b>.Default.Equals(this.b, b.b) && EqualityComparer<c>.Default.Equals(this.c, b.c) && EqualityComparer<d>.Default.Equals(this.d, b.d) && EqualityComparer<e>.Default.Equals(this.e, b.e) && EqualityComparer<f>.Default.Equals(this.f, b.f));
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00010010 File Offset: 0x0000E210
	public override int GetHashCode()
	{
		return (((((-1131190813 + EqualityComparer<a>.Default.GetHashCode(this.a)) * -1521134295 + EqualityComparer<b>.Default.GetHashCode(this.b)) * -1521134295 + EqualityComparer<c>.Default.GetHashCode(this.c)) * -1521134295 + EqualityComparer<d>.Default.GetHashCode(this.d)) * -1521134295 + EqualityComparer<e>.Default.GetHashCode(this.e)) * -1521134295 + EqualityComparer<f>.Default.GetHashCode(this.f);
	}

	// Token: 0x06000020 RID: 32 RVA: 0x000100A8 File Offset: 0x0000E2A8
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ type = {0}, host = {1}, port = {2}, domain = {3}, user = {4}, password = {5} }}";
		object[] array = new object[6];
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
		int num5 = 4;
		e e = this.e;
		array[num5] = ((e != null) ? e.ToString() : null);
		int num6 = 5;
		f f = this.f;
		array[num6] = ((f != null) ? f.ToString() : null);
		return string.Format(formatProvider, text, array);
	}

	// Token: 0x04000013 RID: 19
	private readonly a a;

	// Token: 0x04000014 RID: 20
	private readonly b b;

	// Token: 0x04000015 RID: 21
	private readonly c c;

	// Token: 0x04000016 RID: 22
	private readonly d d;

	// Token: 0x04000017 RID: 23
	private readonly e e;

	// Token: 0x04000018 RID: 24
	private readonly f f;
}
