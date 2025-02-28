using System;
using System.Windows.Forms;
using Agiso.Windows;

namespace Agiso.AliwwApi
{
	// Token: 0x02000723 RID: 1827
	public class WinTxtInput : WindowInfo
	{
		// Token: 0x06002448 RID: 9288 RVA: 0x0006278C File Offset: 0x0006098C
		private void a(string A_0, bool A_1 = false)
		{
			if (A_1)
			{
				ClipboardProxy clipboardProxy = new ClipboardProxy();
				clipboardProxy.Hold();
				base.SetText("");
				AppConfig.WriteLog("SetText Clipboard.SetData Start", LogType.LogForTraceHold, 1);
				ClipboardProxy.SetText(A_0, TextDataFormat.Text, 10);
				AppConfig.WriteLog("SetText Clipboard.SetData End", LogType.LogForTraceHold, 1);
				WindowsAPI.SendMessage(base.HWnd, 770, 0, 0);
				clipboardProxy.Recover();
				Application.DoEvents();
			}
			else
			{
				base.SetText(A_0);
			}
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x00062800 File Offset: 0x00060A00
		public bool SetText(string msg, bool setByPaste, WinTxtInput.CheckSetTextMatchType cType = WinTxtInput.CheckSetTextMatchType.CheckByFirstChar, int retryTimes = 10)
		{
			int i = 0;
			while (i < retryTimes)
			{
				this.a(msg, setByPaste);
				Application.DoEvents();
				string text = base.GetText().Trim();
				bool flag;
				if (cType != WinTxtInput.CheckSetTextMatchType.DisableCheck)
				{
					if (!string.IsNullOrEmpty(msg) || !string.IsNullOrEmpty(text))
					{
						if (!string.IsNullOrEmpty(msg) && !string.IsNullOrEmpty(text))
						{
							if (cType == WinTxtInput.CheckSetTextMatchType.CheckByFirstChar)
							{
								if (text.StartsWith(msg.Substring(0, 1)))
								{
									return true;
								}
							}
							else
							{
								if (cType == WinTxtInput.CheckSetTextMatchType.CheckByFirstLineString)
								{
									throw new Exception("未定义按首行验证的方法");
								}
								if (cType == WinTxtInput.CheckSetTextMatchType.CheckByFullText && text.Equals(msg))
								{
									return true;
								}
							}
						}
						i++;
						continue;
					}
					flag = true;
				}
				else
				{
					flag = true;
				}
				return flag;
			}
			return false;
		}

		// Token: 0x02000724 RID: 1828
		public enum CheckSetTextMatchType
		{
			// Token: 0x04001E06 RID: 7686
			DisableCheck,
			// Token: 0x04001E07 RID: 7687
			CheckByFirstChar,
			// Token: 0x04001E08 RID: 7688
			CheckByFirstLineString,
			// Token: 0x04001E09 RID: 7689
			CheckByFullText
		}
	}
}
