using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator
{
    internal ColorSettings colorSettings;
    internal Planet planet;
    internal Texture2D texture;
    internal const int textureResolution = 50;

    public void UpdateSettings(ColorSettings colorSettings, Planet planet)
    {
        this.colorSettings = colorSettings;
        this.planet = planet;
        if (texture == null)
            texture = new Texture2D(textureResolution, 1);
    }

    public void UpdateElevation(MinMax elevationMinMax)
    {
        planet.planetMaterial.SetVector("_elevationMinMax", new Vector2(elevationMinMax.Min, elevationMinMax.Max));
        planet.planetMaterial.SetFloat("_elevationMean", elevationMinMax.Mean);
        planet.atmosphereMaterial.SetFloat("_elevationMean", elevationMinMax.Mean);
    }

    public void UpdateColors()
    {
        Color[] colors = new Color[textureResolution];
        for (int i = 0; i < textureResolution; i++)
        {
            colors[i] = colorSettings.gradient.Evaluate(i / (textureResolution - 1f));
        }
        texture.SetPixels(colors);
        texture.Apply();

        planet.planetMaterial.SetTexture("_texture", texture);
        planet.atmosphereMaterial.SetTexture("_texture", texture);
    }
}
