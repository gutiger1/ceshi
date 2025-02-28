using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x0200000E RID: 14
[CompilerGenerated]
[DebuggerDisplay("\\{ type = {type}, captcha = {captcha} }", Type = "<Anonymous Type>")]
internal sealed class i<a, b>
{
	// Token: 0x06000059 RID: 89 RVA: 0x00002567 File Offset: 0x00000767
	public a d()
	{
		return this.a;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x0000256F File Offset: 0x0000076F
	public b c()
	{
		return this.b;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00002577 File Offset: 0x00000777
	public i(a A_0, b A_1)
	{
		this.a = A_0;
		this.b = A_1;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00010EFC File Offset: 0x0000F0FC
	public override bool Equals(object value)
	{
		i<a, b> i = value as i<a, b>;
		return this == i || (i != null && EqualityComparer<a>.Default.Equals(this.a, i.a) && EqualityComparer<b>.Default.Equals(this.b, i.b));
	}

	// Token: 0x0600005D RID: 93 RVA: 0x0000258E File Offset: 0x0000078E
	public override int GetHashCode()
	{
		return (1829620644 + EqualityComparer<a>.Default.GetHashCode(this.a)) * -1521134295 + EqualityComparer<b>.Default.GetHashCode(this.b);
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00010F4C File Offset: 0x0000F14C
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ type = {0}, captcha = {1} }}";
		object[] array = new object[2];
		int num = 0;
		a a = this.a;
		array[num] = ((a != null) ? a.ToString() : null);
		int num2 = 1;
		b b = this.b;
		array[num2] = ((b != null) ? b.ToString() : null);
		return string.Format(formatProvider, text, array);
	}

	// Token: 0x04000039 RID: 57
	private readonly a a;

	// Token: 0x0400003A RID: 58
	private readonly b b;
}
