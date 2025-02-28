using System;

namespace Agiso.ChromeDevTools
{
	// Token: 0x02000108 RID: 264
	public interface ICommand
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000860 RID: 2144
		long Id { get; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000861 RID: 2145
		string Method { get; }
	}
}
