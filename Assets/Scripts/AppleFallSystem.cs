using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct AppleFallSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var (transform, properties, entity) in SystemAPI.Query<RefRW<LocalTransform>,
                                                        RefRW<AppleProperties>>().WithEntityAccess())
        {
            var pos = transform.ValueRO.Position;
            var gravity = new float3(0.0f, -9.81f, 0.0f);

            pos += properties.ValueRO.Speed * SystemAPI.Time.DeltaTime;
            pos += gravity * SystemAPI.Time.DeltaTime;

            transform.ValueRW.Position = pos;

            properties.ValueRW.Speed = -1;
        }
    }
}