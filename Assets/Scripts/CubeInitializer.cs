using System;
using UnityEngine;

public class CubeInitializer : MonoBehaviour
{
    public void Initialize(Action<Cube> onClicking)
    {
        Cube[] cubes = GetComponentsInChildren<Cube>(true);

        foreach (Cube cube in cubes)
            cube.Clicking += onClicking;
    }
}