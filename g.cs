using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x0200000C RID: 12
[CompilerGenerated]
[DebuggerDisplay("\\{ type = {type}, guid = {guid} }", Type = "<Anonymous Type>")]
internal sealed class g<a, b>
{
	// Token: 0x0600004E RID: 78 RVA: 0x000024E1 File Offset: 0x000006E1
	public a d()
	{
		return this.a;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000024E9 File Offset: 0x000006E9
	public b c()
	{
		return this.b;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000024F1 File Offset: 0x000006F1
	public g(a A_0, b A_1)
	{
		this.a = A_0;
		this.b = A_1;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00010DBC File Offset: 0x0000EFBC
	public override bool Equals(object value)
	{
		g<a, b> g = value as g<a, b>;
		return this == g || (g != null && EqualityComparer<a>.Default.Equals(this.a, g.a) && EqualityComparer<b>.Default.Equals(this.b, g.b));
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00002508 File Offset: 0x00000708
	public override int GetHashCode()
	{
		return (-410030873 + EqualityComparer<a>.Default.GetHashCode(this.a)) * -1521134295 + EqualityComparer<b>.Default.GetHashCode(this.b);
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00010E0C File Offset: 0x0000F00C
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ type = {0}, guid = {1} }}";
		object[] array = new object[2];
		int num = 0;
		a a = this.a;
		array[num] = ((a != null) ? a.ToString() : null);
		int num2 = 1;
		b b = this.b;
		array[num2] = ((b != null) ? b.ToString() : null);
		return string.Format(formatProvider, text, array);
	}

	// Token: 0x04000036 RID: 54
	private readonly a a;

	// Token: 0x04000037 RID: 55
	private readonly b b;
}
