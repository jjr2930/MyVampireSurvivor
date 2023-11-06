using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Entities;
using UnityEngine;

namespace MyVampireSurvivor.Components
{
    public struct EnemyMovementComponent : IComponentData
    {
        public float movingSpeed;
        public float stopDistance;
        public float sqrStopDistance;
    }
}