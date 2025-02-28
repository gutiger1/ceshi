using System;
using System.Collections.Generic;
using System.IO;
using Agiso.WwService.Sdk.Response;

namespace AliwwClient.Manager
{
	// Token: 0x020000AF RID: 175
	public interface IEmotionManager
	{
		// Token: 0x0600051E RID: 1310
		List<EmotionItem> Get();

		// Token: 0x0600051F RID: 1311
		bool Append(List<Emotion> emotions, out bool hasChange);

		// Token: 0x06000520 RID: 1312
		void Delete(string quickSymbol, out bool hasChange);

		// Token: 0x06000521 RID: 1313
		bool IsFileExists();

		// Token: 0x06000522 RID: 1314
		bool HasUserId();

		// Token: 0x06000523 RID: 1315
		FileInfo GetFileInfo();
	}
}
