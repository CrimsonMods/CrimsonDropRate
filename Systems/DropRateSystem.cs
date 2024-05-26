using ProjectM.EOS;
using ProjectM.Shared;
using ProjectM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using Bloodstone.API;
using Unity.Collections;
using CrimsonDropRate.Configs;

namespace CrimsonDropRate.Systems;

internal static class DropRateSystem
{
    internal static void ChangeDropRate()
    {
        if (DropRateConfig.DropRateModifier.Value == 1.0f)
            return;

        Plugin.Logger.LogInfo($"Changing drop rate values. Modifier: {DropRateConfig.DropRateModifier.Value}");

        var collection = VWorld.Server.GetExistingSystemManaged<PrefabCollectionSystem>();

        foreach (var dropTable in GetEntitiesDropTables())
        {
            var dropTableEntity = collection.PrefabLookupMap[dropTable.DropTableGuid];

            if (!VWorld.Server.EntityManager.HasComponent<DropTableDataBuffer>(dropTableEntity))
                continue;

            var buffer = VWorld.Server.EntityManager.GetBuffer<DropTableDataBuffer>(dropTableEntity);
            var newBuffer = new List<DropTableDataBuffer>();

            foreach (var dropTableData in buffer)
                newBuffer.Add(new DropTableDataBuffer()
                {
                    DropRate = Math.Min(1, dropTableData.DropRate * DropRateConfig.DropRateModifier.Value),
                    ItemGuid = dropTableData.ItemGuid,
                    ItemType = dropTableData.ItemType,
                    Quantity = dropTableData.Quantity
                });

            buffer.RemoveRange(0, buffer.Length);

            foreach (var dropTableData in newBuffer)
                buffer.Add(dropTableData);
        }

        Plugin.Logger.LogInfo($"Drop rate values changed successfully");
    }

    private static IList<DropTableBuffer> GetEntitiesDropTables()
    {
        var result = new List<DropTableBuffer>();
        var entities = VWorld.Server.EntityManager.CreateEntityQuery(new EntityQueryDesc()
        {
            All = new ComponentType[] { ComponentType.ReadOnly<DropTableBuffer>() },
            Options = EntityQueryOptions.IncludeAll
        }).ToEntityArray(Allocator.Temp);

        foreach (var entity in entities)
            foreach (var dropTable in VWorld.Server.EntityManager.GetBuffer<DropTableBuffer>(entity))
                if (!result.Any(r => r.DropTableGuid == dropTable.DropTableGuid))
                    result.Add(dropTable);

        return result;
    }
}
}
