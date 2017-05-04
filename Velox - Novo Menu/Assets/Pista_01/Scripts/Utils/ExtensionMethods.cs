using UnityEngine;
using System.Collections;
//
//https://forum.unity3d.com/threads/re-map-a-number-from-one-range-to-another.119437/

public static class ExtensionMethods
{
    public static float Remap(this float value, float to1, float from1, float to2, float from2)
    {
        return (to2 + (((from2 - to2) * (value - to1)) / (from1 - to1)));
    }
}
