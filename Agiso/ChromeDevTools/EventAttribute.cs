using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools
{
	// Token: 0x02000117 RID: 279
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class EventAttribute : Attribute, GInterface0
	{
		// Token: 0x06000893 RID: 2195 RVA: 0x000048FD File Offset: 0x00002AFD
		public EventAttribute(string methodName)
		{
			this.MethodName = methodName;
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0000490D File Offset: 0x00002B0D
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x00004915 File Offset: 0x00002B15
		public string MethodName { get; private set; }

		// Token: 0x0400052F RID: 1327
		[CompilerGenerated]
		private string a;
	}
}
