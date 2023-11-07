using MyVampireSurvivor.Aspects;
using MyVampireSurvivor.Components;
using System.Runtime;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace MyVampireSurvivor.Systems
{
    [UpdateAfter(typeof(ProjectileSpawningSystem))]
    //[BurstCompile]
    public partial struct ProjectileMovementSystem : ISystem
    {
       // [BurstCompile]
        void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (projectileComponent, projectileLocaltransform, projectileLocalToWorld, entity)
                in SystemAPI.Query< RefRW<ProjectileComponent>, RefRW<LocalTransform>, RefRW<LocalToWorld>>().WithEntityAccess())
            {
                var targetEntity = projectileComponent.ValueRO.targetEntity;
                float3 targetWorldPosition = new float3(0f, 0f, 0f);
                if (Entity.Null == projectileComponent.ValueRO.targetEntity
                    || false == SystemAPI.Exists(targetEntity))
                {
                    targetWorldPosition = new float3(255, 0, 0);
                }
                else
                {
                    var localToWorld = SystemAPI.GetComponent<LocalToWorld>(targetEntity);
                    targetWorldPosition = localToWorld.Position; 
                }

                var projectileMovementSpeed = projectileComponent.ValueRO.movementSpeed;
                var localToWorldMatrix = projectileLocalToWorld.ValueRO.Value;
                var projectileWorldPosition = projectileLocalToWorld.ValueRO.Position;
                //Debug.Log($"projectile world position : {projectileWorldPosition}");

                var worldToLocalMatrix = math.inverse(localToWorldMatrix);
                var direction = targetWorldPosition - projectileWorldPosition;
                direction = math.normalize(direction);
                var projectileNextWorldPosition = projectileWorldPosition + (direction * projectileMovementSpeed * SystemAPI.Time.DeltaTime);
                var projectileNextLocalPosition = MathUtility.MultiplyWithPoint(worldToLocalMatrix, projectileNextWorldPosition);

                projectileLocaltransform.ValueRW.Position = projectileNextLocalPosition;

                float sqrHitDistance = 0.01f * 0.01f;

                var distancesq = math.distancesq(projectileWorldPosition, targetWorldPosition);
                if(distancesq < sqrHitDistance) 
                {
                    ecb.DestroyEntity(entity);

                    if (SystemAPI.HasComponent<EnemyStat>(targetEntity))
                    {
                        var enemyStat = SystemAPI.GetComponentRW<EnemyStat>(targetEntity);
                        enemyStat.ValueRW.currentHp--;
                        if (0 >= enemyStat.ValueRW.currentHp)
                        {
                            ecb.DestroyEntity(targetEntity);
                        }
                    }   
                }
            }
        }
    }

    [BurstCompile]
    public partial struct ProjectileMovmentJob : IJobEntity
    {
        [ReadOnly] public float deltaTime;

        [BurstCompile]
        public void Execute(ProjectileMovementAspect aspect)
        {
            aspect.Move(deltaTime);
        }
    }
}