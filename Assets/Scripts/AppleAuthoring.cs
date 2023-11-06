using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

public class AppleAuthoring : MonoBehaviour
{
    public GameObject ApplePrefab;
    public float speed;

    private class AppleBaker : Baker<AppleAuthoring>
    {
        public override void Bake(AppleAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            var appleComponent = new AppleProperties
            {
                ApplePrefab = GetEntity(authoring.ApplePrefab, TransformUsageFlags.Dynamic),
                Speed = authoring.speed,
            };

            AddComponent(entity, appleComponent);
        }
    }
}

public struct AppleProperties : IComponentData
{
    public Entity ApplePrefab;

    public float Speed;
}