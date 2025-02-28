using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Agiso;
using Agiso.DBAccess;
using Agiso.DbManager;
using Agiso.Object;
using Agiso.Utils;
using AliwwClient.Properties;

namespace AliwwClient
{
	// Token: 0x0200003B RID: 59
	public partial class FormLogAutoReply : BaseForm
	{
		// Token: 0x06000173 RID: 371 RVA: 0x0002065C File Offset: 0x0001E85C
		public FormLogAutoReply()
		{
			this.a();
			if (AppConfig.AllowAutoLogin)
			{
				base.StartPosition = FormStartPosition.Manual;
				base.Location = new Point(Screen.PrimaryScreen.Bounds.Width - base.Width, Screen.PrimaryScreen.Bounds.Height - base.Height - 40);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000206CC File Offset: 0x0001E8CC
		public static FormLogAutoReply FormLogAutoReplyInstance
		{
			get
			{
				if (FormLogAutoReply.a == null || FormLogAutoReply.a.IsDisposed)
				{
					FormLogAutoReply.a = new FormLogAutoReply();
				}
				return FormLogAutoReply.a;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00002AAA File Offset: 0x00000CAA
		private void c(object sender, EventArgs e)
		{
			this.c();
			this.b();
			this.b(null, null);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00020700 File Offset: 0x0001E900
		private void c()
		{
			this.g.Items.Clear();
			this.g.Items.Add("【全部】");
			foreach (AldsAccountInfo aldsAccountInfo in AppConfig.UserList)
			{
				if (aldsAccountInfo.EnableAutoReply)
				{
					this.g.Items.Add(aldsAccountInfo.UserNick);
				}
			}
			this.g.SelectedIndex = 0;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0002079C File Offset: 0x0001E99C
		private void b(object sender, EventArgs e)
		{
			string text = this.g.SelectedItem.ToString();
			DataTable maxTimeTable = LogAutoReplyManager.GetMaxTimeTable(this.i.Value, text);
			this.a(maxTimeTable);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000207D4 File Offset: 0x0001E9D4
		private void a(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			this.d.Clear();
			if (e.IsSelected)
			{
				string text = e.Item.SubItems[0].Text;
				string text2 = e.Item.SubItems[1].Text;
				List<LogAutoReply> list = LogAutoReplyManager.GetList(this.i.Value, text, text2);
				if (list != null && list.Count != 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (LogAutoReply logAutoReply in list)
					{
						string text3;
						switch (logAutoReply.FromType)
						{
						case 1:
							text3 = "窗口";
							break;
						case 2:
							text3 = "chat插件";
							break;
						case 3:
							text3 = "chat插件-新";
							break;
						case 4:
							text3 = "alds插件";
							break;
						default:
							text3 = "";
							break;
						}
						stringBuilder.AppendLine("匹配关键词：" + logAutoReply.KeyWord);
						stringBuilder.AppendLine();
						stringBuilder.AppendLine("匹配答复语：" + logAutoReply.ReplyWord);
						stringBuilder.AppendLine();
						if (!string.IsNullOrEmpty(logAutoReply.DutyManualNick))
						{
							stringBuilder.AppendLine("转接客服：" + logAutoReply.DutyManualNick + ((logAutoReply.IsTransferFail > 0) ? "(失败)" : ""));
							stringBuilder.AppendLine();
							if (!string.IsNullOrEmpty(logAutoReply.TransferFailMsg))
							{
								stringBuilder.AppendLine("转接失败原因：" + logAutoReply.TransferFailMsg);
								stringBuilder.AppendLine();
							}
						}
						stringBuilder.AppendLine(string.Format("卖家昵称：{0} (答复时间：{1:yyyy-MM-dd HH:mm:ss})", logAutoReply.SellerNick, logAutoReply.CreateTime));
						stringBuilder.AppendLine();
						stringBuilder.AppendLine(string.Concat(new string[] { "买家昵称：", logAutoReply.SenderNick, " (咨询时间：", logAutoReply.ConsultTime, ") (", text3, ")" }));
						stringBuilder.AppendLine();
						stringBuilder.AppendLine("咨询语：" + logAutoReply.ConsultWord);
						stringBuilder.AppendLine();
						stringBuilder.AppendLine("################################################################");
						stringBuilder.AppendLine();
					}
					this.d.Text = stringBuilder.ToString();
				}
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00020A7C File Offset: 0x0001EC7C
		private void b()
		{
			this.j.Columns.Add("回复号", 140, HorizontalAlignment.Left);
			this.j.Columns.Add("买家昵称", 140, HorizontalAlignment.Left);
			this.j.Columns.Add("最后答复时间", 95, HorizontalAlignment.Center);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00020AE0 File Offset: 0x0001ECE0
		private void a(DataTable A_0)
		{
			this.j.Items.Clear();
			if (A_0 != null && A_0.Rows.Count != 0)
			{
				for (int i = 0; i < A_0.Rows.Count; i++)
				{
					DataRow dataRow = A_0.Rows[i];
					ListViewItem listViewItem = new ListViewItem(new string[]
					{
						dataRow["SellerNick"].ToString(),
						dataRow["SenderNick"].ToString(),
						DbUtil.TrimDateNull(dataRow["MaxCreateTime"]).ToString("HH:mm:ss")
					});
					this.j.Items.Add(listViewItem);
				}
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00020BAC File Offset: 0x0001EDAC
		private void a(object sender, EventArgs e)
		{
			if (this.j.SelectedItems != null && this.j.SelectedItems.Count > 0)
			{
				foreach (object obj in this.j.SelectedItems)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					string text = listViewItem.SubItems[0].Text;
					string masterNick = Util.GetMasterNick(text);
					string text2 = listViewItem.SubItems[1].Text;
					string text3 = this.l.Text;
					if (string.IsNullOrEmpty(text3))
					{
						AldsAccountInfo accountInfo = AppConfig.GetAccountInfo(masterNick);
						if (accountInfo == null)
						{
							continue;
						}
						text3 = (string.IsNullOrEmpty(accountInfo.ManualNick) ? accountInfo.UserNick : AppConfig.GetDutyManualNick(accountInfo));
						if (!text3.Contains(":") && !text3.StartsWith(accountInfo.UserNick))
						{
							text3 = accountInfo.UserNick + ":" + text3;
						}
					}
					if (!string.IsNullOrEmpty(text3))
					{
						text3 = "cntaobao" + text3;
					}
					text2 = "cntaobao" + text2;
					AppConfig.ProcessStartQn(text3, text2, "", "cntaobao");
				}
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00020D64 File Offset: 0x0001EF64
		private void a()
		{
			this.b = new Container();
			this.c = new SplitContainer();
			this.j = new ListView();
			this.n = new ContextMenuStrip(this.b);
			this.h = new ToolStripMenuItem();
			this.k = new Panel();
			this.l = new TextBox();
			this.m = new Label();
			this.e = new Panel();
			this.i = new DateTimePicker();
			this.g = new ComboBox();
			this.f = new Button();
			this.d = new TextBox();
			((ISupportInitialize)this.c).BeginInit();
			this.c.Panel1.SuspendLayout();
			this.c.Panel2.SuspendLayout();
			this.c.SuspendLayout();
			this.n.SuspendLayout();
			this.k.SuspendLayout();
			this.e.SuspendLayout();
			base.SuspendLayout();
			this.c.Dock = DockStyle.Fill;
			this.c.Location = new Point(0, 0);
			this.c.Name = "splitContainer1";
			this.c.Panel1.Controls.Add(this.j);
			this.c.Panel1.Controls.Add(this.k);
			this.c.Panel1.Controls.Add(this.e);
			this.c.Panel2.Controls.Add(this.d);
			this.c.Size = new Size(828, 521);
			this.c.SplitterDistance = 379;
			this.c.TabIndex = 0;
			this.j.ContextMenuStrip = this.n;
			this.j.Dock = DockStyle.Fill;
			this.j.FullRowSelect = true;
			this.j.HideSelection = false;
			this.j.Location = new Point(0, 21);
			this.j.MultiSelect = false;
			this.j.Name = "LvLogAutoReply";
			this.j.Size = new Size(379, 477);
			this.j.TabIndex = 8;
			this.j.UseCompatibleStateImageBehavior = false;
			this.j.View = View.Details;
			this.j.ItemSelectionChanged += this.a;
			this.n.Items.AddRange(new ToolStripItem[] { this.h });
			this.n.Name = "contextMenuStrip1";
			this.n.Size = new Size(213, 26);
			this.h.Name = "SendToHimToolStripMenuItem";
			this.h.Size = new Size(212, 22);
			this.h.Text = "用默认联系人找他聊天(&C)";
			this.h.Click += this.a;
			this.k.Controls.Add(this.l);
			this.k.Controls.Add(this.m);
			this.k.Dock = DockStyle.Bottom;
			this.k.Location = new Point(0, 498);
			this.k.Name = "panel2";
			this.k.Size = new Size(379, 23);
			this.k.TabIndex = 7;
			this.l.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.l.Location = new Point(77, 1);
			this.l.Name = "TxtDefaultContactor";
			this.l.Size = new Size(302, 21);
			this.l.TabIndex = 10;
			this.m.AutoSize = true;
			this.m.Location = new Point(3, 3);
			this.m.Name = "label1";
			this.m.Size = new Size(77, 12);
			this.m.TabIndex = 7;
			this.m.Text = "默认联系人：";
			this.e.Controls.Add(this.i);
			this.e.Controls.Add(this.g);
			this.e.Controls.Add(this.f);
			this.e.Dock = DockStyle.Top;
			this.e.Location = new Point(0, 0);
			this.e.Name = "panel1";
			this.e.Size = new Size(379, 21);
			this.e.TabIndex = 3;
			this.i.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.i.CustomFormat = "yyyy-MM-dd";
			this.i.Format = DateTimePickerFormat.Custom;
			this.i.Location = new Point(238, 0);
			this.i.Name = "DtpLogAutoReply";
			this.i.Size = new Size(80, 21);
			this.i.TabIndex = 4;
			this.g.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.g.DropDownStyle = ComboBoxStyle.DropDownList;
			this.g.FormattingEnabled = true;
			this.g.Items.AddRange(new object[] { "fadf", "fdaf", "fda", "fdsa" });
			this.g.Location = new Point(0, 0);
			this.g.Name = "CbLogAutoReplySellerF";
			this.g.Size = new Size(232, 20);
			this.g.TabIndex = 3;
			this.f.Dock = DockStyle.Right;
			this.f.Location = new Point(324, 0);
			this.f.Name = "BtnLogAutoReplySearch";
			this.f.Size = new Size(55, 21);
			this.f.TabIndex = 2;
			this.f.Text = "查询(&F)";
			this.f.UseVisualStyleBackColor = true;
			this.f.Click += this.b;
			this.d.Dock = DockStyle.Fill;
			this.d.Location = new Point(0, 0);
			this.d.Multiline = true;
			this.d.Name = "TxtLogAutoReply";
			this.d.ReadOnly = true;
			this.d.ScrollBars = ScrollBars.Vertical;
			this.d.Size = new Size(445, 521);
			this.d.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(828, 521);
			base.Controls.Add(this.c);
			base.Icon = Resources.Icon1;
			base.Name = "FormLogAutoReply";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "智能答复记录";
			base.Load += this.c;
			this.c.Panel1.ResumeLayout(false);
			this.c.Panel2.ResumeLayout(false);
			this.c.Panel2.PerformLayout();
			((ISupportInitialize)this.c).EndInit();
			this.c.ResumeLayout(false);
			this.n.ResumeLayout(false);
			this.k.ResumeLayout(false);
			this.k.PerformLayout();
			this.e.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000187 RID: 391
		private static FormLogAutoReply a;

		// Token: 0x04000189 RID: 393
		private SplitContainer c;

		// Token: 0x0400018A RID: 394
		private TextBox d;

		// Token: 0x0400018B RID: 395
		private Panel e;

		// Token: 0x0400018C RID: 396
		private Button f;

		// Token: 0x0400018D RID: 397
		private ComboBox g;

		// Token: 0x0400018E RID: 398
		private ToolStripMenuItem h;

		// Token: 0x0400018F RID: 399
		private DateTimePicker i;

		// Token: 0x04000190 RID: 400
		private ListView j;

		// Token: 0x04000191 RID: 401
		private Panel k;

		// Token: 0x04000192 RID: 402
		private TextBox l;

		// Token: 0x04000193 RID: 403
		private Label m;

		// Token: 0x04000194 RID: 404
		private ContextMenuStrip n;
	}
}
