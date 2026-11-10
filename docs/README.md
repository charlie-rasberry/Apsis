# Apsis

A .NET library for accurate, fast coordinate, time, and (eventually) orbital-mechanics math as groundwork for a future satellite/network simulation.

## Status

Early development.

## Vision

The long-term goal is a library accurate and fast enough to model orbital mechanics and simulate signal propagation (EMF) between satellites, ground stations, and relay stations — eventually powering a simulation-style game
built around growing a network. That's the direction, not a promise of what exists today.

## What's here now

- Apsis.Coordinates: geodetic (lat/long/altitude) to ECEF coordinate conversion, using fixed-point integer storage (micrometers) rather than floating point.
- Apsis.Time: Epoch (Julian day, microsecond resolution) and Duration (microsecond-based, similar to TimeSpan) for precise time handling.
- Apsis.Units: strongly-typed value types for physical quantities (Distance, Angle) to avoid unit-mixup bugs.
- Apsis.Utilities: shared math helpers used across the other modules.

## License

MIT - see LICENSE.