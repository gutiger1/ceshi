using System;
using Agiso.ChromeDevTools.Protocol.Chrome.DOM;
using Agiso.ChromeDevTools.Serialization;
using Agiso.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Agiso.ChromeDevTools
{
	// Token: 0x02000101 RID: 257
	public static class ChromeExtensions
	{
		// Token: 0x06000823 RID: 2083 RVA: 0x00051CA8 File Offset: 0x0004FEA8
		public static string GetHtml(this ChromeSession chrome)
		{
			CommandResponse<GetDocumentCommandResponse> document = chrome.GetDocument();
			long commandId = chrome.GetCommandId();
			if (document == null)
			{
				throw new Exception("getDocRsp is null! " + JSON.Encode(document));
			}
			if (document.Result == null)
			{
				throw new Exception("getDocRsp.Result is null! " + JSON.Encode(document));
			}
			if (document.Result.Root == null)
			{
				throw new Exception("getDocRsp.Result.Root is null! " + JSON.Encode(document));
			}
			string text = string.Concat(new string[]
			{
				"{\"params\": {\"nodeId\": ",
				document.Result.Root.NodeId.ToString(),
				"}, \"id\": ",
				commandId.ToString(),
				", \"method\": \"DOM.getOuterHTML\"}"
			});
			string text2 = chrome.SendCommand(text);
			CommandResponse<GetOuterHTMLCommandResponse> commandResponse = JsonConvert.DeserializeObject<CommandResponse<GetOuterHTMLCommandResponse>>(text2, new JsonSerializerSettings
			{
				ContractResolver = new MessageContractResolver()
			});
			if (commandResponse == null)
			{
				throw new Exception("rspObj is null! " + JSON.Encode(commandResponse) + text2);
			}
			if (commandResponse.Result == null)
			{
				throw new Exception("rspObj.Result is null! " + JSON.Encode(commandResponse) + text2);
			}
			if (commandResponse.Result.OuterHTML == null)
			{
				throw new Exception("rspObj.Result.OuterHTML is null! " + JSON.Encode(commandResponse) + text2);
			}
			return commandResponse.Result.OuterHTML;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00051E1C File Offset: 0x0005001C
		public static CommandResponse<GetDocumentCommandResponse> GetDocument(this ChromeSession chrome)
		{
			string text = chrome.SendCommand("{\"id\": " + chrome.GetCommandId().ToString() + ", \"method\": \"DOM.getDocument\", \"params\": {}}");
			CommandResponse<GetDocumentCommandResponse> commandResponse;
			if (text == null)
			{
				commandResponse = null;
			}
			else
			{
				commandResponse = ChromeExtensions.a<CommandResponse<GetDocumentCommandResponse>>(text);
			}
			return commandResponse;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00051E64 File Offset: 0x00050064
		private static a a<a>(string A_0)
		{
			JObject jobject = JObject.Parse(A_0);
			if (jobject["error"] != null)
			{
				ErrorResponse errorResponse = jobject.ToObject<ErrorResponse>();
				throw new Exception(errorResponse.ToString());
			}
			return JsonConvert.DeserializeObject<a>(A_0, new JsonSerializerSettings
			{
				ContractResolver = new MessageContractResolver()
			});
		}
	}
}
