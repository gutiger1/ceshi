using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Agiso.CustomControls
{
	// Token: 0x02000100 RID: 256
	public class RectangleCollection : Dictionary<DayOfWeek, List<RectangleInfo>>
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0005171C File Offset: 0x0004F91C
		public Rectangle OutRectangle
		{
			get
			{
				if (this.a == Rectangle.Empty)
				{
					if (this == null || base.Count <= 0)
					{
						return Rectangle.Empty;
					}
					if (!base.ContainsKey(DayOfWeek.Monday) || !base.ContainsKey(DayOfWeek.Sunday))
					{
						return Rectangle.Empty;
					}
					List<RectangleInfo> list = base[DayOfWeek.Monday];
					if (list == null || list.Count <= 0)
					{
						return Rectangle.Empty;
					}
					List<RectangleInfo> list2 = base[DayOfWeek.Sunday];
					if (list2 == null || list2.Count <= 0)
					{
						return Rectangle.Empty;
					}
					Rectangle rect = list[0].Rect;
					Rectangle rect2 = list2[list2.Count - 1].Rect;
					this.a = new Rectangle(rect.X, rect.Y, rect2.X + rect2.Width - rect.X, rect2.Y + rect2.Height - rect.Y);
				}
				return this.a;
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00051844 File Offset: 0x0004FA44
		public void SelectRectangles(Graphics g, Rectangle mouseRectangle, Guid mouseGuid)
		{
			if (this != null && base.Count > 0 && !(mouseRectangle == Rectangle.Empty))
			{
				if (mouseRectangle.Width == 0 && mouseRectangle.Height == 0)
				{
					this.SelectContainRectagent(g, new Point(mouseRectangle.X, mouseRectangle.Y), mouseGuid);
				}
				else
				{
					Rectangle rectangle = Rectangle.Intersect(this.OutRectangle, mouseRectangle);
					if (rectangle.Width > 0 && rectangle.Height > 0)
					{
						foreach (object obj in Enum.GetValues(typeof(DayOfWeek)))
						{
							DayOfWeek dayOfWeek = (DayOfWeek)obj;
							if (base.Keys.Contains(dayOfWeek))
							{
								foreach (RectangleInfo rectangleInfo in base[dayOfWeek])
								{
									Rectangle rect = rectangleInfo.Rect;
									new a<int, int, int, int>(rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);
									Rectangle rectangle2 = Rectangle.Intersect(rectangleInfo.Rect, mouseRectangle);
									if (rectangle2 != Rectangle.Empty && rectangle2.Width > 0 && rectangle2.Height > 0)
									{
										if (rectangleInfo.TempGuid != mouseGuid)
										{
											if (rectangleInfo.IsSelect)
											{
												rectangleInfo.IsSelect = false;
												rectangleInfo.InitFill(g);
											}
											else
											{
												rectangleInfo.IsSelect = true;
												rectangleInfo.FillRectangle(g);
											}
											rectangleInfo.TempGuid = mouseGuid;
										}
									}
									else if (rectangleInfo.IsSelect && rectangleInfo.TempGuid == mouseGuid)
									{
										rectangleInfo.IsSelect = false;
										rectangleInfo.TempGuid = Guid.Empty;
										rectangleInfo.InitFill(g);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00051A98 File Offset: 0x0004FC98
		public void SelectContainRectagent(Graphics g, Point mousePoint, Guid mouseGuid)
		{
			if (this != null && base.Count > 0 && this.OutRectangle.Contains(mousePoint))
			{
				foreach (object obj in Enum.GetValues(typeof(DayOfWeek)))
				{
					DayOfWeek dayOfWeek = (DayOfWeek)obj;
					if (base.Keys.Contains(dayOfWeek))
					{
						foreach (RectangleInfo rectangleInfo in base[dayOfWeek])
						{
							if (rectangleInfo.Rect.Contains(mousePoint))
							{
								if (rectangleInfo.TempGuid != mouseGuid)
								{
									if (rectangleInfo.IsSelect)
									{
										rectangleInfo.IsSelect = false;
										rectangleInfo.InitFill(g);
									}
									else
									{
										rectangleInfo.IsSelect = true;
										rectangleInfo.FillRectangle(g);
									}
									rectangleInfo.TempGuid = mouseGuid;
								}
								return;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00051BD8 File Offset: 0x0004FDD8
		public void Draw(Graphics g)
		{
			if (this != null && base.Count > 0)
			{
				foreach (KeyValuePair<DayOfWeek, List<RectangleInfo>> keyValuePair in this)
				{
					if (keyValuePair.Value != null && keyValuePair.Value.Count > 0)
					{
						foreach (RectangleInfo rectangleInfo in keyValuePair.Value)
						{
							if (rectangleInfo.IsSelect)
							{
								rectangleInfo.FillRectangle(g);
							}
						}
					}
				}
			}
		}

		// Token: 0x04000506 RID: 1286
		private Rectangle a = Rectangle.Empty;
	}
}
