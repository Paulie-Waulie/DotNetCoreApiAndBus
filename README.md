# Dot Net Core Demo Api & Bus

This project was for me to experiment with certain Microsoft technologies such as DotNet Core, Cosmos DB, ARM templates etc. It is not designed to be a perfect example for production code but hopefully it can be useful to others. It is assmued, that you have some prior knowledge of the technologies used.

A simple demo app to highlight certain features of using dotnet core and dotnet standard for a Web Api project. The demo solution will contain a Web Api project which utilises certain Azure resources such as Cosmos DB, Key Vault, Azure Service Bus etc. The solution will contain multiple components, a front end Api and some backend processing components. There is also a load generating tool to demonstrate the solution with continous user load.

## Azure Resources

All the resources used by the App are created by the ARM template or a Powershell Script detailed in the "ARM" section below.

**KeyVault**

The demo uses KeyVault to store secrets. For simplicity the demo uses AD App Registration Secrets rather than a certificate for authentication.

**Cosmos Document DB**

The demo utilises Cosmos DB for it's simple document storage. This is simple to use, provision and at some point a component will be added to utilise the Cosmos DB change feed processor.

**Application Insights**

The demo uses Application Insights for telemetry.

## Components

Currently, there is only a DotNet Core Web Api project. In the future other "backend" processing components will be added.

## Getting Started

To create the required Azure resources there are Powershell scripts and an ARM template, see the ARM section below for more information.


To run with Https you will need a certificate, for demo purposes you can use a self signed certificate which you can generate many ways, the simplest way if available is to use IIS: https://www.sslshopper.com/article-how-to-create-a-self-signed-certificate-in-iis-7.html

You can then set the use UseHttps setting to true and simply set the thumbprint of your certificate.

For help with settings up KeyVault and AD App Registrations see: https://docs.microsoft.com/en-gb/azure/key-vault/key-vault-get-started

For details on getting Application Insights runnign with an application see: https://github.com/Microsoft/ApplicationInsights-aspnetcore/wiki/Getting-Started

One issue found with UseApplicationInsights, the instrumentation key provided in IConfigurationBuilder.AddApplicationInsightsSettings is ignored, the correct instrumentation key seems to be only set if it is present in the appsettings.json or is passed to the overloaded extensions method on IWebHostBuilder called UseApplicationInsights.

## ARM Template

There are two powershell scripts under the "Arm" folder. One creates the dependant Azure artefacts for the application resources and one runs an ARM template to create the application resources.

To enable this to be used for multiple engineers and environments, the scripts use an "environment" parameter, for example "t99". This will be used to create the dependant resources and used to select an individual paramters file for the ARM template.

If either of the scripts seem to have stalled, check that that the Azure Login form has not opened behind your current window. Sometimes the window does not display at the front and the script sits there waiting for you to log in.

First, run the script call "CreateTestResources.ps1" script. Provide a short name for your environment, such as "test", "dev", "t99" etc. This will create app registrations and write out the details that you will need for following steps. The important values are:

| Output                    | Usage                               |
| ------------------------- |:-----------------------------------:|
| Tenant ID                 | Used in ARM Template Parameters     |
| Application Object ID     | Used in ARM Template Parameters     |
| Application Client ID     | Used for KeyVault App Configuration |
| Application Client Secret | Used for KeyVault App Configuration |

To create an individual parameters file, copy the "parametersbase.json" file and name "parameters.{yourenvironmentname}.json"

In the new file, replace **"YOUR_TENANT_ID"** with your Tenant ID and **"YOUR_APP_REGISTRATION_OBJECT_ID"** with your Application Object ID.

You will need the Client ID and Secret for configuring KeyVault settings in the UserSecrets detailed section below.

Once the parameters file is created, execute the "Main.ps1" script and provide the environmtent name which matches the part in the parameters file name. 

## User Secrets

To help support local development, when the app runs within a development environment, user secrets are used to overrite settings in the appsettings.json file. The User Secrets needed are as follows:

**Api Project**

```json
{
    "ApplicationInsights": {
        "InstrumentationKey": "The Instrumentation Key of your AI Respurce, this can be found under the Properties Blade"
    },
    "KeyVaultSettings": {
        "DnsName": "The DNS name of your key vault",
        "AppUserClientId": "Your Azure AD App Registration Application Client ID, will have been outputted by the CreateTestResource.ps1 sccript",
        "AppUserClientSecret": "Your Azure AD App Registration Key, will have been outputted by the CreateTestResource.ps1 sccript"
    },
    "ServerSettings": {
        "ApiCertificateThumbprint": "The thumbprint of your TLS certificate, if using Https"
    }
}
```

For more information see: https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.1&tabs=visual-studio


## License

This project is licensed under the terms of the GNU GENERAL PUBLIC LICENSE.
