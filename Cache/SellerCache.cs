using System;
using System.Collections.Generic;
using Agiso;
using Agiso.Object;
using Agiso.Utils;
using AliwwClient.Object;

namespace AliwwClient.Cache
{
	// Token: 0x020000A6 RID: 166
	[Serializable]
	public class SellerCache
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x0003D718 File Offset: 0x0003B918
		public SellerCache(string sellerNick)
		{
			this._sellerNick = sellerNick;
			this.AliwwMsgQueue = new AgisoQueue();
			this.AliwwMsgQueue.OnAgisoQueueEnqueued += this.b;
			this.AliwwMsgQueue.OnAgisoQueueDequeued += this.a;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0003D76C File Offset: 0x0003B96C
		private void b(object A_0)
		{
			AliwwMessageInfo aliwwMessageInfo = A_0 as AliwwMessageInfo;
			if (aliwwMessageInfo.MsgId == 0L)
			{
				LogWriter.WriteLog("异常消息：" + JSON.Encode(aliwwMessageInfo), 1);
			}
			else
			{
				object obj = SellerCache._obj;
				lock (obj)
				{
					AppConfig.AliwwMsgIdListOrderByEnqueueTime.Add(aliwwMessageInfo.MsgId);
					AppConfig.AliwwMsgDictOrderByEnqueueTime[aliwwMessageInfo.MsgId] = aliwwMessageInfo.SellerNick;
				}
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0003D800 File Offset: 0x0003BA00
		private void a(object A_0)
		{
			AliwwMessageInfo aliwwMessageInfo = A_0 as AliwwMessageInfo;
			object obj = SellerCache._obj;
			lock (obj)
			{
				AppConfig.AliwwMsgIdListOrderByEnqueueTime.Remove(aliwwMessageInfo.MsgId);
				AppConfig.AliwwMsgDictOrderByEnqueueTime.Remove(aliwwMessageInfo.MsgId);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0003D864 File Offset: 0x0003BA64
		public string SellerNick
		{
			get
			{
				return this._sellerNick;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00003A5E File Offset: 0x00001C5E
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x00003A66 File Offset: 0x00001C66
		public string LastSuccessSendUserNick { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00003A6F File Offset: 0x00001C6F
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x00003A77 File Offset: 0x00001C77
		public bool IsDoingSendTick { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00003A80 File Offset: 0x00001C80
		public AgisoQueue AliwwMsgQueue { get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public Dictionary<string, string> DictBuyerTransferCallToManualNickCache
		{
			get
			{
				if (this._dictBuyerTransferCallToManualNickCache == null)
				{
					this._dictBuyerTransferCallToManualNickCache = new Dictionary<string, string>();
				}
				return this._dictBuyerTransferCallToManualNickCache;
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0003D8A8 File Offset: 0x0003BAA8
		public void FailAll(bool reportToServer = true)
		{
			List<long> list = new List<long>();
			AliwwMessageInfo aliwwMessageInfo;
			while (this.AliwwMsgQueue.TryDequeue(out aliwwMessageInfo))
			{
				if (reportToServer)
				{
					list.Add(aliwwMessageInfo.MsgId);
				}
			}
			if (!Util.IsEmptyList<long>(list))
			{
				AppConfig.FailAliwwMessage(list, this.SellerNick, 0);
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0003D8F4 File Offset: 0x0003BAF4
		public void RoolbackMsgs(bool reportToServer = true)
		{
			List<long> list;
			this.RoolbackMsgs(out list, reportToServer);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0003D90C File Offset: 0x0003BB0C
		public void RoolbackMsgs(out List<long> msgIds, bool reportToServer = true)
		{
			msgIds = new List<long>();
			AliwwMessageInfo aliwwMessageInfo;
			while (this.AliwwMsgQueue.TryDequeue(out aliwwMessageInfo))
			{
				if (reportToServer)
				{
					msgIds.Add(aliwwMessageInfo.MsgId);
				}
			}
			if (!Util.IsEmptyList<long>(msgIds))
			{
				AppConfig.RoolbackMsgs(msgIds, this.SellerNick);
			}
		}

		// Token: 0x040003B3 RID: 947
		private string _sellerNick;

		// Token: 0x040003B4 RID: 948
		private static object _obj = new object();

		// Token: 0x040003B8 RID: 952
		private Dictionary<string, string> _dictBuyerTransferCallToManualNickCache;
	}
}
