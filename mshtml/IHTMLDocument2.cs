using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000767 RID: 1895
	[TypeIdentifier]
	[Guid("332C4425-26CB-11D0-B483-00C04FD90119")]
	[CompilerGenerated]
	[ComImport]
	public interface IHTMLDocument2 : IHTMLDocument
	{
		// Token: 0x06002614 RID: 9748
		void _VtblGap1_2();

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06002615 RID: 9749
		[DispId(1004)]
		IHTMLElement body
		{
			[DispId(1004)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x06002616 RID: 9750
		void _VtblGap2_98();

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06002617 RID: 9751
		[DispId(1034)]
		IHTMLWindow2 parentWindow
		{
			[DispId(1034)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}
	}
}
