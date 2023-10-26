using MyVampireSurvivor.Components;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace MyVampireSurvivor.Systems
{
    public partial struct PlayerMovementSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerComponent>();
            var playerComponent = SystemAPI.GetComponent<PlayerComponent>(playerEntity);
            var playerLocalTansform = SystemAPI.GetComponentRW<LocalTransform>(playerEntity);

            Debug.Assert(false == Mathf.Approximately(0f, math.length(playerComponent.forward)),
                "forwardDirection is zero");

            Debug.Assert(false == Mathf.Approximately(0f, math.length(playerComponent.right)),
                "rightDirection is zero");

            float3 direction = float3.zero;

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.W))
            {
                direction += playerComponent.forward;
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.S))
            {
                direction -= playerComponent.forward;
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A))
            {
                direction -= playerComponent.right;
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D))
            {
                direction += playerComponent.right;
            }

            math.normalize(direction);
            direction *= playerComponent.movingSpeed * SystemAPI.Time.DeltaTime;
            playerLocalTansform.ValueRW.Position += direction;
        }
    }
}