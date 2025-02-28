using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools
{
	// Token: 0x0200010B RID: 267
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class CommandAttribute : Attribute, GInterface0
	{
		// Token: 0x0600086A RID: 2154 RVA: 0x000047EF File Offset: 0x000029EF
		public CommandAttribute(string methodName)
		{
			this.MethodName = methodName;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x000047FF File Offset: 0x000029FF
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x00004807 File Offset: 0x00002A07
		public string MethodName { get; private set; }

		// Token: 0x04000523 RID: 1315
		[CompilerGenerated]
		private string a;
	}
}
