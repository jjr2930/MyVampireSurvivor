using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using MyVampireSurvivor.Components;

namespace MyVampireSurvivor.Authorings
{
	public class EnemyAuthoring : MonoBehaviour
	{
		public float moveSpeed;
		public float attackSpeed;
	}

	public class EnemyBaker : Baker<EnemyAuthoring>
	{
        public override void Bake(EnemyAuthoring authoring)
        {
			var entity = GetEntity(TransformUsageFlags.Dynamic);
			AddComponent(entity, new EnemyComponent()
			{
				moveSpeed = authoring.moveSpeed,
				attackRate = authoring.attackSpeed
			});
        }
    }
}