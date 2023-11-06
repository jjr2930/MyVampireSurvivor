using Mono.Cecil.Cil;
using MyVampireSurvivor.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace MyVampireSurvivor
{
    [BurstCompile]
    public readonly partial struct EnemyMovementAspect : IAspect
    {
        private readonly RefRO<EnemyMovementComponent> movementComponent;
        private readonly RefRW<LocalTransform> localTransform;
        private readonly RefRO<LocalToWorld> localToWorld;

        [BurstCompile]
        public void Move(ref float3 playerWorldPosition, float deltaTime)
        {
            var enemyWorldPosition = localToWorld.ValueRO.Position;
            var sqrDistance = math.distancesq(playerWorldPosition.xyz, enemyWorldPosition);
            if(sqrDistance <= movementComponent.ValueRO.sqrStopDistance)
            {
                return;
            }

            var enemyMovementSpeed = movementComponent.ValueRO.movingSpeed;
            var direction = playerWorldPosition - enemyWorldPosition;
            var distance = math.length(direction);
            direction = math.normalize(direction);
            var velocity = direction * enemyMovementSpeed * deltaTime;
            var velocityLength = math.length(velocity);
            if (distance <= velocityLength)
            {
                enemyWorldPosition = playerWorldPosition;
            }
            else
            {
                enemyWorldPosition += velocity;
            }

            var worldToLocal = math.inverse(localToWorld.ValueRO.Value);
            var enemyNextLocalPosition = MathUtility.MultiplyWithPoint(worldToLocal, enemyWorldPosition);
            localTransform.ValueRW.Position = enemyNextLocalPosition;
        }
    }
}