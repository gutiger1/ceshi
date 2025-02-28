using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000768 RID: 1896
	[Guid("3050F485-98B5-11CF-BB82-00AA00BDCE0B")]
	[CompilerGenerated]
	[TypeIdentifier]
	[ComImport]
	public interface IHTMLDocument3
	{
		// Token: 0x06002618 RID: 9752
		void _VtblGap1_39();

		// Token: 0x06002619 RID: 9753
		[DispId(1088)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLElement getElementById([MarshalAs(UnmanagedType.BStr)] [In] string v);
	}
}
