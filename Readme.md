# PSWindowsCredentialManager

A PowerShell binary module for interacting with the Windows Credential Manager.

## Overview

PSWindowsCredentialManager provides cmdlets to store and retrieve credentials in the Windows Credential Manager. This allows you to securely save credentials and access them later from PowerShell scripts without storing sensitive information in plain text.

## Installation

1. Download the latest release
2. Extract the contents to a PowerShell module directory (e.g. `$env:UserProfile\Documents\WindowsPowerShell\Modules\PSWindowsCredentialManager`)
3. Import the module:

```powershell
Import-Module PSWindowsCredentialManager
``` 
## Usage

### Storing Credentials

To store credentials in the Windows Credential Manager:

```powershell
New-StoredCredential -Target "TestTarget" -Credential $credential
``` 
### Retrieving Credentials

To retrieve credentials from the Windows Credential Manager:

```powershell
Get-StoredCredential -Target "TestTarget"
```

## Disclaimer

At the moment this module is in a very early stage of development.
This module is provided "as is" without any warranty. Use at your own risk.
