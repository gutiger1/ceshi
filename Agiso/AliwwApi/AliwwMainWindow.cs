using System;
using System.Runtime.CompilerServices;
using Agiso.AliwwApi.Object;
using Agiso.Extensions;
using Agiso.Windows;

namespace Agiso.AliwwApi
{
	// Token: 0x02000718 RID: 1816
	public class AliwwMainWindow : WindowInfo
	{
		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060023F3 RID: 9203 RVA: 0x0000ED1C File Offset: 0x0000CF1C
		// (set) Token: 0x060023F4 RID: 9204 RVA: 0x0000ED24 File Offset: 0x0000CF24
		public AliwwVersion AliVersion { get; set; }

		// Token: 0x060023F5 RID: 9205 RVA: 0x0005F764 File Offset: 0x0005D964
		public static AliwwMainWindow ParseFromWindowInfo(WindowInfo source)
		{
			AliwwMainWindow aliwwMainWindow;
			if (source == null)
			{
				aliwwMainWindow = null;
			}
			else
			{
				aliwwMainWindow = source.Convert<AliwwMainWindow>();
			}
			return aliwwMainWindow;
		}

		// Token: 0x04001DE2 RID: 7650
		[CompilerGenerated]
		private AliwwVersion a;
	}
}
