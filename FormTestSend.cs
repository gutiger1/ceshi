using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Agiso;
using Agiso.Exceptions;
using Agiso.Object;
using Agiso.Utils;

namespace AliwwClient
{
	// Token: 0x0200003C RID: 60
	public partial class FormTestSend : BaseForm
	{
		// Token: 0x0600017E RID: 382 RVA: 0x00021564 File Offset: 0x0001F764
		public FormTestSend(string sendUserNick = null)
		{
			this.a = sendUserNick;
			this.a();
			if (AppConfig.AllowAutoLogin)
			{
				base.StartPosition = FormStartPosition.Manual;
				base.Location = new Point(Screen.PrimaryScreen.Bounds.Width - base.Width, Screen.PrimaryScreen.Bounds.Height - base.Height - 40);
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000215EC File Offset: 0x0001F7EC
		private void a(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.k.PerformClick();
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00021610 File Offset: 0x0001F810
		private void b(object sender, EventArgs e)
		{
			int num = this.b;
			this.b = num + 1;
			if (num > this.c)
			{
				MessageBox.Show("测试发送太频繁了！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				string text = this.j.Text.Trim();
				string text2 = this.g.Text.Trim();
				string text3 = this.i.Text.Trim();
				string masterNick = Util.GetMasterNick(text);
				int num2 = 0;
				for (;;)
				{
					try
					{
						try
						{
							if (AppConfig.AllowAutoLogin)
							{
								AppConfig.TestSendErrorMsg = "";
								AppConfig.AliwwMsgQueueFirst.Enqueue(new AliwwMessageInfo
								{
									MsgId = AppConfig.TestSendMsgId,
									AliwwMessageSourceType = EnumAliwwMessageSource.FromTestSend,
									BuyerNick = text2,
									MessageBody = text3,
									SellerNick = masterNick,
									CreateTime = DateTime.Now,
									CreateTimeLocal = DateTime.Now,
									EnqueueTime = DateTime.Now
								});
							}
							else if (!AppConfig.HasTestSendAliwwMessageInfo)
							{
								AppConfig.TestSendErrorMsg = "";
								AppConfig.HasTestSendAliwwMessageInfo = true;
								AppConfig.AliwwMsgQueueFirst.Enqueue(new AliwwMessageInfo
								{
									MsgId = AppConfig.TestSendMsgId,
									AliwwMessageSourceType = EnumAliwwMessageSource.FromTestSend,
									BuyerNick = text2,
									MessageBody = text3,
									SellerNick = masterNick,
									CreateTime = DateTime.Now,
									CreateTimeLocal = DateTime.Now,
									EnqueueTime = DateTime.Now
								});
								Task.Run(new Action(FormTestSend.<>c.<>9.a));
							}
						}
						catch (Exception ex)
						{
							if (ex is GetChormeJsonException && num2++ == 0)
							{
								continue;
							}
							MessageBox.Show("测试发送时失败，请尝试重启千牛。如果仍然无法解决，请将该情形反馈给旺旺客服“agiso”协助您解决！\r\n异常信息：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							LogWriter.WriteLog(ex.ToString(), 1);
						}
						break;
					}
					finally
					{
					}
				}
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00021828 File Offset: 0x0001FA28
		private void a(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.a))
			{
				if (AppConfig.UserList != null && AppConfig.UserList.Count > 0 && AppConfig.UserList[0] != null)
				{
					this.j.Text = AppConfig.UserList[0].UserNick;
				}
			}
			else
			{
				this.j.Text = this.a;
			}
			this.g.Text = AppConfig.RobotUserNick;
			this.i.Text = AppConfig.DefaultTestSendMsgBody;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000218E8 File Offset: 0x0001FAE8
		private void a()
		{
			this.e = new GroupBox();
			this.f = new Label();
			this.g = new TextBox();
			this.h = new Label();
			this.i = new TextBox();
			this.j = new TextBox();
			this.k = new Button();
			this.e.SuspendLayout();
			base.SuspendLayout();
			this.e.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.e.Controls.Add(this.f);
			this.e.Controls.Add(this.g);
			this.e.Controls.Add(this.h);
			this.e.Controls.Add(this.i);
			this.e.Controls.Add(this.j);
			this.e.Controls.Add(this.k);
			this.e.Location = new Point(12, 12);
			this.e.Name = "groupBox2";
			this.e.Size = new Size(437, 300);
			this.e.TabIndex = 7;
			this.e.TabStop = false;
			this.e.Text = "测试发送";
			this.f.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.f.AutoSize = true;
			this.f.Location = new Point(6, 240);
			this.f.Name = "label1";
			this.f.Size = new Size(77, 12);
			this.f.TabIndex = 7;
			this.f.Text = "测试发件旺旺";
			this.g.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.g.Location = new Point(89, 267);
			this.g.MaxLength = 32;
			this.g.Name = "textBox2";
			this.g.Size = new Size(247, 21);
			this.g.TabIndex = 202;
			this.h.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.h.AutoSize = true;
			this.h.Location = new Point(6, 270);
			this.h.Name = "label2";
			this.h.Size = new Size(77, 12);
			this.h.TabIndex = 8;
			this.h.Text = "测试收件旺旺";
			this.i.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.i.Location = new Point(6, 20);
			this.i.MaxLength = 8000;
			this.i.Multiline = true;
			this.i.Name = "textBox3";
			this.i.ScrollBars = ScrollBars.Vertical;
			this.i.Size = new Size(421, 211);
			this.i.TabIndex = 203;
			this.j.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.j.Location = new Point(89, 237);
			this.j.MaxLength = 32;
			this.j.Name = "textBox1";
			this.j.Size = new Size(247, 21);
			this.j.TabIndex = 201;
			this.j.KeyDown += this.a;
			this.k.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.k.Location = new Point(342, 237);
			this.k.Name = "button1";
			this.k.Size = new Size(85, 51);
			this.k.TabIndex = 204;
			this.k.Text = "测试发送(&T)";
			this.k.UseVisualStyleBackColor = true;
			this.k.Click += this.b;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(461, 324);
			base.Controls.Add(this.e);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormTestSend";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "测试发送";
			base.Load += this.a;
			this.e.ResumeLayout(false);
			this.e.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000195 RID: 405
		private string a;

		// Token: 0x04000196 RID: 406
		private int b = 0;

		// Token: 0x04000197 RID: 407
		private int c = 20;

		// Token: 0x04000199 RID: 409
		private GroupBox e;

		// Token: 0x0400019A RID: 410
		private Label f;

		// Token: 0x0400019B RID: 411
		private TextBox g;

		// Token: 0x0400019C RID: 412
		private Label h;

		// Token: 0x0400019D RID: 413
		private TextBox i;

		// Token: 0x0400019E RID: 414
		private TextBox j;

		// Token: 0x0400019F RID: 415
		private Button k;
	}
}
