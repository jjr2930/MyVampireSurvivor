using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using MyVampireSurvivor.Components;

namespace MyVampireSurvivor.Authorings
{
    public class MainCamera : MonoBehaviour
    {
        public float distance;
        public Vector3 direction;

        static MainCamera instance;
        public static MainCamera Instance { get => instance; }

        private void Awake()
        {
            instance = this;
        }
    }
}