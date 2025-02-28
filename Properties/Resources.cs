using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace AliwwClient.Properties
{
	// Token: 0x02000068 RID: 104
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	public class Resources
	{
		// Token: 0x06000305 RID: 773 RVA: 0x000025CE File Offset: 0x000007CE
		internal Resources()
		{
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00034D8C File Offset: 0x00032F8C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.a == null)
				{
					ResourceManager resourceManager = new ResourceManager("AliwwClient.Properties.Resources", typeof(Resources).Assembly);
					Resources.a = resourceManager;
				}
				return Resources.a;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00034DCC File Offset: 0x00032FCC
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00003181 File Offset: 0x00001381
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static CultureInfo Culture
		{
			get
			{
				return Resources.b;
			}
			set
			{
				Resources.b = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00034DE0 File Offset: 0x00032FE0
		public static Bitmap banner
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("banner", Resources.b);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00034E0C File Offset: 0x0003300C
		public static Icon Icon1
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Icon1", Resources.b);
				return (Icon)@object;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00034E38 File Offset: 0x00033038
		public static string MyQN
		{
			get
			{
				return Resources.ResourceManager.GetString("MyQN", Resources.b);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00034E5C File Offset: 0x0003305C
		public static string NewRecent
		{
			get
			{
				return Resources.ResourceManager.GetString("NewRecent", Resources.b);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00034E80 File Offset: 0x00033080
		public static string plugProxy
		{
			get
			{
				return Resources.ResourceManager.GetString("plugProxy", Resources.b);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00034EA4 File Offset: 0x000330A4
		public static string QnAutoReply
		{
			get
			{
				return Resources.ResourceManager.GetString("QnAutoReply", Resources.b);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00034EC8 File Offset: 0x000330C8
		public static string qnProxy
		{
			get
			{
				return Resources.ResourceManager.GetString("qnProxy", Resources.b);
			}
		}

		// Token: 0x040002E4 RID: 740
		private static ResourceManager a;

		// Token: 0x040002E5 RID: 741
		private static CultureInfo b;
	}
}
