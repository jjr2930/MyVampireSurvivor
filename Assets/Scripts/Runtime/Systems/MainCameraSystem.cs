using MyVampireSurvivor.Authorings;
using MyVampireSurvivor.Components;
using System;
using Unity.Assertions;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace MyVampireSurvivor.Systems
{
    public partial struct MainCameraSystem : ISystem
    {
        void OnCreate(ref SystemState state) 
        {
            state.RequireForUpdate<PlayerComponent>();
        }

        void OnUpdate(ref SystemState state)
        {
            if (null == Camera.main)
            {
                throw new InvalidOperationException("main camera is null");
            }

            var playerEntity = SystemAPI.GetSingletonEntity<PlayerComponent>();
            var playerLocalToWorld = SystemAPI.GetComponent<LocalToWorld>(playerEntity);

            float3 direction = MainCamera.Instance.direction;
            Debug.Assert(false == Mathf.Approximately(0f, math.length(direction)), "MainCamera direction is zero");

            var nextCameraPosition = playerLocalToWorld.Position + direction * MainCamera.Instance.distance;

            var cameraTransform = MainCamera.Instance.transform;
            cameraTransform.position = nextCameraPosition;
            cameraTransform.LookAt(playerLocalToWorld.Position);
        }
    }
}