using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using Agiso;
using Agiso.DbManager;
using Agiso.Object;
using Agiso.Utils;
using Agiso.WwWebSocket.Model;
using AliwwClient.Enums;

namespace AliwwClient
{
	// Token: 0x02000024 RID: 36
	public partial class FormAldsAccountEdit : BaseForm
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x000027A1 File Offset: 0x000009A1
		public FormAldsAccountEdit(AldsAccountInfo account)
		{
			this.a = account;
			this.a();
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00013FB8 File Offset: 0x000121B8
		public string SellerNick
		{
			get
			{
				if (this.b == null && this.a != null)
				{
					this.b = Util.GetMasterNick(this.a.UserNick);
				}
				return this.b;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000027BE File Offset: 0x000009BE
		private void d()
		{
			this.InitCustomerServiceMould(null);
			this.InitCustomerServiceWorksheet();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00013FFC File Offset: 0x000121FC
		public void InitCustomerServiceMould(string defaultTitle = null)
		{
			FormAldsAccountEdit.a a = new FormAldsAccountEdit.a();
			a.a = defaultTitle;
			this.ap.DataSource = null;
			List<CustomerServiceMould> list = CustomerServiceMouldManager.Get(this.SellerNick);
			if (list != null)
			{
				this.ap.DisplayMember = "Title";
				this.ap.ValueMember = "IdNo";
				this.ap.DataSource = list;
			}
			if (!string.IsNullOrEmpty(a.a))
			{
				long num = list.Where(new Func<CustomerServiceMould, bool>(a.b)).Select(new Func<CustomerServiceMould, long>(FormAldsAccountEdit.<>c.<>9.a)).FirstOrDefault<long>();
				this.ap.SelectedValue = num;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000140C4 File Offset: 0x000122C4
		public string GetWorkTimeDesc(DetailWorksheetInfo detailWorksheet, DayOfWeek weekDay)
		{
			string text = "     ";
			string text2;
			if (detailWorksheet == null)
			{
				text2 = "";
			}
			else if (!detailWorksheet.ContainsKey(weekDay))
			{
				text2 = "";
			}
			else
			{
				List<WorkTimeInfo> list = detailWorksheet[weekDay];
				if (list == null || list.Count <= 0)
				{
					text2 = "";
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (WorkTimeInfo workTimeInfo in list)
					{
						string text3;
						if (workTimeInfo.HourFrom == 0 && workTimeInfo.MinuteFrom == 0)
						{
							text3 = text;
						}
						else
						{
							text3 = workTimeInfo.HourFrom.ToString("D2") + ":" + workTimeInfo.MinuteFrom.ToString("D2");
						}
						string text4;
						if (workTimeInfo.HourTo == 0 && workTimeInfo.MinuteTo == 0)
						{
							text4 = text;
						}
						else
						{
							text4 = workTimeInfo.HourTo.ToString("D2") + ":" + workTimeInfo.MinuteTo.ToString("D2");
						}
						if (text.Equals(text3) && !text.Equals(text4))
						{
							text3 = "00:00";
						}
						stringBuilder.Append("," + text3 + "-" + text4);
					}
					if (stringBuilder.ToString().Length > 0)
					{
						text2 = stringBuilder.ToString().Substring(1);
					}
					else
					{
						text2 = "";
					}
				}
			}
			return text2;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00014294 File Offset: 0x00012494
		public void InitCustomerServiceWorksheet()
		{
			this.o.Items.Clear();
			if (this.ap.SelectedIndex >= 0)
			{
				List<CustomerServiceWorksheet> list = CustomerServiceWorksheetManager.GetList(Util.ToLong(this.ap.SelectedValue), this.SellerNick);
				if (list != null)
				{
					foreach (CustomerServiceWorksheet customerServiceWorksheet in list)
					{
						string workTimeDesc = this.GetWorkTimeDesc(customerServiceWorksheet.DetailWorksheet, DayOfWeek.Monday);
						string workTimeDesc2 = this.GetWorkTimeDesc(customerServiceWorksheet.DetailWorksheet, DayOfWeek.Tuesday);
						string workTimeDesc3 = this.GetWorkTimeDesc(customerServiceWorksheet.DetailWorksheet, DayOfWeek.Wednesday);
						string workTimeDesc4 = this.GetWorkTimeDesc(customerServiceWorksheet.DetailWorksheet, DayOfWeek.Thursday);
						string workTimeDesc5 = this.GetWorkTimeDesc(customerServiceWorksheet.DetailWorksheet, DayOfWeek.Friday);
						string workTimeDesc6 = this.GetWorkTimeDesc(customerServiceWorksheet.DetailWorksheet, DayOfWeek.Saturday);
						string workTimeDesc7 = this.GetWorkTimeDesc(customerServiceWorksheet.DetailWorksheet, DayOfWeek.Sunday);
						ListViewItem listViewItem = new ListViewItem(new string[] { customerServiceWorksheet.UserNick, workTimeDesc, workTimeDesc2, workTimeDesc3, workTimeDesc4, workTimeDesc5, workTimeDesc6, workTimeDesc7 });
						listViewItem.Tag = customerServiceWorksheet;
						this.o.Items.Add(listViewItem);
					}
				}
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00002792 File Offset: 0x00000992
		private void p(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000143F4 File Offset: 0x000125F4
		private void o(object sender, EventArgs e)
		{
			if (this.an.Checked && this.a.VersionNo < 3)
			{
				this.a.EnableAutoReply = false;
				MessageBox.Show("是否未登录成功？建议尝试退出助手重新打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				if (this.u.Checked)
				{
					int num = Util.ToInt(this.ap.SelectedValue);
					if (num <= 0 || this.o.Items.Count <= 0)
					{
						MessageBox.Show("新版客服，客服信息不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						return;
					}
					if (!this.ValidateManualNick(this.ai.Text.Trim()))
					{
						return;
					}
					this.a.TransferNickIfNotDuty = this.ai.Text.Trim();
					this.a.DefaultMouldId = new long?((long)num);
				}
				else
				{
					string text = this.h.Text.Replace("：", ":").Trim();
					StringBuilder stringBuilder = new StringBuilder();
					string[] array = text.Split(new char[] { '\n' });
					foreach (string text2 in array)
					{
						string text3 = text2.Trim();
						if (!string.IsNullOrEmpty(text3))
						{
							if (!this.ValidateManualNick(text3))
							{
								return;
							}
							stringBuilder.AppendLine(text3);
						}
					}
					text = stringBuilder.ToString();
					this.a.ManualNick = text;
				}
				this.a.AutoReplyOnOff = this.an.Checked;
				this.a.AutoSendOnOff = this.i.Checked;
				this.a.IsCustomerServiceNewVersion = this.u.Checked;
				this.a.NotDutyNickReplyMsg = this.al.Text;
				base.DialogResult = DialogResult.OK;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000145EC File Offset: 0x000127EC
		private void n(object sender, EventArgs e)
		{
			this.d();
			this.m(null, null);
			this.an.Checked = this.a.AutoReplyOnOff;
			this.i.Checked = this.a.AutoSendOnOff;
			this.h.Text = this.a.ManualNick;
			this.g.Text = string.Format("正在设置“{0}”", this.a.UserNick);
			this.ap.SelectedValue = ((this.a.DefaultMouldId != null) ? this.a.DefaultMouldId.Value : 0L);
			this.ap.SelectedValue = ((this.a.DefaultMouldId != null) ? this.a.DefaultMouldId.Value : 0L);
			this.ai.Text = this.a.TransferNickIfNotDuty;
			this.al.Text = this.a.NotDutyNickReplyMsg;
			if (AppConfig.UserDict[this.a.UserNick].IsCustomerServiceNewVersion)
			{
				this.u.Checked = true;
			}
			else
			{
				this.r.Checked = true;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000027CD File Offset: 0x000009CD
		private void m(object sender, EventArgs e)
		{
			this.j.Text = "人工客服：";
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00014754 File Offset: 0x00012954
		private void l(object sender, EventArgs e)
		{
			if (this.ap.SelectedIndex < 0)
			{
				MessageBox.Show("请先添加模板，在添加客服", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				new FormCustomerServiceWorksheetNew(this, this.a, Util.ToLong(this.ap.SelectedValue), null).ShowDialog();
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000147AC File Offset: 0x000129AC
		private void k(object sender, EventArgs e)
		{
			if (this.o.SelectedItems.Count <= 0)
			{
				MessageBox.Show("请先选择要修改的客服", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				new FormCustomerServiceWorksheetNew(this, this.a, Util.ToLong(this.ap.SelectedValue), (CustomerServiceWorksheet)this.o.SelectedItems[0].Tag).ShowDialog();
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00014828 File Offset: 0x00012A28
		private void j(object sender, EventArgs e)
		{
			if (this.o.SelectedItems.Count > 0 && MessageBox.Show("确定要删除客服信息？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
			{
				CustomerServiceWorksheet customerServiceWorksheet = (CustomerServiceWorksheet)this.o.SelectedItems[0].Tag;
				CustomerServiceWorksheetManager.Delete(customerServiceWorksheet.IdNo);
				this.InitCustomerServiceWorksheet();
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00014894 File Offset: 0x00012A94
		private void i(object sender, EventArgs e)
		{
			if (this.o.SelectedItems.Count <= 0)
			{
				MessageBox.Show("请选择要复制的客服信息？", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				CustomerServiceWorksheet customerServiceWorksheet = (CustomerServiceWorksheet)this.o.SelectedItems[0].Tag;
				customerServiceWorksheet.IdNo = 0L;
				customerServiceWorksheet.UserNick = Util.GetMasterNick(this.a.UserNick) + ":";
				new FormCustomerServiceWorksheetNew(this, this.a, Util.ToLong(this.ap.SelectedValue), customerServiceWorksheet).ShowDialog();
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000027E0 File Offset: 0x000009E0
		private void h(object sender, EventArgs e)
		{
			new FormCustomerServiceMould(this, ActionType.Add, null).ShowDialog();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00014940 File Offset: 0x00012B40
		private void g(object sender, EventArgs e)
		{
			if (this.ap.SelectedIndex < 0)
			{
				MessageBox.Show("请先选择要修改的模板", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				new FormCustomerServiceMould(this, ActionType.Edit, this.ap.SelectedItem as CustomerServiceMould).ShowDialog();
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00014994 File Offset: 0x00012B94
		private void f(object sender, EventArgs e)
		{
			if (this.ap.SelectedIndex < 0)
			{
				MessageBox.Show("请先选择要删除的模板", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				DialogResult dialogResult = MessageBox.Show("删除模板时，模板下面的客服信息也会被删除。是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
				if (dialogResult == DialogResult.OK)
				{
					CustomerServiceMould customerServiceMould = this.ap.SelectedItem as CustomerServiceMould;
					CustomerServiceMouldManager.Delete(customerServiceMould.IdNo);
					CustomerServiceWorksheetManager.DeleteByMouldId(customerServiceMould.IdNo);
					this.InitCustomerServiceMould(null);
					this.InitCustomerServiceWorksheet();
				}
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00014A1C File Offset: 0x00012C1C
		private void e(object sender, EventArgs e)
		{
			if (this.r.Checked)
			{
				this.p.Visible = true;
				this.v.Visible = false;
				this.ag.Visible = false;
				double num = this.c();
				base.Width = (int)Math.Ceiling(590.0 * num);
				this.af.Width = (int)Math.Ceiling(500.0 * num);
				this.n.Width = (int)Math.Ceiling(450.0 * num);
				this.b();
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00014ABC File Offset: 0x00012CBC
		private void d(object sender, EventArgs e)
		{
			if (this.u.Checked)
			{
				this.p.Visible = false;
				this.v.Visible = true;
				this.ag.Visible = true;
				double num = this.c();
				base.Width = (int)Math.Ceiling(956.0 * num);
				this.af.Width = (int)Math.Ceiling(500.0 * num);
				this.o.Width = (int)Math.Ceiling(756.0 * num);
				this.s.Location = new Point(this.o.Location.X + this.o.Width + 5, this.s.Location.Y);
				this.n.Width = (int)Math.Ceiling(826.0 * num);
				this.b();
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00014BBC File Offset: 0x00012DBC
		private double c()
		{
			float dpiX = AppConfig.DpiX;
			float num = dpiX;
			double num2;
			if (num != 96f)
			{
				if (num != 120f)
				{
					if (num != 144f)
					{
						if (num != 168f)
						{
							if (num != 192f)
							{
								num2 = 1.0;
							}
							else
							{
								num2 = 2.0;
							}
						}
						else
						{
							num2 = 1.75;
						}
					}
					else
					{
						num2 = 1.5;
					}
				}
				else
				{
					num2 = 1.25;
				}
			}
			else
			{
				num2 = 1.0;
			}
			return num2;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00014C40 File Offset: 0x00012E40
		private void b()
		{
			int num;
			if (base.Height > global::k.a().Height)
			{
				num = global::k.a().Location.Y - (base.Height - global::k.a().Height) / 2;
			}
			else
			{
				num = global::k.a().Location.Y + (global::k.a().Height - base.Height) / 2;
			}
			base.Location = new Point(global::k.a().Location.X, num);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000027F0 File Offset: 0x000009F0
		private void c(object sender, EventArgs e)
		{
			this.InitCustomerServiceWorksheet();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000027F8 File Offset: 0x000009F8
		private void b(object sender, EventArgs e)
		{
			this.k(null, null);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00014CD4 File Offset: 0x00012ED4
		public bool ValidateManualNick(string manualNick)
		{
			bool flag;
			if (string.IsNullOrEmpty(manualNick))
			{
				flag = true;
			}
			else if (manualNick == this.a.UserNick)
			{
				MessageBox.Show("代理账号：" + manualNick + "填写错误，无法转接给自己。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				flag = false;
			}
			else
			{
				string masterNick = Util.GetMasterNick(this.a.UserNick);
				if ((manualNick != masterNick && !manualNick.StartsWith(masterNick + ":")) || manualNick.Equals(masterNick + ":"))
				{
					MessageBox.Show(string.Concat(new string[] { "代理账号：", manualNick, "填写错误，请填写本店铺的主号或子号。比如：“", masterNick, "”或者“", masterNick, ":agiso”等。" }), "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00014DC4 File Offset: 0x00012FC4
		private void a(object sender, EventArgs e)
		{
			CustomerServiceMould customerServiceMould = this.ap.SelectedItem as CustomerServiceMould;
			if (customerServiceMould == null)
			{
				MessageBox.Show("请选择要复制的模板", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				new FormCustomerServiceMould(this, ActionType.Copy, customerServiceMould).ShowDialog();
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00014E40 File Offset: 0x00013040
		private void a()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormAldsAccountEdit));
			this.g = new GroupBox();
			this.an = new CheckBox();
			this.i = new CheckBox();
			this.ao = new Label();
			this.k = new Label();
			this.al = new TextBox();
			this.am = new Label();
			this.af = new FlowLayoutPanel();
			this.ag = new Panel();
			this.aj = new Label();
			this.ai = new TextBox();
			this.ah = new Label();
			this.u = new RadioButton();
			this.r = new RadioButton();
			this.n = new FlowLayoutPanel();
			this.p = new Panel();
			this.h = new TextBox();
			this.m = new Label();
			this.v = new Panel();
			this.at = new Button();
			this.o = new ListView();
			this.x = new ColumnHeader();
			this.y = new ColumnHeader();
			this.z = new ColumnHeader();
			this.aa = new ColumnHeader();
			this.ab = new ColumnHeader();
			this.ac = new ColumnHeader();
			this.ad = new ColumnHeader();
			this.ae = new ColumnHeader();
			this.s = new Panel();
			this.ak = new Button();
			this.w = new Button();
			this.q = new Button();
			this.t = new Button();
			this.ap = new ComboBox();
			this.aq = new Button();
			this.ar = new Button();
			this.@as = new Button();
			this.j = new Label();
			this.l = new Label();
			this.d = new Panel();
			this.e = new Button();
			this.f = new Button();
			this.g.SuspendLayout();
			this.af.SuspendLayout();
			this.ag.SuspendLayout();
			this.n.SuspendLayout();
			this.p.SuspendLayout();
			this.v.SuspendLayout();
			this.s.SuspendLayout();
			this.d.SuspendLayout();
			base.SuspendLayout();
			this.g.Controls.Add(this.an);
			this.g.Controls.Add(this.i);
			this.g.Controls.Add(this.ao);
			this.g.Controls.Add(this.k);
			this.g.Controls.Add(this.al);
			this.g.Controls.Add(this.am);
			this.g.Controls.Add(this.af);
			this.g.Controls.Add(this.u);
			this.g.Controls.Add(this.r);
			this.g.Controls.Add(this.n);
			this.g.Controls.Add(this.j);
			this.g.Controls.Add(this.l);
			this.g.Dock = DockStyle.Top;
			this.g.Location = new Point(0, 0);
			this.g.Name = "GbUserNick";
			this.g.Size = new Size(1374, 382);
			this.g.TabIndex = 10;
			this.g.TabStop = false;
			this.g.Text = "正在设置";
			this.an.AutoSize = true;
			this.an.Location = new Point(103, 55);
			this.an.Name = "CbAutoReplyOnOff";
			this.an.Size = new Size(180, 16);
			this.an.TabIndex = 202;
			this.an.Text = "勾选开启后帐号开启智能答复";
			this.an.UseVisualStyleBackColor = true;
			this.i.AutoSize = true;
			this.i.Location = new Point(103, 32);
			this.i.Name = "CbAutoSendOnOff";
			this.i.Size = new Size(156, 16);
			this.i.TabIndex = 100;
			this.i.Text = "勾选开启后帐号参与发送";
			this.i.UseVisualStyleBackColor = true;
			this.ao.AutoSize = true;
			this.ao.Location = new Point(32, 56);
			this.ao.Name = "label3";
			this.ao.Size = new Size(65, 12);
			this.ao.TabIndex = 201;
			this.ao.Text = "智能答复：";
			this.k.AutoSize = true;
			this.k.Location = new Point(32, 33);
			this.k.Name = "label2";
			this.k.Size = new Size(65, 12);
			this.k.TabIndex = 1;
			this.k.Text = "发送开关：";
			this.al.Location = new Point(215, 302);
			this.al.Multiline = true;
			this.al.Name = "txtNotDutyNickReplyMsg";
			this.al.ScrollBars = ScrollBars.Vertical;
			this.al.Size = new Size(328, 33);
			this.al.TabIndex = 513;
			this.al.Text = "抱歉，我的主人没有告诉我人工客服是谁。暂时与我联系吧。";
			this.am.AutoSize = true;
			this.am.Location = new Point(32, 302);
			this.am.Name = "lbNotDutyNickReplyMsg";
			this.am.Size = new Size(173, 24);
			this.am.TabIndex = 512;
			this.am.Text = "转接时，没有当值客服的回复语\r\n（注：不回复则放空）：";
			this.af.Controls.Add(this.ag);
			this.af.Location = new Point(25, 70);
			this.af.Name = "flowLayoutPanel2";
			this.af.Size = new Size(848, 32);
			this.af.TabIndex = 511;
			this.ag.Controls.Add(this.aj);
			this.ag.Controls.Add(this.ai);
			this.ag.Controls.Add(this.ah);
			this.ag.Location = new Point(3, 3);
			this.ag.Name = "panelNew1";
			this.ag.Size = new Size(495, 26);
			this.ag.TabIndex = 202;
			this.aj.AutoSize = true;
			this.aj.Location = new Point(329, 7);
			this.aj.Name = "label6";
			this.aj.Size = new Size(149, 12);
			this.aj.TabIndex = 2;
			this.aj.Text = "所有人都下班时转给该帐号";
			this.ai.Location = new Point(74, 4);
			this.ai.Name = "txtTransferNickIfNotDuty";
			this.ai.Size = new Size(249, 21);
			this.ai.TabIndex = 1;
			this.ah.AutoSize = true;
			this.ah.Location = new Point(3, 7);
			this.ah.Name = "label4";
			this.ah.Size = new Size(65, 12);
			this.ah.TabIndex = 0;
			this.ah.Text = "代理帐号：";
			this.u.AutoSize = true;
			this.u.Location = new Point(34, 150);
			this.u.Name = "cbNewVersion";
			this.u.Size = new Size(47, 16);
			this.u.TabIndex = 510;
			this.u.TabStop = true;
			this.u.Text = "新版";
			this.u.UseVisualStyleBackColor = true;
			this.u.CheckedChanged += this.d;
			this.r.AutoSize = true;
			this.r.Location = new Point(34, 128);
			this.r.Name = "cbOldVersion";
			this.r.Size = new Size(47, 16);
			this.r.TabIndex = 509;
			this.r.TabStop = true;
			this.r.Text = "旧版";
			this.r.UseVisualStyleBackColor = true;
			this.r.CheckedChanged += this.e;
			this.n.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.n.Controls.Add(this.p);
			this.n.Controls.Add(this.v);
			this.n.Location = new Point(97, 102);
			this.n.Name = "flowLayoutPanel1";
			this.n.Size = new Size(1265, 193);
			this.n.TabIndex = 507;
			this.p.Controls.Add(this.h);
			this.p.Controls.Add(this.m);
			this.p.Location = new Point(3, 3);
			this.p.Name = "panelOld";
			this.p.Size = new Size(430, 185);
			this.p.TabIndex = 508;
			this.h.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.h.Location = new Point(3, 3);
			this.h.Multiline = true;
			this.h.Name = "TxtManualNick";
			this.h.ScrollBars = ScrollBars.Vertical;
			this.h.Size = new Size(248, 176);
			this.h.TabIndex = 300;
			this.m.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.m.AutoSize = true;
			this.m.Location = new Point(257, 12);
			this.m.Name = "LblManualPrompt";
			this.m.Size = new Size(173, 36);
			this.m.TabIndex = 501;
			this.m.Text = "多个号时一行一个帐号，程序会\r\n逐个转接平均转接给各个人工\r\n客服。请确保人工客服在线";
			this.v.Controls.Add(this.at);
			this.v.Controls.Add(this.o);
			this.v.Controls.Add(this.s);
			this.v.Controls.Add(this.ap);
			this.v.Controls.Add(this.aq);
			this.v.Controls.Add(this.ar);
			this.v.Controls.Add(this.@as);
			this.v.Location = new Point(439, 3);
			this.v.Name = "panelNew";
			this.v.Size = new Size(809, 185);
			this.v.TabIndex = 510;
			this.at.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.at.Location = new Point(692, 3);
			this.at.Name = "btnCopyMould";
			this.at.Size = new Size(65, 23);
			this.at.TabIndex = 510;
			this.at.Text = "复制模板";
			this.at.UseVisualStyleBackColor = true;
			this.at.Click += this.a;
			this.o.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.o.Columns.AddRange(new ColumnHeader[] { this.x, this.y, this.z, this.aa, this.ab, this.ac, this.ad, this.ae });
			this.o.FullRowSelect = true;
			this.o.HideSelection = false;
			this.o.Location = new Point(3, 27);
			this.o.MultiSelect = false;
			this.o.Name = "lvCustomerServiceWorksheet";
			this.o.Size = new Size(753, 151);
			this.o.TabIndex = 502;
			this.o.UseCompatibleStateImageBehavior = false;
			this.o.View = View.Details;
			this.o.DoubleClick += this.b;
			this.x.Text = "旺旺号";
			this.x.Width = 134;
			this.y.Text = "星期一";
			this.y.Width = 88;
			this.z.Text = "星期二";
			this.z.Width = 88;
			this.aa.Text = "星期三";
			this.aa.Width = 88;
			this.ab.Text = "星期四";
			this.ab.Width = 88;
			this.ac.Text = "星期五";
			this.ac.Width = 88;
			this.ad.Text = "星期六";
			this.ad.Width = 88;
			this.ae.Text = "星期日";
			this.ae.Width = 88;
			this.s.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.s.Controls.Add(this.ak);
			this.s.Controls.Add(this.w);
			this.s.Controls.Add(this.q);
			this.s.Controls.Add(this.t);
			this.s.Location = new Point(760, 27);
			this.s.Name = "panel3";
			this.s.Size = new Size(42, 151);
			this.s.TabIndex = 509;
			this.ak.Location = new Point(0, 76);
			this.ak.Name = "btnCopyCustomerService";
			this.ak.Size = new Size(41, 23);
			this.ak.TabIndex = 510;
			this.ak.Text = "复制";
			this.ak.UseVisualStyleBackColor = true;
			this.ak.Click += this.i;
			this.w.Location = new Point(0, 24);
			this.w.Name = "btnEditCustomerService";
			this.w.Size = new Size(41, 23);
			this.w.TabIndex = 508;
			this.w.Text = "修改";
			this.w.UseVisualStyleBackColor = true;
			this.w.Click += this.k;
			this.q.Location = new Point(0, 50);
			this.q.Name = "btnDelCustomerService";
			this.q.Size = new Size(41, 23);
			this.q.TabIndex = 509;
			this.q.Text = "删除";
			this.q.UseVisualStyleBackColor = true;
			this.q.Click += this.j;
			this.t.Location = new Point(0, 2);
			this.t.Name = "btnAddCustomerService";
			this.t.Size = new Size(41, 23);
			this.t.TabIndex = 507;
			this.t.Text = "添加";
			this.t.UseVisualStyleBackColor = true;
			this.t.Click += this.l;
			this.ap.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.ap.DropDownStyle = ComboBoxStyle.DropDownList;
			this.ap.FormattingEnabled = true;
			this.ap.Location = new Point(3, 3);
			this.ap.Name = "cbCustomerServiceMould";
			this.ap.Size = new Size(472, 20);
			this.ap.TabIndex = 503;
			this.ap.SelectedIndexChanged += this.c;
			this.aq.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.aq.Location = new Point(479, 3);
			this.aq.Name = "btnAddMould";
			this.aq.Size = new Size(65, 23);
			this.aq.TabIndex = 504;
			this.aq.Text = "添加模板";
			this.aq.UseVisualStyleBackColor = true;
			this.aq.Click += this.h;
			this.ar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.ar.Location = new Point(621, 3);
			this.ar.Name = "btnDeleteMould";
			this.ar.Size = new Size(65, 23);
			this.ar.TabIndex = 506;
			this.ar.Text = "删除模板";
			this.ar.UseVisualStyleBackColor = true;
			this.ar.Click += this.f;
			this.@as.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.@as.Location = new Point(550, 3);
			this.@as.Name = "btnEditMould";
			this.@as.Size = new Size(65, 23);
			this.@as.TabIndex = 505;
			this.@as.Text = "修改模板";
			this.@as.UseVisualStyleBackColor = true;
			this.@as.Click += this.g;
			this.j.AutoSize = true;
			this.j.Location = new Point(31, 104);
			this.j.Name = "LblManual";
			this.j.Size = new Size(65, 12);
			this.j.TabIndex = 3;
			this.j.Text = "人工客服：";
			this.l.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.l.AutoSize = true;
			this.l.Location = new Point(30, 344);
			this.l.Name = "label5";
			this.l.Size = new Size(503, 12);
			this.l.TabIndex = 8;
			this.l.Text = "转接功能仅对千牛5及以上千牛版本客服工作台模式有效窗口合并时人为干扰有导致发错人风险";
			this.l.TextAlign = ContentAlignment.MiddleLeft;
			this.d.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.d.Controls.Add(this.e);
			this.d.Controls.Add(this.f);
			this.d.Location = new Point(0, 384);
			this.d.Name = "panel1";
			this.d.Size = new Size(1374, 47);
			this.d.TabIndex = 20;
			this.e.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.e.Location = new Point(1287, 12);
			this.e.Name = "BtnCancel";
			this.e.Size = new Size(75, 23);
			this.e.TabIndex = 22;
			this.e.Text = "取消(&C)";
			this.e.UseVisualStyleBackColor = true;
			this.e.Click += this.p;
			this.f.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.f.Location = new Point(1206, 12);
			this.f.Name = "BtnSave";
			this.f.Size = new Size(75, 23);
			this.f.TabIndex = 21;
			this.f.Text = "保存(&S)";
			this.f.UseVisualStyleBackColor = true;
			this.f.Click += this.o;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1374, 431);
			base.Controls.Add(this.g);
			base.Controls.Add(this.d);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormAldsAccountEdit";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "帐号设置";
			base.Load += this.n;
			this.g.ResumeLayout(false);
			this.g.PerformLayout();
			this.af.ResumeLayout(false);
			this.ag.ResumeLayout(false);
			this.ag.PerformLayout();
			this.n.ResumeLayout(false);
			this.p.ResumeLayout(false);
			this.p.PerformLayout();
			this.v.ResumeLayout(false);
			this.s.ResumeLayout(false);
			this.d.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x0400008C RID: 140
		private AldsAccountInfo a;

		// Token: 0x0400008D RID: 141
		private string b;

		// Token: 0x0400008F RID: 143
		private Panel d;

		// Token: 0x04000090 RID: 144
		private Button e;

		// Token: 0x04000091 RID: 145
		private Button f;

		// Token: 0x04000092 RID: 146
		private GroupBox g;

		// Token: 0x04000093 RID: 147
		private TextBox h;

		// Token: 0x04000094 RID: 148
		private CheckBox i;

		// Token: 0x04000095 RID: 149
		private Label j;

		// Token: 0x04000096 RID: 150
		private Label k;

		// Token: 0x04000097 RID: 151
		private Label l;

		// Token: 0x04000098 RID: 152
		private Label m;

		// Token: 0x04000099 RID: 153
		private FlowLayoutPanel n;

		// Token: 0x0400009A RID: 154
		private ListView o;

		// Token: 0x0400009B RID: 155
		private Panel p;

		// Token: 0x0400009C RID: 156
		private Button q;

		// Token: 0x0400009D RID: 157
		private RadioButton r;

		// Token: 0x0400009E RID: 158
		private Panel s;

		// Token: 0x0400009F RID: 159
		private Button t;

		// Token: 0x040000A0 RID: 160
		private RadioButton u;

		// Token: 0x040000A1 RID: 161
		private Panel v;

		// Token: 0x040000A2 RID: 162
		private Button w;

		// Token: 0x040000A3 RID: 163
		private ColumnHeader x;

		// Token: 0x040000A4 RID: 164
		private ColumnHeader y;

		// Token: 0x040000A5 RID: 165
		private ColumnHeader z;

		// Token: 0x040000A6 RID: 166
		private ColumnHeader aa;

		// Token: 0x040000A7 RID: 167
		private ColumnHeader ab;

		// Token: 0x040000A8 RID: 168
		private ColumnHeader ac;

		// Token: 0x040000A9 RID: 169
		private ColumnHeader ad;

		// Token: 0x040000AA RID: 170
		private ColumnHeader ae;

		// Token: 0x040000AB RID: 171
		private FlowLayoutPanel af;

		// Token: 0x040000AC RID: 172
		private Panel ag;

		// Token: 0x040000AD RID: 173
		private Label ah;

		// Token: 0x040000AE RID: 174
		private TextBox ai;

		// Token: 0x040000AF RID: 175
		private Label aj;

		// Token: 0x040000B0 RID: 176
		private Button ak;

		// Token: 0x040000B1 RID: 177
		private TextBox al;

		// Token: 0x040000B2 RID: 178
		private Label am;

		// Token: 0x040000B3 RID: 179
		private CheckBox an;

		// Token: 0x040000B4 RID: 180
		private Label ao;

		// Token: 0x040000B5 RID: 181
		private ComboBox ap;

		// Token: 0x040000B6 RID: 182
		private Button aq;

		// Token: 0x040000B7 RID: 183
		private Button ar;

		// Token: 0x040000B8 RID: 184
		private Button @as;

		// Token: 0x040000B9 RID: 185
		private Button at;

		// Token: 0x02000026 RID: 38
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x060000E3 RID: 227 RVA: 0x00002817 File Offset: 0x00000A17
			internal bool b(CustomerServiceMould A_0)
			{
				return A_0.Title == this.a;
			}

			// Token: 0x040000BC RID: 188
			public string a;
		}
	}
}
