using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace MyVampireSurvivor.Components
{
    public struct EnemyComponent : IComponentData
    {
        public float moveSpeed;
        public float attackRate;
    }
}