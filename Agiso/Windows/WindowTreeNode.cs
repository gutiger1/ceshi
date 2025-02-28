using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Agiso.Windows
{
	// Token: 0x020006AF RID: 1711
	public class WindowTreeNode
	{
		// Token: 0x0600216E RID: 8558 RVA: 0x0000EAAF File Offset: 0x0000CCAF
		public WindowTreeNode(WindowInfo win)
		{
			this.a = win;
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x0600216F RID: 8559 RVA: 0x0000EACA File Offset: 0x0000CCCA
		public List<WindowTreeNode> ChildList { get; } = new List<WindowTreeNode>();

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x0000EAD2 File Offset: 0x0000CCD2
		public WindowStruct Info
		{
			get
			{
				return this.a.Info;
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x00059B70 File Offset: 0x00057D70
		public string WriteTreeNode(string deep = "")
		{
			StringBuilder stringBuilder = new StringBuilder();
			deep += ">>";
			stringBuilder.Append("hWnd:" + this.a.HWnd.ToString("X2"));
			stringBuilder.Append("\t ClassName:" + this.a.Info.ClassName);
			stringBuilder.Append("\t WindowName:" + this.a.Info.WindowName);
			stringBuilder.AppendLine("\t Text:" + this.a.GetText());
			stringBuilder.AppendLine("\t Visible:" + this.a.Visible.ToString());
			stringBuilder.AppendLine("\t Disabled:" + this.a.Disabled.ToString());
			foreach (WindowTreeNode windowTreeNode in this.ChildList)
			{
				stringBuilder.Append(deep + windowTreeNode.WriteTreeNode(deep));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001296 RID: 4758
		private WindowInfo a;

		// Token: 0x04001297 RID: 4759
		[CompilerGenerated]
		private readonly List<WindowTreeNode> b;
	}
}
