Param(
    [Parameter(Mandatory=$True)]
    [string]$environment
)

Login-AzureRmAccount

# Create Resource Group for Demo
New-AzureRmResourceGroup "payments-spike" "North Europe" -Force

# Create AD App Registration
$appName = "DotNetCoreDemoAppRegistration"
$uri = "http://dotnetcoredemo"
$secret = [GUID]::NewGuid().GUID
$secretSecure = (ConvertTo-SecureString $secret -AsPlainText -Force)

$appRegistration = Get-AzureRmADApplication -IdentifierUri $uri

if ($appRegistration)
{
    Write-Output "App Registration Already Exists!"
}
else {

    $psadCredential =  @{}
    $startDate = Get-Date  
    $psadCredential.StartDate = $startDate
    $psadCredential.EndDate = $startDate.AddYears(10)
    $psadCredential.KeyId = [guid]::NewGuid()
    $psadCredential.Password = $secret

    Write-Output $psadCredential

    $azureAdApplication = New-AzureRmADApplication -DisplayName $appName -HomePage $Uri -IdentifierUris $Uri -PasswordCredentials $psadCredential
    $svcprincipal = New-AzureRmADServicePrincipal -ApplicationId $azureAdApplication.ApplicationId
    $subscriptionId = (Get-AzureRmContext).Subscription.SubscriptionId
    $tenantId = (Get-AzureRmContext).Tenant.TenantId
    $objectId = $svcprincipal.Id
    $clientId = $azureAdApplication.ApplicationId.Guid
    
    Write-Output ""
    Write-Output "Save these values for using them in your application and ARM tempalte parameters"
    Write-Output ""

    echo "SubscriptionId            : $subscriptionId"
    echo "TenantId                  : $tenantId"
    echo "Application Object ID     : $objectId"
    echo "Application Client ID     : $clientId"
    echo "Application Client Secret : $secret"
    Write-Output ""
}


# Create Pre-existing Service Bus Namespace
$northEuropeBusRG = "Demo-RG-Payments-eun-$environment"

New-AzureRmResourceGroup $northEuropeBusRG "North Europe" -Force

New-AzureRmServiceBusNamespace -ResourceGroupName $northEuropeBusRG  -Location "North Europe" -Name "Demo-ASB-Payments-eun-$environment"
New-AzureRmServiceBusAuthorizationRule -ResourceGroupName $northEuropeBusRG -Namespace "Demo-ASB-Payments-eun-$environment" -Name "SendOnlyKey" -Rights "Send"
New-AzureRmServiceBusAuthorizationRule -ResourceGroupName $northEuropeBusRG -Namespace "Demo-ASB-Payments-eun-$environment" -Name "SendAndListenKey" -Rights "Send", "Listen"