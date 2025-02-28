using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AliwwClient
{
	// Token: 0x02000039 RID: 57
	public partial class FormUpgrade : Form
	{
		// Token: 0x0600016A RID: 362 RVA: 0x00002A65 File Offset: 0x00000C65
		public FormUpgrade(string tip)
		{
			this.a();
			this.d.Text = tip;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00002708 File Offset: 0x00000908
		private void b(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00002A87 File Offset: 0x00000C87
		private void a(object sender, EventArgs e)
		{
			k.a().AliwwClient_OnUpgrade(true);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000200C4 File Offset: 0x0001E2C4
		private void a()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormUpgrade));
			this.b = new Panel();
			this.e = new Button();
			this.c = new Button();
			this.d = new Label();
			this.b.SuspendLayout();
			base.SuspendLayout();
			this.b.Controls.Add(this.e);
			this.b.Controls.Add(this.c);
			this.b.Controls.Add(this.d);
			this.b.Dock = DockStyle.Fill;
			this.b.Location = new Point(0, 0);
			this.b.Name = "panel1";
			this.b.Size = new Size(313, 138);
			this.b.TabIndex = 0;
			this.e.Location = new Point(209, 88);
			this.e.Name = "btnCancel";
			this.e.Size = new Size(75, 23);
			this.e.TabIndex = 2;
			this.e.Text = "取消";
			this.e.UseVisualStyleBackColor = true;
			this.e.Click += this.b;
			this.c.Location = new Point(116, 88);
			this.c.Name = "btnUpgrade";
			this.c.Size = new Size(75, 23);
			this.c.TabIndex = 1;
			this.c.Text = "升级";
			this.c.UseVisualStyleBackColor = true;
			this.c.Click += this.a;
			this.d.AutoSize = true;
			this.d.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
			this.d.Location = new Point(23, 19);
			this.d.Name = "label1";
			this.d.Size = new Size(223, 48);
			this.d.TabIndex = 0;
			this.d.Text = "检测到新版本，是否进行升级?\r\n\r\n6.05.25=>6.05.26";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(313, 138);
			base.Controls.Add(this.b);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormUpgrade";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Agiso旺旺发送助手升级";
			this.b.ResumeLayout(false);
			this.b.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x0400017F RID: 383
		private Panel b;

		// Token: 0x04000180 RID: 384
		private Button c;

		// Token: 0x04000181 RID: 385
		private Label d;

		// Token: 0x04000182 RID: 386
		private Button e;
	}
}
