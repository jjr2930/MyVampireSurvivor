using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace MyVampireSurvivor.Components
{
    public partial struct PathfindingInfo : IComponentData
    {
        /// <summary>
        /// 몇 초에 한 번씩 갱신할지
        /// </summary>
        public float interval;
        public float lastTime;
        public Entity entity;
    }
}