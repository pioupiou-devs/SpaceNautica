using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeManager : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    private  float timeSpeed = 1f;

    [SerializeField, Range(0, 15)]
    private  int nbPlanetToGenerate = 1;
    public static int nbPlanetGenerated = 0;
    private void Update() {
        if(nbPlanetGenerated < nbPlanetToGenerate) {
            timeSpeed = 0f;
        }
        Time.timeScale = timeSpeed;
    }
}
