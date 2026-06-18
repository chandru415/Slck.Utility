# Slck.Utility

[![NuGet](https://img.shields.io/nuget/v/Slck.Utility.svg)](https://www.nuget.org/packages/Slck.Utility)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

Lightweight .NET utility library for microservices. Zero external dependencies.

## Installation

```bash
dotnet add package Slck.Utility
```

## Features

### DotEnvLoader

Load `.env` files into the process environment **before** `WebApplication.CreateBuilder()` runs — so values are visible to all configuration providers.

```csharp
using Slck.Utility;

DotEnvLoader.LoadFromConfiguredFile();

var builder = WebApplication.CreateBuilder(args);
// Environment variables from .env are now available via builder.Configuration
```

**Behavior:**
- Reads file path from `DOTNET_ENV_FILE` env var (defaults to `.env`)
- Searches working directory first, then `AppContext.BaseDirectory`
- Never overwrites existing environment variables (CI/CD always wins)
- Supports `"double"` and `'single'` quoted values
- Ignores comments (`#`) and blank lines

### Guard

Concise argument validation with automatic parameter name capture via `CallerArgumentExpression`:

```csharp
using Slck.Utility;

public void CreateUser(string email, string tenantId, int pageSize)
{
    Guard.NotNullOrWhiteSpace(email);     // throws ArgumentException
    Guard.NotNullOrWhiteSpace(tenantId);  // includes "tenantId" in error message
    Guard.Positive(pageSize);             // throws ArgumentOutOfRangeException
}
```

| Method | Throws |
|--------|--------|
| `Guard.NotNull(value)` | `ArgumentNullException` |
| `Guard.NotNullOrWhiteSpace(value)` | `ArgumentException` |
| `Guard.Positive(value)` | `ArgumentOutOfRangeException` |
| `Guard.NotNegative(value)` | `ArgumentOutOfRangeException` |

> `Positive` and `NotNegative` support both `int` and `long` overloads.

### ConfigurationGuard

Validate configuration values at startup — catch placeholders and missing secrets before they cause runtime failures:

```csharp
using Slck.Utility;

var secret = ConfigurationGuard.Require(config["Jwt:Secret"], "Jwt:Secret");
// throws InvalidOperationException if value is null, whitespace, or "CHANGE_ME" / "your-*"
```

| Method | Throws |
|--------|--------|
| `ConfigurationGuard.IsPlaceholder(value)` | Returns `bool` |
| `ConfigurationGuard.Require(value, key)` | `InvalidOperationException` |

### Util

General-purpose parsing helpers:

```csharp
using Slck.Utility;

var tenantId = Util.ParseRequiredGuid(config["TenantId"], "TenantId");
var optionalId = Util.ParseOptionalGuid(config["OptionalId"], "OptionalId");
```

| Method | Throws |
|--------|--------|
| `Util.ParseRequiredGuid(value, name)` | `ArgumentException` |
| `Util.ParseOptionalGuid(value, name)` | `ArgumentException` (if non-empty and invalid) |

### UriHelpers

URI manipulation utilities for consistent base URL handling:

```csharp
using Slck.Utility;

var baseUrl = UriHelpers.EnsureTrailingSlash("https://api.example.com/v1");
// "https://api.example.com/v1/"

var uri = UriHelpers.EnsureTrailingSlash(new Uri("https://api.example.com/v1"));
// Uri: https://api.example.com/v1/
```

## Targets

- `net9.0`
- `net10.0`

## License

[MIT](LICENSE)
