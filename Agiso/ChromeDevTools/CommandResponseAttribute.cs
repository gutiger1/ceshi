using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools
{
	// Token: 0x02000110 RID: 272
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class CommandResponseAttribute : Attribute, GInterface0
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x0000485C File Offset: 0x00002A5C
		public CommandResponseAttribute(string methodName)
		{
			this.MethodName = methodName;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0000486C File Offset: 0x00002A6C
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x00004874 File Offset: 0x00002A74
		public string MethodName { get; private set; }

		// Token: 0x04000527 RID: 1319
		[CompilerGenerated]
		private string a;
	}
}
