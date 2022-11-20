using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Planet/Shape Settings")]
public class ShapeSettings : ScriptableObject
{
    [SerializeField]
    internal float planetRadius = 1;
    [SerializeField]
    internal NoiseLayer[] noiseLayers;

    [System.Serializable]
    public class NoiseLayer
    {
        [SerializeField]
        internal bool enabled = true;
        [SerializeField]
        internal bool useFirstLayerAsMask;
        [SerializeField]
        internal NoiseSettings noiseSettings;
    }
}
