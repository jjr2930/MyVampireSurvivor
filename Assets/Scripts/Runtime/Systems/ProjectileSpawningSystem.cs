using MyVampireSurvivor.Aspects;
using MyVampireSurvivor.Components;
using System;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace MyVampireSurvivor.Systems
{
    //[BurstCompile]
    public partial struct ProjectileSpawningSystem : ISystem
    {
        //[BurstCompile]
        void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            var entities = state.EntityManager.GetAllEntities(Allocator.TempJob);

            foreach (var entityA in entities)
            {
                if (false == SystemAPI.HasComponent<ProjectileSpawningComponent>(entityA))
                    continue;
                
                if (SystemAPI.HasComponent<Prefab>(entityA))
                    continue;

                var myProjectileSpawningComponent = SystemAPI.GetComponentRW<ProjectileSpawningComponent>(entityA);
                var myWorldPosition = SystemAPI.GetComponent<LocalToWorld>(entityA).Position;
                var mySearchingRadius = myProjectileSpawningComponent.ValueRO.targetSearchingRadius;

                var minDistance = float.MaxValue;
                var minEntity = Entity.Null;

                foreach (var entityB in entities)
                {
                    if (entityA.Index == entityB.Index)
                        continue;

                    if (SystemAPI.HasComponent<Prefab>(entityB))
                        continue;

                    if (false == SystemAPI.HasComponent<ObjectTagComponent>(entityB))
                        continue;

                    var yourObjectTag = SystemAPI.GetComponent<ObjectTagComponent>(entityB);
                    var yourWorldPosition = SystemAPI.GetComponent<LocalToWorld>(entityB).Position;
                    var distance = math.distance(yourWorldPosition, myWorldPosition);

                    //거리보다 멀면 생략
                    if (distance >= mySearchingRadius)
                    {
                        continue; 
                    }

                    if (minDistance > distance)
                    {
                        minDistance = distance;
                        minEntity = entityB;
                    }
                }

                if(Entity.Null != minEntity)
                {
                    myProjectileSpawningComponent.ValueRW.target = minEntity;
                }
            }

            foreach (var (projectileSpawnComponent, localToWorld)
                in SystemAPI.Query<RefRW<ProjectileSpawningComponent>, RefRO<LocalToWorld>>())
            {
                var nextSpawnTime = projectileSpawnComponent.ValueRO.nextSpawnTime;
                if (SystemAPI.Time.ElapsedTime >= nextSpawnTime)
                {
                    var prefab = projectileSpawnComponent.ValueRO.projectilePrefab;
                    var newProjectile = ecb.Instantiate(prefab);
                    var spawnerWorldPosition = localToWorld.ValueRO.Position;
                    
                    //Debug.Log($"Spawner world Position : {spawnerWorldPosition}");

                    ecb.SetComponent(newProjectile, LocalTransform.FromPosition(spawnerWorldPosition));
                    ecb.SetComponent(newProjectile, new ProjectileComponent()
                    {
                        targetEntity = projectileSpawnComponent.ValueRO.target,
                        movementSpeed = 10f
                    });
                    projectileSpawnComponent.ValueRW.nextSpawnTime += projectileSpawnComponent.ValueRW.spawningDelay;
                }
            }
        }
    }
    [BurstCompile]
    public partial struct ProjectileTargetSearchingJob : IJobEntity
    {
        [BurstCompile]
        public void Execute()
        {

        }
    }

    [BurstCompile]
    public partial struct ProjectileSpawningJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter parallelWriter;

        [ReadOnly] public double elapsedTime;
        [ReadOnly] public EntityManager entityManger;

        [BurstCompile]
        public void Execute([ChunkIndexInQuery] int chunkIndex, ProjectileSpawningAspect aspect)
        {
            aspect.Spawn(chunkIndex, elapsedTime, ref parallelWriter, ref entityManger);
        }
    }
}