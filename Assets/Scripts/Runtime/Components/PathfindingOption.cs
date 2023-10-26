using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
namespace MyVampireSurvivor.Components
{

    public struct PathfindingOption : IComponentData
    {
        public float findingInterval;
        public float lastFindingTime;
        public float stopDistance;
        public Entity targetEntity;
    }
}