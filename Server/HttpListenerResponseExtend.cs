using System;
using System.IO;
using System.Net;
using System.Text;

namespace AliwwClient.Server
{
	// Token: 0x02000094 RID: 148
	public static class HttpListenerResponseExtend
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x0003CADC File Offset: 0x0003ACDC
		public static void WriteLine(this HttpListenerResponse httpListenerResponse, AjaxResult result)
		{
			using (StreamWriter streamWriter = new StreamWriter(httpListenerResponse.OutputStream, Encoding.UTF8))
			{
				streamWriter.WriteLine(result.Encode());
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0003CB24 File Offset: 0x0003AD24
		public static void WriteLine(this HttpListenerResponse httpListenerResponse, string responseStr)
		{
			using (StreamWriter streamWriter = new StreamWriter(httpListenerResponse.OutputStream, Encoding.UTF8))
			{
				streamWriter.WriteLine(responseStr);
			}
		}
	}
}
