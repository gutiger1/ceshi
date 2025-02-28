using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006C6 RID: 1734
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct DEVMODE
	{
		// Token: 0x04001BC8 RID: 7112
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string dmDeviceName;

		// Token: 0x04001BC9 RID: 7113
		public int dmSpecVersion;

		// Token: 0x04001BCA RID: 7114
		public int dmDriverVersion;

		// Token: 0x04001BCB RID: 7115
		public int dmSize;

		// Token: 0x04001BCC RID: 7116
		public int dmDriverExtra;

		// Token: 0x04001BCD RID: 7117
		public int dmFields;

		// Token: 0x04001BCE RID: 7118
		public short dmOrientation;

		// Token: 0x04001BCF RID: 7119
		public short dmPaperSize;

		// Token: 0x04001BD0 RID: 7120
		public short dmPaperLength;

		// Token: 0x04001BD1 RID: 7121
		public short dmPaperWidth;

		// Token: 0x04001BD2 RID: 7122
		public short dmScale;

		// Token: 0x04001BD3 RID: 7123
		public short dmCopies;

		// Token: 0x04001BD4 RID: 7124
		public short dmDefaultSource;

		// Token: 0x04001BD5 RID: 7125
		public short dmPrintQuality;

		// Token: 0x04001BD6 RID: 7126
		public Point dmPosition;

		// Token: 0x04001BD7 RID: 7127
		public int dmDisplayOrientation;

		// Token: 0x04001BD8 RID: 7128
		public int dmDisplayFixedOutput;

		// Token: 0x04001BD9 RID: 7129
		public short dmColor;

		// Token: 0x04001BDA RID: 7130
		public short dmDuplex;

		// Token: 0x04001BDB RID: 7131
		public short dmYResolution;

		// Token: 0x04001BDC RID: 7132
		public short short_0;

		// Token: 0x04001BDD RID: 7133
		public short dmCollate;

		// Token: 0x04001BDE RID: 7134
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string dmFormName;

		// Token: 0x04001BDF RID: 7135
		public int dmLogPixels;

		// Token: 0x04001BE0 RID: 7136
		public int dmBitsPerPel;

		// Token: 0x04001BE1 RID: 7137
		public int dmPelsWidth;

		// Token: 0x04001BE2 RID: 7138
		public int dmPelsHeight;

		// Token: 0x04001BE3 RID: 7139
		public int dmDisplayFlags;

		// Token: 0x04001BE4 RID: 7140
		public int dmNup;

		// Token: 0x04001BE5 RID: 7141
		public int dmDisplayFrequency;

		// Token: 0x04001BE6 RID: 7142
		public int int_0;

		// Token: 0x04001BE7 RID: 7143
		public int int_1;

		// Token: 0x04001BE8 RID: 7144
		public int dmMediaType;

		// Token: 0x04001BE9 RID: 7145
		public int dmDitherType;

		// Token: 0x04001BEA RID: 7146
		public int dmReserved1;

		// Token: 0x04001BEB RID: 7147
		public int dmReserved2;

		// Token: 0x04001BEC RID: 7148
		public int dmPanningWidth;

		// Token: 0x04001BED RID: 7149
		public int dmPanningHeight;
	}
}
