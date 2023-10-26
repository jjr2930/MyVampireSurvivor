using MyVampireSurvivor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace MyVampireSurvivor.Authorings
{
    public class SpawnerAthoring : MonoBehaviour
    {
        public GameObject prefab;
    }

    public class SpawnerBaker : Baker<SpawnerAthoring>
    {
        public override void Bake(SpawnerAthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new SpanwerComponent()
            {
                prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic)
            });
        }
    }
}
