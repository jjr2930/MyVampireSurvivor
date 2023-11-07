using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using Unity.Transforms;

namespace MyVampireSurvivor.Systems
{
    public partial struct TargetSearchingSystem : ISystem
    {
        void OnUpdate(ref SystemState state) 
        {
            foreach (var (localTransform, localToWorld) in SystemAPI.Query<RefRO<LocalTransform>, RefRO<LocalToWorld>>())
            {
                var myWorldPosition = localToWorld.ValueRO.Position;


            }
        }
    }
}
