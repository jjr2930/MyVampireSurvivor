using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
namespace MyVampireSurvivor.Components
{
    public partial struct ProjectileComponent : IComponentData
    {
        public float movementSpeed;
        public float guideValue;
        public Entity targetEntity;
        //public ComponentLookup<LocalTransform> lookup;
        //public RefRO<LocalTransform> targetLocalTransformReference;
        //public RefRO<LocalToWorld> targetLocalToWorldReference;
    }
}