using MyVampireSurvivor.Components;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace MyVampireSurvivor.Systems
{
    public partial struct Spawner : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var spanwer in SystemAPI.Query<RefRW<SpanwerComponent>>())
            {
                if( SystemAPI.Time.ElapsedTime % 1.0 <= 0.01)
                {
                    Entity newEntity = state.EntityManager.Instantiate(spanwer.ValueRW.prefab);
                    state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(new float3(0, 0, 0)));
                }
            }
        }
    }
}