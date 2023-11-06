using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct AppleSpawningSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var (transform, spawn) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<AppleProperties>>())
        {
            // instantiate a new entity from the prefab
            var entity = ecb.Instantiate(spawn.ValueRO.ApplePrefab);

            // assign transform component to the entity
            var pos = transform.ValueRO.Position;
            var localTransformComponent = LocalTransform.FromPosition(pos);
            ecb.AddComponent(entity, localTransformComponent);
        }
    }
}