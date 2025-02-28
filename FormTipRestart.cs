using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Agiso.Handler;
using AliwwClient.Properties;

namespace AliwwClient
{
	// Token: 0x02000037 RID: 55
	public partial class FormTipRestart : Form
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000029AB File Offset: 0x00000BAB
		// (set) Token: 0x0600015B RID: 347 RVA: 0x000029B2 File Offset: 0x00000BB2
		public static bool HasReplyInstanceShow { get; set; }

		// Token: 0x0600015C RID: 348 RVA: 0x000029BA File Offset: 0x00000BBA
		private FormTipRestart()
		{
			this.a();
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0001FA44 File Offset: 0x0001DC44
		public static FormTipRestart SendInstance
		{
			get
			{
				if (FormTipRestart.b == null || FormTipRestart.b.IsDisposed)
				{
					FormTipRestart.b = new FormTipRestart();
				}
				return FormTipRestart.b;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0001FA78 File Offset: 0x0001DC78
		public static FormTipRestart ReplyInstance
		{
			get
			{
				if (FormTipRestart.c == null || FormTipRestart.c.IsDisposed)
				{
					FormTipRestart.c = new FormTipRestart();
					FormTipRestart.c.e.Text = "智能答复修复完成。需要重启千牛，否则智能答复无法正确匹配。\r\n\r\n是否现在关闭所有千牛，并手动重启千牛？";
				}
				return FormTipRestart.c;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00002708 File Offset: 0x00000908
		private void b(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000029D0 File Offset: 0x00000BD0
		private void a(object sender, EventArgs e)
		{
			Win32Extend.KillProcessByNameWithCmd("AliWorkbench");
			Win32Extend.KillProcessByNameWithCmd("AliApp");
			Win32Extend.KillProcessByNameWithCmd("AliRender");
			base.Close();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0001FAF4 File Offset: 0x0001DCF4
		private void a()
		{
			this.e = new Label();
			this.f = new Button();
			this.g = new Button();
			base.SuspendLayout();
			this.e.Location = new Point(12, 29);
			this.e.MaximumSize = new Size(400, 50);
			this.e.Name = "label1";
			this.e.Size = new Size(358, 42);
			this.e.TabIndex = 0;
			this.e.Text = "通信修复已完成。需要重启千牛，否则消息可能发送失败。\r\n\r\n是否现在关闭所有千牛，并手动重启千牛？";
			this.f.Location = new Point(64, 90);
			this.f.Name = "btnOk";
			this.f.Size = new Size(150, 27);
			this.f.TabIndex = 1;
			this.f.Text = "一键关闭所有千牛(&Y)";
			this.f.UseVisualStyleBackColor = true;
			this.f.Click += this.a;
			this.g.Location = new Point(220, 90);
			this.g.Name = "btnCancel";
			this.g.Size = new Size(150, 27);
			this.g.TabIndex = 2;
			this.g.Text = "稍后手动重启千牛(&N)";
			this.g.UseVisualStyleBackColor = true;
			this.g.Click += this.b;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(382, 129);
			base.Controls.Add(this.g);
			base.Controls.Add(this.f);
			base.Controls.Add(this.e);
			base.Icon = Resources.Icon1;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormTipRestart";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "重启千牛";
			base.ResumeLayout(false);
		}

		// Token: 0x04000170 RID: 368
		[CompilerGenerated]
		private static bool a;

		// Token: 0x04000171 RID: 369
		private static FormTipRestart b;

		// Token: 0x04000172 RID: 370
		private static FormTipRestart c;

		// Token: 0x04000174 RID: 372
		private Label e;

		// Token: 0x04000175 RID: 373
		private Button f;

		// Token: 0x04000176 RID: 374
		private Button g;
	}
}
