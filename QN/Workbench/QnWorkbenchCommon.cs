using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Agiso.AliwwApi;
using Agiso.AliwwApi.Qn;
using Agiso.ChromeDevTools;
using Agiso.Exceptions;
using Agiso.Utils;
using Agiso.Windows;
using AliwwClient.Properties;

namespace AliwwClient.QN.Workbench
{
	// Token: 0x02000096 RID: 150
	public class QnWorkbenchCommon : IQnWorkbench
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0003CB68 File Offset: 0x0003AD68
		public string UserNick
		{
			get
			{
				return this._userNick;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000362D File Offset: 0x0000182D
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x00003635 File Offset: 0x00001835
		public WinChromeContainerQn WinQn { get; set; }

		// Token: 0x06000411 RID: 1041 RVA: 0x0003CB80 File Offset: 0x0003AD80
		public string GetLastError()
		{
			string lastError = this._lastError;
			this._lastError = null;
			return lastError;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000363E File Offset: 0x0000183E
		public QnWorkbenchCommon(string userNick)
		{
			this._userNick = userNick;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0003CBA0 File Offset: 0x0003ADA0
		public bool CheckBefore()
		{
			if (this.WinQn != null && this.WinQn.HWnd != IntPtr.Zero)
			{
				WindowInfo windowInfo = new WindowInfo(this.WinQn.HWnd);
				if (windowInfo.Info.WindowName == null || !windowInfo.Info.WindowName.Equals(this.UserNick + " - 接待中心"))
				{
					this.WinQn = null;
				}
			}
			if (this.WinQn == null || this.WinQn.HWnd == IntPtr.Zero)
			{
				AliwwTalkWindowQn aliwwTalkWindowQn = AliwwTalkWindowQn.Get(this.UserNick);
				if (aliwwTalkWindowQn != null)
				{
					this.WinQn = aliwwTalkWindowQn;
				}
			}
			bool flag;
			if (this.WinQn == null || this.WinQn.HWnd == IntPtr.Zero)
			{
				this._lastError = string.Format("查找不找 {0} 的千牛聊天窗口", this.UserNick);
				flag = false;
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0003CCA0 File Offset: 0x0003AEA0
		public virtual bool Start()
		{
			return this.CheckBefore() && this.ImplantedJs();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0003CCC4 File Offset: 0x0003AEC4
		public bool IsImplantedJsWork()
		{
			ExecuteJsResultInfo executeJsResultInfo;
			return this.CheckBefore() && this.a("window.__agi_lastScanTime__ = new Date(); window.__agi_AIL__", out executeJsResultInfo) && (executeJsResultInfo != null && executeJsResultInfo.result != null && executeJsResultInfo.result.result != null) && Util.ToBoolean(executeJsResultInfo.result.result.value);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0003CD30 File Offset: 0x0003AF30
		public virtual bool ImplantedJs()
		{
			ExecuteJsResultInfo executeJsResultInfo;
			bool flag;
			if (!this.a(Resources.MyQN + Resources.QnAutoReply, out executeJsResultInfo))
			{
				flag = false;
			}
			else if (executeJsResultInfo == null || executeJsResultInfo.result == null || executeJsResultInfo.result.wasThrown || !Util.ToBoolean(executeJsResultInfo.result.result.value))
			{
				this._lastError = this.UserNick + ": Implanted js failure";
				flag = false;
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0003CDB0 File Offset: 0x0003AFB0
		public List<RecvMsgResponse> GetMsg()
		{
			ExecuteJsResultInfo executeJsResultInfo;
			List<RecvMsgResponse> list;
			if (!this.a("var agisoReceivedMsgJson = JSON.stringify(window.__agi_tempArr__); window.__agi_tempArr__ = [];agisoReceivedMsgJson;", out executeJsResultInfo))
			{
				list = null;
			}
			else if (executeJsResultInfo == null)
			{
				list = null;
			}
			else
			{
				string value = executeJsResultInfo.result.result.value;
				if (string.IsNullOrEmpty(value))
				{
					this._lastError = this.UserNick + ": value is null while GetRecvMsg!";
					list = null;
				}
				else
				{
					List<RecvMsgResponse> list2 = new List<RecvMsgResponse>();
					try
					{
						list2 = JSON.Decode<List<RecvMsgResponse>>(value);
					}
					catch (Exception ex)
					{
						LogWriter.WriteLog("value: " + value + "\r\n" + ex.ToString(), 1);
					}
					list = list2;
				}
			}
			return list;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0003CE60 File Offset: 0x0003B060
		private bool a(string A_0, out ExecuteJsResultInfo A_1)
		{
			bool flag;
			try
			{
				if (this.WinQn == null || this.WinQn.HWnd == IntPtr.Zero)
				{
					A_1 = null;
					flag = false;
				}
				else
				{
					RemoteSessionsResponse remoteSessionsResponse;
					A_1 = this.WinQn.ExecuteJsByRecent(A_0, out remoteSessionsResponse);
					if (remoteSessionsResponse == null)
					{
						this._lastError = this.UserNick + ": Not Find DebugUrl";
						this.WinQn = null;
						flag = false;
					}
					else if (A_1 == null)
					{
						this._lastError = this.UserNick + ": 没找到聊天插件";
						this.WinQn = null;
						flag = false;
					}
					else
					{
						flag = true;
					}
				}
			}
			catch (GetChormeJsonException ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
				this._lastError = this.UserNick + ": 出错了，查看下千牛是否掉线";
				A_1 = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000369 RID: 873
		protected string _userNick;

		// Token: 0x0400036A RID: 874
		protected string _lastError = null;

		// Token: 0x0400036B RID: 875
		[CompilerGenerated]
		private WinChromeContainerQn a;
	}
}
