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
        int spawned;
        Unity.Mathematics.Random random;
        public void OnCreate(ref SystemState state)
        {
            random = new Unity.Mathematics.Random(2484);

            spawned = 0;
        }

        
        public void OnUpdate(ref SystemState state)
        {
            if (spawned == 1)
                return;

            foreach (var spwaner in SystemAPI.Query<RefRW<SpanwerComponent>>())
            {
                var randomPoint = GetRandomPoint(new float3(0f, 1f, 0f), 20);
                Entity newEntity = state.EntityManager.Instantiate(spwaner.ValueRW.prefab);
                state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(randomPoint));

                spawned++;
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