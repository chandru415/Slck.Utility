# Changelog

All notable changes to this project will be documented in this file.

## [1.0.0] - 2026-05-23

### Added
- `DotEnvLoader` — Load `.env` files into process environment before host build
- `Guard` — Concise argument validation with `CallerArgumentExpression` support
- `ConfigurationGuard` — Placeholder detection and startup configuration validation
- `UriHelpers` — URI manipulation helpers (trailing slash normalization)

### Changed
- `DotEnvLoader` — Support `export` prefix and inline comment stripping
- `Guard` — Added `long` overloads for `Positive` and `NotNegative`
- `ConfigurationGuard` — Added `Require`, `ParseRequiredGuid`, and `ParseOptionalGuid` methods
- `UriHelpers` — Added `Uri` overload for `EnsureTrailingSlash`
