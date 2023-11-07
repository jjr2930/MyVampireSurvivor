using MyVampireSurvivor.Components;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
namespace MyVampireSurvivor.Authorings
{

    public class ObjectTagAuthoring : MonoBehaviour
    {
        public ObjectTag tag;
    }

    public class ObjectTagAuthoringBaker : Baker<ObjectTagAuthoring>
    {
        public override void Bake(ObjectTagAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ObjectTagComponent()
            {
                tag = authoring.tag
            });
        }
    }
}