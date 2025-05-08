using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _splitChance;
    [SerializeField] private Splitter _splitter;

    public float SplitChance => _splitChance;

    public void Initialize(Splitter splitter, float splitChance)
    {
        _splitter = splitter;
        _splitChance = splitChance;
    }

    private void OnMouseUpAsButton()
    {
        if (_splitter == null)
            return;

        if (IsSplit())
            _splitter.Split(this);

        Destroy(gameObject);
    }

    private bool IsSplit()
    {
        return _splitChance >= UnityEngine.Random.value;
    }
}