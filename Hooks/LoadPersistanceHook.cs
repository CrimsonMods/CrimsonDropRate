using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrimsonDropRate.Systems;
using HarmonyLib;
using ProjectM;

namespace CrimsonDropRate.Hooks;

[HarmonyPatch]
internal static class LoadPersistanceHook
{
    [HarmonyPatch(typeof(LoadPersistenceSystemV2), nameof(LoadPersistenceSystemV2.SetLoadState))]
    [HarmonyPrefix]
    private static void SetLoadState(ServerStartupState.State loadState)
    {
        try
        {
            if (loadState == ServerStartupState.State.SuccessfulStartup)
            {
                DropRateSystem.ChangeDropRate();
            }
        }
        catch (Exception e) 
        {
            Plugin.Logger.LogError(e.Message);
        }
    }
}
