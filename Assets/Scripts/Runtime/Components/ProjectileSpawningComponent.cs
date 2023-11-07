using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace MyVampireSurvivor.Components
{
    public struct ProjectileSpawningComponent : IComponentData
    {
        public float spawningDelay;
        public double nextSpawnTime;
        public float targetSearchingRadius;
        public ObjectTag targetTag;
        public Entity projectilePrefab;
        public Entity target;
        //public ComponentLookup<LocalToWorld> targetLocalToWorldLookup;
        //public ComponentLookup<LocalTransform> targetLocalTransformLookup;
    }
}