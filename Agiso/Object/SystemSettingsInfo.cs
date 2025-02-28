using System;
using System.Runtime.CompilerServices;

namespace Agiso.Object
{
	// Token: 0x02000690 RID: 1680
	public class SystemSettingsInfo
	{
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600200A RID: 8202 RVA: 0x0000E432 File Offset: 0x0000C632
		// (set) Token: 0x0600200B RID: 8203 RVA: 0x0000E43A File Offset: 0x0000C63A
		public Action DisableCloseWindowWhenAutoReplyWhileTrueFunc { get; set; }

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x0600200C RID: 8204 RVA: 0x0000E443 File Offset: 0x0000C643
		// (set) Token: 0x0600200D RID: 8205 RVA: 0x0000E44B File Offset: 0x0000C64B
		public long IdNo { get; set; }

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x0600200E RID: 8206 RVA: 0x0000E454 File Offset: 0x0000C654
		// (set) Token: 0x0600200F RID: 8207 RVA: 0x0000E45C File Offset: 0x0000C65C
		public string SendMessageHotKey { get; set; }

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x0000E465 File Offset: 0x0000C665
		// (set) Token: 0x06002011 RID: 8209 RVA: 0x0000E46D File Offset: 0x0000C66D
		public long Option1 { get; set; }

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002012 RID: 8210 RVA: 0x0000E476 File Offset: 0x0000C676
		// (set) Token: 0x06002013 RID: 8211 RVA: 0x0000E47E File Offset: 0x0000C67E
		public int SendInterval { get; set; }

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002014 RID: 8212 RVA: 0x0000E487 File Offset: 0x0000C687
		// (set) Token: 0x06002015 RID: 8213 RVA: 0x0000E48F File Offset: 0x0000C68F
		public int CloseWindowWhenSended { get; set; }

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06002016 RID: 8214 RVA: 0x0000E498 File Offset: 0x0000C698
		// (set) Token: 0x06002017 RID: 8215 RVA: 0x0000E4A0 File Offset: 0x0000C6A0
		public int CloseWindowBeforeSend { get; set; }

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06002018 RID: 8216 RVA: 0x0000E4A9 File Offset: 0x0000C6A9
		// (set) Token: 0x06002019 RID: 8217 RVA: 0x0000E4B1 File Offset: 0x0000C6B1
		public int AliwwMessageLengthMax { get; set; }

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x0000E4BA File Offset: 0x0000C6BA
		// (set) Token: 0x0600201B RID: 8219 RVA: 0x0000E4C2 File Offset: 0x0000C6C2
		public string ManualNick { get; set; }

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x0000E4CB File Offset: 0x0000C6CB
		// (set) Token: 0x0600201D RID: 8221 RVA: 0x0000E4D3 File Offset: 0x0000C6D3
		public string MsgSendFirstTime { get; set; }

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x0600201E RID: 8222 RVA: 0x0000E4DC File Offset: 0x0000C6DC
		// (set) Token: 0x0600201F RID: 8223 RVA: 0x0000E4E4 File Offset: 0x0000C6E4
		public DateTime ModifyTime { get; set; }

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x0000E4ED File Offset: 0x0000C6ED
		// (set) Token: 0x06002021 RID: 8225 RVA: 0x0000E4F5 File Offset: 0x0000C6F5
		public DateTime CreateTime { get; set; }

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x0000E4FE File Offset: 0x0000C6FE
		// (set) Token: 0x06002023 RID: 8227 RVA: 0x0000E506 File Offset: 0x0000C706
		public int CurrentVersion { get; set; }

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x00052F38 File Offset: 0x00051138
		// (set) Token: 0x06002025 RID: 8229 RVA: 0x00052F60 File Offset: 0x00051160
		public bool AllowCheckTargetNick
		{
			get
			{
				return (this.Option1 & 1L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 1L;
				}
				else
				{
					this.Option1 &= 2147483646L;
				}
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06002026 RID: 8230 RVA: 0x00052FA0 File Offset: 0x000511A0
		// (set) Token: 0x06002027 RID: 8231 RVA: 0x00052FC8 File Offset: 0x000511C8
		public bool DisableCloseWindowWhenAutoReply
		{
			get
			{
				return (this.Option1 & 2L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 2L;
					if (this.DisableCloseWindowWhenAutoReplyWhileTrueFunc != null)
					{
						this.DisableCloseWindowWhenAutoReplyWhileTrueFunc();
					}
				}
				else
				{
					this.Option1 &= 2147483645L;
				}
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x0005301C File Offset: 0x0005121C
		// (set) Token: 0x06002029 RID: 8233 RVA: 0x00053044 File Offset: 0x00051244
		public bool AllowWriteLogAboutThreadAbort
		{
			get
			{
				return (this.Option1 & 32L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 32L;
				}
				else
				{
					this.Option1 &= 2147483615L;
				}
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x00053084 File Offset: 0x00051284
		// (set) Token: 0x0600202B RID: 8235 RVA: 0x000530AC File Offset: 0x000512AC
		[Obsolete("2022-08-03")]
		public bool AllowUseQnIndex
		{
			get
			{
				return (this.Option1 & 64L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 64L;
				}
				else
				{
					this.Option1 &= 2147483583L;
				}
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x000530EC File Offset: 0x000512EC
		// (set) Token: 0x0600202D RID: 8237 RVA: 0x00053114 File Offset: 0x00051314
		public bool AllowSendExpression
		{
			get
			{
				return (this.Option1 & 4L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 4L;
				}
				else
				{
					this.Option1 &= 2147483643L;
				}
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x0600202E RID: 8238 RVA: 0x00053154 File Offset: 0x00051354
		// (set) Token: 0x0600202F RID: 8239 RVA: 0x0005317C File Offset: 0x0005137C
		public bool AllowSetMinimizeCustomerBenchWindowWhileSendSuccess
		{
			get
			{
				return (this.Option1 & 8L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 8L;
				}
				else
				{
					this.Option1 &= 2147483639L;
				}
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06002030 RID: 8240 RVA: 0x000531BC File Offset: 0x000513BC
		// (set) Token: 0x06002031 RID: 8241 RVA: 0x000531E4 File Offset: 0x000513E4
		public bool DisableAutoFitEnterOrCtrlEnter
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
					this.Option1 &= 2147483631L;
				}
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x00053224 File Offset: 0x00051424
		// (set) Token: 0x06002033 RID: 8243 RVA: 0x0005324C File Offset: 0x0005144C
		public bool AllowKillAliApp
		{
			get
			{
				return (this.Option1 & 128L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 128L;
				}
				else
				{
					this.Option1 &= 2147483519L;
				}
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x0005328C File Offset: 0x0005148C
		public bool SendByEnter
		{
			get
			{
				return !"^{ENTER}".Equals(this.SendMessageHotKey);
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06002035 RID: 8245 RVA: 0x000532B0 File Offset: 0x000514B0
		// (set) Token: 0x06002036 RID: 8246 RVA: 0x0000E50F File Offset: 0x0000C70F
		public bool CloseWindowWhenSendedBool
		{
			get
			{
				return this.CloseWindowWhenSended > 0;
			}
			set
			{
				this.CloseWindowWhenSended = (value ? 1 : 0);
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x000532C8 File Offset: 0x000514C8
		// (set) Token: 0x06002038 RID: 8248 RVA: 0x0000E51E File Offset: 0x0000C71E
		public bool CloseWindowBeforeSendBool
		{
			get
			{
				return false;
			}
			set
			{
				this.CloseWindowBeforeSend = (value ? 1 : 0);
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06002039 RID: 8249 RVA: 0x000532D8 File Offset: 0x000514D8
		// (set) Token: 0x0600203A RID: 8250 RVA: 0x00053300 File Offset: 0x00051500
		public bool AutoReplyBySellerNick
		{
			get
			{
				return (this.Option1 & 256L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 256L;
				}
				else
				{
					this.Option1 &= -257L;
				}
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x00053340 File Offset: 0x00051540
		// (set) Token: 0x0600203C RID: 8252 RVA: 0x00053368 File Offset: 0x00051568
		public bool AllowGetMsgByWebSocket
		{
			get
			{
				return (this.Option1 & 512L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 512L;
				}
				else
				{
					this.Option1 &= -513L;
				}
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x000533A8 File Offset: 0x000515A8
		// (set) Token: 0x0600203E RID: 8254 RVA: 0x000533D0 File Offset: 0x000515D0
		public bool AllowSendMsgByWebSocket
		{
			get
			{
				return (this.Option1 & 1024L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 1024L;
				}
				else
				{
					this.Option1 &= -1025L;
				}
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x00053410 File Offset: 0x00051610
		// (set) Token: 0x06002040 RID: 8256 RVA: 0x00053438 File Offset: 0x00051638
		public bool CloseWindowScanAutoReplay
		{
			get
			{
				return (this.Option1 & 2048L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 2048L;
				}
				else
				{
					this.Option1 &= -2049L;
				}
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x00053478 File Offset: 0x00051678
		// (set) Token: 0x06002042 RID: 8258 RVA: 0x000534A0 File Offset: 0x000516A0
		public bool OnlyFirstReply
		{
			get
			{
				return (this.Option1 & 4096L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 4096L;
				}
				else
				{
					this.Option1 &= -4097L;
				}
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x000534E0 File Offset: 0x000516E0
		// (set) Token: 0x06002044 RID: 8260 RVA: 0x00053508 File Offset: 0x00051708
		public bool FirstReplyContinueNoMatch
		{
			get
			{
				return (this.Option1 & 65536L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 65536L;
				}
				else
				{
					this.Option1 &= -65537L;
				}
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x00053548 File Offset: 0x00051748
		// (set) Token: 0x06002046 RID: 8262 RVA: 0x00053570 File Offset: 0x00051770
		public bool FirstReplyContinueMatch
		{
			get
			{
				return (this.Option1 & 131072L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 131072L;
				}
				else
				{
					this.Option1 &= -131073L;
				}
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x000535B0 File Offset: 0x000517B0
		// (set) Token: 0x06002048 RID: 8264 RVA: 0x000535D8 File Offset: 0x000517D8
		public bool AutoSendBeforeMsg
		{
			get
			{
				return (this.Option1 & 8192L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 8192L;
				}
				else
				{
					this.Option1 &= -8193L;
				}
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002049 RID: 8265 RVA: 0x00053618 File Offset: 0x00051818
		// (set) Token: 0x0600204A RID: 8266 RVA: 0x00053640 File Offset: 0x00051840
		public bool CloseSendMsgCheck
		{
			get
			{
				return (this.Option1 & 16384L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 16384L;
				}
				else
				{
					this.Option1 &= -16385L;
				}
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x00053680 File Offset: 0x00051880
		// (set) Token: 0x0600204C RID: 8268 RVA: 0x000536A8 File Offset: 0x000518A8
		public bool EnableSendMsgCheck
		{
			get
			{
				return (this.Option1 & 32768L) > 0L;
			}
			set
			{
				if (value)
				{
					this.Option1 |= 32768L;
				}
				else
				{
					this.Option1 &= -32769L;
				}
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x0600204D RID: 8269 RVA: 0x0000E52D File Offset: 0x0000C72D
		// (set) Token: 0x0600204E RID: 8270 RVA: 0x0000E535 File Offset: 0x0000C735
		public int InsertMsgSuccInterval { get; set; }

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x0000E53E File Offset: 0x0000C73E
		// (set) Token: 0x06002050 RID: 8272 RVA: 0x0000E546 File Offset: 0x0000C746
		public int RecvMsgReplyInterval { get; set; }

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x0000E54F File Offset: 0x0000C74F
		// (set) Token: 0x06002052 RID: 8274 RVA: 0x0000E557 File Offset: 0x0000C757
		public int NoMatchReplyInterval { get; set; }

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x0000E560 File Offset: 0x0000C760
		// (set) Token: 0x06002054 RID: 8276 RVA: 0x0000E568 File Offset: 0x0000C768
		public int SameQueryReplyInterval { get; set; }

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x0000E571 File Offset: 0x0000C771
		// (set) Token: 0x06002056 RID: 8278 RVA: 0x0000E579 File Offset: 0x0000C779
		[Obsolete("弃用该字段，请用TransferInterval")]
		public int PeriodOfTime { get; set; }

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x0000E582 File Offset: 0x0000C782
		// (set) Token: 0x06002058 RID: 8280 RVA: 0x0000E58A File Offset: 0x0000C78A
		public int TransferInterval { get; set; }

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x0000E593 File Offset: 0x0000C793
		// (set) Token: 0x0600205A RID: 8282 RVA: 0x0000E59B File Offset: 0x0000C79B
		public int FirstReplyInterval { get; set; }

		// Token: 0x04001236 RID: 4662
		[CompilerGenerated]
		private Action a;

		// Token: 0x04001237 RID: 4663
		[CompilerGenerated]
		private long b;

		// Token: 0x04001238 RID: 4664
		[CompilerGenerated]
		private string c;

		// Token: 0x04001239 RID: 4665
		[CompilerGenerated]
		private long d;

		// Token: 0x0400123A RID: 4666
		[CompilerGenerated]
		private int e;

		// Token: 0x0400123B RID: 4667
		[CompilerGenerated]
		private int f;

		// Token: 0x0400123C RID: 4668
		[CompilerGenerated]
		private int g;

		// Token: 0x0400123D RID: 4669
		[CompilerGenerated]
		private int h;

		// Token: 0x0400123E RID: 4670
		[CompilerGenerated]
		private string i;

		// Token: 0x0400123F RID: 4671
		[CompilerGenerated]
		private string j;

		// Token: 0x04001240 RID: 4672
		[CompilerGenerated]
		private DateTime k;

		// Token: 0x04001241 RID: 4673
		[CompilerGenerated]
		private DateTime l;

		// Token: 0x04001242 RID: 4674
		[CompilerGenerated]
		private int m;

		// Token: 0x04001243 RID: 4675
		[CompilerGenerated]
		private int n;

		// Token: 0x04001244 RID: 4676
		[CompilerGenerated]
		private int o;

		// Token: 0x04001245 RID: 4677
		[CompilerGenerated]
		private int p;

		// Token: 0x04001246 RID: 4678
		[CompilerGenerated]
		private int q;

		// Token: 0x04001247 RID: 4679
		[CompilerGenerated]
		private int r;

		// Token: 0x04001248 RID: 4680
		[CompilerGenerated]
		private int s;

		// Token: 0x04001249 RID: 4681
		[CompilerGenerated]
		private int t;
	}
}
