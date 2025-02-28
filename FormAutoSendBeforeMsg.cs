using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Agiso;

namespace AliwwClient
{
	// Token: 0x0200002F RID: 47
	public partial class FormAutoSendBeforeMsg : Form
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00002901 File Offset: 0x00000B01
		public FormAutoSendBeforeMsg()
		{
			this.a();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0001D3D8 File Offset: 0x0001B5D8
		private void b(object sender, EventArgs e)
		{
			AppConfig.GetAliwwMsgStartTime = DateTime.Now.AddHours(-5.0);
			base.Close();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00002708 File Offset: 0x00000908
		private void a(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0001D438 File Offset: 0x0001B638
		private void a()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormAutoSendBeforeMsg));
			this.b = new Button();
			this.c = new Button();
			this.d = new Label();
			base.SuspendLayout();
			this.b.Location = new Point(148, 73);
			this.b.Name = "btnSure";
			this.b.Size = new Size(75, 23);
			this.b.TabIndex = 0;
			this.b.Text = "确定";
			this.b.UseVisualStyleBackColor = true;
			this.b.Click += this.b;
			this.c.Location = new Point(238, 73);
			this.c.Name = "btnCancel";
			this.c.Size = new Size(75, 23);
			this.c.TabIndex = 1;
			this.c.Text = "取消";
			this.c.UseVisualStyleBackColor = true;
			this.c.Click += this.a;
			this.d.AutoSize = true;
			this.d.Location = new Point(27, 26);
			this.d.Name = "label1";
			this.d.Size = new Size(239, 12);
			this.d.TabIndex = 2;
			this.d.Text = "是否需要要发送5小时内的未发的订单消息？";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(353, 120);
			base.Controls.Add(this.d);
			base.Controls.Add(this.c);
			base.Controls.Add(this.b);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "FormAutoSendBeforeMsg";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "提示";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000143 RID: 323
		private Button b;

		// Token: 0x04000144 RID: 324
		private Button c;

		// Token: 0x04000145 RID: 325
		private Label d;
	}
}
