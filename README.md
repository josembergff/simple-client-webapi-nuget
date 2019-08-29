# Simple Client WebApi

Package that performs simple requests for Web API.

## Install

Package Manager

```bash
PM> Install-Package simple-client-webapi
```

or .NET CLI

```bash
> dotnet add package simple-client-webapi
```

or Paket CLI

```bash
> paket add simple-client-webapi
```

## Example Usage

```bash
using simple-client-webapi;
using simple-client-webapi.Enums;

namespace ExampleConnectWebAPI
{
    public class ExampleConnectWebAPI
    {
        private readonly ClientWebApiJwt clientJwt;
        public ExampleConnectWebAPI(){}
            clientJwt = new ClientWebApiJwt("{uriWebApi}", "{keyJwt}");
        }

        public List<Product> GetProducts(){
            return clientJwt.Get<List<Product>>("{routeAction}");
        }
    }
 }
```

