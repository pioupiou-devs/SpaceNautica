using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMax
{
    [SerializeField]
    internal float Min { get; private set; }
    [SerializeField]
    internal float Max { get; private set; }

    internal float Mean { get { return Sum / Count; } }
    private float Sum {  get; set; }
    private float Count { get; set; }

    public MinMax()
    {
        Min = float.MaxValue;
        Max = float.MinValue;
        Sum = 0;
    }

    public void AddValue(float value)
    {
        Sum += value;
        Count++;
        Max = (value > Max) ? value : Max;
        Min = (value < Min) ? value : Min;
    }

    public override string ToString()
    {
        return $"Min: {Min}, Max: {Max}";
    }
}
