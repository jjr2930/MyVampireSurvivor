using Unity.Entities;
using Unity.Transforms;
using MyVampireSurvivor.Components;
using Unity.Mathematics;
using Unity.Burst;
using Pathfinding.Components;

namespace MyVampireSurvivor.Aspects
{
    [BurstCompile]
    public readonly partial struct EnemyMovementAspect : IAspect
    {
        public readonly Entity entity;

        private readonly RefRW<LocalToWorld> enemyLocalToWorld;
        private readonly RefRW<LocalTransform> enemyLocalTransform;
        private readonly RefRO<EnemyComponent> enemyComponent;

        [BurstCompile]
        public void Move(float deltaTime, in float4 playerWorldPosition, in DynamicBuffer<PathBuffer> paths)
        {            
            var enemyWorldToLocal = math.inverse(enemyLocalToWorld.ValueRO.Value);
            var playerPositionRelatedEnmey = math.mul(enemyWorldToLocal, playerWorldPosition);
            var toPlayer = playerPositionRelatedEnmey - enemyLocalToWorld.ValueRO.Value.c3;

            math.normalize(toPlayer);
            enemyLocalTransform.ValueRW.Position += (toPlayer.xyz * enemyComponent.ValueRO.moveSpeed * deltaTime);
        }
    }
}