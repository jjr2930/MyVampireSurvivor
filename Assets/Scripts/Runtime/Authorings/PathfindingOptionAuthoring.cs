using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using MyVampireSurvivor.Components;

namespace MyVampireSurvivor.Authorings
{
    public class PathfindingOptionAuthoring : MonoBehaviour
    {
        public float findingInterval;
        public float lastFindingTime;
        public float stopDistance;
    }

    public class PathfindingOptionBaker : Baker<PathfindingOptionAuthoring>
    {
        public override void Bake(PathfindingOptionAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PathfindingOption()
            {
                findingInterval = authoring.findingInterval,
                lastFindingTime = authoring.lastFindingTime,
                stopDistance = authoring.stopDistance
            });
        }
    }
}