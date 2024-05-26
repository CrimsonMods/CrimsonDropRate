using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonDropRate.Configs;

internal static class DropRateConfig
{ 
    internal static ConfigEntry<float> DropRateModifier { get; private set; }

    internal static void Initialize(ConfigFile config)
    {
        DropRateModifier = config.Bind(nameof(DropRateConfig), nameof(DropRateModifier), 1.0f, "Drop rate modifer value");
    }
}
