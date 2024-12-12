using CrimsonDropRate.Configs;
using ProjectM;
using ProjectM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;

namespace CrimsonDropRate.Systems;

internal static class DropRateSystem
{
    internal static void ChangeDropRate()
    {
        if (DropRateConfig.DropRateModifier.Value == 1.0f)
            return;

        Plugin.Logger.LogInfo($"Changing drop rate values. Modifier: {DropRateConfig.DropRateModifier.Value}");

        try
        {
            var collection = Core.SystemService.PrefabCollectionSystem;

            foreach (var dropTable in GetEntitiesDropTables())
            {
                try
                {
                    var dropTableEntity = collection._PrefabGuidToEntityMap[dropTable.DropTableGuid];
                    if (dropTableEntity.Equals(Entity.Null) || !Core.Server.EntityManager.HasComponent<DropTableDataBuffer>(dropTableEntity))
                        continue;

                    var buffer = Core.Server.EntityManager.GetBuffer<DropTableDataBuffer>(dropTableEntity);
                    var newBuffer = new List<DropTableDataBuffer>();

                    foreach (var dropTableData in buffer)
                    {
                        Plugin.Logger.LogInfo($"DropRate: {dropTableData.DropRate}");
                        newBuffer.Add(new DropTableDataBuffer()
                        {
                            DropRate = Math.Min(1, (float)(dropTableData.DropRate * DropRateConfig.DropRateModifier.Value)),
                            ItemGuid = dropTableData.ItemGuid,
                            ItemType = dropTableData.ItemType,
                            Quantity = dropTableData.Quantity
                        });
                    }

                    buffer.RemoveRange(0, buffer.Length);

                    foreach (var dropTableData in newBuffer)
                        buffer.Add(dropTableData);

                }
                catch (Exception ex)
                {
                    Plugin.Logger.LogError($"Error accessing dropTable: {ex.Message}\nStackTrace: {ex.StackTrace}");
                }

            }
        }
        catch (Exception ex)
        {
            Plugin.Logger.LogError($"Error Setting Drop Rate Values: {ex.Message}\nStackTrace: {ex.StackTrace}");
        }
        finally
        {
            Plugin.Logger.LogInfo($"Drop rate values changed successfully");
        }
    }

    private static IList<DropTableBuffer> GetEntitiesDropTables()
    {
        var result = new List<DropTableBuffer>();
        var entities = Core.Server.EntityManager.CreateEntityQuery(new EntityQueryDesc()
        {
            All = new ComponentType[] { ComponentType.ReadOnly<DropTableBuffer>() },
            Options = EntityQueryOptions.IncludeAll
        }).ToEntityArray(Allocator.Temp);

        foreach (var entity in entities)
            foreach (var dropTable in Core.Server.EntityManager.GetBuffer<DropTableBuffer>(entity))
                if (!result.Any(r => r.DropTableGuid == dropTable.DropTableGuid))
                    result.Add(dropTable);

        return result;
    }
}
