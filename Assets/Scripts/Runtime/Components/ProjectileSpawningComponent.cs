using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace MyVampireSurvivor.Components
{
    public struct ProjectileSpawningComponent : IComponentData
    {
        public float spawningDelay;
        public double nextSpawnTime;
        public Entity projectilePrefab;
    }
}