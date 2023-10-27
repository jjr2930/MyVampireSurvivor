using MyVampireSurvivor.Components;
using Pathfinding.Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
namespace MyVampireSurvivor.Aspects
{
    //[BurstCompile]
    public readonly partial struct PathfindingAspect : IAspect
    {
        public readonly Entity entity;
        readonly RefRO<LocalToWorld> localToWorld;
        readonly RefRW<PathfindingInfo> pathfindingOption;
        readonly RefRW<NavMeshAgentInfo> movementInfo;

        //[BurstCompile]
        public void FindPath(in PathfinderAspect pathAspect, in float3 targetPosition, in double elapsedTime)
        {
            var lastTime = pathfindingOption.ValueRO.lastTime;
            var shouldRefresh = elapsedTime - lastTime >= pathfindingOption.ValueRO.interval;
            if (shouldRefresh)
            {
                pathAspect.FindPath(localToWorld.ValueRO.Position, targetPosition);
                pathfindingOption.ValueRW.lastTime = (float)elapsedTime;
            }
        }
    }
}