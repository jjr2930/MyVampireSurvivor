using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace MyVampireSurvivor.Components
{
    public struct NavMeshAgentInfo : IComponentData
    {
        public enum MovingState
        {
            Idle,
            Moving,
            Finsihed,
        }
        public float moveSpeed;
        public float stopDistance;
        public float3 offset;
        public MovingState state;
    }
}