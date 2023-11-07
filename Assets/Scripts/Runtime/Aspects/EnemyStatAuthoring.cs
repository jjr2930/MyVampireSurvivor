using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;
using MyVampireSurvivor.Components;

namespace MyVampireSurvivor.Authorings
{
    public class EnemyStatAuthoring : MonoBehaviour
    {
        public int startHp = 3;
    }

    public class EnemyStatBaker : Baker<EnemyStatAuthoring>
    {
        public override void Bake(EnemyStatAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EnemyStat()
            {
                startHP = authoring.startHp,
                currentHp = authoring.startHp
            });
        }
    }
}
