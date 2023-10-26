using MyVampireSurvivor.Aspects;
using MyVampireSurvivor.Components;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
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

            new MovementJob
            {
                deltaTime = SystemAPI.Time.DeltaTime,
                playerWorldPosition = playerLocalToWorld.Value.c3
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct MovementJob : IJobEntity
    {
        [ReadOnly] public float4 playerWorldPosition;
        [ReadOnly] public float deltaTime;
        [BurstCompile]
        void Execute(EnemyMovementAspect enemyMovementAspect)
        {
            enemyMovementAspect.Move(deltaTime, in playerWorldPosition);
        }
    } 
}