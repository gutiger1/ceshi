using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Agiso.DbManager;
using Agiso.Utils;
using Agiso.WwWebSocket.Model;
using AliwwClient.Enums;

namespace AliwwClient
{
	// Token: 0x02000020 RID: 32
	public partial class FormCustomerServiceMould : BaseForm
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x000026DD File Offset: 0x000008DD
		public FormCustomerServiceMould(FormAldsAccountEdit parentForm, ActionType actionType = ActionType.Add, CustomerServiceMould mould = null)
		{
			this.a();
			this.a = parentForm;
			this.c = actionType;
			this.b = mould;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0001251C File Offset: 0x0001071C
		private void c(object sender, EventArgs e)
		{
			switch (this.c)
			{
			case ActionType.Add:
				this.Text = "添加模板";
				break;
			case ActionType.Edit:
				this.Text = "修改模板";
				this.f.Text = this.b.Title;
				break;
			case ActionType.Copy:
				this.Text = "复制模板";
				break;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002708 File Offset: 0x00000908
		private void b(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00012588 File Offset: 0x00010788
		private void a(object sender, EventArgs e)
		{
			if (this.f.Text.Trim() == "")
			{
				MessageBox.Show("模板名称不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				try
				{
					switch (this.c)
					{
					case ActionType.Add:
						CustomerServiceMouldManager.Insert(this.a.SellerNick, this.f.Text.Trim());
						break;
					case ActionType.Edit:
						CustomerServiceMouldManager.Update(this.b.IdNo, this.f.Text.Trim());
						break;
					case ActionType.Copy:
					{
						long num = CustomerServiceMouldManager.Insert(this.a.SellerNick, this.f.Text.Trim());
						List<CustomerServiceWorksheet> list = CustomerServiceWorksheetManager.GetList(this.b.IdNo, this.a.SellerNick);
						if (!Util.IsEmptyList<CustomerServiceWorksheet>(list))
						{
							foreach (CustomerServiceWorksheet customerServiceWorksheet in list)
							{
								customerServiceWorksheet.MouldId = num;
								customerServiceWorksheet.ModifyTime = (customerServiceWorksheet.CreateTime = DateTime.Now);
								CustomerServiceWorksheetManager.Insert(customerServiceWorksheet);
							}
						}
						break;
					}
					}
					base.Close();
					this.a.InitCustomerServiceMould(this.f.Text.Trim());
				}
				catch (Exception ex)
				{
					if (ex.Message.Contains("UNIQUE") && ex.Message.Contains("SellerNick") && ex.Message.Contains("Title"))
					{
						MessageBox.Show("模板名称已存在了", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000127B8 File Offset: 0x000109B8
		private void a()
		{
			this.e = new Label();
			this.f = new TextBox();
			this.g = new Button();
			this.h = new Button();
			this.i = new Label();
			base.SuspendLayout();
			this.e.AutoSize = true;
			this.e.Location = new Point(22, 13);
			this.e.Name = "label1";
			this.e.Size = new Size(65, 12);
			this.e.TabIndex = 0;
			this.e.Text = "模板名称：";
			this.f.Location = new Point(93, 10);
			this.f.Name = "txtMouldTitle";
			this.f.Size = new Size(253, 21);
			this.f.TabIndex = 1;
			this.g.Location = new Point(178, 63);
			this.g.Name = "btnMouldConfirm";
			this.g.Size = new Size(75, 23);
			this.g.TabIndex = 2;
			this.g.Text = "保存(&S)";
			this.g.UseVisualStyleBackColor = true;
			this.g.Click += this.a;
			this.h.Location = new Point(271, 63);
			this.h.Name = "btnMouldCancel";
			this.h.Size = new Size(75, 23);
			this.h.TabIndex = 3;
			this.h.Text = "取消(&C)";
			this.h.UseVisualStyleBackColor = true;
			this.h.Click += this.b;
			this.i.AutoSize = true;
			this.i.Location = new Point(91, 34);
			this.i.Name = "label2";
			this.i.Size = new Size(209, 12);
			this.i.TabIndex = 4;
			this.i.Text = "比如：节假日安排模板、日常工作安排";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoValidate = AutoValidate.EnablePreventFocusChange;
			base.ClientSize = new Size(354, 94);
			base.Controls.Add(this.i);
			base.Controls.Add(this.h);
			base.Controls.Add(this.g);
			base.Controls.Add(this.f);
			base.Controls.Add(this.e);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormCustomerServiceMould";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "CustomerServiceMould";
			base.Load += this.c;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000066 RID: 102
		private FormAldsAccountEdit a;

		// Token: 0x04000067 RID: 103
		private CustomerServiceMould b;

		// Token: 0x04000068 RID: 104
		private ActionType c;

		// Token: 0x0400006A RID: 106
		private Label e;

		// Token: 0x0400006B RID: 107
		private TextBox f;

		// Token: 0x0400006C RID: 108
		private Button g;

		// Token: 0x0400006D RID: 109
		private Button h;

		// Token: 0x0400006E RID: 110
		private Label i;
	}
}
