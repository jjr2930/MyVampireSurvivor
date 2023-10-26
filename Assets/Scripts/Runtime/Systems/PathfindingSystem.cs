using MyVampireSurvivor.Components;
using Pathfinding.Aspects;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace MyVampireSurvivor.Systems
{
    [UpdateAfter(typeof(Spawner))]
    public partial struct PathfindingSystem : ISystem
    {
        public void OnUpdate(ref SystemState state) 
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerComponent>();
            var playerLocaltoWorld = SystemAPI.GetComponent<LocalToWorld>(playerEntity);
            var elapsedTime = SystemAPI.Time.ElapsedTime;

            foreach (var ( localToWorld, pathfindingOption ) in SystemAPI.Query<RefRW<LocalToWorld>, RefRW<PathfindingOption>>())
            {
                var lastTime = pathfindingOption.ValueRO.lastTime;
                var shouldRefresh = elapsedTime - lastTime >= pathfindingOption.ValueRO.lastTime;
                if (shouldRefresh)
                {
                    var path = SystemAPI.GetAspect<PathfinderAspect>(pathfindingOption.ValueRO.entity);
                    path.FindPath(localToWorld.ValueRO.Position, playerLocaltoWorld.Position);
                    pathfindingOption.ValueRW.lastTime = (float)elapsedTime;
                }                
            }    
        }
    }
}