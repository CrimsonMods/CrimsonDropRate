# CrimsonDropRate
`Server side only` mod to change drop rate.

What is the difference between `DropTableModifier_General` native server configuration and `DropRateModifier` mod configuration?

![alt text](https://github.com/KinetsuDEV/VRisingDropRateModifier/blob/main/Thunderstore/drop-settings-comparison.png?raw=true)

## Installation
* Install [BepInEx](https://v-rising.thunderstore.io/package/BepInEx/BepInExPack_V_Rising/)
* Extract _DropRateModifier.dll_ into _(VRising server folder)/BepInEx/plugins_

## Configurable Values
```ini
[DropRateConfig]

## Drop rate modifier value
# Setting type: Single
# Default value: 1
DropRateModifier = 1
```
The lowest DropRate in the game is 0.001. The highest a DropRate can be is 1.0. 

New Value = the lesser of (DropRate * DropRateModifier) or 1.0

Therefore, the highest possible DropRateModifier before it no longer has an effect would be 1000. At that point every table's drop rate would be 1.0. 

_Do not ask me why you can't tell a difference if your modifier is set to 3. You changed a 0.05 somewhat uncommon drop to a slightly more common uncommon drop of 0.15._

## Support

Want to support my V Rising Mod development? 

Donations Accepted

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/skytech6)

Or buy/play my games! 

[Train Your Minibot](https://store.steampowered.com/app/713740/Train_Your_Minibot/) 

[Boring Movies](https://store.steampowered.com/app/1792500/Boring_Movies/) **Free to Play!**

**If you are looking to hire someone to make a mod for any Unity game reach out to me on Discord! (skytech6)**

### Credits
Ported [VRisingDropRateModifier](https://github.com/KinetsuDEV/VRisingDropRateModifier)
