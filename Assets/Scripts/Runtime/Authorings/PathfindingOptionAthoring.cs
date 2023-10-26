using MyVampireSurvivor.Components;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace MyVampireSurvivor.Authorings
{
    public class PathfindingOptionAthoring : MonoBehaviour
    {
        [Range(0.01f, 10f)]
        public float interval = 1;
    }

    public class PathfindingOptionBaker : Baker<PathfindingOptionAthoring>
    {
        public override void Bake(PathfindingOptionAthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PathfindingOption()
            {
                interval = authoring.interval,
                entity = entity
            }) ;
        }
    }
}