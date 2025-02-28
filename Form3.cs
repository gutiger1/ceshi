using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Agiso.AliwwApi.Qn;
using Agiso.Handler;
using Agiso.Utils;
using Agiso.Windows;
using AliwwClient.Manager;
using AliwwClient.Properties;
using ICSharpCode.SharpZipLib.Zip;

namespace AliwwClient
{
	// Token: 0x02000060 RID: 96
	public partial class Form3 : Form
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x000030C6 File Offset: 0x000012C6
		public Form3()
		{
			this.a();
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00034024 File Offset: 0x00032224
		private void f(object sender, EventArgs e)
		{
			string text = this.s.Text;
			int num = (text.StartsWith("0x") ? Convert.ToInt32(this.s.Text, 16) : Util.ToInt(text));
			IntPtr intPtr = new IntPtr(num);
			this.a("################################################################################");
			this.a("GetText: ", Win32Extend.GetText(intPtr));
			this.a("GetHtmlDocument: ", Win32Extend.GetHtmlText(intPtr));
			this.a("################################################################################");
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000030E3 File Offset: 0x000012E3
		private void a(string A_0, string A_1)
		{
			this.a(A_0);
			this.a("--------------------");
			this.a(A_1);
			this.a("========================================");
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000310B File Offset: 0x0000130B
		private void a(string A_0, params object[] A_1)
		{
			this.a(string.Format(A_0, A_1));
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000311A File Offset: 0x0000131A
		private void a(string A_0)
		{
			this.u.AppendText(A_0 + "\r\n");
		}

		// Token: 0x060002CB RID: 715
		[DllImport("user32.dll")]
		private static extern int FindWindow(string A_0, string A_1);

		// Token: 0x060002CC RID: 716
		[DllImport("user32.dll")]
		private static extern int FindWindowEx(int A_0, int A_1, string A_2, string A_3);

		// Token: 0x060002CD RID: 717
		[DllImport("user32.dll")]
		private static extern int SendMessage(int A_0, uint A_1, int A_2, int A_3);

		// Token: 0x060002CE RID: 718
		[DllImport("user32.dll")]
		private static extern int GetWindowThreadProcessId(int A_0, out int A_1);

		// Token: 0x060002CF RID: 719
		[DllImport("kernel32.dll")]
		private static extern int OpenProcess(uint A_0, bool A_1, int A_2);

		// Token: 0x060002D0 RID: 720
		[DllImport("kernel32.dll")]
		private static extern int VirtualAllocEx(int A_0, IntPtr A_1, uint A_2, uint A_3, uint A_4);

		// Token: 0x060002D1 RID: 721
		[DllImport("kernel32.dll")]
		private static extern bool ReadProcessMemory(int A_0, int A_1, IntPtr A_2, int A_3, ref uint A_4);

		// Token: 0x060002D2 RID: 722
		[DllImport("kernel32.dll")]
		private static extern bool WriteProcessMemory(int A_0, int A_1, IntPtr A_2, int A_3, ref uint A_4);

		// Token: 0x060002D3 RID: 723
		[DllImport("kernel32.dll")]
		private static extern bool CloseHandle(int A_0);

		// Token: 0x060002D4 RID: 724
		[DllImport("kernel32.dll")]
		private static extern bool VirtualFreeEx(int A_0, int A_1, uint A_2, uint A_3);

		// Token: 0x060002D5 RID: 725 RVA: 0x000340AC File Offset: 0x000322AC
		private int b(int A_0)
		{
			return Form3.SendMessage(A_0, 4100U, 0, 0);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000340C8 File Offset: 0x000322C8
		private int a(int A_0)
		{
			return Form3.SendMessage(A_0, 4608U, 0, 0);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000340E4 File Offset: 0x000322E4
		private void e(object sender, EventArgs e)
		{
			this.a = Form3.FindWindow("#32770", "Windows 任务管理器");
			this.a = Form3.FindWindowEx(this.a, 0, "#32770", null);
			this.a = Form3.FindWindowEx(this.a, 0, "SysListView32", null);
			int num = Form3.SendMessage(this.a, 4127U, 0, 0);
			int num2 = this.b(this.a);
			int num3 = this.a(num);
			int num4;
			Form3.GetWindowThreadProcessId(this.a, out num4);
			this.b = Form3.OpenProcess(56U, false, num4);
			this.c = Form3.VirtualAllocEx(this.b, IntPtr.Zero, 4096U, 12288U, 4U);
			string[] array = new string[num3];
			string[,] array2 = this.a(num2, num3);
			this.v.Items.Clear();
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num3; j++)
				{
					array[j] = array2[i, j];
				}
				ListViewItem listViewItem = new ListViewItem(array);
				this.v.Items.Add(listViewItem);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00034214 File Offset: 0x00032414
		private string[,] a(int A_0, int A_1)
		{
			string[,] array = new string[A_0, A_1];
			for (int i = 0; i < A_0; i++)
			{
				for (int j = 0; j < A_1; j++)
				{
					byte[] array2 = new byte[256];
					Form3.a[] array3 = new Form3.a[1];
					array3[0].a = this.q;
					array3[0].b = i;
					array3[0].c = j;
					array3[0].g = array2.Length;
					array3[0].f = (IntPtr)(this.c + Marshal.SizeOf(typeof(Form3.a)));
					uint num = 0U;
					Form3.WriteProcessMemory(this.b, this.c, Marshal.UnsafeAddrOfPinnedArrayElement<Form3.a>(array3, 0), Marshal.SizeOf(typeof(Form3.a)), ref num);
					Form3.SendMessage(this.a, 4171U, i, this.c);
					Form3.ReadProcessMemory(this.b, this.c + Marshal.SizeOf(typeof(Form3.a)), Marshal.UnsafeAddrOfPinnedArrayElement<byte>(array2, 0), array2.Length, ref num);
					string @string = Encoding.Unicode.GetString(array2, 0, (int)num);
					array[i, j] = @string;
				}
			}
			Form3.VirtualFreeEx(this.b, this.c, 0U, 32768U);
			Form3.CloseHandle(this.b);
			return array;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00034384 File Offset: 0x00032584
		private void d(object sender, EventArgs e)
		{
			string text = this.s.Text;
			int num;
			if (text.StartsWith("0x"))
			{
				num = Convert.ToInt32(text, 16);
			}
			else
			{
				num = Convert.ToInt32(text, 10);
			}
			IntPtr intPtr = new IntPtr(num);
			WindowsAPI.SendMessage(intPtr, 513, 1, 983095);
			WindowsAPI.SendMessage(intPtr, 514, 0, 983095);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000343F0 File Offset: 0x000325F0
		private void c(object sender, EventArgs e)
		{
			new Form3.b();
			if (File.Exists("C:\\Program Files (x86)\\AliWorkbench91600\\9.16.00N\\Resources\\newWebui\\sign.json"))
			{
				FileInfo fileInfo = new FileInfo("C:\\Program Files (x86)\\AliWorkbench91600\\9.16.00N\\Resources\\newWebui\\sign.json");
				if (fileInfo.Length > 0L)
				{
					fileInfo.CopyTo("C:\\Program Files (x86)\\AliWorkbench91600\\9.16.00N\\Resources\\newWebui\\sign.json.back");
					fileInfo.Delete();
					using (File.Create("C:\\Program Files (x86)\\AliWorkbench91600\\9.16.00N\\Resources\\newWebui\\sign.json"))
					{
					}
					File.Delete("C:\\Program Files (x86)\\AliWorkbench91600\\9.16.00N\\Resources\\newWebui\\sign.json.back");
				}
			}
			ZipFile zipFile = new ZipFile("C:\\Program Files (x86)\\AliWorkbench91600\\9.16.00N\\Resources\\newWebui\\webui.zip");
			ZipEntry entry = zipFile.GetEntry("web_chat-packer/recent.html");
			string text = "";
			using (Stream inputStream = zipFile.GetInputStream(entry))
			{
				using (StreamReader streamReader = new StreamReader(inputStream))
				{
					text = streamReader.ReadToEnd();
					text = text.Replace(RepairQnAliresManager.DefaultSourceJsUrl, "<script src='https://wwmsg.agiso.com/in' id='_ag'></script>");
				}
			}
			zipFile.BeginUpdate();
			zipFile.Add(new j(text), "web_chat-packer/recent.html");
			zipFile.CommitUpdate();
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000022DD File Offset: 0x000004DD
		private void b(object sender, EventArgs e)
		{
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000022DD File Offset: 0x000004DD
		private void a(object sender, EventArgs e)
		{
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00003133 File Offset: 0x00001333
		private void b()
		{
			MessageBox.Show("这是测试的");
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0003451C File Offset: 0x0003271C
		public static string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
		{
			string text;
			using (Stream responseStream = rsp.GetResponseStream())
			{
				if ("gzip".Equals(rsp.ContentEncoding, StringComparison.OrdinalIgnoreCase))
				{
					using (Stream stream = new GZipStream(responseStream, CompressionMode.Decompress))
					{
						using (StreamReader streamReader = new StreamReader(stream, encoding))
						{
							return streamReader.ReadToEnd();
						}
					}
				}
				using (StreamReader streamReader2 = new StreamReader(responseStream, encoding))
				{
					text = streamReader2.ReadToEnd();
				}
			}
			return text;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000345D4 File Offset: 0x000327D4
		private Encoding a(HttpWebResponse A_0)
		{
			string text = A_0.CharacterSet;
			if (string.IsNullOrEmpty(text))
			{
				text = "utf-8";
			}
			return Encoding.GetEncoding(text);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00034630 File Offset: 0x00032830
		private void a()
		{
			this.s = new TextBox();
			this.t = new Button();
			this.u = new TextBox();
			this.v = new ListView();
			this.w = new Button();
			this.x = new Button();
			this.y = new Button();
			this.z = new Button();
			base.SuspendLayout();
			this.s.Location = new global::System.Drawing.Point(12, 2);
			this.s.Name = "textBox1";
			this.s.Size = new Size(295, 21);
			this.s.TabIndex = 0;
			this.s.Text = "100832";
			this.t.Location = new global::System.Drawing.Point(313, 0);
			this.t.Name = "button1";
			this.t.Size = new Size(75, 23);
			this.t.TabIndex = 1;
			this.t.Text = "button1";
			this.t.UseVisualStyleBackColor = true;
			this.t.Click += this.f;
			this.u.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.u.Location = new global::System.Drawing.Point(12, 29);
			this.u.Multiline = true;
			this.u.Name = "textBox2";
			this.u.ScrollBars = ScrollBars.Vertical;
			this.u.Size = new Size(366, 402);
			this.u.TabIndex = 2;
			this.v.Location = new global::System.Drawing.Point(394, 31);
			this.v.Name = "listView1";
			this.v.Size = new Size(347, 402);
			this.v.TabIndex = 3;
			this.v.UseCompatibleStateImageBehavior = false;
			this.w.Location = new global::System.Drawing.Point(394, 0);
			this.w.Name = "button2";
			this.w.Size = new Size(75, 23);
			this.w.TabIndex = 4;
			this.w.Text = "button2";
			this.w.UseVisualStyleBackColor = true;
			this.w.Click += this.e;
			this.x.Location = new global::System.Drawing.Point(475, 0);
			this.x.Name = "button3";
			this.x.Size = new Size(75, 23);
			this.x.TabIndex = 5;
			this.x.Text = "Click";
			this.x.UseVisualStyleBackColor = true;
			this.x.Click += this.d;
			this.y.Location = new global::System.Drawing.Point(570, 2);
			this.y.Name = "button4";
			this.y.Size = new Size(75, 23);
			this.y.TabIndex = 6;
			this.y.Text = "button4";
			this.y.UseVisualStyleBackColor = true;
			this.y.Click += this.c;
			this.z.Location = new global::System.Drawing.Point(666, 1);
			this.z.Name = "button5";
			this.z.Size = new Size(75, 23);
			this.z.TabIndex = 7;
			this.z.Text = "button5";
			this.z.UseVisualStyleBackColor = true;
			this.z.Click += this.a;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(761, 443);
			base.Controls.Add(this.z);
			base.Controls.Add(this.y);
			base.Controls.Add(this.x);
			base.Controls.Add(this.w);
			base.Controls.Add(this.v);
			base.Controls.Add(this.u);
			base.Controls.Add(this.t);
			base.Controls.Add(this.s);
			base.Icon = Resources.Icon1;
			base.Name = "Form3";
			this.Text = "Form3";
			base.Load += this.b;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040002BF RID: 703
		private int a;

		// Token: 0x040002C0 RID: 704
		private int b;

		// Token: 0x040002C1 RID: 705
		private int c;

		// Token: 0x040002C2 RID: 706
		private int q = 1;

		// Token: 0x040002C4 RID: 708
		private TextBox s;

		// Token: 0x040002C5 RID: 709
		private Button t;

		// Token: 0x040002C6 RID: 710
		private TextBox u;

		// Token: 0x040002C7 RID: 711
		private ListView v;

		// Token: 0x040002C8 RID: 712
		private Button w;

		// Token: 0x040002C9 RID: 713
		private Button x;

		// Token: 0x040002CA RID: 714
		private Button y;

		// Token: 0x040002CB RID: 715
		private Button z;

		// Token: 0x02000061 RID: 97
		private struct a
		{
			// Token: 0x040002CC RID: 716
			public int a;

			// Token: 0x040002CD RID: 717
			public int b;

			// Token: 0x040002CE RID: 718
			public int c;

			// Token: 0x040002CF RID: 719
			public int d;

			// Token: 0x040002D0 RID: 720
			public int e;

			// Token: 0x040002D1 RID: 721
			public IntPtr f;

			// Token: 0x040002D2 RID: 722
			public int g;
		}

		// Token: 0x02000063 RID: 99
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x060002E9 RID: 745 RVA: 0x0000315E File Offset: 0x0000135E
			internal bool c(WindowInfo A_0)
			{
				return A_0.ProcessId == this.a.ProcessId;
			}

			// Token: 0x060002EA RID: 746 RVA: 0x0000315E File Offset: 0x0000135E
			internal bool b(WindowInfo A_0)
			{
				return A_0.ProcessId == this.a.ProcessId;
			}

			// Token: 0x040002D8 RID: 728
			public AliwwTalkWindowQn a;
		}
	}
}
