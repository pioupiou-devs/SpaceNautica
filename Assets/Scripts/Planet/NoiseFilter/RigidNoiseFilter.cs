using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilter : INoiseFilter
{
    private Noise noise = new Noise();
    private NoiseSettings.RigidNoiseSettings settings;

    public RigidNoiseFilter(NoiseSettings.RigidNoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;
        float weight = 1;

        for (int i = 0; i < settings.numLayers; i++)
        {
            // Calculate noise value
            float noise = Mathf.Pow(1- Mathf.Abs(this.noise.Evaluate(point * frequency + settings.center)),2) * weight;
            noiseValue += noise * amplitude;

            // Prepare for next iteration
            weight = Mathf.Clamp01(noise * settings.weightMultiplier);
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }

        // Clamp noise value with min value defined in settings
        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);

        return noiseValue * settings.strength;
    }
}
