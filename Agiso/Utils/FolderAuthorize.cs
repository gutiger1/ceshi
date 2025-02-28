using System;
using System.IO;
using System.Security.AccessControl;

namespace Agiso.Utils
{
	// Token: 0x020000E5 RID: 229
	public class FolderAuthorize
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x00048E60 File Offset: 0x00047060
		public static bool SetFolderACL(string FolderPath, string UserName)
		{
			return FolderAuthorize.SetFolderACL(FolderPath, UserName, FileSystemRights.FullControl, AccessControlType.Allow);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00048E7C File Offset: 0x0004707C
		public static bool SetFolderACL(string FolderPath, string UserName, FileSystemRights Rights, AccessControlType AllowOrDeny)
		{
			return FolderAuthorize.SetFolderACL(FolderPath, UserName, Rights, AllowOrDeny, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlModification.Add);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00048E98 File Offset: 0x00047098
		public static bool SetFolderACL(string FolderPath, string UserName, FileSystemRights Rights, AccessControlType AllowOrDeny, InheritanceFlags Inherits, PropagationFlags PropagateToChildren, AccessControlModification AddResetOrRemove)
		{
			bool flag;
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(FolderPath);
				DirectorySecurity accessControl = directoryInfo.GetAccessControl(AccessControlSections.All);
				FileSystemAccessRule fileSystemAccessRule = new FileSystemAccessRule(UserName, Rights, Inherits, PropagateToChildren, AllowOrDeny);
				accessControl.ModifyAccessRule(AddResetOrRemove, fileSystemAccessRule, out flag);
				directoryInfo.SetAccessControl(accessControl);
			}
			catch
			{
				flag = false;
			}
			return flag;
		}
	}
}
