using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeManager : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    private float timeSpeed = 1f;
    private void Update() {
        Time.timeScale = timeSpeed;
    }
}
