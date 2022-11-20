using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Planet/Color Settings")]
public class ColorSettings : ScriptableObject
{
    [SerializeField]
    internal Gradient gradient = new Gradient() {
        colorKeys = new GradientColorKey[] {
            new GradientColorKey(Color.white, 0f),
            new GradientColorKey(Color.white, 1f)
        },
        alphaKeys = new GradientAlphaKey[] {
            new GradientAlphaKey(1f, 0f),
            new GradientAlphaKey(1f, 1f)
        }
    };
}