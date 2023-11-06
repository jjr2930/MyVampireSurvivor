using MyVampireSurvivor.Aspects;
using MyVampireSurvivor.Components;
using System.Runtime;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;

namespace MyVampireSurvivor.Systems
{
    [BurstCompile]
    public partial struct ProjectileMovingSystem : ISystem
    {
        [BurstCompile]
        void OnUpdate(ref SystemState state)
        {
            new ProjectileMovmentJob()
            {
                deltaTime = SystemAPI.Time.DeltaTime,
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct ProjectileMovmentJob : IJobEntity
    {
        [ReadOnly] public float deltaTime;

        [BurstCompile]
        public void Execute(ProjectileMovementAspect aspect)
        {
            aspect.Move(deltaTime);
        }
    }
}