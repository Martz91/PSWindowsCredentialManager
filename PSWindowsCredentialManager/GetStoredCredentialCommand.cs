using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace PSWindowsCredentialManager
{
    [Cmdlet(VerbsCommon.Get, "StoredCredential")]
    [OutputType(typeof(PSCredential))]
    public class GetStoredCredentialCommand : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public string Target { get; set; }

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose("Begin!");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            if (CredentialManager.CredRead(Target, 1, 0, out IntPtr credentialPtr))
            {
                var credential = Marshal.PtrToStructure<CredentialManager.CREDENTIAL>(credentialPtr);
                var secureString = new SecureString();
                var passwordChars = new char[credential.CredentialBlobSize / 2];

                Marshal.Copy(credential.CredentialBlob, passwordChars, 0, passwordChars.Length);

                foreach (var c in passwordChars)
                {
                    secureString.AppendChar(c);
                }
                var psCredential = new PSCredential(Marshal.PtrToStringUni(credential.UserName), secureString);
                CredentialManager.CredFree(credentialPtr);
                WriteObject(psCredential);
            }
            else
            {
                WriteObject(null);
            }
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        protected override void EndProcessing()
        {
            WriteVerbose("End!");
        }
    }

}
