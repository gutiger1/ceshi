using System;
using System.Collections.Generic;

namespace System.Collections.Concurrent
{
	// Token: 0x0200001D RID: 29
	public class ConcurrentList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000114B8 File Offset: 0x0000F6B8
		public int Count
		{
			get
			{
				object obj = this.b;
				int count;
				lock (obj)
				{
					count = this.a.Count;
				}
				return count;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002667 File Offset: 0x00000867
		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<T>)this.a).IsReadOnly;
			}
		}

		// Token: 0x17000009 RID: 9
		public T this[int index]
		{
			get
			{
				object obj = this.b;
				T t;
				lock (obj)
				{
					t = this.a[index];
				}
				return t;
			}
			set
			{
				object obj = this.b;
				lock (obj)
				{
					this.a[index] = value;
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002674 File Offset: 0x00000874
		internal ConcurrentList()
		{
			this.a = new List<T>();
			this.b = ((ICollection)this.a).SyncRoot;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00011590 File Offset: 0x0000F790
		public void Add(T item)
		{
			object obj = this.b;
			lock (obj)
			{
				this.a.Add(item);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000115D8 File Offset: 0x0000F7D8
		public void Clear()
		{
			object obj = this.b;
			lock (obj)
			{
				this.a.Clear();
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00011620 File Offset: 0x0000F820
		public bool Contains(T item)
		{
			object obj = this.b;
			bool flag2;
			lock (obj)
			{
				flag2 = this.a.Contains(item);
			}
			return flag2;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00011668 File Offset: 0x0000F868
		public void CopyTo(T[] array, int arrayIndex)
		{
			object obj = this.b;
			lock (obj)
			{
				this.a.CopyTo(array, arrayIndex);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000116B0 File Offset: 0x0000F8B0
		public bool Remove(T item)
		{
			object obj = this.b;
			bool flag2;
			lock (obj)
			{
				flag2 = this.a.Remove(item);
			}
			return flag2;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000116F8 File Offset: 0x0000F8F8
		IEnumerator IEnumerable.GetEnumerator()
		{
			object obj = this.b;
			IEnumerator enumerator;
			lock (obj)
			{
				enumerator = this.a.GetEnumerator();
			}
			return enumerator;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00011744 File Offset: 0x0000F944
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			object obj = this.b;
			IEnumerator<T> enumerator;
			lock (obj)
			{
				enumerator = ((IEnumerable<T>)this.a).GetEnumerator();
			}
			return enumerator;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0001178C File Offset: 0x0000F98C
		public int IndexOf(T item)
		{
			object obj = this.b;
			int num;
			lock (obj)
			{
				num = this.a.IndexOf(item);
			}
			return num;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000117D4 File Offset: 0x0000F9D4
		public void Insert(int index, T item)
		{
			object obj = this.b;
			lock (obj)
			{
				this.a.Insert(index, item);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0001181C File Offset: 0x0000FA1C
		public void RemoveAt(int index)
		{
			object obj = this.b;
			lock (obj)
			{
				this.a.RemoveAt(index);
			}
		}

		// Token: 0x04000053 RID: 83
		private List<T> a;

		// Token: 0x04000054 RID: 84
		private object b;
	}
}
