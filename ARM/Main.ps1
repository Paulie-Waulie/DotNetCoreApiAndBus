Param(
    [Parameter(Mandatory=$True)]
    [string]$environment
)

Login-AzureRmAccount
Import-Module -Name AzureRM

New-AzureRmResourceGroupDeployment -TemplateFile "$PSScriptRoot\sampleTemplate.json" -TemplateParameterFile "$PSScriptRoot\parameters.$environment.json" -ResourceGroupName "payments-spike"