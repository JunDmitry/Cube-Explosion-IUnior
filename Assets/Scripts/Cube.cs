using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _splitChance;

    public float SplitChance => _splitChance;

    public void Initialize(float splitChance)
    {
        _splitChance = splitChance;
    }
}