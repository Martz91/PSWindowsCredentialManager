using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace PSWindowsCredentialManager
{
    [Cmdlet(VerbsCommon.New, "StoredCredential")]
    [OutputType(typeof(bool))]
    public class NewStoredCredentialCommand : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public string Target { get; set; }

        [Parameter(
            Position = 1,
            ValueFromPipelineByPropertyName = true)]
        public PSCredential Credential { get; set; }

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose("Begin!");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {

            //var credentialManager = new CredentialManager();
            var credential = new CredentialManager.CREDENTIAL
            {
                Flags = 0,
                Type = 1, // CRED_TYPE_GENERIC
                TargetName = Marshal.StringToCoTaskMemUni(Target),
                CredentialBlob = Marshal.SecureStringToCoTaskMemUnicode(Credential.Password),
                CredentialBlobSize = (uint)(Credential.Password.Length * 2),
                Persist = 2, // CRED_PERSIST_LOCAL_MACHINE
                UserName = Marshal.StringToCoTaskMemUni(Credential.UserName)
            };

            var result = CredentialManager.CredWrite(ref credential, 0);

            Marshal.FreeCoTaskMem(credential.TargetName);
            Marshal.FreeCoTaskMem(credential.CredentialBlob);
            Marshal.FreeCoTaskMem(credential.UserName);

            WriteObject(result);
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        protected override void EndProcessing()
        {
            WriteVerbose("End!");
        }
    }

}
