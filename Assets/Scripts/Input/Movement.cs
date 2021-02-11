using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
[GenerateAuthoringComponent]
public struct Movement : IComponentData
{
    public float2 Value;

    public static implicit operator Movement(float2 val) => new Movement { Value = val };
}
