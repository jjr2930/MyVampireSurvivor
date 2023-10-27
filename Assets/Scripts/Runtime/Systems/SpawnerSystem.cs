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
        Unity.Mathematics.Random random;
        public void OnCreate(ref SystemState state)
        {
            count = 0;
            random = new Unity.Mathematics.Random(2484);
        }
        public void OnUpdate(ref SystemState state)
        {
            foreach (var spwaner in SystemAPI.Query<RefRW<SpanwerComponent>>())
            {
                if(count < 10000)
                {
                    for(int i = 0; i< 10000; i++) 
                    {
                        count++;

                        var randomPoint = GetRandomPoint(new float3(0f, 1f, 0f), 20);
                        Entity newEntity = state.EntityManager.Instantiate(spwaner.ValueRW.prefab);
                        state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(randomPoint));
                    }
                }
            }
        }

        float3 GetRandomPoint(float3 center, float radius)
        {
            var degree = random.NextFloat(0f, 360f);
            var x = math.sin(degree) * radius;
            var z = math.cos(degree) * radius;
            var p = center;
            p.x += x;
            p.z += z;

            return p;
        }
    }
}