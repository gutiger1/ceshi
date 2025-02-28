using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Agiso.CustomControls
{
	// Token: 0x020000FF RID: 255
	public class RectangleInfo
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x00051504 File Offset: 0x0004F704
		public Rectangle Rect
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00004601 File Offset: 0x00002801
		public RectangleInfo(Rectangle rectangle, int interval)
		{
			this.a = rectangle;
			this.b = interval;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00004618 File Offset: 0x00002818
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x00004620 File Offset: 0x00002820
		public bool IsSelect { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00004629 File Offset: 0x00002829
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x00004631 File Offset: 0x00002831
		public Guid TempGuid { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0000463A File Offset: 0x0000283A
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x00004642 File Offset: 0x00002842
		public int Index { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0005151C File Offset: 0x0004F71C
		public int HourFrom
		{
			get
			{
				return this.Index * 24 / this.b;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0005153C File Offset: 0x0004F73C
		public int MinuteFrom
		{
			get
			{
				return this.Index * (1440 / this.b) % 60;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00051564 File Offset: 0x0004F764
		public int HourTo
		{
			get
			{
				return (this.Index + 1) * 24 / this.b;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00051588 File Offset: 0x0004F788
		public int MinuteTo
		{
			get
			{
				return (this.Index + 1) * (1440 / this.b) % 60;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x000515B0 File Offset: 0x0004F7B0
		public int TopBoderWidth
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x000515B0 File Offset: 0x0004F7B0
		public int BottomBoderWidth
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x000515C0 File Offset: 0x0004F7C0
		public int LeftBoderWidth
		{
			get
			{
				int num;
				if (this.HourFrom != 0 && this.HourFrom % 4 == 0 && this.MinuteFrom == 0)
				{
					num = 3;
				}
				else
				{
					num = 1;
				}
				return num;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x000515F4 File Offset: 0x0004F7F4
		public int RightBoderWidth
		{
			get
			{
				int num;
				if (this.HourTo != 24 && this.HourTo % 4 == 0 && this.MinuteTo == 0)
				{
					num = 3;
				}
				else
				{
					num = 1;
				}
				return num;
			}
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0005162C File Offset: 0x0004F82C
		public void FillRectangle(Graphics g)
		{
			g.FillRectangle(new SolidBrush(Color.RoyalBlue), new Rectangle(this.a.X + this.LeftBoderWidth, this.a.Y + this.TopBoderWidth, this.a.Width - this.LeftBoderWidth - this.RightBoderWidth, this.a.Height - this.TopBoderWidth - this.BottomBoderWidth));
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000516A4 File Offset: 0x0004F8A4
		public void InitFill(Graphics g)
		{
			g.FillRectangle(new SolidBrush(Color.White), new Rectangle(this.a.X + this.LeftBoderWidth, this.a.Y + this.TopBoderWidth, this.a.Width - this.LeftBoderWidth - this.RightBoderWidth, this.a.Height - this.TopBoderWidth - this.BottomBoderWidth));
		}

		// Token: 0x04000501 RID: 1281
		private Rectangle a;

		// Token: 0x04000502 RID: 1282
		private int b;

		// Token: 0x04000503 RID: 1283
		[CompilerGenerated]
		private bool c;

		// Token: 0x04000504 RID: 1284
		[CompilerGenerated]
		private Guid d;

		// Token: 0x04000505 RID: 1285
		[CompilerGenerated]
		private int e;
	}
}
