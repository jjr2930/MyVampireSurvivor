using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyVampireSurvivor
{
    [Flags]
    public enum ObjectTag
    {
        Player = 1 << 0,
        Enemy = 1 << 1
    }
}