# Import the module from the build output
Import-Module .PSWindowsCredentialManager\bin\Debug\netstandard2.0\PSWindowsCredentialManager.dll

# Read initial credentials
$credInitial = Get-Credential -Message "Enter initial credentials"

# Store the initial credentials
New-StoredCredential -Target "TestTarget" -Credential $credInitial

# Retrieve the stored credentials   
$credRetrieved = Get-StoredCredential -Target "TestTarget"

# Output the results
if($credRetrieved)
{
    Write-Host "Initial Credentials"
    Write-Host "Username: $($credInitial.UserName)"
    Write-Host "Password: $($credInitial.Password | ConvertFrom-SecureString -AsPlainText)"

    Write-Host ""
    Write-Host "Retrieved Credentials"
    Write-Host "Username: $($credRetrieved.UserName)"
    Write-Host "Password: $($credRetrieved.Password | ConvertFrom-SecureString -AsPlainText)"
}
else
{
    Write-Host "No credentials found"
}
