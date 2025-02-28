using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Agiso.DbManager;
using Agiso.Object;
using Agiso.Utils;
using Agiso.WwWebSocket.Model;
using AliwwClient.QN.Workbench;
using AliwwClient.WebSocketServer;

namespace AliwwClient.Cache
{
	// Token: 0x020000A7 RID: 167
	public class UserCache
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00003A95 File Offset: 0x00001C95
		public string UserNick
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0003D95C File Offset: 0x0003BB5C
		public UserCache(string userNick)
		{
			this.a = userNick;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00003A9D File Offset: 0x00001C9D
		public ConcurrentQueue<RecvMsgResponse> RecvMsgQueue { get; } = new ConcurrentQueue<RecvMsgResponse>();

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00003AA5 File Offset: 0x00001CA5
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x00003AAD File Offset: 0x00001CAD
		public int LastSendProcessId { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00003AB6 File Offset: 0x00001CB6
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x00003ABE File Offset: 0x00001CBE
		public MsgSendSoftware LastSendSoftware { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00003AC7 File Offset: 0x00001CC7
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x00003ACF File Offset: 0x00001CCF
		public DateTime? LastSendMsgTime { get; set; } = new DateTime?(DateTime.Now);

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00003AD8 File Offset: 0x00001CD8
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x00003AE0 File Offset: 0x00001CE0
		public List<CustomerServiceWorksheet> CurrentWorksheet { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00003AE9 File Offset: 0x00001CE9
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00003AF1 File Offset: 0x00001CF1
		public int ManualNickIdx { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00003AFA File Offset: 0x00001CFA
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x00003B02 File Offset: 0x00001D02
		public string DutyManualNick { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00003B0B File Offset: 0x00001D0B
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x00003B13 File Offset: 0x00001D13
		public bool QueryDsrComplete { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00003B1C File Offset: 0x00001D1C
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x00003B24 File Offset: 0x00001D24
		public string QueryDsrErrMsg { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00003B2D File Offset: 0x00001D2D
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x00003B35 File Offset: 0x00001D35
		public bool AccountIsBanned { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00003B3E File Offset: 0x00001D3E
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x00003B46 File Offset: 0x00001D46
		public DateTime? LastTeamForbidTime { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00003B4F File Offset: 0x00001D4F
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x00003B57 File Offset: 0x00001D57
		public DateTime? LastAccountIsBannedTime { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00003B60 File Offset: 0x00001D60
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x00003B68 File Offset: 0x00001D68
		public DateTime? LastIllegalKeywordsTime { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00003B71 File Offset: 0x00001D71
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x00003B79 File Offset: 0x00001D79
		public int SendMsgTimeOutCount { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00003B82 File Offset: 0x00001D82
		public ConcurrentDictionary<string, DateTime> DictRecentGetNewMsgLastRecvTime { get; } = new ConcurrentDictionary<string, DateTime>();

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00003B8A File Offset: 0x00001D8A
		public List<string> RecentRegMsgIsNotNullList { get; } = new List<string>();

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00003B92 File Offset: 0x00001D92
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x00003B9A File Offset: 0x00001D9A
		public DateTime? LastRecentBeatTime { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00003BA3 File Offset: 0x00001DA3
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x00003BAB File Offset: 0x00001DAB
		public DateTime? LastAldsBeatTime { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00003BB4 File Offset: 0x00001DB4
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x00003BBC File Offset: 0x00001DBC
		public DateTime? LastSendMsgSuccDateTime { get; set; } = new DateTime?(DateTime.MinValue);

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0003D9B8 File Offset: 0x0003BBB8
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00003BC5 File Offset: 0x00001DC5
		public RecentBehavior RecentSession
		{
			get
			{
				return this.u;
			}
			set
			{
				Interlocked.Exchange<RecentBehavior>(ref this.u, value);
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public void ClearRecentSession()
		{
			Interlocked.Exchange<RecentBehavior>(ref this.u, null);
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00003BE3 File Offset: 0x00001DE3
		public bool IsRecentSessionNull
		{
			get
			{
				return this.u == null;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0003D9D0 File Offset: 0x0003BBD0
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x00003BEE File Offset: 0x00001DEE
		public AldsBehavior AldsSession
		{
			get
			{
				return this.v;
			}
			set
			{
				Interlocked.Exchange<AldsBehavior>(ref this.v, value);
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00003BFD File Offset: 0x00001DFD
		public void ClearAldsSession()
		{
			Interlocked.Exchange<AldsBehavior>(ref this.v, null);
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00003C0C File Offset: 0x00001E0C
		public bool IsAldsSessionNull
		{
			get
			{
				return this.v == null;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00003C17 File Offset: 0x00001E17
		public bool IsSessionNull
		{
			get
			{
				return this.IsRecentSessionNull && this.IsAldsSessionNull;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0003D9E8 File Offset: 0x0003BBE8
		public BehaviorBase Session
		{
			get
			{
				BehaviorBase behaviorBase;
				if (!this.IsAldsSessionNull)
				{
					behaviorBase = this.AldsSession;
				}
				else if (!this.IsRecentSessionNull)
				{
					behaviorBase = this.RecentSession;
				}
				else
				{
					behaviorBase = null;
				}
				return behaviorBase;
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0003DA20 File Offset: 0x0003BC20
		public BehaviorBase GetSession(string openUid)
		{
			BehaviorBase behaviorBase;
			if (Util.GetMasterNick(this.UserNick) != "agiso" && !string.IsNullOrEmpty(openUid))
			{
				if (Util.IsNum(openUid))
				{
					if (!this.IsRecentSessionNull)
					{
						return this.RecentSession;
					}
				}
				else if (!this.IsAldsSessionNull)
				{
					return this.AldsSession;
				}
				behaviorBase = null;
			}
			else
			{
				behaviorBase = this.Session;
			}
			return behaviorBase;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0003DA90 File Offset: 0x0003BC90
		public Dictionary<string, DateTime> DictBuyerNickTransferTime
		{
			get
			{
				if (this.w == null)
				{
					this.w = new Dictionary<string, DateTime>();
				}
				return this.w;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00003C2A File Offset: 0x00001E2A
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x00003C32 File Offset: 0x00001E32
		public string UserNickAs { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0003DABC File Offset: 0x0003BCBC
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x0003DB0C File Offset: 0x0003BD0C
		public DateTime AldsPlugNoticeTime
		{
			get
			{
				if (this.y == null)
				{
					AgentAldsPlugNotice agentAldsPlugNotice = AgentAldsPlugNoticeManager.Get(this.a);
					this.y = new DateTime?((agentAldsPlugNotice != null) ? agentAldsPlugNotice.NoticeTime : DateTime.MinValue);
				}
				return this.y.Value;
			}
			set
			{
				if (this.y == DateTime.MinValue)
				{
					AgentAldsPlugNoticeManager.Insert(this.a, value);
				}
				else
				{
					AgentAldsPlugNoticeManager.Update(this.a, value);
				}
				this.y = new DateTime?(value);
			}
		}

		// Token: 0x040003B9 RID: 953
		private string a;

		// Token: 0x040003BA RID: 954
		[CompilerGenerated]
		private readonly ConcurrentQueue<RecvMsgResponse> b;

		// Token: 0x040003BB RID: 955
		[CompilerGenerated]
		private int c;

		// Token: 0x040003BC RID: 956
		[CompilerGenerated]
		private MsgSendSoftware d;

		// Token: 0x040003BD RID: 957
		[CompilerGenerated]
		private DateTime? e;

		// Token: 0x040003BE RID: 958
		[CompilerGenerated]
		private List<CustomerServiceWorksheet> f;

		// Token: 0x040003BF RID: 959
		[CompilerGenerated]
		private int g;

		// Token: 0x040003C0 RID: 960
		[CompilerGenerated]
		private string h;

		// Token: 0x040003C1 RID: 961
		[CompilerGenerated]
		private bool i;

		// Token: 0x040003C2 RID: 962
		[CompilerGenerated]
		private string j;

		// Token: 0x040003C3 RID: 963
		[CompilerGenerated]
		private bool k;

		// Token: 0x040003C4 RID: 964
		[CompilerGenerated]
		private DateTime? l;

		// Token: 0x040003C5 RID: 965
		[CompilerGenerated]
		private DateTime? m;

		// Token: 0x040003C6 RID: 966
		[CompilerGenerated]
		private DateTime? n;

		// Token: 0x040003C7 RID: 967
		[CompilerGenerated]
		private int o;

		// Token: 0x040003C8 RID: 968
		[CompilerGenerated]
		private readonly ConcurrentDictionary<string, DateTime> p;

		// Token: 0x040003C9 RID: 969
		[CompilerGenerated]
		private readonly List<string> q;

		// Token: 0x040003CA RID: 970
		[CompilerGenerated]
		private DateTime? r;

		// Token: 0x040003CB RID: 971
		[CompilerGenerated]
		private DateTime? s;

		// Token: 0x040003CC RID: 972
		[CompilerGenerated]
		private DateTime? t;

		// Token: 0x040003CD RID: 973
		private RecentBehavior u;

		// Token: 0x040003CE RID: 974
		private AldsBehavior v;

		// Token: 0x040003CF RID: 975
		private Dictionary<string, DateTime> w;

		// Token: 0x040003D0 RID: 976
		[CompilerGenerated]
		private string x;

		// Token: 0x040003D1 RID: 977
		private DateTime? y;
	}
}
