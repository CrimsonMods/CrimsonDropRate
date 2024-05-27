using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using CrimsonDropRate.Configs;
using HarmonyLib;
using Unity.Entities;
using UnityEngine;

namespace CrimsonDropRate;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    public static World Server { get; } = GetWorld("Server") ?? throw new System.Exception("There is no Server world (yet). Did you install a server mod on the client?");
    Harmony _harmony;
    public static ManualLogSource Logger;

    public override void Load()
    {
        if (!IsServer) return;

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

    private static World GetWorld(string name)
    {
        foreach (var world in World.s_AllWorlds)
        { 
            if(world.Name == name) return world;
        }

        return null;
    }

    private static bool IsServer => Application.productName == "VRisingServer";
}
