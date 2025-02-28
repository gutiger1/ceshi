using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006F3 RID: 1779
	public struct MIB_TCPROW_OWNER_PID
	{
		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x0600232E RID: 9006 RVA: 0x00059DF8 File Offset: 0x00057FF8
		public uint ProcessId
		{
			get
			{
				return this.owningPid;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x00059E10 File Offset: 0x00058010
		public ushort LocalPort
		{
			get
			{
				return BitConverter.ToUInt16(new byte[]
				{
					this.localPort[1],
					this.localPort[0]
				}, 0);
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002330 RID: 9008 RVA: 0x00059E44 File Offset: 0x00058044
		public ushort RemotePort
		{
			get
			{
				return BitConverter.ToUInt16(new byte[]
				{
					this.remotePort[1],
					this.remotePort[0]
				}, 0);
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x00059E78 File Offset: 0x00058078
		public MIB_TCP_STATE State
		{
			get
			{
				return (MIB_TCP_STATE)this.state;
			}
		}

		// Token: 0x04001D44 RID: 7492
		public uint state;

		// Token: 0x04001D45 RID: 7493
		public uint localAddr;

		// Token: 0x04001D46 RID: 7494
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] localPort;

		// Token: 0x04001D47 RID: 7495
		public uint remoteAddr;

		// Token: 0x04001D48 RID: 7496
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] remotePort;

		// Token: 0x04001D49 RID: 7497
		public uint owningPid;
	}
}
