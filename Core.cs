using System;
using System.Linq;
using CrimsonDropRate.Systems;
using Unity.Entities;

namespace CrimsonDropRate;

internal static class Core
{
    public static World Server { get; } = GetServerWorld() ?? throw new Exception("There is no Server world (yet)...");
    public static SystemService SystemService { get; } = new(Server);

    public static bool hasInitialized = false;
    public static void Initialize()
    {
        if (hasInitialized) return;
        DropRateSystem.ChangeDropRate();
        hasInitialized = true;
    }

    static World GetServerWorld()
    {
        return World.s_AllWorlds.ToArray().FirstOrDefault(world => world.Name == "Server");
    }
}
