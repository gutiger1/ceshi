using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Agiso;
using Agiso.Utils;

namespace AliwwClient
{
	// Token: 0x0200001F RID: 31
	public partial class FormAutoReplySetting : Form
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x000026C7 File Offset: 0x000008C7
		public FormAutoReplySetting()
		{
			this.a();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00011900 File Offset: 0x0000FB00
		private void b(object sender, EventArgs e)
		{
			this.m.Text = AppConfig.CurrentSystemSettingInfo.RecvMsgReplyInterval.ToString();
			this.c.Text = AppConfig.CurrentSystemSettingInfo.SameQueryReplyInterval.ToString();
			this.d.Text = AppConfig.CurrentSystemSettingInfo.NoMatchReplyInterval.ToString();
			this.j.Text = AppConfig.CurrentSystemSettingInfo.TransferInterval.ToString();
			this.o.Text = AppConfig.CurrentSystemSettingInfo.FirstReplyInterval.ToString();
			this.p.Checked = AppConfig.CurrentSystemSettingInfo.FirstReplyContinueNoMatch;
			this.q.Checked = AppConfig.CurrentSystemSettingInfo.FirstReplyContinueMatch;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000119C8 File Offset: 0x0000FBC8
		private void a(object sender, EventArgs e)
		{
			int num = Util.ToInt(this.m.Text.Trim());
			int num2 = Util.ToInt(this.c.Text.Trim());
			int num3 = Util.ToInt(this.d.Text.Trim());
			int num4 = Util.ToInt(this.j.Text.Trim());
			int num5 = Util.ToInt(this.o.Text.Trim());
			if (num < 0)
			{
				MessageBox.Show("收到消息后答复的时间必须为数字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (num2 < 0 || num2 > 86400)
			{
				MessageBox.Show("相同咨询不再回复的时间必须为数字，最大可设置86400秒，即24小时", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (num3 < 0)
			{
				MessageBox.Show("无匹配答复不重复答复的时间必须是数字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (num4 < 0)
			{
				MessageBox.Show("转接的时间必须为数字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (num5 < 30)
			{
				MessageBox.Show("首次答复时间间隔必须为数字，且必须大于等于30", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				try
				{
					AppConfig.CurrentSystemSettingInfo.RecvMsgReplyInterval = num;
					AppConfig.CurrentSystemSettingInfo.SameQueryReplyInterval = num2;
					AppConfig.CurrentSystemSettingInfo.NoMatchReplyInterval = num3;
					AppConfig.CurrentSystemSettingInfo.TransferInterval = num4;
					AppConfig.CurrentSystemSettingInfo.FirstReplyInterval = num5;
					AppConfig.CurrentSystemSettingInfo.FirstReplyContinueNoMatch = this.p.Checked;
					AppConfig.CurrentSystemSettingInfo.FirstReplyContinueMatch = this.q.Checked;
					AppConfig.SaveConfig();
				}
				catch (Exception ex)
				{
					MessageBox.Show("保存异常，异常：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					LogWriter.WriteLog(ex.ToString(), 1);
				}
				base.Close();
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00011BD8 File Offset: 0x0000FDD8
		private void a()
		{
			this.b = new Label();
			this.c = new TextBox();
			this.d = new TextBox();
			this.e = new Label();
			this.f = new Button();
			this.g = new Label();
			this.h = new Label();
			this.i = new Label();
			this.j = new TextBox();
			this.k = new Label();
			this.l = new Label();
			this.m = new TextBox();
			this.n = new Label();
			this.o = new TextBox();
			this.p = new CheckBox();
			this.q = new CheckBox();
			base.SuspendLayout();
			this.b.AutoSize = true;
			this.b.Location = new Point(61, 45);
			this.b.Name = "label1";
			this.b.Size = new Size(287, 12);
			this.b.TabIndex = 258;
			this.b.Text = "秒内相同的咨询或者相同的答复不重复答复(0-86400)";
			this.c.Location = new Point(20, 42);
			this.c.Name = "txtSameQueryReplyInterval";
			this.c.Size = new Size(38, 21);
			this.c.TabIndex = 257;
			this.c.Text = "60";
			this.d.Location = new Point(159, 66);
			this.d.Name = "txNoMatchReplyInterval";
			this.d.Size = new Size(38, 21);
			this.d.TabIndex = 263;
			this.d.Text = "0";
			this.e.AutoSize = true;
			this.e.Location = new Point(18, 69);
			this.e.Name = "label5";
			this.e.Size = new Size(137, 12);
			this.e.TabIndex = 262;
			this.e.Text = "无匹配答复后，同一买家";
			this.f.Location = new Point(262, 200);
			this.f.Name = "btnSave";
			this.f.Size = new Size(75, 23);
			this.f.TabIndex = 265;
			this.f.Text = "保存";
			this.f.UseVisualStyleBackColor = true;
			this.f.Click += this.a;
			this.g.AutoSize = true;
			this.g.Location = new Point(200, 69);
			this.g.Name = "label2";
			this.g.Size = new Size(137, 12);
			this.g.TabIndex = 269;
			this.g.Text = "秒内不再进行无匹配答复";
			this.h.AutoSize = true;
			this.h.Location = new Point(18, 93);
			this.h.Margin = new Padding(4, 0, 4, 0);
			this.h.Name = "label3";
			this.h.Size = new Size(125, 12);
			this.h.TabIndex = 272;
			this.h.Text = "转接成功后，同一买家";
			this.i.AutoSize = true;
			this.i.Location = new Point(187, 93);
			this.i.Margin = new Padding(4, 0, 4, 0);
			this.i.Name = "label9";
			this.i.Size = new Size(77, 12);
			this.i.TabIndex = 271;
			this.i.Text = "秒内不再转接";
			this.j.Location = new Point(146, 90);
			this.j.Margin = new Padding(4, 2, 4, 2);
			this.j.Name = "txtTransferInterval";
			this.j.Size = new Size(38, 21);
			this.j.TabIndex = 270;
			this.j.Text = "0";
			this.k.AutoSize = true;
			this.k.Location = new Point(18, 21);
			this.k.Margin = new Padding(4, 0, 4, 0);
			this.k.Name = "label7";
			this.k.Size = new Size(77, 12);
			this.k.TabIndex = 279;
			this.k.Text = "收到消息后，";
			this.l.AutoSize = true;
			this.l.Location = new Point(139, 21);
			this.l.Margin = new Padding(4, 0, 4, 0);
			this.l.Name = "label8";
			this.l.Size = new Size(77, 12);
			this.l.TabIndex = 278;
			this.l.Text = "秒后进行答复";
			this.m.Location = new Point(98, 17);
			this.m.Margin = new Padding(4, 2, 4, 2);
			this.m.Name = "txtRecvMsgReplyInterval";
			this.m.Size = new Size(38, 21);
			this.m.TabIndex = 277;
			this.m.Text = "0";
			this.n.AutoSize = true;
			this.n.Location = new Point(61, 118);
			this.n.Name = "label4";
			this.n.Size = new Size(161, 12);
			this.n.TabIndex = 281;
			this.n.Text = "分钟内无答复才执行首次答复";
			this.o.Location = new Point(20, 115);
			this.o.Name = "txtFrirstReplyInterval";
			this.o.Size = new Size(38, 21);
			this.o.TabIndex = 280;
			this.o.Text = "30";
			this.p.AutoSize = true;
			this.p.Location = new Point(20, 144);
			this.p.Name = "cbFirstReplyContinueNoMatch";
			this.p.Size = new Size(180, 16);
			this.p.TabIndex = 282;
			this.p.Text = "首次答复后，执行无匹配答复";
			this.p.UseVisualStyleBackColor = true;
			this.q.AutoSize = true;
			this.q.Location = new Point(20, 166);
			this.q.Name = "cbFirstReplyContinueMatch";
			this.q.Size = new Size(276, 16);
			this.q.TabIndex = 283;
			this.q.Text = "首次答复后，如果匹配到关键词继续发送答复语";
			this.q.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(353, 244);
			base.Controls.Add(this.q);
			base.Controls.Add(this.p);
			base.Controls.Add(this.n);
			base.Controls.Add(this.o);
			base.Controls.Add(this.k);
			base.Controls.Add(this.l);
			base.Controls.Add(this.m);
			base.Controls.Add(this.h);
			base.Controls.Add(this.i);
			base.Controls.Add(this.j);
			base.Controls.Add(this.g);
			base.Controls.Add(this.f);
			base.Controls.Add(this.d);
			base.Controls.Add(this.e);
			base.Controls.Add(this.b);
			base.Controls.Add(this.c);
			base.Name = "FormAutoReplySetting";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "智能答复设置";
			base.Load += this.b;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000056 RID: 86
		private Label b;

		// Token: 0x04000057 RID: 87
		private TextBox c;

		// Token: 0x04000058 RID: 88
		private TextBox d;

		// Token: 0x04000059 RID: 89
		private Label e;

		// Token: 0x0400005A RID: 90
		private Button f;

		// Token: 0x0400005B RID: 91
		private Label g;

		// Token: 0x0400005C RID: 92
		private Label h;

		// Token: 0x0400005D RID: 93
		private Label i;

		// Token: 0x0400005E RID: 94
		private TextBox j;

		// Token: 0x0400005F RID: 95
		private Label k;

		// Token: 0x04000060 RID: 96
		private Label l;

		// Token: 0x04000061 RID: 97
		private TextBox m;

		// Token: 0x04000062 RID: 98
		private Label n;

		// Token: 0x04000063 RID: 99
		private TextBox o;

		// Token: 0x04000064 RID: 100
		private CheckBox p;

		// Token: 0x04000065 RID: 101
		private CheckBox q;
	}
}
