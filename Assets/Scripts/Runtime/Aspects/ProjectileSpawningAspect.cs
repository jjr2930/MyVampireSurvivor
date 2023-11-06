using MyVampireSurvivor.Components;
using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;

namespace MyVampireSurvivor.Aspects
{
    [BurstCompile]
    public readonly partial struct ProjectileSpawningAspect : IAspect
    {
        public readonly Entity entity;
        readonly RefRW<ProjectileSpawningComponent> projectileSpawningComponent;
        readonly RefRO<LocalTransform> localTransform;

        public void Spawn([ChunkIndexInQuery] int chunkIndex, double elapsedTime, ref EntityCommandBuffer.ParallelWriter parallelWriter)
        {
            var nextSpawnTime = projectileSpawningComponent.ValueRO.nextSpawnTime;
            var delay = projectileSpawningComponent.ValueRO.spawningDelay;
            if (elapsedTime >= nextSpawnTime)
            {
                projectileSpawningComponent.ValueRW.nextSpawnTime = elapsedTime + delay;
                var prefab = projectileSpawningComponent.ValueRO.projectilePrefab;
                var newProjectile = parallelWriter.Instantiate(chunkIndex, prefab);
                parallelWriter.SetComponent(chunkIndex, newProjectile,
                    LocalTransform.FromPosition(localTransform.ValueRO.Position));
            }
        }
    }
}