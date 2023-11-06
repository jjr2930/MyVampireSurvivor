using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyVampireSurvivor.Components;
using Unity.Entities;

namespace MyVampireSurvivor.Authorings
{
    public class ProjectileAuthoring : MonoBehaviour
    {
        public float movingSpeed = 1f;
        [Range(0f,1f)]
        public float guideValue = 1f;
    }
     
    public class ProjectileAuthoringBaker : Baker<ProjectileAuthoring>
    {
        public override void Bake(ProjectileAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ProjectileComponent()
            {
                movementSpeed = authoring.movingSpeed,
                guideValue = authoring.guideValue
            });
        }
    }
}