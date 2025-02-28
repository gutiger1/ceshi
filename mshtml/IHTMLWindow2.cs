using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200076B RID: 1899
	[TypeIdentifier]
	[Guid("332C4427-26CB-11D0-B483-00C04FD90119")]
	[CompilerGenerated]
	[DefaultMember("item")]
	[ComImport]
	public interface IHTMLWindow2 : IHTMLFramesCollection2
	{
		// Token: 0x06002620 RID: 9760
		[DispId(0)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object item([MarshalAs(UnmanagedType.Struct)] [In] ref object pvarIndex);

		// Token: 0x06002621 RID: 9761
		void _VtblGap1_60();

		// Token: 0x06002622 RID: 9762
		[DispId(1165)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object execScript([MarshalAs(UnmanagedType.BStr)] [In] string code, [MarshalAs(UnmanagedType.BStr)] [In] string language = "JScript");
	}
}
