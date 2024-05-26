# CrimsonDropRate
`Server side only` mod to change drop rate.

What is the difference between `DropTableModifier_General` native server configuration and `DropRateModifier` mod configuration?

![alt text](https://github.com/KinetsuDEV/VRisingDropRateModifier/blob/main/Thunderstore/drop-settings-comparison.png?raw=true)

## Installation
* Install [BepInEx](https://v-rising.thunderstore.io/package/BepInEx/BepInExPack_V_Rising/)
* Install [Bloodstone](https://github.com/decaprime/Bloodstone/releases/tag/v0.2.1)
* Extract _DropRateModifier.dll_ into _(VRising server folder)/BepInEx/plugins_

## Configurable Values
```ini
[DropRateConfig]

## Drop rate modifier value
# Setting type: Single
# Default value: 1
DropRateModifier = 1
```

### Credits
Ported [VRisingDropRateModifier](https://github.com/KinetsuDEV/VRisingDropRateModifier)
