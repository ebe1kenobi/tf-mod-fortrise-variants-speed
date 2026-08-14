# VariantSpeed
<img width="640" height="480" alt="headhunters_231254_round_00" src="https://github.com/user-attachments/assets/3cfa17f3-2555-4cc3-8733-88d0a99032a7" />


One **per-player** variant, `SPEED`: the archers that carry it run faster, exactly
the way TowerFall's speed boots make you run faster. How much faster is set in a
window that opens on the variant itself.

A mod for **FortRise 5** (>= 5.3.3). The FortRise 4 version (`tf-mod-fortrise-variants-speed`) is no longer maintained: fixes and new features only land in this repository.

## Installation

1. Install FortRise 5 and start the game through `FortRise.exe`.
2. Copy `release/variantspeed` (or the shipped folder) into `<TowerFall>/FortRise/Mods/`.

Settings are under **Options > Mods > VariantSpeed**.
Data and log files live in `<TowerFall>/FortRise/Saves/VariantSpeed/` and `<TowerFall>/FortRise/Logs/`.

## Usage

<img width="667" height="239" alt="image" src="https://github.com/user-attachments/assets/b7aabb5c-7a9c-4025-8e35-da5d520b1b2c" />

<img width="627" height="211" alt="image" src="https://github.com/user-attachments/assets/2852af98-df77-4296-a0d4-cb83925a359a" />

<img width="886" height="219" alt="image" src="https://github.com/user-attachments/assets/c64ec12c-2ca7-45bc-925d-c9ca1820b6fd" />


Tick `SPEED` on the variants screen, then:

| Key | |
|---|---|
| **Alt** | the game's own per-player window - who runs faster |
| **Alt2** | the speed window - *how much* faster |

In the speed window, left and right move the multiplier by 5 %, between `x1.05` and
`x2.50`; confirm or back closes it. The value is shared by every archer carrying the
variant, and it is remembered between sessions (it is also editable under
**Options > Mods > VariantSpeed**).

### It speeds up the archer, not the game

The variant used to set `Engine.TimeRate`, which sped up *everything* - arrows,
enemies, platforms, animations, even the music. That does not make an archer fast,
it makes the match short, and it breaks anything counted in frames: round timers,
recordings, other mods' variants.

TowerFall has exactly one line for its speed boots, inside `Player.MaxRunSpeed`:
`if (HasSpeedBoots) num *= 1.4f;`. This mod does the same thing with an adjustable
factor, and nothing else moves - not the jump, not the dodge, not the inertia. The
default is `x1.40`, the boots' own value. It stacks with real boots picked up
in-game, as two speed bonuses should.

### Why one variant instead of five

`Speedx1` to `Speedx5` filled five cells of the variants screen to say the same
thing at five fixed speeds. One cell says it better, and the window covers every
speed in between. The window itself is modelled on the game's per-player window -
same panel, same slide-in, same sounds - so it reads as part of the screen rather
than a menu bolted onto it.

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
