using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class RandomValue
{
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;

    public float MinValue => minValue;
    public float MaxValue => maxValue;

    public float GetRandomValue() => Random.Range(MinValue, MaxValue);
}
