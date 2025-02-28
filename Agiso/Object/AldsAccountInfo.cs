using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Agiso.WwService.Sdk.Domain;
using Agiso.WwWebSocket.Model.Enums;

namespace Agiso.Object
{
	// Token: 0x02000680 RID: 1664
	public class AldsAccountInfo : INotifyPropertyChanged
	{
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06001F6A RID: 8042 RVA: 0x000528B8 File Offset: 0x00050AB8
		// (remove) Token: 0x06001F6B RID: 8043 RVA: 0x000528F0 File Offset: 0x00050AF0
		public event PropertyChangedEventHandler PropertyChanged
		{
			[CompilerGenerated]
			add
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.a;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler propertyChangedEventHandler3 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.a, propertyChangedEventHandler3, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.a;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler propertyChangedEventHandler3 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.a, propertyChangedEventHandler3, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x0000DFA1 File Offset: 0x0000C1A1
		// (set) Token: 0x06001F6D RID: 8045 RVA: 0x0000DFA9 File Offset: 0x0000C1A9
		public string UserNick { get; set; }

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x0000DFB2 File Offset: 0x0000C1B2
		// (set) Token: 0x06001F6F RID: 8047 RVA: 0x0000DFBA File Offset: 0x0000C1BA
		public string DisplayUserNick { get; set; }

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x0000DFC3 File Offset: 0x0000C1C3
		// (set) Token: 0x06001F71 RID: 8049 RVA: 0x0000DFCB File Offset: 0x0000C1CB
		public string Password { get; set; }

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06001F72 RID: 8050 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		// (set) Token: 0x06001F73 RID: 8051 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		public long Idx { get; set; }

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x0000DFE5 File Offset: 0x0000C1E5
		// (set) Token: 0x06001F75 RID: 8053 RVA: 0x0000DFED File Offset: 0x0000C1ED
		public DateTime ModifyTime { get; set; }

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x0000DFF6 File Offset: 0x0000C1F6
		// (set) Token: 0x06001F77 RID: 8055 RVA: 0x0000DFFE File Offset: 0x0000C1FE
		public DateTime CreateTime { get; set; }

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x00052928 File Offset: 0x00050B28
		// (set) Token: 0x06001F79 RID: 8057 RVA: 0x00052940 File Offset: 0x00050B40
		public string WebSocketStatus
		{
			get
			{
				return this.h;
			}
			set
			{
				if (this.h != value)
				{
					this.h = value;
					this.a("WebSocketStatus");
				}
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x0000E007 File Offset: 0x0000C207
		// (set) Token: 0x06001F7B RID: 8059 RVA: 0x0000E00F File Offset: 0x0000C20F
		public long Option1 { get; set; }

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x00052970 File Offset: 0x00050B70
		// (set) Token: 0x06001F7D RID: 8061 RVA: 0x00052998 File Offset: 0x00050B98
		public bool AutoSendOnOff
		{
			get
			{
				return (this.Option1 & 1L) > 0L;
			}
			set
			{
				if (this.AutoSendOnOff != value)
				{
					this.Option1 ^= 1L;
					this.a("AutoSendOnOff");
				}
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x000529D8 File Offset: 0x00050BD8
		// (set) Token: 0x06001F7F RID: 8063 RVA: 0x00052A00 File Offset: 0x00050C00
		public bool AutoReplyOnOff
		{
			get
			{
				return (this.Option1 & 2L) > 0L;
			}
			set
			{
				if (this.AutoReplyOnOff != value)
				{
					this.Option1 ^= 2L;
					this.a("AutoReply");
				}
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x00052A40 File Offset: 0x00050C40
		// (set) Token: 0x06001F81 RID: 8065 RVA: 0x00052A68 File Offset: 0x00050C68
		public bool IsCustomerServiceNewVersion
		{
			get
			{
				return (this.Option1 & 16L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 16L;
				}
				else
				{
					this.Option1 &= -17L;
				}
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x0000E018 File Offset: 0x0000C218
		// (set) Token: 0x06001F83 RID: 8067 RVA: 0x0000E020 File Offset: 0x0000C220
		public string ManualNick { get; set; }

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x0000E029 File Offset: 0x0000C229
		// (set) Token: 0x06001F85 RID: 8069 RVA: 0x0000E031 File Offset: 0x0000C231
		public long? DefaultMouldId { get; set; }

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x0000E03A File Offset: 0x0000C23A
		// (set) Token: 0x06001F87 RID: 8071 RVA: 0x0000E042 File Offset: 0x0000C242
		public string TransferNickIfNotDuty { get; set; }

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x0000E04B File Offset: 0x0000C24B
		// (set) Token: 0x06001F89 RID: 8073 RVA: 0x0000E053 File Offset: 0x0000C253
		public string NotDutyNickReplyMsg { get; set; }

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x0000E05C File Offset: 0x0000C25C
		// (set) Token: 0x06001F8B RID: 8075 RVA: 0x0000E064 File Offset: 0x0000C264
		public SendIMType SendIM { get; set; }

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06001F8D RID: 8077 RVA: 0x0000E076 File Offset: 0x0000C276
		// (set) Token: 0x06001F8C RID: 8076 RVA: 0x0000E06D File Offset: 0x0000C26D
		public bool LongOpen { get; set; }

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06001F8E RID: 8078 RVA: 0x0000E07E File Offset: 0x0000C27E
		// (set) Token: 0x06001F8F RID: 8079 RVA: 0x0000E086 File Offset: 0x0000C286
		public bool DisableTransfer { get; set; }

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x0000E08F File Offset: 0x0000C28F
		// (set) Token: 0x06001F91 RID: 8081 RVA: 0x0000E097 File Offset: 0x0000C297
		public string VerifyResult { get; set; }

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06001F92 RID: 8082 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
		// (set) Token: 0x06001F93 RID: 8083 RVA: 0x0000E0A8 File Offset: 0x0000C2A8
		public bool IsValid { get; set; }

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x0000E0B1 File Offset: 0x0000C2B1
		// (set) Token: 0x06001F95 RID: 8085 RVA: 0x0000E0B9 File Offset: 0x0000C2B9
		public int VersionNo { get; set; }

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x00052AA8 File Offset: 0x00050CA8
		// (set) Token: 0x06001F97 RID: 8087 RVA: 0x00052ABC File Offset: 0x00050CBC
		public bool EnableAutoReply
		{
			get
			{
				return this.t;
			}
			set
			{
				if (this.t != value)
				{
					this.t = value;
					this.a("AutoReply");
				}
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x0000E0C2 File Offset: 0x0000C2C2
		// (set) Token: 0x06001F99 RID: 8089 RVA: 0x0000E0CA File Offset: 0x0000C2CA
		public string TiquShortUrl { get; set; }

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x0000E0D3 File Offset: 0x0000C2D3
		// (set) Token: 0x06001F9B RID: 8091 RVA: 0x0000E0DB File Offset: 0x0000C2DB
		public string QnAccountPwd { get; set; }

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06001F9C RID: 8092 RVA: 0x00052AEC File Offset: 0x00050CEC
		// (set) Token: 0x06001F9D RID: 8093 RVA: 0x00052B04 File Offset: 0x00050D04
		public string QnConnectionStatus
		{
			get
			{
				return this.w;
			}
			set
			{
				if (!string.Equals(this.w, value))
				{
					this.w = value;
					this.a("QnConnectionStatus");
				}
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06001F9E RID: 8094 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
		public bool AutoReply
		{
			get
			{
				return this.AutoReplyOnOff && this.EnableAutoReply;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06001F9F RID: 8095 RVA: 0x0000E0F7 File Offset: 0x0000C2F7
		// (set) Token: 0x06001FA0 RID: 8096 RVA: 0x0000E0FF File Offset: 0x0000C2FF
		public DateTime? DeadLine { get; set; }

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06001FA1 RID: 8097 RVA: 0x0000E108 File Offset: 0x0000C308
		// (set) Token: 0x06001FA2 RID: 8098 RVA: 0x0000E110 File Offset: 0x0000C310
		public string TransferMessage { get; set; }

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x00052B38 File Offset: 0x00050D38
		public string DeadLineStr
		{
			get
			{
				return (this.DeadLine != null) ? this.DeadLine.Value.ToString("yyyy-MM-dd") : "";
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06001FA4 RID: 8100 RVA: 0x0000E119 File Offset: 0x0000C319
		// (set) Token: 0x06001FA5 RID: 8101 RVA: 0x0000E121 File Offset: 0x0000C321
		public bool IsRemoveByServer { get; set; } = false;

		// Token: 0x06001FA6 RID: 8102 RVA: 0x00052B78 File Offset: 0x00050D78
		private void a([CallerMemberName] string A_0 = "")
		{
			AldsAccountInfo.a a = new AldsAccountInfo.a();
			a.a = this;
			a.b = A_0;
			if (this.a != null)
			{
				if (global::k.a().InvokeRequired)
				{
					global::k.a().Invoke(new Action(a.c));
				}
				else
				{
					this.a(this, new PropertyChangedEventArgs(a.b));
				}
			}
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x00052BE0 File Offset: 0x00050DE0
		public void Map(AliwwClientAccount acAccount)
		{
			if (acAccount != null)
			{
				this.UserNick = acAccount.UserNick;
				this.EnableAutoReply = acAccount.EnableAutoReply;
				this.IsValid = acAccount.IsValid;
				this.TiquShortUrl = acAccount.TiquShortUrl;
				this.VersionNo = acAccount.VersionNo;
				this.DeadLine = acAccount.DeadLine;
				this.VerifyResult = acAccount.VerifyResult;
			}
		}

		// Token: 0x040011C6 RID: 4550
		[CompilerGenerated]
		private PropertyChangedEventHandler a;

		// Token: 0x040011C7 RID: 4551
		[CompilerGenerated]
		private string b;

		// Token: 0x040011C8 RID: 4552
		[CompilerGenerated]
		private string c;

		// Token: 0x040011C9 RID: 4553
		[CompilerGenerated]
		private string d;

		// Token: 0x040011CA RID: 4554
		[CompilerGenerated]
		private long e;

		// Token: 0x040011CB RID: 4555
		[CompilerGenerated]
		private DateTime f;

		// Token: 0x040011CC RID: 4556
		[CompilerGenerated]
		private DateTime g;

		// Token: 0x040011CD RID: 4557
		private string h = "-";

		// Token: 0x040011CE RID: 4558
		[CompilerGenerated]
		private long i;

		// Token: 0x040011CF RID: 4559
		[CompilerGenerated]
		private string j;

		// Token: 0x040011D0 RID: 4560
		[CompilerGenerated]
		private long? k;

		// Token: 0x040011D1 RID: 4561
		[CompilerGenerated]
		private string l;

		// Token: 0x040011D2 RID: 4562
		[CompilerGenerated]
		private string m;

		// Token: 0x040011D3 RID: 4563
		[CompilerGenerated]
		private SendIMType n;

		// Token: 0x040011D4 RID: 4564
		[CompilerGenerated]
		private bool o;

		// Token: 0x040011D5 RID: 4565
		[CompilerGenerated]
		private bool p;

		// Token: 0x040011D6 RID: 4566
		[CompilerGenerated]
		private string q;

		// Token: 0x040011D7 RID: 4567
		[CompilerGenerated]
		private bool r;

		// Token: 0x040011D8 RID: 4568
		[CompilerGenerated]
		private int s;

		// Token: 0x040011D9 RID: 4569
		private bool t;

		// Token: 0x040011DA RID: 4570
		[CompilerGenerated]
		private string u;

		// Token: 0x040011DB RID: 4571
		[CompilerGenerated]
		private string v;

		// Token: 0x040011DC RID: 4572
		private string w = "-";

		// Token: 0x040011DD RID: 4573
		[CompilerGenerated]
		private DateTime? x;

		// Token: 0x040011DE RID: 4574
		[CompilerGenerated]
		private string y;

		// Token: 0x040011DF RID: 4575
		[CompilerGenerated]
		private bool z;

		// Token: 0x02000681 RID: 1665
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x06001FAA RID: 8106 RVA: 0x0000E152 File Offset: 0x0000C352
			internal void c()
			{
				this.a.a(this.a, new PropertyChangedEventArgs(this.b));
			}

			// Token: 0x040011E0 RID: 4576
			public AldsAccountInfo a;

			// Token: 0x040011E1 RID: 4577
			public string b;
		}
	}
}
