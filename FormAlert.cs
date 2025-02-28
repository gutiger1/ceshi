using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AliwwClient
{
	// Token: 0x0200003A RID: 58
	public partial class FormAlert : Form
	{
		// Token: 0x0600016F RID: 367 RVA: 0x00002A94 File Offset: 0x00000C94
		public FormAlert()
		{
			this.a();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00002708 File Offset: 0x00000908
		private void a(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00020408 File Offset: 0x0001E608
		private void a()
		{
			this.b = new Button();
			this.c = new Label();
			this.d = new Label();
			base.SuspendLayout();
			this.b.Location = new Point(73, 238);
			this.b.Name = "button1";
			this.b.Size = new Size(292, 39);
			this.b.TabIndex = 2;
			this.b.Text = "我已知道风险，认真阅读了说明并已设置正确";
			this.b.UseVisualStyleBackColor = true;
			this.b.Click += this.a;
			this.c.AutoSize = true;
			this.c.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 134);
			this.c.Location = new Point(37, 203);
			this.c.Name = "label2";
			this.c.Size = new Size(235, 12);
			this.c.TabIndex = 3;
			this.c.Text = "因此导致的纠纷或损失Agiso概不负责！";
			this.d.AutoSize = true;
			this.d.Location = new Point(37, 24);
			this.d.Name = "label1";
			this.d.Size = new Size(365, 156);
			this.d.TabIndex = 0;
			this.d.Text = "添加旺旺帐户已成功。\r\n\r\n【注意】当买家聊天窗口共用一个窗体时，有小概率发错人的风险。\r\n\r\n特别是卡密用户，发错人将造成损失。\r\n\r\n【注意】发送时会弹出的窗口，这时如果你正好要粘贴某文字，你的\r\n\r\n粘贴操作有小概率可能会粘贴到正要发送的窗口上，因此建议独立电\r\n\r\n脑发送。\r\n\r\n【注意】请保持助手跟千牛（或者旺旺）同时在线。";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(451, 299);
			base.ControlBox = false;
			base.Controls.Add(this.c);
			base.Controls.Add(this.b);
			base.Controls.Add(this.d);
			base.FormBorderStyle = FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormAlert";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "帐号添加成功";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000184 RID: 388
		private Button b;

		// Token: 0x04000185 RID: 389
		private Label c;

		// Token: 0x04000186 RID: 390
		private Label d;
	}
}
