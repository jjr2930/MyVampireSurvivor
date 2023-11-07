using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
namespace MyVampireSurvivor.Components
{
    public struct ObjectTagComponent : IComponentData
    {
        public ObjectTag tag;
    }
}