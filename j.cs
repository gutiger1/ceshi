using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

// Token: 0x02000012 RID: 18
internal class j : IStaticDataSource
{
	// Token: 0x06000064 RID: 100 RVA: 0x000025D7 File Offset: 0x000007D7
	public j(string A_0)
	{
		this.a = A_0;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x0001103C File Offset: 0x0000F23C
	public Stream GetSource()
	{
		byte[] bytes = Encoding.UTF8.GetBytes(this.a);
		return new MemoryStream(bytes);
	}

	// Token: 0x04000042 RID: 66
	private string a;
}
