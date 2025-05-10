using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _splitChance;

    public event Action<Cube> Clicking;

    public float SplitChance => _splitChance;

    public void Initialize(float splitChance)
    {
        _splitChance = splitChance;
    }

    public void InteractAfterClick()
    {
        Clicking?.Invoke(this);
    }

    private void OnDestroy()
    {
        Clicking = null;
    }
}