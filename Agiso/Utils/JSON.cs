using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Agiso.Utils
{
	// Token: 0x020000E6 RID: 230
	public class JSON
	{
		// Token: 0x060006BF RID: 1727 RVA: 0x00048EF0 File Offset: 0x000470F0
		public static string Encode(object o)
		{
			string text;
			if (o == null || o.ToString() == "null")
			{
				text = null;
			}
			else if (o != null && (o.GetType() == typeof(string) || o.GetType() == typeof(string)))
			{
				text = o.ToString();
			}
			else
			{
				text = JsonConvert.SerializeObject(o, new JsonConverter[]
				{
					new IsoDateTimeConverter
					{
						DateTimeFormat = JSON.DateTimeFormat
					}
				});
			}
			return text;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00048F80 File Offset: 0x00047180
		public static object Decode(string json)
		{
			object obj;
			if (string.IsNullOrEmpty(json))
			{
				obj = "";
			}
			else
			{
				object obj2 = JsonConvert.DeserializeObject(json);
				if (obj2.GetType() == typeof(string) || obj2.GetType() == typeof(string))
				{
					obj2 = JsonConvert.DeserializeObject(obj2.ToString());
				}
				object obj3 = JSON.a(obj2);
				obj = obj3;
			}
			return obj;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00048FEC File Offset: 0x000471EC
		public static T Decode<T>(string json)
		{
			T t;
			if (json == null)
			{
				t = default(T);
			}
			else
			{
				t = JsonConvert.DeserializeObject<T>(json);
			}
			return t;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00049014 File Offset: 0x00047214
		private static object a(JValue A_0)
		{
			object obj;
			if (A_0 == null)
			{
				obj = null;
			}
			else
			{
				if (A_0.GetType() == typeof(string))
				{
					string text = A_0.ToString();
					if (text.Length == 19 && text[10] == 'T' && text[4] == '-' && text[13] == ':')
					{
						A_0 = Convert.ToDateTime(A_0);
					}
				}
				else if (A_0 is JObject)
				{
					JObject jobject = A_0 as JObject;
					Hashtable hashtable = new Hashtable();
					foreach (KeyValuePair<string, JToken> keyValuePair in jobject)
					{
						hashtable[keyValuePair.Key] = JSON.a(keyValuePair.Value);
					}
					A_0 = hashtable;
				}
				else if (A_0 is IList)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.AddRange(A_0 as IList);
					int count = arrayList.Count;
					for (int i = 0; i < count; i++)
					{
						arrayList[i] = JSON.a(arrayList[i]);
					}
					A_0 = arrayList;
				}
				else if (typeof(JValue) == A_0.GetType())
				{
					JValue jvalue = (JValue)A_0;
					A_0 = JSON.a(jvalue.Value);
				}
				obj = A_0;
			}
			return obj;
		}

		// Token: 0x040004D0 RID: 1232
		public static string DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
	}
}
