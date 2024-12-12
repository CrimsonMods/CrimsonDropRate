using Il2CppInterop.Runtime;
using ProjectM;
using System;
using Unity.Entities;

namespace CrimsonDropRate.Systems;

internal class SystemService()
{
    private World _world;
    PrefabCollectionSystem _prefabCollectionSystem;
    public PrefabCollectionSystem PrefabCollectionSystem => _prefabCollectionSystem ??= GetSystem<PrefabCollectionSystem>();

    internal SystemService(World world) : this()
    {
        _world = world;
        _prefabCollectionSystem = GetSystem<PrefabCollectionSystem>();
    }

    T GetSystem<T>() where T : ComponentSystemBase
    {
        return _world.GetExistingSystemManaged<T>() ?? throw new InvalidOperationException($"Failed to get {Il2CppType.Of<T>().FullName} from the Server...");
    }
}