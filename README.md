# Dot Net Core Demo Api & Bus

A simple demo app to highlight certain features of using dotnet core for a Web Api project. The demo solution will contain a Web Api project which utilises certain 

## Components

TODO

## Getting Started

TODO

## ARM Template

TODO

## User Secrets

To help support local development, when the app runs within a development environment, user secrets are used to overrite settings in the appsettings.json file. The User Secrets needed are as follows:

Api Project:

{
    "DocumentDB": {
        "EndpointUri": "The address of your Cosmos account",
        "AuthKey": "The Auth Key of your Cosmos account"
    },
    "ServerSettings": {
        "ApiCertificateThumbprint": "The thumbprint of your TLS certificate, if using Https"
    }
}

## License

This project is licensed under the terms of the GNU GENERAL PUBLIC LICENSE.
