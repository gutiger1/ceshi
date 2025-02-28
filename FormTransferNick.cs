using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace AliwwClient
{
	// Token: 0x02000038 RID: 56
	public partial class FormTransferNick : Form
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000029FC File Offset: 0x00000BFC
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00002A04 File Offset: 0x00000C04
		public string TransferNick { get; private set; }

		// Token: 0x06000165 RID: 357 RVA: 0x00002A0D File Offset: 0x00000C0D
		public FormTransferNick()
		{
			this.a();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00002A23 File Offset: 0x00000C23
		private void b(object sender, EventArgs e)
		{
			this.TransferNick = "";
			base.Close();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00002A36 File Offset: 0x00000C36
		private void a(object sender, EventArgs e)
		{
			this.TransferNick = this.e.Text.Trim().Replace("：", ":");
			base.Close();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0001FD68 File Offset: 0x0001DF68
		private void a()
		{
			this.c = new Panel();
			this.d = new Label();
			this.e = new TextBox();
			this.f = new Button();
			this.g = new Button();
			this.c.SuspendLayout();
			base.SuspendLayout();
			this.c.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.c.Controls.Add(this.d);
			this.c.Controls.Add(this.e);
			this.c.Controls.Add(this.f);
			this.c.Controls.Add(this.g);
			this.c.Location = new Point(12, 12);
			this.c.Name = "panel1";
			this.c.Size = new Size(359, 97);
			this.c.TabIndex = 4;
			this.d.AutoSize = true;
			this.d.Location = new Point(13, 18);
			this.d.Name = "label1";
			this.d.Size = new Size(125, 12);
			this.d.TabIndex = 7;
			this.d.Text = "请输入转接客服账号：";
			this.e.Location = new Point(139, 14);
			this.e.Name = "txtTransferNick";
			this.e.Size = new Size(207, 21);
			this.e.TabIndex = 6;
			this.f.Location = new Point(271, 54);
			this.f.Name = "btnCancel";
			this.f.Size = new Size(75, 23);
			this.f.TabIndex = 5;
			this.f.Text = "取消(&C)";
			this.f.UseVisualStyleBackColor = true;
			this.f.Click += this.b;
			this.g.Location = new Point(185, 54);
			this.g.Name = "btnSure";
			this.g.Size = new Size(75, 23);
			this.g.TabIndex = 4;
			this.g.Text = "确定(&S)";
			this.g.UseVisualStyleBackColor = true;
			this.g.Click += this.a;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(383, 121);
			base.Controls.Add(this.c);
			base.Name = "FormTransferNick";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "指定转接客服";
			this.c.ResumeLayout(false);
			this.c.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000177 RID: 375
		[CompilerGenerated]
		private string a;

		// Token: 0x04000179 RID: 377
		private Panel c;

		// Token: 0x0400017A RID: 378
		private Label d;

		// Token: 0x0400017B RID: 379
		private TextBox e;

		// Token: 0x0400017C RID: 380
		private Button f;

		// Token: 0x0400017D RID: 381
		private Button g;
	}
}
