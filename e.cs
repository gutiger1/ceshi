using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x0200000A RID: 10
[DebuggerDisplay("\\{ AllowAutoExitQn = {AllowAutoExitQn}, LoginOnQn = {LoginOnQn}, LoginOnAliwwBuyer = {LoginOnAliwwBuyer}, AllowAutoKillQnTimeFrom = {AllowAutoKillQnTimeFrom}, AllowAutoKillQnTimeTo = {AllowAutoKillQnTimeTo}, SelectWeekDays = {SelectWeekDays}, QnVersion = {QnVersion}, AutoMinimizeTalkWindow = {AutoMinimizeTalkWindow}, SwitchNickAfterFiveMsg = {SwitchNickAfterFiveMsg}, AutoKillAllAliWorkbenchAndAliApp = {AutoKillAllAliWorkbenchAndAliApp} ... }", Type = "<Anonymous Type>")]
[CompilerGenerated]
internal sealed class e<a, b, c, d, e, f, g, h, i, j, k>
{
	// Token: 0x0600003A RID: 58 RVA: 0x00002459 File Offset: 0x00000659
	public a v()
	{
		return this.a;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00002461 File Offset: 0x00000661
	public b u()
	{
		return this.b;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00002469 File Offset: 0x00000669
	public c t()
	{
		return this.c;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00002471 File Offset: 0x00000671
	public d s()
	{
		return this.d;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00002479 File Offset: 0x00000679
	public e r()
	{
		return this.e;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00002481 File Offset: 0x00000681
	public f q()
	{
		return this.f;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00002489 File Offset: 0x00000689
	public g p()
	{
		return this.g;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00002491 File Offset: 0x00000691
	public h o()
	{
		return this.h;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00002499 File Offset: 0x00000699
	public i n()
	{
		return this.i;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x000024A1 File Offset: 0x000006A1
	public j m()
	{
		return this.j;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x000024A9 File Offset: 0x000006A9
	public k l()
	{
		return this.k;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x000108A4 File Offset: 0x0000EAA4
	public e(a A_0, b A_1, c A_2, d A_3, e A_4, f A_5, g A_6, h A_7, i A_8, j A_9, k A_10)
	{
		this.a = A_0;
		this.b = A_1;
		this.c = A_2;
		this.d = A_3;
		this.e = A_4;
		this.f = A_5;
		this.g = A_6;
		this.h = A_7;
		this.i = A_8;
		this.j = A_9;
		this.k = A_10;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00010910 File Offset: 0x0000EB10
	public override bool Equals(object value)
	{
		global::e<a, b, c, d, e, f, g, h, i, j, k> e = value as global::e<a, b, c, d, e, f, g, h, i, j, k>;
		return this == e || (e != null && EqualityComparer<a>.Default.Equals(this.a, e.a) && EqualityComparer<b>.Default.Equals(this.b, e.b) && EqualityComparer<c>.Default.Equals(this.c, e.c) && EqualityComparer<d>.Default.Equals(this.d, e.d) && EqualityComparer<e>.Default.Equals(this.e, e.e) && EqualityComparer<f>.Default.Equals(this.f, e.f) && EqualityComparer<g>.Default.Equals(this.g, e.g) && EqualityComparer<h>.Default.Equals(this.h, e.h) && EqualityComparer<i>.Default.Equals(this.i, e.i) && EqualityComparer<j>.Default.Equals(this.j, e.j) && EqualityComparer<k>.Default.Equals(this.k, e.k));
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00010A4C File Offset: 0x0000EC4C
	public override int GetHashCode()
	{
		return ((((((((((560751548 + EqualityComparer<a>.Default.GetHashCode(this.a)) * -1521134295 + EqualityComparer<b>.Default.GetHashCode(this.b)) * -1521134295 + EqualityComparer<c>.Default.GetHashCode(this.c)) * -1521134295 + EqualityComparer<d>.Default.GetHashCode(this.d)) * -1521134295 + EqualityComparer<e>.Default.GetHashCode(this.e)) * -1521134295 + EqualityComparer<f>.Default.GetHashCode(this.f)) * -1521134295 + EqualityComparer<g>.Default.GetHashCode(this.g)) * -1521134295 + EqualityComparer<h>.Default.GetHashCode(this.h)) * -1521134295 + EqualityComparer<i>.Default.GetHashCode(this.i)) * -1521134295 + EqualityComparer<j>.Default.GetHashCode(this.j)) * -1521134295 + EqualityComparer<k>.Default.GetHashCode(this.k);
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00010B58 File Offset: 0x0000ED58
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ AllowAutoExitQn = {0}, LoginOnQn = {1}, LoginOnAliwwBuyer = {2}, AllowAutoKillQnTimeFrom = {3}, AllowAutoKillQnTimeTo = {4}, SelectWeekDays = {5}, QnVersion = {6}, AutoMinimizeTalkWindow = {7}, SwitchNickAfterFiveMsg = {8}, AutoKillAllAliWorkbenchAndAliApp = {9}, AlicdnIp = {10} }}";
		object[] array = new object[11];
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
		int num7 = 6;
		g g = this.g;
		array[num7] = ((g != null) ? g.ToString() : null);
		int num8 = 7;
		h h = this.h;
		array[num8] = ((h != null) ? h.ToString() : null);
		int num9 = 8;
		i i = this.i;
		array[num9] = ((i != null) ? i.ToString() : null);
		int num10 = 9;
		j j = this.j;
		array[num10] = ((j != null) ? j.ToString() : null);
		int num11 = 10;
		k k = this.k;
		array[num11] = ((k != null) ? k.ToString() : null);
		return string.Format(formatProvider, text, array);
	}

	// Token: 0x0400002A RID: 42
	private readonly a a;

	// Token: 0x0400002B RID: 43
	private readonly b b;

	// Token: 0x0400002C RID: 44
	private readonly c c;

	// Token: 0x0400002D RID: 45
	private readonly d d;

	// Token: 0x0400002E RID: 46
	private readonly e e;

	// Token: 0x0400002F RID: 47
	private readonly f f;

	// Token: 0x04000030 RID: 48
	private readonly g g;

	// Token: 0x04000031 RID: 49
	private readonly h h;

	// Token: 0x04000032 RID: 50
	private readonly i i;

	// Token: 0x04000033 RID: 51
	private readonly j j;

	// Token: 0x04000034 RID: 52
	private readonly k k;
}
