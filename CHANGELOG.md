# Changelog

## [Unreleased]

## [0.0.1] - 2026-11-06

### Added
- Apsis.Coordinates: geodetic (WGS84 lat/long/altitude) ↔ ECEF coordinate conversion, verified accurate to 1cm.
- Apsis.Time: Epoch (Julian day, Gregorian conversion, leap-year/century handling) and Duration (microsecond-resolution, TimeSpan-like) types.
- Apsis.Units: Distance (metres/kilometres) and Angle (radians/degrees) value types.
- Apsis.Utilities: shared math helpers (degree/radian conversion).
- Test coverage across all four projects.
- CI build pipeline.
- NuGet package metadata (description, tags, readme) for all four projects.

### Changed
- Epoch storage simplified from a two-part representation to a single microsecond long.
- Renamed `PrimeVertical` to `PrimeVerticalRadius` for clarity; removed redundant `EcefXum`/`EcefYum`/`EcefZum` accessors.
- Various micro-optimisations (cached `SquareEccentricity`, fewer `Math.Pow`/`sin` calls per conversion).

### Fixed
- Equatorial radius was off by 10x in micrometres.
- Epoch was losing microsecond precision on conversion.
- Negative durations/coordinates showed a double negative sign in `ToString`.
- `FromStopwatchTick` integer overflow.
- NuGet pack failure (NU5039) from a missing packaged README in Apsis.Utilities.

### Removed
- Dead code: `Clock.cs`, `GeocentricRadius`, `scrapcode.txt`, unused `Random.Shared` usage, an empty leftover project/class, `PublicAPI.Shipped.txt`.

## [Initial development] - 2026-08-15

### Added
- Initial project structure.
- Basic geodetic coordinate data structure.
