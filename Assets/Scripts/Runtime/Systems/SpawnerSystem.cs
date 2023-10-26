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
        int count;
        public void OnCreate(ref SystemState state)
        {
            count = 0;
        }
        public void OnUpdate(ref SystemState state)
        {
            foreach (var spanwer in SystemAPI.Query<RefRW<SpanwerComponent>>())
            {
                if(count < 500)
                {
                    for (int i = 0; i < 500; i++)
                    {
                        count++;
                        Entity newEntity = state.EntityManager.Instantiate(spanwer.ValueRW.prefab);
                        state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(new float3(0, 0, 0)));
                    }
                }
            }
        }
    }
}