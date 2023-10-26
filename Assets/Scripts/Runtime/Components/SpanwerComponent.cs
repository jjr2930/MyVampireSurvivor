using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace MyVampireSurvivor.Components
{
    public struct SpanwerComponent : IComponentData
    {
        public Entity prefab;
    }
}