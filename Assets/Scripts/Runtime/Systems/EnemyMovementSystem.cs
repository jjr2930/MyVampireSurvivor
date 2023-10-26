using MyVampireSurvivor.Aspects;
using MyVampireSurvivor.Components;
using Pathfinding.Components;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

namespace MyVampireSurvivor.Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(Spawner))]
    public partial struct EnemyMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state) 
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerComponent>();
            var playerLocalToWorld = SystemAPI.GetComponent<LocalToWorld>(playerEntity);

            //복사가 너무 일어나는데... reference로 하는 방법은 없을까?
            NativeParallelHashMap<Entity, BufferLookup<PathBuffer>> pathsByEntity
                = new NativeParallelHashMap<Entity, BufferLookup<PathBuffer>>(128, Allocator.TempJob);

            foreach (var enemyComponent in SystemAPI.Query<RefRO<EnemyComponent>>())
            {
                var enemyEntity = enemyComponent.ValueRO.entity;
                var pathsBufferLookup = SystemAPI.GetBufferLookup<PathBuffer>();
                pathsBufferLookup.Update(ref state);
                pathsByEntity.Add(enemyEntity, pathsBufferLookup);
            }

            new MovementJob
            {
                deltaTime = SystemAPI.Time.DeltaTime,
                playerWorldPosition = playerLocalToWorld.Value.c3,
                pathsByEntity = pathsByEntity
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct MovementJob : IJobEntity
    {
        [ReadOnly] public float4 playerWorldPosition;
        [ReadOnly] public float deltaTime;
        [ReadOnly] public NativeParallelHashMap<Entity, BufferLookup<PathBuffer>> pathsByEntity;
        
        void Execute(EnemyMovementAspect enemyMovementAspect )
        {
            var entity = enemyMovementAspect.entity;
            BufferLookup<PathBuffer> pathsBuffer;
            if (pathsByEntity.TryGetValue(entity, out pathsBuffer))
            {
                pathsBuffer]
            }
        }
    } 
}