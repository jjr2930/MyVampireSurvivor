using MyVampireSurvivor.Aspects;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace MyVampireSurvivor.Systems
{
    [BurstCompile]
    public partial struct ProjectileSpawningSystem : ISystem
    {
        [BurstCompile]
        void OnUpdate(ref SystemState state) 
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            var parallelWriter = ecb.AsParallelWriter();

            new ProjectileSpawningJob()
            {
                parallelWriter = parallelWriter,
                elapsedTime = SystemAPI.Time.ElapsedTime,
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct ProjectileSpawningJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter parallelWriter;

        [ReadOnly] public double elapsedTime;

        [BurstCompile]
        public void Execute([ChunkIndexInQuery] int chunkIndex, ProjectileSpawningAspect aspect)
        {
            aspect.Spawn(chunkIndex, elapsedTime, ref parallelWriter);
        }
    }
}