using System;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x0200001B RID: 27
	public static class JTokenExtensions
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00011374 File Offset: 0x0000F574
		public static string GetSafeString(this JToken jtoken)
		{
			string text;
			if (jtoken == null)
			{
				text = null;
			}
			else
			{
				text = jtoken.ToString();
			}
			return text;
		}
	}
}
