# Simple Client WebApi

Package that performs simple requests for Web API.

## Install

Package Manager

```bash
PM> Install-Package SimpleClientWebApi
```

or .NET CLI

```bash
> dotnet add package SimpleClientWebApi
```

or Paket CLI

```bash
> paket add SimpleClientWebApi
```

## Example Usage

```bash
using SimpleClientWebApi;
using SimpleClientWebApi.Enums;

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

