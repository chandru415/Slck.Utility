# Slck.Utility

Lightweight utility library for .NET microservices.

## Features

- **DotEnvLoader** — Load `.env` files before host build (CI/CD values always win)
- **Guard** — Concise argument validation with `CallerArgumentExpression`

## Installation

```bash
dotnet add package Slck.Utility
```

## Usage

### DotEnvLoader

Call before `WebApplication.CreateBuilder()` to inject `.env` values into the process environment:

```csharp
using Slck.Utility;

DotEnvLoader.LoadFromConfiguredFile();

var builder = WebApplication.CreateBuilder(args);
```

Set `DOTNET_ENV_FILE` to override the default `.env` path (useful in `launchSettings.json`):

```json
{
  "profiles": {
    "dev": {
      "environmentVariables": {
        "DOTNET_ENV_FILE": ".env.development"
      }
    }
  }
}
```

### Guard

```csharp
using Slck.Utility;

public void CreateUser(string email, string tenantId)
{
    Guard.NotNullOrWhiteSpace(email);
    Guard.NotNullOrWhiteSpace(tenantId);
    // ...
}
```

## License

MIT
