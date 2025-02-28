using System;
using System.Windows.Forms;
using mshtml;

namespace Agiso
{
	// Token: 0x020000D5 RID: 213
	public static class ExecuteAndReturnParser
	{
		// Token: 0x06000667 RID: 1639 RVA: 0x00047004 File Offset: 0x00045204
		public static string ParseExecuteAndReturn(WebBrowser webBrowser1, string source, string cookieStr)
		{
			string text = "";
			if (source.StartsWith("javascript:"))
			{
				text = source.Substring(11);
			}
			else
			{
				string keyString = Security.GetKeyString(cookieStr);
				if (!string.IsNullOrEmpty(source))
				{
					if (!string.IsNullOrEmpty(keyString))
					{
						text = Security.Decrypt(source, keyString);
					}
					else
					{
						text = source;
					}
				}
			}
			string text2;
			if (string.IsNullOrEmpty(text))
			{
				text2 = "";
			}
			else
			{
				IHTMLDocument2 ihtmldocument = (IHTMLDocument2)webBrowser1.Document.DomDocument;
				IHTMLWindow2 parentWindow = ihtmldocument.parentWindow;
				parentWindow.execScript(text, "JScript");
				text2 = "解析完成";
			}
			return text2;
		}
	}
}
