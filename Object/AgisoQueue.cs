using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Agiso.Object;
using Agiso.Utils;

namespace AliwwClient.Object
{
	// Token: 0x020000A0 RID: 160
	[Serializable]
	public class AgisoQueue : ConcurrentQueue<AliwwMessageInfo>
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00003853 File Offset: 0x00001A53
		private List<long> DistinctMsgIdList { get; } = new List<long>();

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000385B File Offset: 0x00001A5B
		private List<string> DistinctBuyerOpenUidCallList { get; } = new List<string>();

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00003863 File Offset: 0x00001A63
		private List<string> DistinctBuyerOpenUidSendList { get; } = new List<string>();

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000386B File Offset: 0x00001A6B
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x00003873 File Offset: 0x00001A73
		public DateTime LastEnqueueTime { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000387C File Offset: 0x00001A7C
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x00003884 File Offset: 0x00001A84
		public DateTime LastDequeueTime { get; set; }

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600046E RID: 1134 RVA: 0x0003D0F4 File Offset: 0x0003B2F4
		// (remove) Token: 0x0600046F RID: 1135 RVA: 0x0003D12C File Offset: 0x0003B32C
		public event AgisoQueueHandler OnAgisoQueueEnqueued;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000470 RID: 1136 RVA: 0x0003D164 File Offset: 0x0003B364
		// (remove) Token: 0x06000471 RID: 1137 RVA: 0x0003D19C File Offset: 0x0003B39C
		public event AgisoQueueHandler OnAgisoQueueDequeued;

		// Token: 0x06000473 RID: 1139 RVA: 0x0003D1D4 File Offset: 0x0003B3D4
		public new void Enqueue(AliwwMessageInfo ami)
		{
			if (ami.MsgId > 0L)
			{
				object obj = this._obj;
				lock (obj)
				{
					if (this.DistinctMsgIdList.Contains(ami.MsgId))
					{
						LogWriter.WriteLog(string.Format("消息队列已存在重复消息，{0}", ami.MsgId), 1);
						return;
					}
					this.DistinctMsgIdList.Add(ami.MsgId);
				}
			}
			if (ami.MessageTitle == "【转接前回复消息】")
			{
				if (ami.AliwwMessageSourceType == EnumAliwwMessageSource.FromCallUserOnly)
				{
					object obj2 = this._obj;
					lock (obj2)
					{
						if (this.DistinctBuyerOpenUidCallList.Contains(ami.BuyerOpenUid))
						{
							return;
						}
						this.DistinctBuyerOpenUidCallList.Add(ami.BuyerOpenUid);
					}
				}
				if (ami.AliwwMessageSourceType == EnumAliwwMessageSource.FromTransferMsg)
				{
					object obj3 = this._obj;
					lock (obj3)
					{
						if (this.DistinctBuyerOpenUidSendList.Contains(ami.BuyerOpenUid))
						{
							return;
						}
						this.DistinctBuyerOpenUidSendList.Add(ami.BuyerOpenUid);
					}
				}
			}
			base.Enqueue(ami);
			this.LastEnqueueTime = DateTime.Now;
			AgisoQueueHandler onAgisoQueueEnqueued = this.OnAgisoQueueEnqueued;
			if (onAgisoQueueEnqueued != null)
			{
				onAgisoQueueEnqueued(ami);
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0003D364 File Offset: 0x0003B564
		public new bool TryDequeue(out AliwwMessageInfo ami)
		{
			this.LastDequeueTime = DateTime.Now;
			bool flag4;
			if (base.TryDequeue(out ami))
			{
				if (ami.MsgId > 0L)
				{
					object obj = this._obj;
					lock (obj)
					{
						this.DistinctMsgIdList.Remove(ami.MsgId);
					}
				}
				if (ami.MessageTitle == "【转接前回复消息】")
				{
					if (ami.AliwwMessageSourceType == EnumAliwwMessageSource.FromCallUserOnly)
					{
						object obj2 = this._obj;
						lock (obj2)
						{
							this.DistinctBuyerOpenUidCallList.Remove(ami.BuyerOpenUid);
						}
					}
					if (ami.AliwwMessageSourceType == EnumAliwwMessageSource.FromTransferMsg)
					{
						object obj3 = this._obj;
						lock (obj3)
						{
							this.DistinctBuyerOpenUidSendList.Remove(ami.BuyerOpenUid);
						}
					}
				}
				AgisoQueueHandler onAgisoQueueDequeued = this.OnAgisoQueueDequeued;
				if (onAgisoQueueDequeued != null)
				{
					onAgisoQueueDequeued(ami);
				}
				flag4 = true;
			}
			else
			{
				flag4 = false;
			}
			return flag4;
		}

		// Token: 0x0400038E RID: 910
		private object _obj = new object();
	}
}
