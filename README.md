# Dot Net Core Demo Api & Bus

A simple demo app to highlight certain features of using dotnet core and dotnet standard for a Web Api project. The demo solution will contain a Web Api project which utilises certain Azure resources such as Cosmos DB, Key Vault, Azure Service Bus etc. The solution will contain multiple components, a front end Api and some backend processing components. There is also a load generating tool to demonstrate the solution with continous user load.

## Components

TODO

## Getting Started

TODO

For help with settings up KeyVault and AD App Registrations see: https://docs.microsoft.com/en-gb/azure/key-vault/key-vault-get-started

## ARM Template

TODO

## User Secrets

To help support local development, when the app runs within a development environment, user secrets are used to overrite settings in the appsettings.json file. The User Secrets needed are as follows:

Api Project:

{
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
