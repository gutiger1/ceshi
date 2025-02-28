using System;
using System.Collections.Generic;

namespace Agiso.Object
{
	// Token: 0x02000689 RID: 1673
	public class FixedLengthQueue : Queue<double>
	{
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x00052DF8 File Offset: 0x00050FF8
		public double Total
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x00052E10 File Offset: 0x00051010
		public double Avg
		{
			get
			{
				int count = base.Count;
				double num;
				if (count > 0)
				{
					num = this.b / (double)count;
				}
				else
				{
					num = 0.0;
				}
				return num;
			}
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		public FixedLengthQueue(uint length)
		{
			this.a = length;
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x00052E44 File Offset: 0x00051044
		public new void Enqueue(double val)
		{
			while ((long)base.Count >= (long)((ulong)this.a))
			{
				this.a();
			}
			base.Enqueue(val);
			this.b += val;
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x00052E88 File Offset: 0x00051088
		private double a()
		{
			double num2;
			if (base.Count > 0)
			{
				double num = base.Dequeue();
				this.b -= num;
				num2 = num;
			}
			else
			{
				num2 = 0.0;
			}
			return num2;
		}

		// Token: 0x04001218 RID: 4632
		private uint a = 0U;

		// Token: 0x04001219 RID: 4633
		private double b = 0.0;
	}
}
