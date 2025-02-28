using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000769 RID: 1897
	[TypeIdentifier]
	[CompilerGenerated]
	[Guid("3050F1FF-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLElement
	{
		// Token: 0x0600261A RID: 9754
		void _VtblGap1_50();

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x0600261C RID: 9756
		// (set) Token: 0x0600261B RID: 9755
		[DispId(-2147417086)]
		string innerHTML
		{
			[DispId(-2147417086)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
			[DispId(-2147417086)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.BStr)]
			[param: In]
			set;
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x0600261E RID: 9758
		// (set) Token: 0x0600261D RID: 9757
		[DispId(-2147417085)]
		string innerText
		{
			[DispId(-2147417085)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
			[DispId(-2147417085)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.BStr)]
			[param: In]
			set;
		}
	}
}
