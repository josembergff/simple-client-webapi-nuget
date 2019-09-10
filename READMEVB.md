# Simple Client WebApi

Package that performs simple requests for Web API from Visual Basic.

## Install

Package Manager

```bash
PM> Install-Package SimpleClientWebApiVB
```

or .NET CLI

```bash
> dotnet add package SimpleClientWebApiVB
```

or Paket CLI

```bash
> paket add SimpleClientWebApiVB
```

## Example Usage

```bash
imports SimpleClientWebApiVC;


    Public Class ExampleConnectWebAPI
    
        Public Function GetProducts() As Product()
			Dim clientJwt as ClientWebApiJwtVB = new ClientWebApiJwt("{uriWebApi}", "{keyJwt}")
            return clientJwt.Get(Product())("{routeAction}");
        End Function

    End Class
 
```

