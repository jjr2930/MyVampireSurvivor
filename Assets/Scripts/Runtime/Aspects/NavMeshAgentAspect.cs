using Unity.Entities;
using Unity.Transforms;
using MyVampireSurvivor.Components;
using Unity.Mathematics;
using Unity.Burst;
using Pathfinding.Components;
using UnityEngine;
using System.Text;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;

namespace MyVampireSurvivor.Aspects
{
    [BurstCompile]
    public readonly partial struct NavMeshAgentAspect : IAspect
    {
        public readonly Entity entity;

        private readonly RefRO<LocalToWorld> agentLocalToWorld;
        private readonly RefRW<LocalTransform> agentLocalTransform;
        private readonly RefRW<NavMeshAgentInfo> navMeshAgentInfo;
        private readonly DynamicBuffer<PathBuffer> paths;

        [BurstCompile]
        public void Move(float deltaTime, in float4 targetWorldPosition)
        {
            bool hasPath = paths.Length > 0;
            if (false == hasPath)
            {
                return;
            }

            var movingState = navMeshAgentInfo.ValueRW.state;
            switch (movingState)
            {
                case NavMeshAgentInfo.MovingState.Idle:
                case NavMeshAgentInfo.MovingState.Finsihed:
                    {
                        navMeshAgentInfo.ValueRW.state = NavMeshAgentInfo.MovingState.Moving;
                        //paths.RemoveAt(0);
                    }
                    break;

                case NavMeshAgentInfo.MovingState.Moving:
                    {
                        var offset = navMeshAgentInfo.ValueRO.offset;
                        var currentPosition = agentLocalToWorld.ValueRO.Position - offset;
                        var currentTargetPosition = paths[0];
                        var toTarget = currentTargetPosition.position - currentPosition;
                        var length = math.length(toTarget);
                        if (length <= navMeshAgentInfo.ValueRW.stopDistance)
                        {
                            paths.RemoveAt(0);
                            if (0 == paths.Length)
                            {
                                navMeshAgentInfo.ValueRW.state = NavMeshAgentInfo.MovingState.Finsihed;
                            }
                        }
                        else
                        {
                            toTarget = math.normalize(toTarget);
                            var moveSpeed = navMeshAgentInfo.ValueRO.moveSpeed;
                            var worldToLocal = math.inverse(agentLocalToWorld.ValueRO.Value);
                            var nextPosition = currentPosition + (toTarget * deltaTime * moveSpeed);
                            nextPosition += offset;
                            agentLocalTransform.ValueRW.Position = MathUtility.MultiplyWithPoint(worldToLocal, nextPosition);                            
                        }
                    }
                    break;
            }
        }
    }
}