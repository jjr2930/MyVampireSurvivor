using MyVampireSurvivor.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace MyVampireSurvivor.Aspects
{
    [BurstCompile]
    public readonly partial struct ProjectileMovementAspect : IAspect
    {
        public readonly Entity entity;
        
        readonly RefRW<ProjectileComponent> projectileComponent;
        readonly RefRW<LocalTransform> projectileLocalTransform;
        readonly RefRO<LocalToWorld> projectileLocalToWorld;

        [BurstCompile]
        public void Move( float deltaTime)
        {
            var targetEntity = projectileComponent.ValueRO.targetEntity;
            float3 targetWorldPosition = new float3(0f, 0f, 0f);
            if (Entity.Null == projectileComponent.ValueRO.targetEntity)
            {
                //Debug.Log("target is null");
                targetWorldPosition = new float3(255, 0, 0);
            }
            else
            {
                //var localToWorld = projectileComponent.ValueRO.localToWorldLookup.GetRefRO(targetEntity);
                //targetWorldPosition = localToWorld.ValueRO.Position; 
            }

            var projectileMovementSpeed = projectileComponent.ValueRO.movementSpeed;
            var localToWorldMatrix = projectileLocalToWorld.ValueRO.Value;
            var projectileWorldPosition = projectileLocalToWorld.ValueRO.Position;
            Debug.Log($"projectile world position : {projectileWorldPosition}");

            return;
            var worldToLocalMatrix = math.inverse(localToWorldMatrix);
            var direction = targetWorldPosition - projectileWorldPosition;
            direction = math.normalize(direction);
            var projectileNextWorldPosition = projectileWorldPosition + (direction * projectileMovementSpeed * deltaTime);
            var projectileNextLocalPosition = MathUtility.MultiplyWithPoint(worldToLocalMatrix, projectileNextWorldPosition);

            projectileLocalTransform.ValueRW.Position = projectileNextLocalPosition;
        }
    }
}