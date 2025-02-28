using System;
using Agiso;

namespace AliwwClient.Manager
{
	// Token: 0x020000B0 RID: 176
	public static class EmotionManagerFactory
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x0003F960 File Offset: 0x0003DB60
		public static IEmotionManager Get(string userNick)
		{
			IEmotionManager emotionManager;
			if (!AppConfig.AllowAutoLogin)
			{
				emotionManager = null;
			}
			else if (AppConfig.AgentSettings.QnVersion == 1)
			{
				emotionManager = new EmotionXmlManager(userNick);
			}
			else
			{
				emotionManager = new EmotionJsonManager(userNick);
			}
			return emotionManager;
		}
	}
}
