# VariantSpeed

Five variants that change the **game speed**, from `Speedx1` (slowest) to
`Speedx5` (fastest). They are ticked like any other variant and have no settings.

A mod for **FortRise 5** (>= 5.3.3). The FortRise 4 version (`tf-mod-fortrise-variants-speed`) is no longer maintained: fixes and new features only land in this repository.

## Installation

1. Install FortRise 5 and start the game through `FortRise.exe`.
2. Copy `release/variantspeed` (or the shipped folder) into `<TowerFall>/FortRise/Mods/`.

Settings are under **Options > Mods > VariantSpeed**.
Data and log files live in `<TowerFall>/FortRise/Saves/VariantSpeed/` and `<TowerFall>/FortRise/Logs/`.

## Usage

Tick **one** of the `Speedx1` to `Speedx5` variants on the versus variants screen.
They override each other, so ticking several makes no sense.

> All my mods declare the same `Header` (`EBE1 MODS`), so their variants are
> grouped into a **single column** of the variants screen instead of one column
> per mod.

## Build / deployment

| Script | Purpose |
|--------|---------|
| `script/release.bat` | build, then assemble into `release/` |
| `script/deploy.bat` | copy `release/` into the TowerFall `Mods` folder |
| `script/release_deploy.bat` | both, one after the other |

Paths (game folder, module name) are set in `script/config.bat`.
