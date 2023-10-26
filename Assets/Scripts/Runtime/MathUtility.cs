using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
namespace MyVampireSurvivor
{
    [BurstCompile]
    public static class MathUtility 
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 MultiplyWithPoint(float4x4 matrix, float3 point)
        {
            float4 pointF4 = new float4(point.x, point.y, point.z, 1);
            float4 result = math.mul(pointF4, matrix);
            return result.xyz;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 MultiplyWithDirection(float4x4 matrix, float3 direction)
        {
            float4 directionF4 = new float4(direction.x, direction.y, direction.z, 0);
            float4 result = math.mul(directionF4, matrix);
            return result.xyz;
        }
    }
}