using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using MyVampireSurvivor.Components;

namespace MyVampireSurvivor.Authorings
{
    public class EnemyMovementAuthoring : MonoBehaviour
    {
        public float movementSpeed = 1f;
        public float stopDistance = 0.1f;
    }

    public class EnemyMovementBaker : Baker<EnemyMovementAuthoring>
    {
        public override void Bake(EnemyMovementAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EnemyMovementComponent()
            {
                movingSpeed = authoring.movementSpeed,
                stopDistance = authoring.stopDistance,
                sqrStopDistance = authoring.stopDistance * authoring.stopDistance
            });
        }
    }
}