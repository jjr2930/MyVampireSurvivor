using MyVampireSurvivor.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

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
            //var targetEntity = projectileComponent.ValueRO.targetEntity;
            //var targetLocalToWorldLookUp = projectileComponent.ValueRO.targetLocalToWorldReference;
            //var targetWorldPosition = targetLocalToWorldLookUp.ValueRO.Position;
            //var projectileMovementSpeed = projectileComponent.ValueRO.movementSpeed;
            //var localToWorldMatrix = projectileLocalToWorld.ValueRO.Value;
            //var projectileWorldPosition = projectileLocalToWorld.ValueRO.Position;

            //var worldToLocalMatrix = math.inverse(localToWorldMatrix);
            //var direction = targetWorldPosition - projectileWorldPosition;
            //direction = math.normalize(direction);
            //var projectileNextWorldPosition = projectileWorldPosition + direction * projectileMovementSpeed * deltaTime;
            //var projectileNextLocalPosition = MathUtility.MultiplyWithPoint(worldToLocalMatrix, projectileNextWorldPosition);

            //projectileLocalTransform.ValueRW.Position = projectileNextLocalPosition;
        }
    }
}