using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Agiso.CustomControls
{
	// Token: 0x020000FE RID: 254
	public class TimerGrid : UserControl
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0005047C File Offset: 0x0004E67C
		private Graphics G
		{
			get
			{
				if (this.j == null)
				{
					this.j = base.CreateGraphics();
				}
				return this.j;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x000504A8 File Offset: 0x0004E6A8
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x000045D3 File Offset: 0x000027D3
		public int Interval
		{
			get
			{
				return this.k;
			}
			set
			{
				this.k = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x000504C0 File Offset: 0x0004E6C0
		public List<Rectangle> DayOfWeekTitleRectangles
		{
			get
			{
				if (this.l == null)
				{
					this.l = new List<Rectangle>();
					foreach (object obj in Enum.GetValues(typeof(DayOfWeek)))
					{
						DayOfWeek dayOfWeek = (DayOfWeek)obj;
						if (dayOfWeek == DayOfWeek.Sunday)
						{
							Rectangle rectangle = new Rectangle(this.a, this.b + 6 * this.d, this.e, this.d);
							this.l.Add(rectangle);
							this.G.FillRectangle(new SolidBrush(Color.White), rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2);
						}
						else
						{
							Rectangle rectangle2 = new Rectangle(this.a, this.b + (dayOfWeek - DayOfWeek.Monday) * this.d, this.e, this.d);
							this.l.Add(rectangle2);
							this.G.FillRectangle(new SolidBrush(Color.White), rectangle2.X + 1, rectangle2.Y + 1, rectangle2.Width - 2, rectangle2.Height - 2);
						}
					}
				}
				return this.l;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0005063C File Offset: 0x0004E83C
		public RectangleCollection Rectangles
		{
			get
			{
				if (this.m == null)
				{
					this.m = new RectangleCollection();
					foreach (object obj in Enum.GetValues(typeof(DayOfWeek)))
					{
						DayOfWeek dayOfWeek = (DayOfWeek)obj;
						List<RectangleInfo> list = new List<RectangleInfo>();
						for (int i = 0; i < this.Interval; i++)
						{
							if (dayOfWeek == DayOfWeek.Sunday)
							{
								Rectangle rectangle = new Rectangle(this.a + this.e + i * this.c, this.b + 6 * this.d, this.c, this.d);
								list.Add(new RectangleInfo(rectangle, this.Interval)
								{
									Index = i
								});
							}
							else
							{
								Rectangle rectangle2 = new Rectangle(this.a + this.e + i * this.c, this.b + (dayOfWeek - DayOfWeek.Monday) * this.d, this.c, this.d);
								list.Add(new RectangleInfo(rectangle2, this.Interval)
								{
									Index = i
								});
							}
						}
						this.m.Add(dayOfWeek, list);
					}
				}
				return this.m;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x000045DC File Offset: 0x000027DC
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x000045E4 File Offset: 0x000027E4
		public Action MouseUpAction { get; set; }

		// Token: 0x06000801 RID: 2049 RVA: 0x000507BC File Offset: 0x0004E9BC
		public TimerGrid()
		{
			this.a();
			base.MouseDown += this.c;
			base.MouseMove += this.b;
			base.MouseUp += this.a;
			base.Paint += this.b;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0005089C File Offset: 0x0004EA9C
		private void b(object sender, PaintEventArgs e)
		{
			new SolidBrush(this.ForeColor);
			for (int i = 0; i < this.DayOfWeekTitleRectangles.Count; i++)
			{
				Rectangle rectangle = this.DayOfWeekTitleRectangles[i];
				this.G.DrawRectangle(this.f, rectangle);
				StringFormat stringFormat = new StringFormat();
				stringFormat.LineAlignment = StringAlignment.Center;
				stringFormat.Alignment = StringAlignment.Center;
				string text = null;
				switch (i)
				{
				case 0:
					text = "星期日";
					break;
				case 1:
					text = "星期一";
					break;
				case 2:
					text = "星期二";
					break;
				case 3:
					text = "星期三";
					break;
				case 4:
					text = "星期四";
					break;
				case 5:
					text = "星期五";
					break;
				case 6:
					text = "星期六";
					break;
				}
				this.G.DrawString(text, new Font("微软雅黑", 8f), new SolidBrush(Color.Black), rectangle, stringFormat);
			}
			foreach (KeyValuePair<DayOfWeek, List<RectangleInfo>> keyValuePair in this.Rectangles)
			{
				StringFormat stringFormat2 = new StringFormat();
				stringFormat2.LineAlignment = StringAlignment.Center;
				stringFormat2.Alignment = StringAlignment.Center;
				foreach (RectangleInfo rectangleInfo in keyValuePair.Value)
				{
					rectangleInfo.InitFill(this.G);
					Rectangle rect = rectangleInfo.Rect;
					if (rectangleInfo.Index == 0)
					{
						this.f.Width = (float)rectangleInfo.TopBoderWidth;
						this.G.DrawLine(this.f, rect.X, rect.Y, rect.X + this.Interval * this.c, rect.Y);
						if (keyValuePair.Key == DayOfWeek.Sunday)
						{
							this.f.Width = (float)rectangleInfo.TopBoderWidth;
							this.G.DrawLine(this.f, rect.X, rect.Y + this.d, rect.X + this.Interval * this.c, rect.Y + this.d);
						}
					}
					if (keyValuePair.Key == DayOfWeek.Monday)
					{
						int hourFrom = rectangleInfo.HourFrom;
						int minuteFrom = rectangleInfo.MinuteFrom;
						if (minuteFrom == 0)
						{
							this.f.Width = (float)rectangleInfo.LeftBoderWidth;
							this.G.DrawString(hourFrom.ToString() ?? "", new Font("微软雅黑", 6f), new SolidBrush(Color.Black), new PointF((float)rect.X, (float)(rect.Y - 5)), stringFormat2);
							this.G.DrawLine(this.f, rect.X, rect.Y, rect.X, rect.Y + 7 * this.d);
						}
						if (minuteFrom != 0)
						{
							this.g.Width = (float)rectangleInfo.LeftBoderWidth;
							this.G.DrawLine(this.g, rect.X, rect.Y, rect.X, rect.Y + 7 * this.d);
						}
						int hourTo = rectangleInfo.HourTo;
						int minuteTo = rectangleInfo.MinuteTo;
						if (hourTo == 24 && minuteTo == 0)
						{
							this.f.Width = (float)rectangleInfo.RightBoderWidth;
							this.G.DrawString(hourTo.ToString() ?? "", new Font("微软雅黑", 6f), new SolidBrush(Color.Black), new PointF((float)(rect.X + rect.Width), (float)(rect.Y - 5)), stringFormat2);
							this.G.DrawLine(this.f, rect.X + this.c, rect.Y, rect.X + this.c, rect.Y + 7 * this.d);
						}
					}
				}
			}
			this.Rectangles.Draw(this.G);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00050D34 File Offset: 0x0004EF34
		private void c(object sender, MouseEventArgs e)
		{
			this.h = true;
			this.i = new l();
			this.i.e(new Point(e.X, e.Y));
			this.i.e(Guid.NewGuid());
			this.i.e(new Rectangle(e.X, e.Y, 0, 0));
			base.Capture = true;
			Cursor.Clip = base.RectangleToScreen(new Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height));
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00050DD4 File Offset: 0x0004EFD4
		private void b(object sender, MouseEventArgs e)
		{
			if (this.h)
			{
				this.a(e.Location);
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00050DF8 File Offset: 0x0004EFF8
		private void a(object sender, MouseEventArgs e)
		{
			this.h = false;
			base.Capture = false;
			Cursor.Clip = Rectangle.Empty;
			this.b();
			this.Rectangles.SelectRectangles(this.G, this.i.g(), this.i.f());
			this.i = null;
			if (this.MouseUpAction != null)
			{
				this.MouseUpAction();
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00050E68 File Offset: 0x0004F068
		private void a(object sender, PaintEventArgs e)
		{
			new SolidBrush(this.ForeColor);
			for (int i = 0; i < this.DayOfWeekTitleRectangles.Count; i++)
			{
				Rectangle rectangle = this.DayOfWeekTitleRectangles[i];
				this.G.DrawRectangle(this.f, rectangle);
				StringFormat stringFormat = new StringFormat();
				stringFormat.LineAlignment = StringAlignment.Center;
				stringFormat.Alignment = StringAlignment.Center;
				string text = null;
				switch (i)
				{
				case 0:
					text = "星期日";
					break;
				case 1:
					text = "星期一";
					break;
				case 2:
					text = "星期二";
					break;
				case 3:
					text = "星期三";
					break;
				case 4:
					text = "星期四";
					break;
				case 5:
					text = "星期五";
					break;
				case 6:
					text = "星期六";
					break;
				}
				this.G.DrawString(text, new Font("微软雅黑", 8f), new SolidBrush(Color.Black), rectangle, stringFormat);
			}
			foreach (KeyValuePair<DayOfWeek, List<RectangleInfo>> keyValuePair in this.Rectangles)
			{
				StringFormat stringFormat2 = new StringFormat();
				stringFormat2.LineAlignment = StringAlignment.Center;
				stringFormat2.Alignment = StringAlignment.Center;
				foreach (RectangleInfo rectangleInfo in keyValuePair.Value)
				{
					rectangleInfo.InitFill(this.G);
					Rectangle rect = rectangleInfo.Rect;
					if (rectangleInfo.Index == 0)
					{
						this.f.Width = (float)rectangleInfo.TopBoderWidth;
						this.G.DrawLine(this.f, rect.X, rect.Y, rect.X + this.Interval * this.c, rect.Y);
						if (keyValuePair.Key == DayOfWeek.Sunday)
						{
							this.f.Width = (float)rectangleInfo.TopBoderWidth;
							this.G.DrawLine(this.f, rect.X, rect.Y + this.d, rect.X + this.Interval * this.c, rect.Y + this.d);
						}
					}
					if (keyValuePair.Key == DayOfWeek.Monday)
					{
						int hourFrom = rectangleInfo.HourFrom;
						int minuteFrom = rectangleInfo.MinuteFrom;
						if (minuteFrom == 0)
						{
							this.f.Width = (float)rectangleInfo.LeftBoderWidth;
							this.G.DrawString(hourFrom.ToString() ?? "", new Font("微软雅黑", 6f), new SolidBrush(Color.Black), new PointF((float)rect.X, (float)(rect.Y - 5)), stringFormat2);
							this.G.DrawLine(this.f, rect.X, rect.Y, rect.X, rect.Y + 7 * this.d);
						}
						if (minuteFrom != 0)
						{
							this.g.Width = (float)rectangleInfo.LeftBoderWidth;
							this.G.DrawLine(this.g, rect.X, rect.Y, rect.X, rect.Y + 7 * this.d);
						}
						int hourTo = rectangleInfo.HourTo;
						int minuteTo = rectangleInfo.MinuteTo;
						if (hourTo == 24 && minuteTo == 0)
						{
							this.f.Width = (float)rectangleInfo.RightBoderWidth;
							this.G.DrawString(hourTo.ToString() ?? "", new Font("微软雅黑", 6f), new SolidBrush(Color.Black), new PointF((float)(rect.X + rect.Width), (float)(rect.Y - 5)), stringFormat2);
							this.G.DrawLine(this.f, rect.X + this.c, rect.Y, rect.X + this.c, rect.Y + 7 * this.d);
						}
					}
				}
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x000045ED File Offset: 0x000027ED
		private void a(object sender, EventArgs e)
		{
			this.c = 720 / this.Interval;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x000512F0 File Offset: 0x0004F4F0
		private void a(Point A_0)
		{
			this.b();
			int x = this.i.h().X;
			int y = this.i.h().Y;
			if (A_0.X >= x && A_0.Y >= y)
			{
				this.i.e(new Rectangle(x, y, A_0.X - x, A_0.Y - y));
			}
			else if (A_0.X >= x && A_0.Y < y)
			{
				this.i.e(new Rectangle(x, A_0.Y, A_0.X - x, y - A_0.Y));
			}
			else if (A_0.X < x && A_0.Y >= y)
			{
				this.i.e(new Rectangle(A_0.X, y, x - A_0.X, A_0.Y - y));
			}
			else
			{
				this.i.e(new Rectangle(A_0.X, A_0.Y, x - A_0.X, y - A_0.Y));
			}
			this.b();
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00051434 File Offset: 0x0004F634
		private void b()
		{
			Rectangle rectangle = base.RectangleToScreen(this.i.g());
			ControlPaint.DrawReversibleFrame(rectangle, Color.Black, FrameStyle.Thick);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00051460 File Offset: 0x0004F660
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.o != null)
			{
				this.o.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00051490 File Offset: 0x0004F690
		private void a()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = SystemColors.Control;
			base.Name = "TimerGrid";
			base.Size = new Size(947, 331);
			base.Load += this.a;
			base.ResumeLayout(false);
		}

		// Token: 0x040004F2 RID: 1266
		private int a = 10;

		// Token: 0x040004F3 RID: 1267
		private int b = 20;

		// Token: 0x040004F4 RID: 1268
		private int c = 15;

		// Token: 0x040004F5 RID: 1269
		private int d = 30;

		// Token: 0x040004F6 RID: 1270
		private int e = 80;

		// Token: 0x040004F7 RID: 1271
		private Pen f = new Pen(Color.DodgerBlue);

		// Token: 0x040004F8 RID: 1272
		private Pen g = new Pen(Color.DodgerBlue)
		{
			DashStyle = DashStyle.Custom,
			DashPattern = new float[] { 8f, 5f }
		};

		// Token: 0x040004F9 RID: 1273
		private bool h;

		// Token: 0x040004FA RID: 1274
		private l i;

		// Token: 0x040004FB RID: 1275
		private Graphics j;

		// Token: 0x040004FC RID: 1276
		private int k = 48;

		// Token: 0x040004FD RID: 1277
		private List<Rectangle> l;

		// Token: 0x040004FE RID: 1278
		private RectangleCollection m;

		// Token: 0x040004FF RID: 1279
		[CompilerGenerated]
		private Action n;

		// Token: 0x04000500 RID: 1280
		private IContainer o = null;
	}
}
