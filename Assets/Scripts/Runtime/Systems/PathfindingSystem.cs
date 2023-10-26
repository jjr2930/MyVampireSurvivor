using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.AI;

namespace MyVampireSurvivor.Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(Spawner))]
    public partial struct PathfindingSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        { 

        }
    }

    [BurstCompile]
    public partial struct PathfindingJob : IJobEntity
    {
        [ReadOnly] public LocalToWorld targetLocalToWorldTransform;
        [ReadOnly] public float elapsedTime;

        [BurstCompile]
        void Execute(in LocalTransform localTransform)
        {

        }
    }
}