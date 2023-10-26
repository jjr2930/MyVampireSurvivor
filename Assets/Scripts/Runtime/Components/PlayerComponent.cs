using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace MyVampireSurvivor.Components
{
    public struct PlayerComponent : IComponentData
    {
        public float movingSpeed;
        public float3 forward;
        public float3 right;
    }
}