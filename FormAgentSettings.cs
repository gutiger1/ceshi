using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Agiso;
using Agiso.Object;
using Agiso.Utils;
using Agiso.WwWebSocket.Model;
using AliwwClient.Properties;

namespace AliwwClient
{
	// Token: 0x02000023 RID: 35
	public partial class FormAgentSettings : BaseForm
	{
		// Token: 0x060000BC RID: 188 RVA: 0x000130AC File Offset: 0x000112AC
		public FormAgentSettings()
		{
			this.a();
			this.b();
			if (AppConfig.AllowAutoLogin)
			{
				base.StartPosition = FormStartPosition.Manual;
				base.Location = new Point(Screen.PrimaryScreen.Bounds.Width - base.Width, Screen.PrimaryScreen.Bounds.Height - base.Height - 40);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00013124 File Offset: 0x00011324
		public static FormAgentSettings FormAgentSettionInstance
		{
			get
			{
				if (FormAgentSettings.a == null || FormAgentSettings.a.IsDisposed)
				{
					FormAgentSettings.a = new FormAgentSettings();
				}
				return FormAgentSettings.a;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00013158 File Offset: 0x00011358
		private void b()
		{
			Dictionary<long, string> enumValAndDescList = EnumTool.GetEnumValAndDescList<QnVersionType>();
			foreach (KeyValuePair<long, string> keyValuePair in enumValAndDescList)
			{
				RadioButton radioButton = new RadioButton();
				radioButton.Text = keyValuePair.Value;
				radioButton.Name = "rdQnVersion" + keyValuePair.Key.ToString();
				radioButton.Tag = keyValuePair.Key;
				radioButton.CheckedChanged += this.a;
				this.s.Controls.Add(radioButton);
			}
			foreach (object obj in Enum.GetValues(typeof(DayOfWeek)))
			{
				DayOfWeek dayOfWeek = (DayOfWeek)obj;
				CheckBox checkBox = new CheckBox();
				switch (dayOfWeek)
				{
				case DayOfWeek.Sunday:
					checkBox.Text = "星期日";
					break;
				case DayOfWeek.Monday:
					checkBox.Text = "星期一";
					break;
				case DayOfWeek.Tuesday:
					checkBox.Text = "星期二";
					break;
				case DayOfWeek.Wednesday:
					checkBox.Text = "星期三";
					break;
				case DayOfWeek.Thursday:
					checkBox.Text = "星期四";
					break;
				case DayOfWeek.Friday:
					checkBox.Text = "星期五";
					break;
				case DayOfWeek.Saturday:
					checkBox.Text = "星期六";
					break;
				}
				Control control = checkBox;
				string text = "rdWeekDay";
				int num = (int)dayOfWeek;
				control.Name = text + num.ToString();
				checkBox.Tag = (int)dayOfWeek;
				checkBox.Width = 70;
				this.g.Controls.Add(checkBox);
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00002792 File Offset: 0x00000992
		private void c(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00013364 File Offset: 0x00011564
		private void b(object sender, EventArgs e)
		{
			AgentHostSetting agentSettings = AppConfig.AgentSettings;
			this.d.Checked = agentSettings.AllowAutoExitQn;
			this.f.Checked = agentSettings.AutoKillAllAliWorkbenchAndAliApp;
			this.q.Checked = agentSettings.LoginOnQn;
			this.r.Checked = agentSettings.LoginOnAliwwBuyer;
			this.l.Checked = agentSettings.AutoMinimizeTalkWindow;
			foreach (object obj in this.s.Controls)
			{
				Control control = (Control)obj;
				if (control is RadioButton && Util.ToInt(control.Tag) == agentSettings.QnVersion)
				{
					((RadioButton)control).Checked = true;
				}
			}
			this.i.Text = agentSettings.AllowAutoKillQnTimeFrom.ToString();
			this.h.Text = agentSettings.AllowAutoKillQnTimeTo.ToString();
			string[] array = (agentSettings.SelectWeekDays ?? "").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			foreach (object obj2 in this.g.Controls)
			{
				Control control2 = (Control)obj2;
				if (control2 is CheckBox && Array.IndexOf<string>(array, control2.Tag.ToString()) >= 0)
				{
					((CheckBox)control2).Checked = true;
				}
				else
				{
					((CheckBox)control2).Checked = false;
				}
			}
			this.o.Checked = agentSettings.SwitchNickAfterFiveMsg;
			this.m.Text = AppConfig.GetAlicdnLocalHostIp();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00013590 File Offset: 0x00011790
		private void a()
		{
			this.e = new Button();
			this.c = new GroupBox();
			this.p = new Panel();
			this.q = new CheckBox();
			this.r = new CheckBox();
			this.s = new FlowLayoutPanel();
			this.o = new CheckBox();
			this.m = new TextBox();
			this.n = new Label();
			this.l = new CheckBox();
			this.h = new TextBox();
			this.i = new TextBox();
			this.j = new Label();
			this.k = new Label();
			this.g = new FlowLayoutPanel();
			this.f = new CheckBox();
			this.d = new CheckBox();
			this.c.SuspendLayout();
			this.p.SuspendLayout();
			base.SuspendLayout();
			this.e.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.e.Location = new Point(324, 352);
			this.e.Name = "BtnCancel";
			this.e.Size = new Size(75, 23);
			this.e.TabIndex = 2;
			this.e.Text = "取消(&C)";
			this.e.UseVisualStyleBackColor = true;
			this.e.Click += this.c;
			this.c.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.c.Controls.Add(this.p);
			this.c.Controls.Add(this.o);
			this.c.Controls.Add(this.m);
			this.c.Controls.Add(this.n);
			this.c.Controls.Add(this.l);
			this.c.Controls.Add(this.h);
			this.c.Controls.Add(this.i);
			this.c.Controls.Add(this.j);
			this.c.Controls.Add(this.k);
			this.c.Controls.Add(this.g);
			this.c.Controls.Add(this.f);
			this.c.Controls.Add(this.d);
			this.c.Location = new Point(12, 12);
			this.c.Name = "groupBox1";
			this.c.Size = new Size(389, 316);
			this.c.TabIndex = 0;
			this.c.TabStop = false;
			this.c.Text = "代挂选项";
			this.p.Controls.Add(this.q);
			this.p.Controls.Add(this.r);
			this.p.Controls.Add(this.s);
			this.p.Location = new Point(26, 114);
			this.p.Name = "panel1";
			this.p.Size = new Size(310, 46);
			this.p.TabIndex = 26;
			this.q.AutoSize = true;
			this.q.Location = new Point(5, 4);
			this.q.Name = "CbLoginOnQn";
			this.q.Size = new Size(72, 16);
			this.q.TabIndex = 14;
			this.q.Text = "千牛登录";
			this.q.UseVisualStyleBackColor = true;
			this.r.AutoSize = true;
			this.r.Location = new Point(83, 4);
			this.r.Name = "CbLoginOnAliwwBuyer";
			this.r.Size = new Size(72, 16);
			this.r.TabIndex = 15;
			this.r.Text = "旺旺登录";
			this.r.UseVisualStyleBackColor = true;
			this.s.Location = new Point(3, 18);
			this.s.Name = "QnVersionflPanel";
			this.s.Size = new Size(301, 24);
			this.s.TabIndex = 16;
			this.o.AutoSize = true;
			this.o.Location = new Point(30, 95);
			this.o.Name = "cbSwitchNickAfterFiveMsg";
			this.o.Size = new Size(180, 16);
			this.o.TabIndex = 25;
			this.o.Text = "发送五条消息之后就切换用户";
			this.o.UseVisualStyleBackColor = true;
			this.m.Location = new Point(129, 257);
			this.m.Name = "txtAlicdnIP";
			this.m.Size = new Size(184, 21);
			this.m.TabIndex = 24;
			this.n.AutoSize = true;
			this.n.Enabled = false;
			this.n.Location = new Point(28, 260);
			this.n.Name = "label4";
			this.n.Size = new Size(101, 12);
			this.n.TabIndex = 23;
			this.n.Text = "alicdn重定向IP：";
			this.l.AutoSize = true;
			this.l.Location = new Point(30, 74);
			this.l.Name = "cbAutoMinimizeTalkWindow";
			this.l.Size = new Size(180, 16);
			this.l.TabIndex = 21;
			this.l.Text = "发送完成之后自动最小化窗口";
			this.l.UseVisualStyleBackColor = true;
			this.h.Location = new Point(230, 163);
			this.h.Name = "txtAllowAutoKillQnTimeTo";
			this.h.Size = new Size(40, 21);
			this.h.TabIndex = 19;
			this.h.Text = "24";
			this.h.TextAlign = HorizontalAlignment.Right;
			this.i.Location = new Point(167, 163);
			this.i.Name = "txtAllowAutoKillQnTimeFrom";
			this.i.Size = new Size(40, 21);
			this.i.TabIndex = 18;
			this.i.Text = "0";
			this.i.TextAlign = HorizontalAlignment.Right;
			this.j.AutoSize = true;
			this.j.Location = new Point(210, 166);
			this.j.Name = "label3";
			this.j.Size = new Size(17, 12);
			this.j.TabIndex = 17;
			this.j.Text = "到";
			this.k.AutoSize = true;
			this.k.Location = new Point(28, 166);
			this.k.Name = "label2";
			this.k.Size = new Size(137, 12);
			this.k.TabIndex = 16;
			this.k.Text = "允许自动关闭千牛时间从";
			this.g.Location = new Point(26, 188);
			this.g.Name = "WeekDayflPanel";
			this.g.Size = new Size(336, 61);
			this.g.TabIndex = 15;
			this.f.AutoSize = true;
			this.f.Location = new Point(30, 53);
			this.f.Name = "CbAutoKillAllAliWorkbenchAndAliApp";
			this.f.Size = new Size(252, 16);
			this.f.TabIndex = 10;
			this.f.Text = "每一段时间自动杀掉所有千牛和AliApp进程";
			this.f.UseVisualStyleBackColor = true;
			this.d.AutoSize = true;
			this.d.Location = new Point(30, 33);
			this.d.Name = "CbAllowAutoExitQn";
			this.d.Size = new Size(264, 16);
			this.d.TabIndex = 0;
			this.d.Text = "发送完成后自动退出千牛（对所有帐号生效）";
			this.d.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(413, 387);
			base.Controls.Add(this.e);
			base.Controls.Add(this.c);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.Icon = Resources.Icon1;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormAgentSettings";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "代挂选项";
			base.Load += this.b;
			this.c.ResumeLayout(false);
			this.c.PerformLayout();
			this.p.ResumeLayout(false);
			this.p.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00013F7C File Offset: 0x0001217C
		[CompilerGenerated]
		private void a(object sender, EventArgs e)
		{
			if (this.m.Text.Trim() == "")
			{
				this.m.Text = "127.0.0.1";
			}
		}

		// Token: 0x04000079 RID: 121
		private static FormAgentSettings a;

		// Token: 0x0400007B RID: 123
		private GroupBox c;

		// Token: 0x0400007C RID: 124
		private CheckBox d;

		// Token: 0x0400007D RID: 125
		private Button e;

		// Token: 0x0400007E RID: 126
		private CheckBox f;

		// Token: 0x0400007F RID: 127
		private FlowLayoutPanel g;

		// Token: 0x04000080 RID: 128
		private TextBox h;

		// Token: 0x04000081 RID: 129
		private TextBox i;

		// Token: 0x04000082 RID: 130
		private Label j;

		// Token: 0x04000083 RID: 131
		private Label k;

		// Token: 0x04000084 RID: 132
		private CheckBox l;

		// Token: 0x04000085 RID: 133
		private TextBox m;

		// Token: 0x04000086 RID: 134
		private Label n;

		// Token: 0x04000087 RID: 135
		private CheckBox o;

		// Token: 0x04000088 RID: 136
		private Panel p;

		// Token: 0x04000089 RID: 137
		private CheckBox q;

		// Token: 0x0400008A RID: 138
		private CheckBox r;

		// Token: 0x0400008B RID: 139
		private FlowLayoutPanel s;
	}
}
