using System;

namespace Agiso.ChromeDevTools
{
	// Token: 0x0200010D RID: 269
	public interface ICommandResponse
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600086E RID: 2158
		long Id { get; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600086F RID: 2159
		string Method { get; }
	}
}
