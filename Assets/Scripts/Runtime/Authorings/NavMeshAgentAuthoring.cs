using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using MyVampireSurvivor.Components;
using UnityEditor.PackageManager;

namespace MyVampireSurvivor.Authorings
{
	public class NavMeshAgentAuthoring : MonoBehaviour
	{
		public float moveSpeed;
		public Vector3 offset;
		public float stopDistance;
	}

	public class NavMeshAgentBaker : Baker<NavMeshAgentAuthoring>
	{
        public override void Bake(NavMeshAgentAuthoring authoring)
        {
			var entity = GetEntity(TransformUsageFlags.Dynamic);
			AddComponent(entity, new NavMeshAgentInfo()
			{
				moveSpeed = authoring.moveSpeed,
				stopDistance = authoring.stopDistance,
				offset = authoring.offset,
			}) ;
        }
    }
}