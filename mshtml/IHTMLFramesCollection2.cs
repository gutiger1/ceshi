using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200076A RID: 1898
	[CompilerGenerated]
	[DefaultMember("item")]
	[Guid("332C4426-26CB-11D0-B483-00C04FD90119")]
	[TypeIdentifier]
	[ComImport]
	public interface IHTMLFramesCollection2
	{
		// Token: 0x0600261F RID: 9759
		[DispId(0)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object item([MarshalAs(UnmanagedType.Struct)] [In] ref object pvarIndex);
	}
}
