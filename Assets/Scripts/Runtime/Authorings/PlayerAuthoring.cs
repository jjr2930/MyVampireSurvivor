using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using MyVampireSurvivor.Components;

namespace MyVampireSurvivor.Authorings
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public float movingSpeed;
        public Vector3 forward;
        public Vector3 right;
    }

    public class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerComponent()
            {
                movingSpeed = authoring.movingSpeed,
                forward = authoring.forward,
                right = authoring.right,
            });
        }
    }
}