using MyVampireSurvivor.Aspects;
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
            //TODO: it should be to tag or name or something
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerComponent>();
            var playerLocaltoWorld = SystemAPI.GetComponent<LocalToWorld>(playerEntity);
            var targetPosition = playerLocaltoWorld.Position;
            var elapsedTime = SystemAPI.Time.ElapsedTime;

            foreach (var pathfindingAspect in SystemAPI.Query<PathfindingAspect>())
            {
                var pathfinderAspect = SystemAPI.GetAspect<PathfinderAspect>(pathfindingAspect.entity);
                pathfindingAspect.FindPath(in pathfinderAspect, in targetPosition, in elapsedTime);
            }    
        }
    }
}