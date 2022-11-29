using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxMover : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    private float speed = 1f;

    private void Update() {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * speed);
    }
}
