using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using MyVampireSurvivor.Components;

namespace MyVampireSurvivor.Authorings
{
    public class ProjectileSpawningAuthoring : MonoBehaviour
    {
        public float spawnDelay = 0.1f;
        public GameObject projectilePrefab = null;
        public ObjectTag targetTag;
        public float searchingRadius = 10f;
    }

    public class ProjectileSpawningBaker : Baker<ProjectileSpawningAuthoring>
    {
        public override void Bake(ProjectileSpawningAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ProjectileSpawningComponent()
            {
                spawningDelay = authoring.spawnDelay,
                nextSpawnTime = 0f,
                projectilePrefab = GetEntity(authoring.projectilePrefab, TransformUsageFlags.Dynamic),
                targetTag = authoring.targetTag,
                targetSearchingRadius = authoring.searchingRadius,
            });
        }
    }
}