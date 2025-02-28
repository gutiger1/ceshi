using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Agiso.CustomControls;
using Agiso.DbManager;
using Agiso.Object;
using Agiso.Utils;
using Agiso.WwWebSocket.Model;

namespace AliwwClient
{
	// Token: 0x02000029 RID: 41
	public partial class FormCustomerServiceWorksheetNew : BaseForm
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00018DE8 File Offset: 0x00016FE8
		public FormCustomerServiceWorksheetNew(FormAldsAccountEdit parentForm, AldsAccountInfo account, long mouldId, CustomerServiceWorksheet worksheet = null)
		{
			this.a();
			this.a = parentForm;
			this.c = worksheet;
			this.b = account;
			this.d = mouldId;
			TimerGrid timerGrid = this.m;
			timerGrid.MouseUpAction = (Action)Delegate.Combine(timerGrid.MouseUpAction, new Action(this.GetWorksheetInfo));
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00018E50 File Offset: 0x00017050
		public void GetWorksheetInfo()
		{
			this.e = new DetailWorksheetInfo();
			foreach (KeyValuePair<DayOfWeek, List<RectangleInfo>> keyValuePair in this.m.Rectangles)
			{
				List<int> list = new List<int>();
				foreach (RectangleInfo rectangleInfo in keyValuePair.Value)
				{
					if (rectangleInfo.IsSelect)
					{
						list.Add(rectangleInfo.Index);
					}
				}
				list.Sort();
				List<FormCustomerServiceWorksheetNew.Pair> list2 = new List<FormCustomerServiceWorksheetNew.Pair>();
				for (;;)
				{
					FormCustomerServiceWorksheetNew.Pair pair = this.a(ref list);
					if (pair == null)
					{
						break;
					}
					list2.Add(pair);
				}
				int interval = this.m.Interval;
				List<WorkTimeInfo> list3 = new List<WorkTimeInfo>();
				foreach (FormCustomerServiceWorksheetNew.Pair pair2 in list2)
				{
					list3.Add(new WorkTimeInfo
					{
						HourFrom = keyValuePair.Value[pair2.Min].HourFrom,
						MinuteFrom = keyValuePair.Value[pair2.Min].MinuteFrom,
						HourTo = keyValuePair.Value[pair2.Max].HourTo,
						MinuteTo = keyValuePair.Value[pair2.Max].MinuteTo
					});
				}
				this.e[keyValuePair.Key] = list3;
				this.a(this.g, this.a.GetWorkTimeDesc(this.e, DayOfWeek.Monday));
				this.a(this.h, this.a.GetWorkTimeDesc(this.e, DayOfWeek.Tuesday));
				this.a(this.i, this.a.GetWorkTimeDesc(this.e, DayOfWeek.Wednesday));
				this.a(this.l, this.a.GetWorkTimeDesc(this.e, DayOfWeek.Thursday));
				this.a(this.k, this.a.GetWorkTimeDesc(this.e, DayOfWeek.Friday));
				this.a(this.j, this.a.GetWorkTimeDesc(this.e, DayOfWeek.Saturday));
				this.a(this.n, this.a.GetWorkTimeDesc(this.e, DayOfWeek.Sunday));
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00019120 File Offset: 0x00017320
		private void a(Label A_0, string A_1)
		{
			FormCustomerServiceWorksheetNew.a a = new FormCustomerServiceWorksheetNew.a();
			a.a = A_0;
			a.b = A_1;
			if (base.InvokeRequired)
			{
				a.a.Invoke(new Action(a.c));
			}
			else
			{
				a.a.Text = a.b;
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00019174 File Offset: 0x00017374
		private FormCustomerServiceWorksheetNew.Pair a(ref List<int> A_0)
		{
			FormCustomerServiceWorksheetNew.Pair pair;
			if (A_0 == null || A_0.Count <= 0)
			{
				pair = null;
			}
			else
			{
				FormCustomerServiceWorksheetNew.Pair pair2 = new FormCustomerServiceWorksheetNew.Pair();
				int num = A_0[0];
				pair2.Min = num;
				pair2.Max = num;
				if (A_0.Count != 1)
				{
					int num2 = 0;
					for (int i = 1; i < A_0.Count; i++)
					{
						if (++num != A_0[i])
						{
							num2 = i;
							IL_008E:
							A_0.RemoveRange(0, num2);
							return pair2;
						}
						pair2.Max = num;
						num2 = i + 1;
					}
					goto IL_008E;
				}
				A_0.RemoveAt(0);
				pair = pair2;
			}
			return pair;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00019220 File Offset: 0x00017420
		private void c(object sender, EventArgs e)
		{
			if (this.c == null)
			{
				this.Text = "添加客服信息";
				this.o.Text = Util.GetMasterNick(this.b.UserNick) + ":";
				this.o.SelectionStart = this.o.Text.Length;
			}
			else
			{
				if (this.c.IdNo > 0L)
				{
					this.Text = "修改客服信息";
				}
				else
				{
					this.Text = "复制客服信息";
				}
				this.o.Text = this.c.UserNick;
				int interval = this.m.Interval;
				RectangleCollection rectangles = this.m.Rectangles;
				this.e = this.c.DetailWorksheet;
				if (this.e != null)
				{
					foreach (KeyValuePair<DayOfWeek, List<WorkTimeInfo>> keyValuePair in this.e)
					{
						new List<int>();
						if (rectangles.Keys.Contains(keyValuePair.Key))
						{
							foreach (WorkTimeInfo workTimeInfo in keyValuePair.Value)
							{
								int num = workTimeInfo.HourFrom * interval / 24 + ((workTimeInfo.MinuteFrom > 0) ? 1 : 0);
								int num2 = workTimeInfo.HourTo * interval / 24 + ((workTimeInfo.MinuteTo > 0) ? 1 : 0);
								for (int i = num; i < num2; i++)
								{
									rectangles[keyValuePair.Key][i].IsSelect = true;
								}
							}
						}
					}
					this.a(this.g, this.a.GetWorkTimeDesc(this.e, DayOfWeek.Monday));
					this.a(this.h, this.a.GetWorkTimeDesc(this.e, DayOfWeek.Tuesday));
					this.a(this.i, this.a.GetWorkTimeDesc(this.e, DayOfWeek.Wednesday));
					this.a(this.l, this.a.GetWorkTimeDesc(this.e, DayOfWeek.Thursday));
					this.a(this.k, this.a.GetWorkTimeDesc(this.e, DayOfWeek.Friday));
					this.a(this.j, this.a.GetWorkTimeDesc(this.e, DayOfWeek.Saturday));
					this.a(this.n, this.a.GetWorkTimeDesc(this.e, DayOfWeek.Sunday));
				}
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000194EC File Offset: 0x000176EC
		private void b(object sender, EventArgs e)
		{
			string text = this.o.Text;
			if (string.IsNullOrEmpty(text))
			{
				MessageBox.Show("客服昵称不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else if (this.a.ValidateManualNick(text))
			{
				try
				{
					if (this.c == null)
					{
						CustomerServiceWorksheetManager.Insert(new CustomerServiceWorksheet
						{
							MouldId = this.d,
							SellerNick = this.a.SellerNick,
							UserNick = text,
							WorkTimeJson = ((this.e == null) ? "" : JSON.Encode(this.e))
						});
					}
					else
					{
						this.c.MouldId = this.d;
						this.c.SellerNick = this.a.SellerNick;
						this.c.UserNick = text;
						this.c.WorkTimeJson = ((this.e == null) ? "" : JSON.Encode(this.e));
						if (this.c.IdNo > 0L)
						{
							CustomerServiceWorksheetManager.Update(this.c);
						}
						else
						{
							CustomerServiceWorksheetManager.Insert(this.c);
						}
					}
					base.Close();
					this.a.InitCustomerServiceWorksheet();
				}
				catch (Exception ex)
				{
					if (ex.ToString().Contains("UNIQUE") && ex.ToString().Contains("MouldId") && ex.ToString().Contains("UserNick"))
					{
						MessageBox.Show("您添加的客服已存在了", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00002708 File Offset: 0x00000908
		private void a(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000196E4 File Offset: 0x000178E4
		private void a()
		{
			this.r = new Button();
			this.q = new Button();
			this.o = new TextBox();
			this.p = new Label();
			this.n = new Label();
			this.j = new Label();
			this.k = new Label();
			this.l = new Label();
			this.i = new Label();
			this.h = new Label();
			this.g = new Label();
			this.m = new TimerGrid();
			base.SuspendLayout();
			this.r.Location = new Point(761, 289);
			this.r.Name = "btnCancel";
			this.r.Size = new Size(75, 23);
			this.r.TabIndex = 16;
			this.r.Text = "取消(&C)";
			this.r.UseVisualStyleBackColor = true;
			this.r.Click += this.a;
			this.q.Location = new Point(671, 289);
			this.q.Name = "btnSave";
			this.q.Size = new Size(75, 23);
			this.q.TabIndex = 15;
			this.q.Text = "保存(&S)";
			this.q.UseVisualStyleBackColor = true;
			this.q.Click += this.b;
			this.o.Location = new Point(106, 16);
			this.o.Name = "txtCustomerServiceName";
			this.o.Size = new Size(257, 21);
			this.o.TabIndex = 14;
			this.p.AutoSize = true;
			this.p.Location = new Point(35, 19);
			this.p.Name = "label5";
			this.p.Size = new Size(65, 12);
			this.p.TabIndex = 13;
			this.p.Text = "客服昵称：";
			this.n.AutoSize = true;
			this.n.Location = new Point(843, 248);
			this.n.Name = "lbTime0";
			this.n.Size = new Size(0, 12);
			this.n.TabIndex = 10;
			this.j.AutoSize = true;
			this.j.Location = new Point(843, 218);
			this.j.Name = "lbTime6";
			this.j.Size = new Size(0, 12);
			this.j.TabIndex = 9;
			this.k.AutoSize = true;
			this.k.Location = new Point(843, 189);
			this.k.Name = "lbTime5";
			this.k.Size = new Size(0, 12);
			this.k.TabIndex = 8;
			this.l.AutoSize = true;
			this.l.Location = new Point(843, 158);
			this.l.Name = "lbTime4";
			this.l.Size = new Size(0, 12);
			this.l.TabIndex = 7;
			this.i.AutoSize = true;
			this.i.Location = new Point(843, 128);
			this.i.Name = "lbTime3";
			this.i.Size = new Size(0, 12);
			this.i.TabIndex = 3;
			this.h.AutoSize = true;
			this.h.Location = new Point(843, 98);
			this.h.Name = "lbTime2";
			this.h.Size = new Size(0, 12);
			this.h.TabIndex = 2;
			this.g.AutoSize = true;
			this.g.Location = new Point(843, 68);
			this.g.Name = "lbTime1";
			this.g.Size = new Size(0, 12);
			this.g.TabIndex = 1;
			this.m.BackColor = SystemColors.Control;
			this.m.Interval = 48;
			this.m.Location = new Point(19, 39);
			this.m.MouseUpAction = null;
			this.m.Name = "timerControl1";
			this.m.Size = new Size(823, 238);
			this.m.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = SystemColors.Control;
			base.ClientSize = new Size(1073, 347);
			base.Controls.Add(this.r);
			base.Controls.Add(this.q);
			base.Controls.Add(this.o);
			base.Controls.Add(this.p);
			base.Controls.Add(this.n);
			base.Controls.Add(this.j);
			base.Controls.Add(this.k);
			base.Controls.Add(this.l);
			base.Controls.Add(this.i);
			base.Controls.Add(this.h);
			base.Controls.Add(this.g);
			base.Controls.Add(this.m);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormCustomerServiceWorksheetNew";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "FormCustomerServiceWorksheerNew";
			base.Load += this.c;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000E8 RID: 232
		private FormAldsAccountEdit a;

		// Token: 0x040000E9 RID: 233
		private AldsAccountInfo b;

		// Token: 0x040000EA RID: 234
		private CustomerServiceWorksheet c;

		// Token: 0x040000EB RID: 235
		private long d;

		// Token: 0x040000EC RID: 236
		private DetailWorksheetInfo e;

		// Token: 0x040000EE RID: 238
		private Label g;

		// Token: 0x040000EF RID: 239
		private Label h;

		// Token: 0x040000F0 RID: 240
		private Label i;

		// Token: 0x040000F1 RID: 241
		private Label j;

		// Token: 0x040000F2 RID: 242
		private Label k;

		// Token: 0x040000F3 RID: 243
		private Label l;

		// Token: 0x040000F4 RID: 244
		private TimerGrid m;

		// Token: 0x040000F5 RID: 245
		private Label n;

		// Token: 0x040000F6 RID: 246
		private TextBox o;

		// Token: 0x040000F7 RID: 247
		private Label p;

		// Token: 0x040000F8 RID: 248
		private Button q;

		// Token: 0x040000F9 RID: 249
		private Button r;

		// Token: 0x0200002A RID: 42
		public class Pair
		{
			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000109 RID: 265 RVA: 0x00002868 File Offset: 0x00000A68
			// (set) Token: 0x0600010A RID: 266 RVA: 0x00002870 File Offset: 0x00000A70
			public int Min { get; set; }

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600010B RID: 267 RVA: 0x00002879 File Offset: 0x00000A79
			// (set) Token: 0x0600010C RID: 268 RVA: 0x00002881 File Offset: 0x00000A81
			public int Max { get; set; }

			// Token: 0x040000FA RID: 250
			[CompilerGenerated]
			private int a;

			// Token: 0x040000FB RID: 251
			[CompilerGenerated]
			private int b;
		}

		// Token: 0x0200002B RID: 43
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x0600010F RID: 271 RVA: 0x0000288A File Offset: 0x00000A8A
			internal void c()
			{
				this.a.Text = this.b;
			}

			// Token: 0x040000FC RID: 252
			public Label a;

			// Token: 0x040000FD RID: 253
			public string b;
		}
	}
}
