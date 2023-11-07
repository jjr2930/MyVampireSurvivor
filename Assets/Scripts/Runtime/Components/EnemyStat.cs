using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace MyVampireSurvivor.Components
{
    public struct EnemyStat : IComponentData
    {
        public int startHP;
        public int currentHp;
    }
}