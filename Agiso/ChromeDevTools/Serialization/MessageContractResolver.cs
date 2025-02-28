using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Agiso.ChromeDevTools.Serialization
{
	// Token: 0x0200011B RID: 283
	public class MessageContractResolver : DefaultContractResolver
	{
		// Token: 0x0600089D RID: 2205 RVA: 0x00052808 File Offset: 0x00050A08
		protected override string ResolvePropertyName(string propertyName)
		{
			string text;
			if (string.IsNullOrEmpty(propertyName))
			{
				text = propertyName;
			}
			else if (1 == propertyName.Length)
			{
				text = char.ToLowerInvariant(propertyName[0]).ToString();
			}
			else
			{
				text = char.ToLowerInvariant(propertyName[0]).ToString() + propertyName.Substring(1);
			}
			return text;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00052868 File Offset: 0x00050A68
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty jsonProperty = base.CreateProperty(member, memberSerialization);
			if (!jsonProperty.Writable)
			{
				PropertyInfo propertyInfo = member as PropertyInfo;
				if (propertyInfo != null)
				{
					bool flag = propertyInfo.GetSetMethod(true) != null;
					jsonProperty.Writable = flag;
				}
			}
			return jsonProperty;
		}
	}
}
