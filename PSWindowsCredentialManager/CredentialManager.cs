using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace PSWindowsCredentialManager
{
    class CredentialManager
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct CREDENTIAL
        {
            public uint Flags;
            public uint Type;
            public IntPtr TargetName;
            public IntPtr Comment;
            public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
            public uint CredentialBlobSize;
            public IntPtr CredentialBlob;
            public uint Persist;
            public uint AttributeCount;
            public IntPtr Attributes;
            public IntPtr TargetAlias;
            public IntPtr UserName;
        }

        [DllImport("Advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool CredWrite([In] ref CREDENTIAL userCredential, [In] uint flags);

        [DllImport("Advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool CredRead(string target, uint type, uint reservedFlag, out IntPtr credentialPtr);

        [DllImport("Advapi32.dll", SetLastError = true)]
        public static extern void CredFree([In] IntPtr cred);
    }

}
