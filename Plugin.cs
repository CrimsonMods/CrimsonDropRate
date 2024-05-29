using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using Bloodstone.API;
using CrimsonDropRate.Configs;
using HarmonyLib;
using Unity.Entities;
using UnityEngine;

namespace CrimsonDropRate;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("gg.deca.Bloodstone")]
public class Plugin : BasePlugin
{
    Harmony _harmony;
    public static ManualLogSource Logger;

    public override void Load()
    {
        if (!VWorld.IsServer) return;

        Logger = Log;
        DropRateConfig.Initialize(Config);

        // Harmony patching
        _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        _harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());

        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} version {MyPluginInfo.PLUGIN_VERSION} is loaded!");
    }

    public override bool Unload()
    {
        Config.Clear();
        _harmony?.UnpatchSelf();
        return true;
    }
}
