using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x02000018 RID: 24
[CompilerGenerated]
internal sealed class p
{
	// Token: 0x0600008B RID: 139 RVA: 0x0001133C File Offset: 0x0000F53C
	internal static uint a(string A_0)
	{
		uint num;
		if (A_0 != null)
		{
			num = 2166136261U;
			for (int i = 0; i < A_0.Length; i++)
			{
				num = ((uint)A_0[i] ^ num) * 16777619U;
			}
		}
		return num;
	}

	// Token: 0x04000050 RID: 80 RVA: 0x00002050 File Offset: 0x00000250
	// Note: this field is marked with 'hasfieldrva'.
	internal static readonly p.b a;

	// Token: 0x04000051 RID: 81 RVA: 0x00002090 File Offset: 0x00000290
	// Note: this field is marked with 'hasfieldrva'.
	internal static readonly p.a b;

	// Token: 0x04000052 RID: 82 RVA: 0x00002098 File Offset: 0x00000298
	// Note: this field is marked with 'hasfieldrva'.
	internal static readonly p.a c;

	// Token: 0x02000019 RID: 25
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 3)]
	internal struct a
	{
	}

	// Token: 0x0200001A RID: 26
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 58)]
	internal struct b
	{
	}
}
