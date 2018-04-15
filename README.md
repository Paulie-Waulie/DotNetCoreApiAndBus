# Dot Net Core Demo Api & Bus

A simple demo app to highlight certain features of using dotnet core and dotnet standard for a Web Api project. The demo solution will contain a Web Api project which utilises certain Azure resources such as Cosmos DB, Key Vault, Azure Service Bus etc. The solution will contain multiple components, a front end Api and some backend processing components. There is also a load generating tool to demonstrate the solution with continous user load.

## Components

TODO

## Getting Started

TODO

To run with Https you will need a certificate, for demo purposes you can use a self signed certificate which you can generate many ways, the simplest way if available is to use IIS: https://www.sslshopper.com/article-how-to-create-a-self-signed-certificate-in-iis-7.html

You can then set the use UseHttps setting to true and simply set the thumbprint of your certificate.

For help with settings up KeyVault and AD App Registrations see: https://docs.microsoft.com/en-gb/azure/key-vault/key-vault-get-started

For details on getting Application Insights runnign with an application see: https://github.com/Microsoft/ApplicationInsights-aspnetcore/wiki/Getting-Started

One issue found with UseApplicationInsights, the instrumentation key provided in IConfigurationBuilder.AddApplicationInsightsSettings is ignored, the correct instrumentation key seems to be only set if it is present in the appsettings.json or is passed to the overloaded extensions method on IWebHostBuilder called UseApplicationInsights.

## ARM Template

TODO

## User Secrets

To help support local development, when the app runs within a development environment, user secrets are used to overrite settings in the appsettings.json file. The User Secrets needed are as follows:

Api Project:

{
    "ApplicationInsights": {
        "InstrumentationKey": "The Instrumentation Key of your AI Respurce, this can be found under the Properties Blade"
    },
    "KeyVaultSettings": {
        "DnsName": "The DNS name of your key vault",
        "AppUserClientId": "Your Azure AD App Registration Application ID",
        "AppUserClientSecret": "Your Azure AD App Registration Key"
    },
    "ServerSettings": {
        "ApiCertificateThumbprint": "The thumbprint of your TLS certificate, if using Https"
    }
}

For more information see: https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.1&tabs=visual-studio


## License

This project is licensed under the terms of the GNU GENERAL PUBLIC LICENSE.
