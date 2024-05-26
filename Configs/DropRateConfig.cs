using BepInEx.Configuration;

namespace CrimsonDropRate.Configs;

internal static class DropRateConfig
{ 
    internal static ConfigEntry<float> DropRateModifier { get; private set; }

    internal static void Initialize(ConfigFile config)
    {
        DropRateModifier = config.Bind(nameof(DropRateConfig), nameof(DropRateModifier), 1.0f, "Drop rate modifer value");
    }
}
