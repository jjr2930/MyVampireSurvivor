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
        

        public void Spawn([ChunkIndexInQuery] int chunkIndex, double elapsedTime, ref EntityCommandBuffer.ParallelWriter parallelWriter, ref EntityManager entityManger)
        {
            var nextSpawnTime = projectileSpawningComponent.ValueRO.nextSpawnTime;
            var delay = projectileSpawningComponent.ValueRO.spawningDelay;
            if (elapsedTime >= nextSpawnTime)
            {
                //var targetLocalTransformLookup = projectileSpawningComponent.ValueRO.targetLocalTransformLookup;
                //var targetLocalToWorldLookup = projectileSpawningComponent.ValueRO.targetLocalToWorldLookup;
                
                //projectileSpawningComponent.ValueRW.nextSpawnTime = elapsedTime + delay;
                //var prefab = projectileSpawningComponent.ValueRO.projectilePrefab;
                //var newProjectile = parallelWriter.Instantiate(chunkIndex, prefab);
                
                //parallelWriter.SetComponent(chunkIndex, newProjectile,
                //    LocalTransform.FromPosition(localTransform.ValueRO.Position));

                //parallelWriter.SetComponent(chunkIndex, newProjectile, new ProjectileComponent()
                //{
                //    targetEntity = projectileSpawningComponent.ValueRO.target,
                //    localToWorldLookup = targetLocalToWorldLookup,
                //    localTransformLookup = targetLocalTransformLookup,
                //    movementSpeed = 10f
                //});
            }
        }
    }
}