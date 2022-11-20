using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSettings
{
    internal ColorSettings colorSettings;
    internal ShapeSettings shapeSettings;

    public ProceduralSettings()
    {
        colorSettings = GenerateRandomColorGradient();
        shapeSettings = GenerateRandomShape();
    }

    private ColorSettings GenerateRandomColorGradient()
    {
        ColorSettings colorSettings = new ColorSettings();
        Gradient gradient = new Gradient();
        gradient.mode = GradientMode.Blend;
        GradientColorKey[] colorKeys = new GradientColorKey[Random.Range(5, 8)];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[Random.Range(5, 8)];
        for (int i = 0; i < colorKeys.Length; i++)
        {
            colorKeys[i] = new GradientColorKey(Random.ColorHSV(), Random.Range(0f, 1f));
        }
        for (int i = 0; i < alphaKeys.Length; i++)
        {
            alphaKeys[i] = new GradientAlphaKey(Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
        gradient.SetKeys(colorKeys, alphaKeys);
        colorSettings.gradient = gradient;
        return colorSettings;
    }

    private ShapeSettings GenerateRandomShape()
    {
        ShapeSettings shapeSettings = new ShapeSettings();
        shapeSettings.planetRadius = Random.Range(2f, 5f);
        shapeSettings.noiseLayers = new ShapeSettings.NoiseLayer[Random.Range(3, 5)];
        for (int i = 0; i < shapeSettings.noiseLayers.Length; i++)
        {
            shapeSettings.noiseLayers[i] = new ShapeSettings.NoiseLayer();
            
            if(i != 0)
                shapeSettings.noiseLayers[i].useFirstLayerAsMask = true;

            shapeSettings.noiseLayers[i].enabled = true;
            shapeSettings.noiseLayers[i].noiseSettings = new NoiseSettings();
            shapeSettings.noiseLayers[i].noiseSettings.filterType = (NoiseSettings.FilterType)Random.Range(0, 2);
            if (shapeSettings.noiseLayers[i].noiseSettings.filterType == NoiseSettings.FilterType.Simple)
            {
                shapeSettings.noiseLayers[i].noiseSettings.simpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();
                shapeSettings.noiseLayers[i].noiseSettings.simpleNoiseSettings.strength = Random.Range(0f, 1f);
                shapeSettings.noiseLayers[i].noiseSettings.simpleNoiseSettings.numLayers = Random.Range(1, 8);
                shapeSettings.noiseLayers[i].noiseSettings.simpleNoiseSettings.baseRoughness = Random.Range(0f, 1f);
                shapeSettings.noiseLayers[i].noiseSettings.simpleNoiseSettings.roughness = Random.Range(0f, 1f);
                shapeSettings.noiseLayers[i].noiseSettings.simpleNoiseSettings.persistence = Random.Range(0f, 1f);
                shapeSettings.noiseLayers[i].noiseSettings.simpleNoiseSettings.center = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                shapeSettings.noiseLayers[i].noiseSettings.simpleNoiseSettings.minValue = Random.Range(0f, 1f);
            }
            else
            {
                shapeSettings.noiseLayers[i].noiseSettings.rigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();
                shapeSettings.noiseLayers[i].noiseSettings.rigidNoiseSettings.strength = Random.Range(0f, 1f);
                shapeSettings.noiseLayers[i].noiseSettings.rigidNoiseSettings.numLayers = Random.Range(1, 8);
                shapeSettings.noiseLayers[i].noiseSettings.rigidNoiseSettings.baseRoughness = Random.Range(0f, 1f);
                shapeSettings.noiseLayers[i].noiseSettings.rigidNoiseSettings.roughness = Random.Range(0f, 1f);
                shapeSettings.noiseLayers[i].noiseSettings.rigidNoiseSettings.persistence = Random.Range(0f, 1f);
                shapeSettings.noiseLayers[i].noiseSettings.rigidNoiseSettings.center = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                shapeSettings.noiseLayers[i].noiseSettings.rigidNoiseSettings.minValue = Random.Range(0f, 1f);
                shapeSettings.noiseLayers[i].noiseSettings.rigidNoiseSettings.weightMultiplier = Random.Range(0f, 1f);
            }
        }
        return shapeSettings;
    }
}
